using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestXmlConverterReadOnly : IXmlConverterReadOnly<Guest> {
		protected readonly CollectionXmlConverter<Phone, HashSet<Phone>> phones_converter = new CollectionXmlConverter<Phone, HashSet<Phone>>("phones", "phone");

		public Guest XmlToObj(XElement element) {
			ID id = element.Element("id").Value;
			string first_name = element.Element("first_name").Value;
			string last_name = element.Element("last_name").Value;
			Email email = element.Element("email").Value;
			ICollection<Phone> phones = phones_converter.XmlToObj(element.Element("phones"));
			return new Guest(id, first_name, last_name, email, phones);
		}
	}
}