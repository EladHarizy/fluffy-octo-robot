using System;

namespace exceptions {
	public class InvalidPhoneException : Exception {
		public string Phone {
			get;
			private set;
		}

		public InvalidPhoneException() {}

		public InvalidPhoneException(string phone) : this(phone, "Error: Invalid phone number " + phone + '.') {}

		public InvalidPhoneException(string phone, string message) : base(message) {
			Phone = phone;
		}

		public InvalidPhoneException(string message, Exception inner) : base(message, inner) {}
	}
}