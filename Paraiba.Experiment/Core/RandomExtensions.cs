using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Paraiba.Core
{
    public static class RandomExtensions
    {
        public static bool NextUnfairBoolInPercentage(this System.Random rand, int truePercent)
        {
            return rand.Next(100) < truePercent;
        }

        public static bool NextUnfairBool(this System.Random rand, double trueProb)
        {
            return rand.NextDouble() < trueProb;
        }

        public static bool NextBool(this System.Random rand)
        {
            return (rand.Next() & 1) == 0;
        }

        public static T Select<T>(this System.Random rand, IList<T> list)
        {
            return list[rand.Next(list.Count)];
        }

        public static T Select<T>(this System.Random rand, IList<T> list, int maxIndex)
        {
            Debug.Assert(maxIndex <= list.Count);
            return list[rand.Next(maxIndex)];
        }

        public static T Select<T>(this System.Random rand, IList<T> list, int minIndex, int maxIndex)
        {
            Debug.Assert(0 <= minIndex);
            Debug.Assert(minIndex <= maxIndex);
            Debug.Assert(maxIndex <= list.Count);
            return list[rand.Next(minIndex, maxIndex)];
        }

        public static T Select<T>(this System.Random rand, IEnumerable<T> source, int maxIndex)
        {
            return source.ElementAt(rand.Next(maxIndex));
        }

        public static T Select<T>(this System.Random rand, IEnumerable<T> source, int minIndex, int maxIndex)
        {
            Debug.Assert(0 <= minIndex);
            Debug.Assert(minIndex <= maxIndex);
            return source.ElementAt(rand.Next(minIndex, maxIndex));
        }
    }
}