using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Utility
{
	public interface IDeepCloneable<T>
	{
		T DeepClone(CloneMaker maker);
	}

	/// <summary>
	/// 回数を最小に抑えた DeepClone, Clone によるコピー機能を提供します。
	/// </summary>
	public class CloneMaker
	{
		private readonly Dictionary<object, object> dic;

		public CloneMaker()
		{
			dic = new Dictionary<object, object>(new ReferenceEqualityComparer<object>());
		}

		/// <summary>
		/// 指定したオブジェクトのコピーを取得します。
		/// 既にコピーが存在する場合はそれを、そうでなければ DeepClone メソッドで新たにコピーを生成して記憶します。
		/// </summary>
		/// <param name="obj">クローンするオブジェクト</param>
		/// <returns>オブジェクトのコピー</returns>
		public T GetDeepClone<T>(T obj)
			where T : class, IDeepCloneable<T>
		{
			object result;
			if (dic.TryGetValue(obj, out result))
				return (T)result;

			var clone = obj.DeepClone(this);
			dic.Add(obj, clone);
			return clone;
		}

		/// <summary>
		/// 指定したオブジェクトのコピーを取得します。
		/// 既にコピーが存在する場合はそれを、そうでなければ Clone メソッドで新たにコピーを生成して記憶します。
		/// </summary>
		/// <param name="obj">クローンするオブジェクト</param>
		/// <returns>オブジェクトのコピー</returns>
		public T GetClone<T>(T obj)
			where T : class, ICloneable
		{
			Contract.Requires(!(obj is IDeepCloneable<T>));

			object result;
			if (dic.TryGetValue(obj, out result))
				return (T)result;

			var clone = obj.Clone();
			dic.Add(obj, clone);
			return (T)clone;
		}
	}

	/// <summary>
	/// Object.ReferenceEquals を用いて、オブジェクトが同一かどうか比較するメソッドを定義します。
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public class ReferenceEqualityComparer<T> : IEqualityComparer<T>
	{
		#region IEqualityComparer<TValue> メンバ

		public bool Equals(T x, T y)
		{
			return ReferenceEquals(x, y);
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}

		#endregion
	}
}