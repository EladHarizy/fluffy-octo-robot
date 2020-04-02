using System;

namespace FluffyOctoRobot {
	partial class Menu {
		public class Option {
			protected string label;
			public string Label {
				get => label;
			}

			// A function that will be called when the user chooses this option
			// It should take  an array of Objects and return a bool indicating if the menu should be displayed again
			private Func<bool> function;

			public bool Execute() {
				return function();
			}

			// Constructor
			public Option(string label, Func<bool> function) {
				this.label = label;
				this.function = function;
			}
		}
	}
}