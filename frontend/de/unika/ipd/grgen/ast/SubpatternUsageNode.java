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
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternUsageNode extends DeclNode {
	static {
		setName(SubpatternUsageNode.class, "subpattern node");
	}

	CollectNode<ConstraintDeclNode> connections;
	CollectNode<IdentNode> connectionsUnresolved;

	protected PatternTestDeclNode type = null;


	public SubpatternUsageNode(IdentNode n, BaseNode t, CollectNode<IdentNode> c) {
		super(n, t);
		this.connectionsUnresolved = c;
		becomeParent(this.connectionsUnresolved);
	}

	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type.getDeclType();
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(connectionsUnresolved, connections));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("connections");
		return childrenNames;
	}

	private static final DeclarationResolver<PatternTestDeclNode> actionResolver = new DeclarationResolver<PatternTestDeclNode>(PatternTestDeclNode.class);
	private static final CollectPairResolver<ConstraintDeclNode> connectionsResolver =
		new CollectPairResolver<ConstraintDeclNode>(new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type        = actionResolver.resolve(typeUnresolved, this);
		connections = connectionsResolver.resolve(connectionsUnresolved, this);
		return type != null && connections != null;
	}

	@Override
	protected boolean checkLocal() {
		// check if the number of parameters are correct
		int expected = type.pattern.params.getChildren().size();
		int actual = connections.getChildren().size();

		boolean res = (expected == actual);

		if (!res) {
			String patternName = type.ident.toString();

			error.error(getCoords(), "The pattern \"" + patternName + "\" need "
			        + expected + " parameters");
		}

		return res;
	}

	@Override
	protected IR constructIR() {
		List<GraphEntity> subpatternConnections = new LinkedList<GraphEntity>();
		for (ConstraintDeclNode c : connections.getChildren()) {
			subpatternConnections.add((GraphEntity) c.checkIR(GraphEntity.class));
		}
		return new SubpatternUsage("subpattern", getIdentNode().getIdent(), (MatchingAction)type.getIR(), subpatternConnections);
	}
}

