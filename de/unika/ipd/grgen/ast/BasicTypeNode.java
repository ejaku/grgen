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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IntType;
import de.unika.ipd.grgen.ir.PrimitiveType;
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
	private static final Map compatibleMap = new HashMap();
	
	/**
	 * A map, that maps each basic type to a set to all other basic types,
	 * that are castable to the type.
	 */
	private static final Map castableMap = new HashMap();
	
	/**
	 * The string basic type.
	 */
	public static final BasicTypeNode stringType = new BasicTypeNode() {
		protected IR constructIR() {
			return new StringType(getIdentNode().getIdent());
		}
	};
	
	/**
	 * The integer basic type.
	 */
	public static final BasicTypeNode intType = new BasicTypeNode() {
		protected IR constructIR() {
			return new IntType(getIdentNode().getIdent());
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
			return new VoidType("void");
		}
	};
	
	/**
	 * The error basic type. It is compatible to no other type.
	 */
	public static final BasicTypeNode errorType = new BasicTypeNode() {
		protected IR constructIR() {
			return new VoidType("error");
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
	private static Map valueMap = new HashMap();
	
	private static void addTypeToMap(Map map, TypeNode index, TypeNode target) {
		if(!map.containsKey(index))
			map.put(index, new HashSet());
		
		Set s = (Set) map.get(index);
		s.add(target);
	}
	
	private static void addCastability(TypeNode from, TypeNode to) {
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
			Set s = (Set) compatibleMap.get(a);
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
		setName(voidType.getClass(), "void type");
		setName(errorType.getClass(), "error type");
		
		addCompatibility(enumItemType, intType);
		addCompatibility(enumItemType, stringType);
		addCompatibility(booleanType, intType);
		addCastability(intType, stringType);
		addCastability(booleanType, stringType);
		
		valueMap.put(intType, Integer.class);
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
	protected void doGetCompatibleToTypes(Collection coll) {
		debug.entering();
		debug.report(NOTE, "compatible types to " + getName() + ":");
		
		Object obj = compatibleMap.get(this);
		if(obj != null) {
			Collection compat = (Collection) obj;
			for(Iterator it = compat.iterator(); it.hasNext();) {
				debug.report(NOTE, "" + ((BaseNode) it.next()).getName());
			}
			coll.addAll((Collection) obj);
		}
		
		debug.leaving();
	}
	
	
	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#getCastableTypes(java.util.Collection)
	 */
	protected void doGetCastableToTypes(Collection coll) {
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
	
}
