using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic
{
    public static class StackExtensions
    {
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> source)
        {
            foreach (var item in source)
                stack.Push(item);
        }
    }
}