using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites
{
	/// <summary>
	/// 指定したスプライトを利用して配置可能でかつ表示可能なオブジェクトを表します。
	/// </summary>
	public class SurfaceSprite : Sprite
	{
		private readonly Surface _surface;
		private Point2 _location;

		public SurfaceSprite(Surface surface, Point2 location)
		{
			Contract.Requires(surface != null);

			_surface = surface;
			this._location = location;
		}

		public override Surface CurrentSurface
		{
			get { return _surface; }
		}

		public override Size Size
		{
			get { return _surface.Size; }
		}

		public override Point2 Location
		{
			get { return _location; }
			set { _location = value; }
		}

		public override void Draw(Graphics g)
		{
			_surface.Draw(g, _location);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_surface.Draw(g, _location + offset);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_surface.Draw(g, coordinateTransformer(_location, _surface.Size));
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_surface.Draw(g, coordinateTransformer(_location + offset, _surface.Size));
		}
	}
}