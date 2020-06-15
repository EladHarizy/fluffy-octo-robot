using System;
using Lib.Exceptions;

namespace Lib.Entities {
	public partial class Unit {
		private partial class Calendar {
			private class Booking {
				//  Duration of visit
				public int Duration { get; }

				// Date of first night of stay
				public Date Start { get; }

				// Returns the end date
				public Date End {
					get => Start.AddDays(Duration);
				}

				// Constructor that takes in initial date and duration
				public Booking(Date start, int duration) {
					if (duration < 1) {
						throw new NonPositiveDurationException("Error: Duration must be at least one night.");
					}
					Start = start;
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
}