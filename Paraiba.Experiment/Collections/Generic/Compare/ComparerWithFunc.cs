using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare
{
	public class ComparerWithFunc<T> : IComparer<T>
	{
		private readonly Func<T, T, int> _compareFunc;

		public ComparerWithFunc(Func<T, T, int> compareFunc)
		{
			Contract.Requires(compareFunc != null);
			_compareFunc = compareFunc;
		}

		#region IComparer<TValue> メンバ

		public int Compare(T x, T y)
		{
			return _compareFunc(x, y);
		}

		#endregion

		public Func<T, T, int> CompareFunc
		{
			get { return _compareFunc; }
		}
	}
}