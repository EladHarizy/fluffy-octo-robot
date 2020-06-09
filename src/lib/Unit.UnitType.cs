namespace lib {
	public partial class Unit {
		public class UnitType {
			public string Name {
				get;
				private set;
			}

			public UnitType(string name) {
				Name = name;
			}

			public override string ToString() {
				return Name;
			}
		}
	}
}