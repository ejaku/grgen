/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ForIndexAccessNode extends ForGraphQueryNode
{
	static {
		setName(ForIndexAccessNode.class, "for index access loop");
	}

	protected IdentNode indexUnresolved;
	protected IndexDeclNode index;

	public ForIndexAccessNode(Coords coords, BaseNode iterationVariable, int context,
			IdentNode index, PatternGraphNode directlyNestingLHSGraph,
			CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, iterationVariable, loopedStatements);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}
}
