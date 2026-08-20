/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen.ast.decl.executable
{
	using System;

	using ArithmeticOperatorNode = de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
	using BoolConstNode = de.unika.ipd.grgen.ast.expr.BoolConstNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using MemberAccessExprNode = de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
	using TypeConstNode = de.unika.ipd.grgen.ast.expr.TypeConstNode;
	using TypeofNode = de.unika.ipd.grgen.ast.expr.TypeofNode;
	using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
	using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using DoubleConstNode = de.unika.ipd.grgen.ast.expr.numeric.DoubleConstNode;
	using FloatConstNode = de.unika.ipd.grgen.ast.expr.numeric.FloatConstNode;
	using IntConstNode = de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
	using LongConstNode = de.unika.ipd.grgen.ast.expr.numeric.LongConstNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
	using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using ObjectTypeNode = de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Base = de.unika.ipd.grgen.util.Base;

	/// <summary>
	/// A class that represents an evaluator for constant expressions.
	/// </summary>
	public class OperatorEvaluator
	{
		/// <summary>
		/// NOTE: recalculate the serialVersionUID if you change the class.
		/// </summary>
		internal class NotEvaluatableException : Exception
		{
			internal const long serialVersionUID = -4866769730405704919L;

			internal Coords coords;

			public NotEvaluatableException(Coords coords)
				 : base()
			{
				this.coords = coords;
			}

			public override string Message
			{
				get
				{
					return "Expression not evaluatable at " + coords.ToString();
				}
			}
		}

		/// <summary>
		/// NOTE: recalculate the serialVersionUID if you change the class.
		/// </summary>
		internal class ValueException : Exception
		{
			internal const long serialVersionUID = 991159946682342406L;

			internal Coords coords;

			public ValueException(Coords coords)
				: base()
			{
				this.coords = coords;
			}

			public override string Message
			{
				get
				{
					return "Expression not constant or value has wrong type at " + coords.ToString();
				}
			}
		}

		public virtual ExprNode Evaluate(ExprNode expr, OperatorDeclNode @operator, ExprNode[] arguments)
		{
			Base.debug.Report(Base.NOTE, "id: " + @operator.Operator + ", name: " + OperatorDeclNode.GetName(@operator.Operator));

			ExprNode resExpr = expr;
			TypeNode[] paramTypes = @operator.OperandTypes;

			// Check, if the arity matches.
			if(arguments.Length == paramTypes.Length)
			{
				// Check the types of the arguments.
				for(int i = 0; i < arguments.Length; i++)
				{
					Base.debug.Report(Base.NOTE, "parameter type: " + paramTypes[i]
							+ " argument type: " + arguments[i].Type);
					if(!paramTypes[i].IsEqual(arguments[i].Type))
						return resExpr;
				}

				// If we're here, all checks succeeded.
				try
				{
					resExpr = Eval(expr.Coords, @operator, arguments);
				}
				catch(NotEvaluatableException e)
				{
					Base.debug.Report(Base.NOTE, e.ToString());
				}
			}

			if(Base.debug.WillReport(Base.NOTE))
			{
				ConstNode c = (resExpr is ConstNode) ? (ConstNode)resExpr : ConstNode.Invalid;
				Base.debug.Report(Base.NOTE, "result: " + resExpr.GetType() + ", value: " + c.Value);
			}

			return resExpr;
		}

		protected internal virtual ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
		{
			return null;
		}

		private static object CheckValue(ExprNode e, Type type)
		{
			if(!(e is ConstNode))
				throw new ValueException(e.Coords);

			object v = ((ConstNode)e).Value;
			if(!type.IsInstanceOfType(v))
				throw new ValueException(e.Coords);

			return v;
		}

		protected internal static object GetArgValue(ExprNode[] args, OperatorDeclNode op, int pos)
		{
			TypeNode[] paramTypes = op.OperandTypes;

			if(paramTypes[pos].IsBasic())
			{
				BasicTypeNode paramType = (BasicTypeNode)paramTypes[pos];

				return CheckValue(args[pos], paramType.ValueType);
			}
			else
				throw new ValueException(args[pos].Coords);
		}

		public static readonly OperatorEvaluator objectEvaluator = new OperatorEvaluatorAnonymousInnerClass();

		private class OperatorEvaluatorAnonymousInnerClass : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				ObjectTypeNode.Value a0, a1;

				if(OperatorDeclNode.GetArity(op.Operator) != 2)
					throw new NotEvaluatableException(coords);

				try
				{
					a0 = (ObjectTypeNode.Value)GetArgValue(e, op, 0);
					a1 = (ObjectTypeNode.Value)GetArgValue(e, op, 1);
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0.Equals(a1));
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, !a0.Equals(a1));

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator subgraphEvaluator = new OperatorEvaluatorAnonymousInnerClass2();

		private class OperatorEvaluatorAnonymousInnerClass2 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				throw new NotEvaluatableException(coords);
			}
		}

		public static readonly OperatorEvaluator nullEvaluator = new OperatorEvaluatorAnonymousInnerClass3();

		private class OperatorEvaluatorAnonymousInnerClass3 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				if(OperatorDeclNode.GetArity(op.Operator) != 2)
					throw new NotEvaluatableException(coords);

				try
				{
					GetArgValue(e, op, 0);
					GetArgValue(e, op, 1);
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, true);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, false);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator stringEvaluator = new OperatorEvaluatorAnonymousInnerClass4();

		private class OperatorEvaluatorAnonymousInnerClass4 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				string a0;
				object aobj1;

				try
				{
					a0 = (string)GetArgValue(e, op, 0);
					aobj1 = GetArgValue(e, op, 1);
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				if(op.Operator == Operator.ADD)
					return new StringConstNode(coords, a0 + aobj1);

				string a1 = (string)aobj1;

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0.Equals(a1));
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, !a0.Equals(a1));
				//case GE:  return new BoolConstNode(coords, a0.compareTo(a1) >= 0);
				//case GT:  return new BoolConstNode(coords, a0.compareTo(a1) > 0);
				//case LE:  return new BoolConstNode(coords, a0.compareTo(a1) <= 0);
				//case LT:  return new BoolConstNode(coords, a0.compareTo(a1) < 0);
				//case IN:  return new BoolConstNode(coords, a1.contains(a0));

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator intEvaluator = new OperatorEvaluatorAnonymousInnerClass5();

		private class OperatorEvaluatorAnonymousInnerClass5 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				int a0, a1;

				try
				{
					a0 = ((int?)GetArgValue(e, op, 0)).Value;
					a1 = 0;
					if(OperatorDeclNode.GetArity(op.Operator) > 1)
						a1 = ((int?)GetArgValue(e, op, 1)).Value;
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0 == a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, a0 != a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LT:
					return new BoolConstNode(coords, a0 < a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LE:
					return new BoolConstNode(coords, a0 <= a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GT:
					return new BoolConstNode(coords, a0 > a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GE:
					return new BoolConstNode(coords, a0 >= a1);

				case de.unika.ipd.grgen.ast.decl.executable.Operator.ADD:
					return new IntConstNode(coords, a0 + a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SUB:
					return new IntConstNode(coords, a0 - a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MUL:
					return new IntConstNode(coords, a0 * a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.DIV:
					return new IntConstNode(coords, a0 / a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MOD:
					return new IntConstNode(coords, a0 % a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SHL:
					return new IntConstNode(coords, a0 << a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SHR:
					return new IntConstNode(coords, a0 >> a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_SHR:
					return new IntConstNode(coords, (int)(((uint)a0) >> a1));
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR:
					return new IntConstNode(coords, a0 | a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND:
					return new IntConstNode(coords, a0 & a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR:
					return new IntConstNode(coords, a0 ^ a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT:
					return new IntConstNode(coords, ~a0);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NEG:
					return new IntConstNode(coords, -a0);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator longEvaluator = new OperatorEvaluatorAnonymousInnerClass6();

		private class OperatorEvaluatorAnonymousInnerClass6 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				long a0, a1;

				try
				{
					a0 = ((long?)GetArgValue(e, op, 0)).Value;
					a1 = 0;
					if(OperatorDeclNode.GetArity(op.Operator) > 1)
						a1 = ((long?)GetArgValue(e, op, 1)).Value;
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0 == a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, a0 != a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LT:
					return new BoolConstNode(coords, a0 < a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LE:
					return new BoolConstNode(coords, a0 <= a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GT:
					return new BoolConstNode(coords, a0 > a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GE:
					return new BoolConstNode(coords, a0 >= a1);

				case de.unika.ipd.grgen.ast.decl.executable.Operator.ADD:
					return new LongConstNode(coords, a0 + a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SUB:
					return new LongConstNode(coords, a0 - a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MUL:
					return new LongConstNode(coords, a0 * a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.DIV:
					return new LongConstNode(coords, a0 / a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MOD:
					return new LongConstNode(coords, a0 % a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SHL:
					return new LongConstNode(coords, a0 << (int)a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SHR:
					return new LongConstNode(coords, a0 >> (int)a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_SHR:
					return new LongConstNode(coords, (long)(((ulong)a0) >> (int)a1));
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR:
					return new LongConstNode(coords, a0 | a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND:
					return new LongConstNode(coords, a0 & a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR:
					return new LongConstNode(coords, a0 ^ a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT:
					return new LongConstNode(coords, ~a0);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NEG:
					return new LongConstNode(coords, -a0);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator floatEvaluator = new OperatorEvaluatorAnonymousInnerClass7();

		private class OperatorEvaluatorAnonymousInnerClass7 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				float a0, a1;

				try
				{
					a0 = ((float?)GetArgValue(e, op, 0)).Value;
					a1 = 0;
					if(OperatorDeclNode.GetArity(op.Operator) > 1)
						a1 = ((float?)GetArgValue(e, op, 1)).Value;
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0 == a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, a0 != a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LT:
					return new BoolConstNode(coords, a0 < a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LE:
					return new BoolConstNode(coords, a0 <= a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GT:
					return new BoolConstNode(coords, a0 > a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GE:
					return new BoolConstNode(coords, a0 >= a1);

				case de.unika.ipd.grgen.ast.decl.executable.Operator.ADD:
					return new FloatConstNode(coords, a0 + a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SUB:
					return new FloatConstNode(coords, a0 - a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MUL:
					return new FloatConstNode(coords, a0 * a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.DIV:
					return new FloatConstNode(coords, a0 / a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MOD:
					return new FloatConstNode(coords, a0 % a1);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator doubleEvaluator = new OperatorEvaluatorAnonymousInnerClass8();

		private class OperatorEvaluatorAnonymousInnerClass8 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				double a0, a1;

				try
				{
					a0 = ((double?)GetArgValue(e, op, 0)).Value;
					a1 = 0;
					if(OperatorDeclNode.GetArity(op.Operator) > 1)
						a1 = ((double?)GetArgValue(e, op, 1)).Value;
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0 == a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, a0 != a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LT:
					return new BoolConstNode(coords, a0 < a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LE:
					return new BoolConstNode(coords, a0 <= a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GT:
					return new BoolConstNode(coords, a0 > a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.GE:
					return new BoolConstNode(coords, a0 >= a1);

				case de.unika.ipd.grgen.ast.decl.executable.Operator.ADD:
					return new DoubleConstNode(coords, a0 + a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.SUB:
					return new DoubleConstNode(coords, a0 - a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MUL:
					return new DoubleConstNode(coords, a0 * a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.DIV:
					return new DoubleConstNode(coords, a0 / a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.MOD:
					return new DoubleConstNode(coords, a0 % a1);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator typeEvaluator = new OperatorEvaluatorAnonymousInnerClass9();

		private class OperatorEvaluatorAnonymousInnerClass9 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				bool is_node1, is_node2;

				if(e[0] is TypeConstNode)
				{
					TypeNode type = (TypeNode)((TypeConstNode)e[0]).Value;
					is_node1 = type is NodeTypeNode;
				}
				else if(e[0] is TypeofNode)
				{
					TypeNode type = ((TypeofNode)e[0]).Entity.DeclType;
					is_node1 = type is NodeTypeNode;
				}
				else
					throw new NotEvaluatableException(coords);

				if(e[1] is TypeConstNode)
				{
					TypeNode type = (TypeNode)((TypeConstNode)e[1]).Value;
					is_node2 = type is NodeTypeNode;
				}
				else if(e[1] is TypeofNode)
				{
					TypeNode type = ((TypeofNode)e[1]).Entity.DeclType;
					is_node2 = type is NodeTypeNode;
				}
				else
					throw new NotEvaluatableException(coords);

				if(is_node1 != is_node2)
				{
					Base.error.Warning(coords, "comparison between node and edge types will always fail");
					switch(op.Operator)
					{
					case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					case de.unika.ipd.grgen.ast.decl.executable.Operator.LT:
					case de.unika.ipd.grgen.ast.decl.executable.Operator.GT:
					case de.unika.ipd.grgen.ast.decl.executable.Operator.LE:
					case de.unika.ipd.grgen.ast.decl.executable.Operator.GE:
						return new BoolConstNode(coords, false);
					case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
						return new BoolConstNode(coords, true);

					default:
						break;
					}
				}
				throw new NotEvaluatableException(coords);
			}
		}

		public static readonly OperatorEvaluator booleanEvaluator = new OperatorEvaluatorAnonymousInnerClass10();

		private class OperatorEvaluatorAnonymousInnerClass10 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				bool a0, a1;

				try
				{
					a0 = ((bool?)GetArgValue(e, op, 0)).Value;
					a1 = false;
					if(OperatorDeclNode.GetArity(op.Operator) > 1)
						a1 = ((bool?)GetArgValue(e, op, 1)).Value;
				}
				catch(ValueException)
				{
					throw new NotEvaluatableException(coords);
				}

				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.EQ:
					return new BoolConstNode(coords, a0 == a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.NE:
					return new BoolConstNode(coords, a0 != a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_AND:
					return new BoolConstNode(coords, a0 && a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_OR:
					return new BoolConstNode(coords, a0 || a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_NOT:
					return new BoolConstNode(coords, !a0);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR:
					return new BoolConstNode(coords, a0 | a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND:
					return new BoolConstNode(coords, a0 & a1);
				case de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR:
					return new BoolConstNode(coords, a0 ^ a1);

				default:
					throw new NotEvaluatableException(coords);
				}
			}
		}

		public static readonly OperatorEvaluator condEvaluator = new OperatorEvaluatorAnonymousInnerClass11();

		private class OperatorEvaluatorAnonymousInnerClass11 : OperatorEvaluator
		{
			public override ExprNode Evaluate(ExprNode expr, OperatorDeclNode op, ExprNode[] args)
			{
				try
				{
					return ((bool?)GetArgValue(args, op, 0)).Value ? args[1] : args[2];
				}
				catch(ValueException)
				{
					return expr;
				}
			}
		}

		public static readonly OperatorEvaluator mapEvaluator = new OperatorEvaluatorAnonymousInnerClass12();

		private class OperatorEvaluatorAnonymousInnerClass12 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.IN:
				{
					if(e[1] is ArithmeticOperatorNode)
					{
						ArithmeticOperatorNode opNode = (ArithmeticOperatorNode)e[1];
						if(opNode.Operator == Operator.BIT_AND)
						{
							ExprNode set1 = opNode.children[0];
							ExprNode set2 = opNode.children[1];
							ExprNode in1 = (new ArithmeticOperatorNode(set1.Coords, Operator.IN, e[0], set1)).Evaluate();
							ExprNode in2 = (new ArithmeticOperatorNode(set2.Coords, Operator.IN, e[0], set2)).Evaluate();
							return (new ArithmeticOperatorNode(opNode.Coords, Operator.LOG_AND, in1, in2)).Evaluate();
						}
						else if(opNode.Operator == Operator.BIT_OR)
						{
							ExprNode set1 = opNode.children[0];
							ExprNode set2 = opNode.children[1];
							ExprNode in1 = (new ArithmeticOperatorNode(set1.Coords, Operator.IN, e[0], set1)).Evaluate();
							ExprNode in2 = (new ArithmeticOperatorNode(set2.Coords, Operator.IN, e[0], set2)).Evaluate();
							return (new ArithmeticOperatorNode(opNode.Coords, Operator.LOG_OR, in1, in2)).Evaluate();
						}
					}
					else if(e[0] is ConstNode)
					{
						ConstNode val = (ConstNode)e[0];

						MapInitNode mapInit = null;
						if(e[1] is MapInitNode)
							mapInit = (MapInitNode)e[1];
						else if(e[1] is MemberAccessExprNode)
						{
							MemberDeclNode member = ((MemberAccessExprNode)e[1]).Decl;
							if(member.IsConst() && member.ConstInitializer != null)
								mapInit = (MapInitNode)member.ConstInitializer;
						}
						if(mapInit != null)
						{
							if(mapInit.Contains(val))
								return new BoolConstNode(coords, true);
							else if(mapInit.AreKeysConstant())
								return new BoolConstNode(coords, false);
							// Otherwise not decideable because of non-constant entries in map keys
						}
					}
					break;
				}
				case de.unika.ipd.grgen.ast.decl.executable.Operator.INDEX:
				{
					if(e[1] is ConstNode)
					{
						ConstNode key = (ConstNode)e[1];

						MapInitNode mapInit = null;
						if(e[0] is MapInitNode)
							mapInit = (MapInitNode)e[0];
						else if(e[0] is MemberAccessExprNode)
						{
							MemberDeclNode member = ((MemberAccessExprNode)e[0]).Decl;
							if(member.IsConst() && member.ConstInitializer != null)
								mapInit = (MapInitNode)member.ConstInitializer;
						}
						if(mapInit != null)
						{
							ExprNode val = mapInit.GetAtIndex(key);
							if(mapInit.IsConstant() && val is ConstNode)
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
		}

		public static readonly OperatorEvaluator setEvaluator = new OperatorEvaluatorAnonymousInnerClass13();

		private class OperatorEvaluatorAnonymousInnerClass13 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.IN:
				{
					if(e[1] is ArithmeticOperatorNode)
					{
						ArithmeticOperatorNode opNode = (ArithmeticOperatorNode)e[1];
						if(opNode.Operator == Operator.BIT_AND)
						{
							ExprNode set1 = opNode.children[0];
							ExprNode set2 = opNode.children[1];
							ExprNode in1 = (new ArithmeticOperatorNode(set1.Coords, Operator.IN, e[0], set1)).Evaluate();
							ExprNode in2 = (new ArithmeticOperatorNode(set2.Coords, Operator.IN, e[0], set2)).Evaluate();
							return (new ArithmeticOperatorNode(opNode.Coords, Operator.LOG_AND, in1, in2)).Evaluate();
						}
						else if(opNode.Operator == Operator.BIT_OR)
						{
							ExprNode set1 = opNode.children[0];
							ExprNode set2 = opNode.children[1];
							ExprNode in1 = (new ArithmeticOperatorNode(set1.Coords, Operator.IN, e[0], set1)).Evaluate();
							ExprNode in2 = (new ArithmeticOperatorNode(set2.Coords, Operator.IN, e[0], set2)).Evaluate();
							return (new ArithmeticOperatorNode(opNode.Coords, Operator.LOG_OR, in1, in2)).Evaluate();
						}
					}
					else if(e[0] is ConstNode)
					{
						ConstNode val = (ConstNode)e[0];

						SetInitNode setInit = null;
						if(e[1] is SetInitNode)
							setInit = (SetInitNode)e[1];
						else if(e[1] is MemberAccessExprNode)
						{
							MemberDeclNode member = ((MemberAccessExprNode)e[1]).Decl;
							if(member.IsConst() && member.ConstInitializer != null)
								setInit = (SetInitNode)member.ConstInitializer;
						}
						if(setInit != null)
						{
							if(setInit.Contains(val))
								return new BoolConstNode(coords, true);
							else if(setInit.IsConstant())
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
		}

		public static readonly OperatorEvaluator arrayEvaluator = new OperatorEvaluatorAnonymousInnerClass14();

		private class OperatorEvaluatorAnonymousInnerClass14 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.INDEX:
				{
					if(e[1] is ConstNode)
					{
						ConstNode index = (ConstNode)e[1];

						ArrayInitNode arrayInit = null;
						if(e[0] is ArrayInitNode)
							arrayInit = (ArrayInitNode)e[0];
						else if(e[0] is MemberAccessExprNode)
						{
							MemberDeclNode member = ((MemberAccessExprNode)e[0]).Decl;
							if(member.IsConst() && member.ConstInitializer != null)
								arrayInit = (ArrayInitNode)member.ConstInitializer;
						}
						if(arrayInit != null)
						{
							ExprNode val = arrayInit.GetAtIndex(index);
							if(val is ConstNode)
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
		}

		public static readonly OperatorEvaluator dequeEvaluator = new OperatorEvaluatorAnonymousInnerClass15();

		private class OperatorEvaluatorAnonymousInnerClass15 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				switch(op.Operator)
				{
				case de.unika.ipd.grgen.ast.decl.executable.Operator.INDEX:
				{
					if(e[1] is ConstNode)
					{
						ConstNode index = (ConstNode)e[1];

						DequeInitNode dequeInit = null;
						if(e[0] is DequeInitNode)
							dequeInit = (DequeInitNode)e[0];
						else if(e[0] is MemberAccessExprNode)
						{
							MemberDeclNode member = ((MemberAccessExprNode)e[0]).Decl;
							if(member.IsConst() && member.ConstInitializer != null)
								dequeInit = (DequeInitNode)member.ConstInitializer;
						}
						if(dequeInit != null)
						{
							ExprNode val = dequeInit.GetAtIndex(index);
							if(val is ConstNode)
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
		}

		public static readonly OperatorEvaluator untypedEvaluator = new OperatorEvaluatorAnonymousInnerClass16();

		private class OperatorEvaluatorAnonymousInnerClass16 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				throw new NotEvaluatableException(coords);
			}
		}

		public static readonly OperatorEvaluator emptyEvaluator = new OperatorEvaluatorAnonymousInnerClass17();

		private class OperatorEvaluatorAnonymousInnerClass17 : OperatorEvaluator
		{
			protected internal override ExprNode Eval(Coords coords, OperatorDeclNode op, ExprNode[] e)
			{
				throw new NotEvaluatableException(coords);
			}
		}
	}

}
