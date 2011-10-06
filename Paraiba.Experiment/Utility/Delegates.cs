using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Utility
{
	public static class Delegates
	{
		public static TResult NOP<TResult>()
		{
			return default(TResult);
		}

		public static TResult NOP<T, TResult>(T arg)
		{
			return default(TResult);
		}

		public static TResult NOP<T1, T2, TResult>(T1 arg1, T2 arg2)
		{
			return default(TResult);
		}

		public static TResult NOP<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3)
		{
			return default(TResult);
		}

		public static TResult NOP<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			return default(TResult);
		}

		public static void NOP()
		{
		}

		public static void NOP<T>(T arg)
		{
		}

		public static void NOP<T1, T2>(T1 arg1, T2 arg2)
		{
		}

		public static void NOP<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
		{
		}

		public static void NOP<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
		}
	}
}