using System;
using lib;

namespace data {
	interface IData {
		IDataAccessorReadOnly<Amenity, string> Amenity { get; }

		IDataAccessorReadOnly<BankBranch, Tuple<ID, ID>> BankBranch { get; }

		IDataAccessorReadOnly<City, string> City { get; }

		IDataAccessor<Guest, ID> Guest { get; }

		IDataAccessor<GuestRequest, ID> GuestRequest { get; }

		IDataAccessor<Host, ID> Host { get; }

		IDataAccessor<HostingUnit, ID> HostingUnit { get; }

		IDataAccessor<Order, ID> Order { get; }

		IDataAccessorReadOnly<Order.Status, string> OrderStatus { get; }

		IDataAccessorReadOnly<Unit.UnitType, string> UnitType { get; }
	}
}