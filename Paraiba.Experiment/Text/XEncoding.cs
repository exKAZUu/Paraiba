using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Text
{
	public class XEncoding
	{
		private static Encoding _sjis;
		private static Encoding _eucJP;
		private static Encoding _jis;

		public static Encoding SJIS
		{
			get {
				if (_sjis == null)
					_sjis = Encoding.GetEncoding("shift_jis");
				return _sjis;
			}
		}

		public static Encoding EUC_JP
		{
			get
			{
				if (_eucJP == null)
					_eucJP = Encoding.GetEncoding("euc-jp");
				return _eucJP;
			}
		}

		public static Encoding JIS
		{
			get
			{
				if (_jis == null)
					_jis = Encoding.GetEncoding("iso-2022-jp");
				return _jis;
			}
		}
	}
}