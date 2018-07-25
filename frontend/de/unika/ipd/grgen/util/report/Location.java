/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util.report;

/**
 * Represents a location known to the user, where a message can be
 * generated. For example, in parser, the file and line number in
 * the file that is parsed.
 */
public interface Location {

	/**
	 * Get the location's string representation.
	 * This string is only meaningful, if #hasLocation() returns true.
	 * @return The string representation of the location.
	 */
	String getLocation();

	/**
	 * Checks, if the location is valid. If it is valid, #getLocation()
	 * returns a valid location string, if not, the string returned by
	 * #getLocation() is to be ignored.
	 * @return true, if the location is valid.
	 */
	boolean hasLocation();
}
