/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using Exec = de.unika.ipd.grgen.ir.Exec;
using IR = de.unika.ipd.grgen.ir.IR;
using ExecStatement = de.unika.ipd.grgen.ir.stmt.ExecStatement;

/// <summary>
/// AST node representing an embedded exec statement.
/// </summary>
public class ExecStatementNode : EvalStatementNode
{
	static ExecStatementNode()
	{
		SetClassName(typeof(ExecStatementNode), "ExecStatement");
	}

	internal ExecNode exec;

	public int context;

	public ExecStatementNode(ExecNode exec, int context)
		: base(exec.Coords)
	{
		this.exec = exec;
		BecomeParent(this.exec);
		this.context = context;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(exec);
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
			childrenNames.Add("exec");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		if((context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION)
		{
			if((context & BaseNode.CONTEXT_METHOD) == BaseNode.CONTEXT_METHOD)
			{
				ReportError("An exec is not allowed in a method.");
				return false;
			}
			else if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				ReportError("An exec is not allowed in a function.");
				return false;
			}
		}
		return true;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	public override bool NoExecStatement(bool inEvalHereContext)
	{
		if(inEvalHereContext)
		{
			ReportError("An exec inside an evalhere is forbidden"
					+ " (you may move it outside the evalhere, but note that it is then executed at the end of rewriting).");
		}
		else
		{
			ReportError("An exec inside an eval is forbidden in an alternative or iterated -- move it outside of the eval"
					+ " (so it becomes a deferred exec, executed at the end of rewriting, on the by-then current graph and the local entities valid at the end of its local rewriting).");
		}
		return false;
	}

	protected internal override IR ConstructIR()
	{
		ExecStatement ws = new ExecStatement(exec.CheckIR(typeof(Exec)));
		return ws;
	}
}

}
