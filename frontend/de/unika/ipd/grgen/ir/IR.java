/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.awt.Color;
import java.util.Collection;
import java.util.Collections;
import java.util.Map;

import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.XMLDumpable;

/**
 * Base class for all IR classes.
 */
public abstract class IR extends Base implements GraphDumpable, XMLDumpable {

	private static final IR bad = new Bad();

	/** The name of this IR object */
	private String name;

	/** Names of the children of this node */
	private String[] childrenNames;

	/** children name object for children without names */
	private static final String[] noChildrenNames = { };

	private boolean canonicalValid = false;


	/** Make a new IR object and name it. */
	protected IR(String name) {
		this.name = name;
		childrenNames = noChildrenNames;
	}

	/** @return true, if this ir object is bad, false otherwise. */
	public boolean isBad() {
		return false;
	}

	/** @return A bad ir object. */
	public static IR getBad() {
		return bad;
	}

	/** @return The name of this IR object (that is group, node, edge, test, ...). */
	public String getName() {
		return name;
	}

	/** Set the name of this IR object. */
	protected void setName(String newName) {
		name = newName;
	}

	/**
	 * View an IR object as a string.
	 * The string of an IR object is its name.
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return name;
	}

	/**
	 * Set the names of the children of this node.
	 * @param names A string array with the names.
	 */
	protected void setChildrenNames(String[] names) {
		this.childrenNames = names;
	}

	/** @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Collection<? extends IR> getWalkableChildren() {
		return Collections.emptySet();
	}

	/**
	 * Build the canonical form.
	 * Compound types must sort their members alphabetically.
	 */
	protected void canonicalizeLocal() {
	}

	public final void canonicalize() {
		if(!canonicalValid) {
			canonicalizeLocal();
			canonicalValid = true;
		}
	}

	protected final void invalidateCanonical() {
		canonicalValid = false;
	}

	/** Add this type to the digest. */
	void addToDigest(StringBuffer sb) {
	}

//////////////////////////////////////////////////////////////////////////////////////////
	// XML dumping
//////////////////////////////////////////////////////////////////////////////////////////

	/** @return Name of the tag as string. */
	public String getTagName() {
		return getName().replace(' ', '_');
	}

	/** @return Name of the tag that expresses a reference to this object. */
	public String getRefTagName() {
		return getName().replace(' ', '_') + "_ref";
	}

	/**
	 * Add the XML fields to a map.
	 * @param fields The map to add the fields to.
	 */
	public void addFields(Map<String, Object> fields) {
	}

	/** @return A unique ID for this object. */
	public String getXMLId() {
		return getId();
	}

//////////////////////////////////////////////////////////////////////////////////////////
	// graph dumping
//////////////////////////////////////////////////////////////////////////////////////////

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId() */
	public String getNodeId() {
		return getId();
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor() */
	public Color getNodeColor() {
		return Color.WHITE;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape() */
	public int getNodeShape() {
		return GraphDumper.DEFAULT;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return name;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo() */
	public String getNodeInfo() {
		return "ID: " + getId();
	}

	/**
	 * By default this object has the number of the edge as edge label.
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	public String getEdgeLabel(int edge) {
		return edge < childrenNames.length ? childrenNames[edge] : "" + edge;
	}
}
