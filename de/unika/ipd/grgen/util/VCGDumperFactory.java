/**
 * VCGDUmperFactory.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;
import java.io.File;
import java.io.FileOutputStream;
import java.io.PrintStream;
import java.io.FileNotFoundException;



public class VCGDumperFactory implements GraphDumperFactory {
	
	public GraphDumper get(File f) {
		
		try {
			FileOutputStream fos = new FileOutputStream(f);
			PrintStream ps = new PrintStream(fos);
			return new VCGDumper(ps);
		} catch (FileNotFoundException e) {
			e.printStackTrace(System.err);
		}
		
		return null;
	}
	
}

