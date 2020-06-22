using System;
using Lib.Entities;

namespace Lib.Exceptions {
	public class UnitUnavailableException : System.Exception {
		public Unit Unit { get; }

		public Date Date { get; }

		public int Duration { get; }

		public UnitUnavailableException() {}

		public UnitUnavailableException(string message) : base(message) {}

		public UnitUnavailableException(Unit unit, Date date, int duration) : this(unit, date, duration, "Error: The unit " + unit.ID + " is unavailable between " + date.ToString("dd/MM/yyyy") + " and " + date.AddDays(duration).ToString("dd/MM/yyyy") + '.') {}

		public UnitUnavailableException(Unit unit, Date date, int duration, string message) : base(message) {
			Unit = unit;
			Date = date;
			Duration = duration;
		}

		public UnitUnavailableException(string message, System.Exception inner) : base(message, inner) {}
	}
}