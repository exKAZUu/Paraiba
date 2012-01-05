using System;

namespace Paraiba.Wrap {
	public abstract class MutableWrap<T> : Wrap<T>, IMutableWrap<T>
	{
		#region IMutableWrap<TValue> メンバ

		public abstract void Set(T value);

		#endregion

		public static implicit operator MutableWrap<T>(T value)
		{
			return new MutableValueWrap<T>(value);
		}

		public static implicit operator MutableWrap<T>(Func<T> evaluator)
		{
			return new MutableLazyWrap<T>(evaluator);
		}

		public static implicit operator T(MutableWrap<T> wrap)
		{
			return wrap.Value;
		}
	}
}