/**
 * @file BasicTypeNode.java
 * @author shack
 * @date Jul 6, 2003
 */
package de.unika.ipd.grgen.ast;

import java.util.HashMap;
import java.util.HashSet;
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

	/**
	 * A map, that maps each basic type to a set to all other basic types, 
	 * that are compatible to the type.
	 */
	private static Map compatibleMap = new HashMap();

	private static void addCompatibility(TypeNode a, TypeNode b) {
		if(!compatibleMap.containsKey(a))
			compatibleMap.put(a, new HashSet());
			 
		Set s = (Set) compatibleMap.get(a);
		a.addChild(b);
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
		setName(voidType.getClass(), "void type");
		setName(errorType.getClass(), "error type");
		
		addCompatibility(intType, booleanType);
		addCompatibility(booleanType, intType);
		
		
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

	/** 
	 * @see de.unika.ipd.grgen.ast.TypeNode#coercible(de.unika.ipd.grgen.ast.TypeNode)
	 */
	protected boolean coercible(TypeNode type) {
		return isCompatible(this, type);
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

}
