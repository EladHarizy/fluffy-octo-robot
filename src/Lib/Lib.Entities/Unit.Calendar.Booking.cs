using System;
using Lib.Exceptions;

namespace Lib.Entities {
	public partial class Unit {
		public partial class Calendar {
			public class Booking : IComparable<Booking> {
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

				public int CompareTo(Booking other) {
					return Start < other.Start ? -1 : (Start > other.Start ? 1 : 0);
				}
			}
		}
	}
}