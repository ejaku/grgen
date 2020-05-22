/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ConstraintDeclNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.type.TypeExpr;

import java.util.Collection;
import java.util.HashSet;

public abstract class ConstraintDeclNode extends DeclNode
{
	protected TypeExprNode constraints;

	public int context; // context of declaration, contains CONTEXT_LHS if declaration is located on left hand side,
						// or CONTEXT_RHS if declaration is located on right hand side

	public PatternGraphNode directlyNestingLHSGraph;
	public boolean defEntityToBeYieldedTo;

	protected boolean isCopy;

	/** The retyped version of this element if any. */
	protected ConstraintDeclNode retypedElem = null;

	public boolean maybeDeleted = false;
	public boolean maybeRetyped = false;
	protected boolean maybeNull = false;

	ExprNode initialization = null;

	CollectNode<NameOrAttributeInitializationNode> nameOrAttributeInits =
			new CollectNode<NameOrAttributeInitializationNode>();

	protected ConstraintDeclNode(IdentNode id, BaseNode type, boolean isCopy, int context, TypeExprNode constraints,
			PatternGraphNode directlyNestingLHSGraph, boolean maybeNull, boolean defEntityToBeYieldedTo)
	{
		super(id, type);
		this.isCopy = isCopy;
		this.constraints = constraints;
		becomeParent(this.constraints);
		this.context = context;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.maybeNull = maybeNull;
		this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
	}

	/** sets an expression to be used to initialize the graph entity, only used for local variables, not pattern elements */
	public void setInitialization(ExprNode initialization)
	{
		this.initialization = initialization;
	}

	public void addNameOrAttributeInitialization(NameOrAttributeInitializationNode nameOrAttributeInit)
	{
		this.nameOrAttributeInits.addChild(nameOrAttributeInit);
	}

	@Override
	protected boolean checkLocal()
	{
		return initializationIsWellTyped()
				& noRhsConstraint()
				& noLhsCopy()
				& noLhsNameOrAttributeInit()
				& atMostOneNameInit();
	}

	private boolean initializationIsWellTyped()
	{
		if(initialization == null)
			return true;

		TypeNode targetType = getDeclType();
		TypeNode exprType = initialization.getType();

		if(exprType.isEqual(targetType))
			return true;

		if(targetType instanceof NodeTypeNode && exprType instanceof NodeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof EdgeTypeNode) {
			Collection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.doGetCompatibleToTypes(superTypes);
			if(superTypes.contains(targetType)) {
				return true;
			}
		}

		error.error(getCoords(), "can't initialize " + targetType + " with " + exprType);
		return false;
	}

	private boolean noRhsConstraint()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			return true;

		if(constraints != TypeExprNode.getEmpty()) {
			constraints.reportError("replacement elements are not allowed to be type constrained, only pattern elements are");
			return false;
		}
		
		return true;
	}

	private boolean noLhsCopy()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			return true;
		
		if(isCopy) {
			reportError("LHS copy<> not allowed");
			return false;
		}
		
		return true;
	}

	private boolean noLhsNameOrAttributeInit()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) 
			return true;

		if(nameOrAttributeInits.size() > 0) {
			reportError("A name or attribute initialization is not allowed in the pattern");
			return false;
		}
		
		return true;
	}

	private boolean atMostOneNameInit()
	{
		boolean atMostOneNameInit = true;

		boolean nameInitFound = false;
		for(NameOrAttributeInitializationNode nain : nameOrAttributeInits.getChildren()) {
			if(nain.attributeUnresolved == null) {
				if(!nameInitFound)
					nameInitFound = true;
				else {
					reportError("Only one name initialization allowed");
					atMostOneNameInit = false;
				}
			}
		}
		
		return atMostOneNameInit;
	}

	protected final TypeExpr getConstraints()
	{
		return constraints.checkIR(TypeExpr.class);
	}

	/** @returns True, if this element has eventually been deleted due to homomorphy */
	protected boolean isMaybeDeleted()
	{
		return maybeDeleted;
	}

	/** @returns True, if this element has eventually been retyped due to homomorphy */
	protected boolean isMaybeRetyped()
	{
		return maybeRetyped;
	}

	/** @returns the retyped version of this element or null. */
	public ConstraintDeclNode getRetypedElement()
	{
		return retypedElem;
	}

	public abstract InheritanceTypeNode getDeclType();

	public static String getKindStr()
	{
		return "node or edge declaration";
	}

	public static String getUseStr()
	{
		return "node or edge";
	}
}
