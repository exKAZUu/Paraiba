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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites {
    /// <summary>
    ///   複数の効果を組み合わせた配置可能でかつ表示可能なアニメーションオブジェクトを表します。
    /// </summary>
    public class ComplexAnimationSprite : AnimationSprite {
        private readonly IList<AnimationSprite> _animationSprites;
        private int _iAnimationSprites;
        private float _lapse;

        public ComplexAnimationSprite(IList<AnimationSprite> animationSprites) {
            Contract.Requires(animationSprites != null);

            _animationSprites = animationSprites;
            _iAnimationSprites = 0;
        }

        #region Sprite メンバ

        public override Surface CurrentSurface {
            get { return _animationSprites[_iAnimationSprites].CurrentSurface; }
        }

        public override Point2 Location {
            get { return _animationSprites[_iAnimationSprites].Location; }
            set { _animationSprites[_iAnimationSprites].Location = value; }
        }

        public override Size Size {
            get { return _animationSprites[_iAnimationSprites].Size; }
        }

        public override void Draw(Graphics g) {
            _animationSprites[_iAnimationSprites].Draw(g);
        }

        public override void Draw(Graphics g, Vector2 offset) {
            _animationSprites[_iAnimationSprites].Draw(g, offset);
        }

        public override void Draw(
            Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
            _animationSprites[_iAnimationSprites].Draw(g, coordinateTransformer);
        }

        public override void Draw(
            Graphics g, Vector2 offset,
            Func<Point2, Size, Point2> coordinateTransformer) {
            _animationSprites[_iAnimationSprites].Draw(
                g, offset, coordinateTransformer);
        }

        #endregion

        #region IAnimation メンバ

        public override bool Ended {
            get { return _animationSprites[_iAnimationSprites].Ended; }
        }

        public override float ExcessTime {
            get {
                // 無駄だと思われるが、正確な値を計算する
                float time = 0;
                for (int i = _iAnimationSprites; i < _animationSprites.Count;
                    i++) {
                    time += _animationSprites[i].ExcessTime;
                }
                return time;
            }
        }

        public override bool Elapse(float time) {
            _lapse += time;
            bool result = _animationSprites[_iAnimationSprites].Elapse(time);
            while (_animationSprites[_iAnimationSprites].Ended) {
                if (++_iAnimationSprites >= _animationSprites.Count) {
                    _iAnimationSprites = _animationSprites.Count - 1;
                    break;
                }
                _animationSprites[_iAnimationSprites].Elapse(
                    _animationSprites[_iAnimationSprites - 1].ExcessTime);
                // アニメーションが切り替わったので更新が必要であることを伝える
                result = true;
            }
            return result;
        }

        public override void Reset() {
            for (int i = 0; i <= _iAnimationSprites; i++) {
                _animationSprites[i].Reset();
            }
            _iAnimationSprites = 0;
        }

        #endregion
    }
}