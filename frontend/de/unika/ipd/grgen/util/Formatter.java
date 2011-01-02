/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ExpressionFormatter.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ir.Cast;
import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.MapAccessExpr;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.MapSizeExpr;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetSizeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.VariableExpression;
import de.unika.ipd.grgen.ir.Visited;
import de.unika.ipd.grgen.ir.Nameof;

public class Formatter {

	/* binary operator symbols of the C-language */
	// ATTENTION: the first two shift operations are signed shifts
	// 		the second right shift is signed. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	private static String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", "!", "~", "-", "(cast)"
	};

	public static String formatConditionEval(Expression cond) {
		StringBuffer sb = new StringBuffer();
		formatConditionEvalAux(sb, cond);
		return sb.toString();
	}

	private static void formatConditionEvalAux(StringBuffer sb, Expression cond) {
		if(cond instanceof Operator) {
			Operator op = (Operator)cond;
			switch (op.arity()) {
				case 1:
					sb.append("(" + opSymbols[op.getOpCode()] + " ");
					formatConditionEvalAux(sb, op.getOperand(0));
					sb.append(")");
					break;
				case 2:
					formatConditionEvalAux(sb, op.getOperand(0));
					sb.append(" " + opSymbols[op.getOpCode()] + " ");
					formatConditionEvalAux(sb, op.getOperand(1));
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						sb.append("(");
						formatConditionEvalAux(sb, op.getOperand(0));
						sb.append(") ? (");
						formatConditionEvalAux(sb, op.getOperand(1));
						sb.append(") : (");
						formatConditionEvalAux(sb, op.getOperand(2));
						sb.append(")");
						break;
					}
					// FALL THROUGH
				default: throw new UnsupportedOperationException("Unsupported Operation arrity (" + op.arity() + ")");
			}
		}
		else if(cond instanceof Qualification) {
			Qualification qual = (Qualification)cond;
			Entity entity = qual.getOwner();

			if(entity instanceof Node) {
				sb.append(formatIdentifiable(entity) + "." + formatIdentifiable(qual.getMember()));
			} else if (entity instanceof Edge) {
				sb.append(formatIdentifiable(entity) + "." + formatIdentifiable(qual.getMember()));
			} else
				throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		}
		else if (cond instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) cond;
			Type type = constant.getType();

			switch (type.classify()) {
				case Type.IS_STRING: //emit C-code for string constants
					sb.append("'" +constant.getValue() + "'");
					break;
				case Type.IS_BOOLEAN: //emit C-code for boolean constans
					Boolean bool_const = (Boolean) constant.getValue();
					if ( bool_const.booleanValue() )
						sb.append("true"); /* true-value */
					else
						sb.append("false"); /* false-value */
					break;
				case Type.IS_INTEGER: //emit C-code for integer constants
					sb.append(constant.getValue().toString()); /* this also applys to enum constants */
			}
		}
		else if(cond instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) cond;
			sb.append("ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		}
		else if(cond instanceof Typeof) {
			Typeof to = (Typeof)cond;
			sb.append(formatIdentifiable(to.getEntity()) + ".type");
		}
		else if(cond instanceof Cast) {
			Cast cast = (Cast) cond;
			Type type = cast.getType();

			if(type.classify() == Type.IS_STRING) {
				formatConditionEvalAux(sb, cast.getExpression());
				sb.append(".ToString()");
			}
			else {
				String typeName = "";

				switch(type.classify()) {
					case Type.IS_INTEGER: typeName = "int"; break;
					case Type.IS_FLOAT: typeName = "float"; break;
					case Type.IS_DOUBLE: typeName = "double"; break;
					case Type.IS_BOOLEAN: typeName = "bool"; break;
					default:
						throw new UnsupportedOperationException(
							"This is either a forbidden cast, which should have been " +
							"rejected on building the IR, or an allowed cast, which " +
							"should have been processed by the above code.");
				}

				sb.append("((" + typeName  + ") ");
				formatConditionEvalAux(sb, cast.getExpression());
				sb.append(")");
			}
		}
		else if(cond instanceof VariableExpression) {
			Variable var = ((VariableExpression) cond).getVariable();
			sb.append(var.getIdent());
		}
		else if(cond instanceof Visited) {
			Visited vis = (Visited) cond;
			sb.append("visited(" + formatIdentifiable(vis.getEntity()) + ", ");
			formatConditionEvalAux(sb, vis.getVisitorID());
			sb.append(")");
		}
		else if(cond instanceof Nameof) {
			// TODO
		}
		else if(cond instanceof MapAccessExpr) {
			// MAP TODO
		}
		else if(cond instanceof MapSizeExpr) {
			// MAP TODO
		}
		else if(cond instanceof MapInit) {
			// MAP TODO
		}
		else if(cond instanceof SetSizeExpr) {
			// MAP TODO
		}
		else if(cond instanceof SetInit) {
			// MAP TODO
		}
		else if(cond instanceof GraphEntityExpression) {
			// MAP TODO
		}
		else if(cond instanceof ExternalFunctionInvocationExpr) {
			// TODO
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + cond + ")");
	}

	private static String formatIdentifiable(Identifiable id) {
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}
}

