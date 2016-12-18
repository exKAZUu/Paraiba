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

namespace Paraiba.Utility {
    public struct Interval {
        internal int max_;
        internal int min_;

        public Interval(int min, int max) {
            if (min < max) {
                min_ = min;
                max_ = max;
            } else {
                min_ = max;
                max_ = min;
            }
        }

        public int Min {
            get { return min_; }
            set { min_ = value; }
        }

        public int Max {
            get { return max_; }
            set { max_ = value; }
        }

        public static Interval operator +(Interval r1, Interval r2) {
            return new Interval(r1.min_ + r2.min_, r1.max_ + r1.max_);
        }

        public static Interval operator -(Interval r1, Interval r2) {
            return new Interval(r1.min_ - r2.min_, r1.max_ - r1.max_);
        }

        public static Interval operator -(Interval r1) {
            return new Interval(-r1.max_, -r1.min_);
        }

        public bool InRange(int value) {
            return min_ <= value && value <= max_;
        }

        public int Limit(int value) {
            int min = min_, max = max_;
            if (value <= min) {
                return min;
            }
            if (value >= max) {
                return max;
            }
            return value;
        }

        public static int Limit(int value, int min, int max) {
            if (value <= min) {
                return min;
            }
            if (value >= max) {
                return max;
            }
            return value;
        }
    }

    public struct IntervalF {
        internal float _max;
        internal float _min;

        public IntervalF(float min, float max) {
            if (min < max) {
                _min = min;
                _max = max;
            } else {
                _min = max;
                _max = min;
            }
        }

        public float Min {
            get { return _min; }
            set { _min = value; }
        }

        public float Max {
            get { return _max; }
            set { _max = value; }
        }

        public static IntervalF operator +(IntervalF r1, IntervalF r2) {
            return new IntervalF(r1._min + r2._min, r1._max + r1._max);
        }

        public static IntervalF operator -(IntervalF r1, IntervalF r2) {
            return new IntervalF(r1._min - r2._min, r1._max - r1._max);
        }

        public static IntervalF operator -(IntervalF r1) {
            return new IntervalF(-r1._max, -r1._min);
        }

        public bool InRange(float value) {
            return _min <= value && value <= _max;
        }

        public float Limit(float value) {
            float min = _min, max = _max;
            if (value <= min) {
                return min;
            }
            if (value >= max) {
                return max;
            }
            return value;
        }

        public static float Limit(float value, float min, float max) {
            if (value <= min) {
                return min;
            }
            if (value >= max) {
                return max;
            }
            return value;
        }
    }
}

// end namespace