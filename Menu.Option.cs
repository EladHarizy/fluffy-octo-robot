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
			private Func<Object[], bool> function;

			// The list of parameters that will be passed to function
			private Object[] args;

			public bool Execute() {
				return function(args);
			}

			// Constructor
			public Option(string label, Object[] args, Func<Object[], bool> function) {
				this.label = label;
				this.args = args;
				this.function = function;
			}
		}
	}
}