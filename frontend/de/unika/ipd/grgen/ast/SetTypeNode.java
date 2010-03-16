/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SetTypeNode.java 22952 2008-10-16 19:50:10Z moritz $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SetType;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

public class SetTypeNode extends DeclaredTypeNode {
	static {
		setName(SetTypeNode.class, "set type");
	}

	@Override
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

	private IdentNode valueTypeUnresolved;
	protected TypeNode valueType;

	// the set type node instances are created in ParserEnvironment as needed
	public SetTypeNode(IdentNode valueTypeIdent) {
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

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 */
	public static boolean fixupDefinition(IdentNode id, Scope scope) {
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			id.reportError("Identifier \"" + id + "\" not declared in this scope: " + scope);
		}

		return res;
	}

	private static final DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal() {
		if(valueTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)valueTypeUnresolved, valueTypeUnresolved.getScope());
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(valueType == null) return false;

		OperatorSignature.makeBinOp(OperatorSignature.IN, BasicTypeNode.booleanType,
				valueType, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EQ, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.NE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.GE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LT, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.LE, BasicTypeNode.booleanType,
				this, this, OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_OR, this, this, this,
				OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.BIT_AND, this, this, this,
				OperatorSignature.setEvaluator);
		OperatorSignature.makeBinOp(OperatorSignature.EXCEPT, this, this, this,
				OperatorSignature.setEvaluator);

		return true;
	}

	@Override
	protected IR constructIR() {
		return new SetType(valueType.getType());
	}
}
