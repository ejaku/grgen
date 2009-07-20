/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
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

import java.io.File;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.ir.BooleanType;
import de.unika.ipd.grgen.ir.Cast;
import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.DoubleType;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.FloatType;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.IntType;
import de.unika.ipd.grgen.ir.MapAccessExpr;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.MapItem;
import de.unika.ipd.grgen.ir.MapDomainExpr;
import de.unika.ipd.grgen.ir.MapRangeExpr;
import de.unika.ipd.grgen.ir.MapSizeExpr;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetItem;
import de.unika.ipd.grgen.ir.SetSizeExpr;
import de.unika.ipd.grgen.ir.MapType;
import de.unika.ipd.grgen.ir.SetType;
import de.unika.ipd.grgen.ir.MemberExpression;
import de.unika.ipd.grgen.ir.Nameof;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.ObjectType;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.StringIndexOf;
import de.unika.ipd.grgen.ir.StringLastIndexOf;
import de.unika.ipd.grgen.ir.StringLength;
import de.unika.ipd.grgen.ir.StringReplace;
import de.unika.ipd.grgen.ir.StringSubstring;
import de.unika.ipd.grgen.ir.StringType;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.VariableExpression;
import de.unika.ipd.grgen.ir.Visited;
import de.unika.ipd.grgen.ir.VoidType;

import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;

public abstract class CSharpBase {
	public interface ExpressionGenerationState {
		Map<Expression, String> mapExprToTempVar();
		boolean useVarForMapResult();
	}

	public CSharpBase(String nodeTypePrefix, String edgeTypePrefix) {
		this.nodeTypePrefix = nodeTypePrefix;
		this.edgeTypePrefix = edgeTypePrefix;
	}

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

	public void genVarTypeSet(StringBuffer sb, Collection<? extends Entity> set, boolean brackets) {
		if (brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof("+ formatAttributeType(id) + "))");
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
			throw new IllegalArgumentException("Unknown type " + type + " (" + type.getClass() + ")");
	}

	public String formatNodeOrEdge(Entity ent) {
		if (ent instanceof Node)
			return "Node";
		else if (ent instanceof Edge)
			return "Edge";
		else
			throw new IllegalArgumentException("Illegal entity type " + ent + " (" + ent.getClass() + ")");
	}

	public String getNodeOrEdgeTypePrefix(Type type) {
		if (type instanceof NodeType)
			return nodeTypePrefix;
		else if (type instanceof EdgeType)
			return edgeTypePrefix;
		else
			throw new IllegalArgumentException("Unknown type " + type + " (" + type.getClass() + ")");
	}

	public String getNodeOrEdgeTypePrefix(Entity ent) {
		if (ent instanceof Node)
			return nodeTypePrefix;
		else if (ent instanceof Edge)
			return edgeTypePrefix;
		else
			throw new IllegalArgumentException("Illegal entity type " + ent + " (" + ent.getClass() + ")");
	}


	String matchType(PatternGraph patternGraph, boolean isSubpattern, String pathPrefix) {
		String matchClassContainer;
		if(isSubpattern) {
			matchClassContainer = "Pattern_" + patternGraph.getNameOfGraph();
		} else {
			matchClassContainer = "Rule_" + patternGraph.getNameOfGraph();
		}
		String nameOfMatchClass = "Match_" + pathPrefix + patternGraph.getNameOfGraph();
		return matchClassContainer + "." + nameOfMatchClass;
	}

	public String formatTypeClassName(Type type) {
		return formatNodeOrEdge(type) + "Type_" + formatIdentifiable(type);
	}

	public String formatTypeClassRef(Type type) {
		return "GRGEN_MODEL." + formatTypeClassName(type);
	}

	public String formatElementClassRaw(Type type) {
		return getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
	}

	public String formatElementClassName(Type type) {
		return "@" + formatElementClassRaw(type);
	}

	public String formatElementClassRef(Type type) {
		return "GRGEN_MODEL." + formatElementClassName(type);
	}

	public String formatElementInterfaceRef(Type type) {
		if(!(type instanceof InheritanceType)) {
			assert(false);
			return getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
		}

		InheritanceType nodeEdgeType = (InheritanceType)type;
		String ident = formatIdentifiable(type);
		if(nodeEdgeType.isAbstract()) {
			if(ident == "AEdge") return "GRGEN_LIBGR.IEdge";
		}
		else if(ident == "Node") return "GRGEN_LIBGR.INode";
		else if(ident == "Edge" || ident == "UEdge") return "GRGEN_LIBGR.IEdge";

		return "GRGEN_MODEL.I" + formatElementClassRaw(type);
	}

	public String formatVarDeclWithCast(String type, String varName) {
		return type + " " + varName + " = (" + type + ") ";
	}

	public String formatNodeAssign(Node node, Collection<Node> extractNodeAttributeObject) {
		if(extractNodeAttributeObject.contains(node))
			return formatVarDeclWithCast(formatElementClassRef(node.getType()), formatEntity(node));
		else
			return "LGSPNode " + formatEntity(node) + " = ";
	}

	public String formatEdgeAssign(Edge edge, Collection<Edge> extractEdgeAttributeObject) {
		if(extractEdgeAttributeObject.contains(edge))
			return formatVarDeclWithCast(formatElementClassRef(edge.getType()), formatEntity(edge));
		else
			return "LGSPEdge " + formatEntity(edge) + " = ";
	}

	public String formatAttributeType(Type t) {
		if (t instanceof IntType)
			return "int";
		else if (t instanceof BooleanType)
			return "bool";
		else if (t instanceof FloatType)
			return "float";
		else if (t instanceof DoubleType)
			return "double";
		else if (t instanceof StringType)
			return "string";
		else if (t instanceof EnumType)
			return "GRGEN_MODEL.ENUM_" + formatIdentifiable(t);
		else if (t instanceof ObjectType || t instanceof VoidType)
			return "object"; //TODO maybe we need another output type
		else if (t instanceof MapType) {
			MapType mapType = (MapType) t;
			return "Dictionary<" + formatAttributeType(mapType.getKeyType())
					+ ", " + formatAttributeType(mapType.getValueType()) + ">";
		}
		else if (t instanceof SetType) {
			SetType setType = (SetType) t;
			return "Dictionary<" + formatAttributeType(setType.getValueType())
					+ ", GRGEN_LIBGR.SetValueType>";
		}
		else throw new IllegalArgumentException("Illegal type: " + t);
	}

	public String formatAttributeType(Entity e) {
		return formatAttributeType(e.getType());
	}

	public String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatIdentifiable(e);
	}

	public String formatEntity(Entity entity) {
		return formatEntity(entity, "");
	}

	public String formatEntity(Entity entity, String pathPrefix) {
		if(entity instanceof Node) {
			return pathPrefix + "node_" + formatIdentifiable(entity);
		}
		else if (entity instanceof Edge) {
			return pathPrefix + "edge_" + formatIdentifiable(entity);
		}
		else if (entity instanceof Variable) {
			return pathPrefix + "var_" + formatIdentifiable(entity);
		}
		else {
			throw new IllegalArgumentException("Unknown entity " + entity + " (" + entity.getClass() + ")");
		}
	}

	public String formatEntity(Entity entity, String pathPrefix,
							   HashMap<Entity, String> alreadyDefinedEntityToName) {
		if(alreadyDefinedEntityToName!=null && alreadyDefinedEntityToName.get(entity)!=null)
			return alreadyDefinedEntityToName.get(entity);
		return formatEntity(entity, pathPrefix);
	}

	public String formatInt(int i) {
		return (i == Integer.MAX_VALUE) ? "int.MaxValue" : new Integer(i).toString();
	}

	public String formatLong(long l) {
		return (l == Long.MAX_VALUE) ? "long.MaxValue" : new Long(l).toString();
	}

	public void genBinOpDefault(StringBuffer sb, Operator op, ExpressionGenerationState modifyGenerationState) {
		sb.append("(");
		genExpression(sb, op.getOperand(0), modifyGenerationState);
		sb.append(" " + opSymbols[op.getOpCode()] + " ");
		genExpression(sb, op.getOperand(1), modifyGenerationState);
		sb.append(")");
	}

	public strictfp void genExpression(StringBuffer sb, Expression expr,
			ExpressionGenerationState modifyGenerationState) {
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			switch (op.arity()) {
				case 1:
					sb.append("(" + opSymbols[op.getOpCode()] + " ");
					genExpression(sb, op.getOperand(0), modifyGenerationState);
					sb.append(")");
					break;
				case 2:
					switch(op.getOpCode())
					{
						case Operator.IN:
							genExpression(sb, op.getOperand(1), modifyGenerationState);
							sb.append(".ContainsKey(");
							genExpression(sb, op.getOperand(0), modifyGenerationState);
							sb.append(")");
							break;

						case Operator.BIT_OR:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryHelper.Union(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						case Operator.BIT_AND:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryHelper.Intersect(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						case Operator.EXCEPT:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryHelper.Except(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						default:
							genBinOpDefault(sb, op, modifyGenerationState);
							break;
					}
					break;
				case 3:
					if(op.getOpCode()==Operator.COND) {
						sb.append("((");
						genExpression(sb, op.getOperand(0), modifyGenerationState);
						sb.append(") ? (");
						genExpression(sb, op.getOperand(1), modifyGenerationState);
						sb.append(") : (");
						genExpression(sb, op.getOperand(2), modifyGenerationState);
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
			genQualAccess(sb, qual, modifyGenerationState);
		}
		else if(expr instanceof MemberExpression) {
			MemberExpression memberExp = (MemberExpression) expr;
			genMemberAccess(sb, memberExp.getMember());
		}
		else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression) expr;
			sb.append("GRGEN_MODEL.ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		}
		else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) expr;
			sb.append(getValueAsCSSharpString(constant));
		}
		else if(expr instanceof Nameof) {
			Nameof no = (Nameof) expr;
			if(no.getEntity()==null) {
				sb.append("graph.Name"); // name of graph
			}
			else {
				sb.append("graph.GetElementName(" + formatEntity(no.getEntity()) + ")"); // name of entity
			}
		}
		else if(expr instanceof Typeof) {
			Typeof to = (Typeof) expr;
			sb.append(formatEntity(to.getEntity()) + ".type");
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			String typeName = getTypeNameForCast(cast);

			if(typeName == "string") {
				genExpression(sb, cast.getExpression(), modifyGenerationState);
				sb.append(".ToString()");
			}
			else if(typeName == "object") {
				// no cast needed
				genExpression(sb, cast.getExpression(), modifyGenerationState);
			}
			else {
				sb.append("((" + typeName  + ") ");
				genExpression(sb, cast.getExpression(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if(expr instanceof VariableExpression) {
			Variable var = ((VariableExpression) expr).getVariable();
			sb.append("var_" + var.getIdent());
		}
		else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression) expr).getGraphEntity();
			sb.append(formatEntity(ent));
		}
		else if(expr instanceof Visited) {
			Visited vis = (Visited) expr;
			sb.append("graph.IsVisited(" + formatEntity(vis.getEntity()) + ", ");
			genExpression(sb, vis.getVisitorID(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof StringLength) {
			StringLength strlen = (StringLength) expr;
			sb.append("(");
			genExpression(sb, strlen.getStringExpr(), modifyGenerationState);
			sb.append(").Length");
		}
		else if (expr instanceof StringSubstring) {
			StringSubstring strsubstr = (StringSubstring) expr;
			sb.append("(");
			genExpression(sb, strsubstr.getStringExpr(), modifyGenerationState);
			sb.append(").Substring(");
			genExpression(sb, strsubstr.getStartExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, strsubstr.getLengthExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof StringIndexOf) {
			StringIndexOf strio = (StringIndexOf) expr;
			sb.append("(");
			genExpression(sb, strio.getStringExpr(), modifyGenerationState);
			sb.append(").IndexOf(");
			genExpression(sb, strio.getStringToSearchForExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof StringLastIndexOf) {
			StringLastIndexOf strlio = (StringLastIndexOf) expr;
			sb.append("(");
			genExpression(sb, strlio.getStringExpr(), modifyGenerationState);
			sb.append(").LastIndexOf(");
			genExpression(sb, strlio.getStringToSearchForExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof StringReplace) {
			StringReplace strrepl = (StringReplace) expr;
			sb.append("((");
			genExpression(sb, strrepl.getStringExpr(), modifyGenerationState);
			sb.append(").Substring(0, ");
			genExpression(sb, strrepl.getStartExpr(), modifyGenerationState);
			sb.append(") + ");
			genExpression(sb, strrepl.getReplaceStrExpr(), modifyGenerationState);
			sb.append(" + (");
			genExpression(sb, strrepl.getStringExpr(), modifyGenerationState);
			sb.append(").Substring(");
			genExpression(sb, strrepl.getStartExpr(), modifyGenerationState);
			sb.append(" + ");
			genExpression(sb, strrepl.getLengthExpr(), modifyGenerationState);
			sb.append("))");
		}
		else if (expr instanceof MapAccessExpr) {
			MapAccessExpr ma = (MapAccessExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ma));
			}
			else {
				sb.append("(");
				genExpression(sb, ma.getTargetExpr(), modifyGenerationState);
				sb.append("[");
				genExpression(sb, ma.getKeyExpr(), modifyGenerationState);
				sb.append("])");
			}
		}
		else if (expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ms));
			}
			else {
				sb.append("(");
				genExpression(sb, ms.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof MapDomainExpr) {
			MapDomainExpr md = (MapDomainExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(md));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryHelper.Domain(");
				genExpression(sb, md.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof MapRangeExpr) {
			MapRangeExpr mr = (MapRangeExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mr));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryHelper.Range(");
				genExpression(sb, mr.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ss));
			}
			else {
				sb.append("(");
				genExpression(sb, ss.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof MapInit) {
			MapInit mi = (MapInit)expr;
			if(mi.isConstant()) {
				sb.append(mi.getAnonymnousMapName());
			} else {
				sb.append("fill_" + mi.getAnonymnousMapName() + "(");
				boolean first = true;
				for(MapItem item : mi.getMapItems()) {
					if(first) {
						genExpression(sb, item.getKeyExpr(), modifyGenerationState);
						sb.append(", ");
						genExpression(sb, item.getValueExpr(), modifyGenerationState);
						first = false;
					} else {
						sb.append(", ");
						genExpression(sb, item.getKeyExpr(), modifyGenerationState);
						sb.append(", ");
						genExpression(sb, item.getValueExpr(), modifyGenerationState);
					}
				}
				sb.append(")");
			}
		}
		else if (expr instanceof SetInit) {
			SetInit si = (SetInit)expr;
			if(si.isConstant()) {
				sb.append(si.getAnonymnousSetName());
			} else {
				sb.append("fill_" + si.getAnonymnousSetName() + "(");
				boolean first = true;
				for(SetItem item : si.getSetItems()) {
					if(first) {
						genExpression(sb, item.getValueExpr(), modifyGenerationState);
						first = false;
					} else {
						sb.append(", ");
						genExpression(sb, item.getValueExpr(), modifyGenerationState);
					}
				}
				sb.append(")");
			}
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
	}

	protected String getValueAsCSSharpString(Constant constant)
	{
		Type type = constant.getType();

		switch (type.classify()) {
			case Type.IS_STRING: //emit C-code for string constants
				Object value = constant.getValue();
				if(value == null)
					return "null";
				else
					return "\"" + constant.getValue() + "\"";
			case Type.IS_BOOLEAN: //emit C-code for boolean constans
				Boolean bool_const = (Boolean) constant.getValue();
				if(bool_const.booleanValue())
					return "true"; /* true-value */
				else
					return "false"; /* false-value */
			case Type.IS_INTEGER: //emit C-code for integer constants
			case Type.IS_DOUBLE: //emit C-code for double constants
				return constant.getValue().toString();
			case Type.IS_FLOAT: //emit C-code for float constants
				return constant.getValue().toString() + "f"; /* this also applys to enum constants */
			case Type.IS_TYPE: //emit code for type constants
				InheritanceType it = (InheritanceType) constant.getValue();
				return formatTypeClassRef(it) + ".typeVar";
			case Type.IS_OBJECT: // If value is not null throw Exc
				if(constant.getValue() == null) {
					return "null";
				}
			default:
				throw new UnsupportedOperationException("unsupported type");
		}
	}

	protected String getTypeNameForCast(Cast cast)
	{
		Type type = cast.getType();

		String typeName = "";

		switch(type.classify()) {
			case Type.IS_STRING: typeName = "string"; break;
			case Type.IS_INTEGER: typeName = "int"; break;
			case Type.IS_FLOAT: typeName = "float"; break;
			case Type.IS_DOUBLE: typeName = "double"; break;
			case Type.IS_BOOLEAN: typeName = "bool"; break;
			case Type.IS_OBJECT: typeName = "object"; break;
			default:
				throw new UnsupportedOperationException(
					"This is either a forbidden cast, which should have been " +
						"rejected on building the IR, or an allowed cast, which " +
						"should have been processed by the above code.");
		}

		return typeName;
	}

	protected String escapeDoubleQuotes(String input)
	{
		StringBuffer sb = new StringBuffer(input.length()+2);
		for(int i=0; i<input.length(); ++i) {
			if(input.charAt(i)=='"') {
				sb.append("\\\"");
			} else {
				sb.append(input.charAt(i));
			}
		}
		return sb.toString();
	}

	protected abstract void genQualAccess(StringBuffer sb, Qualification qual, Object modifyGenerationState);
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
			"-", "*", "/", "%", "!", "~", "-"
	};

	private String nodeTypePrefix;
	private String edgeTypePrefix;
}
