using System;

namespace lib {
	public static class Int32 {
		public static int Digits(this int n) {
			return Convert.ToInt32(Math.Floor(Math.Log10(n) + 1));
		}
	}
}