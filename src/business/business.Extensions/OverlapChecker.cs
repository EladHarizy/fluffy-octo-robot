using System;

namespace business.Extensions {
	internal static class OverlapChecker {
		public static bool Overlaps(Date date1, int duration1, Date date2, int duration2) {
			// Two bookings will overlap iff one starts inside the other
			return (date1 >= date2 && date1 < date2.AddDays(duration2))
				|| (date2 >= date1 && date2 < date1.AddDays(duration1));
		}
	}
}