using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic.Compare
{
	public class NonGenericComparer<T> : IComparer, IComparer<T>
	{
		private readonly IComparer<T> _comparer;

		public IComparer<T> Comparer
		{
			get { return _comparer; }
		}

		public NonGenericComparer(IComparer<T> comparer)
		{
			_comparer = comparer;
		}

		public int Compare(object x, object y)
		{
			if (x is T)
			{
				return y is T ? _comparer.Compare((T)x, (T)y) : -1;
			}
			return y is T ? 1 : (x.Equals(y) ? 0 : -1);
		}

		public int Compare(T x, T y)
		{
			return _comparer.Compare(x, y);
		}
	}
}
