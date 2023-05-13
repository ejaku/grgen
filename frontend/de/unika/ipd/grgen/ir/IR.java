/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.awt.Color;
import java.util.Map;

import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.XMLDumpable;

/**
 * Base class for all IR classes.
 */
public abstract class IR extends Base implements GraphDumpable, XMLDumpable
{
	private static final IR bad = new Bad();

	/** The name of this IR object */
	private String name;

	/** Names of the children of this node */
	private String[] childrenNames;

	/** children name object for children without names */
	private static final String[] noChildrenNames = { };

	private boolean canonicalValid = false;

	/** Make a new IR object and name it. */
	protected IR(String name)
	{
		this.name = name;
		childrenNames = noChildrenNames;
	}

	/** @return true, if this ir object is bad, false otherwise. */
	public boolean isBad()
	{
		return false;
	}

	/** @return A bad ir object. */
	public static IR getBad()
	{
		return bad;
	}

	/** @return The name of this IR object (that is group, node, edge, test, ...). */
	public String getName()
	{
		return name;
	}

	/** Set the name of this IR object. */
	public void setName(String newName)
	{
		name = newName;
	}

	/**
	 * View an IR object as a string.
	 * The string of an IR object is its name.
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		return name;
	}

	/**
	 * Set the names of the children of this node.
	 * @param names A string array with the names.
	 */
	protected void setChildrenNames(String[] names)
	{
		this.childrenNames = names;
	}

	/**
	 * Build the canonical form.
	 * Compound types must sort their members alphabetically.
	 */
	protected void canonicalizeLocal()
	{
		// default implementation for IR objects without named members
	}

	public final void canonicalize()
	{
		if(!canonicalValid) {
			canonicalizeLocal();
			canonicalValid = true;
		}
	}

	protected final void invalidateCanonical()
	{
		canonicalValid = false;
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// XML dumping
	//////////////////////////////////////////////////////////////////////////////////////////

	/** @return Name of the tag as string. */
	@Override
	public String getTagName()
	{
		return getName().replace(' ', '_');
	}

	/** @return Name of the tag that expresses a reference to this object. */
	@Override
	public String getRefTagName()
	{
		return getName().replace(' ', '_') + "_ref";
	}

	/**
	 * Add the XML fields to a map.
	 * @param fields The map to add the fields to.
	 */
	@Override
	public void addFields(Map<String, Object> fields)
	{
		// empty
	}

	/** @return A unique ID for this object. */
	@Override
	public String getXMLId()
	{
		return getId();
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	// graph dumping
	//////////////////////////////////////////////////////////////////////////////////////////

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId() */
	@Override
	public String getNodeId()
	{
		return getId();
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor() */
	@Override
	public Color getNodeColor()
	{
		return Color.WHITE;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape() */
	@Override
	public int getNodeShape()
	{
		return GraphDumper.DEFAULT;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	@Override
	public String getNodeLabel()
	{
		return name;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo() */
	@Override
	public String getNodeInfo()
	{
		return "ID: " + getId();
	}

	/**
	 * By default this object has the number of the edge as edge label.
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	@Override
	public String getEdgeLabel(int edge)
	{
		return edge < childrenNames.length ? childrenNames[edge] : "" + edge;
	}
}
