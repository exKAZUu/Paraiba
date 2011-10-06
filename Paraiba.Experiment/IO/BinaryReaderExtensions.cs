using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Paraiba.IO
{
	public static class BinaryReaderExtensions
	{
		public static void SkipBytes(this BinaryReader reader, int count)
		{
			while (--count >= 0) {
				reader.Read();
			}
		}
	}
}