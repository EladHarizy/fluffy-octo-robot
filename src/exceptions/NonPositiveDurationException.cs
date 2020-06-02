using System;

namespace exceptions {
	public class NonPositiveDurationException : NonPositiveException {
		public NonPositiveDurationException() : base() {}

		public NonPositiveDurationException(int n) : base(n) {}

		public NonPositiveDurationException(string message) : base(message) {}

		public NonPositiveDurationException(int n, string message) : base(n, message) {}

		public NonPositiveDurationException(string message, Exception inner) : base(message, inner) {}
	}
}