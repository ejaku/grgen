/**
 * Dumpable.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;


import java.io.PrintStream;

/**
 * Something that can dump itself onto a print stream.
 */
public interface StreamDumpable {
	
	void dump(PrintStream ps);
	
}

