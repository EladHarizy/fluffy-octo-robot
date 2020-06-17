using System.Collections.Generic;
using System.Linq;

namespace Lib.Extensions {
	public static class IEnumerable_extensions {
		public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerable) {
			return enumerable.Select(item => item);
		}
	}
}