namespace FluffyOctoRobot {
	class main {
		static void Main(string[] args) {
			Menu menu = new Menu(
				"Welcome to the Fluffy Octo-Robot!\nHere you can manage the rent of your villa.\nWhat would you like to do?",
				new Menu.Option(
					"Add a new visit.",
					() => {
						// Elad, this is for you. This is what should execute when the user picks option a
						return true; // True because we want the menu to repeat once the option does its task
					}
				),
				new Menu.Option(
					"Show the yearly occupancy dates.",
					() => {
						// Elad, this is for you. This is what should execute when the user picks option b
						return true;
					}
				),
				new Menu.Option(
					"Show occupancy statistics.",
					() => {
						// Elad, this is for you. This is what should execute when the user picks option c
						return true;
					}
				),
				new Menu.Option(
					"Exit",
					() => {
						return false;
					}
				)
			);

			menu.Init();
		}
	}
}