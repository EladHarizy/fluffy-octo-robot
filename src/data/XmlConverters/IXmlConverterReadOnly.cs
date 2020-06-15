using System.Xml.Linq;

namespace data {
	interface IXmlConverterReadOnly<T> {
		T XmlToObj(XElement element);
	}
}