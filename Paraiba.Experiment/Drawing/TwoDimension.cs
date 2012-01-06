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

using Paraiba.Geometry;

namespace Paraiba.Drawing {
    public static class TwoDimensionExtensions {
        public static void Rotate90(this I2Dimension<int> src) {
            var x = src.X;
            src.X = src.Y;
            src.Y = -x;
        }

        public static void Rotate90(
                this I2Dimension<int> src, I2Dimension<int> pivot) {
            src.X -= pivot.X;
            src.Y -= pivot.Y;
            src.Rotate90();
            src.X += pivot.X;
            src.Y += pivot.Y;
        }

        public static void Rotate180(this I2Dimension<int> src) {
            src.X = -src.X;
            src.Y = -src.Y;
        }

        public static void Rotate180(
                this I2Dimension<int> src, I2Dimension<int> pivot) {
            src.X -= pivot.X;
            src.Y -= pivot.Y;
            src.Rotate180();
            src.X += pivot.X;
            src.Y += pivot.Y;
        }

        public static void Rotate270(this I2Dimension<int> src) {
            var x = src.X;
            src.X = -src.Y;
            src.Y = x;
        }

        public static void Rotate270(
                this I2Dimension<int> src, I2Dimension<int> pivot) {
            src.X -= pivot.X;
            src.Y -= pivot.Y;
            src.Rotate270();
            src.X += pivot.X;
            src.Y += pivot.Y;
        }
    }
}