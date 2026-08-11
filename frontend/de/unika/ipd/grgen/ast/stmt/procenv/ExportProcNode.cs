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
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ExportProc = de.unika.ipd.grgen.ir.stmt.procenv.ExportProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ExportProcNode : BuiltinProcedureInvocationBaseNode
{
	static ExportProcNode()
	{
		SetClassName(typeof(ExportProcNode), "export procedure");
	}

	private ExprNode pathExpr;
	private ExprNode graphExpr; // maybe null, then the current graph is to be exported

	public ExportProcNode(Coords coords, ExprNode pathExpr, ExprNode graphExpr)
		: base(coords)
	{

		this.pathExpr = BecomeParent(pathExpr);
		this.graphExpr = BecomeParent(graphExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(pathExpr);
			if(graphExpr != null)
				children.Add(graphExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("path");
			if(graphExpr != null)
				childrenNames.Add("graph");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		TypeNode pathExprType = pathExpr.Type;
		if(graphExpr != null)
		{
			TypeNode graphExprType = graphExpr.Type;
			if(!(graphExprType.Equals(BasicTypeNode.graphType)))
			{
				ReportError("The File::export procedure expects as 1. argument (subgraphToExport)"
						+ " a value of type graph"
						+ " (but is given a value of type " + graphExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			if(!(pathExprType.Equals(BasicTypeNode.stringType)))
			{
				ReportError("The File::export procedure expects as 2. argument (filePath)"
						+ " a value of type string"
						+ " (but is given a value of type " + pathExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		else
		{
			if(!(pathExprType.Equals(BasicTypeNode.stringType)))
			{
				ReportError("The File::export procedure expects as argument (filePath)"
						+ " a value of type string"
						+ " (but is given a value of type " + pathExprType.ToStringWithDeclarationCoords() + ").");
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
		pathExpr = pathExpr.Evaluate();
		if(graphExpr != null)
			graphExpr = graphExpr.Evaluate();
		return new ExportProc(pathExpr.CheckIR(typeof(Expression)),
				graphExpr != null ? graphExpr.CheckIR(typeof(Expression)) : null);
	}
}

}
