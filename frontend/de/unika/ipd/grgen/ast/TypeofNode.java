/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node representing the current type of a
 * certain node/edge.
 */
public class TypeofNode extends ExprNode {
	static {
		setName(TypeofNode.class, "typeof");
	}

	private IdentNode entityUnresolved;
	private EdgeDeclNode entityEdgeDecl = null;
	private NodeDeclNode entityNodeDecl = null;

	public TypeofNode(Coords coords, IdentNode entity) {
		super(coords);
		this.entityUnresolved= entity;
		becomeParent(this.entityUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationPairResolver<EdgeDeclNode, NodeDeclNode> entityResolver =
		new DeclarationPairResolver<EdgeDeclNode, NodeDeclNode>(EdgeDeclNode.class, NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		Pair<EdgeDeclNode, NodeDeclNode> resolved = entityResolver.resolve(entityUnresolved, this);
		if (resolved != null) {
			entityEdgeDecl = resolved.fst;
			entityNodeDecl = resolved.snd;
		}

		return (resolved != null);
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
		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl).checkIR(Entity.class);

		return new Typeof(entity);
	}

	protected DeclNode getEntity() {
		assert isResolved();

		return getValidResolvedVersion(entityEdgeDecl, entityNodeDecl);
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.typeType;
	}
}
