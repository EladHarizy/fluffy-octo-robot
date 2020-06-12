using System;
using System.Net;
using lib;

namespace data {
	public class Data : IData {
		public DataAccessorReadOnly<string, Amenity> Amenity { get; }

		public DataAccessorReadOnly<Tuple<ID, ID>, BankBranch> BankBranch { get; }

		public DataAccessorReadOnly<string, City> City { get; }

		public DataAccessor<ID, Guest> Guest { get; }

		public DataAccessor<ID, GuestRequest> GuestRequest { get; }

		public DataAccessor<ID, Host> Host { get; }

		public DataAccessor<ID, HostingUnit> HostingUnit { get; }

		public DataAccessor<ID, Order> Order { get; }

		public DataAccessorReadOnly<string, Order.Status> OrderStatus { get; }

		public DataAccessorReadOnly<string, Unit.UnitType> UnitType { get; }

		Data() {
			Amenity = new DataAccessorReadOnly<string, Amenity>(
				"data_files/amenities.xml",
				"amenities",
				(element) => element.Element("name").Value,
				(element) => new Amenity(element.Element("name").Value)
			);

			BankBranch = new DataAccessorReadOnly<Tuple<ID, ID>, BankBranch>(
				"data_files/bank_branches.xml",
				"BRANCHES",
				element => {
					return new Tuple<ID, ID>(
						new ID(element.Element("Bank_Code").Value),
						new ID(element.Element("Branch_Code").Value)
					);
				},
				element => {
					ID bank_id = new ID(WebUtility.HtmlDecode(element.Element("Bank_Code").Value));
					ID branch_id = new ID(WebUtility.HtmlDecode(element.Element("Branch_Code").Value));
					string bank_name = WebUtility.HtmlDecode(element.Element("Bank_Name").Value);
					string branch_address = WebUtility.HtmlDecode(element.Element("Address").Value);
					City branch_city = WebUtility.HtmlDecode(element.Element("City").Value);
					return new BankBranch(bank_id, bank_name, branch_id, branch_address, branch_city);
				}
			);
		}
	}
}