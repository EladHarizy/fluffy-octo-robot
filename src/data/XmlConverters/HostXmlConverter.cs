using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class HostXmlConverter : IIndexedXmlConverter<ID, Host> {
		private BankAccountXmlConverter bank_account_converter = new BankAccountXmlConverter();

		public Host XmlToObj(XElement element) {
			ID id = XmlToKey(element);
			string first_name = element.Element("first_name").Value.Trim();
			string last_name = element.Element("last_name").Value.Trim();
			Email email = element.Element("email").Value.Trim();
			Phone phone = element.Element("phone").Value.Trim();
			IEnumerable<byte> password_hash = Convert.FromBase64String(element.Element("password_hash").Value.Trim());
			BankAccount account = bank_account_converter.XmlToObj(element.Element("bank_account"));
			bool collection_clearance = bool.Parse(element.Element("collection_clearance").Value.Trim());
			return new Host(id, first_name, last_name, email, phone, password_hash, account, collection_clearance);
		}

		public XElement ObjToXml(Host host) {
			return new XElement(
				"host",
				new XElement("id", host.ID),
				new XElement("first_name", host.FirstName),
				new XElement("last_name", host.LastName),
				new XElement("email", host.Email),
				new XElement("phone", host.Phone),
				new XElement("password_hash", Convert.ToBase64String(host.PasswordHash.ToArray())),
				bank_account_converter.ObjToXml(host.BankAccount),
				new XElement("collection_clearance", host.DebitAuthorisation)
			);
		}

		public ID XmlToKey(XElement element) {
			return element.Element("id").Value.Trim();
		}
	}
}