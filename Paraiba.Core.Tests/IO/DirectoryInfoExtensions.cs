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
using System.Linq;
using NUnit.Framework;
using Paraiba.IO;

namespace Paraiba.Tests.IO {
	/// <summary>
	///   Tests for <see cref="DirectoryInfoExtensions" /> .
	/// </summary>
	[TestFixture]
	public class DirectoryInfoExtensionsTest {
		[Test]
		public void SafeDelete() {
			var dir = new DirectoryInfo("abc");
			dir.SafeDelete(true);
			Assert.That(dir.SafeDelete(true), Is.False);
			dir.GetDirectory("test2").Create();
			Assert.That(dir.SafeDelete(true), Is.True);
			Assert.That(dir.Exists, Is.False);
		}

		[Test]
		public void Clear() {
			var dir = new DirectoryInfo("abc");
			var subdir = dir.GetDirectory("test2");
			subdir.Create();
			using (var fs = new FileStream(subdir.GetFile("test3.txt").FullName, FileMode.Create)) {
				fs.WriteByte(1);
			}
			dir.Clear();
			Assert.That(dir.Exists, Is.True);
			Assert.That(dir.SafeClear(), Is.True);
			Assert.That(dir.SafeDelete(), Is.True);
			dir.Refresh();
			Assert.That(dir.Exists, Is.False);
			Assert.That(dir.SafeClear(), Is.False);
		}
	}
}