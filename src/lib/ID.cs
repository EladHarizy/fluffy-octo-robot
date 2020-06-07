using System;
using exceptions;

namespace lib {
	public class ID {
		private int id;

		private int digits;

		public override string ToString() {
			return id.ToString("D" + digits.ToString());
		}

		public ID(int id, int digits = 0) {
			if (digits <= 0) {
				digits = id.Digits();
			} else if (digits < id.Digits()) {
				throw new TooManyDigitsException(id, digits);
			}
			this.id = id;
			this.digits = digits;
		}
	}
}