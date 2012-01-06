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
using System.Drawing;

namespace Paraiba.Drawing {
    public static class RectangleExtensions {
        public static Rectangle Circumscribed(this RectangleF rect) {
            var left = (int)rect.Left;
            var top = (int)rect.Top;
            return new Rectangle(
                    left, top,
                    (int)Math.Ceiling(rect.Right) - left,
                    (int)Math.Ceiling(rect.Bottom) - top);
        }

        public static Rectangle Round(this RectangleF rect) {
            return Rectangle.Round(rect);
        }
    }
}