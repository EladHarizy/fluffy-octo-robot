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

			if (!CheckOverlaps(start, duration)) {
				bookings.Add(new Booking(start, duration));
			}
		}

		private bool CheckOverlaps(DateTime start, int duration) {
			return CheckOverlaps(new Booking(start, duration));
		}

		private bool CheckOverlaps(Booking booking) {
			return
		}

	}

}