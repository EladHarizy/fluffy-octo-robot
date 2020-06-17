using System.Xml.Linq;
using Lib.Entities;

namespace data {
	internal class UnitTypeXmlConverter : IXmlConverterReadOnly<Unit.Type> {
		public Unit.Type XmlToObj(XElement element) {
			return new Unit.Type(element.Element("name").Value);
		}
	}
}