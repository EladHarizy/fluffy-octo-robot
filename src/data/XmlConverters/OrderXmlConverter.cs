using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	internal class OrderXmlConverter : IXmlConverter<Order> {
		private DataAccessorReadOnly<ID, Unit> units = new DataAccessorReadOnly<ID, Unit>("../data_files/units.xml", "units", new UnitXmlConverterReadOnly());

		private DataAccessorReadOnly<ID, GuestRequest> guest_requests = new DataAccessorReadOnly<ID, GuestRequest>("../data_files/guest_requests.xml", "guest_requests", new GuestRequestXmlConverterReadOnly());

		public XElement ObjToXml(Order obj) {
			throw new System.NotImplementedException();
		}

		public Order XmlToObj(XElement element) {
			return new Order(
				element.Element("id").Value,
				units[element.Element("unit_id").Value],
				guest_requests[element.Element("guest_request_id").Value],
				element.Element("status").Value,
				Date.Parse(element.Element("creation_date").Value),
				Date.Parse(element.Element("email_delivery_date").Value)
			);
		}
	}
}