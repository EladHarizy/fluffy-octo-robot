using System;
using System.Text;

namespace lib {
	public class Order {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
			private set;
		}

		private HostingUnit hosting_unit;
		public ID HostingUnitKey {
			get => hosting_unit.ID;
		}

		private GuestRequest guest_request;
		public ID GuestRequestKey {
			get => guest_request.ID;
		}

		public Status status {
			get;
			set;
		}

		public DateTime CreationDate {
			get;
			private set;
		}

		// Email delivery date to customer (We'll have to come up with a more descriptive name for this variable)
		public DateTime OrderDate {
			get;
			private set;
		}

		Order(HostingUnit hosting_unit, GuestRequest guest_request) {
			this.hosting_unit = hosting_unit;
			this.guest_request = guest_request;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Order Details:");
			sb.AppendLine("Hosting Unit Key: " + HostingUnitKey);
			sb.AppendLine("Guest Request Key: " + GuestRequestKey);
			sb.AppendLine("Order Key: " + ID.ToString());
			sb.AppendLine("Status: " + status);
			return sb.ToString();
		}
	}
}