/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.executable.OperatorTypeNode;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * Operator description / pseudo-declaration class.
 */
public class OperatorDeclNode extends FunctionOrOperatorDeclBaseNode
{
	public static final int ERROR = 0;
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
	public static final int CONST = 23;
	public static final int COND = 24;
	public static final int IN = 25; // MAP TODO: den operator richtig implementieren, mit typbalancing etc.
	public static final int EXCEPT = 26;
	public static final int SE = 27;

	private static final int OPERATORS = SE + 1;

	private static final OperatorTypeNode operatorType = new OperatorTypeNode();

	/** Arity map of the operators. */
	private static final Map<Integer, Integer> arities = new HashMap<Integer, Integer>();

	/** Name map of the operators. */
	private static final Map<Integer, String> names = new HashMap<Integer, String>();

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
		arities.put(new Integer(CONST), zero);
		arities.put(new Integer(ERROR), zero);
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
		names.put(new Integer(CONST), "Const");
		names.put(new Integer(IN), "In");
		names.put(new Integer(EXCEPT), "Except");
		names.put(new Integer(SE), "Se");
		names.put(new Integer(ERROR), "Error");
	}

	/** Just short forms for less verbose coding. */
	static final TypeNode STRING = BasicTypeNode.stringType;
	static final TypeNode BOOLEAN = BasicTypeNode.booleanType;
	static final TypeNode BYTE = BasicTypeNode.byteType;
	static final TypeNode SHORT = BasicTypeNode.shortType;
	static final TypeNode INT = BasicTypeNode.intType;
	static final TypeNode LONG = BasicTypeNode.longType;
	static final TypeNode FLOAT = BasicTypeNode.floatType;
	static final TypeNode DOUBLE = BasicTypeNode.doubleType;
	static final TypeNode OBJECT = BasicTypeNode.objectType;
	static final TypeNode GRAPH = BasicTypeNode.graphType;
	static final TypeNode NULL = BasicTypeNode.nullType;
	static final TypeNode ENUM = BasicTypeNode.enumItemType;
	static final TypeNode TYPE = BasicTypeNode.typeType;
	static final TypeNode UNTYPED = BasicTypeNode.untypedType;

	/**
	 * Each generic operator is mapped by its ID to a set of concrete operator signatures.
	 */
	private static final Map<Integer, HashSet<OperatorDeclNode>> operators = new HashMap<Integer, HashSet<OperatorDeclNode>>();

	/**
	 * Makes an entry in the {@link #operators} map.
	 *
	 * @param id
	 *            The ID of the operator.
	 * @param resultType
	 *            The result type of the operator.
	 * @param operandTypes
	 *            The operand types of the operator.
	 * @param evaluator
	 *            an Evaluator
	 */
	public static final void makeOp(int id, TypeNode resultType,
			TypeNode[] operandTypes, OperatorEvaluator evaluator)
	{
		Integer operatorId = new Integer(id);

		HashSet<OperatorDeclNode> typeMap = operators.get(operatorId);
		if(typeMap == null) {
			typeMap = new LinkedHashSet<OperatorDeclNode>();
			operators.put(operatorId, typeMap);
		}

		OperatorDeclNode newOpSig = new OperatorDeclNode(id, resultType,
				operandTypes, evaluator);
		typeMap.add(newOpSig);
	}

	/**
	 * Enter a binary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeBinOp(int id, TypeNode resultType,
			TypeNode leftType, TypeNode rightType, OperatorEvaluator evaluator)
	{
		makeOp(id, resultType, new TypeNode[] { leftType, rightType }, evaluator);
	}

	/**
	 * Enter an unary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeUnOp(int id, TypeNode resultType,
			TypeNode operandType, OperatorEvaluator evaluator)
	{
		makeOp(id, resultType, new TypeNode[] { operandType }, evaluator);
	}

	// Initialize the operators map.
	static {
		// String operators
		makeBinOp(EQ, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(NE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(GE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(GT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(LE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(LT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(IN, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);

		// object operators
		makeBinOp(EQ, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);
		makeBinOp(NE, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);

		// null operators
		makeBinOp(EQ, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);
		makeBinOp(NE, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);

		// subgraph operators
		makeBinOp(EQ, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
		makeBinOp(NE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
		makeBinOp(SE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);

		// Integer comparison
		makeBinOp(EQ, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(NE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(GE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(GT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(LE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(LT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);

		// Long comparison
		makeBinOp(EQ, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(NE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(GE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(GT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(LE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(LT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);

		// Float comparison
		makeBinOp(EQ, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(NE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(GE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(GT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(LE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(LT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		// Double comparison
		makeBinOp(EQ, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(NE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(GE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(GT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(LE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(LT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		// Boolean operators
		makeBinOp(LOG_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(LOG_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeUnOp(LOG_NOT, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		makeBinOp(BIT_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(BIT_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(BIT_XOR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		// Boolean comparison
		makeBinOp(EQ, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(NE, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		// Integer arithmetic (byte and short are casted to integer)
		makeBinOp(ADD, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(SUB, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(MUL, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(DIV, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(MOD, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(SHL, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(BIT_SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(BIT_OR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(BIT_AND, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(BIT_XOR, INT, INT, INT, OperatorEvaluator.intEvaluator);

		makeUnOp(NEG, INT, INT, OperatorEvaluator.intEvaluator);
		makeUnOp(BIT_NOT, INT, INT, OperatorEvaluator.intEvaluator);

		// Long arithmetic
		makeBinOp(ADD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(SUB, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(MUL, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(DIV, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(MOD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(SHL, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(BIT_SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(BIT_OR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(BIT_AND, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(BIT_XOR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);

		makeUnOp(NEG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeUnOp(BIT_NOT, LONG, LONG, OperatorEvaluator.longEvaluator);

		// Float arithmetic
		makeBinOp(ADD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(SUB, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(MUL, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(DIV, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(MOD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		makeUnOp(NEG, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		// Double arithmetic
		makeBinOp(ADD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(SUB, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(MUL, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(DIV, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(MOD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		makeUnOp(NEG, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		// "String arithmetic"
		makeBinOp(ADD, STRING, STRING, STRING, OperatorEvaluator.stringEvaluator);

		// Type comparison
		makeBinOp(EQ, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(NE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(GE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(GT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(LE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(LT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);

		// And of course the ternary COND operator
		makeOp(COND, BYTE, new TypeNode[] { BOOLEAN, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, SHORT, new TypeNode[] { BOOLEAN, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, INT, new TypeNode[] { BOOLEAN, INT, INT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, LONG, new TypeNode[] { BOOLEAN, LONG, LONG }, OperatorEvaluator.condEvaluator);
		makeOp(COND, FLOAT, new TypeNode[] { BOOLEAN, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, DOUBLE, new TypeNode[] { BOOLEAN, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, STRING, new TypeNode[] { BOOLEAN, STRING, STRING }, OperatorEvaluator.condEvaluator);
		makeOp(COND, BOOLEAN, new TypeNode[] { BOOLEAN, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
		makeOp(COND, TYPE, new TypeNode[] { BOOLEAN, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, OBJECT, new TypeNode[] { BOOLEAN, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);
		// makeOp(COND, ENUM, new TypeNode[] { BOOLEAN, ENUM, ENUM }, OperatorEvaluator.condEvaluator);

		/////////////////////////////////////////////////////////////////////////////////////////
		// Operators to handle the untyped type that may appear in the sequence expressions due to untyped graph global variables

		// Comparison operators
		makeBinOp(EQ, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(NE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(GE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(GT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(LE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(LT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(IN, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(SE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Boolean (and set) operators
		makeBinOp(LOG_AND, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(LOG_OR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeUnOp(LOG_NOT, BOOLEAN, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeBinOp(BIT_AND, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(BIT_OR, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(BIT_XOR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeBinOp(EXCEPT, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Arithmetic (and string or array/deque concatenation) operators
		makeBinOp(ADD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(SUB, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(MUL, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(DIV, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(MOD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeUnOp(NEG, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Condition operator ?:
		makeOp(COND, BYTE, new TypeNode[] { UNTYPED, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, SHORT, new TypeNode[] { UNTYPED, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, INT, new TypeNode[] { UNTYPED, INT, INT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, LONG, new TypeNode[] { UNTYPED, LONG, LONG }, OperatorEvaluator.condEvaluator);
		makeOp(COND, FLOAT, new TypeNode[] { UNTYPED, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
		makeOp(COND, DOUBLE, new TypeNode[] { UNTYPED, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, STRING, new TypeNode[] { UNTYPED, STRING, STRING }, OperatorEvaluator.condEvaluator);
		makeOp(COND, BOOLEAN, new TypeNode[] { UNTYPED, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
		makeOp(COND, TYPE, new TypeNode[] { UNTYPED, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
		makeOp(COND, OBJECT, new TypeNode[] { UNTYPED, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);

		makeOp(COND, UNTYPED, new TypeNode[] { BOOLEAN, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);

		makeOp(COND, UNTYPED, new TypeNode[] { UNTYPED, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);
	}

	/**
	 * Get the operand types of this operator signature.
	 * @return The operand types.
	 */
	public TypeNode[] getOperandTypes()
	{
		TypeNode[] array = new TypeNode[parameterTypes.size()];
		return parameterTypes.toArray(array);
	}

	/**
	 * Get the arity of an operator.
	 *
	 * @param id
	 *            The ID of the operator.
	 * @return The arity of the operator.
	 */
	public static int getArity(int id)
	{
		return arities.get(new Integer(id)).intValue();
	}

	/**
	 * Get the name of an operator.
	 *
	 * @param id
	 *            ID of the operator.
	 * @return The name of the operator.
	 */
	public static String getName(int id)
	{
		return names.get(new Integer(id));
	}

	/**
	 * Check, if the given ID is a valid operator ID.
	 *
	 * @param id
	 *            An operator ID.
	 * @return true, if the ID is a valid operator ID, false if not.
	 */
	private static boolean isValidId(int id)
	{
		return id >= 0 && id < OPERATORS;
	}

	/**
	 * Get the "nearest" operator for a given set of operand types. This method
	 * selects the operator that will provoke the least implicit type casts when
	 * used.
	 *
	 * @param id
	 *            The operator id.
	 * @param operandTypes
	 *            The operands.
	 * @return The "nearest" operator.
	 */
	public static OperatorDeclNode getNearestOperator(int id, Vector<TypeNode> operandTypes)
	{
		Integer operatorId = new Integer(id);
		OperatorDeclNode resultingOperator = INVALID;
		int nearestDistance = Integer.MAX_VALUE;

		boolean hasVoid = false;
		boolean hasUntyped = false;
		boolean checkEnums = false;
		boolean[] isEnum = null;

		for(int i = 0; i < operandTypes.size(); i++) {
			if(operandTypes.get(i) == BasicTypeNode.voidType)
				hasVoid = true;
			else if(operandTypes.get(i) == BasicTypeNode.untypedType)
				hasUntyped = true;
			else if(operandTypes.get(i) instanceof EnumTypeNode) {
				if(isEnum == null) {
					isEnum = new boolean[operandTypes.size()]; // initialized to false
					checkEnums = true;
				}
				isEnum[i] = true;
			}
		}

		HashSet<OperatorDeclNode> operatorCandidates = operators.get(operatorId);
		if(operatorCandidates == null)
			return INVALID;

		Iterator<OperatorDeclNode> it = operatorCandidates.iterator();
		while(it.hasNext()) {
			OperatorDeclNode operatorCandidate = it.next();
			
			operatorCandidate.resolve();
			
			int distance = operatorCandidate.getDistance(operandTypes);

			String arguments = "";
			for(TypeNode tn : operandTypes) {
				arguments += tn.toString() + ", ";
			}
			debug.report(NOTE, "dist: " + distance + " for signature: " + operatorCandidate + " against " + arguments);

			if(distance == Integer.MAX_VALUE)
				continue;

			if(checkEnums) {
				// Make implicit casts from enum to int for half the price
				distance *= 2;

				TypeNode[] candidateOperandTypes = operatorCandidate.getOperandTypes();
				for(int i = 0; i < operandTypes.size(); i++) {
					if(isEnum[i] && candidateOperandTypes[i] == BasicTypeNode.intType)
						distance--;
				}
			}

			if(distance < nearestDistance) {
				nearestDistance = distance;
				resultingOperator = operatorCandidate;
				if(nearestDistance == 0)
					break;
			}
		}

		// Don't allow "null+a.obj" to be turned into "(string) null + (string) a.obj".
		// But allow "a + b" being enums to be turned into "(int) a + (int) b".
		// Also allow "a == b" being void (abstract attribute) to become "(string) a == (string) b".
		if(!hasVoid && (checkEnums && nearestDistance >= 4 // costs doubled
				|| !checkEnums && nearestDistance >= 2)) {
			resultingOperator = INVALID;
			resultingOperator.resolve();
		}

		// Don't allow untyped to get introduced on type mismatches (one argument untyped -> untyped as result ok)
		if(resultingOperator.getResultType() == BasicTypeNode.untypedType && !hasUntyped) {
			resultingOperator = INVALID;
			resultingOperator.resolve();
		}

		debug.report(NOTE, "selected: " + resultingOperator);

		return resultingOperator;
	}

	/**
	 * An invalid operator signature.
	 */
	private static final OperatorDeclNode INVALID = new OperatorDeclNode(ERROR, BasicTypeNode.errorType,
			new TypeNode[] {}, OperatorEvaluator.emptyEvaluator) {
		@Override
		public boolean isValid()
		{
			return false;
		}
	};

	/** id of the operator. */
	int id;

	/** The evaluator for constant expressions for this operator. */
	private OperatorEvaluator evaluator;

	/**
	 * Make a new operator. This is used exclusively in this class, so it's
	 * private.
	 *
	 * @param id
	 *            The operator id.
	 * @param resultType
	 *            The result type of the operator.
	 * @param operandTypes
	 *            The operand types.
	 * @param evaluator
	 *            The evaluator for this operator signature.
	 */
	private OperatorDeclNode(int id, TypeNode resultType, TypeNode[] operandTypes, OperatorEvaluator evaluator)
	{
		super(new IdentNode(Symbol.Definition.getInvalid()), operatorType);

		this.resultType = resultType;
		this.parameterTypes = new Vector<TypeNode>();
		for(TypeNode operandType : operandTypes) {
			this.parameterTypes.add(operandType);
		}
		
		this.id = id;
		this.evaluator = evaluator;

		assert isValidId(id) : "need a valid operator id: " + id;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean res = resultType.resolve();
		for(TypeNode parameterType : parameterTypes) {
			res &= parameterType.resolve();
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();
		return operatorType;
	}

	/**
	 * Evaluate an expression using this operator signature.
	 *
	 * @param expr
	 *            The expression to be evaluated.
	 * @param arguments
	 *            The arguments for this operator.
	 * @return
	 *            The possibly simplified value of the expression.
	 */
	public ExprNode evaluate(ArithmeticOperatorNode expr, ExprNode[] arguments)
	{
		return evaluator.evaluate(expr, this, arguments);
	}

	/**
	 * Check, if this signature is ok, not bad.
	 *
	 * @return true, if the signature is ok, false, if not.
	 */
	public boolean isValid()
	{
		return true;
	}

	public int getOpId()
	{
		return id;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		String res = getResultType().toString() + " ";
		res += names.get(new Integer(id)) + "(";
		TypeNode[] opTypes = getOperandTypes();
		for(int i = 0; i < opTypes.length; i++) {
			res += (i == 0 ? "" : ",") + opTypes[i];
		}
		res += ")";
		return res;
	}
}
