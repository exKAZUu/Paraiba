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

using System.Linq;
using NUnit.Framework;
using Paraiba.Core;

namespace Paraiba.Tests.Core {
	/// <summary>
	///   Tests for <see cref="StringExtensions" /> .
	/// </summary>
	[TestFixture]
	public class StringExtensionsTest {
		[Test]
		[TestCase("a a a", "a", 0, 5, Result = new[] { 0, 2, 4 })]
		[TestCase("abcdefg", "h", 0, 7, Result = new int[] { })]
		[TestCase("aa aa aa aa", "aa", 1, 9, Result = new[] { 3, 6 })]
		public int[] IndicesOf(string text, string value, int startIndex, int count) {
			return text.IndicesOf(value, startIndex, count).ToArray();
		}

		[Test]
		public void ReplaceNewlinesForWindows() {
			Assert.That("aa\r\na\n".ReplaceNewlinesForWindows(), Is.EqualTo("aa\r\na\r\n"));
		}

		[Test]
		public void ReplaceNewlinesForUnix() {
			Assert.That("aa\r\na\n".ReplaceNewlinesForUnix(), Is.EqualTo("aa\na\n"));
		}
	}
}