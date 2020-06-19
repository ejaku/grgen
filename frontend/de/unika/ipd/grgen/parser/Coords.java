/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.util.report.Location;

public class Coords implements Location
{
	protected static final Coords INVALID = new Coords();

	protected static final Coords BUILTIN = new Coords(0, 0, "<builtin>");

	public static final Coords getInvalid()
	{
		return INVALID;
	}

	public static final Coords getBuiltin()
	{
		return BUILTIN;
	}

	protected int line;
	protected int column;
	protected String filename; // non-null if line!=-1 && column!=-1

	/**
	 * Create empty coordinates.
	 * Coordinates made with this constructor will return false
	 * on #hasLocation().
	 */
	public Coords()
	{
		this(-1, -1, null);
	}

	/**
	 * Fully construct new coordinates
	 * @param line The line
	 * @param column The column
	 * @param filename The filename
	 */
	public Coords(int line, int column, String filename)
	{
		this.line = line;
		this.column = column;
		this.filename = filename;
	}

	/**
	 * Make coordinates just from line and column. The filename is set
	 * to the default filename.
	 * @param line The line
	 * @param column The column
	 */
	public Coords(int line, int column)
	{
		this(line, column, null);
	}

	/**
	 * Checks, wheather the coordinates are valid.
	 * @return true, if the coordinates are set and valid, false otherwise
	 */
	private boolean valid()
	{
		return line != -1 && column != -1;
	}

	@Override
	public String toString()
	{
		if(valid())
			return filename + ":" + line + "," + column;
		else
			return "nowhere";
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Location#getLocation()
	 */
	@Override
	public String getLocation()
	{
		return toString();
	}

	/**
	 * @see de.unika.ipd.grgen.util.report.Location#hasLocation()
	 */
	@Override
	public boolean hasLocation()
	{
		return valid();
	}

	/**
	 * Compare coordinates.
	 * Coordainates are equal, if they have the same filename (or both none)
	 * the same line and column.
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	@Override
	public boolean equals(Object obj)
	{
		boolean res = false;
		if(obj instanceof Coords) {
			Coords c = (Coords)obj;
			res = line == c.line && column == c.column &&
					((filename == null && c.filename == null)
							|| (filename.equals(c.filename)));
		}
		return res;
	}

	@Override
	public int hashCode()
	{
		return ( (filename != null ? filename.hashCode() : 13) * 31 + line ) * 31 + column;
	}
	
	/**
	 * Get the line of the coordinates.
	 * @return The line.
	 */
	public int getLine()
	{
		return line;
	}

	/**
	 * Get the column of the coordinates.
	 * @return The column.
	 */
	public int getColumn()
	{
		return column;
	}

	/**
	 * Get the filename of the coordinates.
	 * @return The filename.
	 */
	public String getFileName()
	{
		return filename;
	}

	public boolean comesBefore(Coords that)
	{
		if(!this.valid())
			return false;
		if(!that.valid())
			return false;
		if(this.getLine() < that.getLine())
			return true;
		if(this.getLine() == that.getLine())
			if(this.getColumn() < that.getColumn())
				return true;
		return false;
	}
}
