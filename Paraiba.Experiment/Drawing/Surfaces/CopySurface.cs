using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces
{
	/// <summary>
	/// １つのスプライトを指定した差分位置すべてに同じ内容で表示するスプライトを表します。
	/// </summary>
	public class CopySurface : Surface
	{
		private readonly Surface _surface;
		private readonly IEnumerable<Vector2> _offsetVectors;

		public CopySurface(Surface originalSurface, IEnumerable<Vector2> offsetVectors)
		{
			Contract.Requires(originalSurface != null);
			Contract.Requires(offsetVectors != null);

			_surface = originalSurface;
			_offsetVectors = offsetVectors;
		}

		public override Size Size
		{
			get { return _surface.Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			_surface.Draw(g, x, y);
			foreach (Vector2 v in _offsetVectors)
			{
				_surface.Draw(g, x + v.X, y + v.Y);
			}
		}

		public override Image GetImage()
		{
			return _surface.GetImage();
		}
	}
}