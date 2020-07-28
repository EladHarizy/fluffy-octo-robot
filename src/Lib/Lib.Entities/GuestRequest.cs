using System;
using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Exceptions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class GuestRequest : ICloneable<GuestRequest>, IEntity<ID> {
		public ID ID { get; set; }

		public Guest Guest { get; }

		// Date that the request was created
		public Date CreationDate { get; }

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
		public bool Active { get; set; }

		private int adults;
		public int Adults {
			get => adults;
			set {
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
			set => children = Math.Max(0, value);
		}

		public string Message { get; }

		public ICollection<City> DesiredCities { get; set; }

		public ICollection<Unit.Type> DesiredUnitTypes { get; set; }

		public ICollection<Amenity> DesiredAmenities { get; set; }

		// Constructor with only some values for creating a new guest request
		public GuestRequest(
			Guest guest,
			Date start_date,
			Date end_date,
			int adults,
			int children,
			string message,
			ICollection<City> desired_cities,
			ICollection<Unit.Type> desired_unit_types,
			ICollection<Amenity> desired_amenities
		) : this(
			null, // initialized ID to null
			guest,
			Date.Today,
			start_date,
			end_date,
			true, // Initializes active to true
			adults,
			children,
			message,
			desired_cities,
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
			string message,
			ICollection<City> desired_cities,
			ICollection<Unit.Type> desired_unit_types,
			ICollection<Amenity> desired_amenities
		) {
			ID = id;
			Guest = guest;
			CreationDate = creation_date;
			this.start_date = start_date; // If loading from database, may be in the past
			EndDate = end_date;
			Active = active;
			Adults = adults;
			Children = children;
			Message = message;
			DesiredCities = desired_cities;
			DesiredUnitTypes = desired_unit_types;
			DesiredAmenities = desired_amenities;
		}

		public override string ToString() {
			return ID;
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public GuestRequest Clone() {
			return new GuestRequest(ID, Guest.Clone(), CreationDate, StartDate, EndDate, Active, Adults, Children, Message, new HashSet<City>(DesiredCities), new HashSet<Unit.Type>(DesiredUnitTypes), new HashSet<Amenity>(DesiredAmenities));
		}

		public override bool Equals(object obj) {
			return obj is GuestRequest request
				&& EqualityComparer<ID>.Default.Equals(ID, request.ID);
		}

		public override int GetHashCode() {
			int hashCode = -704701015;
			hashCode = hashCode * -1521134295 + EqualityComparer<ID>.Default.GetHashCode(ID);
			return hashCode;
		}
	}
}