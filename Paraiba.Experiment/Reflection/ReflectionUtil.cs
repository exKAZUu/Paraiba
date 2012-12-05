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
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace Paraiba.Reflection {
	public static class ReflectionUtil {
		public static void SetProperty<TObject, TValue>(
				Expression<Func<TObject, TValue>> expression, TObject obj,
				TValue value) {
			Contract.Requires(expression.Body is MemberExpression);
			Contract.Requires(
					((MemberExpression)expression.Body).Member is PropertyInfo);

			var info = (PropertyInfo)((MemberExpression)expression.Body).Member;
			info.SetValue(obj, value, null);
		}
	}
}