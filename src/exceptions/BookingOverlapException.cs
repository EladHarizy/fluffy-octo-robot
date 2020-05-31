using System;

namespace exceptions {
	public class BookingOverlapException : Exception {
		public BookingOverlapException() {}

		public BookingOverlapException(string message) : base(message) {}

		public BookingOverlapException(string message, Exception inner) : base(message, inner) {}
	}
}