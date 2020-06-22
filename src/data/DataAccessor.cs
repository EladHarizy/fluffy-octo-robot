using System;
using System.Linq;
using System.Xml.Linq;
using Lib.Config;
using Lib.DataTypes;
using Lib.Exceptions;
using Lib.Interfaces;

namespace data {
	public class DataAccessor<T> : DataAccessorReadOnly<ID, T> where T : IIndexed<ID> {
		// An XElement which represents the root of the XML file
		private readonly XElement root;

		private IXmlConverter<T> Converter {
			get => (IXmlConverter<T>) ConverterReadOnly;
		}

		internal DataAccessor(
			string file_name,
			string collection_tag_name,
			IXmlConverter<T> converter,
			Func<T, T> copier = null
		) : base(file_name, converter) {
			root = XElement.Load(file_name);
			collection_xml = root.Descendants(collection_tag_name).First();
		}

		public void Add(T obj) {
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

		public void Remove(ID key) {
			if (cache != null) {
				cache.Remove(key);
			}
			get_xml(key).Remove();
			root.Save(FileName);
		}

		public void Update(T obj) {
			if (obj.Key() == null) {
				throw new ObjectNotInDBException(obj, "This object does not exits in the database.");
			}
			ID key = obj.Key();
			if (cache != null) {
				cache[key] = clone(obj);
			}
			get_xml(key).Remove();
			collection_xml.Add(Converter.ObjToXml(obj));
			root.Save(FileName);
		}

		// Returns true if the XElement has the key provided
		private bool xml_matches_key(XElement element, ID key) {
			return ConverterReadOnly.XmlToObj(element).Key().Equals(key);
		}

		// Given a key, returns an XElement representing the object with that key
		private XElement get_xml(ID key) {
			return collection_xml.Elements().First(element => xml_matches_key(element, key));
		}

		// Retrieves the last assigned ID from the XML file, and returns a new ID for the next number, updating the XML as well
		private ID next_key() {
			ID next_id = Config.LatestID(FileName).Next();
			Config.LatestID(FileName, next_id);
			return next_id;
		}
	}
}