/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.BaseInternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.AttributeInitialization;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.InternalObjectInit;
import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;

public class AttributeInitializationNode extends BaseNode
{
	public ObjectInitNode objectInit;
	public InternalObjectInit objectInitIR;
	
	public IdentNode ownerUnresolved;
	public BaseInternalObjectTypeNode owner;

	public IdentNode attributeUnresolved;
	public MemberDeclNode attribute;
	public ExprNode initialization;

	public AttributeInitializationNode(ObjectInitNode objectInit, IdentNode owner, IdentNode attribute, ExprNode initialization)
	{
		this.objectInit = objectInit;
		this.ownerUnresolved = owner;
		this.attributeUnresolved = attribute;
		this.initialization = initialization;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(attributeUnresolved, attribute));
		children.add(initialization);
		return children;
	}

	@Override
	protected Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("attribute");
		childrenNames.add("initialization");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<BaseInternalObjectTypeNode> objectTypeResolver =
			new DeclarationTypeResolver<BaseInternalObjectTypeNode>(BaseInternalObjectTypeNode.class);

	private static final DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		owner = objectTypeResolver.resolve(ownerUnresolved, this);
		if(owner == null || !owner.resolve())
			return false;

		owner.fixupDefinition(attributeUnresolved);
		attribute = memberResolver.resolve(attributeUnresolved, this);
		return attribute != null;
	}

	@Override
	protected boolean checkLocal()
	{
		if(attribute.isConst()) {
			objectInit.reportError("An assignment to a const member is not allowed"
					+ " (but occurs for " + attribute + ").");
			return false;
		}
		
		if(owner.isConst()) {
			objectInit.reportError("An assignment to an object of const type is not allowed"
					+ " (but occurs for " + attribute + " of " + owner.getIdentNode() + ").");
			return false;
		}

		TypeNode targetType = attribute.getDeclType();
		TypeNode exprType = initialization.getType();

		if(exprType.isEqual(targetType))
			return true;

		initialization = becomeParent(initialization.adjustType(targetType, objectInit.getCoords()));
		if(initialization == ConstNode.getInvalid())
			return false;

		if(targetType instanceof NodeTypeNode && exprType instanceof NodeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof EdgeTypeNode) {
			Collection<TypeNode> superTypes = new HashSet<TypeNode>();
			exprType.doGetCompatibleToTypes(superTypes);
			if(!superTypes.contains(targetType)) {
				objectInit.reportError("Cannot initialize-assign a value of " + exprType.toStringWithDeclarationCoords()
						+ " to an attribute of " + targetType.toStringWithDeclarationCoords() + " (this occurs for " + attribute + ").");
				return false;
			}
		}
		if(targetType instanceof NodeTypeNode && exprType instanceof EdgeTypeNode
				|| targetType instanceof EdgeTypeNode && exprType instanceof NodeTypeNode) {
			objectInit.reportError("Cannot initialize-assign a value of " + exprType.toStringWithDeclarationCoords()
					+ " to an attribute of " + targetType.toStringWithDeclarationCoords() + " (this occurs for " + attribute + ").");
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

		AttributeInitialization ai = new AttributeInitialization();

		// mark this node as already visited
		setIR(ai);

		assert(objectInitIR != null);
		ai.init = objectInitIR;
		ai.owner = owner.checkIR(BaseInternalObjectType.class);
		ai.attribute = attribute.checkIR(Entity.class);
		initialization = initialization.evaluate();
		ai.expr = initialization.checkIR(Expression.class);

		return ai;
	}
}
