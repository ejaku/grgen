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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

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
	public static final int UNBOUND = Integer.MAX_VALUE;
	
	/** The upper and lower bound. */
	private int lower, upper;
	
	/**
	 * @param coords
	 */
	public RangeSpecNode(Coords coords, int lower, int upper) {
		super(coords);
		this.lower = lower;
		this.upper = upper;
	}
	
	public String getName() {
		return super.getName() + " [" + lower + ":" + upper + "]";
	}
	
	public boolean isBoundedUp() {
		return upper != UNBOUND;
	}
	
	public boolean isBoundedLow() {
		return lower != UNBOUND;
	}
	
	/**
	 * @return the lower bound of the range.
	 */
	public int getLower() {
		return lower;
	}
	
	/**
	 * @return the upper bound of the range.
	 */
	public int getUpper() {
		return upper;
	}
	
}
