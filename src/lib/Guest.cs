using System.Net.Mail;

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
	}
}