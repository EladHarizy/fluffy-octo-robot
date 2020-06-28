using System.Collections.Generic;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Order {
		public class Status : IEntityReadOnly<string> {
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

			public override bool Equals(object obj) {
				return obj is Status status && Name == status.Name;
			}

			public override int GetHashCode() {
				return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
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