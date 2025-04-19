/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;

public class NameOrAttributeInitializationNode extends BaseNode
{
	public ConstraintDeclNode owner;
	public GraphEntity ownerIR;

	public IdentNode attributeUnresolved;
	public MemberDeclNode attribute;
	public ExprNode initialization;

	public NameOrAttributeInitializationNode(ConstraintDeclNode owner, IdentNode attribute, ExprNode initialization)
	{
		this.owner = owner;
		this.attributeUnresolved = attribute;
		this.initialization = initialization;
	}

	public NameOrAttributeInitializationNode(ConstraintDeclNode owner, ExprNode initialization)
	{
		this.owner = owner;
		this.initialization = initialization;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(attributeUnresolved != null)
			children.add(getValidVersion(attributeUnresolved, attribute));
		children.add(initialization);
		return children;
	}

	@Override
	protected Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(attributeUnresolved != null)
			childrenNames.add("attribute");
		childrenNames.add("initialization");
		return childrenNames;
	}

	private static final DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(attributeUnresolved != null) {
			owner.getDeclType().fixupDefinition(attributeUnresolved);
			attribute = memberResolver.resolve(attributeUnresolved, this);
			return attribute != null;
		}
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		if(attributeUnresolved == null) {
			TypeNode targetType = StringTypeNode.stringType;
			TypeNode exprType = initialization.getType();

			if(exprType.isEqual(targetType))
				return true;

			initialization = becomeParent(initialization.adjustType(targetType, owner.getCoords()));
			if(initialization == ConstNode.getInvalid()) {
				owner.reportError("The name of an element must be initialized with a value of type string"
						+ " (but it is initialized with a value of type " + exprType.getTypeName() + ").");
				return false;
			}

			return true;
		}

		if(attribute.isConst()) {
			owner.reportError("An assignment to a const member is not allowed"
					+ " (but " + attribute.getIdentNode() + " is const).");
			return false;
		}

		if(owner.getDeclType().isConst()) {
			owner.reportError("An assignment to a const type object is not allowed"
					+ " (but " + owner.getDeclType().getTypeName() + " is const).");
			return false;
		}

		TypeNode targetType = attribute.getDeclType();
		TypeNode exprType = initialization.getType();

		if(exprType.isEqual(targetType))
			return true;

		initialization = becomeParent(initialization.adjustType(targetType, owner.getCoords()));
		if(initialization == ConstNode.getInvalid())
			return false;

		if(targetType instanceof NodeTypeNode && exprType instanceof NodeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof EdgeTypeNode) {
			Collection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.doGetCompatibleToTypes(superTypes);
			if(!superTypes.contains(targetType)) {
				owner.reportError("Cannot initialize an attribute of type " + targetType.toStringWithDeclarationCoords()
						+ " with a value of type " + exprType.toStringWithDeclarationCoords() + ".");
				return false;
			}
		}
		if(targetType instanceof NodeTypeNode && exprType instanceof EdgeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof NodeTypeNode) {
			owner.reportError("Cannot initialize an attribute of type " + targetType.toStringWithDeclarationCoords()
					+ " with a value of type " + exprType.toStringWithDeclarationCoords() + ".");
			return false;
		}
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if(isIRAlreadySet()) {
			return getIR();
		}

		NameOrAttributeInitialization nai = new NameOrAttributeInitialization();

		// mark this node as already visited
		setIR(nai);

		assert(ownerIR != null);
		nai.owner = ownerIR;
		if(attribute != null)
			nai.attribute = attribute.checkIR(Entity.class);
		initialization = initialization.evaluate();
		nai.expr = initialization.checkIR(Expression.class);

		return nai;
	}
}
