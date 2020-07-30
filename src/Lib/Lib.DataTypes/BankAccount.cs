using System.Text;
using Lib.Entities;
using Lib.Interfaces;

namespace Lib.DataTypes {
	public class BankAccount : ICloneable<BankAccount> {
		public BankBranch Branch { get; private set; }

		public ID AccountNumber { get; private set; }

		public BankAccount(BankBranch branch, ID account_number) {
			Branch = branch;
			AccountNumber = account_number;
		}

		public override string ToString() {
			return Branch.ToString() + ' ' + AccountNumber.ToString();
		}

		public BankAccount Clone() {
			return new BankAccount(Branch, AccountNumber);
		}
	}
}