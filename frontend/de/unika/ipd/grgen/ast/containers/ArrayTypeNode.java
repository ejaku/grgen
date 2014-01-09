/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.ArrayType;
import de.unika.ipd.grgen.ir.Type;

public class ArrayTypeNode extends DeclaredTypeNode {
	static {
		setName(ArrayTypeNode.class, "array type");
	}

	@Override
	public String getName() {
		return "array<" + valueTypeUnresolved.toString() + "> type";
	}

	private static HashMap<String, ArrayTypeNode> arrayTypes = new HashMap<String, ArrayTypeNode>();

	public static ArrayTypeNode getArrayType(IdentNode valueTypeIdent) {
		String keyStr = valueTypeIdent.toString();
		ArrayTypeNode arrayTypeNode = arrayTypes.get(keyStr);

		if(arrayTypeNode == null)
			arrayTypes.put(keyStr, arrayTypeNode = new ArrayTypeNode(valueTypeIdent));

		return arrayTypeNode;
	}

	private IdentNode valueTypeUnresolved;
	public TypeNode valueType;

	// the array type node instances are created in ParserEnvironment as needed
	public ArrayTypeNode(IdentNode valueTypeIdent) {
		valueTypeUnresolved = becomeParent(valueTypeIdent);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	private static final DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal() {
		if(valueTypeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)valueTypeUnresolved);
		else if(valueTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)valueTypeUnresolved, valueTypeUnresolved.getScope());
		valueType = typeResolver.resolve(valueTypeUnresolved, this);
		if(valueType instanceof TestTypeNode || valueType instanceof RuleTypeNode) {
			valueType = MatchTypeNode.getMatchType(valueTypeUnresolved);
			if(valueType != null) valueType.resolve();
		}

		if(valueType == null) return false;

		if(valueType instanceof InheritanceTypeNode) {
			OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
					BasicTypeNode.typeType, this, OperatorSignature.arrayEvaluator);
		} else {
			OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
					valueType, this, OperatorSignature.arrayEvaluator);
		}
		OperatorSignature.makeBinOp(OperatorSignature.EQ, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.NE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.arrayEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.ADD, this, this, this,
				OperatorSignature.arrayEvaluator);

		TypeNode.addCompatibility(this, BasicTypeNode.stringType);

		return true;
	}

	@Override
	protected IR constructIR() {
		Type vt = valueType.getType();

		// return if the keyType or valueType construction already constructed the IR object
		if (isIRAlreadySet()) {
			return (ArrayType)getIR();
		}

		return new ArrayType(vt);
	}
}
