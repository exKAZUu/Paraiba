﻿#region License

// Copyright (C) 2008-2012 Kazunori Sakamoto
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

namespace Paraiba.IO {
    public static class DirectoryInfoExtensions2 {
        public static void DeleteIfExist(this DirectoryInfo directoryInfo) {
            if (directoryInfo.Exists) {
                directoryInfo.Delete();
            }
        }

        public static void DeleteIfExist(
                this DirectoryInfo directoryInfo, bool recursive) {
            if (directoryInfo.Exists) {
                directoryInfo.Delete(recursive);
            }
        }

        public static string GetFullPath(this DirectoryInfo directoryInfo) {
            return directoryInfo.FullName;
        }

        public static string GetFullPath(
                this DirectoryInfo directoryInfo,
                params string[] subDirectoryNames) {
            return subDirectoryNames.Aggregate(
                    directoryInfo.FullName, Path.Combine);
        }

        public static DirectoryInfo GetSubDirectory(
                this DirectoryInfo directoryInfo,
                params string[] subDirectoryNames) {
            return
                    new DirectoryInfo(
                            directoryInfo.GetFullPath(subDirectoryNames));
        }

        public static FileInfo GetSubFile(
                this DirectoryInfo directoryInfo, string subFileName) {
            return
                    new FileInfo(
                            Path.Combine(
                                    directoryInfo.GetFullPath(), subFileName));
        }

        public static FileInfo GetSubFile(
                this DirectoryInfo directoryInfo, params string[] subNames) {
            return new FileInfo(directoryInfo.GetFullPath(subNames));
        }
    }
}