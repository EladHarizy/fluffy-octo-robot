using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestRequestXmlConverter : GuestRequestXmlConverterReadOnly, IXmlConverter<GuestRequest> {
		public XElement ObjToXml(GuestRequest guest_request) {
			return new XElement(
				"guest",
				new XElement("id", guest_request.ID),
				new XElement("guest", guest_request.Guest.ID),
				new XElement("creation_date", guest_request.CreationDate.ToString("dd/MM/yyyy")),
				new XElement("start_date", guest_request.StartDate.ToString("dd/MM/yyyy")),
				new XElement("end_date", guest_request.EndDate.ToString("dd/MM/yyyy")),
				new XElement("active", guest_request.Active),
				new XElement("adults", guest_request.Adults),
				new XElement("children", guest_request.Children),
				region_converter.ObjToXml(guest_request.Region),
				unit_types_converter.ObjToXml(guest_request.DesiredUnitTypes),
				amenities_converter.ObjToXml(guest_request.DesiredAmenities)
			);
		}
	}
}