using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class DeletingUnitWithConfirmedOrderException : System.Exception {
		public Order Order { get; }

		public Unit Unit { get; }

		public DeletingUnitWithConfirmedOrderException() {}

		public DeletingUnitWithConfirmedOrderException(string message) : base(message) {}

		public DeletingUnitWithConfirmedOrderException(Unit unit, Order order) : this(unit, order, "Error: The unit " + unit.ID + " cannot be removed. Order number " + order.ID + " is still confirmed with this unit.") {}

		public DeletingUnitWithConfirmedOrderException(Unit unit, Order order, string message) : base(message) {
			Unit = unit;
			Order = order;
		}

		public DeletingUnitWithConfirmedOrderException(string message, System.Exception inner) : base(message, inner) {}

	}
}