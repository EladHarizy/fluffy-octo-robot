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

		private Host host;

		public string HostingUnitName {
			get;
			private set;
		}

		// Constructor. Does not take an ID. Generates a new ID
		public HostingUnit(
			Host host,
			string hosting_unit_name,
			Date available_from,
			Date available_until
		) : this(
			id_generator.Next(),
			host,
			hosting_unit_name,
			available_from,
			available_until
		) {}

		// Constructor. Takes an ID, host, and two dates which indicate the period of time in which the unit is available
		public HostingUnit(ID id, Host host, string hosting_unit_name, Date available_from, Date available_until) {
			ID = id;
			calendar = new Calendar(available_from, available_until);
		}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("Hosting Unit Details");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("--------------------");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Host Name:\t");
			sb.Append(host.Name);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Unit Name:\t");
			sb.Append(HostingUnitName);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Occupied on:");
			sb.Append(Tabulator.Tabulate(calendar.Occupancy(), tabs + 1));

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
			return this_percentage.CompareTo(that_percentage);
		}

		// Returns start date and duration
		public bool Available(Date start_date, int duration) {
			return !calendar.Overlaps(start_date, duration);
		}

		public bool Available(GuestRequest guest_request) {
			return !calendar.Overlaps(guest_request.StartDate, guest_request.Duration);
		}
	}
}