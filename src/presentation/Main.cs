using System;
using System.Collections.Generic;
using System.Linq;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	internal class main {
		static Random rand = new Random();

		private static GuestRequest CreateRandomRequest(Date start_date, Date end_date) {
			RandomDate date_generator = new RandomDate(start_date, end_date);
			Date date1 = date_generator.Next();
			Date date2 = date_generator.Next();
			if (date1 > date2) {
				Date temp = date2;
				date2 = date1;
				date1 = temp;
			}
			if (date2 == date1) {
				date2 = date2.AddDays(1);
			}
			return new GuestRequest(new Guest("Bob", "Smith", "bob@smith.com"), date1, date2, 1, 0, null, null, null);
		}

		static void Main(string[] args) {
			BankBranch branch = new BankBranch(new ID(10, 3), "Discount Bank", new ID(800, 3), "221B Baker Street", new City("Jerusalem"));
			List<Host> hosts = new List<Host>() {
				new Host(
						"Alice",
						"Alderson",
						"alice@fluffy-octo-robot.com",
						new HashSet<PhoneNumber> {
							"+441234456654",
							"+350 543 45334"
						},
						branch,
						999999
					),
					new Host(
						"Bob",
						"Barkley",
						"bob@fluffy-octo-robot.com",
						new HashSet<PhoneNumber> {
							"+441234456654",
							"00350 543 45334"
						},
						branch,
						349853
					),
					new Host(
						"Charlie",
						"Clemands",
						"charlie@fluffy-octo-robot.com",
						new HashSet<PhoneNumber>(),
						branch,
						492234
					),
					new Host(
						"Dave",
						"Davidson",
						"dave@fluffy-octo-robot.com",
						new HashSet<PhoneNumber> {
							"00441234456654"
						},
						branch,
						673567
					),
					new Host(
						"Elliott",
						"Edwards",
						"elliott@fluffy-octo-robot.com",
						new HashSet<PhoneNumber> {
							"+350 543 45334"
						},
						branch,
						236909
					)
			};

			Date start_date = new Date(Date.Today.Year + 1, 1, 1);
			Date end_date = new Date(Date.Today.Year + 2, 1, 1);

			hosts[0].Add(new HostingUnit(hosts[0], "Alice's Wonderland", start_date, end_date));
			hosts[0].Add(new HostingUnit(hosts[0], "Alice's Other Wonderland", start_date, end_date));

			hosts[1].Add(new HostingUnit(hosts[1], "Bob's Bungalow", start_date, end_date));

			hosts[2].Add(new HostingUnit(hosts[2], "Chalie's Chapel", start_date, end_date));
			hosts[2].Add(new HostingUnit(hosts[2], "Chalie's Chalet", start_date, end_date));
			hosts[2].Add(new HostingUnit(hosts[2], "Chalie's Cave", start_date, end_date));

			hosts[3].Add(new HostingUnit(hosts[3], "Dave's Dorm", start_date, end_date));

			hosts[4].Add(new HostingUnit(hosts[4], "Elliott's Eggplant", start_date, end_date));

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
			Dictionary<ID, double> dict = new Dictionary<ID, double>();
			foreach (Host host in hosts) {
				foreach (HostingUnit unit in host) {
					dict[unit.ID] = unit.OccupancyPercentage();
				}
			}

			//get max value in dictionary
			double max_val = dict.Values.Max();

			//get max value key name in dictionary
			ID max_key = dict.FirstOrDefault(x => x.Value == max_val).Key;

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