/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type.container;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.Operator;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.MapType;

public class MapTypeNode extends ContainerTypeNode
{
	static {
		setClassName(MapTypeNode.class, "map type");
	}

	@Override
	public String getTypeName()
	{
		return "map<" + keyTypeUnresolved.toString() + "," + valueTypeUnresolved.toString() + ">";
	}
	
	public IdentNode keyTypeUnresolved;
	public TypeNode keyType;
	public IdentNode valueTypeUnresolved;
	public TypeNode valueType;

	private SetTypeNode exceptCompatibleSetTyp;

	// the map type node instances are created in ParserEnvironment as needed
	public MapTypeNode(IdentNode keyTypeIdent, IdentNode valueTypeIdent)
	{
		keyTypeUnresolved = becomeParent(keyTypeIdent);
		valueTypeUnresolved = becomeParent(valueTypeIdent);
		exceptCompatibleSetTyp = new SetTypeNode(keyTypeIdent);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		// no children
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		// no children
		return childrenNames;
	}

	private static DeclarationTypeResolver<TypeNode> typeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(keyTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)keyTypeUnresolved);
		else
			fixupDefinition(keyTypeUnresolved, keyTypeUnresolved.getScope());
		if(valueTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)valueTypeUnresolved);
		else
			fixupDefinition(valueTypeUnresolved, valueTypeUnresolved.getScope());

		keyType = typeResolver.resolve(keyTypeUnresolved, this);
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		exceptCompatibleSetTyp.resolve();

		if(keyType == null || valueType == null)
			return false;

		OperatorDeclNode.makeBinOp(Operator.IN, BasicTypeNode.booleanType,
				keyType, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.INDEX, valueType,
				this, keyType, OperatorEvaluator.mapEvaluator);

		OperatorDeclNode.makeBinOp(Operator.EQ, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.NE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.SE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);

		OperatorDeclNode.makeBinOp(Operator.GT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.GE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.LT, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.LE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.mapEvaluator);

		OperatorDeclNode.makeBinOp(Operator.BIT_OR, this,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.BIT_AND, this,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.EXCEPT, this,
				this, this, OperatorEvaluator.mapEvaluator);
		OperatorDeclNode.makeBinOp(Operator.EXCEPT, this,
				this, exceptCompatibleSetTyp, OperatorEvaluator.mapEvaluator);

		TypeNode.addCompatibility(this, BasicTypeNode.stringType);

		return true;
	}

	@Override
	public TypeNode getElementType()
	{
		return keyType;
	}

	@Override
	protected IR constructIR()
	{
		Type kt = keyType.getIRType();
		Type vt = valueType.getIRType();
		return new MapType(kt, vt);
	}
}
