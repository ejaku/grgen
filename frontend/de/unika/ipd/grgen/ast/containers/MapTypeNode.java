/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.containers.MapType;
import de.unika.ipd.grgen.ir.Type;

public class MapTypeNode extends DeclaredTypeNode {
	static {
		setName(MapTypeNode.class, "map type");
	}

	@Override
	public String getName() {
		return "map<" + keyTypeUnresolved.toString() + "," + valueTypeUnresolved.toString() + "> type";
	}

	private static HashMap<String, MapTypeNode> mapTypes = new HashMap<String, MapTypeNode>();

	public static MapTypeNode getMapType(IdentNode keyTypeIdent, IdentNode valueTypeIdent) {
		String keyStr = keyTypeIdent.toString() + "->" + valueTypeIdent.toString();
		MapTypeNode mapTypeNode = mapTypes.get(keyStr);

		if(mapTypeNode == null) {
			mapTypes.put(keyStr, mapTypeNode = new MapTypeNode(keyTypeIdent, valueTypeIdent));
			mapTypeNode.setExceptCompatibleSetType(SetTypeNode.getSetType(keyTypeIdent));
		}

		return mapTypeNode;
	}

	protected IdentNode keyTypeUnresolved;
	public TypeNode keyType;
	protected IdentNode valueTypeUnresolved;
	public TypeNode valueType;

	private SetTypeNode exceptCompatibleSetTyp;

	// the map type node instances are created in ParserEnvironment as needed
	public MapTypeNode(IdentNode keyTypeIdent, IdentNode valueTypeIdent) {
		keyTypeUnresolved   = becomeParent(keyTypeIdent);
		valueTypeUnresolved = becomeParent(valueTypeIdent);
	}

	private void setExceptCompatibleSetType(SetTypeNode stn) {
		exceptCompatibleSetTyp = stn;
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

	private static DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal() {
		if(keyTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)keyTypeUnresolved, keyTypeUnresolved.getScope());
		if(valueTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)valueTypeUnresolved, valueTypeUnresolved.getScope());

		keyType   = typeResolver.resolve(keyTypeUnresolved, this);
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(keyType == null || valueType == null) return false;

		if(keyType instanceof InheritanceTypeNode) {
			OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
					BasicTypeNode.typeType, this, OperatorSignature.mapEvaluator);
		} else {
			OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
					keyType, this, OperatorSignature.mapEvaluator);
		}
		OperatorSignature.makeBinOp(OperatorSignature.EQ, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.NE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_OR, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_AND, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, exceptCompatibleSetTyp,
				OperatorSignature.mapEvaluator);

		TypeNode.addCompatibility(this, BasicTypeNode.stringType);

		return true;
	}

	@Override
	protected MapType constructIR() {
		Type kt = keyType.getType();
		Type vt = valueType.getType();

		// return if the keyType or valueType construction already constructed the IR object
		if (isIRAlreadySet()) {
			return (MapType)getIR();
		}

		return new MapType(kt, vt);
	}
}
