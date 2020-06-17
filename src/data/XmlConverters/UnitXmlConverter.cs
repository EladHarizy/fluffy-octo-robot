using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class UnitXmlConverter : UnitXmlConverterReadOnly, IXmlConverter<Unit> {
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