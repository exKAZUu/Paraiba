#region License

// Copyright (C) 2012 Kazunori Sakamoto
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
	public static class FileSystemInfoExtensions {
		/// <summary>
		///   Returns a value indicating whether the file or directory exists null-safely.
		/// </summary>
		/// <param name="info"> the </param>
		/// <returns> </returns>
		public static bool SafeExists(this FileSystemInfo info) {
			return info != null && info.Exists;
		}

		public static bool SafeFileExists(this FileSystemInfo info) {
			return info.SafeExists() && (info.Attributes & FileAttributes.Directory) == 0;
		}

		public static bool SafeFileExists(this FileInfo info) {
			return info.SafeExists();
		}

		public static bool SafeFileExists(this DirectoryInfo info) {
			return false;
		}

		public static bool SafeDirectoryExists(this FileSystemInfo info) {
			return info.SafeExists() && (info.Attributes & FileAttributes.Directory) != 0;
		}

		public static bool SafeDirectoryExists(this DirectoryInfo info) {
			return info.SafeExists();
		}

		public static bool SafeDirectoryExists(this FileInfo info) {
			return false;
		}

		public static FileInfo GetFile(this DirectoryInfo info, string path) {
			return new FileInfo(Path.Combine(info.FullName, path));
		}

		public static DirectoryInfo GetDirectory(this DirectoryInfo info, string path) {
			return new DirectoryInfo(Path.Combine(info.FullName, path));
		}
	}
}