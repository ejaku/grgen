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
	public enum Operator
	{
		ERROR,
		LOG_OR,
		LOG_AND,
		BIT_OR,
		BIT_XOR,
		BIT_AND,
		EQ,
		NE,
		LT,
		LE,
		GT,
		GE,
		SHL,
		SHR,
		BIT_SHR,
		ADD,
		SUB,
		MUL,
		DIV,
		MOD,
		LOG_NOT,
		BIT_NOT,
		NEG,
		CONST,
		COND,
		IN,
		INDEX,
		EXCEPT,
		SE
	}

	private static final OperatorTypeNode operatorType = new OperatorTypeNode();

	/** Arity map of the operators. */
	private static final Map<Operator, Integer> arities = new HashMap<Operator, Integer>();

	/** Name map of the operators. */
	private static final Map<Operator, String> names = new HashMap<Operator, String>();

	static {
		Integer two = new Integer(2);
		Integer one = new Integer(1);
		Integer zero = new Integer(0);

		for(Operator op : Operator.values())
			arities.put(op, two);

		arities.put(Operator.COND, new Integer(3));
		arities.put(Operator.LOG_NOT, one);
		arities.put(Operator.BIT_NOT, one);
		arities.put(Operator.NEG, one);
		arities.put(Operator.CONST, zero);
		arities.put(Operator.ERROR, zero);
	}

	static {
		names.put(Operator.COND, "Cond");
		names.put(Operator.LOG_OR, "LogOr");
		names.put(Operator.LOG_AND, "LogAnd");
		names.put(Operator.BIT_XOR, "BitXor");
		names.put(Operator.BIT_OR, "BitOr");
		names.put(Operator.BIT_AND, "BitAnd");
		names.put(Operator.EQ, "Eq");
		names.put(Operator.NE, "Ne");
		names.put(Operator.LT, "Lt");
		names.put(Operator.LE, "Le");
		names.put(Operator.GT, "Gt");
		names.put(Operator.GE, "Ge");
		names.put(Operator.SHL, "Shl");
		names.put(Operator.SHR, "Shr");
		names.put(Operator.BIT_SHR, "BitShr");
		names.put(Operator.ADD, "Add");
		names.put(Operator.SUB, "Sub");
		names.put(Operator.MUL, "Mul");
		names.put(Operator.DIV, "Div");
		names.put(Operator.MOD, "Mod");
		names.put(Operator.LOG_NOT, "LogNot");
		names.put(Operator.BIT_NOT, "BitNot");
		names.put(Operator.NEG, "Neg");
		names.put(Operator.CONST, "Const");
		names.put(Operator.IN, "In");
		names.put(Operator.INDEX, "IndexedAccess");
		names.put(Operator.EXCEPT, "Except");
		names.put(Operator.SE, "Se");
		names.put(Operator.ERROR, "Error");
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
	private static final Map<Operator, HashSet<OperatorDeclNode>> operators =
			new HashMap<Operator, HashSet<OperatorDeclNode>>();

	/**
	 * Makes an entry in the {@link #operators} map.
	 *
	 * @param operator The operator.
	 * @param resultType The result type of the operator.
	 * @param operandTypes The operand types of the operator.
	 * @param evaluator an Evaluator
	 */
	public static final void makeOp(Operator operator, TypeNode resultType,
			TypeNode[] operandTypes, OperatorEvaluator evaluator)
	{
		HashSet<OperatorDeclNode> typeMap = operators.get(operator);
		if(typeMap == null) {
			typeMap = new LinkedHashSet<OperatorDeclNode>();
			operators.put(operator, typeMap);
		}

		OperatorDeclNode newOpSig = new OperatorDeclNode(operator, resultType,
				operandTypes, evaluator);
		typeMap.add(newOpSig);
	}

	/**
	 * Enter a binary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeBinOp(Operator operator, TypeNode resultType,
			TypeNode leftType, TypeNode rightType, OperatorEvaluator evaluator)
	{
		makeOp(operator, resultType, new TypeNode[] { leftType, rightType }, evaluator);
	}

	/**
	 * Enter an unary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeUnOp(Operator operator, TypeNode resultType,
			TypeNode operandType, OperatorEvaluator evaluator)
	{
		makeOp(operator, resultType, new TypeNode[] { operandType }, evaluator);
	}

	// Initialize the operators map.
	static {
		// String operators
		makeBinOp(Operator.EQ, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
		makeBinOp(Operator.IN, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);

		// object operators
		makeBinOp(Operator.EQ, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);

		// null operators
		makeBinOp(Operator.EQ, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);

		// subgraph operators
		makeBinOp(Operator.EQ, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
		makeBinOp(Operator.SE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);

		// Integer comparison
		makeBinOp(Operator.EQ, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);

		// Long comparison
		makeBinOp(Operator.EQ, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);

		// Float comparison
		makeBinOp(Operator.EQ, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		// Double comparison
		makeBinOp(Operator.EQ, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		// Boolean operators
		makeBinOp(Operator.LOG_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(Operator.LOG_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeUnOp(Operator.LOG_NOT, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		makeBinOp(Operator.BIT_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(Operator.BIT_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(Operator.BIT_XOR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		// Boolean comparison
		makeBinOp(Operator.EQ, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

		// Integer arithmetic (byte and short are casted to integer)
		makeBinOp(Operator.ADD, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.SUB, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.MUL, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.DIV, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.MOD, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.SHL, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.BIT_SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.BIT_OR, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.BIT_AND, INT, INT, INT, OperatorEvaluator.intEvaluator);
		makeBinOp(Operator.BIT_XOR, INT, INT, INT, OperatorEvaluator.intEvaluator);

		makeUnOp(Operator.NEG, INT, INT, OperatorEvaluator.intEvaluator);
		makeUnOp(Operator.BIT_NOT, INT, INT, OperatorEvaluator.intEvaluator);

		// Long arithmetic
		makeBinOp(Operator.ADD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.SUB, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.MUL, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.DIV, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.MOD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.SHL, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.BIT_SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.BIT_OR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.BIT_AND, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeBinOp(Operator.BIT_XOR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);

		makeUnOp(Operator.NEG, LONG, LONG, OperatorEvaluator.longEvaluator);
		makeUnOp(Operator.BIT_NOT, LONG, LONG, OperatorEvaluator.longEvaluator);

		// Float arithmetic
		makeBinOp(Operator.ADD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.SUB, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.MUL, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.DIV, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
		makeBinOp(Operator.MOD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		makeUnOp(Operator.NEG, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

		// Double arithmetic
		makeBinOp(Operator.ADD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.SUB, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.MUL, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.DIV, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
		makeBinOp(Operator.MOD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		makeUnOp(Operator.NEG, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

		// "String arithmetic"
		makeBinOp(Operator.ADD, STRING, STRING, STRING, OperatorEvaluator.stringEvaluator);

		// Type comparison
		makeBinOp(Operator.EQ, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);

		// And of course the ternary COND operator
		makeOp(Operator.COND, BYTE, new TypeNode[] { BOOLEAN, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, SHORT, new TypeNode[] { BOOLEAN, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, INT, new TypeNode[] { BOOLEAN, INT, INT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, LONG, new TypeNode[] { BOOLEAN, LONG, LONG }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, FLOAT, new TypeNode[] { BOOLEAN, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, DOUBLE, new TypeNode[] { BOOLEAN, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, STRING, new TypeNode[] { BOOLEAN, STRING, STRING }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, BOOLEAN, new TypeNode[] { BOOLEAN, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, TYPE, new TypeNode[] { BOOLEAN, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, OBJECT, new TypeNode[] { BOOLEAN, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);
		// makeOp(Operator.COND, ENUM, new TypeNode[] { BOOLEAN, ENUM, ENUM }, OperatorEvaluator.condEvaluator);

		/////////////////////////////////////////////////////////////////////////////////////////
		// Operators to handle the untyped type that may appear in the sequence expressions due to untyped graph global variables

		// Comparison operators
		makeBinOp(Operator.EQ, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.NE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.GE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.GT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.LE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.LT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.IN, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.SE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Boolean (and set) operators
		makeBinOp(Operator.LOG_AND, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.LOG_OR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeUnOp(Operator.LOG_NOT, BOOLEAN, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeBinOp(Operator.BIT_AND, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.BIT_OR, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.BIT_XOR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeBinOp(Operator.EXCEPT, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Arithmetic (and string or array/deque concatenation) operators
		makeBinOp(Operator.ADD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.SUB, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.MUL, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.DIV, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		makeBinOp(Operator.MOD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		makeUnOp(Operator.NEG, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

		// Condition operator ?:
		makeOp(Operator.COND, BYTE, new TypeNode[] { UNTYPED, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, SHORT, new TypeNode[] { UNTYPED, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, INT, new TypeNode[] { UNTYPED, INT, INT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, LONG, new TypeNode[] { UNTYPED, LONG, LONG }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, FLOAT, new TypeNode[] { UNTYPED, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, DOUBLE, new TypeNode[] { UNTYPED, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, STRING, new TypeNode[] { UNTYPED, STRING, STRING }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, BOOLEAN, new TypeNode[] { UNTYPED, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, TYPE, new TypeNode[] { UNTYPED, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
		makeOp(Operator.COND, OBJECT, new TypeNode[] { UNTYPED, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);

		makeOp(Operator.COND, UNTYPED, new TypeNode[] { BOOLEAN, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);

		makeOp(Operator.COND, UNTYPED, new TypeNode[] { UNTYPED, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);
		
		// Indexed access (of map, array, deque) operators
		makeBinOp(Operator.INDEX, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
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
	 * @param operator The operator.
	 * @return The arity of the operator.
	 */
	public static int getArity(Operator operator)
	{
		return arities.get(operator).intValue();
	}

	/**
	 * Get the name of an operator.
	 *
	 * @param id ID of the operator.
	 * @return The name of the operator.
	 */
	public static String getName(Operator operator)
	{
		return names.get(operator);
	}

	/**
	 * Get the "nearest" operator for a given set of operand types. This method
	 * selects the operator that will provoke the least implicit type casts when
	 * used.
	 *
	 * @param operator The operator.
	 * @param operandTypes The operands.
	 * @return The declaration of the "nearest" operator.
	 */
	public static OperatorDeclNode getNearestOperator(Operator operator, Vector<TypeNode> operandTypes)
	{
		OperatorDeclNode resultingOperator = INVALID;
		int nearestDistance = Integer.MAX_VALUE;

		boolean hasVoid = false;
		boolean hasUntyped = false;
		boolean checkEnums = false;
		boolean[] isEnum = new boolean[operandTypes.size()]; // initialized to false

		for(int i = 0; i < operandTypes.size(); i++) {
			if(operandTypes.get(i) == BasicTypeNode.voidType)
				hasVoid = true;
			else if(operandTypes.get(i) == BasicTypeNode.untypedType)
				hasUntyped = true;
			else if(operandTypes.get(i) instanceof EnumTypeNode) {
				checkEnums = true;
				isEnum[i] = true;
			}
		}

		HashSet<OperatorDeclNode> operatorCandidates = operators.get(operator);
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
	private static final OperatorDeclNode INVALID = new OperatorDeclNode(Operator.ERROR, BasicTypeNode.errorType,
			new TypeNode[] {}, OperatorEvaluator.emptyEvaluator) {
		@Override
		public boolean isValid()
		{
			return false;
		}
	};

	/** id of the operator. */
	Operator operator;

	/** The evaluator for constant expressions for this operator. */
	private OperatorEvaluator evaluator;

	/**
	 * Make a new operator. This is used exclusively in this class, so it's
	 * private.
	 *
	 * @param operator The operator.
	 * @param resultType The result type of the operator.
	 * @param operandTypes The operand types.
	 * @param evaluator The evaluator for this operator signature.
	 */
	private OperatorDeclNode(Operator operator, TypeNode resultType, TypeNode[] operandTypes,
			OperatorEvaluator evaluator)
	{
		super(new IdentNode(Symbol.Definition.getInvalid()), operatorType);

		this.resultType = resultType;
		this.parameterTypes = new Vector<TypeNode>();
		for(TypeNode operandType : operandTypes) {
			this.parameterTypes.add(operandType);
		}
		
		this.operator = operator;
		this.evaluator = evaluator;
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
	 * @param expr The expression to be evaluated.
	 * @param arguments The arguments for this operator.
	 * @return The possibly simplified value of the expression.
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

	public Operator getOperator()
	{
		return operator;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		String res = getResultType().toString() + " ";
		res += names.get(operator) + "(";
		TypeNode[] opTypes = getOperandTypes();
		for(int i = 0; i < opTypes.length; i++) {
			res += (i == 0 ? "" : ",") + opTypes[i];
		}
		res += ")";
		return res;
	}
}
