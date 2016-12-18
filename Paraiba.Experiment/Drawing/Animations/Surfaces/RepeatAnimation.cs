#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
    public class RepeatAnimation : AnimationSurface {
        private readonly AnimationSurface _animationSurface;
        private readonly int _nRepeat;
        private int _repeatCount;

        public RepeatAnimation(AnimationSurface animationSurface, int nRepeat) {
            Contract.Requires(animationSurface != null);

            _animationSurface = animationSurface;
            _nRepeat = nRepeat;
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
            get { return _repeatCount < _nRepeat; }
        }

        public override float ExcessTime {
            get { return _animationSurface.ExcessTime; }
        }

        public override bool Elapse(float time) {
            var result = _animationSurface.Elapse(time);
            if (_animationSurface.Ended) {
                _repeatCount++;
                if (_repeatCount < _nRepeat) {
                    var excessTime = _animationSurface.ExcessTime;
                    _animationSurface.Reset();
                    _animationSurface.Elapse(excessTime);
                    return true;
                }
            }
            return result;
        }

        public override void Reset() {
            _animationSurface.Reset();
            _repeatCount = 0;
        }

        #endregion
    }
}