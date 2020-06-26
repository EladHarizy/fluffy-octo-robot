using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestXmlConverter : IXmlConverter<Guest> {
		private readonly CollectionXmlConverter<Phone, HashSet<Phone>> phones_converter = new CollectionXmlConverter<Phone, HashSet<Phone>>("phones", "phone");

		public Guest XmlToObj(XElement element) {
			ID id = element.Element("id").Value;
			string first_name = element.Element("first_name").Value;
			string last_name = element.Element("last_name").Value;
			Email email = element.Element("email").Value;
			IEnumerable<byte> password_hash = Convert.FromBase64String(element.Element("password_hash").Value);
			ICollection<Phone> phones = phones_converter.XmlToObj(element.Element("phones"));
			return new Guest(id, first_name, last_name, email, password_hash, phones);
		}

		public XElement ObjToXml(Guest guest) {
			XElement phones_xml = phones_converter.ObjToXml(guest.Phones);
			return new XElement(
				"guest",
				new XElement("id", guest.ID),
				new XElement("first_name", guest.FirstName),
				new XElement("last_name", guest.LastName),
				new XElement("email", guest.Email),
				new XElement("password_hash", Convert.ToBase64String(guest.PasswordHash.ToArray())),
				phones_xml
			);
		}
	}
}