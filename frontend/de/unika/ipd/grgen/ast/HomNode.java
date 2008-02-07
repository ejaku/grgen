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

import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

/**
 * AST node that represents a set of potentially homomorph nodes
 * children: *:IdentNode resolved to NodeDeclNode|EdgeDeclNoe
 */
public class HomNode extends BaseNode {
	static {
		setName(HomNode.class, "homomorph");
	}

	Vector<NodeDeclNode> childrenNode = new Vector<NodeDeclNode>();
	Vector<EdgeDeclNode> childrenEdge = new Vector<EdgeDeclNode>();

	Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public HomNode(Coords coords) {
		super(coords);
	}

	public void addChild(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return getValidVersionVector(childrenUnresolved, childrenNode, childrenEdge);
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> resolver =
			new DeclarationPairResolver<NodeDeclNode,EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class);
		for(int i=0; i<childrenUnresolved.size(); ++i) {
			Pair<NodeDeclNode, EdgeDeclNode> resolved = resolver.resolve(childrenUnresolved.get(i), this);
			successfullyResolved = (resolved != null) && successfullyResolved;
			if (resolved != null) {
				if(resolved.fst!=null) {
					childrenNode.add(resolved.fst);
				}
				if(resolved.snd!=null) {
					childrenEdge.add(resolved.snd);
				}
			}
		}
		return successfullyResolved;
	}


	/**
	 * Check whether all children are of same type (node or edge)
	 * and additionally one entity may not be used in two different hom
	 * statements
	 */
	protected boolean checkLocal() {
		if (childrenNode.isEmpty() && childrenEdge.isEmpty()) {
			this.reportError("Hom statement is empty");
			return false;
		}
		if (!childrenNode.isEmpty() && !childrenEdge.isEmpty()) {
			this.reportError("Hom statement may only contain nodes or edges at a time");
			return false;
		}

		boolean successfullyChecked = true;
		TypeChecker nodeTypeChecker = new TypeChecker(NodeTypeNode.class);
		for(BaseNode n : childrenNode) {
			successfullyChecked = nodeTypeChecker.check(n, error) && successfullyChecked;
		}
		TypeChecker edgeTypeChecker = new TypeChecker(DirectedEdgeTypeNode.class);
		for(BaseNode n : childrenEdge) {
			successfullyChecked = edgeTypeChecker.check(n, error) && successfullyChecked;
		}
		return successfullyChecked;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}
}
