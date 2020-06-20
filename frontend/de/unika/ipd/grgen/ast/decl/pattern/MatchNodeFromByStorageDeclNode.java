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
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

public abstract class MatchNodeFromByStorageDeclNode extends NodeDeclNode
{
	static {
		setName(MatchNodeFromByStorageDeclNode.class, "match node from by storage decl");
	}

	protected BaseNode storageUnresolved;
	protected VarDeclNode storage = null;
	protected QualIdentNode storageAttribute = null;
	protected NodeDeclNode storageGlobalVariable = null;

	protected MatchNodeFromByStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
			PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.storageUnresolved = storage;
		becomeParent(this.storageUnresolved);
	}
	
	protected TypeNode getStorageType()
	{
		if(storage != null)
			return storage.getDeclType();
		else if(storageGlobalVariable != null)
			return storageGlobalVariable.getDeclType();
		else
			return storageAttribute.getDecl().getDeclType();
	}
}
