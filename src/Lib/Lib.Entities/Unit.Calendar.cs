using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Lib.Exceptions;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit {
		public partial class Calendar : ICloneable<Calendar>, IEnumerable<Calendar.Booking> {

			// The list of Bookings
			// We were given the green light to continue using a set of bookings implementation rather than a matrix, since we already implemented all the methods this way in the first assignment.
			public SortedSet<Booking> Bookings { get; }

			public Calendar() {
				Bookings = new SortedSet<Booking>();
			}

			public Calendar(SortedSet<Booking> bookings) {
				Bookings = bookings;
			}

			public Calendar Clone() {
				Calendar calendar = new Calendar(new SortedSet<Booking>(Bookings));
				return calendar;
			}

			public IEnumerator<Booking> GetEnumerator() {
				return Bookings.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return Bookings.GetEnumerator();
			}

			public override string ToString() {
				return ToString(0);
			}

			public string ToString(int tabs) {
				StringBuilder sb = new StringBuilder();

				Date prev_end_date = Date.MaxValue;

				foreach (Booking booking in Bookings) {
					if (booking.End > prev_end_date) {
						sb.Append(prev_end_date.ToString("dd/MM/yyyy"));
						sb.Append('\n');
					}
					sb.Append('\t', tabs);
					sb.Append(booking.Start.ToString("dd/MM/yyyy"));
					sb.Append(" - ");
					prev_end_date = booking.End;
				}

				if (Bookings.Count > 0) {
					sb.Append(prev_end_date.ToString("dd/MM/yyyy"));
				}

				return sb.ToString();
			}
		}
	}
}