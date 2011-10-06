using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Paraiba.Linq;

namespace Paraiba.Xml
{
	public static class XmlUtil
	{
		public static bool EqualsWithElementAndValue(XElement xe1, XElement xe2)
		{
			Contract.Requires(xe1 != null);
			Contract.Requires(xe2 != null);

			if (!xe1.Name.Equals(xe2.Name))
				return false;

			var hasElements = xe1.HasElements;
			if (hasElements != xe2.HasElements)
				return false;

			if (!hasElements)
				return xe1.Value == xe2.Value;

			var es1 = xe1.Elements().ToList();
			var es2 = xe2.Elements().ToList();
			var count = es1.Count;
			if (count != es2.Count)
				return false;

			for (int i = 0; i < count; i++) {
				if (!EqualsWithElementAndValue(es1[i], es2[i]))
					return false;
			}
			return true;
		}
	}
}
