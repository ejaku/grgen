/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ConstraintDeclNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.TypeExpr;

public abstract class ConstraintDeclNode extends DeclNode
{
	protected TypeExprNode constraints;

	protected int context; // context of declaration, contains CONTEXT_LHS if declaration is located on left hand side,
				 // or CONTEXT_RHS if declaration is located on right hand side

	protected PatternGraphNode directlyNestingLHSGraph;
	boolean defEntityToBeYieldedTo;

	/** The retyped version of this element if any. */
	protected ConstraintDeclNode retypedElem = null;

	protected boolean maybeDeleted = false;
	protected boolean maybeRetyped = false;
	protected boolean maybeNull = false; 

	
	protected ConstraintDeclNode(IdentNode id, BaseNode type, int context, TypeExprNode constraints,
			PatternGraphNode directlyNestingLHSGraph, boolean maybeNull, boolean defEntityToBeYieldedTo) {
		super(id, type);
		this.constraints = constraints;
		becomeParent(this.constraints);
		this.context = context;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.maybeNull = maybeNull;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
	}

	@Override
	protected boolean checkLocal() {
		return onlyPatternElementsAreAllowedToBeConstrained();
	}

	private boolean onlyPatternElementsAreAllowedToBeConstrained() {
		if(constraints!=TypeExprNode.getEmpty()) {
			if((context & CONTEXT_LHS_OR_RHS) != CONTEXT_LHS) {
				constraints.reportError("replacement elements are not allowed to be type constrained, only pattern elements are");
				return false;
			}
		}
		return true;
	}

	protected final TypeExpr getConstraints() {
		return constraints.checkIR(TypeExpr.class);
	}

	/** @returns True, if this element has eventually been deleted due to homomorphy */
	protected boolean isMaybeDeleted() {
		return maybeDeleted;
	}

	/** @returns True, if this element has eventually been retyped due to homomorphy */
	protected boolean isMaybeRetyped() {
		return maybeRetyped;
	}

	/** @returns the retyped version of this element or null. */
	protected ConstraintDeclNode getRetypedElement() {
		return retypedElem;
	}

	public abstract InheritanceTypeNode getDeclType();

	public static String getKindStr() {
		return "node or edge declaration";
	}

	public static String getUseStr() {
		return "node or edge";
	}
}

