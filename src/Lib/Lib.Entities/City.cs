using Lib.Interfaces;

namespace Lib.Entities {
	public class City : IIndexedReadOnly<string> {
		public string Name { get; }

		public City(string name) {
			Name = name;
		}

		public override string ToString() {
			return Name;
		}

		public string Key() {
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