using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Guest : Person, ICloneable<Guest> {

		public Guest(string first_name, string last_name, string email, Phone phone, Password password) : this(null, first_name, last_name, email, phone, password.Hash()) {}

		public Guest(ID id, string first_name, string last_name, Email email, Phone phone, IEnumerable<byte> password_hash) : base(id, first_name, last_name, email, phone, password_hash) {}

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
			return new Guest(ID, FirstName, LastName, Email, Phone, PasswordHash);
		}
	}
}