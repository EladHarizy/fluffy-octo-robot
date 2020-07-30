using System;
using System.IO;
using Lib.Config;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;
using Lib.Interfaces;

namespace data {
	class Data : IData {
		public DataAccessor<Admin> Admin { get; }

		public DataAccessorReadOnly<string, Amenity> Amenity { get; }

		public DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		public DataAccessorReadOnly<string, City> City { get; }

		public DataAccessor<Guest> Guest { get; }

		public DataAccessor<GuestRequest> GuestRequest { get; }

		public DataAccessor<Host> Host { get; }

		public DataAccessor<Unit> Unit { get; }

		public DataAccessor<Order> Order { get; }

		public DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		public DataAccessorReadOnly<string, Unit.Type> UnitType { get; }

		public Data() {
			DatabaseInitializer.InitializeDatabase();

			Admin = new DataAccessor<Admin>(
				Path.Combine(Config.BaseDataPath, "admins.xml"),
				"admins",
				new AdminXmlConverter(),
				(admin) => admin.Clone()
			);

			Amenity = new DataAccessorReadOnly<string, Amenity>(
				Path.Combine(Config.BaseDataPath, "amenities.xml"),
				"amenities",
				new AmenityXmlConverter()
			);

			BankBranch = new DataAccessorReadOnly<Tuple<ID, ID>, BankBranch>(
				Path.Combine(Config.BaseDataPath, "bank_branches.xml"),
				"BRANCHES",
				new BankBranchXmlConverter()
			);

			City = new DataAccessorReadOnly<string, City>(
				Path.Combine(Config.BaseDataPath, "cities.xml"),
				"cities",
				new CityXmlConverter()
			);

			Guest = new DataAccessor<Guest>(
				Path.Combine(Config.BaseDataPath, "guests.xml"),
				"guests",
				new GuestXmlConverter(),
				(guest) => guest.Clone()
			);

			GuestRequest = new DataAccessor<GuestRequest>(
				Path.Combine(Config.BaseDataPath, "guest_requests.xml"),
				"guest_requests",
				new GuestRequestXmlConverter(),
				(guest_request) => guest_request.Clone()
			);

			Host = new DataAccessor<Host>(
				Path.Combine(Config.BaseDataPath, "hosts.xml"),
				"hosts",
				new HostXmlConverter(),
				(host) => host.Clone()
			);

			Unit = new DataAccessor<Unit>(
				Path.Combine(Config.BaseDataPath, "units.xml"),
				"units",
				new UnitXmlConverter(),
				(unit) => unit.Clone()
			);

			Order = new DataAccessor<Order>(
				Path.Combine(Config.BaseDataPath, "orders.xml"),
				"orders",
				new OrderXmlConverter(),
				(order) => order.Clone()
			);

			OrderStatus = new DataAccessorReadOnly<string, Order.Status>(
				Path.Combine(Config.BaseDataPath, "order_statuses.xml"),
				"order_statuses",
				new OrderStatusXmlConverter()
			);

			UnitType = new DataAccessorReadOnly<string, Unit.Type>(
				Path.Combine(Config.BaseDataPath, "unit_types.xml"),
				"unit_types",
				new UnitTypeXmlConverter()
			);
		}

		public DataAccessor<T> GetAccessor<T>() where T : IEntity<ID> {
			if (typeof(T) == typeof(Admin)) {
				return Admin as DataAccessor<T>;
			}
			if (typeof(T) == typeof(Guest)) {
				return Guest as DataAccessor<T>;
			}
			if (typeof(T) == typeof(GuestRequest)) {
				return GuestRequest as DataAccessor<T>;
			}
			if (typeof(T) == typeof(Host)) {
				return Host as DataAccessor<T>;
			}
			if (typeof(T) == typeof(Unit)) {
				return Unit as DataAccessor<T>;
			}
			if (typeof(T) == typeof(Order)) {
				return Order as DataAccessor<T>;
			}
			throw new InvalidTypeException(typeof(T));
		}
	}
}