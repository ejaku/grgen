/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Stack;

/**
 * Base class for a reporting facility
 */
public abstract class Reporter {

	private int level, levels;
	private Stack zones;
	
	protected List handlers;

  /**
   * Create a new reporter specifiying the number of reporting levels.
   * The current level is set initially to levels, which is
   * the highest reporting level + 1 and therefore reports all messages
   * with a valid level. 
   * @param level Number of reporting levels 
   */
  public Reporter(int levels) {
  	this.level = levels;
  	this.levels = levels;
  	this.handlers = new LinkedList();
  	this.zones = new Stack();
  }
  
  /**
   * Add a handler to this reporter
   * @param handler The handler to add
   */
  public void addHandler(Handler handler) {
  	handlers.add(handler);
  }
  
  /**
   * Remove a handler from this reporter
   * @param handler The handler to remove
   */
  public void removeHandler(Handler handler) {
  	handlers.remove(handler);
  }

	/**
	 * Set the reporting level.
	 * Setting it to 0 will disable all reporting. Basically, all messages 
	 * with reporting level smaller than <code>level</code> will be displayed.
	 * @param level The new level for the reporter.
	 */
	public void setLevel(int level) {
		this.level = level > levels ? levels : level;
	}
	
	/**
	 * Get the current reporting level.
	 * @return The current reporting level. 
	 */
	public int getLevel() {
		return level;
	}
	
	/**
	 * Form a string from a stack trace element.
	 * This method is called from {@link #entering()} to produce the
	 * output string from a stack trace.
	 * @param ste The stack trace.
	 * @return Some string, that should be displayed while entering.
	 */
	protected String getStackTraceString(StackTraceElement ste) {
		String className = ste.getClassName();
		int lastDot = className.lastIndexOf('.');
		if(lastDot != -1)
			className = className.substring(lastDot + 1);
			
		return className + "." + ste.getMethodName();
	}
	
	/**
	 * Get the current zone.
	 * Get the current zone which was entered using 
	 * {@link entering(String s)}
	 * @return The current zone string or "" if no zone is entered currently.
	 */
	public String currZone() {
		return zones.size() > 0 ? (String) zones.peek() : "";
	}
	
	/**
	 * Disbales reporting on this reporter.
	 * Re-enable it by setting the level to some value > 0
	 */
	public void disable() {
		setLevel(0);
	}
	
	/**
	 * Check whether this reporter is disabled
	 * @return true, if no message will be reported, false otherwise.
	 */
	public boolean isDisabled() {
		return getLevel() == 0;
	}
	
	/**
	 * Checks, whether a message supplied with this level will be reported 
	 * @param channel The channel to check
	 * @return true, if the message would be reported, false if not.
	 */
	public boolean willReport(int level) {
		return level < getLevel();
	}

	public void report(int level, Location loc, String msg) {
		if(willReport(level)) {
			Iterator it = handlers.iterator();
			while(it.hasNext()) {
				Handler h = (Handler) it.next();
				h.report(level, loc, msg);
			}
		}
	}
		
	public void report(int channel, String msg) {
		if(willReport(channel)) {
			Iterator it = handlers.iterator();
			while(it.hasNext()) {
				Handler h = (Handler) it.next();
				h.report(channel, EmptyLocation.getEmptyLocation(), msg);
			}
		}
	}
	
	/**
	 * Enter a reporting zone.
	 * The zones are organized on a stack, so calling 
	 * {@link #leaving()} will return to the zone one was before.
	 * @param s The name of the zone to enter.
	 */
	public void entering(String s) {
		Iterator it = handlers.iterator();
		while(it.hasNext()) {
			Handler h = (Handler) it.next();
			h.entering(s);
		}
		zones.push(s);
	}
	
	/**
	 * Enter with the caller method's name.
	 */
	public void entering() {
		StackTraceElement ste = (new Exception().getStackTrace())[1];
		
		entering(getStackTraceString(ste));
	}

	/**
	 * Leave to current reporting zone.
	 */
	public void leaving() {
		if(zones.size() > 0) {
			zones.pop();
  		Iterator it = handlers.iterator();
	  	while(it.hasNext()) {
		  	Handler h = (Handler) it.next();
			  h.leaving();
			}
		}
	}
}

