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

namespace Paraiba.Utility {
	public interface IDeepCloneable<T> {
		T DeepClone(CloneMaker maker);
	}

	/// <summary>
	///   回数を最小に抑えた DeepClone, Clone によるコピー機能を提供します。
	/// </summary>
	public class CloneMaker {
		private readonly Dictionary<object, object> dic;

		public CloneMaker() {
			dic =
					new Dictionary<object, object>(
							new ReferenceEqualityComparer<object>());
		}

		/// <summary>
		///   指定したオブジェクトのコピーを取得します。 既にコピーが存在する場合はそれを、そうでなければ DeepClone メソッドで新たにコピーを生成して記憶します。
		/// </summary>
		/// <param name="obj"> クローンするオブジェクト </param>
		/// <returns> オブジェクトのコピー </returns>
		public T GetDeepClone<T>(T obj)
				where T : class, IDeepCloneable<T> {
			object result;
			if (dic.TryGetValue(obj, out result)) {
				return (T)result;
			}

			var clone = obj.DeepClone(this);
			dic.Add(obj, clone);
			return clone;
		}

		/// <summary>
		///   指定したオブジェクトのコピーを取得します。 既にコピーが存在する場合はそれを、そうでなければ Clone メソッドで新たにコピーを生成して記憶します。
		/// </summary>
		/// <param name="obj"> クローンするオブジェクト </param>
		/// <returns> オブジェクトのコピー </returns>
		public T GetClone<T>(T obj)
				where T : class, ICloneable {
			Contract.Requires(!(obj is IDeepCloneable<T>));

			object result;
			if (dic.TryGetValue(obj, out result)) {
				return (T)result;
			}

			var clone = obj.Clone();
			dic.Add(obj, clone);
			return (T)clone;
		}
	}

	/// <summary>
	///   Object.ReferenceEquals を用いて、オブジェクトが同一かどうか比較するメソッドを定義します。
	/// </summary>
	/// <typeparam name="TValue"> </typeparam>
	public class ReferenceEqualityComparer<T> : IEqualityComparer<T> {
		#region IEqualityComparer<TValue> メンバ

		public bool Equals(T x, T y) {
			return ReferenceEquals(x, y);
		}

		public int GetHashCode(T obj) {
			return obj.GetHashCode();
		}

		#endregion
	}
}