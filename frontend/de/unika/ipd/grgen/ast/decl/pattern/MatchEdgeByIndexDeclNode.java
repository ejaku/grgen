/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;

public abstract class MatchEdgeByIndexDeclNode extends EdgeDeclNode
{
	static {
		setName(MatchEdgeByIndexAccessEqualityDeclNode.class, "match edge by index");
	}

	protected IdentNode indexUnresolved;
	protected IndexDeclNode index;

	protected MatchEdgeByIndexDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, PatternGraphNode directlyNestingLHSGraph)
	{
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}
}
