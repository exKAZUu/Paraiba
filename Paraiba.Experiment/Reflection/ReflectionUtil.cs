using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Paraiba.Reflection
{
	public static class ReflectionUtil
	{
		public static void SetProperty<TObject, TValue>(Expression<Func<TObject, TValue>> expression, TObject obj, TValue value)
		{
			Contract.Requires(expression.Body is MemberExpression);
			Contract.Requires(((MemberExpression)expression.Body).Member is PropertyInfo);

			var info = (PropertyInfo)((MemberExpression)expression.Body).Member;
			info.SetValue(obj, value, null);
		}
	}
}
