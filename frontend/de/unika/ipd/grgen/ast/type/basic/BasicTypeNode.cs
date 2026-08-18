/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>
namespace de.unika.ipd.grgen.ast.type.basic
{

	using System;
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using NullConstNode = de.unika.ipd.grgen.ast.expr.NullConstNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

	/// <summary>
	/// A basic type AST node such as string or int
	/// </summary>
	public abstract class BasicTypeNode : DeclaredTypeNode
	{
		public static readonly BasicTypeNode stringType = new StringTypeNode();
		public static readonly BasicTypeNode typeType = new TypeTypeNode();
		public static readonly BasicTypeNode byteType = new ByteTypeNode();
		public static readonly BasicTypeNode shortType = new ShortTypeNode();
		public static readonly BasicTypeNode intType = new IntTypeNode();
		public static readonly BasicTypeNode longType = new LongTypeNode();
		public static readonly BasicTypeNode doubleType = new DoubleTypeNode();
		public static readonly BasicTypeNode floatType = new FloatTypeNode();
		public static readonly BasicTypeNode booleanType = new BooleanTypeNode();
		public static readonly BasicTypeNode objectType = new ObjectTypeNode();
		public static readonly BasicTypeNode enumItemType = new EnumItemTypeNode();
		public static readonly BasicTypeNode voidType = new VoidTypeNode();
		public static readonly BasicTypeNode nullType = new NullTypeNode();
		public static readonly BasicTypeNode graphType = new GraphTypeNode();
		public static readonly BasicTypeNode untypedType = new UntypedExecVarTypeNode();

		public static readonly TypeNode errorType = new ErrorTypeNode(IdentNode.Invalid);

		public static TypeNode GetErrorType(IdentNode id)
		{
			return new ErrorTypeNode(id);
		}

		private static object invalidValueType = new ObjectAnonymousInnerClass();

		private class ObjectAnonymousInnerClass : object
		{
			public override string ToString()
			{
				return "invalid value";
			}
		}

		/// <summary>
		/// This map contains the value types of the basic types.
		///  (BasicTypeNode -> Class) 
		/// </summary>
		protected internal static IDictionary<BasicTypeNode, Type> valueMap = new Dictionary<BasicTypeNode, Type>();

		static BasicTypeNode()
		{
			SetClassName(typeof(BasicTypeNode), "basic type");

			valueMap[byteType] = typeof(SByte);
			valueMap[shortType] = typeof(Int16);
			valueMap[intType] = typeof(Int32);
			valueMap[longType] = typeof(Int64);
			valueMap[floatType] = typeof(Single);
			valueMap[doubleType] = typeof(Double);
			valueMap[booleanType] = typeof(Boolean);
			valueMap[stringType] = typeof(String);
			valueMap[enumItemType] = typeof(Int32);
			valueMap[objectType] = typeof(ObjectTypeNode.Value);
			valueMap[nullType] = typeof(NullConstNode.Value);
			valueMap[untypedType] = typeof(UntypedExecVarTypeNode.Value);

			//////////////////////////////////////////////////////////
			//implicit casts; upcasts for arithmetic, and everything to string (easy emitting)
			//////////////////////////////////////////////////////////

			AddCompatibility(enumItemType, byteType);
			AddCompatibility(enumItemType, shortType);
			AddCompatibility(enumItemType, intType);
			AddCompatibility(enumItemType, longType);
			AddCompatibility(enumItemType, floatType);
			AddCompatibility(enumItemType, doubleType);

			AddCompatibility(byteType, shortType);
			AddCompatibility(byteType, intType);
			AddCompatibility(byteType, longType);
			AddCompatibility(byteType, floatType);
			AddCompatibility(byteType, doubleType);

			AddCompatibility(shortType, intType);
			AddCompatibility(shortType, longType);
			AddCompatibility(shortType, floatType);
			AddCompatibility(shortType, doubleType);

			AddCompatibility(intType, longType);
			AddCompatibility(intType, floatType);
			AddCompatibility(intType, doubleType);

			AddCompatibility(longType, floatType);
			AddCompatibility(longType, doubleType);

			AddCompatibility(floatType, doubleType);

			AddCompatibility(enumItemType, stringType);
			AddCompatibility(byteType, stringType);
			AddCompatibility(shortType, stringType);
			AddCompatibility(intType, stringType);
			AddCompatibility(longType, stringType);
			AddCompatibility(floatType, stringType);
			AddCompatibility(doubleType, stringType);
			AddCompatibility(booleanType, stringType);
			AddCompatibility(objectType, stringType);
			AddCompatibility(voidType, stringType);

			//////////////////////////////////////////////////////////
			//implicit casts to untyped (due to sequence variables of statically not known type)
			//////////////////////////////////////////////////////////

			AddCompatibility(enumItemType, untypedType);
			AddCompatibility(byteType, untypedType);
			AddCompatibility(shortType, untypedType);
			AddCompatibility(intType, untypedType);
			AddCompatibility(longType, untypedType);
			AddCompatibility(floatType, untypedType);
			AddCompatibility(doubleType, untypedType);
			AddCompatibility(booleanType, untypedType);
			AddCompatibility(objectType, untypedType);
			AddCompatibility(voidType, untypedType);
			AddCompatibility(stringType, untypedType);
			AddCompatibility(nullType, untypedType);
			AddCompatibility(graphType, untypedType);

			//////////////////////////////////////////////////////////
			//explicit casts; downcasts for arithmetic, everything into an object
			//////////////////////////////////////////////////////////

			AddCastability(shortType, byteType);

			AddCastability(intType, byteType);
			AddCastability(intType, shortType);

			AddCastability(longType, byteType);
			AddCastability(longType, shortType);
			AddCastability(longType, intType);

			AddCastability(floatType, byteType);
			AddCastability(floatType, shortType);
			AddCastability(floatType, intType);
			AddCastability(floatType, longType);

			AddCastability(doubleType, byteType);
			AddCastability(doubleType, shortType);
			AddCastability(doubleType, intType);
			AddCastability(doubleType, longType);
			AddCastability(doubleType, floatType);

			AddCastability(enumItemType, objectType);
			AddCastability(byteType, objectType);
			AddCastability(shortType, objectType);
			AddCastability(intType, objectType);
			AddCastability(longType, objectType);
			AddCastability(floatType, objectType);
			AddCastability(doubleType, objectType);
			AddCastability(booleanType, objectType);
			AddCastability(stringType, objectType);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				// no children
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				// no children
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.type.TypeNode.isBasic() "/>
		public override sealed bool IsBasic()
		{
			return true;
		}

		/// <summary>
		/// Return the Java class, that represents a value of a constant in this type. </summary>
		public Type ValueType
		{
			get
			{
				if(!valueMap.ContainsKey(this))
					return invalidValueType.GetType();
				else
					return valueMap[this];
			}
		}

		public static string KindStr
		{
			get
			{
				return "basic type";
			}
		}

		// implements type promotion (byte/short->int, float->double)
		public static TypeNode GetArrayAccumulationResultType(TypeNode inputType)
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

}
