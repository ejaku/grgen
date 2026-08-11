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
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SynchronizationExitProc = de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationExitProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class SynchronizationExitProcNode : BuiltinProcedureInvocationBaseNode
{
	static SynchronizationExitProcNode()
	{
		SetClassName(typeof(SynchronizationExitProcNode), "synchronization exit procedure");
	}

	private ExprNode criticalSectionObjectExpr;

	public SynchronizationExitProcNode(Coords coords, ExprNode criticalSectionObjectExpr)
		: base(coords)
	{

		this.criticalSectionObjectExpr = BecomeParent(criticalSectionObjectExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(criticalSectionObjectExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("criticalSectionObjectExpr");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode criticalSectionObjectExprType = criticalSectionObjectExpr.Type;
		if(!criticalSectionObjectExprType.IsLockableType())
		{
			criticalSectionObjectExpr.ReportError("The Synchronization::exit procedure expects as argument (criticalSectionObject)"
					+ " a value that is not of basic type (with exception of type object)"
					+ " (but is given a value of type " + criticalSectionObjectExprType.ToStringWithDeclarationCoords() + ").");
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
		criticalSectionObjectExpr = criticalSectionObjectExpr.Evaluate();
		return new SynchronizationExitProc(criticalSectionObjectExpr.CheckIR(typeof(Expression)));
	}
}

}
