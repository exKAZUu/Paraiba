﻿using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	/// <summary>
	/// 前後に静止している期間を設けることで指定したアニメーションの時間を延長したものを表す
	/// </summary>
	public class ExtendTimeAnimation : AnimationSurface
	{
		private readonly AnimationSurface _animationSurface;
		private readonly float _backSpanTime;
		private readonly float _frontSpanTime;
		private float _lapseFrontSpanTime;

		public ExtendTimeAnimation(AnimationSurface animationSurface, float backSpanTime)
			: this(animationSurface, 0, backSpanTime)
		{
		}

		public ExtendTimeAnimation(AnimationSurface animationSurface, float frontSpanTime, float backSpanTime)
		{
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
			_frontSpanTime = frontSpanTime;
			_backSpanTime = backSpanTime;
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
			get { return _lapseFrontSpanTime + _animationSurface.ExcessTime >= _frontSpanTime + _backSpanTime; }
		}

		public override float ExcessTime
		{
			get { return _lapseFrontSpanTime + _animationSurface.ExcessTime - (_frontSpanTime + _backSpanTime); }
		}

		public override bool Elapse(float time)
		{
			Debug.Assert(_lapseFrontSpanTime <= _frontSpanTime);
			if (_lapseFrontSpanTime == _frontSpanTime)
			{
				_animationSurface.Elapse(time);
			}
			else
			{
				_lapseFrontSpanTime += time;
				if (_lapseFrontSpanTime < _frontSpanTime)
					return false;
				_animationSurface.Elapse(_lapseFrontSpanTime - _frontSpanTime);
				// _lapseFrontSpanTime > _frontSpanTime にならないように値を修正
				_lapseFrontSpanTime = _frontSpanTime;
			}
			return _animationSurface.ExcessTime >= _backSpanTime;
		}

		public override void Reset()
		{
			_lapseFrontSpanTime = 0;
			_animationSurface.Reset();
		}

		#endregion
	}
}