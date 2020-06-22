using Lib.Entities;

namespace Lib.Exceptions {
	public class OrderClosedException : System.Exception {
		public Order Order { get; }

		public OrderClosedException() {}

		public OrderClosedException(string message) : base(message) {}

		public OrderClosedException(Order order) : this(order, "Error: Order " + order.ID + " is already closed.") {}

		public OrderClosedException(Order order, string message) : this(message) {
			Order = order;
		}

		public OrderClosedException(string message, System.Exception inner) : base(message, inner) {}

	}
}