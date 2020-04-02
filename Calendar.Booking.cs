using System;
namespace FluffyOctoRobot {
	partial class Calendar {
		private class Booking {
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

			// Returns the end date
			public DateTime End {
				get => Start.AddDays(Duration);
			}

			// Constructor that takes in initial date and duration
			public Booking(DateTime start, int duration) {
				// Check to see if the start date is not in the past
				if (start < DateTime.Now.Date) {
					throw new ArgumentOutOfRangeException("Error: Booking cannot be before current date");
				}
				// Check to see if duration is at least one day
				if (duration < 1) {
					throw new ArgumentOutOfRangeException("Error: Duration must be at least one night");
				}
				Start = start.Date;
				Duration = duration;
			}

			// Function that given two bookings will return true if they overlap
			public bool Overlaps(Booking booking) {
				// Two bookings will overlap iff one starts inside the other
				return (this.Start >= booking.Start && this.Start < booking.End)
					|| (booking.Start >= this.Start && booking.Start < this.End);
			}
		}
	}
}