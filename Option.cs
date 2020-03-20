namespace FluffyOctoRobot {
	abstract class Option {
		// Fields and properties
		string description;
		public string Description {
			get;
		}

		// Constructor
		public Option(string description) {
			this.Description = description;
		}

		// The function that gets called when the option is selected
		public abstract void Execute();
	}

	class GuestRequestOption : Option {

		public GuestRequestOption() : base("Add a new visit.") {}

		public override void Execute() {
			// Elad, this is for you. This is what should execute when the user picks option a
		}
	}

	class ShowOccupancyOption : Option {

		public ShowOccupancyOption() : base("Show the yearly occupancy dates.") {}

		public override void Execute() {
			// Elad, this is for you. This is what should execute when the user picks option b
		}
	}

	class ShowStatsOption : Option {

		public ShowStatsOption() : base("Show occupancy statistics.") {}

		public override void Execute() {
			// Elad, this is for you. This is what should execute when the user picks option c
		}
	}

	class ExitOption : Option {

		public ExitOption() : base("Exit.") {}

		public override void Execute() {
			// Elad, this is for you. This is what should execute when the user picks option d
		}
	}
}