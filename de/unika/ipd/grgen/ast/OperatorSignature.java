/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;
import java.util.Map;
import java.util.HashMap;

/**
 * Operator Description class.
 */
public class OperatorSignature extends FunctionSignature {

	public static final int COND = 0;
	public static final int LOG_OR = 1;
	public static final int LOG_AND = 2;
	public static final int BIT_OR = 3;
	public static final int BIT_XOR = 4;
	public static final int BIT_AND = 5;
	public static final int EQ = 6;
	public static final int NE = 7;
	public static final int LT = 8;
	public static final int LE = 9; 
	public static final int GT = 10;
	public static final int GE = 11;
	public static final int SHL = 12;
	public static final int SHR = 13;	
	public static final int BIT_SHR = 14;	
	public static final int ADD = 15;	
	public static final int SUB = 16;	
	public static final int MUL = 17;	
	public static final int DIV = 18;	
	public static final int MOD = 19;	
	public static final int LOG_NOT = 20;	
	public static final int BIT_NOT = 21;	
	public static final int NEG = 22;	
	public static final int CAST = 23;	
	public static final int CONST = 24;	
	public static final int QUAL = 25;
	
	private static final int OPERATORS = QUAL + 1;	

	/** Arity map of the operators. */
	private static final Map arities = new HashMap();
	
	/** Name map of the operators. */
	private static final Map names = new HashMap();
	
	static {
		Integer two = new Integer(2);
		Integer one = new Integer(1);
		Integer zero = new Integer(0);
		
		for(int i = 0; i < OPERATORS; i++)
			arities.put(new Integer(i), two);
			
		arities.put(new Integer(COND), new Integer(3));
		arities.put(new Integer(LOG_NOT), one);
		arities.put(new Integer(BIT_NOT), one);
		arities.put(new Integer(NEG), one);
		arities.put(new Integer(CAST), one);
		arities.put(new Integer(CONST), zero);
	}
	
	static {
		names.put(new Integer(COND), "Cond");
		names.put(new Integer(LOG_OR), "LogOr");
		names.put(new Integer(LOG_AND), "LogAnd");
		names.put(new Integer(BIT_XOR), "BitXor");
		names.put(new Integer(BIT_OR), "BitOr");
		names.put(new Integer(BIT_AND), "BitAnd");
		names.put(new Integer(EQ), "Eq");
		names.put(new Integer(NE), "Ne");
		names.put(new Integer(LT), "Lt");
		names.put(new Integer(LE), "Le");
		names.put(new Integer(GT), "Gt");
		names.put(new Integer(GE), "Ge");
		names.put(new Integer(SHL), "Shl");
		names.put(new Integer(SHR), "Shr");
		names.put(new Integer(BIT_SHR), "BitShr");
		names.put(new Integer(ADD), "Add");
		names.put(new Integer(SUB), "Sub");
		names.put(new Integer(MUL), "Mul");
		names.put(new Integer(DIV), "Div");
		names.put(new Integer(MOD), "Mod");
		names.put(new Integer(LOG_NOT), "LogNot");
		names.put(new Integer(BIT_NOT), "BitNot");
		names.put(new Integer(NEG), "Neg");
		names.put(new Integer(QUAL), "Qual");
		names.put(new Integer(CAST), "Cast");
		names.put(new Integer(CONST), "Const");
	}
	
	/** Just a short form for the string type. */
	final static private TypeNode STRING = BasicTypeNode.stringType;
	
	/** Just a short form for the boolean type. */	
	final static private TypeNode BOOLEAN = BasicTypeNode.booleanType;
	
	/** Just a short form for the int type. */	
	final static private TypeNode INT = BasicTypeNode.intType;
	
	/** 
	 * Each operator is mapped by its ID to a Map, which maps
	 * each result type of the specific operator to a its
	 * signature.
	 */
	final static private Map operators = new HashMap();
	
	/**
	 * Makes an entry in the {@link #operators} map.
	 * @param id The ID of the operator.
	 * @param resType The result type of the operator.
	 * @param opTypes The operand types of the operator.
	 */
	final static private void makeOp(int id, TypeNode resType, TypeNode[] opTypes) {
		Integer oid = new Integer(id);
		
		if(operators.get(oid) == null) 
			operators.put(oid, new HashMap());
			
		Map typeMap = (Map) operators.get(oid);
		typeMap.put(resType, new OperatorSignature(id, resType, opTypes));
	}
	
	/**
	 * Enter a binary operator.
	 * This is just a convenience function for 
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.   
	 */
	final static private void makeBinOp(int id, TypeNode res, 
		TypeNode op0, TypeNode op1) {
		
		makeOp(id, res, new TypeNode[] { op0, op1 });		
	}

	/**
	 * Enter an unary operator.
	 * This is just a convenience function for 
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.   
	 */
	final static private void makeUnOp(int id, TypeNode res, TypeNode op0) {
		makeOp(id, res, new TypeNode[] { op0 });
	}

	// Initialize the operators map.	
	static {
		
		// String operators
		makeBinOp(EQ, BOOLEAN, STRING, STRING);
		makeBinOp(NE, BOOLEAN, STRING, STRING);
		makeBinOp(GE, BOOLEAN, STRING, STRING);
		makeBinOp(GT, BOOLEAN, STRING, STRING);
		makeBinOp(LE, BOOLEAN, STRING, STRING);
		makeBinOp(LT, BOOLEAN, STRING, STRING);
		
		// Integer comparison
		makeBinOp(EQ, BOOLEAN, INT, INT);
		makeBinOp(NE, BOOLEAN, INT, INT);
		makeBinOp(GE, BOOLEAN, INT, INT);
		makeBinOp(GT, BOOLEAN, INT, INT);
		makeBinOp(LE, BOOLEAN, INT, INT);
		makeBinOp(LT, BOOLEAN, INT, INT);
		
		// Boolean operators 
		makeBinOp(EQ, BOOLEAN, BOOLEAN, BOOLEAN);
		makeBinOp(NE, BOOLEAN, BOOLEAN, BOOLEAN);
		makeBinOp(LOG_AND, BOOLEAN, BOOLEAN, BOOLEAN);
		makeBinOp(LOG_OR, BOOLEAN, BOOLEAN, BOOLEAN);
		makeUnOp(LOG_NOT, BOOLEAN, BOOLEAN);
		
		// Integer arithmetic 
		makeBinOp(ADD, INT, INT, INT);
		makeBinOp(SUB, INT, INT, INT);
		makeBinOp(MUL, INT, INT, INT);
		makeBinOp(DIV, INT, INT, INT);
		makeBinOp(MOD, INT, INT, INT);
		makeBinOp(SHL, INT, INT, INT);
		makeBinOp(SHR, INT, INT, INT);
		makeBinOp(BIT_SHR, INT, INT, INT);
		makeBinOp(BIT_OR, INT, INT, INT);
		makeBinOp(BIT_AND, INT, INT, INT);
		makeBinOp(BIT_XOR, INT, INT, INT);

		makeUnOp(NEG, INT, INT);
		makeUnOp(BIT_NOT, INT, INT);
		
		// "String arithmetic"
		makeBinOp(ADD, STRING, STRING, STRING);
		makeBinOp(ADD, STRING, STRING, INT);
		makeBinOp(ADD, STRING, STRING, BOOLEAN);				
		
		// The type casts 
		makeUnOp(CAST, INT, BOOLEAN);
		makeUnOp(CAST, BOOLEAN, INT);
		
		// And of course the ternary COND operator
		makeOp(COND, INT, new TypeNode[] { BOOLEAN, INT, INT });		
		makeOp(COND, STRING, new TypeNode[] { BOOLEAN, STRING, STRING });		
		makeOp(COND, BOOLEAN, new TypeNode[] { BOOLEAN, BOOLEAN, BOOLEAN });						
	}

	/** 
	 * Get the arity of an operator.
	 * @param id The ID of the operator.
	 * @return The arity of the operator.
	 */
	protected static int getArity(int id) {
		return ((Integer) arities.get(new Integer(id))).intValue(); 
	}
	
	/** 
	 * Get the name of an operator.
	 * @param id ID of the operator.
	 * @return The name of the operator.
	 */
	protected static String getName(int id) {
		return ((String) names.get(new Integer(id)));
	}
	
	/** 
	 * Check, if the given ID is a valid operator ID.
	 * @param id An operator ID.
	 * @return true, if the ID is a valid operator ID, false if not.
	 */
	private static boolean isValidId(int id) {
		return id >= 0 && id < OPERATORS; 
	}
	
	/**
	 * Get the "nearest" operator for a given set of operand types.
	 * This method selects the operator that will provoke the least implicit
	 * type casts when used. 
	 * @param id The operator id.
	 * @param opTypes The operands.
	 * @return The "nearest" operator.
	 */
	protected static OperatorSignature getNearest(int id, TypeNode[] opTypes) {
		Integer oid = new Integer(id);
		OperatorSignature res = null;
		int nearest = Integer.MAX_VALUE;
		
		assert operators.get(oid) != null : "Operator \"" + getName(id) 
		  + "\" must be registered";
		Map resTypeMap = (Map) operators.get(oid);
		
		for(Iterator it = resTypeMap.values().iterator(); it.hasNext();) {
			OperatorSignature op = (OperatorSignature) it.next();
			int dist = op.getDistance(opTypes);
			
			if(dist < nearest) {
				nearest = dist;
				res = op;
			}
		}
		
		return res;
	}

	/** id of the operator. */
	private int id;

	/**
	 * Make a new operator.
	 * This is used exclusively in this class, so it's private.
	 * @param id The operator id.
	 * @param resType The result type of the operator.
	 * @param opTypes The operand types.
	 */
	private OperatorSignature(int id, TypeNode resType, TypeNode[] opTypes) {
		super(resType, opTypes);
		this.id = id;
		
		assert isValidId(id) : "need a valid operator id: " + id; 
	}
}
