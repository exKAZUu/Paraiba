using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic.Compare
{
	public class PartialComparer<TSource, TKey> : IComparer<TSource>
		where TKey : IComparable<TKey>
	{
		private readonly Func<TSource, TKey> _selectFunc;

		public Func<TSource, TKey> SelectFunc
		{
			get { return _selectFunc; }
		}

		public PartialComparer(Func<TSource, TKey> selectFunc)
		{
			_selectFunc = selectFunc;
		}

		public int Compare(TSource x, TSource y)
		{
			return _selectFunc(x).CompareTo(_selectFunc(y));
		}
	}
}
