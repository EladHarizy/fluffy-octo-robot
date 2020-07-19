using System.Collections.Generic;
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

			public override bool Equals(object obj) {
				return obj is Type type
					&& Name == type.Name;
			}

			public override int GetHashCode() {
				return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
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