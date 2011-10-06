using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Paraiba.IO
{
	public static class TextWriterExtensions
	{
		public static void WriteFromStream(this TextWriter writer, TextReader reader)
		{
			int c;
			while((c = reader.Read()) >= 0)
			{
				writer.Write((char)c);
			}
		}
	}
}