using System;
using System.Collections.Generic;
using System.Linq;
using business.Extensions;
using data;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace business {
	public class Business : IBusiness {
		private IData data = DataFactory.New();

		public void AddGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Add(guest_request);
		}

		public void UpdateGuestRequest(GuestRequest guest_request) {
			data.GuestRequest.Update(guest_request);
		}

		public void AddUnit(Unit unit) {
			data.Unit.Add(unit);
		}

		public void DeleteUnit(Unit unit) {
			if (data.Order.All.Where(order => order.OrderStatus == "Sent Mail" && order.Unit.ID == unit.ID).Count() != 0) {
				throw new DeletingUnitWithOpenOrderException("Error: Cannot remove a unit which has an open order.");
			}
			data.Unit.Remove(unit.ID);
		}

		public void UpdateUnit(Unit unit) {
			data.Unit.Update(unit);
		}

		public void AddOrder(Order order) {
			if (order.OrderStatus != "Not addressed") {
				throw new InitialStatusException(order.OrderStatus);
			}
			data.Order.Add(order);
		}

		public void UpdateOrder(ID id, Order.Status status) {
			Order order = data.Order[id];
			if (order.OrderStatus != "Not addressed" && !order.Unit.Host.DebitAuthorisation) {
				throw new NoDebitAuthorisationException("Error: Cannot change the order status to anything other than 'Not addressed' because the host does not have debit authorisation.");
			}
			data.Order.Update(order);
			if (status == "Sent Mail") {
				//TODO Send email
			}
		}

		public void AddHost(Host host) {
			data.Host.Add(host);
		}

		public void UpdateHost(Host host) {
			data.Host.Update(host);
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

		//returns number of days from first day to current day (if first is in furture then it would be a negative number)
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