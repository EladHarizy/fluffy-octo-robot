using System.Text.RegularExpressions;
using Lib.Exceptions;

namespace Lib.DataTypes {
	public class Phone {
		private const string regex = @"(?:00|\+)(?:1|[2-9]\d{1,2})\d{5,14}";

		public string Number { get; }

		public Phone(string number) {
			number = Regex.Replace(number, @"^00", "+");
			number = Regex.Replace(number, @"[ -]", "");
			if (!Regex.Match(number, regex, RegexOptions.IgnoreCase).Success) {
				throw (number[0] == '+') ? new InvalidPhoneException(number) : new InvalidPhoneException(number, "Error: Phone number is missing a country code.");
			}
			Number = number;
		}

		public override string ToString() {
			return Number;
		}

		public static implicit operator Phone(string str) {
			return new Phone(str);
		}

		public static implicit operator string(Phone phone_number) {
			return phone_number.Number;
		}
	}
}