using System;

namespace Lib.Extensions {
	public static class Int32Extensions {
		public static int Digits(this int n) {
			if (n == 0) {
				return 0;
			}
			return Convert.ToInt32(Math.Floor(Math.Log10(n) + 1));
		}
	}
}