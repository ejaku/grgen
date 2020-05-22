/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast.type;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.Map;

import de.unika.ipd.grgen.ast.expr.ArithmeticOpNode;
import de.unika.ipd.grgen.ast.expr.BoolConstNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
import de.unika.ipd.grgen.ast.expr.TypeConstNode;
import de.unika.ipd.grgen.ast.expr.TypeofNode;
import de.unika.ipd.grgen.ast.expr.numeric.DoubleConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloatConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.LongConstNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Operator Description class.
 */
public class OperatorSignature extends FunctionSignature
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
	private static final Map<Integer, HashSet<OperatorSignature>> operators = new HashMap<Integer, HashSet<OperatorSignature>>();

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
			TypeNode[] operandTypes, Evaluator evaluator)
	{
		Integer operatorId = new Integer(id);

		HashSet<OperatorSignature> typeMap = operators.get(operatorId);
		if(typeMap == null) {
			typeMap = new LinkedHashSet<OperatorSignature>();
			operators.put(operatorId, typeMap);
		}

		OperatorSignature newOpSig = new OperatorSignature(id, resultType,
				operandTypes, evaluator);
		typeMap.add(newOpSig);
	}

	/**
	 * Enter a binary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeBinOp(int id, TypeNode resultType,
			TypeNode leftType, TypeNode rightType, Evaluator evaluator)
	{
		makeOp(id, resultType, new TypeNode[] { leftType, rightType }, evaluator);
	}

	/**
	 * Enter an unary operator. This is just a convenience function for
	 * {@link #makeOp(int, TypeNode, TypeNode[])}.
	 */
	public static final void makeUnOp(int id, TypeNode resultType,
			TypeNode operandType, Evaluator evaluator)
	{
		makeOp(id, resultType, new TypeNode[] { operandType }, evaluator);
	}

	/**
	 * A class that represents an evaluator for constant expressions.
	 */
	static class Evaluator
	{
		public ExprNode evaluate(ExprNode expr, OperatorSignature operator, ExprNode[] arguments)
		{
			debug.report(NOTE, "id: " + operator.id + ", name: " + names.get(new Integer(operator.id)));

			ExprNode resExpr = expr;
			TypeNode[] paramTypes = operator.getOperandTypes();

			// Check, if the arity matches.
			if(arguments.length == paramTypes.length) {
				// Check the types of the arguments.
				for(int i = 0; i < arguments.length; i++) {
					debug.report(NOTE, "parameter type: " + paramTypes[i]
							+ " argument type: " + arguments[i].getType());
					if(!paramTypes[i].isEqual(arguments[i].getType()))
						return resExpr;
				}

				// If we're here, all checks succeeded.
				try {
					resExpr = eval(expr.getCoords(), operator, arguments);
				} catch(NotEvaluatableException e) {
					debug.report(NOTE, e.toString());
				}
			}

			if(debug.willReport(NOTE)) {
				ConstNode c = (resExpr instanceof ConstNode) ? (ConstNode)resExpr : ConstNode.getInvalid();
				debug.report(NOTE, "result: " + resExpr.getClass() + ", value: " + c.getValue());
			}

			return resExpr;
		}

		/**
		 * NOTE: recalculate the serialVersionUID if you change the class.
		 */
		class NotEvaluatableException extends Exception
		{
			private static final long serialVersionUID = -4866769730405704919L;

			private Coords coords;

			public NotEvaluatableException(Coords coords)
			{
				super();
				this.coords = coords;
			}

			@Override
			public String getMessage()
			{
				return "Expression not evaluatable at " + coords.toString();
			}
		}

		/**
		 * NOTE: recalculate the serialVersionUID if you change the class.
		 */
		class ValueException extends Exception
		{
			private static final long serialVersionUID = 991159946682342406L;

			private Coords coords;

			public ValueException(Coords coords)
			{
				super();
				this.coords = coords;
			}

			@Override
			public String getMessage()
			{
				return "Expression not constant or value has wrong type at " + coords.toString();
			}
		}

		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			return null;
		}

		private Object checkValue(ExprNode e, Class<?> type) throws ValueException
		{
			if(!(e instanceof ConstNode))
				throw new ValueException(e.getCoords());

			Object v = ((ConstNode)e).getValue();
			if(!type.isInstance(v))
				throw new ValueException(e.getCoords());

			return v;
		}

		protected Object getArgValue(ExprNode[] args, OperatorSignature op, int pos) throws ValueException
		{
			TypeNode[] paramTypes = op.getOperandTypes();

			if(paramTypes[pos].isBasic()) {
				BasicTypeNode paramType = (BasicTypeNode)paramTypes[pos];

				return checkValue(args[pos], paramType.getValueType());
			} else
				throw new ValueException(args[pos].getCoords());
		}
	}

	public static final Evaluator objectEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			ObjectTypeNode.Value a0, a1;

			if(getArity(op.getOpId()) != 2)
				throw new NotEvaluatableException(coords);

			try {
				a0 = (ObjectTypeNode.Value)getArgValue(e, op, 0);
				a1 = (ObjectTypeNode.Value)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0.equals(a1));
			case NE:
				return new BoolConstNode(coords, !a0.equals(a1));

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator subgraphEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords);
		}
	};

	private static final Evaluator nullEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{

			if(getArity(op.getOpId()) != 2)
				throw new NotEvaluatableException(coords);

			try {
				getArgValue(e, op, 0);
				getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, true);
			case NE:
				return new BoolConstNode(coords, false);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator stringEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			String a0;
			Object aobj1;

			try {
				a0 = (String)getArgValue(e, op, 0);
				aobj1 = getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			if(op.id == ADD)
				return new StringConstNode(coords, a0 + aobj1);

			String a1 = (String)aobj1;

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0.equals(a1));
			case NE:
				return new BoolConstNode(coords, !a0.equals(a1));
			//case GE:  return new BoolConstNode(coords, a0.compareTo(a1) >= 0);
			//case GT:  return new BoolConstNode(coords, a0.compareTo(a1) > 0);
			//case LE:  return new BoolConstNode(coords, a0.compareTo(a1) <= 0);
			//case LT:  return new BoolConstNode(coords, a0.compareTo(a1) < 0);
			//case IN:  return new BoolConstNode(coords, a1.contains(a0));

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator intEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			int a0, a1;

			try {
				a0 = (Integer)getArgValue(e, op, 0);
				a1 = 0;
				if(getArity(op.getOpId()) > 1)
					a1 = (Integer)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0 == a1);
			case NE:
				return new BoolConstNode(coords, a0 != a1);
			case LT:
				return new BoolConstNode(coords, a0 < a1);
			case LE:
				return new BoolConstNode(coords, a0 <= a1);
			case GT:
				return new BoolConstNode(coords, a0 > a1);
			case GE:
				return new BoolConstNode(coords, a0 >= a1);

			case ADD:
				return new IntConstNode(coords, a0 + a1);
			case SUB:
				return new IntConstNode(coords, a0 - a1);
			case MUL:
				return new IntConstNode(coords, a0 * a1);
			case DIV:
				return new IntConstNode(coords, a0 / a1);
			case MOD:
				return new IntConstNode(coords, a0 % a1);
			case SHL:
				return new IntConstNode(coords, a0 << a1);
			case SHR:
				return new IntConstNode(coords, a0 >> a1);
			case BIT_SHR:
				return new IntConstNode(coords, a0 >>> a1);
			case BIT_OR:
				return new IntConstNode(coords, a0 | a1);
			case BIT_AND:
				return new IntConstNode(coords, a0 & a1);
			case BIT_XOR:
				return new IntConstNode(coords, a0 ^ a1);
			case BIT_NOT:
				return new IntConstNode(coords, ~a0);
			case NEG:
				return new IntConstNode(coords, -a0);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator longEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			long a0, a1;

			try {
				a0 = (Long)getArgValue(e, op, 0);
				a1 = 0;
				if(getArity(op.getOpId()) > 1)
					a1 = (Long)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0 == a1);
			case NE:
				return new BoolConstNode(coords, a0 != a1);
			case LT:
				return new BoolConstNode(coords, a0 < a1);
			case LE:
				return new BoolConstNode(coords, a0 <= a1);
			case GT:
				return new BoolConstNode(coords, a0 > a1);
			case GE:
				return new BoolConstNode(coords, a0 >= a1);

			case ADD:
				return new LongConstNode(coords, a0 + a1);
			case SUB:
				return new LongConstNode(coords, a0 - a1);
			case MUL:
				return new LongConstNode(coords, a0 * a1);
			case DIV:
				return new LongConstNode(coords, a0 / a1);
			case MOD:
				return new LongConstNode(coords, a0 % a1);
			case SHL:
				return new LongConstNode(coords, a0 << a1);
			case SHR:
				return new LongConstNode(coords, a0 >> a1);
			case BIT_SHR:
				return new LongConstNode(coords, a0 >>> a1);
			case BIT_OR:
				return new LongConstNode(coords, a0 | a1);
			case BIT_AND:
				return new LongConstNode(coords, a0 & a1);
			case BIT_XOR:
				return new LongConstNode(coords, a0 ^ a1);
			case BIT_NOT:
				return new LongConstNode(coords, ~a0);
			case NEG:
				return new LongConstNode(coords, -a0);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator floatEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			float a0, a1;

			try {
				a0 = (Float)getArgValue(e, op, 0);
				a1 = 0;
				if(getArity(op.getOpId()) > 1)
					a1 = (Float)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0 == a1);
			case NE:
				return new BoolConstNode(coords, a0 != a1);
			case LT:
				return new BoolConstNode(coords, a0 < a1);
			case LE:
				return new BoolConstNode(coords, a0 <= a1);
			case GT:
				return new BoolConstNode(coords, a0 > a1);
			case GE:
				return new BoolConstNode(coords, a0 >= a1);

			case ADD:
				return new FloatConstNode(coords, a0 + a1);
			case SUB:
				return new FloatConstNode(coords, a0 - a1);
			case MUL:
				return new FloatConstNode(coords, a0 * a1);
			case DIV:
				return new FloatConstNode(coords, a0 / a1);
			case MOD:
				return new FloatConstNode(coords, a0 % a1);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator doubleEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			double a0, a1;

			try {
				a0 = (Double)getArgValue(e, op, 0);
				a1 = 0;
				if(getArity(op.getOpId()) > 1)
					a1 = (Double)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0 == a1);
			case NE:
				return new BoolConstNode(coords, a0 != a1);
			case LT:
				return new BoolConstNode(coords, a0 < a1);
			case LE:
				return new BoolConstNode(coords, a0 <= a1);
			case GT:
				return new BoolConstNode(coords, a0 > a1);
			case GE:
				return new BoolConstNode(coords, a0 >= a1);

			case ADD:
				return new DoubleConstNode(coords, a0 + a1);
			case SUB:
				return new DoubleConstNode(coords, a0 - a1);
			case MUL:
				return new DoubleConstNode(coords, a0 * a1);
			case DIV:
				return new DoubleConstNode(coords, a0 / a1);
			case MOD:
				return new DoubleConstNode(coords, a0 % a1);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	private static final Evaluator typeEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			boolean is_node1, is_node2;

			if(e[0] instanceof TypeConstNode) {
				TypeNode type = (TypeNode)((TypeConstNode)e[0]).getValue();
				is_node1 = type instanceof NodeTypeNode;
			} else if(e[0] instanceof TypeofNode) {
				TypeNode type = ((TypeofNode)e[0]).getEntity().getDeclType();
				is_node1 = type instanceof NodeTypeNode;
			} else
				throw new NotEvaluatableException(coords);

			if(e[1] instanceof TypeConstNode) {
				TypeNode type = (TypeNode)((TypeConstNode)e[1]).getValue();
				is_node2 = type instanceof NodeTypeNode;
			} else if(e[0] instanceof TypeofNode) {
				TypeNode type = ((TypeofNode)e[1]).getEntity().getDeclType();
				is_node2 = type instanceof NodeTypeNode;
			} else
				throw new NotEvaluatableException(coords);

			if(is_node1 != is_node2) {
				error.warning(coords, "comparison between node and edge types will always fail");
				switch(op.id) {
				case EQ:
				case LT:
				case GT:
				case LE:
				case GE:
					return new BoolConstNode(coords, false);

				case NE:
					return new BoolConstNode(coords, true);
				}
			}
			throw new NotEvaluatableException(coords);
		}
	};

	private static final Evaluator booleanEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			boolean a0, a1;

			try {
				a0 = (Boolean)getArgValue(e, op, 0);
				a1 = false;
				if(getArity(op.getOpId()) > 1)
					a1 = (Boolean)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.id) {
			case EQ:
				return new BoolConstNode(coords, a0 == a1);
			case NE:
				return new BoolConstNode(coords, a0 != a1);
			case LOG_AND:
				return new BoolConstNode(coords, a0 && a1);
			case LOG_OR:
				return new BoolConstNode(coords, a0 || a1);
			case LOG_NOT:
				return new BoolConstNode(coords, !a0);
			case BIT_OR:
				return new BoolConstNode(coords, a0 | a1);
			case BIT_AND:
				return new BoolConstNode(coords, a0 & a1);
			case BIT_XOR:
				return new BoolConstNode(coords, a0 ^ a1);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	public static final Evaluator condEvaluator = new Evaluator() {
		public ExprNode evaluate(ExprNode expr, OperatorSignature op, ExprNode[] args)
		{
			try {
				return (Boolean)getArgValue(args, op, 0) ? args[1] : args[2];
			} catch(ValueException x) {
				return expr;
			}
		}
	};

	public static final Evaluator mapEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords); // MAP TODO: evaluate in, map access if map const
		}
	};

	public static final Evaluator setEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			switch(op.id) {
			case IN: {
				if(e[1] instanceof ArithmeticOpNode) {
					ArithmeticOpNode opNode = (ArithmeticOpNode)e[1];
					if(opNode.getOpId() == BIT_AND) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOpNode(set1.getCoords(), IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOpNode(set2.getCoords(), IN, e[0], set2).evaluate();
						return new ArithmeticOpNode(opNode.getCoords(), LOG_AND, in1, in2).evaluate();
					} else if(opNode.getOpId() == BIT_OR) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOpNode(set1.getCoords(), IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOpNode(set2.getCoords(), IN, e[0], set2).evaluate();
						return new ArithmeticOpNode(opNode.getCoords(), LOG_OR, in1, in2).evaluate();
					}
				} else if(e[0] instanceof ConstNode) {
					ConstNode val = (ConstNode)e[0];

					SetInitNode setInit = null;
					if(e[1] instanceof SetInitNode) {
						setInit = (SetInitNode)e[1];
					} else if(e[1] instanceof MemberAccessExprNode) {
						MemberDeclNode member = ((MemberAccessExprNode)e[1]).getDecl();
						if(member.isConst() && member.getConstInitializer() != null)
							setInit = (SetInitNode)member.getConstInitializer();
					}
					if(setInit != null) {
						if(setInit.contains(val))
							return new BoolConstNode(coords, true);
						else if(setInit.isConstant())
							return new BoolConstNode(coords, false);
						// Otherwise not decideable because of non-constant entries in set
					}
				}
				break;
			}
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final Evaluator arrayEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords); // MAP TODO: evaluate
		}
	};

	public static final Evaluator dequeEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords); // MAP TODO: evaluate
		}
	};

	public static final Evaluator untypedEvaluator = new Evaluator() {
		protected ExprNode eval(Coords coords, OperatorSignature op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords);
		}
	};

	private static final Evaluator emptyEvaluator = new Evaluator();

	// Initialize the operators map.
	static {
		// String operators
		makeBinOp(EQ, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(NE, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(GE, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(GT, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(LE, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(LT, BOOLEAN, STRING, STRING, stringEvaluator);
		makeBinOp(IN, BOOLEAN, STRING, STRING, stringEvaluator);

		// object operators
		makeBinOp(EQ, BOOLEAN, OBJECT, OBJECT, objectEvaluator);
		makeBinOp(NE, BOOLEAN, OBJECT, OBJECT, objectEvaluator);

		// null operators
		makeBinOp(EQ, BOOLEAN, NULL, NULL, nullEvaluator);
		makeBinOp(NE, BOOLEAN, NULL, NULL, nullEvaluator);

		// subgraph operators
		makeBinOp(EQ, BOOLEAN, GRAPH, GRAPH, subgraphEvaluator);
		makeBinOp(NE, BOOLEAN, GRAPH, GRAPH, subgraphEvaluator);
		makeBinOp(SE, BOOLEAN, GRAPH, GRAPH, subgraphEvaluator);

		// Integer comparison
		makeBinOp(EQ, BOOLEAN, INT, INT, intEvaluator);
		makeBinOp(NE, BOOLEAN, INT, INT, intEvaluator);
		makeBinOp(GE, BOOLEAN, INT, INT, intEvaluator);
		makeBinOp(GT, BOOLEAN, INT, INT, intEvaluator);
		makeBinOp(LE, BOOLEAN, INT, INT, intEvaluator);
		makeBinOp(LT, BOOLEAN, INT, INT, intEvaluator);

		// Long comparison
		makeBinOp(EQ, BOOLEAN, LONG, LONG, longEvaluator);
		makeBinOp(NE, BOOLEAN, LONG, LONG, longEvaluator);
		makeBinOp(GE, BOOLEAN, LONG, LONG, longEvaluator);
		makeBinOp(GT, BOOLEAN, LONG, LONG, longEvaluator);
		makeBinOp(LE, BOOLEAN, LONG, LONG, longEvaluator);
		makeBinOp(LT, BOOLEAN, LONG, LONG, longEvaluator);

		// Float comparison
		makeBinOp(EQ, BOOLEAN, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(NE, BOOLEAN, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(GE, BOOLEAN, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(GT, BOOLEAN, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(LE, BOOLEAN, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(LT, BOOLEAN, FLOAT, FLOAT, floatEvaluator);

		// Double comparison
		makeBinOp(EQ, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(NE, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(GE, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(GT, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(LE, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(LT, BOOLEAN, DOUBLE, DOUBLE, doubleEvaluator);

		// Boolean operators
		makeBinOp(LOG_AND, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);
		makeBinOp(LOG_OR, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);
		makeUnOp(LOG_NOT, BOOLEAN, BOOLEAN, booleanEvaluator);

		makeBinOp(BIT_AND, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);
		makeBinOp(BIT_OR, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);
		makeBinOp(BIT_XOR, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);

		// Boolean comparison
		makeBinOp(EQ, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);
		makeBinOp(NE, BOOLEAN, BOOLEAN, BOOLEAN, booleanEvaluator);

		// Integer arithmetic (byte and short are casted to integer)
		makeBinOp(ADD, INT, INT, INT, intEvaluator);
		makeBinOp(SUB, INT, INT, INT, intEvaluator);
		makeBinOp(MUL, INT, INT, INT, intEvaluator);
		makeBinOp(DIV, INT, INT, INT, intEvaluator);
		makeBinOp(MOD, INT, INT, INT, intEvaluator);
		makeBinOp(SHL, INT, INT, INT, intEvaluator);
		makeBinOp(SHR, INT, INT, INT, intEvaluator);
		makeBinOp(BIT_SHR, INT, INT, INT, intEvaluator);
		makeBinOp(BIT_OR, INT, INT, INT, intEvaluator);
		makeBinOp(BIT_AND, INT, INT, INT, intEvaluator);
		makeBinOp(BIT_XOR, INT, INT, INT, intEvaluator);

		makeUnOp(NEG, INT, INT, intEvaluator);
		makeUnOp(BIT_NOT, INT, INT, intEvaluator);

		// Long arithmetic
		makeBinOp(ADD, LONG, LONG, LONG, longEvaluator);
		makeBinOp(SUB, LONG, LONG, LONG, longEvaluator);
		makeBinOp(MUL, LONG, LONG, LONG, longEvaluator);
		makeBinOp(DIV, LONG, LONG, LONG, longEvaluator);
		makeBinOp(MOD, LONG, LONG, LONG, longEvaluator);
		makeBinOp(SHL, LONG, LONG, INT, longEvaluator);
		makeBinOp(SHR, LONG, LONG, INT, longEvaluator);
		makeBinOp(BIT_SHR, LONG, LONG, INT, longEvaluator);
		makeBinOp(BIT_OR, LONG, LONG, LONG, longEvaluator);
		makeBinOp(BIT_AND, LONG, LONG, LONG, longEvaluator);
		makeBinOp(BIT_XOR, LONG, LONG, LONG, longEvaluator);

		makeUnOp(NEG, LONG, LONG, longEvaluator);
		makeUnOp(BIT_NOT, LONG, LONG, longEvaluator);

		// Float arithmetic
		makeBinOp(ADD, FLOAT, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(SUB, FLOAT, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(MUL, FLOAT, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(DIV, FLOAT, FLOAT, FLOAT, floatEvaluator);
		makeBinOp(MOD, FLOAT, FLOAT, FLOAT, floatEvaluator);

		makeUnOp(NEG, FLOAT, FLOAT, floatEvaluator);

		// Double arithmetic
		makeBinOp(ADD, DOUBLE, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(SUB, DOUBLE, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(MUL, DOUBLE, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(DIV, DOUBLE, DOUBLE, DOUBLE, doubleEvaluator);
		makeBinOp(MOD, DOUBLE, DOUBLE, DOUBLE, doubleEvaluator);

		makeUnOp(NEG, DOUBLE, DOUBLE, doubleEvaluator);

		// "String arithmetic"
		makeBinOp(ADD, STRING, STRING, STRING, stringEvaluator);

		// Type comparison
		makeBinOp(EQ, BOOLEAN, TYPE, TYPE, typeEvaluator);
		makeBinOp(NE, BOOLEAN, TYPE, TYPE, typeEvaluator);
		makeBinOp(GE, BOOLEAN, TYPE, TYPE, typeEvaluator);
		makeBinOp(GT, BOOLEAN, TYPE, TYPE, typeEvaluator);
		makeBinOp(LE, BOOLEAN, TYPE, TYPE, typeEvaluator);
		makeBinOp(LT, BOOLEAN, TYPE, TYPE, typeEvaluator);

		// And of course the ternary COND operator
		makeOp(COND, BYTE, new TypeNode[] { BOOLEAN, BYTE, BYTE }, condEvaluator);
		makeOp(COND, SHORT, new TypeNode[] { BOOLEAN, SHORT, SHORT }, condEvaluator);
		makeOp(COND, INT, new TypeNode[] { BOOLEAN, INT, INT }, condEvaluator);
		makeOp(COND, LONG, new TypeNode[] { BOOLEAN, LONG, LONG }, condEvaluator);
		makeOp(COND, FLOAT, new TypeNode[] { BOOLEAN, FLOAT, FLOAT }, condEvaluator);
		makeOp(COND, DOUBLE, new TypeNode[] { BOOLEAN, DOUBLE, DOUBLE }, condEvaluator);
		makeOp(COND, STRING, new TypeNode[] { BOOLEAN, STRING, STRING }, condEvaluator);
		makeOp(COND, BOOLEAN, new TypeNode[] { BOOLEAN, BOOLEAN, BOOLEAN }, condEvaluator);
		makeOp(COND, TYPE, new TypeNode[] { BOOLEAN, TYPE, TYPE }, condEvaluator);
		makeOp(COND, OBJECT, new TypeNode[] { BOOLEAN, OBJECT, OBJECT }, condEvaluator);
		// makeOp(COND, ENUM, new TypeNode[] { BOOLEAN, ENUM, ENUM }, condEvaluator);

		/////////////////////////////////////////////////////////////////////////////////////////
		// Operators to handle the untyped type that may appear in the sequence expressions due to untyped graph global variables

		// Comparison operators
		makeBinOp(EQ, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(NE, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(GE, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(GT, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(LE, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(LT, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(IN, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(SE, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);

		// Boolean (and set) operators
		makeBinOp(LOG_AND, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(LOG_OR, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);
		makeUnOp(LOG_NOT, BOOLEAN, UNTYPED, untypedEvaluator);

		makeBinOp(BIT_AND, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(BIT_OR, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(BIT_XOR, BOOLEAN, UNTYPED, UNTYPED, untypedEvaluator);

		makeBinOp(EXCEPT, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);

		// Arithmetic (and string or array/deque concatenation) operators
		makeBinOp(ADD, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(SUB, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(MUL, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(DIV, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);
		makeBinOp(MOD, UNTYPED, UNTYPED, UNTYPED, untypedEvaluator);

		makeUnOp(NEG, UNTYPED, UNTYPED, untypedEvaluator);

		// Condition operator ?:
		makeOp(COND, BYTE, new TypeNode[] { UNTYPED, BYTE, BYTE }, condEvaluator);
		makeOp(COND, SHORT, new TypeNode[] { UNTYPED, SHORT, SHORT }, condEvaluator);
		makeOp(COND, INT, new TypeNode[] { UNTYPED, INT, INT }, condEvaluator);
		makeOp(COND, LONG, new TypeNode[] { UNTYPED, LONG, LONG }, condEvaluator);
		makeOp(COND, FLOAT, new TypeNode[] { UNTYPED, FLOAT, FLOAT }, condEvaluator);
		makeOp(COND, DOUBLE, new TypeNode[] { UNTYPED, DOUBLE, DOUBLE }, condEvaluator);
		makeOp(COND, STRING, new TypeNode[] { UNTYPED, STRING, STRING }, condEvaluator);
		makeOp(COND, BOOLEAN, new TypeNode[] { UNTYPED, BOOLEAN, BOOLEAN }, condEvaluator);
		makeOp(COND, TYPE, new TypeNode[] { UNTYPED, TYPE, TYPE }, condEvaluator);
		makeOp(COND, OBJECT, new TypeNode[] { UNTYPED, OBJECT, OBJECT }, condEvaluator);

		makeOp(COND, UNTYPED, new TypeNode[] { BOOLEAN, UNTYPED, UNTYPED }, untypedEvaluator);

		makeOp(COND, UNTYPED, new TypeNode[] { UNTYPED, UNTYPED, UNTYPED }, untypedEvaluator);
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
	public static OperatorSignature getNearestOperator(int id, TypeNode[] operandTypes)
	{
		Integer operatorId = new Integer(id);
		OperatorSignature resultingOperator = INVALID;
		int nearestDistance = Integer.MAX_VALUE;

		boolean hasVoid = false;
		boolean hasUntyped = false;
		boolean checkEnums = false;
		boolean[] isEnum = null;

		for(int i = 0; i < operandTypes.length; i++) {
			if(operandTypes[i] == BasicTypeNode.voidType)
				hasVoid = true;
			else if(operandTypes[i] == BasicTypeNode.untypedType)
				hasUntyped = true;
			else if(operandTypes[i] instanceof EnumTypeNode) {
				if(isEnum == null) {
					isEnum = new boolean[operandTypes.length]; // initialized to false
					checkEnums = true;
				}
				isEnum[i] = true;
			}
		}

		HashSet<OperatorSignature> operatorCandidates = operators.get(operatorId);
		if(operatorCandidates == null)
			return INVALID;

		Iterator<OperatorSignature> it = operatorCandidates.iterator();
		while(it.hasNext()) {
			OperatorSignature operatorCandidate = it.next();
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
				for(int i = 0; i < operandTypes.length; i++) {
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
		}

		// Don't allow untyped to get introduced on type mismatches (one argument untyped -> untyped as result ok)
		if(resultingOperator.getResultType() == BasicTypeNode.untypedType && !hasUntyped) {
			resultingOperator = INVALID;
		}

		debug.report(NOTE, "selected: " + resultingOperator);

		return resultingOperator;
	}

	/**
	 * An invalid operator signature.
	 */
	private static final OperatorSignature INVALID = new OperatorSignature(ERROR, BasicTypeNode.errorType,
			new TypeNode[] {}, emptyEvaluator) {
		public boolean isValid()
		{
			return false;
		}
	};

	/** id of the operator. */
	private int id;

	/** The evaluator for constant expressions for this operator. */
	private Evaluator evaluator;

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
	private OperatorSignature(int id, TypeNode resultType, TypeNode[] operandTypes, Evaluator evaluator)
	{
		super(resultType, operandTypes);
		this.id = id;
		this.evaluator = evaluator;

		assert isValidId(id) : "need a valid operator id: " + id;
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
	public ExprNode evaluate(ArithmeticOpNode expr, ExprNode[] arguments)
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
