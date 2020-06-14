using System;

namespace Lib.Exceptions {
	public class IDOverflowException : Exception {
		public IDOverflowException() {}

		public IDOverflowException(string message) : base(message) {}

		public IDOverflowException(string message, Exception inner) : base(message, inner) {}
	}
}