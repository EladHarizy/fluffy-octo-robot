using System.Collections.Generic;
using Lib.Interfaces;

namespace Lib.Entities {

	// Our professor (Dan Erez) told us the we can implement ICloneable in each class, instead of adding a clone extension to each class
	public class Amenity : IEntityReadOnly<string>, ICloneable<Amenity> {
		public string Name { get; private set; }

		public Amenity(string name) {
			Name = name;
		}

		public override string ToString() {
			return Name;
		}

		public string Key() {
			return Name;
		}

		public Amenity Clone() {
			return new Amenity(Name);
		}

		public override bool Equals(object obj) {
			return obj is Amenity amenity
				&& Name == amenity.Name;
		}

		public override int GetHashCode() {
			return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
		}

		public static implicit operator Amenity(string name) {
			return new Amenity(name);
		}

		public static implicit operator string(Amenity amenity) {
			return amenity.Name;
		}
	}
}