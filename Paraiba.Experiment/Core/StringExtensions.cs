using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Paraiba.Text;
using Paraiba.Utility;

namespace Paraiba.Core
{
	public static class StringExtensions
	{
		public static char EndChar(this string str)
		{
			Contract.Requires(str != null);
			Contract.Requires(str.Length > 0);
			return str[str.Length - 1];
		}

		public static string AddIfNotEndsWith(this string str, char end)
		{
			Contract.Requires(str != null);
			if (str.Length == 0 || str.EndChar() != end)
				return str + end;
			return str;
		}

		public static string AddIfNotEndsWith(this string str, string end)
		{
			Contract.Requires(str != null);
			if (str.EndsWith(end))
				return str + end;
			return str;
		}

		public static string JoinString<T>(this IEnumerable<T> strs, string delimiter)
		{
			Contract.Requires(strs != null);
			Contract.Requires(delimiter != null);
			return strs.JoinStringBuilder(delimiter).ToString();
		}

		public static string JoinString<T>(this IEnumerable<T> strs)
		{
			Contract.Requires(strs != null);
			return strs.JoinStringBuilder().ToString();
		}

		public static int ToInt(this string str)
		{
			Contract.Requires(str != null);
			return int.Parse(str);
		}

		public static int ToIntOrDefault(this string str, int defaultValue = default(int))
		{
			Contract.Requires(str != null);
			int value;
			return int.TryParse(str, out value) ? value : defaultValue;
		}

		public static Tuple<string, string> Halve(this string str, char delimiter)
		{
			var index = str.IndexOf(delimiter);
			return index >= 0
				? Tuple.Create(str.Substring(0, index), str.Substring(index + 1))
				: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> Halve(this string str, string delimiter)
		{
			var index = str.IndexOf(delimiter);
			return index >= 0
				? Tuple.Create(str.Substring(0, index), str.Substring(index + delimiter.Length))
				: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> HalveLast(this string str, char delimiter)
		{
			var index = str.LastIndexOf(delimiter);
			return index >= 0
				? Tuple.Create(str.Substring(0, index), str.Substring(index + 1))
				: Tuple.Create(str, string.Empty);
		}

		public static Tuple<string, string> HalveLast(this string str, string delimiter)
		{
			var index = str.LastIndexOf(delimiter);
			return index >= 0
				? Tuple.Create(str.Substring(0, index), str.Substring(index + delimiter.Length))
				: Tuple.Create(str, string.Empty);
		}

		public static string[] HalveToArray(this string str, char delimiter)
		{
			int index = str.IndexOf(delimiter);
			if (index >= 0)
				return new[] { str.Substring(0, index), str.Substring(index + 1) };
			return new[] { str, string.Empty };
		}

		public static string[] HalveToArray(this string str, string delimiter)
		{
			int index = str.IndexOf(delimiter);
			if (index >= 0)
				return new[] { str.Substring(0, index), str.Substring(index + delimiter.Length) };
			return new[] { str, string.Empty };
		}
	}
}