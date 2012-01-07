#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
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
using System.Linq.Expressions;
using Binary =
        System.Func<System.Linq.Expressions.ParameterExpression,
                System.Linq.Expressions.ParameterExpression,
                System.Linq.Expressions.BinaryExpression>;
using Unary =
        System.Func<System.Linq.Expressions.ParameterExpression,
                System.Linq.Expressions.UnaryExpression>;

namespace Paraiba.Linq.Expressions {
    /// <summary>
    ///   動的に型 T の加減乗除関数を作る。
    /// </summary>
    /// <typeparam name="T"> 対象となる型。 </typeparam>
    public static class Operator<T> {
        private static readonly ParameterExpression x =
                Expression.Parameter(typeof(T), "x");

        private static readonly ParameterExpression y =
                Expression.Parameter(typeof(T), "y");

        public static readonly Func<T, T, T> Add = Lambda(Expression.Add);

        public static readonly Func<T, T, T> Subtract =
                Lambda(Expression.Subtract);

        public static readonly Func<T, T, T> Multiply =
                Lambda(Expression.Multiply);

        public static readonly Func<T, T, T> Divide = Lambda(Expression.Divide);
        public static readonly Func<T, T, T> Modulo = Lambda(Expression.Modulo);
        public static readonly Func<T, T> Plus = Lambda(Expression.UnaryPlus);
        public static readonly Func<T, T> Negate = Lambda(Expression.Negate);

        public static Func<T, T, T> Lambda(Binary op) {
            return Expression.Lambda<Func<T, T, T>>(op(x, y), x, y).Compile();
        }

        public static Func<T, T> Lambda(Unary op) {
            return Expression.Lambda<Func<T, T>>(op(x), x).Compile();
        }
    }
}