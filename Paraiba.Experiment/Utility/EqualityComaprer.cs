using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Utility
{
	public class EqualityComparerWithFunc<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, bool> _equalsFunc;
		private readonly Func<T, int> _getHashFunc;

		public Func<T, T, bool> EqualsFunc
		{
			get { return _equalsFunc; }
		}

		public Func<T, int> GetHashFunc
		{
			get { return _getHashFunc; }
		}

		public EqualityComparerWithFunc(Func<T, T, bool> equalsFunc, Func<T, int> getHashFunc)
		{
			_equalsFunc = equalsFunc;
			_getHashFunc = getHashFunc;
		}

		#region IEqualityComparer<T> メンバ

		public bool Equals(T x, T y)
		{
			return _equalsFunc(x, y);
		}

		public int GetHashCode(T obj)
		{
			return _getHashFunc(obj);
		}

		#endregion
	}
}