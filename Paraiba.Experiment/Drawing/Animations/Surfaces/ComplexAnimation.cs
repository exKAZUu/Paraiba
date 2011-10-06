﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	/// <summary>
	/// 複数の効果を組み合わせて表示するアニメーションを表します。
	/// </summary>
	public class ComplexAnimation : AnimationSurface
	{
		private readonly IList<AnimationSurface> _animationSurfaces;
		private int _iAnimationSurfaces;

		public ComplexAnimation(IList<AnimationSurface> animationSurfaces)
		{
			Contract.Requires(animationSurfaces != null);

			_animationSurfaces = animationSurfaces;
			_iAnimationSurfaces = 0;
		}

		#region AnimationSurface Members

		public override Size Size
		{
			get { return _animationSurfaces[_iAnimationSurfaces].Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			_animationSurfaces[_iAnimationSurfaces].Draw(g, x, y);
		}

		public override Image GetImage()
		{
			return _animationSurfaces[_iAnimationSurfaces].GetImage();
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return _animationSurfaces[_iAnimationSurfaces].Ended; }
		}

		public override float ExcessTime
		{
			get
			{
				// 無駄だと思われるが、正確な値を計算する
				float time = 0;
				for (int i = _iAnimationSurfaces; i < _animationSurfaces.Count; i++)
					time += _animationSurfaces[_iAnimationSurfaces].ExcessTime;
				return time;
			}
		}

		public override bool Elapse(float time)
		{
			bool result = _animationSurfaces[_iAnimationSurfaces].Elapse(time);
			while (_animationSurfaces[_iAnimationSurfaces].Ended)
			{
				if (++_iAnimationSurfaces >= _animationSurfaces.Count)
				{
					_iAnimationSurfaces = _animationSurfaces.Count - 1;
					break;
				}

				_animationSurfaces[_iAnimationSurfaces].Elapse(_animationSurfaces[_iAnimationSurfaces - 1].ExcessTime);
				// アニメーションが切り替わったので更新が必要であることを伝える
				result = true;
			}
			return result;
		}

		public override void Reset()
		{
			for (int i = 0; i <= _iAnimationSurfaces; i++)
			{
				_animationSurfaces[i].Reset();
			}
			_iAnimationSurfaces = 0;
		}

		#endregion
	}
}