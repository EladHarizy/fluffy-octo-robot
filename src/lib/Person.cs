using System.Collections.Generic;
using System.Text;

namespace lib {
	public abstract class Person {
		public virtual ID ID { get; }

		public virtual string FirstName { get; }

		public virtual string LastName { get; }

		public virtual string Name {
			get => FirstName + ' ' + LastName;
		}

		public EmailAddress Email { get; set; }

		protected HashSet<PhoneNumber> phones = new HashSet<PhoneNumber>();
		public virtual IReadOnlyCollection<PhoneNumber> Phones {
			get => phones;
		}

		public Person(ID id, string first_name, string last_name, EmailAddress email, HashSet<PhoneNumber> phones) {
			ID = id;
			FirstName = first_name;
			LastName = last_name;
			Email = email;
			this.phones = phones;
		}

		public Person(ID id, string first_name, string last_name, string email) : this(id, first_name, last_name, new EmailAddress(email), new HashSet<PhoneNumber>()) {}

		public virtual void AddPhone(string phone) {
			phones.Add(new PhoneNumber(phone));
		}

		public virtual void RemovePhone(PhoneNumber phone) {
			phones.Remove(phone);
		}

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
			if (phones.Count == 1) {
				sb.Append("Phone:\t");
				sb.Append(Phones.GetEnumerator().Current);
				sb.Append('\n');
			} else if (phones.Count > 1) {
				sb.Append("Phones:\n");
				foreach (PhoneNumber phone in Phones) {
					sb.Append('\t', tabs + 1);
					sb.Append(phone);
					sb.Append('\n');
				}
			}

			return sb.ToString();
		}
	}
}