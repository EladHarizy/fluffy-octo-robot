using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Lib.DataTypes;

namespace Lib.Config {
	public static class Config {
		public static string BasePath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fluffy-octo-robot/");

		private static XElement root;
		private static XElement Root {
			get {
				if (root == null) {
					root = XElement.Load(Path.Combine(BasePath, "config.xml"));
				}
				return root;
			}
		}

		private static XElement config_xml;
		private static XElement ConfigXml {
			get {
				if (config_xml == null) {
					config_xml = Root.DescendantsAndSelf("config").First();
				}
				return config_xml;
			}
		}

		public static int? fee_per_day;
		public static int FeePerDay {
			get {
				if (fee_per_day == null) {
					fee_per_day = int.Parse(ConfigXml.Element("fee_per_day").Value.Trim());
				}
				return fee_per_day ?? 0;
			}
		}

		public static ID LatestID(string file_name) {
			return ConfigXml.Element("latest_ids").Element(Path.GetFileName(file_name)).Value.Trim();
		}

		public static void LatestID(string file_name, ID id) {
			ConfigXml.Element("latest_ids").Element(Path.GetFileName(file_name)).Value = id;
			Root.Save(Path.Combine(BasePath, "config.xml"));
		}
	}
}