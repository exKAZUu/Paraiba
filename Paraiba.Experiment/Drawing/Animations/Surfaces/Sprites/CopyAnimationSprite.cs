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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites {
    /// <summary>
    ///   １つのアニメーションオブジェクトを指定した差分位置すべてに同じ内容で表示する、 配置可能でかつ表示可能なアニメーションオブジェクトを表します。
    /// </summary>
    public class CopyAnimationSprite : AnimationSprite {
        private readonly AnimationSprite _animationSprite;
        private readonly IEnumerable<Vector2> _offsetVectors;

        public CopyAnimationSprite(
                AnimationSprite originalAnimationSprite,
                IEnumerable<Vector2> offsetVectors) {
            Contract.Requires(originalAnimationSprite != null);
            Contract.Requires(offsetVectors != null);

            _animationSprite = originalAnimationSprite;
            _offsetVectors = offsetVectors;
        }

        public CopyAnimationSprite(
                AnimationSprite originalAnimationSprite,
                IEnumerable<Point2> points) {
            _animationSprite = originalAnimationSprite;
            var v = (Vector2)originalAnimationSprite.Location;
            _offsetVectors = points.Select(p => (Vector2)p - v);
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
            foreach (Vector2 v in _offsetVectors) {
                _animationSprite.Draw(g, v);
            }
        }

        public override void Draw(Graphics g, Vector2 offset) {
            _animationSprite.Draw(g, offset);
            foreach (Vector2 v in _offsetVectors) {
                _animationSprite.Draw(g, offset + v);
            }
        }

        public override void Draw(
                Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
            _animationSprite.Draw(g, coordinateTransformer);
            foreach (Vector2 v in _offsetVectors) {
                _animationSprite.Draw(g, v, coordinateTransformer);
            }
        }

        public override void Draw(
                Graphics g, Vector2 offset,
                Func<Point2, Size, Point2> coordinateTransformer) {
            _animationSprite.Draw(g, offset, coordinateTransformer);
            foreach (Vector2 v in _offsetVectors) {
                _animationSprite.Draw(g, offset + v, coordinateTransformer);
            }
        }

        #endregion

        #region IAnimation メンバ

        public override bool Ended {
            get { return _animationSprite.Ended; }
        }

        public override float ExcessTime {
            get { return _animationSprite.ExcessTime; }
        }

        public override bool Elapse(float time) {
            return _animationSprite.Elapse(time);
        }

        public override void Reset() {
            _animationSprite.Reset();
        }

        #endregion
    }
}