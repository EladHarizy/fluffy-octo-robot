using System.IO;
using System.Text;

namespace lib {
	static class Tabulator {
		public static string Tabulate(string str, int tabs) {
			StringBuilder sb = new StringBuilder();
			using(StringReader reader = new StringReader(str)) {
				string line = string.Empty;
				do {
					line = reader.ReadLine();
					if (line != null) {
						sb.Append('\t', tabs);
						sb.Append(line);
						sb.Append('\n');
					}
				} while (line != null);
			}
			return sb.ToString();
		}
	}
}