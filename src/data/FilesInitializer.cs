using System.IO;
using Lib.Config;

namespace data {
	internal static class FilesInitializer {
		public static void InitializeAll() {
			if (!Directory.Exists(Config.BasePath)) {
				Directory.CreateDirectory(Config.BasePath);
			}

			if (!Directory.Exists(Path.Combine(Config.BasePath, "data"))) {
				Directory.CreateDirectory(Path.Combine(Config.BasePath, "data"));
			}

			InitializeConfig();
			InitializeAmenities();
			InitializeBankBranches();
			InitializeCities();
			InitializeGuestRequests();
			InitializeGuests();
			InitializeHosts();
			InitializeOrders();
			InitializeOrderStatuses();
			InitializeUnits();
			InitializeUnitTypes();
		}

		private static void InitializeConfig() {
			string path = Path.Combine(Config.BasePath, "config.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/config.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeAmenities() {
			string path = Path.Combine(Config.BasePath, "data/amenities.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/amenities.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeBankBranches() {
			string path = Path.Combine(Config.BasePath, "data/bank_branches.xml");
			string url = "https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeCities() {
			string path = Path.Combine(Config.BasePath, "data/cities.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/cities.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeGuestRequests() {
			string path = Path.Combine(Config.BasePath, "data/guest_requests.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/guest_requests.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeGuests() {
			string path = Path.Combine(Config.BasePath, "data/guests.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/guests.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeHosts() {
			string path = Path.Combine(Config.BasePath, "data/hosts.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/hosts.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeOrderStatuses() {
			string path = Path.Combine(Config.BasePath, "data/order_statuses.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/order_statuses.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeOrders() {
			string path = Path.Combine(Config.BasePath, "data/orders.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/orders.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeUnits() {
			string path = Path.Combine(Config.BasePath, "data/units.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/units.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void InitializeUnitTypes() {
			string path = Path.Combine(Config.BasePath, "data/unit_types.xml");
			string url = "https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/unit_types.xml";
			if (!File.Exists(path)) {
				DownloadFile(url, path);
			}
		}

		private static void DownloadFile(string url, string path) {
			long fileSize = 0;
			int bufferSize = 1024;
			bufferSize *= 1000;
			long existLen = 0;

			System.IO.FileStream saveFileStream;
			if (System.IO.File.Exists(path)) {
				System.IO.FileInfo destinationFileInfo = new System.IO.FileInfo(path);
				existLen = destinationFileInfo.Length;
			}

			if (existLen > 0) {
				saveFileStream = new System.IO.FileStream(
					path,
					System.IO.FileMode.Append,
					System.IO.FileAccess.Write,
					System.IO.FileShare.ReadWrite
				);
			} else {
				saveFileStream = new System.IO.FileStream(
					path,
					System.IO.FileMode.Create,
					System.IO.FileAccess.Write,
					System.IO.FileShare.ReadWrite
				);
			}

			System.Net.HttpWebRequest httpReq;
			System.Net.HttpWebResponse httpRes;
			httpReq = (System.Net.HttpWebRequest) System.Net.HttpWebRequest.Create(url);
			httpReq.AddRange((int) existLen);
			System.IO.Stream resStream;
			httpRes = (System.Net.HttpWebResponse) httpReq.GetResponse();
			resStream = httpRes.GetResponseStream();

			fileSize = httpRes.ContentLength;

			int byteSize;
			byte[] downBuffer = new byte[bufferSize];

			while ((byteSize = resStream.Read(downBuffer, 0, downBuffer.Length)) > 0) {
				saveFileStream.Write(downBuffer, 0, byteSize);
			}

			saveFileStream.Close();
		}
	}
}