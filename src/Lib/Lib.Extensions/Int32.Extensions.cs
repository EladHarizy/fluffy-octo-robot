using System;

namespace Lib.Extensions {
	public static class Int32Extensions {
		public static int Digits(this int n) {
			return Convert.ToInt32(Math.Floor(Math.Log10(n) + 1));
		}
	}
}