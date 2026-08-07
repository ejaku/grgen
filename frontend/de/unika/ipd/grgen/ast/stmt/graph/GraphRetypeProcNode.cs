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
using GraphRetypeEdgeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeEdgeProc;
using GraphRetypeNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeNodeProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node for retyping a node or an edge to a new type.
/// </summary>
public class GraphRetypeProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphRetypeProcNode()
	{
		SetClassName(typeof(GraphRetypeProcNode), "retype procedure");
	}

	private ExprNode entityExpr;
	private ExprNode entityTypeExpr;

	internal IList<TypeNode> returnTypes;

	public GraphRetypeProcNode(Coords coords, ExprNode entity, ExprNode entityType)
		: base(coords)
	{
		this.entityExpr = entity;
		BecomeParent(this.entityExpr);
		this.entityTypeExpr = entityType;
		BecomeParent(this.entityTypeExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(entityExpr);
		children.Add(entityTypeExpr);
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
		childrenNames.Add("entity");
		childrenNames.Add("new type");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		TypeNode entityExprType = entityExpr.Type;
		TypeNode entityTypeExprType = entityTypeExpr.Type;
		if(entityExprType is NodeTypeNode && entityTypeExprType is NodeTypeNode)
			return true;
		if(entityExprType is EdgeTypeNode && entityTypeExprType is EdgeTypeNode)
			return true;
		ReportError("The retype procedure expects as 1. argument (node) a value of type Node and as 2. argument (nodeType) a value of type node type,"
				+ " or as 1. argument (edge) a value of type Edge and as 2. argument (edgeType) a value of type edge type "
				+ " (but is given values of type " + entityExprType.ToStringWithDeclarationCoords()
				+ " and " + entityTypeExprType.ToStringWithDeclarationCoords() + ").");
		return false;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		entityExpr = entityExpr.Evaluate();
		entityTypeExpr = entityTypeExpr.Evaluate();
		if(entityTypeExpr.Type is NodeTypeNode)
		{
			GraphRetypeNodeProc retypeNode = new GraphRetypeNodeProc(entityExpr.CheckIR(typeof(Expression)),
					entityTypeExpr.CheckIR(typeof(Expression)), entityTypeExpr.Type.IRType);
			return retypeNode;
		}
		else
		{
			GraphRetypeEdgeProc retypeEdge = new GraphRetypeEdgeProc(entityExpr.CheckIR(typeof(Expression)),
					entityTypeExpr.CheckIR(typeof(Expression)), entityTypeExpr.Type.IRType);
			return retypeEdge;
		}
	}

	public override IList<TypeNode> Type
	{
		get
		{
		if(returnTypes == null)
		{
			returnTypes = new List<TypeNode>();
			returnTypes.Add(entityTypeExpr.Type);
		}
		return returnTypes;
		}
	}
}

}
