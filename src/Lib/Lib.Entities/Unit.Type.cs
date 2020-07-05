using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit {
		public class Type : IEntityReadOnly<string> {
			public string Name { get; }

			public Type(string name) {
				Name = name;
			}

			public override string ToString() {
				return Name;
			}

			public string Key() {
				return Name;
			}

			public static implicit operator Type(string name) {
				return new Type(name);
			}

			public static implicit operator string(Type type) {
				return type.Name;
			}
		}
	}
}