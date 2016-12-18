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

using System.IO;
using NUnit.Framework;
using Paraiba.Core;
using Paraiba.IO;

namespace Paraiba.Tests.IO {
    /// <summary>
    /// Tests for <see cref="ParaibaPath" />.
    /// </summary>
    [TestFixture]
    public class ParaibaPathTest {
        [Test]
        [TestCase(@"c:", ExpectedResult = @"c:")]
        [TestCase(@"c:/", ExpectedResult = @"c:\")]
        [TestCase(@"c:/test", ExpectedResult = @"c:\test")]
        [TestCase(@"c:/test/", ExpectedResult = @"c:\test\")]
        [TestCase(@"c:\", ExpectedResult = @"c:\")]
        [TestCase(@"c:\test", ExpectedResult = @"c:\test")]
        [TestCase(@"c:\test\", ExpectedResult = @"c:\test\")]
        public string NormalizeDirectorySeparatorChar(string path) {
            path = Serialize(path);
            return DeserializeAbsolutePath(ParaibaPath.NormalizeDirectorySeparators(path));
        }

        [Test]
        [TestCase(@"c:", ExpectedResult = @"c:\")]
        [TestCase(@"c:\", ExpectedResult = @"c:\")]
        [TestCase(@"c:\test", ExpectedResult = @"c:\test\")]
        [TestCase(@"c:\test\", ExpectedResult = @"c:\test\")]
        public string ComplementDirectorySeparatorChar(string path) {
            path = Serialize(path);
            return
                    DeserializeAbsolutePath(
                        ParaibaPath.NormalizeDirectorySeparatorsAddingToTail(path));
        }

        [Test]
        [TestCase(@"c:\aaa\bbb\", @"c:\aaa\ccc\", ExpectedResult = @"..\bbb\")]
        [TestCase(@"c:\aaa\bbb", @"c:\aaa", ExpectedResult = @"bbb")]
        [TestCase(@"c:\aaa\bbb\", @"c:\aaa\", ExpectedResult = @"bbb\")]
        [TestCase(@"c:\あいう\えお", @"c:\あいう", ExpectedResult = @"えお")]
        [TestCase(@"c:\ソソソ\えお", @"c:\ソソ", ExpectedResult = @"..\ソソソ\えお")]
        [TestCase(@"c:\aaa\bbb\ccc", @"c:\aaa\bbb\ddd", ExpectedResult = @"..\ccc")]
        [TestCase(@"c:\aaa\ddd", @"c:\aaa\bbb\ddd", ExpectedResult = @"..\..\ddd")]
        [TestCase(@"c:\aaa\bbb\ccc", @"c:\aaa\ddd", ExpectedResult = @"..\bbb\ccc")]
        public string GetRelativePath(string path, string basePath) {
            path = Serialize(path);
            basePath = Serialize(basePath);
            return DeserializeRelativePath(ParaibaPath.GetRelativePath(path, basePath));
        }

        [Test]
        [TestCase(@"c:\aaa\bbb", @"c:\aaa")]
        [TestCase(@"c:\aaa\bbb\", @"c:\aaa")]
        [TestCase(@"c:\aaa\bbb", @"c:\aaa\")]
        [TestCase(@"c:\aaa\bbb\", @"c:\aaa\")]
        [TestCase(@"c:\aaa", @"c:\aaa\bbb")]
        [TestCase(@"c:\aaa\", @"c:\aaa\bbb")]
        [TestCase(@"c:\aaa", @"c:\aaa\bbb\")]
        [TestCase(@"c:\aaa\", @"c:\aaa\bbb\")]
        [TestCase(@"c:\aaa\bbb", @"d:\aaa\")]
        [TestCase(@"c:\あ\い\う\え\お", @"d:\a\b\c\d\e")]
        [TestCase(@"c:\aaa\bbb", @"c:\aaa\bbb")]
        [TestCase(@"c:\aaa\bbb\", @"c:\aaa\bbb")]
        [TestCase(@"c:\aaa\bbb", @"c:\aaa\bbb\")]
        [TestCase(@"c:\a a\ db", @"c:\a　 \b b\")]
        public void GetFullPath(string path, string basePath) {
            path = Serialize(path);
            basePath = Serialize(basePath);
            Assert.That(
                ParaibaPath.GetFullPath(ParaibaPath.GetRelativePath(path, basePath), basePath),
                Is.EqualTo(path));
        }

        private static string Serialize(string path) {
            path = path.Replace('/', Path.AltDirectorySeparatorChar)
                    .Replace('\\', Path.DirectorySeparatorChar);
            if (ParaibaEnvironment.OnUnixLike()) {
                path = path.Substring(2);
            }
            return path;
        }

        private static string DeserializeAbsolutePath(string path) {
            path = path.Replace(Path.DirectorySeparatorChar, '\\');
            if (ParaibaEnvironment.OnUnixLike()) {
                path = "c:" + path;
            }
            return path;
        }

        private static string DeserializeRelativePath(string path) {
            return path.Replace(Path.DirectorySeparatorChar, '\\');
        }
    }
}