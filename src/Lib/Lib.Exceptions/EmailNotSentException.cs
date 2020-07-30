using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class EmailNotSentException : Exception {

		public EmailNotSentException() : this("Error: The Email could not be sent. Please check your internet connection.") {}

		public EmailNotSentException(string message) : base(message) {}

		public EmailNotSentException(string message, System.Exception inner) : base(message, inner) {}

		public EmailNotSentException(Order order, string message, System.Exception inner) : this(message, inner) {}

	}
}