using System;
using Lib.Exceptions;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.DataTypes {
	public class ID : IAutoIndexable<ID> {
		public int Number { get; }

		public int Digits { get; }

		public ID(string id, int digits = 0) : this(int.Parse(id), digits) {}

		public ID(int id, int digits = 0) {
			if (digits <= 0) {
				digits = id.Digits();
			} else if (digits < id.Digits()) {
				throw new IncorrectDigitsException(id, digits);
			}
			Number = id;
			Digits = digits;
		}

		public override string ToString() {
			return Number.ToString("D" + Digits.ToString());
		}

		public override bool Equals(object obj) {
			return obj is ID id && Number == id.Number && Digits == id.Digits;
		}

		public override int GetHashCode() {
			int hashCode = -169179105;
			hashCode = hashCode * -1521134295 + Number.GetHashCode();
			hashCode = hashCode * -1521134295 + Digits.GetHashCode();
			return hashCode;
		}

		public ID Next() {
			if (Number == Math.Pow(10, Digits) - 1) {
				throw new IDOverflowException(Number + 1, Digits);
			}
			return new ID(Number + 1, Digits);
		}

		public static implicit operator ID(int n) {
			return new ID(n);
		}

		public static implicit operator ID(string str) {
			return new ID(str, str.Length);
		}

		public static implicit operator string(ID id) {
			return id.ToString();
		}
	}
}