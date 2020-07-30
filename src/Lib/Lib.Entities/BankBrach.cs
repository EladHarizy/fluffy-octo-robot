using System;
using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Exceptions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class BankBranch : IEntityReadOnly<Tuple<ID, ID>> {
		public ID BankID { get; }

		public ID BranchID { get; }

		public string BankName { get; }

		public string BranchAddress { get; }

		public City BranchCity { get; }

		public BankBranch(ID bank_id, string bank_name, ID branch_id, string branch_address, City branch_city) {
			if (bank_id.Digits != 2) {
				if (bank_id.Digits < 2) {
					bank_id = new ID(bank_id, 2);
				} else {
					throw new IncorrectDigitsException(bank_id.Number, bank_id.Digits, "Error: Bank ID must be at most two digits.");
				}
			}
			BankID = bank_id;

			BankName = bank_name;

			if (branch_id.Digits != 3) {
				if (branch_id.Digits < 3) {
					branch_id = new ID(branch_id, 3);
				} else {
					throw new IncorrectDigitsException(branch_id.Number, branch_id.Digits, "Error: Branch ID must be three digits.");
				}
			}
			BranchID = branch_id;
			BranchAddress = branch_address;
			BranchCity = branch_city;
		}

		public override string ToString() {
			return BankID.ToString() + ' ' + BranchID.ToString();
		}

		public Tuple<ID, ID> Key() {
			return new Tuple<ID, ID>(BankID, BranchID);
		}

		public override bool Equals(object obj) {
			return obj is BankBranch branch
				&& EqualityComparer<ID>.Default.Equals(BankID, branch.BankID)
				&& EqualityComparer<ID>.Default.Equals(BranchID, branch.BranchID);
		}

		public override int GetHashCode() {
			int hashCode = 208762139;
			hashCode = hashCode * -1521134295 + EqualityComparer<ID>.Default.GetHashCode(BankID);
			hashCode = hashCode * -1521134295 + EqualityComparer<ID>.Default.GetHashCode(BranchID);
			return hashCode;
		}
	}
}