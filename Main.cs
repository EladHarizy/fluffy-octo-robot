using System;

namespace FluffyOctoRobot {
	class main {
		static void Main(string[] args) {
			Menu menu = new Menu(
				"Welcome to the Fluffy Octo-Robot!\nHere you can manage the rent of your villa.\nWhat would you like to do?",
				new Menu.Option(
					"Add a new visit.",
					new Object[] {
						// Parameters to the lambda below should be passed here
						// They can be accessed in the lambda by args[i]
					},
					(Object[] args) => {
						Console.WriteLine(args[0]);
						// Elad, this is for you. This is what should execute when the user picks option a
						return true; // True because we want the menu to repeat once the option does its task
					}
				),
				new Menu.Option(
					"Show the yearly occupancy dates.",
					new Object[] {
						// Parameters to the lambda below should be passed here
						// They can be accessed in the lambda by args[i]
					},
					(Object[] args) => {
						// Elad, this is for you. This is what should execute when the user picks option b
						return true;
					}
				),
				new Menu.Option(
					"Show occupancy statistics.",
					new Object[] {
						// Parameters to the lambda below should be passed here
						// They can be accessed in the lambda by args[i]
					},
					(Object[] args) => {
						// Elad, this is for you. This is what should execute when the user picks option c
						return true;
					}
				),
				new Menu.Option(
					"Exit",
					null,
					(Object[] args) => {
						return false;
					}
				)
			);

			menu.Init();
		}
	}
}