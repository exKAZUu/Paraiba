using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Paraiba.IO
{
	public static class DirectoryInfoExtensions
	{
		public static void CreateIfNotExist(this DirectoryInfo directoryInfo)
		{
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
		}

		public static void DeleteIfNotExist(this DirectoryInfo directoryInfo)
		{
			if (!directoryInfo.Exists)
			{
				directoryInfo.Delete();
			}
		}

		public static void DeleteIfNotExist(this DirectoryInfo directoryInfo, bool recursive)
		{
			if (!directoryInfo.Exists)
			{
				directoryInfo.Delete(recursive);
			}
		}

		public static string GetFullPath(this DirectoryInfo directoryInfo)
		{
			return directoryInfo.FullName;
		}

		public static string GetFullPath(this DirectoryInfo directoryInfo, params string[] subDirectoryNames)
		{
			return subDirectoryNames.Aggregate(directoryInfo.FullName, Path.Combine);
		}

		public static DirectoryInfo GetSubDirectory(this DirectoryInfo directoryInfo, params string[] subDirectoryNames)
		{
			return new DirectoryInfo(directoryInfo.GetFullPath(subDirectoryNames));
		}

		public static FileInfo GetSubFile(this DirectoryInfo directoryInfo, string subFileName)
		{
			return new FileInfo(Path.Combine(directoryInfo.GetFullPath(), subFileName));
		}

		public static FileInfo GetSubFile(this DirectoryInfo directoryInfo, params string[] subNames)
		{
			return new FileInfo(directoryInfo.GetFullPath(subNames));
		}
	}
}