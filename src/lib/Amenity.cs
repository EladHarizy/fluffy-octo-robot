namespace lib {
	public class Amenity {
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
	}
}