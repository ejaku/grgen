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
 * @author buchwald
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.parser.Coords;

/**
 * 
 */
public class ExactNode extends BaseNode
{
	static {
		setName(ExactNode.class, "exact");
	}
	
	Vector<BaseNode> children = new Vector<BaseNode>();

	public ExactNode(Coords coords) {
		super(coords);
	}

	public void addChild(BaseNode n) {
		n = n==null ? NULL : n;
		becomeParent(n);
		children.add(n);
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
		Resolver resolver = new DeclResolver(new Class[] { NodeDeclNode.class });
		for(int i=0; i<children.size(); ++i) {
			BaseNode resolved = resolver.resolve(children.get(i));
			successfullyResolved = resolved!=null && successfullyResolved;
			children.set(i, ownedResolutionResult(children.get(i), resolved));
		}
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		for(int i=0; i<children.size(); ++i) {
			successfullyResolved = children.get(i).resolve() && successfullyResolved;
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
			
			for(int i=0; i<children.size(); ++i) {
				childrenChecked = children.get(i).check() && childrenChecked;
			}
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/**
	 * Check whether all children are of node type.
	 * 
	 * TODO warn if some statements are redundant.
	 */
	protected boolean checkLocal() {
		if (children.isEmpty()) {
			this.reportError("Exact statement is empty");
			return false;
		}

		boolean successfullyChecked = true;
		Checker checker = new SimpleChecker(NodeDeclNode.class);
		for(BaseNode n : children) {
			successfullyChecked = checker.check(n, error) && successfullyChecked;
		}
		return successfullyChecked;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}

	// debug guards to protect again accessing wrong elements
	public void setChild(int pos, BaseNode n) {
		assert(false);
	}
	public BaseNode getChild(int i) {
		assert(false);
		return null;
	}
	public int children() {
		assert(false);
		return 0;
	}
	public BaseNode replaceChild(int i, BaseNode n) {
		assert(false);
		return null;
	}
}
