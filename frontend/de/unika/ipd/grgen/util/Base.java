/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util;

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
	public static final int NOTE = 4;	//NOTE: changed from 1 to 4

	/** The debug reporter for debugging */
	public static Reporter debug;

	/** The error reporter for error reporting */
	public static ErrorReporter error;

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
}
