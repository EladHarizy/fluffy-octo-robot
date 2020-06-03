using System;
using System.Text;

namespace lib {
	public class HostingUnit : IComparable<HostingUnit> {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
			private set;
		}

		private Calendar calendar;

		// Default constructor. Sets available_from to the start of the next year
		public HostingUnit() : this(new Date(Date.Today.Year + 1, 1, 1)) {}

		// Constructor. Takes a start date and sets the calendar to a year long
		public HostingUnit(Date available_from) : this(available_from, available_from.AddYears(1)) {}

		// Constructor. Takes two dates which indicate the period of time in which the unit is available
		public HostingUnit(Date available_from, Date available_until) {
			ID = id_generator.Next();
			calendar = new Calendar(available_from, available_until);
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Host Name: " + Owner);
			sb.AppendLine("Unit Name: " + HostingUnitName);
			sb.AppendLine("Occupied at these times:");
			sb.AppendLine(calendar.Occupancy());
			return sb.ToString();
		}

		// Adds the event to the calendar if available, marks request as accepted and returns true
		// Otherwise does nothing and returns false
		public bool ApproveRequest(GuestRequest guest_request) {
			try {
				calendar.AddToCalendar(guest_request.StartDate, guest_request.EndDate);
				guest_request.Active = false;
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
		public bool Available(Date start_date, int duration) {
			return !calendar.Overlaps(start_date, duration);
		}

		public bool Available(GuestRequest guest_request) {
			return !calendar.Overlaps(guest_request.StartDate, guest_request.Duration);
		}
		Host Owner;
		public string HostingUnitName {
			get;
			private set;
		}

	}
}