/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

import java.util.Stack;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * A debug message reporter.
 */
public class DebugReporter extends Reporter {

	Pattern pattern;
	Matcher matcher;
	
	boolean inclusive;
	
	Stack enableStack;
	
  /**
   * Make a new debug reporter.
   * @param maxChannel All channel including this are enabled.
   */
  public DebugReporter(int maxChannel) {
    super(maxChannel);
    pattern = Pattern.compile(".*");
    matcher = pattern.matcher("");
    inclusive = true;
    enableStack = new Stack();
  }

	/**
	 * Set the class filter.
	 * The class filter is a regular expression. Each class calling
	 * this debug reporter is matched against this regex. Only if the
	 * regex matches, the message is reported.
	 * @param it An iterator iterating over string objects. 
	 */
	public void setFilter(String regex) {
		pattern = Pattern.compile(regex);
		matcher = pattern.matcher("");
	}
	
	/**
	 * Determines the meaning of the filter.
	 * If <code>value</code> is true, than all debug zones matching
	 * the filter are reported, all other are ignored. If set to false,
	 * All debug zones not matching the filter are entered, the others 
	 * are ignored.
	 * @param value Inclusive or exclusive filtering.
	 */
	public void setFilterInclusive(boolean value) {
		inclusive = value;
	}

	/**
	 * Enter the debug zone.
	 * If this zone is not filtered with the specified filter (see 
	 * {@link #setFilter(String)}, true is pushed on the enabled stack,
	 * otherwise false. 
	 * @see de.unika.ipd.grgen.util.report.Reporter#entering(java.lang.String)
	 */
	public void entering(String s) {
		matcher.reset(s);
		boolean matched = matcher.matches();
		boolean enabled = inclusive ? matched : !matched;
		enableStack.push(new Boolean(enabled));
		
		if(enabled)
			super.entering(s); 
	}
	
	/**
	 * Leave the debug zone.
	 * Pop the enabled flag from the enabled stack.
	 * @see de.unika.ipd.grgen.util.report.Reporter#leaving()
	 */
	public void leaving() {
		boolean enabled = true;
		
		if(enableStack.size() > 0) {
			enabled = ((Boolean) enableStack.peek()).booleanValue();
			enableStack.pop();
		}
		
		if(enabled)
			super.leaving();
	}
  
  /**
   * This debug reporter will only report, if the zone was entered 
   * with a string, that matched the filter. This is indicated using  
   * the enable stack.
   * @see de.unika.ipd.grgen.util.report.Reporter#willReport(int)
   */
  public boolean willReport(int level) {
  	boolean valid = true;
  	if(enableStack.size() > 0)
  		valid = ((Boolean) enableStack.peek()).booleanValue();
		return valid && super.willReport(level);
  }

}
