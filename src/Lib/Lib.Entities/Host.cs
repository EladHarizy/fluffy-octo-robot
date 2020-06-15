using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Host : Person, IEnumerable<Unit>, ICollection<Unit>, ICloneable<Host> {
		private List<Unit> units;

		public BankAccount BankAccount { get; }

		public bool CollectionClearance { get; private set; }

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
			HashSet<Phone> phones,
			BankBranch bank_branch,
			int account_number
		) : this(
			null, // initialized ID to null
			first_name,
			last_name,
			new Email(email),
			phones,
			new List<Unit>(),
			new BankAccount(bank_branch, account_number),
			true // collection clearance default
		) {}

		public Host(
			ID id,
			string first_name,
			string last_name,
			Email email,
			HashSet<Phone> phones,
			List<Unit> units,
			BankAccount bank_account,
			bool collection_clearance
		) : base(
			id,
			first_name,
			last_name,
			email,
			phones
		) {
			this.units = units;
			BankAccount = bank_account;
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
			foreach (Unit unit in units) {
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
			IList<Unit> available_units = units.FindAll(unit => unit.Available(guest_request));
			if (available_units.Count != 0) {
				Unit unit = available_units[random.Next(available_units.Count)];
				unit.ApproveRequest(guest_request);
				return unit.ID.ToString();
			}
			return "";
		}

		public int AnnualOccupancy() {
			int sum = 0;
			foreach (Unit unit in units) {
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

		public Unit this [ID ID] {
			get {
				return this [ID.ToString()];
			}
		}

		public Unit this [string ID] {
			get {
				foreach (Unit unit in units) {
					if (unit.ID.ToString() == ID) {
						return unit;
					}
				}
				return null;
			}
		}

		public IEnumerator<Unit> GetEnumerator() {
			return units.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			// call the generic version of the method
			return this.GetEnumerator();
		}

		public void Add(Unit unit) {
			units.Add(unit);
		}

		public void Clear() {
			units.Clear();
		}

		public bool Contains(Unit unit) {
			return units.Contains(unit);
		}

		public void CopyTo(Unit[] array, int index) {
			units.CopyTo(array, index);
		}

		public bool Remove(Unit unit) {
			return units.Remove(unit);
		}

		public Host Clone() {
			return new Host(ID, FirstName, LastName, Email, (HashSet<Phone>) Phones.Clone(), (List<Unit>) units.Clone(), BankAccount, CollectionClearance);
		}
	}
}