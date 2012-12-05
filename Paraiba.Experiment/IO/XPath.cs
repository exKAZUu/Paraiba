#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.IO;

namespace Paraiba.IO {
	public class XPath {
		public static string NormalizeDirectorySeparatorChar(string path) {
			return path.Replace(
					Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		public static string AddDirectorySeparatorChar(string path) {
			if (!path.EndsWith(Path.DirectorySeparatorChar + "")) {
				return path + Path.DirectorySeparatorChar;
			}
			return path;
		}

		public static string GetRelativePath(
				string targetFullPath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = AddDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetFullPath);
			var relativePath = baseUri.MakeRelativeUri(targetUri).ToString();
			// URLデコードして、'/'を'\'に変更する)
			return
					NormalizeDirectorySeparatorChar(
							Uri.UnescapeDataString(relativePath));
		}

		public static string GetFullPath(
				string targetRelativePath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = AddDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetRelativePath);
			return NormalizeDirectorySeparatorChar(targetUri.LocalPath);
		}
	}
}