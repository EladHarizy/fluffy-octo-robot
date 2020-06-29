using System.Xml.Linq;
using Lib.Entities;

namespace data {
	class CityXmlConverter : IXmlConverterReadOnly<City> {
		public City XmlToObj(XElement element) {
			return new City(element.Element("name").Value.Trim());
		}
	}
}