using System.Text;
using System.Text.RegularExpressions;
using exceptions;

namespace lib {
	public abstract class Person {
		public virtual ID ID {
			get;
			protected set;
		}

		public virtual string FirstName {
			get;
			protected set;
		}

		public virtual string LastName {
			get;
			protected set;
		}

		public virtual string Name {
			get => FirstName + ' ' + LastName;
		}

		protected string email;
		public virtual string Email {
			get => email;
			set {
				string regex = @"^(?=[A-Z0-9][A-Z0-9@._%+-]{5,253}$)[A-Z0-9._%+-]{1,64}@(?:(?=[A-Z0-9-]{1,63}\.)[A-Z0-9]+(?:-[A-Z0-9]+)*\.){1,8}[A-Z]{2,63}$";
				if (!Regex.Match(value, regex, RegexOptions.IgnoreCase).Success) {
					throw new InvalidEmailException(value);
				}
				email = value;
			}
		}

		public virtual string ToString() {
			return ToString(0);
		}

		public virtual string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("ID:\t\t");
			sb.Append(ID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Name:\t");
			sb.Append(Name);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Email:\t");
			sb.Append(Email);
			sb.Append('\n');

			return sb.ToString();
		}
	}
}