using System.Xml.Linq;

namespace data {
	interface IIndexedXmlConverter<TKey, TObj> : IXmlConverter<TObj> {
		TKey XmlToKey(XElement element);
	}
}