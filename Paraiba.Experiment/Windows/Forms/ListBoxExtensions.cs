using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public static class ListBoxExtensions
	{
		public static void SelectAll(this ListBox listBox)
		{
			int count = listBox.Items.Count;
			for (int i = 0; i < count; i++)
				listBox.SetSelected(i, true);
		}

		public static void UnSelectAll(this ListBox listBox)
		{
			int count = listBox.Items.Count;
			for (int i = 0; i < count; i++)
				listBox.SetSelected(i, false);
		}
	}
}