using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public static class ListViewExtensions
	{
		public static void SelectAll(this ListView listView)
		{
			foreach (ListViewItem item in listView.Items)
			{
				item.Selected = true;
			}
		}

		public static void UnSelectAll(this ListView listView)
		{
			foreach (ListViewItem item in listView.Items)
			{
				item.Selected = false;
			}
		}
	}
}