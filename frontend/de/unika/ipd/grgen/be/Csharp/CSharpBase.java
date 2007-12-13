/**
 * CSharpBase.java
 *
 * Auxiliary routines used for the CSharp backends.
 *
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.be.Csharp;

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;
import java.io.File;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashSet;

public abstract class CSharpBase {
	/**
	 * Write a character sequence to a file using the given path.
	 * @param path The path for the file.
	 * @param filename The filename.
	 * @param cs A character sequence.
	 */
	public void writeFile(File path, String filename, CharSequence cs) {
		Util.writeFile(new File(path, filename), cs, Base.error);
	}

	/**
	 * Dumps a C-like set representation.
	 */
	public void genSet(StringBuffer sb, Collection<? extends Identifiable> set, String pre, String post, boolean brackets) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Identifiable> iter = set.iterator(); iter.hasNext();) {
			Identifiable id = iter.next();
			sb.append(pre + formatIdentifiable(id) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}

	public void genEntitySet(StringBuffer sb, Collection<? extends Entity> set, String pre, String post, boolean brackets,
			PatternGraph outer, int negCount) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append(pre + formatEntity(id, outer, negCount) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}

	public String formatIdentifiable(Identifiable id) {
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}

	public String formatNodeOrEdge(boolean isNode) {
		if(isNode)
			return "Node";
		else
			return "Edge";
	}

	public String formatNodeOrEdge(Type type) {
		if (type instanceof NodeType)
			return formatNodeOrEdge(true);
		else if (type instanceof EdgeType)
			return formatNodeOrEdge(false);
		else
			throw new IllegalArgumentException("Unknown type" + type + "(" + type.getClass() + ")");
	}

	public String formatTypeClass(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatIdentifiable(type);
	}

	public String formatElementClass(Type type) {
		return formatNodeOrEdge(type) + "_" + formatIdentifiable(type);
	}

	public String formatCastedAssign(Type type, String typePrefix, String varName) {
		String ctype = typePrefix + formatElementClass(type);
		return ctype + " " + varName + " = (" + ctype + ") ";
	}

	public String formatNodeAssign(Node node, Collection<Node> extractNodeAttributeObject) {
		if(extractNodeAttributeObject.contains(node))
			return formatCastedAssign(node.getType(), "", formatEntity(node));
		else
			return "LGSPNode " + formatEntity(node) + " = ";
	}

	public String formatEdgeAssign(Edge edge, Collection<Edge> extractEdgeAttributeObject) {
		if(extractEdgeAttributeObject.contains(edge))
			return formatCastedAssign(edge.getType(), "", formatEntity(edge));
		else
			return "LGSPEdge " + formatEntity(edge) + " = ";
	}

	public String formatAttributeType(Entity e) {
		Type t = e.getType();
		if (t instanceof IntType)
			return "int";
		else if (t instanceof BooleanType)
			return "bool";
		else if (t instanceof FloatType)
			return "float";
		else if (t instanceof DoubleType)
			return "double";
		else if (t instanceof StringType)
			return "String";
		else if (t instanceof EnumType)
			return "ENUM_" + formatIdentifiable(e.getType());
		else if (t instanceof ObjectType)
			return "Object"; //TODO maybe we need another output type
		else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
	}

	public String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatIdentifiable(e);
	}

	public String formatEntity(Entity entity, PatternGraph outer, int negCount) {
		if(entity instanceof Node) {
			return ( (outer !=null && !outer.getNodes().contains(entity)) ? "neg_" + negCount + "_" : "")
				+ "node_" + formatIdentifiable(entity);
		}
		else if (entity instanceof Edge) {
			return ( (outer !=null && !outer.getEdges().contains(entity)) ? "neg_" + negCount + "_" : "")
				+ "edge_" + formatIdentifiable(entity);
		}
		else
			throw new IllegalArgumentException("Unknown entity" + entity + "(" + entity.getClass() + ")");
	}

	public String formatEntity(Entity entity) {
		return formatEntity(entity, null, 0);
	}

	public String formatInt(int i) {
		return (i == Integer.MAX_VALUE) ? "int.MaxValue" : new Integer(i).toString();
	}

	/*	public void collectNeededAttributes(Expression expr) {
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			switch(op.arity()) {
				case 3:
					collectNeededAttributes(op.getOperand(2));
					// FALLTHROUGH
				case 2:
					collectNeededAttributes(op.getOperand(1));
					// FALLTHROUGH
				case 1:
					collectNeededAttributes(op.getOperand(0));
					break;
			}
		}
		else if(expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			GraphEntity entity = (GraphEntity) qual.getOwner();
			if(entity instanceof Node)
				nodesNeededAsAttributes.add((Node) entity);
			else if(entity instanceof Edge)
				edgesNeededAsAttributes.add((Edge) entity);

			HashSet<Entity> neededAttrs = neededAttributes.get(entity);
			if(neededAttrs == null) {
				neededAttributes.put(entity, neededAttrs = new LinkedHashSet<Entity>());
			}
			neededAttrs.add(qual.getMember());
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			collectNeededAttributes(cast.getExpression());
		}
	 }*/

	public strictfp void genExpression(StringBuffer sb, Expression expr) {
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			switch (op.arity()) {
				case 1:
					sb.append("(" + opSymbols[op.getOpCode()] + " ");
					genExpression(sb, op.getOperand(0));
					sb.append(")");
					break;
				case 2:
					sb.append("(");
					genExpression(sb, op.getOperand(0));
					sb.append(" " + opSymbols[op.getOpCode()] + " ");
					genExpression(sb, op.getOperand(1));
					sb.append(")");
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						sb.append("((");
						genExpression(sb, op.getOperand(0));
						sb.append(") ? (");
						genExpression(sb, op.getOperand(1));
						sb.append(") : (");
						genExpression(sb, op.getOperand(2));
						sb.append("))");
						break;
					}
					// FALLTHROUGH
				default:
					throw new UnsupportedOperationException(
						"Unsupported operation arity (" + op.arity() + ")");
			}
		}
		else if(expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			Entity entity = qual.getOwner();
			genQualAccess(entity, sb, qual);
		}
		else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) expr;
			sb.append("ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		}
		else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) expr;
			Type type = constant.getType();

			switch (type.classify()) {
				case Type.IS_STRING: //emit C-code for string constants
					sb.append("\"" + constant.getValue() + "\"");
					break;
				case Type.IS_BOOLEAN: //emit C-code for boolean constans
					Boolean bool_const = (Boolean) constant.getValue();
					if(bool_const.booleanValue())
						sb.append("true"); /* true-value */
					else
						sb.append("false"); /* false-value */
					break;
				case Type.IS_INTEGER: //emit C-code for integer constants
				case Type.IS_DOUBLE: //emit C-code for double constants
					sb.append(constant.getValue().toString());
					break;
				case Type.IS_FLOAT: //emit C-code for float constants
					sb.append(constant.getValue().toString()); /* this also applys to enum constants */
					sb.append('f');
					break;
				case Type.IS_TYPE: //emit code for type constants
					InheritanceType it = (InheritanceType) constant.getValue();
					sb.append(formatTypeClass(it) + ".typeVar");
					break;
				default:
					throw new UnsupportedOperationException("unsupported type");
			}
		}
		else if(expr instanceof Typeof) {
			Typeof to = (Typeof)expr;
			sb.append(formatEntity(to.getEntity()) + ".type");
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			Type type = cast.getType();

			if(type.classify() == Type.IS_STRING) {
				genExpression(sb, cast.getExpression());
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
				genExpression(sb, cast.getExpression());
				sb.append(")");
			}
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
	}

	protected abstract void genQualAccess(Entity entity, StringBuffer sb, Qualification qual);

	/*	private void genQualAccess(Entity entity, StringBuffer sb, Qualification qual, boolean inRewriteModify) {
		Entity member = qual.getMember();
		if(inRewriteModify) {
			if(accessViaVariable((GraphEntity) entity, member)) {
				sb.append("var_" + formatEntity(entity) + "_" + formatIdentifiable(member));
			}
			else {
				if(entity instanceof Node) {
					if(!newOrRetypedNodes.contains(entity))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else if(entity instanceof Edge) {
					if(!newOrRetypedEdges.contains(entity))		// element extracted from match?
						sb.append("i");							// yes, attributes only accessible via interface
				}
				else
					throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");

				sb.append(formatEntity(entity) + ".@" + formatIdentifiable(member));
			}
		}
		else {	 // in genConditions()
			sb.append("((I" + (entity instanceof Node ? "Node" : "Edge") + "_" +
					formatIdentifiable(entity.getType()) + ") ");
			sb.append(formatEntity(entity) + ").@" + formatIdentifiable(member));
		}
	 }*/

	///////////////////////
	// Private variables //
	///////////////////////

	/* binary operator symbols of the C-language */
	// ATTENTION: the first two shift operations are signed shifts,
	// 		the second right shift is unsigned. This Backend simply gens
	//		C-bitwise-shift-operations on signed integers, for simplicity ;-)
	// TODO: Check whether this is correct...
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", "!", "~", "-", "(cast)"
	};

	private HashSet<Node> nodesNeededAsAttributes = new LinkedHashSet<Node>();
	private HashSet<Edge> edgesNeededAsAttributes = new LinkedHashSet<Edge>();
}

