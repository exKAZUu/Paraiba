using System;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace Paraiba.Drawing.Surfaces
{
	/// <summary>
	/// 表示可能な文字列のオブジェクトを表します。
	/// </summary>
	public class StringSurface : Surface
	{
		private readonly Brush _brush;
		private readonly Font _font;
		private readonly String _str;
		private readonly StringFormat _stringFormat;
		private Size _size;

		public StringSurface(Graphics g, String str, Font font, Brush brush)
			: this(g, str, font, brush, null)
		{
		}

		public StringSurface(Graphics g, String str, Font font, Brush brush, StringFormat stringFormat)
		{
			Contract.Requires(str != null);
			Contract.Requires(font != null);
			Contract.Requires(brush != null);

			_str = str;
			_font = font;
			_brush = brush;
			_stringFormat = stringFormat;
			_size = g.MeasureString(_str, _font).ToSize();
		}

		public override Size Size
		{
			get { return _size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			g.DrawString(_str, _font, _brush, x, y, _stringFormat);
		}
	}
}