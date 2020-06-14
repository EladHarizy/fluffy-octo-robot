using System.Text.RegularExpressions;
using Lib.Exceptions;

namespace Lib.DataTypes {
	public class PhoneNumber {
		private const string regex = @"(?:00|\+)(?:1|[2-9]\d{1,2})\d{5,14}";

		public string Number { get; }

		public PhoneNumber(string number) {
			number = Regex.Replace(number, @"^00", "+");
			number = Regex.Replace(number, @"[ -]", "");
			if (!Regex.Match(number, regex, RegexOptions.IgnoreCase).Success) {
				throw new InvalidPhoneException(number);
			}
			Number = number;
		}

		public override string ToString() {
			return Number;
		}

		public static implicit operator PhoneNumber(string str) {
			return new PhoneNumber(str);
		}

		public static implicit operator string(PhoneNumber phone_number) {
			return phone_number.Number;
		}
	}
}