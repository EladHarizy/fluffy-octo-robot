using System;

namespace exceptions {
	public class PastDateException : Exception {

		public PastDateException() : base("Error: Date cannot be in the past.") {}

		public PastDateException(string message) : base(message) {}

		public PastDateException(string message, Exception inner) : base(message, inner) {}
	}
}