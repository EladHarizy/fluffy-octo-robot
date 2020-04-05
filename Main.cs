using System;

namespace FluffyOctoRobot {
	class main {
		static void Main(string[] args) {
			Calendar calendar = new Calendar();
			Menu menu = new Menu("Welcome to the Fluffy Octo-Robot!\nHere you can manage the rent of your villa.\nWhat would you like to do?");

			menu.AddOption(
				"Add a new visit.",
				() => {
					while (true) {
						DateTime start = input<DateTime>("Enter start date: ");
						int duration = input<int>("Enter the number of nights to reserve: ");
						try {
							calendar.AddToCalendar(start, duration);
							Console.WriteLine("Booking created successfully.");
							return true; // True because we want the menu to repeat once the option does its task
						} catch (ApplicationException e) {
							Console.WriteLine(e.Message);
							Console.WriteLine();
						}
					}
				}
			);

			menu.AddOption(
				"Show the yearly occupancy dates.",
				() => {
					Console.WriteLine("The following dates are booked.");
					Console.Write(calendar.Occupancy());
					return true;
				}
			);

			menu.AddOption(
				"Show occupancy statistics.",
				() => {
					string today = DateTime.Now.ToString("dd/MM/yyyy");
					string next_year = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
					int occupied_days = calendar.OccupiedDays();
					float percentage = 100 * occupied_days / (DateTime.Now.AddYears(1) - DateTime.Now).Days;
					Console.WriteLine("There are {0} occupied days between today ({1}) and a year from now ({2}).", occupied_days, today, next_year);
					Console.WriteLine("That is {0}% occupied.", Math.Round(percentage, 2));
					return true;
				}
			);

			menu.AddOption(
				"Exit.",
				() => {
					Console.WriteLine("Goodbye.");
					return false;
				}
			);

			menu.Init();
		}

		// Function that reads in some input of type T and returns it. Will loop on invalid input.
		static T input<T>(string prompt = "") {
			while (true) {
				if (prompt != "") {
					Console.Write(prompt);
				}
				try {
					System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
					return (T) converter.ConvertFromString(Console.ReadLine());
				} catch (FormatException) {
					Console.WriteLine("Error: Unable to parse input.");
				}
			}
		}
	}
}