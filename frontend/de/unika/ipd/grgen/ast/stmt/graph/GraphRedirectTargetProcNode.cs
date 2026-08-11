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
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphRedirectTargetProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectTargetProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class GraphRedirectTargetProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphRedirectTargetProcNode()
	{
		SetClassName(typeof(GraphRedirectTargetProcNode), "graph redirect target procedure");
	}

	private ExprNode edgeExpr;
	private ExprNode newTargetExpr;
	private ExprNode oldTargetNameExpr;

	public GraphRedirectTargetProcNode(Coords coords, ExprNode edgeExpr, ExprNode newTargetExpr,
			ExprNode oldTargetNameExpr)
		: base(coords)
	{

		this.edgeExpr = edgeExpr;
		BecomeParent(edgeExpr);
		this.newTargetExpr = newTargetExpr;
		BecomeParent(newTargetExpr);
		this.oldTargetNameExpr = oldTargetNameExpr;
		if(oldTargetNameExpr != null)
			BecomeParent(oldTargetNameExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(edgeExpr);
			children.Add(newTargetExpr);
			if(oldTargetNameExpr != null)
				children.Add(oldTargetNameExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("edge");
			childrenNames.Add("newTarget");
			if(oldTargetNameExpr != null)
				childrenNames.Add("oldTargetName");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode edgeExprType = edgeExpr.Type;
		if(!(edgeExprType is EdgeTypeNode))
		{
			ReportError("The redirectTarget procedure expects as 1. argument (edgeToBeRedirected)"
					+ " a value of type Edge"
					+ " (but is given a value of type " + edgeExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode newTargetExprType = newTargetExpr.Type;
		if(!(newTargetExprType is NodeTypeNode))
		{
			ReportError("The redirectTarget procedure expects as 2. argument (newTargetNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + newTargetExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		if(oldTargetNameExpr != null)
		{
			TypeNode oldTargetNameExprType = oldTargetNameExpr.Type;
			if(!(oldTargetNameExprType.Equals(BasicTypeNode.stringType)))
			{
				ReportError("The redirectTarget procedure expects as 3. argument (oldTargetName)"
						+ " a value of type string"
						+ " (but is given a value of type " + oldTargetNameExprType.ToStringWithDeclarationCoords() + ").");
				return false;
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
		edgeExpr = edgeExpr.Evaluate();
		newTargetExpr = newTargetExpr.Evaluate();
		if(oldTargetNameExpr != null)
			oldTargetNameExpr = oldTargetNameExpr.Evaluate();
		return new GraphRedirectTargetProc(edgeExpr.CheckIR(typeof(Expression)),
				newTargetExpr.CheckIR(typeof(Expression)),
				oldTargetNameExpr != null ? oldTargetNameExpr.CheckIR(typeof(Expression)) : null);
	}
}

}
