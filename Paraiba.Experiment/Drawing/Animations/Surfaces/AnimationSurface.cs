using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	/// <summary>
	/// アニメーションする（時間経過とともに変化する）表示可能オブジェクトを表します。
	/// </summary>
	public abstract class AnimationSurface : Surface, IAnimation
	{
		public abstract bool Ended { get; }
		public abstract float ExcessTime { get; }
		public abstract bool Elapse(float time);
		public abstract void Reset();
	}
}