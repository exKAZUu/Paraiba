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

using System.Collections.Generic;
using System.IO;

namespace Paraiba.IO {
    public static class TextReaderExtensions {
        /// <summary>
        ///   Reads all lines of characters from the current stream and returns the data as a enumerable of strings.
        /// </summary>
        /// <param name="reader"> The input stream </param>
        /// <returns> The following all lines from the input stream </returns>
        public static IEnumerable<string> ReadLines(this TextReader reader) {
            string line;
            while ((line = reader.ReadLine()) != null) {
                yield return line;
            }
        }

        public static void SkipChars(this TextReader reader, int count) {
            while (--count >= 0) {
                reader.Read();
            }
        }
    }
}