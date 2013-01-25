﻿#region License

// Copyright (C) 2011-2013 Kazunori Sakamoto
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
	/// <summary>
	/// A utility calss for file paths.
	/// </summary>
	public static class ParaibaPath {
		/// <summary>
		/// Returns the path where directory separator chars in the specified path are replaced with a primary one.
		/// </summary>
		/// <param name="path">The path to be normaized.</param>
		/// <returns>The path where directory separator chars in the specified path are replaced with a primary one.</returns>
		public static string NormalizeDirectorySeparatorChar(string path) {
			return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		/// <summary>
		/// Return the path which is complemented with a directory separator char.
		/// </summary>
		/// <param name="path">The path to be complemented.</param>
		/// <returns>The path which is complemented with a directory separator char</returns>
		public static string ComplementDirectorySeparatorChar(string path) {
			if (!path.EndsWith(Path.DirectorySeparatorChar + "")) {
				return path + Path.DirectorySeparatorChar;
			}
			return path;
		}

		/// <summary>
		/// Return the relative path of the specified full path with the specified base path.
		/// </summary>
		/// <param name="targetFullPath">The target full path to acquire the relative path.</param>
		/// <param name="basePath">The base path to acquire the relative path.</param>
		/// <returns>The relative path of the specified path with the specified base path.</returns>
		public static string GetRelativePath(string targetFullPath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = ComplementDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetFullPath);
			var relativePath = baseUri.MakeRelativeUri(targetUri).ToString();
			// URLデコードして、'/'を'\'に変更する)
			return NormalizeDirectorySeparatorChar(Uri.UnescapeDataString(relativePath));
		}

		/// <summary>
		/// Return the full path of the specified relative path with the specified base path. 
		/// </summary>
		/// <param name="targetRelativePath">The target relative path to acquire the full path.</param>
		/// <param name="basePath">The base path to acquire the full path.</param>
		/// <returns>The full path of the specified relative path with the specified base path.</returns>
		public static string GetFullPath(string targetRelativePath, string basePath) {
			basePath = NormalizeDirectorySeparatorChar(basePath);
			basePath = ComplementDirectorySeparatorChar(basePath);
			var baseUri = new Uri(basePath);
			var targetUri = new Uri(baseUri, targetRelativePath);
			return NormalizeDirectorySeparatorChar(targetUri.LocalPath);
		}
	}
}