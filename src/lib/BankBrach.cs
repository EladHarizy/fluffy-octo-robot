using System;
using System.Text;

namespace lib {
	public class BankBranch {
		private int bank_id;
		public string BankID {
			get => bank_id.ToString("D4");
		}
		public string BankName {
			get;
			private set;
		}
		private int branch_id;
		public string BranchID {
			get => branch_id.ToString("D4");
		}
		public string BranchAddress {
			get;
			private set;
		}
		public string BranchCity {
			get;
			private set;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Bank Details:");
			sb.AppendLine("Bank Name - " + BankName);
			sb.AppendLine("Bank Number - " + BankID.ToString());
			sb.AppendLine("Branch Number - " + BranchID.ToString());
			sb.AppendLine("Branch Address - " + BranchAddress);
			sb.AppendLine("Branch City - " + BranchCity);
			return sb.ToString();
		}
	}
}