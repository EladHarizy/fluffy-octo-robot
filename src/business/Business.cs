using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using business.Extensions;
using data;
using Lib.Config;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace business {
	internal class Business : IBusiness {
		private IData data = DataFactory.Data;

		private TUser User<TUser>(Email email) where TUser : User {
			try {
				return data.GetAccessor<TUser>().All.First(p => p.Email == email);
			} catch (InvalidOperationException ex) {
				throw new InexistentEmailException(email, ex);
			}
		}

		public void AddGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Add(guest_request);
		}

		public void EditGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Edit(guest_request);
		}

		public void DeleteGuestRequest(GuestRequest guest_request) {
			IList<Order> orders = OrdersOf(guest_request).ToList();
			for (int i = 0; i < orders.Count; ++i) {
				if (orders[i].OrderStatus == "Confirmed") {
					throw new DeletingGuestRequestWithConfirmedOrderException(guest_request, orders[i]);
				}
			}
			for (int i = 0; i < orders.Count; ++i) {
				orders[i].GuestRequest = null;
				EditOrder(orders[i], "Cancelled");
			}
			data.GuestRequest.Remove(guest_request.ID);
		}

		public void AddUnit(Unit unit) {
			data.Unit.Add(unit);
		}

		public void DeleteUnit(Unit unit) {
			IList<Order> orders = OrdersOf(unit).ToList();
			for (int i = 0; i < orders.Count; ++i) {
				if (orders[i].OrderStatus == "Confirmed") {
					throw new DeletingUnitWithConfirmedOrderException(unit, orders[i]);
				}
			}
			for (int i = 0; i < orders.Count; ++i) {
				orders[i].Unit = null;
				EditOrder(orders[i], "Cancelled");
			}
			data.Unit.Remove(unit.ID);
		}

		public void EditUnit(Unit unit) {
			data.Unit.Edit(unit);
		}

		public void AddOrder(Order order) {
			if (order.OrderStatus != "Not addressed") {
				throw new InitialStatusException(order.OrderStatus);
			}
			if (!order.Unit.Available(order.GuestRequest.StartDate, order.GuestRequest.Duration)) {
				throw new UnitUnavailableException(order.Unit, order.GuestRequest.StartDate, order.GuestRequest.Duration);
			}
			data.Order.Add(order);
		}

		// Edits the given order and returns a collection of all affected orders
		public IEnumerable<Order> EditOrder(Order order, Order.Status status) {
			ICollection<Order> affected_orders = new List<Order>();
			if (order.OrderStatus != status) {
				// Order is already cancelled
				if (order.OrderStatus == "Cancelled" || order.OrderStatus == "Confirmed") {
					throw new OrderStatusChangeException(order, "Error: Could not change the status because the order is already closed (confirmed or cancelled).");
				}
				// Order is being opened
				if (status == "Not addressed") {
					throw new OrderStatusChangeException(order, "Error: An order cannot be reopened.");
				}

				affected_orders.Add(order);

				// Order is being confirmed
				if (status == "Confirmed") {
					if (order.Unit == null) {
						throw new OrderStatusChangeException(order, "Error: Cannot confirm this order because the hosting unit it is referring to has been deleted.");
					}
					if (order.GuestRequest == null) {
						throw new OrderStatusChangeException(order, "Error: Cannot confirm this order because the guest request it is referring to has been deleted.");
					}
					if (!order.Unit.Host.DebitAuthorisation) {
						throw new OrderStatusChangeException(order, "Error: Cannot close the order because the host does not have debit authorisation.");
					}
					try {
						int fee = Config.FeePerDay * order.GuestRequest.Duration;
						order.Unit.Bookings.Add(new Unit.Calendar.Booking(order.GuestRequest.StartDate, order.GuestRequest.Duration));
					} catch (BookingOverlapException ex) {
						throw new OrderStatusChangeException(order, "Error: Cannot confirm this order because the your unit is occupied on the requested dates.", ex);
					}
					data.Unit.Edit(order.Unit); // Update the unit's calendar in the database

					// Close all other open orders on this guest request or that overlap the same hosting unit
					List<Order> orders_to_close = data.Order.All.Where(order1 =>
						order.ID != order1.ID // It's a different order
						&& (order1.OrderStatus == "Not addresses" || order1.OrderStatus == "Sent email") // The order is open
						&& (
							order.GuestRequest.ID == order1.GuestRequest.ID // The order is on the same guest request as the confirmed one
							|| (order.Unit.ID == order1.Unit.ID && order.Overlaps(order1)) // The orders overlap on the same unit
						)
					).ToList();
					for (int i = 0; i < orders_to_close.Count; ++i) {
						EditOrder(orders_to_close[i], "Cancelled");
						affected_orders.Add(orders_to_close[i]);
					}
				} else if (status == "Sent email") {
					try {
						new InvitationSender(order).Send();
					} catch (Exception ex) when(ex is InvalidOperationException || ex is ObjectDisposedException || ex is SmtpException || ex is SmtpFailedRecipientException || ex is SmtpFailedRecipientsException) {
						throw new OrderStatusChangeException(order, "Error: Could not send invitation to the guest. Please check your internet connection.", ex);
					}
				}
			}
			order.OrderStatus = status;
			data.Order.Edit(order);
			return affected_orders;
		}

		public Admin Admin(Email email) {
			return User<Admin>(email);
		}

		public Guest Guest(Email email) {
			return User<Guest>(email);
		}

		public void AddGuest(Guest guest) {
			if (data.Guest.All.FirstOrDefault(g => g.Email == guest.Email) != null) {
				throw new EmailExistsException(guest.Email);
			}
			data.Guest.Add(guest);
		}

		public Host Host(Email email) {
			return User<Host>(email);
		}

		public void AddHost(Host host) {
			if (data.Host.All.FirstOrDefault(h => h.Email == host.Email) != null) {
				throw new EmailExistsException(host.Email);
			}
			data.Host.Add(host);
		}

		public void EditHost(Host host) {
			if (data.Host.All.FirstOrDefault(h => h.Email == host.Email && h.ID != host.ID) != null) {
				throw new EmailExistsException(host.Email);
			}
			if (!host.DebitAuthorisation) {
				foreach (Order order in OrdersOf(host)) {
					if (order.OrderStatus == "Sent email") {
						throw new AuthoriaztionRevokedWithOpenOrderException(host, order);
					}
				}
			}
			data.Host.Edit(host);
		}

		// Returns the person with the given email and password
		// If the password is wrong WrongPasswordException is thrown
		// If the email doesn't exist InexistentEmailException is thrown
		public bool SignIn<TUser>(TUser person, string password) where TUser : User {
			if (new Password(password).MatchesHash(person.PasswordHash)) {
				return true;
			}
			return false;
		}

		public IEnumerable<Amenity> Amenities {
			get => data.Amenity.All;
		}

		public IEnumerable<City> Cities {
			get => data.City.All;
		}

		public IEnumerable<Unit.Type> UnitTypes {
			get => data.UnitType.All;
		}

		public IEnumerable<Unit> Units {
			get => data.Unit.All;
		}

		public IEnumerable<Unit> UnitsOf(Host host) {
			return data.Unit.All.Where(unit => unit.Host.ID == host.ID);
		}

		public IEnumerable<Order.Status> OrderStatuses {
			get => data.OrderStatus.All;
		}

		public IEnumerable<GuestRequest> GuestRequests() {
			return data.GuestRequest.All;
		}

		public IEnumerable<Order> Orders {
			get => data.Order.All;
		}

		public IEnumerable<Order> OrdersOf(Host host) {
			return data.Order.All.Where(order => order.Unit != null && order.Unit.Host.ID == host.ID);
		}

		public IEnumerable<Order> OrdersOf(Unit unit) {
			return data.Order.All.Where(order => order.Unit != null && order.Unit.ID == unit.ID);
		}

		public IEnumerable<Order> OrdersOf(GuestRequest guest_request) {
			return data.Order.All.Where(order => order.GuestRequest != null && order.GuestRequest.ID == guest_request.ID);
		}

		public IEnumerable<BankBranch> BankBranches() {
			return data.BankBranch.All;
		}

		public IEnumerable<GuestRequest> FilterGuestRequests(Func<GuestRequest, bool> condition) {
			return data.GuestRequest.All.Where(condition);
		}

		public IEnumerable<Unit> AvailableUnits(Date date, int duration) {
			return data.Unit.All.Where(unit => unit.Available(date, duration));
		}

		//returns number of days from the first date to the second date
		public int NumberOfDays(Date date1, Date date2) {
			return (date2 - date1).Days;
		}

		//returns number of days from first day to current day (if first is in future then it would be a negative number)
		public int NumberOfDays(Date date1) {
			return NumberOfDays(date1, Date.Today);
		}

		public IEnumerable<Order> OrdersOlderThan(int days) {
			return data.Order.All.Where(order => NumberOfDays(order.CreationDate) > days);
		}

		public int UnitCount(Host host) {
			return UnitsOf(host).Count();
		}

		public IDictionary<City, IEnumerable<GuestRequest>> GuestRequestsByCity() {
			IDictionary<City, IEnumerable<GuestRequest>> dict = new Dictionary<City, IEnumerable<GuestRequest>>();
			foreach (City city in data.City.All) {
				dict[city] = data.GuestRequest.All.Where(guest_request => guest_request.DesiredCities.Contains(city));
			}
			return dict;
		}

		public IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount() {
			return data.GuestRequest.All.GroupBy(guest_request => guest_request.GuestCount());
		}

		public IEnumerable<IGrouping<int, Host>> HostsByUnitCount() {
			return data.Host.All.GroupBy(host => UnitCount(host));
		}

		public IEnumerable<IGrouping<City, Unit>> UnitsByCity() {
			return data.Unit.All.GroupBy(unit => unit.City);
		}
	}
}