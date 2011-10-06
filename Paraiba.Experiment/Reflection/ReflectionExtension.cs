using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Paraiba.Reflection
{
	public static class ReflectionExtension
	{
		public static Func<TResult> ToFunc<TResult>(this MethodInfo m)
		{
			return m.ToDelegate<Func<TResult>>();
		}

		public static Func<T, TResult> ToFunc<T, TResult>(this MethodInfo m)
		{
			return m.ToDelegate<Func<T, TResult>>();
		}

		public static Func<T1, T2, TResult> ToFunc<T1, T2, TResult>(this MethodInfo m)
		{
			return m.ToDelegate<Func<T1, T2, TResult>>();
		}

		public static Func<T1, T2, T3, TResult> ToFunc<T1, T2, T3, TResult>(this MethodInfo m)
		{
			return m.ToDelegate<Func<T1, T2, T3, TResult>>();
		}

		public static Func<T1, T2, T3, T4, TResult> ToFunc<T1, T2, T3, T4, TResult>(this MethodInfo m)
		{
			return m.ToDelegate<Func<T1, T2, T3, T4, TResult>>();
		}

		public static Action ToAction(this MethodInfo m)
		{
			return m.ToDelegate<Action>();
		}

		public static Action<T> ToAction<T>(this MethodInfo m)
		{
			return m.ToDelegate<Action<T>>();
		}

		public static Action<T1, T2> ToAction<T1, T2>(this MethodInfo m)
		{
			return m.ToDelegate<Action<T1, T2>>();
		}

		public static Action<T1, T2, T3> ToAction<T1, T2, T3>(this MethodInfo m)
		{
			return m.ToDelegate<Action<T1, T2, T3>>();
		}

		public static Action<T1, T2, T3, T4> ToAction<T1, T2, T3, T4>(this MethodInfo m)
		{
			return m.ToDelegate<Action<T1, T2, T3, T4>>();
		}

		public static T ToDelegate<T>(this MethodInfo m)
			where T : class
		{
			return Delegate.CreateDelegate(typeof(T), m) as T;
		}

		public static Delegate GetGetDelegate(this PropertyInfo pinfo)
		{
			var minfo = pinfo.GetGetMethod();
			var funcType = typeof(Func<,>).MakeGenericType(minfo.DeclaringType, pinfo.PropertyType);
			return Delegate.CreateDelegate(funcType, minfo);
		}

		public static Func<T, TResult> GetGetFunc<T, TResult>(this PropertyInfo pinfo)
		{
			var minfo = pinfo.GetGetMethod();
			return (Func<T, TResult>)Delegate.CreateDelegate(typeof(Func<T, TResult>), minfo);
		}

		public static Delegate GetSetDelegate(this PropertyInfo pinfo)
		{
			var minfo = pinfo.GetSetMethod();
			var funcType = typeof(Action<,>).MakeGenericType(minfo.DeclaringType, pinfo.PropertyType);
			return Delegate.CreateDelegate(funcType, minfo);
		}

		public static Action<T, TValue> GetSetAction<T, TValue>(this PropertyInfo pinfo)
		{
			var minfo = pinfo.GetSetMethod();
			return (Action<T, TValue>)Delegate.CreateDelegate(typeof(Action<T, TValue>), minfo);
		}
	}
}