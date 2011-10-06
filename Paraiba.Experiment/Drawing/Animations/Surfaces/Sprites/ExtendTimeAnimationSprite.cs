using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites
{
	/// <summary>
	/// 前後に静止している期間を設けることで指定したアニメーションオブジェクトの時間を延長したものを表す
	/// </summary>
	public class ExtendTimeAnimationSprite : AnimationSprite
	{
		private readonly AnimationSprite _animationSprite;
		private readonly float _backSpanTime;
		private readonly float _frontSpanTime;
		private float _lapseFrontSpanTime;

		public ExtendTimeAnimationSprite(AnimationSprite animationSprite, float backSpanTime)
			: this(animationSprite, 0, backSpanTime)
		{
		}

		public ExtendTimeAnimationSprite(AnimationSprite animationSprite, float frontSpanTime, float backSpanTime)
		{
			Contract.Requires(animationSprite != null);

			_animationSprite = animationSprite;
			_frontSpanTime = frontSpanTime;
			_backSpanTime = backSpanTime;
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
			_animationSprite.Draw(g);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_animationSprite.Draw(g, offset);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_animationSprite.Draw(g, coordinateTransformer);
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_animationSprite.Draw(g, offset, coordinateTransformer);
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return _lapseFrontSpanTime + _animationSprite.ExcessTime >= _frontSpanTime + _backSpanTime; }
		}

		public override float ExcessTime
		{
			get { return _lapseFrontSpanTime + _animationSprite.ExcessTime - (_frontSpanTime + _backSpanTime); }
		}

		public override bool Elapse(float time)
		{
			if (_frontSpanTime != _lapseFrontSpanTime)
			{
				_lapseFrontSpanTime += time;
				if (_lapseFrontSpanTime < _frontSpanTime)
					return false;
				time = _lapseFrontSpanTime - _frontSpanTime;
				_lapseFrontSpanTime = _frontSpanTime;
			}
			return _animationSprite.Elapse(time);
		}

		public override void Reset()
		{
			_lapseFrontSpanTime = 0;
			_animationSprite.Reset();
		}

		#endregion
	}
}