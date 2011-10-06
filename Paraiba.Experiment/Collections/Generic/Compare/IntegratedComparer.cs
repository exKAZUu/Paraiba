using System.Collections.Generic;

namespace Paraiba.Collections.Generic.Compare
{
	public class IntegratedComparer<T> : IComparer<T>
	{
		private readonly IEnumerable<IComparer<T>> _cmps;

		public IntegratedComparer(IEnumerable<IComparer<T>> cmps)
		{
			_cmps = cmps;
		}

		public int Compare(T x, T y)
		{
			foreach (var cmp in _cmps)
			{
				var result = cmp.Compare(x, y);
				if (result != 0)
					return result;
			}
			return 0;
		}
	}
}