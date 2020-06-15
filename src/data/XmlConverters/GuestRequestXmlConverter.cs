using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.Entities;

namespace data {
	class GuestRequestXmlConverter : IXmlConverter<GuestRequest> {
		private readonly ICollectionXmlConverter<City, HashSet<City>> region_converter = new ICollectionXmlConverter<City, HashSet<City>>("region", "city");

		private readonly ICollectionXmlConverter<Unit.Type, HashSet<Unit.Type>> unit_types_converter = new ICollectionXmlConverter<Unit.Type, HashSet<Unit.Type>>("desired_unit_types", "unit_type");

		private readonly ICollectionXmlConverter<Amenity, HashSet<Amenity>> amenities_converter = new ICollectionXmlConverter<Amenity, HashSet<Amenity>>("desired_amenities", "amenity");

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

		public GuestRequest XmlToObj(XElement element) {
			return new GuestRequest(
				element.Element("id").Value,
				DataFactory.Data.Guest[element.Element("guest").Value],
				Date.Parse(element.Element("creation_date").Value),
				Date.Parse(element.Element("start_date").Value),
				Date.Parse(element.Element("end_date").Value),
				bool.Parse(element.Element("active").Value),
				int.Parse(element.Element("adults").Value),
				int.Parse(element.Element("children").Value),
				region_converter.XmlToObj(element.Element("region")),
				unit_types_converter.XmlToObj(element.Element("desired_unit_types")),
				amenities_converter.XmlToObj(element.Element("desired_amenities"))
			);
		}
	}
}