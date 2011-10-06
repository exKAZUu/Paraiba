using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites
{
	public class CustomCoordAnimationSprite : AnimationSprite
	{
		private readonly AnimationSprite _animationSprite;
		private readonly Func<Point2, Size, Point2> _coordinateTransformer;

		public CustomCoordAnimationSprite(AnimationSprite animationSprite, Func<Point2, Size, Point2> coordinateTransformer)
		{
			Contract.Requires(animationSprite != null);
			Contract.Requires(coordinateTransformer != null);

			_animationSprite = animationSprite;
			_coordinateTransformer = coordinateTransformer;
		}

		#region Sprite メンバ

		public override Surface CurrentSurface
		{
			get { return _animationSprite.CurrentSurface; }
		}

		public override Point2 Location
		{
			get { return _animationSprite.Location; }
			set { _animationSprite.Location = value; }
		}

		public override Size Size
		{
			get { return _animationSprite.Size; }
		}

		public override void Draw(Graphics g)
		{
			_animationSprite.Draw(g, _coordinateTransformer);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_animationSprite.Draw(g, offset, _coordinateTransformer);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			// this._coordinateTransformer は主に描画開始位置の調整で利用
			// _coordinateTransformer は一般的な座標変換で利用
			// 最後に生成した CustomCoordSprite が最初に適用される
			_animationSprite.Draw(g, (p, size) => _coordinateTransformer(coordinateTransformer(p, size), size));
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			// this._coordinateTransformer は主に描画開始位置の調整で利用
			// _coordinateTransformer は一般的な座標変換で利用
			// 最後に生成した CustomCoordSprite が最初に適用される
			_animationSprite.Draw(g, offset, (p, size) => _coordinateTransformer(coordinateTransformer(p, size), size));
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return _animationSprite.Ended; }
		}

		public override float ExcessTime
		{
			get { return _animationSprite.ExcessTime; }
		}

		public override bool Elapse(float time)
		{
			return _animationSprite.Elapse(time);
		}

		public override void Reset()
		{
			_animationSprite.Reset();
		}

		#endregion
	}
}