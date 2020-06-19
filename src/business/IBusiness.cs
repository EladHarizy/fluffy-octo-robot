using System.Collections.Generic;
using Lib.Entities;

namespace business {
	public interface IBusiness {
		// Add a customer request
		void AddGuestRequest(GuestRequest guest_request);

		// Customer request update (status change)
		void UpdateGuestRequest(GuestRequest guest_request);

		// Add a hosting unit
		void AddUnit(Unit unit);

		// Delete a hosting unit
		void DeleteUnit(Unit unit);

		// Hosting unit update
		void UpdateUnit(Unit unit1);

		// Add an order
		void AddOrder(Order order);

		// Order Update (Status Change)
		void UpdateOrder(Order order);

		// Get a list of all accommodation units
		ICollection<Unit> Units();

		// Receive a list of all customer requests
		ICollection<GuestRequest> GuestRequests();

		// Receive a list of all orders
		ICollection<Order> Orders();

		// Receives a list of all existing bank branches in Israel
		ICollection<BankBranch> BankBranches();
	}
}