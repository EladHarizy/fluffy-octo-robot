using System;

namespace exceptions {
	public class IncorrectDigitsException : Exception {
		public int N { get; private set; }

		public int Digits { get; private set; }

		private string message;
		public override string Message {
			get => message;
		}

		public IncorrectDigitsException(int n, int digits) {
			N = n;
			Digits = digits;
		}

		public IncorrectDigitsException(int n, int digits, string message) : this(n, digits) {
			this.message = message;
		}
	}
}