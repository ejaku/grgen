/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System;
	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using VisitedNode = de.unika.ipd.grgen.ast.expr.graph.VisitedNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using CompoundAssignment = de.unika.ipd.grgen.ir.stmt.CompoundAssignment;
	using CompoundAssignmentChanged = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChanged;
	using CompoundAssignmentChangedVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVar;
	using CompoundAssignmentChangedVisited = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentChangedVisited;
	using CompoundAssignmentVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVar;
	using CompoundAssignmentVarChanged = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChanged;
	using CompoundAssignmentVarChangedVar = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVar;
	using CompoundAssignmentVarChangedVisited = de.unika.ipd.grgen.ir.stmt.CompoundAssignmentVarChangedVisited;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class CompoundAssignNode : EvalStatementNode
	{
		static CompoundAssignNode()
		{
			SetClassName(typeof(CompoundAssignNode), "compound assign statement");
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
			: base(coords)
		{
			this.targetUnresolved = BecomeParent(target);
			this.compoundAssignmentType = compoundAssignmentType;
			this.valueExpr = BecomeParent(valueExpr);
			this.targetChangedUnresolved = BecomeParent(targetChanged);
			this.targetCompoundAssignmentType = targetCompoundAssignmentType;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(targetUnresolved, targetQual, targetVar));
				children.Add(valueExpr);
				if(targetChangedUnresolved != null)
				{
					children.Add(GetValidVersion(targetChangedUnresolved,
							targetChangedQual, targetChangedVar, targetChangedVis));
				}
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("target");
				childrenNames.Add("valueExpr");
				if(targetChangedUnresolved != null)
					childrenNames.Add("targetChanged");
				return childrenNames;
			}
		}

		public virtual BaseNode ValidTarget
		{
			get
			{
				return targetQual != null ? (BaseNode)targetQual : (BaseNode)targetVar;
			}
		}

		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			if(targetUnresolved is IdentExprNode)
			{
				IdentExprNode unresolved = (IdentExprNode)targetUnresolved;
				if(unresolved.Resolve() && unresolved.decl is VarDeclNode)
					targetVar = (VarDeclNode)unresolved.decl;
				else
				{
					ReportError("Error in resolving the left hand side of the compound assignment, a parameter variable is expected (given is " + unresolved.Ident + ").");
					successfullyResolved = false;
				}
			}
			else if(targetUnresolved is QualIdentNode)
			{
				QualIdentNode unresolved = (QualIdentNode)targetUnresolved;
				if(unresolved.Resolve())
					targetQual = unresolved;
				else
				{
					ReportError("Error in resolving the left hand side of the compound assignment, a qualified attribute is expected (given is " + unresolved + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Internal error - invalid left hand side in compound assignment.");
				successfullyResolved = false;
			}

			if(targetChangedUnresolved != null)
			{
				if(targetChangedUnresolved is IdentExprNode)
				{
					IdentExprNode unresolved = (IdentExprNode)targetChangedUnresolved;
					if(unresolved.Resolve() && unresolved.decl is VarDeclNode)
						targetChangedVar = (VarDeclNode)unresolved.decl;
					else
					{
						ReportError("Error in resolving the changement assign target of the compound assignment, a parameter variable is expected (given is " + unresolved.Ident + ").");
						successfullyResolved = false;
					}
				}
				else if(targetChangedUnresolved is QualIdentNode)
				{
					QualIdentNode unresolved = (QualIdentNode)targetChangedUnresolved;
					if(unresolved.Resolve())
						targetChangedQual = unresolved;
					else
					{
						ReportError("Error in resolving the changement assign target of the compound assignment, a qualified attribute is expected (given is " + unresolved + ").");
						successfullyResolved = false;
					}
				}
				else if(targetChangedUnresolved is VisitedNode)
				{
					VisitedNode unresolved = (VisitedNode)targetChangedUnresolved;
					if(unresolved.Resolve())
						targetChangedVis = unresolved;
					else
					{
						ReportError("Error in resolving the changement assign target of the compound assignment, a visited flag is expected (given is " + unresolved + ").");
						successfullyResolved = false;
					}
				}
				else
				{
					ReportError("Internal error - invalid changement assign target in compound assignment.");
					successfullyResolved = false;
				}
			}

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode targetType = targetQual != null ? targetQual.Decl.DeclType : targetVar.DeclType;
			if(compoundAssignmentType == CompoundAssignmentType.CONCATENATE
					&& !(targetType is ArrayTypeNode || targetType is DequeTypeNode))
			{
				ValidTarget.ReportError("Compound assignment expects a left hand side of array or deque type"
						+ " (given is type " + targetType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			if(compoundAssignmentType != CompoundAssignmentType.CONCATENATE
					&& !(targetType is SetTypeNode || targetType is MapTypeNode))
			{
				ValidTarget.ReportError("Compound assignment expects a left hand side of set or map type"
					+ " (given is type " + targetType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode exprType = valueExpr.Type;
			if(!exprType.IsEqual(targetType))
			{
				valueExpr.ReportError("Cannot compound-assign a value of type " + exprType.ToStringWithDeclarationCoords()
						+ " to a variable of type " + targetType.ToStringWithDeclarationCoords() + ".");
				return false;
			}
			if(targetChangedUnresolved != null)
			{
				TypeNode targetChangedType = null;
				if(targetChangedQual != null)
					targetChangedType = targetChangedQual.Decl.DeclType;
				else if(targetChangedVar != null)
					targetChangedType = targetChangedVar.DeclType;
				else if(targetChangedVis != null)
					targetChangedType = targetChangedVis.Type;
				if(targetChangedType != BasicTypeNode.booleanType)
				{
					targetChangedUnresolved.ReportError("The type of the target of the changement assignment"
							+ " of the compound assignment must be boolean"
							+ " (but given is " + targetChangedType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			valueExpr = valueExpr.Evaluate();
			if(targetQual != null)
			{
				if(targetChangedQual != null)
				{
					return new CompoundAssignmentChanged(targetQual.CheckIR<Qualification>(typeof(Qualification)),
							MapCompoundAssignmentType(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedQual.CheckIR<Qualification>(typeof(Qualification)));
				}
				else if(targetChangedVar != null)
				{
					return new CompoundAssignmentChangedVar(targetQual.CheckIR<Qualification>(typeof(Qualification)),
							MapCompoundAssignmentType(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedVar.CheckIR<Variable>(typeof(Variable)));
				}
				else if(targetChangedVis != null)
				{
					return new CompoundAssignmentChangedVisited(targetQual.CheckIR<Qualification>(typeof(Qualification)),
							MapCompoundAssignmentType(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentType(targetCompoundAssignmentType), targetChangedVis.CheckIR<Visited>(typeof(Visited)));
				}
				else
				{
					return new CompoundAssignment(targetQual.CheckIR<Qualification>(typeof(Qualification)),
							MapCompoundAssignmentType(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)));
				}
			}
			else
			{
				if(targetChangedQual != null)
				{
					return new CompoundAssignmentVarChanged(targetVar.CheckIR<Variable>(typeof(Variable)),
							MapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedQual.CheckIR<Qualification>(typeof(Qualification)));
				}
				else if(targetChangedVar != null)
				{
					return new CompoundAssignmentVarChangedVar(targetVar.CheckIR<Variable>(typeof(Variable)),
							MapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedVar.CheckIR<Variable>(typeof(Variable)));
				}
				else if(targetChangedVis != null)
				{
					return new CompoundAssignmentVarChangedVisited(targetVar.CheckIR<Variable>(typeof(Variable)),
							MapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)),
							MapCompoundAssignmentTypeVar(targetCompoundAssignmentType), targetChangedVis.CheckIR<Visited>(typeof(Visited)));
				}
				else
				{
					return new CompoundAssignmentVar(targetVar.CheckIR<Variable>(typeof(Variable)),
							MapCompoundAssignmentTypeVar(compoundAssignmentType), valueExpr.CheckIR<Expression>(typeof(Expression)));
				}
			}
		}

		internal virtual CompoundAssignment.CompoundAssignmentType MapCompoundAssignmentType(CompoundAssignmentType type)
		{
			switch(type)
			{
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.NONE:
				return CompoundAssignment.CompoundAssignmentType.NONE;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.UNION:
				return CompoundAssignment.CompoundAssignmentType.UNION;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.INTERSECTION:
				return CompoundAssignment.CompoundAssignmentType.INTERSECTION;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.WITHOUT:
				return CompoundAssignment.CompoundAssignmentType.WITHOUT;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.CONCATENATE:
				return CompoundAssignment.CompoundAssignmentType.CONCATENATE;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.ASSIGN:
				return CompoundAssignment.CompoundAssignmentType.ASSIGN;
			default:
				throw new Exception("Internal failure");
			}
		}

		internal virtual CompoundAssignmentVar.CompoundAssignmentType MapCompoundAssignmentTypeVar(CompoundAssignmentType type)
		{
			switch(type)
			{
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.NONE:
				return CompoundAssignmentVar.CompoundAssignmentType.NONE;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.UNION:
				return CompoundAssignmentVar.CompoundAssignmentType.UNION;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.INTERSECTION:
				return CompoundAssignmentVar.CompoundAssignmentType.INTERSECTION;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.WITHOUT:
				return CompoundAssignmentVar.CompoundAssignmentType.WITHOUT;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.CONCATENATE:
				return CompoundAssignmentVar.CompoundAssignmentType.CONCATENATE;
			case de.unika.ipd.grgen.ast.stmt.CompoundAssignNode.CompoundAssignmentType.ASSIGN:
				return CompoundAssignmentVar.CompoundAssignmentType.ASSIGN;
			default:
				throw new Exception("Internal failure");
			}
		}
	}

}
