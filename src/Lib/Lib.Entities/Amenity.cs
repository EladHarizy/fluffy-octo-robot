using Lib.Interfaces;

namespace Lib.Entities {

	// Our professor (Dan Erez) told us the we can implement ICloneable in each class, instead of adding a clone extension to each class
	public class Amenity : ICloneable<Amenity>, IIndexedReadOnly<string> {
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

		public static implicit operator Amenity(string name) {
			return new Amenity(name);
		}

		public static implicit operator string(Amenity amenity) {
			return amenity.Name;
		}
	}
}