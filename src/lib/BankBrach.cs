using System;
using System.Text;

namespace lib {
	public class BankBranch {

		BankBranch(int bank_id, string bank_name, int branch_id, string branch_city) {
			this.bank_id = bank_id;
			BankName = bank_name;
			this.branch_id = branch_id;
			BranchCity = branch_city;
		}
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