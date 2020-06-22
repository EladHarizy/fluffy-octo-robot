using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Host : Person, ICloneable<Host> {
		public BankAccount BankAccount { get; }

		public bool DebitAuthorisation { get; private set; }

		// Host constructor
		public Host(
			string first_name,
			string last_name,
			string email,
			ICollection<Phone> phones,
			BankBranch bank_branch,
			int account_number
		) : this(
			null, // initialized ID to null
			first_name,
			last_name,
			new Email(email),
			phones,
			new BankAccount(bank_branch, account_number),
			true // collection clearance default
		) {}

		public Host(
			ID id,
			string first_name,
			string last_name,
			Email email,
			ICollection<Phone> phones,
			BankAccount bank_account,
			bool collection_clearance
		) : base(
			id,
			first_name,
			last_name,
			email,
			phones
		) {
			BankAccount = bank_account;
			DebitAuthorisation = collection_clearance;
		}

		public override string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("Host Details");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("------------");
			sb.Append('\n');

			sb.Append(base.ToString(tabs));

			sb.Append('\t', tabs);
			sb.Append("Bank Account:");
			sb.Append(BankAccount.ToString(tabs + 1));

			sb.Append('\t', tabs);
			sb.Append("Collection Clearance:\t");
			sb.Append('\n');

			return sb.ToString();
		}

		public Host Clone() {
			return new Host(ID, FirstName, LastName, Email, Phones.Clone(), BankAccount, DebitAuthorisation);
		}
	}
}