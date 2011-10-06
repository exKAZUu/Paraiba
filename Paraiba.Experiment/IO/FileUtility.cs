using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Paraiba.IO {
	public static class FileUtility {
		public static void CopyRecursively(string srcPath, string dstPath) {
			Contract.Requires(Directory.Exists(srcPath));
			var files = Directory.GetFiles(srcPath);
			Directory.CreateDirectory(dstPath);
			foreach (var file in files) {
				File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)), true);
			}
			var dirs = Directory.GetDirectories(srcPath);
			foreach (var dir in dirs) {
				CopyRecursively(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
			}
		}
	}
}
