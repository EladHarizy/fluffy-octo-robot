using System.Text;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public partial class Unit : ICloneable<Unit>, IIndexed<ID> {
		public ID ID { get; private set; }

		public Calendar Bookings { get; }

		public Host Host { get; }

		public string UnitName { get; private set; }

		public Unit(
			Host host,
			string hosting_unit_name
		) : this(
			null, // initialized ID to null
			host,
			hosting_unit_name,
			new Calendar()
		) {}

		public Unit(ID id, Host host, string hosting_unit_name, Calendar bookings) {
			ID = id;
			Host = host;
			UnitName = hosting_unit_name;
			Bookings = bookings;
		}

		public override string ToString() {
			return ToString(0);
		}

		public string ToString(int tabs) {
			StringBuilder sb = new StringBuilder();

			sb.Append('\t', tabs);
			sb.Append("Hosting Unit Details");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("--------------------");
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Host Name:\t");
			sb.Append(Host.Name);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Unit Name:\t");
			sb.Append(UnitName);
			sb.Append('\n');

			sb.Append('\t', tabs);
			sb.Append("Occupied on:");
			sb.Append('\n');
			sb.Append(Bookings.ToString(tabs + 1));

			return sb.ToString();
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}

		public Unit Clone() {
			return new Unit(ID, Host, UnitName, Bookings.Clone());
		}
	}
}