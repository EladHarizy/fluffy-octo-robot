using System;
using System.Collections.Generic;
using Lib.Entities;

namespace business {
	public class Business : IBusiness {
		public ICollection<GuestRequest> AllCustomerRequirements(Predicate<GuestRequest> condition) {
			throw new NotImplementedException();
		}

		public ICollection<Order> AllOrders(params Date[] dates) {
			throw new NotImplementedException();
		}

		public ICollection<Unit> AvailableUnits(Date date, int duration) {
			throw new NotImplementedException();
		}

		public int NumberOfDays(Date date1, Date date2) {
			throw new NotImplementedException();
		}
	}
}