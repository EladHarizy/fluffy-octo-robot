using System;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Interfaces;

namespace data {
	public class DataAccessor<T> : DataAccessorReadOnly<ID, T> where T : IIndexed<ID> {
		// An XElement which represents the root of the XML file
		private readonly XElement root;

		// Function that takes an object of type T and returns a new XElement representing an object of type T
		private readonly Func<T, XElement> obj_to_xml;

		public DataAccessor(
			string file_name,
			string collection_tag_name,
			Func<XElement, T> xml_to_obj,
			Func<T, XElement> obj_to_xml,
			Func<T, T> copier = null
		) : base(file_name, xml_to_obj, copier) {
			root = XElement.Load(file_name);
			collection_xml = root.Descendants(collection_tag_name).First();
			this.obj_to_xml = obj_to_xml;
		}

		public void Add(T obj) {
			if (obj.Key() == null) {
				obj.Key(next_key());
			}
			if (cache != null) {
				cache.Add(obj.Key(), copy(obj));
			}
			collection_xml.Add(obj_to_xml(obj));
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
			ID key = obj.Key();
			if (cache != null) {
				cache[key] = copy(obj);
			}
			get_xml(key).Remove();
			collection_xml.Add(obj_to_xml(obj));
			root.Save(FileName);
		}

		// Returns true if the XElement has the key provided
		private bool xml_matches_key(XElement element, ID key) {
			return xml_to_obj(element).Key().Equals(key);
		}

		// Given a key, returns an XElement representing the object with that key
		private XElement get_xml(ID key) {
			return collection_xml.Elements().First(element => xml_matches_key(element, key));
		}

		private ID next_key() {
			XElement xml_id = root.Element("next_id");
			string current_str = xml_id.Value;
			ID next_id = new ID(int.Parse(current_str) + 1, current_str.Length);
			xml_id.Value = next_id;
			root.Save(FileName);
			return next_id;
		}
	}
}