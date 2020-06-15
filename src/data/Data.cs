using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class Data : IData {
		public DataAccessorReadOnly<string, Amenity> Amenity { get; }

		public DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		public DataAccessorReadOnly<string, City> City { get; }

		public DataAccessor<ID, Guest> Guest { get; }

		public DataAccessor<ID, GuestRequest> GuestRequest { get; }

		public DataAccessor<ID, Host> Host { get; }

		public DataAccessor<ID, Unit> Unit { get; }

		public DataAccessor<ID, Order> Order { get; }

		public DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		public DataAccessorReadOnly<string, Unit.UnitType> UnitType { get; }

		internal Data() {
			Amenity = new DataAccessorReadOnly<string, Amenity>(
				"data_files/amenities.xml",
				"amenities",
				element => element.ToAmenity()
			);

			BankBranch = new DataAccessorReadOnly<Tuple<ID, ID>, BankBranch>(
				"data_files/bank_branches.xml",
				"BRANCHES",
				element => element.ToBankBranch()
			);

			City = new DataAccessorReadOnly<string, City>(
				"data_files/cities.xml",
				"cities",
				(element) => new City(element.Element("name").Value)
			);

			Guest = new DataAccessor<ID, Guest>(
				"data_files/guests.xml",
				"guests",
				element => element.ToGuest()
			);

			GuestRequest = new DataAccessor<ID, GuestRequest>(
				"data_files/guest_requests.xml",
				"guest_requests",
				element => element.ToGuestRequest()
			);
		}
	}
}