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
	///   A utility class for writing data to files.
	/// </summary>
	public static class ParaibaFile {
		/// <summary>
		/// Write the specified byte array to the file
		/// if the file does not exist or has different size with the byte array.
		/// </summary>
		/// <param name="fileInfo">The <c>FileInfo</c> instance to be written.</param>
		/// <param name="byteArray">The byte array to be written.</param>
		public static void WriteIfDifferentSize(
				FileInfo fileInfo, byte[] byteArray) {
			if (fileInfo.Exists && fileInfo.Length == byteArray.Length) {
				return;
			}
			fileInfo.Directory.Create();
			File.WriteAllBytes(fileInfo.FullName, byteArray);
		}

		/// <summary>
		/// Write the specified byte array to the file
		/// if the file does not exist or has different size with the byte array.
		/// </summary>
		/// <param name="path">The file path to be written.</param>
		/// <param name="byteArray">The byte array to be written.</param>
		public static void WriteIfDifferentSize(
				string path, byte[] byteArray) {
			WriteIfDifferentSize(new FileInfo(path), byteArray);
		}
	}
}