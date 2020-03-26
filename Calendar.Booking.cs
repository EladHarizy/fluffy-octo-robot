using System;
namespace FluffyOctoRobot {
	partial class Calendar {
		class Booking {
			// duration of visit
			//	private int duration;
			public int Duration {
				get;
				private set;
			}

			//date of first night of stay
			private DateTime dateTime;

			//constructor that takes in initial date and duration
			public Booking(DateTime dt, int duration) {
				Duration = duration;
				this.dateTime = dt;
			}

			// returns the end date
			DateTime endDate() {
				DateTime newDate = dateTime.AddDays(Duration);
				return (newDate);
			}
		}
	}
}