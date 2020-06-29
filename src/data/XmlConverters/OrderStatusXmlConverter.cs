using System.Xml.Linq;
using Lib.Entities;

namespace data {
	internal class OrderStatusXmlConverter : IXmlConverterReadOnly<Order.Status> {
		public Order.Status XmlToObj(XElement element) {
			return new Order.Status(element.Element("name").Value.Trim());
		}
	}
}