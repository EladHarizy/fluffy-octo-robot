using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class OrderStatusChangeException : Exception {
		public Order Order { get; }

		public OrderStatusChangeException() : this("Error: Order status could not be changed.") {}

		public OrderStatusChangeException(string message) : base(message) {}

		public OrderStatusChangeException(Order order) : this(order, "Error: Order " + order.ID + "'s status could not be changed.") {}

		public OrderStatusChangeException(Order order, string message) : this(message) {
			Order = order;
		}

		public OrderStatusChangeException(string message, System.Exception inner) : base(message, inner) {}

		public OrderStatusChangeException(Order order, string message, System.Exception inner) : this(message, inner) {}

	}
}