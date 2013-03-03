/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.GraphRemove;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRemoveNode extends EvalStatementNode {
	static {
		setName(GraphRemoveNode.class, "graph remove statement");
	}

	private ExprNode entityUnresolved;
	private NodeDeclNode entityNodeDecl;
	private EdgeDeclNode entityEdgeDecl;

	public GraphRemoveNode(Coords coords, ExprNode entity) {
		super(coords);

		entityUnresolved = entity;
		becomeParent(entityUnresolved);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl));
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		fixupDefinition(entityUnresolved, entityUnresolved.getScope());

		if(entityUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)entityUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof NodeDeclNode) {
					entityNodeDecl = (NodeDeclNode)unresolved.decl;
					return true;
				} else if(unresolved.decl instanceof EdgeDeclNode) {
					entityEdgeDecl = (EdgeDeclNode)unresolved.decl;
					return true;
				}
			}
		}

		entityUnresolved.reportError("error in resolving graph remove node, only node or edge allowed as argument.");
		return false;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl).checkIR(Entity.class);

		return new GraphRemove(entity);
	}
}
