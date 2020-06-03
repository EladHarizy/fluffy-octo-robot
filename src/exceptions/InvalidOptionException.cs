using System;

namespace exceptions {
	public class InvalidOptionException : Exception {
		public InvalidOptionException() {}

		public InvalidOptionException(string message) : base(message) {}

		public InvalidOptionException(string message, Exception inner) : base(message, inner) {}
	}
}