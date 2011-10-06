using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces
{
	public class CustomCoordSurface : Surface
	{
		private readonly Surface _surface;
		private readonly Func<Point2, Size, Point2> _coordinateTransformer;

		public CustomCoordSurface(Surface surface, Func<Point2, Size, Point2> coordinateTransformer)
		{
			Contract.Requires(surface != null);
			Contract.Requires(coordinateTransformer != null);

			_surface = surface;
			_coordinateTransformer = coordinateTransformer;
		}

		public override Size Size
		{
			get { return _surface.Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			// 最後に生成した CustomCoordSurface が最初に適用される
			var p = _coordinateTransformer(new Point2(x, y), _surface.Size);
			_surface.Draw(g, p.X, p.Y);
		}

		public override Image GetImage()
		{
			return _surface.GetImage();
		}
	}
}