using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	public class EndlessRepeatAnimation : AnimationSurface
	{
		private readonly AnimationSurface _animationSurface;

		public EndlessRepeatAnimation(AnimationSurface animationSurface)
		{
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
		}

		#region AnimationSurface Members

		public override Size Size
		{
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			_animationSurface.Draw(g, x, y);
		}

		public override Image GetImage()
		{
			return _animationSurface.GetImage();
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return false; }
		}

		public override float ExcessTime
		{
			get { return float.NegativeInfinity; }
		}

		public override bool Elapse(float time)
		{
			var result = _animationSurface.Elapse(time);
			if (_animationSurface.Ended)
			{
				var excessTime = _animationSurface.ExcessTime;
				_animationSurface.Reset();
				_animationSurface.Elapse(excessTime);
				return true;
			}
			return result;
		}

		public override void Reset()
		{
			_animationSurface.Reset();
		}

		#endregion
	}
}