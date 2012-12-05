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

namespace Paraiba.Geometry {
    public class Polygon {
        internal float[] corossProducts;
        internal Point2F[] points;

        public Polygon(Point2F[] points) {
            this.points = points;
            corossProducts = new float[(points.Length + 1) / 2];
        }

        public bool isOverlap(Point2F q) {
            bool inside = false;
            Point2F p1 = points[points.Length - 1];
            bool isP1yBigger = (p1.Y >= q.Y);
            for (int i = 0; i < points.Length; i++) {
                Point2F p2 = points[i];
                bool isP2yBigger = (p2.Y >= q.Y);
                // p1.y_ < q.y_ <= p2.y_ || p2.y_ < q.y_ <= p1.y_
                if (isP1yBigger != isP2yBigger) {
                    /*
					 * (p1.x_(p2.y_ - q.y_) - p2.x_(p1.y_ - q.y_)) / (p2.y_ - p1.y_) >= q.x_
					 * isP2yBigger == true  ÅÃ p1.x_(p2.y_ - q.y_) - p2.x_(p1.y_ - q.y_) >= q.x_(p2.y_ - p1.y_)
					 * isP2yBigger == false ÅÃ p1.x_(p2.y_ - q.y_) - p2.x_(p1.y_ - q.y_) <= q.x_(p2.y_ - p1.y_)
					 */
                    if (((p2.Y - q.Y) * (p1.X - p2.X)
                            >= (p2.X - q.X) * (p1.Y - p2.Y)) == isP2yBigger) {
                        inside = !inside;
                    }
                }
                p1 = p2;
                isP1yBigger = isP2yBigger;
            }
            return inside;
        }

        public bool isOverlap(StraightLine line) {
            //return IsOverlap();

            //for (int i = 0; i < poly.points.Length; i++) {
            //    if (this.IsOverlap(poly.points[i]))
            //        return true;
            //}
            return false;
        }

        public bool isOverlap(Polygon poly) {
            for (int i = 0; i < points.Length; i++) {
                if (poly.isOverlap(points[i])) {
                    return true;
                }
            }
            for (int i = 0; i < poly.points.Length; i++) {
                if (isOverlap(poly.points[i])) {
                    return true;
                }
            }
            return false;
        }
    }
}