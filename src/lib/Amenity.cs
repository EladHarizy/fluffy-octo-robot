using System;

namespace lib {

	// Our professor (Dan Erez) told us the we can implement ICloneable in each class, instead of adding a clone extension to each class
	public class Amenity : ICloneable<Amenity> {
		public string Name {
			get;
			private set;
		}

		public Amenity(string name) {
			Name = name;
		}

		public override string ToString() {
			return Name;
		}

		public Amenity Clone() {
			return new Amenity(Name);
		}
	}
}