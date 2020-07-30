using System;
using System.Collections.Generic;
using System.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace business {
	public interface IBusiness {
		void AddGuestRequest(GuestRequest guest_request);

		void EditGuestRequest(GuestRequest guest_request);

		void DeleteGuestRequest(GuestRequest guest_request);

		void AddUnit(Unit unit);

		void DeleteUnit(Unit unit);

		void EditUnit(Unit unit);

		void AddOrder(Order order);

		IEnumerable<Order> EditOrder(Order order, Order.Status status);

		Admin Admin(Email email);

		Guest Guest(Email email);

		void AddGuest(Guest guest);

		Host Host(Email email);

		void AddHost(Host host);

		void EditHost(Host host);

		bool SignIn<TUser>(TUser user, string password) where TUser : User;

		IEnumerable<Amenity> Amenities { get; }

		IEnumerable<City> Cities { get; }

		IEnumerable<Unit.Type> UnitTypes { get; }

		IEnumerable<Unit> Units { get; }

		IEnumerable<Order.Status> OrderStatuses { get; }

		IEnumerable<Unit> UnitsOf(Host host);

		IEnumerable<GuestRequest> GuestRequests();

		IEnumerable<Order> Orders { get; }

		IEnumerable<Order> OrdersOf(Host host);

		IEnumerable<Order> OrdersOf(Unit unit);

		IEnumerable<BankBranch> BankBranches();

		IEnumerable<Unit> AvailableUnits(Date date, int duration);

		int NumberOfDays(Date date1, Date date2);

		int NumberOfDays(Date date1);

		IEnumerable<Order> OrdersOlderThan(int days);

		IEnumerable<GuestRequest> FilterGuestRequests(Func<GuestRequest, bool> condition);

		int UnitCount(Host host);

		// Groupings
		IDictionary<City, IEnumerable<GuestRequest>> GuestRequestsByCity();

		IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsByGuestCount();

		IEnumerable<IGrouping<int, Host>> HostsByUnitCount();

		IEnumerable<IGrouping<City, Unit>> UnitsByCity();
	}
}