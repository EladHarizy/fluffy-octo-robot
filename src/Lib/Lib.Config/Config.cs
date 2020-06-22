using System.Xml.Linq;
using Lib.DataTypes;

namespace Lib.Config {
	public static class Config {
		private static XElement root = XElement.Load("config.xml");

		private static XElement config_xml = root.Element("config");

		public static int ClosingFee { get; } = int.Parse(config_xml.Element("closing_fee").Value);

		public static ID LatestID(string file_name) {
			return int.Parse(config_xml.Element("latest_ids").Element(file_name).Value);
		}

		public static void LatestID(string file_name, ID id) {
			config_xml.Element("latest_ids").Element(file_name).Value = id;
			root.Save("config.xml");
		}
	}
}