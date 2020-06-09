using System;
using System.Text;

namespace lib {
	public partial class Order {
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

		public Date CreationDate {
			get;
			private set;
		}

		// Email delivery date to customer (We'll have to come up with a more descriptive name for this variable)
		public Date OrderDate {
			get;
			private set;
		}

		Order(HostingUnit hosting_unit, GuestRequest guest_request) {
			this.hosting_unit = hosting_unit;
			this.guest_request = guest_request;
		}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();
			sb.Append('\t', tabs);
			sb.Append("Order Details");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("-------------");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Order ID:\t\t");
			sb.Append(ID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Hosting Unit Key:\t");
			sb.Append(HostingUnitKey);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Guest Request Key:\t");
			sb.Append(GuestRequestKey);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Status:\t\t\t");
			sb.Append(status);
			sb.Append("\n");

			return sb.ToString();
		}
	}
}