using System;
namespace FluffyOctoRobot {
	partial class Calendar {
		class Booking {
			//  Duration of visit
			public int Duration {
				get;
				private set;
			}

			// Date of first night of stay
			public DateTime Start {
				get;
				private set;
			}

			// Constructor that takes in initial date and duration
			public Booking(DateTime start, int duration) {
				Duration = duration;
				Start = start;
			}

			// Returns the end date
			public DateTime End() {
				return Start.AddDays(Duration);
			}
		}
	}
}