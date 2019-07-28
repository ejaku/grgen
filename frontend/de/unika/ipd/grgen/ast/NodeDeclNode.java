/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
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
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NameOrAttributeInitialization;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * Declaration of a node.
 */
public class NodeDeclNode extends ConstraintDeclNode implements NodeCharacter {
	static {
		setName(NodeDeclNode.class, "node");
	}

	protected NodeDeclNode typeNodeDecl = null;
	protected TypeDeclNode typeTypeDecl = null;
	boolean isCopy;

	private static DeclarationPairResolver<NodeDeclNode, TypeDeclNode> typeResolver =
		new DeclarationPairResolver<NodeDeclNode, TypeDeclNode>(NodeDeclNode.class, TypeDeclNode.class);


	public NodeDeclNode(IdentNode id, BaseNode type, boolean isCopy,
			int context, TypeExprNode constr,
			PatternGraphNode directlyNestingLHSGraph,
			boolean maybeNull, boolean defEntityToBeYieldedTo) {
		super(id, type, context, constr, directlyNestingLHSGraph, maybeNull, defEntityToBeYieldedTo);
		this.isCopy = isCopy;
	}

	public NodeDeclNode(IdentNode id, BaseNode type, boolean isCopy,
			int context, TypeExprNode constr,
			PatternGraphNode directlyNestingLHSGraph) {
		this(id, type, isCopy, context, constr, directlyNestingLHSGraph, false, false);
	}

	/** The TYPE child could be a node in case the type is
	 *  inherited dynamically via the typeof/copy operator */
	@Override
	public NodeTypeNode getDeclType() {
		assert isResolved() : "not resolved";

		DeclNode curr = getValidResolvedVersion(typeNodeDecl, typeTypeDecl);
		return (NodeTypeNode) curr.getDeclType();
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(nameOrAttributeInits);
		if(initialization!=null) children.add(initialization);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("nameOrAttributeInits");
		if(initialization!=null) childrenNames.add("initialization expression");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		Pair<NodeDeclNode, TypeDeclNode> resolved = typeResolver.resolve(typeUnresolved, this);
		if(resolved == null) return false;

		typeNodeDecl = resolved.fst;
		typeTypeDecl = resolved.snd;

		TypeDeclNode typeDecl;

		if(typeNodeDecl != null) {
			HashSet<NodeDeclNode> visited = new HashSet<NodeDeclNode>();
			NodeDeclNode prev = typeNodeDecl;
			NodeDeclNode cur = typeNodeDecl.typeNodeDecl;

			while(cur != null) {
				if(visited.contains(cur)) {
					reportError("Circular typeof/copy not allowed");
					return false;
				}
				visited.add(cur);
				prev = cur;
				cur = cur.typeNodeDecl;
			}

			if(prev.typeTypeDecl == null && !prev.resolve()) return false;
			typeDecl = prev.typeTypeDecl;
		}
		else typeDecl = typeTypeDecl;

		if(!typeDecl.resolve()) return false;
		if(!(typeDecl.getDeclType() instanceof NodeTypeNode)) {
			typeUnresolved.reportError("Type of node \"" + getIdentNode() + "\" must be a node type (use edge syntax for edges, var for variables, ref for containers)");
			return false;
		}

		return true;
	}

	/**
	 * Warn on typeofs of new created graph nodes (with known type).
	 */
	private void warnOnTypeofOfRhsNodes() {
		if ((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			// As long as we're typed with a rhs edge we change our type to the type of that node,
			// the first time we do so we emit a warning to the user (further steps will be warned by the elements reached there)
			boolean firstTime = true;
			while (inheritsType()
					&& (typeNodeDecl.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
				if (firstTime) {
					firstTime = false;
					reportWarning("type of node " + typeNodeDecl.ident + " is statically known");
				}
				typeTypeDecl = typeNodeDecl.typeTypeDecl;
				typeNodeDecl = typeNodeDecl.typeNodeDecl;
			}
			// either reached a statically known type by walking rhs elements
			// or reached a lhs element (with statically unknown type as it matches any subtypes)
		}
	}

	private static final Checker typeChecker = new TypeChecker(NodeTypeNode.class);

	@Override
	protected boolean checkLocal() {
		warnOnTypeofOfRhsNodes();

		boolean noLhsCopy = true;
		if((context & CONTEXT_LHS_OR_RHS)==CONTEXT_LHS) {
			if(isCopy) {
				reportError("LHS copy<> not allowed");
				noLhsCopy = false;
			}
		}

		boolean noLhsNameOrAttributeInit = true;
		if((context & CONTEXT_LHS_OR_RHS)==CONTEXT_LHS) {
			if(nameOrAttributeInits.children.size()>0) {
				reportError("A name or attribute initialization is not allowed in the pattern");
				noLhsNameOrAttributeInit = false;
			}
		}

		boolean atMostOneNameInit = true;
		boolean nameInitFound = false;
		for(NameOrAttributeInitializationNode nain : nameOrAttributeInits.children) {
			if(nain.attributeUnresolved == null) {
				if(!nameInitFound)
					nameInitFound = true;
				else {
					reportError("Only one name initialization allowed");
					atMostOneNameInit = false;
				}
			}
		}

		return super.checkLocal()
			& typeChecker.check(getValidResolvedVersion(typeNodeDecl, typeTypeDecl), error)
			& noLhsCopy
			& noLhsNameOrAttributeInit
			& atMostOneNameInit;
	}

	/**
	 * Yields a dummy <code>NodeDeclNode</code> needed as
	 * dummy tgt or src node for dangling edges.
	 */
	public static NodeDeclNode getDummy(IdentNode id, BaseNode type, int context, PatternGraphNode directlyNestingLHSGraph) {
		return new DummyNodeDeclNode(id, type, context, directlyNestingLHSGraph);
	}

	public boolean isDummy() {
		return false;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor() */
	@Override
	public Color getNodeColor() {
		return Color.GREEN;
	}

	/** @see de.unika.ipd.grgen.ast.NodeCharacter#getNode() */
	public Node getNode() {
		return checkIR(Node.class);
	}

	protected final boolean inheritsType() {
		assert isResolved();

		return typeNodeDecl != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		NodeTypeNode tn = getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		Node node = new Node(ident.getIdent(), nt, ident.getAnnotations(), 
				directlyNestingLHSGraph!=null ? directlyNestingLHSGraph.getGraph() : null,
				isMaybeDeleted(), isMaybeRetyped(), defEntityToBeYieldedTo, context);
		node.setConstraints(getConstraints());

		if( node.getConstraints().contains(node.getType()) ) {
			error.error(getCoords(), "Self NodeType may not be contained in TypeCondition of Node "
							+ "("+ node.getType() + ")");
		}

		if(inheritsType()) {
			node.setTypeof(typeNodeDecl.checkIR(Node.class), isCopy);
		}

		node.setMaybeNull(maybeNull);

		if(initialization!=null) {
			initialization = initialization.evaluate();
			node.setInitialization(initialization.checkIR(Expression.class));
		}
		
		for(NameOrAttributeInitializationNode nain : nameOrAttributeInits.children) {
			nain.ownerIR = node;
			node.addNameOrAttributeInitialization(nain.checkIR(NameOrAttributeInitialization.class));
		}

		return node;
	}

	public static String getKindStr() {
		return "node declaration";
	}
	public static String getUseStr() {
		return "node";
	}
}

