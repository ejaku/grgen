/**
 * ExpressionFormatter.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ir.*;

public class Formatter {
	
	/* binary operator symbols of the C-language */
	// ATTENTION: the forst two shift operations are signed shifts
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
		else throw new UnsupportedOperationException("Unsupported expression type (" + cond + ")");
	}
	
	private static String formatIdentifiable(Identifiable id) {
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}
}

