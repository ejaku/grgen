/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public class EdgeDeclNode extends ConstraintDeclNode implements EdgeCharacter {
	static {
		setName(EdgeDeclNode.class, "edge declaration");
	}

	protected EdgeDeclNode typeEdgeDecl = null;
	protected TypeDeclNode typeTypeDecl = null;

	protected static final DeclarationPairResolver<EdgeDeclNode,TypeDeclNode> typeResolver =
		new DeclarationPairResolver<EdgeDeclNode,TypeDeclNode>(EdgeDeclNode.class, TypeDeclNode.class);

	public EdgeDeclNode(IdentNode id, BaseNode type, int context, TypeExprNode constraints) {
		super(id, type, context, constraints);
		setName("edge");
	}

	public EdgeDeclNode(IdentNode id, BaseNode type, int declLocation) {
		this(id, type, declLocation, TypeExprNode.getEmpty());
	}

	/**
	 * Create EdgeDeclNode and immediately resolve and check it.
	 * NOTE: Use this to create and insert an EdgeDeclNode into the AST after
	 * the AST is already checked.
	 * TODO Change type of type iff CollectNode support generics
	 */
	public EdgeDeclNode(IdentNode id, BaseNode type, int declLocation, BaseNode parent) {
		this(id, type, declLocation, TypeExprNode.getEmpty());
		parent.becomeParent(this);

		resolve();
		check();
	}

	/** The TYPE child could be an edge in case the type is
	 *  inherited dynamically via the typeof operator */
	public EdgeTypeNode getDeclType() {
		assert isResolved();

		DeclNode curr = getValidResolvedVersion(typeEdgeDecl, typeTypeDecl);
		return (EdgeTypeNode) curr.getDeclType();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		Pair<EdgeDeclNode, TypeDeclNode> resolved = typeResolver.resolve(typeUnresolved, this);
		if(resolved == null) return false;

		typeEdgeDecl = resolved.fst;
		typeTypeDecl = resolved.snd;

		TypeDeclNode typeDecl;

		if(typeEdgeDecl != null) {
			HashSet<EdgeDeclNode> visited = new HashSet<EdgeDeclNode>();
			EdgeDeclNode prev = typeEdgeDecl;
			EdgeDeclNode cur = typeEdgeDecl.typeEdgeDecl;
			while(cur != null) {
				if(visited.contains(cur)) {
					reportError("Circular typeofs are not allowed");
					return false;
				}
				visited.add(cur);
				prev = cur;
				cur = cur.typeEdgeDecl;
			}
			typeDecl = prev.typeTypeDecl;
		}
		else typeDecl = typeTypeDecl;

		if(!typeDecl.resolve()) return false;
		if(!(typeDecl.getDeclType() instanceof EdgeTypeNode)) {
			typeUnresolved.reportError("Type of edge \"" + getIdentNode() + "\" must be a edge type");
			return false;
		}

		return true;
	}

	protected boolean checkLocal() {
		Checker typeChecker = new TypeChecker(EdgeTypeNode.class);
		return super.checkLocal()
			& typeChecker.check(getValidResolvedVersion(typeEdgeDecl, typeTypeDecl), error);
	}

	/** Returns whether the edge type is a typeof statement. */
	public boolean hasTypeof() {
		assert isResolved();

		return typeEdgeDecl != null;
	}

	/**
	 * Edges have more info to give
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	protected String extraNodeInfo() {
		return "";
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.YELLOW;
	}

	/**
	 * Get the IR object correctly casted.
	 * @return The edge IR object.
	 */
	public Edge getEdge() {
		return checkIR(Edge.class);
	}

	protected boolean inheritsType() {
		return typeEdgeDecl != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		// This must be ok after checking all nodes.
		TypeNode tn = getDeclType();
		EdgeType et = tn.checkIR(EdgeType.class);
		IdentNode ident = getIdentNode();

		Edge edge = new Edge(ident.getIdent(), et, ident.getAnnotations(), isMaybeDeleted(), isMaybeRetyped());
		edge.setConstraints(getConstraints());

		if(inheritsType()) {
			edge.setTypeof(typeEdgeDecl.checkIR(Edge.class));
		}

		return edge;
	}

	public static String getKindStr() {
		return "edge declaration";
	}

	public static String getUseStr() {
		return "edge";
	}
}

