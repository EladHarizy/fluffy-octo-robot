using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestRequestXmlConverter : IXmlConverter<GuestRequest> {
		private DataAccessorReadOnly<ID, Guest> Guests {
			get => DataFactory.Data.Guest;
		}

		private readonly CollectionXmlConverter<City, HashSet<City>> region_converter = new CollectionXmlConverter<City, HashSet<City>>("region", "city");

		private readonly CollectionXmlConverter<Unit.Type, HashSet<Unit.Type>> unit_types_converter = new CollectionXmlConverter<Unit.Type, HashSet<Unit.Type>>("desired_unit_types", "unit_type");

		private readonly CollectionXmlConverter<Amenity, HashSet<Amenity>> amenities_converter = new CollectionXmlConverter<Amenity, HashSet<Amenity>>("desired_amenities", "amenity");

		public GuestRequest XmlToObj(XElement element) {
			return new GuestRequest(
				element.Element("id").Value.Trim(),
				Guests[element.Element("guest_id").Value.Trim()],
				Date.Parse(element.Element("creation_date").Value.Trim()),
				Date.Parse(element.Element("start_date").Value.Trim()),
				Date.Parse(element.Element("end_date").Value.Trim()),
				bool.Parse(element.Element("active").Value.Trim()),
				int.Parse(element.Element("adults").Value.Trim()),
				int.Parse(element.Element("children").Value.Trim()),
				region_converter.XmlToObj(element.Element("region")),
				unit_types_converter.XmlToObj(element.Element("desired_unit_types")),
				amenities_converter.XmlToObj(element.Element("desired_amenities"))
			);
		}

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