using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic
{
	public static class LinkedListExtensions
	{
		public static LinkedListNode<T> Find<T>(this LinkedList<T> list, Predicate<T> predicate)
		{
			Contract.Requires(list != null);
			Contract.Requires(predicate != null);

			for (var node = list.First; node != null; node = node.Next)
			{
				if (predicate(node.Value))
				{
					return node;
				}
			}
			return null;
		}

		public static LinkedListNode<T> Find<T>(this LinkedList<T> list, T value, Func<T, T, int> comapreFunc)
		{
			Contract.Requires(list != null);
			Contract.Requires(comapreFunc != null);

			for (var node = list.First; node != null; node = node.Next)
			{
				if (comapreFunc(value, node.Value) == 0)
				{
					return node;
				}
			}
			return null;
		}

		public static LinkedListNode<T> Find<T>(this LinkedList<T> list, T value, Func<T, T, bool> equalFunc)
		{
			Contract.Requires(list != null);
			Contract.Requires(equalFunc != null);

			for (var node = list.First; node != null; node = node.Next)
			{
				if (equalFunc(value, node.Value))
				{
					return node;
				}
			}
			return null;
		}

		public static LinkedListNode<T> Find<T>(this LinkedList<T> list, T value, IComparer<T> cmp)
		{
			Contract.Requires(list != null);
			Contract.Requires(cmp != null);

			for (var node = list.First; node != null; node = node.Next)
			{
				if (cmp.Compare(value, node.Value) == 0)
				{
					return node;
				}
			}
			return null;
		}

		public static LinkedListNode<T> Find<T>(this LinkedList<T> list, T value, IEqualityComparer<T> eqcmp)
		{
			Contract.Requires(list != null);
			Contract.Requires(eqcmp != null);

			for (var node = list.First; node != null; node = node.Next)
			{
				if (eqcmp.Equals(value, node.Value))
				{
					return node;
				}
			}
			return null;
		}

		public static IEnumerable<LinkedListNode<T>> GetPreviousNodes<T>(this LinkedListNode<T> from)
		{
			while (from != null)
			{
				yield return from;
				from = from.Previous;
			}
		}

		public static IEnumerable<LinkedListNode<T>> GetPreviousNodes<T>(this LinkedListNode<T> from, LinkedListNode<T> to)
		{
			while (from != to)
			{
				yield return from;
				from = from.Previous;
			}
			yield return from;
		}

		public static IEnumerable<LinkedListNode<T>> GetNextNodes<T>(this LinkedListNode<T> from)
		{
			while (from != null)
			{
				yield return from;
				from = from.Next;
			}
		}

		public static IEnumerable<LinkedListNode<T>> GetNextNodes<T>(this LinkedListNode<T> from, LinkedListNode<T> to)
		{
			while (from != to)
			{
				yield return from;
				from = from.Next;
			}
			yield return from;
		}
	}
}