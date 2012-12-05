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

using System.IO;

namespace Paraiba.IO {
	/// <summary>
	/// Provides a set of extension methods for <see cref="DirectoryInfo" />.
	/// </summary>
	public static class DirectoryInfoExtensions {
		/// <summary>
		/// Deletes a file or directory null-safely and returns true if it exists,
		/// specifying whether to delete subdirectories and files.
		/// </summary>
		/// <param name="info">A DirectoryInfo instance to delete.</param>
		/// <param name="recursive"><c>true</c> to delete this directory, its subdirectories, and all files; otherwise, <c>false</c>.</param>
		/// <returns>The value indicating whether the file or directory exists.</returns>
		public static bool SafeDelete(this DirectoryInfo info, bool recursive) {
			if (info == null) {
				return false;
			}
			if (!info.Exists) {
				return false;
			}
			info.Delete(recursive);
			return true;
		}

		/// <summary>
		/// Delete all files and subdirectories in the specified directory null-safely and returns true if it exists,
		/// </summary>
		/// <param name="info">The DirectoryInfo instance to be clear.</param>
		/// <returns>The value indicating whether the file or directory exists.</returns>
		public static bool SafeClear(this DirectoryInfo info) {
			if (info == null || !info.Exists) {
				return false;
			}
			foreach (var dirInfo in info.EnumerateDirectories()) {
				dirInfo.Delete(true);
			}
			foreach (var fileInfo in info.EnumerateFiles()) {
				fileInfo.Delete();
			}
			return true;
		}

		/// <summary>
		/// Delete all files and subdirectories in the specified directory.
		/// </summary>
		/// <param name="info">The DirectoryInfo instance to be clear.</param>
		public static void Clear(this DirectoryInfo info) {
			foreach (var dirInfo in info.EnumerateDirectories()) {
				dirInfo.Delete(true);
			}
			foreach (var fileInfo in info.EnumerateFiles()) {
				fileInfo.Delete();
			}
		}
	}
}