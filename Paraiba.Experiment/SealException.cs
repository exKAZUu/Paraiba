using System;

namespace Paraiba
{
	public class SealException : Exception
	{
		public SealException()
		{
		}

		public SealException(string message) : base(message)
		{
		}
	}
}