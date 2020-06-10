using System.Text;
using exceptions;

namespace lib {
	public class BankBranch {

		public string BankName {
			get;
		}

		public string BranchAddress {
			get;
		}

		public City BranchCity {
			get;
		}

		public ID BankID {
			get;
		}

		public ID BranchID {
			get;
		}

		public BankBranch(ID bank_id, string bank_name, ID branch_id, City branch_city) {
			if (bank_id.Digits != 3) {
				throw new IncorrectDigitsException(bank_id.Number, bank_id.Digits, "Error: Bank ID must be three digits.");
			}
			BankID = bank_id;

			BankName = bank_name;

			if (branch_id.Digits != 3) {
				throw new IncorrectDigitsException(branch_id.Number, branch_id.Digits, "Error: Branch ID must be three digits.");
			}
			BranchID = branch_id;

			BranchCity = branch_city;
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();
			sb.Append('\t', tabs);
			sb.Append("Bank Details:");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("-------------");
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Bank Name:\t");
			sb.Append(BankName);
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Bank Number:\t");
			sb.Append(BankID);
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Branch Number:\t");
			sb.Append(BranchID);
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Branch Address:\t");
			sb.Append(BranchAddress);
			sb.Append("\n");

			sb.Append('\t', tabs);
			sb.Append("Branch City:\t");
			sb.Append(BranchCity);
			sb.Append("\n");

			return sb.ToString();
		}
	}
}