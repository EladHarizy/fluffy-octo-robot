using System;

namespace lib {
	public class RandomDate {
		private int days;

		public Date StartDate {
			get;
			set;
		}

		public RandomDate(Date start_date, Date end_date) {
			StartDate = start_date;
			days = (end_date - StartDate).Days;
		}

		static private Random random = new Random();
		public Date Next() {
			return StartDate.AddDays(random.Next(days));
		}
	}
}