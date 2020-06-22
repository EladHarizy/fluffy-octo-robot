using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class InitialStatusException : Exception {
		public Order.Status Status { get; }

		public InitialStatusException() {}

		public InitialStatusException(string message) : base(message) {}

		public InitialStatusException(Order.Status status) : this(status, "Error: When adding a new order, the status must be 'Not addressed'. The received order had status '" + status + "'.") {}

		public InitialStatusException(Order.Status status, string message) : this(message) {
			Status = status;
		}

		public InitialStatusException(string message, Exception innerException) : base(message, innerException) {}
	}
}