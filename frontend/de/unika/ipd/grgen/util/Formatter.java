/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ExpressionFormatter.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Cast;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.ir.expr.EnumExpression;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.Typeof;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.Type.TypeClass;

public class Formatter
{
	/* (binary and unary) operator symbols (of the C-language) */
	// ATTENTION: the first two shift operations are signed shifts
	// 		the second right shift is signed. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private static String getOperatorSymbol(Operator.OperatorCode opCode)
	{
		switch(opCode)
		{
		case LOG_OR: return "||";
		case LOG_AND: return "&&";
		case BIT_OR: return "|";
		case BIT_XOR: return "^";
		case BIT_AND: return "&";
		case EQ: return "==";
		case NE: return "!=";
		case LT: return "<";
		case LE: return "<=";
		case GT: return ">";
		case GE: return ">=";
		case SHL: return "<<";
		case SHR: return ">>";
		case BIT_SHR: return ">>";
		case ADD: return "+";
		case SUB: return "-";
		case MUL: return "*";
		case DIV: return "/";
		case MOD: return "%";
		case LOG_NOT: return "!";
		case BIT_NOT: return "~";
		case NEG: return "-";
		case IN: return "in";
		case EXCEPT: return "\\";
		case SE: return "~~";
		default: throw new RuntimeException("internal failure");
		}
	}

	public static String formatConditionEval(Expression cond)
	{
		StringBuffer sb = new StringBuffer();
		formatConditionEvalAux(sb, cond);
		return sb.toString();
	}

	private static void formatConditionEvalAux(StringBuffer sb, Expression cond)
	{
		if(cond instanceof Operator) {
			Operator op = (Operator)cond;
			switch(op.arity()) {
			case 1:
				sb.append("(" + getOperatorSymbol(op.getOpCode()) + " ");
				formatConditionEvalAux(sb, op.getOperand(0));
				sb.append(")");
				break;
			case 2:
				formatConditionEvalAux(sb, op.getOperand(0));
				sb.append(" " + getOperatorSymbol(op.getOpCode()) + " ");
				formatConditionEvalAux(sb, op.getOperand(1));
				break;
			case 3:
				if(op.getOpCode() == Operator.OperatorCode.COND) {
					sb.append("(");
					formatConditionEvalAux(sb, op.getOperand(0));
					sb.append(") ? (");
					formatConditionEvalAux(sb, op.getOperand(1));
					sb.append(") : (");
					formatConditionEvalAux(sb, op.getOperand(2));
					sb.append(")");
					break;
				}
				//$FALL-THROUGH$
			default:
				throw new UnsupportedOperationException("Unsupported Operation arrity (" + op.arity() + ")");
			}
		} else if(cond instanceof Qualification) {
			Qualification qual = (Qualification)cond;
			Entity entity = qual.getOwner();

			if(entity instanceof Node) {
				sb.append(formatIdentifiable(entity) + "." + formatIdentifiable(qual.getMember()));
			} else if(entity instanceof Edge) {
				sb.append(formatIdentifiable(entity) + "." + formatIdentifiable(qual.getMember()));
			} else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		} else if(cond instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant)cond;
			Type type = constant.getType();

			switch(type.classify()) {
			case IS_STRING: //emit C-code for string constants
				sb.append("'" + constant.getValue() + "'");
				break;
			case IS_BOOLEAN: //emit C-code for boolean constans
				Boolean bool_const = (Boolean)constant.getValue();
				if(bool_const.booleanValue())
					sb.append("true"); /* true-value */
				else
					sb.append("false"); /* false-value */
				break;
			case IS_INTEGER: //emit C-code for integer constants
				sb.append(constant.getValue().toString()); /* this also applys to enum constants */
				break;
			default:
				break;
			}
		} else if(cond instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression)cond;
			sb.append("ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		} else if(cond instanceof Typeof) {
			Typeof to = (Typeof)cond;
			sb.append(formatIdentifiable(to.getEntity()) + ".type");
		} else if(cond instanceof Cast) {
			Cast cast = (Cast)cond;
			Type type = cast.getType();

			if(type.classify() == TypeClass.IS_STRING) {
				formatConditionEvalAux(sb, cast.getExpression());
				sb.append(".ToString()");
			} else {
				String typeName = "";

				switch(type.classify()) {
				case IS_INTEGER:
					typeName = "int";
					break;
				case IS_FLOAT:
					typeName = "float";
					break;
				case IS_DOUBLE:
					typeName = "double";
					break;
				case IS_BOOLEAN:
					typeName = "bool";
					break;
				default:
					throw new UnsupportedOperationException(
							"This is either a forbidden cast, which should have been " +
									"rejected on building the IR, or an allowed cast, which " +
									"should have been processed by the above code.");
				}

				sb.append("((" + typeName + ") ");
				formatConditionEvalAux(sb, cast.getExpression());
				sb.append(")");
			}
		} else if(cond instanceof VariableExpression) {
			Variable var = ((VariableExpression)cond).getVariable();
			sb.append(var.getIdent());
		} else if(cond instanceof Visited) {
			Visited vis = (Visited)cond;
			formatConditionEvalAux(sb, vis.getEntity());
			sb.append(".visited[");
			formatConditionEvalAux(sb, vis.getVisitorID());
			sb.append("]");
		} else {
			sb.append("Unsupported expression type (" + cond + ")");
		}
	}

	private static String formatIdentifiable(Identifiable id)
	{
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}
}
