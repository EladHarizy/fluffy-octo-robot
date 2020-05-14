using System;

namespace FluffyOctoRobot {

	public class RandomDate {

		private int days;

		private DateTime start_date;
		public DateTime StartDate {
			get => start_date;
			set => start_date = value.Date;
		}

		public RandomDate(DateTime start_date, DateTime end_date) {
			StartDate = start_date;
			days = (end_date.Date - StartDate).Days;
		}

		static private Random random = new Random();
		public DateTime Next() {
			return StartDate.AddDays(random.Next(days));
		}

	}

}