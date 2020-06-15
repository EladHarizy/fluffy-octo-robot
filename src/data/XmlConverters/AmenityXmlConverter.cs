using System.Net;
using System.Xml.Linq;
using Lib.Entities;

namespace data {
	class AmenityXmlConverter : IXmlConverterReadOnly<Amenity> {
		public Amenity XmlToObj(XElement element) {
			return new Amenity(element.Element("name").Value);
		}
	}
}