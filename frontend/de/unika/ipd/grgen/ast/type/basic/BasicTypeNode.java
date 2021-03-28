/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */
package de.unika.ipd.grgen.ast.type.basic;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.expr.NullConstNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

/**
 * A basic type AST node such as string or int
 */
public abstract class BasicTypeNode extends DeclaredTypeNode
{
	public static final BasicTypeNode stringType = new StringTypeNode();
	public static final BasicTypeNode typeType = new TypeTypeNode();
	public static final BasicTypeNode byteType = new ByteTypeNode();
	public static final BasicTypeNode shortType = new ShortTypeNode();
	public static final BasicTypeNode intType = new IntTypeNode();
	public static final BasicTypeNode longType = new LongTypeNode();
	public static final BasicTypeNode doubleType = new DoubleTypeNode();
	public static final BasicTypeNode floatType = new FloatTypeNode();
	public static final BasicTypeNode booleanType = new BooleanTypeNode();
	public static final BasicTypeNode objectType = new ObjectTypeNode();
	public static final BasicTypeNode enumItemType = new EnumItemTypeNode();
	public static final BasicTypeNode voidType = new VoidTypeNode();
	public static final BasicTypeNode nullType = new NullTypeNode();
	public static final BasicTypeNode graphType = new GraphTypeNode();
	public static final BasicTypeNode untypedType = new UntypedExecVarTypeNode();

	public static final TypeNode errorType = new ErrorTypeNode(IdentNode.getInvalid());

	public static TypeNode getErrorType(IdentNode id)
	{
		return new ErrorTypeNode(id);
	}

	private static Object invalidValueType = new Object() {
		@Override
		public String toString()
		{
			return "invalid value";
		}
	};

	/** This map contains the value types of the basic types.
	 *  (BasicTypeNode -> Class) */
	protected static Map<BasicTypeNode, Class<?>> valueMap = new HashMap<BasicTypeNode, Class<?>>();

	static {
		setName(BasicTypeNode.class, "basic type");

		valueMap.put(byteType, Byte.class);
		valueMap.put(shortType, Short.class);
		valueMap.put(intType, Integer.class);
		valueMap.put(longType, Long.class);
		valueMap.put(floatType, Float.class);
		valueMap.put(doubleType, Double.class);
		valueMap.put(booleanType, Boolean.class);
		valueMap.put(stringType, String.class);
		valueMap.put(enumItemType, Integer.class);
		valueMap.put(objectType, ObjectTypeNode.Value.class);
		valueMap.put(nullType, NullConstNode.Value.class);
		valueMap.put(untypedType, UntypedExecVarTypeNode.Value.class);

		//////////////////////////////////////////////////////////
		//implicit casts; upcasts for arithmetic, and everything to string (easy emitting)
		//////////////////////////////////////////////////////////

		addCompatibility(enumItemType, byteType);
		addCompatibility(enumItemType, shortType);
		addCompatibility(enumItemType, intType);
		addCompatibility(enumItemType, longType);
		addCompatibility(enumItemType, floatType);
		addCompatibility(enumItemType, doubleType);

		addCompatibility(byteType, shortType);
		addCompatibility(byteType, intType);
		addCompatibility(byteType, longType);
		addCompatibility(byteType, floatType);
		addCompatibility(byteType, doubleType);

		addCompatibility(shortType, intType);
		addCompatibility(shortType, longType);
		addCompatibility(shortType, floatType);
		addCompatibility(shortType, doubleType);

		addCompatibility(intType, longType);
		addCompatibility(intType, floatType);
		addCompatibility(intType, doubleType);

		addCompatibility(longType, floatType);
		addCompatibility(longType, doubleType);

		addCompatibility(floatType, doubleType);

		addCompatibility(enumItemType, stringType);
		addCompatibility(byteType, stringType);
		addCompatibility(shortType, stringType);
		addCompatibility(intType, stringType);
		addCompatibility(longType, stringType);
		addCompatibility(floatType, stringType);
		addCompatibility(doubleType, stringType);
		addCompatibility(booleanType, stringType);
		addCompatibility(objectType, stringType);
		addCompatibility(voidType, stringType);

		//////////////////////////////////////////////////////////
		//implicit casts to untyped (due to sequence variables of statically not known type)
		//////////////////////////////////////////////////////////

		addCompatibility(enumItemType, untypedType);
		addCompatibility(byteType, untypedType);
		addCompatibility(shortType, untypedType);
		addCompatibility(intType, untypedType);
		addCompatibility(longType, untypedType);
		addCompatibility(floatType, untypedType);
		addCompatibility(doubleType, untypedType);
		addCompatibility(booleanType, untypedType);
		addCompatibility(objectType, untypedType);
		addCompatibility(voidType, untypedType);
		addCompatibility(stringType, untypedType);
		addCompatibility(nullType, untypedType);

		//////////////////////////////////////////////////////////
		//explicit casts; downcasts for arithmetic, everything into an object
		//////////////////////////////////////////////////////////

		addCastability(shortType, byteType);

		addCastability(intType, byteType);
		addCastability(intType, shortType);

		addCastability(longType, byteType);
		addCastability(longType, shortType);
		addCastability(longType, intType);

		addCastability(floatType, byteType);
		addCastability(floatType, shortType);
		addCastability(floatType, intType);
		addCastability(floatType, longType);

		addCastability(doubleType, byteType);
		addCastability(doubleType, shortType);
		addCastability(doubleType, intType);
		addCastability(doubleType, longType);
		addCastability(doubleType, floatType);

		addCastability(enumItemType, objectType);
		addCastability(byteType, objectType);
		addCastability(shortType, objectType);
		addCastability(intType, objectType);
		addCastability(longType, objectType);
		addCastability(floatType, objectType);
		addCastability(doubleType, objectType);
		addCastability(booleanType, objectType);
		addCastability(stringType, objectType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.type.TypeNode#isBasic() */
	@Override
	public final boolean isBasic()
	{
		return true;
	}

	/** Return the Java class, that represents a value of a constant in this type. */
	public final Class<?> getValueType()
	{
		if(!valueMap.containsKey(this)) {
			return invalidValueType.getClass();
		} else {
			return valueMap.get(this);
		}
	}

	public static String getKindStr()
	{
		return "basic type";
	}

	// implements type promotion (byte/short->int, float->double)
	public static TypeNode getArrayAccumulationResultType(TypeNode inputType)
	{
		if(inputType == byteType)
			return intType;
		else if(inputType == shortType)
			return intType;
		else if(inputType == intType)
			return intType;
		else if(inputType == longType)
			return longType;
		else if(inputType == floatType)
			return doubleType;
		else if(inputType == doubleType)
			return doubleType;
		else
			return errorType;
	}
}
