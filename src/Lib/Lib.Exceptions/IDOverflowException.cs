using System;

namespace Lib.Exceptions {
	public class IDOverflowException : Exception {
		public IDOverflowException() {}

		public IDOverflowException(int new_number, int digits) : this("Error: Tried to generate a new ID " + new_number + "for an object, but only " + digits + " digits are allowed.") {}

		public IDOverflowException(string message) : base(message) {}

		public IDOverflowException(string message, Exception inner) : base(message, inner) {}
	}
}