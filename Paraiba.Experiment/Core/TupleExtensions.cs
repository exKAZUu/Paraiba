using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Core
{
	public static class TupleExtensions
	{
		public static bool IsInclude<T>(this Tuple<T, T> tuple, T value)
			where T :IComparable<T>
		{
			return tuple.Item1.CompareTo(value) <= 0 && value.CompareTo(tuple.Item2) < 0;
		}
	}
}
