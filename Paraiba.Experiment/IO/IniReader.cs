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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Paraiba.IO {
    public class IniReader {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)] private readonly
                Dictionary<string, Dictionary<string, string>> _dic;

        public IniReader(string filename, Encoding encoding) {
            _dic = new Dictionary<string, Dictionary<string, string>>();
            string sectionName = null;
            Dictionary<string, string> section = null;

            using (var reader = new StreamReader(filename, encoding)) {
                string line = null;
                while ((line = reader.ReadLine()) != null) {
                    line = line.Trim();
                    int commentIndex = line.IndexOf('#');
                    if (commentIndex >= 0) {
                        line = line.Substring(0, commentIndex);
                    }
                    if (line.Length == 0) {
                        continue;
                    }
                    if (line[0] == '[' && line[line.Length - 1] == ']') {
                        if (sectionName != null) {
                            _dic[sectionName] = section;
                        }
                        sectionName = line.Substring(1, line.Length - 2);
                        if (_dic.TryGetValue(sectionName, out section) == false) {
                            section = new Dictionary<string, string>();
                        }
                    } else {
                        try {
                            string[] pair = line.Split('=');
                            switch (pair.Length) {
                            case 1:
                                section[line] = null;
                                break;
                            case 2:
                                section[pair[0]] = pair[1];
                                break;
                            default:
                                throw new FormatException("line : " + line);
                            }
                        } catch (NullReferenceException e) {
                            throw new FormatException("No Section", e);
                        }
                    }
                } // close file
            }
        }
    }
}