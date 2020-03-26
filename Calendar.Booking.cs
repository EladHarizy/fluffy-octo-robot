using System;
namespace FluffyOctoRobot {
	partial class Calendar {
		class Booking {
			// duration of visit
			public int Duration {
				get;
				private set;
			}

			//date of first night of stay
			private DateTime dateTime;

			//constructor that takes in initial date and duration
			public Booking(DateTime dt, int duration) {
				Console.Write("Enter Start Date: ");
				DateTime choice;
				try {
					choice = DateTime.Parse(Console.ReadLine());
					if (choice < DateTime.Now) {
						throw new System.IO.InvalidDataException();
					}
					Duration = duration;
					this.dateTime = dt;
				} catch {

				}
			}

			// returns the end date
			DateTime endDate() {
				DateTime newDate = dateTime.AddDays(Duration);
				return (newDate);
			}
		}
	}
}