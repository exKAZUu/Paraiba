using System;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Drawing.Surfaces.Sprites;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites
{
	/// <summary>
	/// アニメーションする（時間経過とともに変化する）配置可能でかつ表示可能なオブジェクトを表します。
	/// </summary>
	public abstract class AnimationSprite : Sprite, IAnimation {
		public abstract bool Ended { get; }
		public abstract float ExcessTime { get; }
		public abstract bool Elapse(float time);
		public abstract void Reset();
	}
}