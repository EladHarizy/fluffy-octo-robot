using System;
using System.Collections.Generic;
using System.Text;

namespace FluffyOctoRobot {
	partial class Calendar {

		// The list of Bookings
		// We were given the green light to continue using a list implementation rather than a matrix, since we already implemented all the methods with a list in the first assignment.
		private List<Booking> bookings = new List<Booking>();

		private DateTime start_date;
		public DateTime StartDate {
			get => start_date;
			private set => start_date = value.Date;
		}

		private DateTime end_date;
		public DateTime EndDate {
			get => end_date;
			private set => end_date = value.Date;
		}

		public Calendar() : this(new DateTime(DateTime.Now.Year + 1, 1, 1)) {}

		public Calendar(DateTime start) : this(start, start.AddYears(1)) {}

		public Calendar(DateTime start, DateTime end) {
			if (end.Date <= start.Date) {
				throw new ApplicationException("Error: End date must be after start date.");
			}
			StartDate = start;
			EndDate = end;
		}

		public void AddToCalendar(DateTime start, DateTime end) {
			AddToCalendar(start, (end.Date - start.Date).Days);
		}

		public void AddToCalendar(DateTime start, int duration) {
			start = start.Date;
			if (start.AddDays(duration) >= EndDate || start < StartDate) {
				throw new ApplicationException("Error: This booking is out of range of the calendar.");
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