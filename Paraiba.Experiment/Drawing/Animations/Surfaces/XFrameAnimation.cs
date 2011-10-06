using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	/// <summary>
	/// デリゲートから得られる間隔でSpriteを切り替えて表示するアニメーションを表します。
	/// </summary>
	public class XFrameAnimation : AnimationSurface
	{
		private readonly Func<int, float> _getIntervalFunc;
		private readonly IList<Surface> _surfaces;
		private float _interval;
		private bool _isEnded;
		private int _Sprite;
		private float _lapseTime;

		public XFrameAnimation(IList<Surface> surfaces, Func<int, float> getIntervalWithIndexFunc)
		{
			Contract.Requires(surfaces != null);
			Contract.Requires(getIntervalWithIndexFunc != null);

			_surfaces = surfaces;
			_getIntervalFunc = getIntervalWithIndexFunc;
			_interval = getIntervalWithIndexFunc(0);
		}

		#region AnimationSurface Members

		public override Size Size
		{
			get { return _surfaces[_Sprite].Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			_surfaces[_Sprite].Draw(g, x, y);
		}

		public override Image GetImage()
		{
			return _surfaces[_Sprite].GetImage();
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return _isEnded; }
		}

		public override float ExcessTime
		{
			get { return _lapseTime; }
		}

		public override bool Elapse(float time)
		{
			_lapseTime += time;
			// 既にアニメーションが終了していた場合は何もしない
			if (_isEnded) return false;

			var lastSprite = _surfaces[_Sprite];

			while (_lapseTime >= _interval)
			{
				_lapseTime -= _interval;
				_Sprite++;
				if (_Sprite >= _surfaces.Count)
				{
					_isEnded = true;
					_Sprite = _surfaces.Count - 1;
					break;
				}
				_interval = _getIntervalFunc(_Sprite);
			}

			// 表示するスプライトが変わった場合、更新が必要であることを示す
			return lastSprite != _surfaces[_Sprite];
		}

		public override void Reset()
		{
			_Sprite = 0;
			_lapseTime = 0;
			_interval = _getIntervalFunc(0);
			_isEnded = false;
		}

		#endregion
	}
}