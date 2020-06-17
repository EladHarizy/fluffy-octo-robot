using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class BankAccountXmlConverter : IXmlConverter<BankAccount> {
		public XElement ObjToXml(BankAccount account) {
			return new XElement("bank_account",
				new XElement("bank_id", account.Branch.BankID),
				new XElement("branch_id", account.Branch.BranchID),
				new XElement("account_number", account.AccountNumber)
			);
		}

		public BankAccount XmlToObj(XElement element) {
			ID bank_id = element.Element("bank_id").Value;
			ID branch_id = element.Element("branch_id").Value;
			ID account_number = element.Element("account_number").Value;
			return new BankAccount(
				DataFactory.Data.BankBranch[new Tuple<ID, ID>(bank_id, branch_id)],
				account_number
			);
		}
	}
}