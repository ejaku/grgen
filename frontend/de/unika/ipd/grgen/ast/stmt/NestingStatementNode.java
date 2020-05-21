/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an eval statement that contains nested statements; it opens a block.
 * (For non-top-level statements (eval part, function, procedure).)
 */
public abstract class NestingStatementNode extends EvalStatementNode
{
	static {
		setName(NestingStatementNode.class, "NestingStatement");
	}

	protected CollectNode<EvalStatementNode> statements;

	protected NestingStatementNode(Coords coords, CollectNode<EvalStatementNode> statements)
	{
		super(coords);
		this.statements = statements;
		becomeParent(this.statements);
	}

	/*public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}*/
}
