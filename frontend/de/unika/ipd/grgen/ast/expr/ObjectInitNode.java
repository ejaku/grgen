/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.model.type.BaseInternalObjectTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.AttributeInitialization;
import de.unika.ipd.grgen.ir.expr.InternalObjectInit;
import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
import de.unika.ipd.grgen.parser.Coords;

public class ObjectInitNode extends ExprNode
{
	static {
		setName(ObjectInitNode.class, "internal (transient) object init");
	}

	private IdentNode objectTypeUnresolved;
	private BaseInternalObjectTypeNode objectType;
	
	CollectNode<AttributeInitializationNode> attributeInits =
			new CollectNode<AttributeInitializationNode>();

	public ObjectInitNode(Coords coords, IdentNode objectType)
	{
		super(coords);
		this.objectTypeUnresolved = objectType;
	}

	public void addAttributeInitialization(AttributeInitializationNode attributeInit)
	{
		this.attributeInits.addChild(attributeInit);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(attributeInits);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("attributeInits");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<BaseInternalObjectTypeNode> objectTypeResolver =
			new DeclarationTypeResolver<BaseInternalObjectTypeNode>(BaseInternalObjectTypeNode.class);

	@Override
	protected boolean resolveLocal()
	{
		objectType = objectTypeResolver.resolve(objectTypeUnresolved, this);
		return objectType != null && objectType.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return getObjectType();
	}

	public BaseInternalObjectTypeNode getObjectType()
	{
		assert(isResolved());
		return objectType;
	}

	@Override
	protected IR constructIR()
	{
		BaseInternalObjectType type = objectType.checkIR(BaseInternalObjectType.class);
		
		InternalObjectInit init = new InternalObjectInit(type);
		
		for(AttributeInitializationNode ain : attributeInits.getChildren()) {
			ain.objectInitIR = init;
			init.addAttributeInitialization(ain.checkIR(AttributeInitialization.class));
		}

		return init;
	}

	public InternalObjectInit getObjectInit()
	{
		return checkIR(InternalObjectInit.class);
	}

	public static String getKindStr()
	{
		return "internal (transient) object initialization";
	}
}
