using System;
using System.Collections;
using System.Collections.Generic;

namespace Paraiba.Collections
{
	/// <summary>
	/// リスト内で実際に保持しているノードクラス
	/// パッケージ外からはポインタのように使用することができ、
	/// これを引数にして要素の削除や隣接ノードの取得が可能である。
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class SortedListNode<TKey, TValue>
	{
		internal KeyValuePair<TKey, TValue> item_;
		internal SortedListNode<TKey, TValue> next_;
		internal SortedListNode<TKey, TValue> prev_;

		internal SortedListNode()
		{
			prev_ = this;
			next_ = this;
		}

		public SortedListNode(TKey key, TValue value)
		{
			item_ = new KeyValuePair<TKey, TValue>(key, value);
		}

		public KeyValuePair<TKey, TValue> Item
		{
			get { return item_; }
		}
	}

	public class SortedListEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
	{
		private readonly SortedLinkedList<TKey, TValue> list;
		private SortedListNode<TKey, TValue> current;

		public SortedListEnumerator(SortedLinkedList<TKey, TValue> list, SortedListNode<TKey, TValue> current)
		{
			this.list = list;
			this.current = current;
		}

		#region IEnumerator<KeyValuePair<TKey,TValue>> Members

		///<summary>
		///列挙子の現在位置にあるコレクション内の要素を取得します。
		///</summary>
		///<returns>
		///コレクション内の、列挙子の現在位置にある要素。
		///</returns>
		public KeyValuePair<TKey, TValue> Current
		{
			get { return current.item_; }
		}

		///<summary>
		///コレクション内の現在の要素を取得します。
		///</summary>
		///<returns>
		///コレクション内の現在の要素。
		///</returns>
		///<exception cref="TValue:System.InvalidOperationException">
		///列挙子が、コレクションの最初の要素の前、または最後の要素の後に位置しています。
		///または 
		///列挙子が作成された後に、コレクションが変更されました。
		///</exception><filterpriority>2</filterpriority>
		object IEnumerator.Current
		{
			get { return current.item_; }
		}

		///<summary>
		///アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
		///</summary>
		///<filterpriority>2</filterpriority>
		public void Dispose()
		{
		}

		///<summary>
		///列挙子をコレクションの次の要素に進めます。
		///</summary>
		///<returns>
		///列挙子が次の要素に正常に進んだ場合は true。列挙子がコレクションの末尾を越えた場合は false。
		///</returns>
		///<exception cref="TValue:System.InvalidOperationException">
		///列挙子が作成された後に、コレクションが変更されました。 
		///</exception><filterpriority>2</filterpriority>
		public bool MoveNext()
		{
			var nextNode = current.next_;
			if (nextNode != list.head_)
			{
				current = nextNode;
				return true;
			}
			return false;
		}

		///<summary>
		///列挙子を初期位置、つまりコレクションの最初の要素の前に設定します。
		///</summary>
		///<exception cref="TValue:System.InvalidOperationException">
		///列挙子が作成された後に、コレクションが変更されました。 
		///</exception><filterpriority>2</filterpriority>
		public void Reset()
		{
			current = list.head_;
		}

		#endregion

		/// <summary>
		/// リストに要素を追加する
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void Add(TKey key, TValue value)
		{
			list.Add(key, value);
		}

		/// <summary>
		/// Current プロパティで示される要素を削除する
		/// この操作を行っても MoveNext メソッドに影響を与えない
		/// すなわち、削除後一つ前の要素に移動する
		/// </summary>
		/// <returns>削除に成功したかどうか</returns>
		public bool Remove()
		{
			var node = current;
			if (node != list.head_)
			{
				current = current.prev_;
				list.Remove(node);
				return true;
			}
			return false;
		}
	}

	public class SortedLinkedList<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
	{
		private readonly IComparer<TKey> _cmp;
		private int _count;
		internal SortedListNode<TKey, TValue> head_;

		public SortedLinkedList()
			: this(Comparer<TKey>.Default)
		{
		}

		public SortedLinkedList(IComparer<TKey> cmp)
		{
			_cmp = cmp;
			_count = 0;
			head_ = new SortedListNode<TKey, TValue>();
		}

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数を取得します。
		///</summary>
		///<returns>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数。
		///</returns>
		public int Count
		{
			get { return _count; }
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		///<summary>
		///コレクションを反復処理する列挙子を返します。
		///</summary>
		///<returns>
		///コレクションを反復処理するために使用できる <see cref="TValue:System.Collections.Generic.IEnumerator`1" />。
		///</returns>
		///<filterpriority>1</filterpriority>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedListEnumerator<TKey, TValue>(this, head_);
		}

		///<summary>
		///コレクションを反復処理する列挙子を返します。
		///</summary>
		///<returns>
		///コレクションを反復処理するために使用できる <see cref="TValue:System.Collections.IEnumerator" /> オブジェクト。
		///</returns>
		///<filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedListEnumerator<TKey, TValue>(this, head_);
		}

		#endregion

		public void Add(TKey key, TValue value)
		{
			// 番兵の設置
			head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

			AddPrivate(key, value, head_);
		}

		public void Add(TKey key, TValue value, SortedListNode<TKey, TValue> pivot)
		{
			// 番兵の設置
			head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

			if (_cmp.Compare(pivot.item_.Key, key) > 0)
				AddPrivate(key, value, pivot);
			else
				AddPrivate(key, value, head_);
		}

		private void AddPrivate(TKey key, TValue value, SortedListNode<TKey, TValue> node)
		{
			do
			{
				node = node.prev_;
			} while (_cmp.Compare(node.item_.Key, key) > 0);
			Insert(node, new SortedListNode<TKey, TValue>(key, value));
		}

		public SortedListNode<TKey, TValue> FindFirstNode(TKey key)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));

			var node = head_;
			do
			{
				node = node.next_;
			} while (_cmp.Compare(node.item_.Key, key) != 0);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> FindFirstNode(TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);

			var node = head_;
			do
			{
				node = node.next_;
			} while (node.item_.Value.Equals(value) == false);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> FindFirstNode(TKey key, TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

			var node = head_;
			do
			{
				node = node.next_;
			} while (_cmp.Compare(node.item_.Key, key) != 0 || node.item_.Value.Equals(value) == false);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> FindLastNode(TKey key)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));

			var node = head_;
			do
			{
				node = node.prev_;
			} while (_cmp.Compare(node.item_.Key, key) != 0);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> FindLastNode(TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);
			
			var node = head_;
			do
			{
				node = node.prev_;
			} while (node.item_.Value.Equals(value) == false);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> FindLastNode(TKey key, TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, value);

			var node = head_;
			do
			{
				node = node.prev_;
			} while (_cmp.Compare(node.item_.Key, key) != 0 || node.item_.Value.Equals(value) == false);

			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> GetNextNode(SortedListNode<TKey, TValue> node)
		{
			node = node.next_;
			return node != head_ ? node : null;
		}

		public SortedListNode<TKey, TValue> GetPrevNode(SortedListNode<TKey, TValue> node)
		{
			node = node.prev_;
			return node != head_ ? node : null;
		}

		private void Insert(SortedListNode<TKey, TValue> prev, SortedListNode<TKey, TValue> newNext)
		{
			newNext.next_ = prev.next_;
			newNext.prev_ = prev;
			prev.next_.prev_ = newNext;
			prev.next_ = newNext;
			_count++;
		}

		public void Remove(SortedListNode<TKey, TValue> node)
		{
			node.prev_.next_ = node.next_;
			node.next_.prev_ = node.prev_;
			_count--;
		}

		public void Remove(TKey key)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, default(TValue));
			
			var node = head_;
			do
			{
				node = node.next_;
			} while (_cmp.Compare(node.item_.Key, key) != 0);

			// 番兵でなければ削除
			if (node != head_)
				Remove(node);
		}

		public void Remove(TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(default(TKey), value);
			
			var node = head_;
			do
			{
				node = node.next_;
			} while (node.item_.Value.Equals(value) == false);

			// 番兵でなければ削除
			if (node != head_)
				Remove(node);
		}

		public void Remove(TKey key, TValue value)
		{
			// 番兵を設置して探索
			head_.item_ = new KeyValuePair<TKey, TValue>(key, value);
			
			var node = head_;
			do
			{
				node = node.next_;
			} while (_cmp.Compare(node.item_.Key, key) != 0 || node.item_.Value.Equals(value) == false);
			
			// 番兵でなければ削除
			if (node != head_)
				Remove(node);
		}

		public SortedListEnumerator<TKey, TValue> GetListEnumerator()
		{
			return new SortedListEnumerator<TKey, TValue>(this, head_);
		}

		#region ICollection<KeyValuePair<TKey,TValue>> メンバ

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> に項目を追加します。
		///</summary>
		///<param name="item"><see cref="TValue:System.Collections.Generic.ICollection`1" /> に追加するオブジェクト。
		///</param>
		///<exception cref="TValue:System.NotSupportedException"><see cref="TValue:System.Collections.Generic.ICollection`1" /> は読み取り専用です。
		///</exception>
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> からすべての項目を削除します。
		///</summary>
		///<exception cref="TValue:System.NotSupportedException"><see cref="TValue:System.Collections.Generic.ICollection`1" /> は読み取り専用です。 
		///</exception>
		public void Clear()
		{
			head_.next_ = head_;
			head_.prev_ = head_;
			_count = 0;
		}

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> に特定の値が格納されているかどうかを判断します。
		///</summary>
		///<returns>
		///<paramref name="item" /> が <see cref="TValue:System.Collections.Generic.ICollection`1" /> に存在する場合は true。それ以外の場合は false。
		///</returns>
		///<param name="item"><see cref="TValue:System.Collections.Generic.ICollection`1" /> 内で検索するオブジェクト。
		///</param>
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return FindFirstNode(item.Key, item.Value) != null;
		}

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> の要素を <see cref="TValue:System.Array" /> にコピーします。<see cref="TValue:System.Array" /> の特定のインデックスからコピーが開始されます。
		///</summary>
		///<param name="array"><see cref="TValue:System.Collections.Generic.ICollection`1" /> から要素がコピーされる 1 次元の <see cref="TValue:System.Array" />。<see cref="TValue:System.Array" /> には、0 から始まるインデックス番号が必要です。
		///</param>
		///<param name="arrayIndex">
		///コピーの開始位置となる、<paramref name="array" /> の 0 から始まるインデックス番号。
		///</param>
		///<exception cref="TValue:System.ArgumentNullException"><paramref name="array" /> が null です。
		///</exception>
		///<exception cref="TValue:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> が 0 未満です。
		///</exception>
		///<exception cref="TValue:System.ArgumentException"><paramref name="array" /> が多次元です。
		///または
		///<paramref name="arrayIndex" /> が array の長さ以上です。
		///または
		///コピー元の <see cref="TValue:System.Collections.Generic.ICollection`1" /> の要素数が、<paramref name="arrayIndex" /> からコピー先の <paramref name="array" /> の末尾までに格納できる数を超えています。
		///または
		///型 <paramref name="TValue" /> をコピー先の <paramref name="array" /> の型に自動的にキャストすることはできません。
		///</exception>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException();
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException();
			if (array.Length > arrayIndex + _count)
				throw new ArgumentException();

			var node = head_.next_;
			while (node != head_)
			{
				array[arrayIndex++] = node.item_;
			}
		}

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> が読み取り専用かどうかを示す値を取得します。
		///</summary>
		///<returns>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> が読み取り専用の場合は true。それ以外の場合は false。
		///</returns>
		public bool IsReadOnly
		{
			get { return false; }
		}

		///<summary>
		///<see cref="TValue:System.Collections.Generic.ICollection`1" /> 内で最初に見つかった特定のオブジェクトを削除します。
		///</summary>
		///<returns>
		///<paramref name="item" /> が <see cref="TValue:System.Collections.Generic.ICollection`1" /> から正常に削除された場合は true。それ以外の場合は false。このメソッドは、<paramref name="item" /> が元の <see cref="TValue:System.Collections.Generic.ICollection`1" /> に見つからない場合にも false を返します。
		///</returns>
		///<param name="item"><see cref="TValue:System.Collections.Generic.ICollection`1" /> から削除するオブジェクト。
		///</param>
		///<exception cref="TValue:System.NotSupportedException"><see cref="TValue:System.Collections.Generic.ICollection`1" /> は読み取り専用です。
		///</exception>
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			var node = FindFirstNode(item.Key, item.Value);
			if (node != null)
			{
				Remove(node);
				return true;
			}
			return false;
		}

		#endregion
	}
}