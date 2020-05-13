using System;

namespace FluffyOctoRobot {
	public class HostingUnit : IComparable<HostingUnit> {
		private static int id_counter = 0;

		private int id;
		public string ID {
			get => id.ToString("D8");
		}

		private Calendar calendar;

		// Default constructor. Sets available_from to the start of the next year
		public HostingUnit() : this(new DateTime(DateTime.Now.Year + 1, 1, 1)) {}

		// Constructor. Takes a start date and sets the calendar to a year long
		public HostingUnit(DateTime available_from) : this(available_from, available_from.AddYears(1)) {}

		// Constructor. Takes two dates which indicate the period of time in which the unit is available
		public HostingUnit(DateTime available_from, DateTime available_until) {
			if (id_counter >= 99999999) {
				throw new ApplicationException("Error: All possible IDs have been allocated. Unable to create any more hosting units.");
			}
			calendar = new Calendar(available_from, available_until);
			id = ++id_counter;
		}

		public override string ToString() {
			return calendar.Occupancy();
		}

		// Adds the event to the calendar if available, marks request as accepted and returns true
		// Otherwise does nothing and returns false
		public bool ApproveRequest(GuestRequest guest_request) {
			try {
				calendar.AddToCalendar(guest_request.StartDate, guest_request.EndDate);
				guest_request.Approved = true;
				return true;
			} catch (ApplicationException) {
				return false;
			}
		}

		// Returns the average yearly occupancy
		public int AnnualOccupancy() {
			return Convert.ToInt32(365.25 * calendar.OccupiedDays() / calendar.Days());
		}

		public double OccupancyPercentage() {
			return calendar.OccupiedPercentage();
		}

		public int CompareTo(HostingUnit h) {
			double this_percentage = OccupancyPercentage();
			double that_percentage = h.OccupancyPercentage();
			if (this_percentage < that_percentage) {
				return -1;
			}
			if (this_percentage > that_percentage) {
				return 1;
			}
			return 0;
		}

		//returns start date and duration
		public bool Available(DateTime start_date, int duration) {
			return calendar.Overlaps(start_date, duration);
		}

		public bool Available(GuestRequest guest_request) {
			return calendar.Overlaps(guest_request.StartDate, guest_request.Duration);
		}

	}
}