/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;

/**
 * An enumeration type AST node.
 */
public class EnumTypeNode extends CompoundTypeNode {
	static {
		setName(EnumTypeNode.class, "enum type");
	}

	CollectNode<EnumItemNode> elements;

	/*
	 private static final OperatorSignature.Evaluator enumEvaluator =
	 new OperatorSignature.Evaluator() {
	 public ConstNode evaluate(Coords coords, OperatorSignature op,
	 ConstNode[] args) {

	 switch(op.getOpId()) {
	 case OperatorSignature.EQ:
	 return new BoolConstNode(coords, args[0].getValue().equals(args[1].getValue()));
	 case OperatorSignature.NE:
	 return new BoolConstNode(coords, !args[0].getValue().equals(args[1].getValue()));
	 }
	 return ConstNode.getInvalid();
	 }
	 };
	 */

	public EnumTypeNode(CollectNode<EnumItemNode> body) {
		this.elements = body;
		becomeParent(this.elements);

		//enumerations can be used with the conditional operator
		OperatorSignature.makeOp(OperatorSignature.COND, this,
								 new TypeNode[] { BasicTypeNode.booleanType, this, this },
								 OperatorSignature.condEvaluator
								);

		//the compatibility of the this enum type
		addCompatibility(this, BasicTypeNode.intType);
		addCompatibility(this, BasicTypeNode.floatType);
		addCompatibility(this, BasicTypeNode.doubleType);
		addCompatibility(this, BasicTypeNode.stringType);
	}

	/*
	 protected void doGetCastableToTypes(Collection<TypeNode> coll) {
	 Object obj = BasicTypeNode.castableMap.get(this);
	 if(obj != null)
	 coll.addAll((Collection) obj);
	 }*/

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(elements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("elements");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		Ident name = getIdentNode().checkIR(Ident.class);
		EnumType ty = new EnumType(name);

		for (EnumItemNode item : elements.getChildren()) {
			EnumItem it = item.getItem();
			it.getValue().lateInit(ty, it);
			ty.addItem(it);
		}

		return ty;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#coercible(de.unika.ipd.grgen.ast.TypeNode)
	 * Enums are not coercible to any type.
	 */
	protected boolean compatible(TypeNode t) {
		return t == this;
	}

	/** @see de.unika.ipd.grgen.ast.BasicTypeNode#getValueType() */
	public Class<Integer> getValueType() {
		return Integer.class;
	}

	public String toString() {
		return "enum " + getIdentNode();
	}

	public static String getKindStr() {
		return "enum type";
	}

	public static String getUseStr() {
		return "enum";
	}
}
