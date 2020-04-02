using System;
using System.Collections.Generic;

namespace FluffyOctoRobot {
	partial class Calendar {

		// The list of Bookings
		private List<Booking> bookings = new List<Booking>();

		public void AddToCalendar(DateTime start, int duration) {
			if (start > DateTime.Now.AddYears(1).Date) {
				throw new ArgumentOutOfRangeException("Error: Date entered cannot be more than a year from now");
			}

			if (Overlaps(start, duration)) {
				throw new ArgumentException("Error: This booking overlaps with an existing booking");
			}

			bookings.Add(new Booking(start, duration));
		}

		public bool Overlaps(DateTime start, int duration) {
			return Overlaps(new Booking(start, duration));
		}

		private bool Overlaps(Booking new_booking) {
			foreach (Booking booking in bookings) {
				if (booking.Overlaps(new_booking)) {
					return true;
				}
			}
			return false;
		}

	}

}