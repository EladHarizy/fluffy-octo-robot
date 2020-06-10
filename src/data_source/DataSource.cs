using System;
using System.Collections.Generic;
using System.IO;
using lib;

namespace data_source {
	public class DataSource {
		public IDictionary<string, Amenity> Amenities;
		public IDictionary<Tuple<ID, ID>, BankBranch> BankBranches;
		public IDictionary<string, City> Cities;

		public DataSource() {
			Amenities = load_from_txt<Amenity>("amenities.dat");
			Cities = load_from_txt<City>("cities.dat");
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
	}
}