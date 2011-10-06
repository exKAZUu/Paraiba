using System;

namespace Paraiba.Monads
{
	public interface IMonad<T>
	{
		IMonad<T2> Bind<T2>(Func<T, IMonad<T2>> func);
		IMonad<T> Return(T value);
	}
}