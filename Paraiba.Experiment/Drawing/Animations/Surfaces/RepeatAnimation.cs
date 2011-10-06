using System.Diagnostics.Contracts;
using System.Drawing;

using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	public class RepeatAnimation : AnimationSurface
	{
		private readonly AnimationSurface _animationSurface;
		private readonly int _nRepeat;
		private int _repeatCount;

		public RepeatAnimation(AnimationSurface animationSurface, int nRepeat)
		{
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
			_nRepeat = nRepeat;
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
			get { return _repeatCount < _nRepeat; }
		}

		public override float ExcessTime
		{
			get { return _animationSurface.ExcessTime; }
		}

		public override bool Elapse(float time)
		{
			var result = _animationSurface.Elapse(time);
			if (_animationSurface.Ended)
			{
				_repeatCount++;
				if (_repeatCount < _nRepeat)
				{
					var excessTime = _animationSurface.ExcessTime;
					_animationSurface.Reset();
					_animationSurface.Elapse(excessTime);
					return true;
				}
			}
			return result;
		}

		public override void Reset()
		{
			_animationSurface.Reset();
			_repeatCount = 0;
		}

		#endregion
	}
}