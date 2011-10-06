using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public static class CheckedListBoxExtensions
	{
		public static IEnumerable<int> AsEnumerableInt(this CheckedListBox.CheckedIndexCollection collection)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				yield return collection[i];
			}
		}
	}
}
