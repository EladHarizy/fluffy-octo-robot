using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using business.Extensions;
using data;
using Lib.Config;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace business {
	public class Business : IBusiness {
		private IData data = DataFactory.New();

		private TPerson Person<TPerson>(Email email) where TPerson : Person {
			try {
				return data.GetAccessor<TPerson>().All.First(p => p.Email == email);
			} catch (InvalidOperationException error) {
				throw new InexistentEmailException(email, error);
			}
		}

		public void AddGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Add(guest_request);
		}

		public void EditGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Edit(guest_request);
		}

		public void AddUnit(Unit unit) {
			data.Unit.Add(unit);
		}

		public void DeleteUnit(Unit unit) {
			foreach (Order order in data.Order.All) {
				if (order.OrderStatus == "Sent Mail" && order.Unit.ID == unit.ID) {
					throw new DeletingUnitWithOpenOrderException(unit, order);
				}
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

		public void EditOrder(ID id, Order.Status status) {
			Order order = data.Order[id];
			// Order is already closed
			if (order.OrderStatus == "Closed due to customer unresponsiveness" || order.OrderStatus == "Closed due to customer response") {
				throw new OrderClosedException(order);
			}
			// Order is being opened
			if (status == "Not addressed" && order.OrderStatus != status) {
				throw new ArgumentException("Error: An order cannot be reopened.");
			}
			// Order is being closed
			if (status == "Closed due to customer response") {
				if (!order.Unit.Host.DebitAuthorisation) {
					throw new NoDebitAuthorisationException("Error: Cannot change the order status to anything other than 'Not addressed' because the host does not have debit authorisation.");
				}
				int fee = Config.FeePerDay * order.GuestRequest.Duration;
				order.Unit.Bookings.Add(new Unit.Calendar.Booking(order.GuestRequest.StartDate, order.GuestRequest.Duration));
				foreach (Order order1 in data.Order.All) {
					if (order.Unit.ID == order1.Unit.ID && order.ID != order1.ID) {
						EditOrder(order1.ID, "Closed due to customer unresponsiveness");
					}
				}
			}
			if (status == "Sent Mail") {
				//TODO Send email
			}
			order.OrderStatus = status;
			data.Order.Edit(order);
		}

		public Guest Guest(ID id) {
			return data.Guest[id];
		}

		public Guest Guest(Email email) {
			return Person<Guest>(email);
		}

		public void AddGuest(Guest guest) {
			if (data.Guest.All.FirstOrDefault(g => g.Email == guest.Email) != null) {
				throw new EmailExistsException(guest.Email);
			}
			data.Guest.Add(guest);
		}

		public Host Host(ID id) {
			return data.Host[id];
		}

		public Host Host(Email email) {
			return Person<Host>(email);
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
				foreach (Order order in data.Order.All) {
					if (order.OrderStatus == "Sent Mail" && order.Unit.Host.ID == host.ID) {
						throw new AuthoriaztionRevokedWithOpenOrderException(host, order);
					}
				}
			}
			data.Host.Edit(host);
		}

		// Returns the person with the given email and password
		// If the password is wrong WrongPasswordException is thrown
		// If the email doesn't exist InexistentEmailException is thrown
		public bool SignIn<TPerson>(TPerson person, string password) where TPerson : Person {
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

		public IEnumerable<Unit> Units() {
			return data.Unit.All;
		}

		public IEnumerable<Unit> UnitsOf(Host host) {
			return data.Unit.All.Where(unit => unit.Host.ID == host.ID);
		}

		public IEnumerable<GuestRequest> GuestRequests() {
			return data.GuestRequest.All;
		}

		public IEnumerable<Order> Orders() {
			return data.Order.All;
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

		public int OrdersCount(GuestRequest guest_request) {
			return data.Order.All.Where(order => order.GuestRequest.ID == guest_request.ID).Count();
		}

		public int OrdersCount(Unit unit) {
			return data.Order.All.Where(order => order.Unit.ID == unit.ID).Count();
		}

		public int UnitCount(Host host) {
			return UnitsOf(host).Count();
		}

		public IDictionary<City, IEnumerable<GuestRequest>> GuestRequestsByCity() {
			IDictionary<City, IEnumerable<GuestRequest>> dict = new Dictionary<City, IEnumerable<GuestRequest>>();
			foreach (City city in data.City.All) {
				dict[city] = data.GuestRequest.All.Where(guest_request => guest_request.Region.Contains(city));
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