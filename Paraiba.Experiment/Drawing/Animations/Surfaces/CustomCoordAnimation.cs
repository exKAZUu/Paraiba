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
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces {
    public class CustomCoordAnimation : AnimationSurface {
        private readonly AnimationSurface _animationSurface;
        private readonly Func<Point2, Size, Point2> _coordinateTransformer;

        public CustomCoordAnimation(
                AnimationSurface animationSurface,
                Func<Point2, Size, Point2> coordinateTransformer) {
            Contract.Requires(animationSurface != null);
            Contract.Requires(coordinateTransformer != null);

            _animationSurface = animationSurface;
            _coordinateTransformer = coordinateTransformer;
        }

        public override Size Size {
            get { return _animationSurface.Size; }
        }

        public override void Draw(Graphics g, int x, int y) {
            var p = _coordinateTransformer(
                    new Point2(x, y), _animationSurface.Size);
            _animationSurface.Draw(g, p.X, p.Y);
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