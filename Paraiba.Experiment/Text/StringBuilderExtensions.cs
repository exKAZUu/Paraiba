using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Text
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder Appends<T>(this StringBuilder builder, IEnumerable<T> values)
		{
			foreach (var value in values)
			{
				builder.Append(value);
			}
			return builder;
		}

		public static StringBuilder AppendLines<T>(this StringBuilder builder, IEnumerable<T> values)
		{
			foreach (var value in values)
			{
				builder.AppendLine(value.ToString());
			}
			return builder;
		}

		public static StringBuilder JoinStringBuilder<T>(this IEnumerable<T> strs)
		{
			using (var enumerator = strs.GetEnumerator()) {
				if (enumerator.MoveNext()) {
					var builder = new StringBuilder(enumerator.Current.ToString());
					while (enumerator.MoveNext())
						builder.Append(enumerator.Current.ToString());
					return builder;
				}
			}
			return new StringBuilder();
		}

		public static StringBuilder JoinStringBuilder<T>(this IEnumerable<T> strs, string delimiter)
		{
			using (var enumerator = strs.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					var builder = new StringBuilder(enumerator.Current.ToString());
					while (enumerator.MoveNext()) {
						builder.Append(delimiter);
						builder.Append(enumerator.Current.ToString());
					}
					return builder;
				}
			}
			return new StringBuilder();
		}
	}
}
