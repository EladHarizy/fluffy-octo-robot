using System;
using System.Net;
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