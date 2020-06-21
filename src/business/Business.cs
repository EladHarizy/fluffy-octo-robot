using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Entities;

namespace business {
	public class Business : IBusiness {
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

		public void UpdateUnit(Unit unit1) {
			throw new System.NotImplementedException();
		}

		public void AddOrder(Order order) {
			throw new System.NotImplementedException();
		}

		public void UpdateOrder(Order order) {
			throw new System.NotImplementedException();
		}

		public ICollection<Unit> Units() {
			throw new System.NotImplementedException();
		}

		public ICollection<GuestRequest> GuestRequests() {
			throw new System.NotImplementedException();
		}

		public ICollection<Order> Orders() {
			throw new System.NotImplementedException();
		}

		public ICollection<BankBranch> BankBranches() {
			throw new System.NotImplementedException();
		}
    
		public ICollection<GuestRequest> AllCustomerRequirements(Predicate<GuestRequest> condition) {
			throw new NotImplementedException();
		}

		public ICollection<Unit> AvailableUnits(Date date, int duration) {
			throw new NotImplementedException();
		}

		public int NumberOfDays(Date date1, Date date2) {
			throw new NotImplementedException();
		}

		public ICollection<Order> OrdersOlderThan(int number_of_dates) {
			throw new NotImplementedException();
		}

		public int OrdersCount(GuestRequest guest_request) {
			throw new System.NotImplementedException();
		}

		public int OrdersCount(Unit unit) {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<City, GuestRequest>> GuestRequestsByCity() {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount() {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<int, Host>> HostsByUnitCount() {
			throw new System.NotImplementedException();
		}

		public IEnumerable<IGrouping<City, Unit>> UnitsByCity() {
			throw new System.NotImplementedException();
		}
	}
}