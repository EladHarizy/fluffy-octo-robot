using System;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Interfaces;

namespace data {
	public interface IData {
		DataAccessor<Admin> Admin { get; }

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

		DataAccessor<T> GetAccessor<T>() where T : IEntity<ID>;
	}
}