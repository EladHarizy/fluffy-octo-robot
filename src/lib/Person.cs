using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

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

		public MailAddress Email {
			get;
			protected set;
		}

		protected List<PhoneNumber> phones = new List<PhoneNumber>();
		public virtual IList<PhoneNumber> Phones {
			get => phones.AsReadOnly();
		}

		public virtual void AddPhone(string phone) {
			phones.Add(new PhoneNumber(phone));
		}

		public override string ToString() {
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