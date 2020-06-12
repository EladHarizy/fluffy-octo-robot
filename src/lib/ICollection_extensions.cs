using System.Collections.Generic;
using System.Linq;

namespace lib {
	public static class ICollection_extensions {
		public static ICollection<T> Clone<T>(this ICollection<T> enumerable) {
			return enumerable.Select(item => item) as ICollection<T>;
		}
	}
}