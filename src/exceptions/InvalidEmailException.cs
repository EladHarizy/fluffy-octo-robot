using System;

namespace exceptions {
	public class InvalidEmailException : Exception {
		public string Email { get; private set; }

		public InvalidEmailException() {}

		public InvalidEmailException(string email) : this(email, "Error: Invalid email address " + email + '.') {}

		public InvalidEmailException(string email, string message) : base(message) {
			Email = email;
		}

		public InvalidEmailException(string message, Exception inner) : base(message, inner) {}
	}
}