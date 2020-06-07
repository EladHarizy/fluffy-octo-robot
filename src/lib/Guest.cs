using System.Net.Mail;
using System.Text;

namespace lib {
	public class Guest : Person {
		private static IDGenerator id_generator = new IDGenerator(8);

		private Guest() {
			ID = id_generator.Next();
		}

		public Guest(string first_name, string last_name, string email) : this() {
			FirstName = first_name;
			LastName = last_name;
			Email = new MailAddress(email);
		}

		public override string ToString() {
			return ToString(0);
		}

		public override string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();
			sb.Append('\t', tabs);
			sb.Append("Guest Details");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("-------------");
			sb.Append("\n");

			sb.Append(base.ToString(tabs));

			return sb.ToString();
		}
	}
}