using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit {
		public class Type : IIndexedReadOnly<string> {
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
		}
	}
}