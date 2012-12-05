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
using System.Diagnostics;
using System.Linq.Expressions;
using Paraiba.Collections.Generic;
using Paraiba.Collections.Generic.Compare;

namespace Paraiba.Utility {
	[DebuggerStepThrough]
	public static class Make {
		// TODO: 移動
		private static Func<T, TR> Memoize<T, TR>(ref Func<T, TR> func) {
			var cache = new Dictionary<T, TR>();
			var f = func;
			func = arg => {
				TR value;
				if (!cache.TryGetValue(arg, out value)) {
					value = f(arg);
					cache.Add(arg, f(arg));
				}
				return value;
			};
			return func;
		}

		public static Expression<Func<TR>> Expression<TR>(
				Expression<Func<TR>> e) {
			return e;
		}

		public static Expression<Func<T1, TR>> Expression<T1, TR>(
				Expression<Func<T1, TR>> e) {
			return e;
		}

		public static Expression<Func<T1, T2, TR>> Expression<T1, T2, TR>(
				Expression<Func<T1, T2, TR>> e) {
			return e;
		}

		public static Expression<Func<T1, T2, T3, TR>> Expression
				<T1, T2, T3, TR>(Expression<Func<T1, T2, T3, TR>> e) {
			return e;
		}

		public static Expression<Func<T1, T2, T3, T4, TR>> Expression
				<T1, T2, T3, T4, TR>(Expression<Func<T1, T2, T3, T4, TR>> e) {
			return e;
		}

		public static Action Action(this Action action) {
			return action;
		}

		public static Action<T> Create<T>(this Action<T> action) {
			return action;
		}

		public static Action<T1, T2> Create<T1, T2>(this Action<T1, T2> action) {
			return action;
		}

		public static Action<T1, T2, T3> Create<T1, T2, T3>(
				this Action<T1, T2, T3> action) {
			return action;
		}

		public static Action<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
				this Action<T1, T2, T3, T4> action) {
			return action;
		}

		public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(
				IDictionary<TKey, TValue> dict) {
			return new Dictionary<TKey, TValue>(dict);
		}

		public static List<T> List<T>(IEnumerable<T> source) {
			return new List<T>(source);
		}

		public static EnumerableUnit<T> EnumerableUnit<T>(T unit) {
			return new EnumerableUnit<T>(unit);
		}

		public static IEnumerable<T> Enumerable<T>(params T[] items) {
			return items;
		}

		public static ComparerWithFunc<T> Comparer<T>(
				this Func<T, T, int> compareFunc) {
			return new ComparerWithFunc<T>(compareFunc);
		}
	}
}