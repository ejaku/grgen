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
