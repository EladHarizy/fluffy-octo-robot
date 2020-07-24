using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestXmlConverter : IIndexedXmlConverter<ID, Guest> {
		public Guest XmlToObj(XElement element) {
			ID id = XmlToKey(element);
			string first_name = element.Element("first_name").Value.Trim();
			string last_name = element.Element("last_name").Value.Trim();
			Email email = element.Element("email").Value.Trim();
			Phone phone = element.Element("phone").Value.Trim();
			IEnumerable<byte> password_hash = Convert.FromBase64String(element.Element("password_hash").Value.Trim());
			return new Guest(id, first_name, last_name, email, phone, password_hash);
		}

		public XElement ObjToXml(Guest guest) {
			return new XElement(
				"guest",
				new XElement("id", guest.ID),
				new XElement("first_name", guest.FirstName),
				new XElement("last_name", guest.LastName),
				new XElement("email", guest.Email),
				new XElement("phone", guest.Phone),
				new XElement("password_hash", Convert.ToBase64String(guest.PasswordHash.ToArray()))
			);
		}

		public ID XmlToKey(XElement element) {
			return element.Element("id").Value.Trim();
		}
	}
}