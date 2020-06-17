using System.IO;
using System.Text;

namespace Lib.Extensions {
	public static class string_extensions {
		public static string Tabulate(this string str, int tabs) {
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