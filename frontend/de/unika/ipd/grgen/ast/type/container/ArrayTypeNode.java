/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type.container;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayTypeNode extends ContainerTypeNode
{
	static {
		setName(ArrayTypeNode.class, "array type");
	}

	@Override
	public String getTypeName()
	{
		return "array<" + valueTypeUnresolved.toString() + ">";
	}
	
	public IdentNode valueTypeUnresolved;
	public TypeNode valueType;

	// the array type node instances are created in ParserEnvironment as needed
	public ArrayTypeNode(IdentNode valueTypeIdent)
	{
		valueTypeUnresolved = becomeParent(valueTypeIdent);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	private static final DeclarationTypeResolver<TypeNode> typeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(valueTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)valueTypeUnresolved);
		else if(valueTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)valueTypeUnresolved, valueTypeUnresolved.getScope());
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(valueType == null)
			return false;

		if(valueType instanceof InheritanceTypeNode) {
			OperatorDeclNode.makeBinOp(OperatorDeclNode.IN, BasicTypeNode.booleanType,
					BasicTypeNode.typeType, this, OperatorEvaluator.arrayEvaluator);
		} else {
			OperatorDeclNode.makeBinOp(OperatorDeclNode.IN, BasicTypeNode.booleanType,
					valueType, this, OperatorEvaluator.arrayEvaluator);
		}
		OperatorDeclNode.makeBinOp(OperatorDeclNode.EQ, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.NE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.GT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.GE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.LT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.LE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.arrayEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.ADD, this, this, this,
				OperatorEvaluator.arrayEvaluator);

		TypeNode.addCompatibility(this, BasicTypeNode.stringType);

		return true;
	}

	@Override
	public TypeNode getElementType()
	{
		return valueType;
	}

	@Override
	protected IR constructIR()
	{
		Type vt = valueType.getType();
		return new ArrayType(vt);
	}
}
