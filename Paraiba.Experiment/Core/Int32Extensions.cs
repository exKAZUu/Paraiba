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

namespace Paraiba.Core {
	public static class Int32Extensions {
		public static char ToChar(this int i) {
			return (char)(i + '0');
		}

		public static void Times(this int n, Action action) {
			for (int i = 0; i < n; i++) {
				action();
			}
		}

		public static void Times(this int n, Action<int> action) {
			for (int i = 0; i < n; i++) {
				action(i);
			}
		}

		public static void UpTo(this int n, int max, Action action) {
			while (n <= max) {
				action();
				n++;
			}
		}

		public static void UpTo(this int n, int max, Action<int> action) {
			while (n <= max) {
				action(n);
				n++;
			}
		}

		public static void DownTo(this int n, int min, Action action) {
			while (n >= min) {
				action();
				n--;
			}
		}

		public static void DownTo(this int n, int min, Action<int> action) {
			while (n >= min) {
				action(n);
				n--;
			}
		}

		public static void Step(this int n, int limit, int step, Action action) {
			while (n <= limit) {
				action();
				n += step;
			}
		}

		public static void Step(
				this int n, int limit, int step, Action<int> action) {
			while (n <= limit) {
				action(n);
				n += step;
			}
		}
	}
}