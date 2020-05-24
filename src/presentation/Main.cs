using System;
using System.Collections.Generic;
using System.Linq;
using lib;

namespace presentation {
	class main {
		static Random rand = new Random(DateTime.Now.Millisecond);

		private static GuestRequest CreateRandomRequest(DateTime start_date, DateTime end_date) {
			RandomDate date_generator = new RandomDate(start_date, end_date);
			DateTime date1 = date_generator.Next();
			DateTime date2 = date_generator.Next();
			if (date1 > date2) {
				DateTime temp = date2;
				date2 = date1;
				date1 = temp;
			}
			if (date2 == date1) {
				date2 = date2.AddDays(1);
			}
			return new GuestRequest(date1, date2);
		}
		static void Main(string[] args) {
			List<Host> hosts = new List<Host>() {
				new Host(1, rand.Next(1, 5)),
					new Host(2, rand.Next(1, 5)),
					new Host(3, rand.Next(1, 5)),
					new Host(4, rand.Next(1, 5)),
					new Host(5, rand.Next(1, 5))
			};
			DateTime start_date = new DateTime(DateTime.Now.Year + 1, 1, 1);
			DateTime end_date = new DateTime(DateTime.Now.Year + 2, 1, 1);

			for (int i = 0; i < 100; ++i) {
				foreach (Host host in hosts) {
					GuestRequest gr1 = CreateRandomRequest(start_date, end_date);
					int requests = rand.Next(1, 4);
					if (requests == 1) {
						host.AssignRequests(gr1);
					} else if (requests == 2) {
						GuestRequest gr2 = CreateRandomRequest(start_date, end_date);
						host.AssignRequests(gr1, gr2);
					} else {
						GuestRequest gr2 = CreateRandomRequest(start_date, end_date);
						GuestRequest gr3 = CreateRandomRequest(start_date, end_date);
						host.AssignRequests(gr1, gr2, gr3);
					}
				}
			}

			//Create dictionary for all units <unitkey, occupancy_percentage>
			Dictionary<string, double> dict = new Dictionary<string, double>();
			foreach (Host host in hosts) {
				foreach (HostingUnit unit in host) {
					dict[unit.ID] = unit.OccupancyPercentage();
				}
			}

			//get max value in dictionary
			double max_val = dict.Values.Max();

			//get max value key name in dictionary
			string max_key = dict.FirstOrDefault(x => x.Value == dict.Values.Max()).Key;

			//find the Host that its unit has the maximum occupancy percentage
			foreach (Host host in hosts) {
				if (host[max_key] != null) {
					//sort this host by occupancy of its units
					host.SortUnits();
					//print this host details
					Console.WriteLine("**** Details of the Host with the most occupied unit:\n");
					Console.WriteLine(host);
					break;
				}

			}
		}
	}
}