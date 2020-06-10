using System;
using System.Collections.Generic;
using System.Text;
using exceptions;

namespace lib {
	public class GuestRequest : ICloneable<GuestRequest> {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
		}

		public Guest Guest {
			get;
		}

		// Date that the request was created
		public Date CreationDate {
			get;
		}

		// First date of the stay
		private Date start_date;
		public Date StartDate {
			get => start_date;
			set {
				if (value < Date.Today) {
					throw new PastDateException();
				}
				start_date = value;
			}
		}

		// Next day after the guest leaves
		private Date end_date;
		public Date EndDate {
			get => end_date;
			set {
				if (value <= StartDate) {
					throw new NonPositiveDurationException();
				}
				end_date = value;
			}
		}

		public int Duration {
			get => (EndDate - StartDate).Days;
		}

		// A guest request is active when it has not yet been assigned to a hosting unit
		public bool Active {
			get;
			set;
		}

		private int adults;
		public int Adults {
			get => adults;
			private set {
				if (value < 1) {
					adults = 1;
					throw new NonPositiveAdultsException(value);
				}
				adults = value;
			}
		}

		private int children;
		public int Children {
			get => children;
			private set => children = Math.Max(0, value);
		}

		public HashSet<City> Region {
			get;
			set;
		}

		public HashSet<Unit.UnitType> DesiredUnitTypes {
			get;
			set;
		}

		public HashSet<Amenity> DesiredAmenities {
			get;
			set;
		}

		// Constructor with only some values for creating a new guest request
		public GuestRequest(
			Guest guest,
			Date start_date,
			Date end_date,
			int adults,
			int children,
			HashSet<City> region,
			HashSet<Unit.UnitType> desired_unit_types,
			HashSet<Amenity> desired_amenities
		) : this(
			id_generator.Next(),
			guest,
			Date.Today,
			start_date,
			end_date,
			true, // Initializes active to true
			adults,
			children,
			region,
			desired_unit_types,
			desired_amenities
		) {}

		// Constructor with all the values for restoring an old guest request from storage
		public GuestRequest(
			ID id,
			Guest guest,
			Date creation_date,
			Date start_date,
			Date end_date,
			bool active,
			int adults,
			int children,
			HashSet<City> region,
			HashSet<Unit.UnitType> desired_unit_types,
			HashSet<Amenity> desired_amenities
		) {
			ID = id;
			Guest = guest;
			CreationDate = creation_date;
			StartDate = start_date;
			EndDate = end_date;
			Active = active;
			Adults = adults;
			Children = children;
			Region = region;
			DesiredUnitTypes = desired_unit_types;
			DesiredAmenities = desired_amenities;
		}

		// Same as the partial constructor, but takes an int as the number of days instead of an end date
		public GuestRequest(
			Guest guest,
			Date start_date,
			int duration,
			int adults,
			int children,
			HashSet<City> region,
			HashSet<Unit.UnitType> desired_unit_types,
			HashSet<Amenity> desired_amenities
		) : this(
			guest,
			start_date,
			start_date.AddDays(duration),
			adults, children,
			region,
			desired_unit_types,
			desired_amenities
		) {}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();
			sb.Append('\t', tabs);
			sb.Append("Guest Request Details");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("---------------------");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("ID:\t\t\t");
			sb.Append(ID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Guest:\t\t\n");
			sb.Append(Guest.ToString(tabs + 1));

			sb.Append('\t', tabs);
			sb.Append("Created on:\t\t");
			sb.Append(CreationDate.ToString("dd/MM/yyyy"));
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Start date:\t\t");
			sb.Append(StartDate.ToString("dd/MM/yyyy"));
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("End date:\t\t");
			sb.Append(EndDate.ToString("dd/MM/yyyy"));
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Active:\t\t\t");
			sb.Append(Active ? "Yes" : "No");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Adults:\t\t\t");
			sb.Append(Adults);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Children:\t\t");
			sb.Append(Children);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Region:\t\t\t");
			sb.Append(string.Join(", ", Region));
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Unit types:\t\t");
			sb.Append(string.Join(", ", DesiredUnitTypes));
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Desired amenities:\t");
			sb.Append(string.Join(", ", DesiredAmenities));
			sb.Append('\n');

			return sb.ToString();
		}

		public GuestRequest Clone() {
			return new GuestRequest(ID, Guest.Clone(), CreationDate, StartDate, EndDate, Active, Adults, Children, (HashSet<City>) Region.Clone(), (HashSet<Unit.UnitType>) DesiredUnitTypes.Clone(), (HashSet<Amenity>) DesiredAmenities.Clone());
		}
	}
}