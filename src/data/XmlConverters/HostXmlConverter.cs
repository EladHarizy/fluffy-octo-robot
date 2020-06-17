using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class HostXmlConverter : HostXmlConverterReadOnly, IXmlConverter<Host> {
		public XElement ObjToXml(Host host) {
			return new XElement(
				"host",
				new XElement("id", host.ID),
				new XElement("first_name", host.FirstName),
				new XElement("last_name", host.LastName),
				new XElement("email", host.Email),
				phones_converter.ObjToXml(host.Phones),
				bank_account_converter.ObjToXml(host.BankAccount),
				new XElement("collection_clearance", host.CollectionClearance)
			);
		}
	}
}