/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Typeof;
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
	private VarDeclNode entityVarDecl = null;

	public TypeofNode(Coords coords, IdentNode entity) {
		super(coords);
		this.entityUnresolved= entity;
		becomeParent(this.entityUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl, entityVarDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode> entityResolver =
		new DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode>(EdgeDeclNode.class, NodeDeclNode.class, VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean res = fixupDefinition(entityUnresolved, entityUnresolved.getScope());
		
		Triple<EdgeDeclNode, NodeDeclNode, VarDeclNode> resolved = entityResolver.resolve(entityUnresolved, this);
		if (resolved != null) {
			entityEdgeDecl = resolved.first;
			entityNodeDecl = resolved.second;
			entityVarDecl = resolved.third;
		}

		return res && resolved != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		if(entityVarDecl != null
				&& !(entityVarDecl.getDeclType() instanceof NodeTypeNode)
				&& !(entityVarDecl.getDeclType() instanceof EdgeTypeNode))
		{
			reportError("the variable for a typeof must be of node or edge type.");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl).checkIR(Entity.class);

		return new Typeof(entity);
	}

	protected DeclNode getEntity() {
		assert isResolved();

		return getValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl);
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.typeType;
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
		if(entityVarDecl!=null) {
			if(entityVarDecl.defEntityToBeYieldedTo) {
				entityVarDecl.reportError("A def variable ("+entityVarDecl+") can't be accessed from an if");
				return false;
			}
		}
		return true;
	}
}
