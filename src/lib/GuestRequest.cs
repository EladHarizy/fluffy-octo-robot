using System;
using System.Collections.Generic;

namespace lib {
	public class GuestRequest {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
			private set;
		}

		private Guest guest;

		// Date that the request was created
		public Date CreationDate {
			get;
			private set;
		}

		// First date of the stay
		public Date StartDate {
			get;
			private set;
		}

		// Next day after the guest leaves
		public Date EndDate {
			get;
			private set;
		}

		public int Duration {
			get => (EndDate - StartDate).Days;
		}

		public bool Approved {
			get;
			set;
		}

		public bool Active {
			get;
			set;
		}

		public HashSet<City> Region;

		public override string ToString() {
			return "Start: " + StartDate.ToString("dd/MM/yyyy") + "\t\t" + EndDate.ToString("dd/MM/yyyy") + "\t\t" + (Approved ? "" : "Not ") + "Approved";
		}

		public GuestRequest(Date start_date) {
			ID = id_generator.Next();
			StartDate = start_date;
		}

		public GuestRequest(Date start_date, int duration) : this(start_date) {
			EndDate = StartDate.AddDays(duration);
		}

		public GuestRequest(Date start_date, Date end_date) : this(start_date) {
			EndDate = end_date;
		}
	}
}