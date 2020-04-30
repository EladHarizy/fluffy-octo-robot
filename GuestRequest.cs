using System;

namespace FluffyOctoRobot {
	class GuestRequest {
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
	}
}