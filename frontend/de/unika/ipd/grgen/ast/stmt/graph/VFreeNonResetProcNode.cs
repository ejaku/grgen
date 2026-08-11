/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.graph
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
using VFreeNonResetProc = de.unika.ipd.grgen.ir.stmt.graph.VFreeNonResetProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class VFreeNonResetProcNode : BuiltinProcedureInvocationBaseNode
{
	static VFreeNonResetProcNode()
	{
		SetClassName(typeof(VFreeNonResetProcNode), "vfreenonreset procedure");
	}

	private ExprNode visFlagExpr;

	public VFreeNonResetProcNode(Coords coords, ExprNode visFlagExpr)
		: base(coords)
	{

		this.visFlagExpr = BecomeParent(visFlagExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(visFlagExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("visFlagExpr");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode visFlagExprType = visFlagExpr.Type;
		if(!visFlagExprType.IsEqual(BasicTypeNode.intType))
		{
			visFlagExpr.ReportError("The vfreenonreset procedure expects as argument (visitedFlagId)"
					+ " a value of type int"
					+ " (but is given a value of type " + visFlagExprType.ToStringWithDeclarationCoords() + ").");
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
		visFlagExpr = visFlagExpr.Evaluate();
		return new VFreeNonResetProc(visFlagExpr.CheckIR(typeof(Expression)));
	}
}

}
