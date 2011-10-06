using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Paraiba.IO
{
	public static class TextReaderExtensions
	{
		/// <summary>
		/// Reads all lines of characters from the current stream and returns the data as a enumerable of strings.
		/// </summary>
		/// <param name="reader">The input stream</param>
		/// <returns>The following all lines from the input stream</returns>
		public static IEnumerable<string> ReadLines(this TextReader reader) {
			string line;
			while ((line = reader.ReadLine()) != null)
				yield return line;
		}

		public static void SkipChars(this TextReader reader, int count) {
			while (--count >= 0) {
				reader.Read();
			}
		}
	}
}