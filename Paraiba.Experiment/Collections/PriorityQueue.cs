using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections
{
	/// <summary>
	/// 優先度付きキューを表します。
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public class PriorityQueue<T>
	{
		private readonly List<T> _array;
		private readonly IComparer<T> _cmp;

		public PriorityQueue()
			: this(Comparer<T>.Default)
		{
		}

		public PriorityQueue(IComparer<T> cmp)
		{
			Contract.Requires(cmp != null);

			// 最初の要素は番兵用に確保
			_array = new List<T> { default(T) };
			_cmp = cmp;
		}

		/// <summary>
		/// <see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数を取得します。
		/// </summary>
		/// <returns>
		/// <see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数。
		/// </returns>
		public int Count
		{
			get { return _array.Count - 1; }
		}

		public void Clear()
		{
			_array.RemoveRange(1, _array.Count - 1);
		}

		public bool Contains(T item)
		{
			return _array.IndexOf(item, 1) != -1;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Contract.Requires(0 <= arrayIndex);
			Contract.Requires(arrayIndex + Count <= array.Length);

			var arr = _array;
			int n = arr.Count;
			if (n <= 2)
			{
				if (n == 2)
					array[arrayIndex] = arr[1];
				return;
			}

			var cmp = _cmp;
			int endIndex = n + arrayIndex - 1;

			for (int i = 1; i < n; i++)
				array[endIndex - (i)] = arr[i];

			for (n--; n >= 3; n--)
			{
				// 優先順位の最も高い要素を記憶する
				var result = array[endIndex - (1)];

				// 末尾の要素を根として考えてコピーする
				var item = array[endIndex - (n)];
				// downheap 操作により根の値を適切な位置へ移動する
				int i = 1, j = 2, k = j - (cmp.Compare(array[endIndex - (j + 1)], array[endIndex - (j)]) >> 31);
				T tmp;
				while ((j = k*2) < n && cmp.Compare(item, (tmp = array[endIndex - (j)])) > 0)
				{
					array[endIndex - (i)] = array[endIndex - (k)];
					i = k;
					k = j - (cmp.Compare(array[endIndex - (j + 1)], tmp) >> 31);
				}
				array[endIndex - (i)] = array[endIndex - (k)];
				// 適切な位置に末尾の要素を配置
				array[endIndex - (k)] = item;

				// 番兵として残しておいた末尾の要素のコピー元に、優先順位の最も高い要素を移動する
				array[endIndex - (n)] = result;
			}
			{
				T item = array[endIndex - (2)];
				array[endIndex - (2)] = array[endIndex - (1)];
				array[endIndex - (1)] = item;
			}
		}

		public T Dequeue()
		{
			var arr = _array;

			// 要素数のチェック
			int n = arr.Count - 1;
			if (n < 1)
				throw new InvalidOperationException("PriorityQueue が空です。");

			var cmp = _cmp;

			// 優先順位の最も高い要素を記憶する
			T result = arr[1];

			if (n <= 2)
			{
				// 要素が2つ以下の場合、残りの要素を根に移動するだけ
				arr[1] = arr[n];
			}
			else
			{
				// 末尾の要素を根として考えてコピーする
				var item = arr[n];
				// downheap 操作により根の値を適切な位置へ移動する
				int i = 1, j = 2, k = j - (cmp.Compare(arr[j + 1], arr[j]) >> 31);
				T tmp;
				while ((j = k*2) < n && cmp.Compare(item, (tmp = arr[j])) > 0)
				{
					arr[i] = arr[k];
					i = k;
					k = j - (cmp.Compare(arr[j + 1], tmp) >> 31);
				}
				arr[i] = arr[k];
				// 適切な位置に末尾の要素を配置
				arr[k] = item;
			}

			// 優先順位の最も低い要素のコピー元（番兵）を削除
			arr.RemoveAt(n);

			return result;
		}

		public void Enqueue(T item)
		{
			var arr = _array;
			var cmp = _cmp;

			// 番兵
			arr[0] = item;
			// とりあえず末尾の要素として配置する
			arr.Add(item);

			// upheap 操作により末尾の要素を適切な位置へ移動する
			int i = arr.Count - 1, j;
			if (cmp.Compare(item, arr[j = i >> 1]) < 0)
			{
				do
				{
					arr[i] = arr[j];
					i = j;
				} while (cmp.Compare(item, arr[j = i >> 1]) < 0);
				// 適切な位置に末尾の要素を配置
				arr[i] = item;
			}
		}

		public bool Exists(Predicate<T> match)
		{
			var arr = _array;
			int length = arr.Count;
			for (int i = 1; i < length; i++)
			{
				if (match(arr[i]))
					return true;
			}
			return false;
		}

		public T Peek()
		{
			// 要素数のチェック
			if (_array.Count < 2)
				throw new InvalidOperationException("PriorityQueue が空です。");
			return _array[1];
		}

		public T[] ToArray()
		{
			var arr = new T[_array.Count - 1];
			CopyTo(arr, 0);
			return arr;
		}

		public void TrimExcess()
		{
			_array.TrimExcess();
		}
	}
}