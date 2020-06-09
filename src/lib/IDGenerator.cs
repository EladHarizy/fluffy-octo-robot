using exceptions;

namespace lib {
	class IDGenerator {
		private int counter;

		public int Digits {
			get;
			private set;
		}

		public IDGenerator(int digits) {
			Digits = digits;
			counter = 0;
		}

		public ID Next() {
			++counter;
			if (counter.Digits() > Digits) {
				throw new IDOverflowException();
			}
			return new ID(counter, Digits);
		}
	}
}