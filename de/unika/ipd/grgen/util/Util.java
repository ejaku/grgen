/**
 * Util.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import java.io.File;

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
	
}

