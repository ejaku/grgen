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
using de.unika.ipd.grgen.ast;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing an eval statement that contains nested statements; it opens a block.
/// (For non-top-level statements (eval part, function, procedure).)
/// </summary>
public abstract class NestingStatementNode : EvalStatementNode
{
	static NestingStatementNode()
	{
		SetClassName(typeof(NestingStatementNode), "NestingStatement");
	}

	protected internal CollectNode<EvalStatementNode> statements;

	protected internal NestingStatementNode(Coords coords, CollectNode<EvalStatementNode> statements)
		: base(coords)
	{
		this.statements = statements;
		BecomeParent(this.statements);
	}

	/*public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}*/
}

}
