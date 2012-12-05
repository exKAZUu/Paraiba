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

namespace Paraiba.Utility {
    public static class Delegates {
        public static TResult NOP<TResult>() {
            return default(TResult);
        }

        public static TResult NOP<T, TResult>(T arg) {
            return default(TResult);
        }

        public static TResult NOP<T1, T2, TResult>(T1 arg1, T2 arg2) {
            return default(TResult);
        }

        public static TResult NOP<T1, T2, T3, TResult>(
                T1 arg1, T2 arg2, T3 arg3) {
            return default(TResult);
        }

        public static TResult NOP<T1, T2, T3, T4, TResult>(
                T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            return default(TResult);
        }

        public static void NOP() {}

        public static void NOP<T>(T arg) {}

        public static void NOP<T1, T2>(T1 arg1, T2 arg2) {}

        public static void NOP<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {}

        public static void NOP<T1, T2, T3, T4>(
                T1 arg1, T2 arg2, T3 arg3, T4 arg4) {}
    }
}