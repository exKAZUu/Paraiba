using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Paraiba.IO {
	public class XPath {
		public static string NormalizeDirectorySeparatorChar(string path) {
			return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		public static string AddDirectorySeparatorChar(string path) {
			if (!path.EndsWith(Path.DirectorySeparatorChar + ""))
				return path + Path.DirectorySeparatorChar;
			return path;
		}

		public static string GetRelativePath(string targetFullPath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = AddDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetFullPath);
			var relativePath = baseUri.MakeRelativeUri(targetUri).ToString();
			// URLデコードして、'/'を'\'に変更する)
			return NormalizeDirectorySeparatorChar(Uri.UnescapeDataString(relativePath));
		}

		public static string GetFullPath(string targetRelativePath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = AddDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetRelativePath);
			return NormalizeDirectorySeparatorChar(targetUri.LocalPath);
		}
	}
}