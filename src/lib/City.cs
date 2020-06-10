namespace lib {
	public class City {
		public string Name {
			get;
		}

		public City(string name) {
			Name = name;
		}

		public override string ToString() {
			return Name;
		}

		public static implicit operator City(string name) {
			return new City(name);
		}

		public static implicit operator string(City city) {
			return city.Name;
		}
	}
}