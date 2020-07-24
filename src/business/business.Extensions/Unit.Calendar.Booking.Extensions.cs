using Lib.Entities;

namespace business.Extensions {
	public static class UnitCalendarBookingExtensions {
		public static bool Overlaps(this Unit.Calendar.Booking booking, Unit.Calendar.Booking new_booking) {
			return OverlapChecker.Overlaps(booking.Start, booking.Duration, new_booking.Start, new_booking.Duration);
		}
	}
}