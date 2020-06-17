using System;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class UnitXmlConverterReadOnly : IXmlConverterReadOnly<Unit> {
		protected DataAccessorReadOnly<ID, Host> hosts = new DataAccessorReadOnly<ID, Host>("../data_files/hosts.xml", "hosts", new HostXmlConverterReadOnly());

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
				calendar
			);
		}
	}
}