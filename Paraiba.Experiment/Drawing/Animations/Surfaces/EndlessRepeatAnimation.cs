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

using System.Diagnostics.Contracts;
using System.Drawing;

namespace Paraiba.Drawing.Animations.Surfaces {
	public class EndlessRepeatAnimation : AnimationSurface {
		private readonly AnimationSurface _animationSurface;

		public EndlessRepeatAnimation(AnimationSurface animationSurface) {
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
		}

		public override Size Size {
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			_animationSurface.Draw(g, x, y);
		}

		public override Image GetImage() {
			return _animationSurface.GetImage();
		}

		#region IAnimation メンバ

		public override bool Ended {
			get { return false; }
		}

		public override float ExcessTime {
			get { return float.NegativeInfinity; }
		}

		public override bool Elapse(float time) {
			var result = _animationSurface.Elapse(time);
			if (_animationSurface.Ended) {
				var excessTime = _animationSurface.ExcessTime;
				_animationSurface.Reset();
				_animationSurface.Elapse(excessTime);
				return true;
			}
			return result;
		}

		public override void Reset() {
			_animationSurface.Reset();
		}

		#endregion
	}
}