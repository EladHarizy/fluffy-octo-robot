using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class AdminXmlConverter : IIndexedXmlConverter<ID, Admin> {
		public Admin XmlToObj(XElement element) {
			ID id = XmlToKey(element);
			Email email = element.Element("email").Value.Trim();
			IEnumerable<byte> password_hash = Convert.FromBase64String(element.Element("password_hash").Value.Trim());
			return new Admin(id, email, password_hash);
		}

		public XElement ObjToXml(Admin admin) {
			return new XElement(
				"admin",
				new XElement("id", admin.ID),
				new XElement("email", admin.Email),
				new XElement("password_hash", Convert.ToBase64String(admin.PasswordHash.ToArray()))
			);
		}

		public ID XmlToKey(XElement element) {
			return element.Element("id").Value.Trim();
		}
	}
}