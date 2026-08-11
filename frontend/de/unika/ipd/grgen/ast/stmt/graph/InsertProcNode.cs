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
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using InsertProc = de.unika.ipd.grgen.ir.stmt.graph.InsertProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node for inserting the subgraph to the given main graph (destroying the original graph).
/// </summary>
public class InsertProcNode : BuiltinProcedureInvocationBaseNode
{
	static InsertProcNode()
	{
		SetClassName(typeof(InsertProcNode), "insert procedure");
	}

	private ExprNode graphExpr;

	public InsertProcNode(Coords coords, ExprNode graphExpr)
		: base(coords)
	{
		this.graphExpr = graphExpr;
		BecomeParent(this.graphExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(graphExpr);
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
			childrenNames.Add("graphExpr");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		TypeNode graphExprType = graphExpr.Type;
		if(!(graphExprType.Equals(BasicTypeNode.graphType)))
		{
			ReportError("The insert procedure expects as argument (subgraphToInsertIntoTheCurrentGraph)"
					+ " a value of type graph"
					+ " (but is given a value of type " + graphExprType.ToStringWithDeclarationCoords() + ").");
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
		graphExpr = graphExpr.Evaluate();
		InsertProc insert = new InsertProc(graphExpr.CheckIR(typeof(Expression)));
		return insert;
	}
}

}
