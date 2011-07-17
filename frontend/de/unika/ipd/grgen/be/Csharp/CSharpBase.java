/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * CSharpBase.java
 *
 * Auxiliary routines used for the CSharp backends.
 *
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: CSharpBase.java 26976 2010-10-11 00:11:23Z eja $
 */

package de.unika.ipd.grgen.be.Csharp;

import java.io.File;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;

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

	public boolean existsFile(File path, String filename) {
		return new File(path, filename).exists();
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

		if(type instanceof ExternalType) {
			return "GRGEN_MODEL." + type.getIdent().toString();
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
		if (t instanceof ByteType)
			return "sbyte";
		if (t instanceof ShortType)
			return "short";
		if (t instanceof IntType)
			return "int";
		if (t instanceof LongType)
			return "long";
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
			return "Dictionary<" + formatType(mapType.getKeyType())
					+ ", " + formatType(mapType.getValueType()) + ">";
		}
		else if (t instanceof SetType) {
			SetType setType = (SetType) t;
			return "Dictionary<" + formatType(setType.getValueType())
					+ ", GRGEN_LIBGR.SetValueType>";
		}
		else if (t instanceof ArrayType) {
			ArrayType arrayType = (ArrayType) t;
			return "List<" + formatType(arrayType.getValueType()) + ">";
		}
		else if (t instanceof ExternalType) {
			ExternalType extType = (ExternalType) t;
			return "GRGEN_MODEL." + extType.getIdent();
		}
		else throw new IllegalArgumentException("Illegal type: " + t);
	}

	public String formatAttributeType(Entity e) {
		return formatAttributeType(e.getType());
	}

	public String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatIdentifiable(e);
	}

	public String formatType(Type type) {
		if(type instanceof InheritanceType) {
			return formatElementInterfaceRef(type);
		} else {
			return formatAttributeType(type);
		}
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
		if(op.getOpCode()==Operator.BIT_SHR)
		{
			sb.append("((int)(((uint)");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(") " + opSymbols[op.getOpCode()] + " ");
			genExpression(sb, op.getOperand(1), modifyGenerationState);
			sb.append("))");
		}
		else
		{
			sb.append("(");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(" " + opSymbols[op.getOpCode()] + " ");
			genExpression(sb, op.getOperand(1), modifyGenerationState);
			sb.append(")");
		}
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
						{
							Type opType = op.getOperand(1).getType();
							genExpression(sb, op.getOperand(1), modifyGenerationState);
							sb.append(opType instanceof ArrayType ? ".Contains(" : ".ContainsKey(");
							if(op.getOperand(0) instanceof GraphEntityExpression)
								sb.append("(" + formatElementInterfaceRef(op.getOperand(0).getType()) + ")(");
							genExpression(sb, op.getOperand(0), modifyGenerationState);
							if(op.getOperand(0) instanceof GraphEntityExpression)
								sb.append(")");
							sb.append(")");
							break;
						}

						case Operator.ADD:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Concatenate(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						case Operator.BIT_OR:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Union(");
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
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Intersect(");
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
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Except(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						case Operator.EQ:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Equal(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.Equal(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							} else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.NE:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.NotEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.NotEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.GT:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.GreaterThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.GreaterThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.GE:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.GreaterOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.LT:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.LessThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.LessThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else  {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.LE:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.LessOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.DictionaryListHelper.LessOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
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
			} else {
				sb.append("graph.GetElementName(" + formatEntity(no.getEntity()) + ")"); // name of entity
			}
		}
		else if(expr instanceof Typeof) {
			Typeof to = (Typeof) expr;
			sb.append(formatEntity(to.getEntity()) + ".lgspType");
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			String typeName = getTypeNameForCast(cast);

			if(typeName == "string") {
				if(cast.getExpression().getType() instanceof MapType || cast.getExpression().getType() instanceof SetType) {
					sb.append("GRGEN_LIBGR.DictionaryListHelper.ToString(");
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(", graph)");
				} else if(cast.getExpression().getType() instanceof ArrayType) {
					sb.append("GRGEN_LIBGR.DictionaryListHelper.ToString(");
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(", graph)");
				} else {
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(".ToString()");
				}
			} else if(typeName == "object") {
				// no cast needed
				genExpression(sb, cast.getExpression(), modifyGenerationState);
			} else {
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
		else if(expr instanceof RandomExpr) {
			RandomExpr re = (RandomExpr) expr;
			if(re.getNumExpr()!=null) {
				sb.append("GRGEN_LIBGR.Sequence.randomGenerator.Next(");
				genExpression(sb, re.getNumExpr(), modifyGenerationState);
			} else {
				sb.append("GRGEN_LIBGR.Sequence.randomGenerator.NextDouble(");
			}
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
		else if (expr instanceof IndexedAccessExpr) {
			IndexedAccessExpr ia = (IndexedAccessExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ia));
			}
			else {
				sb.append("(");
				genExpression(sb, ia.getTargetExpr(), modifyGenerationState);
				sb.append("[");
				if(ia.getKeyExpr() instanceof GraphEntityExpression)
					sb.append("(" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + ")(");
				genExpression(sb, ia.getKeyExpr(), modifyGenerationState);
				if(ia.getKeyExpr() instanceof GraphEntityExpression)
					sb.append(")");
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
				sb.append("GRGEN_LIBGR.DictionaryListHelper.Domain(");
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
				sb.append("GRGEN_LIBGR.DictionaryListHelper.Range(");
				genExpression(sb, mr.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof MapPeekExpr) {
			MapPeekExpr mp = (MapPeekExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mp));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryListHelper.Peek(");
				genExpression(sb, mp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, mp.getNumberExpr(), modifyGenerationState);
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
		else if (expr instanceof SetPeekExpr) {
			SetPeekExpr sp = (SetPeekExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(sp));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryListHelper.Peek(");
				genExpression(sb, sp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, sp.getNumberExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArraySizeExpr) {
			ArraySizeExpr as = (ArraySizeExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(as));
			}
			else {
				sb.append("(");
				genExpression(sb, as.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof ArrayPeekExpr) {
			ArrayPeekExpr ap = (ArrayPeekExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ap));
			}
			else {
				sb.append("(");
				genExpression(sb, ap.getTargetExpr(), modifyGenerationState);
				sb.append("[");
				genExpression(sb, ap.getNumberExpr(), modifyGenerationState);
				sb.append("])");
			}
		}
		else if (expr instanceof ArrayIndexOfExpr) {
			ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ai));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryListHelper.IndexOf(");
				genExpression(sb, ai.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ai.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayLastIndexOfExpr) {
			ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ali));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryListHelper.LastIndexOf(");
				genExpression(sb, ali.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ali.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArraySubarrayExpr) {
			ArraySubarrayExpr asa = (ArraySubarrayExpr)expr;
			if(modifyGenerationState.useVarForMapResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(asa));
			}
			else {
				sb.append("GRGEN_LIBGR.DictionaryListHelper.Subarray(");
				genExpression(sb, asa.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, asa.getStartExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, asa.getLengthExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof MapInit) {
			MapInit mi = (MapInit)expr;
			if(mi.isConstant()) {
				sb.append(mi.getAnonymousMapName());
			} else {
				sb.append("fill_" + mi.getAnonymousMapName() + "(");
				boolean first = true;
				for(MapItem item : mi.getMapItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item.getKeyExpr() instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getKeyExpr().getType()) + ")(");
					genExpression(sb, item.getKeyExpr(), modifyGenerationState);
					if(item.getKeyExpr() instanceof GraphEntityExpression)
						sb.append(")");

					sb.append(", ");

					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getValueExpr().getType()) + ")(");
					genExpression(sb, item.getValueExpr(), modifyGenerationState);
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		}
		else if (expr instanceof SetInit) {
			SetInit si = (SetInit)expr;
			if(si.isConstant()) {
				sb.append(si.getAnonymousSetName());
			} else {
				sb.append("fill_" + si.getAnonymousSetName() + "(");
				boolean first = true;
				for(SetItem item : si.getSetItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getValueExpr().getType()) + ")(");
					genExpression(sb, item.getValueExpr(), modifyGenerationState);
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayInit) {
			ArrayInit ai = (ArrayInit)expr;
			if(ai.isConstant()) {
				sb.append(ai.getAnonymousArrayName());
			} else {
				sb.append("fill_" + ai.getAnonymousArrayName() + "(");
				boolean first = true;
				for(ArrayItem item : ai.getArrayItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getValueExpr().getType()) + ")(");
					genExpression(sb, item.getValueExpr(), modifyGenerationState);
					if(item.getValueExpr() instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		}
		else if (expr instanceof ExternalFunctionInvocationExpr) {
			ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
			sb.append("GRGEN_EXPR.ExternalFunctions." + efi.getExternalFunc().getIdent() + "(");
			for(int i=0; i<efi.arity(); ++i) {
				Expression argument = efi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
				}
				genExpression(sb, argument, modifyGenerationState);
				if(i+1 < efi.arity()) sb.append(", ");
			}
			sb.append(")");
		}
		else if (expr instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr ce = (IncidentEdgeExpr) expr;
			sb.append("graph."+(ce.isOutgoing() ? "Outgoing" : "Incoming")+"("
				+ formatEntity(ce.getNode())+", "
				+ formatTypeClassRef(ce.getIncidentEdgeType()) + ".typeVar, "
				+ formatTypeClassRef(ce.getAdjacentNodeType()) + ".typeVar"
				+ ")");
		}
		else if (expr instanceof MaxExpr) {
			MaxExpr m = (MaxExpr)expr;
			sb.append("Math.Max(");
			genExpression(sb, m.getLeftExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, m.getRightExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof MinExpr) {
			MinExpr m = (MinExpr)expr;
			sb.append("Math.Min(");
			genExpression(sb, m.getLeftExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, m.getRightExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof PowExpr) {
			PowExpr m = (PowExpr)expr;
			sb.append("Math.Pow(");
			genExpression(sb, m.getLeftExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, m.getRightExpr(), modifyGenerationState);
			sb.append(")");
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
	}

	protected String getValueAsCSSharpString(Constant constant)
	{
		Type type = constant.getType();

		//emit C-code for constants
		switch (type.classify()) {
			case Type.IS_STRING: 
				Object value = constant.getValue();
				if(value == null)
					return "null";
				else
					return "\"" + constant.getValue() + "\"";
			case Type.IS_BOOLEAN:
				Boolean bool_const = (Boolean) constant.getValue();
				if(bool_const.booleanValue())
					return "true"; /* true-value */
				else
					return "false"; /* false-value */
			case Type.IS_BYTE:
			case Type.IS_SHORT:
			case Type.IS_INTEGER: /* this also applys to enum constants */
			case Type.IS_DOUBLE:
				return constant.getValue().toString();
			case Type.IS_LONG:
				return constant.getValue().toString() + "l";
			case Type.IS_FLOAT:
				return constant.getValue().toString() + "f";
			case Type.IS_TYPE:
				InheritanceType it = (InheritanceType) constant.getValue();
				return formatTypeClassRef(it) + ".typeVar";
			case Type.IS_OBJECT:
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
			case Type.IS_BYTE: typeName = "sbyte"; break;
			case Type.IS_SHORT: typeName = "short"; break;
			case Type.IS_INTEGER: typeName = "int"; break;
			case Type.IS_LONG: typeName = "long"; break;
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

	protected void addAnnotations(StringBuilder sb, Identifiable ident, String targetName)
	{
		for(String annotationKey : ident.getAnnotations().keySet()) {
			String annotationValue = ident.getAnnotations().get(annotationKey).toString();
			sb.append("\t\t\t" + targetName+ ".Add(\"" +annotationKey + "\", \"" + annotationValue + "\");\n");
		}
	}

	protected void addAnnotations(StringBuffer sb, Identifiable ident, String targetName)
	{
		for(String annotationKey : ident.getAnnotations().keySet()) {
			String annotationValue = ident.getAnnotations().get(annotationKey).toString();
			sb.append("\t\t\t" + targetName+ ".Add(\"" +annotationKey + "\", \"" + annotationValue + "\");\n");
		}
	}

	///////////////////////
	// Private variables //
	///////////////////////

	/* binary operator symbols of the C-language */
	// The first two shift operations are signed shifts, the second right shift is unsigned.
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", "!", "~", "-"
	};

	private String nodeTypePrefix;
	private String edgeTypePrefix;
}
