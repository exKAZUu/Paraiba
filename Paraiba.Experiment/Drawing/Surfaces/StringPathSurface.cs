using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paraiba.Drawing.Surfaces
{
	public class StringPathSurface : Surface
	{
		private readonly Brush _brush;
		private readonly GraphicsPath _path;
		private Matrix _matrix;
		private Size _size;
		private int _lastX, _lastY;

		public StringPathSurface(String str, Font font, Brush brush)
			: this(str, font, brush, null)
		{
		}

		public StringPathSurface(String str, Font font, Brush brush, StringFormat stringFormat)
		{
			Contract.Requires(str != null);
			Contract.Requires(font != null);
			Contract.Requires(brush != null);

			_brush = brush;

			// グラフィックスパスの生成
			_path = new GraphicsPath();
			_path.AddString(str, font.FontFamily, (int)font.Style, font.Size, new Point(0, 0), stringFormat);
			// サイズを取得する
			var rect = _path.GetBounds();
			_size = rect.Size.ToSize();
			// 描画時にマージンがなくなるように平行移動
			var matrix = new Matrix(1, 0, 0, 1, -rect.Left, -rect.Top);
			_path.Transform(matrix);
			// 描画位置を(0, 0)で記憶
			matrix.Reset();
			_matrix = matrix;
		}

		public override Size Size
		{
			get { return _size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			var matrix = _matrix;
			// 前回の描画位置との差分を計算
			//matrix = new Matrix(1, 0, 0, 1, x - _lastX, y - _lastY);
			matrix.Translate((x - _lastX) - matrix.OffsetX, (y - _lastY) - matrix.OffsetY);
			_lastX = x; _lastY = y;
			// 計算した差分だけ平行移動
			_path.Transform(matrix);
			_matrix = matrix;

			var oldMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.HighQuality;

			g.FillPath(_brush, _path);

			g.SmoothingMode = oldMode;
		}
	}
}