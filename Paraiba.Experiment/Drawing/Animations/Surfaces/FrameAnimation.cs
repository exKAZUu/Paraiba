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
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces {
	/// <summary>
	///   等間隔でSpriteを切り替えて表示するアニメーションを表します。
	/// </summary>
	public class FrameAnimation : AnimationSurface {
		private readonly float _interval;
		private readonly IList<Surface> _surfaces;
		private int _iSurface;
		private float _lapseTime;

		public FrameAnimation(IList<Surface> surfaces, float interval) {
			Contract.Requires(surfaces != null);

			_surfaces = surfaces;
			_interval = interval;
		}

		public FrameAnimation(
				IList<Surface> surfaces, float time, bool isTotalTime)
				: this(surfaces, isTotalTime ? time / surfaces.Count : time) {}

		public override Size Size {
			get { return _surfaces[_iSurface].Size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			_surfaces[_iSurface].Draw(g, x, y);
		}

		public override Image GetImage() {
			return _surfaces[_iSurface].GetImage();
		}

		#region IAnimation メンバ

		public override bool Ended {
			get { return _lapseTime >= _interval * _surfaces.Count; }
		}

		public override float ExcessTime {
			get { return _lapseTime - _interval * _surfaces.Count; }
		}

		public override bool Elapse(float time) {
			var lastSprite = _surfaces[_iSurface];

			_lapseTime += time;
			int Sprite = (int)(_lapseTime / _interval);
			if (Sprite >= _surfaces.Count) {
				Sprite = _surfaces.Count - 1;
			}
			_iSurface = Sprite;

			// 表示するスプライトが変わった場合、更新が必要であることを示す
			return lastSprite != _surfaces[Sprite];
		}

		public override void Reset() {
			_iSurface = 0;
			_lapseTime = 0;
		}

		#endregion
	}
}