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

using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces {
    public class StaticAnimation : AnimationSurface {
        private readonly Surface _surface;

        public StaticAnimation(Surface surface) {
            Contract.Requires(surface != null);

            _surface = surface;
        }

        public override Size Size {
            get { return _surface.Size; }
        }

        public override void Draw(Graphics g, int x, int y) {
            _surface.Draw(g, x, y);
        }

        public override Image GetImage() {
            return _surface.GetImage();
        }

        #region IAnimation メンバ

        public override bool Ended {
            get { return false; }
        }

        public override float ExcessTime {
            get { return float.NegativeInfinity; }
        }

        public override bool Elapse(float time) {
            return false;
        }

        public override void Reset() {}

        #endregion
    }
}