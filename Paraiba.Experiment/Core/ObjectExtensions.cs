using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Core
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// nullでないとき実行
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="arg">this</param>
        /// <param name="action">行うアクション</param>
        public static void NotNull<T>(this T arg, Action<T> action)
        {
            if (arg != null)
                action(arg);
        }
    }
}