using System;

namespace Lib.Exceptions {
	public class NonPositiveException : Exception {
		public int Number { get; private set; }

		public NonPositiveException() : base("Error: Expected a positive number.") {}

		public NonPositiveException(int n) : this(n, "Error: Expected a positive number; received " + n.ToString() + '.') {}

		public NonPositiveException(string message) : base(message) {}

		public NonPositiveException(int n, string message) : base(message) {
			Number = n;
		}

		public NonPositiveException(string message, Exception inner) : base(message, inner) {}
	}
}