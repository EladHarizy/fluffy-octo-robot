using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	internal class OrderXmlConverter : IIndexedXmlConverter<ID, Order> {
		private DataAccessorReadOnly<ID, Unit> Units {
			get => DataFactory.Data.Unit;
		}

		private DataAccessorReadOnly<ID, GuestRequest> GuestRequests {
			get => DataFactory.Data.GuestRequest;
		}

		public XElement ObjToXml(Order order) {
			string guest_request_id = "deleted";
			if (order.GuestRequest != null) {
				guest_request_id = order.GuestRequest.ID;
			}

			string unit_id = "deleted";
			if (order.Unit != null) {
				unit_id = order.Unit.ID;
			}

			return new XElement("order",
				new XElement("id", order.ID),
				new XElement("unit_id", unit_id),
				new XElement("guest_request_id", guest_request_id),
				new XElement("status", order.OrderStatus),
				new XElement("creation_date", order.CreationDate),
				new XElement("email_delivery_date", order.EmailDeliveryDate),
				new XElement("message", order.Message)
			);
		}

		public Order XmlToObj(XElement element) {
			string email_delivery_date_string = element.Element("email_delivery_date").Value.Trim();
			Date? email_delivery_date = email_delivery_date_string == "" ? null : (Date?) Date.Parse(email_delivery_date_string);

			GuestRequest guest_request = null;
			string guest_request_id = element.Element("guest_request_id").Value.Trim();
			if (guest_request_id != "deleted") {
				guest_request = GuestRequests[guest_request_id];
			}

			Unit unit = null;
			string unit_id = element.Element("unit_id").Value.Trim();
			if (unit_id != "deleted") {
				unit = Units[unit_id];
			}

			return new Order(
				XmlToKey(element),
				unit,
				guest_request,
				element.Element("status").Value.Trim(),
				Date.Parse(element.Element("creation_date").Value.Trim()),
				email_delivery_date,
				element.Element("message").Value.Trim()
			);
		}

		public ID XmlToKey(XElement element) {
			return element.Element("id").Value.Trim();
		}
	}
}