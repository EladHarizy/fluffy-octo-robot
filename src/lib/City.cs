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
	}
}