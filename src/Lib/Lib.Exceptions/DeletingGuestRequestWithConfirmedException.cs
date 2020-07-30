using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class DeletingGuestRequestWithConfirmedOrderException : System.Exception {
		public Order Order { get; }

		public GuestRequest GuestRequest { get; }

		public DeletingGuestRequestWithConfirmedOrderException() {}

		public DeletingGuestRequestWithConfirmedOrderException(string message) : base(message) {}

		public DeletingGuestRequestWithConfirmedOrderException(GuestRequest guest_request, Order order) : this(guest_request, order, "Error: The guest request " + guest_request.ID + " cannot be removed because order number " + order.ID + " is confirmed with this request.") {}

		public DeletingGuestRequestWithConfirmedOrderException(GuestRequest guest_request, Order order, string message) : base(message) {
			GuestRequest = guest_request;
			Order = order;
		}

		public DeletingGuestRequestWithConfirmedOrderException(string message, System.Exception inner) : base(message, inner) {}

	}
}