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

using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing {
    public static class GraphicsExtensionMethod {
        public static void DrawSurface(
            this Graphics g, Surface surface, Point p) {
            surface.Draw(g, p);
        }

        public static void DrawSurface(
            this Graphics g, Surface surface, Point2 p) {
            surface.Draw(g, p);
        }

        public static void DrawSurface(
            this Graphics g, Surface surface, int x, int y) {
            surface.Draw(g, x, y);
        }
    }
}