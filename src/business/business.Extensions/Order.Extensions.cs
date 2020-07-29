using Lib.Entities;

namespace business.Extensions {
	public static class OrderExtensions {
		public static bool Overlaps(this Order order, Order order1) {
			return OverlapChecker.Overlaps(order.StartDate, order.Duration, order1.StartDate, order1.Duration);
		}
	}
}