using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	internal class OrderXmlConverter : IXmlConverter<Order> {
		private DataAccessorReadOnly<ID, Unit> Units {
			get => DataFactory.Data.Unit;
		}

		private DataAccessorReadOnly<ID, GuestRequest> GuestRequests {
			get => DataFactory.Data.GuestRequest;
		}

		public XElement ObjToXml(Order order) {
			return new XElement("order",
				new XElement("id", order.ID),
				new XElement("unit_id", order.Unit.ID),
				new XElement("guest_request_id", order.GuestRequest.ID),
				new XElement("status", order.OrderStatus),
				new XElement("creation_date", order.CreationDate),
				new XElement("email_delivery_date", order.EmailDeliveryDate),
				new XElement("message", order.Message)
			);
		}

		public Order XmlToObj(XElement element) {
			string email_delivery_date_string = element.Element("email_delivery_date").Value.Trim();
			Date? email_delivery_date = email_delivery_date_string == "" ? null : (Date?) Date.Parse(email_delivery_date_string);
			return new Order(
				element.Element("id").Value.Trim(),
				Units[element.Element("unit_id").Value.Trim()],
				GuestRequests[element.Element("guest_request_id").Value.Trim()],
				element.Element("status").Value.Trim(),
				Date.Parse(element.Element("creation_date").Value.Trim()),
				email_delivery_date,
				element.Element("message").Value.Trim()
			);
		}
	}
}