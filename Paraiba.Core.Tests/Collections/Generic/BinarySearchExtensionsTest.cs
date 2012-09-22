#region License

// Copyright (C) 2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using NUnit.Framework;
using Paraiba.Collections.Generic;

namespace Paraiba.Tests.Collections.Generic {
	/// <summary>
	///   Tests for <see cref="BinarySearchExtensions" /> .
	/// </summary>
	[TestFixture]
	public class BinarySearchExtensionsTest {
		[Test]
		public void FindLowerBound() {
			var values = new[] { 1, 1, 3, 3 };
			Assert.That(values.FindLowerBound(4), Is.EqualTo(4));
			Assert.That(values.FindLowerBound(3), Is.EqualTo(2));
			Assert.That(values.FindLowerBound(2), Is.EqualTo(2));
			Assert.That(values.FindLowerBound(1), Is.EqualTo(0));
			Assert.That(values.FindLowerBound(0), Is.EqualTo(0));
		}

		[Test]
		public void FindUpperBound() {
			var values = new[] { 1, 1, 3, 3 };
			Assert.That(values.FindUpperBound(4), Is.EqualTo(3));
			Assert.That(values.FindUpperBound(3), Is.EqualTo(3));
			Assert.That(values.FindUpperBound(2), Is.EqualTo(1));
			Assert.That(values.FindUpperBound(1), Is.EqualTo(1));
			Assert.That(values.FindUpperBound(0), Is.EqualTo(-1));
		}
	}
}