﻿#region License

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
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites {
    public class CustomCoordAnimationSprite : AnimationSprite {
        private readonly AnimationSprite _animationSprite;
        private readonly Func<Point2, Size, Point2> _coordinateTransformer;

        public CustomCoordAnimationSprite(
            AnimationSprite animationSprite,
            Func<Point2, Size, Point2> coordinateTransformer) {
            Contract.Requires(animationSprite != null);
            Contract.Requires(coordinateTransformer != null);

            _animationSprite = animationSprite;
            _coordinateTransformer = coordinateTransformer;
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
            _animationSprite.Draw(g, _coordinateTransformer);
        }

        public override void Draw(Graphics g, Vector2 offset) {
            _animationSprite.Draw(g, offset, _coordinateTransformer);
        }

        public override void Draw(
            Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
            // this._coordinateTransformer は主に描画開始位置の調整で利用
            // _coordinateTransformer は一般的な座標変換で利用
            // 最後に生成した CustomCoordSprite が最初に適用される
            _animationSprite.Draw(
                g,
                (p, size) =>
                            _coordinateTransformer(coordinateTransformer(p, size), size));
        }

        public override void Draw(
            Graphics g, Vector2 offset,
            Func<Point2, Size, Point2> coordinateTransformer) {
            // this._coordinateTransformer は主に描画開始位置の調整で利用
            // _coordinateTransformer は一般的な座標変換で利用
            // 最後に生成した CustomCoordSprite が最初に適用される
            _animationSprite.Draw(
                g, offset,
                (p, size) =>
                            _coordinateTransformer(coordinateTransformer(p, size), size));
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