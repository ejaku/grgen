/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.util.report.Location;

public class Coords implements Location {

	protected static final Coords INVALID = new Coords();

	protected static final Coords BUILTIN = new Coords(0, 0, "<builtin>");

	public static final Coords getInvalid() {
		return INVALID;
	}

	public static final Coords getBuiltin() {
		return BUILTIN;
	}

	/**
	 * The default filename for the coordinates
	 * It should only be changed, if the lexer is switching to another file.
	 */
	// TODO use or remove it
	// private static String defaultFilename = null;

	protected int line, col;
	protected String filename;

	/**
	 * Set the default filename coordinates get, when they are constructed.
	 * @param filename The default filename for coordinates. If null,
	 * the coordinates are meant to have no filename (i.e. The filename
	 * is not printed in the toString() method.
	 */
	// TODO use or remove it
	/* public static void setDefaultFilename(String filename) {
		defaultFilename = filename;
	}*/

	/**
	 * Create empty coordinates.
	 * Coordinates made with this constructor will return false
	 * on #hasLocation().
	 */
	public Coords() {
		this(-1, -1, null);
	}

	/**
	 * Fully construct new coordinates
	 * @param line The line
	 * @param col The column
	 * @param filename The filename
	 */
	public Coords(int line, int col, String filename) {
		this.line = line;
		this.col = col;
		this.filename = filename;
	}


	/**
	 * Make coordinates just from line and column. The filename is set
	 * to the default filename.
	 * @param line The line
	 * @param col The column
	 */
	public Coords(int line, int col) {
		this(line, col, null);
	}


	/**
	 * Checks, wheather the coordinates are valid.
	 * @return true, if the coordinates are set and valid, false otherwise
	 */
	private boolean valid() {
		return line != -1 && col != -1;
	}

	public String toString() {
		if(valid())
			return filename + ":" + line + "," + col;
			// return (filename != null ? filename + ":" : "") + line + "," + col;
		else
			return "nowhere";
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Location#getLocation()
	 */
	public String getLocation() {
		return toString();
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Location#hasLocation()
	 */
	public boolean hasLocation() {
		return valid();
	}

	/**
	 * Compare coordinates.
	 * Coordainates are equal, if they have the same filename (or both none)
	 * the same line and column.
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	public boolean equals(Object obj) {
		boolean res = false;
		if(obj instanceof Coords) {
			Coords c = (Coords) obj;
			res = line == c.line && col == c.col &&
				((filename == null && c.filename == null)
					 || (filename.equals(c.filename)));
		}
		return res;
	}

	/**
	 * Get the line of the coordinates.
	 * @return The line.
	 */
	public int getLine() {
		return line;
	}

	/**
	 * Get the column of the coordinates.
	 * @return The column.
	 */
	public int getColumn() {
		return col;
	}

	/**
	 * Get the filename of the coordinates.
	 * @return The filename.
	 */
	public String getFileName() {
		return filename;
	}

}
