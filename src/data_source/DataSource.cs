using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using lib;

namespace data_source {
	public class DataSource {
		public IDictionary<string, Amenity> Amenities;
		public IDictionary<Tuple<ID, ID>, BankBranch> BankBranches;
		public IDictionary<string, City> Cities;

		public IDictionary<ID, Guest> Guests;

		public DataSource() {
			Amenities = load_from_txt<Amenity>("amenities.dat");

			BankBranches = new Dictionary<Tuple<ID, ID>, BankBranch>();
			load_bank_branches("bank_branches.xml");

			Cities = load_from_txt<City>("cities.dat");

			load_guests();
		}

		private static IDictionary<string, T> load_from_txt<T>(string file_name) {
			IDictionary<string, T> dict = new Dictionary<string, T>();
			string line;
			StreamReader file = new StreamReader(file_name);
			while ((line = file.ReadLine()) != null) {
				dict.Add(line, (T) Activator.CreateInstance(typeof(T), new object[] { line }));
			}
			file.Close();
			return dict;
		}

		private void load_bank_branches(string file_name) {
			XElement root = XElement.Load(file_name);
			foreach (XElement branch_xml in root.Descendants("BRANCH")) {
				ID bank_id = WebUtility.HtmlDecode(branch_xml.Element("Bank_Code").Value);
				ID branch_id = WebUtility.HtmlDecode(branch_xml.Element("Branch_Code").Value);
				string bank_name = WebUtility.HtmlDecode(branch_xml.Element("Bank_Name").Value);
				string branch_address = WebUtility.HtmlDecode(branch_xml.Element("Address").Value);
				City branch_city = WebUtility.HtmlDecode(branch_xml.Element("City").Value);

				Tuple<ID, ID> key = new Tuple<ID, ID>(bank_id, branch_id);
				BankBranch branch = new BankBranch(bank_id, bank_name, branch_id, branch_city);

				BankBranches.Add(key, branch);
			}
		}

		private void load_guests() {
			Guests = new Dictionary<ID, Guest>();

		}
	}
}