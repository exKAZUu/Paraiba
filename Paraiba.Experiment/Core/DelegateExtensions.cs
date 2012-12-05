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

using System;
using Paraiba.Utility;

namespace Paraiba.Core {
    public static class DelegateExtensions {
        public static TResult InvokeIfNotNull<TResult>(
                this Func<TResult> func, TResult deafultResult) {
            if (func != null) {
                return func();
            }
            return deafultResult;
        }

        public static TResult InvokeIfNotNull<T, TResult>(
                this Func<T, TResult> func, T arg, TResult deafultResult) {
            if (func != null) {
                return func(arg);
            }
            return deafultResult;
        }

        public static TResult InvokeIfNotNull<T1, T2, TResult>(
                this Func<T1, T2, TResult> func, T1 arg1, T2 arg2,
                TResult deafultResult) {
            if (func != null) {
                return func(arg1, arg2);
            }
            return deafultResult;
        }

        public static TResult InvokeIfNotNull<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3,
                TResult deafultResult) {
            if (func != null) {
                return func(arg1, arg2, arg3);
            }
            return deafultResult;
        }

        public static TResult InvokeIfNotNull<T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2,
                T3 arg3, T4 arg4, TResult deafultResult) {
            if (func != null) {
                return func(arg1, arg2, arg3, arg4);
            }
            return deafultResult;
        }

        public static TResult InvokeIfNotNull<TResult>(this Func<TResult> func) {
            return func.InvokeIfNotNull(default(TResult));
        }

        public static TResult InvokeIfNotNull<T, TResult>(
                this Func<T, TResult> func, T arg) {
            return func.InvokeIfNotNull(arg, default(TResult));
        }

        public static TResult InvokeIfNotNull<T1, T2, TResult>(
                this Func<T1, T2, TResult> func, T1 arg1, T2 arg2) {
            return func.InvokeIfNotNull(arg1, arg2, default(TResult));
        }

        public static TResult InvokeIfNotNull<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3) {
            return func.InvokeIfNotNull(arg1, arg2, arg3, default(TResult));
        }

        public static TResult InvokeIfNotNull<T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2,
                T3 arg3, T4 arg4) {
            return func.InvokeIfNotNull(
                    arg1, arg2, arg3, arg4, default(TResult));
        }

        public static void InvokeIfNotNull(this Action action) {
            if (action != null) {
                action();
            }
        }

        public static void InvokeIfNotNull<T>(this Action<T> action, T arg) {
            if (action != null) {
                action(arg);
            }
        }

        public static void InvokeIfNotNull<T1, T2>(
                this Action<T1, T2> action, T1 arg1, T2 arg2) {
            if (action != null) {
                action(arg1, arg2);
            }
        }

        public static void InvokeIfNotNull<T1, T2, T3>(
                this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3) {
            if (action != null) {
                action(arg1, arg2, arg3);
            }
        }

        public static void InvokeIfNotNull<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3,
                T4 arg4) {
            if (action != null) {
                action(arg1, arg2, arg3, arg4);
            }
        }

        public static Func<TResult> GetNOP<TResult>(this Func<TResult> func) {
            return Delegates.NOP<TResult>;
        }

        public static Func<T, TResult> GetNOP<T, TResult>(
                this Func<T, TResult> func) {
            return Delegates.NOP<T, TResult>;
        }

        public static Func<T1, T2, TResult> GetNOP<T1, T2, TResult>(
                this Func<T1, T2, TResult> func) {
            return Delegates.NOP<T1, T2, TResult>;
        }

        public static Func<T1, T2, T3, TResult> GetNOP<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func) {
            return Delegates.NOP<T1, T2, T3, TResult>;
        }

        public static Func<T1, T2, T3, T4, TResult> GetNOP
                <T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func) {
            return Delegates.NOP<T1, T2, T3, T4, TResult>;
        }

        public static Func<TResult> GetNOP<TResult>(
                this Func<TResult> func, TResult result) {
            return () => result;
        }

        public static Func<T, TResult> GetNOP<T, TResult>(
                this Func<T, TResult> func, TResult result) {
            return (_1) => result;
        }

        public static Func<T1, T2, TResult> GetNOP<T1, T2, TResult>(
                this Func<T1, T2, TResult> func, TResult result) {
            return (_1, _2) => result;
        }

        public static Func<T1, T2, T3, TResult> GetNOP<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, TResult result) {
            return (_1, _2, _3) => result;
        }

        public static Func<T1, T2, T3, T4, TResult> GetNOP
                <T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, TResult result) {
            return (_1, _2, _3, _4) => result;
        }

        public static Action GetNOP(this Action action) {
            return Delegates.NOP;
        }

        public static Action<T> GetNOP<T>(this Action<T> action) {
            return Delegates.NOP<T>;
        }

        public static Action<T1, T2> GetNOP<T1, T2>(this Action<T1, T2> action) {
            return Delegates.NOP<T1, T2>;
        }

        public static Action<T1, T2, T3> GetNOP<T1, T2, T3>(
                this Action<T1, T2, T3> action) {
            return Delegates.NOP<T1, T2, T3>;
        }

        public static Action<T1, T2, T3, T4> GetNOP<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> action) {
            return Delegates.NOP<T1, T2, T3, T4>;
        }

        // TODO: カリー化の定義を見直す

        public static Func<TResult> GetCurrying<T1, TResult>(
                this Func<T1, TResult> func, T1 arg1) {
            return () => func(arg1);
        }

        public static Func<T2, TResult> GetCurrying<T1, T2, TResult>(
                this Func<T1, T2, TResult> func, T1 arg1) {
            return (arg2) => func(arg1, arg2);
        }

        public static Func<TResult> GetCurrying<T1, T2, TResult>(
                this Func<T1, T2, TResult> func, T1 arg1, T2 arg2) {
            return () => func(arg1, arg2);
        }

        public static Func<T2, T3, TResult> GetCurrying<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, T1 arg1) {
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        public static Func<T3, TResult> GetCurrying<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2) {
            return (arg3) => func(arg1, arg2, arg3);
        }

        public static Func<TResult> GetCurrying<T1, T2, T3, TResult>(
                this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3) {
            return () => func(arg1, arg2, arg3);
        }

        public static Func<T2, T3, T4, TResult> GetCurrying
                <T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, T1 arg1) {
            return (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Func<T3, T4, TResult> GetCurrying<T1, T2, T3, T4, TResult>
                (this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2) {
            return (arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Func<T4, TResult> GetCurrying<T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2,
                T3 arg3) {
            return (arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Func<TResult> GetCurrying<T1, T2, T3, T4, TResult>(
                this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2,
                T3 arg3, T4 arg4) {
            return () => func(arg1, arg2, arg3, arg4);
        }

        public static Action GetCurrying<T1>(this Action<T1> func, T1 arg1) {
            return () => func(arg1);
        }

        public static Action<T2> GetCurrying<T1, T2>(
                this Action<T1, T2> func, T1 arg1) {
            return (arg2) => func(arg1, arg2);
        }

        public static Action GetCurrying<T1, T2>(
                this Action<T1, T2> func, T1 arg1, T2 arg2) {
            return () => func(arg1, arg2);
        }

        public static Action<T2, T3> GetCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T1 arg1) {
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        public static Action<T3> GetCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T1 arg1, T2 arg2) {
            return (arg3) => func(arg1, arg2, arg3);
        }

        public static Action GetCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T1 arg1, T2 arg2, T3 arg3) {
            return () => func(arg1, arg2, arg3);
        }

        public static Action<T2, T3, T4> GetCurrying<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> func, T1 arg1) {
            return (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Action<T3, T4> GetCurrying<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> func, T1 arg1, T2 arg2) {
            return (arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Action<T4> GetCurrying<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> func, T1 arg1, T2 arg2, T3 arg3) {
            return (arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Action GetCurrying<T1, T2, T3, T4>(
                this Action<T1, T2, T3, T4> func, T1 arg1, T2 arg2, T3 arg3,
                T4 arg4) {
            return () => func(arg1, arg2, arg3, arg4);
        }

        public static Action<T2> GetXCurrying<T1, T2>(
                this Action<T1, T2> func, T1 arg1) {
            return (arg2) => func(arg1, arg2);
        }

        public static Action<T1> GetXCurrying<T1, T2>(
                this Action<T1, T2> func, T2 arg2) {
            return (arg1) => func(arg1, arg2);
        }

        public static Action<T2, T3> GetXCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T1 arg1) {
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        public static Action<T1, T3> GetXCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T2 arg2) {
            return (arg1, arg3) => func(arg1, arg2, arg3);
        }

        public static Action<T1, T2> GetXCurrying<T1, T2, T3>(
                this Action<T1, T2, T3> func, T3 arg3) {
            return (arg1, arg2) => func(arg1, arg2, arg3);
        }
    }
}