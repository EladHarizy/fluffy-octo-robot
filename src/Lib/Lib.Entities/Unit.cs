using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit : ICloneable<Unit>, IEntity<ID> {
		public ID ID { get; private set; }

		public Calendar Bookings { get; }

		public Host Host { get; }

		public string Name { get; set; }

		public string Description { get; set; }

		public City City { get; set; }

		public IEnumerable<Amenity> Amenities { get; set; }

		public Type UnitType { get; set; }

		public int OccupiedDays {
			get => Bookings.OccupiedDays;
		}

		public Unit(
			Host host,
			string name,
			string description,
			City city,
			ICollection<Amenity> amenities,
			Type unit_type
		) : this(
			null, // initialized ID to null
			host,
			name,
			description,
			city,
			amenities,
			unit_type,
			new Calendar()
		) {}

		public Unit(ID id, Host host, string name, string description, City city, ICollection<Amenity> amenities, Type unit_type, Calendar bookings) {
			ID = id;
			Host = host;
			Name = name;
			Description = description;
			City = city;
			Amenities = amenities;
			UnitType = unit_type;
			Bookings = bookings;
		}

		public override string ToString() {
			return (string) ID + " - " + Name;
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Unit Clone() {
			return new Unit(ID, Host, Name, Description, City, new HashSet<Amenity>(Amenities), UnitType, Bookings.Clone());
		}

		public override bool Equals(object obj) {
			return obj is Unit unit
				&& EqualityComparer<ID>.Default.Equals(ID, unit.ID)
				&& EqualityComparer<Host>.Default.Equals(Host, unit.Host);
		}

		public override int GetHashCode() {
			int hashCode = 1325579111;
			hashCode = hashCode * -1521134295 + EqualityComparer<ID>.Default.GetHashCode(ID);
			hashCode = hashCode * -1521134295 + EqualityComparer<Host>.Default.GetHashCode(Host);
			return hashCode;
		}
	}
}