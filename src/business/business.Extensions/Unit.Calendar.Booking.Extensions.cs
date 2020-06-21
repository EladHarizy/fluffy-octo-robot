using Lib.Entities;

namespace business.Extensions {
	public static class UnitCalendarBookingExtensions {
		public static bool Overlaps(this Unit.Calendar.Booking booking, Unit.Calendar.Booking new_booking) {
			// Two bookings will overlap iff one starts inside the other
			return (booking.Start >= new_booking.Start && booking.Start < new_booking.End)
				|| (new_booking.Start >= booking.Start && new_booking.Start < booking.End);
		}
	}
}