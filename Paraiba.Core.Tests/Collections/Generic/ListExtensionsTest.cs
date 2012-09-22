﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Paraiba.Collections.Generic;

namespace Paraiba.Tests.Collections.Generic
{
	[TestFixture]
	public class ListExtensionsTest
	{
		[Test]
		public void Shuffle() {
			var rand1 = new Random(0);
			var rand2 = new Random(1);
			var ints1= Enumerable.Range(0, 1000).ToArray();
			var ints2= Enumerable.Range(0, 1000).ToArray();
			Assert.That(ints1.Shuffle(rand1), Is.Not.EqualTo(ints2.Shuffle(rand2)));
		}

		[Test]
		public void ShuffleUsingSameRandom() {
			var rand1 = new Random(0);
			var rand2 = new Random(0);
			var ints1= Enumerable.Range(0, 1000).ToArray();
			var ints2= Enumerable.Range(0, 1000).ToArray();
			Assert.That(ints1.Shuffle(rand1), Is.EqualTo(ints2.Shuffle(rand2)));
		}
	}
}
