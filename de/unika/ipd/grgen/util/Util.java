/**
 * Util.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;

public class Util {
	
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
			FileOutputStream fos =
				new FileOutputStream(file);
			PrintStream ps = new PrintStream(fos);
			
			ps.print(cs);
			fos.close();
			
		} catch(FileNotFoundException e) {
			reporter.error(e.toString());
		} catch(IOException e) {
			reporter.error(e.toString());
		}
  }
}

