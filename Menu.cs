using System;
using System.Collections.Generic;

namespace FluffyOctoRobot {
	partial class Menu {
		// The question or prompt that will be displayed to the user
		private string intro;

		// The list of options that will be displayed to the user
		private List<Option> options = new List<Option>();

		// Constructor that takes any number of options
		public Menu(string intro, params Option[] options) {
			this.intro = intro;
			foreach (Option option in options) {
				this.options.Add(option);
			}
		}

		private void Intro() {
			Console.Clear();
			Console.WriteLine(intro);
		}

		// Function that prints all the options and labels them with a number (starting from 1)
		private void PrintOptions() {
			Console.WriteLine();
			int i = 0;
			foreach (Option option in options) {
				Console.WriteLine("{1} - {0}", option.Label, ++i);
			}
			Console.WriteLine();
		}

		// Function that prompts for an option between 1 and options.Count and returns the number of the chosen option minus 1
		private int Read() {
			while (true) {
				Console.Write("Choose an option [1-{0}]: ", options.Count);
				int choice;
				try {
					choice = Int32.Parse(Console.ReadLine());
					if (choice < 1 || choice > options.Count) {
						throw new System.IO.InvalidDataException();
					}
					return choice - 1; // Minus 1 because options are displayed from 1
				} catch (Exception e) when(e is FormatException || e is System.IO.InvalidDataException) {
					Console.WriteLine("Error: Input must be an integer between 1 and {0}.", options.Count);
				}
			}
		}

		// Function that calls intro, then calls printOptions, then inputs an option, and executes the function of the corresponding option
		public void Init() {
			Intro();
			bool repeat;
			do {
				PrintOptions();
				int index = Read();
				repeat = options[index].Execute();
			} while (repeat);
		}
	}
}