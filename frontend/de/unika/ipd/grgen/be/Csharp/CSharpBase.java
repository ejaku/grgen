/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
import de.unika.ipd.grgen.ir.executable.FunctionMethod;
import de.unika.ipd.grgen.ir.executable.ProcedureMethod;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.MatchType;
import de.unika.ipd.grgen.ir.type.MatchTypeIterated;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.BooleanType;
import de.unika.ipd.grgen.ir.type.basic.ByteType;
import de.unika.ipd.grgen.ir.type.basic.DoubleType;
import de.unika.ipd.grgen.ir.type.basic.FloatType;
import de.unika.ipd.grgen.ir.type.basic.GraphType;
import de.unika.ipd.grgen.ir.type.basic.IntType;
import de.unika.ipd.grgen.ir.type.basic.LongType;
import de.unika.ipd.grgen.ir.type.basic.ObjectType;
import de.unika.ipd.grgen.ir.type.basic.ShortType;
import de.unika.ipd.grgen.ir.type.basic.StringType;
import de.unika.ipd.grgen.ir.type.basic.VoidType;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.ir.type.container.MapType;
import de.unika.ipd.grgen.ir.type.container.SetType;
import de.unika.ipd.grgen.ir.expr.*;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsDequeExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsMapExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsSetExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsString;
import de.unika.ipd.grgen.ir.expr.array.ArrayAvgExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayCopyConstructor;
import de.unika.ipd.grgen.ir.expr.array.ArrayDevExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayEmptyExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayExtract;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEach;
import de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEachBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfByExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMaxExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMedUnorderedExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayMinExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscending;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscendingBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescending;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescendingBy;
import de.unika.ipd.grgen.ir.expr.array.ArrayPeekExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayProdExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayReverseExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySizeExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySubarrayExpr;
import de.unika.ipd.grgen.ir.expr.array.ArraySumExpr;
import de.unika.ipd.grgen.ir.expr.array.ArrayVarExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeAsSetExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeCopyConstructor;
import de.unika.ipd.grgen.ir.expr.deque.DequeEmptyExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.expr.deque.DequeLastIndexOfExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequePeekExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeSizeExpr;
import de.unika.ipd.grgen.ir.expr.deque.DequeSubdequeExpr;
import de.unika.ipd.grgen.ir.expr.graph.AdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeWithRemainingDepthExpr;
import de.unika.ipd.grgen.ir.expr.graph.CanonizeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountAdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountEdgesExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountIncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountNodesExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.CountReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.DefinedSubgraphExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByNameExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByUniqueExpr;
import de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
import de.unika.ipd.grgen.ir.expr.graph.EmptyExpr;
import de.unika.ipd.grgen.ir.expr.graph.EqualsAnyExpr;
import de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IndexedIncidenceCountIndexAccessExpr;
import de.unika.ipd.grgen.ir.expr.graph.InducedSubgraphExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsAdjacentNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsIncidentEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.IsReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.Nameof;
import de.unika.ipd.grgen.ir.expr.graph.NodeByNameExpr;
import de.unika.ipd.grgen.ir.expr.graph.NodeByUniqueExpr;
import de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
import de.unika.ipd.grgen.ir.expr.graph.OppositeExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableEdgeExpr;
import de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
import de.unika.ipd.grgen.ir.expr.graph.SizeExpr;
import de.unika.ipd.grgen.ir.expr.graph.SourceExpr;
import de.unika.ipd.grgen.ir.expr.graph.TargetExpr;
import de.unika.ipd.grgen.ir.expr.graph.ThisExpr;
import de.unika.ipd.grgen.ir.expr.graph.Uniqueof;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.expr.invocation.FunctionInvocationExpr;
import de.unika.ipd.grgen.ir.expr.invocation.FunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.map.MapCopyConstructor;
import de.unika.ipd.grgen.ir.expr.map.MapDomainExpr;
import de.unika.ipd.grgen.ir.expr.map.MapEmptyExpr;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.expr.map.MapPeekExpr;
import de.unika.ipd.grgen.ir.expr.map.MapRangeExpr;
import de.unika.ipd.grgen.ir.expr.map.MapSizeExpr;
import de.unika.ipd.grgen.ir.expr.numeric.AbsExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ArcSinCosTanExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ByteMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ByteMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.CeilExpr;
import de.unika.ipd.grgen.ir.expr.numeric.DoubleMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.DoubleMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.EExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloatMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloatMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.FloorExpr;
import de.unika.ipd.grgen.ir.expr.numeric.IntMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.IntMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LogExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LongMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.LongMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.MaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.MinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.PiExpr;
import de.unika.ipd.grgen.ir.expr.numeric.PowExpr;
import de.unika.ipd.grgen.ir.expr.numeric.RoundExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SgnExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ShortMaxExpr;
import de.unika.ipd.grgen.ir.expr.numeric.ShortMinExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SinCosTanExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SqrExpr;
import de.unika.ipd.grgen.ir.expr.numeric.SqrtExpr;
import de.unika.ipd.grgen.ir.expr.numeric.TruncateExpr;
import de.unika.ipd.grgen.ir.expr.procenv.ExistsFileExpr;
import de.unika.ipd.grgen.ir.expr.procenv.ImportExpr;
import de.unika.ipd.grgen.ir.expr.procenv.NowExpr;
import de.unika.ipd.grgen.ir.expr.procenv.RandomExpr;
import de.unika.ipd.grgen.ir.expr.set.SetAsArrayExpr;
import de.unika.ipd.grgen.ir.expr.set.SetCopyConstructor;
import de.unika.ipd.grgen.ir.expr.set.SetEmptyExpr;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.expr.set.SetPeekExpr;
import de.unika.ipd.grgen.ir.expr.set.SetSizeExpr;
import de.unika.ipd.grgen.ir.expr.string.StringAsArray;
import de.unika.ipd.grgen.ir.expr.string.StringEndsWith;
import de.unika.ipd.grgen.ir.expr.string.StringIndexOf;
import de.unika.ipd.grgen.ir.expr.string.StringLastIndexOf;
import de.unika.ipd.grgen.ir.expr.string.StringLength;
import de.unika.ipd.grgen.ir.expr.string.StringReplace;
import de.unika.ipd.grgen.ir.expr.string.StringStartsWith;
import de.unika.ipd.grgen.ir.expr.string.StringSubstring;
import de.unika.ipd.grgen.ir.expr.string.StringToLower;
import de.unika.ipd.grgen.ir.expr.string.StringToUpper;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.ir.model.type.ExternalType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.SourceBuilder;
import de.unika.ipd.grgen.util.Util;

public abstract class CSharpBase
{
	public CSharpBase(String nodeTypePrefix, String edgeTypePrefix)
	{
		this.nodeTypePrefix = nodeTypePrefix;
		this.edgeTypePrefix = edgeTypePrefix;
	}

	/**
	 * Write a character sequence to a file using the given path.
	 * @param path The path for the file.
	 * @param filename The filename.
	 * @param cs A character sequence.
	 */
	public void writeFile(File path, String filename, CharSequence cs)
	{
		Util.writeFile(new File(path, filename), cs, Base.error);
	}

	public boolean existsFile(File path, String filename)
	{
		return new File(path, filename).exists();
	}

	public void copyFile(File sourcePath, File targetPath)
	{
		try {
			Util.copyFile(sourcePath, targetPath);
		} catch(IOException ex) {
			System.out.println(ex.getMessage());
		}
	}

	/**
	 * Dumps a C-like set representation.
	 */
	public void genSet(SourceBuilder sb, Collection<? extends Identifiable> set, String pre, String post,
			boolean brackets)
	{
		if(brackets)
			sb.append("{ ");
		for(Iterator<? extends Identifiable> iter = set.iterator(); iter.hasNext();) {
			Identifiable id = iter.next();
			sb.append(pre + formatIdentifiable(id) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if(brackets)
			sb.append(" }");
	}

	public void genEntitySet(SourceBuilder sb, Collection<? extends Entity> set, String pre, String post,
			boolean brackets, String pathPrefix, HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append(pre + formatEntity(id, pathPrefix, alreadyDefinedEntityToName) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if(brackets)
			sb.append(" }");
	}

	public void genVarTypeSet(SourceBuilder sb, Collection<? extends Entity> set, boolean brackets)
	{
		if(brackets)
			sb.append("{ ");
		for(Iterator<? extends Entity> iter = set.iterator(); iter.hasNext();) {
			Entity id = iter.next();
			sb.append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + formatAttributeType(id) + "))");
			if(iter.hasNext())
				sb.append(", ");
		}
		if(brackets)
			sb.append(" }");
	}

	public void genSubpatternUsageSet(SourceBuilder sb, Collection<? extends SubpatternUsage> set, String pre,
			String post,
			boolean brackets, String pathPrefix,
			HashMap<? extends Identifiable, String> alreadyDefinedIdentifiableToName)
	{
		if(brackets)
			sb.append("{ ");
		for(Iterator<? extends SubpatternUsage> iter = set.iterator(); iter.hasNext();) {
			SubpatternUsage spu = iter.next();
			sb.append(pre + formatIdentifiable(spu, pathPrefix, alreadyDefinedIdentifiableToName) + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if(brackets)
			sb.append(" }");
	}

	public void genAlternativesSet(SourceBuilder sb, Collection<? extends Rule> set,
			String pre, String post, boolean brackets)
	{
		if(brackets)
			sb.append("{ ");
		for(Iterator<? extends Rule> iter = set.iterator(); iter.hasNext();) {
			Rule altCase = iter.next();
			PatternGraph altCasePattern = altCase.getLeft();
			sb.append(pre + altCasePattern.getNameOfGraph() + post);
			if(iter.hasNext())
				sb.append(", ");
		}
		if(brackets)
			sb.append(" }");
	}

	public String formatIdentifiable(Identifiable id)
	{
		String res = id.getIdent().toString();
		return res.replace('$', '_');
	}

	public String getPackagePrefixDot(Identifiable id)
	{
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn() != null) {
				return cip.getPackageContainedIn() + ".";
			}
		}
		return "";
	}

	public String getPackagePrefixDoubleColon(Identifiable id)
	{
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn() != null) {
				return cip.getPackageContainedIn() + "::";
			}
		}
		return "";
	}

	public String getPackagePrefix(Identifiable id)
	{
		if(id instanceof ContainedInPackage) {
			ContainedInPackage cip = (ContainedInPackage)id;
			if(cip.getPackageContainedIn() != null) {
				return cip.getPackageContainedIn();
			}
		}
		return "";
	}

	public String formatIdentifiable(Identifiable id, String pathPrefix)
	{
		String ident = id.getIdent().toString();
		return pathPrefix + ident.replace('$', '_');
	}

	public String formatIdentifiable(Identifiable id, String pathPrefix,
			HashMap<? extends Identifiable, String> alreadyDefinedIdentifiableToName)
	{
		if(alreadyDefinedIdentifiableToName != null && alreadyDefinedIdentifiableToName.get(id) != null)
			return alreadyDefinedIdentifiableToName.get(id);
		String ident = id.getIdent().toString();
		return pathPrefix + ident.replace('$', '_');
	}

	public String formatNodeOrEdge(boolean isNode)
	{
		return isNode ? "Node" : "Edge";
	}

	public String formatNodeOrEdge(Type type)
	{
		if(type instanceof NodeType)
			return "Node";
		else if(type instanceof EdgeType)
			return "Edge";
		else
			throw new IllegalArgumentException("Unknown type " + type + " (" + type.getClass() + ")");
	}

	public String formatNodeOrEdge(Entity ent)
	{
		if(ent instanceof Node)
			return "Node";
		else if(ent instanceof Edge)
			return "Edge";
		else
			throw new IllegalArgumentException("Illegal entity type " + ent + " (" + ent.getClass() + ")");
	}

	public String getNodeOrEdgeTypePrefix(Type type)
	{
		if(type instanceof NodeType)
			return nodeTypePrefix;
		else if(type instanceof EdgeType)
			return edgeTypePrefix;
		else
			throw new IllegalArgumentException("Unknown type " + type + " (" + type.getClass() + ")");
	}

	public String getNodeOrEdgeTypePrefix(Entity ent)
	{
		if(ent instanceof Node)
			return nodeTypePrefix;
		else if(ent instanceof Edge)
			return edgeTypePrefix;
		else
			throw new IllegalArgumentException("Illegal entity type " + ent + " (" + ent.getClass() + ")");
	}

	String matchType(PatternGraph patternGraph, Rule subpattern, boolean isSubpattern, String pathPrefix)
	{
		String matchClassContainer;
		if(isSubpattern) {
			matchClassContainer = "GRGEN_ACTIONS." + getPackagePrefixDot(subpattern) + "Pattern_"
					+ patternGraph.getNameOfGraph();
		} else {
			matchClassContainer = "GRGEN_ACTIONS." + getPackagePrefixDot(subpattern) + "Rule_"
					+ patternGraph.getNameOfGraph();
		}
		String nameOfMatchClass = "Match_" + pathPrefix + patternGraph.getNameOfGraph();
		return matchClassContainer + "." + nameOfMatchClass;
	}

	public String formatTypeClassName(Type type)
	{
		return formatNodeOrEdge(type) + "Type_" + formatIdentifiable(type);
	}

	public String formatTypeClassRef(Type type)
	{
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatTypeClassName(type);
	}

	public String formatTypeClassRefInstance(Type type)
	{
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatTypeClassName(type) + ".typeVar";
	}

	public String formatElementClassRaw(Type type)
	{
		return getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
	}

	public String formatElementClassName(Type type)
	{
		return "@" + formatElementClassRaw(type);
	}

	public String formatElementClassRef(Type type)
	{
		return "GRGEN_MODEL." + getPackagePrefixDot(type) + formatElementClassName(type);
	}

	public String formatElementInterfaceRef(Type type)
	{
		if(!(type instanceof InheritanceType)) {
			assert(false);
			return getNodeOrEdgeTypePrefix(type) + formatIdentifiable(type);
		}

		if(type instanceof ExternalType) {
			return "GRGEN_MODEL." + type.getIdent().toString();
		}

		switch(formatIdentifiable(type)) {
		case "Node":
		case "AEdge":
		case "Edge":
		case "UEdge":
			InheritanceType nodeEdgeType = (InheritanceType)type;
			return getRootElementInterfaceRef(nodeEdgeType);
		}

		return "GRGEN_MODEL." + getPackagePrefixDot(type) + "I" + formatElementClassRaw(type);
	}

	public String getRootElementInterfaceRef(InheritanceType nodeOrEdgeType)
	{
		if(nodeOrEdgeType instanceof NodeType) {
			return "GRGEN_LIBGR.INode";
		} else { // instanceof EdgeType
			EdgeType edgeType = (EdgeType)nodeOrEdgeType;
			if(edgeType.getDirectedness() == EdgeType.Directedness.Directed)
				return "GRGEN_LIBGR.IDEdge";
			else if(edgeType.getDirectedness() == EdgeType.Directedness.Undirected)
				return "GRGEN_LIBGR.IUEdge";
			else
				return "GRGEN_LIBGR.IEdge";
		}
	}

	public String getDirectedness(Type type)
	{
		SetType setType = (SetType)type;
		EdgeType edgeType = (EdgeType)setType.getValueType();
		if(edgeType.getDirectedness() == EdgeType.Directedness.Directed)
			return "GRGEN_LIBGR.Directedness.Directed";
		else if(edgeType.getDirectedness() == EdgeType.Directedness.Undirected)
			return "GRGEN_LIBGR.Directedness.Undirected";
		else
			return "GRGEN_LIBGR.Directedness.Arbitrary";
	}

	public String getDirectednessSuffix(Type type)
	{
		SetType setType = (SetType)type;
		EdgeType edgeType = (EdgeType)setType.getValueType();
		if(edgeType.getDirectedness() == EdgeType.Directedness.Directed)
			return "Directed";
		else if(edgeType.getDirectedness() == EdgeType.Directedness.Undirected)
			return "Undirected";
		else
			return "";
	}

	public String formatVarDeclWithCast(String type, String varName)
	{
		return type + " " + varName + " = (" + type + ") ";
	}

	public String formatNodeAssign(Node node, Collection<Node> extractNodeAttributeObject)
	{
		if(extractNodeAttributeObject.contains(node))
			return formatVarDeclWithCast(formatElementClassRef(node.getType()), formatEntity(node));
		else
			return "LGSPNode " + formatEntity(node) + " = ";
	}

	public String formatEdgeAssign(Edge edge, Collection<Edge> extractEdgeAttributeObject)
	{
		if(extractEdgeAttributeObject.contains(edge))
			return formatVarDeclWithCast(formatElementClassRef(edge.getType()), formatEntity(edge));
		else
			return "LGSPEdge " + formatEntity(edge) + " = ";
	}

	public String formatSequenceType(Type t)
	{
		if(t instanceof ByteType)
			return "byte";
		if(t instanceof ShortType)
			return "short";
		if(t instanceof IntType)
			return "int";
		if(t instanceof LongType)
			return "long";
		else if(t instanceof BooleanType)
			return "boolean";
		else if(t instanceof FloatType)
			return "float";
		else if(t instanceof DoubleType)
			return "double";
		else if(t instanceof StringType)
			return "string";
		else if(t instanceof EnumType)
			return getPackagePrefixDoubleColon(t) + formatIdentifiable(t);
		else if(t instanceof ObjectType || t instanceof VoidType)
			return "object";
		else if(t instanceof MapType) {
			MapType mapType = (MapType)t;
			return "map<" + formatSequenceType(mapType.getKeyType())
					+ ", " + formatSequenceType(mapType.getValueType()) + ">";
		} else if(t instanceof SetType) {
			SetType setType = (SetType)t;
			return "set<" + formatType(setType.getValueType()) + ">";
		} else if(t instanceof ArrayType) {
			ArrayType arrayType = (ArrayType)t;
			return "array<" + formatType(arrayType.getValueType()) + ">";
		} else if(t instanceof DequeType) {
			DequeType dequeType = (DequeType)t;
			return "deque<" + formatType(dequeType.getValueType()) + ">";
		} else if(t instanceof GraphType) {
			return "graph";
		} else if(t instanceof ExternalType) {
			ExternalType extType = (ExternalType)t;
			return extType.getIdent().toString();
		} else if(t instanceof InheritanceType) {
			return getPackagePrefixDoubleColon(t) + formatIdentifiable(t);
		} else if(t instanceof MatchTypeIterated) {
			MatchTypeIterated matchType = (MatchTypeIterated)t;
			String actionName = matchType.getAction().getIdent().toString();
			String iteratedName = matchType.getIterated().getIdent().toString();
			return "match<" + actionName + "." + iteratedName + ">";
		} else if(t instanceof MatchType) {
			MatchType matchType = (MatchType)t;
			String actionName = matchType.getAction().getIdent().toString();
			return "match<" + actionName + ">";
		} else if(t instanceof DefinedMatchType) {
			DefinedMatchType matchType = (DefinedMatchType)t;
			String matchTypeName = matchType.getIdent().toString();
			return "match<class" + matchTypeName + ">";
		} else
			throw new IllegalArgumentException("Illegal type: " + t);
	}

	public String formatAttributeType(Type t)
	{
		if(t instanceof ByteType)
			return "sbyte";
		if(t instanceof ShortType)
			return "short";
		if(t instanceof IntType)
			return "int";
		if(t instanceof LongType)
			return "long";
		else if(t instanceof BooleanType)
			return "bool";
		else if(t instanceof FloatType)
			return "float";
		else if(t instanceof DoubleType)
			return "double";
		else if(t instanceof StringType)
			return "string";
		else if(t instanceof EnumType)
			return "GRGEN_MODEL." + getPackagePrefixDot(t) + "ENUM_" + formatIdentifiable(t);
		else if(t instanceof ObjectType || t instanceof VoidType)
			return "object"; //TODO maybe we need another output type
		else if(t instanceof MapType) {
			MapType mapType = (MapType)t;
			return "Dictionary<" + formatType(mapType.getKeyType())
					+ ", " + formatType(mapType.getValueType()) + ">";
		} else if(t instanceof SetType) {
			SetType setType = (SetType)t;
			return "Dictionary<" + formatType(setType.getValueType())
					+ ", GRGEN_LIBGR.SetValueType>";
		} else if(t instanceof ArrayType) {
			ArrayType arrayType = (ArrayType)t;
			return "List<" + formatType(arrayType.getValueType()) + ">";
		} else if(t instanceof DequeType) {
			DequeType dequeType = (DequeType)t;
			return "GRGEN_LIBGR.Deque<" + formatType(dequeType.getValueType()) + ">";
		} else if(t instanceof GraphType) {
			return "GRGEN_LIBGR.IGraph";
		} else if(t instanceof ExternalType) {
			ExternalType extType = (ExternalType)t;
			return "GRGEN_MODEL." + extType.getIdent();
		} else if(t instanceof InheritanceType) {
			return formatElementInterfaceRef(t);
		} else if(t instanceof MatchTypeIterated) {
			MatchTypeIterated matchType = (MatchTypeIterated)t;
			String packagePrefix = getPackagePrefixDot(matchType);
			Rule action = matchType.getAction();
			String actionName = action.getIdent().toString();
			Rule iterated = matchType.getIterated();
			String iteratedName = iterated.getIdent().toString();
			return "GRGEN_ACTIONS." + packagePrefix + "Rule_" + actionName
					+ ".IMatch_" + actionName + "_" + iteratedName;
		} else if(t instanceof MatchType) {
			MatchType matchType = (MatchType)t;
			String packagePrefix = getPackagePrefixDot(matchType);
			Rule action = matchType.getAction();
			String actionName = action.getIdent().toString();
			return "GRGEN_ACTIONS." + packagePrefix + "Rule_" + actionName + ".IMatch_" + actionName;
		} else if(t instanceof DefinedMatchType) {
			DefinedMatchType definedMatchType = (DefinedMatchType)t;
			String packagePrefix = getPackagePrefixDot(definedMatchType);
			String matchClassName = definedMatchType.getIdent().toString();
			return "GRGEN_ACTIONS." + packagePrefix + "IMatch_" + matchClassName;
		} else
			throw new IllegalArgumentException("Illegal type: " + t);
	}

	public String formatAttributeType(Entity e)
	{
		return formatAttributeType(e.getType());
	}

	public String formatAttributeTypeName(Entity e)
	{
		return "AttributeType_" + formatIdentifiable(e);
	}

	public String formatFunctionMethodInfoName(FunctionMethod fm, InheritanceType type)
	{
		return "FunctionMethodInfo_" + formatIdentifiable(fm) + "_" + formatIdentifiable(type);
	}

	public String formatProcedureMethodInfoName(ProcedureMethod pm, InheritanceType type)
	{
		return "ProcedureMethodInfo_" + formatIdentifiable(pm) + "_" + formatIdentifiable(type);
	}

	public String formatExternalFunctionMethodInfoName(ExternalFunctionMethod efm, ExternalType type)
	{
		return "FunctionMethodInfo_" + formatIdentifiable(efm) + "_" + formatIdentifiable(type);
	}

	public String formatExternalProcedureMethodInfoName(ExternalProcedureMethod epm, ExternalType type)
	{
		return "ProcedureMethodInfo_" + formatIdentifiable(epm) + "_" + formatIdentifiable(type);
	}

	public String formatType(Type type)
	{
		if(type instanceof InheritanceType) {
			return formatElementInterfaceRef(type);
		} else {
			return formatAttributeType(type);
		}
	}

	public String formatEntity(Entity entity)
	{
		return formatEntity(entity, "");
	}

	public String formatEntity(Entity entity, String pathPrefix)
	{
		if(entity.getIdent().toString() == "this") {
			if(entity.getType() instanceof ArrayType)
				return "this_matches";
			else
				return "this";
		} else if(entity instanceof Node) {
			return pathPrefix + "node_" + formatIdentifiable(entity);
		} else if(entity instanceof Edge) {
			return pathPrefix + "edge_" + formatIdentifiable(entity);
		} else if(entity instanceof Variable) {
			return pathPrefix + "var_" + formatIdentifiable(entity);
		} else {
			throw new IllegalArgumentException("Unknown entity " + entity + " (" + entity.getClass() + ")");
		}
	}

	public String formatEntity(Entity entity, String pathPrefix,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		if(alreadyDefinedEntityToName != null && alreadyDefinedEntityToName.get(entity) != null)
			return alreadyDefinedEntityToName.get(entity);
		return formatEntity(entity, pathPrefix);
	}

	public String formatInt(int i)
	{
		return (i == Integer.MAX_VALUE) ? "int.MaxValue" : new Integer(i).toString();
	}

	public String formatLong(long l)
	{
		return (l == Long.MAX_VALUE) ? "long.MaxValue" : new Long(l).toString();
	}

	public Entity getAtMostOneNeededNodeOrEdge(NeededEntities needs, List<Entity> parameters)
	{
		HashSet<GraphEntity> neededEntities = new HashSet<GraphEntity>();
		for(Node node : needs.nodes) {
			if(parameters.indexOf(node) != -1)
				continue;
			neededEntities.add(node);
		}
		for(Edge edge : needs.edges) {
			if(parameters.indexOf(edge) != -1)
				continue;
			neededEntities.add(edge);
		}
		if(neededEntities.size() == 1)
			return neededEntities.iterator().next();
		else if(neededEntities.size() > 1)
			throw new UnsupportedOperationException("INTERNAL ERROR, more than one needed entity for index access!");
		return null;
	}

	public void genBinOpDefault(SourceBuilder sb, Operator op, ExpressionGenerationState modifyGenerationState)
	{
		if(op.getOpCode() == Operator.BIT_SHR) {
			sb.append("((int)(((uint)");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(") " + opSymbols[op.getOpCode()] + " ");
			genExpression(sb, op.getOperand(1), modifyGenerationState);
			sb.append("))");
		} else {
			sb.append("(");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(" " + opSymbols[op.getOpCode()] + " ");
			genExpression(sb, op.getOperand(1), modifyGenerationState);
			sb.append(")");
		}
	}

	public strictfp void genExpression(SourceBuilder sb, Expression expr,
			ExpressionGenerationState modifyGenerationState)
	{
		if(expr instanceof Operator) {
			Operator op = (Operator)expr;
			genOperator(sb, op, modifyGenerationState);
		} else if(expr instanceof Qualification) {
			Qualification qual = (Qualification)expr;
			if(qual.getOwner() != null) {
				genQualAccess(sb, qual, modifyGenerationState);
			} else {
				sb.append("(");
				genExpression(sb, qual.getOwnerExpr(), modifyGenerationState);
				sb.append(").@" + formatIdentifiable(qual.getMember()));
			}
		} else if(expr instanceof MemberExpression) {
			MemberExpression memberExp = (MemberExpression)expr;
			genMemberAccess(sb, memberExp.getMember());
		} else if(expr instanceof EnumExpression) {
			EnumExpression enumExp = (EnumExpression)expr;
			sb.append("GRGEN_MODEL." + getPackagePrefixDot(enumExp.getType()) + "ENUM_"
					+ enumExp.getType().getIdent().toString() + ".@" + enumExp.getEnumItem().toString());
		} else if(expr instanceof Constant) { // gen C-code for constant expressions
			Constant constant = (Constant)expr;
			sb.append(getValueAsCSSharpString(constant));
		} else if(expr instanceof Nameof) {
			Nameof no = (Nameof)expr;
			if(no.getNamedEntity() == null) {
				sb.append("GRGEN_LIBGR.GraphHelper.Nameof(null, graph)"); // name of graph
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Nameof(");
				genExpression(sb, no.getNamedEntity(), modifyGenerationState); // name of entity
				sb.append(", graph)");
			}
		} else if(expr instanceof Uniqueof) {
			Uniqueof no = (Uniqueof)expr;
			if(no.getEntity() == null)
				sb.append("((GRGEN_LGSP.LGSPGraph)graph).GraphId");
			else {
				sb.append("(");
				if(no.getEntity().getType() instanceof NodeType)
					sb.append("(GRGEN_LGSP.LGSPNode)");
				else if(no.getEntity().getType() instanceof EdgeType)
					sb.append("(GRGEN_LGSP.LGSPEdge)");
				else
					sb.append("(GRGEN_LGSP.LGSPGraph)");
				genExpression(sb, no.getEntity(), modifyGenerationState); // unique id of entity
				if(no.getEntity().getType() instanceof GraphType)
					sb.append(").GraphId");
				else
					sb.append(").uniqueId");
			}
		} else if(expr instanceof ExistsFileExpr) {
			ExistsFileExpr efe = (ExistsFileExpr)expr;
			sb.append("System.IO.File.Exists((string)");
			genExpression(sb, efe.getPathExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof ImportExpr) {
			ImportExpr ie = (ImportExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.Import(");
			genExpression(sb, ie.getPathExpr(), modifyGenerationState);
			sb.append(", actionEnv.Backend, graph.Model)");
		} else if(expr instanceof CopyExpr) {
			CopyExpr ce = (CopyExpr)expr;
			Type t = ce.getSourceExpr().getType();
			if(t instanceof MatchType || t instanceof MatchTypeIterated || t instanceof DefinedMatchType) {
				sb.append("((" + formatType(t) + ")(");
				genExpression(sb, ce.getSourceExpr(), modifyGenerationState);
				sb.append(").Clone())");
			} else if(t instanceof GraphType) {
				sb.append("GRGEN_LIBGR.GraphHelper.Copy(");
				genExpression(sb, ce.getSourceExpr(), modifyGenerationState);
				sb.append(")");
			} else {
				sb.append("new " + formatType(t) + "(");
				genExpression(sb, ce.getSourceExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof Count) {
			Count count = (Count)expr;
			sb.append("curMatch." + formatIdentifiable(count.getIterated()) + ".Count");
		} else if(expr instanceof Typeof) {
			Typeof to = (Typeof)expr;
			if(to.getEntity().getType() instanceof NodeType)
				sb.append("((GRGEN_LGSP.LGSPNode)" + formatEntity(to.getEntity()) + ").lgspType");
			else
				sb.append("((GRGEN_LGSP.LGSPEdge)" + formatEntity(to.getEntity()) + ").lgspType");
		} else if(expr instanceof Cast) {
			Cast cast = (Cast)expr;
			String typeName = getTypeNameForCast(cast);

			if(typeName == "string") {
				if(cast.getExpression().getType() instanceof MapType
						|| cast.getExpression().getType() instanceof SetType) {
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
					sb.append("GRGEN_LIBGR.EmitHelper.ToStringNonNull(");
					genExpression(sb, cast.getExpression(), modifyGenerationState);
					sb.append(", graph)");
				}
			} else if(typeName == "object") {
				// no cast needed
				genExpression(sb, cast.getExpression(), modifyGenerationState);
			} else {
				sb.append("((" + typeName + ") ");
				genExpression(sb, cast.getExpression(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof VariableExpression) {
			Variable var = ((VariableExpression)expr).getVariable();
			if(!Expression.isGlobalVariable(var)) {
				if(var.getIdent().toString().equals("this") && var.getType() instanceof ArrayType)
					sb.append("this_matches");
				else
					sb.append(formatEntity(var));
			} else {
				sb.append(formatGlobalVariableRead(var));
			}
		} else if(expr instanceof GraphEntityExpression) {
			GraphEntity ent = ((GraphEntityExpression)expr).getGraphEntity();
			if(!Expression.isGlobalVariable(ent)) {
				sb.append(formatEntity(ent));
			} else {
				sb.append(formatGlobalVariableRead(ent));
			}
		} else if(expr instanceof Visited) {
			Visited vis = (Visited)expr;
			sb.append("graph.IsVisited(");
			genExpression(sb, vis.getEntity(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, vis.getVisitorID(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof RandomExpr) {
			RandomExpr re = (RandomExpr)expr;
			if(re.getNumExpr() != null) {
				sb.append("GRGEN_LIBGR.Sequence.randomGenerator.Next(");
				genExpression(sb, re.getNumExpr(), modifyGenerationState);
			} else {
				sb.append("GRGEN_LIBGR.Sequence.randomGenerator.NextDouble(");
			}
			sb.append(")");
		} else if(expr instanceof ThisExpr) {
			sb.append("graph");
		} else if(expr instanceof StringLength) {
			StringLength strlen = (StringLength)expr;
			sb.append("(");
			genExpression(sb, strlen.getStringExpr(), modifyGenerationState);
			sb.append(").Length");
		} else if(expr instanceof StringToUpper) {
			StringToUpper strtoup = (StringToUpper)expr;
			sb.append("(");
			genExpression(sb, strtoup.getStringExpr(), modifyGenerationState);
			sb.append(").ToUpperInvariant()");
		} else if(expr instanceof StringToLower) {
			StringToLower strtolo = (StringToLower)expr;
			sb.append("(");
			genExpression(sb, strtolo.getStringExpr(), modifyGenerationState);
			sb.append(").ToLowerInvariant()");
		} else if(expr instanceof StringSubstring) {
			StringSubstring strsubstr = (StringSubstring)expr;
			sb.append("(");
			genExpression(sb, strsubstr.getStringExpr(), modifyGenerationState);
			sb.append(").Substring(");
			genExpression(sb, strsubstr.getStartExpr(), modifyGenerationState);
			if(strsubstr.getLengthExpr() != null) {
				sb.append(", ");
				genExpression(sb, strsubstr.getLengthExpr(), modifyGenerationState);
			}
			sb.append(")");
		} else if(expr instanceof StringIndexOf) {
			StringIndexOf strio = (StringIndexOf)expr;
			sb.append("(");
			genExpression(sb, strio.getStringExpr(), modifyGenerationState);
			sb.append(").IndexOf(");
			genExpression(sb, strio.getStringToSearchForExpr(), modifyGenerationState);
			if(strio.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpression(sb, strio.getStartIndexExpr(), modifyGenerationState);
			}
			sb.append(", StringComparison.InvariantCulture");
			sb.append(")");
		} else if(expr instanceof StringLastIndexOf) {
			StringLastIndexOf strlio = (StringLastIndexOf)expr;
			sb.append("(");
			genExpression(sb, strlio.getStringExpr(), modifyGenerationState);
			sb.append(").LastIndexOf(");
			genExpression(sb, strlio.getStringToSearchForExpr(), modifyGenerationState);
			if(strlio.getStartIndexExpr() != null) {
				sb.append(", ");
				genExpression(sb, strlio.getStartIndexExpr(), modifyGenerationState);
			}
			sb.append(", StringComparison.InvariantCulture");
			sb.append(")");
		} else if(expr instanceof StringStartsWith) {
			StringStartsWith strsw = (StringStartsWith)expr;
			sb.append("(");
			genExpression(sb, strsw.getStringExpr(), modifyGenerationState);
			sb.append(").StartsWith(");
			genExpression(sb, strsw.getStringToSearchForExpr(), modifyGenerationState);
			sb.append(", StringComparison.InvariantCulture");
			sb.append(")");
		} else if(expr instanceof StringEndsWith) {
			StringEndsWith strew = (StringEndsWith)expr;
			sb.append("(");
			genExpression(sb, strew.getStringExpr(), modifyGenerationState);
			sb.append(").EndsWith(");
			genExpression(sb, strew.getStringToSearchForExpr(), modifyGenerationState);
			sb.append(", StringComparison.InvariantCulture");
			sb.append(")");
		} else if(expr instanceof StringReplace) {
			StringReplace strrepl = (StringReplace)expr;
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
		} else if(expr instanceof StringAsArray) {
			StringAsArray saa = (StringAsArray)expr;
			sb.append("GRGEN_LIBGR.ContainerHelper.StringAsArray(");
			genExpression(sb, saa.getStringExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, saa.getStringToSplitAtExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof IndexedAccessExpr) {
			IndexedAccessExpr ia = (IndexedAccessExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ia));
			} else {
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
		} else if(expr instanceof IndexedIncidenceCountIndexAccessExpr) {
			IndexedIncidenceCountIndexAccessExpr ia = (IndexedIncidenceCountIndexAccessExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ia));
			} else {
				sb.append("((GRGEN_LIBGR.IIncidenceCountIndex)graph.Indices.GetIndex(\"" + ia.getTarget().getIdent()
						+ "\")).GetIncidenceCount(");
				//sb.append("(" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + ")(");
				genExpression(sb, ia.getKeyExpr(), modifyGenerationState);
				//sb.append(")");
				sb.append(")");
			}
		} else if(expr instanceof MapSizeExpr) {
			MapSizeExpr ms = (MapSizeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ms));
			} else {
				sb.append("(");
				genExpression(sb, ms.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		} else if(expr instanceof MapEmptyExpr) {
			MapEmptyExpr me = (MapEmptyExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(me));
			} else {
				sb.append("((");
				genExpression(sb, me.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		} else if(expr instanceof MapDomainExpr) {
			MapDomainExpr md = (MapDomainExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(md));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Domain(");
				genExpression(sb, md.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof MapRangeExpr) {
			MapRangeExpr mr = (MapRangeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mr));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Range(");
				genExpression(sb, mr.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof MapAsArrayExpr) {
			MapAsArrayExpr maa = (MapAsArrayExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(maa));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.MapAsArray(");
				genExpression(sb, maa.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof MapPeekExpr) {
			MapPeekExpr mp = (MapPeekExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(mp));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, mp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, mp.getNumberExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof SetSizeExpr) {
			SetSizeExpr ss = (SetSizeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ss));
			} else {
				sb.append("(");
				genExpression(sb, ss.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		} else if(expr instanceof SetEmptyExpr) {
			SetEmptyExpr se = (SetEmptyExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(se));
			} else {
				sb.append("((");
				genExpression(sb, se.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		} else if(expr instanceof SetPeekExpr) {
			SetPeekExpr sp = (SetPeekExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(sp));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, sp.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, sp.getNumberExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof SetAsArrayExpr) {
			SetAsArrayExpr saa = (SetAsArrayExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(saa));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.SetAsArray(");
				genExpression(sb, saa.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArraySizeExpr) {
			ArraySizeExpr as = (ArraySizeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(as));
			} else {
				sb.append("(");
				genExpression(sb, as.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		} else if(expr instanceof ArrayEmptyExpr) {
			ArrayEmptyExpr ae = (ArrayEmptyExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ae));
			} else {
				sb.append("((");
				genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		} else if(expr instanceof ArrayPeekExpr) {
			ArrayPeekExpr ap = (ArrayPeekExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ap));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, ap.getTargetExpr(), modifyGenerationState);
				if(ap.getNumberExpr() != null) {
					sb.append(", ");
					genExpression(sb, ap.getNumberExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof ArrayIndexOfExpr) {
			ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ai));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
				genExpression(sb, ai.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ai.getValueExpr(), modifyGenerationState);
				if(ai.getStartIndexExpr() != null) {
					sb.append(", ");
					genExpression(sb, ai.getStartIndexExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof ArrayIndexOfByExpr) {
			ArrayIndexOfByExpr aib = (ArrayIndexOfByExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aib));
			} else {
				sb.append("GRGEN_MODEL.Comparer_"
						+ ((ArrayType)aib.getTargetExpr().getType()).getValueType().getIdent().toString() + "_"
						+ formatIdentifiable(aib.getMember()) + ".IndexOfBy(");
				genExpression(sb, aib.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, aib.getValueExpr(), modifyGenerationState);
				if(aib.getStartIndexExpr() != null) {
					sb.append(", ");
					genExpression(sb, aib.getStartIndexExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof ArrayIndexOfOrderedExpr) {
			ArrayIndexOfOrderedExpr aio = (ArrayIndexOfOrderedExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aio));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.IndexOfOrdered(");
				genExpression(sb, aio.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, aio.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayIndexOfOrderedByExpr) {
			ArrayIndexOfOrderedByExpr aiob = (ArrayIndexOfOrderedByExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aiob));
			} else {
				sb.append("GRGEN_MODEL.Comparer_"
						+ ((ArrayType)aiob.getTargetExpr().getType()).getValueType().getIdent().toString() + "_"
						+ formatIdentifiable(aiob.getMember()) + ".IndexOfOrderedBy(");
				genExpression(sb, aiob.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, aiob.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayLastIndexOfExpr) {
			ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ali));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
				genExpression(sb, ali.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, ali.getValueExpr(), modifyGenerationState);
				if(ali.getStartIndexExpr() != null) {
					sb.append(", ");
					genExpression(sb, ali.getStartIndexExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof ArrayLastIndexOfByExpr) {
			ArrayLastIndexOfByExpr alib = (ArrayLastIndexOfByExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(alib));
			} else {
				sb.append("GRGEN_MODEL.Comparer_"
						+ ((ArrayType)alib.getTargetExpr().getType()).getValueType().getIdent().toString() + "_"
						+ formatIdentifiable(alib.getMember()) + ".LastIndexOfBy(");
				genExpression(sb, alib.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, alib.getValueExpr(), modifyGenerationState);
				if(alib.getStartIndexExpr() != null) {
					sb.append(", ");
					genExpression(sb, alib.getStartIndexExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof ArraySubarrayExpr) {
			ArraySubarrayExpr as = (ArraySubarrayExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(as));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Subarray(");
				genExpression(sb, as.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, as.getStartExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, as.getLengthExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayOrderAscending) {
			ArrayOrderAscending aoa = (ArrayOrderAscending)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aoa));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayOrderAscending(");
				genExpression(sb, aoa.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayOrderDescending) {
			ArrayOrderDescending aod = (ArrayOrderDescending)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aod));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayOrderDescending(");
				genExpression(sb, aod.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayKeepOneForEach) {
			ArrayKeepOneForEach ako = (ArrayKeepOneForEach)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ako));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayKeepOneForEach(");
				genExpression(sb, ako.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayOrderAscendingBy) {
			ArrayOrderAscendingBy aoab = (ArrayOrderAscendingBy)expr;
			Type arrayValueType = ((ArrayType)aoab.getTargetExpr().getType()).getValueType();
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aoab));
			} else {
				if(arrayValueType instanceof InheritanceType) {
					InheritanceType graphElementType = (InheritanceType)arrayValueType;
					String comparerName = getPackagePrefixDot(graphElementType) + "Comparer_"
							+ graphElementType.getIdent().toString() + "_" + formatIdentifiable(aoab.getMember());
					sb.append("GRGEN_MODEL." + comparerName + ".ArrayOrderAscendingBy(");
					genExpression(sb, aoab.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchTypeIterated) {
					MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String iteratedName = formatIdentifiable(matchType.getIterated());
					String functionName = "orderAscendingBy_" + formatIdentifiable(aoab.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, aoab.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchType) {
					MatchType matchType = (MatchType)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String functionName = "orderAscendingBy_" + formatIdentifiable(aoab.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, aoab.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof DefinedMatchType) {
					DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
					String matchClassPackage = getPackagePrefixDot(definedMatchType);
					String matchClassName = formatIdentifiable(definedMatchType);
					String functionName = "orderAscendingBy_" + formatIdentifiable(aoab.getMember());
					String arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + matchClassPackage + "MatchClassFilters." + arrayFunctionName + "(");
					genExpression(sb, aoab.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				}
			}
		} else if(expr instanceof ArrayOrderDescendingBy) {
			ArrayOrderDescendingBy aodb = (ArrayOrderDescendingBy)expr;
			Type arrayValueType = ((ArrayType)aodb.getTargetExpr().getType()).getValueType();
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aodb));
			} else {
				if(arrayValueType instanceof InheritanceType) {
					InheritanceType graphElementType = (InheritanceType)arrayValueType;
					String comparerName = getPackagePrefixDot(graphElementType) + "Comparer_"
							+ graphElementType.getIdent().toString() + "_" + formatIdentifiable(aodb.getMember());
					sb.append("GRGEN_MODEL." + comparerName + ".ArrayOrderDescendingBy(");
					genExpression(sb, aodb.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchTypeIterated) {
					MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String iteratedName = formatIdentifiable(matchType.getIterated());
					String functionName = "orderDescendingBy_" + formatIdentifiable(aodb.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, aodb.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchType) {
					MatchType matchType = (MatchType)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String functionName = "orderDescendingBy_" + formatIdentifiable(aodb.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, aodb.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof DefinedMatchType) {
					DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
					String matchClassPackage = getPackagePrefixDot(definedMatchType);
					String matchClassName = formatIdentifiable(definedMatchType);
					String functionName = "orderDescendingBy_" + formatIdentifiable(aodb.getMember());
					String arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + matchClassPackage + "MatchClassFilters." + arrayFunctionName + "(");
					genExpression(sb, aodb.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				}
			}
		} else if(expr instanceof ArrayKeepOneForEachBy) {
			ArrayKeepOneForEachBy akob = (ArrayKeepOneForEachBy)expr;
			Type arrayValueType = ((ArrayType)akob.getTargetExpr().getType()).getValueType();
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(akob));
			} else {
				if(arrayValueType instanceof InheritanceType) {
					InheritanceType graphElementType = (InheritanceType)arrayValueType;
					String comparerName = getPackagePrefixDot(graphElementType) + "Comparer_"
							+ graphElementType.getIdent().toString() + "_" + formatIdentifiable(akob.getMember());
					sb.append("GRGEN_MODEL." + comparerName + ".ArrayKeepOneForEachBy(");
					genExpression(sb, akob.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchTypeIterated) {
					MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String iteratedName = formatIdentifiable(matchType.getIterated());
					String functionName = "keepOneForEachBy_" + formatIdentifiable(akob.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, akob.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchType) {
					MatchType matchType = (MatchType)arrayValueType;
					String rulePackage = getPackagePrefixDot(matchType.getAction());
					String ruleName = formatIdentifiable(matchType.getAction());
					String functionName = "keepOneForEachBy_" + formatIdentifiable(akob.getMember());
					String arrayFunctionName = "Array_" + ruleName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + rulePackage + "MatchFilters." + arrayFunctionName + "(");
					genExpression(sb, akob.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof DefinedMatchType) {
					DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
					String matchClassPackage = getPackagePrefixDot(definedMatchType);
					String matchClassName = formatIdentifiable(definedMatchType);
					String functionName = "keepOneForEachBy_" + formatIdentifiable(akob.getMember());
					String arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
					sb.append("GRGEN_ACTIONS." + matchClassPackage + "MatchClassFilters." + arrayFunctionName + "(");
					genExpression(sb, akob.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				}
			}
		} else if(expr instanceof ArrayReverseExpr) {
			ArrayReverseExpr ar = (ArrayReverseExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ar));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayReverse(");
				genExpression(sb, ar.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayExtract) {
			ArrayExtract ae = (ArrayExtract)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ae));
			} else {
				Type arrayValueType = ((ArrayType)ae.getTargetExpr().getType()).getValueType();
				if(arrayValueType instanceof InheritanceType) {
					InheritanceType graphElementType = (InheritanceType)arrayValueType;
					String comparerName = getPackagePrefixDot(graphElementType) + "Comparer_"
							+ graphElementType.getIdent().toString() + "_" + formatIdentifiable(ae.getMember());
					sb.append("GRGEN_MODEL." + comparerName + ".Extract(");
					genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchTypeIterated) {
					MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
					Rule rule = matchType.getAction();
					String ruleName = getPackagePrefixDot(rule) + "Rule_" + formatIdentifiable(rule);
					Rule iterated = matchType.getIterated();
					String iteratedName = formatIdentifiable(iterated);
					sb.append("GRGEN_ACTIONS." + ruleName + ".Extractor_" + iteratedName + ".Extract_"
							+ formatIdentifiable(ae.getMember()) + "(");
					genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof MatchType) {
					MatchType matchType = (MatchType)arrayValueType;
					Rule rule = matchType.getAction();
					String ruleName = getPackagePrefixDot(rule) + "Rule_" + formatIdentifiable(rule);
					sb.append("GRGEN_ACTIONS." + ruleName + ".Extractor.Extract_" + formatIdentifiable(ae.getMember()) + "(");
					genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				} else if(arrayValueType instanceof DefinedMatchType) {
					DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
					String matchClassName = getPackagePrefixDot(definedMatchType) + "MatchClassInfo_"
							+ formatIdentifiable(definedMatchType);
					sb.append("GRGEN_ACTIONS." + matchClassName + ".Extractor.Extract_"
							+ formatIdentifiable(ae.getMember()) + "(");
					genExpression(sb, ae.getTargetExpr(), modifyGenerationState);
					sb.append(")");
				}
			}
		} else if(expr instanceof ArrayAsSetExpr) {
			ArrayAsSetExpr aas = (ArrayAsSetExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aas));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayAsSet(");
				genExpression(sb, aas.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayAsDequeExpr) {
			ArrayAsDequeExpr aad = (ArrayAsDequeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aad));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayAsDeque(");
				genExpression(sb, aad.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayAsMapExpr) {
			ArrayAsMapExpr aam = (ArrayAsMapExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aam));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayAsMap(");
				genExpression(sb, aam.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayAsString) {
			ArrayAsString aas = (ArrayAsString)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aas));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.ArrayAsString(");
				genExpression(sb, aas.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, aas.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArraySumExpr) {
			ArraySumExpr as = (ArraySumExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(as));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Sum(");
				genExpression(sb, as.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayProdExpr) {
			ArrayProdExpr ap = (ArrayProdExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ap));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Prod(");
				genExpression(sb, ap.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayMinExpr) {
			ArrayMinExpr am = (ArrayMinExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(am));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Min(");
				genExpression(sb, am.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayMaxExpr) {
			ArrayMaxExpr am = (ArrayMaxExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(am));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Max(");
				genExpression(sb, am.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayAvgExpr) {
			ArrayAvgExpr aa = (ArrayAvgExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(aa));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Avg(");
				genExpression(sb, aa.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayMedExpr) {
			ArrayMedExpr am = (ArrayMedExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(am));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Med(");
				genExpression(sb, am.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayMedUnorderedExpr) {
			ArrayMedUnorderedExpr amu = (ArrayMedUnorderedExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(amu));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.MedUnordered(");
				genExpression(sb, amu.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayVarExpr) {
			ArrayVarExpr av = (ArrayVarExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(av));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Var(");
				genExpression(sb, av.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof ArrayDevExpr) {
			ArrayDevExpr ad = (ArrayDevExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ad));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Dev(");
				genExpression(sb, ad.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof DequeSizeExpr) {
			DequeSizeExpr ds = (DequeSizeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(ds));
			} else {
				sb.append("(");
				genExpression(sb, ds.getTargetExpr(), modifyGenerationState);
				sb.append(").Count");
			}
		} else if(expr instanceof DequeEmptyExpr) {
			DequeEmptyExpr de = (DequeEmptyExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(de));
			} else {
				sb.append("((");
				genExpression(sb, de.getTargetExpr(), modifyGenerationState);
				sb.append(").Count==0)");
			}
		} else if(expr instanceof DequePeekExpr) {
			DequePeekExpr dp = (DequePeekExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dp));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Peek(");
				genExpression(sb, dp.getTargetExpr(), modifyGenerationState);
				if(dp.getNumberExpr() != null) {
					sb.append(", ");
					genExpression(sb, dp.getNumberExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof DequeIndexOfExpr) {
			DequeIndexOfExpr di = (DequeIndexOfExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(di));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
				genExpression(sb, di.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, di.getValueExpr(), modifyGenerationState);
				if(di.getStartIndexExpr() != null) {
					sb.append(", ");
					genExpression(sb, di.getStartIndexExpr(), modifyGenerationState);
				}
				sb.append(")");
			}
		} else if(expr instanceof DequeLastIndexOfExpr) {
			DequeLastIndexOfExpr dli = (DequeLastIndexOfExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dli));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
				genExpression(sb, dli.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dli.getValueExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof DequeSubdequeExpr) {
			DequeSubdequeExpr dsd = (DequeSubdequeExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(dsd));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.Subdeque(");
				genExpression(sb, dsd.getTargetExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dsd.getStartExpr(), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, dsd.getLengthExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof DequeAsSetExpr) {
			DequeAsSetExpr das = (DequeAsSetExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(das));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.DequeAsSet(");
				genExpression(sb, das.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof DequeAsArrayExpr) {
			DequeAsArrayExpr daa = (DequeAsArrayExpr)expr;
			if(modifyGenerationState != null && modifyGenerationState.useVarForResult()) {
				sb.append(modifyGenerationState.mapExprToTempVar().get(daa));
			} else {
				sb.append("GRGEN_LIBGR.ContainerHelper.DequeAsArray(");
				genExpression(sb, daa.getTargetExpr(), modifyGenerationState);
				sb.append(")");
			}
		} else if(expr instanceof MapInit) {
			MapInit mi = (MapInit)expr;
			if(mi.isConstant()) {
				sb.append(mi.getAnonymousMapName());
			} else {
				sb.append("fill_" + mi.getAnonymousMapName() + "(");
				boolean first = true;
				for(ExpressionPair item : mi.getMapItems()) {
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
		} else if(expr instanceof SetInit) {
			SetInit si = (SetInit)expr;
			if(si.isConstant()) {
				sb.append(si.getAnonymousSetName());
			} else {
				sb.append("fill_" + si.getAnonymousSetName() + "(");
				boolean first = true;
				for(Expression item : si.getSetItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getType()) + ")(");
					genExpression(sb, item, modifyGenerationState);
					if(item instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		} else if(expr instanceof ArrayInit) {
			ArrayInit ai = (ArrayInit)expr;
			if(ai.isConstant()) {
				sb.append(ai.getAnonymousArrayName());
			} else {
				sb.append("fill_" + ai.getAnonymousArrayName() + "(");
				boolean first = true;
				for(Expression item : ai.getArrayItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getType()) + ")(");
					genExpression(sb, item, modifyGenerationState);
					if(item instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		} else if(expr instanceof DequeInit) {
			DequeInit di = (DequeInit)expr;
			if(di.isConstant()) {
				sb.append(di.getAnonymousDequeName());
			} else {
				sb.append("fill_" + di.getAnonymousDequeName() + "(");
				boolean first = true;
				for(Expression item : di.getDequeItems()) {
					if(first)
						first = false;
					else
						sb.append(", ");

					if(item instanceof GraphEntityExpression)
						sb.append("(" + formatElementInterfaceRef(item.getType()) + ")(");
					genExpression(sb, item, modifyGenerationState);
					if(item instanceof GraphEntityExpression)
						sb.append(")");
				}
				sb.append(")");
			}
		} else if(expr instanceof MapCopyConstructor) {
			MapCopyConstructor mcc = (MapCopyConstructor)expr;
			sb.append("GRGEN_LIBGR.ContainerHelper.FillMap(");
			sb.append("new " + formatType(mcc.getMapType()) + "(), ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getKeyType()) + "\", ");
			sb.append("\"" + formatSequenceType(mcc.getMapType().getValueType()) + "\", ");
			genExpression(sb, mcc.getMapToCopy(), modifyGenerationState);
			sb.append(", graph.Model");
			sb.append(")");
		} else if(expr instanceof SetCopyConstructor) {
			SetCopyConstructor scc = (SetCopyConstructor)expr;
			sb.append("GRGEN_LIBGR.ContainerHelper.FillSet(");
			sb.append("new " + formatType(scc.getSetType()) + "(), ");
			sb.append("\"" + formatSequenceType(scc.getSetType().getValueType()) + "\", ");
			genExpression(sb, scc.getSetToCopy(), modifyGenerationState);
			sb.append(", graph.Model");
			sb.append(")");
		} else if(expr instanceof ArrayCopyConstructor) {
			ArrayCopyConstructor acc = (ArrayCopyConstructor)expr;
			sb.append("GRGEN_LIBGR.ContainerHelper.FillArray(");
			sb.append("new " + formatType(acc.getArrayType()) + "(), ");
			sb.append("\"" + formatSequenceType(acc.getArrayType().getValueType()) + "\", ");
			genExpression(sb, acc.getArrayToCopy(), modifyGenerationState);
			sb.append(", graph.Model");
			sb.append(")");
		} else if(expr instanceof DequeCopyConstructor) {
			DequeCopyConstructor dcc = (DequeCopyConstructor)expr;
			sb.append("GRGEN_LIBGR.ContainerHelper.FillDeque(");
			sb.append("new " + formatType(dcc.getDequeType()) + "(), ");
			sb.append("\"" + formatSequenceType(dcc.getDequeType().getValueType()) + "\", ");
			genExpression(sb, dcc.getDequeToCopy(), modifyGenerationState);
			sb.append(", graph.Model");
			sb.append(")");
		} else if(expr instanceof FunctionInvocationExpr) {
			FunctionInvocationExpr fi = (FunctionInvocationExpr)expr;
			sb.append("GRGEN_ACTIONS." + getPackagePrefixDot(fi.getFunction()) + "Functions."
					+ fi.getFunction().getIdent().toString() + "(actionEnv, graph");
			for(int i = 0; i < fi.arity(); ++i) {
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
		} else if(expr instanceof ExternalFunctionInvocationExpr) {
			ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
			sb.append("GRGEN_EXPR.ExternalFunctions." + efi.getExternalFunc().getIdent().toString()
					+ "(actionEnv, graph");
			for(int i = 0; i < efi.arity(); ++i) {
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
		} else if(expr instanceof FunctionMethodInvocationExpr) {
			FunctionMethodInvocationExpr fmi = (FunctionMethodInvocationExpr)expr;
			Entity owner = fmi.getOwner();
			sb.append("((" + formatElementInterfaceRef(owner.getType()) + ") ");
			sb.append(formatEntity(owner) + ").@");
			sb.append(fmi.getFunction().getIdent().toString() + "(actionEnv, graph");
			for(int i = 0; i < fmi.arity(); ++i) {
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
		} else if(expr instanceof ExternalFunctionMethodInvocationExpr) {
			ExternalFunctionMethodInvocationExpr efmi = (ExternalFunctionMethodInvocationExpr)expr;
			sb.append("(");
			genExpression(sb, efmi.getOwner(), modifyGenerationState);
			sb.append(").@");
			sb.append(efmi.getExternalFunc().getIdent().toString() + "(actionEnv, graph");
			for(int i = 0; i < efmi.arity(); ++i) {
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
		} else if(expr instanceof EdgesExpr) {
			EdgesExpr e = (EdgesExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.Edges");
			sb.append(getDirectednessSuffix(e.getType()));
			sb.append("(graph, ");
			genExpression(sb, e.getEdgeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof NodesExpr) {
			NodesExpr n = (NodesExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.Nodes(graph, ");
			genExpression(sb, n.getNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountEdgesExpr) {
			CountEdgesExpr ce = (CountEdgesExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.CountEdges(graph, ");
			genExpression(sb, ce.getEdgeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountNodesExpr) {
			CountNodesExpr cn = (CountNodesExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.CountNodes(graph, ");
			genExpression(sb, cn.getNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof NowExpr) {
			//NowExpr n = (NowExpr)expr;
			sb.append("DateTime.UtcNow.ToFileTime()");
		} else if(expr instanceof EmptyExpr) {
			//EmptyExpr e = (EmptyExpr)expr;
			sb.append("(graph.NumNodes+graph.NumEdges == 0)");
		} else if(expr instanceof SizeExpr) {
			//SizeExpr s = (SizeExpr)expr;
			sb.append("(graph.NumNodes+graph.NumEdges)");
		} else if(expr instanceof SourceExpr) {
			SourceExpr s = (SourceExpr)expr;
			sb.append("((");
			genExpression(sb, s.getEdgeExpr(), modifyGenerationState);
			sb.append(").Source)");
		} else if(expr instanceof TargetExpr) {
			TargetExpr t = (TargetExpr)expr;
			sb.append("((");
			genExpression(sb, t.getEdgeExpr(), modifyGenerationState);
			sb.append(").Target)");
		} else if(expr instanceof OppositeExpr) {
			OppositeExpr o = (OppositeExpr)expr;
			sb.append("((");
			genExpression(sb, o.getEdgeExpr(), modifyGenerationState);
			sb.append(").Opposite(");
			genExpression(sb, o.getNodeExpr(), modifyGenerationState);
			sb.append("))");
		} else if(expr instanceof NodeByNameExpr) {
			NodeByNameExpr nbn = (NodeByNameExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, ");
			genExpression(sb, nbn.getNameExpr(), modifyGenerationState);
			if(!nbn.getNodeTypeExpr().getType().getIdent().toString().equals("Node")) {
				sb.append(", ");
				genExpression(sb, nbn.getNodeTypeExpr(), modifyGenerationState);
			}
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof EdgeByNameExpr) {
			EdgeByNameExpr ebn = (EdgeByNameExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, ");
			genExpression(sb, ebn.getNameExpr(), modifyGenerationState);
			if(!ebn.getEdgeTypeExpr().getType().getIdent().toString().equals("AEdge")) {
				sb.append(", ");
				genExpression(sb, ebn.getEdgeTypeExpr(), modifyGenerationState);
			}
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof NodeByUniqueExpr) {
			NodeByUniqueExpr nbu = (NodeByUniqueExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.GetNode(graph, ");
			genExpression(sb, nbu.getUniqueExpr(), modifyGenerationState);
			if(!nbu.getNodeTypeExpr().getType().getIdent().toString().equals("Node")) {
				sb.append(", ");
				genExpression(sb, nbu.getNodeTypeExpr(), modifyGenerationState);
			}
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof EdgeByUniqueExpr) {
			EdgeByUniqueExpr ebu = (EdgeByUniqueExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.GetEdge(graph, ");
			genExpression(sb, ebu.getUniqueExpr(), modifyGenerationState);
			if(!ebu.getEdgeTypeExpr().getType().getIdent().toString().equals("AEdge")) {
				sb.append(", ");
				genExpression(sb, ebu.getEdgeTypeExpr(), modifyGenerationState);
			}
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IncidentEdgeExpr) {
			IncidentEdgeExpr ie = (IncidentEdgeExpr)expr;
			if(ie.Direction() == IncidentEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.Outgoing");
			} else if(ie.Direction() == IncidentEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.Incoming");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Incident");
			}
			sb.append(getDirectednessSuffix(ie.getType()));
			sb.append("(");
			genExpression(sb, ie.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ie.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ie.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof AdjacentNodeExpr) {
			AdjacentNodeExpr an = (AdjacentNodeExpr)expr;
			if(an.Direction() == AdjacentNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.AdjacentOutgoing(");
			} else if(an.Direction() == AdjacentNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.AdjacentIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Adjacent(");
			}
			genExpression(sb, an.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, an.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, an.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountIncidentEdgeExpr) {
			CountIncidentEdgeExpr cie = (CountIncidentEdgeExpr)expr;
			if(cie.Direction() == CountIncidentEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountOutgoing(");
			} else if(cie.Direction() == CountIncidentEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountIncident(");
			}
			genExpression(sb, cie.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cie.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cie.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountAdjacentNodeExpr) {
			CountAdjacentNodeExpr can = (CountAdjacentNodeExpr)expr;
			if(can.Direction() == CountAdjacentNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountAdjacentOutgoing(graph, ");
			} else if(can.Direction() == CountAdjacentNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountAdjacentIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountAdjacent(graph, ");
			}
			genExpression(sb, can.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, can.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, can.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsAdjacentNodeExpr) {
			IsAdjacentNodeExpr ian = (IsAdjacentNodeExpr)expr;
			if(ian.Direction() == IsReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsAdjacentOutgoing(");
			} else if(ian.Direction() == IsReachableNodeExpr.INCOMING) {
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
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsIncidentEdgeExpr) {
			IsIncidentEdgeExpr iie = (IsIncidentEdgeExpr)expr;
			if(iie.Direction() == IsReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsOutgoing(");
			} else if(iie.Direction() == IsReachableEdgeExpr.INCOMING) {
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
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof ReachableEdgeExpr) {
			ReachableEdgeExpr re = (ReachableEdgeExpr)expr;
			if(re.Direction() == ReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing");
			} else if(re.Direction() == ReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableEdges");
			}
			sb.append(getDirectednessSuffix(re.getType()));
			sb.append("(graph, ");
			genExpression(sb, re.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, re.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, re.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof ReachableNodeExpr) {
			ReachableNodeExpr rn = (ReachableNodeExpr)expr;
			if(rn.Direction() == ReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableOutgoing(");
			} else if(rn.Direction() == ReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.ReachableIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.Reachable(");
			}
			genExpression(sb, rn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, rn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, rn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountReachableEdgeExpr) {
			CountReachableEdgeExpr cre = (CountReachableEdgeExpr)expr;
			if(cre.Direction() == CountReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesOutgoing(graph, ");
			} else if(cre.Direction() == CountReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachableEdges(graph, ");
			}
			genExpression(sb, cre.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cre.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cre.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountReachableNodeExpr) {
			CountReachableNodeExpr crn = (CountReachableNodeExpr)expr;
			if(crn.Direction() == CountReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachableOutgoing(");
			} else if(crn.Direction() == CountReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachableIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountReachable(");
			}
			genExpression(sb, crn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, crn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, crn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsReachableNodeExpr) {
			IsReachableNodeExpr irn = (IsReachableNodeExpr)expr;
			if(irn.Direction() == IsReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableOutgoing(graph, ");
			} else if(irn.Direction() == IsReachableNodeExpr.INCOMING) {
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
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsReachableEdgeExpr) {
			IsReachableEdgeExpr ire = (IsReachableEdgeExpr)expr;
			if(ire.Direction() == IsReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesOutgoing(graph, ");
			} else if(ire.Direction() == IsReachableEdgeExpr.INCOMING) {
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
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof BoundedReachableEdgeExpr) {
			BoundedReachableEdgeExpr bre = (BoundedReachableEdgeExpr)expr;
			if(bre.Direction() == BoundedReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing");
			} else if(bre.Direction() == BoundedReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdges");
			}
			sb.append(getDirectednessSuffix(bre.getType()));
			sb.append("(graph, ");
			genExpression(sb, bre.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, bre.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, bre.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, bre.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof BoundedReachableNodeExpr) {
			BoundedReachableNodeExpr brn = (BoundedReachableNodeExpr)expr;
			if(brn.Direction() == BoundedReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(");
			} else if(brn.Direction() == BoundedReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachable(");
			}
			genExpression(sb, brn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brn.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof BoundedReachableNodeWithRemainingDepthExpr) {
			BoundedReachableNodeWithRemainingDepthExpr brnwrd = (BoundedReachableNodeWithRemainingDepthExpr)expr;
			if(brnwrd.Direction() == BoundedReachableNodeWithRemainingDepthExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthOutgoing(");
			} else if(brnwrd.Direction() == BoundedReachableNodeWithRemainingDepthExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepth(");
			}
			genExpression(sb, brnwrd.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brnwrd.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brnwrd.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, brnwrd.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountBoundedReachableEdgeExpr) {
			CountBoundedReachableEdgeExpr cbre = (CountBoundedReachableEdgeExpr)expr;
			if(cbre.Direction() == CountBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesOutgoing(graph, ");
			} else if(cbre.Direction() == CountBoundedReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdges(graph, ");
			}
			genExpression(sb, cbre.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbre.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbre.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbre.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof CountBoundedReachableNodeExpr) {
			CountBoundedReachableNodeExpr cbrn = (CountBoundedReachableNodeExpr)expr;
			if(cbrn.Direction() == CountBoundedReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableOutgoing(");
			} else if(cbrn.Direction() == CountBoundedReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableIncoming(");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.CountBoundedReachable(");
			}
			genExpression(sb, cbrn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbrn.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbrn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, cbrn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsBoundedReachableNodeExpr) {
			IsBoundedReachableNodeExpr ibrn = (IsBoundedReachableNodeExpr)expr;
			if(ibrn.Direction() == IsBoundedReachableNodeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableOutgoing(graph, ");
			} else if(ibrn.Direction() == IsBoundedReachableNodeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachable(graph, ");
			}
			genExpression(sb, ibrn.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibrn.getEndNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibrn.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibrn.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibrn.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof IsBoundedReachableEdgeExpr) {
			IsBoundedReachableEdgeExpr ibre = (IsBoundedReachableEdgeExpr)expr;
			if(ibre.Direction() == IsBoundedReachableEdgeExpr.OUTGOING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesOutgoing(graph, ");
			} else if(ibre.Direction() == IsBoundedReachableEdgeExpr.INCOMING) {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesIncoming(graph, ");
			} else {
				sb.append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdges(graph, ");
			}
			genExpression(sb, ibre.getStartNodeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibre.getEndEdgeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibre.getDepthExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibre.getIncidentEdgeTypeExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, ibre.getAdjacentNodeTypeExpr(), modifyGenerationState);
			if(modifyGenerationState.emitProfilingInstrumentation())
				sb.append(", actionEnv");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof InducedSubgraphExpr) {
			InducedSubgraphExpr is = (InducedSubgraphExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, is.getSetExpr(), modifyGenerationState);
			sb.append(", graph)");
		} else if(expr instanceof DefinedSubgraphExpr) {
			DefinedSubgraphExpr ds = (DefinedSubgraphExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.DefinedSubgraph");
			switch(getDirectednessSuffix(ds.getSetExpr().getType())) {
			case "Directed":
				sb.append("Directed(");
				sb.append("(IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)");
				break;
			case "Undirected":
				sb.append("Undirected(");
				sb.append("(IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)");
				break;
			default:
				sb.append("(");
				sb.append("(IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
				break;
			}
			genExpression(sb, ds.getSetExpr(), modifyGenerationState);
			sb.append(", graph)");
		} else if(expr instanceof EqualsAnyExpr) {
			EqualsAnyExpr ea = (EqualsAnyExpr)expr;
			sb.append("GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)");
			genExpression(sb, ea.getSubgraphExpr(), modifyGenerationState);
			sb.append(", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)");
			genExpression(sb, ea.getSetExpr(), modifyGenerationState);
			sb.append(", ");
			sb.append(ea.getIncludingAttributes() ? "true" : "false");
			if(modifyGenerationState.isToBeParallelizedActionExisting())
				sb.append(", threadId");
			sb.append(")");
		} else if(expr instanceof MaxExpr) {
			MaxExpr m = (MaxExpr)expr;
			sb.append("Math.Max(");
			genExpression(sb, m.getLeftExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, m.getRightExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof MinExpr) {
			MinExpr m = (MinExpr)expr;
			sb.append("Math.Min(");
			genExpression(sb, m.getLeftExpr(), modifyGenerationState);
			sb.append(", ");
			genExpression(sb, m.getRightExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof AbsExpr) {
			AbsExpr a = (AbsExpr)expr;
			sb.append("Math.Abs(");
			genExpression(sb, a.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof SgnExpr) {
			SgnExpr s = (SgnExpr)expr;
			sb.append("Math.Sign(");
			genExpression(sb, s.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof PiExpr) {
			//PiExpr pi = (PiExpr)expr;
			sb.append("Math.PI");
		} else if(expr instanceof EExpr) {
			//EExpr e = (EExpr)expr;
			sb.append("Math.E");
		} else if(expr instanceof ByteMinExpr) {
			sb.append("SByte.MinValue");
		} else if(expr instanceof ByteMaxExpr) {
			sb.append("SByte.MaxValue");
		} else if(expr instanceof ShortMinExpr) {
			sb.append("Int16.MinValue");
		} else if(expr instanceof ShortMaxExpr) {
			sb.append("Int16.MaxValue");
		} else if(expr instanceof IntMinExpr) {
			sb.append("Int32.MinValue");
		} else if(expr instanceof IntMaxExpr) {
			sb.append("Int32.MaxValue");
		} else if(expr instanceof LongMinExpr) {
			sb.append("Int64.MinValue");
		} else if(expr instanceof LongMaxExpr) {
			sb.append("Int64.MaxValue");
		} else if(expr instanceof FloatMinExpr) {
			sb.append("Single.MinValue");
		} else if(expr instanceof FloatMaxExpr) {
			sb.append("Single.MaxValue");
		} else if(expr instanceof DoubleMinExpr) {
			sb.append("Double.MinValue");
		} else if(expr instanceof DoubleMaxExpr) {
			sb.append("Double.MaxValue");
		} else if(expr instanceof CeilExpr) {
			CeilExpr c = (CeilExpr)expr;
			sb.append("Math.Ceiling(");
			genExpression(sb, c.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof FloorExpr) {
			FloorExpr f = (FloorExpr)expr;
			sb.append("Math.Floor(");
			genExpression(sb, f.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof RoundExpr) {
			RoundExpr r = (RoundExpr)expr;
			sb.append("Math.Round(");
			genExpression(sb, r.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof TruncateExpr) {
			TruncateExpr t = (TruncateExpr)expr;
			sb.append("Math.Truncate(");
			genExpression(sb, t.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof SinCosTanExpr) {
			SinCosTanExpr sct = (SinCosTanExpr)expr;
			switch(sct.getWhich()) {
			case sin:
				sb.append("Math.Sin(");
				break;
			case cos:
				sb.append("Math.Cos(");
				break;
			case tan:
				sb.append("Math.Tan(");
				break;
			}
			genExpression(sb, sct.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof ArcSinCosTanExpr) {
			ArcSinCosTanExpr asct = (ArcSinCosTanExpr)expr;
			switch(asct.getWhich()) {
			case arcsin:
				sb.append("Math.Asin(");
				break;
			case arccos:
				sb.append("Math.Acos(");
				break;
			case arctan:
				sb.append("Math.Atan(");
				break;
			}
			genExpression(sb, asct.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof CanonizeExpr) {
			CanonizeExpr c = (CanonizeExpr)expr;
			sb.append("(");
			genExpression(sb, c.getGraphExpr(), modifyGenerationState);
			sb.append(").Canonize()");
		} else if(expr instanceof SqrExpr) {
			SqrExpr s = (SqrExpr)expr;
			sb.append("GRGEN_LIBGR.MathHelper.Sqr(");
			genExpression(sb, s.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof SqrtExpr) {
			SqrtExpr s = (SqrtExpr)expr;
			sb.append("Math.Sqrt(");
			genExpression(sb, s.getExpr(), modifyGenerationState);
			sb.append(")");
		} else if(expr instanceof PowExpr) {
			PowExpr p = (PowExpr)expr;
			if(p.getLeftExpr() != null) {
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
		} else if(expr instanceof LogExpr) {
			LogExpr l = (LogExpr)expr;
			sb.append("Math.Log(");
			genExpression(sb, l.getLeftExpr(), modifyGenerationState);
			if(l.getRightExpr() != null) {
				sb.append(", ");
				genExpression(sb, l.getRightExpr(), modifyGenerationState);
			}
			sb.append(")");
		} else if(expr instanceof ProjectionExpr) {
			ProjectionExpr proj = (ProjectionExpr)expr;
			sb.append(proj.getProjectedValueVarName());
		} else if(expr instanceof MatchAccess) {
			MatchAccess ma = (MatchAccess)expr;
			genExpression(sb, ma.getExpr(), modifyGenerationState);
			sb.append(".");
			sb.append(formatEntity(ma.getEntity()));
		} else if(expr instanceof IteratedQueryExpr) {
			IteratedQueryExpr iq = (IteratedQueryExpr)expr;
			sb.append("curMatch." + iq.getIteratedName().toString() + ".ToListExact()");
		} else
			throw new UnsupportedOperationException("Unsupported expression type (" + expr + ")");
	}

	public void genOperator(SourceBuilder sb, Operator op,
			ExpressionGenerationState modifyGenerationState)
	{
		switch(op.arity()) {
		case 1:
			sb.append("(" + opSymbols[op.getOpCode()] + " ");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(")");
			break;
		case 2:
			genBinaryOperator(sb, op, modifyGenerationState);
			break;
		case 3:
			if(op.getOpCode() == Operator.COND) {
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

	public void genBinaryOperator(SourceBuilder sb, Operator op,
			ExpressionGenerationState modifyGenerationState)
	{
		switch(op.getOpCode()) {
		case Operator.IN: {
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

		case Operator.ADD: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else
				genBinOpDefault(sb, op, modifyGenerationState);
			break;
		}

		case Operator.BIT_OR: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Union(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else
				genBinOpDefault(sb, op, modifyGenerationState);
			break;
		}

		case Operator.BIT_AND: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Intersect(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else
				genBinOpDefault(sb, op, modifyGenerationState);
			break;
		}

		case Operator.EXCEPT: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Except(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else
				genBinOpDefault(sb, op, modifyGenerationState);
			break;
		}

		case Operator.EQ: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.Equal(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof GraphType) {
				sb.append("((GRGEN_LIBGR.IGraph)");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(modifyGenerationState.model().isEqualClassDefined()
					&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
				sb.append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(",");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		case Operator.NE: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof GraphType) {
				sb.append("!((GRGEN_LIBGR.IGraph)");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(modifyGenerationState.model().isEqualClassDefined()
					&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
				sb.append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(",");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		case Operator.SE: {
			sb.append("((GRGEN_LIBGR.IGraph)");
			genExpression(sb, op.getOperand(0), modifyGenerationState);
			sb.append(").HasSameStructure((GRGEN_LIBGR.IGraph)");
			genExpression(sb, op.getOperand(1), modifyGenerationState);
			sb.append(")");
			break;
		}

		case Operator.GT: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof StringType) {
				sb.append("(String.Compare(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(", StringComparison.InvariantCulture)>0)");
			} else if(modifyGenerationState.model().isLowerClassDefined()
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
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		case Operator.GE: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof StringType) {
				sb.append("(String.Compare(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(", StringComparison.InvariantCulture)>=0)");
			} else if(modifyGenerationState.model().isLowerClassDefined()
					&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
				sb.append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(",");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		case Operator.LT: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessThan(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof StringType) {
				sb.append("(String.Compare(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(", StringComparison.InvariantCulture)<0)");
			} else if(modifyGenerationState.model().isLowerClassDefined()
					&& (opType instanceof ObjectType || opType instanceof ExternalType)) {
				sb.append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(",");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		case Operator.LE: {
			Type opType = op.getOperand(0).getType();
			if(opType instanceof MapType || opType instanceof SetType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof ArrayType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof DequeType) {
				sb.append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(")");
			} else if(opType instanceof StringType) {
				sb.append("(String.Compare(");
				genExpression(sb, op.getOperand(0), modifyGenerationState);
				sb.append(", ");
				genExpression(sb, op.getOperand(1), modifyGenerationState);
				sb.append(", StringComparison.InvariantCulture)<=0)");
			} else if(modifyGenerationState.model().isLowerClassDefined()
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
			} else {
				genBinOpDefault(sb, op, modifyGenerationState);
			}
			break;
		}

		default:
			genBinOpDefault(sb, op, modifyGenerationState);
			break;
		}
	}

	protected String formatGlobalVariableRead(Entity globalVar)
	{
		return "((" + formatType(globalVar.getType())
				+ ")((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).GetVariableValue(\""
				+ formatIdentifiable(globalVar) + "\"))";
	}

	protected String formatGlobalVariableWrite(Entity globalVar, String value)
	{
		return "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).SetVariableValue(\""
				+ formatIdentifiable(globalVar) + "\", (" + formatType(globalVar.getType()) + ")(" + value + "))";
	}

	protected String getValueAsCSSharpString(Constant constant)
	{
		Type type = constant.getType();

		//emit C-code for constants
		switch(type.classify()) {
		case Type.IS_STRING:
			Object value = constant.getValue();
			if(value == null)
				return "null";
			else
				return "\"" + constant.getValue() + "\"";
		case Type.IS_BOOLEAN:
			Boolean bool_const = (Boolean)constant.getValue();
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
			return constant.getValue().toString() + "L";
		case Type.IS_FLOAT:
			return constant.getValue().toString() + "f";
		case Type.IS_TYPE:
			InheritanceType it = (InheritanceType)constant.getValue();
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
			return "0L";
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
		case Type.IS_EXTERNAL_TYPE:
			return formatType(cast.getType());
		case Type.IS_NODE:
			return formatType(cast.getType());
		case Type.IS_EDGE:
			return formatType(cast.getType());
		case Type.IS_SET:
		case Type.IS_MAP:
		case Type.IS_ARRAY:
		case Type.IS_DEQUE:
			if(cast.getType().classify() == Type.IS_SET) {
				// cast to set<Edge> or set<UEdge> from set<AEdge> allowed at compile time, requires check at runtime for directedness
				if(((SetType)cast.getType()).getValueType().getIdent().toString().equals("Edge"))
					return "directed set";
				else if(((SetType)cast.getType()).getValueType().getIdent().toString().equals("UEdge"))
					return "undirected set";
			}
			return "object"; // besides, only the null type can/will be casted into a container type, so the most specific base type is sufficient, which is object
		default:
			throw new UnsupportedOperationException(
					"This is either a forbidden cast, which should have been " +
							"rejected on building the IR, or an allowed cast, which " +
							"should have been processed by the above code.");
		}
	}

	protected String getTypeNameForTempVarDecl(Type type)
	{
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
			return "GRGEN_MODEL." + type.getIdent();
		case Type.IS_NODE:
			return formatElementInterfaceRef(type);
		case Type.IS_EDGE:
			return formatElementInterfaceRef(type);
		default:
			throw new IllegalArgumentException();
		}
	}

	protected String escapeBackslashAndDoubleQuotes(String input)
	{
		return input.replace("\\", "\\\\").replace("\"", "\\\"");
	}

	protected abstract void genQualAccess(SourceBuilder sb, Qualification qual, Object modifyGenerationState);

	protected abstract void genMemberAccess(SourceBuilder sb, Entity member);

	protected void addAnnotations(SourceBuilder sb, Identifiable ident, String targetName)
	{
		for(String annotationKey : ident.getAnnotations().keySet()) {
			String annotationValue = ident.getAnnotations().get(annotationKey).toString();
			sb.appendFront(targetName + ".annotations.Add(\"" + annotationKey + "\", \"" + annotationValue + "\");\n");
		}
	}

	protected void forceNotConstant(List<EvalStatement> statements)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(EvalStatement eval : statements) {
			eval.collectNeededEntities(needs);
		}
		forceNotConstant(needs);
	}

	protected void forceNotConstant(NeededEntities needs)
	{
		// todo: more fine-grained never assigned, the important thing is that the constant constructor is temporary, not assigned to a variable
		for(Expression containerExpr : needs.containerExprs) {
			if(containerExpr instanceof MapInit) {
				MapInit mapInit = (MapInit)containerExpr;
				mapInit.forceNotConstant();
			} else if(containerExpr instanceof SetInit) {
				SetInit setInit = (SetInit)containerExpr;
				setInit.forceNotConstant();
			} else if(containerExpr instanceof ArrayInit) {
				ArrayInit arrayInit = (ArrayInit)containerExpr;
				arrayInit.forceNotConstant();
			} else if(containerExpr instanceof DequeInit) {
				DequeInit dequeInit = (DequeInit)containerExpr;
				dequeInit.forceNotConstant();
			}
		}
	}

	protected void genLocalContainersEvals(SourceBuilder sb, Collection<EvalStatement> evals,
			List<String> staticInitializers, String pathPrefixForElements,
			HashMap<Entity, String> alreadyDefinedEntityToName)
	{
		NeededEntities needs = new NeededEntities(false, false, false, false, false, true, false, false);
		for(EvalStatement eval : evals) {
			eval.collectNeededEntities(needs);
		}
		genLocalContainers(sb, needs, staticInitializers, false);
	}

	protected void genLocalContainers(SourceBuilder sb, NeededEntities needs, List<String> staticInitializers,
			boolean neverAssigned)
	{
		// todo: more fine-grained never assigned, the important thing is that the constant constructor is temporary, not assigned to a variable
		sb.append("\n");
		for(Expression containerExpr : needs.containerExprs) {
			if(containerExpr instanceof MapInit) {
				MapInit mapInit = (MapInit)containerExpr;
				if(!neverAssigned)
					mapInit.forceNotConstant();
				genLocalMap(sb, mapInit, staticInitializers);
			} else if(containerExpr instanceof SetInit) {
				SetInit setInit = (SetInit)containerExpr;
				if(!neverAssigned)
					setInit.forceNotConstant();
				genLocalSet(sb, setInit, staticInitializers);
			} else if(containerExpr instanceof ArrayInit) {
				ArrayInit arrayInit = (ArrayInit)containerExpr;
				if(!neverAssigned)
					arrayInit.forceNotConstant();
				genLocalArray(sb, arrayInit, staticInitializers);
			} else if(containerExpr instanceof DequeInit) {
				DequeInit dequeInit = (DequeInit)containerExpr;
				if(!neverAssigned)
					dequeInit.forceNotConstant();
				genLocalDeque(sb, dequeInit, staticInitializers);
			}
		}
	}

	protected void genLocalMap(SourceBuilder sb, MapInit mapInit, List<String> staticInitializers)
	{
		String mapName = mapInit.getAnonymousMapName();
		String attrType = formatAttributeType(mapInit.getType());
		if(mapInit.isConstant()) {
			sb.appendFront("public static readonly " + attrType + " " + mapName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + mapName);
			sb.appendFront("static void init_" + mapName + "() {\n");
			sb.indent();
			for(ExpressionPair item : mapInit.getMapItems()) {
				sb.appendFront("");
				sb.append(mapName);
				sb.append("[");
				genExpression(sb, item.getKeyExpr(), null);
				sb.append("] = ");
				genExpression(sb, item.getValueExpr(), null);
				sb.append(";\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("public static " + attrType + " fill_" + mapName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(ExpressionPair item : mapInit.getMapItems()) {
				String itemKeyType = formatType(item.getKeyExpr().getType());
				String itemValueType = formatType(item.getValueExpr().getType());
				if(first) {
					sb.append(itemKeyType + " itemkey" + itemCounter + ",");
					sb.append(itemValueType + " itemvalue" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemKeyType + " itemkey" + itemCounter + ",");
					sb.append(itemValueType + " itemvalue" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.indent();
			sb.appendFront(attrType + " " + mapName + " = " +
					"new " + attrType + "();\n");

			int itemLength = mapInit.getMapItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.appendFront(mapName);
				sb.append("[" + "itemkey" + itemCounter + "] = itemvalue" + itemCounter + ";\n");
			}
			sb.appendFront("return " + mapName + ";\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	protected void genLocalSet(SourceBuilder sb, SetInit setInit, List<String> staticInitializers)
	{
		String setName = setInit.getAnonymousSetName();
		String attrType = formatAttributeType(setInit.getType());
		if(setInit.isConstant()) {
			sb.appendFront("public static readonly " + attrType + " " + setName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + setName);
			sb.appendFront("static void init_" + setName + "() {\n");
			sb.indent();
			for(Expression item : setInit.getSetItems()) {
				sb.appendFront(setName);
				sb.append("[");
				genExpression(sb, item, null);
				sb.append("] = null;\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("public static " + attrType + " fill_" + setName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(Expression item : setInit.getSetItems()) {
				String itemType = formatType(item.getType());
				if(first) {
					sb.append(itemType + " item" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemType + " item" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.indent();
			sb.appendFront(attrType + " " + setName + " = " +
					"new " + attrType + "();\n");

			int itemLength = setInit.getSetItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.appendFront(setName);
				sb.append("[" + "item" + itemCounter + "] = null;\n");
			}
			sb.appendFront("return " + setName + ";\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	protected void genLocalArray(SourceBuilder sb, ArrayInit arrayInit, List<String> staticInitializers)
	{
		String arrayName = arrayInit.getAnonymousArrayName();
		String attrType = formatAttributeType(arrayInit.getType());
		if(arrayInit.isConstant()) {
			sb.appendFront("public static readonly " + attrType + " " + arrayName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + arrayName);
			sb.appendFront("static void init_" + arrayName + "() {\n");
			sb.indent();
			for(Expression item : arrayInit.getArrayItems()) {
				sb.appendFront(arrayName);
				sb.append(".Add(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("public static " + attrType + " fill_" + arrayName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(Expression item : arrayInit.getArrayItems()) {
				String itemType = formatType(item.getType());
				if(first) {
					sb.append(itemType + " item" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemType + " item" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.indent();
			sb.appendFront(attrType + " " + arrayName + " = " +
					"new " + attrType + "();\n");

			int itemLength = arrayInit.getArrayItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.appendFront(arrayName);
				sb.append(".Add(" + "item" + itemCounter + ");\n");
			}
			sb.appendFront("return " + arrayName + ";\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	protected void genLocalDeque(SourceBuilder sb, DequeInit dequeInit, List<String> staticInitializers)
	{
		String dequeName = dequeInit.getAnonymousDequeName();
		String attrType = formatAttributeType(dequeInit.getType());
		if(dequeInit.isConstant()) {
			sb.appendFront("public static readonly " + attrType + " " + dequeName + " = " +
					"new " + attrType + "();\n");
			staticInitializers.add("init_" + dequeName);
			sb.appendFront("static void init_" + dequeName + "() {\n");
			sb.indent();
			for(Expression item : dequeInit.getDequeItems()) {
				sb.appendFront("");
				sb.append(dequeName);
				sb.append(".Add(");
				genExpression(sb, item, null);
				sb.append(");\n");
			}
			sb.unindent();
			sb.appendFront("}\n");
		} else {
			sb.appendFront("public static " + attrType + " fill_" + dequeName + "(");
			int itemCounter = 0;
			boolean first = true;
			for(Expression item : dequeInit.getDequeItems()) {
				String itemType = formatType(item.getType());
				if(first) {
					sb.append(itemType + " item" + itemCounter);
					first = false;
				} else {
					sb.append(", " + itemType + " item" + itemCounter);
				}
				++itemCounter;
			}
			sb.append(") {\n");
			sb.indent();
			sb.appendFront(attrType + " " + dequeName + " = " +
					"new " + attrType + "();\n");

			int itemLength = dequeInit.getDequeItems().size();
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter) {
				sb.appendFront(dequeName);
				sb.append(".Enqueue(" + "item" + itemCounter + ");\n");
			}
			sb.appendFront("return " + dequeName + ";\n");
			sb.unindent();
			sb.appendFront("}\n");
		}
	}

	protected void genCompareMethod(SourceBuilder sb, String typeName,
			String attributeOrMemberName, Type attributeOrMemberType, boolean ascending)
	{
		if(ascending)
			sb.appendFront("public override int Compare(" + typeName + " a, " + typeName + " b)\n");
		else
			sb.appendFront("public override int Compare(" + typeName + " b, " + typeName + " a)\n");
		sb.appendFront("{\n");
		sb.indent();
		if(attributeOrMemberType.classify() == Type.IS_EXTERNAL_TYPE
				|| attributeOrMemberType.classify() == Type.IS_OBJECT) {
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsEqual(a.@" + attributeOrMemberName + ", b.@"
					+ attributeOrMemberName + ")) return 0;\n");
			sb.appendFront("if(AttributeTypeObjectCopierComparer.IsLower(a.@" + attributeOrMemberName + ", b.@"
					+ attributeOrMemberName + ")) return -1;\n");
			sb.appendFront("return 1;\n");
		} else if(attributeOrMemberType instanceof StringType)
			sb.appendFront("return StringComparer.InvariantCulture.Compare(a.@" + attributeOrMemberName + ", b.@"
					+ attributeOrMemberName + ");\n");
		else
			sb.appendFront("return a.@" + attributeOrMemberName + ".CompareTo(b.@" + attributeOrMemberName + ");\n");
		sb.unindent();
		sb.appendFront("}\n");
	}

	protected void generateArrayKeepOneForEach(SourceBuilder sb, String arrayFunctionName, String matchInterfaceName,
			String attributeOrMemberName, String attributeOrMemberType)
	{
		sb.appendFront("public static List<" + matchInterfaceName + "> " + arrayFunctionName
				+ "(List<" + matchInterfaceName + "> list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("List<" + matchInterfaceName + "> newList = new List<" + matchInterfaceName + ">();\n");

		sb.appendFront("Dictionary<" + attributeOrMemberType + ", GRGEN_LIBGR.SetValueType> alreadySeenMembers "
				+ "= new Dictionary<" + attributeOrMemberType + ", GRGEN_LIBGR.SetValueType>();\n");
		sb.appendFront("foreach(" + matchInterfaceName + " element in list)\n");
		sb.appendFront("{\n");
		sb.indent();
		sb.appendFront("if(!alreadySeenMembers.ContainsKey(element.@" + attributeOrMemberName + ")) {\n");
		sb.indent();
		sb.appendFront("newList.Add(element);\n");
		sb.appendFront("alreadySeenMembers.Add(element.@" + attributeOrMemberName + ", null);\n");
		sb.unindent();
		sb.appendFront("}\n");
		sb.unindent();
		sb.appendFront("}\n");

		sb.appendFront("return newList;\n");
		sb.unindent();
		sb.appendFront("}\n");
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

	protected String nodeTypePrefix;
	protected String edgeTypePrefix;
}
