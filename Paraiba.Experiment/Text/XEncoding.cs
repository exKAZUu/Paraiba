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

using System.Text;

namespace Paraiba.Text {
	public class XEncoding {
		private static Encoding _sjis;
		private static Encoding _eucJP;
		private static Encoding _jis;

		public static Encoding SJIS {
			get {
				if (_sjis == null) {
					_sjis = Encoding.GetEncoding("shift_jis");
				}
				return _sjis;
			}
		}

		public static Encoding EUC_JP {
			get {
				if (_eucJP == null) {
					_eucJP = Encoding.GetEncoding("euc-jp");
				}
				return _eucJP;
			}
		}

		public static Encoding JIS {
			get {
				if (_jis == null) {
					_jis = Encoding.GetEncoding("iso-2022-jp");
				}
				return _jis;
			}
		}
	}
}