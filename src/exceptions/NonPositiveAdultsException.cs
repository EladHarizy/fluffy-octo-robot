using System;

namespace exceptions {
	public class NonPositiveAdultsException : NonPositiveException {
		public NonPositiveAdultsException() : base() {}

		public NonPositiveAdultsException(int n) : base(n) {}

		public NonPositiveAdultsException(string message) : base(message) {}

		public NonPositiveAdultsException(int n, string message) : base(n, message) {}

		public NonPositiveAdultsException(string message, Exception inner) : base(message, inner) {}
	}
}