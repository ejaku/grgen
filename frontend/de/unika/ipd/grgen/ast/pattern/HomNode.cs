/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.pattern;

import java.awt.Color;
import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a set of potentially homomorph nodes
 * children: *:IdentNode resolved to NodeDeclNode|EdgeDeclNoe
 */
public class HomNode extends BaseNode
{
	static {
		setClassName(HomNode.class, "homomorph");
	}

	private List<NodeDeclNode> childrenNode = new ArrayList<NodeDeclNode>();
	private List<EdgeDeclNode> childrenEdge = new ArrayList<EdgeDeclNode>();

	private List<BaseNode> childrenUnresolved = new ArrayList<BaseNode>();

	public HomNode(Coords coords)
	{
		super(coords);
	}

	public void addChild(BaseNode child)
	{
		assert(!isResolved());
		becomeParent(child);
		childrenUnresolved.add(child);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		return getValidVersionList(childrenUnresolved, childrenNode, childrenEdge);
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		// nameless children
		return childrenNames;
	}

	public List<NodeDeclNode> getHomNodes()
	{
		return childrenNode;
	}

	public List<EdgeDeclNode> getHomEdges()
	{
		return childrenEdge;
	}

	private static final DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> declResolver =
			new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		for(int i = 0; i < childrenUnresolved.size(); ++i) {
			Pair<NodeDeclNode, EdgeDeclNode> resolved = declResolver.resolve(childrenUnresolved.get(i), this);
			successfullyResolved = (resolved != null) && successfullyResolved;
			if(resolved != null) {
				if(resolved.fst != null) {
					childrenNode.add(resolved.fst);
				}
				if(resolved.snd != null) {
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
	@Override
	protected boolean checkLocal()
	{
		if(childrenNode.isEmpty() && childrenEdge.isEmpty()) {
			this.reportError("The hom statement is empty.");
			return false;
		}
		if(!childrenNode.isEmpty() && !childrenEdge.isEmpty()) {
			this.reportError("The hom statement may only contain nodes or edges at a time"
					+ " (this is violated by node " + childrenNode.get(0) + " and edge " + childrenEdge.get(0) + ").");
			return false;
		}

		boolean successfullyChecked = true;
		for(NodeDeclNode node : childrenNode) {
			successfullyChecked = nodeTypeChecker.check(node, error) && successfullyChecked;
		}
		for(EdgeDeclNode edge : childrenEdge) {
			successfullyChecked = edgeTypeChecker.check(edge, error) && successfullyChecked;
		}
		warnEdgeTypes();

		return successfullyChecked;
	}

	/** Checks whether all edges are compatible to each other.*/
	private void warnEdgeTypes()
	{
		boolean isDirectedEdge = false;
		boolean isUndirectedEdge = false;

		for(int i = 0; i < childrenEdge.size(); i++) {
			TypeNode type = childrenEdge.get(i).getDeclType();
			if(type instanceof DirectedEdgeTypeNode) {
				isDirectedEdge = true;
			}
			if(type instanceof UndirectedEdgeTypeNode) {
				isUndirectedEdge = true;
			}
		}

		if(isDirectedEdge && isUndirectedEdge) {
			reportWarning("The hom statement may only contain directed or undirected edges at a time.");
		}
	}

	@Override
	public Color getNodeColor()
	{
		return Color.PINK;
	}
}
