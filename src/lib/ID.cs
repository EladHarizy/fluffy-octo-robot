using System;
using exceptions;

namespace lib {
	public class ID {
		public int Number {
			get;
		}

		public int Digits {
			get;
		}

		public override string ToString() {
			return Number.ToString("D" + Digits.ToString());
		}

		public ID(int id, int digits = 0) {
			if (digits <= 0) {
				digits = id.Digits();
			} else if (digits < id.Digits()) {
				throw new IncorrectDigitsException(id, digits);
			}
			Number = id;
			Digits = digits;
		}

		public static implicit operator ID(string str) {
			return new ID(int.Parse(str), str.Length);
		}

		public static implicit operator ID(int n) {
			return new ID(n);
		}

		public static implicit operator string(ID id) {
			return id.ToString();
		}
	}
}