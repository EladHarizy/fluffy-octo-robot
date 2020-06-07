using System.Text;
using System.Text.RegularExpressions;
using exceptions;

namespace lib {
	public class Guest {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
			private set;
		}

		public string FirstName {
			get;
			private set;
		}

		public string LastName {
			get;
			private set;
		}

		public string Name {
			get => FirstName + ' ' + LastName;
		}

		private string email;
		public string Email {
			get => email;
			set {
				string regex = @"^(?=[A-Z0-9][A-Z0-9@._%+-]{5,253}$)[A-Z0-9._%+-]{1,64}@(?:(?=[A-Z0-9-]{1,63}\.)[A-Z0-9]+(?:-[A-Z0-9]+)*\.){1,8}[A-Z]{2,63}$";
				if (!Regex.Match(value, regex, RegexOptions.IgnoreCase).Success) {
					throw new InvalidEmailException(value);
				}
				email = value;
			}
		}

		private Guest() {
			ID = id_generator.Next();
		}

		public Guest(string first_name, string last_name, string email) : this() {
			FirstName = first_name;
			LastName = last_name;
			Email = email;
		}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();
			sb.Append('\t', tabs);
			sb.Append("Guest Details");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("-------------");
			sb.Append("\n");

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

			return sb.ToString();
		}
	}
}