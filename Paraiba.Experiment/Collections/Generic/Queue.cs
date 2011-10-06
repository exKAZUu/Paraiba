using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> source)
        {
            foreach (var item in source)
                queue.Enqueue(item);
        }
    }
}