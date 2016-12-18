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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Paraiba.Core {
    public static class RandomExtensions {
        public static bool NextUnfairBoolInPercentage(
            this Random rand, int truePercent) {
            return rand.Next(100) < truePercent;
        }

        public static bool NextUnfairBool(this Random rand, double trueProb) {
            return rand.NextDouble() < trueProb;
        }

        public static bool NextBool(this Random rand) {
            return (rand.Next() & 1) == 0;
        }

        public static T Select<T>(this Random rand, IList<T> list) {
            return list[rand.Next(list.Count)];
        }

        public static T Select<T>(this Random rand, IList<T> list, int maxIndex) {
            Debug.Assert(maxIndex <= list.Count);
            return list[rand.Next(maxIndex)];
        }

        public static T Select<T>(
            this Random rand, IList<T> list, int minIndex, int maxIndex) {
            Debug.Assert(0 <= minIndex);
            Debug.Assert(minIndex <= maxIndex);
            Debug.Assert(maxIndex <= list.Count);
            return list[rand.Next(minIndex, maxIndex)];
        }

        public static T Select<T>(
            this Random rand, IEnumerable<T> source, int maxIndex) {
            return source.ElementAt(rand.Next(maxIndex));
        }

        public static T Select<T>(
            this Random rand, IEnumerable<T> source, int minIndex,
            int maxIndex) {
            Debug.Assert(0 <= minIndex);
            Debug.Assert(minIndex <= maxIndex);
            return source.ElementAt(rand.Next(minIndex, maxIndex));
        }
    }
}