/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;

public abstract class MatchEdgeByIndexDeclNode extends EdgeDeclNode
{
	static {
		setName(MatchEdgeByIndexAccessEqualityDeclNode.class, "match edge by index");
	}

	protected IdentNode indexUnresolved;
	protected IndexDeclNode index;

	protected MatchEdgeByIndexDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, CopyKind.None, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}
	
	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		return successfullyResolved;
	}
	
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("Cannot employ match edge by index in the rewrite part"
					+ " (as it occurs in match edge" + emptyWhenAnonymousPostfix(" ") + " by index access of " + index.getIdentNode() + ").");
			res = false;
		}
		return res;
	}
}
