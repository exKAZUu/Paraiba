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
using NUnit.Framework;
using Paraiba.IO;

namespace Paraiba.Tests.IO {
	/// <summary>
	///   Tests for <see cref="FileSystemInfoExtensions" /> .
	/// </summary>
	[TestFixture]
	public class FileSystemInfoExtensionsTest {
		[Test]
		public void SafeExistsForFileInfo() {
			Assert.That(((FileInfo)null).SafeExists(), Is.False);
			Assert.That(new FileInfo(@"C:\").SafeExists(), Is.False);
			Assert.That(new FileInfo(@"C:\").SafeFileExists(), Is.False);
			Assert.That(new FileInfo(@"C:\").SafeDirectoryExists(), Is.False);
			Assert.That(new FileInfo(@"C:\Windows\notepad.exe").SafeExists(), Is.True);
			Assert.That(new FileInfo(@"C:\Windows\notepad.exe").SafeFileExists(), Is.True);
			Assert.That(new FileInfo(@"C:\Windows\notepad.exe").SafeDirectoryExists(), Is.False);
		}

		[Test]
		public void SafeExistsForDirectoryInfo() {
			Assert.That(((DirectoryInfo)null).SafeExists(), Is.False);
			Assert.That(new DirectoryInfo(@"C:\").SafeExists(), Is.True);
			Assert.That(new DirectoryInfo(@"C:\").SafeFileExists(), Is.False);
			Assert.That(new DirectoryInfo(@"C:\").SafeDirectoryExists(), Is.True);
			Assert.That(new DirectoryInfo(@"C:\Windows\notepad.exe").SafeExists(), Is.False);
			Assert.That(new DirectoryInfo(@"C:\Windows\notepad.exe").SafeFileExists(), Is.False);
			Assert.That(new DirectoryInfo(@"C:\Windows\notepad.exe").SafeDirectoryExists(), Is.False);
		}

		[Test]
		public void SafeExistsForFileSystemInfo() {
			Assert.That(((FileSystemInfo)null).SafeExists(), Is.False);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\")).SafeExists(), Is.False);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\")).SafeFileExists(), Is.False);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\")).SafeDirectoryExists(), Is.False);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\Windows\notepad.exe")).SafeExists(), Is.True);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\Windows\notepad.exe")).SafeFileExists(), Is.True);
			Assert.That(((FileSystemInfo)new FileInfo(@"C:\Windows\notepad.exe")).SafeDirectoryExists(), Is.False);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\")).SafeExists(), Is.True);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\")).SafeFileExists(), Is.False);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\")).SafeDirectoryExists(), Is.True);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\Windows\notepad.exe")).SafeExists(), Is.False);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\Windows\notepad.exe")).SafeFileExists(), Is.False);
			Assert.That(((FileSystemInfo)new DirectoryInfo(@"C:\Windows\notepad.exe")).SafeDirectoryExists(), Is.False);
		}

		[Test]
		public void GetFile() {
			var info = new DirectoryInfo("C:/test").GetFile("test.txt");
			Assert.That(info.FullName, Is.EqualTo("C:\\test\\test.txt"));
		}

		[Test]
		public void GetDirectory() {
			var info = new DirectoryInfo("C:/test").GetDirectory("test");
			Assert.That(info.FullName, Is.EqualTo("C:\\test\\test"));
		}
	}
}