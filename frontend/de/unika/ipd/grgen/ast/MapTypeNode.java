/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapType;

public class MapTypeNode extends DeclaredTypeNode {
	static {
		setName(MapTypeNode.class, "map type");
	}
	
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
	
	IdentNode keyTypeUnresolved;
	TypeNode keyType;
	IdentNode valueTypeUnresolved;
	TypeNode valueType;
	
	SetTypeNode exceptCompatibleSetTyp;
	
	// the map type node instances are created in ParserEnvironment as needed
	public MapTypeNode(IdentNode keyTypeIdent, IdentNode valueTypeIdent) {
		keyTypeUnresolved   = becomeParent(keyTypeIdent);
		valueTypeUnresolved = becomeParent(valueTypeIdent);
	}
	
	public void setExceptCompatibleSetType(SetTypeNode stn) {
		exceptCompatibleSetTyp = stn;
	}
	
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}
	
	private static final DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	protected boolean resolveLocal() {
		keyType   = typeResolver.resolve(keyTypeUnresolved, this);
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(keyType == null || valueType == null) return false;
		
		OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
				keyType, this, OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_OR, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_AND, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, this,
				OperatorSignature.mapEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, exceptCompatibleSetTyp,
				OperatorSignature.mapEvaluator);
		
		return true;
	}
	
	protected IR constructIR() {
		return new MapType(keyType.getType(), valueType.getType());
	}
}
