using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Entities;

namespace business {
	public interface IBusiness {
		void AddGuestRequest(GuestRequest guest_request);

		void UpdateGuestRequest(GuestRequest guest_request);

		void AddUnit(Unit unit);

		void DeleteUnit(Unit unit);

		void UpdateUnit(Unit unit1);

		void AddOrder(Order order);

		void UpdateOrder(Order order);

		ICollection<Unit> Units();

		ICollection<GuestRequest> GuestRequests();

		ICollection<Order> Orders();

		ICollection<BankBranch> BankBranches();

		ICollection<Unit> AvailableUnits(Date date, int duration);

		int NumberOfDays(Date date1, Date date2);

		ICollection<Order> OrdersOlderThan(int number_of_dates);

		ICollection<GuestRequest> AllCustomerRequirements(Predicate<GuestRequest> condition);

		int OrdersCount(GuestRequest guest_request);

		int OrdersCount(Unit unit);

		// Groupings
		IEnumerable<IGrouping<City, GuestRequest>> GuestRequestsByCity();

		IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount();

		IEnumerable<IGrouping<int, Host>> HostsByUnitCount();

		IEnumerable<IGrouping<City, Unit>> UnitsByCity();
	}
}