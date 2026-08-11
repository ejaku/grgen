/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphRemoveProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRemoveProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class GraphRemoveProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphRemoveProcNode()
	{
		SetClassName(typeof(GraphRemoveProcNode), "graph remove procedure");
	}

	private ExprNode entityExpr;

	public GraphRemoveProcNode(Coords coords, ExprNode entityExpr)
		: base(coords)
	{

		this.entityExpr = entityExpr;
		BecomeParent(entityExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(entityExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("entity");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode entityExprType = entityExpr.Type;
		if(entityExprType is EdgeTypeNode)
			return true;
		if(entityExprType is NodeTypeNode)
			return true;
		ReportError("The rem procedure expects as argument (entity)"
				+ " a value of type Node or Edge"
				+ " (but is given a value of type " + entityExprType.ToStringWithDeclarationCoords() + ").");
		return false;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		entityExpr = entityExpr.Evaluate();
		return new GraphRemoveProc(entityExpr.CheckIR(typeof(Expression)));
	}
}

}
