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
		private DateTime start_date;
		public DateTime StartDate {
			get => start_date;
			set => start_date = value.Date;
		}

		// Next day after the guest leaves
		private DateTime end_date;
		public DateTime EndDate {
			get => end_date;
			set => end_date = value.Date;
		}

		public bool Approved {
			get;
			set;
		}

		public override string ToString() {
			return "Start: " + StartDate.ToString("dd/MM/yyyy") + "\t\t" + EndDate.ToString("dd/MM/yyyy") + "\t\t" + (Approved ? "" : "Not ") + "Approved";
		}

		public int Duration {
			get => (end_date - start_date).Days;
		}

		public GuestRequest(DateTime start_date) {
			ID = id_generator.Next();
			StartDate = start_date;
		}

		public GuestRequest(DateTime start_date, int duration) : this(start_date) {
			EndDate = StartDate.AddDays(duration);
		}

		public GuestRequest(DateTime start_date, DateTime end_date) : this(start_date) {
			EndDate = end_date;
		}
	}
}