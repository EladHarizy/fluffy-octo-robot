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

		public XElement ObjToXml(Order obj) {
			throw new System.NotImplementedException();
		}

		public Order XmlToObj(XElement element) {
			return new Order(
				element.Element("id").Value.Trim(),
				Units[element.Element("unit_id").Value.Trim()],
				GuestRequests[element.Element("guest_request_id").Value.Trim()],
				element.Element("status").Value.Trim(),
				Date.Parse(element.Element("creation_date").Value.Trim()),
				Date.Parse(element.Element("email_delivery_date").Value.Trim())
			);
		}
	}
}