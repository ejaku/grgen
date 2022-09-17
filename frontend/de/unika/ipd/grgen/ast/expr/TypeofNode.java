/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Typeof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node representing the current type of a
 * certain node/edge.
 */
public class TypeofNode extends ExprNode
{
	static {
		setName(TypeofNode.class, "typeof");
	}

	private IdentNode entityUnresolved;
	private EdgeDeclNode entityEdgeDecl = null;
	private NodeDeclNode entityNodeDecl = null;
	private VarDeclNode entityVarDecl = null;

	public TypeofNode(Coords coords, IdentNode entity)
	{
		super(coords);
		this.entityUnresolved = entity;
		becomeParent(this.entityUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entityUnresolved, entityEdgeDecl, entityNodeDecl, entityVarDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	private static final DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode> entityResolver =
			new DeclarationTripleResolver<EdgeDeclNode, NodeDeclNode, VarDeclNode>(EdgeDeclNode.class, NodeDeclNode.class, VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean res = fixupDefinition(entityUnresolved, entityUnresolved.getScope());

		Triple<EdgeDeclNode, NodeDeclNode, VarDeclNode> resolved = entityResolver.resolve(entityUnresolved, this);
		if(resolved != null) {
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
	protected boolean checkLocal()
	{
		if(entityVarDecl != null
				&& !(entityVarDecl.getDeclType() instanceof NodeTypeNode)
				&& !(entityVarDecl.getDeclType() instanceof EdgeTypeNode)) {
			reportError("The variable in a typeof (" + entityUnresolved + ") must be of node or edge type, but is of type " + entityVarDecl.getDeclType()
					+ " (which is a " + entityVarDecl.getDeclType().getKind() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		Entity entity = getValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl).checkIR(Entity.class);

		return new Typeof(entity);
	}

	public DeclNode getEntity()
	{
		assert isResolved();

		return getValidResolvedVersion(entityEdgeDecl, entityNodeDecl, entityVarDecl);
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.typeType;
	}

	@Override
	public boolean noDefElement(String containingConstruct)
	{
		if(entityEdgeDecl != null) {
			if(entityEdgeDecl.defEntityToBeYieldedTo) {
				entityEdgeDecl.reportError("A def edge (" + entityUnresolved + ")"
						+ " cannot be accessed from a " + containingConstruct + ".");
				return false;
			}
		}
		if(entityNodeDecl != null) {
			if(entityNodeDecl.defEntityToBeYieldedTo) {
				entityNodeDecl.reportError("A def node (" + entityUnresolved + ")"
						+ " cannot be accessed from a " + containingConstruct + ".");
				return false;
			}
		}
		if(entityVarDecl != null) {
			if(entityVarDecl.defEntityToBeYieldedTo && !entityVarDecl.lambdaExpressionVariable) {
				entityVarDecl.reportError("A def variable (" + entityUnresolved + ")"
						+ " cannot be accessed from a " + containingConstruct + ".");
				return false;
			}
		}
		return true;
	}
}
