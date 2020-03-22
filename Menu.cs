using System;
using System.Collections.Generic;

namespace FluffyOctoRobot {
	class Menu {
		// The question or prompt that will be displayed to the user
		private string prompt;

		// The list of options that will be displayed to the user
		private List<Option> options = new List<Option>();

		// Constructor that takes any number of options
		public Menu(string prompt, params Option[] options) {
			this.prompt = prompt;
			foreach (Option option in options) {
				this.options.Add(option);
			}
		}

		// Function that prints all the options and labels them with a number (starting from 1)
		private void Print() {
			Console.Clear();
			Console.WriteLine(prompt + '\n');

			int i = 0;
			foreach (Option option in options) {
				Console.WriteLine("\t{1} - {0}", options[i].Label, ++i);
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

		// Function that calls print, then inputs an option, and executes the function of the corresponding option
		public void Init() {
			bool repeat;
			do {
				Print();
				int index = Read();
				repeat = options[index].Execute();
			} while (repeat);
		}

		public class Option {
			private string label;
			public string Label {
				get => label;
			}

			// A function that will be called when the user chooses this option
			// It should take no parameters and return a bool indicating if the menu should be displayed again
			private Func<bool> function;
			public Func<bool> Execute {
				get => function;
			}

			// Constructor
			public Option(string label, Func<bool> function) {
				this.label = label;
				this.function = function;
			}
		}
	}
}