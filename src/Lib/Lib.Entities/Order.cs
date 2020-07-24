using System;
using System.Text;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Order : ICloneable<Order>, IEntity<ID> {
		public ID ID { get; set; }

		public Unit Unit { get; }

		public GuestRequest GuestRequest { get; }

		public Status OrderStatus { get; set; }

		public Date CreationDate { get; }

		// Email delivery date to customer
		public Date? EmailDeliveryDate { get; set; }

		public string Message { get; set; }

		public Date StartDate {
			get => GuestRequest.StartDate;
		}

		public Date EndDate {
			get => GuestRequest.EndDate;
		}

		public int Duration {
			get => GuestRequest.Duration;
		}

		public Order(Unit hosting_unit, GuestRequest guest_request, string message) : this(null, hosting_unit, guest_request, new Status("Not addressed"), Date.Today, null, message) {}

		public Order(ID id, Unit hosting_unit, GuestRequest guest_request, Status status, Date creation_date, Date? email_delivery_date, string message) {
			ID = id;
			Unit = hosting_unit;
			GuestRequest = guest_request;
			OrderStatus = status;
			CreationDate = creation_date;
			EmailDeliveryDate = email_delivery_date;
			Message = message;
		}

		public override string ToString() {
			return "Order " + ID.ToString();
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Order Clone() {
			return new Order(ID, Unit.Clone(), GuestRequest.Clone(), OrderStatus, CreationDate, EmailDeliveryDate, Message);
		}
	}
}