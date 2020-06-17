using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Order {
		public class Status : IIndexedReadOnly<string> {
			public string Name { get; }

			public Status(string name) {
				Name = name;
			}

			public override string ToString() {
				return Name;
			}

			public string Key() {
				return Name;
			}

			public static implicit operator Status(string name) {
				return new Status(name);
			}

			public static implicit operator string(Status status) {
				return status.Name;
			}
		}
	}
}