using System;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	public interface IData {
		DataAccessorReadOnly<string, Amenity> Amenity { get; }

		DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		DataAccessorReadOnly<string, City> City { get; }

		DataAccessor<Guest> Guest { get; }

		DataAccessor<GuestRequest> GuestRequest { get; }

		DataAccessor<Host> Host { get; }

		DataAccessor<Unit> Unit { get; }

		DataAccessor<Order> Order { get; }

		DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		DataAccessorReadOnly<string, Unit.Type> UnitType { get; }
	}
}