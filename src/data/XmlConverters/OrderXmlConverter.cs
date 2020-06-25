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
				element.Element("id").Value,
				Units[element.Element("unit_id").Value],
				GuestRequests[element.Element("guest_request_id").Value],
				element.Element("status").Value,
				Date.Parse(element.Element("creation_date").Value),
				Date.Parse(element.Element("email_delivery_date").Value)
			);
		}
	}
}