/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Represents a reused single graph entity.
 *
 * This node is needed to distinguish between reused single nodes and reused
 * subpatterns.
 * After resolving in {@link GraphNode#resolveLocal()} this node should disappear.
 *
 * @author buchwald
 *
 */
public class SingleGraphEntityNode extends BaseNode
{
	private IdentNode entityUnresolved;
	private NodeDeclNode entityNode;
	private SubpatternUsageNode entitySubpattern;

	public SingleGraphEntityNode(IdentNode ent) {
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

	private static final DeclarationPairResolver<NodeDeclNode, SubpatternUsageNode> entityResolver =
		new DeclarationPairResolver<NodeDeclNode, SubpatternUsageNode>(NodeDeclNode.class, SubpatternUsageNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!fixupDefinition(entityUnresolved, entityUnresolved.getScope()))
			return false;
		
		Pair<NodeDeclNode, SubpatternUsageNode> pair = entityResolver.resolve(entityUnresolved, this);

		if (pair != null) {
			entityNode = pair.fst;
			entitySubpattern = pair.snd;
		}

		return entityNode != null || entitySubpattern != null;
	}
	
	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 */
	public static boolean fixupDefinition(BaseNode elem, Scope scope) {
		if(!(elem instanceof IdentNode)) {
			return true;
		}
		IdentNode id = (IdentNode)elem;
		
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			id.reportError("Identifier \"" + id + "\" not declared in this scope: " + scope);
		}

		return res;
	}

	protected SubpatternUsageNode getEntitySubpattern()
    {
	    assert isResolved();

		return entitySubpattern;
    }

	protected NodeDeclNode getEntityNode() {
		assert isResolved();

		return entityNode;
	}

	public static String getKindStr() {
		return "single graph entity";
	}

	public static String getUseStr() {
		return "SingleGraphEntityNode";
	}
}
