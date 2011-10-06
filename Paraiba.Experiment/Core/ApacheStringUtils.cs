using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Core
{
	public static class ApacheStringUtils
	{
		public static string Left(this string str, int length) {
			return str.Substring(0, length);
		}

		public static string Right(this string str, int length) {
			return str.Substring(str.Length - length, length);
		}

		public static string SubstringBefore(this string str, string separator)
		{
			var index = str.IndexOf(separator);
			return index >= 0 ? str.Substring(0, index) : str;
		}

		public static string SubstringAfter(this string str, string separator)
		{
			var index = str.IndexOf(separator);
			return index >= 0 ? str.Substring(index + separator.Length) : string.Empty;
		}

		public static string SubstringBeforeLast(this string str, string separator)
		{
			var index = str.LastIndexOf(separator);
			return index >= 0 ? str.Substring(0, index) : str;
		}

		public static string SubstringAfterLast(this string str, string separator)
		{
			var index = str.LastIndexOf(separator);
			return index >= 0 ? str.Substring(index + separator.Length) : string.Empty;
		}

		public static string StripMargin(this string str)
		{
			return str.Split(new[] {"\r\n"}, StringSplitOptions.None)
				.Select(l => l.TrimStart().Substring(1)).JoinString("\r\n");
		}
	}
}
