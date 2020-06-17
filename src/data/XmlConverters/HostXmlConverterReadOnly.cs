using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class HostXmlConverterReadOnly : IXmlConverterReadOnly<Host> {
		protected readonly CollectionXmlConverter<Phone, HashSet<Phone>> phones_converter = new CollectionXmlConverter<Phone, HashSet<Phone>>("phones", "phone");

		protected BankAccountXmlConverter bank_account_converter = new BankAccountXmlConverter();

		public Host XmlToObj(XElement element) {
			ID id = element.Element("id").Value;
			string first_name = element.Element("first_name").Value;
			string last_name = element.Element("last_name").Value;
			Email email = element.Element("email").Value;
			ICollection<Phone> phones = phones_converter.XmlToObj(element.Element("phones"));
			BankAccount account = bank_account_converter.XmlToObj(element.Element("bank_account"));
			bool collection_clearance = bool.Parse(element.Element("collection_clearance").Value);
			return new Host(id, first_name, last_name, email, phones, account, collection_clearance);
		}
	}
}