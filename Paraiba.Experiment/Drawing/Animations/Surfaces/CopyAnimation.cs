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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces {
	/// <summary>
	///   １つのアニメーションを指定した差分位置すべてに同じ内容で表示するアニメーションを表します。
	/// </summary>
	public class CopyAnimation : AnimationSurface {
		private readonly AnimationSurface _animationSurface;
		private readonly IEnumerable<Vector2> _offsetVectors;

		public CopyAnimation(
				AnimationSurface originalAnimationSurface,
				IEnumerable<Vector2> offsetVectors) {
			Contract.Requires(originalAnimationSurface != null);
			Contract.Requires(offsetVectors != null);

			_animationSurface = originalAnimationSurface;
			_offsetVectors = offsetVectors;
		}

		public override Size Size {
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			_animationSurface.Draw(g, x, y);
			foreach (Vector2 v in _offsetVectors) {
				_animationSurface.Draw(g, x + v.X, y + v.Y);
			}
		}

		public override Image GetImage() {
			return _animationSurface.GetImage();
		}

		#region IAnimation メンバ

		public override bool Ended {
			get { return _animationSurface.Ended; }
		}

		public override float ExcessTime {
			get { return _animationSurface.ExcessTime; }
		}

		public override bool Elapse(float time) {
			return _animationSurface.Elapse(time);
		}

		public override void Reset() {
			_animationSurface.Reset();
		}

		#endregion
	}
}