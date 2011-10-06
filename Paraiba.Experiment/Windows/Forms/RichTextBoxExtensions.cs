using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public static class RichTextBoxExtensions
	{
		public static void Select(this RichTextBox richTextBox, int startLine, int endLine, int startPos, int endPos)
		{
			var text = richTextBox.Text;

			var startLineHeadPos = 0;
			var iLines = 1;
			for (; iLines < startLine; iLines++)
			{
				startLineHeadPos = text.IndexOf('\n', startLineHeadPos) + 1;
			}

			var endLineHeadPos = startLineHeadPos;
			for (; iLines < endLine; iLines++)
			{
				endLineHeadPos = text.IndexOf('\n', endLineHeadPos) + 1;
			}

			var start = startLineHeadPos + startPos;
			var end = endLineHeadPos + endPos;

			richTextBox.Select(start, end - start);
		}
	}
}
