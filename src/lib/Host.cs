using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace lib {
	public class Host : Person, IEnumerable<HostingUnit>, ICollection<HostingUnit>, ICloneable<Host> {
		private static IDGenerator id_generator = new IDGenerator(8);

		private List<HostingUnit> units = new List<HostingUnit>();

		private BankAccount bank_account;

		public bool CollectionClearance {
			get;
			private set;
		}

		// Used to randomly select an available unit
		private static Random random = new Random();

		public int Count {
			get => units.Count;
		}

		public bool IsReadOnly {
			get => false;
		}

		// Host constructor
		public Host(
			string first_name,
			string last_name,
			string email,
			BankBranch bank_branch,
			int account_number
		) : this(
			id_generator.Next(),
			first_name,
			last_name,
			email,
			new BankAccount(bank_branch, account_number),
			true // collection clearance default
		) {}

		public Host(
			ID id,
			string first_name,
			string last_name,
			string email,
			BankAccount bank_account,
			bool collection_clearance
		) : base(
			id,
			first_name,
			last_name,
			email
		) {
			this.bank_account = bank_account;
			CollectionClearance = collection_clearance;
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

		public void Add(HostingUnit unit) {
			units.Add(unit);
		}

		public void Clear() {
			units.Clear();
		}

		public bool Contains(HostingUnit unit) {
			return units.Contains(unit);
		}

		public void CopyTo(HostingUnit[] array, int index) {
			units.CopyTo(array, index);
		}

		public bool Remove(HostingUnit unit) {
			return units.Remove(unit);
		}

		public Host Clone() {
			Host other = (Host) this.MemberwiseClone();
			other.FirstName = FirstName;
			other.LastName = LastName;
			other.ID = ID.Clone();
			other.Email = new MailAddress(Email.Address, Email.DisplayName);
			other.units = units.ConvertAll(item => item.Clone());
			other.bank_account = bank_account.Clone();
			other.CollectionClearance = CollectionClearance;
			return other;
		}
	}
}