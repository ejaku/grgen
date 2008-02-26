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

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.parser.Coords;

/**
 *
 */
public class InducedNode extends BaseNode {
	static {
		setName(InducedNode.class, "induced");
	}

	Vector<NodeDeclNode> children = new Vector<NodeDeclNode>();

	Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public InducedNode(Coords coords) {
		super(coords);
	}

	public void addChild(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return getValidVersionVector(childrenUnresolved, children);
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	private static final DeclarationResolver<NodeDeclNode> childrenResolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		for(int i=0; i<childrenUnresolved.size(); ++i) {
			children.add(childrenResolver.resolve(childrenUnresolved.get(i), this));
			successfullyResolved = children.get(i)!=null && successfullyResolved;
		}
		return successfullyResolved;
	}

	/**
	 * Check whether all children are of node type.
	 */
	protected boolean checkLocal() {
		if (children.isEmpty()) {
			this.reportError("Induced statement is empty");
			return false;
		}

		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}
}
