/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a range specification (used by ConnAssertNode).
 * children: none
 */
public class RangeSpecNode extends BaseNode {
	static {
		setName(RangeSpecNode.class, "range spec");
	}

	/** Constant, signaling if one bound is bounded. */
	public static final long UNBOUND = Long.MAX_VALUE;

	/** The upper and lower bound. */
	private long lower, upper;

	/**
	 * @param coords
	 */
	public RangeSpecNode(Coords coords, long lower, long upper) {
		super(coords);
		this.lower = lower;
		this.upper = upper;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		boolean good = true;
		if(lower < 0) {
			error.error(getCoords(), "Lower bound of range must be positive");
			good = false;
		}
		if(upper < 0) {
			error.error(getCoords(), "Upper bound of range must be positive");
			good = false;
		}
		if(lower>upper) {
			error.error(getCoords(), "Lower bound must be less (or equal) than upper bound of range");
			good = false;
		}
		return good;
	}

	public String getName() {
		return super.getName() + " [" + lower + ":" + upper + "]";
	}

	public boolean isBoundedUp() {
		return upper != UNBOUND;
	}

	/** @return the lower bound of the range. */
	public long getLower() {
		return lower;
	}

	/** @return the upper bound of the range. */
	public long getUpper() {
		return upper;
	}
}
