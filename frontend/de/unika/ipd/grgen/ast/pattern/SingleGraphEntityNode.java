/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;

/**
 * Represents a reused single (pattern) graph entity.
 *
 * This node is needed to distinguish between reused single nodes and reused
 * subpatterns.
 * After resolving in {@link PatternGraphRhsNode#resolveLocal()} this node should disappear.
 *
 * @author buchwald
 *
 */
public class SingleGraphEntityNode extends BaseNode
{
	private IdentNode entityUnresolved;
	private NodeDeclNode entityNode;
	private SubpatternUsageDeclNode entitySubpattern;

	public SingleGraphEntityNode(IdentNode ent)
	{
		super(ent.getCoords());
		entityUnresolved = ent;
		becomeParent(this.entityUnresolved);
	}

	@Override
	protected boolean checkLocal()
	{
		// this node should not exist after resolving
		return false;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entityUnresolved);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationPairResolver<NodeDeclNode, SubpatternUsageDeclNode> entityResolver =
			new DeclarationPairResolver<NodeDeclNode, SubpatternUsageDeclNode>(NodeDeclNode.class, SubpatternUsageDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!fixupDefinition(entityUnresolved, entityUnresolved.getScope()))
			return false;

		Pair<NodeDeclNode, SubpatternUsageDeclNode> pair = entityResolver.resolve(entityUnresolved, this);

		if(pair != null) {
			entityNode = pair.fst;
			entitySubpattern = pair.snd;
		}

		return entityNode != null || entitySubpattern != null;
	}

	protected SubpatternUsageDeclNode getEntitySubpattern()
	{
		assert isResolved();

		return entitySubpattern;
	}

	protected NodeDeclNode getEntityNode()
	{
		assert isResolved();

		return entityNode;
	}

	public static String getKindStr()
	{
		return "single graph entity";
	}
}
