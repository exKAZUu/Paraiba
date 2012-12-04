using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Core
{
	/// <summary>
	/// A utility class for enhancing <see cref="Environment" />.
	/// </summary>
	public static class EnvironmentUtils
	{
		/// <summary>
		/// Returns the boolean whether this program runs on Mono or not.
		/// </summary>
		/// <returns>The boolean whether this program runs on Mono or not.</returns>
		public static bool OnMono() {
			int p = (int) Environment.OSVersion.Platform;
			return (p == 4) || (p == 6) || (p == 128);
		}
	}
}
