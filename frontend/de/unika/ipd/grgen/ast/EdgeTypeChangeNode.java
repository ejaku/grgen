/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack, Adam Szalkowski
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.RetypedEdge;

/**
 *
 */
public class EdgeTypeChangeNode extends EdgeDeclNode implements EdgeCharacter {
	static {
		setName(EdgeTypeChangeNode.class, "edge type change decl");
	}

	BaseNode oldUnresolved;
	EdgeDeclNode old = null;

	public EdgeTypeChangeNode(IdentNode id, BaseNode newType, int context, BaseNode oldid, PatternGraphNode directlyNestingLHSGraph) {
		super(id, newType, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.oldUnresolved = oldid;
		becomeParent(this.oldUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(oldUnresolved, old));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("old");
		return childrenNames;
	}

	private static final DeclarationResolver<EdgeDeclNode> edgeResolver = new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();

		old = edgeResolver.resolve(oldUnresolved, this);
		if(old != null)
			old.retypedElem = this;

		return successfullyResolved && old != null;
	}

	/** @return the original edge for this retyped edge */
	public EdgeDeclNode getOldEdge() {
		assert isResolved();
		return old;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		boolean res = super.checkLocal()
			& edgeChecker.check(old, error);
		if (!res) {
			return false;
		}

		// check if source edge of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode
				|| curr instanceof SubpatternDeclNode
				|| curr instanceof AlternativeCaseNode)) {
			prev = curr;
			// doesn't matter which parent you choose, in the end you reach RuleDeclNode/SubpatternDeclNode/AlternativeCaseNode
			curr = curr.getParents().iterator().next();
		}
		if (curr instanceof RuleDeclNode && prev == ((RuleDeclNode)curr).right
				|| curr instanceof SubpatternDeclNode && prev == ((SubpatternDeclNode)curr).right
				|| curr instanceof AlternativeCaseNode && prev == ((AlternativeCaseNode)curr).right) {
			reportError("Source edge of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same edge
		// declaration occurs
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			// to be erroneous there must be another EdgeTypeChangeNode with the same OLD-child
			// TODO: p.old == old always true, since p is a parent (of type EdgeTypeChangeNode) of old?
			if (p != this && p instanceof EdgeTypeChangeNode && (((EdgeTypeChangeNode)p).old == old)) {
				reportError("Two (and hence ambiguous) retype statements for the same edge are forbidden,"
								+ "previous retype statement at " + p.getCoords());
				res = false;
			}
		}

		return res & onlyReplacementEdgesAreAllowedToChangeType();
	}

	protected boolean onlyReplacementEdgesAreAllowedToChangeType() {
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			return true;
		}

		constraints.reportError("pattern edges are not allowed to change type, only replacement edges are");
		return false;
	}

	public Edge getEdge() {
		return checkIR(Edge.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		EdgeTypeNode etn = getDeclType();
		EdgeType et = etn.getEdgeType();
		IdentNode ident = getIdentNode();

		RetypedEdge res = new RetypedEdge(ident.getIdent(), et, ident.getAnnotations(),
				isMaybeDeleted(), isMaybeRetyped());

		Edge oldEdge = old.getEdge();
		oldEdge.setRetypedEdge(res);
		res.setOldEdge(oldEdge);
		res.directlyNestingLHSGraph = oldEdge.directlyNestingLHSGraph;

		if (inheritsType()) {
			res.setTypeof(typeEdgeDecl.checkIR(Edge.class));
		}

		return res;
	}
}

