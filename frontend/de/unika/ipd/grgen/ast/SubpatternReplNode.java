/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;

public class SubpatternReplNode extends BaseNode {
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}

	IdentNode subpatternUnresolved;
	SubpatternUsageNode subpattern;
	CollectNode<IdentNode> replConnections;

	public SubpatternReplNode(IdentNode n, CollectNode<IdentNode> c) {
		this.subpatternUnresolved = n;
		becomeParent(this.subpatternUnresolved);
		this.replConnections = c;
		becomeParent(this.replConnections);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(subpatternUnresolved, subpattern));
		children.add(replConnections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpattern");
		childrenNames.add("replConnections");
		return childrenNames;
	}

	private static final DeclarationResolver<SubpatternUsageNode> subpatternResolver = new DeclarationResolver<SubpatternUsageNode>(SubpatternUsageNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		subpattern = subpatternResolver.resolve(subpatternUnresolved, this);

		return subpattern != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	// FIXME neue IR Klasse einbauen
	/*@Override
	protected IR constructIR() {
		List<GraphEntity> subpatternConnections = new LinkedList<GraphEntity>();
    	for (ConstraintDeclNode c : replConnections.getChildren()) {
    		subpatternConnections.add((GraphEntity) c.checkIR(GraphEntity.class));
    	}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(), (MatchingAction)type.getIR(), subpatternConnections);
	}*/
}
