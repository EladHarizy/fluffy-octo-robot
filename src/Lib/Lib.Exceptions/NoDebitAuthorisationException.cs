using System;

namespace Lib.Exceptions {
	public class NoDebitAuthorisationException : Exception {
		public NoDebitAuthorisationException() : base("Error: Host is unauthorised by the bank for debit collection.") {}

		public NoDebitAuthorisationException(string message) : base(message) {}

		public NoDebitAuthorisationException(string message, Exception innerException) : base(message, innerException) {}
	}
}