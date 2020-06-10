using System.Collections.Generic;
using lib;

namespace data {
	interface IDataAccessor<T, K> : IDataAccessorReadOnly<T, K> {

		new T this [K key] { get; set; }

		void Add(K key, T obj);

		void Remove(K key);

		void Update(K key, T obj);
	}
}