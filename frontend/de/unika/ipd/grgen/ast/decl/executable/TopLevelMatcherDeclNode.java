/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

/**
 * Base class for top level pattern matching related ast nodes
 */
public abstract class TopLevelMatcherDeclNode extends MatcherDeclNode
{
	public TopLevelMatcherDeclNode(IdentNode id, TypeNode type, PatternGraphLhsNode left)
	{
		super(id, type, left);
	}
}
