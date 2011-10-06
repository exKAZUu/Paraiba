using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare
{
	public static class CompareUtil
	{
		public static int Compare<T>(T x, T y)
			where T : IComparable<T>
		{
			return x.CompareTo(y);
		}

		public static int Compare<T1, T2>(T1 x1, T2 x2, T1 y1, T2 y2)
			where T1 : IComparable<T1>
			where T2 : IComparable<T2>
		{
			var result = x1.CompareTo(y1);
			return result != 0 ? result : x2.CompareTo(y2);
		}

		public static int Compare<T1, T2, T3>(T1 x1, T2 x2, T3 x3, T1 y1, T2 y2, T3 y3)
			where T1 : IComparable<T1>
			where T2 : IComparable<T2>
			where T3 : IComparable<T3>
		{
			var result = x1.CompareTo(y1);
			return result != 0
				? result : (result = x2.CompareTo(y2)) != 0
				? result : x3.CompareTo(y3);
		}

		public static int Compare<T1, T2, T3, T4>(T1 x1, T2 x2, T3 x3, T4 x4, T1 y1, T2 y2, T3 y3, T4 y4)
			where T1 : IComparable<T1>
			where T2 : IComparable<T2>
			where T3 : IComparable<T3>
			where T4 : IComparable<T4>
		{
			var result = x1.CompareTo(y1);
			return result != 0
				? result : (result = x2.CompareTo(y2)) != 0
				? result : (result = x3.CompareTo(y3)) != 0
				? result : x4.CompareTo(y4);
		}

		public static Comparison<T> Integrate<T>(params Comparison<T>[] cmps)
		{
			Contract.Requires(cmps != null);

			return Integrate(cmps);
		}

		public static Comparison<T> Integrate<T>(IEnumerable<Comparison<T>> cmps)
		{
			Contract.Requires(cmps != null);

			return (x, y) => {
				foreach (var cmp in cmps)
				{
					var result = cmp(x, y);
					if (result != 0)
						return result;
				}
				return 0;
			};
		}

		public static IntegratedComparer<T> Integrate<T>(params IComparer<T>[] cmps)
		{
			Contract.Requires(cmps != null);

			return new IntegratedComparer<T>(cmps);
		}

		public static IntegratedComparer<T> Integrate<T>(IEnumerable<IComparer<T>> cmps)
		{
			Contract.Requires(cmps != null);

			return new IntegratedComparer<T>(cmps);
		}
	}
}


