/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapType;

public class MapTypeNode extends DeclaredTypeNode {
	static {
		setName(MapTypeNode.class, "map type");
	}
	
	IdentNode keyTypeUnresolved;
	TypeNode keyType;
	IdentNode valueTypeUnresolved;
	TypeNode valueType;
	
	public MapTypeNode(IdentNode keyTypeIdent, IdentNode valueTypeIdent) {
		keyTypeUnresolved   = becomeParent(keyTypeIdent);
		valueTypeUnresolved = becomeParent(valueTypeIdent);
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
		
		return true;
	}
	
	protected IR constructIR() {
		return new MapType(keyType.getType(), valueType.getType());
	}
}
