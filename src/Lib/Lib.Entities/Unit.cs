using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit : ICloneable<Unit>, IEntity<ID> {
		public ID ID { get; private set; }

		public Calendar Bookings { get; }

		public Host Host { get; }

		public string UnitName { get; private set; }

		public City City { get; }

		public ICollection<Amenity> Amenities { get; }

		public Type UnitType { get; }

		public int OccupiedDays {
			get => Bookings.OccupiedDays;
		}

		public Unit(
			Host host,
			string unit_name,
			City city,
			ICollection<Amenity> amenities,
			Type unit_type
		) : this(
			null, // initialized ID to null
			host,
			unit_name,
			city,
			amenities,
			unit_type,
			new Calendar()
		) {}

		public Unit(ID id, Host host, string unit_name, City city, ICollection<Amenity> amenities, Type unit_type, Calendar bookings) {
			ID = id;
			Host = host;
			UnitName = unit_name;
			City = city;
			Amenities = amenities;
			UnitType = unit_type;
			Bookings = bookings;
		}

		public override string ToString() {
			return (string) ID + " - " + UnitName;
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Unit Clone() {
			return new Unit(ID, Host, UnitName, City, new HashSet<Amenity>(Amenities), UnitType, Bookings.Clone());
		}
	}
}