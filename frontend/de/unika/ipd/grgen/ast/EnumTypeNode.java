/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;

/**
 * An enumeration type AST node.
 */
public class EnumTypeNode extends CompoundTypeNode
{
	static {
		setName(EnumTypeNode.class, "enum type");
	}

	BaseNode elements;

	private static final Checker childrenChecker =
		new CollectChecker(new SimpleChecker(EnumItemNode.class));

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

	public EnumTypeNode(CollectNode body)
	{
		this.elements = body==null ? NULL : body;
		becomeParent(this.elements);

		//the castability of the this enum type
		addCastability(this, BasicTypeNode.stringType);
		addCastability(this, BasicTypeNode.floatType);
		addCastability(this, BasicTypeNode.doubleType);

		//enumerations can be used with the conditional operator
		OperatorSignature.makeOp(OperatorSignature.COND, this,
								 new TypeNode[] { BasicTypeNode.booleanType, this, this },
								 OperatorSignature.condEvaluator
								);

		//the compatibility of the this enum type
		addCompatibility(this, BasicTypeNode.intType);
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
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result
		
		successfullyResolved = elements.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = elements.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return childrenChecker.check(elements, error);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR()
	{
		Ident name = (Ident) getIdentNode().checkIR(Ident.class);
		EnumType ty = new EnumType(name);

		for (BaseNode n : elements.getChildren()) {
			EnumItemNode item = (EnumItemNode) n;
			ty.addItem(item.getItem());
		}
		/*
		 for(Iterator i = elements.getChildren(); i.hasNext();) {
		 EnumItemNode item = (EnumItemNode) i.next();

		 ty.addItem(item.getEnumItem()
		 EnumItem ir = (EnumItem) item.checkIR(EnumItem.class);

		 }

		 Ident name = (Ident) getIdentNode().checkIR(Ident.class);
		 EnumType ty = new EnumType(name);
		 for(Iterator i = elements.getChildren(); i.hasNext();) {
		 BaseNode child = (BaseNode) i.next();
		 Ident id = (Ident) child.checkIR(Ident.class);
		 ty.addItem(id);
		 }*/
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

	public boolean isEqual(TypeNode t) {
		return (this == t);
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
	
	// debug guards to protect again accessing wrong elements
	public void addChild(BaseNode n) {
		assert(false);
	}
	public void setChild(int pos, BaseNode n) {
		assert(false);
	}
	public BaseNode getChild(int i) {
		assert(false);
		return null;
	}
	public int children() {
		assert(false);
		return 0;
	}
	public BaseNode replaceChild(int i, BaseNode n) {
		assert(false);
		return null;
	}
}
