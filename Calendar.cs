using System;
using System.Collections.Generic;
using System.Text;

namespace FluffyOctoRobot {
	partial class Calendar {

		// The list of Bookings
		private List<Booking> bookings = new List<Booking>();

		public void AddToCalendar(DateTime start, int duration) {
			if (start > DateTime.Now.AddYears(1).Date) {
				throw new ApplicationException("Error: Date entered cannot be more than a year from now.");
			}

			if (Overlaps(start, duration)) {
				throw new ApplicationException("Error: This booking overlaps with an existing booking.");
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

		public string Occupancy() {
			bookings.Sort((a, b) => a.Start.CompareTo(b.Start)); // So that adjacent bookings can be merged
			StringBuilder occupancy = new StringBuilder();
			for (int i = 0; i < bookings.Count; ++i) {
				occupancy.Append(bookings[i].Start.ToString("dd/MM/yyyy"));
				occupancy.Append(" - ");
				while (i + 1 < bookings.Count && bookings[i].End == bookings[i + 1].Start) {
					++i;
				}
				occupancy.AppendLine(bookings[i].End.ToString("dd/MM/yyyy"));
			}
			return occupancy.ToString();
		}

		public int OccupiedDays() {
			int occupied_days = 0;
			foreach (Booking booking in bookings) {
				occupied_days += booking.Duration;
			}
			return occupied_days;
		}
	}
}