using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Text;

namespace Paraiba.Core
{
	public static class CharExtensions
	{
		public static int ToInt(this char ch)
		{
			return ch - '0';
		}

		public static string JoinString(this IEnumerable<char> chars)
		{
			return JoinStringBuilder(chars).ToString();
		}

		public static StringBuilder JoinStringBuilder(this IEnumerable<char> chars)
		{
			var buffer = new StringBuilder();
			return buffer.Appends(chars);
		}

		public static string JoinString(this ICollection<char> chars)
		{
			return JoinStringBuilder(chars).ToString();
		}

		public static StringBuilder JoinStringBuilder(this ICollection<char> chars)
		{
			var buffer = new StringBuilder(chars.Count);
			return buffer.Appends(chars);
		}
	}
}
