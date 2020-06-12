using System;
using System.Linq;
using System.Xml.Linq;
using exceptions;

namespace data {
	public class DataAccessor<K, T> : DataAccessorReadOnly<K, T> {
		// An XElement which represents the root of the XML file
		protected readonly XElement root;

		// Function that takes an object of type T and returns a new XElement representing an object of type T
		protected readonly Func<T, XElement> obj_to_xml;

		// Function that takes an object of type T and returns its key
		protected Func<T, K> obj_to_key;

		public DataAccessor(
			string file_name,
			string collection_tag_name,
			Func<XElement, K> xml_to_key,
			Func<XElement, T> xml_to_obj,
			Func<T, XElement> obj_to_xml,
			Func<T, K> obj_to_key,
			Func<T, T> copier = null
		) : base(file_name, xml_to_key, xml_to_obj, copier) {
			root = XElement.Load(file_name);
			collection_xml = root.Descendants(collection_tag_name).First();
			this.obj_to_xml = obj_to_xml;
			this.obj_to_key = obj_to_key;
		}

		public void Add(K key, T obj) {
			if (!key.Equals(obj_to_key(obj))) {
				throw new KeyObjectMismatch("Error: Cannot add this object because it does not match the key that was passed.");
			}
			if (cache != null) {
				cache.Add(key, obj);
			}
			collection_xml.Add(obj_to_xml(obj));
			root.Save(FileName);
		}

		public void Remove(K key) {
			if (cache != null) {
				cache.Remove(key);
			}
			GetXml(key).Remove();
			root.Save(FileName);
		}

		public void Update(K key, T obj) {
			if (!key.Equals(obj_to_key(obj))) {
				throw new KeyObjectMismatch("Error: Cannot update this object because it does not match the key that was passed.");
			}
			if (cache != null) {
				cache[key] = obj;
			}
			GetXml(key).Remove();
			collection_xml.Add(obj_to_xml(obj));
			root.Save(FileName);
		}

		// Given a key, returns an XElement representing the object with that key
		protected virtual XElement GetXml(K key) {
			return collection_xml.Elements().First(element => xml_matches_key(element, key));
		}
	}
}