using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Guest : Person, ICloneable<Guest> {

		public Guest(string first_name, string last_name, string email, IEnumerable<byte> password_hash) : base(null, first_name, last_name, email, password_hash) {}

		public Guest(ID id, string first_name, string last_name, Email email, IEnumerable<byte> password_hash, ICollection<Phone> phones) : base(id, first_name, last_name, email, password_hash, phones) {}

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
			return new Guest(ID, FirstName, LastName, Email, PasswordHash, Phones.Clone());
		}
	}
}