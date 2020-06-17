using System.Xml.Linq;

namespace data {
	public interface IXmlConverterReadOnly<T> {
		T XmlToObj(XElement element);
	}
}