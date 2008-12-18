/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id: NameofNode.java 19421 2008-04-28 17:07:35Z eja $
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

	IdentNode entityUnresolved; // null if name of graph is requested
	EdgeDeclNode entityEdgeDecl = null;
	NodeDeclNode entityNodeDecl = null;

	public NameofNode(Coords coords, IdentNode entity) {
		super(coords);
		this.entityUnresolved = entity;
		becomeParent(this.entityUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(entityUnresolved!=null) children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(entityUnresolved!=null) childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationPairResolver<EdgeDeclNode, NodeDeclNode> entityResolver =
		new DeclarationPairResolver<EdgeDeclNode, NodeDeclNode>(EdgeDeclNode.class, NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		if(entityUnresolved==null) return true;
		
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
	protected boolean checkLocal() {
		return true;
	}

	protected IR constructIR() {
		if(entityEdgeDecl==null && entityNodeDecl==null) {
			return new Nameof(null, getType().getType());
		}
		
		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl).checkIR(Entity.class);
		return new Nameof(entity, getType().getType());
	}

	public DeclNode getEntity() {
		assert isResolved();

		if(entityUnresolved==null) {
			return null;
		}
		
		return getValidResolvedVersion(entityEdgeDecl, entityNodeDecl);
	}
	
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
}
