using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Paraiba.Collections.Generic;
using Paraiba.Utility;

namespace Paraiba.Linq
{
	public static class EnumerableExtensions2
	{
		/// <summary>
		/// Determines whether a sequence is sorted or not sorted.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">A sequence to be judged.</param>
		/// <returns>true if the source sequence is sorted; otherwise, false.</returns>
		public static bool IsSorted<TSource>(this IEnumerable<TSource> source)
		{
			Contract.Requires(source != null);

			return source.IsSorted(Comparer<TSource>.Default);
		}

		/// <summary>
		/// Determines whether a sequence is sorted or not sorted.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">A sequence to be judged.</param>
		/// <param name="cmp">An IComparer<(Of <(TSource>)>) to compare elements.</param>
		/// <returns>true if the source sequence is sorted; otherwise, false.</returns>
		public static bool IsSorted<TSource>(this IEnumerable<TSource> source, IComparer<TSource> cmp)
		{
			Contract.Requires(source != null);
			Contract.Requires(cmp != null);

			return source.Zip2Chain()
				.All(t => cmp.Compare(t.Item1, t.Item2) <= 0);
		}

		/// <summary>
		/// Determines whether a sequence contains no element.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <returns>true if the source sequence contains no element; otherwise false.</returns>
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			Contract.Requires(source != null);

			using (var itr = source.GetEnumerator())
			{
				return itr.MoveNext() == false;
			}
		}

		/// <summary>
		/// Determines whether a collection is null or contains no element.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of collection.</typeparam>
		/// <param name="collection">A collection in which to locate a value.</param>
		/// <returns>true if the source collection is null or contains no element; otherwise false.</returns>
		public static bool IsNullOrEmpty<TSource>(this ICollection<TSource> collection)
		{
			return collection == null || collection.Count <= 0;
		}

		/// <summary>
		/// Determines whether a sequence is null or contains no element.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <returns>true if the source sequence is null or contains no element; otherwise false.</returns>
		public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
				return true;

			using (var itr = source.GetEnumerator())
			{
				return itr.MoveNext() == false;
			}
		}

		/// <summary>
		/// Concatenate two sequences alternately.
		/// </summary>
		/// <example>{1,3,5}, {2,4,6,8} => {1,2,3,4,5,6,8}</example>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="source1">A sequence which is first concatenated.</param>
		/// <param name="source2">A sequence which is secondly concatenated.</param>
		/// <returns>A sequence that contains the  alternately concatenated elements of the two input sequences.</returns>
		public static IEnumerable<TSource> AlternatelyConcat<TSource>(this IEnumerable<TSource> source1, IEnumerable<TSource> source2)
		{
			Contract.Requires(source1 != null);
			Contract.Requires(source2 != null);
			Contract.Ensures(source1.All(e => Contract.Result<IEnumerable<TSource>>().Contains(e)));
			Contract.Ensures(source2.All(e => Contract.Result<IEnumerable<TSource>>().Contains(e)));

			using (var itr1 = source1.GetEnumerator())
			{
				using (var itr2 = source2.GetEnumerator())
				{
					while (itr1.MoveNext())
					{
						yield return itr1.Current;

						if (!itr2.MoveNext())
						{
							// itr2 が終わったので itr1 のみを参照する
							while (itr1.MoveNext())
							{
								yield return itr1.Current;
							}
							yield break;
						}
						yield return itr2.Current;
					}

					// itr1 が終わったので itr1 のみを参照する
					while (itr2.MoveNext())
					{
						yield return itr2.Current;
					}
					yield break;
				}
			}
		}

		public static IEnumerable<TResult> CastWithoutNull<TResult>(this IEnumerable source)
			where TResult : class
		{
			foreach (var item in source) {
				var result = item as TResult;
				if (result != null)
					yield return result;
			}
		}

		public static IEnumerable<TResult> CastWhere<TResult>(this IEnumerable source)
		{
			foreach (var item in source) {
				if (item is TResult)
					yield return (TResult)(object)item;
			}
		}

		/// <summary>
		/// 2 つのシーケンスの直積を求めます。{1,2}, {3,4,5} => {(1,3),(1,4),(1,5),(2,3),(2,4),(2,5)}
		/// </summary>
		/// <typeparam name="T1">1 つ目のシーケンスの要素の型</typeparam>
		/// <typeparam name="T2">2 つ目のシーケンスの要素の型</typeparam>
		/// <param name="source1">1 つ目のシーケンス</param>
		/// <param name="source2">2 つ目のシーケンス</param>
		/// <returns>2 つのシーケンスの直積を表すシーケンス</returns>
		public static IEnumerable<Tuple<T1, T2>> DirectProduct<T1, T2>(this IEnumerable<T1> source1,
																	   IEnumerable<T2> source2)
		{
			Contract.Requires(source1 != null);
			Contract.Requires(source2 != null);

			foreach (var item1 in source1)
			{
				foreach (var item2 in source2)
				{
					yield return Tuple.Create(item1, item2);
				}
			}
		}

		/// <summary>
		/// シーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンス</param>
		/// <param name="action">各要素に適用する関数</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			int index = 0;
			foreach (var item in source)
			{
				action(item);
				index++;
			}
			return index;
		}

		/// <summary>
		/// シーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンス</param>
		/// <param name="action">各要素に適用する関数。この関数は要素のインデックスも引数に取ります</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			int index = 0;
			foreach (var item in source)
			{
				action(item, index++);
			}
			return index;
		}

		/// <summary>
		/// シーケンスのシーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンスのシーケンス</param>
		/// <param name="action">各要素に適用する関数</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach2<T>(this IEnumerable<IEnumerable<T>> source, Action<T> action)
		{
			// HACK: 共変性の導入(IEnumerable<IList<T>> への対応)
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			return source.Planarize().ForEach(action);
		}

		/// <summary>
		/// シーケンスのシーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンスのシーケンス</param>
		/// <param name="action">各要素に適用する関数</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach2<T>(this IEnumerable<List<T>> source, Action<T> action)
		{
			// HACK: 共変性の導入(IEnumerable<IList<T>> への対応)
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			return source.Planarize().ForEach(action);
		}

		/// <summary>
		/// シーケンスのシーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンスのシーケンス</param>
		/// <param name="action">各要素に適用する関数。この関数は要素の2つのインデックスも引数に取ります</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach2<T>(this IEnumerable<IEnumerable<T>> source, Action<T, int, int> action)
		{
			// HACK: 共変性の導入(IEnumerable<IList<T>> への対応)
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			int index1 = 0, n = 0;
			foreach (var item2 in source)
			{
				int index2 = 0;
				foreach (var item in item2)
				{
					action(item, index1, index2);
					index2++;
				}
				index1++;
				n += index2;
			}
			return n;
		}

		/// <summary>
		/// シーケンスのシーケンスの各要素に対して関数を適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンスのシーケンス</param>
		/// <param name="action">各要素に適用する関数。この関数は要素の2つのインデックスも引数に取ります</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach2<T>(this IEnumerable<List<T>> source, Action<T, int, int> action)
		{
			// HACK: 共変性の導入(IEnumerable<IList<T>> への対応)
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			int index1 = 0, n = 0;
			foreach (var item2 in source)
			{
				int index2 = 0;
				foreach (var item in item2)
				{
					action(item, index1, index2);
					index2++;
				}
				index1++;
				n += index2;
			}
			return n;
		}

		/// <summary>
		/// シーケンスの各要素に対して2つの関数を交互に適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンス</param>
		/// <param name="action">各要素に適用する1つ目の関数</param>
		/// <param name="action2">各要素に適用する2つ目の関数</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach<T>(this IEnumerable<T> source, Action<T> action, Action<T> action2)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);
			Contract.Requires(action2 != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					action(itr.Current);
					index++;

					if (!itr.MoveNext())
					{
						break;
					}

					action2(itr.Current);
					index++;
				}
			}
			return index;
		}

		/// <summary>
		/// シーケンスの各要素に対して2つの関数を交互に適用します。
		/// </summary>
		/// <typeparam name="T">入力シーケンスの要素の型</typeparam>
		/// <param name="source">関数の適用対象のシーケンス</param>
		/// <param name="action">各要素に適用する1つ目の関数。この関数は要素のインデックスも引数に取ります</param>
		/// <param name="action2">各要素に適用する2つ目の関数。この関数は要素のインデックスも引数に取ります</param>
		/// <returns>処理した要素数</returns>
		public static int ForEach<T>(this IEnumerable<T> source, Action<T, int> action, Action<T, int> action2)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					action(itr.Current, index++);
					if (!itr.MoveNext())
					{
						break;
					}
					action2(itr.Current, index++);
				}
			}
			return index;
		}

		public static IEnumerable<Tuple<T1, TSecond>> Zip<T1, TSecond>(this IEnumerable<T1> first, IEnumerable<TSecond> second)
		{
			return first.Zip(second, Tuple.Create);
		}

		public static bool TryGetFirst<TResult>(this IEnumerable<TResult> source, out TResult first)
		{
			Contract.Requires(source != null);

			foreach (var item in source)
			{
				first = item;
				return true;
			}
			first = default(TResult);
			return false;
		}

		public static bool TryGetFirst<TResult>(this IEnumerable<TResult> source, Func<TResult, bool> predict,
												out TResult first)
		{
			Contract.Requires(source != null);
			Contract.Requires(predict != null);

			return source.Where(predict).TryGetFirst(out first);
		}

		public static IEnumerable<Tuple<TResult, TResult>> Zip2Chain<TResult>(this IEnumerable<TResult> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					var last = itr.Current;
					while (itr.MoveNext())
					{
						var item = itr.Current;
						yield return Tuple.Create(last, item);

						last = item;
					}
				}
			}
		}

		public static IEnumerable<Tuple<TResult, TResult, TResult>> Zip3Chain<TResult>(this IEnumerable<TResult> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					var last1 = itr.Current;
					if (itr.MoveNext())
					{
						var last2 = itr.Current;
						while (itr.MoveNext())
						{
							var item = itr.Current;
							yield return Tuple.Create(last1, last2, item);

							last1 = last2;
							last2 = item;
						}
					}
				}
			}
		}

		public static IEnumerable<Tuple<TResult, TResult, TResult, TResult>> Zip4Chain<TResult>(this IEnumerable<TResult> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					var last1 = itr.Current;
					if (itr.MoveNext())
					{
						var last2 = itr.Current;
						if (itr.MoveNext())
						{
							var last3 = itr.Current;
							while (itr.MoveNext())
							{
								var item = itr.Current;
								yield return Tuple.Create(last1, last2, last3, item);

								last1 = last2;
								last2 = last3;
								last3 = item;
							}
						}
					}
				}
			}
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}

		public static IEnumerable<string> SelectString<T>(this IEnumerable<T> source)
		{
			return source.Select(item => item.ToString());
		}

		public static IEnumerable<TResult> Repeat<TResult>(this IEnumerable<TResult> source)
		{
			while (true)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<TResult> Repeat<TResult>(this IEnumerable<TResult> source, int count)
		{
			while (--count > 0)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static int ForEachApartFirst<T>(this IEnumerable<T> source, Action<T> firstAction, Action<T> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(firstAction != null);
			Contract.Requires(action != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					firstAction(itr.Current);
					index++;
				}
				while (itr.MoveNext())
				{
					action(itr.Current);
					index++;
				}
			}
			return index;
		}

		public static int ForEachApartFirst<T>(this IEnumerable<T> source, Action<T> firstAction, Action<T, int> action)
		{
			Contract.Requires(source != null);
			Contract.Requires(firstAction != null);
			Contract.Requires(action != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					firstAction(itr.Current);
					index++;
				}
				while (itr.MoveNext())
				{
					action(itr.Current, index++);
				}
			}
			return index;
		}

		public static int ForEachApartLast<T>(this IEnumerable<T> source, Action<T> action, Action<T> lastAction)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);
			Contract.Requires(lastAction != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					T last = itr.Current;
					while (itr.MoveNext())
					{
						action(last);
						index++;
						last = itr.Current;
					}
					lastAction(last);
					index++;
				}
			}
			return index;
		}

		public static int ForEachApartLast<T>(this IEnumerable<T> source, Action<T, int> action,
											  Action<T, int> lastAction)
		{
			Contract.Requires(source != null);
			Contract.Requires(action != null);
			Contract.Requires(lastAction != null);

			int index = 0;
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					T last = itr.Current;
					while (itr.MoveNext())
					{
						action(last, index++);
						last = itr.Current;
					}
					lastAction(last, index++);
				}
			}
			return index;
		}

		public static IEnumerable<TResult> SelectApartFirst<TSource, TResult>(this IEnumerable<TSource> source,
																			  Func<TSource, TResult> firstSelector,
																			  Func<TSource, TResult> selector)
		{
			Contract.Requires(source != null);
			Contract.Requires(firstSelector != null);
			Contract.Requires(selector != null);

			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					yield return firstSelector(itr.Current);
				}
				while (itr.MoveNext())
				{
					yield return selector(itr.Current);
				}
			}
		}

		public static IEnumerable<TResult> SelectApartFirst<TSource, TResult>(this IEnumerable<TSource> source,
																			  Func<TSource, TResult> firstSelector,
																			  Func<TSource, int, TResult> selector)
		{
			Contract.Requires(source != null);
			Contract.Requires(firstSelector != null);
			Contract.Requires(selector != null);

			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					yield return firstSelector(itr.Current);
				}
				int index = 1;
				while (itr.MoveNext())
				{
					yield return selector(itr.Current, index++);
				}
			}
		}

		public static IEnumerable<TResult> SelectApartLast<TSource, TResult>(this IEnumerable<TSource> source,
																			 Func<TSource, TResult> selector,
																			 Func<TSource, TResult> lastSelector)
		{
			Contract.Requires(source != null);
			Contract.Requires(selector != null);
			Contract.Requires(lastSelector != null);

			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					TSource last = itr.Current;
					while (itr.MoveNext())
					{
						yield return selector(last);
						last = itr.Current;
					}
					yield return lastSelector(last);
				}
			}
		}

		public static IEnumerable<TResult> SelectApartLast<TSource, TResult>(this IEnumerable<TSource> source,
																			 Func<TSource, int, TResult> selector,
																			 Func<TSource, int, TResult> lastSelector)
		{
			Contract.Requires(source != null);
			Contract.Requires(selector != null);
			Contract.Requires(lastSelector != null);

			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					int index = 0;
					TSource last = itr.Current;
					while (itr.MoveNext())
					{
						yield return selector(last, index++);
						last = itr.Current;
					}
					yield return lastSelector(last, index);
				}
			}
		}

		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					while (itr.MoveNext())
					{
						yield return itr.Current;
					}
				}
			}
		}

		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (itr.MoveNext())
				{
					yield return itr.Current;
				}
			}
		}

		public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (!itr.MoveNext())
					yield break;

				var last = itr.Current;
				while (itr.MoveNext())
				{
					yield return last;
					last = itr.Current;
				}
			}
		}

		public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source)
		{
			using (var itr = source.GetEnumerator())
			{
				if (!itr.MoveNext())
					yield break;

				var last = itr.Current;
				while (itr.MoveNext())
				{
					last = itr.Current;
				}
				yield return last;
			}
		}

		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T last)
		{
			foreach (var item in source)
			{
				yield return item;
			}

			yield return last;
		}

		public static IEnumerable<T> ConcatFirst<T>(this IEnumerable<T> source, T first)
		{
			yield return first;

			foreach (var item in source)
			{
				yield return item;
			}
		}

		public static List<T> ToList<T>(this IEnumerable<T> source, int capacity)
		{
			var list = new List<T>(capacity);
			list.AddRange(source);
			return list;
		}

		public static T[] ToArray<T>(this IEnumerable<T> source, int length)
		{
			var array = new T[length];
			using (var itr = source.GetEnumerator())
			{
				for (int i = 0; i < array.Length && itr.MoveNext(); i++)
				{
					array[i] = itr.Current;
				}
			}
			return array;
		}

		public static void Repeat(this int count, Action action)
		{
			for (int i = 0; i < count; i++)
			{
				action();
			}
		}

		public static void Repeat(this int count, Action<int> action)
		{
			for (int i = 0; i < count; i++)
			{
				action(i);
			}
		}

		public static void While(this bool cond, Action action)
		{
			while (cond)
			{
				action();
			}
		}

		public static void While(this bool cond, Action<int> action)
		{
			int i = 0;
			while (cond)
			{
				action(i++);
			}
		}

		public static void DoWhile(this bool cond, Action action)
		{
			do
			{
				action();
			} while (cond);
		}

		public static void DoWhile(this bool cond, Action<int> action)
		{
			int i = 0;
			do
			{
				action(i++);
			} while (cond);
		}

		public static bool ReferenceEqualsItems<T>(this IEnumerable<T> source1, IEnumerable<T> source2)
		{
			using (var itr1 = source1.GetEnumerator())
			{
				using (var itr2 = source2.GetEnumerator())
				{
					while (itr1.MoveNext())
					{
						if (!itr2.MoveNext() || !ReferenceEquals(itr1.Current, itr2.Current))
						{
							return false;
						}
					}
					// itr1 と itr2 の要素数が等しいか
					return !itr2.MoveNext();
				}
			}
		}

		public static bool EqualsItems<T>(this IEnumerable<T> source1, IEnumerable<T> source2)
		{
			return source1.EqualsItems(source2, EqualityComparer<T>.Default);
		}

		public static bool EqualsItems<T>(this IEnumerable<T> source1, IEnumerable<T> source2,
										  IEqualityComparer<T> eqcmp)
		{
			using (var itr1 = source1.GetEnumerator())
			{
				using (var itr2 = source2.GetEnumerator())
				{
					while (itr1.MoveNext())
					{
						if (!itr2.MoveNext() || !eqcmp.Equals(itr1.Current, itr2.Current))
						{
							return false;
						}
					}
					// itr1 と itr2 の要素数が等しいか
					return !itr2.MoveNext();
				}
			}
		}

		public static bool EqualsItems<T>(this IEnumerable<T> source1, IEnumerable<T> source2,
										  Func<T, T, bool> eqcmpFunc)
		{
			using (var itr1 = source1.GetEnumerator())
			{
				using (var itr2 = source2.GetEnumerator())
				{
					while (itr1.MoveNext())
					{
						if (!itr2.MoveNext() || !eqcmpFunc(itr1.Current, itr2.Current))
						{
							return false;
						}
					}
					// itr1 と itr2 の要素数が等しいか
					return !itr2.MoveNext();
				}
			}
		}

		public static IEnumerable<IList<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> isSeparatorFunc)
		{
			using (var itr = source.GetEnumerator())
			{
				// 空のシーケンスに対しては何もしない
				if (!itr.MoveNext())
				{
					yield break;
				}

				var result = new List<T>();
				do
				{
					var e = itr.Current;
					if (isSeparatorFunc(e))
					{
						yield return result;
						result = new List<T>();
					}
					else
					{
						result.Add(e);
					}
				} while (itr.MoveNext());
				yield return result;
			}
		}

		public static IEnumerable<IList<T>> SplitWithoutEmpty<T>(this IEnumerable<T> source,
																 Func<T, bool> isSeparatorFunc)
		{
			using (var itr = source.GetEnumerator())
			{
				// 空のシーケンスに対しては何もしない
				if (!itr.MoveNext())
				{
					yield break;
				}

				var result = new List<T>();
				do
				{
					var e = itr.Current;
					if (isSeparatorFunc(e))
					{
						if (!result.IsEmpty())
						{
							yield return result;
							result = new List<T>();
						}
					}
					else
					{
						result.Add(e);
					}
				} while (itr.MoveNext());

				if (!result.IsEmpty())
				{
					yield return result;
				}
			}
		}

		public static IEnumerable<T> PickUpFirst<T>(this IEnumerable<IEnumerable<T>> sources)
		{
			foreach (var source in sources)
			{
				using (var itr = source.GetEnumerator())
				{
					itr.MoveNext();
					yield return itr.Current;
				}
			}
		}

		public static IEnumerable<T> PickUpFirst<T>(this IEnumerable<IList<T>> sources)
		{
			foreach (var source in sources)
			{
				yield return source[0];
			}
		}

		public static IEnumerable<T> PickUpFirst<T>(this IEnumerable<IEnumerable<T>> sources, Func<T, bool> predicate)
		{
			foreach (var source in sources)
			{
				yield return source.First(predicate);
			}
		}

		public static IEnumerable<T> PickUpFirst<T>(this IEnumerable<IList<T>> sources, Func<T, bool> predicate)
		{
			foreach (var source in sources)
			{
				yield return source.First(predicate);
			}
		}

		public static IEnumerable<T> PickUpValidFirst<T>(this IEnumerable<IEnumerable<T>> sources)
		{
			foreach (var source in sources)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<T> PickUpValidFirst<T>(this IEnumerable<IList<T>> sources)
		{
			foreach (var source in sources)
			{
				if (!source.IsEmpty())
				{
					yield return source[0];
				}
			}
		}

		public static IEnumerable<T> PickUpValidFirst<T>(this IEnumerable<IEnumerable<T>> sources,
														 Func<T, bool> predicate)
		{
			foreach (var source in sources)
			{
				T item;
				if (source.TryGetFirst(predicate, out item))
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<T> PickUpValidFirst<T>(this IEnumerable<IList<T>> sources, Func<T, bool> predicate)
		{
			foreach (var source in sources)
			{
				T item;
				if (source.TryGetFirst(predicate, out item))
				{
					yield return item;
				}
			}
		}

		public static bool IsIntersect<T>(this IEnumerable<T> source1, IEnumerable<T> source2)
		{
			return source1.Any(item => source2.Contains(item));
		}

		public static bool IsIntersect<T>(this IEnumerable<T> source1, IEnumerable<T> source2,
										  IEqualityComparer<T> comparer)
		{
			return source1.Any(item => source2.Contains(item, comparer));
		}

		public static IEnumerable<T> Planarize<T>(this IEnumerable<IEnumerable<T>> sources)
		{
			foreach (var source in sources)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<T> Planarize<T>(this IEnumerable<List<T>> sources)
		{
			foreach (var source in sources)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<T> Planarize<T>(this IEnumerable<IList<T>> sources)
		{
			foreach (var source in sources)
			{
				foreach (var item in source)
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<T> ToEnumerable<T>(this T item)
		{
			return Make.EnumerableUnit(item);
		}

		public static IEnumerable<T> TakeWhileWithNext<T>(this IEnumerable<T> source, Func<T, bool> func)
		{
			foreach (var item in source)
			{
				yield return item;
				if (!func(item))
				{
					break;
				}
			}
		}

		public static IEnumerable<T> OddIndexElements<T>(this IEnumerable<T> source)
		{
			using (var etr = source.GetEnumerator())
			{
				if (etr.MoveNext())
				{
					do
					{
						yield return etr.Current;
						etr.MoveNext();
					} while (etr.MoveNext());
				}
			}
		}

		public static IEnumerable<T> EvenIndexElements<T>(this IEnumerable<T> source)
		{
			using (var etr = source.GetEnumerator())
			{
				etr.MoveNext();
				if (etr.MoveNext())
				{
					do
					{
						yield return etr.Current;
						etr.MoveNext();
					} while (etr.MoveNext());
				}
			}
		}

		public static Tuple<List<T>, List<T>> Halve<T>(this IEnumerable<T> source, Func<T, bool> classifier)
		{
			var list1 = new List<T>();
			var list2 = new List<T>();

			foreach (var item in source) {
				if (classifier(item)) {
					list1.Add(item);
				}
				else {
					list2.Add(item);
				}
			}

			return Tuple.Create(list1, list2);
		}

		public static TSource MaxElementOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			where TKey : IComparable<TKey>
		{
			using (var etr = source.GetEnumerator()) {
				if (etr.MoveNext()) {
					var result = etr.Current;
					var maxValue = keySelector(result);
					while (etr.MoveNext()) {
						var item = etr.Current;
						var value = keySelector(item);
						if (maxValue.CompareTo(value) < 0) {
							result = item;
							maxValue = value;
						}
					}
					return result;
				}
			}
			return default(TSource);
		}

		public static TSource MinElementOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			where TKey : IComparable<TKey>
		{
			using (var etr = source.GetEnumerator()) {
				if (etr.MoveNext()) {
					var result = etr.Current;
					var minValue = keySelector(result);
					while (etr.MoveNext()) {
						var item = etr.Current;
						var value = keySelector(item);
						if (minValue.CompareTo(value) > 0) {
							result = item;
							minValue = value;
						}
					}
					return result;
				}
			}
			return default(TSource);
		}

		public static LinkedList<TSource> ToLinkedList<TSource>(this IEnumerable<TSource> source)
		{
			return new LinkedList<TSource>(source);
		}
	}
}