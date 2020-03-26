using System;

namespace FluffyOctoRobot {
	partial class Menu {
		public class Option {
			public string Label {
				get;
				private set;
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
				Label = label;
				this.args = args;
				this.function = function;
			}
		}
	}
}