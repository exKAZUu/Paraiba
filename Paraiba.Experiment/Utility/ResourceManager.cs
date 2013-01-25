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
using System.Diagnostics.Contracts;
using Paraiba.Collections.Generic;

namespace Paraiba.Utility {
	public abstract class ResourceManager<TKey, TResource>
			where TResource : class {
		protected IDictionary<TKey, LinkedListNode<ResourceItem<TResource>>>
				ResourceDict;

		protected LinkedList<ResourceItem<TResource>> ResourceList;

		protected IDictionary<TKey, LinkedListNode<ResourceItem<WeakReference>>>
				WeakResourceDict;

		protected LinkedList<ResourceItem<WeakReference>> WeakResourceList;

		protected ResourceManager()
				: this(EqualityComparer<TKey>.Default) {}

		protected ResourceManager(IComparer<TKey> keyComparer) {
			Contract.Requires(keyComparer != null);

			ResourceList = new LinkedList<ResourceItem<TResource>>();
			WeakResourceList = new LinkedList<ResourceItem<WeakReference>>();
			ResourceDict =
					new SortedDictionary
							<TKey, LinkedListNode<ResourceItem<TResource>>>(
							keyComparer);
			WeakResourceDict =
					new SortedDictionary
							<TKey, LinkedListNode<ResourceItem<WeakReference>>>(
							keyComparer);
		}

		protected ResourceManager(IEqualityComparer<TKey> keyComparer) {
			Contract.Requires(keyComparer != null);

			ResourceList = new LinkedList<ResourceItem<TResource>>();
			WeakResourceList = new LinkedList<ResourceItem<WeakReference>>();
			ResourceDict =
					new Dictionary
							<TKey, LinkedListNode<ResourceItem<TResource>>>(
							keyComparer);
			WeakResourceDict =
					new Dictionary
							<TKey, LinkedListNode<ResourceItem<WeakReference>>>(
							keyComparer);
		}

		/// <summary>
		///   指定したノードが保持するリソースを解放する。 指定したノードはリソースリストから削除して、キャッシュリストに追加して管理する。
		/// </summary>
		/// <param name="node"> </param>
		protected void Release(LinkedListNode<ResourceItem<TResource>> node) {
			Contract.Requires(node != null);
			Debug.Assert(node.List == ResourceList);

			var rc = node.Value.Resource;
			var key = node.Value.Key;
			// キャッシュリストに追加
			var newNode = WeakResourceList.AddFirst(
					new ResourceItem<WeakReference>(
							key,
							new WeakReference(
									new DisposableResource(this, key, rc))));
			WeakResourceDict.Add(key, newNode);
			// リソース解放の通知
			RemovingResource(rc);
			// リソースノードの削除
			ResourceList.Remove(node);
			ResourceDict.Remove(key);
		}

		/// <summary>
		///   指定したノードが保持するリソースを完全に解放する。 指定したノードはリソースリストから削除して、DisposeResource メソッドを呼び出す。
		/// </summary>
		/// <param name="node"> </param>
		protected void ReleaseCompletely(
				LinkedListNode<ResourceItem<TResource>> node) {
			Contract.Requires(node != null);
			Debug.Assert(node.List == ResourceList);

			var rc = node.Value.Resource;
			// リソース解放の通知
			RemovingResource(rc);
			// リソースノードの削除
			Debug.Assert(ResourceDict.ContainsKey(node.Value.Key));
			ResourceList.Remove(node);
			ResourceDict.Remove(node.Value.Key);
			// リソースの解放
			DisposeResource(node.Value.Key, rc);
		}

		/// <summary>
		///   指定したノードが保持するリソースを完全に解放する。 指定したノードはキャッシュリストから削除して、DisposeResource メソッドを呼び出す。
		/// </summary>
		/// <param name="node"> </param>
		protected void ReleaseCompletely(
				LinkedListNode<ResourceItem<WeakReference>> node) {
			Contract.Requires(node != null);
			Debug.Assert(node.List == WeakResourceList);

			// キャッシュリストからの削除なので、RemovingResource は不要
			// キャッシュノードの削除
			Debug.Assert(WeakResourceDict.ContainsKey(node.Value.Key));
			WeakResourceList.Remove(node);
			WeakResourceDict.Remove(node.Value.Key);
			// リソースの解放
			var rc = (DisposableResource)node.Value.Resource.Target;
			if (node.Value.Resource.IsAlive) {
				DisposeResource(node.Value.Key, rc.Resource);
			}
		}

		protected abstract void AddingResource(TResource rc);
		protected abstract void RemovingResource(TResource rc);
		protected abstract void DisposeResource(TKey key, TResource rc);

		/// <summary>
		///   指定したキーと指定した対応するリソースをリソースリストに上書き追加する。
		/// </summary>
		/// <param name="key"> </param>
		/// <param name="rc"> </param>
		/// <returns> </returns>
		protected LinkedListNode<ResourceItem<TResource>> AddNode(
				TKey key, TResource rc) {
			// 既に対応するリソースのノードが存在している場合は完全に解放する
			ReleaseCompletely(key);
			// リソース追加の通知
			AddingResource(rc);
			// リソースを保持するノードを生成して追加する
			var node =
					ResourceList.AddFirst(new ResourceItem<TResource>(key, rc));
			ResourceDict.Add(key, node);
			return node;
		}

		/// <summary>
		///   指定したキーに対応するリソースを保持するノードを取得する。
		/// </summary>
		/// <param name="key"> </param>
		/// <returns> </returns>
		protected LinkedListNode<ResourceItem<TResource>> GetNode(TKey key) {
			// リソースリストから要素を探索
			{
				var node = ResourceDict.GetValueOrDefault(key);
				if (node != null) {
					// 先頭に移動
					var resourceList = ResourceList;
					resourceList.Remove(node);
					resourceList.AddFirst(node);
					return node;
				}
			}

			// キャッシュリストから要素を探索
			{
				var node = WeakResourceDict.GetValueOrDefault(key);
				if (node != null) {
					// キャッシュリストから取り除く
					WeakResourceList.Remove(node);
					WeakResourceDict.Remove(key);

					var rc = (DisposableResource)node.Value.Resource.Target;
					if (node.Value.Resource.IsAlive) {
						// リソースが残っていれば、リソースリストの先頭に移動
						AddingResource(rc.Resource);
						var newNode =
								ResourceList.AddFirst(
										new ResourceItem<TResource>(
												key, rc.Resource));
						ResourceDict.Add(key, newNode);
						return newNode;
					}
				}
			}

			return null;
		}

		/// <summary>
		///   全てのリソースを解放する
		/// </summary>
		public void Clear() {
			var resourceList = ResourceList;
			var weakResourceList = WeakResourceList;
			var weakResourceDict = WeakResourceDict;

			foreach (var item in resourceList) {
				var key = item.Key;
				var rc = item.Resource;
				// リソース解放の通知
				RemovingResource(rc);
				// キャッシュリストに追加
				var node =
						weakResourceList.AddFirst(
								new ResourceItem<WeakReference>
										(
										key,
										new WeakReference(
												new DisposableResource(
														this, key, rc))));
				weakResourceDict.Add(key, node);
			}
			// リソースリストから削除
			resourceList.Clear();
			ResourceDict.Clear();
		}

		/// <summary>
		///   全てのキャッシュ（WeakReference で管理された）リソースを解放する
		/// </summary>
		public void ClearCache() {
			var list = WeakResourceList;
			foreach (var item in list) {
				var rc = (DisposableResource)item.Resource.Target;
				if (item.Resource.IsAlive) {
					DisposeResource(item.Key, rc.Resource);
				}
			}
			// キャッシュリストから削除
			list.Clear();
			WeakResourceDict.Clear();
		}

		/// <summary>
		///   不要なキャッシュを削除する
		/// </summary>
		public void ClearDisposedCache() {
			var weakResourceList = WeakResourceList;
			var weakResourceDict = WeakResourceDict;

			// キャッシュリストから不要な要素を探索
			var node = weakResourceList.First;
			while (node != null) {
				var tmp = node;
				node = node.Next;
				// リソースが消えていたら、キャッシュリストからノードを削除
				if (!tmp.Value.Resource.IsAlive) {
					weakResourceList.Remove(tmp);
					weakResourceDict.Remove(tmp.Value.Key);
				}
			}
		}

		/// <summary>
		///   全てのリソースを完全に解放する
		/// </summary>
		public void ClearCompletely() {
			// リソースリストの全要素の強制的な解放を試みる
			{
				var list = ResourceList;
				foreach (var item in list) {
					var rc = item.Resource;
					RemovingResource(rc);
					DisposeResource(item.Key, rc);
				}
				list.Clear();
				ResourceDict.Clear();
			}

			// キャッシュリストの全要素の強制的な解放を試みる
			ClearCache();
		}

		/// <summary>
		///   最も利用されていないリソースを解放する
		/// </summary>
		public void Release() {
			var node = ResourceList.Last;
			if (node != null) {
				Release(node);
			}
		}

		/// <summary>
		///   指定したキーに対応するリソースを解放する
		/// </summary>
		/// <param name="key"> </param>
		/// <returns> </returns>
		public bool Release(TKey key) {
			// リソースリストから探索して、存在すればキャッシュリストに移動
			var node = ResourceDict.GetValueOrDefault(key);
			if (node != null) {
				Release(node);
				return true;
			}
			return false;
		}

		/// <summary>
		///   最も利用されていないリソースを完全に解放する
		/// </summary>
		public void ReleaseCompletely() {
			var node = ResourceList.Last;
			if (node != null) {
				ReleaseCompletely(node);
			}
		}

		/// <summary>
		///   指定したキーに対応するリソースを完全に解放する
		/// </summary>
		/// <param name="key"> </param>
		/// <returns> </returns>
		public bool ReleaseCompletely(TKey key) {
			// リソースリストから探索して、存在すれば強制的な解放を試みる
			{
				var node = ResourceDict.GetValueOrDefault(key);
				if (node != null) {
					ReleaseCompletely(node);
					return true;
				}
			}

			// キャッシュリストから探索して、存在すれば強制的な解放を試みる
			{
				var node = WeakResourceDict.GetValueOrDefault(key);
				if (node != null) {
					ReleaseCompletely(node);
					return true;
				}
			}
			return false;
		}

		#region Nested type: DisposableResource

		/// <summary>
		///   WeakReference で管理するために IDisposable を実装した リソースとリソースのキーの組合せを表します。
		/// </summary>
		protected class DisposableResource : IDisposable {
			private readonly TKey _key;
			private readonly ResourceManager<TKey, TResource> _man;
			internal TResource Resource;

			internal DisposableResource(
					ResourceManager<TKey, TResource> man, TKey key,
					TResource resource) {
				_man = man;
				_key = key;
				Resource = resource;
			}

			#region IDisposable メンバ

			public void Dispose() {
				_man.DisposeResource(_key, Resource);
			}

			#endregion
		}

		#endregion

		#region Nested type: ResourceItem

		/// <summary>
		///   リソースとリソースのキーの組合せを表します。
		/// </summary>
		/// <typeparam name="T"> </typeparam>
		protected class ResourceItem<T> {
			internal TKey Key;
			internal T Resource;

			internal ResourceItem(TKey key, T resource) {
				Key = key;
				Resource = resource;
			}

			internal ResourceItem(TKey key)
					: this(key, default(T)) {}
		}

		#endregion
	}
}