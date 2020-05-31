using System;

namespace lib {
	public static class int_Digits {
		public static int Digits(this int n) {
			return Convert.ToInt32(Math.Floor(Math.Log10(n) + 1));
		}
	}
}