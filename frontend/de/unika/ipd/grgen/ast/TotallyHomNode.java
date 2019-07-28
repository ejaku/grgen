/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
 * AST node that represents a totally homomorph node and a set of nodes it must be isomorph to
 */
public class TotallyHomNode extends BaseNode {
	static {
		setName(TotallyHomNode.class, "totally homomorph");
	}

	NodeDeclNode node;
	EdgeDeclNode edge;
	Vector<NodeDeclNode> childrenNode = new Vector<NodeDeclNode>();
	Vector<EdgeDeclNode> childrenEdge = new Vector<EdgeDeclNode>();

	private BaseNode entity;
	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public TotallyHomNode(Coords coords) {
		super(coords);
	}

	public void setTotallyHom(BaseNode n) {
		assert(entity==null);
		becomeParent(n);
		entity = n;

	}

	public void addChild(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entity, node, edge));
		children.addAll(getValidVersionVector(childrenUnresolved, childrenNode, childrenEdge));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("totally homomorph entity");
		// nameless isomorph children
		return childrenNames;
	}

	private static final DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> declResolver = new DeclarationPairResolver<NodeDeclNode,EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		Pair<NodeDeclNode, EdgeDeclNode> resolved = declResolver.resolve(entity, this);
		boolean successfullyResolved = resolved != null;
		if(resolved!=null) {
			if(resolved.fst!=null) {
				node = resolved.fst;
			} else {
				edge = resolved.snd;
			}
		}

		for(int i=0; i<childrenUnresolved.size(); ++i) {
			resolved = declResolver.resolve(childrenUnresolved.get(i), this);
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
		if (node!=null) {
			if (!childrenEdge.isEmpty()) {
				this.reportError("totally hom statement independent(.)  may only contain nodes or edges at a time");
				return false;
			}
		}
		if (edge!=null) {
			if (!childrenNode.isEmpty()) {
				this.reportError("totally hom statement independent(.)  may only contain nodes or edges at a time");
				return false;
			}
		}

		boolean successfullyChecked = true;
		for(BaseNode n : childrenNode) {
			successfullyChecked = nodeTypeChecker.check(n, error) && successfullyChecked;
		}
		for(BaseNode n : childrenEdge) {
			successfullyChecked = edgeTypeChecker.check(n, error) && successfullyChecked;
		}
		if (edge!=null) {
			warnEdgeTypes();
		}

		return successfullyChecked;
	}

	/** Checks whether all edges are compatible to each other.*/
	private void warnEdgeTypes()
    {
	    boolean isDirectedEdge = edge.getDeclType() instanceof DirectedEdgeTypeNode;
	    boolean isUndirectedEdge = edge.getDeclType() instanceof UndirectedEdgeTypeNode;

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
