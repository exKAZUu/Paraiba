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
using NUnit.Framework;
using Paraiba.IO;

namespace Paraiba.Tests.IO {
	/// <summary>
	///   Tests for <see cref="FileUtils" /> .
	/// </summary>
	[TestFixture]
	public class FileUtilsTest {
		[Test]
		public void TestCopyRecursively() {
			var tempPath = Path.GetTempPath();
			var srcDirPath1 = Path.Combine(tempPath, "paraiba_test");
			var srcDirPath2 = Path.Combine(srcDirPath1, "paraiba_test2");
			Directory.CreateDirectory(srcDirPath1);
			using (var sw = File.CreateText(Path.Combine(srcDirPath1, "test.txt"))) {
				sw.WriteLine("test line");
			}
			Directory.CreateDirectory(srcDirPath2);
			using (var sw = File.CreateText(Path.Combine(srcDirPath2, "test.txt"))) {
				sw.WriteLine("test line2");
			}
			var outDirPath1 = Path.Combine(tempPath, "paraiba_out");
			var outDirPath2 = Path.Combine(outDirPath1, "paraiba_test2");
			FileUtils.CopyRecursively(srcDirPath1, outDirPath1);
			Assert.That(
					File.ReadAllText(Path.Combine(srcDirPath1, "test.txt")),
					Is.EqualTo(File.ReadAllText(Path.Combine(outDirPath1, "test.txt"))));
			Assert.That(
					File.ReadAllText(Path.Combine(srcDirPath2, "test.txt")),
					Is.EqualTo(File.ReadAllText(Path.Combine(outDirPath2, "test.txt"))));
			Directory.Delete(srcDirPath1, true);
			Directory.Delete(outDirPath1, true);
		}
	}
}