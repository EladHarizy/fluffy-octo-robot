using System;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit : IComparable<Unit>, ICloneable<Unit>, IIndexed<ID> {
		public ID ID { get; private set; }

		private Calendar calendar;

		private Host host;

		public string UnitName { get; private set; }

		// Constructor. Does not take an ID. Generates a new ID
		public Unit(
			Host host,
			string hosting_unit_name,
			Date available_from,
			Date available_until
		) : this(
			null, // initialized ID to null
			host,
			hosting_unit_name,
			new Calendar(available_from, available_until)
		) {}

		// Constructor. Takes an ID, host, and two dates which indicate the period of time in which the unit is available
		private Unit(ID id, Host host, string hosting_unit_name, Calendar calendar) {
			ID = id;
			this.host = host;
			UnitName = hosting_unit_name;
			this.calendar = calendar;
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
			sb.Append(UnitName);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Occupied on:");
			sb.Append('\n');
			sb.Append(calendar.Occupancy().Tabulate(tabs + 1));

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

		public int CompareTo(Unit h) {
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

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Unit Clone() {
			return new Unit(ID, host, UnitName, calendar.Clone());
		}
	}
}