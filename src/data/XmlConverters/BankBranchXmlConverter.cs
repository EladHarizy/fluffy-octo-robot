using System.Net;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class BankBranchXmlConverter : IXmlConverterReadOnly<BankBranch> {
		public BankBranch XmlToObj(XElement element) {
			ID bank_id = element.Element("Bank_Code").Value;
			ID branch_id = element.Element("Branch_Code").Value;
			string bank_name = element.Element("Bank_Name").Value;
			string branch_address = element.Element("Address").Value;
			City branch_city = element.Element("City").Value;
			return new BankBranch(bank_id, bank_name, branch_id, branch_address, branch_city);
		}
	}
}