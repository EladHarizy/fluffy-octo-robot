using System;
using System.Linq;
using System.Xml.Linq;
using Lib.Exceptions;
using Lib.Interfaces;

namespace data {
	public class DataAccessor<TKey, TObj> : DataAccessorReadOnly<TKey, TObj> where TKey : IAutoIndexable<TKey> where TObj : IIndexed<TKey> {
		// An XElement which represents the root of the XML file
		private readonly XElement root;

		private IXmlConverter<TObj> Converter {
			get => (IXmlConverter<TObj>) ConverterReadOnly;
		}

		internal DataAccessor(
			string file_name,
			string collection_tag_name,
			IXmlConverter<TObj> converter,
			Func<TObj, TObj> copier = null
		) : base(file_name, converter) {
			root = XElement.Load(file_name);
			collection_xml = root.Descendants(collection_tag_name).First();
		}

		public void Add(TObj obj) {
			if (obj.Key() != null) {
				throw new ObjectInDBException(obj, "Object with ID " + obj.Key() + " already exits in the database.");
			}
			obj.Key(next_key());
			if (cache != null) {
				cache.Add(obj.Key(), clone(obj));
			}
			collection_xml.Add(Converter.ObjToXml(obj));
			root.Save(FileName);
		}

		public void Remove(TKey key) {
			if (cache != null) {
				cache.Remove(key);
			}
			get_xml(key).Remove();
			root.Save(FileName);
		}

		public void Update(TObj obj) {
			if (obj.Key() == null) {
				throw new ObjectNotInDBException(obj, "This object does not exits in the database.");
			}
			TKey key = obj.Key();
			if (cache != null) {
				cache[key] = clone(obj);
			}
			get_xml(key).Remove();
			collection_xml.Add(Converter.ObjToXml(obj));
			root.Save(FileName);
		}

		// Returns true if the XElement has the key provided
		private bool xml_matches_key(XElement element, TKey key) {
			return ConverterReadOnly.XmlToObj(element).Key().Equals(key);
		}

		// Given a key, returns an XElement representing the object with that key
		private XElement get_xml(TKey key) {
			return collection_xml.Elements().First(element => xml_matches_key(element, key));
		}

		// Retrieves the last assigned ID from the XML file, and returns a new ID for the next number, updating the XML as well
		private TKey next_key() {
			XElement xml_id = root.Element("current_id");
			TKey next_id = ((TKey) Activator.CreateInstance(typeof(TKey), xml_id.Value)).Next();
			xml_id.Value = next_id.ToString();
			root.Save(FileName);
			return next_id;
		}
	}
}