#region License

// Copyright (C) 2008-2012 Kazunori Sakamoto
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
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites {
    public class CopySprite : Sprite {
        private readonly IEnumerable<Vector2> _offsetVectors;
        private readonly Sprite _sprite;

        public CopySprite(
                Sprite originalSprite, IEnumerable<Vector2> offsetVectors) {
            Contract.Requires(originalSprite != null);
            Contract.Requires(offsetVectors != null);

            _sprite = originalSprite;
            _offsetVectors = offsetVectors;
        }

        #region Sprite メンバ

        public override Surface CurrentSurface {
            get { return _sprite.CurrentSurface; }
        }

        public override Point2 Location {
            get { return _sprite.Location; }
            set { _sprite.Location = value; }
        }

        public override Size Size {
            get { return _sprite.Size; }
        }

        public override void Draw(Graphics g) {
            _sprite.Draw(g);
            foreach (var v in _offsetVectors) {
                _sprite.Draw(g, v);
            }
        }

        public override void Draw(Graphics g, Vector2 offset) {
            _sprite.Draw(g, offset);
            foreach (var v in _offsetVectors) {
                _sprite.Draw(g, offset + v);
            }
        }

        public override void Draw(
                Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
            _sprite.Draw(g, coordinateTransformer);
            foreach (var v in _offsetVectors) {
                _sprite.Draw(g, v, coordinateTransformer);
            }
        }

        public override void Draw(
                Graphics g, Vector2 offset,
                Func<Point2, Size, Point2> coordinateTransformer) {
            _sprite.Draw(g, offset, coordinateTransformer);
            foreach (var v in _offsetVectors) {
                _sprite.Draw(g, offset + v, coordinateTransformer);
            }
        }

        #endregion
    }
}