using System;
using System.Text;

namespace lib {
	public partial class Order : ICloneable<Order> {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
		}

		private HostingUnit hosting_unit;
		public ID HostingUnitKey {
			get => hosting_unit.ID;
		}

		private GuestRequest guest_request;
		public ID GuestRequestKey {
			get => guest_request.ID;
		}

		public Status OrderStatus {
			get;
			set;
		}

		public Date CreationDate {
			get;
		}

		// Email delivery date to customer (We'll have to come up with a more descriptive name for this variable)
		public Date? OrderDate {
			get;
			set;
		}

		public Order(HostingUnit hosting_unit, GuestRequest guest_request) : this(id_generator.Next(), hosting_unit, guest_request, new Status("Not Addressed"), Date.Today, null) {}

		public Order(ID id, HostingUnit hosting_unit, GuestRequest guest_request, Status status, Date creation_date, Date? order_date) {
			ID = id;
			this.hosting_unit = hosting_unit;
			this.guest_request = guest_request;
			OrderStatus = status;
			CreationDate = creation_date;
			OrderDate = order_date;
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
			sb.Append(OrderStatus);
			sb.Append("\n");

			return sb.ToString();
		}

		public Order Clone() {
			return new Order(ID, hosting_unit.Clone(), guest_request.Clone(), OrderStatus, CreationDate, OrderDate);
		}
	}
}