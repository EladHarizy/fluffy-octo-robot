using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Extensions {
	public static class ICollection_extensions {
		public static ICollection<T> Clone<T>(this ICollection<T> enumerable) {
			return enumerable.Select(item => item) as ICollection<T>;
		}

		public static IReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> collection) {
			return new ReadOnlyCollectionWrapper<T>(collection);
		}
	}

	public class ReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T> {
		private ICollection<T> collection;
		public ReadOnlyCollectionWrapper(ICollection<T> collection) {
			this.collection = collection;
		}

		public int Count {
			get => collection.Count;
		}

		public IEnumerator<T> GetEnumerator() {
			return collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}