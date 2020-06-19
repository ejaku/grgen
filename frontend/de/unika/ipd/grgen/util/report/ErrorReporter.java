/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util.report;

/**
 * The error reported class.
 */
public class ErrorReporter extends Reporter
{
	public static final int ERROR = 1;
	public static final int WARNING = 2;
	public static final int NOTE = 4;

	protected static int errCount = 0;
	protected static int warnCount = 0;

	/**
	 * Create a new error reporter.
	 */
	public ErrorReporter()
	{
		setMask(ERROR | WARNING | NOTE);
	}

	/**
	 * Report an error at a given location.
	 *
	 * @param loc The location.
	 * @param msg The error message.
	 */
	public void error(Location loc, String msg)
	{
		if(msg.equals("mismatched input '$' expecting RPAREN"))
			report(ERROR, loc, msg + " -- forgot \"@\"?");
		else
			report(ERROR, loc, msg);
		++errCount;
	}

	/**
	 * Report an error.
	 * @param msg
	 */
	public void error(String msg)
	{
		report(ERROR, msg);
		++errCount;
	}

	/**
	 * Report a warning at a given location.
	 *
	 * @param loc The location.
	 * @param msg The warning message.
	 */
	public void warning(Location loc, String msg)
	{
		report(WARNING, loc, msg);
		++warnCount;
	}

	/**
	 * report a warning.
	 * @param msg The warning message.
	 */
	public void warning(String msg)
	{
		report(WARNING, msg);
		++warnCount;
	}

	/**
	 * Report a note at a given location.
	 *
	 * @param loc The location.
	 * @param msg The note message.
	 */
	public void note(Location loc, String msg)
	{
		report(NOTE, loc, msg);
	}

	/**
	 * Report a note.
	 * @param msg The note message.
	 */
	public void note(String msg)
	{
		report(NOTE, msg);
	}

	/**
	 * Returns the number of occured errors.
	 * @return
	 */
	public static int getErrorCount()
	{
		return errCount;
	}

	/**
	 * Returns the number of occured warnings.
	 * @return
	 */
	public static int getWarnCount()
	{
		return warnCount;
	}
}
