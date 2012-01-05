using System.Collections;
using System.Collections.Generic;
using Paraiba.Wrap;

namespace Paraiba.Utility
{
	public class ListWrapper<T> : ListWrapper<IList<T>, T>
	{
		public ListWrapper(IWrap<IList<T>> wrap)
			: base(wrap)
		{
		}
	}

	public class ListWrapper<TList, T> : ListWrapper<IWrap<TList>, TList, T>
		where TList : IList<T>
	{
		public ListWrapper(IWrap<TList> wrap)
			: base(wrap)
		{
		}
	}

	/// <summary>
	/// IListをラップしたIWrap型のオブジェクトを与えることで、
	/// 委譲により IList インタフェースの実装を提供します。
	/// </summary>
	/// <remarks>
	/// Adaptor pattern
	/// Adaptee: TWrap
	/// Adaptor: 本クラス
	/// Target: IList
	/// </remarks>
	/// <typeparam name="TWrap"></typeparam>
	/// <typeparam name="TList"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <seealso>http://kurusugawa.jp/blog/archives/585/</seealso>
	public class ListWrapper<TWrap, TList, T> : IList<T>
		where TWrap : IWrap<TList>
		where TList : IList<T>
	{
		private readonly TWrap _wrap;

		public ListWrapper(TWrap wrap)
		{
			_wrap = wrap;
		}

		public TWrap IWrap
		{
			get { return _wrap; }
		}

		public TList List
		{
			get { return _wrap.Value; }
		}

		#region IList<TValue> Members

		public void Add(T item)
		{
			_wrap.Value.Add(item);
		}

		public void Clear()
		{
			_wrap.Value.Clear();
		}

		public bool Contains(T item)
		{
			return _wrap.Value.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_wrap.Value.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _wrap.Value.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool Remove(T item)
		{
			return _wrap.Value.Remove(item);
		}

		public int Count
		{
			get { return _wrap.Value.Count; }
		}

		public bool IsReadOnly
		{
			get { return _wrap.Value.IsReadOnly; }
		}

		public int IndexOf(T item)
		{
			return _wrap.Value.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			_wrap.Value.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			_wrap.Value.RemoveAt(index);
		}

		public T this[int index]
		{
			get { return _wrap.Value[index]; }
			set { _wrap.Value[index] = value; }
		}

		#endregion
	}
}