using System.Net.Mail;
using System.Text;

namespace lib {
	public class Guest : Person, ICloneable<Guest> {
		private static IDGenerator id_generator = new IDGenerator(8);

		public Guest(string first_name, string last_name, string email) : base(id_generator.Next(), first_name, last_name, email) {}

		public Guest(ID id, string first_name, string last_name, string email) : base(id, first_name, last_name, email) {}

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

		public Guest Clone() {
			Guest other = (Guest) this.MemberwiseClone();
			other.FirstName = FirstName;
			other.LastName = LastName;
			other.ID = ID.Clone();
			other.Email = Email;
			return other;
		}
	}
}