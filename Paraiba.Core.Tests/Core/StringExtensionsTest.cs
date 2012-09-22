using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Paraiba.Core;

namespace Paraiba.Tests.Core
{
	[TestFixture]
	public class StringExtensionsTest
	{
		[Test]
		[TestCase("a a a", "a", 0, 5, Result = new[] {0, 2, 4})]
		[TestCase("abcdefg", "h", 0, 7, Result = new int[] {})]
		[TestCase("aa aa aa aa", "aa", 1, 9, Result = new[] {3, 6})]
		public int[] IndexesOf(string text, string value, int startIndex, int count) {
			return text.IndexesOf(value, startIndex, count).ToArray();
		}
	}
}
