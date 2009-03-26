/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.IR;
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

	protected static final DeclarationPairResolver<NodeDeclNode, TypeDeclNode> typeResolver =
		new DeclarationPairResolver<NodeDeclNode, TypeDeclNode>(NodeDeclNode.class, TypeDeclNode.class);

	/**
	 * Make a new node declaration.
	 * @param id The identifier of the node.
	 * @param type The type of the node.
	 */
	public NodeDeclNode(IdentNode id, BaseNode type, int context, TypeExprNode constr, PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, context, constr, directlyNestingLHSGraph);
	}

	/** The TYPE child could be a node in case the type is
	 *  inherited dynamically via the typeof operator */
	public NodeTypeNode getDeclType() {
		assert isResolved() : "not resolved";

		DeclNode curr = getValidResolvedVersion(typeNodeDecl, typeTypeDecl);
		return (NodeTypeNode) curr.getDeclType();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
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
					reportError("Circular typeofs are not allowed");
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
			typeUnresolved.reportError("Type of node \"" + getIdentNode() + "\" must be a node type");
			return false;
		}

		return true;
	}

	/** Returns whether the node type is a typeof statement. */
	public boolean hasTypeof() {
		assert isResolved();

		return typeNodeDecl != null;
	}

	private static final Checker typeChecker = new TypeChecker(NodeTypeNode.class);

	protected boolean checkLocal() {
		return super.checkLocal()
			& typeChecker.check(getValidResolvedVersion(typeNodeDecl, typeTypeDecl), error);
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
	public Color getNodeColor() {
		return Color.GREEN;
	}

	/** @see de.unika.ipd.grgen.ast.NodeCharacter#getNode() */
	public Node getNode() {
		return checkIR(Node.class);
	}

	protected boolean inheritsType() {
		return typeNodeDecl != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		Node res = new Node(ident.getIdent(), nt, ident.getAnnotations(),
				directlyNestingLHSGraph.getGraph(), isMaybeDeleted(), isMaybeRetyped());
		res.setConstraints(getConstraints());

		if( res.getConstraints().contains(res.getType()) ) {
			error.error(getCoords(), "Self NodeType may not be contained in TypeCondition of Node "
							+ "("+ res.getType() + ")");
		}

		if(inheritsType()) {
			res.setTypeof(typeNodeDecl.checkIR(Node.class));
		}

		return res;
	}

	public static String getKindStr() {
		return "node declaration";
	}
	public static String getUseStr() {
		return "node";
	}
}

