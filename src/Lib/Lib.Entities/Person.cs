using System.Collections.Generic;
using System.Text;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public abstract class Person : IEntity<ID> {
		public virtual ID ID { get; set; }

		public virtual string FirstName { get; }

		public virtual string LastName { get; }

		public virtual string Name {
			get => FirstName + ' ' + LastName;
		}

		public virtual Email Email { get; set; }

		public virtual IEnumerable<byte> PasswordHash { get; }

		public virtual ICollection<Phone> Phones { get; }

		public Person(ID id, string first_name, string last_name, Email email, IEnumerable<byte> password_hash, IEnumerable<Phone> phones) {
			ID = id;
			FirstName = first_name;
			LastName = last_name;
			Email = email;
			PasswordHash = password_hash;
			Phones = phones as ICollection<Phone>;
			if (Phones == null) {
				Phones = new HashSet<Phone>(phones);
			}
		}

		public Person(ID id, string first_name, string last_name, string email, IEnumerable<byte> password_hash) : this(id, first_name, last_name, email, password_hash, new HashSet<Phone>()) {}

		public override string ToString() {
			return ToString(0);
		}

		public virtual string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("ID:\t");
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

			sb.Append('\t', tabs);
			if (Phones.Count == 1) {
				sb.Append("Phone:\t");
				sb.Append(Phones.GetEnumerator().Current);
				sb.Append('\n');
			} else if (Phones.Count > 1) {
				sb.Append("Phones:\n");
				foreach (Phone phone in Phones) {
					sb.Append('\t', tabs + 1);
					sb.Append(phone);
					sb.Append('\n');
				}
			}

			return sb.ToString();
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}
	}
}