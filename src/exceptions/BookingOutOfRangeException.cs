using System;

namespace exceptions {
	public class BookingOutOfRangeException : Exception {
		public BookingOutOfRangeException() {}

		public BookingOutOfRangeException(string message) : base(message) {}

		public BookingOutOfRangeException(string message, Exception inner) : base(message, inner) {}
	}
}