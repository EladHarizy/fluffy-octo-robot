using System;
using System.Runtime.Serialization;
using Lib.DataTypes;

namespace Lib.Exceptions {
	public class InexistentEmailException : Exception {
		public Email Email { get; }

		public InexistentEmailException() {}

		public InexistentEmailException(Email email) : this("Error: There is no one in the system with the email '" + email + "'.") {
			Email = email;
		}

		public InexistentEmailException(Email email, Exception inner) : this("Error: There is no one in the system with the email '" + email + "'.", inner) {
			Email = email;
		}

		public InexistentEmailException(string message) : base(message) {}

		public InexistentEmailException(string message, Exception inner) : base(message, inner) {}

		protected InexistentEmailException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}