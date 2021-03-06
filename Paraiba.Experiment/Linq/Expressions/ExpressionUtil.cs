﻿#region License

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
using System.Linq;
using System.Linq.Expressions;

namespace Paraiba.Linq.Expressions {
    public static class ExpressionUtil {
        public static Expression<Func<T, bool>> True<T>() {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>() {
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2) {
            var invokedExpr = Expression.Invoke(
                expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (
                Expression.OrElse(expr1.Body, invokedExpr),
                expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2) {
            var invokedExpr = Expression.Invoke(
                expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
            (
                Expression.AndAlso(expr1.Body, invokedExpr),
                expr1.Parameters);
        }

        /// <summary>
        ///   式木の構造が一致してれば、少なくとも ToString の結果は一致するので、 それで2つの式木の一致性を判定。
        /// </summary>
        public static void SimpleCheck(Expression e1, Expression e2) {
            if (e1.ToString() != e2.ToString()) {
                Console.Write("not match: {0}, {1}\n", e1, e2);
            }
        }
    }
}