/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using RollbackTransactionProc = de.unika.ipd.grgen.ir.stmt.procenv.RollbackTransactionProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class RollbackTransactionProcNode : BuiltinProcedureInvocationBaseNode
{
	static RollbackTransactionProcNode()
	{
		SetClassName(typeof(RollbackTransactionProcNode), "rollback transaction procedure");
	}

	private ExprNode transactionIdExpr;

	public RollbackTransactionProcNode(Coords coords, ExprNode transactionIdExpr)
		: base(coords)
	{

		this.transactionIdExpr = BecomeParent(transactionIdExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(transactionIdExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("transactionIdExpr");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode transactionIdExprType = transactionIdExpr.Type;
		if(!transactionIdExprType.IsEqual(BasicTypeNode.intType))
		{
			transactionIdExpr.ReportError("The Transaction::rollback procedure expects as argument (transactionId)"
					+ " a value of type int"
					+ " (but is given a value of type " + transactionIdExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		transactionIdExpr = transactionIdExpr.Evaluate();
		return new RollbackTransactionProc(transactionIdExpr.CheckIR(typeof(Expression)));
	}
}

}
