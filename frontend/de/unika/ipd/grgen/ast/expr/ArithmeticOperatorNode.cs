/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
	using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using MapAddItem = de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
	using MapRemoveItem = de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
	using SetAddItem = de.unika.ipd.grgen.ir.stmt.set.SetAddItem;
	using SetRemoveItem = de.unika.ipd.grgen.ir.stmt.set.SetRemoveItem;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using IndexedAccessExpr = de.unika.ipd.grgen.ir.expr.IndexedAccessExpr;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// An arithmetic operator.
	/// </summary>
	public class ArithmeticOperatorNode : OperatorNode
	{
		static ArithmeticOperatorNode()
		{
			SetClassName(typeof(ArithmeticOperatorNode), "arithmetic operator");
			AssocOpCode(Operator.COND, OperatorCode.COND);
			AssocOpCode(Operator.LOG_OR, OperatorCode.LOG_OR);
			AssocOpCode(Operator.LOG_AND, OperatorCode.LOG_AND);
			AssocOpCode(Operator.BIT_OR, OperatorCode.BIT_OR);
			AssocOpCode(Operator.BIT_XOR, OperatorCode.BIT_XOR);
			AssocOpCode(Operator.BIT_AND, OperatorCode.BIT_AND);
			AssocOpCode(Operator.EQ, OperatorCode.EQ);
			AssocOpCode(Operator.NE, OperatorCode.NE);
			AssocOpCode(Operator.SE, OperatorCode.SE);
			AssocOpCode(Operator.LT, OperatorCode.LT);
			AssocOpCode(Operator.LE, OperatorCode.LE);
			AssocOpCode(Operator.GT, OperatorCode.GT);
			AssocOpCode(Operator.GE, OperatorCode.GE);
			AssocOpCode(Operator.SHL, OperatorCode.SHL);
			AssocOpCode(Operator.SHR, OperatorCode.SHR);
			AssocOpCode(Operator.BIT_SHR, OperatorCode.BIT_SHR);
			AssocOpCode(Operator.ADD, OperatorCode.ADD);
			AssocOpCode(Operator.SUB, OperatorCode.SUB);
			AssocOpCode(Operator.MUL, OperatorCode.MUL);
			AssocOpCode(Operator.DIV, OperatorCode.DIV);
			AssocOpCode(Operator.MOD, OperatorCode.MOD);
			AssocOpCode(Operator.LOG_NOT, OperatorCode.LOG_NOT);
			AssocOpCode(Operator.BIT_NOT, OperatorCode.BIT_NOT);
			AssocOpCode(Operator.NEG, OperatorCode.NEG);
			AssocOpCode(Operator.IN, OperatorCode.IN);
			AssocOpCode(Operator.EXCEPT, OperatorCode.EXCEPT);
		}

		/// <summary>
		/// maps an operator to an IR opcode, filled with code beyond </summary>
		private static IDictionary<Operator, OperatorCode> irOpCodeMap = new Dictionary<Operator, OperatorCode>();


		private QualIdentNode target = null; // if !null it's a set/map union/except which is to be broken up

		/// <param name="coords"> Source code coordinates. </param>
		/// <param name="opId"> ID of the operator. </param>
		public ArithmeticOperatorNode(Coords coords, Operator @operator)
			: base(coords, @operator)
		{
		}

		public ArithmeticOperatorNode(Coords coords, Operator @operator, ExprNode op1, ExprNode op2)
			: base(coords, @operator)
		{
			children.Add(op1);
			children.Add(op2);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				return new List<BaseNode>(children);
			}
		}

		public virtual ICollection<ExprNode> ChildrenExact
		{
			get
			{
				return children;
			}
		}

		public virtual IList<ExprNode> ChildrenAsList
		{
			get
			{
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
				// nameless children
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.evaluate() "/>
		public override ExprNode Evaluate()
		{
			int n = children.Count;
			ExprNode[] args = new ExprNode[n];

			for(int i = 0; i < n; i++)
			{
				ExprNode c = children[i];
				args[i] = c.Evaluate();
				children[i] = args[i];
			}

			return OperatorDecl.Evaluate(this, args);
		}

		/// <summary>
		/// mark to break set/map assignment of set/map expression up into set/map add/remove to/from target statements </summary>
		public virtual void MarkToBreakUpIntoStateChangingOperations(QualIdentNode target)
		{
			this.target = target;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			if(target != null)
			{
				Qualification qual = target.CheckIR(typeof(Qualification));
				EvalStatement previous = null;
				EvalStatement first = null;
				if(children[0].IR is EvalStatement)
				{
					first = children[0].CheckIR(typeof(EvalStatement));
					previous = first;
					while(previous.Next != null)
						previous = previous.Next;
				}

				first = ReplaceIRSetMapOrExceptByIRAddRemove(qual, previous, first);

				return first;
			}

			if(OperatorDecl.Operator == Operator.INDEX)
			{
				Expression texp = children[0].CheckIR(typeof(Expression));
				Type type = texp.Type;
				Type accessedType;
				if(type is MapType)
					accessedType = ((MapType)type).ValueType;
				else if(type is DequeType)
					accessedType = ((DequeType)type).ValueType;
				else if(type is ArrayType)
					accessedType = ((ArrayType)type).ValueType;
				else
					accessedType = type; // assuming untypedType
				return new IndexedAccessExpr(texp,
						children[1].CheckIR(typeof(Expression)), accessedType);
			}

			DeclaredTypeNode type = (DeclaredTypeNode)Type;
			de.unika.ipd.grgen.ir.expr.Operator op = new de.unika.ipd.grgen.ir.expr.Operator(type.IRType, GetIROpCode(Operator));

			foreach(ExprNode child in children)
			{
				Expression ir = child.CheckIR(typeof(Expression));
				op.AddOperand(ir);
			}

			return op;
		}

		private EvalStatement ReplaceIRSetMapOrExceptByIRAddRemove(Qualification qual,
				EvalStatement previous, EvalStatement first)
		{
			if(OperatorDecl.Operator == Operator.BIT_OR)
			{
				if(children[1].Type is SetTypeNode)
				{
					SetInitNode initNode = (SetInitNode)children[1];
					foreach(ExprNode item in initNode.Items.GetChildrenExact())
					{
						SetAddItem addItem = new SetAddItem(qual,
								item.CheckIR(typeof(Expression)));
						if(first == null)
							first = addItem;
						if(previous != null)
							previous.Next = addItem;
						previous = addItem;
					}
				}
				else
				{ //if(children.get(1).getType() instanceof MapTypeNode)
					MapInitNode initNode = (MapInitNode)children[1];
					foreach(ExprPairNode item in initNode.Items.GetChildrenExact())
					{
						MapAddItem addItem = new MapAddItem(qual,
								item.keyExpr.CheckIR(typeof(Expression)), item.valueExpr.CheckIR(typeof(Expression)));
						if(first == null)
							first = addItem;
						if(previous != null)
							previous.Next = addItem;
						previous = addItem;
					}
				}
			}
			else
			{ //if(getOperator().getOpId()==OperatorSignature.EXCEPT) // only BIT_OR/EXCEPT are marked
				if(children[1].Type is SetTypeNode)
				{
					SetInitNode initNode = (SetInitNode)children[1];
					if(children[0].Type is MapTypeNode)
					{ // handle map \ set
						foreach(ExprNode item in initNode.Items.GetChildrenExact())
						{
							MapRemoveItem remItem = new MapRemoveItem(qual,
									item.CheckIR(typeof(Expression)));
							if(first == null)
								first = remItem;
							if(previous != null)
								previous.Next = remItem;
							previous = remItem;
						}
					}
					else
					{ // handle normal case set \ set
						foreach(ExprNode item in initNode.Items.GetChildrenExact())
						{
							SetRemoveItem remItem = new SetRemoveItem(qual,
									item.CheckIR(typeof(Expression)));
							if(first == null)
								first = remItem;
							if(previous != null)
								previous.Next = remItem;
							previous = remItem;
						}
					}
				}
				else
				{ //if(children.get(1).getType() instanceof MapTypeNode)
					MapInitNode initNode = (MapInitNode)children[1];
					foreach(ExprPairNode item in initNode.Items.GetChildrenExact())
					{
						MapRemoveItem remItem = new MapRemoveItem(qual,
								item.keyExpr.CheckIR(typeof(Expression)));
						if(first == null)
							first = remItem;
						if(previous != null)
							previous.Next = remItem;
						previous = remItem;
					}
				}
			}
			return first;
		}

		private static void AssocOpCode(Operator @operator, OperatorCode opcode)
		{
			irOpCodeMap[@operator] = opcode;
		}

		/// <summary>
		/// Maps an operator ID to an IR opcode. </summary>
		private static OperatorCode GetIROpCode(Operator @operator)
		{
			return irOpCodeMap[@operator];
		}

		public override string ToString()
		{
			return OperatorDeclNode.GetName(Operator);
		}
	}

}
