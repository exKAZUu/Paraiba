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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Paraiba.Text;

namespace Paraiba.Core {
	public static class StringExtensions2 {
		public static char EndChar(this string str) {
			Contract.Requires(str != null);
			Contract.Requires(str.Length > 0);
			return str[str.Length - 1];
		}

		public static string AddIfNotEndsWith(this string str, char end) {
			Contract.Requires(str != null);
			if (str.Length == 0 || str.EndChar() != end) {
				return str + end;
			}
			return str;
		}

		public static string AddIfNotEndsWith(this string str, string end) {
			Contract.Requires(str != null);
			if (str.EndsWith(end)) {
				return str + end;
			}
			return str;
		}

		public static string JoinString<T>(
				this IEnumerable<T> strs, string delimiter) {
			Contract.Requires(strs != null);
			Contract.Requires(delimiter != null);
			return strs.JoinStringBuilder(delimiter).ToString();
		}

		public static string JoinString<T>(this IEnumerable<T> strs) {
			Contract.Requires(strs != null);
			return strs.JoinStringBuilder().ToString();
		}

		public static int ToInt(this string str) {
			Contract.Requires(str != null);
			return int.Parse(str);
		}

		public static int ToIntOrDefault(
				this string str, int defaultValue = default(int)) {
			Contract.Requires(str != null);
			int value;
			return int.TryParse(str, out value) ? value : defaultValue;
		}

		public static Tuple<string, string> Halve(
				this string str, char delimiter) {
			var index = str.IndexOf(delimiter);
			return index >= 0
					? Tuple.Create(
							str.Substring(0, index),
							str.Substring(index + 1))
					: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> Halve(
				this string str, string delimiter) {
			var index = str.IndexOf(delimiter);
			return index >= 0
					? Tuple.Create(
							str.Substring(0, index),
							str.Substring(index + delimiter.Length))
					: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> HalveLast(
				this string str, char delimiter) {
			var index = str.LastIndexOf(delimiter);
			return index >= 0
					? Tuple.Create(
							str.Substring(0, index),
							str.Substring(index + 1))
					: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> HalveLast(
				this string str, string delimiter) {
			var index = str.LastIndexOf(delimiter);
			return index >= 0
					? Tuple.Create(
							str.Substring(0, index),
							str.Substring(index + delimiter.Length))
					: Tuple.Create(str, string.Empty);
		}

		public static string[] HalveToArray(this string str, char delimiter) {
			int index = str.IndexOf(delimiter);
			if (index >= 0) {
				return new[]
				{ str.Substring(0, index), str.Substring(index + 1) };
			}
			return new[] { str, string.Empty };
		}

		public static string[] HalveToArray(this string str, string delimiter) {
			int index = str.IndexOf(delimiter);
			if (index >= 0) {
				return new[] {
						str.Substring(0, index),
						str.Substring(index + delimiter.Length)
				};
			}
			return new[] { str, string.Empty };
		}
	}
}