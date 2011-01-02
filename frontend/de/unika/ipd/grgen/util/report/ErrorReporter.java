/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

	public static final int ERROR = 1;
	public static final int WARNING = 2;
	public static final int NOTE = 4;

	protected static int errCount = 0;
	protected static int warnCount = 0;

	// TODO use or remove it
	/*private static final String[] levelNames = {
		"error", "warning", "note"
	};*/

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
