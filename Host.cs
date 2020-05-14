using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FluffyOctoRobot {
	public class Host : IEnumerable<HostingUnit> {
		private int ID;
		private List<HostingUnit> units = new List<HostingUnit>();

		// Host constructor
		public Host(int key, int hosting_units) : this(key, hosting_units, new DateTime(DateTime.Now.Year + 1, 1, 1), new DateTime(DateTime.Now.Year + 2, 1, 1)) {}

		public Host(int key, int hosting_units, DateTime start_date, DateTime end_date) {
			ID = key;

			for (int i = 1; i <= hosting_units; ++i) {
				units.Add(new HostingUnit(start_date, end_date));
			}
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("=============");
			sb.AppendLine("Host ID - " + ID.ToString());
			foreach (HostingUnit unit in units) {
				sb.AppendLine("-------------");
				sb.AppendLine("Hosting unit ID - " + unit.ID);
				sb.AppendLine();
				sb.AppendLine(unit.ToString());
			}
			return sb.ToString();
		}

		private static Random random = new Random();

		// Assign an arbitrary hosting unit to the request, and return the unit's ID
		private string SubmitRequest(GuestRequest guest_request) {
			List<HostingUnit> available_units = units.FindAll(unit => unit.Available(guest_request));
			if (available_units.Count != 0) {
				HostingUnit unit = available_units[random.Next(available_units.Count)];
				unit.ApproveRequest(guest_request);
				return unit.ID;
			}
			return "";
		}

		public int AnnualOccupancy() {
			int sum = 0;
			foreach (HostingUnit unit in units) {
				sum += unit.AnnualOccupancy();
			}
			return sum;
		}

		public void SortUnits() {
			units.Sort();
		}

		public bool AssignRequests(params GuestRequest[] requests) {
			bool accepted = true;
			foreach (GuestRequest request in requests) {
				if (SubmitRequest(request) == "") {
					accepted = false;
				}
			}
			return accepted;
		}

		public HostingUnit this [string ID] {
			get {
				foreach (HostingUnit unit in units) {
					if (unit.ID == ID) {
						return unit;
					}
				}
				return null;
			}
		}

		public IEnumerator<HostingUnit> GetEnumerator() {
			return units.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			// call the generic version of the method
			return this.GetEnumerator();
		}
	}

}