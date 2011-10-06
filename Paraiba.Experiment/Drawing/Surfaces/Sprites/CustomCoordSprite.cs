using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites
{
	public class CustomCoordSprite : Sprite
	{
		private readonly Sprite _sprite;
		private readonly Func<Point2, Size, Point2> _coordinateTransformer;

		public CustomCoordSprite(Sprite sprite, Func<Point2, Size, Point2> coordinateTransformer)
		{
			Contract.Requires(sprite != null);
			Contract.Requires(coordinateTransformer != null);

			_sprite = sprite;
			_coordinateTransformer = coordinateTransformer;
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
			_sprite.Draw(g, _coordinateTransformer);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_sprite.Draw(g, offset, _coordinateTransformer);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			// this._coordinateTransformer は主に描画開始位置の調整で利用
			// _coordinateTransformer は一般的な座標変換で利用
			// 最後に生成した CustomCoordSprite が最初に適用される
			_sprite.Draw(g, (p, size) => _coordinateTransformer(coordinateTransformer(p, size), size));
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			// this._coordinateTransformer は主に描画開始位置の調整で利用
			// _coordinateTransformer は一般的な座標変換で利用
			// 最後に生成した CustomCoordSprite が最初に適用される
			_sprite.Draw(g, offset, (p, size) => _coordinateTransformer(coordinateTransformer(p, size), size));
		}

		#endregion
	}
}