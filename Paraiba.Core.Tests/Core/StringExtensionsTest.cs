#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
        [TestCase("", "", ExpectedResult = 0)]
        [TestCase("a", "", ExpectedResult = -1)]
        [TestCase("abc", "b", ExpectedResult = -2)]
        [TestCase("abc", "ac", ExpectedResult = -1)]
        [TestCase("abc", "abc", ExpectedResult = 0)]
        [TestCase("adbdc", "abc", ExpectedResult = -2)]
        public int CalculateSimilarity(string str1, string str2) {
            return str1.CalculateSimilarity(str2);
        }

        [Test]
        [TestCase("", 0, ExpectedResult = "")]
        [TestCase("abc", 0, ExpectedResult = "")]
        [TestCase("abc", 1, ExpectedResult = "a")]
        [TestCase("abc", 2, ExpectedResult = "ab")]
        [TestCase("abc", 3, ExpectedResult = "abc")]
        [TestCase("abc", 4, typeof(ArgumentOutOfRangeException), ExpectedResult = null)]
        public string Left(string text, int length, Type expectedException = null) {
            try {
                return text.Left(length);
            } catch (Exception e) {
                Assert.That(e, Is.TypeOf(expectedException));
                return null;
            }
        }

        [Test]
        [TestCase("", 0, ExpectedResult = "")]
        [TestCase("abc", 0, ExpectedResult = "")]
        [TestCase("abc", 1, ExpectedResult = "c")]
        [TestCase("abc", 2, ExpectedResult = "bc")]
        [TestCase("abc", 3, ExpectedResult = "abc")]
        [TestCase("abc", 4, typeof(ArgumentOutOfRangeException), ExpectedResult = null)]
        public string Right(string text, int length, Type expectedException = null) {
            try {
                return text.Right(length);
            } catch (Exception e) {
                Assert.That(e, Is.TypeOf(expectedException));
                return null;
            }
        }

        [Test]
        [TestCase("a a a", "a", 0, 5, ExpectedResult = new[] { 0, 2, 4 })]
        [TestCase("abcdefg", "h", 0, 7, ExpectedResult = new int[] { })]
        [TestCase("aa aa aa aa", "aa", 1, 9, ExpectedResult = new[] { 3, 6 })]
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