using System.Text;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public class BankAccount : ICloneable<BankAccount> {
		public BankBranch Branch { get; private set; }

		public ID AccountNumber { get; private set; }

		public BankAccount(BankBranch branch, ID account_number) {
			Branch = branch;
			AccountNumber = account_number;
		}

		public BankAccount(BankBranch branch, int account_number) : this(branch, new ID(account_number, 6)) {}

		public BankAccount(BankBranch branch, string account_number) : this(branch, int.Parse(account_number)) {}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("Bank Account Details");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("--------------------");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Bank ID:\t");
			sb.Append(Branch.BankID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Branch ID:\t");
			sb.Append(Branch.BranchID);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Account Number:\t");
			sb.Append(AccountNumber);
			sb.Append('\n');

			return sb.ToString();
		}

		public BankAccount Clone() {
			return new BankAccount(Branch, AccountNumber);
		}
	}
}