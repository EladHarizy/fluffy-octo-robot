namespace lib {
	public partial class Order {
		public class Status {
			public string Name {
				get;
			}

			public Status(string name) {
				Name = name;
			}

			public override string ToString() {
				return Name;
			}
		}
	}
}