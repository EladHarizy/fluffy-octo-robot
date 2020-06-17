using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace data {
	class CollectionXmlConverter<T, TCollection> : IXmlConverter<ICollection<T>> where TCollection : ICollection<T>, new() {
		public string CollectionTag { get; }

		IXmlConverter<T> SubConverter { get; }

		public CollectionXmlConverter(string collection_tag, string element_tag) {
			CollectionTag = collection_tag;
			SubConverter = new XmlConverter<T>(
				x => new XElement(element_tag, x.ToString()),
				element => (T) Activator.CreateInstance(typeof(T), element.Value)
			);
		}

		public CollectionXmlConverter(string collection_tag, IXmlConverter<T> sub_converter) {
			CollectionTag = collection_tag;
			SubConverter = sub_converter;
		}

		public XElement ObjToXml(ICollection<T> collection) {
			XElement collection_xml = new XElement(CollectionTag);
			foreach (T obj in collection) {
				collection_xml.Add(SubConverter.ObjToXml(obj));
			}
			return collection_xml;
		}

		public ICollection<T> XmlToObj(XElement element) {
			return XmlToConcreteObj(element);
		}

		public TCollection XmlToConcreteObj(XElement element) {
			TCollection collection = new TCollection();
			foreach (XElement element_xml in element.Elements()) {
				collection.Add(SubConverter.XmlToObj(element_xml));
			}
			return collection;
		}
	}
}