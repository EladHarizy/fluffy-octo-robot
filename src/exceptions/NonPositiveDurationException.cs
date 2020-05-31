using System;

namespace exceptions {
	public class NonPositiveDurationException : Exception {
		public NonPositiveDurationException() {}

		public NonPositiveDurationException(string message) : base(message) {}

		public NonPositiveDurationException(string message, Exception inner) : base(message, inner) {}
	}
}