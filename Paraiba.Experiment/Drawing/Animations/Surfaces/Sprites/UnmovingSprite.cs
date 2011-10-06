using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Drawing.Surfaces.Sprites;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites
{
	/// <summary>
	/// 時間経過によって位置が変化しない配置可能でかつ表示可能なアニメーションオブジェクトを表します。
	/// </summary>
	public class UnmovingSprite : AnimationSprite
	{
		private readonly AnimationSurface _animationSurface;
		private Point2 _location;

		public UnmovingSprite(AnimationSurface animationSurface, Point2 location)
		{
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
			_location = location;
		}

		#region AnimationSprite Members

		public override Surface CurrentSurface
		{
			get { return _animationSurface; }
		}

		public override Size Size
		{
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g)
		{
			_animationSurface.Draw(g, _location);
		}

		public override void Draw(Graphics g, Vector2 offset)
		{
			_animationSurface.Draw(g, _location + offset);
		}

		public override void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_animationSurface.Draw(g, coordinateTransformer(_location, _animationSurface.Size));
		}

		public override void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer)
		{
			_animationSurface.Draw(g, coordinateTransformer(_location + offset, _animationSurface.Size));
		}

		#endregion

		#region IAnimation メンバ

		public override float ExcessTime
		{
			get { return _animationSurface.ExcessTime; }
		}

		public override bool Ended
		{
			get { return _animationSurface.Ended; }
		}

		public override Point2 Location
		{
			get { return _location; }
			set { _location = value; }
		}

		public override bool Elapse(float time)
		{
			return _animationSurface.Elapse(time);
		}

		public override void Reset()
		{
			_animationSurface.Reset();
		}

		#endregion
	}
}