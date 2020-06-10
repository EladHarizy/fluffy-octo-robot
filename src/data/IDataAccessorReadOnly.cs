using System.Collections.Generic;
using lib;

namespace data {
	interface IDataAccessorReadOnly<T, K> {
		ICollection<T> All { get; }

		T this [K key] { get; }

		T Get(K key);
	}
}