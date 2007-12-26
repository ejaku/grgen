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

import java.util.HashMap;
import java.util.Map;

/**
 * A basic type AST node such as string or int
 */
public abstract class BasicTypeNode extends DeclaredTypeNode
{
	public static final BasicTypeNode stringType = new StringTypeNode();
	public static final BasicTypeNode typeType = new TypeTypeNode();
	public static final BasicTypeNode intType = new IntTypeNode();
	public static final BasicTypeNode doubleType = new DoubleTypeNode();
	public static final BasicTypeNode floatType = new FloatTypeNode();
	public static final BasicTypeNode booleanType = new BooleanTypeNode();
	public static final BasicTypeNode objectType = ObjectTypeNode.OBJECT_TYPE;
	public static final BasicTypeNode enumItemType = new EnumItemTypeNode();
	public static final BasicTypeNode voidType = new VoidTypeNode();

	public static final TypeNode errorType = new ErrorType(IdentNode.getInvalid());

	
	public static TypeNode getErrorType(IdentNode id) {
		return new ErrorType(id);
	}

	private static Object invalidValueType = new Object() {
		public String toString() {
			return "invalid value";
		}
	};

	/**
	 * This map contains the value types of the basic types.
	 * (BasicTypeNode -> Class)
	 */
	protected static Map<BasicTypeNode, Class<?>> valueMap = new HashMap<BasicTypeNode, Class<?>>();

	static {
		setName(BasicTypeNode.class, "basic type");

		//no explicit cast required
		addCompatibility(intType, floatType);
		addCompatibility(intType, doubleType);
		addCompatibility(floatType, doubleType);
		addCompatibility(enumItemType, intType);

		//require explicit cast
		addCastability(booleanType, stringType);
		addCastability(intType, stringType);
		addCastability(floatType, intType);
		addCastability(floatType, stringType);
		addCastability(doubleType, intType);
		addCastability(doubleType, floatType);
		addCastability(doubleType, stringType);

		addCastability(enumItemType, stringType);
		addCastability(enumItemType, floatType);
		addCastability(enumItemType, doubleType);

		valueMap.put(intType, Integer.class);
		valueMap.put(floatType, Float.class);
		valueMap.put(doubleType, Double.class);
		valueMap.put(booleanType, Boolean.class);
		valueMap.put(stringType, String.class);
		valueMap.put(enumItemType, Integer.class);
		valueMap.put(objectType, ObjectTypeNode.Value.class);

//		addCompatibility(voidType, intType);
//		addCompatibility(voidType, booleanType);
//		addCompatibility(voidType, stringType);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result
		
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
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		
		return successfullyChecked;
	}

	/**
	 * This node may have no children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return children() == 0;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#isBasic()
	 */
	public boolean isBasic() {
		return true;
	}

	/**
	 * Return the Java class, that represents a value of a constant in this
	 * type.
	 * @return
	 */
	public Class<?> getValueType() {
		if(!valueMap.containsKey(this))
			return invalidValueType.getClass();
		else
			return valueMap.get(this);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#isEqual(de.unika.ipd.grgen.ast.TypeNode)
	 */
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

