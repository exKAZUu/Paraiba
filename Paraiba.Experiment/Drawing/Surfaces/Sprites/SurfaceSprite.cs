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
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites {
    /// <summary>
    ///   指定したスプライトを利用して配置可能でかつ表示可能なオブジェクトを表します。
    /// </summary>
    public class SurfaceSprite : Sprite {
        private readonly Surface _surface;
        private Point2 _location;

        public SurfaceSprite(Surface surface, Point2 location) {
            Contract.Requires(surface != null);

            _surface = surface;
            _location = location;
        }

        public override Surface CurrentSurface {
            get { return _surface; }
        }

        public override Size Size {
            get { return _surface.Size; }
        }

        public override Point2 Location {
            get { return _location; }
            set { _location = value; }
        }

        public override void Draw(Graphics g) {
            _surface.Draw(g, _location);
        }

        public override void Draw(Graphics g, Vector2 offset) {
            _surface.Draw(g, _location + offset);
        }

        public override void Draw(
            Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
            _surface.Draw(g, coordinateTransformer(_location, _surface.Size));
        }

        public override void Draw(
            Graphics g, Vector2 offset,
            Func<Point2, Size, Point2> coordinateTransformer) {
            _surface.Draw(
                g, coordinateTransformer(_location + offset, _surface.Size));
        }
    }
}