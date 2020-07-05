using System.Collections.Generic;
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
			this.email = email.ToLower();
		}

		public override bool Equals(object obj) {
			return obj is Email email && this.email == email.email;
		}

		public override int GetHashCode() {
			return 848330207 + EqualityComparer<string>.Default.GetHashCode(email);
		}

		public override string ToString() {
			return email;
		}

		public static bool operator ==(Email email1, Email email2) {
			return email1.Equals(email2);
		}

		public static bool operator !=(Email email1, Email email2) {
			return !(email1 == email2);
		}

		public static implicit operator Email(string email) {
			return new Email(email);
		}

		public static implicit operator string(Email email) {
			return email.email;
		}
	}
}