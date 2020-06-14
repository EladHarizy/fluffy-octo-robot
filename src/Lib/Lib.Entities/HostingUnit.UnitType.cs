using Lib.Interfaces;

namespace Lib.Entities {
	public partial class HostingUnit {
		public class UnitType : IIndexedReadOnly<string> {
			public string Name { get; }

			public UnitType(string name) {
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