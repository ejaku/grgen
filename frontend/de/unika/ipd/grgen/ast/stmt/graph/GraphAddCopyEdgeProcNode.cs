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
using GraphAddCopyEdgeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyEdgeProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node for adding a copy of an edge to graph.
/// </summary>
public class GraphAddCopyEdgeProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphAddCopyEdgeProcNode()
	{
		SetClassName(typeof(GraphAddCopyEdgeProcNode), "graph add copy edge procedure");
	}

	private ExprNode oldEdge;
	private ExprNode sourceNode;
	private ExprNode targetNode;

	internal IList<TypeNode> returnTypes;

	private bool deep;

	public GraphAddCopyEdgeProcNode(Coords coords, ExprNode edgeType, ExprNode sourceNode, ExprNode targetNode, bool deep)
		: base(coords)
	{
		this.oldEdge = edgeType;
		BecomeParent(this.oldEdge);
		this.sourceNode = sourceNode;
		BecomeParent(this.sourceNode);
		this.targetNode = targetNode;
		BecomeParent(this.targetNode);
		this.deep = deep;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(oldEdge);
		children.Add(sourceNode);
		children.Add(targetNode);
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
		childrenNames.Add("old edge");
		childrenNames.Add("source node");
		childrenNames.Add("target node");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		TypeNode oldEdgeType = oldEdge.Type;
		if(!(oldEdgeType is EdgeTypeNode))
		{
			ReportError("The addCopy procedure expects as 1. argument (oldEdge)"
					+ " a value of type Edge"
					+ " (but is given a value of type " + oldEdgeType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode sourceNodeType = sourceNode.Type;
		if(!(sourceNodeType is NodeTypeNode))
		{
			ReportError("The addCopy procedure expects as 2. argument (sourceNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + sourceNodeType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode targetNodeType = targetNode.Type;
		if(!(targetNodeType is NodeTypeNode))
		{
			ReportError("The addCopy procedure expects as 3. argument (targetNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + targetNodeType.ToStringWithDeclarationCoords() + ").");
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
		oldEdge = oldEdge.Evaluate();
		sourceNode = sourceNode.Evaluate();
		targetNode = targetNode.Evaluate();
		GraphAddCopyEdgeProc addCopyEdge = new GraphAddCopyEdgeProc(oldEdge.CheckIR(typeof(Expression)),
				sourceNode.CheckIR(typeof(Expression)), targetNode.CheckIR(typeof(Expression)),
				oldEdge.Type.IRType, deep);
		return addCopyEdge;
	}

	public override IList<TypeNode> Type
	{
		get
		{
		if(returnTypes == null)
		{
			returnTypes = new List<TypeNode>();
			returnTypes.Add(oldEdge.Type);
		}
		return returnTypes;
		}
	}
}

}
