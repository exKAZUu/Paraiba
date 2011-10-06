using System;
using System.Diagnostics.Contracts;

namespace Paraiba.Utility
{
	// 反変性のためインタフェースが必要
	public abstract class Wrap<T> : IWrap<T>
	{
		#region IWrap<TValue> Members

		public abstract T Value { get; }

		#endregion

		// TValue -> Wrap<TValue> への暗黙な変換はバグの原因になる可能性大？
		public static implicit operator Wrap<T>(T value)
		{
			return new ValueWrap<T>(value);
		}

		public static implicit operator Wrap<T>(Func<T> evaluator)
		{
			return new LazyWrap<T>(evaluator);
		}

		public static implicit operator T(Wrap<T> wrap)
		{
			return wrap.Value;
		}
	}
}