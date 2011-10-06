using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare
{
	public class ComparerWithComparison<T> : IComparer<T>
	{
		private readonly Comparison<T> _comparison;

		public ComparerWithComparison(Comparison<T> comparison)
		{
			Contract.Requires(comparison != null);

			_comparison = comparison;
		}

		#region IComparer<TValue> メンバ

		public int Compare(T x, T y)
		{
			return _comparison(x, y);
		}

		#endregion

		public Comparison<T> Comparison
		{
			get { return _comparison; }
		}
	}
}