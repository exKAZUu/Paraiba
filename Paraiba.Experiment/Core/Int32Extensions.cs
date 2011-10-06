using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Core
{
	public static class Int32Extensions
	{
		public static char ToChar(this int i)
		{
			return (char)(i + '0');
		}

		public static void Times(this int n, Action action)
		{
			for (int i = 0; i < n; i++)
			{
				action();
			}
		}

		public static void Times(this int n, Action<int> action)
		{
			for (int i = 0; i < n; i++)
			{
				action(i);
			}
		}

		public static void UpTo(this int n, int max, Action action)
		{
			while(n <= max)
			{
				action();
				n++;
			}
		}

		public static void UpTo(this int n, int max, Action<int> action)
		{
			while (n <= max)
			{
				action(n);
				n++;
			}
		}

		public static void DownTo(this int n, int min, Action action)
		{
			while (n >= min)
			{
				action();
				n--;
			}
		}

		public static void DownTo(this int n, int min, Action<int> action)
		{
			while (n >= min)
			{
				action(n);
				n--;
			}
		}

		public static void Step(this int n, int limit, int step, Action action)
		{
			while (n <= limit)
			{
				action();
				n += step;
			}
		}

		public static void Step(this int n, int limit, int step, Action<int> action)
		{
			while (n <= limit)
			{
				action(n);
				n += step;
			}
		}
	}
}