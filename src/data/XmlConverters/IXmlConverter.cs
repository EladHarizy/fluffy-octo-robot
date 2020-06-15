using System.Xml.Linq;

namespace data {
	interface IXmlConverter<T> : IXmlConverterReadOnly<T> {
		XElement ObjToXml(T obj);
	}
}