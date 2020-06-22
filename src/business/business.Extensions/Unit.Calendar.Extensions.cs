using Lib.Entities;
using Lib.Exceptions;

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

		public static void Add(this Unit.Calendar bookings, Unit.Calendar.Booking booking) {
			if (bookings.Overlaps(booking)) {
				throw new BookingOverlapException("Error: Cannot add booking to a unit because it overlaps with another booking.");
			}
			bookings.Add(booking);
		}
	}
}