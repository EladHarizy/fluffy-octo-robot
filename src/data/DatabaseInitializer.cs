using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib.Config;

namespace data {
	internal static class DatabaseInitializer {
		public static void InitializeDatabase() {
			if (!Directory.Exists(Config.BaseConfigPath)) {
				Directory.CreateDirectory(Config.BaseConfigPath);
			}

			if (!Directory.Exists(Path.Combine(Config.BaseConfigPath, "data"))) {
				Directory.CreateDirectory(Path.Combine(Config.BaseConfigPath, "data"));
			}

			IEnumerable<Tuple<Uri, string, string>> files = new List<Tuple<Uri, string, string>>() {
				new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/config.xml"), Config.BaseConfigPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/admins.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/amenities.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml"), Config.BaseDataPath, "bank_branches.xml"),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/cities.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/guest_requests.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/guest_requests.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/guests.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/hosts.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/order_statuses.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/orders.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/units.xml"), Config.BaseDataPath, null),

					new Tuple<Uri, string, string>(new Uri("https://raw.githubusercontent.com/EladHarizy/fluffy-octo-robot/master/data_files/unit_types.xml"), Config.BaseDataPath, null),
			};

			foreach (Tuple<Uri, string, string> file in files) {
				InitializeFile(file.Item1, file.Item2, file.Item3);
			}
		}

		private static void InitializeFile(Uri uri, string base_path, string file_name = null) {
			string path = Path.Combine(base_path, file_name ?? uri.Segments.Last());
			if (!File.Exists(path)) {
				DownloadFile(uri, path);
			}
		}

		private static void DownloadFile(Uri uri, string path) {
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
			httpReq = (System.Net.HttpWebRequest) System.Net.HttpWebRequest.Create(uri);
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