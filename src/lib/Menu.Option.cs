using System;

namespace lib {
	partial class Menu {
		public class Option : ICloneable<Option> {
			public string Label {
				get;
				private set;
			}

			// A function that will be called when the user chooses this option
			// It should take  an array of Objects and return a bool indicating if the menu should be displayed again
			private Func<bool> function;

			public bool Execute() {
				return function();
			}

			// Constructor
			public Option(string label, Func<bool> function) {
				this.Label = label;
				this.function = function;
			}
		}
	}
}