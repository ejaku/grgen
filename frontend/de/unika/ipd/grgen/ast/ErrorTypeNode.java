/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

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

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.VoidType;

/**
 * The error basic type. It is compatible to no other type.
 * TODO: Why compatible to no other type? The error node within an compiler 
 * should be compatible to every other node, to protect against error avalanches
 */
class ErrorType extends TypeNode
{
	static {
		setName(ErrorType.class, "error type");
	}

	private IdentNode id;

	public ErrorType(IdentNode id) {
		this.id = id;
		setCoords(id.getCoords());
	}
	
	/** implementation of Walkable @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Collection<? extends BaseNode> getWalkableChildren() {
		return children;
	}
	
	/** get names of the walkable children, same order as in getWalkableChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		return true;
	}
	
	protected IR constructIR() {
		return new VoidType(id.getIdent());
	}
	
	public String getUseString() {
		return "error type";
	}
	
	public String toString() {
		return "error type";
	}
};
