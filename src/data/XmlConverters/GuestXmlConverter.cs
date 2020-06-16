using System.Xml.Linq;
using Lib.Entities;

namespace data {
	class GuestXmlConverter : GuestXmlConverterReadOnly, IXmlConverter<Guest> {
		public XElement ObjToXml(Guest guest) {
			XElement phones_xml = phones_converter.ObjToXml(guest.Phones);
			return new XElement(
				"guest",
				new XElement("id", guest.ID),
				new XElement("first_name", guest.FirstName),
				new XElement("last_name", guest.LastName),
				new XElement("email", guest.Email),
				phones_xml
			);
		}
	}
}