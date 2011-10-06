using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Paraiba.Linq;

namespace Paraiba.Xml.Linq
{
	public static class XElementExtensions2
	{
		public static XElement AsPreviousElement(this XElement element)
		{
			return (XElement)element.PreviousNode;
		}

		public static XElement AsNextElement(this XElement element)
		{
			return (XElement)element.NextNode;
		}
		public static XElement AsFirstElement(this XElement element)
		{
			return (XElement)element.FirstNode;
		}

		public static XElement AsLastElement(this XElement element)
		{
			return (XElement)element.LastNode;
		}

		public static IEnumerable<XElement> AsElements(this XElement element)
		{
			var node = element.FirstNode;
			while (node != null) {
				yield return (XElement)node;
				node = node.NextNode;
			}
		}

		public static XElement AsElementAt(this XElement element, int index)
		{
			Contract.Requires(0 <= index);

			var node = element.FirstNode;
			while (--index >= 0) {
				node = node.NextNode;
			}
			return (XElement)node;
		}

		public static XElement AsElementAtOrDefault(this XElement element, int index)
		{
			if (index < 0)
				return null;

			var node = element.FirstNode;
			while (--index >= 0) {
				if (node == null)
					return null;
				node = node.NextNode;
			}
			return (XElement)node;
		}

		public static IEnumerable<XElement> ParentsAndSelf(this XElement target)
		{
			yield return target;
			var node = target.Parent;
			while (node != null) {
				yield return node;
				node = node.Parent;
			}
		}

		public static IEnumerable<XElement> Parents(this XObject target)
		{
			var node = target.Parent;
			while (node != null)
			{
				yield return node;
				node = node.Parent;
			}
		}

		public static IEnumerable<XElement> ParentsWhile(this XObject xobj, XObject endParent)
		{
			return xobj.Parents().TakeWhile(e => !ReferenceEquals(e, endParent));
		}

		public static XElement ElementAt(this XElement element, int index)
		{
			return element.Elements().ElementAt(index);
		}

		public static XElement ElementAtOrDefault(this XElement element, int index)
		{
			return element.Elements().ElementAtOrDefault(index);
		}

		public static IEnumerable<XElement> Independents(this IEnumerable<XElement> elements)
		{
			return elements.ToList().Independents();
		}

		public static IEnumerable<XElement> Independents(this IList<XElement> elementList)
		{
			return elementList.Where(e => !e.Descendants().IsIntersect(elementList));
		}

		public static bool Contains(this XElement parent, XElement target)
		{
			return target.ParentsAndSelf().Any(e => e == parent);
		}

		public static int Depth(this XObject xobj)
		{
			var node = xobj.Parent;
			var depth = 0;
			while (node != null) {
				node = node.Parent;
				depth++;
			}
			return depth;
		}
	}
}