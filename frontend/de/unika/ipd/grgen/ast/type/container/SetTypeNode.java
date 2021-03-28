/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.SetType;

public class SetTypeNode extends ContainerTypeNode
{
	static {
		setName(SetTypeNode.class, "set type");
	}

	@Override
	public String getTypeName()
	{
		return "set<" + valueTypeUnresolved.toString() + ">";
	}
	
	public IdentNode valueTypeUnresolved;
	public TypeNode valueType;

	// the set type node instances are created in ParserEnvironment as needed
	public SetTypeNode(IdentNode valueTypeIdent)
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
		else
			fixupDefinition(valueTypeUnresolved, valueTypeUnresolved.getScope());
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(valueType == null)
			return false;

		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.IN, BasicTypeNode.booleanType,
				valueType, this, OperatorEvaluator.setEvaluator);

		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.EQ, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.NE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.SE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);

		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.GT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.GE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.LT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.LE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.setEvaluator);

		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.BIT_OR, this,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.BIT_AND, this,
				this, this, OperatorEvaluator.setEvaluator);
		OperatorDeclNode.makeBinOp(OperatorDeclNode.Operator.EXCEPT, this,
				this, this, OperatorEvaluator.setEvaluator);

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
		return new SetType(vt);
	}
}
