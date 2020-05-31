using System;
using System.Collections.Generic;
using System.Text;

namespace lib {
	partial class Calendar {

		// The list of Bookings
		// We were given the green light to continue using a list implementation rather than a matrix, since we already implemented all the methods with a list in the first assignment.
		private List<Booking> bookings = new List<Booking>();

		public Date StartDate {
			get;
			private set;
		}

		public Date EndDate {
			get;
			private set;
		}

		public Calendar() : this(new Date(Date.Today.Year + 1, 1, 1)) {}

		public Calendar(Date start) : this(start, start.AddYears(1)) {}

		public Calendar(Date start, Date end) {
			if (end <= start) {
				throw new ApplicationException("Error: End date must be after start date.");
			}
			StartDate = start;
			EndDate = end;
		}

		public void AddToCalendar(Date start, Date end) {
			AddToCalendar(start, (end - start).Days);
		}

		public void AddToCalendar(Date start, int duration) {
			if (start.AddDays(duration) >= EndDate || start < StartDate) {
				throw new ApplicationException("Error: This booking is out of range of the calendar.");
			}

			if (Overlaps(start, duration)) {
				throw new ApplicationException("Error: This booking overlaps with an existing booking.");
			}

			bookings.Add(new Booking(start, duration));
		}

		public bool Overlaps(Date start, int duration) {
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

		// Returns a string with all the occupied periods of time
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

		// Returns the number of occupied days
		public int OccupiedDays() {
			int occupied_days = 0;
			foreach (Booking booking in bookings) {
				occupied_days += booking.Duration;
			}
			return occupied_days;
		}

		public float OccupiedPercentage() {
			return 100 * (float) (OccupiedDays()) / Days();
		}

		public int Days() {
			return (EndDate - StartDate).Days;
		}
	}
}