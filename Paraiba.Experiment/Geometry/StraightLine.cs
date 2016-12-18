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

using Paraiba.Utility;

namespace Paraiba.Geometry {
    public class StraightLine {
        // ax + by + c = 0

        internal float a_, b_, c_;
        internal IntervalF xInterval_, yInterval_;

        public StraightLine(Point2F p1, Point2F p2) {
            float a = p1.Y - p2.Y, b = p2.X - p1.X;
            a_ = a;
            b_ = b;
            c_ = -(a * p1.X + b * p1.Y);
            xInterval_ = new IntervalF(p1.X, p2.X);
            yInterval_ = new IntervalF(p1.Y, p2.Y);
        }

        public StraightLine(
            float a, float b, float c, IntervalF xInterval,
            IntervalF yInterval) {
            a_ = a;
            b_ = b;
            c_ = c;
            xInterval_ = xInterval;
            yInterval_ = yInterval;
        }

        public StraightLine(float a, float b, float c, IntervalF xInterval)
            : this(
                a, b, c, xInterval,
                new IntervalF(
                    (-a * xInterval._min - c) / b,
                    (-a * xInterval._max - c) / b)) {}

        public StraightLine(
            float a, float b, float c, IntervalF interval, bool isXRange)
            : this(a, b, c,
                isXRange
                    ? interval
                    : new IntervalF(
                        (-b * interval._min - c) / a,
                        (-b * interval._max - c) / a),
                isXRange == false
                    ? interval
                    : new IntervalF(
                        (-a * interval._min - c) / b,
                        (-a * interval._max - c) / b)) {}

        public bool IsOverlap(Point2F p) {
            return a_ * p.X + b_ * p.Y + c_ == 0 &&
                   xInterval_.InRange(p.X) && yInterval_.InRange(p.Y);
        }

        public bool IsOverlap(StraightLine line) {
            float d = a_ * line.b_ - line.a_ * b_;

            if (d != 0) {
                // ��_�����݂���
                float x = (-line.b_ * c_ + b_ * line.c_) / d;
                if (!xInterval_.InRange(x) || !line.xInterval_.InRange(x)) {
                    return false;
                }

                float y = (+line.a_ * c_ - a_ * line.c_) / d;
                return yInterval_.InRange(y) && line.yInterval_.InRange(y);
            } else {
                // ���s�ł���i��_�����݂��Ȃ��j
                return false;
            }
        }
    }
}