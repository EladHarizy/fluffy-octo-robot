using System.Text.RegularExpressions;
using exceptions;

namespace lib {
	public class PhoneNumber {
		private string number;
		public string Number {
			get => number;
			set {
				string regex = @"(?:00|\+)(?:1|[2-9]\d{1,2})\d{5,14}";
				if (!Regex.Match(value, regex, RegexOptions.IgnoreCase).Success) {
					throw new InvalidPhoneException(value);
				}
				number = value;
			}
		}

		public PhoneNumber(string number) {
			Number = number;
		}

		public override string ToString() {
			return Number;
		}
	}
}