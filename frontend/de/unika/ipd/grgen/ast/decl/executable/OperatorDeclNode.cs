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
	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ArithmeticOperatorNode = de.unika.ipd.grgen.ast.expr.ArithmeticOperatorNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using OperatorTypeNode = de.unika.ipd.grgen.ast.type.executable.OperatorTypeNode;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;

	/// <summary>
	/// Operator description / pseudo-declaration class.
	/// </summary>
	public class OperatorDeclNode : FunctionOrOperatorDeclBaseNode
	{
		private static readonly OperatorTypeNode operatorType = new OperatorTypeNode();

		/// <summary>
		/// Arity map of the operators. </summary>
		private static readonly IDictionary<Operator, int> arities = new Dictionary<Operator, int>();

		/// <summary>
		/// Name map of the operators. </summary>
		private static readonly IDictionary<Operator, string> names = new Dictionary<Operator, string>();

		static OperatorDeclNode()
		{
			foreach(Operator op in (Operator[])Enum.GetValues(typeof(Operator)))
				arities[op] = 2;

			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.COND] = 3;
			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_NOT] = 1;
			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT] = 1;
			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.NEG] = 1;
			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.CONST] = 0;
			arities[de.unika.ipd.grgen.ast.decl.executable.Operator.ERROR] = 0;

			names[de.unika.ipd.grgen.ast.decl.executable.Operator.COND] = "Cond";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_OR] = "LogOr";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_AND] = "LogAnd";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR] = "BitXor";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR] = "BitOr";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND] = "BitAnd";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.EQ] = "Eq";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.NE] = "Ne";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.LT] = "Lt";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.LE] = "Le";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.GT] = "Gt";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.GE] = "Ge";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.SHL] = "Shl";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.SHR] = "Shr";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_SHR] = "BitShr";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.ADD] = "Add";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.SUB] = "Sub";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.MUL] = "Mul";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.DIV] = "Div";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.MOD] = "Mod";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_NOT] = "LogNot";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT] = "BitNot";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.NEG] = "Neg";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.CONST] = "Const";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.IN] = "In";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.INDEX] = "IndexedAccess";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.EXCEPT] = "Except";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.SE] = "Se";
			names[de.unika.ipd.grgen.ast.decl.executable.Operator.ERROR] = "Error";

			InitializeOperatorsMap();
		}

		/// <summary>
		/// Just short forms for less verbose coding. </summary>
		internal static readonly TypeNode STRING = BasicTypeNode.stringType;
		internal static readonly TypeNode BOOLEAN = BasicTypeNode.booleanType;
		internal static readonly TypeNode BYTE = BasicTypeNode.byteType;
		internal static readonly TypeNode SHORT = BasicTypeNode.shortType;
		internal static readonly TypeNode INT = BasicTypeNode.intType;
		internal static readonly TypeNode LONG = BasicTypeNode.longType;
		internal static readonly TypeNode FLOAT = BasicTypeNode.floatType;
		internal static readonly TypeNode DOUBLE = BasicTypeNode.doubleType;
		internal static readonly TypeNode OBJECT = BasicTypeNode.objectType;
		internal static readonly TypeNode GRAPH = BasicTypeNode.graphType;
		internal static readonly TypeNode NULL = BasicTypeNode.nullType;
		internal static readonly TypeNode ENUM = BasicTypeNode.enumItemType;
		internal static readonly TypeNode TYPE = BasicTypeNode.typeType;
		internal static readonly TypeNode UNTYPED = BasicTypeNode.untypedType;

		/// <summary>
		/// Each generic operator is mapped by its ID to a set of concrete operator signatures.
		/// </summary>
		private static readonly IDictionary<Operator, ISet<OperatorDeclNode>> operators =
				new Dictionary<Operator, ISet<OperatorDeclNode>>();

		/// <summary>
		/// Makes an entry in the <seealso cref="operators"/> map.
		/// </summary>
		/// <param name="operator"> The operator. </param>
		/// <param name="resultType"> The result type of the operator. </param>
		/// <param name="operandTypes"> The operand types of the operator. </param>
		/// <param name="evaluator"> an Evaluator </param>
		public static void MakeOp(Operator @operator, TypeNode resultType,
				TypeNode[] operandTypes, OperatorEvaluator evaluator)
		{
			ISet<OperatorDeclNode> typeMap = operators[@operator];
			if(typeMap == null)
			{
				typeMap = new LinkedHashSet<OperatorDeclNode>();
				operators[@operator] = typeMap;
			}

			OperatorDeclNode newOpSig = new OperatorDeclNode(@operator, resultType,
					operandTypes, evaluator);
			typeMap.Add(newOpSig);
		}

		/// <summary>
		/// Enter a binary operator. This is just a convenience function for
		/// <seealso cref="makeOp(int, TypeNode, TypeNode[])"/>.
		/// </summary>
		public static void MakeBinOp(Operator @operator, TypeNode resultType,
				TypeNode leftType, TypeNode rightType, OperatorEvaluator evaluator)
		{
			MakeOp(@operator, resultType, new TypeNode[] { leftType, rightType }, evaluator);
		}

		/// <summary>
		/// Enter an unary operator. This is just a convenience function for
		/// <seealso cref="makeOp(int, TypeNode, TypeNode[])"/>.
		/// </summary>
		public static void MakeUnOp(Operator @operator, TypeNode resultType,
				TypeNode operandType, OperatorEvaluator evaluator)
		{
			MakeOp(@operator, resultType, new TypeNode[] { operandType }, evaluator);
		}

		// Initialize the operators map.
		static void InitializeOperatorsMap()
		{
			// String operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.IN, BOOLEAN, STRING, STRING, OperatorEvaluator.stringEvaluator);

			// object operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, OBJECT, OBJECT, OperatorEvaluator.objectEvaluator);

			// null operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, NULL, NULL, OperatorEvaluator.nullEvaluator);

			// subgraph operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SE, BOOLEAN, GRAPH, GRAPH, OperatorEvaluator.subgraphEvaluator);

			// Integer comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, INT, INT, OperatorEvaluator.intEvaluator);

			// Long comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, LONG, LONG, OperatorEvaluator.longEvaluator);

			// Float comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

			// Double comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

			// Boolean operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_NOT, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

			// Boolean comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, BOOLEAN, BOOLEAN, OperatorEvaluator.booleanEvaluator);

			// Integer arithmetic (byte and short are casted to integer)
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SUB, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MUL, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.DIV, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MOD, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SHL, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_SHR, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND, INT, INT, INT, OperatorEvaluator.intEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR, INT, INT, INT, OperatorEvaluator.intEvaluator);

			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NEG, INT, INT, OperatorEvaluator.intEvaluator);
			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT, INT, INT, OperatorEvaluator.intEvaluator);

			// Long arithmetic
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SUB, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MUL, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.DIV, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MOD, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SHL, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_SHR, LONG, LONG, INT, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR, LONG, LONG, LONG, OperatorEvaluator.longEvaluator);

			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NEG, LONG, LONG, OperatorEvaluator.longEvaluator);
			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_NOT, LONG, LONG, OperatorEvaluator.longEvaluator);

			// Float arithmetic
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SUB, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MUL, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.DIV, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MOD, FLOAT, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NEG, FLOAT, FLOAT, OperatorEvaluator.floatEvaluator);

			// Double arithmetic
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SUB, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MUL, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.DIV, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MOD, DOUBLE, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NEG, DOUBLE, DOUBLE, OperatorEvaluator.doubleEvaluator);

			// "String arithmetic"
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, STRING, STRING, STRING, OperatorEvaluator.stringEvaluator);

			// Type comparison
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, TYPE, TYPE, OperatorEvaluator.typeEvaluator);

			// And of course the ternary COND operator
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, BYTE, new TypeNode[] { BOOLEAN, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, SHORT, new TypeNode[] { BOOLEAN, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, INT, new TypeNode[] { BOOLEAN, INT, INT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, LONG, new TypeNode[] { BOOLEAN, LONG, LONG }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, FLOAT, new TypeNode[] { BOOLEAN, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, DOUBLE, new TypeNode[] { BOOLEAN, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, STRING, new TypeNode[] { BOOLEAN, STRING, STRING }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, BOOLEAN, new TypeNode[] { BOOLEAN, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, TYPE, new TypeNode[] { BOOLEAN, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, OBJECT, new TypeNode[] { BOOLEAN, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);
			// makeOp(Operator.COND, ENUM, new TypeNode[] { BOOLEAN, ENUM, ENUM }, OperatorEvaluator.condEvaluator);

			/////////////////////////////////////////////////////////////////////////////////////////
			// Operators to handle the untyped type that may appear in the sequence expressions due to untyped graph global variables

			// Comparison operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EQ, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.GT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LT, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.IN, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SE, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

			// Boolean (and set) operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_AND, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_OR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.LOG_NOT, BOOLEAN, UNTYPED, OperatorEvaluator.untypedEvaluator);

			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_AND, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_OR, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.BIT_XOR, BOOLEAN, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.EXCEPT, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

			// Arithmetic (and string or array/deque concatenation) operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.ADD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.SUB, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MUL, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.DIV, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.MOD, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

			MakeUnOp(de.unika.ipd.grgen.ast.decl.executable.Operator.NEG, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);

			// Condition operator ?:
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, BYTE, new TypeNode[] { UNTYPED, BYTE, BYTE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, SHORT, new TypeNode[] { UNTYPED, SHORT, SHORT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, INT, new TypeNode[] { UNTYPED, INT, INT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, LONG, new TypeNode[] { UNTYPED, LONG, LONG }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, FLOAT, new TypeNode[] { UNTYPED, FLOAT, FLOAT }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, DOUBLE, new TypeNode[] { UNTYPED, DOUBLE, DOUBLE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, STRING, new TypeNode[] { UNTYPED, STRING, STRING }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, BOOLEAN, new TypeNode[] { UNTYPED, BOOLEAN, BOOLEAN }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, TYPE, new TypeNode[] { UNTYPED, TYPE, TYPE }, OperatorEvaluator.condEvaluator);
			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, OBJECT, new TypeNode[] { UNTYPED, OBJECT, OBJECT }, OperatorEvaluator.condEvaluator);

			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, UNTYPED, new TypeNode[] { BOOLEAN, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);

			MakeOp(de.unika.ipd.grgen.ast.decl.executable.Operator.COND, UNTYPED, new TypeNode[] { UNTYPED, UNTYPED, UNTYPED }, OperatorEvaluator.untypedEvaluator);

			// Indexed access (of map, array, deque) operators
			MakeBinOp(de.unika.ipd.grgen.ast.decl.executable.Operator.INDEX, UNTYPED, UNTYPED, UNTYPED, OperatorEvaluator.untypedEvaluator);
		}

		/// <summary>
		/// Get the operand types of this operator signature. </summary>
		/// <returns> The operand types. </returns>
		public virtual TypeNode[] OperandTypes
		{
			get
			{
				TypeNode[] array = new TypeNode[parameterTypes.Count];
				return parameterTypes.ToArray(array);
			}
		}

		/// <summary>
		/// Get the arity of an operator.
		/// </summary>
		/// <param name="operator"> The operator. </param>
		/// <returns> The arity of the operator. </returns>
		public static int GetArity(Operator @operator)
		{
			return arities[@operator];
		}

		/// <summary>
		/// Get the name of an operator.
		/// </summary>
		/// <param name="id"> ID of the operator. </param>
		/// <returns> The name of the operator. </returns>
		public static string GetName(Operator @operator)
		{
			return names[@operator];
		}

		/// <summary>
		/// Get the "nearest" operator for a given set of operand types. This method
		/// selects the operator that will provoke the least implicit type casts when
		/// used.
		/// </summary>
		/// <param name="operator"> The operator. </param>
		/// <param name="operandTypes"> The operands. </param>
		/// <returns> The declaration of the "nearest" operator. </returns>
		public static OperatorDeclNode GetNearestOperator(Operator @operator, IList<TypeNode> operandTypes)
		{
			OperatorDeclNode resultingOperator = INVALID;
			int nearestDistance = int.MaxValue;

			bool hasVoid = false;
			bool hasUntyped = false;
			bool checkEnums = false;
			bool[] isEnum = new bool[operandTypes.Count]; // initialized to false

			for(int i = 0; i < operandTypes.Count; i++)
			{
				if(operandTypes[i] == BasicTypeNode.voidType)
					hasVoid = true;
				else if(operandTypes[i] == BasicTypeNode.untypedType)
					hasUntyped = true;
				else if(operandTypes[i] is EnumTypeNode)
				{
					checkEnums = true;
					isEnum[i] = true;
				}
			}

			ISet<OperatorDeclNode> operatorCandidates = operators[@operator];
			if(operatorCandidates == null)
				return INVALID;

			foreach(OperatorDeclNode operatorCandidate in operatorCandidates)
			{
				operatorCandidate.Resolve();

				int distance = operatorCandidate.GetDistance(operandTypes);

				string arguments = "";
				foreach(TypeNode tn in operandTypes)
					arguments += tn.ToString() + ", ";
				debug.Report(NOTE, "dist: " + distance + " for signature: " + operatorCandidate + " against " + arguments);

				if(distance == int.MaxValue)
					continue;

				if(checkEnums)
				{
					// Make implicit casts from enum to int for half the price
					distance *= 2;

					TypeNode[] candidateOperandTypes = operatorCandidate.OperandTypes;
					for(int i = 0; i < operandTypes.Count; i++)
					{
						if(isEnum[i] && candidateOperandTypes[i] == BasicTypeNode.intType)
							distance--;
					}
				}

				if(distance < nearestDistance)
				{
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
					|| !checkEnums && nearestDistance >= 2))
			{
				resultingOperator = INVALID;
				resultingOperator.Resolve();
			}

			bool untypedOperator = false;
			foreach(TypeNode operandTypeOfOperator in resultingOperator.OperandTypes)
			{
				if(operandTypeOfOperator == UNTYPED)
					untypedOperator = true;
			}

			// Don't allow untyped to get introduced on type mismatches (one argument untyped -> untyped as result ok)
			if(untypedOperator && !hasUntyped)
			{
				resultingOperator = INVALID;
				resultingOperator.Resolve();
			}

			debug.Report(NOTE, "selected: " + resultingOperator);

			return resultingOperator;
		}

		/// <summary>
		/// An invalid operator signature.
		/// </summary>
		private static readonly OperatorDeclNode INVALID = new OperatorDeclNodeAnonymousInnerClass(BasicTypeNode.errorType,
				OperatorEvaluator.emptyEvaluator);

		private class OperatorDeclNodeAnonymousInnerClass : OperatorDeclNode
		{
			private readonly OperatorDeclNode outerInstance;

			public OperatorDeclNodeAnonymousInnerClass(OperatorDeclNode outerInstance, TypeNode errorType, UnknownType emptyEvaluator) : base(Operator.ERROR, errorType, new TypeNode[] {}, emptyEvaluator)
			{
				this.outerInstance = outerInstance;
			}

			public override bool isValid()
			{
				return false;
			}
		}

		/// <summary>
		/// id of the operator. </summary>
		internal Operator @operator;

		/// <summary>
		/// The evaluator for constant expressions for this operator. </summary>
		private OperatorEvaluator evaluator;

		/// <summary>
		/// Make a new operator. This is used exclusively in this class, so it's
		/// private.
		/// </summary>
		/// <param name="operator"> The operator. </param>
		/// <param name="resultType"> The result type of the operator. </param>
		/// <param name="operandTypes"> The operand types. </param>
		/// <param name="evaluator"> The evaluator for this operator signature. </param>
		private OperatorDeclNode(Operator @operator, TypeNode resultType, TypeNode[] operandTypes, OperatorEvaluator evaluator)
			: base(new IdentNode(Symbol.Definition.Invalid), operatorType)
		{

			this.resultType = resultType;
			this.parameterTypes = new List<TypeNode>();
			foreach(TypeNode operandType in operandTypes)
				this.parameterTypes.Add(operandType);

			this.@operator = @operator;
			this.evaluator = evaluator;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
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
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool res = resultType.Resolve();
			foreach(TypeNode parameterType in parameterTypes)
				res &= parameterType.Resolve();
			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());
				return operatorType;
			}
		}

		/// <summary>
		/// Evaluate an expression using this operator signature.
		/// </summary>
		/// <param name="expr"> The expression to be evaluated. </param>
		/// <param name="arguments"> The arguments for this operator. </param>
		/// <returns> The possibly simplified value of the expression. </returns>
		public virtual ExprNode Evaluate(ArithmeticOperatorNode expr, ExprNode[] arguments)
		{
			return evaluator.Evaluate(expr, this, arguments);
		}

		/// <summary>
		/// Check, if this signature is ok, not bad.
		/// </summary>
		/// <returns> true, if the signature is ok, false, if not. </returns>
		public virtual bool IsValid()
		{
			return true;
		}

		public virtual Operator Operator
		{
			get
			{
				return @operator;
			}
		}

		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			string res = ResultType.ToString() + " ";
			res += names[@operator] + "(";
			TypeNode[] opTypes = OperandTypes;
			for(int i = 0; i < opTypes.Length; i++)
				res += (i == 0 ? "" : ",") + opTypes[i];
			res += ")";
			return res;
		}
	}

}
