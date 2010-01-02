/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

	
	public EdgeDeclNode(IdentNode id, BaseNode type, int context, TypeExprNode constraints, 
			PatternGraphNode directlyNestingLHSGraph, boolean maybeNull) {
		super(id, type, context, constraints, directlyNestingLHSGraph, maybeNull);
		setName("edge");
	}

	public EdgeDeclNode(IdentNode id, BaseNode type, int context, TypeExprNode constraints, PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, context, constraints, directlyNestingLHSGraph, false);
		setName("edge");
	}

	public EdgeDeclNode(IdentNode id, BaseNode type, int declLocation, PatternGraphNode directlyNestingLHSGraph) {
		this(id, type, declLocation, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
	}

	/**
	 * Create EdgeDeclNode and immediately resolve and check it.
	 * NOTE: Use this to create and insert an EdgeDeclNode into the AST after
	 * the AST is already checked.
	 * TODO Change type of type iff CollectNode support generics
	 */
	public EdgeDeclNode(IdentNode id, BaseNode type, int declLocation, BaseNode parent, PatternGraphNode directlyNestingLHSGraph) {
		this(id, type, declLocation, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		parent.becomeParent(this);

		resolve();
		check();
	}

	/** The TYPE child could be an edge in case the type is
	 *  inherited dynamically via the typeof operator */
	@Override
	public EdgeTypeNode getDeclType() {
		assert isResolved();

		DeclNode curr = getValidResolvedVersion(typeEdgeDecl, typeTypeDecl);
		return (EdgeTypeNode) curr.getDeclType();
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
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

	/**
	 * Warn on typeofs of new created graph edges (with known type).
	 */
	private void warnOnTypeofOfRhsEdges() {
		if ((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			// As long as we're typed with a rhs edge we change our type to the type of that edge,
			// the first time we do so we emit a warning to the user (further steps will be warned by the elements reached there)
			boolean firstTime = true;
			while (inheritsType()
					&& (typeEdgeDecl.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
				if (firstTime) {
					firstTime = false;
					reportWarning("type of edge " + typeEdgeDecl.ident + " is statically known");
				}
				typeTypeDecl = typeEdgeDecl.typeTypeDecl;
				typeEdgeDecl = typeEdgeDecl.typeEdgeDecl;
			}
			// either reached a statically known type by walking rhs elements
			// or reached a lhs element (with statically unknown type as it matches any subtypes)
		}
	}

	private static final Checker typeChecker = new TypeChecker(EdgeTypeNode.class);

	@Override
	protected boolean checkLocal() {
		warnOnTypeofOfRhsEdges();

		return super.checkLocal()
			& typeChecker.check(getValidResolvedVersion(typeEdgeDecl, typeTypeDecl), error);
	}

	/**
	 * Edges have more info to give
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	@Override
	protected String extraNodeInfo() {
		return "";
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
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

	protected final boolean inheritsType() {
		assert isResolved();

		return typeEdgeDecl != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		TypeNode tn = getDeclType();
		EdgeType et = tn.checkIR(EdgeType.class);
		IdentNode ident = getIdentNode();

		Edge edge = new Edge(ident.getIdent(), et, ident.getAnnotations(),
				directlyNestingLHSGraph.getGraph(), isMaybeDeleted(), isMaybeRetyped(), context);
		edge.setConstraints(getConstraints());

		if(inheritsType()) {
			edge.setTypeof(typeEdgeDecl.checkIR(Edge.class));
		}

		edge.setMaybeNull(maybeNull);
		
		return edge;
	}

	public static String getKindStr() {
		return "edge declaration";
	}

	public static String getUseStr() {
		return "edge";
	}
}

