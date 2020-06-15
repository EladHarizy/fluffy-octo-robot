using System;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	public interface IData {
		DataAccessorReadOnly<string, Amenity> Amenity { get; }

		DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		DataAccessorReadOnly<string, City> City { get; }

		DataAccessor<ID, Guest> Guest { get; }

		DataAccessor<ID, GuestRequest> GuestRequest { get; }

		DataAccessor<ID, Host> Host { get; }

		DataAccessor<ID, Unit> Unit { get; }

		DataAccessor<ID, Order> Order { get; }

		DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		DataAccessorReadOnly<string, Unit.Type> Type { get; }
	}
}