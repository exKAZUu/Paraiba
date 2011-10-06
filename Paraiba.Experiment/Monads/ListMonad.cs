using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Linq;

namespace Paraiba.Monads
{
	public static class ListMonad
	{
		public static IEnumerable<T2> Bind<T1, T2>(this IEnumerable<T1> source, Func<T1, IEnumerable<T2>> func)
		{
			return source.SelectMany(func);
		}

		public static IEnumerable<T> Return<T>(T value)
		{
			return value.ToEnumerable();
		}
	}
}
