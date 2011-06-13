/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Util.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.io.PrintStream;
import java.lang.reflect.Method;
import java.util.Vector;

import de.unika.ipd.grgen.util.report.ErrorReporter;

public class Util
{
	/**
	 * Removes from a filename the prefix that contains path information
	 *
	 * @param    filename	a filename
	 *
	 * @return   the filename without leading path
	 */
	public static String removePathPrefix(String filename)
	{
		int lastSepPos = filename.lastIndexOf(File.separatorChar);

		if (lastSepPos < 0) return filename;

		return filename.substring(lastSepPos + 1);
	}

	/**
	 * Removes from a filename the suffix that contains file type information,
	 * '.grg' for example.
	 *
	 * @param	filename 	a filename
	 * @param	suffix 		file type suffix without the dot
	 * 						(e.g., "exe", but not ".exe")
	 *
	 * @return   the filename without the given suffix and the seperating dot;
	 * 			 if the given suffix is not there <code>filename</code> is returned.
	 */
	public static String removeFileSuffix(String filename, String suffix)
	{
		int lastDotPos = filename.lastIndexOf('.');

		if (lastDotPos < 0) return filename;

		if (lastDotPos == filename.length()-1) {
			if ( ! suffix.equals("") ) return filename;
			else return filename.substring(0, lastDotPos);
		}

		if (filename.substring(lastDotPos+1).equals(suffix))
			return filename.substring(0, lastDotPos);

		return filename;
	}

	/**
	 * Creates a action name only consisting of characters, numbers, and '_'
	 * from a given filename.
	 *
	 * @param filename 		The filename to create the action name from.
	 *
	 * @return the action name corresponding to the filename.
	 */
	public static String getActionsNameFromFilename(String filename) {
		String name = Util.removePathPrefix(Util.removeFileSuffix(filename, "grg"));
		name = name.replaceAll("[^a-zA-Z0-9_]", "_");
		char firstChar = name.charAt(0);
		if(firstChar >= '0' && firstChar <= '9')
			name = "_" + name;
		return name;
	}

	/**
	 * Checks if the given filename can be used as a valid action name.
	 * Stricter than getActionsNameFromFilename, .NET can't handle the rewritten version,
	 * if the rewritten version is needed for plain old GrGen, then limit this check to GrGen.NET.
	 * */
	public static boolean isFilenameValidActionName(String filename) {
		String name = Util.removePathPrefix(Util.removeFileSuffix(filename, "grg"));
		return !name.matches("[^a-zA-Z0-9_]");
	}

	public static File findFile(File[] paths, String file) {
		for(int i = 0; i < paths.length; i++) {
			File curr = new File(paths[i], file);
			if(curr.exists())
				return curr;
		}

		return null;
	}

	private static final char[] hexChars = "0123456789abcdef".toCharArray();

	public static String hexString(byte[] arr) {
		StringBuffer sb = new StringBuffer();

		for(int i = 0; i < arr.length; i++) {
			byte b = arr[i];

			sb.append(hexChars[b & 0xf]);
			sb.append(hexChars[(b >>> 4) & 0xf]);
		}

		return sb.toString();
	}

	/**
	 * Write a string buffer to a file.
	 * @param file The file.
	 * @param The character sequence to print (can be a
	 * {@link String} or {@link StringBuffer}
	 */
  public static void writeFile(File file, CharSequence cs, ErrorReporter reporter) {
		try {
			BufferedOutputStream bos =
				new BufferedOutputStream(new FileOutputStream(file));
			PrintStream ps = new PrintStream(bos);
			ps.print(cs);
			ps.close();

		} catch(FileNotFoundException e) {
			reporter.error(e.toString());
		}
  }

	public static PrintStream openFile(File file, ErrorReporter reporter) {
		OutputStream os = NullOutputStream.STREAM;

		try {
			os = new BufferedOutputStream(new FileOutputStream(file));

		} catch(FileNotFoundException e) {
			reporter.error(e.toString());
		}

		return new PrintStream(os);
	}

	public static void closeFile(PrintStream ps) {
		ps.flush();
		ps.close();
	}

	/**
	 * Tells whether c1 is subclass of c2.
	 */
	public static boolean isSubClass(Class<?> c1, Class<?> c2)
	{
		for (Class<?> c = c1; c != Object.class; c = c.getSuperclass())
			if (c == c2) return true;

		return false;
	}
	/**
	 * Tells whether a given class contains a given method
	 * @param c The class object
	 * @param m The Name of the method
	 */
	public static boolean containsMethod(Class<?> c, String m)
	{
		Vector<Method> allMethods = new Vector<Method>();
		for (Method mm: c.getMethods()) allMethods.add(mm);

		try	{
			return allMethods.contains(c.getMethod(m));
		}
		catch (Exception e) { return false; }
	}
	/**
	 * Get a comma separated list of strings characterising the kinds of
	 * the given class objects.
	 * @param classes The class objects
	 * @param sc A class all the given classes must be subclass of
	 * @param m The Name of the method
	 */

	public static String getStrList(Class<?>[] classes, Class<?> sc, String m)
	{
		StringBuffer res = new StringBuffer();
		boolean first = true;

		for (Class<?> c: classes) {
			if ( !first ) res.append(", ");
			try {
				if (
					isSubClass(c, sc) &&
					containsMethod(c, m) &&
					c.getMethod("m").getReturnType() == String.class
				)
					res.append((String) c.getMethod(m).invoke(null));
				else
					res.append("<invalid>");
			}
			catch(Exception e) { res.append("<invalid>"); }
			first = false;
		}
		return res.toString();
	}

	/**
	 * Get a comma separated list of strings characterising the kinds of
	 * the given class objects. Between the last two entries there is an 'or'.
	 */
	public static String getStrListWithOr(Class<?>[] classes, Class<?> sc, String m)
	{
		StringBuffer res = new StringBuffer();
		// TODO use or remove it
		// boolean first = true;

		int l = classes.length;

		for (int i = 0; i < l; i++) {
			try {
				Class<?> c = classes[i];
				if ( i == l - 1 && l > 1 ) res.append(" or ");
				else if ( i > 0 && l > 2 ) res.append(", ");

				if ( isSubClass(c, sc) && containsMethod(c, m) )
					if ( c.getMethod(m).getReturnType() == String.class ) {
						res.append( (String) c.getMethod(m).invoke(null) );
						continue;
					}

				res.append("<invalid>");
			}
			catch(Exception e) { res.append("<invalid>"); }
		}
		return res.toString();
	}

	/** return result string of invoking method m on c */
	public static String getStr(Class<?> c, Class<?> sc, String m)
	{
		try {
			if(isSubClass(c, sc) && containsMethod(c, m)) {
				if(c.getMethod(m).getReturnType() == String.class) {
					String str = (String) c.getMethod(m).invoke(null);
					if(str.equals("base node"))
						str += " <" + c.toString() + ">";

					return str;
				}
			}
			return "<invalid>";
		}
		catch(Exception e)
		{
			return "<invalid>";
		}
	}

	public static String toString(StreamDumpable dumpable) {
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(bos);
		dumpable.dump(ps);
		ps.flush();
		ps.close();
		return bos.toString();
	}
}

