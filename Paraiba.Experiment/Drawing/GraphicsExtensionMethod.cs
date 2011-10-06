using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing
{
	public static class GraphicsExtensionMethod
	{
		public static void DrawSurface(this Graphics g, Surface surface, Point p)
		{
			surface.Draw(g, p);
		}

		public static void DrawSurface(this Graphics g, Surface surface, Point2 p)
		{
			surface.Draw(g, p);
		}

		public static void DrawSurface(this Graphics g, Surface surface, int x, int y)
		{
			surface.Draw(g, x, y);
		}
	}
}