/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.decl;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PatternGraphNode;
import de.unika.ipd.grgen.ast.TypeExprNode;

public abstract class MatchNodeByIndexNode extends NodeDeclNode
{
	static {
		setName(MatchNodeByIndexNode.class, "match node by index");
	}

	protected IdentNode indexUnresolved;
	protected IndexDeclNode index;

	protected MatchNodeByIndexNode(IdentNode id, BaseNode type, int context,
			IdentNode index, PatternGraphNode directlyNestingLHSGraph)
	{
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}
}
