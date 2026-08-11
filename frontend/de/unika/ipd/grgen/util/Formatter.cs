/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// ExpressionFormatter.java
/// 
/// @author Created by Omnicore CodeGuide
/// </summary>

namespace de.unika.ipd.grgen.util
{
	using System;
	using System.Text;

	using de.unika.ipd.grgen.ir;
	using Cast = de.unika.ipd.grgen.ir.expr.Cast;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using EnumExpression = de.unika.ipd.grgen.ir.expr.EnumExpression;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Typeof = de.unika.ipd.grgen.ir.expr.Typeof;
	using VariableExpression = de.unika.ipd.grgen.ir.expr.VariableExpression;
	using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using TypeClass = de.unika.ipd.grgen.ir.type.Type.TypeClass;

	public class Formatter
	{
		/* (binary and unary) operator symbols (of the C-language) */
		// ATTENTION: the first two shift operations are signed shifts
		// 		the second right shift is signed. This Backend simply gens
		//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
		private static string GetOperatorSymbol(OperatorCode opCode)
		{
			switch(opCode)
			{
			case OperatorCode.LOG_OR:
				return "||";
			case OperatorCode.LOG_AND:
				return "&&";
			case OperatorCode.BIT_OR:
				return "|";
			case OperatorCode.BIT_XOR:
				return "^";
			case OperatorCode.BIT_AND:
				return "&";
			case OperatorCode.EQ:
				return "==";
			case OperatorCode.NE:
				return "!=";
			case OperatorCode.LT:
				return "<";
			case OperatorCode.LE:
				return "<=";
			case OperatorCode.GT:
				return ">";
			case OperatorCode.GE:
				return ">=";
			case OperatorCode.SHL:
				return "<<";
			case OperatorCode.SHR:
				return ">>";
			case OperatorCode.BIT_SHR:
				return ">>";
			case OperatorCode.ADD:
				return "+";
			case OperatorCode.SUB:
				return "-";
			case OperatorCode.MUL:
				return "*";
			case OperatorCode.DIV:
				return "/";
			case OperatorCode.MOD:
				return "%";
			case OperatorCode.LOG_NOT:
				return "!";
			case OperatorCode.BIT_NOT:
				return "~";
			case OperatorCode.NEG:
				return "-";
			case OperatorCode.IN:
				return "in";
			case OperatorCode.EXCEPT:
				return "\\";
			case OperatorCode.SE:
				return "~~";
			default:
				throw new Exception("internal failure");
			}
		}

		public static string FormatConditionEval(Expression cond)
		{
			StringBuilder sb = new StringBuilder();
			FormatConditionEvalAux(sb, cond);
			return sb.ToString();
		}

		private static void FormatConditionEvalAux(StringBuilder sb, Expression cond)
		{
			if(cond is Operator)
			{
				Operator op = (Operator)cond;
				switch(op.Arity())
				{
				case 1:
					sb.Append("(" + GetOperatorSymbol(op.OpCode) + " ");
					FormatConditionEvalAux(sb, op.GetOperand(0));
					sb.Append(")");
					break;
				case 2:
					FormatConditionEvalAux(sb, op.GetOperand(0));
					sb.Append(" " + GetOperatorSymbol(op.OpCode) + " ");
					FormatConditionEvalAux(sb, op.GetOperand(1));
					break;
				case 3:
					if(op.OpCode == OperatorCode.COND)
					{
						sb.Append("(");
						FormatConditionEvalAux(sb, op.GetOperand(0));
						sb.Append(") ? (");
						FormatConditionEvalAux(sb, op.GetOperand(1));
						sb.Append(") : (");
						FormatConditionEvalAux(sb, op.GetOperand(2));
						sb.Append(")");
						break;
					}
					//$FALL-THROUGH$
				default:
					throw new System.NotSupportedException("Unsupported Operation arrity (" + op.Arity() + ")");
				}
			}
			else if(cond is Qualification)
			{
				Qualification qual = (Qualification)cond;
				Entity entity = qual.Owner;

				if(entity is Node)
					sb.Append(FormatIdentifiable(entity) + "." + FormatIdentifiable(qual.Member));
				else if(entity is Edge)
					sb.Append(FormatIdentifiable(entity) + "." + FormatIdentifiable(qual.Member));
				else
					throw new System.NotSupportedException("Unsupported Entity (" + entity + ")");
			}
			else if(cond is Constant)
			{ // gen C-code for constant expressions
				Constant constant = (Constant)cond;
				Type type = constant.Type;

				switch(type.Classify())
				{
				case Type.TypeClass.IS_STRING: //emit C-code for string constants
					sb.Append("'" + constant.Value + "'");
					break;
				case Type.TypeClass.IS_BOOLEAN: //emit C-code for boolean constans
					bool? bool_const = (bool?)constant.Value;
					if(bool_const.Value)
						sb.Append("true"); // true-value
					else
						sb.Append("false"); // false-value
					break;
				case Type.TypeClass.IS_INTEGER: //emit C-code for integer constants
					sb.Append(constant.Value.ToString()); // this also applys to enum constants
					break;
				default:
					break;
				}
			}
			else if(cond is EnumExpression)
			{
				EnumExpression enumExp = (EnumExpression)cond;
				sb.Append("ENUM_" + enumExp.Type.Ident.ToString() + ".@" + enumExp.EnumItem.ToString());
			}
			else if(cond is Typeof)
			{
				Typeof to = (Typeof)cond;
				sb.Append(FormatIdentifiable(to.Entity) + ".type");
			}
			else if(cond is Cast)
			{
				Cast cast = (Cast)cond;
				Type type = cast.Type;

				if(type.Classify() == Type.TypeClass.IS_STRING)
				{
					FormatConditionEvalAux(sb, cast.Expression);
					sb.Append(".ToString()");
				}
				else
				{
					string typeName = "";

					switch(type.Classify())
					{
					case Type.TypeClass.IS_INTEGER:
						typeName = "int";
						break;
					case Type.TypeClass.IS_FLOAT:
						typeName = "float";
						break;
					case Type.TypeClass.IS_DOUBLE:
						typeName = "double";
						break;
					case Type.TypeClass.IS_BOOLEAN:
						typeName = "bool";
						break;
					default:
						throw new System.NotSupportedException(
								"This is either a forbidden cast, which should have been " +
										"rejected on building the IR, or an allowed cast, which " +
										"should have been processed by the above code.");
					}

					sb.Append("((" + typeName + ") ");
					FormatConditionEvalAux(sb, cast.Expression);
					sb.Append(")");
				}
			}
			else if(cond is VariableExpression)
			{
				Variable var = ((VariableExpression)cond).Variable;
				sb.Append(var.Ident);
			}
			else if(cond is Visited)
			{
				Visited vis = (Visited)cond;
				FormatConditionEvalAux(sb, vis.Entity);
				sb.Append(".visited[");
				FormatConditionEvalAux(sb, vis.VisitorID);
				sb.Append("]");
			}
			else
				sb.Append("Unsupported expression type (" + cond + ")");
		}

		private static string FormatIdentifiable(Identifiable id)
		{
			string res = id.Ident.ToString();
			return res.Replace('$', '_');
		}
	}

}
