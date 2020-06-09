using exceptions;

namespace lib {
	public class ID {
		public int Number {
			get;
			private set;
		}

		public int Digits {
			get;
			private set;
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
	}
}