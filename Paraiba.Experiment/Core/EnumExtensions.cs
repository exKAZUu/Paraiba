using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Utility;

namespace Paraiba.Core
{
    public static class EnumExtensions
    {
        public static int GetCount(this Enum e)
        {
            return e.GetType().GetFields().Length - 1;
        }

        public static int GetMaxInt(this Enum e)
        {
            int max = 0;
            var fields = e.GetType().GetFields();
            for (int i = 1; i < fields.Length; i++)
                max = Math.Max(max, (int)fields[i].GetValue(null));
            return max;
        }

        public static int GetMinInt(this Enum e)
        {
            int max = 0;
            var fields = e.GetType().GetFields();
            for (int i = 1; i < fields.Length; i++)
                max = Math.Max(max, (int)fields[i].GetValue(null));
            return max;
        }

        public static Tuple<int, int> GetMinMaxInt(this Enum e)
        {
            int min = 0, max = 0;
            var fields = e.GetType().GetFields();
            for (int i = 1; i < fields.Length; i++)
            {
                var value = (int)fields[i].GetValue(null);
                if (value < min)
                    min = value;
                else if (max < value)
                    max = value;
            }
            return Tuple.Create(min, max);
        }
    }
}