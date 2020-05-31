using System;

namespace exceptions {
	public class TooManyDigitsException : Exception {
		public int N {
			get;
			private set;
		}

		public int Digits {
			get;
			private set;
		}

		private string message;
		public override string Message {
			get => message;
		}

		public TooManyDigitsException(int n, int digits) {
			N = n;
			Digits = digits;
		}

		public TooManyDigitsException(int n, int digits, string message) : this(n, digits) {
			this.message = message;
		}
	}
}