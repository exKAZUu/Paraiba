﻿#region License

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
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Paraiba.IO;

namespace Paraiba.Tests.IO {
    /// <summary>
    ///   Tests for <see cref="FileSystemInfoExtensions" /> .
    /// </summary>
    [TestFixture]
    public class FileSystemInfoExtensionsTest {
        private static readonly string FilePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
        private static readonly string DirPath = Path.GetDirectoryName(FilePath);

        [Test]
        public void SafeExistsForFileInfo() {
            Assert.That(((FileInfo)null).SafeExists(), Is.False);
            Assert.That(new FileInfo(DirPath).SafeExists(), Is.False);
            Assert.That(new FileInfo(DirPath).SafeFileExists(), Is.False);
            Assert.That(new FileInfo(DirPath).SafeDirectoryExists(), Is.False);
            Assert.That(new FileInfo(FilePath).SafeExists(), Is.True);
            Assert.That(new FileInfo(FilePath).SafeFileExists(), Is.True);
            Assert.That(new FileInfo(FilePath).SafeDirectoryExists(), Is.False);
        }

        [Test]
        public void SafeExistsForDirectoryInfo() {
            Assert.That(((DirectoryInfo)null).SafeExists(), Is.False);
            Assert.That(new DirectoryInfo(DirPath).SafeExists(), Is.True);
            Assert.That(new DirectoryInfo(DirPath).SafeFileExists(), Is.False);
            Assert.That(new DirectoryInfo(DirPath).SafeDirectoryExists(), Is.True);
            Assert.That(new DirectoryInfo(FilePath).SafeExists(), Is.False);
            Assert.That(new DirectoryInfo(FilePath).SafeFileExists(), Is.False);
            Assert.That(new DirectoryInfo(FilePath).SafeDirectoryExists(), Is.False);
        }

        [Test]
        public void SafeExistsForFileSystemInfo() {
            Assert.That(((FileSystemInfo)null).SafeExists(), Is.False);
            Assert.That((new FileInfo(DirPath)).SafeExists(), Is.False);
            Assert.That(
                ((FileSystemInfo)new FileInfo(DirPath)).SafeFileExists(), Is.False);
            Assert.That(
                ((FileSystemInfo)new FileInfo(DirPath)).SafeDirectoryExists(), Is.False);
            Assert.That((new FileInfo(FilePath)).SafeExists(), Is.True);
            Assert.That(
                ((FileSystemInfo)new FileInfo(FilePath)).SafeFileExists(), Is.True);
            Assert.That(
                ((FileSystemInfo)new FileInfo(FilePath)).SafeDirectoryExists(), Is.False);
            Assert.That((new DirectoryInfo(DirPath)).SafeExists(), Is.True);
            Assert.That(
                ((FileSystemInfo)new DirectoryInfo(DirPath)).SafeFileExists(), Is.False);
            Assert.That(
                ((FileSystemInfo)new DirectoryInfo(DirPath)).SafeDirectoryExists(), Is.True);
            Assert.That((new DirectoryInfo(FilePath)).SafeExists(), Is.False);
            Assert.That(
                ((FileSystemInfo)new DirectoryInfo(FilePath)).SafeFileExists(), Is.False);
            Assert.That(
                ((FileSystemInfo)new DirectoryInfo(FilePath)).SafeDirectoryExists(),
                Is.False);
        }

        [Test]
        public void GetFile() {
            var info = new DirectoryInfo(".").GetFile("test.txt");
            var info2 = new FileInfo("./test.txt");
            Assert.That(info.FullName, Is.EqualTo(info2.FullName));
        }

        [Test]
        public void GetDirectory() {
            var info = new DirectoryInfo(".").GetDirectory("test");
            var info2 = new FileInfo("./test");
            Assert.That(info.FullName, Is.EqualTo(info2.FullName));
        }
    }
}