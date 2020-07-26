using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class OrderStatusChangedException : Exception {
		public Order Order { get; }

		public OrderStatusChangedException() : this("Error: Order status could not be changed.") {}

		public OrderStatusChangedException(string message) : base(message) {}

		public OrderStatusChangedException(Order order) : this(order, "Error: Order " + order.ID + "'s status could not be changed.") {}

		public OrderStatusChangedException(Order order, string message) : this(message) {
			Order = order;
		}

		public OrderStatusChangedException(string message, System.Exception inner) : base(message, inner) {}

		public OrderStatusChangedException(Order order, string message, System.Exception inner) : this(message, inner) {}

	}
}