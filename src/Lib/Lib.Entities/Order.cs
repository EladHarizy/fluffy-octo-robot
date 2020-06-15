using System;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Order : ICloneable<Order>, IIndexed<ID> {
		public ID ID { get; set; }

		private Unit hosting_unit;
		public ID UnitKey {
			get => hosting_unit.ID;
		}

		private GuestRequest guest_request;
		public ID GuestRequestKey {
			get => guest_request.ID;
		}

		public Status OrderStatus { get; set; }

		public Date CreationDate { get; }

		// Email delivery date to customer (We'll have to come up with a more descriptive name for this variable)
		public Date? OrderDate { get; set; }

		public Order(Unit hosting_unit, GuestRequest guest_request) : this(null, hosting_unit, guest_request, new Status("Not Addressed"), Date.Today, null) {}

		public Order(ID id, Unit hosting_unit, GuestRequest guest_request, Status status, Date creation_date, Date? order_date) {
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
			sb.Append(UnitKey);
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

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Order Clone() {
			return new Order(ID, hosting_unit.Clone(), guest_request.Clone(), OrderStatus, CreationDate, OrderDate);
		}
	}
}