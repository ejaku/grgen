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
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.BooleanType;
import de.unika.ipd.grgen.ir.FloatType;
import de.unika.ipd.grgen.ir.DoubleType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IntType;
import de.unika.ipd.grgen.ir.PrimitiveType;
import de.unika.ipd.grgen.ir.TypeType;
import de.unika.ipd.grgen.ir.StringType;
import de.unika.ipd.grgen.ir.VoidType;

/**
 * A basic type AST node such as string or int
 */
public abstract class BasicTypeNode extends DeclaredTypeNode {
	
	/**
	 * A map, that maps each basic type to a set to all other basic types,
	 * that are compatible to the type.
	 */
	private static final Map<TypeNode, HashSet> compatibleMap = new HashMap<TypeNode, HashSet>();
	
	/**
	 * A map, that maps each basic type to a set to all other basic types,
	 * that are castable to the type.
	 */
	private static final Map<TypeNode, HashSet> castableMap = new HashMap<TypeNode, HashSet>();
	
	/**
	 * The string basic type.
	 */
	public static final BasicTypeNode stringType = new BasicTypeNode() {
		protected IR constructIR() {
			return new StringType(getIdentNode().getIdent());
		}
		public String toString() {
			return "string";
		}
	};
	
	/**
	 * The type basic type.
	 */
	public static final BasicTypeNode typeType = new BasicTypeNode() {
		protected IR constructIR() {
			return new TypeType(getIdentNode().getIdent());
		}
	};
	
	/**
	 * The integer basic type.
	 */
	public static final BasicTypeNode intType = new BasicTypeNode() {
		protected IR constructIR() {
			return new IntType(getIdentNode().getIdent());
		}
		public String toString() {
			return "int";
		}
	};
	
	/**
	 * The double precision floating point basic type.
	 */
	public static final BasicTypeNode doubleType = new BasicTypeNode() {
		protected IR constructIR() {
			return new DoubleType(getIdentNode().getIdent());
		}
		public String toString() {
			return "double";
		}
	};
	/**
	 * The floating point basic type.
	 */
	public static final BasicTypeNode floatType = new BasicTypeNode() {
		protected IR constructIR() {
			return new FloatType(getIdentNode().getIdent());
		}
		public String toString() {
			return "float";
		}
	};
	
	/**
	 * The boolean basic type.
	 */
	public static final BasicTypeNode booleanType =
		new BasicTypeNode() {
		protected IR constructIR() {
			return new BooleanType(getIdentNode().getIdent());
		}
		public String toString() {
			return "boolean";
		}
	};
	
	/**
	 * The enum member type.
	 */
	public static final BasicTypeNode enumItemType =
		new BasicTypeNode() {
		protected IR constructIR() {
			return new IntType(getIdentNode().getIdent());
		}
	};


	/**
	 * The void basic type. It is compatible to no other type.
	 */
	public static final BasicTypeNode voidType =
		new BasicTypeNode() {
		protected IR constructIR() {
			return new VoidType(getIdentNode().getIdent());
		}
		public String toString() {
			return "void";
		}
	};
	
	/**
	 * The error basic type. It is compatible to no other type.
	 */
	public static final BasicTypeNode errorType = new BasicTypeNode() {
		protected IR constructIR() {
			return new VoidType(getIdentNode().getIdent());
		}
		public String toString() {
			return "error type";
		}
	};
	
	private static Object invalidValueType = new Object() {
		public String toString() {
			return "invalid value";
		}
	};
	
	/**
	 * This map contains the value types of the basic types.
	 * (BasicTypeNode -> Class)
	 */
	private static Map<BasicTypeNode, Object> valueMap = new HashMap<BasicTypeNode, Object>();
	
	private static void addTypeToMap(Map<TypeNode, HashSet> map, TypeNode index, TypeNode target) {
		if(!map.containsKey(index))
			map.put(index, new HashSet());
		
		Set<TypeNode> s = map.get(index);
		s.add(target);
	}
	
	public static void addCastability(TypeNode from, TypeNode to) {
		addTypeToMap(castableMap, from, to);
	}
	
	/**
	 * Add a compatibility to the compatibility map.
	 * @param a The first type.
	 * @param b The second type.
	 */
	private static void addCompatibility(TypeNode a, TypeNode b) {
		addTypeToMap(compatibleMap, a, b);
	}
	
	/**
	 * Checks, if two types are compatible
	 * @param a The first type.
	 * @param b The second type.
	 * @return true, if the two types are compatible.
	 */
	private static boolean isCompatible(TypeNode a, TypeNode b) {
		boolean res = false;
		
		if(compatibleMap.containsKey(a)) {
			Set s = compatibleMap.get(a);
			res = s.contains(b);
		}
		
		return res;
	}
	
	static {
		setName(BasicTypeNode.class, "basic type");
		setName(intType.getClass(), "int type");
		setName(booleanType.getClass(), "boolean type");
		setName(stringType.getClass(), "string type");
		setName(enumItemType.getClass(), "enum item type");
		setName(floatType.getClass(), "float type");
		setName(doubleType.getClass(), "double type");
		setName(typeType.getClass(), "type type");
		setName(voidType.getClass(), "void type");
		setName(errorType.getClass(), "error type");
		
		//no explicit cast required
		addCompatibility(intType, floatType);
		addCompatibility(intType, doubleType);
		addCompatibility(floatType, doubleType);

		//require explicit cast
		addCastability(booleanType, stringType);
		addCastability(intType, stringType);
		addCastability(floatType, intType);
		addCastability(floatType, stringType);
		addCastability(doubleType, intType);
		addCastability(doubleType, floatType);
		addCastability(doubleType, stringType);
		
		valueMap.put(intType, Integer.class);
		valueMap.put(floatType, Float.class);
		valueMap.put(doubleType, Double.class);
		valueMap.put(booleanType, Boolean.class);
		valueMap.put(stringType, String.class);
		valueMap.put(enumItemType, Integer.class);
		
//		addCompatibility(voidType, intType);
//		addCompatibility(voidType, booleanType);
//		addCompatibility(voidType, stringType);
	}
	
	/**
	 * This node may have no children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return children() == 0;
	}
	
	protected PrimitiveType getPrimitiveType() {
		return (PrimitiveType) checkIR(PrimitiveType.class);
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
	public Class getValueType() {
		if(!valueMap.containsKey(this))
			return invalidValueType.getClass();
		else
			return (Class) valueMap.get(this);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#getCompatibleTypes(java.util.Collection)
	 */
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		debug.report(NOTE, "compatible types to " + getName() + ":");
		
		Object obj = compatibleMap.get(this);
		if(obj != null) {
			Collection<BaseNode> compat = (Collection) obj;
			for(Iterator<BaseNode> it = compat.iterator(); it.hasNext();) {
				debug.report(NOTE, "" + it.next().getName());
			}
			coll.addAll((Collection) obj);
		}
	}
	
	
	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#getCastableTypes(java.util.Collection)
	 */
	protected void doGetCastableToTypes(Collection<TypeNode> coll) {
		Object obj = castableMap.get(this);
		if(obj != null)
			coll.addAll((Collection) obj);
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
