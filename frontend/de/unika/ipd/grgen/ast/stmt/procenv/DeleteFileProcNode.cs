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
using DeleteFileProc = de.unika.ipd.grgen.ir.stmt.procenv.DeleteFileProc;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class DeleteFileProcNode : BuiltinProcedureInvocationBaseNode
{
	static DeleteFileProcNode()
	{
		SetClassName(typeof(DeleteFileProcNode), "deleteFile procedure");
	}

	private ExprNode pathExpr;

	public DeleteFileProcNode(Coords coords, ExprNode pathExpr)
		: base(coords)
	{

		this.pathExpr = BecomeParent(pathExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(pathExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("path");
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
		if(!(pathExprType.Equals(BasicTypeNode.stringType)))
		{
			ReportError("The File::delete procedure expects as argument (file path)"
					+ " a value of type string"
					+ " (but is given a value of type " + pathExprType.ToStringWithDeclarationCoords() + ").");
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
		pathExpr = pathExpr.Evaluate();
		return new DeleteFileProc(pathExpr.CheckIR(typeof(Expression)));
	}
}

}
