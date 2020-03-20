using System;
using System.Collections.Generic;

namespace FluffyOctoRobot {
	class Menu {
		List<Option> options = new List<Option>();

		// Constructor that takes any number of options
		public Menu(params Option[] options) {
			foreach (Option option in options) {
				this.options.Add(option);
			}
		}

		// Function that prints all the options
		void Print() {
			foreach (Option option in options) {
				// TODO
			}
		}

		public Option Prompt() {

		}
	}
}