/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

/**
 * The error reported class.
 */
public class ErrorReporter extends Reporter {

	public final static int ERROR = 0;
	public final static int WARNING = 1;
	public final static int NOTE = 2;
	
	protected static int errCount = 0; 
	protected static int warnCount = 0; 

	private final static String[] levelNames = {
		"error", "warning", "note"
	};
	
	private static String getMsg(int level, String msg) {
		return levelNames[level] + ": " + msg;
	}

  /**
   * Create a new error reporter. 
   */
  public ErrorReporter() {
    super(3);
  }
  
  
  /**
   * Report an error at a given location.
   * 
   * @param loc The location.
   * @param msg The error message.
   */
  public void error(Location loc, String msg) {
  	report(ERROR, loc, getMsg(ERROR, msg));
  	++errCount;
  }
  
  /**
   * Report an error.
   * @param msg
   */
  public void error(String msg) {
  	report(ERROR, getMsg(ERROR, msg));
	  ++errCount;
  }
  
  /**
   * Report a warning at a given location.
   * 
   * @param loc The location.
   * @param msg The warning message.
   */
  public void warning(Location loc, String msg) {
  	report(WARNING, getMsg(WARNING, msg));
		++warnCount;
  }

	/**
	 * report a warning.
   * @param msg The warning message.
	 */  
  public void warning(String msg) {
  	report(WARNING, getMsg(WARNING, msg));
  	++warnCount;
  }

	/**
	 * Report a note at a given location.
	 * 
   * @param loc The location.
   * @param msg The note message.
	 */  
  public void note(Location loc, String msg) {
  	report(NOTE, loc, getMsg(NOTE, msg));
  }
  
  /**
   * Report a note.
   * @param msg The note message.
   */
  public void note(String msg) {
  	report(NOTE, getMsg(NOTE, msg));
  }
  
  /**
   * Enter is disabled in this reporter. Error reporting is plain 
   * not nested.
   */
  public void entering(String s) {
  }

  /**
   * Leave is also disabled.
   */
  public void leaving() {
  }
  
  /**
   * Returns the number of occured errors.
   * @return
   */
  public static int getErrorCount() {
		return errCount;
  }

  /**
   * Returns the number of occured warnings.
   * @return
   */
  public static int getWarnCount() {
		return warnCount;
  }
  
}
