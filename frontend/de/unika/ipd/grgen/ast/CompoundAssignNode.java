/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SetUnionWithoutNode.java 22995 2008-10-18 14:51:18Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.CompoundAssignment;
import de.unika.ipd.grgen.ir.CompoundAssignmentChanged;
import de.unika.ipd.grgen.ir.CompoundAssignmentChangedVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentChangedVisited;
import de.unika.ipd.grgen.ir.CompoundAssignmentVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChanged;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChangedVar;
import de.unika.ipd.grgen.ir.CompoundAssignmentVarChangedVisited;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.Visited;
import de.unika.ipd.grgen.parser.Coords;

public class CompoundAssignNode extends EvalStatementNode
{
	static {
		setName(CompoundAssignNode.class, "compound assign statement");
	}

	public static final int NONE = -1;
	public static final int UNION = 0;
	public static final int INTERSECTION = 2;
	public static final int WITHOUT = 3;
	public static final int ASSIGN = 4;
		
	private BaseNode targetUnresolved; // QualIdentNode|IdentExprNode
	private int compoundAssignmentType;
	private ExprNode valueExpr;
	private BaseNode targetChangedUnresolved; // QualIdentNode|IdentExprNode|VisitedNode|null
	private int targetCompoundAssignmentType;
	
	private QualIdentNode targetQual;
	private VarDeclNode targetVar;
	private QualIdentNode targetChangedQual;
	private VarDeclNode targetChangedVar;
	private VisitedNode targetChangedVis;

	public CompoundAssignNode(Coords coords, BaseNode target, int compoundAssignmentType, ExprNode valueExpr,
			int targetCompoundAssignmentType, BaseNode targetChanged)
	{
		super(coords);
		this.targetUnresolved = becomeParent(target);
		this.compoundAssignmentType = compoundAssignmentType;
		this.valueExpr = becomeParent(valueExpr);
		this.targetChangedUnresolved = becomeParent(targetChanged);
		this.targetCompoundAssignmentType = targetCompoundAssignmentType;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(targetUnresolved, targetQual, targetVar));
		children.add(valueExpr);
		if(targetChangedUnresolved!=null)
			children.add(getValidVersion(targetChangedUnresolved, targetChangedQual, targetChangedVar, targetChangedVis));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		if(targetChangedUnresolved!=null)
			childrenNames.add("targetChanged");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		
		if(targetUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)targetUnresolved;
			if(unresolved.resolve() && unresolved.decl instanceof VarDeclNode) {
				targetVar = (VarDeclNode)unresolved.decl;
			} else {
				reportError("compound assign expects a parameter variable.");
				successfullyResolved = false;
			}
		} else if(targetUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)targetUnresolved;
			if(unresolved.resolve()) {
				targetQual = unresolved;
			} else {
				reportError("compound assign expects a qualified attribute.");
				successfullyResolved = false;
			}
		} else {
			reportError("internal error - invalid target in compound assign");
			successfullyResolved = false;
		}

		if(targetChangedUnresolved!=null)
		{
			if(targetChangedUnresolved instanceof IdentExprNode) {
				IdentExprNode unresolved = (IdentExprNode)targetChangedUnresolved;
				if(unresolved.resolve() && unresolved.decl instanceof VarDeclNode) {
					targetChangedVar = (VarDeclNode)unresolved.decl;
				} else {
					reportError("compound assign changement assign expects a parameter variable.");
					successfullyResolved = false;
				}
			} else if(targetChangedUnresolved instanceof QualIdentNode) {
				QualIdentNode unresolved = (QualIdentNode)targetChangedUnresolved;
				if(unresolved.resolve()) {
					targetChangedQual = unresolved;
				} else {
					reportError("compound assign changement assign expects a qualified attribute.");
					successfullyResolved = false;
				}
			} else if(targetChangedUnresolved instanceof VisitedNode) {
				VisitedNode unresolved = (VisitedNode)targetChangedUnresolved;
				if(unresolved.resolve()) {
					targetChangedVis = unresolved;
				} else {
					reportError("compound assign changement assign expects a visited flag.");
					successfullyResolved = false;
				}
			} else {
				reportError("internal error - invalid changement assign target in compound assign");
				successfullyResolved = false;
			}
		}
		
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetQual!=null ? targetQual.getDecl().getDeclType() : targetVar.getDeclType();
		if(!(targetType instanceof SetTypeNode || targetType instanceof MapTypeNode)) {
			reportError("compound assignment expects a target of set or map type.");
			return false;
		}
		TypeNode exprType = valueExpr.getType();
		if (!exprType.isEqual(targetType))
		{
			valueExpr.reportError("the expression value to the "
					+ "compound assignment must be of type " +targetType.toString());
			return false;
		}
		if(targetChangedUnresolved!=null) {
			TypeNode targetChangedType = null;
			if(targetChangedQual!=null) 
				targetChangedType = targetChangedQual.getDecl().getDeclType();
			else if(targetChangedVar!=null)
				targetChangedType = targetChangedVar.getDeclType();
			else if(targetChangedVis!=null)
				targetChangedType = targetChangedVis.getType();
			if(targetChangedType != BasicTypeNode.booleanType) {
				targetChangedUnresolved.reportError("the type of the target of the changement assignment "
						+ "of the compound assignment must be of boolean type ");
				return false;
			}
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		if(targetQual!=null) {
			if(targetChangedQual!=null)
				return new CompoundAssignmentChanged(targetQual.checkIR(Qualification.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedQual.checkIR(Qualification.class));
			else if(targetChangedVar!=null)
				return new CompoundAssignmentChangedVar(targetQual.checkIR(Qualification.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedVar.checkIR(Variable.class));
			else if(targetChangedVis!=null)
				return new CompoundAssignmentChangedVisited(targetQual.checkIR(Qualification.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedVis.checkIR(Visited.class));
			else
				return new CompoundAssignment(targetQual.checkIR(Qualification.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class));
		} else {
			if(targetChangedQual!=null)
				return new CompoundAssignmentVarChanged(targetVar.checkIR(Variable.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedQual.checkIR(Qualification.class));
			else if(targetChangedVar!=null)
				return new CompoundAssignmentVarChangedVar(targetVar.checkIR(Variable.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedVar.checkIR(Variable.class));
			else if(targetChangedVis!=null)
				return new CompoundAssignmentVarChangedVisited(targetVar.checkIR(Variable.class),
						compoundAssignmentType, valueExpr.checkIR(Expression.class),
						targetCompoundAssignmentType, targetChangedVis.checkIR(Visited.class));
			else
				return new CompoundAssignmentVar(targetVar.checkIR(Variable.class), 
						compoundAssignmentType, valueExpr.checkIR(Expression.class));
		}
	}
}
