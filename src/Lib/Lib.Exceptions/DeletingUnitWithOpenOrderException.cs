using System;
using Lib.Entities;

namespace Lib.Exceptions {
    public class DeletingUnitWithOpenOrderException : System.Exception {
        public Order Order { get; }

        public Unit Unit { get; }

        public DeletingUnitWithOpenOrderException() {}

        public DeletingUnitWithOpenOrderException(string message) : base(message) {}

        public DeletingUnitWithOpenOrderException(Unit unit, Order order) : this(unit, order, "Error: The unit " + unit.ID + " cannot be removed. Order number " + order.ID + " is still opened.") {}

        public DeletingUnitWithOpenOrderException(Unit unit, Order order, string message) : base(message) {
            Unit = unit;
            Order = order;
        }

        public DeletingUnitWithOpenOrderException(string message, System.Exception inner) : base(message, inner) {}

    }
}