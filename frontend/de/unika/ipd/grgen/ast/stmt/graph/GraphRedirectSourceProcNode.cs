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
using GraphRedirectSourceProc = de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class GraphRedirectSourceProcNode : BuiltinProcedureInvocationBaseNode
{
	static GraphRedirectSourceProcNode()
	{
		SetClassName(typeof(GraphRedirectSourceProcNode), "graph redirect source procedure");
	}

	private ExprNode edgeExpr;
	private ExprNode newSourceExpr;
	private ExprNode oldSourceNameExpr;

	public GraphRedirectSourceProcNode(Coords coords, ExprNode edgeExpr, ExprNode newSourceExpr,
			ExprNode oldSourceNameExpr)
		: base(coords)
	{

		this.edgeExpr = edgeExpr;
		BecomeParent(edgeExpr);
		this.newSourceExpr = newSourceExpr;
		BecomeParent(newSourceExpr);
		this.oldSourceNameExpr = oldSourceNameExpr;
		if(oldSourceNameExpr != null)
			BecomeParent(oldSourceNameExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(edgeExpr);
		children.Add(newSourceExpr);
		if(oldSourceNameExpr != null)
			children.Add(oldSourceNameExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("edge");
		childrenNames.Add("newSource");
		if(oldSourceNameExpr != null)
			childrenNames.Add("oldSourceName");
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
			ReportError("The redirectSource procedure expects as 1. argument (edgeToBeRedirected)"
					+ " a value of type Edge"
					+ " (but is given a value of type " + edgeExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode newSourceExprType = newSourceExpr.Type;
		if(!(newSourceExprType is NodeTypeNode))
		{
			ReportError("The redirectSource procedure expects as 2. argument (newSourceNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + newSourceExprType.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		if(oldSourceNameExpr != null)
		{
			TypeNode oldSourceNameExprType = oldSourceNameExpr.Type;
			if(!(oldSourceNameExprType.Equals(BasicTypeNode.stringType)))
			{
				ReportError("The redirectSource procedure expects as 3. argument (oldSourceName)"
						+ " a value of type string"
						+ " (but is given a value of type " + oldSourceNameExprType.ToStringWithDeclarationCoords() + ").");
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
		newSourceExpr = newSourceExpr.Evaluate();
		if(oldSourceNameExpr != null)
			oldSourceNameExpr = oldSourceNameExpr.Evaluate();
		return new GraphRedirectSourceProc(edgeExpr.CheckIR(typeof(Expression)),
				newSourceExpr.CheckIR(typeof(Expression)),
				oldSourceNameExpr != null ? oldSourceNameExpr.CheckIR(typeof(Expression)) : null);
	}
}

}
