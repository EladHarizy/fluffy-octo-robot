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

		public BankAccount(BankBranch branch, int account_number) {
			Branch = branch;
			AccountNumber = new ID(account_number, 6);
		}

		public BankAccount(BankBranch branch, string account_number) : this(branch, int.Parse(account_number)) {}

		public BankAccount Clone() {
			BankAccount other = (BankAccount) this.MemberwiseClone();
			other.Branch = Branch.Clone();
			other.AccountNumber = AccountNumber.Clone();
			return other;
		}
	}
}