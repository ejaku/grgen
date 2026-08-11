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
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using InsertDefinedSubgraphProc = de.unika.ipd.grgen.ir.stmt.graph.InsertDefinedSubgraphProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding an inserted edge of the insertion of a defined subgraph of an edge set.
/// </summary>
public class InsertDefinedSubgraphProcNode : BuiltinProcedureInvocationBaseNode
{
	static InsertDefinedSubgraphProcNode()
	{
		SetClassName(typeof(InsertDefinedSubgraphProcNode), "insert defined subgraph procedure");
	}

	private ExprNode edgeSetExpr;
	private ExprNode edgeExpr;

	internal IList<TypeNode> returnTypes;

	public InsertDefinedSubgraphProcNode(Coords coords, ExprNode edgeSetExpr, ExprNode edgeExpr)
		: base(coords)
	{
		this.edgeSetExpr = edgeSetExpr;
		BecomeParent(this.edgeSetExpr);
		this.edgeExpr = edgeExpr;
		BecomeParent(this.edgeExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(edgeSetExpr);
		children.Add(edgeExpr);
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
		childrenNames.Add("edgeSetExpr");
		childrenNames.Add("edgeExpr");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		TypeNode edgeSetExprType = edgeSetExpr.Type;
		if(!(edgeSetExprType is SetTypeNode))
		{
			edgeSetExpr.ReportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		SetTypeNode type = (SetTypeNode)edgeSetExprType;
		if(!(type.valueType is EdgeTypeNode))
		{
			edgeSetExpr.ReportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		EdgeTypeNode edgeValueType = (EdgeTypeNode)type.valueType;
		if(edgeValueType != EdgeTypeNode.arbitraryEdgeType
				&& edgeValueType != EdgeTypeNode.directedEdgeType
				&& edgeValueType != EdgeTypeNode.undirectedEdgeType)
		{
			edgeSetExpr.ReportError("The insertDefinedSubgraph procedure expects as 1. argument (setOfEdges)"
					+ " a value of type set<AEdge> or set<Edge> or set<UEdge>"
					+ " (but is given a value of type " + edgeSetExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode edgeExprType = edgeExpr.Type;
		if(!(edgeExprType is EdgeTypeNode))
		{
			edgeExpr.ReportError("The insertDefinedSubgraph procedure expects as 2. argument (edge)"
					+ " a value of type AEdge or Edge or UEdge"
					+ " (but is given a value of type " + edgeExprType.ToStringWithDeclarationCoords() + ").");
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
		edgeSetExpr = edgeSetExpr.Evaluate();
		edgeExpr = edgeExpr.Evaluate();
		InsertDefinedSubgraphProc insertDefined = new InsertDefinedSubgraphProc(
				edgeSetExpr.CheckIR(typeof(Expression)), edgeExpr.CheckIR(typeof(Expression)),
				edgeExpr.Type.IRType);
		return insertDefined;
	}

	public override IList<TypeNode> Type
	{
		get
		{
		if(returnTypes == null)
		{
			returnTypes = new List<TypeNode>();
			returnTypes.Add(edgeExpr.Type);
		}
		return returnTypes;
		}
	}
}

}
