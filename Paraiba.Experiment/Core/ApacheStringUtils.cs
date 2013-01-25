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

using System;
using System.Linq;

namespace Paraiba.Core {
	public static class ApacheStringUtils {
		public static string Left(this string str, int length) {
			return str.Substring(0, length);
		}

		public static string Right(this string str, int length) {
			return str.Substring(str.Length - length, length);
		}

		public static string SubstringBefore(this string str, string separator) {
			var index = str.IndexOf(separator);
			return index >= 0 ? str.Substring(0, index) : str;
		}

		public static string SubstringAfter(this string str, string separator) {
			var index = str.IndexOf(separator);
			return index >= 0
					? str.Substring(index + separator.Length)
					: string.Empty;
		}

		public static string SubstringBeforeLast(
				this string str, string separator) {
			var index = str.LastIndexOf(separator);
			return index >= 0 ? str.Substring(0, index) : str;
		}

		public static string SubstringAfterLast(
				this string str, string separator) {
			var index = str.LastIndexOf(separator);
			return index >= 0
					? str.Substring(index + separator.Length)
					: string.Empty;
		}

		public static string StripMargin(this string str) {
			return str.Split(new[] { "\r\n" }, StringSplitOptions.None)
			          .Select(l => l.TrimStart().Substring(1)).JoinString("\r\n");
		}
	}
}