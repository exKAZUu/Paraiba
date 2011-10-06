using Paraiba.Drawing.Animations;
using Paraiba.Drawing.Animations.Surfaces.Sprites;

namespace Paraiba.Windows.Forms
{
	public class AnimationSpriteControl : BasicSpriteControl<AnimationSprite>, IAnimation
	{
		public AnimationSpriteControl(AnimationSprite animationSprite)
			: base(animationSprite)
		{
		}

		#region IAnimation メンバ

		public bool Ended
		{
			get { return sprite_.Ended; }
		}

		public float ExcessTime
		{
			get { return sprite_.ExcessTime; }
		}

		public bool Elapse(float time)
		{
			if (sprite_.Elapse(time))
			{
				ArrangeSizeAndLocation();
				InvalidateEx();
				return true;
			}
			return false;
		}

		public void Reset()
		{
			sprite_.Reset();
		}

		#endregion
	}
}