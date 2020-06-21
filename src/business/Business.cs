using System;
using System.Collections.Generic;
using System.Linq;
using business.Extensions;
using data;
using Lib.Entities;

namespace business {
	public class Business : IBusiness {
		private IData data = DataFactory.New();

		public void AddGuestRequest(GuestRequest guest_request) {
			throw new System.NotImplementedException();
		}

		public void UpdateGuestRequest(GuestRequest guest_request) {
			throw new System.NotImplementedException();
		}

		public void AddUnit(Unit unit) {
			throw new System.NotImplementedException();
		}

		public void DeleteUnit(Unit unit) {
			throw new System.NotImplementedException();
		}

		public void UpdateUnit(Unit unit) {
			throw new System.NotImplementedException();
		}

		public void AddOrder(Order order) {
			throw new System.NotImplementedException();
		}

		public void UpdateOrder(Order order) {
			throw new System.NotImplementedException();
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

		public IEnumerable<GuestRequest> FilterCustomerRequirements(Predicate<GuestRequest> condition) {
			throw new NotImplementedException();
		}

		public IEnumerable<Unit> AvailableUnits(Date date, int duration) {
			throw new NotImplementedException();
		}

		public int NumberOfDays(Date date1, Date date2) {
			throw new NotImplementedException();
		}

		public IEnumerable<Order> OrdersOlderThan(int number_of_dates) {
			throw new NotImplementedException();
		}

		public int OrdersCount(GuestRequest guest_request) {
			throw new System.NotImplementedException();
		}

		public int OrdersCount(Unit unit) {
			throw new System.NotImplementedException();
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