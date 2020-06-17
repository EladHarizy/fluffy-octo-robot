using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	class GuestRequestXmlConverterReadOnly : IXmlConverterReadOnly<GuestRequest> {
		protected readonly DataAccessorReadOnly<ID, Guest> guests = new DataAccessorReadOnly<ID, Guest>("../data_files/guests.xml", "guests", new GuestXmlConverterReadOnly());

		protected readonly CollectionXmlConverter<City, HashSet<City>> region_converter = new CollectionXmlConverter<City, HashSet<City>>("region", "city");

		protected readonly CollectionXmlConverter<Unit.Type, HashSet<Unit.Type>> unit_types_converter = new CollectionXmlConverter<Unit.Type, HashSet<Unit.Type>>("desired_unit_types", "unit_type");

		protected readonly CollectionXmlConverter<Amenity, HashSet<Amenity>> amenities_converter = new CollectionXmlConverter<Amenity, HashSet<Amenity>>("desired_amenities", "amenity");

		public GuestRequest XmlToObj(XElement element) {
			return new GuestRequest(
				element.Element("id").Value,
				guests[element.Element("guest_id").Value],
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