using Lib.Entities;

namespace business.Extensions {
	public static class UnitCalendarExtensions {
		public static bool Overlaps(this Unit.Calendar calendar, Unit.Calendar.Booking new_booking) {
			foreach (Unit.Calendar.Booking booking in calendar.Bookings) {
				if (booking.Overlaps(new_booking)) {
					return true;
				}
			}
			return false;
		}
	}
}