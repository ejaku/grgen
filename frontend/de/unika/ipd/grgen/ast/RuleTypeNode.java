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


/**
 * Type of rule decl node declaration.
 */
class RuleTypeNode extends TypeNode
{
	static {
		setName(RuleTypeNode.class, "rule type");
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		return true;
	}
};
	