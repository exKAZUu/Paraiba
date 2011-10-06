using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare
{
	public static class ComparerExtensions
	{
		public static IComparer<T> ToReverse<T>(this ReverseComparer<T> comparer)
		{
			Contract.Requires(comparer != null);

			return comparer.OriginalComparer;
		}

		public static ReverseComparer<T> ToReverse<T>(this IComparer<T> comparer)
		{
			Contract.Requires(comparer != null);

			return new ReverseComparer<T>(comparer);
		}

		public static NonGenericComparer<T> ToNonGeneric<T>(this IComparer<T> comparer)
		{
			Contract.Requires(comparer != null);

			return new NonGenericComparer<T>(comparer);
		}

		public static ComparerWithComparison<T> ToComparer<T>(this Comparison<T> compareFunc)
		{
			Contract.Requires(compareFunc != null);

			return new ComparerWithComparison<T>(compareFunc);
		}

		public static ComparerWithFunc<T> ToComparer<T>(this Func<T, T, int> compareFunc)
		{
			Contract.Requires(compareFunc != null);

			return new ComparerWithFunc<T>(compareFunc);
		}

		public static PartialComparer<TSource, TKey> ToComparer<TSource, TKey>(this Func<TSource, TKey> selector)
			where TKey : IComparable<TKey>
		{
			Contract.Requires(selector != null);

			return new PartialComparer<TSource, TKey>(selector);
		}
	}
}