/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @file CollectNode.java
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a set of potentially homomorph nodes
 * children: *:IdentNode resolved to NodeDeclNode|EdgeDeclNoe
 */
public class HomNode extends BaseNode {
	static {
		setName(HomNode.class, "homomorph");
	}

	private Vector<NodeDeclNode> childrenNode = new Vector<NodeDeclNode>();
	private Vector<EdgeDeclNode> childrenEdge = new Vector<EdgeDeclNode>();

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public HomNode(Coords coords) {
		super(coords);
	}

	public void addChild(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		return getValidVersionVector(childrenUnresolved, childrenNode, childrenEdge);
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	private static final DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> declResolver = new DeclarationPairResolver<NodeDeclNode,EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;

		for(int i=0; i<childrenUnresolved.size(); ++i) {
			Pair<NodeDeclNode, EdgeDeclNode> resolved = declResolver.resolve(childrenUnresolved.get(i), this);
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

	private static final TypeChecker nodeTypeChecker = new TypeChecker(NodeTypeNode.class);
	private static final TypeChecker edgeTypeChecker = new TypeChecker(EdgeTypeNode.class);

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
		for(BaseNode n : childrenNode) {
			successfullyChecked = nodeTypeChecker.check(n, error) && successfullyChecked;
		}
		for(BaseNode n : childrenEdge) {
			successfullyChecked = edgeTypeChecker.check(n, error) && successfullyChecked;
		}
		warnEdgeTypes();

		return successfullyChecked;
	}

	/** Checks whether all edges are compatible to each other.*/
	private void warnEdgeTypes()
    {
	    boolean isDirectedEdge = false;
	    boolean isUndirectedEdge = false;

		for (int i=0; i < childrenEdge.size(); i++ ) {
			TypeNode type = childrenEdge.get(i).getDeclType();
			if (type instanceof DirectedEdgeTypeNode) {
				isDirectedEdge = true;
			}
			if (type instanceof UndirectedEdgeTypeNode) {
				isUndirectedEdge = true;
			}
        }

		if (isDirectedEdge && isUndirectedEdge) {
			reportWarning("Hom statement may only contain directed or undirected edges at a time");
		}
    }

	@Override
	public Color getNodeColor() {
		return Color.PINK;
	}
}
