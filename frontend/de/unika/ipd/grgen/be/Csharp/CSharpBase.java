/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


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
import java.util.HashMap;
import java.util.Iterator;

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

	public void genEntitySet(StringBuffer sb, Collection<? extends Entity> set, String pre, String post,
							 boolean brackets, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append(pre + formatEntity(id, pathPrefix, alreadyDefinedEntityToName) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}

	public void genSubpatternUsageSet(StringBuffer sb, Collection<? extends SubpatternUsage> set, String pre, String post,
									  boolean brackets, String pathPrefix, HashMap<? extends Identifiable, String> alreadyDefinedIdentifiableToName) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends SubpatternUsage> iter = set.iterator(); iter.hasNext();) {
			SubpatternUsage spu = iter.next();
			sb.append(pre + formatIdentifiable(spu, pathPrefix, alreadyDefinedIdentifiableToName) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if (brackets)
			sb.append(" }");
	}

	public void genAlternativesSet(StringBuffer sb, Collection<? extends Rule> set,
								   String pre, String post, boolean brackets) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Rule> iter = set.iterator(); iter.hasNext();) {
			Rule altCase = iter.next();
			PatternGraph altCasePattern = altCase.getLeft();
			sb.append(pre + altCasePattern.getNameOfGraph() + post);
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

	public String formatIdentifiable(Identifiable id, String pathPrefix) {
		String ident = id.getIdent().toString();
		return pathPrefix+ident.replace('$', '_');
	}

	public String formatIdentifiable(Identifiable id, String pathPrefix,
									 HashMap<? extends Identifiable, String> alreadyDefinedIdentifiableToName) {
		if(alreadyDefinedIdentifiableToName!=null && alreadyDefinedIdentifiableToName.get(id)!=null)
			return alreadyDefinedIdentifiableToName.get(id);
		String ident = id.getIdent().toString();
		return pathPrefix+ident.replace('$', '_');
	}

	public String formatNodeOrEdge(boolean isNode) {
		return isNode ? "Node" : "Edge";
	}

	public String formatNodeOrEdge(Type type) {
		if (type instanceof NodeType)
			return "Node";
		else if (type instanceof EdgeType)
			return "Edge";
		else
			throw new IllegalArgumentException("Unknown type" + type + "(" + type.getClass() + ")");
	}

	public String formatNodeOrEdge(Entity ent) {
		if (ent instanceof Node)
			return "Node";
		else if (ent instanceof Edge)
			return "Edge";
		else
			throw new IllegalArgumentException("Illegal entity type" + ent + "(" + ent.getClass() + ")");
	}

	public String formatTypeClass(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatIdentifiable(type);
	}

	public String formatElementClass(Type type) {
		return formatNodeOrEdge(type) + "_" + formatIdentifiable(type);
	}

	public String formatVarDeclWithCast(Type type, String typePrefix, String varName) {
		String ctype = typePrefix + formatElementClass(type);
		return ctype + " " + varName + " = (" + ctype + ") ";
	}

	public String formatNodeAssign(Node node, Collection<Node> extractNodeAttributeObject) {
		if(extractNodeAttributeObject.contains(node))
			return formatVarDeclWithCast(node.getType(), "", formatEntity(node));
		else
			return "LGSPNode " + formatEntity(node) + " = ";
	}

	public String formatEdgeAssign(Edge edge, Collection<Edge> extractEdgeAttributeObject) {
		if(extractEdgeAttributeObject.contains(edge))
			return formatVarDeclWithCast(edge.getType(), "", formatEntity(edge));
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
		else if (t instanceof ObjectType || t instanceof VoidType)
			return "Object"; //TODO maybe we need another output type
		else throw new IllegalArgumentException("Unknown Entity: " + e + "(" + t + ")");
	}

	public String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatIdentifiable(e);
	}

	public String formatEntity(Entity entity) {
		return formatEntity(entity, "");
	}

	public String formatEntity(Entity entity, String pathPrefix) {
		if(entity instanceof Node) {
			return pathPrefix+"node_"+formatIdentifiable(entity);
		}
		else if (entity instanceof Edge) {
			return pathPrefix+"edge_"+formatIdentifiable(entity);
		}
		else if (entity instanceof Variable) {
			//TODO NYI
			System.err.println("formatEntity(): TODO NYI Variable " + entity +"  TODO");
			return "/* TODO NYI Variable TODO */";
		}
		else {
			throw new IllegalArgumentException("Unknown entity " + entity + " (" + entity.getClass() + ")");
		}
	}

	public String formatEntity(Entity entity, String pathPrefix,
							   HashMap<Entity, String> alreadyDefinedEntityToName) {
		if(entity instanceof Node) {
			if(alreadyDefinedEntityToName!=null && alreadyDefinedEntityToName.get(entity)!=null)
				return alreadyDefinedEntityToName.get(entity);
			return pathPrefix+"node_"+formatIdentifiable(entity);
		}
		else if (entity instanceof Edge) {
			if(alreadyDefinedEntityToName!=null && alreadyDefinedEntityToName.get(entity)!=null)
				return alreadyDefinedEntityToName.get(entity);
			return pathPrefix+"edge_"+formatIdentifiable(entity);
		}
		else {
			throw new IllegalArgumentException("Unknown entity" + entity + "(" + entity.getClass() + ")");
		}
	}

	public String formatInt(int i) {
		return (i == Integer.MAX_VALUE) ? "int.MaxValue" : new Integer(i).toString();
	}

	public String formatLong(long l) {
		return (l == Long.MAX_VALUE) ? "long.MaxValue" : new Long(l).toString();
	}

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
			genQualAccess(sb, qual);
		}
		else if(expr instanceof MemberExpression) {
			MemberExpression memberExp = (MemberExpression) expr;
			genMemberAccess(sb, memberExp.getMember());
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
				case Type.IS_OBJECT: // If value is not null throw Exc
					if(constant.getValue() == null) {
						sb.append("null");
						break;
					}
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

	protected abstract void genQualAccess(StringBuffer sb, Qualification qual);
	protected abstract void genMemberAccess(StringBuffer sb, Entity member);

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

	// TODO use or remove it
	// private HashSet<Node> nodesNeededAsAttributes = new LinkedHashSet<Node>();
	// private HashSet<Edge> edgesNeededAsAttributes = new LinkedHashSet<Edge>();
}

