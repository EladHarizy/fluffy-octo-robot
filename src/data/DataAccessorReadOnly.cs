using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.Extensions;
using Lib.Interfaces;

namespace data {
	public class DataAccessorReadOnly<K, T> where T : IIndexedReadOnly<K> {
		public virtual string FileName { get; }

		// The parent tag of all the objects in question
		protected XElement collection_xml;

		// A dictionary that stores a copy of the data in the XML file
		protected IDictionary<K, T> cache;

		// Function that takes an XElement representing an object of type T and returns a new object of type T
		protected readonly Func<XElement, T> xml_to_obj;

		// Function that clones objects of type T. By default it returns a reference to the same object, which is acceptable if T is immutable
		protected readonly Func<T, T> copy;

		public ICollection<T> All {
			get {
				load_cache();
				return cache.Values.Clone();
			}
		}

		public virtual T this [K key] {
			get => Get(key);
		}

		public DataAccessorReadOnly(
			string file_name,
			string collection_tag_name,
			Func<XElement, T> xml_to_obj,
			Func<T, T> copier = null
		) : this(file_name, xml_to_obj, copier) {
			collection_xml = XElement.Load(file_name).Descendants(collection_tag_name).First();
		}

		protected DataAccessorReadOnly(
			string file_name,
			Func<XElement, T> xml_to_obj,
			Func<T, T> copier
		) {
			FileName = file_name;
			this.xml_to_obj = xml_to_obj;
			this.copy = copier ?? (x => x);
		}

		// Given a key, returns an object of type T
		public virtual T Get(K key) {
			load_cache();
			return copy(cache[key]);
		}

		// Loads XML into cache, if the cache is null
		protected virtual void load_cache() {
			if (cache == null) {
				cache = new Dictionary<K, T>();
				foreach (XElement element in collection_xml.Elements()) {
					cache.Add(xml_to_obj(element).Key(), xml_to_obj(element));
				}
			}
		}
	}
}