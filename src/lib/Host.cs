using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lib {
	public class Host : Person, IEnumerable<HostingUnit> {
		private static IDGenerator id_generator = new IDGenerator(8);

		private List<HostingUnit> units = new List<HostingUnit>();

		private BankAccount bank_account;

		public bool CollectionClearance {
			get;
			private set;
		}

		// Used to randomly select an available unit
		private static Random random = new Random();

		// Host constructor
		public Host(int hosting_units) : this(hosting_units, new Date(Date.Today.Year + 1, 1, 1), new Date(Date.Today.Year + 2, 1, 1)) {}

		public Host(int hosting_units, Date start_date, Date end_date) {
			ID = id_generator.Next();

			for (int i = 1; i <= hosting_units; ++i) {
				units.Add(new HostingUnit(start_date, end_date));
			}
		}

		public override string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("Host Details");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("------------");
			sb.Append('\n');

			sb.Append(base.ToString(tabs));

			sb.Append('\t', tabs);
			sb.Append("Hosting Units:");
			sb.Append('\n');
			bool first_element = true;
			foreach (HostingUnit unit in units) {
				if (!first_element) {
					sb.Append("====================\n"); // Same length as "Hosting Unit Details"
				} else {
					first_element = false;
				}
				sb.Append(unit.ToString(tabs + 1));
				sb.Append('\t', tabs + 1);
			}

			return sb.ToString();
		}

		// Assign an arbitrary hosting unit to the request, and return the unit's ID
		private string SubmitRequest(GuestRequest guest_request) {
			IList<HostingUnit> available_units = units.FindAll(unit => unit.Available(guest_request));
			if (available_units.Count != 0) {
				HostingUnit unit = available_units[random.Next(available_units.Count)];
				unit.ApproveRequest(guest_request);
				return unit.ID.ToString();
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

		public HostingUnit this [ID ID] {
			get {
				return this [ID.ToString()];
			}
		}

		public HostingUnit this [string ID] {
			get {
				foreach (HostingUnit unit in units) {
					if (unit.ID.ToString() == ID) {
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