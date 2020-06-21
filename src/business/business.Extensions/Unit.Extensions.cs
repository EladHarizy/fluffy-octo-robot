using System;
using Lib.Entities;

namespace business.Extensions {
	public static class UnitExtensions {
		public static bool Available(this Unit unit, Date date, int duration) {
			return !unit.Bookings.Overlaps(new Unit.Calendar.Booking(date, duration));
		}
	}
}