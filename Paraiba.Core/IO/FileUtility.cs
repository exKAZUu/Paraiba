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

using System.Diagnostics.Contracts;
using System.IO;

namespace Paraiba.IO {
	public static class FileUtility {
		/// <summary>
		/// Copy files and directories in the specified directory.
		/// </summary>
		/// <param name="srcPath"></param>
		/// <param name="dstPath"></param>
		public static void CopyRecursively(string srcPath, string dstPath) {
			Contract.Requires(Directory.Exists(srcPath));
			var files = Directory.GetFiles(srcPath);
			Directory.CreateDirectory(dstPath);
			foreach (var file in files) {
				File.Copy(
						file, Path.Combine(dstPath, Path.GetFileName(file)),
						true);
			}
			var dirs = Directory.GetDirectories(srcPath);
			foreach (var dir in dirs) {
				CopyRecursively(
						dir, Path.Combine(dstPath, Path.GetFileName(dir)));
			}
		}

		public static void CopyRecursivelyContentsOnly(string srcPath, string dstPath) {
			Contract.Requires(Directory.Exists(srcPath));
			var files = Directory.GetFiles(srcPath);
			Directory.CreateDirectory(dstPath);
			foreach (var file in files) {
				File.Copy(
						file, Path.Combine(dstPath, Path.GetFileName(file)),
						true);
			}
			var dirs = Directory.GetDirectories(srcPath);
			foreach (var dir in dirs) {
				CopyRecursively(
						dir, Path.Combine(dstPath, Path.GetFileName(dir)));
			}
		}
	}
}