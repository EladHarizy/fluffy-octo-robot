using System.Text.RegularExpressions;
using exceptions;

namespace lib {
	public class EmailAddress {
		private const string regex = @"^(?=[A-Z0-9][A-Z0-9@._%+-]{5,253}$)[A-Z0-9._%+-]{1,64}@(?:(?=[A-Z0-9-]{1,63}\.)[A-Z0-9]+(?:-[A-Z0-9]+)*\.){1,8}[A-Z]{2,63}$";

		public string Email {
			get;
		}

		public EmailAddress(string email) {
			if (!Regex.Match(email, regex, RegexOptions.IgnoreCase).Success) {
				throw new InvalidEmailException(email);
			}
			Email = email;
		}

		public override string ToString() {
			return Email;
		}

		public static implicit operator EmailAddress(string email) {
			return new EmailAddress(email);
		}

		public static implicit operator string(EmailAddress email) {
			return email.Email;
		}
	}
}