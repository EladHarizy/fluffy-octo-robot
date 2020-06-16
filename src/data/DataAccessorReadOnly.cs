using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.Extensions;
using Lib.Interfaces;

namespace data {
	public class DataAccessorReadOnly<TKey, TObj> where TObj : IIndexedReadOnly<TKey> {
		public virtual string FileName { get; }

		// The parent tag of all the objects in question
		protected XElement collection_xml;

		// A dictionary that stores a copy of the data in the XML file
		protected IDictionary<TKey, TObj> cache;

		// Function that takes an XElement representing an object of type TObj and returns a new object of type TObj
		protected readonly Func<XElement, TObj> xml_to_obj;

		protected readonly bool cloneable;

		public ICollection<TObj> All {
			get {
				load_cache();
				return cache.Values.Clone();
			}
		}

		public virtual TObj this [TKey key] {
			get => Get(key);
		}

		public DataAccessorReadOnly(
			string file_name,
			string collection_tag_name,
			Func<XElement, TObj> xml_to_obj
		) : this(file_name, xml_to_obj) {
			collection_xml = XElement.Load(file_name).Descendants(collection_tag_name).First();
		}

		protected DataAccessorReadOnly(
			string file_name,
			Func<XElement, TObj> xml_to_obj
		) {
			FileName = file_name;
			this.xml_to_obj = xml_to_obj;
			cloneable = typeof(TObj).GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICloneable<>));
		}

		// Given a key, returns an object of type TObj
		public virtual TObj Get(TKey key) {
			load_cache();
			return clone(cache[key]);
		}

		// Loads XML into cache, if the cache is null
		protected virtual void load_cache() {
			if (cache == null) {
				cache = new Dictionary<TKey, TObj>();
				foreach (XElement element in collection_xml.Elements()) {
					TObj obj = xml_to_obj(element);
					cache.Add(obj.Key(), obj);
				}
			}
		}

		protected virtual TObj clone(TObj obj) {
			return cloneable ? ((ICloneable<TObj>) obj).Clone() : obj;
		}
	}
}