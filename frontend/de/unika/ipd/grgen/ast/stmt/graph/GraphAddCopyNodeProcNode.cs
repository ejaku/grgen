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
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphAddCopyNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyNodeProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node for adding a copy of a node to graph.
/// </summary>
public class GraphAddCopyNodeProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphAddCopyNodeProcNode()
	{
		SetClassName(typeof(GraphAddCopyNodeProcNode), "graph add copy node procedure");
	}

	private ExprNode oldNode;

	internal IList<TypeNode> returnTypes;

	private bool deep;

	public GraphAddCopyNodeProcNode(Coords coords, ExprNode nodeType, bool deep)
		: base(coords)
	{
		this.oldNode = nodeType;
		BecomeParent(this.oldNode);
		this.deep = deep;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(oldNode);
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
			childrenNames.Add("old node");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		TypeNode oldNodeType = oldNode.Type;
		if(!(oldNodeType is NodeTypeNode))
		{
			ReportError("The addCopy procedure expects as argument (oldNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + oldNodeType.ToStringWithDeclarationCoords() + ").");
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
		oldNode = oldNode.Evaluate();
		GraphAddCopyNodeProc addCopyNode = new GraphAddCopyNodeProc(oldNode.CheckIR(typeof(Expression)),
				oldNode.Type.IRType, deep);
		return addCopyNode;
	}

	public override IList<TypeNode> Type
	{
		get
		{
			if(returnTypes == null)
			{
				returnTypes = new List<TypeNode>();
				returnTypes.Add(oldNode.Type);
			}
			return returnTypes;
		}
	}
}

}
