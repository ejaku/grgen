/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.expr.graph.VisitedNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignment;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChanged;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVisited;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChanged;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVar;
import de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVisited;
import de.unika.ipd.grgen.parser.Coords;

public class CompoundAssignNode extends EvalStatementNode
{
	static {
		setName(CompoundAssignNode.class, "compound assign statement");
	}

	public enum CompoundAssignmentType
	{
		NONE,
		UNION,
		INTERSECTION,
		WITHOUT,
		CONCATENATE,
		ASSIGN
	}

	private BaseNode targetUnresolved; // QualIdentNode|IdentExprNode
	private CompoundAssignmentType compoundAssignmentType;
	private ExprNode valueExpr;
	private BaseNode targetChangedUnresolved; // QualIdentNode|IdentExprNode|VisitedNode|null
	private CompoundAssignmentType targetCompoundAssignmentType;

	private QualIdentNode targetQual;
	private VarDeclNode targetVar;
	private QualIdentNode targetChangedQual;
	private VarDeclNode targetChangedVar;
	private VisitedNode targetChangedVis;

	public CompoundAssignNode(Coords coords, BaseNode target, CompoundAssignmentType compoundAssignmentType, ExprNode valueExpr,
			CompoundAssignmentType targetCompoundAssignmentType, BaseNode targetChanged)
	{
		super(coords);
		this.targetUnresolved = becomeParent(target);
		this.compoundAssignmentType = compoundAssignmentType;
		this.valueExpr = becomeParent(valueExpr);
		this.targetChangedUnresolved = becomeParent(targetChanged);
		this.targetCompoundAssignmentType = targetCompoundAssignmentType;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(targetUnresolved, targetQual, targetVar));
		children.add(valueExpr);
		if(targetChangedUnresolved != null)
			children.add(getValidVersion(targetChangedUnresolved,
					targetChangedQual, targetChangedVar, targetChangedVis));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		if(targetChangedUnresolved != null)
			childrenNames.add("targetChanged");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		if(targetUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)targetUnresolved;
			if(unresolved.resolve() && unresolved.decl instanceof VarDeclNode) {
				targetVar = (VarDeclNode)unresolved.decl;
			} else {
				reportError("Error in resolving the left hand side of the compound assignment, a parameter variable is expected (given is " + unresolved.getIdent() + ").");
				successfullyResolved = false;
			}
		} else if(targetUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)targetUnresolved;
			if(unresolved.resolve()) {
				targetQual = unresolved;
			} else {
				reportError("Error in resolving the left hand side of the compound assignment, a qualified attribute is expected (given is " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid left hand side in compound assignment.");
			successfullyResolved = false;
		}

		if(targetChangedUnresolved != null) {
			if(targetChangedUnresolved instanceof IdentExprNode) {
				IdentExprNode unresolved = (IdentExprNode)targetChangedUnresolved;
				if(unresolved.resolve() && unresolved.decl instanceof VarDeclNode) {
					targetChangedVar = (VarDeclNode)unresolved.decl;
				} else {
					reportError("Error in resolving the changement assign target of the compound assignment, a parameter variable is expected (given is " + unresolved.getIdent() + ").");
					successfullyResolved = false;
				}
			} else if(targetChangedUnresolved instanceof QualIdentNode) {
				QualIdentNode unresolved = (QualIdentNode)targetChangedUnresolved;
				if(unresolved.resolve()) {
					targetChangedQual = unresolved;
				} else {
					reportError("Error in resolving the changement assign target of the compound assignment, a qualified attribute is expected (given is " + unresolved + ").");
					successfullyResolved = false;
				}
			} else if(targetChangedUnresolved instanceof VisitedNode) {
				VisitedNode unresolved = (VisitedNode)targetChangedUnresolved;
				if(unresolved.resolve()) {
					targetChangedVis = unresolved;
				} else {
					reportError("Error in resolving the changement assign target of the compound assignment, a visited flag is expected (given is " + unresolved + ").");
					successfullyResolved = false;
				}
			} else {
				reportError("Internal error - invalid changement assign target in compound assignment.");
				successfullyResolved = false;
			}
		}

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode targetType = targetQual != null ? targetQual.getDecl().getDeclType() : targetVar.getDeclType();
		if(compoundAssignmentType == CompoundAssignmentType.CONCATENATE
				&& !(targetType instanceof ArrayTypeNode || targetType instanceof DequeTypeNode)) {
			(targetQual != null ? targetQual : targetVar).reportError("Compound assignment expects a left hand side of array or deque type"
					+ " (given is type " + targetType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		if(compoundAssignmentType != CompoundAssignmentType.CONCATENATE
				&& !(targetType instanceof SetTypeNode || targetType instanceof MapTypeNode)) {
			(targetQual != null ? targetQual : targetVar).reportError("Compound assignment expects a left hand side of set or map type"
					+ " (given is type " + targetType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode exprType = valueExpr.getType();
		if(!exprType.isEqual(targetType)) {
			valueExpr.reportError("Cannot compound-assign a value of type " + exprType.toStringWithDeclarationCoords()
					+ " to a variable of type " + targetType.toStringWithDeclarationCoords() + ".");
			return false;
		}
		if(targetChangedUnresolved != null) {
			TypeNode targetChangedType = null;
			if(targetChangedQual != null)
				targetChangedType = targetChangedQual.getDecl().getDeclType();
			else if(targetChangedVar != null)
				targetChangedType = targetChangedVar.getDeclType();
			else if(targetChangedVis != null)
				targetChangedType = targetChangedVis.getType();
			if(targetChangedType != BasicTypeNode.booleanType) {
				targetChangedUnresolved.reportError("The type of the target of the changement assignment"
						+ " of the compound assignment must be boolean"
						+ " (but given is " + targetChangedType.toStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		valueExpr = valueExpr.evaluate();
		if(targetQual != null) {
			if(targetChangedQual != null)
				return new CompoundAssignmentChanged(targetQual.checkIR(Qualification.class),
						mapCompoundAssignmentType(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedQual.checkIR(Qualification.class));
			else if(targetChangedVar != null)
				return new CompoundAssignmentChangedVar(targetQual.checkIR(Qualification.class),
						mapCompoundAssignmentType(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedVar.checkIR(Variable.class));
			else if(targetChangedVis != null)
				return new CompoundAssignmentChangedVisited(targetQual.checkIR(Qualification.class),
						mapCompoundAssignmentType(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedVis.checkIR(Visited.class));
			else
				return new CompoundAssignment(targetQual.checkIR(Qualification.class),
						mapCompoundAssignmentType(compoundAssignmentType), valueExpr.checkIR(Expression.class));
		} else {
			if(targetChangedQual != null)
				return new CompoundAssignmentVarChanged(targetVar.checkIR(Variable.class),
						mapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedQual.checkIR(Qualification.class));
			else if(targetChangedVar != null)
				return new CompoundAssignmentVarChangedVar(targetVar.checkIR(Variable.class),
						mapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedVar.checkIR(Variable.class));
			else if(targetChangedVis != null)
				return new CompoundAssignmentVarChangedVisited(targetVar.checkIR(Variable.class),
						mapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.checkIR(Expression.class),
						mapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedVis.checkIR(Visited.class));
			else
				return new CompoundAssignmentVar(targetVar.checkIR(Variable.class),
						mapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.checkIR(Expression.class));
		}
	}
	
	CompoundAssignment.CompoundAssignmentType mapCompoundAssignmentType(CompoundAssignmentType type)
	{
		switch(type)
		{
		case NONE: return CompoundAssignment.CompoundAssignmentType.NONE;
		case UNION: return CompoundAssignment.CompoundAssignmentType.UNION;
		case INTERSECTION: return CompoundAssignment.CompoundAssignmentType.INTERSECTION;
		case WITHOUT: return CompoundAssignment.CompoundAssignmentType.WITHOUT;
		case CONCATENATE: return CompoundAssignment.CompoundAssignmentType.CONCATENATE;
		case ASSIGN: return CompoundAssignment.CompoundAssignmentType.ASSIGN;
		default: throw new RuntimeException("Internal failure");
		}
	}
	
	CompoundAssignmentVar.CompoundAssignmentType mapCompoundAssignmentTypeVar(CompoundAssignmentType type)
	{
		switch(type)
		{
		case NONE: return CompoundAssignmentVar.CompoundAssignmentType.NONE;
		case UNION: return CompoundAssignmentVar.CompoundAssignmentType.UNION;
		case INTERSECTION: return CompoundAssignmentVar.CompoundAssignmentType.INTERSECTION;
		case WITHOUT: return CompoundAssignmentVar.CompoundAssignmentType.WITHOUT;
		case CONCATENATE: return CompoundAssignmentVar.CompoundAssignmentType.CONCATENATE;
		case ASSIGN: return CompoundAssignmentVar.CompoundAssignmentType.ASSIGN;
		default: throw new RuntimeException("Internal failure");
		}
	}
}
