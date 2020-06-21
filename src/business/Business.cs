using System;
using System.Collections.Generic;
using System.Linq;
using data;
using Lib.Entities;

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
			data.Unit.Remove(unit.ID);
		}

		public void UpdateUnit(Unit unit) {
			data.Unit.Update(unit);
		}

		public void AddOrder(Order order) {
			data.Order.Add(order);
		}

		public void UpdateOrder(Order order) {
			data.Order.Update(order);
		}

		public IEnumerable<Unit> Units() {
			return data.Unit.All;
		}

		public IEnumerable<Unit> Units(Host host) {
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
			return Units(host).Count();
		}

		public IEnumerable<IGrouping<City, GuestRequest>> GuestRequestsByCity() {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount() {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<int, Host>> HostsByUnitCount() {
			return data.Unit.All.GroupBy(unit => UnitCount(host));
		}

		public IEnumerable<IGrouping<City, Unit>> UnitsByCity() {
			return data.Unit.All.GroupBy(unit => unit.City);
		}
	}
}