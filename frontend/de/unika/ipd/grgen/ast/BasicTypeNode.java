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
 * @file BasicTypeNode.java
 * @author shack
 * @date Jul 6, 2003
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

/**
 * A basic type AST node such as string or int
 */
public abstract class BasicTypeNode extends DeclaredTypeNode {
	public static final BasicTypeNode stringType = new StringTypeNode();
	public static final BasicTypeNode typeType = new TypeTypeNode();
	public static final BasicTypeNode intType = new IntTypeNode();
	public static final BasicTypeNode doubleType = new DoubleTypeNode();
	public static final BasicTypeNode floatType = new FloatTypeNode();
	public static final BasicTypeNode booleanType = new BooleanTypeNode();
	public static final BasicTypeNode objectType = ObjectTypeNode.OBJECT_TYPE;
	public static final BasicTypeNode enumItemType = new EnumItemTypeNode();
	public static final BasicTypeNode voidType = new VoidTypeNode();

	public static final TypeNode errorType = new ErrorTypeNode(IdentNode.getInvalid());


	public static TypeNode getErrorType(IdentNode id) {
		return new ErrorTypeNode(id);
	}

	private static Object invalidValueType = new Object() {
		public String toString() {
			return "invalid value";
		}
	};

	/** This map contains the value types of the basic types.
	 *  (BasicTypeNode -> Class) */
	protected static Map<BasicTypeNode, Class<?>> valueMap = new HashMap<BasicTypeNode, Class<?>>();

	static {
		setName(BasicTypeNode.class, "basic type");

		//no explicit cast required
		addCompatibility(enumItemType, intType);
		addCompatibility(enumItemType, floatType);
		addCompatibility(enumItemType, doubleType);
		addCompatibility(enumItemType, stringType);

		addCompatibility(booleanType, stringType);

		addCompatibility(intType, floatType);
		addCompatibility(intType, doubleType);
		addCompatibility(intType, stringType);

		addCompatibility(floatType, doubleType);
		addCompatibility(floatType, stringType);

		addCompatibility(doubleType, stringType);

		//require explicit cast
		addCastability(floatType, intType);

		addCastability(doubleType, intType);
		addCastability(doubleType, floatType);

		valueMap.put(intType, Integer.class);
		valueMap.put(floatType, Float.class);
		valueMap.put(doubleType, Double.class);
		valueMap.put(booleanType, Boolean.class);
		valueMap.put(stringType, String.class);
		valueMap.put(enumItemType, Integer.class);
		valueMap.put(objectType, ObjectTypeNode.Value.class);

//		addCompatibility(voidType, intType);
//		addCompatibility(voidType, booleanType);
		addCompatibility(voidType, stringType);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.TypeNode#isBasic() */
	public boolean isBasic() {
		return true;
	}

	/** Return the Java class, that represents a value of a constant in this type. */
	public Class<?> getValueType() {
		if(!valueMap.containsKey(this)) {
			return invalidValueType.getClass();
		} else {
			return valueMap.get(this);
		}
	}

	/** @see de.unika.ipd.grgen.ast.TypeNode#isEqual(de.unika.ipd.grgen.ast.TypeNode) */
	public boolean isEqual(TypeNode t) {
		return t == this;
	}

	public static String getKindStr() {
		return "basic type";
	}

	public static String getUseStr() {
		return "basic type";
	}
}

