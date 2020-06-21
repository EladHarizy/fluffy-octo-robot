using System;
using System.Collections.Generic;
using Lib.Entities;

namespace business {
	public interface IBusiness {
		ICollection<Unit> AvailableUnits(Date date, int duration);

		int NumberOfDays(Date date1, Date date2);

		ICollection<Order> AllOrders(params Date[] dates);

		ICollection<GuestRequest> AllCustomerRequirements(Predicate<GuestRequest> condition);
	}
}