using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Paraiba.Xml.Linq
{
	public static class XNodeExtension
	{
		public static IEnumerable<XNode> PreviousNodes(this XNode node)
		{
			node = node.PreviousNode;
			while (node != null) {
				yield return node;
				node = node.PreviousNode;
			}
		}

		public static IEnumerable<XNode> NextNodes(this XNode node)
		{
			node = node.NextNode;
			while (node != null) {
				yield return node;
				node = node.NextNode;
			}
		}

		public static IEnumerable<XNode> PreviousNodesAndSelf(this XNode node)
		{
			do {
				yield return node;
				node = node.PreviousNode;
			} while (node != null);
		}

		public static IEnumerable<XNode> NextNodesAndSelf(this XNode node)
		{
			do {
				yield return node;
				node = node.NextNode;
			} while (node != null);
		}
	}
}
