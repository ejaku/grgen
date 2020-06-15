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

import de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
import de.unika.ipd.grgen.ast.expr.BoolConstNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
import de.unika.ipd.grgen.ast.expr.TypeConstNode;
import de.unika.ipd.grgen.ast.expr.TypeofNode;
import de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
import de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.numeric.DoubleConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloatConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.LongConstNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Base;

/**
 * A class that represents an evaluator for constant expressions.
 */
public class OperatorEvaluator
{
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

	public ExprNode evaluate(ExprNode expr, OperatorDeclNode operator, ExprNode[] arguments)
	{
		Base.debug.report(Base.NOTE, "id: " + operator.getOperator() + ", name: " + OperatorDeclNode.getName(operator.getOperator()));

		ExprNode resExpr = expr;
		TypeNode[] paramTypes = operator.getOperandTypes();

		// Check, if the arity matches.
		if(arguments.length == paramTypes.length) {
			// Check the types of the arguments.
			for(int i = 0; i < arguments.length; i++) {
				Base.debug.report(Base.NOTE, "parameter type: " + paramTypes[i]
						+ " argument type: " + arguments[i].getType());
				if(!paramTypes[i].isEqual(arguments[i].getType()))
					return resExpr;
			}

			// If we're here, all checks succeeded.
			try {
				resExpr = eval(expr.getCoords(), operator, arguments);
			} catch(NotEvaluatableException e) {
				Base.debug.report(Base.NOTE, e.toString());
			}
		}

		if(Base.debug.willReport(Base.NOTE)) {
			ConstNode c = (resExpr instanceof ConstNode) ? (ConstNode)resExpr : ConstNode.getInvalid();
			Base.debug.report(Base.NOTE, "result: " + resExpr.getClass() + ", value: " + c.getValue());
		}

		return resExpr;
	}

	protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
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

	protected Object getArgValue(ExprNode[] args, OperatorDeclNode op, int pos) throws ValueException
	{
		TypeNode[] paramTypes = op.getOperandTypes();

		if(paramTypes[pos].isBasic()) {
			BasicTypeNode paramType = (BasicTypeNode)paramTypes[pos];

			return checkValue(args[pos], paramType.getValueType());
		} else
			throw new ValueException(args[pos].getCoords());
	}

	public static final OperatorEvaluator objectEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			ObjectTypeNode.Value a0, a1;

			if(OperatorDeclNode.getArity(op.getOperator()) != 2)
				throw new NotEvaluatableException(coords);

			try {
				a0 = (ObjectTypeNode.Value)getArgValue(e, op, 0);
				a1 = (ObjectTypeNode.Value)getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
			case EQ:
				return new BoolConstNode(coords, a0.equals(a1));
			case NE:
				return new BoolConstNode(coords, !a0.equals(a1));

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	public static final OperatorEvaluator subgraphEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator nullEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			if(OperatorDeclNode.getArity(op.getOperator()) != 2)
				throw new NotEvaluatableException(coords);

			try {
				getArgValue(e, op, 0);
				getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
			case EQ:
				return new BoolConstNode(coords, true);
			case NE:
				return new BoolConstNode(coords, false);

			default:
				throw new NotEvaluatableException(coords);
			}
		}
	};

	public static final OperatorEvaluator stringEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			String a0;
			Object aobj1;

			try {
				a0 = (String)getArgValue(e, op, 0);
				aobj1 = getArgValue(e, op, 1);
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			if(op.getOperator() == OperatorDeclNode.Operator.ADD)
				return new StringConstNode(coords, a0 + aobj1);

			String a1 = (String)aobj1;

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator intEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			int a0, a1;

			try {
				a0 = ((Integer)getArgValue(e, op, 0)).intValue();
				a1 = 0;
				if(OperatorDeclNode.getArity(op.getOperator()) > 1)
					a1 = ((Integer)getArgValue(e, op, 1)).intValue();
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator longEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			long a0, a1;

			try {
				a0 = ((Long)getArgValue(e, op, 0)).longValue();
				a1 = 0;
				if(OperatorDeclNode.getArity(op.getOperator()) > 1)
					a1 = ((Long)getArgValue(e, op, 1)).longValue();
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator floatEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			float a0, a1;

			try {
				a0 = ((Float)getArgValue(e, op, 0)).floatValue();
				a1 = 0;
				if(OperatorDeclNode.getArity(op.getOperator()) > 1)
					a1 = ((Float)getArgValue(e, op, 1)).floatValue();
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator doubleEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			double a0, a1;

			try {
				a0 = ((Double)getArgValue(e, op, 0)).doubleValue();
				a1 = 0;
				if(OperatorDeclNode.getArity(op.getOperator()) > 1)
					a1 = ((Double)getArgValue(e, op, 1)).doubleValue();
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator typeEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
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
				Base.error.warning(coords, "comparison between node and edge types will always fail");
				switch(op.getOperator()) {
				case EQ:
				case LT:
				case GT:
				case LE:
				case GE:
					return new BoolConstNode(coords, false);
				case NE:
					return new BoolConstNode(coords, true);

				default:
					break;
				}
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator booleanEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			boolean a0, a1;

			try {
				a0 = ((Boolean)getArgValue(e, op, 0)).booleanValue();
				a1 = false;
				if(OperatorDeclNode.getArity(op.getOperator()) > 1)
					a1 = ((Boolean)getArgValue(e, op, 1)).booleanValue();
			} catch(ValueException x) {
				throw new NotEvaluatableException(coords);
			}

			switch(op.getOperator()) {
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

	public static final OperatorEvaluator condEvaluator = new OperatorEvaluator() {
		@Override
		public ExprNode evaluate(ExprNode expr, OperatorDeclNode op, ExprNode[] args)
		{
			try {
				return ((Boolean)getArgValue(args, op, 0)).booleanValue() ? args[1] : args[2];
			} catch(ValueException x) {
				return expr;
			}
		}
	};

	public static final OperatorEvaluator mapEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			switch(op.getOperator()) {
			case IN:
			{
				if(e[1] instanceof ArithmeticOperatorNode) {
					ArithmeticOperatorNode opNode = (ArithmeticOperatorNode)e[1];
					if(opNode.getOperator() == OperatorDeclNode.Operator.BIT_AND) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOperatorNode(set1.getCoords(), OperatorDeclNode.Operator.IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOperatorNode(set2.getCoords(), OperatorDeclNode.Operator.IN, e[0], set2).evaluate();
						return new ArithmeticOperatorNode(opNode.getCoords(), OperatorDeclNode.Operator.LOG_AND, in1, in2).evaluate();
					} else if(opNode.getOperator() == OperatorDeclNode.Operator.BIT_OR) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOperatorNode(set1.getCoords(), OperatorDeclNode.Operator.IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOperatorNode(set2.getCoords(), OperatorDeclNode.Operator.IN, e[0], set2).evaluate();
						return new ArithmeticOperatorNode(opNode.getCoords(), OperatorDeclNode.Operator.LOG_OR, in1, in2).evaluate();
					}
				} else if(e[0] instanceof ConstNode) {
					ConstNode val = (ConstNode)e[0];

					MapInitNode mapInit = null;
					if(e[1] instanceof MapInitNode) {
						mapInit = (MapInitNode)e[1];
					} else if(e[1] instanceof MemberAccessExprNode) {
						MemberDeclNode member = ((MemberAccessExprNode)e[1]).getDecl();
						if(member.isConst() && member.getConstInitializer() != null)
							mapInit = (MapInitNode)member.getConstInitializer();
					}
					if(mapInit != null) {
						if(mapInit.contains(val))
							return new BoolConstNode(coords, true);
						else if(mapInit.areKeysConstant())
							return new BoolConstNode(coords, false);
						// Otherwise not decideable because of non-constant entries in map keys
					}
				}
				break;
			}
			case INDEX:
			{
				if(e[1] instanceof ConstNode) {
					ConstNode key = (ConstNode)e[1];

					MapInitNode mapInit = null;
					if(e[0] instanceof MapInitNode) {
						mapInit = (MapInitNode)e[0];
					} else if(e[0] instanceof MemberAccessExprNode) {
						MemberDeclNode member = ((MemberAccessExprNode)e[0]).getDecl();
						if(member.isConst() && member.getConstInitializer() != null)
							mapInit = (MapInitNode)member.getConstInitializer();
					}
					if(mapInit != null) {
						ExprNode val = mapInit.getAtIndex(key);
						if(mapInit.isConstant() && val instanceof ConstNode)
							return val;
					}
				}
				break;
			}
			default:
				break;
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator setEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			switch(op.getOperator()) {
			case IN:
			{
				if(e[1] instanceof ArithmeticOperatorNode) {
					ArithmeticOperatorNode opNode = (ArithmeticOperatorNode)e[1];
					if(opNode.getOperator() == OperatorDeclNode.Operator.BIT_AND) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOperatorNode(set1.getCoords(), OperatorDeclNode.Operator.IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOperatorNode(set2.getCoords(), OperatorDeclNode.Operator.IN, e[0], set2).evaluate();
						return new ArithmeticOperatorNode(opNode.getCoords(), OperatorDeclNode.Operator.LOG_AND, in1, in2).evaluate();
					} else if(opNode.getOperator() == OperatorDeclNode.Operator.BIT_OR) {
						ExprNode set1 = opNode.children.get(0);
						ExprNode set2 = opNode.children.get(1);
						ExprNode in1 = new ArithmeticOperatorNode(set1.getCoords(), OperatorDeclNode.Operator.IN, e[0], set1).evaluate();
						ExprNode in2 = new ArithmeticOperatorNode(set2.getCoords(), OperatorDeclNode.Operator.IN, e[0], set2).evaluate();
						return new ArithmeticOperatorNode(opNode.getCoords(), OperatorDeclNode.Operator.LOG_OR, in1, in2).evaluate();
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
			default:
				break;
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator arrayEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			switch(op.getOperator()) {
			case INDEX:
			{
				if(e[1] instanceof ConstNode) {
					ConstNode index = (ConstNode)e[1];

					ArrayInitNode arrayInit = null;
					if(e[0] instanceof ArrayInitNode) {
						arrayInit = (ArrayInitNode)e[0];
					} else if(e[0] instanceof MemberAccessExprNode) {
						MemberDeclNode member = ((MemberAccessExprNode)e[0]).getDecl();
						if(member.isConst() && member.getConstInitializer() != null)
							arrayInit = (ArrayInitNode)member.getConstInitializer();
					}
					if(arrayInit != null) {
						ExprNode val = arrayInit.getAtIndex(index);
						if(val instanceof ConstNode)
							return val;
					}
				}
				break;
			}
			default:
				break;
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator dequeEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			switch(op.getOperator()) {
			case INDEX:
			{
				if(e[1] instanceof ConstNode) {
					ConstNode index = (ConstNode)e[1];

					DequeInitNode dequeInit = null;
					if(e[0] instanceof DequeInitNode) {
						dequeInit = (DequeInitNode)e[0];
					} else if(e[0] instanceof MemberAccessExprNode) {
						MemberDeclNode member = ((MemberAccessExprNode)e[0]).getDecl();
						if(member.isConst() && member.getConstInitializer() != null)
							dequeInit = (DequeInitNode)member.getConstInitializer();
					}
					if(dequeInit != null) {
						ExprNode val = dequeInit.getAtIndex(index);
						if(val instanceof ConstNode)
							return val;
					}
				}
				break;
			}
			default:
				break;
			}
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator untypedEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords);
		}
	};

	public static final OperatorEvaluator emptyEvaluator = new OperatorEvaluator() {
		@Override
		protected ExprNode eval(Coords coords, OperatorDeclNode op, ExprNode[] e) throws NotEvaluatableException
		{
			throw new NotEvaluatableException(coords);
		}
	};
}
