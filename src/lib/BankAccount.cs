using System;

namespace lib {
	public class BankAccount : ICloneable<BankAccount> {
		public BankBranch Branch {
			get;
			private set;
		}

		public ID AccountNumber {
			get;
			private set;
		}

		public BankAccount(BankBranch branch, ID account_number) {
			Branch = branch;
			AccountNumber = account_number;
		}

		public BankAccount(BankBranch branch, int account_number) : this(branch, new ID(account_number, 6)) {}

		public BankAccount(BankBranch branch, string account_number) : this(branch, int.Parse(account_number)) {}

		public BankAccount Clone() {
			return new BankAccount(Branch, AccountNumber);
		}
	}
}