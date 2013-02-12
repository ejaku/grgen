/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Nameof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the name of some node/edge or the graph.
 */
public class NameofNode extends ExprNode {
	static {
		setName(NameofNode.class, "nameof");
	}

	private IdentNode entityUnresolved; // null if name of graph is requested
	private EdgeDeclNode entityEdgeDecl = null;
	private NodeDeclNode entityNodeDecl = null;

	public NameofNode(Coords coords, IdentNode entity) {
		super(coords);
		this.entityUnresolved = entity;
		becomeParent(this.entityUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(entityUnresolved!=null) children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(entityUnresolved!=null) childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationPairResolver<EdgeDeclNode, NodeDeclNode> entityResolver =
		new DeclarationPairResolver<EdgeDeclNode, NodeDeclNode>(EdgeDeclNode.class, NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		if(entityUnresolved==null)
			return true;

		boolean res = fixupDefinition(entityUnresolved, entityUnresolved.getScope());

		Pair<EdgeDeclNode, NodeDeclNode> resolved = entityResolver.resolve(entityUnresolved, this);
		if (resolved != null) {
			entityEdgeDecl = resolved.fst;
			entityNodeDecl = resolved.snd;
		}

		return res && resolved != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(entityEdgeDecl==null && entityNodeDecl==null) {
			return new Nameof(null, getType().getType());
		}

		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl).checkIR(Entity.class);
		return new Nameof(entity, getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
	
	public boolean noDefElementInCondition() {
		if(entityEdgeDecl!=null) {
			if(entityEdgeDecl.defEntityToBeYieldedTo) {
				entityEdgeDecl.reportError("A def entity ("+entityEdgeDecl+") can't be accessed from an if");
				return false;
			}
		}
		if(entityNodeDecl!=null) {
			if(entityNodeDecl.defEntityToBeYieldedTo) {
				entityNodeDecl.reportError("A def variable ("+entityNodeDecl+") can't be accessed from an if");
				return false;
			}
		}
		return true;
	}
}
