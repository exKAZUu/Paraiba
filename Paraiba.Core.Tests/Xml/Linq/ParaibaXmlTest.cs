#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
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

using System.Xml.Linq;
using NUnit.Framework;
using Paraiba.Xml.Linq;

namespace Paraiba.Tests.Xml.Linq {
	/// <summary>
	///   Tests for <see cref="ParaibaXml" /> .
	/// </summary>
	[TestFixture]
	public class ParaibaXmlTest {
		[Test]
		public void TestSingleElement() {
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a"), new XElement("a")), Is.True);
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a"), new XElement("b")), Is.False);
		}

		[Test]
		public void TestTwoElements() {
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a", new XElement("b")),
							new XElement("a", new XElement("b"))), Is.True);
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a", new XElement("b")),
							new XElement("b", new XElement("a"))), Is.False);
		}

		[Test]
		public void TestFourElements() {
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement(
									"a", new XElement("b", new XElement("c")), new XElement("d")),
							new XElement(
									"a", new XElement("b", new XElement("c")), new XElement("d"))),
					Is.True);
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a", new XElement("b"), new XElement("d")),
							new XElement(
									"a", new XElement("b", new XElement("c")), new XElement("d"))),
					Is.False);
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement(
									"a", new XElement("b", new XElement("d")), new XElement("c")),
							new XElement(
									"a", new XElement("b", new XElement("c")), new XElement("d"))),
					Is.False);
		}

		[Test]
		public void TestElementsOfDifferentSizes() {
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a"), new XElement("a", new XElement("b"))), Is.False);
			Assert.That(
					ParaibaXml.EqualsElementsAndValues(
							new XElement("a"), new XElement("b", new XElement("a"))), Is.False);
		}
	}
}