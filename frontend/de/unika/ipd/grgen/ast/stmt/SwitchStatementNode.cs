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

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using CaseStatement = de.unika.ipd.grgen.ir.stmt.CaseStatement;
using SwitchStatement = de.unika.ipd.grgen.ir.stmt.SwitchStatement;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a switch statement.
/// </summary>
public class SwitchStatementNode : EvalStatementNode
{
	static SwitchStatementNode()
	{
		SetClassName(typeof(SwitchStatementNode), "SwitchStatement");
	}

	private ExprNode switchExpr;
	internal CollectNode<CaseStatementNode> cases;

	public SwitchStatementNode(Coords coords, ExprNode switchExpr, CollectNode<CaseStatementNode> cases)
		: base(coords)
	{
		this.switchExpr = switchExpr;
		BecomeParent(switchExpr);
		this.cases = cases;
		BecomeParent(this.cases);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(switchExpr);
		children.Add(cases);
		return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("switchExpr");
		childrenNames.Add("cases");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode switchExprType = switchExpr.Type;
		if(!(switchExprType.IsEqual(BasicTypeNode.byteType))
				&& !(switchExprType.IsEqual(BasicTypeNode.shortType))
				&& !(switchExprType.IsEqual(BasicTypeNode.intType))
				&& !(switchExprType.IsEqual(BasicTypeNode.longType))
				&& !(switchExprType.IsEqual(BasicTypeNode.booleanType))
				&& !(switchExprType.IsEqual(BasicTypeNode.stringType))
				&& !(switchExprType is EnumTypeNode))
		{
			ReportError("The expression switched upon must be of type byte or short or int or long or boolean or string or enum,"
					+ " but is of type " + switchExprType.ToStringWithDeclarationCoords() + ".");
			return false;
		}
		bool defaultVisited = false;
		foreach(CaseStatementNode caseStmt in cases.ChildrenExact)
		{
			ExprNode caseConstantExpr = caseStmt.caseConstantExpr;
			if(caseConstantExpr != null)
			{
				// just to be sure, the syntax as-such is not allowing non-constants 
				if(!(caseConstantExpr.Evaluate() is ConstNode))
				{
					caseStmt.ReportError("A case statement of a switch statement expects a constant expression.");
					return false;
				}
				TypeNode caseConstantExprType = caseConstantExpr.Type;
				if(!(caseConstantExprType.IsCompatibleTo(switchExprType)))
				{
					caseStmt.ReportError("The type " + caseConstantExprType.ToStringWithDeclarationCoords() + " of the case expression"
							+ " is not compatible to the type " + switchExprType.ToStringWithDeclarationCoords() + " of the switch expression.");
					return false;
				}
			}
			else
			{
				if(defaultVisited)
				{
					caseStmt.ReportError("Only one else branch allowed per switch.");
					return false;
				}
				defaultVisited = true;
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
		switchExpr = switchExpr.Evaluate();
		SwitchStatement switchStmt = new SwitchStatement(switchExpr.CheckIR(typeof(Expression)));
		foreach(EvalStatementNode statement in cases.ChildrenExact)
			switchStmt.AddStatement(statement.CheckIR(typeof(CaseStatement)));
		return switchStmt;
	}
}

}
