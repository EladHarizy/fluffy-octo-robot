using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Host : Person, ICloneable<Host> {
		public BankAccount BankAccount { get; }

		public bool DebitAuthorisation { get; private set; }

		// Host constructor
		public Host(
			string first_name,
			string last_name,
			Email email,
			Phone phone,
			Password password,
			BankBranch bank_branch,
			ID account_number
		) : this(
			null, // initialized ID to null
			first_name,
			last_name,
			email,
			phone,
			password.Hash(),
			new BankAccount(bank_branch, account_number),
			true // collection clearance default
		) {}

		public Host(
			ID id,
			string first_name,
			string last_name,
			Email email,
			Phone phone,
			IEnumerable<byte> password_hash,
			BankAccount bank_account,
			bool debit_authorisation
		) : base(
			id,
			first_name,
			last_name,
			email,
			phone,
			password_hash
		) {
			BankAccount = bank_account;
			DebitAuthorisation = debit_authorisation;
		}

		public Host Clone() {
			return new Host(ID, FirstName, LastName, Email, Phone, PasswordHash, BankAccount, DebitAuthorisation);
		}

		public override bool Equals(object obj) {
			return obj is Host host
				&& base.Equals(obj);
		}

		public override int GetHashCode() {
			int hashCode = 1708286331;
			hashCode = hashCode * -1521134295 + base.GetHashCode();
			return hashCode;
		}
	}
}