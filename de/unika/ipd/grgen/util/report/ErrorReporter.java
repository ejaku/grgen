/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

/**
 * The error reported class.
 */
public class ErrorReporter extends Reporter {
	
	public final static int ERROR = 1;
	public final static int WARNING = 2;
	public final static int NOTE = 4;
	
	protected static int errCount = 0;
	protected static int warnCount = 0;
	
	private final static String[] levelNames = {
		"error", "warning", "note"
	};
	
	private static String getMsg(int level, String msg) {
		//return levelNames[level] + ": " + msg;
		return msg;
	}
	
	/**
	 * Create a new error reporter.
	 */
	public ErrorReporter() {
		setMask(ERROR | WARNING | NOTE);
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
		report(WARNING, loc, getMsg(WARNING, msg));
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
