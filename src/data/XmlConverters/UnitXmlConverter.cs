using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class UnitXmlConverter : IXmlConverter<Unit> {
		private DataAccessorReadOnly<ID, Host> hosts = DataFactory.Data.Host;

		public Unit XmlToObj(XElement element) {
			Unit.Calendar calendar = new Unit.Calendar();
			foreach (XElement booking_xml in element.Element("calendar").Elements()) {
				calendar.Bookings.Add(
					new Unit.Calendar.Booking(
						Date.Parse(booking_xml.Element("start").Value),
						int.Parse(booking_xml.Element("duration").Value)
					)
				);
			}

			return new Unit(
				element.Element("id").Value,
				hosts[element.Element("host_id").Value],
				element.Element("name").Value,
				element.Element("city").Value,
				calendar
			);
		}

		public XElement ObjToXml(Unit unit) {
			XElement calendar_xml = new XElement("calendar");
			foreach (Unit.Calendar.Booking booking in unit.Bookings) {
				calendar_xml.Add(
					new XElement(
						"booking",
						new XElement("start", booking.Start.ToString("dd/MM/yyyy")),
						new XElement("duration", booking.Duration)
					)
				);
			}

			return new XElement(
				"unit",
				new XElement("id", unit.ID),
				calendar_xml,
				new XElement("host_id", unit.Host.ID),
				new XElement("name", unit.UnitName)
			);
		}
	}
}