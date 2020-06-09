using System.Collections.Generic;
using lib;

namespace data {
	interface IDataAccessor<T, K> {
		ICollection<T> All {
			get;
		}

		T this [K key] {
			get;
			set;
		}

		void Add(K key, T obj);

		void Remove(K key);

		T Get(K key);

		void Update(K key, T obj);
	}
}