using System;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Order : ICloneable<Order>, IIndexed<ID> {
		public ID ID { get; set; }

		public Unit Unit { get; }

		public GuestRequest GuestRequest { get; }

		public Status OrderStatus { get; set; }

		public Date CreationDate { get; }

		// Email delivery date to customer
		public Date? EmailDeliveryDate { get; set; }

		public Order(Unit hosting_unit, GuestRequest guest_request) : this(null, hosting_unit, guest_request, new Status("Not Addressed"), Date.Today, null) {}

		public Order(ID id, Unit hosting_unit, GuestRequest guest_request, Status status, Date creation_date, Date? order_date) {
			ID = id;
			Unit = hosting_unit;
			GuestRequest = guest_request;
			OrderStatus = status;
			CreationDate = creation_date;
			EmailDeliveryDate = order_date;
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
			sb.Append(Unit.ID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Guest Request Key:\t");
			sb.Append(GuestRequest.ID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Status:\t\t\t");
			sb.Append(OrderStatus);
			sb.Append("\n");

			return sb.ToString();
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Order Clone() {
			return new Order(ID, Unit.Clone(), GuestRequest.Clone(), OrderStatus, CreationDate, EmailDeliveryDate);
		}
	}
}