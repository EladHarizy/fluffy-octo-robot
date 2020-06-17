using System.Text.RegularExpressions;
using Lib.Exceptions;

namespace Lib.DataTypes {
	public class Email {
		private const string regex = @"^(?=[A-Z0-9][A-Z0-9@._%+-]{5,253}$)[A-Z0-9._%+-]{1,64}@(?:(?=[A-Z0-9-]{1,63}\.)[A-Z0-9]+(?:-[A-Z0-9]+)*\.){1,8}[A-Z]{2,63}$";

		private readonly string email;

		public Email(string email) {
			if (!Regex.Match(email, regex, RegexOptions.IgnoreCase).Success) {
				throw new InvalidEmailException(email);
			}
			this.email = email;
		}

		public override string ToString() {
			return email;
		}

		public static implicit operator Email(string email) {
			return new Email(email);
		}

		public static implicit operator string(Email email) {
			return email.email;
		}
	}
}