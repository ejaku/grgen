/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

/**
 * 
 */
public class ErrorReporter extends Reporter {

	public final static int ERROR = 0;
	public final static int WARNING = 1;
	public final static int NOTE = 2;

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
  
  
  
  public void error(Location loc, String msg) {
  	report(ERROR, loc, getMsg(ERROR, msg));
  }
  
  public void error(String msg) {
  	report(ERROR, getMsg(ERROR, msg));
  }
  
  public void warning(Location loc, String msg) {
  	report(WARNING, getMsg(WARNING, msg));
  }
  
  public void warning(String msg) {
  	report(WARNING, getMsg(WARNING, msg));
  }
  
  public void note(Location loc, String msg) {
  	report(NOTE, loc, getMsg(NOTE, msg));
  }
  
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
}
