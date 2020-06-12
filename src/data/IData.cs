using System;
using lib;

namespace data {
	public interface IData {
		DataAccessorReadOnly<string, Amenity> Amenity { get; }

		DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		DataAccessorReadOnly<string, City> City { get; }

		DataAccessor<ID, Guest> Guest { get; }

		DataAccessor<ID, GuestRequest> GuestRequest { get; }

		DataAccessor<ID, Host> Host { get; }

		DataAccessor<ID, HostingUnit> HostingUnit { get; }

		DataAccessor<ID, Order> Order { get; }

		DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		DataAccessorReadOnly<string, Unit.UnitType> UnitType { get; }
	}
}