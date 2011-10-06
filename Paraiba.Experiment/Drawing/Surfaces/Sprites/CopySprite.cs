using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites
{
	public class CopySprite : Sprite
	{
		private readonly IEnumerable<Vector2> _offsetVectors;
		private readonly Sprite _sprite;

		public CopySprite(Sprite originalSprite, IEnumerable<Vector2> offsetVectors)
		{
			Contract.Requires(originalSprite != null);
			Contract.Requires(offsetVectors != null);

			_sprite = originalSprite;
			_offsetVectors = offsetVectors;
		}

		#region Sprite メンバ

		public override Surface CurrentSurface
		{
			get { return _sprite.CurrentSurface; }
		}

		public override Point2 Location
		{
			get { return _sprite.Location; }
			set { _sprite.Location = value; }
		}

		public override Size Size
		{
			get { return _sprite.Size; }
		}

		public override void Draw(Graphics g)
		{
			_sprite.Draw(g);
			foreach (var v in _offsetVectors)
				_sprite.Draw(g, v);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_sprite.Draw(g, offset);
			foreach (var v in _offsetVectors)
				_sprite.Draw(g, offset + v);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_sprite.Draw(g, coordinateTransformer);
			foreach (var v in _offsetVectors)
				_sprite.Draw(g, v, coordinateTransformer);
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_sprite.Draw(g, offset, coordinateTransformer);
			foreach (var v in _offsetVectors)
				_sprite.Draw(g, offset + v, coordinateTransformer);
		}

		#endregion
	}
}