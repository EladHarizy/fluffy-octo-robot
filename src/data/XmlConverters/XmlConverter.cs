using System;
using System.Xml.Linq;

namespace data {
	class XmlConverter<T> : IXmlConverter<T> {
		private readonly Func<T, XElement> obj_to_xml;

		private readonly Func<XElement, T> xml_to_obj;

		public XmlConverter(Func<T, XElement> obj_to_xml, Func<XElement, T> xml_to_obj) {
			this.obj_to_xml = obj_to_xml;
			this.xml_to_obj = xml_to_obj;
		}

		public XElement ObjToXml(T obj) {
			return obj_to_xml(obj);
		}

		public T XmlToObj(XElement element) {
			return xml_to_obj(element);
		}
	}
}