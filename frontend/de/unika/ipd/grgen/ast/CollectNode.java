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
 * @file CollectNode.java
 * @author shack
 * @date Jul 21, 2003
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

/**
 * An AST node that represents a collection of other nodes.
 * children: *:BaseNode
 *
 * Normally AST nodes contain a fixed number of children,
 * which are accessed by their fixed index within the children vector.
 * This node collects a statically unknown number of children AST nodes,
 * originating in unbounded list constructs in the parsing syntax.
 */
public class CollectNode extends BaseNode
{
	static {
		setName(CollectNode.class, "collect");
	}

	public CollectNode() {
		super();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result
		
		for(int i=0; i<children(); ++i) {
			successfullyResolved = getChild(i).resolve() && successfullyResolved;
		}
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			for(int i=0; i<children(); ++i) {
				childrenChecked = getChild(i).check() && childrenChecked;
			}
		}
		
		boolean locallyChecked = true; 
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	public Color getNodeColor() {
		return Color.GRAY;
	}
}
