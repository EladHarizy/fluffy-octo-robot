using System;

namespace lib {
	public class GuestRequest {
		private static IDGenerator id_generator = new IDGenerator(8);
		public ID ID {
			get;
			private set;
		}

		private Guest guest;

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

		public bool Approved {
			get;
			set;
		}

		public override string ToString() {
			return "Start: " + StartDate.ToString("dd/MM/yyyy") + "\t\t" + EndDate.ToString("dd/MM/yyyy") + "\t\t" + (Approved ? "" : "Not ") + "Approved";
		}

		public int Duration {
			get => (EndDate - StartDate).Days;
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