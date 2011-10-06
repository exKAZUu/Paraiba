using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare
{
	public class ReverseComparer<T> : IComparer<T>
	{
		private readonly IComparer<T> _cmp;

		public ReverseComparer(IComparer<T> cmp)
		{
			Contract.Requires(cmp != null);

			_cmp = cmp;
		}

		public IComparer<T> OriginalComparer
		{
			get { return _cmp; }
			//set { _cmp = value; }	// 状態変化できない方が良いかな
		}

		#region IComparer<TValue> Members

		public int Compare(T x, T y)
		{
			return _cmp.Compare(x, y) * -1;
		}

		#endregion
	}
}