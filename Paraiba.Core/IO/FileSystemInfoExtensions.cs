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
	/// Provides a set of extension methods for <see cref="FileSystemInfo" />,
	/// <see cref="FileInfo" /> and <see cref="DirectoryInfo" />.
	/// </summary>
	public static class FileSystemInfoExtensions {
		/// <summary>
		/// Deletes a file or directory null-safely and returns true if it exists.
		/// </summary>
		/// <param name="info">A <c>FileSystemInfo</c> instance to delete.</param>
		/// <returns>The value indicating whether the file or directory exists.</returns>
		public static bool SafeDelete(this FileSystemInfo info) {
			if (info != null && info.Exists) {
				info.Delete();
				return true;
			}
			return false;
		}

		/// <summary>
		///   Returns a value indicating whether the file or directory exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>FileSystemInfo</c> instance to check existing. </param>
		/// <returns> The value indicating whether the file or directory exists. </returns>
		public static bool SafeExists(this FileSystemInfo info) {
			return info != null && info.Exists;
		}

		/// <summary>
		///   Returns a value indicating whether the file exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>FileSystemInfo</c> instance to check existing. </param>
		/// <returns> The value indicating whether the file exists. </returns>
		public static bool SafeFileExists(this FileSystemInfo info) {
			return info.SafeExists() && (info.Attributes & FileAttributes.Directory) == 0;
		}

		/// <summary>
		///   Returns a value indicating whether the file exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>FileInfo</c> instance to check existing. </param>
		/// <returns> The value indicating whether the file exists. </returns>
		public static bool SafeFileExists(this FileInfo info) {
			return info.SafeExists();
		}

		/// <summary>
		///   Returns a value indicating whether the file exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>DirectoryInfo</c> instance to check existing. </param>
		/// <returns> <c>false</c> . </returns>
		public static bool SafeFileExists(this DirectoryInfo info) {
			return false;
		}

		/// <summary>
		///   Returns a value indicating whether the directory exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>FileSystemInfo</c> instance to check existing. </param>
		/// <returns> The value indicating whether the directory exists. </returns>
		public static bool SafeDirectoryExists(this FileSystemInfo info) {
			return info.SafeExists() && (info.Attributes & FileAttributes.Directory) != 0;
		}

		/// <summary>
		///   Returns a value indicating whether the directory exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>DirectoryInfo</c> instance to check existing. </param>
		/// <returns> The value indicating whether the directory exists. </returns>
		public static bool SafeDirectoryExists(this DirectoryInfo info) {
			return info.SafeExists();
		}

		/// <summary>
		///   Returns a value indicating whether the directory exists null-safely.
		/// </summary>
		/// <param name="info"> A <c>FileInfo</c> instance to check existing. </param>
		/// <returns> <c>false</c> . </returns>
		public static bool SafeDirectoryExists(this FileInfo info) {
			return false;
		}

		/// <summary>
		///   Returns a <c>FileInfo</c> combining a base <c>DirectoryInfo</c> and a relative file path.
		/// </summary>
		/// <param name="baseDirInfo"> A base <c>DirectoryInfo</c> . </param>
		/// <param name="relativeFilePath"> A relative file path. </param>
		/// <returns> The <c>FileInfo</c> for the combined path. </returns>
		public static FileInfo GetFile(
				this DirectoryInfo baseDirInfo, string relativeFilePath) {
			return new FileInfo(Path.Combine(baseDirInfo.FullName, relativeFilePath));
		}

		/// <summary>
		///   Returns a <c>DirectoryInfo</c> combining a base <c>DirectoryInfo</c> and a relative directory path.
		/// </summary>
		/// <param name="baseDirInfo"> A base <c>DirectoryInfo</c> . </param>
		/// <param name="relativeDirPath"> A relative directory path. </param>
		/// <returns> The <c>DirectoryInfo</c> for the combined path. </returns>
		public static DirectoryInfo GetDirectory(
				this DirectoryInfo baseDirInfo, string relativeDirPath) {
			return new DirectoryInfo(Path.Combine(baseDirInfo.FullName, relativeDirPath));
		}
	}
}