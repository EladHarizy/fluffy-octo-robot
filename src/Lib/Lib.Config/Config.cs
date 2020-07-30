using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using Lib.DataTypes;

namespace Lib.Config {
	public static class Config {
		public static string BaseConfigPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fluffy-octo-robot/");

		public static string BaseDataPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fluffy-octo-robot/data/");

		private static XElement root;
		private static XElement Root {
			get {
				if (root == null) {
					root = XElement.Load(Path.Combine(BaseConfigPath, "config.xml"));
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

		private static SmtpClient smtp_client;
		public static SmtpClient SmtpClient {
			get {
				if (smtp_client == null) {
					XElement smtp_client_xml = ConfigXml.Element("smtp_client");
					smtp_client = new SmtpClient();

					smtp_client.EnableSsl = true;
					smtp_client.UseDefaultCredentials = false;
					smtp_client.DeliveryMethod = SmtpDeliveryMethod.Network;

					string username = smtp_client_xml.Element("username").Value;
					string password = smtp_client_xml.Element("password").Value;

					smtp_client.Credentials = new NetworkCredential(username, password);
					smtp_client.Port = int.Parse(smtp_client_xml.Element("port").Value.Trim());
					smtp_client.Host = smtp_client_xml.Element("host").Value;
				}
				return smtp_client;
			}
		}

		private static Email admin_email;
		public static Email AdminEmail {
			get {
				if (admin_email == null) {
					admin_email = new Email(ConfigXml.Element("admin_email").Value.Trim());
				}
				return admin_email;
			}
		}

		public static ID LatestID(string file_name) {
			return ConfigXml.Element("latest_ids").Element(Path.GetFileName(file_name)).Value.Trim();
		}

		public static void LatestID(string file_name, ID id) {
			ConfigXml.Element("latest_ids").Element(Path.GetFileName(file_name)).Value = id;
			Root.Save(Path.Combine(BaseConfigPath, "config.xml"));
		}
	}
}