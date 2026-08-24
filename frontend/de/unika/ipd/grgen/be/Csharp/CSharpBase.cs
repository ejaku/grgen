/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Auxiliary routines used for the CSharp backends.
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Text;

	using de.unika.ipd.grgen.ir;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using ExternalFunctionMethod = de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
	using ExternalProcedureMethod = de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
	using FunctionMethod = de.unika.ipd.grgen.ir.executable.FunctionMethod;
	using ProcedureMethod = de.unika.ipd.grgen.ir.executable.ProcedureMethod;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;
	using MatchTypeIterated = de.unika.ipd.grgen.ir.type.MatchTypeIterated;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using TypeClass = de.unika.ipd.grgen.ir.type.Type.TypeClass;
	using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;
	using ByteType = de.unika.ipd.grgen.ir.type.basic.ByteType;
	using DoubleType = de.unika.ipd.grgen.ir.type.basic.DoubleType;
	using FloatType = de.unika.ipd.grgen.ir.type.basic.FloatType;
	using GraphType = de.unika.ipd.grgen.ir.type.basic.GraphType;
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;
	using LongType = de.unika.ipd.grgen.ir.type.basic.LongType;
	using ObjectType = de.unika.ipd.grgen.ir.type.basic.ObjectType;
	using ShortType = de.unika.ipd.grgen.ir.type.basic.ShortType;
	using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;
	using VoidType = de.unika.ipd.grgen.ir.type.basic.VoidType;
	using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;
	using ContainerType = de.unika.ipd.grgen.ir.type.container.ContainerType;
	using DequeType = de.unika.ipd.grgen.ir.type.container.DequeType;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;
	using SetType = de.unika.ipd.grgen.ir.type.container.SetType;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using de.unika.ipd.grgen.ir.expr;
	using ArrayAndExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAndExpr;
	using ArrayAsDequeExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAsDequeExpr;
	using ArrayAsMapExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAsMapExpr;
	using ArrayAsSetExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAsSetExpr;
	using ArrayAsString = de.unika.ipd.grgen.ir.expr.array.ArrayAsString;
	using ArrayAvgExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAvgExpr;
	using ArrayCopyConstructor = de.unika.ipd.grgen.ir.expr.array.ArrayCopyConstructor;
	using ArrayDevExpr = de.unika.ipd.grgen.ir.expr.array.ArrayDevExpr;
	using ArrayEmptyExpr = de.unika.ipd.grgen.ir.expr.array.ArrayEmptyExpr;
	using ArrayExtract = de.unika.ipd.grgen.ir.expr.array.ArrayExtract;
	using ArrayGroup = de.unika.ipd.grgen.ir.expr.array.ArrayGroup;
	using ArrayGroupBy = de.unika.ipd.grgen.ir.expr.array.ArrayGroupBy;
	using ArrayIndexOfByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfByExpr;
	using ArrayIndexOfExpr = de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfExpr;
	using ArrayIndexOfOrderedByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedByExpr;
	using ArrayIndexOfOrderedExpr = de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedExpr;
	using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
	using ArrayKeepOneForEach = de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEach;
	using ArrayKeepOneForEachBy = de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEachBy;
	using ArrayLastIndexOfByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfByExpr;
	using ArrayLastIndexOfExpr = de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfExpr;
	using ArrayMapExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMapExpr;
	using ArrayMapStartWithAccumulateByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMapStartWithAccumulateByExpr;
	using ArrayMaxExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMaxExpr;
	using ArrayMedExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMedExpr;
	using ArrayMedUnorderedExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMedUnorderedExpr;
	using ArrayMinExpr = de.unika.ipd.grgen.ir.expr.array.ArrayMinExpr;
	using ArrayOrExpr = de.unika.ipd.grgen.ir.expr.array.ArrayOrExpr;
	using ArrayOrderAscending = de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscending;
	using ArrayOrderAscendingBy = de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscendingBy;
	using ArrayOrderDescending = de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescending;
	using ArrayOrderDescendingBy = de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescendingBy;
	using ArrayPeekExpr = de.unika.ipd.grgen.ir.expr.array.ArrayPeekExpr;
	using ArrayProdExpr = de.unika.ipd.grgen.ir.expr.array.ArrayProdExpr;
	using ArrayRemoveIfExpr = de.unika.ipd.grgen.ir.expr.array.ArrayRemoveIfExpr;
	using ArrayReverseExpr = de.unika.ipd.grgen.ir.expr.array.ArrayReverseExpr;
	using ArrayShuffleExpr = de.unika.ipd.grgen.ir.expr.array.ArrayShuffleExpr;
	using ArraySizeExpr = de.unika.ipd.grgen.ir.expr.array.ArraySizeExpr;
	using ArraySubarrayExpr = de.unika.ipd.grgen.ir.expr.array.ArraySubarrayExpr;
	using ArraySumExpr = de.unika.ipd.grgen.ir.expr.array.ArraySumExpr;
	using ArrayVarExpr = de.unika.ipd.grgen.ir.expr.array.ArrayVarExpr;
	using DequeAsArrayExpr = de.unika.ipd.grgen.ir.expr.deque.DequeAsArrayExpr;
	using DequeAsSetExpr = de.unika.ipd.grgen.ir.expr.deque.DequeAsSetExpr;
	using DequeCopyConstructor = de.unika.ipd.grgen.ir.expr.deque.DequeCopyConstructor;
	using DequeEmptyExpr = de.unika.ipd.grgen.ir.expr.deque.DequeEmptyExpr;
	using DequeIndexOfExpr = de.unika.ipd.grgen.ir.expr.deque.DequeIndexOfExpr;
	using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
	using DequeLastIndexOfExpr = de.unika.ipd.grgen.ir.expr.deque.DequeLastIndexOfExpr;
	using DequePeekExpr = de.unika.ipd.grgen.ir.expr.deque.DequePeekExpr;
	using DequeSizeExpr = de.unika.ipd.grgen.ir.expr.deque.DequeSizeExpr;
	using DequeSubdequeExpr = de.unika.ipd.grgen.ir.expr.deque.DequeSubdequeExpr;
	using AdjacentNodeExpr = de.unika.ipd.grgen.ir.expr.graph.AdjacentNodeExpr;
	using BoundedReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableEdgeExpr;
	using BoundedReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
	using BoundedReachableNodeWithRemainingDepthExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeWithRemainingDepthExpr;
	using CanonizeExpr = de.unika.ipd.grgen.ir.expr.graph.CanonizeExpr;
	using CountAdjacentNodeExpr = de.unika.ipd.grgen.ir.expr.graph.CountAdjacentNodeExpr;
	using CountBoundedReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableEdgeExpr;
	using CountBoundedReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.CountBoundedReachableNodeExpr;
	using CountEdgesExpr = de.unika.ipd.grgen.ir.expr.graph.CountEdgesExpr;
	using CountEdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.CountEdgesFromIndexAccessFromToExpr;
	using CountEdgesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.CountEdgesFromIndexAccessSameExpr;
	using CountIncidenceFromIndexExpr = de.unika.ipd.grgen.ir.expr.graph.CountIncidenceFromIndexExpr;
	using CountIncidentEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.CountIncidentEdgeExpr;
	using CountNodesExpr = de.unika.ipd.grgen.ir.expr.graph.CountNodesExpr;
	using CountNodesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.CountNodesFromIndexAccessFromToExpr;
	using CountNodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.CountNodesFromIndexAccessSameExpr;
	using CountReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.CountReachableEdgeExpr;
	using CountReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.CountReachableNodeExpr;
	using DefinedSubgraphExpr = de.unika.ipd.grgen.ir.expr.graph.DefinedSubgraphExpr;
	using EdgeByNameExpr = de.unika.ipd.grgen.ir.expr.graph.EdgeByNameExpr;
	using EdgeByUniqueExpr = de.unika.ipd.grgen.ir.expr.graph.EdgeByUniqueExpr;
	using EdgesExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
	using EdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
	using EdgesFromIndexAccessMultipleFromToExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessMultipleFromToExpr;
	using EdgesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessSameExpr;
	using EmptyExpr = de.unika.ipd.grgen.ir.expr.graph.EmptyExpr;
	using EqualsAnyExpr = de.unika.ipd.grgen.ir.expr.graph.EqualsAnyExpr;
	using GetEquivalentExpr = de.unika.ipd.grgen.ir.expr.graph.GetEquivalentExpr;
	using Graphof = de.unika.ipd.grgen.ir.expr.graph.Graphof;
	using IncidentEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IncidentEdgeExpr;
	using IndexSizeExpr = de.unika.ipd.grgen.ir.expr.graph.IndexSizeExpr;
	using InducedSubgraphExpr = de.unika.ipd.grgen.ir.expr.graph.InducedSubgraphExpr;
	using IsAdjacentNodeExpr = de.unika.ipd.grgen.ir.expr.graph.IsAdjacentNodeExpr;
	using IsBoundedReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableEdgeExpr;
	using IsBoundedReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.IsBoundedReachableNodeExpr;
	using IsInEdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.IsInEdgesFromIndexAccessFromToExpr;
	using IsInEdgesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.IsInEdgesFromIndexAccessSameExpr;
	using IsInNodesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.IsInNodesFromIndexAccessFromToExpr;
	using IsInNodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.IsInNodesFromIndexAccessSameExpr;
	using IsIncidentEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IsIncidentEdgeExpr;
	using IsReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IsReachableEdgeExpr;
	using IsReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.IsReachableNodeExpr;
	using MinMaxFromIndexExpr = de.unika.ipd.grgen.ir.expr.graph.MinMaxFromIndexExpr;
	using Nameof = de.unika.ipd.grgen.ir.expr.graph.Nameof;
	using NodeByNameExpr = de.unika.ipd.grgen.ir.expr.graph.NodeByNameExpr;
	using NodeByUniqueExpr = de.unika.ipd.grgen.ir.expr.graph.NodeByUniqueExpr;
	using NodesExpr = de.unika.ipd.grgen.ir.expr.graph.NodesExpr;
	using NodesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessFromToExpr;
	using NodesFromIndexAccessMultipleFromToExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessMultipleFromToExpr;
	using NodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessSameExpr;
	using OppositeExpr = de.unika.ipd.grgen.ir.expr.graph.OppositeExpr;
	using ReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.ReachableEdgeExpr;
	using ReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
	using SizeExpr = de.unika.ipd.grgen.ir.expr.graph.SizeExpr;
	using SourceExpr = de.unika.ipd.grgen.ir.expr.graph.SourceExpr;
	using TargetExpr = de.unika.ipd.grgen.ir.expr.graph.TargetExpr;
	using ThisExpr = de.unika.ipd.grgen.ir.expr.graph.ThisExpr;
	using Uniqueof = de.unika.ipd.grgen.ir.expr.graph.Uniqueof;
	using Visited = de.unika.ipd.grgen.ir.expr.graph.Visited;
	using ExternalFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionInvocationExpr;
	using ExternalFunctionMethodInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionMethodInvocationExpr;
	using FunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.FunctionInvocationExpr;
	using FunctionMethodInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.FunctionMethodInvocationExpr;
	using MapAsArrayExpr = de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
	using MapCopyConstructor = de.unika.ipd.grgen.ir.expr.map.MapCopyConstructor;
	using MapDomainExpr = de.unika.ipd.grgen.ir.expr.map.MapDomainExpr;
	using MapEmptyExpr = de.unika.ipd.grgen.ir.expr.map.MapEmptyExpr;
	using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
	using MapPeekExpr = de.unika.ipd.grgen.ir.expr.map.MapPeekExpr;
	using MapRangeExpr = de.unika.ipd.grgen.ir.expr.map.MapRangeExpr;
	using MapSizeExpr = de.unika.ipd.grgen.ir.expr.map.MapSizeExpr;
	using AbsExpr = de.unika.ipd.grgen.ir.expr.numeric.AbsExpr;
	using ArcSinCosTanExpr = de.unika.ipd.grgen.ir.expr.numeric.ArcSinCosTanExpr;
	using ByteMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.ByteMaxExpr;
	using ByteMinExpr = de.unika.ipd.grgen.ir.expr.numeric.ByteMinExpr;
	using CeilExpr = de.unika.ipd.grgen.ir.expr.numeric.CeilExpr;
	using DoubleMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.DoubleMaxExpr;
	using DoubleMinExpr = de.unika.ipd.grgen.ir.expr.numeric.DoubleMinExpr;
	using EExpr = de.unika.ipd.grgen.ir.expr.numeric.EExpr;
	using FloatMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.FloatMaxExpr;
	using FloatMinExpr = de.unika.ipd.grgen.ir.expr.numeric.FloatMinExpr;
	using FloorExpr = de.unika.ipd.grgen.ir.expr.numeric.FloorExpr;
	using IntMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.IntMaxExpr;
	using IntMinExpr = de.unika.ipd.grgen.ir.expr.numeric.IntMinExpr;
	using LogExpr = de.unika.ipd.grgen.ir.expr.numeric.LogExpr;
	using LongMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.LongMaxExpr;
	using LongMinExpr = de.unika.ipd.grgen.ir.expr.numeric.LongMinExpr;
	using MaxExpr = de.unika.ipd.grgen.ir.expr.numeric.MaxExpr;
	using MinExpr = de.unika.ipd.grgen.ir.expr.numeric.MinExpr;
	using PiExpr = de.unika.ipd.grgen.ir.expr.numeric.PiExpr;
	using PowExpr = de.unika.ipd.grgen.ir.expr.numeric.PowExpr;
	using RoundExpr = de.unika.ipd.grgen.ir.expr.numeric.RoundExpr;
	using SgnExpr = de.unika.ipd.grgen.ir.expr.numeric.SgnExpr;
	using ShortMaxExpr = de.unika.ipd.grgen.ir.expr.numeric.ShortMaxExpr;
	using ShortMinExpr = de.unika.ipd.grgen.ir.expr.numeric.ShortMinExpr;
	using SinCosTanExpr = de.unika.ipd.grgen.ir.expr.numeric.SinCosTanExpr;
	using SqrExpr = de.unika.ipd.grgen.ir.expr.numeric.SqrExpr;
	using SqrtExpr = de.unika.ipd.grgen.ir.expr.numeric.SqrtExpr;
	using TruncateExpr = de.unika.ipd.grgen.ir.expr.numeric.TruncateExpr;
	using ExistsFileExpr = de.unika.ipd.grgen.ir.expr.procenv.ExistsFileExpr;
	using ImportExpr = de.unika.ipd.grgen.ir.expr.procenv.ImportExpr;
	using NowExpr = de.unika.ipd.grgen.ir.expr.procenv.NowExpr;
	using RandomExpr = de.unika.ipd.grgen.ir.expr.procenv.RandomExpr;
	using SetAsArrayExpr = de.unika.ipd.grgen.ir.expr.set.SetAsArrayExpr;
	using SetCopyConstructor = de.unika.ipd.grgen.ir.expr.set.SetCopyConstructor;
	using SetEmptyExpr = de.unika.ipd.grgen.ir.expr.set.SetEmptyExpr;
	using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
	using SetMaxExpr = de.unika.ipd.grgen.ir.expr.set.SetMaxExpr;
	using SetMinExpr = de.unika.ipd.grgen.ir.expr.set.SetMinExpr;
	using SetPeekExpr = de.unika.ipd.grgen.ir.expr.set.SetPeekExpr;
	using SetSizeExpr = de.unika.ipd.grgen.ir.expr.set.SetSizeExpr;
	using StringAsArray = de.unika.ipd.grgen.ir.expr.@string.StringAsArray;
	using StringEndsWith = de.unika.ipd.grgen.ir.expr.@string.StringEndsWith;
	using StringIndexOf = de.unika.ipd.grgen.ir.expr.@string.StringIndexOf;
	using StringLastIndexOf = de.unika.ipd.grgen.ir.expr.@string.StringLastIndexOf;
	using StringLength = de.unika.ipd.grgen.ir.expr.@string.StringLength;
	using StringReplace = de.unika.ipd.grgen.ir.expr.@string.StringReplace;
	using StringStartsWith = de.unika.ipd.grgen.ir.expr.@string.StringStartsWith;
	using StringSubstring = de.unika.ipd.grgen.ir.expr.@string.StringSubstring;
	using StringToLower = de.unika.ipd.grgen.ir.expr.@string.StringToLower;
	using StringToUpper = de.unika.ipd.grgen.ir.expr.@string.StringToUpper;
	using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using ExternalObjectType = de.unika.ipd.grgen.ir.model.type.ExternalObjectType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;
	using InternalTransientObjectType = de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Base = de.unika.ipd.grgen.util.Base;
	using Direction = de.unika.ipd.grgen.util.Direction;
	using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;
	using Util = de.unika.ipd.grgen.util.Util;

	public abstract class CSharpBase
	{
		public CSharpBase(string nodeTypePrefix, string edgeTypePrefix, string objectTypePrefix, string transientObjectTypePrefix)
		{
			this.nodeTypePrefix = nodeTypePrefix;
			this.edgeTypePrefix = edgeTypePrefix;
			this.objectTypePrefix = objectTypePrefix;
			this.transientObjectTypePrefix = transientObjectTypePrefix;
		}

		/// <summary>
		/// Write a character sequence to a file using the given path. </summary>
		/// <param name="path"> The path for the file. </param>
		/// <param name="filename"> The filename. </param>
		/// <param name="sb"> A string builder. </param>
		public static void WriteFile(DirectoryInfo path, string filename, StringBuilder sb)
		{
			Util.WriteFile(FileAndDirectoryHelper.GetFileInfo(path, filename), sb, Base.error);
		}

		/*public static bool ExistsFile(File path, string filename)
		{
			return (new File(path, filename)).Exists();
		}*/

		public static void CopyFile(FileInfo sourcePath, FileInfo targetPath)
		{
			try
			{
				File.Copy(sourcePath.FullName, targetPath.FullName, true);
			}
			catch(IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Dumps a C-like set representation.
		/// </summary>
		public static void GenSet<T1>(SourceBuilder sb, ICollection<T1> set, string pre, string post, bool brackets) where T1 : Identifiable
		{
			if(brackets)
				sb.Append("{ ");
			bool first = true;
			foreach(Identifiable id in set)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				sb.Append(pre + FormatIdentifiable(id) + post);
			}
			if(brackets)
				sb.Append(" }");
		}

		public static void GenEntitySet<T1>(SourceBuilder sb, ICollection<T1> set, string pre, string post,
				bool brackets, string pathPrefix, Dictionary<Entity, string> alreadyDefinedEntityToName) where T1 : Entity
		{
			if(brackets)
				sb.Append("{ ");
			bool first = true;
			foreach(Entity id in set)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				sb.Append(pre + FormatEntity(id, pathPrefix, alreadyDefinedEntityToName) + post);
			}
			if(brackets)
				sb.Append(" }");
		}

		public virtual void GenVarTypeSet<T1>(SourceBuilder sb, ICollection<T1> set, bool brackets) where T1 : Entity
		{
			if(brackets)
				sb.Append("{ ");
			bool first = true;
			foreach(Entity id in set)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				sb.Append("GRGEN_LIBGR.VarType.GetVarType(typeof(" + FormatAttributeType(id) + "))");
			}
			if(brackets)
				sb.Append(" }");
		}

		public static void GenSubpatternUsageSet(SourceBuilder sb, ICollection<SubpatternUsage> set, string pre,
				string post,
				bool brackets, string pathPrefix,
				Dictionary<Identifiable, string> alreadyDefinedIdentifiableToName)
		{
			if(brackets)
				sb.Append("{ ");
			bool first = true;
			foreach(SubpatternUsage spu in set)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				sb.Append(pre + FormatIdentifiable(spu, pathPrefix, alreadyDefinedIdentifiableToName) + post);
			}
			if(brackets)
				sb.Append(" }");
		}

		public static void GenAlternativesSet<T1>(SourceBuilder sb, ICollection<T1> set,
				string pre, string post, bool brackets) where T1 : de.unika.ipd.grgen.ir.executable.Rule
		{
			if(brackets)
				sb.Append("{ ");
			bool first = true;
			foreach(Rule altCase in set)
			{
				if(first)
					first = false;
				else
					sb.Append(", ");
				PatternGraphLhs altCasePattern = altCase.Left;
				sb.Append(pre + altCasePattern.NameOfGraph + post);
			}
			if(brackets)
				sb.Append(" }");
		}

		public static string FormatIdentifiable(Identifiable id)
		{
			string res = id.Ident.ToString();
			return res.Replace('$', '_');
		}

		public static string GetPackagePrefixDot(Identifiable id)
		{
			if(id is ContainedInPackage)
			{
				ContainedInPackage cip = (ContainedInPackage)id;
				if(!string.ReferenceEquals(cip.PackageContainedIn, null))
					return cip.PackageContainedIn + ".";
			}
			return "";
		}

		public static string GetPackagePrefixDoubleColon(Identifiable id)
		{
			if(id is ContainedInPackage)
			{
				ContainedInPackage cip = (ContainedInPackage)id;
				if(!string.ReferenceEquals(cip.PackageContainedIn, null))
					return cip.PackageContainedIn + "::";
			}
			return "";
		}

		public static string GetPackagePrefix(Identifiable id)
		{
			if(id is ContainedInPackage)
			{
				ContainedInPackage cip = (ContainedInPackage)id;
				if(!string.ReferenceEquals(cip.PackageContainedIn, null))
					return cip.PackageContainedIn;
			}
			return "";
		}

		public static string FormatIdentifiable(Identifiable id, string pathPrefix)
		{
			string ident = id.Ident.ToString();
			return pathPrefix + ident.Replace('$', '_');
		}

		public static string FormatIdentifiable<T1>(T1 id, string pathPrefix,
				Dictionary<T1, string> alreadyDefinedIdentifiableToName) where T1 : Identifiable
		{
			if(alreadyDefinedIdentifiableToName != null && alreadyDefinedIdentifiableToName[id] != null)
				return alreadyDefinedIdentifiableToName[id];
			string ident = id.Ident.ToString();
			return pathPrefix + ident.Replace('$', '_');
		}

		public static string FormatInheritanceTypeValue(Type type)
		{
			if(type is NodeType)
				return "Node";
			else if(type is EdgeType)
				return "Edge";
			else if(type is InternalObjectType)
				return "Object";
			else if(type is InternalTransientObjectType)
				return "TransientObject";
			else
				throw new ArgumentException("Unknown type " + type + " (" + type.GetType() + ")");
		}

		public static string FormatGraphElement(Entity ent)
		{
			if(ent is Node)
				return "Node";
			else if(ent is Edge)
				return "Edge";
			else
				throw new ArgumentException("Illegal entity type " + ent + " (" + ent.GetType() + ")");
		}

		public virtual string GetInheritanceTypePrefix(Type type)
		{
			if(type is NodeType)
				return nodeTypePrefix;
			else if(type is EdgeType)
				return edgeTypePrefix;
			else if(type is InternalObjectType)
				return objectTypePrefix;
			else if(type is InternalTransientObjectType)
				return transientObjectTypePrefix;
			else
				throw new ArgumentException("Unknown type " + type + " (" + type.GetType() + ")");
		}

		public virtual string GetInheritanceTypePrefix(Entity ent)
		{
			if(ent is Node)
				return nodeTypePrefix;
			else if(ent is Edge)
				return edgeTypePrefix;
			else if(ent.Type is InternalObjectType)
				return objectTypePrefix;
			else if(ent.Type is InternalTransientObjectType)
				return transientObjectTypePrefix;
			else
				throw new ArgumentException("Illegal entity type " + ent + " (" + ent.GetType() + ")");
		}

		internal static string MatchType(PatternGraphLhs patternGraph, Rule subpattern, bool isSubpattern, string pathPrefix)
		{
			string matchClassContainer;
			if(isSubpattern)
				matchClassContainer = "GRGEN_ACTIONS." + GetPackagePrefixDot(subpattern) + "Pattern_" + patternGraph.NameOfGraph;
			else
				matchClassContainer = "GRGEN_ACTIONS." + GetPackagePrefixDot(subpattern) + "Rule_" + patternGraph.NameOfGraph;
			string nameOfMatchClass = "Match_" + pathPrefix + patternGraph.NameOfGraph;
			return matchClassContainer + "." + nameOfMatchClass;
		}

		public static string FormatTypeClassName(Type type)
		{
			return FormatInheritanceTypeValue(type) + "Type_" + FormatIdentifiable(type);
		}

		public static string FormatTypeClassRef(Type type)
		{
			return "GRGEN_MODEL." + GetPackagePrefixDot(type) + FormatTypeClassName(type);
		}

		public static string FormatTypeClassRefInstance(Type type)
		{
			return "GRGEN_MODEL." + GetPackagePrefixDot(type) + FormatTypeClassName(type) + ".typeVar";
		}

		public virtual string FormatInheritanceClassRaw(Type type)
		{
			return GetInheritanceTypePrefix(type) + FormatIdentifiable(type);
		}

		public virtual string FormatInheritanceClassName(Type type)
		{
			return "@" + FormatInheritanceClassRaw(type);
		}

		public virtual string FormatInheritanceClassRef(Type type)
		{
			return "GRGEN_MODEL." + GetPackagePrefixDot(type) + FormatInheritanceClassName(type);
		}

		public virtual string FormatElementInterfaceRef(Type type)
		{
			if(!(type is InheritanceType))
			{
				Debug.Assert((false));
				return GetInheritanceTypePrefix(type) + FormatIdentifiable(type);
			}

			if(type is ExternalObjectType)
				return "GRGEN_MODEL." + type.Ident.ToString();

			switch(FormatIdentifiable(type))
			{
			case "Node":
			case "AEdge":
			case "Edge":
			case "UEdge":
			case "Object":
			case "TransientObject":
				InheritanceType inheritanceType = (InheritanceType)type;
				return GetRootElementInterfaceRef(inheritanceType);
			}

			return "GRGEN_MODEL." + GetPackagePrefixDot(type) + "I" + FormatInheritanceClassRaw(type);
		}

		public static string GetRootElementInterfaceRef(InheritanceType inheritanceType)
		{
			if(inheritanceType is InternalObjectType)
				return "GRGEN_LIBGR.IObject";
			else if(inheritanceType is InternalTransientObjectType)
				return "GRGEN_LIBGR.ITransientObject";
			else if(inheritanceType is NodeType)
				return "GRGEN_LIBGR.INode";
			else
			{ // instanceof EdgeType
				EdgeType edgeType = (EdgeType)inheritanceType;
				if(edgeType.Directedness == EdgeType.DirectednessKind.Directed)
					return "GRGEN_LIBGR.IDEdge";
				else if(edgeType.Directedness == EdgeType.DirectednessKind.Undirected)
					return "GRGEN_LIBGR.IUEdge";
				else
					return "GRGEN_LIBGR.IEdge";
			}
		}

		public static string GetDirectedness(Type type)
		{
			SetType setType = (SetType)type;
			EdgeType edgeType = (EdgeType)setType.ValueType;
			if(edgeType.Directedness == EdgeType.DirectednessKind.Directed)
				return "GRGEN_LIBGR.Directedness.Directed";
			else if(edgeType.Directedness == EdgeType.DirectednessKind.Undirected)
				return "GRGEN_LIBGR.Directedness.Undirected";
			else
				return "GRGEN_LIBGR.Directedness.Arbitrary";
		}

		public static string GetDirectednessSuffix(Type type)
		{
			SetType setType = (SetType)type;
			EdgeType edgeType = (EdgeType)setType.ValueType;
			if(edgeType.Directedness == EdgeType.DirectednessKind.Directed)
				return "Directed";
			else if(edgeType.Directedness == EdgeType.DirectednessKind.Undirected)
				return "Undirected";
			else
				return "";
		}

		public static string FormatVarDeclWithCast(string type, string varName)
		{
			return type + " " + varName + " = (" + type + ") ";
		}

		public virtual string FormatNodeAssign(Node node, ICollection<Node> extractNodeAttributeObject)
		{
			if(extractNodeAttributeObject.Contains(node))
				return FormatVarDeclWithCast(FormatInheritanceClassRef(node.Type), FormatEntity(node));
			else
				return "LGSPNode " + FormatEntity(node) + " = ";
		}

		public virtual string FormatEdgeAssign(Edge edge, ICollection<Edge> extractEdgeAttributeObject)
		{
			if(extractEdgeAttributeObject.Contains(edge))
				return FormatVarDeclWithCast(FormatInheritanceClassRef(edge.Type), FormatEntity(edge));
			else
				return "LGSPEdge " + FormatEntity(edge) + " = ";
		}

		public virtual string FormatSequenceType(Type t)
		{
			if(t is ByteType)
				return "byte";
			if(t is ShortType)
				return "short";
			if(t is IntType)
				return "int";
			if(t is LongType)
				return "long";
			else if(t is BooleanType)
				return "boolean";
			else if(t is FloatType)
				return "float";
			else if(t is DoubleType)
				return "double";
			else if(t is StringType)
				return "string";
			else if(t is EnumType)
				return GetPackagePrefixDoubleColon(t) + FormatIdentifiable(t);
			else if(t is ObjectType || t is VoidType)
				return "object";
			else if(t is MapType)
			{
				MapType mapType = (MapType)t;
				return "map<" + FormatSequenceType(mapType.KeyType) + ", " + FormatSequenceType(mapType.ValueType) + ">";
			}
			else if(t is SetType)
			{
				SetType setType = (SetType)t;
				return "set<" + FormatType(setType.ValueType) + ">";
			}
			else if(t is ArrayType)
			{
				ArrayType arrayType = (ArrayType)t;
				return "array<" + FormatType(arrayType.ValueType) + ">";
			}
			else if(t is DequeType)
			{
				DequeType dequeType = (DequeType)t;
				return "deque<" + FormatType(dequeType.ValueType) + ">";
			}
			else if(t is GraphType)
				return "graph";
			else if(t is ExternalObjectType)
			{
				ExternalObjectType extType = (ExternalObjectType)t;
				return extType.Ident.ToString();
			}
			else if(t is InheritanceType)
				return GetPackagePrefixDoubleColon(t) + FormatIdentifiable(t);
			else if(t is MatchTypeIterated)
			{
				MatchTypeIterated matchType = (MatchTypeIterated)t;
				string actionName = matchType.Action.Ident.ToString();
				string iteratedName = matchType.Iterated.Ident.ToString();
				return "match<" + actionName + "." + iteratedName + ">";
			}
			else if(t is MatchType)
			{
				MatchType matchType = (MatchType)t;
				string actionName = matchType.Action.Ident.ToString();
				return "match<" + actionName + ">";
			}
			else if(t is DefinedMatchType)
			{
				DefinedMatchType matchType = (DefinedMatchType)t;
				string matchTypeName = matchType.Ident.ToString();
				return "match<class" + matchTypeName + ">";
			}
			else
				throw new ArgumentException("Illegal type: " + t);
		}

		public virtual string FormatAttributeType(Type t)
		{
			if(t is ByteType)
				return "sbyte";
			if(t is ShortType)
				return "short";
			if(t is IntType)
				return "int";
			if(t is LongType)
				return "long";
			else if(t is BooleanType)
				return "bool";
			else if(t is FloatType)
				return "float";
			else if(t is DoubleType)
				return "double";
			else if(t is StringType)
				return "string";
			else if(t is EnumType)
				return "GRGEN_MODEL." + GetPackagePrefixDot(t) + "ENUM_" + FormatIdentifiable(t);
			else if(t is ObjectType || t is VoidType)
				return "object"; //TODO maybe we need another output type
			else if(t is MapType)
			{
				MapType mapType = (MapType)t;
				return "Dictionary<" + FormatType(mapType.KeyType) + ", " + FormatType(mapType.ValueType) + ">";
			}
			else if(t is SetType)
			{
				SetType setType = (SetType)t;
				return "Dictionary<" + FormatType(setType.ValueType) + ", GRGEN_LIBGR.SetValueType>";
			}
			else if(t is ArrayType)
			{
				ArrayType arrayType = (ArrayType)t;
				return "List<" + FormatType(arrayType.ValueType) + ">";
			}
			else if(t is DequeType)
			{
				DequeType dequeType = (DequeType)t;
				return "GRGEN_LIBGR.Deque<" + FormatType(dequeType.ValueType) + ">";
			}
			else if(t is GraphType)
				return "GRGEN_LIBGR.IGraph";
			else if(t is ExternalObjectType)
			{
				ExternalObjectType extType = (ExternalObjectType)t;
				return "GRGEN_MODEL." + extType.Ident;
			}
			else if(t is InheritanceType)
				return FormatElementInterfaceRef(t);
			else if(t is MatchTypeIterated)
			{
				MatchTypeIterated matchType = (MatchTypeIterated)t;
				string packagePrefix = GetPackagePrefixDot(matchType);
				Rule action = matchType.Action;
				string actionName = action.Ident.ToString();
				Rule iterated = matchType.Iterated;
				string iteratedName = iterated.Ident.ToString();
				return "GRGEN_ACTIONS." + packagePrefix + "Rule_" + actionName + ".IMatch_" + actionName + "_" + iteratedName;
			}
			else if(t is MatchType)
			{
				MatchType matchType = (MatchType)t;
				string packagePrefix = GetPackagePrefixDot(matchType);
				Rule action = matchType.Action;
				string actionName = action.Ident.ToString();
				return "GRGEN_ACTIONS." + packagePrefix + "Rule_" + actionName + ".IMatch_" + actionName;
			}
			else if(t is DefinedMatchType)
			{
				DefinedMatchType definedMatchType = (DefinedMatchType)t;
				string packagePrefix = GetPackagePrefixDot(definedMatchType);
				string matchClassName = definedMatchType.Ident.ToString();
				return "GRGEN_ACTIONS." + packagePrefix + "IMatch_" + matchClassName;
			}
			else
				throw new ArgumentException("Illegal type: " + t);
		}

		// formats match class name instead of match interface name like formatAttributeType
		public virtual string FormatDefinedMatchType(DefinedMatchType definedMatchType)
		{
			string packagePrefix = GetPackagePrefixDot(definedMatchType);
			string matchClassName = definedMatchType.Ident.ToString();
			return "GRGEN_ACTIONS." + packagePrefix + "Match_" + matchClassName;
		}

		public virtual string FormatBaseInternalObjectType(BaseInternalObjectType objectType)
		{
			if(objectType is InternalObjectType)
			{
				string packagePrefix = GetPackagePrefixDot(objectType);
				string objectTypeName = objectType.Ident.ToString();
				return "GRGEN_MODEL." + packagePrefix + objectTypePrefix + objectTypeName;
			}
			else
			{
				string packagePrefix = GetPackagePrefixDot(objectType);
				string objectTypeName = objectType.Ident.ToString();
				return "GRGEN_MODEL." + packagePrefix + transientObjectTypePrefix + objectTypeName;
			}
		}

		public virtual string FormatAttributeType(Entity e)
		{
			return FormatAttributeType(e.Type);
		}

		public static string FormatAttributeTypeName(Entity e)
		{
			return "AttributeType_" + FormatIdentifiable(e);
		}

		public static string FormatFunctionMethodInfoName(FunctionMethod fm, InheritanceType type)
		{
			return "FunctionMethodInfo_" + FormatIdentifiable(fm) + "_" + FormatIdentifiable(type);
		}

		public static string FormatProcedureMethodInfoName(ProcedureMethod pm, InheritanceType type)
		{
			return "ProcedureMethodInfo_" + FormatIdentifiable(pm) + "_" + FormatIdentifiable(type);
		}

		public static string FormatExternalFunctionMethodInfoName(ExternalFunctionMethod efm, ExternalObjectType type)
		{
			return "FunctionMethodInfo_" + FormatIdentifiable(efm) + "_" + FormatIdentifiable(type);
		}

		public static string FormatExternalProcedureMethodInfoName(ExternalProcedureMethod epm, ExternalObjectType type)
		{
			return "ProcedureMethodInfo_" + FormatIdentifiable(epm) + "_" + FormatIdentifiable(type);
		}

		public virtual string FormatType(Type type)
		{
			if(type is InheritanceType)
				return FormatElementInterfaceRef(type);
			else
				return FormatAttributeType(type);
		}

		public static string FormatEntity(Entity entity)
		{
			return FormatEntity(entity, "");
		}

		public static string FormatEntity(Entity entity, string pathPrefix)
		{
			if(entity.Ident.ToString().Equals("this"))
			{
				if(entity.Type is ArrayType)
					return "this_matches";
				else
					return "this";
			}
			else if(entity is Node)
				return pathPrefix + "node_" + FormatIdentifiable(entity);
			else if(entity is Edge)
				return pathPrefix + "edge_" + FormatIdentifiable(entity);
			else if(entity is Variable)
			{
				if(((Variable)entity).isLambdaExpressionVariable)
					return pathPrefix + "var_" + FormatIdentifiable(entity) + "_" + entity.Id;
				else
					return pathPrefix + "var_" + FormatIdentifiable(entity);
			}
			else if(entity.Type is BaseInternalObjectType)
				return pathPrefix + FormatIdentifiable(entity);
			else
				throw new ArgumentException("Unknown entity " + entity + " (" + entity.GetType() + ")");
		}

		public static string FormatEntity(Entity entity, string pathPrefix,
				Dictionary<Entity, string> alreadyDefinedEntityToName)
		{
			if(alreadyDefinedEntityToName != null && alreadyDefinedEntityToName.ContainsKey(entity))
				return alreadyDefinedEntityToName[entity];
			return FormatEntity(entity, pathPrefix);
		}

		public static string FormatInt(int i)
		{
			return (i == int.MaxValue) ? "int.MaxValue" : i.ToString();
		}

		public static string FormatLong(long l)
		{
			return (l == long.MaxValue) ? "long.MaxValue" : l.ToString();
		}

		public static GraphEntity GetAtMostOneNeededGraphElement(NeededEntities needs, IList<Entity> parameters)
		{
			HashSet<GraphEntity> neededEntities = new HashSet<GraphEntity>();
			foreach(Node node in needs.nodes)
			{
				if(parameters.IndexOf(node) != -1)
					continue;
				neededEntities.Add(node);
			}
			foreach(Edge edge in needs.edges)
			{
				if(parameters.IndexOf(edge) != -1)
					continue;
				neededEntities.Add(edge);
			}
			if(neededEntities.Count == 1)
				return EnumeratorHelper.GetFirstElement(neededEntities);
			else if(neededEntities.Count > 1)
				throw new System.NotSupportedException("INTERNAL ERROR, more than one needed entity for index access!");
			return null;
		}

		public virtual void GenBinOpDefault(SourceBuilder sb, Operator op, ExpressionGenerationState modifyGenerationState)
		{
			if(op.OpCode == OperatorCode.BIT_SHR)
			{
				sb.Append("((int)(((uint)");
				GenExpression(sb, op.GetOperand(0), modifyGenerationState);
				sb.Append(") " + GetOperatorSymbol(op.OpCode) + " ");
				GenExpression(sb, op.GetOperand(1), modifyGenerationState);
				sb.Append("))");
			}
			else
			{
				sb.Append("(");
				GenExpression(sb, op.GetOperand(0), modifyGenerationState);
				sb.Append(" " + GetOperatorSymbol(op.OpCode) + " ");
				GenExpression(sb, op.GetOperand(1), modifyGenerationState);
				sb.Append(")");
			}
		}

	// JAVA TO C# CONVERTER TASK: There is no equivalent to 'strictfp' in C#:
	// ORIGINAL LINE: public strictfp void genExpression(de.unika.ipd.grgen.util.SourceBuilder sb, Expression expr, ExpressionGenerationState modifyGenerationState)
		public virtual void GenExpression(SourceBuilder sb, Expression expr,
				ExpressionGenerationState modifyGenerationState)
		{
			if(expr is Operator)
			{
				Operator op = (Operator)expr;
				GenOperator(sb, op, modifyGenerationState);
			}
			else if(expr is Qualification)
			{
				Qualification qual = (Qualification)expr;
				if(qual.Owner != null)
					GenQualAccess(sb, qual, modifyGenerationState);
				else
				{
					sb.Append("(");
					GenExpression(sb, qual.OwnerExpr, modifyGenerationState);
					sb.Append(").@" + FormatIdentifiable(qual.Member));
				}
			}
			else if(expr is MemberExpression)
			{
				MemberExpression memberExp = (MemberExpression)expr;
				GenMemberAccess(sb, memberExp.Member);
			}
			else if(expr is EnumExpression)
			{
				EnumExpression enumExp = (EnumExpression)expr;
				sb.Append("GRGEN_MODEL." + GetPackagePrefixDot(enumExp.Type) + "ENUM_"
						+ enumExp.Type.Ident.ToString() + ".@" + enumExp.EnumItem.ToString());
			}
			else if(expr is Constant)
			{ // gen C-code for constant expressions
				Constant constant = (Constant)expr;
				sb.Append(GetValueAsCSSharpString(constant));
			}
			else if(expr is Nameof)
			{
				Nameof no = (Nameof)expr;
				if(no.NamedEntity == null)
					sb.Append("GRGEN_LIBGR.GraphHelper.Nameof(null, graph)"); // name of graph
				else
				{
					sb.Append("GRGEN_LIBGR.GraphHelper.Nameof(");
					GenExpression(sb, no.NamedEntity, modifyGenerationState); // name of entity
					sb.Append(", graph)");
				}
			}
			else if(expr is Uniqueof)
			{
				Uniqueof uo = (Uniqueof)expr;
				if(uo.Entity == null)
					sb.Append("((GRGEN_LGSP.LGSPGraph)graph).GraphId");
				else
				{
					sb.Append("(");
					if(uo.Entity.Type is NodeType)
						sb.Append("(GRGEN_LGSP.LGSPNodeWithUniqueId)");
					else if(uo.Entity.Type is EdgeType)
						sb.Append("(GRGEN_LGSP.LGSPEdgeWithUniqueId)");
					else if(uo.Entity.Type is InternalObjectType)
						sb.Append("(GRGEN_LGSP.LGSPObject)");
					else
						sb.Append("(GRGEN_LGSP.LGSPGraph)");
					GenExpression(sb, uo.Entity, modifyGenerationState); // unique id of entity
					if(uo.Entity == null || uo.Entity.Type is GraphType)
						sb.Append(").GraphId");
					else
						sb.Append(").uniqueId");
				}
			}
			else if(expr is Graphof)
			{
				Graphof go = (Graphof)expr;
				sb.Append("(");
				sb.Append("(GRGEN_LIBGR.IContained)");
				GenExpression(sb, go.Entity, modifyGenerationState);
				sb.Append(").GetContainingGraph()");
			}
			else if(expr is ExistsFileExpr)
			{
				ExistsFileExpr efe = (ExistsFileExpr)expr;
				sb.Append("global::System.IO.File.Exists((string)");
				GenExpression(sb, efe.PathExpr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is ImportExpr)
			{
				ImportExpr ie = (ImportExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.Import(");
				GenExpression(sb, ie.PathExpr, modifyGenerationState);
				sb.Append(", actionEnv.Backend, graph.Model)");
			}
			else if(expr is CopyExpr)
			{
				CopyExpr ce = (CopyExpr)expr;
				Type t = ce.SourceExpr.Type;
				if(ce.Deep)
				{
					if(t is GraphType)
					{
						sb.Append("GRGEN_LIBGR.GraphHelper.Copy(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(t is InternalObjectType)
					{
						sb.Append("((" + FormatType(t) + ")(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(").Copy(graph, new Dictionary<object, object>()))");
					}
					else if(t is InternalTransientObjectType)
					{
						sb.Append("((" + FormatType(t) + ")(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(").Copy(graph, new Dictionary<object, object>()))");
					}
					else if(t is ContainerType)
					{
						sb.Append("GRGEN_LIBGR.ContainerHelper.Copy(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(", graph, new Dictionary<object, object>())");
					}
					else
					{ // object/external object type
						if(modifyGenerationState.Model.IsCopyClassDefined())
						{
							sb.Append("((" + FormatType(t) + ")(");
							sb.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.Copy(");
							GenExpression(sb, ce.SourceExpr, modifyGenerationState);
							sb.Append(", graph, new Dictionary<object, object>())))");
						}
						else
							sb.Append("GRGEN_MODEL.ExternalObjectType_object.ThrowCopyClassMissingException()");
					}
				}
				else
				{
					if(t is MatchType || t is MatchTypeIterated || t is DefinedMatchType)
					{
						sb.Append("((" + FormatType(t) + ")(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(").Clone())");
					}
					else if(t is InternalObjectType)
					{
						sb.Append("((" + FormatType(t) + ")(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(").Clone(graph))");
					}
					else if(t is InternalTransientObjectType)
					{
						sb.Append("((" + FormatType(t) + ")(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(").Clone())");
					}
					else if(t is ContainerType)
					{
						sb.Append("new " + FormatType(t) + "(");
						GenExpression(sb, ce.SourceExpr, modifyGenerationState);
						sb.Append(")");
					}
					else
					{ // object/external object type
						if(modifyGenerationState.Model.IsCopyClassDefined())
						{
							sb.Append("((" + FormatType(t) + ")(");
							sb.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.Copy(");
							GenExpression(sb, ce.SourceExpr, modifyGenerationState);
							sb.Append(", graph, null)));\n");
						}
						else
							sb.Append("GRGEN_MODEL.ExternalObjectType_object.ThrowCopyClassMissingException()");
					}
				}
			}
			else if(expr is Count)
			{
				Count count = (Count)expr;
				sb.Append("curMatch." + FormatIdentifiable(count.Iterated) + ".Count");
			}
			else if(expr is Typeof)
			{
				Typeof to = (Typeof)expr;
				if(to.Entity.Type is NodeType)
					sb.Append("((GRGEN_LGSP.LGSPNode)" + FormatEntity(to.Entity) + ").lgspType");
				else
					sb.Append("((GRGEN_LGSP.LGSPEdge)" + FormatEntity(to.Entity) + ").lgspType");
			}
			else if(expr is Cast)
			{
				Cast cast = (Cast)expr;
				string typeName = GetTypeNameForCast(cast);

				if(string.ReferenceEquals(typeName, "string"))
				{
					if(cast.Expression.Type is MapType || cast.Expression.Type is SetType)
					{
						sb.Append("GRGEN_LIBGR.EmitHelper.ToString(");
						GenExpression(sb, cast.Expression, modifyGenerationState);
						sb.Append(", graph, null, null, null)");
					}
					else if(cast.Expression.Type is ArrayType)
					{
						sb.Append("GRGEN_LIBGR.EmitHelper.ToString(");
						GenExpression(sb, cast.Expression, modifyGenerationState);
						sb.Append(", graph, null, null, null)");
					}
					else if(cast.Expression.Type is DequeType)
					{
						sb.Append("GRGEN_LIBGR.EmitHelper.ToString(");
						GenExpression(sb, cast.Expression, modifyGenerationState);
						sb.Append(", graph, null, null, null)");
					}
					else
					{
						sb.Append("GRGEN_LIBGR.EmitHelper.ToStringNonNull(");
						GenExpression(sb, cast.Expression, modifyGenerationState);
						sb.Append(", graph, null, null, null)");
					}
				}
				else if(string.ReferenceEquals(typeName, "object"))
				{
					// no cast needed
					GenExpression(sb, cast.Expression, modifyGenerationState);
				}
				else
				{
					sb.Append("((" + typeName + ") ");
					GenExpression(sb, cast.Expression, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is VariableExpression)
			{
				Variable var = ((VariableExpression)expr).Variable;
				if(!Expression.IsGlobalVariable(var))
				{
					if(var.Ident.ToString().Equals("this") && var.Type is ArrayType)
						sb.Append("this_matches");
					else
						sb.Append(FormatEntity(var));
				}
				else
					sb.Append(FormatGlobalVariableRead(var));
			}
			else if(expr is GraphEntityExpression)
			{
				GraphEntity ent = ((GraphEntityExpression)expr).GraphEntity;
				if(!Expression.IsGlobalVariable(ent))
					sb.Append(FormatEntity(ent));
				else
					sb.Append(FormatGlobalVariableRead(ent));
			}
			else if(expr is Visited)
			{
				Visited vis = (Visited)expr;
				sb.Append("graph.IsVisited(");
				GenExpression(sb, vis.Entity, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, vis.VisitorID, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is RandomExpr)
			{
				RandomExpr re = (RandomExpr)expr;
				if(re.NumExpr != null)
				{
					sb.Append("GRGEN_LIBGR.Sequence.randomGenerator.Next(");
					GenExpression(sb, re.NumExpr, modifyGenerationState);
				}
				else
					sb.Append("GRGEN_LIBGR.Sequence.randomGenerator.NextDouble(");
				sb.Append(")");
			}
			else if(expr is ThisExpr)
				sb.Append("graph");
			else if(expr is StringLength)
			{
				StringLength strlen = (StringLength)expr;
				sb.Append("(");
				GenExpression(sb, strlen.StringExpr, modifyGenerationState);
				sb.Append(").Length");
			}
			else if(expr is StringToUpper)
			{
				StringToUpper strtoup = (StringToUpper)expr;
				sb.Append("(");
				GenExpression(sb, strtoup.StringExpr, modifyGenerationState);
				sb.Append(").ToUpperInvariant()");
			}
			else if(expr is StringToLower)
			{
				StringToLower strtolo = (StringToLower)expr;
				sb.Append("(");
				GenExpression(sb, strtolo.StringExpr, modifyGenerationState);
				sb.Append(").ToLowerInvariant()");
			}
			else if(expr is StringSubstring)
			{
				StringSubstring strsubstr = (StringSubstring)expr;
				sb.Append("(");
				GenExpression(sb, strsubstr.StringExpr, modifyGenerationState);
				sb.Append(").Substring(");
				GenExpression(sb, strsubstr.StartExpr, modifyGenerationState);
				if(strsubstr.LengthExpr != null)
				{
					sb.Append(", ");
					GenExpression(sb, strsubstr.LengthExpr, modifyGenerationState);
				}
				sb.Append(")");
			}
			else if(expr is StringIndexOf)
			{
				StringIndexOf strio = (StringIndexOf)expr;
				sb.Append("(");
				GenExpression(sb, strio.StringExpr, modifyGenerationState);
				sb.Append(").IndexOf(");
				GenExpression(sb, strio.StringToSearchForExpr, modifyGenerationState);
				if(strio.StartIndexExpr != null)
				{
					sb.Append(", ");
					GenExpression(sb, strio.StartIndexExpr, modifyGenerationState);
				}
				sb.Append(", StringComparison.InvariantCulture");
				sb.Append(")");
			}
			else if(expr is StringLastIndexOf)
			{
				StringLastIndexOf strlio = (StringLastIndexOf)expr;
				sb.Append("(");
				GenExpression(sb, strlio.StringExpr, modifyGenerationState);
				sb.Append(").LastIndexOf(");
				GenExpression(sb, strlio.StringToSearchForExpr, modifyGenerationState);
				if(strlio.StartIndexExpr != null)
				{
					sb.Append(", ");
					GenExpression(sb, strlio.StartIndexExpr, modifyGenerationState);
				}
				sb.Append(", StringComparison.InvariantCulture");
				sb.Append(")");
			}
			else if(expr is StringStartsWith)
			{
				StringStartsWith strsw = (StringStartsWith)expr;
				sb.Append("(");
				GenExpression(sb, strsw.StringExpr, modifyGenerationState);
				sb.Append(").StartsWith(");
				GenExpression(sb, strsw.StringToSearchForExpr, modifyGenerationState);
				sb.Append(", StringComparison.InvariantCulture");
				sb.Append(")");
			}
			else if(expr is StringEndsWith)
			{
				StringEndsWith strew = (StringEndsWith)expr;
				sb.Append("(");
				GenExpression(sb, strew.StringExpr, modifyGenerationState);
				sb.Append(").EndsWith(");
				GenExpression(sb, strew.StringToSearchForExpr, modifyGenerationState);
				sb.Append(", StringComparison.InvariantCulture");
				sb.Append(")");
			}
			else if(expr is StringReplace)
			{
				StringReplace strrepl = (StringReplace)expr;
				sb.Append("((");
				GenExpression(sb, strrepl.StringExpr, modifyGenerationState);
				sb.Append(").Substring(0, ");
				GenExpression(sb, strrepl.StartExpr, modifyGenerationState);
				sb.Append(") + ");
				GenExpression(sb, strrepl.ReplaceStrExpr, modifyGenerationState);
				sb.Append(" + (");
				GenExpression(sb, strrepl.StringExpr, modifyGenerationState);
				sb.Append(").Substring(");
				GenExpression(sb, strrepl.StartExpr, modifyGenerationState);
				sb.Append(" + ");
				GenExpression(sb, strrepl.LengthExpr, modifyGenerationState);
				sb.Append("))");
			}
			else if(expr is StringAsArray)
			{
				StringAsArray saa = (StringAsArray)expr;
				sb.Append("GRGEN_LIBGR.ContainerHelper.StringAsArray(");
				GenExpression(sb, saa.StringExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, saa.StringToSplitAtExpr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IndexedAccessExpr)
			{
				IndexedAccessExpr ia = (IndexedAccessExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ia]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("(");
					GenExpression(sb, ia.TargetExpr, modifyGenerationState);
					sb.Append("[");
					if(ia.KeyExpr is GraphEntityExpression)
						sb.Append("(" + FormatElementInterfaceRef(ia.KeyExpr.Type) + ")(");
					GenExpression(sb, ia.KeyExpr, modifyGenerationState);
					if(ia.KeyExpr is GraphEntityExpression)
						sb.Append(")");
					sb.Append("])");
				}
			}
			else if(expr is CountIncidenceFromIndexExpr)
			{
				CountIncidenceFromIndexExpr cifi = (CountIncidenceFromIndexExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[cifi]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("((GRGEN_LIBGR.IIncidenceCountIndex)graph.Indices.GetIndex(\"" + cifi.Index.Ident
							+ "\")).GetIncidenceCount(");
					//sb.append("(" + formatElementInterfaceRef(ia.getKeyExpr().getType()) + ")(");
					GenExpression(sb, cifi.KeyExpr, modifyGenerationState);
					//sb.append(")");
					sb.Append(")");
				}
			}
			else if(expr is MapSizeExpr)
			{
				MapSizeExpr ms = (MapSizeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ms]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("(");
					GenExpression(sb, ms.TargetExpr, modifyGenerationState);
					sb.Append(").Count");
				}
			}
			else if(expr is MapEmptyExpr)
			{
				MapEmptyExpr me = (MapEmptyExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[me]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("((");
					GenExpression(sb, me.TargetExpr, modifyGenerationState);
					sb.Append(").Count==0)");
				}
			}
			else if(expr is MapDomainExpr)
			{
				MapDomainExpr md = (MapDomainExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[md]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Domain(");
					GenExpression(sb, md.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is MapRangeExpr)
			{
				MapRangeExpr mr = (MapRangeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[mr]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Range(");
					GenExpression(sb, mr.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is MapAsArrayExpr)
			{
				MapAsArrayExpr maa = (MapAsArrayExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[maa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.MapAsArray(");
					GenExpression(sb, maa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is MapPeekExpr)
			{
				MapPeekExpr mp = (MapPeekExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[mp]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
					GenExpression(sb, mp.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, mp.NumberExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is SetSizeExpr)
			{
				SetSizeExpr ss = (SetSizeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ss]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("(");
					GenExpression(sb, ss.TargetExpr, modifyGenerationState);
					sb.Append(").Count");
				}
			}
			else if(expr is SetEmptyExpr)
			{
				SetEmptyExpr se = (SetEmptyExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[se]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("((");
					GenExpression(sb, se.TargetExpr, modifyGenerationState);
					sb.Append(").Count==0)");
				}
			}
			else if(expr is SetPeekExpr)
			{
				SetPeekExpr sp = (SetPeekExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[sp]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
					GenExpression(sb, sp.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, sp.NumberExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is SetMinExpr)
			{
				SetMinExpr sm = (SetMinExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[sm]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Min(");
					GenExpression(sb, sm.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is SetMaxExpr)
			{
				SetMaxExpr sm = (SetMaxExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[sm]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Max(");
					GenExpression(sb, sm.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is SetAsArrayExpr)
			{
				SetAsArrayExpr saa = (SetAsArrayExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[saa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.SetAsArray(");
					GenExpression(sb, saa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArraySizeExpr)
			{
				ArraySizeExpr @as = (ArraySizeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[@as]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("(");
					GenExpression(sb, @as.TargetExpr, modifyGenerationState);
					sb.Append(").Count");
				}
			}
			else if(expr is ArrayEmptyExpr)
			{
				ArrayEmptyExpr ae = (ArrayEmptyExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ae]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("((");
					GenExpression(sb, ae.TargetExpr, modifyGenerationState);
					sb.Append(").Count==0)");
				}
			}
			else if(expr is ArrayPeekExpr)
			{
				ArrayPeekExpr ap = (ArrayPeekExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ap]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
					GenExpression(sb, ap.TargetExpr, modifyGenerationState);
					if(ap.NumberExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, ap.NumberExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is ArrayIndexOfExpr)
			{
				ArrayIndexOfExpr ai = (ArrayIndexOfExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ai]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
					GenExpression(sb, ai.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, ai.ValueExpr, modifyGenerationState);
					if(ai.StartIndexExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, ai.StartIndexExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is ArrayIndexOfByExpr)
			{
				ArrayIndexOfByExpr aib = (ArrayIndexOfByExpr)expr;
				Type arrayValueType = aib.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aib]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						sb.Append("GRGEN_MODEL.ArrayHelper_"
								+ aib.TargetTypeExact.ValueType.Ident.ToString() + "_"
								+ FormatIdentifiable(aib.Member) + ".ArrayIndexOfBy(");
						GenExpression(sb, aib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aib.ValueExpr, modifyGenerationState);
						if(aib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, aib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "indexOfBy_" + FormatIdentifiable(aib.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aib.ValueExpr, modifyGenerationState);
						if(aib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, aib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "indexOfBy_" + FormatIdentifiable(aib.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aib.ValueExpr, modifyGenerationState);
						if(aib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, aib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "indexOfBy_" + FormatIdentifiable(aib.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aib.ValueExpr, modifyGenerationState);
						if(aib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, aib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayIndexOfOrderedExpr)
			{
				ArrayIndexOfOrderedExpr aio = (ArrayIndexOfOrderedExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aio]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.IndexOfOrdered(");
					GenExpression(sb, aio.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, aio.ValueExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayIndexOfOrderedByExpr)
			{
				ArrayIndexOfOrderedByExpr aiob = (ArrayIndexOfOrderedByExpr)expr;
				Type arrayValueType = aiob.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aiob]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						sb.Append("GRGEN_MODEL.ArrayHelper_"
								+ aiob.TargetTypeExact.ValueType.Ident.ToString() + "_"
								+ FormatIdentifiable(aiob.Member) + ".ArrayIndexOfOrderedBy(");
						GenExpression(sb, aiob.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aiob.ValueExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "indexOfOrderedBy_" + FormatIdentifiable(aiob.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aiob.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aiob.ValueExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "indexOfOrderedBy_" + FormatIdentifiable(aiob.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aiob.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aiob.ValueExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "indexOfOrderedBy_" + FormatIdentifiable(aiob.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aiob.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, aiob.ValueExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayLastIndexOfExpr)
			{
				ArrayLastIndexOfExpr ali = (ArrayLastIndexOfExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ali]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
					GenExpression(sb, ali.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, ali.ValueExpr, modifyGenerationState);
					if(ali.StartIndexExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, ali.StartIndexExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is ArrayLastIndexOfByExpr)
			{
				ArrayLastIndexOfByExpr alib = (ArrayLastIndexOfByExpr)expr;
				Type arrayValueType = alib.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[alib]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						sb.Append("GRGEN_MODEL.ArrayHelper_"
								+ alib.TargetTypeExact.ValueType.Ident.ToString() + "_"
								+ FormatIdentifiable(alib.Member) + ".ArrayLastIndexOfBy(");
						GenExpression(sb, alib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, alib.ValueExpr, modifyGenerationState);
						if(alib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, alib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "lastIndexOfBy_" + FormatIdentifiable(alib.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, alib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, alib.ValueExpr, modifyGenerationState);
						if(alib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, alib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "lastIndexOfBy_" + FormatIdentifiable(alib.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, alib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, alib.ValueExpr, modifyGenerationState);
						if(alib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, alib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "lastIndexOfBy_" + FormatIdentifiable(alib.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, alib.TargetExpr, modifyGenerationState);
						sb.Append(", ");
						GenExpression(sb, alib.ValueExpr, modifyGenerationState);
						if(alib.StartIndexExpr != null)
						{
							sb.Append(", ");
							GenExpression(sb, alib.StartIndexExpr, modifyGenerationState);
						}
						sb.Append(")");
					}
				}
			}
			else if(expr is ArraySubarrayExpr)
			{
				ArraySubarrayExpr @as = (ArraySubarrayExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[@as]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Subarray(");
					GenExpression(sb, @as.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, @as.StartExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, @as.LengthExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayOrderAscending)
			{
				ArrayOrderAscending aoa = (ArrayOrderAscending)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aoa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayOrderAscending(");
					GenExpression(sb, aoa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayOrderDescending)
			{
				ArrayOrderDescending aod = (ArrayOrderDescending)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aod]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayOrderDescending(");
					GenExpression(sb, aod.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayGroup)
			{
				ArrayGroup ag = (ArrayGroup)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ag]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayGroup(");
					GenExpression(sb, ag.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayKeepOneForEach)
			{
				ArrayKeepOneForEach ako = (ArrayKeepOneForEach)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ako]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayKeepOneForEach(");
					GenExpression(sb, ako.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayOrderAscendingBy)
			{
				ArrayOrderAscendingBy aoab = (ArrayOrderAscendingBy)expr;
				Type arrayValueType = aoab.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aoab]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						InheritanceType graphElementType = (InheritanceType)arrayValueType;
						string arrayHelperClassName = GetPackagePrefixDot(graphElementType) + "ArrayHelper_"
								+ graphElementType.Ident.ToString() + "_" + FormatIdentifiable(aoab.Member);
						sb.Append("GRGEN_MODEL." + arrayHelperClassName + ".ArrayOrderAscendingBy(");
						GenExpression(sb, aoab.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "orderAscendingBy_" + FormatIdentifiable(aoab.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aoab.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "orderAscendingBy_" + FormatIdentifiable(aoab.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aoab.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "orderAscendingBy_" + FormatIdentifiable(aoab.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aoab.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayOrderDescendingBy)
			{
				ArrayOrderDescendingBy aodb = (ArrayOrderDescendingBy)expr;
				Type arrayValueType = aodb.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aodb]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						InheritanceType graphElementType = (InheritanceType)arrayValueType;
						string arrayHelperClassName = GetPackagePrefixDot(graphElementType) + "ArrayHelper_"
								+ graphElementType.Ident.ToString() + "_" + FormatIdentifiable(aodb.Member);
						sb.Append("GRGEN_MODEL." + arrayHelperClassName + ".ArrayOrderDescendingBy(");
						GenExpression(sb, aodb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "orderDescendingBy_" + FormatIdentifiable(aodb.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aodb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "orderDescendingBy_" + FormatIdentifiable(aodb.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aodb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "orderDescendingBy_" + FormatIdentifiable(aodb.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, aodb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayGroupBy)
			{
				ArrayGroupBy agb = (ArrayGroupBy)expr;
				Type arrayValueType = agb.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[agb]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						InheritanceType graphElementType = (InheritanceType)arrayValueType;
						string arrayHelperClassName = GetPackagePrefixDot(graphElementType) + "ArrayHelper_"
								+ graphElementType.Ident.ToString() + "_" + FormatIdentifiable(agb.Member);
						sb.Append("GRGEN_MODEL." + arrayHelperClassName + ".ArrayGroupBy(");
						GenExpression(sb, agb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "groupBy_" + FormatIdentifiable(agb.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, agb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "groupBy_" + FormatIdentifiable(agb.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, agb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "groupBy_" + FormatIdentifiable(agb.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, agb.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayKeepOneForEachBy)
			{
				ArrayKeepOneForEachBy akob = (ArrayKeepOneForEachBy)expr;
				Type arrayValueType = akob.TargetTypeExact.ValueType;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[akob]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					if(arrayValueType is InheritanceType)
					{
						InheritanceType graphElementType = (InheritanceType)arrayValueType;
						string arrayHelperClassName = GetPackagePrefixDot(graphElementType) + "ArrayHelper_"
								+ graphElementType.Ident.ToString() + "_" + FormatIdentifiable(akob.Member);
						sb.Append("GRGEN_MODEL." + arrayHelperClassName + ".ArrayKeepOneForEachBy(");
						GenExpression(sb, akob.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string iteratedName = FormatIdentifiable(matchType.Iterated);
						string functionName = "keepOneForEachBy_" + FormatIdentifiable(akob.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + iteratedName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, akob.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						string rulePackage = GetPackagePrefixDot(matchType.Action);
						string ruleName = FormatIdentifiable(matchType.Action);
						string functionName = "keepOneForEachBy_" + FormatIdentifiable(akob.Member);
						string arrayFunctionName = "Array_" + ruleName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + rulePackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, akob.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassPackage = GetPackagePrefixDot(definedMatchType);
						string matchClassName = FormatIdentifiable(definedMatchType);
						string functionName = "keepOneForEachBy_" + FormatIdentifiable(akob.Member);
						string arrayFunctionName = "Array_" + matchClassName + "_" + functionName;
						sb.Append("GRGEN_ACTIONS." + matchClassPackage + "ArrayHelper." + arrayFunctionName + "(");
						GenExpression(sb, akob.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayReverseExpr)
			{
				ArrayReverseExpr ar = (ArrayReverseExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ar]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayReverse(");
					GenExpression(sb, ar.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayShuffleExpr)
			{
				ArrayShuffleExpr ar = (ArrayShuffleExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ar]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Shuffle(");
					GenExpression(sb, ar.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayExtract)
			{
				ArrayExtract ae = (ArrayExtract)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ae]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					Type arrayValueType = ae.TargetTypeExact.ValueType;
					if(arrayValueType is InheritanceType)
					{
						InheritanceType graphElementType = (InheritanceType)arrayValueType;
						string arrayHelperClassName = GetPackagePrefixDot(graphElementType) + "ArrayHelper_"
								+ graphElementType.Ident.ToString() + "_" + FormatIdentifiable(ae.Member);
						sb.Append("GRGEN_MODEL." + arrayHelperClassName + ".Extract(");
						GenExpression(sb, ae.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchTypeIterated)
					{
						MatchTypeIterated matchType = (MatchTypeIterated)arrayValueType;
						Rule rule = matchType.Action;
						string ruleName = GetPackagePrefixDot(rule) + "Rule_" + FormatIdentifiable(rule);
						Rule iterated = matchType.Iterated;
						string iteratedName = FormatIdentifiable(iterated);
						sb.Append("GRGEN_ACTIONS." + ruleName + ".Extractor_" + iteratedName + ".Extract_"
								+ FormatIdentifiable(ae.Member) + "(");
						GenExpression(sb, ae.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is MatchType)
					{
						MatchType matchType = (MatchType)arrayValueType;
						Rule rule = matchType.Action;
						string ruleName = GetPackagePrefixDot(rule) + "Rule_" + FormatIdentifiable(rule);
						sb.Append("GRGEN_ACTIONS." + ruleName + ".Extractor.Extract_" + FormatIdentifiable(ae.Member) + "(");
						GenExpression(sb, ae.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
					else if(arrayValueType is DefinedMatchType)
					{
						DefinedMatchType definedMatchType = (DefinedMatchType)arrayValueType;
						string matchClassName = GetPackagePrefixDot(definedMatchType) + "MatchClassInfo_"
								+ FormatIdentifiable(definedMatchType);
						sb.Append("GRGEN_ACTIONS." + matchClassName + ".Extractor.Extract_"
								+ FormatIdentifiable(ae.Member) + "(");
						GenExpression(sb, ae.TargetExpr, modifyGenerationState);
						sb.Append(")");
					}
				}
			}
			else if(expr is ArrayMapExpr)
			{
				ArrayMapExpr am = (ArrayMapExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[am]);
				else
				{
					// call of generated array map method
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
					am.CollectNeededEntities(needs);
					string arrayMapName = "ArrayMap_" + am.Id;
					sb.Append(arrayMapName + "(actionEnv, ");
					GenExpression(sb, am.TargetExpr, modifyGenerationState);
					foreach(Node node in needs.nodes)
					{
						sb.Append(", (");
						sb.Append(FormatType(node.Type));
						sb.Append(")");
						sb.Append(FormatEntity(node));
					}
					foreach(Edge edge in needs.edges)
					{
						sb.Append(", (");
						sb.Append(FormatType(edge.Type));
						sb.Append(")");
						sb.Append(FormatEntity(edge));
					}
					foreach(Variable var in needs.variables)
					{
						sb.Append(", (");
						sb.Append(FormatType(var.Type));
						sb.Append(")");
						sb.Append(FormatEntity(var));
					}
					if(modifyGenerationState.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append(")");

					GenerateArrayMap(am, modifyGenerationState);
				}
			}
			else if(expr is ArrayRemoveIfExpr)
			{
				ArrayRemoveIfExpr ari = (ArrayRemoveIfExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ari]);
				else
				{
					// call of generated array removeIf method
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
					ari.CollectNeededEntities(needs);
					string arrayRemoveIfName = "ArrayRemoveIf_" + ari.Id;
					sb.Append(arrayRemoveIfName + "(actionEnv, ");
					GenExpression(sb, ari.TargetExpr, modifyGenerationState);
					foreach(Node node in needs.nodes)
					{
						sb.Append(", (");
						sb.Append(FormatType(node.Type));
						sb.Append(")");
						sb.Append(FormatEntity(node));
					}
					foreach(Edge edge in needs.edges)
					{
						sb.Append(", (");
						sb.Append(FormatType(edge.Type));
						sb.Append(")");
						sb.Append(FormatEntity(edge));
					}
					foreach(Variable var in needs.variables)
					{
						sb.Append(", (");
						sb.Append(FormatType(var.Type));
						sb.Append(")");
						sb.Append(FormatEntity(var));
					}
					if(modifyGenerationState.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append(")");

					GenerateArrayRemoveIf(ari, modifyGenerationState);
				}
			}
			else if(expr is ArrayMapStartWithAccumulateByExpr)
			{
				ArrayMapStartWithAccumulateByExpr am = (ArrayMapStartWithAccumulateByExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[am]);
				else
				{
					// call of generated array map start with accumulate by method
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
					am.CollectNeededEntities(needs);
					string arrayMapName = "ArrayMapStartWithAccumulateBy_" + am.Id;
					sb.Append(arrayMapName + "(actionEnv, ");
					GenExpression(sb, am.TargetExpr, modifyGenerationState);
					foreach(Node node in needs.nodes)
					{
						sb.Append(", (");
						sb.Append(FormatType(node.Type));
						sb.Append(")");
						sb.Append(FormatEntity(node));
					}
					foreach(Edge edge in needs.edges)
					{
						sb.Append(", (");
						sb.Append(FormatType(edge.Type));
						sb.Append(")");
						sb.Append(FormatEntity(edge));
					}
					foreach(Variable var in needs.variables)
					{
						sb.Append(", (");
						sb.Append(FormatType(var.Type));
						sb.Append(")");
						sb.Append(FormatEntity(var));
					}
					if(modifyGenerationState.IsToBeParallelizedActionExisting())
						sb.Append(", threadId");
					sb.Append(")");

					GenerateArrayMapStartWithAccumulateBy(am, modifyGenerationState);
				}
			}
			else if(expr is ArrayAsSetExpr)
			{
				ArrayAsSetExpr aas = (ArrayAsSetExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aas]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsSet(");
					GenExpression(sb, aas.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayAsDequeExpr)
			{
				ArrayAsDequeExpr aad = (ArrayAsDequeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aad]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsDeque(");
					GenExpression(sb, aad.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayAsMapExpr)
			{
				ArrayAsMapExpr aam = (ArrayAsMapExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aam]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsMap(");
					GenExpression(sb, aam.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayAsString)
			{
				ArrayAsString aas = (ArrayAsString)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aas]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.ArrayAsString(");
					GenExpression(sb, aas.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, aas.ValueExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArraySumExpr)
			{
				ArraySumExpr @as = (ArraySumExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[@as]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Sum(");
					GenExpression(sb, @as.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayProdExpr)
			{
				ArrayProdExpr ap = (ArrayProdExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ap]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Prod(");
					GenExpression(sb, ap.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayMinExpr)
			{
				ArrayMinExpr am = (ArrayMinExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[am]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Min(");
					GenExpression(sb, am.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayMaxExpr)
			{
				ArrayMaxExpr am = (ArrayMaxExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[am]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Max(");
					GenExpression(sb, am.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayAvgExpr)
			{
				ArrayAvgExpr aa = (ArrayAvgExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Avg(");
					GenExpression(sb, aa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayMedExpr)
			{
				ArrayMedExpr am = (ArrayMedExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[am]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Med(");
					GenExpression(sb, am.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayMedUnorderedExpr)
			{
				ArrayMedUnorderedExpr amu = (ArrayMedUnorderedExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[amu]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.MedUnordered(");
					GenExpression(sb, amu.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayVarExpr)
			{
				ArrayVarExpr av = (ArrayVarExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[av]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Var(");
					GenExpression(sb, av.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayDevExpr)
			{
				ArrayDevExpr ad = (ArrayDevExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ad]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Dev(");
					GenExpression(sb, ad.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayAndExpr)
			{
				ArrayAndExpr aa = (ArrayAndExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[aa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.And(");
					GenExpression(sb, aa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is ArrayOrExpr)
			{
				ArrayOrExpr ao = (ArrayOrExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ao]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Or(");
					GenExpression(sb, ao.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is DequeSizeExpr)
			{
				DequeSizeExpr ds = (DequeSizeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[ds]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("(");
					GenExpression(sb, ds.TargetExpr, modifyGenerationState);
					sb.Append(").Count");
				}
			}
			else if(expr is DequeEmptyExpr)
			{
				DequeEmptyExpr de = (DequeEmptyExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[de]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("((");
					GenExpression(sb, de.TargetExpr, modifyGenerationState);
					sb.Append(").Count==0)");
				}
			}
			else if(expr is DequePeekExpr)
			{
				DequePeekExpr dp = (DequePeekExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[dp]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Peek(");
					GenExpression(sb, dp.TargetExpr, modifyGenerationState);
					if(dp.NumberExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, dp.NumberExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is DequeIndexOfExpr)
			{
				DequeIndexOfExpr di = (DequeIndexOfExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[di]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.IndexOf(");
					GenExpression(sb, di.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, di.ValueExpr, modifyGenerationState);
					if(di.StartIndexExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, di.StartIndexExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is DequeLastIndexOfExpr)
			{
				DequeLastIndexOfExpr dli = (DequeLastIndexOfExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[dli]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.LastIndexOf(");
					GenExpression(sb, dli.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, dli.ValueExpr, modifyGenerationState);
					if(dli.StartIndexExpr != null)
					{
						sb.Append(", ");
						GenExpression(sb, dli.StartIndexExpr, modifyGenerationState);
					}
					sb.Append(")");
				}
			}
			else if(expr is DequeSubdequeExpr)
			{
				DequeSubdequeExpr dsd = (DequeSubdequeExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[dsd]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.Subdeque(");
					GenExpression(sb, dsd.TargetExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, dsd.StartExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, dsd.LengthExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is DequeAsSetExpr)
			{
				DequeAsSetExpr das = (DequeAsSetExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[das]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.DequeAsSet(");
					GenExpression(sb, das.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is DequeAsArrayExpr)
			{
				DequeAsArrayExpr daa = (DequeAsArrayExpr)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[daa]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.DequeAsArray(");
					GenExpression(sb, daa.TargetExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is MapInit)
			{
				MapInit mi = (MapInit)expr;
				if(mi.IsConstant())
					sb.Append(mi.AnonymousMapName);
				else
				{
					sb.Append("fill_" + mi.AnonymousMapName + "(");
					bool first = true;
					foreach(ExpressionPair item in mi.MapItems)
					{
						if(first)
							first = false;
						else
							sb.Append(", ");

						if(item.KeyExpr is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(item.KeyExpr.Type) + ")(");
						GenExpression(sb, item.KeyExpr, modifyGenerationState);
						if(item.KeyExpr is GraphEntityExpression)
							sb.Append(")");

						sb.Append(", ");

						if(item.ValueExpr is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(item.ValueExpr.Type) + ")(");
						GenExpression(sb, item.ValueExpr, modifyGenerationState);
						if(item.ValueExpr is GraphEntityExpression)
							sb.Append(")");
					}
					sb.Append(")");
				}
			}
			else if(expr is SetInit)
			{
				SetInit si = (SetInit)expr;
				if(si.IsConstant())
					sb.Append(si.AnonymousSetName);
				else
				{
					sb.Append("fill_" + si.AnonymousSetName + "(");
					bool first = true;
					foreach(Expression item in si.SetItems)
					{
						if(first)
							first = false;
						else
							sb.Append(", ");

						if(item is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(item.Type) + ")(");
						GenExpression(sb, item, modifyGenerationState);
						if(item is GraphEntityExpression)
							sb.Append(")");
					}
					sb.Append(")");
				}
			}
			else if(expr is ArrayInit)
			{
				ArrayInit ai = (ArrayInit)expr;
				if(ai.IsConstant())
					sb.Append(ai.AnonymousArrayName);
				else
				{
					sb.Append("fill_" + ai.AnonymousArrayName + "(");
					bool first = true;
					foreach(Expression item in ai.ArrayItems)
					{
						if(first)
							first = false;
						else
							sb.Append(", ");

						if(item is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(item.Type) + ")(");
						GenExpression(sb, item, modifyGenerationState);
						if(item is GraphEntityExpression)
							sb.Append(")");
					}
					sb.Append(")");
				}
			}
			else if(expr is DequeInit)
			{
				DequeInit di = (DequeInit)expr;
				if(di.IsConstant())
					sb.Append(di.AnonymousDequeName);
				else
				{
					sb.Append("fill_" + di.AnonymousDequeName + "(");
					bool first = true;
					foreach(Expression item in di.DequeItems)
					{
						if(first)
							first = false;
						else
							sb.Append(", ");

						if(item is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(item.Type) + ")(");
						GenExpression(sb, item, modifyGenerationState);
						if(item is GraphEntityExpression)
							sb.Append(")");
					}
					sb.Append(")");
				}
			}
			else if(expr is MatchInit)
			{
				MatchInit mi = (MatchInit)expr;
				sb.Append("new " + FormatDefinedMatchType(mi.MatchType) + "()");
			}
			else if(expr is InternalObjectInit)
			{
				InternalObjectInit ioi = (InternalObjectInit)expr;
				string fetchUniqueIdIfObject = "";
				if(ioi.BaseInternalObjectType is InternalObjectType)
					fetchUniqueIdIfObject = modifyGenerationState.Model.IsUniqueClassDefined() ? "graph.GlobalVariables.FetchObjectUniqueId()" : "-1";
				if(ioi.attributeInitializations.Count == 0)
					sb.Append("new " + FormatBaseInternalObjectType(ioi.BaseInternalObjectType) + "(" + fetchUniqueIdIfObject + ")");
				else
				{
					sb.Append("fill_" + ioi.AnonymousInternalObjectInitName + "(" + fetchUniqueIdIfObject);
					bool first = ioi.BaseInternalObjectType is InternalObjectType ? false : true;
					foreach(Expression aie in ioi.AttributeInitializationExpressions)
					{
						if(first)
							first = false;
						else
							sb.Append(", ");

						if(aie is GraphEntityExpression)
							sb.Append("(" + FormatElementInterfaceRef(aie.Type) + ")(");
						GenExpression(sb, aie, modifyGenerationState);
						if(aie is GraphEntityExpression)
							sb.Append(")");
					}
					sb.Append(")");
				}
			}
			else if(expr is MapCopyConstructor)
			{
				MapCopyConstructor mcc = (MapCopyConstructor)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[mcc]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.FillMap(");
					sb.Append("new " + FormatType(mcc.MapType) + "(), ");
					sb.Append("\"" + FormatSequenceType(mcc.MapType.KeyType) + "\", ");
					sb.Append("\"" + FormatSequenceType(mcc.MapType.ValueType) + "\", ");
					GenExpression(sb, mcc.MapToCopy, modifyGenerationState);
					sb.Append(", graph.Model");
					sb.Append(")");
				}
			}
			else if(expr is SetCopyConstructor)
			{
				SetCopyConstructor scc = (SetCopyConstructor)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[scc]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.FillSet(");
					sb.Append("new " + FormatType(scc.SetType) + "(), ");
					sb.Append("\"" + FormatSequenceType(scc.SetType.ValueType) + "\", ");
					GenExpression(sb, scc.SetToCopy, modifyGenerationState);
					sb.Append(", graph.Model");
					sb.Append(")");
				}
			}
			else if(expr is ArrayCopyConstructor)
			{
				ArrayCopyConstructor acc = (ArrayCopyConstructor)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[acc]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.FillArray(");
					sb.Append("new " + FormatType(acc.ArrayType) + "(), ");
					sb.Append("\"" + FormatSequenceType(acc.ArrayType.ValueType) + "\", ");
					GenExpression(sb, acc.ArrayToCopy, modifyGenerationState);
					sb.Append(", graph.Model");
					sb.Append(")");
				}
			}
			else if(expr is DequeCopyConstructor)
			{
				DequeCopyConstructor dcc = (DequeCopyConstructor)expr;
				if(modifyGenerationState != null && modifyGenerationState.UseVarForResult())
					sb.Append(modifyGenerationState.MapExprToTempVar[dcc]);
				else
				{
					SwitchToVarForResultAsNeeded(modifyGenerationState);
					sb.Append("GRGEN_LIBGR.ContainerHelper.FillDeque(");
					sb.Append("new " + FormatType(dcc.DequeType) + "(), ");
					sb.Append("\"" + FormatSequenceType(dcc.DequeType.ValueType) + "\", ");
					GenExpression(sb, dcc.DequeToCopy, modifyGenerationState);
					sb.Append(", graph.Model");
					sb.Append(")");
				}
			}
			else if(expr is FunctionInvocationExpr)
			{
				FunctionInvocationExpr fi = (FunctionInvocationExpr)expr;
				sb.Append("GRGEN_ACTIONS." + GetPackagePrefixDot(fi.Function) + "Functions."
						+ fi.Function.Ident.ToString() + "(actionEnv, graph");
				for(int i = 0; i < fi.Arity(); ++i)
				{
					sb.Append(", ");
					Expression argument = fi.GetArgument(i);
					if(argument.Type is InheritanceType)
						sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
					GenExpression(sb, argument, modifyGenerationState);
				}
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is ExternalFunctionInvocationExpr)
			{
				ExternalFunctionInvocationExpr efi = (ExternalFunctionInvocationExpr)expr;
				sb.Append("GRGEN_EXPR.ExternalFunctions." + efi.ExternalFunc.Ident.ToString() + "(actionEnv, graph");
				for(int i = 0; i < efi.Arity(); ++i)
				{
					sb.Append(", ");
					Expression argument = efi.GetArgument(i);
					if(argument.Type is InheritanceType)
						sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
					GenExpression(sb, argument, modifyGenerationState);
				}
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is FunctionMethodInvocationExpr)
			{
				FunctionMethodInvocationExpr fmi = (FunctionMethodInvocationExpr)expr;
				Entity owner = fmi.Owner;
				sb.Append("((" + FormatElementInterfaceRef(owner.Type) + ") ");
				sb.Append(FormatEntity(owner) + ").@");
				sb.Append(fmi.Function.Ident.ToString() + "(actionEnv, graph");
				for(int i = 0; i < fmi.Arity(); ++i)
				{
					sb.Append(", ");
					Expression argument = fmi.GetArgument(i);
					if(argument.Type is InheritanceType)
						sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
					GenExpression(sb, argument, modifyGenerationState);
				}
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is ExternalFunctionMethodInvocationExpr)
			{
				ExternalFunctionMethodInvocationExpr efmi = (ExternalFunctionMethodInvocationExpr)expr;
				sb.Append("(");
				GenExpression(sb, efmi.Owner, modifyGenerationState);
				sb.Append(").@");
				sb.Append(efmi.ExternalFunc.Ident.ToString() + "(actionEnv, graph");
				for(int i = 0; i < efmi.Arity(); ++i)
				{
					sb.Append(", ");
					Expression argument = efmi.GetArgument(i);
					if(argument.Type is InheritanceType)
						sb.Append("(" + FormatElementInterfaceRef(argument.Type) + ")");
					GenExpression(sb, argument, modifyGenerationState);
				}
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is EdgesExpr)
			{
				EdgesExpr e = (EdgesExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.Edges");
				sb.Append(GetDirectednessSuffix(e.Type));
				sb.Append("(graph, ");
				GenExpression(sb, e.EdgeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is NodesExpr)
			{
				NodesExpr n = (NodesExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.Nodes(graph, ");
				GenExpression(sb, n.NodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountEdgesExpr)
			{
				CountEdgesExpr ce = (CountEdgesExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.CountEdges(graph, ");
				GenExpression(sb, ce.EdgeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountNodesExpr)
			{
				CountNodesExpr cn = (CountNodesExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.CountNodes(graph, ");
				GenExpression(sb, cn.NodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is NowExpr)
			{
				//NowExpr n = (NowExpr)expr;
				sb.Append("DateTime.UtcNow.ToFileTime()");
			}
			else if(expr is EmptyExpr)
			{
				//EmptyExpr e = (EmptyExpr)expr;
				sb.Append("(graph.NumNodes+graph.NumEdges == 0)");
			}
			else if(expr is SizeExpr)
			{
				//SizeExpr s = (SizeExpr)expr;
				sb.Append("(graph.NumNodes+graph.NumEdges)");
			}
			else if(expr is SourceExpr)
			{
				SourceExpr s = (SourceExpr)expr;
				sb.Append("((");
				GenExpression(sb, s.EdgeExpr, modifyGenerationState);
				sb.Append(").Source)");
			}
			else if(expr is TargetExpr)
			{
				TargetExpr t = (TargetExpr)expr;
				sb.Append("((");
				GenExpression(sb, t.EdgeExpr, modifyGenerationState);
				sb.Append(").Target)");
			}
			else if(expr is OppositeExpr)
			{
				OppositeExpr o = (OppositeExpr)expr;
				sb.Append("((");
				GenExpression(sb, o.EdgeExpr, modifyGenerationState);
				sb.Append(").Opposite(");
				GenExpression(sb, o.NodeExpr, modifyGenerationState);
				sb.Append("))");
			}
			else if(expr is NodeByNameExpr)
			{
				NodeByNameExpr nbn = (NodeByNameExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, ");
				GenExpression(sb, nbn.NameExpr, modifyGenerationState);
				if(!nbn.NodeTypeExpr.Type.Ident.ToString().Equals("Node"))
				{
					sb.Append(", ");
					GenExpression(sb, nbn.NodeTypeExpr, modifyGenerationState);
				}
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is EdgeByNameExpr)
			{
				EdgeByNameExpr ebn = (EdgeByNameExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, ");
				GenExpression(sb, ebn.NameExpr, modifyGenerationState);
				if(!ebn.EdgeTypeExpr.Type.Ident.ToString().Equals("AEdge"))
				{
					sb.Append(", ");
					GenExpression(sb, ebn.EdgeTypeExpr, modifyGenerationState);
				}
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is NodeByUniqueExpr)
			{
				NodeByUniqueExpr nbu = (NodeByUniqueExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.GetNode(graph, ");
				GenExpression(sb, nbu.UniqueExpr, modifyGenerationState);
				if(!nbu.NodeTypeExpr.Type.Ident.ToString().Equals("Node"))
				{
					sb.Append(", ");
					GenExpression(sb, nbu.NodeTypeExpr, modifyGenerationState);
				}
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is EdgeByUniqueExpr)
			{
				EdgeByUniqueExpr ebu = (EdgeByUniqueExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.GetEdge(graph, ");
				GenExpression(sb, ebu.UniqueExpr, modifyGenerationState);
				if(!ebu.EdgeTypeExpr.Type.Ident.ToString().Equals("AEdge"))
				{
					sb.Append(", ");
					GenExpression(sb, ebu.EdgeTypeExpr, modifyGenerationState);
				}
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IncidentEdgeExpr)
			{
				IncidentEdgeExpr ie = (IncidentEdgeExpr)expr;
				if(ie.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.Outgoing");
				else if(ie.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.Incoming");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.Incident");
				sb.Append(GetDirectednessSuffix(ie.Type));
				sb.Append("(");
				GenExpression(sb, ie.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ie.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ie.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is AdjacentNodeExpr)
			{
				AdjacentNodeExpr an = (AdjacentNodeExpr)expr;
				if(an.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.AdjacentOutgoing(");
				else if(an.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.AdjacentIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.Adjacent(");
				GenExpression(sb, an.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, an.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, an.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountIncidentEdgeExpr)
			{
				CountIncidentEdgeExpr cie = (CountIncidentEdgeExpr)expr;
				if(cie.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountOutgoing(");
				else if(cie.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountIncident(");
				GenExpression(sb, cie.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cie.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cie.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountAdjacentNodeExpr)
			{
				CountAdjacentNodeExpr can = (CountAdjacentNodeExpr)expr;
				if(can.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountAdjacentOutgoing(graph, ");
				else if(can.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountAdjacentIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountAdjacent(graph, ");
				GenExpression(sb, can.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, can.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, can.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsAdjacentNodeExpr)
			{
				IsAdjacentNodeExpr ian = (IsAdjacentNodeExpr)expr;
				if(ian.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentOutgoing(");
				else if(ian.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsAdjacent(");
				GenExpression(sb, ian.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ian.EndNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ian.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ian.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsIncidentEdgeExpr)
			{
				IsIncidentEdgeExpr iie = (IsIncidentEdgeExpr)expr;
				if(iie.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsOutgoing(");
				else if(iie.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsIncident(");
				GenExpression(sb, iie.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, iie.EndEdgeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, iie.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, iie.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is ReachableEdgeExpr)
			{
				ReachableEdgeExpr re = (ReachableEdgeExpr)expr;
				if(re.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing");
				else if(re.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.ReachableEdges");
				sb.Append(GetDirectednessSuffix(re.Type));
				sb.Append("(graph, ");
				GenExpression(sb, re.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, re.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, re.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is ReachableNodeExpr)
			{
				ReachableNodeExpr rn = (ReachableNodeExpr)expr;
				if(rn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.ReachableOutgoing(");
				else if(rn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.ReachableIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.Reachable(");
				GenExpression(sb, rn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, rn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, rn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountReachableEdgeExpr)
			{
				CountReachableEdgeExpr cre = (CountReachableEdgeExpr)expr;
				if(cre.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesOutgoing(graph, ");
				else if(cre.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdges(graph, ");
				GenExpression(sb, cre.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cre.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cre.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountReachableNodeExpr)
			{
				CountReachableNodeExpr crn = (CountReachableNodeExpr)expr;
				if(crn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachableOutgoing(");
				else if(crn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachableIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountReachable(");
				GenExpression(sb, crn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, crn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, crn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsReachableNodeExpr)
			{
				IsReachableNodeExpr irn = (IsReachableNodeExpr)expr;
				if(irn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachableOutgoing(graph, ");
				else if(irn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachableIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachable(graph, ");
				GenExpression(sb, irn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, irn.EndNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, irn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, irn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsReachableEdgeExpr)
			{
				IsReachableEdgeExpr ire = (IsReachableEdgeExpr)expr;
				if(ire.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesOutgoing(graph, ");
				else if(ire.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdges(graph, ");
				GenExpression(sb, ire.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ire.EndEdgeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ire.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ire.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is BoundedReachableEdgeExpr)
			{
				BoundedReachableEdgeExpr bre = (BoundedReachableEdgeExpr)expr;
				if(bre.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing");
				else if(bre.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdges");
				sb.Append(GetDirectednessSuffix(bre.Type));
				sb.Append("(graph, ");
				GenExpression(sb, bre.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, bre.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, bre.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, bre.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is BoundedReachableNodeExpr)
			{
				BoundedReachableNodeExpr brn = (BoundedReachableNodeExpr)expr;
				if(brn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing(");
				else if(brn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachable(");
				GenExpression(sb, brn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brn.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is BoundedReachableNodeWithRemainingDepthExpr)
			{
				BoundedReachableNodeWithRemainingDepthExpr brnwrd = (BoundedReachableNodeWithRemainingDepthExpr)expr;
				if(brnwrd.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthOutgoing(");
				else if(brnwrd.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepth(");
				GenExpression(sb, brnwrd.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brnwrd.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brnwrd.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, brnwrd.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountBoundedReachableEdgeExpr)
			{
				CountBoundedReachableEdgeExpr cbre = (CountBoundedReachableEdgeExpr)expr;
				if(cbre.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesOutgoing(graph, ");
				else if(cbre.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdges(graph, ");
				GenExpression(sb, cbre.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbre.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbre.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbre.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is CountBoundedReachableNodeExpr)
			{
				CountBoundedReachableNodeExpr cbrn = (CountBoundedReachableNodeExpr)expr;
				if(cbrn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableOutgoing(");
				else if(cbrn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableIncoming(");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachable(");
				GenExpression(sb, cbrn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbrn.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbrn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, cbrn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsBoundedReachableNodeExpr)
			{
				IsBoundedReachableNodeExpr ibrn = (IsBoundedReachableNodeExpr)expr;
				if(ibrn.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableOutgoing(graph, ");
				else if(ibrn.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachable(graph, ");
				GenExpression(sb, ibrn.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibrn.EndNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibrn.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibrn.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibrn.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is IsBoundedReachableEdgeExpr)
			{
				IsBoundedReachableEdgeExpr ibre = (IsBoundedReachableEdgeExpr)expr;
				if(ibre.Direction() == Direction.OUTGOING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesOutgoing(graph, ");
				else if(ibre.Direction() == Direction.INCOMING)
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesIncoming(graph, ");
				else
					sb.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdges(graph, ");
				GenExpression(sb, ibre.StartNodeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibre.EndEdgeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibre.DepthExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibre.IncidentEdgeTypeExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, ibre.AdjacentNodeTypeExpr, modifyGenerationState);
				if(modifyGenerationState.EmitProfilingInstrumentation())
					sb.Append(", actionEnv");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is NodesFromIndexAccessSameExpr)
			{
				NodesFromIndexAccessSameExpr nfias = (NodesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = nfias.IndexAccessEquality;
				if(nfias.Type is SetType)
					sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexSame(");
				else
					sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexSameAsArray(");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is NodesFromIndexAccessFromToExpr)
			{
				NodesFromIndexAccessFromToExpr nfiaft = (NodesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = nfiaft.IndexAccessOrdering;
				if(nfiaft.Type is SetType)
					sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexFromTo(");
				else
				{
					if(iao.ascending)
						sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexFromToAsArrayAscending(");
					else
						sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexFromToAsArrayDescending(");
				}
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is EdgesFromIndexAccessSameExpr)
			{
				EdgesFromIndexAccessSameExpr efias = (EdgesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = efias.IndexAccessEquality;
				if(efias.Type is SetType)
					sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexSame(");
				else
					sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexSameAsArray(");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is EdgesFromIndexAccessFromToExpr)
			{
				EdgesFromIndexAccessFromToExpr efiaft = (EdgesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = efiaft.IndexAccessOrdering;
				if(efiaft.Type is SetType)
					sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexFromTo(");
				else
				{
					if(iao.ascending)
						sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexFromToAsArrayAscending(");
					else
						sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexFromToAsArrayDescending(");
				}
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is CountNodesFromIndexAccessSameExpr)
			{
				CountNodesFromIndexAccessSameExpr cnfias = (CountNodesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = cnfias.IndexAccessEquality;
				sb.Append("GRGEN_LIBGR.IndexHelper.CountNodesFromIndexSame(");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is CountNodesFromIndexAccessFromToExpr)
			{
				CountNodesFromIndexAccessFromToExpr cnfiaft = (CountNodesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = cnfiaft.IndexAccessOrdering;
				sb.Append("GRGEN_LIBGR.IndexHelper.CountNodesFromIndexFromTo(");
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is CountEdgesFromIndexAccessSameExpr)
			{
				CountEdgesFromIndexAccessSameExpr cefias = (CountEdgesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = cefias.IndexAccessEquality;
				sb.Append("GRGEN_LIBGR.IndexHelper.CountEdgesFromIndexSame(");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is CountEdgesFromIndexAccessFromToExpr)
			{
				CountEdgesFromIndexAccessFromToExpr cefiaft = (CountEdgesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = cefiaft.IndexAccessOrdering;
				sb.Append("GRGEN_LIBGR.IndexHelper.CountEdgesFromIndexFromTo(");
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IsInNodesFromIndexAccessSameExpr)
			{
				IsInNodesFromIndexAccessSameExpr iinfias = (IsInNodesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = iinfias.IndexAccessEquality;
				sb.Append("GRGEN_LIBGR.IndexHelper.IsInNodesFromIndexSame(");
				GenExpression(sb, iinfias.CandidateExpr, modifyGenerationState);
				sb.Append(", ");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IsInNodesFromIndexAccessFromToExpr)
			{
				IsInNodesFromIndexAccessFromToExpr iinfiaft = (IsInNodesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = iinfiaft.IndexAccessOrdering;
				sb.Append("GRGEN_LIBGR.IndexHelper.IsInNodesFromIndexFromTo(");
				GenExpression(sb, iinfiaft.CandidateExpr, modifyGenerationState);
				sb.Append(", ");
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IsInEdgesFromIndexAccessSameExpr)
			{
				IsInEdgesFromIndexAccessSameExpr iiefias = (IsInEdgesFromIndexAccessSameExpr)expr;
				IndexAccessEquality iae = iiefias.IndexAccessEquality;
				sb.Append("GRGEN_LIBGR.IndexHelper.IsInEdgesFromIndexSame(");
				GenExpression(sb, iiefias.CandidateExpr, modifyGenerationState);
				sb.Append(", ");
				GenIndexAccessEquality(sb, iae, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IsInEdgesFromIndexAccessFromToExpr)
			{
				IsInEdgesFromIndexAccessFromToExpr iiefiaft = (IsInEdgesFromIndexAccessFromToExpr)expr;
				IndexAccessOrdering iao = iiefiaft.IndexAccessOrdering;
				sb.Append("GRGEN_LIBGR.IndexHelper.IsInEdgesFromIndexFromTo(");
				GenExpression(sb, iiefiaft.CandidateExpr, modifyGenerationState);
				sb.Append(", ");
				GenIndexAccessOrdering(sb, iao, modifyGenerationState);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is NodesFromIndexAccessMultipleFromToExpr)
			{
				NodesFromIndexAccessMultipleFromToExpr nfiamft = (NodesFromIndexAccessMultipleFromToExpr)expr;
				sb.Append("GRGEN_LIBGR.IndexHelper.NodesFromIndexMultipleFromTo(");
				GenProfilingAndOrParallelizationArgumentsAtBegin(sb, modifyGenerationState);
				bool first = true;
				foreach(IndexAccessOrdering iao in nfiamft.IndexAccesses)
				{
					if(first)
						first = false;
					else
						sb.Append(",");
					sb.Append("new GRGEN_LIBGR.IndexHelper.IndexAccess(");
					GenIndexAccessOrdering(sb, iao, modifyGenerationState);
					sb.Append(")");
				}
				sb.Append(")");
			}
			else if(expr is EdgesFromIndexAccessMultipleFromToExpr)
			{
				EdgesFromIndexAccessMultipleFromToExpr efiamft = (EdgesFromIndexAccessMultipleFromToExpr)expr;
				sb.Append("GRGEN_LIBGR.IndexHelper.EdgesFromIndexMultipleFromTo(");
				GenProfilingAndOrParallelizationArgumentsAtBegin(sb, modifyGenerationState);
				bool first = true;
				foreach(IndexAccessOrdering iao in efiamft.IndexAccesses)
				{
					if(first)
						first = false;
					else
						sb.Append(",");
					sb.Append("new GRGEN_LIBGR.IndexHelper.IndexAccess(");
					GenIndexAccessOrdering(sb, iao, modifyGenerationState);
					sb.Append(")");
				}
				sb.Append(")");
			}
			else if(expr is MinMaxFromIndexExpr)
			{
				MinMaxFromIndexExpr mmfi = (MinMaxFromIndexExpr)expr;
				if(mmfi.Type is NodeType)
				{
					if(mmfi.IsMin())
						sb.Append("GRGEN_LIBGR.IndexHelper.MinNodeFromIndex(");
					else
						sb.Append("GRGEN_LIBGR.IndexHelper.MaxNodeFromIndex(");
				}
				else
				{
					if(mmfi.IsMin())
						sb.Append("GRGEN_LIBGR.IndexHelper.MinEdgeFromIndex(");
					else
						sb.Append("GRGEN_LIBGR.IndexHelper.MaxEdgeFromIndex(");
				}
				sb.Append("((GRGEN_MODEL." + modifyGenerationState.Model.Ident + "IndexSet)graph.Indices)." + mmfi.index.Ident);
				GenProfilingAndOrParallelizationArguments(sb, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is IndexSizeExpr)
			{
				IndexSizeExpr mmfi = (IndexSizeExpr)expr;
				sb.Append("(((GRGEN_MODEL." + modifyGenerationState.Model.Ident + "IndexSet)graph.Indices)." + mmfi.index.Ident);
				sb.Append(").Size");
			}
			else if(expr is InducedSubgraphExpr)
			{
				InducedSubgraphExpr @is = (InducedSubgraphExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, @is.SetExpr, modifyGenerationState);
				sb.Append(", graph)");
			}
			else if(expr is DefinedSubgraphExpr)
			{
				DefinedSubgraphExpr ds = (DefinedSubgraphExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.DefinedSubgraph");
				switch(GetDirectednessSuffix(ds.SetExpr.Type))
				{
				case "Directed":
					sb.Append("Directed(");
					sb.Append("(IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)");
					break;
				case "Undirected":
					sb.Append("Undirected(");
					sb.Append("(IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)");
					break;
				default:
					sb.Append("(");
					sb.Append("(IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
					break;
				}
				GenExpression(sb, ds.SetExpr, modifyGenerationState);
				sb.Append(", graph)");
			}
			else if(expr is EqualsAnyExpr)
			{
				EqualsAnyExpr ea = (EqualsAnyExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)");
				GenExpression(sb, ea.SubgraphExpr, modifyGenerationState);
				sb.Append(", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, ea.SetExpr, modifyGenerationState);
				sb.Append(", ");
				sb.Append(ea.IncludingAttributes ? "true" : "false");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is GetEquivalentExpr)
			{
				GetEquivalentExpr ge = (GetEquivalentExpr)expr;
				sb.Append("GRGEN_LIBGR.GraphHelper.GetEquivalent((GRGEN_LIBGR.IGraph)");
				GenExpression(sb, ge.SubgraphExpr, modifyGenerationState);
				sb.Append(", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)");
				GenExpression(sb, ge.SetExpr, modifyGenerationState);
				sb.Append(", ");
				sb.Append(ge.IncludingAttributes ? "true" : "false");
				if(modifyGenerationState.IsToBeParallelizedActionExisting())
					sb.Append(", threadId");
				sb.Append(")");
			}
			else if(expr is MaxExpr)
			{
				MaxExpr m = (MaxExpr)expr;
				sb.Append("Math.Max(");
				GenExpression(sb, m.LeftExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, m.RightExpr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is MinExpr)
			{
				MinExpr m = (MinExpr)expr;
				sb.Append("Math.Min(");
				GenExpression(sb, m.LeftExpr, modifyGenerationState);
				sb.Append(", ");
				GenExpression(sb, m.RightExpr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is AbsExpr)
			{
				AbsExpr a = (AbsExpr)expr;
				sb.Append("Math.Abs(");
				GenExpression(sb, a.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is SgnExpr)
			{
				SgnExpr s = (SgnExpr)expr;
				sb.Append("Math.Sign(");
				GenExpression(sb, s.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is PiExpr)
			{
				//PiExpr pi = (PiExpr)expr;
				sb.Append("Math.PI");
			}
			else if(expr is EExpr)
			{
				//EExpr e = (EExpr)expr;
				sb.Append("Math.E");
			}
			else if(expr is ByteMinExpr)
				sb.Append("SByte.MinValue");
			else if(expr is ByteMaxExpr)
				sb.Append("SByte.MaxValue");
			else if(expr is ShortMinExpr)
				sb.Append("Int16.MinValue");
			else if(expr is ShortMaxExpr)
				sb.Append("Int16.MaxValue");
			else if(expr is IntMinExpr)
				sb.Append("Int32.MinValue");
			else if(expr is IntMaxExpr)
				sb.Append("Int32.MaxValue");
			else if(expr is LongMinExpr)
				sb.Append("Int64.MinValue");
			else if(expr is LongMaxExpr)
				sb.Append("Int64.MaxValue");
			else if(expr is FloatMinExpr)
				sb.Append("Single.MinValue");
			else if(expr is FloatMaxExpr)
				sb.Append("Single.MaxValue");
			else if(expr is DoubleMinExpr)
				sb.Append("Double.MinValue");
			else if(expr is DoubleMaxExpr)
				sb.Append("Double.MaxValue");
			else if(expr is CeilExpr)
			{
				CeilExpr c = (CeilExpr)expr;
				sb.Append("Math.Ceiling(");
				GenExpression(sb, c.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is FloorExpr)
			{
				FloorExpr f = (FloorExpr)expr;
				sb.Append("Math.Floor(");
				GenExpression(sb, f.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is RoundExpr)
			{
				RoundExpr r = (RoundExpr)expr;
				sb.Append("Math.Round(");
				GenExpression(sb, r.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is TruncateExpr)
			{
				TruncateExpr t = (TruncateExpr)expr;
				sb.Append("Math.Truncate(");
				GenExpression(sb, t.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is SinCosTanExpr)
			{
				SinCosTanExpr sct = (SinCosTanExpr)expr;
				switch(sct.Which)
				{
				case SinCosTanExpr.TrigonometryFunctionType.sin:
					sb.Append("Math.Sin(");
					break;
				case SinCosTanExpr.TrigonometryFunctionType.cos:
					sb.Append("Math.Cos(");
					break;
				case SinCosTanExpr.TrigonometryFunctionType.tan:
					sb.Append("Math.Tan(");
					break;
				}
				GenExpression(sb, sct.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is ArcSinCosTanExpr)
			{
				ArcSinCosTanExpr asct = (ArcSinCosTanExpr)expr;
				switch(asct.Which)
				{
				case ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arcsin:
					sb.Append("Math.Asin(");
					break;
				case ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arccos:
					sb.Append("Math.Acos(");
					break;
				case ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arctan:
					sb.Append("Math.Atan(");
					break;
				}
				GenExpression(sb, asct.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is CanonizeExpr)
			{
				CanonizeExpr c = (CanonizeExpr)expr;
				sb.Append("(");
				GenExpression(sb, c.GraphExpr, modifyGenerationState);
				sb.Append(").Canonize()");
			}
			else if(expr is SqrExpr)
			{
				SqrExpr s = (SqrExpr)expr;
				sb.Append("GRGEN_LIBGR.MathHelper.Sqr(");
				GenExpression(sb, s.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is SqrtExpr)
			{
				SqrtExpr s = (SqrtExpr)expr;
				sb.Append("Math.Sqrt(");
				GenExpression(sb, s.Expr, modifyGenerationState);
				sb.Append(")");
			}
			else if(expr is PowExpr)
			{
				PowExpr p = (PowExpr)expr;
				if(p.LeftExpr != null)
				{
					sb.Append("Math.Pow(");
					GenExpression(sb, p.LeftExpr, modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, p.RightExpr, modifyGenerationState);
					sb.Append(")");
				}
				else
				{
					sb.Append("Math.Exp(");
					GenExpression(sb, p.RightExpr, modifyGenerationState);
					sb.Append(")");
				}
			}
			else if(expr is LogExpr)
			{
				LogExpr l = (LogExpr)expr;
				sb.Append("Math.Log(");
				GenExpression(sb, l.LeftExpr, modifyGenerationState);
				if(l.RightExpr != null)
				{
					sb.Append(", ");
					GenExpression(sb, l.RightExpr, modifyGenerationState);
				}
				sb.Append(")");
			}
			else if(expr is ProjectionExpr)
			{
				ProjectionExpr proj = (ProjectionExpr)expr;
				sb.Append(proj.ProjectedValueVarName);
			}
			else if(expr is MatchAccess)
			{
				MatchAccess ma = (MatchAccess)expr;
				GenExpression(sb, ma.Expr, modifyGenerationState);
				sb.Append(".");
				sb.Append(FormatEntity(ma.Entity));
			}
			else if(expr is IteratedQueryExpr)
			{
				IteratedQueryExpr iq = (IteratedQueryExpr)expr;
				sb.Append("curMatch." + iq.IteratedName.ToString() + ".ToListExact()");
			}
			else if(expr is ScanExpr)
			{
				ScanExpr s = (ScanExpr)expr;
				sb.Append("((" + FormatType(s.Type) + ")");
				sb.Append("GRGEN_LIBGR.GRSImport.Scan(" + FormatAttributeTypeObject(s.Type) + ", ");
				GenExpression(sb, s.StringExpr, modifyGenerationState);
				sb.Append(", graph))");
			}
			else if(expr is TryScanExpr)
			{
				TryScanExpr ts = (TryScanExpr)expr;
				sb.Append("GRGEN_LIBGR.GRSImport.TryScan(" + FormatAttributeTypeObject(ts.TargetType) + ", ");
				GenExpression(sb, ts.StringExpr, modifyGenerationState);
				sb.Append(", graph)");
			}
			else
				throw new System.NotSupportedException("Unsupported expression type (" + expr + ")");
		}

		internal virtual void GenIndexAccessEquality(SourceBuilder sb, IndexAccessEquality iae, ExpressionGenerationState modifyGenerationState)
		{
			sb.Append("((GRGEN_MODEL." + modifyGenerationState.Model.Ident + "IndexSet)graph.Indices)." + iae.index.Ident + ", ");
			GenExpression(sb, iae.expr, modifyGenerationState);
		}

		internal virtual void GenIndexAccessOrdering(SourceBuilder sb, IndexAccessOrdering iao, ExpressionGenerationState modifyGenerationState)
		{
			sb.Append("((GRGEN_MODEL." + modifyGenerationState.Model.Ident + "IndexSet)graph.Indices)." + iao.index.Ident + ", ");
			if(iao.From() != null)
				GenExpression(sb, iao.From(), modifyGenerationState);
			else
				sb.Append("null");
			sb.Append(", ");
			sb.Append(iao.IncludingFrom() ? "true" : "false");
			sb.Append(", ");
			if(iao.To() != null)
				GenExpression(sb, iao.To(), modifyGenerationState);
			else
				sb.Append("null");
			sb.Append(", ");
			sb.Append(iao.IncludingTo() ? "true" : "false");
		}

		internal virtual void GenProfilingAndOrParallelizationArguments(SourceBuilder sb, ExpressionGenerationState modifyGenerationState)
		{
			if(modifyGenerationState.EmitProfilingInstrumentation())
				sb.Append(", actionEnv");
			if(modifyGenerationState.IsToBeParallelizedActionExisting())
				sb.Append(", threadId");
		}

		internal virtual void GenProfilingAndOrParallelizationArgumentsAtBegin(SourceBuilder sb, ExpressionGenerationState modifyGenerationState)
		{
			if(modifyGenerationState.EmitProfilingInstrumentation())
				sb.Append("actionEnv, ");
			if(modifyGenerationState.IsToBeParallelizedActionExisting())
				sb.Append("threadId, ");
		}

		protected internal virtual void SwitchToVarForResultAsNeeded(ExpressionGenerationState modifyGenerationState)
		{
			if(modifyGenerationState != null && modifyGenerationState.SwitchToVarForResultAfterFirstVarUsage())
				modifyGenerationState.SwitchToVarForResult();
		}

		protected internal virtual string FormatAttributeTypeObject(Type t)
		{
			SourceBuilder sb = new SourceBuilder();
			if(t is MapType)
			{
				MapType mt = (MapType)t;
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", null, ");
				sb.Append(FormatAttributeTypeObject(mt.ValueType) + ", ");
				sb.Append(FormatAttributeTypeObject(mt.KeyType) + ", ");
				sb.Append("null, null, null, null)");
			}
			else if(t is SetType)
			{
				SetType st = (SetType)t;
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", null, ");
				sb.Append(FormatAttributeTypeObject(st.ValueType) + ", null,");
				sb.Append("null, null, null, null)");
			}
			else if(t is ArrayType)
			{
				ArrayType at = (ArrayType)t;
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", null, ");
				sb.Append(FormatAttributeTypeObject(at.ValueType) + ", null,");
				sb.Append("null, null, null, null)");
			}
			else if(t is DequeType)
			{
				DequeType qt = (DequeType)t;
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", null, ");
				sb.Append(FormatAttributeTypeObject(qt.ValueType) + ", null,");
				sb.Append("null, null, null, null)");
			}
			else if(t is EnumType)
			{
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", ");
				sb.Append("GRGEN_MODEL." + GetPackagePrefixDot(t) + "Enums.@" + FormatIdentifiable(t) + ", ");
				sb.Append("null, null, ");
				sb.Append("null, null, null, null)");
			}
			else
			{ // maybe todo: distinguish node/edge/class object/transient class object
				sb.Append("new GRGEN_LIBGR.AttributeType(\"dummy\", null, " + GetAttributeKind(t) + ", null, ");
				sb.Append("null, null, ");
				sb.Append("null, null, null, null)");
			}
			return sb.ToString();
		}

		private static string GetAttributeKind(Type t)
		{
			if(t is ByteType)
				return "GRGEN_LIBGR.AttributeKind.ByteAttr";
			else if(t is ShortType)
				return "GRGEN_LIBGR.AttributeKind.ShortAttr";
			else if(t is IntType)
				return "GRGEN_LIBGR.AttributeKind.IntegerAttr";
			else if(t is LongType)
				return "GRGEN_LIBGR.AttributeKind.LongAttr";
			else if(t is FloatType)
				return "GRGEN_LIBGR.AttributeKind.FloatAttr";
			else if(t is DoubleType)
				return "GRGEN_LIBGR.AttributeKind.DoubleAttr";
			else if(t is BooleanType)
				return "GRGEN_LIBGR.AttributeKind.BooleanAttr";
			else if(t is StringType)
				return "GRGEN_LIBGR.AttributeKind.StringAttr";
			else if(t is EnumType)
				return "GRGEN_LIBGR.AttributeKind.EnumAttr";
			else if(t is ObjectType || t is VoidType || t is ExternalObjectType)
				return "GRGEN_LIBGR.AttributeKind.ObjectAttr";
			else if(t is MapType)
				return "GRGEN_LIBGR.AttributeKind.MapAttr";
			else if(t is SetType)
				return "GRGEN_LIBGR.AttributeKind.SetAttr";
			else if(t is ArrayType)
				return "GRGEN_LIBGR.AttributeKind.ArrayAttr";
			else if(t is DequeType)
				return "GRGEN_LIBGR.AttributeKind.DequeAttr";
			else if(t is NodeType)
				return "GRGEN_LIBGR.AttributeKind.NodeAttr";
			else if(t is EdgeType)
				return "GRGEN_LIBGR.AttributeKind.EdgeAttr";
			else if(t is GraphType)
				return "GRGEN_LIBGR.AttributeKind.GraphAttr";
			else if(t is InternalObjectType)
				return "GRGEN_LIBGR.AttributeKind.InternalClassObjectAttr";
			else if(t is InternalTransientObjectType)
				return "GRGEN_LIBGR.AttributeKind.InternalClassTransientObjectAttr";
			else
				throw new ArgumentException("Unknown Type: " + t);
		}

		public virtual void GenOperator(SourceBuilder sb, Operator op,
				ExpressionGenerationState modifyGenerationState)
		{
			switch(op.Arity())
			{
			case 1:
				sb.Append("(" + GetOperatorSymbol(op.OpCode) + " ");
				GenExpression(sb, op.GetOperand(0), modifyGenerationState);
				sb.Append(")");
				break;
			case 2:
				GenBinaryOperator(sb, op, modifyGenerationState);
				break;
			case 3:
				if(op.OpCode == OperatorCode.COND)
				{
					sb.Append("((");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(") ? (");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(") : (");
					GenExpression(sb, op.GetOperand(2), modifyGenerationState);
					sb.Append("))");
					break;
				}
				goto default; //$FALL-THROUGH$
			default:
				throw new System.NotSupportedException("Unsupported operation arity (" + op.Arity() + ")");
			}
		}

		public virtual void GenBinaryOperator(SourceBuilder sb, Operator op,
				ExpressionGenerationState modifyGenerationState)
		{
			switch(op.OpCode)
			{
			case de.unika.ipd.grgen.ir.expr.OperatorCode.IN:
			{
				Type opType = op.GetOperand(1).Type;
				GenExpression(sb, op.GetOperand(1), modifyGenerationState);
				bool isDictionary = opType is SetType || opType is MapType;
				sb.Append(isDictionary ? ".ContainsKey(" : ".Contains(");
				if(op.GetOperand(0) is GraphEntityExpression)
					sb.Append("(" + FormatElementInterfaceRef(op.GetOperand(0).Type) + ")(");
				GenExpression(sb, op.GetOperand(0), modifyGenerationState);
				if(op.GetOperand(0) is GraphEntityExpression)
					sb.Append(")");
				sb.Append(")");
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.ADD:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Concatenate(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_OR:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Union(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_AND:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Intersect(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.EXCEPT:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Except(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.EQ:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Equal(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Equal(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.Equal(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is GraphType)
				{
					sb.Append("GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else if(opType is InternalObjectType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.IObject)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.IObject)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else if(opType is InternalTransientObjectType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.ITransientObject)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.ITransientObject)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.NE:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.NotEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is GraphType)
				{
					sb.Append("!GRGEN_LIBGR.GraphHelper.Equal((GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else if(opType is InternalObjectType)
				{
					sb.Append("!GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.IObject)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.IObject)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else if(opType is InternalTransientObjectType)
				{
					sb.Append("!GRGEN_LIBGR.ContainerHelper.IsEqual((GRGEN_LIBGR.ITransientObject)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.ITransientObject)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.SE:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is SetType)
				{
					SetType setType = (SetType)opType;
					sb.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqualSet(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					if(setType.ValueType is InheritanceType && !(setType.ValueType is ExternalObjectType))
					{
						sb.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
						sb.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
					}
					else
					{
						sb.Append(", new Dictionary<object, object>()");
						sb.Append(", new Dictionary<object, object>()");
					}
					sb.Append(")");
				}
				else if(opType is MapType)
				{
					MapType mapType = (MapType)opType;
					sb.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqualMap(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					if(mapType.KeyType is InheritanceType && !(mapType.KeyType is ExternalObjectType))
					{
						sb.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
						sb.Append(", new Dictionary<GRGEN_LIBGR.IAttributeBearer, object>()");
					}
					else
					{
						sb.Append(", new Dictionary<object, object>()");
						sb.Append(", new Dictionary<object, object>()");
					}
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					ArrayType arrayType = (ArrayType)opType;
					string methodName = arrayType.ValueType is InheritanceType && !(arrayType.ValueType is ExternalObjectType) ?
							"DeeplyEqualArrayAttributeBearer" : "DeeplyEqualArrayObject";
					sb.Append("GRGEN_LIBGR.ContainerHelper." + methodName + "(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					DequeType dequeType = (DequeType)opType;
					string methodName = dequeType.ValueType is InheritanceType && !(dequeType.ValueType is ExternalObjectType) ?
							"DeeplyEqualDequeAttributeBearer" : "DeeplyEqualDequeObject";
					sb.Append("GRGEN_LIBGR.ContainerHelper." + methodName + "(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					sb.Append(")");
				}
				else if(opType is InternalObjectType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					sb.Append(")");
				}
				else if(opType is InternalTransientObjectType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.DeeplyEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()");
					sb.Append(")");
				}
				else if(modifyGenerationState.Model.IsEqualClassDefined() && (opType is ObjectType || opType is ExternalObjectType))
				{
					sb.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>())");
				}
				else
				{
					sb.Append("GRGEN_LIBGR.GraphHelper.HasSameStructure((GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append("), (GRGEN_LIBGR.IGraph)(");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append("))");
				}
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.GT:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is StringType)
				{
					sb.Append("(String.Compare(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", StringComparison.InvariantCulture)>0)");
				}
				else if(modifyGenerationState.Model.IsLowerClassDefined() && (opType is ObjectType || opType is ExternalObjectType))
				{
					sb.Append("(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>())");
					sb.Append("&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()))");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.GE:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.GreaterOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is StringType)
				{
					sb.Append("(String.Compare(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", StringComparison.InvariantCulture)>=0)");
				}
				else if(modifyGenerationState.Model.IsLowerClassDefined() && (opType is ObjectType || opType is ExternalObjectType))
				{
					sb.Append("!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>())");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.LT:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessThan(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is StringType)
				{
					sb.Append("(String.Compare(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", StringComparison.InvariantCulture)<0)");
				}
				else if(modifyGenerationState.Model.IsLowerClassDefined() && (opType is ObjectType || opType is ExternalObjectType))
				{
					sb.Append("GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>())");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			case de.unika.ipd.grgen.ir.expr.OperatorCode.LE:
			{
				Type opType = op.GetOperand(0).Type;
				if(opType is MapType || opType is SetType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is ArrayType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is DequeType)
				{
					sb.Append("GRGEN_LIBGR.ContainerHelper.LessOrEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(")");
				}
				else if(opType is StringType)
				{
					sb.Append("(String.Compare(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(", ");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", StringComparison.InvariantCulture)<=0)");
				}
				else if(modifyGenerationState.Model.IsLowerClassDefined() && (opType is ObjectType || opType is ExternalObjectType))
				{
					sb.Append("(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>())");
					sb.Append("|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(");
					GenExpression(sb, op.GetOperand(0), modifyGenerationState);
					sb.Append(",");
					GenExpression(sb, op.GetOperand(1), modifyGenerationState);
					sb.Append(", new Dictionary<object, object>()))");
				}
				else
					GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}

			default:
				GenBinOpDefault(sb, op, modifyGenerationState);
				break;
			}
		}

		protected internal virtual string FormatGlobalVariableRead(Entity globalVar)
		{
			return "((" + FormatType(globalVar.Type)
					+ ")((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).GetVariableValue(\""
					+ FormatIdentifiable(globalVar) + "\"))";
		}

		protected internal virtual string FormatGlobalVariableWrite(Entity globalVar, string value)
		{
			return "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv).SetVariableValue(\""
					+ FormatIdentifiable(globalVar) + "\", (" + FormatType(globalVar.Type) + ")(" + value + "))";
		}

		protected internal static string GetValueAsCSSharpString(Constant constant)
		{
			Type type = constant.Type;

			//emit C-code for constants
			switch(type.Classify())
			{
			case Type.TypeClass.IS_STRING:
				object value = constant.Value;
				if(value == null)
					return "null";
				else
					return "\"" + constant.Value + "\"";
			case Type.TypeClass.IS_BOOLEAN:
				bool? bool_const = (bool?)constant.Value;
				if(bool_const.Value)
					return "true"; // true-value
				else
					return "false"; // false-value
			case Type.TypeClass.IS_BYTE:
			case Type.TypeClass.IS_SHORT:
			case Type.TypeClass.IS_INTEGER: // this also applys to enum constants
			case Type.TypeClass.IS_DOUBLE:
				return constant.Value.ToString();
			case Type.TypeClass.IS_LONG:
				return constant.Value.ToString() + "L";
			case Type.TypeClass.IS_FLOAT:
				return constant.Value.ToString() + "f";
			case Type.TypeClass.IS_TYPE:
				InheritanceType it = (InheritanceType)constant.Value;
				return FormatTypeClassRef(it) + ".typeVar";
			case Type.TypeClass.IS_GRAPH:
			case Type.TypeClass.IS_OBJECT:
			case Type.TypeClass.IS_INTERNAL_CLASS_OBJECT:
			case Type.TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT:
			case Type.TypeClass.IS_NODE:
			case Type.TypeClass.IS_EDGE:
			case Type.TypeClass.IS_SET:
			case Type.TypeClass.IS_MAP:
			case Type.TypeClass.IS_ARRAY:
			case Type.TypeClass.IS_DEQUE:
			case Type.TypeClass.IS_MATCH:
			case Type.TypeClass.IS_DEFINED_MATCH:
				if(constant.Value == null)
					return "null";
				goto default; //$FALL-THROUGH$
			default:
				throw new System.NotSupportedException("unsupported type");
			}
		}

		protected internal static string GetInitializationValue(Type type)
		{
			if(type is ByteType || type is ShortType || type is IntType
					|| type is EnumType || type is DoubleType)
				return "0";
			else if(type is FloatType)
				return "0f";
			else if(type is LongType)
				return "0L";
			else if(type is BooleanType)
				return "false";
			else
				return "null";
		}

		protected internal virtual string GetTypeNameForCast(Cast cast)
		{
			Type type = cast.Type;
			switch(type.Classify())
			{
			case Type.TypeClass.IS_STRING:
				return "string";
			case Type.TypeClass.IS_BYTE:
				return "sbyte";
			case Type.TypeClass.IS_SHORT:
				return "short";
			case Type.TypeClass.IS_INTEGER:
				return "int";
			case Type.TypeClass.IS_LONG:
				return "long";
			case Type.TypeClass.IS_FLOAT:
				return "float";
			case Type.TypeClass.IS_DOUBLE:
				return "double";
			case Type.TypeClass.IS_BOOLEAN:
				return "bool";
			case Type.TypeClass.IS_OBJECT:
				return "object";
			case Type.TypeClass.IS_GRAPH:
				return "GRGEN_LIBGR.IGraph";
			case Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT:
				return FormatType(cast.Type);
			case Type.TypeClass.IS_INTERNAL_CLASS_OBJECT:
				return FormatType(cast.Type);
			case Type.TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT:
				return FormatType(cast.Type);
			case Type.TypeClass.IS_NODE:
				return FormatType(cast.Type);
			case Type.TypeClass.IS_EDGE:
				return FormatType(cast.Type);
			case Type.TypeClass.IS_SET:
			case Type.TypeClass.IS_MAP:
			case Type.TypeClass.IS_ARRAY:
			case Type.TypeClass.IS_DEQUE:
				if(cast.Type.Classify() == Type.TypeClass.IS_SET)
				{
					// cast to set<Edge> or set<UEdge> from set<AEdge> allowed at compile time, requires check at runtime for directedness
					if(((SetType)cast.Type).ValueType.Ident.ToString().Equals("Edge"))
						return "directed set";
					else if(((SetType)cast.Type).ValueType.Ident.ToString().Equals("UEdge"))
						return "undirected set";
				}
				return "object"; // besides, only the null type can/will be casted into a container type, so the most specific base type is sufficient, which is object
			default:
				throw new System.NotSupportedException(
						"This is either a forbidden cast, which should have been " +
								"rejected on building the IR, or an allowed cast, which " +
								"should have been processed by the above code.");
			}
		}

		protected internal virtual string GetTypeNameForTempVarDecl(Type type)
		{
			switch(type.Classify())
			{
			case Type.TypeClass.IS_BOOLEAN:
				return "bool";
			case Type.TypeClass.IS_BYTE:
				return "sbyte";
			case Type.TypeClass.IS_SHORT:
				return "short";
			case Type.TypeClass.IS_INTEGER:
				return "int";
			case Type.TypeClass.IS_LONG:
				return "long";
			case Type.TypeClass.IS_FLOAT:
				return "float";
			case Type.TypeClass.IS_DOUBLE:
				return "double";
			case Type.TypeClass.IS_STRING:
				return "string";
			case Type.TypeClass.IS_OBJECT:
			case Type.TypeClass.IS_UNKNOWN:
				return "object";
			case Type.TypeClass.IS_GRAPH:
				return "GRGEN_LIBGR.IGraph";
			case Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT:
				return "GRGEN_MODEL." + type.Ident;
			case Type.TypeClass.IS_INTERNAL_CLASS_OBJECT:
				return FormatElementInterfaceRef(type);
			case Type.TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT:
				return FormatElementInterfaceRef(type);
			case Type.TypeClass.IS_NODE:
				return FormatElementInterfaceRef(type);
			case Type.TypeClass.IS_EDGE:
				return FormatElementInterfaceRef(type);
			default:
				throw new ArgumentException();
			}
		}

		protected internal static string EscapeBackslashAndDoubleQuotes(string input)
		{
			return input.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		protected internal abstract void GenQualAccess(SourceBuilder sb, Qualification qual, object modifyGenerationState);

		protected internal abstract void GenMemberAccess(SourceBuilder sb, Entity member);

		protected internal static void AddAnnotations(SourceBuilder sb, Identifiable ident, string targetName)
		{
			foreach(string annotationKey in ident.Annotations.KeySet())
			{
				string annotationValue = ident.Annotations.Get(annotationKey).ToString();
				sb.AppendFront(targetName + ".annotations.Add(\"" + annotationKey + "\", \"" + annotationValue + "\");\n");
			}
		}

		protected internal static void ForceNotConstant(ICollection<EvalStatement> statements)
		{
			NeededEntities needs = new NeededEntities(Needs.CONTAINER_EXPRS);
			foreach(EvalStatement eval in statements)
				eval.CollectNeededEntities(needs);
			ForceNotConstant(needs);
		}

		protected internal static void ForceNotConstant(NeededEntities needs)
		{
			// todo: more fine-grained never assigned, the important thing is that the constant constructor is temporary, not assigned to a variable
			foreach(Expression containerExpr in needs.containerExprs)
			{
				if(containerExpr is MapInit)
				{
					MapInit mapInit = (MapInit)containerExpr;
					mapInit.ForceNotConstant();
				}
				else if(containerExpr is SetInit)
				{
					SetInit setInit = (SetInit)containerExpr;
					setInit.ForceNotConstant();
				}
				else if(containerExpr is ArrayInit)
				{
					ArrayInit arrayInit = (ArrayInit)containerExpr;
					arrayInit.ForceNotConstant();
				}
				else if(containerExpr is DequeInit)
				{
					DequeInit dequeInit = (DequeInit)containerExpr;
					dequeInit.ForceNotConstant();
				}
			}
		}

		protected internal virtual void GenLocalContainersEvals(SourceBuilder sb, ICollection<EvalStatement> evals,
				IList<string> staticInitializers, string pathPrefixForElements,
				Dictionary<Entity, string> alreadyDefinedEntityToName)
		{
			NeededEntities needs = new NeededEntities(Needs.CONTAINER_EXPRS);
			foreach(EvalStatement eval in evals)
				eval.CollectNeededEntities(needs);
			GenLocalContainers(sb, needs, staticInitializers, false);
		}

		protected internal virtual void GenLocalContainers(SourceBuilder sb, NeededEntities needs, IList<string> staticInitializers,
				bool neverAssigned)
		{
			// todo: more fine-grained never assigned, the important thing is that the constant constructor is temporary, not assigned to a variable
			sb.Append("\n");
			foreach(Expression containerExpr in needs.containerExprs)
			{
				if(containerExpr is MapInit)
				{
					MapInit mapInit = (MapInit)containerExpr;
					if(!neverAssigned)
						mapInit.ForceNotConstant();
					GenLocalMap(sb, mapInit, staticInitializers);
				}
				else if(containerExpr is SetInit)
				{
					SetInit setInit = (SetInit)containerExpr;
					if(!neverAssigned)
						setInit.ForceNotConstant();
					GenLocalSet(sb, setInit, staticInitializers);
				}
				else if(containerExpr is ArrayInit)
				{
					ArrayInit arrayInit = (ArrayInit)containerExpr;
					if(!neverAssigned)
						arrayInit.ForceNotConstant();
					GenLocalArray(sb, arrayInit, staticInitializers);
				}
				else if(containerExpr is DequeInit)
				{
					DequeInit dequeInit = (DequeInit)containerExpr;
					if(!neverAssigned)
						dequeInit.ForceNotConstant();
					GenLocalDeque(sb, dequeInit, staticInitializers);
				}
				else if(containerExpr is InternalObjectInit)
				{
					InternalObjectInit internalObjectInit = (InternalObjectInit)containerExpr;
					GenLocalInternalObjectAttributeInitializer(sb, internalObjectInit, staticInitializers);
				}
			}
		}

		protected internal virtual void GenLocalMap(SourceBuilder sb, MapInit mapInit, IList<string> staticInitializers)
		{
			string mapName = mapInit.AnonymousMapName;
			string attrType = FormatAttributeType(mapInit.Type);
			if(mapInit.IsConstant())
			{
				sb.AppendFront("public static readonly " + attrType + " " + mapName + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + mapName);
				sb.AppendFront("static void init_" + mapName + "() {\n");
				sb.Indent();
				foreach(ExpressionPair item in mapInit.MapItems)
				{
					sb.AppendFront("");
					sb.Append(mapName);
					sb.Append("[");
					GenExpression(sb, item.KeyExpr, null);
					sb.Append("] = ");
					GenExpression(sb, item.ValueExpr, null);
					sb.Append(";\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public static " + attrType + " fill_" + mapName + "(");
				int itemCounter = 0;
				bool first = true;
				foreach(ExpressionPair item in mapInit.MapItems)
				{
					string itemKeyType = FormatType(item.KeyExpr.Type);
					string itemValueType = FormatType(item.ValueExpr.Type);
					if(first)
					{
						sb.Append(itemKeyType + " itemkey" + itemCounter + ",");
						sb.Append(itemValueType + " itemvalue" + itemCounter);
						first = false;
					}
					else
					{
						sb.Append(", " + itemKeyType + " itemkey" + itemCounter + ",");
						sb.Append(itemValueType + " itemvalue" + itemCounter);
					}
					++itemCounter;
				}
				sb.Append(") {\n");
				sb.Indent();
				sb.AppendFront(attrType + " " + mapName + " = " +
						"new " + attrType + "();\n");

				int itemLength = mapInit.MapItems.Count;
				for(itemCounter = 0; itemCounter < itemLength; ++itemCounter)
				{
					sb.AppendFront(mapName);
					sb.Append("[" + "itemkey" + itemCounter + "] = itemvalue" + itemCounter + ";\n");
				}
				sb.AppendFront("return " + mapName + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		protected internal virtual void GenLocalSet(SourceBuilder sb, SetInit setInit, IList<string> staticInitializers)
		{
			string setName = setInit.AnonymousSetName;
			string attrType = FormatAttributeType(setInit.Type);
			if(setInit.IsConstant())
			{
				sb.AppendFront("public static readonly " + attrType + " " + setName + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + setName);
				sb.AppendFront("static void init_" + setName + "() {\n");
				sb.Indent();
				foreach(Expression item in setInit.SetItems)
				{
					sb.AppendFront(setName);
					sb.Append("[");
					GenExpression(sb, item, null);
					sb.Append("] = null;\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public static " + attrType + " fill_" + setName + "(");
				int itemCounter = 0;
				bool first = true;
				foreach(Expression item in setInit.SetItems)
				{
					string itemType = FormatType(item.Type);
					if(first)
					{
						sb.Append(itemType + " item" + itemCounter);
						first = false;
					}
					else
						sb.Append(", " + itemType + " item" + itemCounter);
					++itemCounter;
				}
				sb.Append(") {\n");
				sb.Indent();
				sb.AppendFront(attrType + " " + setName + " = " +
						"new " + attrType + "();\n");

				int itemLength = setInit.SetItems.Count;
				for(itemCounter = 0; itemCounter < itemLength; ++itemCounter)
				{
					sb.AppendFront(setName);
					sb.Append("[" + "item" + itemCounter + "] = null;\n");
				}
				sb.AppendFront("return " + setName + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		protected internal virtual void GenLocalArray(SourceBuilder sb, ArrayInit arrayInit, IList<string> staticInitializers)
		{
			string arrayName = arrayInit.AnonymousArrayName;
			string attrType = FormatAttributeType(arrayInit.Type);
			if(arrayInit.IsConstant())
			{
				sb.AppendFront("public static readonly " + attrType + " " + arrayName + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + arrayName);
				sb.AppendFront("static void init_" + arrayName + "() {\n");
				sb.Indent();
				foreach(Expression item in arrayInit.ArrayItems)
				{
					sb.AppendFront(arrayName);
					sb.Append(".Add(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public static " + attrType + " fill_" + arrayName + "(");
				int itemCounter = 0;
				bool first = true;
				foreach(Expression item in arrayInit.ArrayItems)
				{
					string itemType = FormatType(item.Type);
					if(first)
					{
						sb.Append(itemType + " item" + itemCounter);
						first = false;
					}
					else
						sb.Append(", " + itemType + " item" + itemCounter);
					++itemCounter;
				}
				sb.Append(") {\n");
				sb.Indent();
				sb.AppendFront(attrType + " " + arrayName + " = " +
						"new " + attrType + "();\n");

				int itemLength = arrayInit.ArrayItems.Count;
				for(itemCounter = 0; itemCounter < itemLength; ++itemCounter)
				{
					sb.AppendFront(arrayName);
					sb.Append(".Add(" + "item" + itemCounter + ");\n");
				}
				sb.AppendFront("return " + arrayName + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		protected internal virtual void GenLocalDeque(SourceBuilder sb, DequeInit dequeInit, IList<string> staticInitializers)
		{
			string dequeName = dequeInit.AnonymousDequeName;
			string attrType = FormatAttributeType(dequeInit.Type);
			if(dequeInit.IsConstant())
			{
				sb.AppendFront("public static readonly " + attrType + " " + dequeName + " = " +
						"new " + attrType + "();\n");
				staticInitializers.Add("init_" + dequeName);
				sb.AppendFront("static void init_" + dequeName + "() {\n");
				sb.Indent();
				foreach(Expression item in dequeInit.DequeItems)
				{
					sb.AppendFront("");
					sb.Append(dequeName);
					sb.Append(".Add(");
					GenExpression(sb, item, null);
					sb.Append(");\n");
				}
				sb.Unindent();
				sb.AppendFront("}\n");
			}
			else
			{
				sb.AppendFront("public static " + attrType + " fill_" + dequeName + "(");
				int itemCounter = 0;
				bool first = true;
				foreach(Expression item in dequeInit.DequeItems)
				{
					string itemType = FormatType(item.Type);
					if(first)
					{
						sb.Append(itemType + " item" + itemCounter);
						first = false;
					}
					else
						sb.Append(", " + itemType + " item" + itemCounter);
					++itemCounter;
				}
				sb.Append(") {\n");
				sb.Indent();
				sb.AppendFront(attrType + " " + dequeName + " = " +
						"new " + attrType + "();\n");

				int itemLength = dequeInit.DequeItems.Count;
				for(itemCounter = 0; itemCounter < itemLength; ++itemCounter)
				{
					sb.AppendFront(dequeName);
					sb.Append(".Enqueue(" + "item" + itemCounter + ");\n");
				}
				sb.AppendFront("return " + dequeName + ";\n");
				sb.Unindent();
				sb.AppendFront("}\n");
			}
		}

		protected internal virtual void GenLocalInternalObjectAttributeInitializer(SourceBuilder sb, InternalObjectInit internalObjectInit, IList<string> staticInitializers)
		{
			string internalObjectName = internalObjectInit.AnonymousInternalObjectInitName;
			Entity internalObject = new Entity(internalObjectName, new Ident(internalObjectName, Coords.Builtin), internalObjectInit.Type, false, true, 0);
			string attrType = FormatInheritanceClassRef(internalObjectInit.Type);

			string uniqueIdDeclIfObject = internalObjectInit.BaseInternalObjectType is InternalObjectType ? "long uniqueId" : "";
			sb.AppendFront("public static " + attrType + " fill_" + internalObjectName + "(" + uniqueIdDeclIfObject);
			int itemCounter = 0;
			bool first = internalObjectInit.BaseInternalObjectType is InternalObjectType ? false : true;
			foreach(Expression item in internalObjectInit.AttributeInitializationExpressions)
			{
				string itemType = FormatType(item.Type);
				if(first)
				{
					sb.Append(itemType + " item" + itemCounter);
					first = false;
				}
				else
					sb.Append(", " + itemType + " item" + itemCounter);
				++itemCounter;
			}
			sb.Append(") {\n");
			sb.Indent();

			// uniqueIdUsageIfObject has to be -1 in case of isUniqueClassDefined(), an assert could be added...
			string uniqueIdUsageIfObject = internalObjectInit.BaseInternalObjectType is InternalObjectType ? "uniqueId" : "";
			sb.AppendFront(attrType + " " + internalObjectName + " = " +
					"new " + attrType + "(" + uniqueIdUsageIfObject + ");\n");

			int itemLength = internalObjectInit.attributeInitializations.Count;
			for(itemCounter = 0; itemCounter < itemLength; ++itemCounter)
			{
				sb.AppendFront(FormatEntity(internalObject) + ".@"
						+ FormatIdentifiable(internalObjectInit.attributeInitializations[itemCounter].attribute));
				sb.Append(" = ");
				sb.Append("item" + itemCounter + ";\n");
			}
			sb.AppendFront("return " + internalObjectName + ";\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		protected internal static void GenCompareMethod(SourceBuilder sb, string typeName,
				string attributeOrMemberName, Type attributeOrMemberType, bool ascending)
		{
			if(ascending)
				sb.AppendFront("public override int Compare(" + typeName + " a, " + typeName + " b)\n");
			else
				sb.AppendFront("public override int Compare(" + typeName + " b, " + typeName + " a)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			if(attributeOrMemberType.Classify() == Type.TypeClass.IS_EXTERNAL_CLASS_OBJECT
					|| attributeOrMemberType.Classify() == Type.TypeClass.IS_OBJECT)
			{
				sb.AppendFront("if(AttributeTypeObjectCopierComparer.IsEqual(a.@" + attributeOrMemberName + ", b.@"
						+ attributeOrMemberName + ", new Dictionary<object, object>())) return 0;\n");
				sb.AppendFront("if(AttributeTypeObjectCopierComparer.IsLower(a.@" + attributeOrMemberName + ", b.@"
						+ attributeOrMemberName + ", new Dictionary<object, object>())) return -1;\n");
				sb.AppendFront("return 1;\n");
			}
			else if(attributeOrMemberType is StringType)
				sb.AppendFront("return StringComparer.InvariantCulture.Compare(a.@" + attributeOrMemberName + ", b.@"
						+ attributeOrMemberName + ");\n");
			else
				sb.AppendFront("return a.@" + attributeOrMemberName + ".CompareTo(b.@" + attributeOrMemberName + ");\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		protected internal static void GenerateArrayGroupBy(SourceBuilder sb, string arrayFunctionName, string matchInterfaceName,
				string attributeOrMemberName, string attributeOrMemberType)
		{
			sb.AppendFront("public static List<" + matchInterfaceName + "> " + arrayFunctionName
					+ "(List<" + matchInterfaceName + "> list)\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("Dictionary<" + attributeOrMemberType + ", List<" + matchInterfaceName + ">> seenValues "
					+ "= new Dictionary<" + attributeOrMemberType + ", List<" + matchInterfaceName + ">>();\n");
			sb.AppendFront("for(int pos = 0; pos < list.Count; ++pos)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(seenValues.ContainsKey(list[pos].@" + attributeOrMemberName + ")) {\n");
			sb.Indent();
			sb.AppendFront("seenValues[list[pos].@" + attributeOrMemberName + "].Add(list[pos]);\n");
			sb.Unindent();
			sb.AppendFront("} else {\n");
			sb.Indent();
			sb.AppendFront("List<" + matchInterfaceName + "> tempList = new List<" + matchInterfaceName + ">();\n");
			sb.AppendFront("tempList.Add(list[pos]);\n");
			sb.AppendFront("seenValues.Add(list[pos].@" + attributeOrMemberName + ", tempList);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("List<" + matchInterfaceName + "> newList = new List<" + matchInterfaceName + ">();\n");
			sb.AppendFront("foreach(List<" + matchInterfaceName + "> entry in seenValues.Values)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("newList.AddRange(entry);\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return newList;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		protected internal static void GenerateArrayKeepOneForEach(SourceBuilder sb, string arrayFunctionName, string matchInterfaceName,
				string attributeOrMemberName, string attributeOrMemberType)
		{
			sb.AppendFront("public static List<" + matchInterfaceName + "> " + arrayFunctionName
					+ "(List<" + matchInterfaceName + "> list)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("List<" + matchInterfaceName + "> newList = new List<" + matchInterfaceName + ">();\n");

			sb.AppendFront("Dictionary<" + attributeOrMemberType + ", GRGEN_LIBGR.SetValueType> alreadySeenMembers "
					+ "= new Dictionary<" + attributeOrMemberType + ", GRGEN_LIBGR.SetValueType>();\n");
			sb.AppendFront("foreach(" + matchInterfaceName + " element in list)\n");
			sb.AppendFront("{\n");
			sb.Indent();
			sb.AppendFront("if(!alreadySeenMembers.ContainsKey(element.@" + attributeOrMemberName + ")) {\n");
			sb.Indent();
			sb.AppendFront("newList.Add(element);\n");
			sb.AppendFront("alreadySeenMembers.Add(element.@" + attributeOrMemberName + ", null);\n");
			sb.Unindent();
			sb.AppendFront("}\n");
			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return newList;\n");
			sb.Unindent();
			sb.AppendFront("}\n");
		}

		protected internal virtual void GenerateArrayMap(ArrayMapExpr arrayMap, ExpressionGenerationState modifyGenerationState)
		{
			SourceBuilder sb = new SourceBuilder();
			sb.Indent().Indent();

			string arrayMapName = "ArrayMap_" + arrayMap.Id;

			ArrayType arrayInputTypeType = arrayMap.TargetTypeExact;
			string arrayInputType = FormatType(arrayInputTypeType);
			string elementInputType = FormatType(arrayInputTypeType.valueType);
			ArrayType arrayOutputTypeType = (ArrayType)arrayMap.Type;
			string arrayOutputType = FormatType(arrayOutputTypeType);
			string elementOutputType = FormatType(arrayOutputTypeType.valueType);

			string targetVarName = "target";
			string sourceVarName = "source";
			string resultVarName = "result";

			sb.AppendFront("static " + arrayOutputType + " " + arrayMapName + "(");
			sb.Append("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv");

			// collect all variables, create parameters - like for if/eval
			NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
			arrayMap.CollectNeededEntities(needs);

			sb.Append(", " + arrayInputType + " " + sourceVarName);

			foreach(Node node in needs.nodes)
			{
				sb.Append(", ");
				sb.Append(FormatType(node.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(node));
			}
			foreach(Edge edge in needs.edges)
			{
				sb.Append(", ");
				sb.Append(FormatType(edge.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(edge));
			}
			foreach(Variable var in needs.variables)
			{
				sb.Append(", ");
				sb.Append(FormatType(var.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(var));
			}

			if(modifyGenerationState.IsToBeParallelizedActionExisting())
				sb.Append(", int threadId");

			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			sb.AppendFront(arrayOutputType + " " + targetVarName + " = new " + arrayOutputType + "();\n");

			if(arrayMap.ArrayAccessVar != null)
			{
				string arrayAccessVarName = FormatEntity(arrayMap.ArrayAccessVar);
				sb.Append(arrayInputType + " " + arrayAccessVarName + " = " + sourceVarName + ";\n");
			}

			string indexVarName = arrayMap.IndexVar != null ? FormatEntity(arrayMap.IndexVar) : "index";
			sb.AppendFront("for(int " + indexVarName + " = 0; " + indexVarName + " < " + sourceVarName + ".Count; ++" + indexVarName + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			string elementVarName = FormatEntity(arrayMap.ElementVar);
			sb.AppendFront(elementInputType + " " + elementVarName + " = " + sourceVarName + "[" + indexVarName + "];\n");
			sb.AppendFront(elementOutputType + " " + resultVarName + " = ");
			GenExpression(sb, arrayMap.MappingExpr, modifyGenerationState);
			sb.Append(";\n");
			sb.AppendFront(targetVarName + ".Add(" + resultVarName + ");\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return " + targetVarName + ";\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			modifyGenerationState.PerElementMethodSourceBuilder.Append(sb.ToString());
		}

		protected internal virtual void GenerateArrayRemoveIf(ArrayRemoveIfExpr arrayRemoveIf, ExpressionGenerationState modifyGenerationState)
		{
			SourceBuilder sb = new SourceBuilder();
			sb.Indent().Indent();

			string arrayRemoveIfName = "ArrayRemoveIf_" + arrayRemoveIf.Id;

			ArrayType arrayTypeType = arrayRemoveIf.TargetTypeExact;
			string arrayType = FormatType(arrayTypeType);
			string elementType = FormatType(arrayTypeType.valueType);

			string targetVarName = "target";
			string sourceVarName = "source";

			sb.AppendFront("static " + arrayType + " " + arrayRemoveIfName + "(");
			sb.Append("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv");

			// collect all variables, create parameters - like for if/eval
			NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
			arrayRemoveIf.CollectNeededEntities(needs);

			sb.Append(", " + arrayType + " " + sourceVarName);

			foreach(Node node in needs.nodes)
			{
				sb.Append(", ");
				sb.Append(FormatType(node.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(node));
			}
			foreach(Edge edge in needs.edges)
			{
				sb.Append(", ");
				sb.Append(FormatType(edge.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(edge));
			}
			foreach(Variable var in needs.variables)
			{
				sb.Append(", ");
				sb.Append(FormatType(var.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(var));
			}

			if(modifyGenerationState.IsToBeParallelizedActionExisting())
				sb.Append(", int threadId");

			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			sb.AppendFront(arrayType + " " + targetVarName + " = new " + arrayType + "();\n");

			if(arrayRemoveIf.ArrayAccessVar != null)
			{
				string arrayAccessVarName = FormatEntity(arrayRemoveIf.ArrayAccessVar);
				sb.Append(arrayType + " " + arrayAccessVarName + " = " + sourceVarName + ";\n");
			}

			string indexVarName = arrayRemoveIf.IndexVar != null ? FormatEntity(arrayRemoveIf.IndexVar) : "index";
			sb.AppendFront("for(int " + indexVarName + " = 0; " + indexVarName + " < " + sourceVarName + ".Count; ++" + indexVarName + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			string elementVarName = FormatEntity(arrayRemoveIf.ElementVar);
			sb.AppendFront(elementType + " " + elementVarName + " = " + sourceVarName + "[" + indexVarName + "];\n");
			sb.Append("if(!(bool)(");
			GenExpression(sb, arrayRemoveIf.ConditionExpr, modifyGenerationState);
			sb.Append("))\n");
			sb.AppendFrontIndented(targetVarName + ".Add(" + sourceVarName + "[" + indexVarName + "]);\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return " + targetVarName + ";\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			modifyGenerationState.PerElementMethodSourceBuilder.Append(sb.ToString());
		}

		protected internal virtual void GenerateArrayMapStartWithAccumulateBy(ArrayMapStartWithAccumulateByExpr arrayMap, ExpressionGenerationState modifyGenerationState)
		{
			SourceBuilder sb = new SourceBuilder();
			sb.Indent().Indent();

			string arrayMapName = "ArrayMapStartWithAccumulateBy_" + arrayMap.Id;

			ArrayType arrayInputTypeType = arrayMap.TargetTypeExact;
			string arrayInputType = FormatType(arrayInputTypeType);
			string elementInputType = FormatType(arrayInputTypeType.valueType);
			ArrayType arrayOutputTypeType = (ArrayType)arrayMap.Type;
			string arrayOutputType = FormatType(arrayOutputTypeType);
			string elementOutputType = FormatType(arrayOutputTypeType.valueType);

			string targetVarName = "target";
			string sourceVarName = "source";
			string resultVarName = "result";

			sb.AppendFront("static " + arrayOutputType + " " + arrayMapName + "(");
			sb.Append("GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv");

			// collect all variables, create parameters - like for if/eval
			NeededEntities needs = new NeededEntities(Needs.NODES | Needs.EDGES | Needs.VARS | Needs.COMPUTATION_CONTEXT | Needs.LAMBDAS);
			arrayMap.CollectNeededEntities(needs);

			sb.Append(", " + arrayInputType + " " + sourceVarName);

			foreach(Node node in needs.nodes)
			{
				sb.Append(", ");
				sb.Append(FormatType(node.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(node));
			}
			foreach(Edge edge in needs.edges)
			{
				sb.Append(", ");
				sb.Append(FormatType(edge.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(edge));
			}
			foreach(Variable var in needs.variables)
			{
				sb.Append(", ");
				sb.Append(FormatType(var.Type));
				sb.Append(" ");
				sb.Append(FormatEntity(var));
			}

			if(modifyGenerationState.IsToBeParallelizedActionExisting())
				sb.Append(", int threadId");

			sb.Append(")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
			sb.AppendFront(arrayOutputType + " " + targetVarName + " = new " + arrayOutputType + "();\n");

			if(arrayMap.InitArrayAccessVar != null)
			{
				string initArrayAccessVarName = FormatEntity(arrayMap.InitArrayAccessVar);
				sb.Append(arrayInputType + " " + initArrayAccessVarName + " = " + sourceVarName + ";\n");
			}

			string previousAccumulationAccessVarName = FormatEntity(arrayMap.PreviousAccumulationAccessVar);
			sb.AppendFront(elementOutputType + " " + previousAccumulationAccessVarName + " = ");
			GenExpression(sb, arrayMap.InitExpr, modifyGenerationState);
			sb.Append(";\n");

			if(arrayMap.ArrayAccessVar != null)
			{
				string arrayAccessVarName = FormatEntity(arrayMap.ArrayAccessVar);
				sb.Append(arrayInputType + " " + arrayAccessVarName + " = " + sourceVarName + ";\n");
			}

			string indexVarName = arrayMap.IndexVar != null ? FormatEntity(arrayMap.IndexVar) : "index";
			sb.AppendFront("for(int " + indexVarName + " = 0; " + indexVarName + " < " + sourceVarName + ".Count; ++" + indexVarName + ")\n");
			sb.AppendFront("{\n");
			sb.Indent();

			string elementVarName = FormatEntity(arrayMap.ElementVar);
			sb.AppendFront(elementInputType + " " + elementVarName + " = " + sourceVarName + "[" + indexVarName + "];\n");
			sb.AppendFront(elementOutputType + " " + resultVarName + " = ");
			GenExpression(sb, arrayMap.MappingExpr, modifyGenerationState);
			sb.Append(";\n");
			sb.AppendFront(targetVarName + ".Add(" + resultVarName + ");\n");

			sb.AppendFront(previousAccumulationAccessVarName + " = " + resultVarName + ";\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			sb.AppendFront("return " + targetVarName + ";\n");

			sb.Unindent();
			sb.AppendFront("}\n");

			modifyGenerationState.PerElementMethodSourceBuilder.Append(sb.ToString());
		}

		/* (unary and binary) operator symbols (of the C-language) */
		// The first two shift operations are signed shifts, the second right shift is unsigned.
		private static string GetOperatorSymbol(OperatorCode opCode)
		{
			switch(opCode)
			{
			case de.unika.ipd.grgen.ir.expr.OperatorCode.LOG_OR:
				return "||";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.LOG_AND:
				return "&&";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_OR:
				return "|";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_XOR:
				return "^";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_AND:
				return "&";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.EQ:
				return "==";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.NE:
				return "!=";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.LT:
				return "<";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.LE:
				return "<=";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.GT:
				return ">";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.GE:
				return ">=";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.SHL:
				return "<<";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.SHR:
				return ">>";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_SHR:
				return ">>";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.ADD:
				return "+";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.SUB:
				return "-";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.MUL:
				return "*";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.DIV:
				return "/";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.MOD:
				return "%";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.LOG_NOT:
				return "!";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.BIT_NOT:
				return "~";
			case de.unika.ipd.grgen.ir.expr.OperatorCode.NEG:
				return "-";
			default:
				throw new Exception("internal failure");
			}
		}

		protected internal static bool AccessViaVariable(ModifyGenerationStateConst state, Entity elem, Entity attr)
		{
			if(elem is GraphEntity)
			{
				ISet<Entity> forcedAttrs;
				state.ForceAttributeToVar.TryGetValue((GraphEntity)elem, out forcedAttrs);
				return forcedAttrs != null && forcedAttrs.Contains(attr);
			}
			else
				return false;
		}

		protected internal static bool AccessViaInterface(ModifyGenerationStateConst state, Entity elem)
		{
			if(elem is GraphEntity)
				return state.AccessViaInterface.Contains((GraphEntity)elem);
			else
				return false;
		}

		protected internal string nodeTypePrefix;
		protected internal string edgeTypePrefix;
		protected internal string objectTypePrefix;
		protected internal string transientObjectTypePrefix;
	}

}
