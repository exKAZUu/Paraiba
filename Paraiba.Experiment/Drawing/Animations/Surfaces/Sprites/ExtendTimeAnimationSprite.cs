#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites {
	/// <summary>
	///   前後に静止している期間を設けることで指定したアニメーションオブジェクトの時間を延長したものを表す
	/// </summary>
	public class ExtendTimeAnimationSprite : AnimationSprite {
		private readonly AnimationSprite _animationSprite;
		private readonly float _backSpanTime;
		private readonly float _frontSpanTime;
		private float _lapseFrontSpanTime;

		public ExtendTimeAnimationSprite(
				AnimationSprite animationSprite, float backSpanTime)
				: this(animationSprite, 0, backSpanTime) {}

		public ExtendTimeAnimationSprite(
				AnimationSprite animationSprite, float frontSpanTime,
				float backSpanTime) {
			Contract.Requires(animationSprite != null);

			_animationSprite = animationSprite;
			_frontSpanTime = frontSpanTime;
			_backSpanTime = backSpanTime;
		}

		#region Sprite メンバ

		public override Surface CurrentSurface {
			get { return _animationSprite.CurrentSurface; }
		}

		public override Point2 Location {
			get { return _animationSprite.Location; }
			set { _animationSprite.Location = value; }
		}

		public override Size Size {
			get { return _animationSprite.Size; }
		}

		public override void Draw(Graphics g) {
			_animationSprite.Draw(g);
		}

		public override void Draw(Graphics g, Vector2 offset) {
			_animationSprite.Draw(g, offset);
		}

		public override void Draw(
				Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
			_animationSprite.Draw(g, coordinateTransformer);
		}

		public override void Draw(
				Graphics g, Vector2 offset,
				Func<Point2, Size, Point2> coordinateTransformer) {
			_animationSprite.Draw(g, offset, coordinateTransformer);
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended {
			get {
				return _lapseFrontSpanTime + _animationSprite.ExcessTime
						>= _frontSpanTime + _backSpanTime;
			}
		}

		public override float ExcessTime {
			get {
				return _lapseFrontSpanTime + _animationSprite.ExcessTime
						- (_frontSpanTime + _backSpanTime);
			}
		}

		public override bool Elapse(float time) {
			if (_frontSpanTime != _lapseFrontSpanTime) {
				_lapseFrontSpanTime += time;
				if (_lapseFrontSpanTime < _frontSpanTime) {
					return false;
				}
				time = _lapseFrontSpanTime - _frontSpanTime;
				_lapseFrontSpanTime = _frontSpanTime;
			}
			return _animationSprite.Elapse(time);
		}

		public override void Reset() {
			_lapseFrontSpanTime = 0;
			_animationSprite.Reset();
		}

		#endregion
	}
}