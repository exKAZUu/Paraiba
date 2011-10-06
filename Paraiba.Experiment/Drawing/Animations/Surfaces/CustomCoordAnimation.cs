using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces
{
	public class CustomCoordAnimation : AnimationSurface
	{
		private readonly AnimationSurface _animationSurface;
		private readonly Func<Point2, Size, Point2> _coordinateTransformer;

		public CustomCoordAnimation(AnimationSurface animationSurface, Func<Point2, Size, Point2> coordinateTransformer)
		{
			Contract.Requires(animationSurface != null);
			Contract.Requires(coordinateTransformer != null);

			_animationSurface = animationSurface;
			_coordinateTransformer = coordinateTransformer;
		}

		#region AnimationSurface Members

		public override Size Size
		{
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			var p = _coordinateTransformer(new Point2(x, y), _animationSurface.Size);
			_animationSurface.Draw(g, p.X, p.Y);
		}

		public override Image GetImage()
		{
			return _animationSurface.GetImage();
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return _animationSurface.Ended; }
		}

		public override float ExcessTime
		{
			get { return _animationSurface.ExcessTime; }
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