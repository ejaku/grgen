/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapTypeNode.java 22952 2008-10-16 19:50:10Z moritz $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SetType;

public class SetTypeNode extends DeclaredTypeNode {
	static {
		setName(SetTypeNode.class, "set type");
	}
	
	public String getName() {
		return "set<" + valueTypeUnresolved.toString() + "> type";
	}
	
	private static HashMap<String, SetTypeNode> setTypes = new HashMap<String, SetTypeNode>();
	
	public static SetTypeNode getSetType(IdentNode valueTypeIdent) {
		String keyStr = valueTypeIdent.toString();
		SetTypeNode setTypeNode = setTypes.get(keyStr);

		if(setTypeNode == null)
			setTypes.put(keyStr, setTypeNode = new SetTypeNode(valueTypeIdent));

		return setTypeNode;
	}
	
	IdentNode valueTypeUnresolved;
	TypeNode valueType;
	
	// the set type node instances are created in ParserEnvironment as needed
	public SetTypeNode(IdentNode valueTypeIdent) {
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
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(valueType == null) return false;
		
		OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
				valueType, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_OR, this, this, this,
				OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_AND, this, this, this,
				OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, this,
				OperatorSignature.setEvaluator);
		
		return true;
	}
	
	protected IR constructIR() {
		return new SetType(valueType.getType());
	}
}
