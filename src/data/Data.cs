using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace data {
	public class Data : IData {
		public DataAccessorReadOnly<string, Amenity> Amenity { get; }

		public DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		public DataAccessorReadOnly<string, City> City { get; }

		public DataAccessor<Guest> Guest { get; }

		public DataAccessor<GuestRequest> GuestRequest { get; }

		public DataAccessor<Host> Host { get; }

		public DataAccessor<HostingUnit> HostingUnit { get; }

		public DataAccessor<Order> Order { get; }

		public DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		public DataAccessorReadOnly<string, HostingUnit.UnitType> UnitType { get; }

		Data() {
			Amenity = new DataAccessorReadOnly<string, Amenity>(
				"data_files/amenities.xml",
				"amenities",
				(element) => new Amenity(element.Element("name").Value)
			);

			BankBranch = new DataAccessorReadOnly<Tuple<ID, ID>, BankBranch>(
				"data_files/bank_branches.xml",
				"BRANCHES",
				element => {
					ID bank_id = WebUtility.HtmlDecode(element.Element("Bank_Code").Value);
					ID branch_id = WebUtility.HtmlDecode(element.Element("Branch_Code").Value);
					string bank_name = WebUtility.HtmlDecode(element.Element("Bank_Name").Value);
					string branch_address = WebUtility.HtmlDecode(element.Element("Address").Value);
					City branch_city = WebUtility.HtmlDecode(element.Element("City").Value);
					return new BankBranch(bank_id, bank_name, branch_id, branch_address, branch_city);
				}
			);

			City = new DataAccessorReadOnly<string, City>(
				"data_files/cities.xml",
				"cities",
				(element) => new City(element.Element("name").Value)
			);

			Guest = new DataAccessor<Guest>(
				"data_files/guests.xml",
				"guests",
				(element) => {
					ID id = WebUtility.HtmlDecode(element.Element("id").Value);
					string first_name = WebUtility.HtmlDecode(element.Element("first_name").Value);
					string last_name = WebUtility.HtmlDecode(element.Element("last_name").Value);
					EmailAddress email = WebUtility.HtmlDecode(element.Element("email").Value);
					ICollection<PhoneNumber> phones = new HashSet<PhoneNumber>();
					foreach (XElement phone_xml in element.Element("phones").Elements()) {
						phones.Add(WebUtility.HtmlDecode(phone_xml.Value));
					}
					return new Guest(id, first_name, last_name, email, phones);
				},
				(guest) => {
					return new XElement(
						"guest",
						new XElement("id", guest.ID.ToString()),
						new XElement("first_name", guest.FirstName),
						new XElement("last_name", guest.LastName),
						new XElement("email", guest.Email.ToString())
						// phone numbers TODO
					)
				}
			);
		}
	}
}