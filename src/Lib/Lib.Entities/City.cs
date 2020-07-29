using System.Collections.Generic;
using Lib.Interfaces;

namespace Lib.Entities {
	public class City : IEntityReadOnly<string> {
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

		public override bool Equals(object obj) {
			return obj is City city
				&& Name == city.Name;
		}

		public override int GetHashCode() {
			return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
		}

		public static implicit operator City(string name) {
			return new City(name);
		}

		public static implicit operator string(City city) {
			return city.Name;
		}
	}
}