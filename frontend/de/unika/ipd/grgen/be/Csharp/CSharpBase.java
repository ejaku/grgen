/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Auxiliary routines used for the CSharp backends.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.io.File;
import java.io.IOException;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ir.containers.*;

import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;

public abstract class CSharpBase {
	public interface ExpressionGenerationState {
		Map<Expression, String> mapExprToTempVar();
		boolean useVarForResult();
		Model model();
		boolean isToBeParallelizedActionExisting();

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
	
	public void copyFile(File sourcePath, File targetPath) {
		try {
			Util.copyFile(sourcePath, targetPath);
		} 
		catch(IOException ex) {
			System.out.println(ex.getMessage());
		}
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

	public String getPackagePrefixDot(Identifiable id) {
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn()!=null) {
				return cip.getPackageContainedIn() + ".";
			}
		}
		return "";
	}

	public String getPackagePrefixDoubleColon(Identifiable id) {
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn()!=null) {
				return cip.getPackageContainedIn() + "::";
			}
		}
		return "";
	}

	public String getPackagePrefix(Identifiable id) {
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn()!=null) {
				return cip.getPackageContainedIn();
			}
		}
		return "";
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


	String matchType(PatternGraph patternGraph, Rule subpattern, boolean isSubpattern, String pathPrefix) {
		String matchClassContainer;
		if(isSubpattern) {
			matchClassContainer = getPackagePrefixDot(subpattern) + "Pattern_" + patternGraph.getNameOfGraph();
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
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatTypeClassName(type);
	}

	public String formatTypeClassRefInstance(Type type) {
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatTypeClassName(type) + ".typeVar";
	}

	public String formatElementClassRaw(Type type) {
		return getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
	}

	public String formatElementClassName(Type type) {
		return "@" + formatElementClassRaw(type);
	}

	public String formatElementClassRef(Type type) {
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatElementClassName(type);
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

		return "GRGEN_MODEL." + getPackagePrefixDot(type) + "I" + formatElementClassRaw(type);
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
			return "GRGEN_MODEL." + getPackagePrefixDot(t) + "ENUM_" + formatIdentifiable(t);
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
		else if (t instanceof DequeType) {
			DequeType dequeType = (DequeType) t;
			return "GRGEN_LIBGR.Deque<" + formatType(dequeType.getValueType()) + ">";
		}
		else if (t instanceof GraphType) {
			return "GRGEN_LIBGR.IGraph";
		}
		else if (t instanceof ExternalType) {
			ExternalType extType = (ExternalType) t;
			return "GRGEN_MODEL." + extType.getIdent();
		}
		else if(t instanceof InheritanceType) {
			return formatElementInterfaceRef(t);
		}
		else if(t instanceof MatchType) {
			MatchType matchType = (MatchType) t;
			String actionName = matchType.getAction().getIdent().toString();
			return "Rule_" + actionName + ".IMatch_" + actionName;
		}
		else throw new IllegalArgumentException("Illegal type: " + t);
	}

	public String formatAttributeType(Entity e) {
		return formatAttributeType(e.getType());
	}

	public String formatAttributeTypeName(Entity e) {
		return "AttributeType_" + formatIdentifiable(e);
	}
	
	public String formatFunctionMethodInfoName(FunctionMethod fm, InheritanceType type) {
		return "FunctionMethodInfo_" + formatIdentifiable(fm) + "_" + formatIdentifiable(type);
	}

	public String formatProcedureMethodInfoName(ProcedureMethod pm, InheritanceType type) {
		return "ProcedureMethodInfo_" + formatIdentifiable(pm) + "_" + formatIdentifiable(type);
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
		if(entity.getIdent().toString()=="this") {
			if(entity.getType() instanceof ArrayType)
				return "this_matches";
			else
				return "this";
		}
		else if(entity instanceof Node) {
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
	
	public Entity getAtMostOneNeededNodeOrEdge(NeededEntities needs, List<Entity> parameters) {
		HashSet<GraphEntity> neededEntities = new HashSet<GraphEntity>();
		for(Node node : needs.nodes) {
			if(parameters.indexOf(node)!=-1)
				continue;
			neededEntities.add(node);
		}
		for(Edge edge : needs.edges) {
			if(parameters.indexOf(edge)!=-1)
				continue;
			neededEntities.add(edge);
		}
		if(neededEntities.size() == 1)
			return neededEntities.iterator().next();
		else if(neededEntities.size() > 1)
			throw new UnsupportedOperationException("INTERNAL ERROR, more than one needed entity for index access!");
		return null;
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
							boolean isDictionary = opType instanceof SetType || opType instanceof MapType;
							sb.append(isDictionary ? ".ContainsKey(" : ".Contains(");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else 
								genBinOpDefault(sb, op, modifyGenerationState);
							break;
						}

						case Operator.BIT_OR:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.Union(");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.Intersect(");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.Except(");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof GraphType) {
								sb.append("((GRGEN_LIBGR.IGraph)");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(modifyGenerationState.model().isEqualClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.NE:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof GraphType) {
								sb.append("!((GRGEN_LIBGR.IGraph)");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(modifyGenerationState.model().isEqualClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else {
								genBinOpDefault(sb, op, modifyGenerationState);
							}
							break;
						}

						case Operator.SE:
						{
							sb.append("((GRGEN_LIBGR.IGraph)");
							genExpression(sb, op.getOperand(0), modifyGenerationState);
							sb.append(").HasSameStructure((GRGEN_LIBGR.IGraph)");
							genExpression(sb, op.getOperand(1), modifyGenerationState);
							sb.append(")");
							break;
						}

						case Operator.GT:
						{
							Type opType = op.getOperand(0).getType();
							if(opType instanceof MapType || opType instanceof SetType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof StringType) {
								sb.append("(String.Compare(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")>0)");
							}
							else if(modifyGenerationState.model().isLowerClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
								sb.append("&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append("))");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof StringType) {
								sb.append("(String.Compare(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")>=0)");
							}
							else if(modifyGenerationState.model().isLowerClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof StringType) {
								sb.append("(String.Compare(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")<0)");
							}
							else if(modifyGenerationState.model().isLowerClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
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
								sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof ArrayType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof DequeType) {
								sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
							}
							else if(opType instanceof StringType) {
								sb.append("(String.Compare(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(", ");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")<=0)");
							}
							else if(modifyGenerationState.model().isLowerClassDefined()
									&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
								sb.append("(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append(")");
								sb.append("|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
								genExpression(sb, op.getOperand(0), modifyGenerationState);
								sb.append(",");
								genExpression(sb, op.getOperand(1), modifyGenerationState);
								sb.append("))");
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
			sb.append("GRGEN_MODEL." + getPackagePrefixDot(enumExp.getType()) + "ENUM_" + enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		}
		else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant) expr;
			sb.append(getValueAsCSSharpString(constant));
		}
		else if(expr instanceof Nameof) {
			Nameof no = (Nameof) expr;
			if(no.getNamedEntity()==null) {
				sb.append("GRGEN_LIBGR.GraphHelper.Nameof(null, graph)"); // name of graph
			} else {
            	sb.append("GRGEN_LIBGR.GraphHelper.Nameof(");
				genExpression(sb, no.getNamedEntity(), modifyGenerationState); // name of entity
				sb.append(", graph)");
			}
		}
		else if(expr instanceof Uniqueof) {
			Uniqueof no = (Uniqueof) expr;
			if(no.getEntity()==null)
				sb.append("((GRGEN_LGSP.LGSPGraph)graph).GraphID");
			else
			{
	        	sb.append("(");
	        	if(no.getEntity().getType() instanceof NodeType)
	        		sb.append("(GRGEN_LGSP.LGSPNode)");
	        	else if(no.getEntity().getType() instanceof EdgeType)
	        		sb.append("(GRGEN_LGSP.LGSPEdge)");
	        	else
	        		sb.append("(GRGEN_LGSP.LGSPGraph)");
				genExpression(sb, no.getEntity(), modifyGenerationState); // unique id of entity
				if(no.getEntity().getType() instanceof GraphType)
					sb.append(").GraphID");
				else
					sb.append(").uniqueId");
			}
		}
		else if(expr instanceof ExistsFileExpr) {
			ExistsFileExpr efe = (ExistsFileExpr) expr;
        	sb.append("System.IO.File.Exists((string)");
			genExpression(sb, efe.getPathExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if(expr instanceof ImportExpr) {
			ImportExpr ie = (ImportExpr) expr;
        	sb.append("GRGEN_LIBGR.GraphHelper.Import(");
			genExpression(sb, ie.getPathExpr(), modifyGenerationState);
			sb.append(", graph)");
		}
		else if(expr instanceof CopyExpr) {
			CopyExpr ce = (CopyExpr) expr;
			Type t = ce.getSourceExpr().getType();
			if(t instanceof MatchType) {
	        	sb.append("(("+formatType(t)+")(");
				genExpression(sb, ce.getSourceExpr(), modifyGenerationState);
				sb.append(").Clone())");
			} else {
	        	sb.append("GRGEN_LIBGR.GraphHelper.Copy(");
				genExpression(sb, ce.getSourceExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if(expr instanceof Count) {
			Count count = (Count) expr;
			sb.append("curMatch." + formatIdentifiable(count.getIterated()) + ".Count");
		}
		else if(expr instanceof Typeof) {
			Typeof to = (Typeof) expr;
			if(to.getEntity().getType() instanceof NodeType)
				sb.append("((GRGEN_LGSP.LGSPNode)" + formatEntity(to.getEntity()) + ").lgspType");
			else
				sb.append("((GRGEN_LGSP.LGSPEdge)" + formatEntity(to.getEntity()) + ").lgspType");				
		}
		else if(expr instanceof Cast) {
			Cast cast = (Cast) expr;
			String typeName = getTypeNameForCast(cast);

			if(typeName == "string") {
				if(cast.getExpression().getType() instanceof MapType || cast.getExpression().getType() instanceof SetType) {
					sb.append("GRGEN_LIBGR.EmitHelper.ToString(");
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(", graph)");
				} else if(cast.getExpression().getType() instanceof ArrayType) {
					sb.append("GRGEN_LIBGR.EmitHelper.ToString(");
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(", graph)");
				} else if(cast.getExpression().getType() instanceof DequeType) {
					sb.append("GRGEN_LIBGR.EmitHelper.ToString(");
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
			if(!Expression.isGlobalVariable(var)) {
				if(var.getIdent().toString().equals("this") && var.getType() instanceof ArrayType)
					sb.append("this_matches");
				else
					sb.append(formatEntity(var));
			} else {
				sb.append(formatGlobalVariableRead(var));
			}
		}
		else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression) expr).getGraphEntity();
			if(!Expression.isGlobalVariable(ent)) {
				sb.append(formatEntity(ent));
			} else {
				sb.append(formatGlobalVariableRead(ent));
			}
		}
		else if(expr instanceof Visited) {
			Visited vis = (Visited) expr;
			sb.append("graph.IsVisited(");
			genExpression(sb, vis.getEntity(), modifyGenerationState);
			sb.append(", ");
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
		else if(expr instanceof ThisExpr) {
			sb.append("graph");
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
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
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
		else if (expr instanceof IndexedIncidenceIndexAccessExpr) {
			IndexedIncidenceIndexAccessExpr ia = (IndexedIncidenceIndexAccessExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ia));
			}
			else {
				sb.append("((GRGEN_LIBGR.IIncidenceIndex)graph.Indices.GetIndex(\"" + ia.getTarget().getIdent() + "\")).GetIncidenceCount(");
//				sb.append("(" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + ")(");
				genExpression(sb, ia.getKeyExpr(), modifyGenerationState);
//				sb.append(")");
				sb.append(")");
			}
		}
		else if (expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ms));
			}
			else {
				sb.append("(");
				genExpression(sb, ms.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof MapEmptyExpr) {
			MapEmptyExpr me = (MapEmptyExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(me));
			}
			else {
				sb.append("((");
				genExpression(sb, me.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		}
		else if (expr instanceof MapDomainExpr) {
			MapDomainExpr md = (MapDomainExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(md));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Domain(");
				genExpression(sb, md.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof MapRangeExpr) {
			MapRangeExpr mr = (MapRangeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mr));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Range(");
				genExpression(sb, mr.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof MapPeekExpr) {
			MapPeekExpr mp = (MapPeekExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mp));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, mp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, mp.getNumberExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ss));
			}
			else {
				sb.append("(");
				genExpression(sb, ss.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof SetEmptyExpr) {
			SetEmptyExpr se = (SetEmptyExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(se));
			}
			else {
				sb.append("((");
				genExpression(sb, se.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		}
		else if (expr instanceof SetPeekExpr) {
			SetPeekExpr sp = (SetPeekExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(sp));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, sp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, sp.getNumberExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArraySizeExpr) {
			ArraySizeExpr as = (ArraySizeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(as));
			}
			else {
				sb.append("(");
				genExpression(sb, as.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof ArrayEmptyExpr) {
			ArrayEmptyExpr ae = (ArrayEmptyExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ae));
			}
			else {
				sb.append("((");
				genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		}
		else if (expr instanceof ArrayPeekExpr) {
			ArrayPeekExpr ap = (ArrayPeekExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ap));
			}
			else {
				sb.append("(");
				genExpression(sb, ap.getTargetExpr(), modifyGenerationState);
				sb.append("[");
				if(ap.getNumberExpr()!=null)
					genExpression(sb, ap.getNumberExpr(), modifyGenerationState);
				else {
					sb.append("(");
					genExpression(sb, ap.getTargetExpr(), modifyGenerationState);
					sb.append(").Count - 1");
				}
				sb.append("])");
			}
		}
		else if (expr instanceof ArrayIndexOfExpr) {
			ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ai));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
				genExpression(sb, ai.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ai.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArrayLastIndexOfExpr) {
			ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ali));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
				genExpression(sb, ali.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ali.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof ArraySubarrayExpr) {
			ArraySubarrayExpr asa = (ArraySubarrayExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(asa));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Subarray(");
				genExpression(sb, asa.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, asa.getStartExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, asa.getLengthExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof DequeSizeExpr) {
			DequeSizeExpr ds = (DequeSizeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ds));
			}
			else {
				sb.append("(");
				genExpression(sb, ds.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		}
		else if (expr instanceof DequeEmptyExpr) {
			DequeEmptyExpr de = (DequeEmptyExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(de));
			}
			else {
				sb.append("((");
				genExpression(sb, de.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		}
		else if (expr instanceof DequePeekExpr) {
			DequePeekExpr dp = (DequePeekExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dp));
			}
			else {
				sb.append("(");
				genExpression(sb, dp.getTargetExpr(), modifyGenerationState);
				sb.append("[");
				if(dp.getNumberExpr()!=null)
					genExpression(sb, dp.getNumberExpr(), modifyGenerationState);
				else
					sb.append("0");
				sb.append("])");
			}
		}
		else if (expr instanceof DequeIndexOfExpr) {
			DequeIndexOfExpr di = (DequeIndexOfExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(di));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
				genExpression(sb, di.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, di.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof DequeLastIndexOfExpr) {
			DequeLastIndexOfExpr dli = (DequeLastIndexOfExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dli));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
				genExpression(sb, dli.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dli.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if (expr instanceof DequeSubdequeExpr) {
			DequeSubdequeExpr dsd = (DequeSubdequeExpr)expr;
			if(modifyGenerationState!=null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dsd));
			}
			else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Subdeque(");
				genExpression(sb, dsd.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dsd.getStartExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dsd.getLengthExpr(), modifyGenerationState);
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
		else if (expr instanceof DequeInit) {
			DequeInit di = (DequeInit)expr;
			if(di.isConstant()) {
				sb.append(di.getAnonymousDequeName());
			} else {
				sb.append("fill_" + di.getAnonymousDequeName() + "(");
				boolean first = true;
				for(DequeItem item : di.getDequeItems()) {
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
		else if (expr instanceof FunctionInvocationExpr) {
			FunctionInvocationExpr fi = (FunctionInvocationExpr) expr;
			sb.append("GRGEN_ACTIONS." + getPackagePrefixDot(fi.getFunction()) + "Functions." + fi.getFunction().getIdent().toString() + "(actionEnv, graph");
			for(int i=0; i<fi.arity(); ++i) {
				sb.append(", ");
				Expression argument = fi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
				}
				genExpression(sb, argument, modifyGenerationState);
			}
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof ExternalFunctionInvocationExpr) {
			ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
			sb.append("GRGEN_EXPR.ExternalFunctions." + efi.getExternalFunc().getIdent().toString() + "(actionEnv, graph");
			for(int i=0; i<efi.arity(); ++i) {
				sb.append(", ");
				Expression argument = efi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
				}
				genExpression(sb, argument, modifyGenerationState);
			}
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof FunctionMethodInvocationExpr) {
			FunctionMethodInvocationExpr fmi = (FunctionMethodInvocationExpr)expr;
			Entity owner = fmi.getOwner();
			sb.append("(("+ formatElementInterfaceRef(owner.getType()) + ") ");
			sb.append(formatEntity(owner) + ").@");
			sb.append(fmi.getFunction().getIdent().toString() + "(actionEnv, graph");
			for(int i=0; i<fmi.arity(); ++i) {
				sb.append(", ");
				Expression argument = fmi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
				}
				genExpression(sb, argument, modifyGenerationState);
			}
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof ExternalFunctionMethodInvocationExpr) {
			ExternalFunctionMethodInvocationExpr efmi = (ExternalFunctionMethodInvocationExpr)expr;
			Entity owner = efmi.getOwner();
			sb.append("(("+ formatElementInterfaceRef(owner.getType()) + ") ");
			sb.append(formatEntity(owner) + ").@");
			sb.append(efmi.getExternalFunc().getIdent().toString() + "(actionEnv, graph");
			for(int i=0; i<efmi.arity(); ++i) {
				sb.append(", ");
				Expression argument = efmi.getArgument(i);
				if(argument.getType() instanceof InheritanceType) {
					sb.append("(" + formatElementInterfaceRef(argument.getType()) + ")");
				}
				genExpression(sb, argument, modifyGenerationState);
			}
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof EdgesExpr) {
			EdgesExpr e = (EdgesExpr) expr;
			sb.append("GRGEN_LIBGR.GraphHelper.Edges(graph, ");
			genExpression(sb, e.getEdgeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof NodesExpr) {
			NodesExpr n = (NodesExpr) expr;
			sb.append("GRGEN_LIBGR.GraphHelper.Nodes(graph, ");
			genExpression(sb, n.getNodeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof NowExpr) {
			//NowExpr n = (NowExpr)expr;
			sb.append("DateTime.UtcNow.ToFileTime()");
		}
		else if (expr instanceof EmptyExpr) {
			//EmptyExpr e = (EmptyExpr)expr;
			sb.append("(graph.NumNodes+graph.NumEdges == 0)");
		}
		else if (expr instanceof SizeExpr) {
			//SizeExpr s = (SizeExpr)expr;
			sb.append("(graph.NumNodes+graph.NumEdges)");
		}
		else if (expr instanceof SourceExpr) {
			SourceExpr s = (SourceExpr) expr;
			sb.append("((");
			genExpression(sb, s.getEdgeExpr(), modifyGenerationState);
			sb.append(").Source)");
		}
		else if (expr instanceof TargetExpr) {
			TargetExpr t = (TargetExpr) expr;
			sb.append("((");
			genExpression(sb, t.getEdgeExpr(), modifyGenerationState);
			sb.append(").Target)");
		}
		else if (expr instanceof OppositeExpr) {
			OppositeExpr o = (OppositeExpr) expr;
			sb.append("((");
			genExpression(sb, o.getEdgeExpr(), modifyGenerationState);
			sb.append(").Opposite(");
			genExpression(sb, o.getNodeExpr(), modifyGenerationState);
			sb.append("))");
		}
		else if (expr instanceof NodeByNameExpr) {
			NodeByNameExpr nbn = (NodeByNameExpr) expr;
			sb.append("((GRGEN_LIBGR.INamedGraph)graph).GetNode(");
			genExpression(sb, nbn.getNameExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof EdgeByNameExpr) {
			EdgeByNameExpr ebn = (EdgeByNameExpr) expr;
			sb.append("((GRGEN_LIBGR.INamedGraph)graph).GetEdge(");
			genExpression(sb, ebn.getNameExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof NodeByUniqueExpr) {
			NodeByUniqueExpr nbu = (NodeByUniqueExpr) expr;
			sb.append("graph.GetNode(");
			genExpression(sb, nbu.getUniqueExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof EdgeByUniqueExpr) {
			EdgeByUniqueExpr ebu = (EdgeByUniqueExpr) expr;
			sb.append("graph.GetEdge(");
			genExpression(sb, ebu.getUniqueExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr ie = (IncidentEdgeExpr) expr;
			if(ie.Direction()==IncidentEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.Outgoing(");
			} else if(ie.Direction()==IncidentEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.Incoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Incident(");
			}
			genExpression(sb, ie.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ie.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ie.getAdjacentNodeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr an = (AdjacentNodeExpr) expr;
			if(an.Direction()==AdjacentNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.AdjacentOutgoing(");
			} else if(an.Direction()==AdjacentNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.AdjacentIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Adjacent(");
			}
			genExpression(sb, an.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, an.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, an.getAdjacentNodeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof IsAdjacentNodeExpr) {
			IsAdjacentNodeExpr ian = (IsAdjacentNodeExpr) expr;
			if(ian.Direction()==IsReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsAdjacentOutgoing(");
			} else if(ian.Direction()==IsReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsAdjacentIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsAdjacent(");
			}
			genExpression(sb, ian.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ian.getEndNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ian.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ian.getAdjacentNodeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof IsIncidentEdgeExpr) {
			IsIncidentEdgeExpr iie = (IsIncidentEdgeExpr) expr;
			if(iie.Direction()==IsReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsOutgoing(");
			} else if(iie.Direction()==IsReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsIncident(");
			}
			genExpression(sb, iie.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, iie.getEndEdgeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, iie.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, iie.getAdjacentNodeTypeExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr re = (ReachableEdgeExpr) expr;
			if(re.Direction()==ReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing(graph, ");
			} else if(re.Direction()==ReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdges(graph, ");
			}
			genExpression(sb, re.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, re.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, re.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof ReachableNodeExpr) {
			ReachableNodeExpr rn = (ReachableNodeExpr) expr;
			if(rn.Direction()==ReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableOutgoing(");
			} else if(rn.Direction()==ReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Reachable(");
			}
			genExpression(sb, rn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, rn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, rn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof IsReachableNodeExpr) {
			IsReachableNodeExpr irn = (IsReachableNodeExpr) expr;
			if(irn.Direction()==IsReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableOutgoing(graph, ");
			} else if(irn.Direction()==IsReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachable(graph, ");
			}
			genExpression(sb, irn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, irn.getEndNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, irn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, irn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof IsReachableEdgeExpr) {
			IsReachableEdgeExpr ire = (IsReachableEdgeExpr) expr;
			if(ire.Direction()==IsReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesOutgoing(graph, ");
			} else if(ire.Direction()==IsReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableEdges(graph, ");
			}
			genExpression(sb, ire.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ire.getEndEdgeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ire.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ire.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		}
		else if (expr instanceof InducedSubgraphExpr) {
			InducedSubgraphExpr is = (InducedSubgraphExpr) expr;
			sb.append("GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, is.getSetExpr(), modifyGenerationState);
			sb.append(", graph)");
		}
		else if (expr instanceof DefinedSubgraphExpr) {
			DefinedSubgraphExpr ds = (DefinedSubgraphExpr) expr;
			sb.append("GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, ds.getSetExpr(), modifyGenerationState);
			sb.append(", graph)");
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
		else if (expr instanceof AbsExpr) {
			AbsExpr a = (AbsExpr)expr;
			sb.append("Math.Abs(");
			genExpression(sb, a.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof SgnExpr) {
			SgnExpr s = (SgnExpr)expr;
			sb.append("Math.Sign(");
			genExpression(sb, s.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof CeilExpr) {
			CeilExpr c = (CeilExpr)expr;
			sb.append("Math.Ceiling(");
			genExpression(sb, c.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof FloorExpr) {
			FloorExpr f = (FloorExpr)expr;
			sb.append("Math.Floor(");
			genExpression(sb, f.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof RoundExpr) {
			RoundExpr r = (RoundExpr)expr;
			sb.append("Math.Round(");
			genExpression(sb, r.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof TruncateExpr) {
			TruncateExpr t = (TruncateExpr)expr;
			sb.append("Math.Truncate(");
			genExpression(sb, t.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof SinCosTanExpr) {
			SinCosTanExpr sct = (SinCosTanExpr)expr;
			switch(sct.getWhich()) {
			case SinCosTanExpr.SIN:
				sb.append("Math.Sin(");
				break;
			case SinCosTanExpr.COS:
				sb.append("Math.Cos(");
				break;
			case SinCosTanExpr.TAN:
				sb.append("Math.Tan(");
				break;
			}
			genExpression(sb, sct.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof ArcSinCosTanExpr) {
			ArcSinCosTanExpr asct = (ArcSinCosTanExpr)expr;
			switch(asct.getWhich()) {
			case ArcSinCosTanExpr.ARC_SIN:
				sb.append("Math.Asin(");
				break;
			case ArcSinCosTanExpr.ARC_COS:
				sb.append("Math.Acos(");
				break;
			case ArcSinCosTanExpr.ARC_TAN:
				sb.append("Math.Atan(");
				break;
			}
			genExpression(sb, asct.getExpr(), modifyGenerationState);
			sb.append(")");
		}
		else if (expr instanceof CanonizeExpr) {
			CanonizeExpr c = (CanonizeExpr)expr;
			sb.append("(");
			genExpression(sb, c.getGraphExpr(), modifyGenerationState);
			sb.append(").Canonize()");
		}
		else if (expr instanceof LogExpr) {
			LogExpr l = (LogExpr)expr;
			sb.append("Math.Log(");
			genExpression(sb, l.getLeftExpr(), modifyGenerationState);
			if(l.getRightExpr()!=null) {
				sb.append(", ");
				genExpression(sb, l.getRightExpr(), modifyGenerationState);
			}
			sb.append(")");
		}
		else if (expr instanceof PowExpr) {
			PowExpr p = (PowExpr)expr;
			if(p.getLeftExpr()!=null) {
				sb.append("Math.Pow(");
				genExpression(sb, p.getLeftExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, p.getRightExpr(), modifyGenerationState);
				sb.append(")");
			} else {
				sb.append("Math.Exp(");
				genExpression(sb, p.getRightExpr(), modifyGenerationState);
				sb.append(")");
			}
		}
		else if(expr instanceof ProjectionExpr) {
			ProjectionExpr proj = (ProjectionExpr) expr;
			sb.append(proj.getProjectedValueVarName());
		}
		else if(expr instanceof MatchAccess) {
			MatchAccess ma = (MatchAccess) expr;
			genExpression(sb, ma.getExpr(), modifyGenerationState);
			sb.append(".");
			sb.append(formatEntity(ma.getEntity()));
		}
		else throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
	}

	protected String formatGlobalVariableRead(Entity globalVar)
	{
		return "((" + formatType(globalVar.getType()) + ")((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).GetVariableValue(\"" + formatIdentifiable(globalVar) + "\"))";
	}

	protected String formatGlobalVariableWrite(Entity globalVar, String value)
	{
		return "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).SetVariableValue(\"" + formatIdentifiable(globalVar) + "\", (" + formatType(globalVar.getType()) + ")(" + value + "))";
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
			case Type.IS_GRAPH:
				return "null"; // there is no graph constant - assert instead?
			default:
				throw new UnsupportedOperationException("unsupported type");
		}
	}

	protected String getInitializationValue(Type type)
	{
		if(type instanceof ByteType || type instanceof ShortType || type instanceof IntType 
				|| type instanceof EnumType || type instanceof DoubleType) {
			return "0";
		} else if(type instanceof FloatType) {
			return "0f";
		} else if(type instanceof LongType) {
			return "0l";
		} else if(type instanceof BooleanType) {
			return "false";
		} else {
			return "null";
		}
	}

	protected String getTypeNameForCast(Cast cast)
	{
		switch(cast.getType().classify()) {
			case Type.IS_STRING:
				return "string";
			case Type.IS_BYTE:
				return "sbyte";
			case Type.IS_SHORT:
				return "short";
			case Type.IS_INTEGER:
				return "int";
			case Type.IS_LONG:
				return "long";
			case Type.IS_FLOAT:
				return "float";
			case Type.IS_DOUBLE:
				return "double";
			case Type.IS_BOOLEAN:
				return "bool";
			case Type.IS_OBJECT:
				return "object";
			case Type.IS_GRAPH:
				return "GRGEN_LIBGR.IGraph";
			default:
				throw new UnsupportedOperationException(
					"This is either a forbidden cast, which should have been " +
						"rejected on building the IR, or an allowed cast, which " +
						"should have been processed by the above code.");
		}
	}
	
	protected String getTypeNameForTempVarDecl(Type type) {
		switch(type.classify()) {
			case Type.IS_BOOLEAN:
				return "bool";
			case Type.IS_BYTE:
				return "sbyte";
			case Type.IS_SHORT:
				return "short";
			case Type.IS_INTEGER:
				return "int";
			case Type.IS_LONG:
				return "long";
			case Type.IS_FLOAT:
				return "float";
			case Type.IS_DOUBLE:
				return "double";
			case Type.IS_STRING:
				return "string";
			case Type.IS_OBJECT:
			case Type.IS_UNKNOWN:
				return "Object";
			case Type.IS_GRAPH:
				return "GRGEN_LIBGR.IGraph";
			case Type.IS_EXTERNAL_TYPE:
				return "GRGEN_MODEL."+type.getIdent();
			case Type.IS_NODE:
				return formatElementInterfaceRef(type);
			case Type.IS_EDGE:
				return formatElementInterfaceRef(type);
			default:
				throw new IllegalArgumentException();
		}
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
	// THIS ARRAY MUST BE IN THE SAME ORDER AS Operator.opNames and the corresponding constants!
	private String[] opSymbols = {
		null, "||", "&&", "|", "^", "&",
			"==", "!=", "<", "<=", ">", ">=", "<<", ">>", ">>", "+",
			"-", "*", "/", "%", "!", "~", "-"
	};

	private String nodeTypePrefix;
	private String edgeTypePrefix;
}
