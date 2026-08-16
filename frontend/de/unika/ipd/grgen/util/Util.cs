/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Util.java
/// 
/// @author Created by Omnicore CodeGuide
/// </summary>

namespace de.unika.ipd.grgen.util
{

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	public class Util
	{
		/// <summary>
		/// Removes from a filename the prefix that contains path information </summary>
		/// <param name="filename">	a filename </param>
		/// <returns>   the filename without leading path </returns>
		public static string RemovePathPrefix(string filename)
		{
			int lastSepPos = filename.LastIndexOf(Path.DirectorySeparatorChar);

			if(lastSepPos < 0)
				return filename;

			return filename.Substring(lastSepPos + 1);
		}

		/// <summary>
		/// Removes from a filename the suffix that contains file type information,
		/// '.grg' for example.
		/// @param	filename 	a filename
		/// @param	suffix 		file type suffix without the dot
		/// 						(e.g., "exe", but not ".exe") </summary>
		/// <returns>   the filename without the given suffix and the seperating dot;
		/// 			 if the given suffix is not there <code>filename</code> is returned. </returns>
		public static string RemoveFileSuffix(string filename, string suffix)
		{
			int lastDotPos = filename.LastIndexOf('.');

			if(lastDotPos < 0)
				return filename;

			if(lastDotPos == filename.Length - 1)
			{
				if(!suffix.Equals(""))
					return filename;
				else
					return filename.Substring(0, lastDotPos);
			}

			if(filename.Substring(lastDotPos + 1).Equals(suffix))
				return filename.Substring(0, lastDotPos);

			return filename;
		}

		/// <summary>
		/// Creates a action name only consisting of characters, numbers, and '_'
		/// from a given filename. </summary>
		/// <param name="filename"> 		The filename to create the action name from. </param>
		/// <returns> the action name corresponding to the filename. </returns>
		public static string GetActionsNameFromFilename(string filename)
		{
			string name = Util.RemovePathPrefix(Util.RemoveFileSuffix(filename, "grg"));
			name = name.ReplaceAll("[^a-zA-Z0-9_]", "_");
			char firstChar = name[0];
			if(firstChar >= '0' && firstChar <= '9')
				name = "_" + name;
			return name;
		}

		/// <summary>
		/// Checks if the given filename can be used as a valid action name.
		/// Stricter than getActionsNameFromFilename, .NET can't handle the rewritten version,
		/// if the rewritten version is needed for plain old GrGen, then limit this check to GrGen.NET.
		/// 
		/// </summary>
		public static bool IsFilenameValidActionName(string filename)
		{
			string name = Util.RemovePathPrefix(Util.RemoveFileSuffix(filename, "grg"));
			return !name.Matches("[^a-zA-Z0-9_]");
		}

		public static File FindFile(File[] paths, string file)
		{
			for(int i = 0; i < paths.Length; i++)
			{
				File curr = new File(paths[i], file);
				if(curr.Exists())
					return curr;
			}

			return null;
		}

		private static readonly char[] hexChars = "0123456789abcdef".ToCharArray();

		public static string HexString(sbyte[] arr)
		{
			StringBuilder sb = new StringBuilder();

			for(int i = 0; i < arr.Length; i++)
			{
				sbyte b = arr[i];

				sb.Append(hexChars[b & 0xf]);
				sb.Append(hexChars[(b >>> 4) & 0xf]);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Write a string buffer to a file. </summary>
		/// <param name="file"> The file. </param>
		/// <param name="The"> character sequence to print (can be a
		/// <seealso cref="string"/> or <seealso cref="System.Text.StringBuilder"/> </param>
		public static void WriteFile(File file, CharSequence cs, ErrorReporter reporter)
		{
			try
			{
				using(BufferedOutputStream bos = new BufferedOutputStream(new FileStream(file, FileMode.Create, FileAccess.Write)))
				{
					using(PrintStream ps = new PrintStream(bos))
					{
						ps.Print(cs);
					}
				}
			}
			catch(IOException e)
			{
				reporter.Error(e.ToString());
			}
		}

		public static PrintStream OpenFile(File file, ErrorReporter reporter)
		{
			Stream os = NullOutputStream.STREAM;

			try
			{
				os = new BufferedOutputStream(new FileStream(file, FileMode.Create, FileAccess.Write));
			}
			catch(FileNotFoundException e)
			{
				reporter.Error(e.ToString());
			}

			return new PrintStream(os);
		}

		public static void CloseFile(PrintStream ps)
		{
			ps.Flush();
			ps.close();
		}

		/// <summary>
		/// Tells whether c1 is subclass of c2.
		/// </summary>
		public static bool IsSubClass(Type c1, Type c2)
		{
			for(Type c = c1; c != typeof(object); c = c.BaseType)
			{
				if(c == c2)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Tells whether a given class contains a given method </summary>
		/// <param name="c"> The class object </param>
		/// <param name="m"> The Name of the method </param>
		public static bool ContainsMethod(Type c, string m)
		{
			IList<System.Reflection.MethodInfo> allMethods = new List<System.Reflection.MethodInfo>();
			foreach(System.Reflection.MethodInfo mm in c.GetMethods())
				allMethods.Add(mm);

			try
			{
				return allMethods.Contains(c.GetMethod(m));
			}
			catch(Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// Get a comma separated list of strings characterising the kinds of
		/// the given class objects. </summary>
		/// <param name="classes"> The class objects </param>
		/// <param name="sc"> A class all the given classes must be subclass of </param>
		/// <param name="m"> The Name of the method </param>
		public static string GetStrList(Type[] classes, Type sc, string m)
		{
			StringBuilder res = new StringBuilder();
			bool first = true;

			foreach(Type c in classes)
			{
				if(!first)
					res.Append(", ");
				try
				{
					if(IsSubClass(c, sc) &&
							ContainsMethod(c, m) &&
							c.GetMethod("m").ReturnType == typeof(string))
					{
						res.Append((string)c.GetMethod(m).Invoke(null));
					}
					else
						res.Append("<invalid>");
				}
				catch(Exception)
				{
					res.Append("<invalid>");
				}
				first = false;
			}
			return res.ToString();
		}

		/// <summary>
		/// Get a comma separated list of strings characterising the kinds of
		/// the given class objects. Between the last two entries there is an 'or'.
		/// </summary>
		public static string GetStrListWithOr(Type[] classes, Type sc, string m)
		{
			StringBuilder res = new StringBuilder();

			int length = classes.Length;

			for(int i = 0; i < length; i++)
			{
				try
				{
					Type c = classes[i];
					if(i == length - 1 && length > 1) // or as separator before last entry
						res.Append(" or ");
					else if(i > 0 && length > 2) // , as separator before other entries (none before first)
						res.Append(", ");

					if(IsSubClass(c, sc) && ContainsMethod(c, m))
					{
						if(c.GetMethod(m).ReturnType == typeof(string))
						{
							res.Append((string)c.GetMethod(m).Invoke(null));
							continue;
						}
					}

					res.Append("<invalid>");
				}
				catch(Exception)
				{
					res.Append("<invalid>");
				}
			}
			return res.ToString();
		}

		/// <summary>
		/// return result string of invoking method m on c </summary>
		public static string GetStr(Type c, Type sc, string m)
		{
			try
			{
				if(IsSubClass(c, sc) && ContainsMethod(c, m))
				{
					if(c.GetMethod(m).ReturnType == typeof(string))
					{
						string str = (string)c.GetMethod(m).Invoke(null);
						if(str.Equals("base node"))
							str += " <" + c.ToString() + ">";

						return str;
					}
				}
				return "<invalid>";
			}
			catch(Exception)
			{
				return "<invalid>";
			}
		}

		public static string toString(StreamDumpable dumpable)
		{
			try
			{
				using(MemoryStream bos = new MemoryStream())
				{
					using(PrintStream ps = new PrintStream(bos))
					{
						dumpable.Dump(ps);
						ps.Flush();
						return bos.ToString();
					}
				}
			}
			catch(IOException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				return "";
			}
		}

		public static void CopyFile(File sourceFile, File targetFile)
		{
			if(!targetFile.Exists())
				targetFile.CreateNewFile();

			using(FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
			{
				using(FileChannel sourceChannel = sourceStream.GetChannel())
				{
					using(FileStream targetStream = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
					{
						using(FileChannel targetChannel = targetStream.GetChannel())
						{
							long count = 0;
							long size = sourceChannel.Size();
							while((count += targetChannel.TransferFrom(sourceChannel, count, size - count)) < size)
							{
								// empty
							}
						}
					}
				}
			}
		}
	}

}
