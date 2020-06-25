using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class Data : IData {
		public DataAccessorReadOnly<string, Amenity> Amenity { get; } = new DataAccessorReadOnly<string, Amenity>(
			"../data/data_files/amenities.xml",
			"amenities",
			new AmenityXmlConverter()
		);

		public DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; } = new DataAccessorReadOnly<Tuple<ID, ID>, BankBranch>(
			"../data/data_files/bank_branches.xml",
			"BRANCHES",
			new BankBranchXmlConverter()
		);

		public DataAccessorReadOnly<string, City> City { get; } = new DataAccessorReadOnly<string, City>(
			"../data/data_files/cities.xml",
			"cities",
			new CityXmlConverter()
		);

		public DataAccessor<Guest> Guest { get; } = new DataAccessor<Guest>(
			"../data/data_files/guests.xml",
			"guests",
			new GuestXmlConverter(),
			(guest) => guest.Clone()
		);

		public DataAccessor<GuestRequest> GuestRequest { get; } = new DataAccessor<GuestRequest>(
			"../data/data_files/guest_requests.xml",
			"guest_requests",
			new GuestRequestXmlConverter(),
			(guest_request) => guest_request.Clone()
		);

		public DataAccessor<Host> Host { get; } = new DataAccessor<Host>(
			"../data/data_files/hosts.xml",
			"hosts",
			new HostXmlConverter(),
			(host) => host.Clone()
		);

		public DataAccessor<Unit> Unit { get; } = new DataAccessor<Unit>(
			"../data/data_files/units.xml",
			"units",
			new UnitXmlConverter(),
			(unit) => unit.Clone()
		);

		public DataAccessor<Order> Order { get; } = new DataAccessor<Order>(
			"../data/data_files/orders.xml",
			"orders",
			new OrderXmlConverter(),
			(order) => order.Clone()
		);

		public DataAccessorReadOnly<string, Order.Status> OrderStatus { get; } = new DataAccessorReadOnly<string, Order.Status>(
			"../data/data_files/order_statuses.xml",
			"order_statuses",
			new OrderStatusXmlConverter()
		);

		public DataAccessorReadOnly<string, Unit.Type> UnitType { get; } = new DataAccessorReadOnly<string, Unit.Type>(
			"../data/data_files/unit_types.xml",
			"unit_types",
			new UnitTypeXmlConverter()
		);
	}
}