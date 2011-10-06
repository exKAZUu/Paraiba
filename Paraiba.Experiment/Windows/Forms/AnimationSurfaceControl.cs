using Paraiba.Drawing.Animations;
using Paraiba.Drawing.Animations.Surfaces;

namespace Paraiba.Windows.Forms
{
	public class AnimationSurfaceControl : BasicSurfaceControl<AnimationSurface>, IAnimation
	{
		public AnimationSurfaceControl(AnimationSurface animationSurface)
			: base(animationSurface)
		{
		}

		#region IAnimation メンバ

		public bool Ended
		{
			get { return surface_.Ended; }
		}

		public float ExcessTime
		{
			get { return surface_.ExcessTime; }
		}

		public bool Elapse(float time)
		{
			if (surface_.Elapse(time))
			{
				ArrangeSizeAndLocation();
				InvalidateEx();
				return true;
			}
			return false;
		}

		public void Reset()
		{
			surface_.Reset();
		}

		#endregion
	}
}