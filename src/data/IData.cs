using System;
using lib;

namespace data {
	interface IData {
		IDataAccessor<Amenity, string> Amenity {
			get;
		}

		IDataAccessor<BankBranch, Tuple<ID, ID>> BankBranch {
			get;
		}

		IDataAccessor<City, string> City {
			get;
		}

		IDataAccessor<Guest, ID> Guest {
			get;
		}

		IDataAccessor<GuestRequest, ID> GuestRequest {
			get;
		}

		IDataAccessor<Host, ID> Host {
			get;
		}

		IDataAccessor<HostingUnit, ID> HostingUnit {
			get;
		}

		IDataAccessor<Order, ID> Order {
			get;
		}

		IDataAccessor<Order.Status, string> OrderStatus {
			get;
		}

		IDataAccessor<Unit.UnitType, string> UnitType {
			get;
		}
	}
}