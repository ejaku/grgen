/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.Reporter;

/**
 * Base class for all grgen facilities.
 * This class defines basic facilities and behaviour for all grgen classes.
 */
public class Base implements Id {

	/** static id counter */
	private static long currId = 1;

	/** The id of this object */ 
	private String id;

	/** constants for debug reporting */
	public static final int NOTE = 0;

	/** The debug reporter for debugging */
	public static Reporter debug;
	
	/** The error reporter for error reporting */
	protected static ErrorReporter error;

	/** Are new technology features enabled? */ 
	protected static boolean enableNT;
	
	/** Is debugging enabled. */
	protected static boolean enableDebug;
	
	/**
	 * Set the reporting facilities of the base class
	 * @param debug The debug reporter 
	 * @param error The error reporter
	 */
	public static void setReporters(Reporter debug, ErrorReporter error) {
		Base.debug = debug;
		Base.error = error;
	}

  /**
   * Get a new ID for this object. 
   */
  public Base() {
  	id = "" + currId++;
  }

	/**
	 * @see de.unika.ipd.grgen.util.ID#getId()
	 */
	public String getId() {
		return id;
	}
	
  /**
   * Write a string buffer to a file.
   * @param file The file.
   * @param The character sequence to print (can be a 
   * {@link String} or {@link StringBuffer}
   */
  protected void writeFile(File file, CharSequence cs) {
    try {
      FileOutputStream fos = 
      	new FileOutputStream(file);
      PrintStream ps = new PrintStream(fos);
      
      ps.print(cs);
      fos.close();
    } catch (FileNotFoundException e) {
      error.error(e.getMessage());
    } catch (IOException e) {
      error.error(e.getMessage());
    }
  }

}
