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

		void UpdateUnit(Unit unit);

		void AddOrder(Order order);

		void UpdateOrder(Order order);

		IEnumerable<Unit> Units();

		IEnumerable<Unit> Units(Host host);

		IEnumerable<GuestRequest> GuestRequests();

		IEnumerable<Order> Orders();

		IEnumerable<BankBranch> BankBranches();

		IEnumerable<Unit> AvailableUnits(Date date, int duration);

		int NumberOfDays(Date date1, Date date2);

		IEnumerable<Order> OrdersOlderThan(int number_of_dates);

		IEnumerable<GuestRequest> FilterCustomerRequirements(Predicate<GuestRequest> condition);

		int OrdersCount(GuestRequest guest_request);

		int OrdersCount(Unit unit);

		int UnitCount(Host host);

		// Groupings
		IEnumerable<IGrouping<City, GuestRequest>> GuestRequestsByCity();

		IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount();

		IEnumerable<IGrouping<int, Host>> HostsByUnitCount();

		IEnumerable<IGrouping<City, Unit>> UnitsByCity();
	}
}