/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence expression generator contains code to generate a sequence expression,
    /// it is in use by the sequence computation generator and the sequence generator,
    /// and also called by the sequence generator helper.
    /// </summary>
    public class SequenceExpressionGenerator
    {
        readonly IGraphModel model;

        readonly SequenceCheckingEnvironment env;

        readonly SequenceGeneratorHelper seqHelper;

        public readonly SourceBuilder perElementMethodSource; // for generation of lambda expression execution functions of the array per element methods / per element match filters

        readonly bool fireDebugEvents;


        public SequenceExpressionGenerator(IGraphModel model, SequenceCheckingEnvironment env, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.model = model;
            this.env = env;
            this.seqHelper = seqHelper;
            this.perElementMethodSource = new SourceBuilder();
            this.fireDebugEvents = fireDebugEvents;
        }

        // source is needed for a method call chain or expressions that require temporary variables, 
        // to emit the state changing computation methods or the temporary variable declarations (not the assignement, needs to be computed from inside the expression)
        // before returning the final expression method call ready to be emitted
        public string GetSequenceExpression(SequenceExpression expr, SourceBuilder source)
        {
            switch(expr.SequenceExpressionType)
            {
            case SequenceExpressionType.Conditional:
                return GetSequenceExpressionConditional((SequenceExpressionConditional)expr, source);
            case SequenceExpressionType.Except:
                return GetSequenceExpressionExcept((SequenceExpressionExcept)expr, source);
            case SequenceExpressionType.LazyOr:
                return GetSequenceExpressionLazyOr((SequenceExpressionLazyOr)expr, source);
            case SequenceExpressionType.LazyAnd:
                return GetSequenceExpressionLazyAnd((SequenceExpressionLazyAnd)expr, source);
            case SequenceExpressionType.StrictOr:
                return GetSequenceExpressionStrictOr((SequenceExpressionStrictOr)expr, source);
            case SequenceExpressionType.StrictXor:
                return GetSequenceExpressionStrictXor((SequenceExpressionStrictXor)expr, source);
            case SequenceExpressionType.StrictAnd:
                return GetSequenceExpressionStrictAnd((SequenceExpressionStrictAnd)expr, source);
            case SequenceExpressionType.Equal:
                return GetSequenceExpressionEqual((SequenceExpressionEqual)expr, source);
            case SequenceExpressionType.NotEqual:
                return GetSequenceExpressionNotEqual((SequenceExpressionNotEqual)expr, source);
            case SequenceExpressionType.StructuralEqual:
                return GetSequenceExpressionStructuralEqual((SequenceExpressionStructuralEqual)expr, source);
            case SequenceExpressionType.Lower:
                return GetSequenceExpressionLower((SequenceExpressionLower)expr, source);
            case SequenceExpressionType.Greater:
                return GetSequenceExpressionGreater((SequenceExpressionGreater)expr, source);
            case SequenceExpressionType.LowerEqual:
                return GetSequenceExpressionLowerEqual((SequenceExpressionLowerEqual)expr, source);
            case SequenceExpressionType.GreaterEqual:
                return GetSequenceExpressionGreaterEqual((SequenceExpressionGreaterEqual)expr, source);
            case SequenceExpressionType.ShiftLeft:
                return GetSequenceExpressionShiftLeft((SequenceExpressionShiftLeft)expr, source);
            case SequenceExpressionType.ShiftRight:
                return GetSequenceExpressionShiftRight((SequenceExpressionShiftRight)expr, source);
            case SequenceExpressionType.ShiftRightUnsigned:
                return GetSequenceExpressionShiftRightUnsigned((SequenceExpressionShiftRightUnsigned)expr, source);
            case SequenceExpressionType.Plus:
                return GetSequenceExpressionPlus((SequenceExpressionPlus)expr, source);
            case SequenceExpressionType.Minus:
                return GetSequenceExpressionMinus((SequenceExpressionMinus)expr, source);
            case SequenceExpressionType.Mul:
                return GetSequenceExpressionMul((SequenceExpressionMul)expr, source);
            case SequenceExpressionType.Div:
                return GetSequenceExpressionDiv((SequenceExpressionDiv)expr, source);
            case SequenceExpressionType.Mod:
                return GetSequenceExpressionMod((SequenceExpressionMod)expr, source);
            case SequenceExpressionType.Not:
                return GetSequenceExpressionNot((SequenceExpressionNot)expr, source);
            case SequenceExpressionType.UnaryPlus:
                return GetSequenceExpressionUnaryPlus((SequenceExpressionUnaryPlus)expr, source);
            case SequenceExpressionType.UnaryMinus:
                return GetSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)expr, source);
            case SequenceExpressionType.BitwiseComplement:
                return GetSequenceExpressionBitwiseComplement((SequenceExpressionBitwiseComplement)expr, source);
            case SequenceExpressionType.Cast:
                return GetSequenceExpressionCast((SequenceExpressionCast)expr, source);
            case SequenceExpressionType.Def:
                return GetSequenceExpressionDef((SequenceExpressionDef)expr, source);
            case SequenceExpressionType.IsVisited:
                return GetSequenceExpressionIsVisited((SequenceExpressionIsVisited)expr, source);
            case SequenceExpressionType.Now:
                return GetSequenceExpressionNow((SequenceExpressionNow)expr, source);
            case SequenceExpressionType.Random:
                return GetSequenceExpressionRandom((SequenceExpressionRandom)expr, source);
            case SequenceExpressionType.Typeof:
                return GetSequenceExpressionTypeof((SequenceExpressionTypeof)expr, source);
            case SequenceExpressionType.ExistsFile:
                return GetSequenceExpressionExistsFile((SequenceExpressionExistsFile)expr, source);
            case SequenceExpressionType.Import:
                return GetSequenceExpressionImport((SequenceExpressionImport)expr, source);
            case SequenceExpressionType.Copy:
                return GetSequenceExpressionCopyClone((SequenceExpressionCopy)expr, source);
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                if(((SequenceExpressionAttributeOrMatchAccess)expr).AttributeAccess != null)
                    return GetSequenceExpressionAttributeAccess(((SequenceExpressionAttributeOrMatchAccess)expr).AttributeAccess, source);
                else if(((SequenceExpressionAttributeOrMatchAccess)expr).MatchAccess != null)
                    return GetSequenceExpressionElementOfMatch(((SequenceExpressionAttributeOrMatchAccess)expr).MatchAccess, source);
                else
                    return GetSequenceExpressionAttributeAccessOrElementOfMatch((SequenceExpressionAttributeOrMatchAccess)expr, source);
            case SequenceExpressionType.Constant:
                return GetSequenceExpressionConstant((SequenceExpressionConstant)expr, source);
            case SequenceExpressionType.Variable:
                return GetSequenceExpressionVariable((SequenceExpressionVariable)expr, source);
            case SequenceExpressionType.New:
                return GetSequenceExpressionNew((SequenceExpressionNew)expr, source);
            case SequenceExpressionType.RuleQuery:
                return GetSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)expr, source);
            case SequenceExpressionType.MultiRuleQuery:
                return GetSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)expr, source);
            case SequenceExpressionType.MappingClause:
                return GetSequenceExpressionMappingClause((SequenceExpressionMappingClause)expr, source);
            case SequenceExpressionType.Scan:
                return GetSequenceExpressionScan((SequenceExpressionScan)expr, source);
            case SequenceExpressionType.TryScan:
                return GetSequenceExpressionTryScan((SequenceExpressionTryScan)expr, source);
            case SequenceExpressionType.FunctionCall:
                return GetSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)expr, source);
            case SequenceExpressionType.FunctionMethodCall:
                return GetSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)expr, source);

            // graph expressions
            case SequenceExpressionType.EqualsAny:
                return GetSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)expr, source);
            case SequenceExpressionType.GetEquivalent:
                return GetSequenceExpressionGetEquivalent((SequenceExpressionGetEquivalent)expr, source);
            case SequenceExpressionType.Canonize:
                return GetSequenceExpressionCanonize((SequenceExpressionCanonize)expr, source);
            case SequenceExpressionType.Nameof:
                return GetSequenceExpressionNameof((SequenceExpressionNameof)expr, source);
            case SequenceExpressionType.Uniqueof:
                return GetSequenceExpressionUniqueof((SequenceExpressionUniqueof)expr, source);
            case SequenceExpressionType.This:
                return GetSequenceExpressionThis((SequenceExpressionThis)expr, source);
            case SequenceExpressionType.ElementFromGraph:
                return GetSequenceExpressionElementFromGraph((SequenceExpressionElementFromGraph)expr, source);
            case SequenceExpressionType.NodeByName:
                return GetSequenceExpressionNodeByName((SequenceExpressionNodeByName)expr, source);
            case SequenceExpressionType.EdgeByName:
                return GetSequenceExpressionEdgeByName((SequenceExpressionEdgeByName)expr, source);
            case SequenceExpressionType.NodeByUnique:
                return GetSequenceExpressionNodeByUnique((SequenceExpressionNodeByUnique)expr, source);
            case SequenceExpressionType.EdgeByUnique:
                return GetSequenceExpressionEdgeByUnique((SequenceExpressionEdgeByUnique)expr, source);
            case SequenceExpressionType.Source:
                return GetSequenceExpressionSource((SequenceExpressionSource)expr, source);
            case SequenceExpressionType.Target:
                return GetSequenceExpressionTarget((SequenceExpressionTarget)expr, source);
            case SequenceExpressionType.Opposite:
                return GetSequenceExpressionOpposite((SequenceExpressionOpposite)expr, source);
            case SequenceExpressionType.Empty:
                return GetSequenceExpressionEmpty((SequenceExpressionEmpty)expr, source);
            case SequenceExpressionType.Size:
                return GetSequenceExpressionSize((SequenceExpressionSize)expr, source);
            case SequenceExpressionType.Nodes:
                return GetSequenceExpressionNodes((SequenceExpressionNodes)expr, source);
            case SequenceExpressionType.Edges:
                return GetSequenceExpressionEdges((SequenceExpressionEdges)expr, source);
            case SequenceExpressionType.CountNodes:
                return GetSequenceExpressionCountNodes((SequenceExpressionCountNodes)expr, source);
            case SequenceExpressionType.CountEdges:
                return GetSequenceExpressionCountEdges((SequenceExpressionCountEdges)expr, source);
            case SequenceExpressionType.AdjacentNodes:
            case SequenceExpressionType.AdjacentNodesViaIncoming:
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
            case SequenceExpressionType.IncidentEdges:
            case SequenceExpressionType.IncomingEdges:
            case SequenceExpressionType.OutgoingEdges:
                return GetSequenceExpressionAdjacentIncident((SequenceExpressionAdjacentIncident)expr, source);
            case SequenceExpressionType.CountAdjacentNodes:
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
            case SequenceExpressionType.CountIncidentEdges:
            case SequenceExpressionType.CountIncomingEdges:
            case SequenceExpressionType.CountOutgoingEdges:
                return GetSequenceExpressionCountAdjacentIncident((SequenceExpressionCountAdjacentIncident)expr, source);
            case SequenceExpressionType.ReachableNodes:
            case SequenceExpressionType.ReachableNodesViaIncoming:
            case SequenceExpressionType.ReachableNodesViaOutgoing:
            case SequenceExpressionType.ReachableEdges:
            case SequenceExpressionType.ReachableEdgesViaIncoming:
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                return GetSequenceExpressionReachable((SequenceExpressionReachable)expr, source);
            case SequenceExpressionType.CountReachableNodes:
            case SequenceExpressionType.CountReachableNodesViaIncoming:
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
            case SequenceExpressionType.CountReachableEdges:
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                return GetSequenceExpressionCountReachable((SequenceExpressionCountReachable)expr, source);
            case SequenceExpressionType.BoundedReachableNodes:
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.BoundedReachableEdges:
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                return GetSequenceExpressionBoundedReachable((SequenceExpressionBoundedReachable)expr, source);
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                return GetSequenceExpressionBoundedReachableWithRemainingDepth((SequenceExpressionBoundedReachableWithRemainingDepth)expr, source);
            case SequenceExpressionType.CountBoundedReachableNodes:
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.CountBoundedReachableEdges:
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                return GetSequenceExpressionCountBoundedReachable((SequenceExpressionCountBoundedReachable)expr, source);
            case SequenceExpressionType.IsAdjacentNodes:
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
            case SequenceExpressionType.IsIncidentEdges:
            case SequenceExpressionType.IsIncomingEdges:
            case SequenceExpressionType.IsOutgoingEdges:
                return GetSequenceExpressionIsAdjacentIncident((SequenceExpressionIsAdjacentIncident)expr, source);
            case SequenceExpressionType.IsReachableNodes:
            case SequenceExpressionType.IsReachableNodesViaIncoming:
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
            case SequenceExpressionType.IsReachableEdges:
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                return GetSequenceExpressionIsReachable((SequenceExpressionIsReachable)expr, source);
            case SequenceExpressionType.IsBoundedReachableNodes:
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.IsBoundedReachableEdges:
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                return GetSequenceExpressionIsBoundedReachable((SequenceExpressionIsBoundedReachable)expr, source);
            case SequenceExpressionType.InducedSubgraph:
                return GetSequenceExpressionInducedSubgraph((SequenceExpressionInducedSubgraph)expr, source);
            case SequenceExpressionType.DefinedSubgraph:
                return GetSequenceExpressionDefinedSubgraph((SequenceExpressionDefinedSubgraph)expr, source);

            // container expressions
            case SequenceExpressionType.InContainerOrString:
                return GetSequenceExpressionInContainerOrString((SequenceExpressionInContainerOrString)expr, source);
            case SequenceExpressionType.ContainerSize:
                return GetSequenceExpressionContainerSize((SequenceExpressionContainerSize)expr, source);
            case SequenceExpressionType.ContainerEmpty:
                return GetSequenceExpressionContainerEmpty((SequenceExpressionContainerEmpty)expr, source);
            case SequenceExpressionType.ContainerAccess:
                return GetSequenceExpressionContainerAccess((SequenceExpressionContainerAccess)expr, source);
            case SequenceExpressionType.ContainerPeek:
                return GetSequenceExpressionContainerPeek((SequenceExpressionContainerPeek)expr, source);
            case SequenceExpressionType.SetCopyConstructor:
                return GetSequenceExpressionSetCopyConstructor((SequenceExpressionSetCopyConstructor)expr, source);
            case SequenceExpressionType.MapCopyConstructor:
                return GetSequenceExpressionMapCopyConstructor((SequenceExpressionMapCopyConstructor)expr, source);
            case SequenceExpressionType.ArrayCopyConstructor:
                return GetSequenceExpressionArrayCopyConstructor((SequenceExpressionArrayCopyConstructor)expr, source);
            case SequenceExpressionType.DequeCopyConstructor:
                return GetSequenceExpressionDequeCopyConstructor((SequenceExpressionDequeCopyConstructor)expr, source);
            case SequenceExpressionType.ContainerAsArray:
                return GetSequenceExpressionContainerAsArray((SequenceExpressionContainerAsArray)expr, source);
            case SequenceExpressionType.MapDomain:
                return GetSequenceExpressionMapDomain((SequenceExpressionMapDomain)expr, source);
            case SequenceExpressionType.MapRange:
                return GetSequenceExpressionMapRange((SequenceExpressionMapRange)expr, source);
            case SequenceExpressionType.SetConstructor:
            case SequenceExpressionType.ArrayConstructor:
            case SequenceExpressionType.DequeConstructor:
                return GetSequenceExpressionContainerConstructor((SequenceExpressionContainerConstructor)expr, source);
            case SequenceExpressionType.MapConstructor:
                return GetSequenceExpressionMapConstructor((SequenceExpressionMapConstructor)expr, source);
            case SequenceExpressionType.ArrayOrDequeOrStringIndexOf:
                return GetSequenceExpressionArrayOrDequeOrStringIndexOf((SequenceExpressionArrayOrDequeOrStringIndexOf)expr, source);
            case SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf:
                return GetSequenceExpressionArrayOrDequeOrStringLastIndexOf((SequenceExpressionArrayOrDequeOrStringLastIndexOf)expr, source);
            case SequenceExpressionType.ArrayIndexOfOrdered:
                return GetSequenceExpressionArrayIndexOfOrdered((SequenceExpressionArrayIndexOfOrdered)expr, source);
            case SequenceExpressionType.ArraySum:
                return GetSequenceExpressionArraySum((SequenceExpressionArraySum)expr, source);
            case SequenceExpressionType.ArrayProd:
                return GetSequenceExpressionArrayProd((SequenceExpressionArrayProd)expr, source);
            case SequenceExpressionType.ArrayMin:
                return GetSequenceExpressionArrayMin((SequenceExpressionArrayMin)expr, source);
            case SequenceExpressionType.ArrayMax:
                return GetSequenceExpressionArrayMax((SequenceExpressionArrayMax)expr, source);
            case SequenceExpressionType.ArrayAvg:
                return GetSequenceExpressionArrayAvg((SequenceExpressionArrayAvg)expr, source);
            case SequenceExpressionType.ArrayMed:
                return GetSequenceExpressionArrayMed((SequenceExpressionArrayMed)expr, source);
            case SequenceExpressionType.ArrayMedUnordered:
                return GetSequenceExpressionArrayMedUnordered((SequenceExpressionArrayMedUnordered)expr, source);
            case SequenceExpressionType.ArrayVar:
                return GetSequenceExpressionArrayVar((SequenceExpressionArrayVar)expr, source);
            case SequenceExpressionType.ArrayDev:
                return GetSequenceExpressionArrayDev((SequenceExpressionArrayDev)expr, source);
            case SequenceExpressionType.ArrayAnd:
                return GetSequenceExpressionArrayAnd((SequenceExpressionArrayAnd)expr, source);
            case SequenceExpressionType.ArrayOr:
                return GetSequenceExpressionArrayOr((SequenceExpressionArrayOr)expr, source);
            case SequenceExpressionType.ArrayOrDequeAsSet:
                return GetSequenceExpressionArrayOrDequeAsSet((SequenceExpressionArrayOrDequeAsSet)expr, source);
            case SequenceExpressionType.ArrayAsMap:
                return GetSequenceExpressionArrayAsMap((SequenceExpressionArrayAsMap)expr, source);
            case SequenceExpressionType.ArrayAsDeque:
                return GetSequenceExpressionArrayAsDeque((SequenceExpressionArrayAsDeque)expr, source);
            case SequenceExpressionType.ArrayAsString:
                return GetSequenceExpressionArrayAsString((SequenceExpressionArrayAsString)expr, source);
            case SequenceExpressionType.ArraySubarray:
                return GetSequenceExpressionArraySubarray((SequenceExpressionArraySubarray)expr, source);
            case SequenceExpressionType.DequeSubdeque:
                return GetSequenceExpressionDequeSubdeque((SequenceExpressionDequeSubdeque)expr, source);
            case SequenceExpressionType.ArrayOrderAscending:
                return GetSequenceExpressionArrayOrderAscending((SequenceExpressionArrayOrderAscending)expr, source);
            case SequenceExpressionType.ArrayOrderDescending:
                return GetSequenceExpressionArrayOrderDescending((SequenceExpressionArrayOrderDescending)expr, source);
            case SequenceExpressionType.ArrayGroup:
                return GetSequenceExpressionArrayGroup((SequenceExpressionArrayGroup)expr, source);
            case SequenceExpressionType.ArrayKeepOneForEach:
                return GetSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)expr, source);
            case SequenceExpressionType.ArrayReverse:
                return GetSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)expr, source);
            case SequenceExpressionType.ArrayShuffle:
                return GetSequenceExpressionArrayShuffle((SequenceExpressionArrayShuffle)expr, source);
            case SequenceExpressionType.ArrayExtract:
                return GetSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)expr, source);
            case SequenceExpressionType.ArrayMap:
                return GetSequenceExpressionArrayMap((SequenceExpressionArrayMap)expr, source);
            case SequenceExpressionType.ArrayRemoveIf:
                return GetSequenceExpressionArrayRemoveIf((SequenceExpressionArrayRemoveIf)expr, source);
            case SequenceExpressionType.ArrayMapStartWithAccumulateBy:
                return GetSequenceExpressionArrayMapStartWithAccumulateBy((SequenceExpressionArrayMapStartWithAccumulateBy)expr, source);
            case SequenceExpressionType.ArrayOrderAscendingBy:
                return GetSequenceExpressionArrayOrderAscendingBy((SequenceExpressionArrayOrderAscendingBy)expr, source);
            case SequenceExpressionType.ArrayOrderDescendingBy:
                return GetSequenceExpressionArrayOrderDescendingBy((SequenceExpressionArrayOrderDescendingBy)expr, source);
            case SequenceExpressionType.ArrayGroupBy:
                return GetSequenceExpressionArrayGroupBy((SequenceExpressionArrayGroupBy)expr, source);
            case SequenceExpressionType.ArrayKeepOneForEachBy:
                return GetSequenceExpressionArrayKeepOneForEachBy((SequenceExpressionArrayKeepOneForEachBy)expr, source);
            case SequenceExpressionType.ArrayIndexOfBy:
                return GetSequenceExpressionArrayIndexOfBy((SequenceExpressionArrayIndexOfBy)expr, source);
            case SequenceExpressionType.ArrayLastIndexOfBy:
                return GetSequenceExpressionArrayLastIndexOfBy((SequenceExpressionArrayLastIndexOfBy)expr, source);
            case SequenceExpressionType.ArrayIndexOfOrderedBy:
                return GetSequenceExpressionArrayIndexOfOrderedBy((SequenceExpressionArrayIndexOfOrderedBy)expr, source);
            case SequenceExpressionType.MatchClassConstructor:
                return GetSequenceExpressionMatchClassConstructor((SequenceExpressionMatchClassConstructor)expr, source);

            // string expressions
            case SequenceExpressionType.StringLength:
                return GetSequenceExpressionStringLength((SequenceExpressionStringLength)expr, source);
            case SequenceExpressionType.StringStartsWith:
                return GetSequenceExpressionStringStartsWith((SequenceExpressionStringStartsWith)expr, source);
            case SequenceExpressionType.StringEndsWith:
                return GetSequenceExpressionStringEndsWith((SequenceExpressionStringEndsWith)expr, source);
            case SequenceExpressionType.StringSubstring:
                return GetSequenceExpressionStringSubstring((SequenceExpressionStringSubstring)expr, source);
            case SequenceExpressionType.StringReplace:
                return GetSequenceExpressionStringReplace((SequenceExpressionStringReplace)expr, source);
            case SequenceExpressionType.StringToLower:
                return GetSequenceExpressionStringToLower((SequenceExpressionStringToLower)expr, source);
            case SequenceExpressionType.StringToUpper:
                return GetSequenceExpressionStringToUpper((SequenceExpressionStringToUpper)expr, source);
            case SequenceExpressionType.StringAsArray:
                return GetSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)expr, source);

            // numeric expressions
            case SequenceExpressionType.MathMin:
                return GetSequenceExpressionMathMin((SequenceExpressionMathMin)expr, source);
            case SequenceExpressionType.MathMax:
                return GetSequenceExpressionMathMax((SequenceExpressionMathMax)expr, source);
            case SequenceExpressionType.MathAbs:
                return GetSequenceExpressionMathAbs((SequenceExpressionMathAbs)expr, source);
            case SequenceExpressionType.MathCeil:
                return GetSequenceExpressionMathCeil((SequenceExpressionMathCeil)expr, source);
            case SequenceExpressionType.MathFloor:
                return GetSequenceExpressionMathFloor((SequenceExpressionMathFloor)expr, source);
            case SequenceExpressionType.MathRound:
                return GetSequenceExpressionMathRound((SequenceExpressionMathRound)expr, source);
            case SequenceExpressionType.MathTruncate:
                return GetSequenceExpressionMathTruncate((SequenceExpressionMathTruncate)expr, source);
            case SequenceExpressionType.MathSqr:
                return GetSequenceExpressionMathSqr((SequenceExpressionMathSqr)expr, source);
            case SequenceExpressionType.MathSqrt:
                return GetSequenceExpressionMathSqrt((SequenceExpressionMathSqrt)expr, source);
            case SequenceExpressionType.MathPow:
                return GetSequenceExpressionMathPow((SequenceExpressionMathPow)expr, source);
            case SequenceExpressionType.MathLog:
                return GetSequenceExpressionMathLog((SequenceExpressionMathLog)expr, source);
            case SequenceExpressionType.MathSgn:
                return GetSequenceExpressionMathSgn((SequenceExpressionMathSgn)expr, source);
            case SequenceExpressionType.MathSin:
                return GetSequenceExpressionMathSin((SequenceExpressionMathSin)expr, source);
            case SequenceExpressionType.MathCos:
                return GetSequenceExpressionMathCos((SequenceExpressionMathCos)expr, source);
            case SequenceExpressionType.MathTan:
                return GetSequenceExpressionMathTan((SequenceExpressionMathTan)expr, source);
            case SequenceExpressionType.MathArcSin:
                return GetSequenceExpressionMathArcSin((SequenceExpressionMathArcSin)expr, source);
            case SequenceExpressionType.MathArcCos:
                return GetSequenceExpressionMathArcCos((SequenceExpressionMathArcCos)expr, source);
            case SequenceExpressionType.MathArcTan:
                return GetSequenceExpressionMathArcTan((SequenceExpressionMathArcTan)expr, source);
            case SequenceExpressionType.MathPi:
                return GetSequenceExpressionMathPi((SequenceExpressionMathPi)expr, source);
            case SequenceExpressionType.MathE:
                return GetSequenceExpressionMathE((SequenceExpressionMathE)expr, source);
            case SequenceExpressionType.MathByteMin:
                return GetSequenceExpressionMathByteMin((SequenceExpressionMathByteMin)expr, source);
            case SequenceExpressionType.MathByteMax:
                return GetSequenceExpressionMathByteMax((SequenceExpressionMathByteMax)expr, source);
            case SequenceExpressionType.MathShortMin:
                return GetSequenceExpressionMathShortMin((SequenceExpressionMathShortMin)expr, source);
            case SequenceExpressionType.MathShortMax:
                return GetSequenceExpressionMathShortMax((SequenceExpressionMathShortMax)expr, source);
            case SequenceExpressionType.MathIntMin:
                return GetSequenceExpressionMathIntMin((SequenceExpressionMathIntMin)expr, source);
            case SequenceExpressionType.MathIntMax:
                return GetSequenceExpressionMathIntMax((SequenceExpressionMathIntMax)expr, source);
            case SequenceExpressionType.MathLongMin:
                return GetSequenceExpressionMathLongMin((SequenceExpressionMathLongMin)expr, source);
            case SequenceExpressionType.MathLongMax:
                return GetSequenceExpressionMathLongMax((SequenceExpressionMathLongMax)expr, source);
            case SequenceExpressionType.MathFloatMin:
                return GetSequenceExpressionMathFloatMin((SequenceExpressionMathFloatMin)expr, source);
            case SequenceExpressionType.MathFloatMax:
                return GetSequenceExpressionMathFloatMax((SequenceExpressionMathFloatMax)expr, source);
            case SequenceExpressionType.MathDoubleMin:
                return GetSequenceExpressionMathDoubleMin((SequenceExpressionMathDoubleMin)expr, source);
            case SequenceExpressionType.MathDoubleMax:
                return GetSequenceExpressionMathDoubleMax((SequenceExpressionMathDoubleMax)expr, source);
            default:
                throw new Exception("Unknown sequence expression type: " + expr.SequenceExpressionType);
            }
        }

        private string GetSequenceExpressionConditional(SequenceExpressionConditional seqCond, SourceBuilder source)
        {
            // todo: cast to most specific common supertype
            if(TypesHelper.GetNodeType(seqCond.TrueCase.Type(env), model) != null && TypesHelper.GetNodeType(seqCond.FalseCase.Type(env), model) != null)
            {
                return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                + " ? (GRGEN_LIBGR.INode)" + GetSequenceExpression(seqCond.TrueCase, source)
                + " : (GRGEN_LIBGR.INode)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
            }
            else if(TypesHelper.GetEdgeType(seqCond.TrueCase.Type(env), model) != null && TypesHelper.GetEdgeType(seqCond.FalseCase.Type(env), model) != null)
            {
                return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                + " ? (GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqCond.TrueCase, source)
                + " : (GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
            }
            else
            {
                return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                + " ? (object)" + GetSequenceExpression(seqCond.TrueCase, source)
                + " : (object)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
            }
        }

        private string GetSequenceExpressionExcept(SequenceExpressionExcept seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.ExceptStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.ExceptObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionLazyOr(SequenceExpressionLazyOr seq, SourceBuilder source)
        {
            return "((bool)" + GetSequenceExpression(seq.Left, source) + " || (bool)" + GetSequenceExpression(seq.Right, source) + ")";
        }

        private string GetSequenceExpressionLazyAnd(SequenceExpressionLazyAnd seq, SourceBuilder source)
        {
            return "((bool)" + GetSequenceExpression(seq.Left, source) + " && (bool)" + GetSequenceExpression(seq.Right, source) + ")";
        }

        private string GetSequenceExpressionStrictOr(SequenceExpressionStrictOr seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.OrStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.OrObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionStrictXor(SequenceExpressionStrictXor seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.XorStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.XorObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionStrictAnd(SequenceExpressionStrictAnd seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.AndStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.AndObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionEqual(SequenceExpressionEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.EqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.EqualObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionNotEqual(SequenceExpressionNotEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.NotEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.NotEqualObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.NotEqual, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionStructuralEqual(SequenceExpressionStructuralEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.StructuralEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.StructuralEqualObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.StructuralEqual, " + leftType + ", " + rightType + ", graph.Model), "
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionLower(SequenceExpressionLower seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.LowerStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.LowerObjects("
                + leftExpr + ", " + rightExpr + ", "
                + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Lower, " + leftType + ", " + rightType + ", graph.Model),"
                + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionGreater(SequenceExpressionGreater seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.GreaterStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.GreaterObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Greater, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionLowerEqual(SequenceExpressionLowerEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.LowerEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.LowerEqualObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.LowerEqual, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionGreaterEqual(SequenceExpressionGreaterEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.GreaterEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.GreaterEqualObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.GreaterEqual, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionShiftLeft(SequenceExpressionShiftLeft seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.ShiftLeftStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.ShiftLeft("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.ShiftLeft, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionShiftRight(SequenceExpressionShiftRight seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.ShiftRightStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.ShiftRight("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.ShiftRight, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionShiftRightUnsigned(SequenceExpressionShiftRightUnsigned seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.ShiftRightUnsignedStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.ShiftRightUnsigned("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.ShiftRightUnsigned, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionPlus(SequenceExpressionPlus seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.PlusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.PlusObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Plus, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionMinus(SequenceExpressionMinus seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.MinusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.MinusObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Minus, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionMul(SequenceExpressionMul seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.MulStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.MulObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mul, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionDiv(SequenceExpressionDiv seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.DivStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.DivObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Div, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionMod(SequenceExpressionMod seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
            string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
            if(seq.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.ModStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.ModObjects("
                    + leftExpr + ", " + rightExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mod, " + leftType + ", " + rightType + ", graph.Model),"
                    + leftType + ", " + rightType + ", graph)";
            }
        }

        private string GetSequenceExpressionNot(SequenceExpressionNot seqNot, SourceBuilder source)
        {
            return "!" + "((bool)" + GetSequenceExpression(seqNot.Operand, source) + ")";
        }

        private string GetSequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus seqUnaryPlus, SourceBuilder source)
        {
            string operandExpr = GetSequenceExpression(seqUnaryPlus.Operand, source);
            string operandType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + operandExpr + ", graph.Model)";
            if(seqUnaryPlus.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.UnaryPlusStatic(operandExpr, seqUnaryPlus.BalancedTypeStatic, seqUnaryPlus.OperandTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.UnaryPlusObjects("
                    + operandExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.UnaryPlus, " + operandType + ", graph.Model),"
                    + operandType + ", graph)";
            }
        }

        private string GetSequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus seqUnaryMinus, SourceBuilder source)
        {
            string operandExpr = GetSequenceExpression(seqUnaryMinus.Operand, source);
            string operandType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + operandExpr + ", graph.Model)";
            if(seqUnaryMinus.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.UnaryMinusStatic(operandExpr, seqUnaryMinus.BalancedTypeStatic, seqUnaryMinus.OperandTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.UnaryMinusObjects("
                    + operandExpr + ", " 
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.UnaryMinus, " + operandType + ", graph.Model),"
                    + operandType + ", graph)";
            }
        }

        private string GetSequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement seqBitwiseComplement, SourceBuilder source)
        {
            string operandExpr = GetSequenceExpression(seqBitwiseComplement.Operand, source);
            string operandType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + operandExpr + ", graph.Model)";
            if(seqBitwiseComplement.BalancedTypeStatic != "")
                return SequenceExpressionGeneratorHelper.BitwiseComplementStatic(operandExpr, seqBitwiseComplement.BalancedTypeStatic, seqBitwiseComplement.OperandTypeStatic, model);
            else
            {
                return "GRGEN_LIBGR.SequenceExpressionExecutionHelper.UnaryComplement("
                    + operandExpr + ", "
                    + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.BitwiseComplement, " + operandType + ", graph.Model),"
                    + operandType + ", graph)";
            }
        }

        private string GetSequenceExpressionCast(SequenceExpressionCast seqCast, SourceBuilder source)
        {
            string targetType = "UNSUPPORTED TYPE CAST";
            if(seqCast.TargetType is NodeType)
                targetType = ((NodeType)seqCast.TargetType).NodeInterfaceName;
            if(seqCast.TargetType is EdgeType)
                targetType = ((EdgeType)seqCast.TargetType).EdgeInterfaceName;
            if(seqCast.TargetType is ObjectType)
                targetType = ((ObjectType)seqCast.TargetType).ObjectInterfaceName;
            if(seqCast.TargetType is TransientObjectType)
                targetType = ((TransientObjectType)seqCast.TargetType).TransientObjectInterfaceName;
            // TODO: handle the non-node/edge/object/transient-object-types, too
            return "((" + targetType + ")" + GetSequenceExpression(seqCast.Operand, source) + ")";
        }

        private string GetSequenceExpressionDef(SequenceExpressionDef seqDef, SourceBuilder source)
        {
            String condition = "(";
            bool isFirst = true;
            foreach(SequenceExpression var in seqDef.DefVars)
            {
                if(isFirst)
                    isFirst = false;
                else
                    condition += " && ";
                condition += GetSequenceExpression(var, source) + "!=null";
            }
            condition += ")";
            return condition;
        }

        private string GetSequenceExpressionIsVisited(SequenceExpressionIsVisited seqIsVisited, SourceBuilder source)
        {
            return "graph.IsVisited("
                + "(GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(seqIsVisited.GraphElementVarExpr, source)
                + (seqIsVisited.VisitedFlagExpr != null ? ", (int)" + GetSequenceExpression(seqIsVisited.VisitedFlagExpr, source) : ", 0")
                + ")";
        }

        private string GetSequenceExpressionNow(SequenceExpressionNow seqNow, SourceBuilder source)
        {
            return "DateTime.UtcNow.ToFileTime()";
        }

        private string GetSequenceExpressionRandom(SequenceExpressionRandom seqRandom, SourceBuilder source)
        {
            if(seqRandom.UpperBound != null)
                return "GRGEN_LIBGR.Sequence.randomGenerator.Next((int)" + GetSequenceExpression(seqRandom.UpperBound, source) + ")";
            else
                return "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()";
        }

        private string GetSequenceExpressionTypeof(SequenceExpressionTypeof seqTypeof, SourceBuilder source)
        {
            return "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + GetSequenceExpression(seqTypeof.Entity, source) + ", graph.Model)";
        }

        private string GetSequenceExpressionExistsFile(SequenceExpressionExistsFile seqExistsFile, SourceBuilder source)
        {
            return "System.IO.File.Exists((string)" + GetSequenceExpression(seqExistsFile.Path, source) + ")";
        }

        private string GetSequenceExpressionImport(SequenceExpressionImport seqImport, SourceBuilder source)
        {
            return "GRGEN_LIBGR.GraphHelper.Import(" + GetSequenceExpression(seqImport.Path, source) + ", procEnv.Backend, graph.Model)";
        }

        private string GetSequenceExpressionCopyClone(SequenceExpressionCopy seqCopy, SourceBuilder source)
        {
            if(seqCopy.Deep)
                return GetSequenceExpressionCopy(seqCopy, source);
            else
                return GetSequenceExpressionClone(seqCopy, source);
        }

        private string GetSequenceExpressionCopy(SequenceExpressionCopy seqCopy, SourceBuilder source)
        {
            if(seqCopy.ObjectToBeCopied.Type(env) == "graph")
                return "GRGEN_LIBGR.GraphHelper.Copy((GRGEN_LIBGR.IGraph)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("set<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.Copy((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "), "
                    + "graph, new Dictionary<object, object>())";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("map<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.Copy((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "), "
                    + "graph, new Dictionary<object, object>())";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("array<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.Copy((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "), "
                    + "graph, new Dictionary<object, object>())";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.Copy((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "), "
                    + "graph, new Dictionary<object, object>())";
            }
            else if(env.Model.ObjectModel.GetType(seqCopy.ObjectToBeCopied.Type(env)) != null)
            {
                return "((GRGEN_LIBGR.IObject)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Copy(graph, new Dictionary<object, object>()))";
            }
            else if(env.Model.TransientObjectModel.GetType(seqCopy.ObjectToBeCopied.Type(env)) != null)
            {
                return "((GRGEN_LIBGR.ITransientObject)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Copy(graph, new Dictionary<object, object>()))";
            }
            else //if(seqCopy.ObjectToBeCopied.Type(env) == "")
                return "GRGEN_LIBGR.TypesHelper.Copy(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ", graph)";
        }

        private string GetSequenceExpressionClone(SequenceExpressionCopy seqCopy, SourceBuilder source)
        {
            if(seqCopy.ObjectToBeCopied.Type(env) == "graph")
                return "GRGEN_LIBGR.GraphHelper.Copy((GRGEN_LIBGR.IGraph)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("set<"))
            {
                return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                    + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("map<"))
            {
                return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                    + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("array<"))
            {
                return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                    + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("deque<"))
            {
                return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                    + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                    + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
            }
            else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("match<"))
            {
                string rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env));
                string matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env)));
                return "((" + matchInterfaceName + ")(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Clone())";
            }
            else if(env.Model.ObjectModel.GetType(seqCopy.ObjectToBeCopied.Type(env)) != null)
            {
                return "((GRGEN_LIBGR.IObject)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Clone(graph))";
            }
            else if(env.Model.TransientObjectModel.GetType(seqCopy.ObjectToBeCopied.Type(env)) != null)
            {
                return "((GRGEN_LIBGR.ITransientObject)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Clone())";
            }
            else //if(seqCopy.ObjectToBeCopied.Type(env) == "")
                return "GRGEN_LIBGR.TypesHelper.Clone(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ", graph)";
        }

        private string GetSequenceExpressionAttributeAccessOrElementOfMatch(SequenceExpressionAttributeOrMatchAccess seqAttrOrMa, SourceBuilder source)
        {
            return "GRGEN_LIBGR.ContainerHelper.GetAttributeOrElementOfMatch(" 
                + GetSequenceExpression(seqAttrOrMa.Source, source) + ", (string)(\"" + seqAttrOrMa.AttributeOrElementName + "\"))";
        }

        private string GetSequenceExpressionElementOfMatch(SequenceExpressionMatchAccess seqMA, SourceBuilder source)
        {
            String matchInterfaceName;
            if(seqMA.Source.Type(env).StartsWith("match<class "))
            {
                string matchClass = NamesOfEntities.MatchInterfaceName(TypesHelper.GetMatchClassName(seqMA.Source.Type(env)));
                matchInterfaceName = /*"GRGEN_ACTIONS." + */matchClass;
            }
            else // only "match<" for match of rule
            {
                String rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqMA.Source.Type(env));
                matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqMA.Source.Type(env)));
            }
            string match = "((" + matchInterfaceName + ")" + GetSequenceExpression(seqMA.Source, source) + ")";
            if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                return match + ".node_" + seqMA.ElementName;
            else if(TypesHelper.GetEdgeType(seqMA.Type(env), model) != null)
                return match + ".edge_" + seqMA.ElementName;
            else
                return match + ".var_" + seqMA.ElementName;
        }

        private string GetSequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess seqAttr, SourceBuilder source)
        {
            string element = "((GRGEN_LIBGR.IAttributeBearer)" + GetSequenceExpression(seqAttr.Source, source) + ")";
            string value = element + ".GetAttribute(\"" + seqAttr.AttributeName + "\")";
            string type = seqAttr.Type(env);
            if(type == ""
                    || type.StartsWith("set<") || type.StartsWith("map<")
                    || type.StartsWith("array<") || type.StartsWith("deque<"))
            {
                return "GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(" + element + ", \"" + seqAttr.AttributeName + "\", " + value + ")";
            }
            else
            {
                return "(" + TypesHelper.XgrsTypeToCSharpType(type, env.Model) + ")(" + value + ")";
            }
        }

        private string GetSequenceExpressionConstant(SequenceExpressionConstant seqConst, SourceBuilder source)
        {
            return seqHelper.GetConstant(seqConst.Constant);
        }

        private string GetSequenceExpressionVariable(SequenceExpressionVariable seqVar, SourceBuilder source)
        {
            return seqHelper.GetVar(seqVar.Variable);
        }

        private string GetSequenceExpressionNew(SequenceExpressionNew seqNew, SourceBuilder source)
        {
            if(seqNew.AttributeInitializationList == null)
            {
                if(TypesHelper.GetObjectType(seqNew.ConstructedType, model) != null)
                    return "procEnv.Graph.Model.ObjectModel.GetType(\"" + seqNew.ConstructedType + "\").CreateObject(procEnv.Graph, procEnv.Graph.GlobalVariables.FetchObjectUniqueId())";
                else //if(TypesHelper.GetTransientObjectType(seqNew.ConstructedType, model) != null)
                    return "procEnv.Graph.Model.TransientObjectModel.GetType(\"" + seqNew.ConstructedType + "\").CreateTransientObject()";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("fillFromSequence_" + seqNew.Id + "(");
                BaseObjectType objectType = env.Model.ObjectModel.GetType(seqNew.ConstructedType);
                if(objectType != null)
                    sb.Append("procEnv.Graph.GlobalVariables.FetchObjectUniqueId()");
                for(int i = 0; i < seqNew.AttributeInitializationList.Count; ++i)
                {
                    KeyValuePair<string, SequenceExpression> attributeInitialization = seqNew.AttributeInitializationList[i];
                    if(i > 0 || objectType != null)
                        sb.Append(", ");
                    sb.Append("(");
                    sb.Append(TypesHelper.XgrsTypeToCSharpType(env.TypeOfMemberOrAttribute(seqNew.ConstructedType, attributeInitialization.Key), model));
                    sb.Append(")");
                    sb.Append("(");
                    sb.Append(GetSequenceExpression(attributeInitialization.Value, source));
                    sb.Append(")");
                }
                sb.Append(")");
                return sb.ToString();
            }
        }

        private string GetSequenceExpressionScan(SequenceExpressionScan seqScan, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            if(seqScan.ResultType != null)
                sb.Append("(" + TypesHelper.XgrsTypeToCSharpType(seqScan.ResultType, env.Model) + ")");
            sb.Append("(GRGEN_LIBGR.GRSImport.Scan(GRGEN_LIBGR.TypesHelper.XgrsTypeToAttributeType(");
            if(seqScan.ResultType != null)
                sb.Append("\"" + seqScan.ResultType + "\"");
            else
                sb.Append("\"object\"");
            sb.Append(", procEnv.Graph.Model), ");
            sb.Append("(string)" + GetSequenceExpression(seqScan.StringExpr, source) + ", procEnv.Graph)");
            sb.Append(")");
            return sb.ToString();
        }

        private string GetSequenceExpressionTryScan(SequenceExpressionTryScan seqTryScan, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(bool)");
            sb.Append("(GRGEN_LIBGR.GRSImport.TryScan(GRGEN_LIBGR.TypesHelper.XgrsTypeToAttributeType(");
            if(seqTryScan.ResultType != null)
                sb.Append("\"" + seqTryScan.ResultType + "\"");
            else
                sb.Append("\"object\"");
            sb.Append(", procEnv.Graph.Model), ");
            sb.Append("(string)" + GetSequenceExpression(seqTryScan.StringExpr, source) + ", procEnv.Graph)");
            sb.Append(")");
            return sb.ToString();
        }

        public string GetSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqRuleQuery, SourceBuilder source)
        {
            SequenceRuleAllCall ruleCall = seqRuleQuery.RuleCall;

            StringBuilder sb = new StringBuilder();
            sb.Append("RuleQuery_" + ruleCall.Id + "(procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqRuleQuery.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                sb.Append(", ");
                sb.Append("var_" + seqVar.Name);
            }

            sb.Append(")");
            return sb.ToString();
        }

        public void EmitSequenceExpressionRuleQueryImplementation(SequenceExpressionRuleQuery seqRuleQuery, SequenceGenerator seqGen, NeededEntitiesEmitter needs, SourceBuilder source)
        {
            SequenceRuleAllCall ruleCall = seqRuleQuery.RuleCall;

            String matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Rule_" + ruleCall.Name;
            String patternName = ruleCall.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);

            source.AppendFrontFormat("static List<{0}> RuleQuery_" + ruleCall.Id + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv", matchType);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqRuleQuery.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                source.Append(", ");
                source.Append(TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model));
                source.Append(" ");
                source.Append("var_" + seqVar.Name);
            }

            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqRuleQuery.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqRuleQuery.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqRuleQuery.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            source.AppendFrontFormat("GRGEN_LIBGR.IMatchesExact<{0}> matches = ", matchType);

            SourceBuilder matchesSourceBuilder = new SourceBuilder();
            matchesSourceBuilder.AppendFormat("((GRGEN_LIBGR.IMatchesExact<{0}>)procEnv.MatchForQuery({1}, {2}{3}, procEnv.MaxMatches, {4}, {5}))",
                matchType, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Action_" + ruleCall.Name + ".Instance",
                ruleCall.Subgraph != null ? seqHelper.GetVar(ruleCall.Subgraph) : "null",
                seqHelper.BuildParametersInObject(ruleCall, ruleCall.ArgumentExpressions, source), ruleCall.Special ? "true" : "false",
                fireDebugEvents ? "true" : "false");
            for(int i = 0; i < ruleCall.Filters.Count; ++i)
            {
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitFilterCall(matchesSourceBuilder, ruleCall.Filters[i], patternName, matchesSource, ruleCall.PackagePrefixedName, true);
            }
            source.AppendFormat("{0};\n", matchesSourceBuilder.ToString());

            source.AppendFrontFormat("List<{0}> result = ", matchType);
            source.Append("matches.ToListExact()");
            source.Append(";\n");

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, "matches", ruleCall.special ? "true" : "false", fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, "matches", ruleCall.special ? "true" : "false", fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "result", fireDebugEvents);

            source.AppendFront("return result;\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private string GetSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqMultiRuleQuery, SourceBuilder source)
        {
            SequenceMultiRuleAllCall seqMulti = seqMultiRuleQuery.MultiRuleCall;

            StringBuilder sb = new StringBuilder();
            sb.Append("MultiRuleQuery_" + seqMulti.Id + "(procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqMultiRuleQuery.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                sb.Append(", ");
                sb.Append("var_" + seqVar.Name);
            }

            sb.Append(")");
            return sb.ToString();
        }

        public void EmitSequenceExpressionMultiRuleQueryImplementation(SequenceExpressionMultiRuleQuery seqMultiRuleQuery, SequenceGenerator seqGen, NeededEntitiesEmitter needs, SourceBuilder source)
        {
            SequenceMultiRuleAllCall seqMulti = seqMultiRuleQuery.MultiRuleCall;

            String matchType = NamesOfEntities.MatchInterfaceName(seqMultiRuleQuery.MatchClass);

            source.AppendFrontFormat("static List<{0}> MultiRuleQuery_" + seqMulti.Id + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv", matchType);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqMultiRuleQuery.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                source.Append(", ");
                source.Append(TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model));
                source.Append(" ");
                source.Append("var_" + seqVar.Name);
            }

            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqMulti.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqMultiRuleQuery.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqMulti.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            String matchListName = "MatchList_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);

            SourceBuilder matchesSourceBuilder = new SourceBuilder();

            if(seqMulti.Filters.Count != 0)
                matchesSourceBuilder.AppendFormat("GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_LIBGR.IMatch>(");
            else
                matchesSourceBuilder.AppendFormat("GRGEN_LIBGR.MatchListHelper.ToList<{0}>(", matchType);

            matchesSourceBuilder.Append(GetRuleCallOfSequenceMultiRuleAllCall(seqMulti, seqMulti.Sequences.Count - 1, matchListName, source));

            // emit code for match class (non-rule-based) filtering
            for(int i = 0; i < seqMulti.Filters.Count; ++i)
            {
                if(i == 0)
                    matchesSourceBuilder.Append(")");

                SequenceFilterCallBase sequenceFilterCall = seqMulti.Filters[i];
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitMatchClassFilterCall(matchesSourceBuilder, sequenceFilterCall, matchesSource, true);
            }

            if(seqMulti.Filters.Count == 0) // todo: rethink parenthesis ends handling - maybe fix is frickelei
                matchesSourceBuilder.Append(")");

            source.AppendFrontFormat("List<{0}> result = ", matchType);

            if(seqMulti.Filters.Count != 0)
                source.Append("GRGEN_LIBGR.MatchListHelper.ToList<" + matchType + ">(" + matchesSourceBuilder.ToString() + ");\n");
            else
                source.Append(matchesSourceBuilder.ToString() + ";\n");

            source.AppendFrontFormat("bool[] specialArray = new bool[{0}];\n", seqMultiRuleQuery.MultiRuleCall.Sequences.Count);
            for(int i = 0; i < seqMultiRuleQuery.MultiRuleCall.Sequences.Count; ++i)
            {
                source.AppendFrontFormat("specialArray[{0}] = {1};\n", i, ((SequenceRuleCall)seqMultiRuleQuery.MultiRuleCall.Sequences[i]).special ? "true" : "false");
            }

            string matchesArray = "multi_rule_call_result_" + seqMulti.Id; // implicit name defined elsewhere - TODO: explicit
            if(seqMulti.Filters.Count != 0)
                source.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.RemoveUnavailable(result, {0});\n", matchesArray);

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, matchesArray, "specialArray", fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, matchesArray, "specialArray", fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "result", fireDebugEvents);

            source.AppendFront("return result;\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        public string GetSequenceExpressionMappingClause(SequenceExpressionMappingClause seqMappingClause, SourceBuilder source)
        {
            SequenceMultiRulePrefixedSequence seqMulti = seqMappingClause.MultiRulePrefixedSequence;

            StringBuilder sb = new StringBuilder();
            sb.Append("MappingClause_" + seqMulti.Id + "(procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqMappingClause.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                sb.Append(", ");
                sb.Append("var_" + seqVar.Name);
            }

            sb.Append(")");
            return sb.ToString();
        }

        public void EmitSequenceExpressionMappingClauseImplementation(SequenceExpressionMappingClause seqMappingClause, SequenceGenerator seqGen, NeededEntitiesEmitter needs, SourceBuilder source)
        {
            SequenceMultiRulePrefixedSequence seqMulti = seqMappingClause.MultiRulePrefixedSequence;

            source.AppendFront("static List<GRGEN_LIBGR.IGraph> MappingClause_" + seqMulti.Id + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqMappingClause.GetLocalVariables(variables, constructors);
            foreach(SequenceVariable seqVar in variables.Keys)
            {
                source.Append(", ");
                source.Append(TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model));
                source.Append(" ");
                source.Append("var_" + seqVar.Name);
            }

            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            String patternMatchingConstructVarName = "patternMatchingConstruct_" + seqMulti.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.PatternMatchingConstruct {0} = new GRGEN_LIBGR.PatternMatchingConstruct(\"{1}\", {2});\n",
                patternMatchingConstructVarName, SequenceGeneratorHelper.Escape(seqMappingClause.Symbol),
                SequenceGeneratorHelper.ConstructTypeValue(seqMappingClause.ConstructType));
            SequenceRuleCallMatcherGenerator.EmitBeginExecutionEventFiring(source, patternMatchingConstructVarName, fireDebugEvents);

            source.AppendFront("List<GRGEN_LIBGR.IGraph> graphs = new List<GRGEN_LIBGR.IGraph>();\n");

            needs.EmitNeededVarAndRuleEntities(seqMulti, source);

            string matchListName = EmitSequenceMultiRulePrefixedSequenceMatchingForMappingClause(seqMulti, source);

            // code to handle the rewrite next match
            source.AppendFront("if(" + matchListName + ".Count != 0) {\n");
            source.Indent();

            // iterate through matches, use Modify on each, fire the next match event after the first
            String enumeratorName = "enum_" + seqMulti.Id;
            source.AppendFront("IEnumerator<GRGEN_LIBGR.IMatch> " + enumeratorName + " = " + matchListName + ".GetEnumerator();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();

            String matchToConstructIndexName = "MatchToConstructIndex_" + seqMulti.Id;
            source.AppendFrontFormat("switch({0}[" + enumeratorName + ".Current])\n", matchToConstructIndexName);
            source.AppendFront("{\n");
            source.Indent();

            // emit code for rewriting the current match (for each rule, rule fitting to the match is selected by rule name)
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                SequenceMultiRulePrefixedSequenceRewritingGenerator ruleRewritingGenerator = new SequenceMultiRulePrefixedSequenceRewritingGenerator(
                    seqMulti, (SequenceRulePrefixedSequence)seqMulti.RulePrefixedSequences[i], this, seqHelper, fireDebugEvents);
                ruleRewritingGenerator.EmitRewritingMapping(source, seqGen, matchListName, enumeratorName, i);
            }

            source.AppendFrontFormat("default: throw new Exception(\"Unknown construct index of pattern \" + {0}.Current.Pattern.PackagePrefixedName + \" in match!\");", enumeratorName);
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators = new SequenceRuleCallMatcherGenerator[seqMulti.RulePrefixedSequences.Count];
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = (SequenceRulePrefixedSequence)seqMulti.RulePrefixedSequences[i];
                ruleMatcherGenerators[i] = new SequenceRuleCallMatcherGenerator(seqRulePrefixedSequence.Rule, this, seqHelper, fireDebugEvents);
            }
            
            SequenceRuleCallMatcherGenerator.EmitFinishedEventFiring(source, ruleMatcherGenerators, fireDebugEvents);
            SequenceRuleCallMatcherGenerator.EmitEndExecutionEventFiring(source, patternMatchingConstructVarName, "graphs", fireDebugEvents);

            source.AppendFront("return graphs;\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private string EmitSequenceMultiRulePrefixedSequenceMatchingForMappingClause(SequenceMultiRulePrefixedSequence seqMulti, SourceBuilder source)
        {
            // likely todo: in case of full events handling also in expressions: matches list (!= match list) missing
            String matchListName = "MatchList_" + seqMulti.Id;
            String matchToConstructIndexName = "MatchToConstructIndex_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);
            source.AppendFrontFormat("Dictionary<GRGEN_LIBGR.IMatch, int> {0} = new Dictionary<GRGEN_LIBGR.IMatch, int>();\n", matchToConstructIndexName);

            // emit code for matching all the contained rules
            SequenceRuleCallMatcherGenerator[] ruleMatcherGenerators = new SequenceRuleCallMatcherGenerator[seqMulti.RulePrefixedSequences.Count];
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = (SequenceRulePrefixedSequence)seqMulti.RulePrefixedSequences[i];
                ruleMatcherGenerators[i] = new SequenceRuleCallMatcherGenerator(seqRulePrefixedSequence.Rule, this, seqHelper, fireDebugEvents);
                ruleMatcherGenerators[i].EmitMatchingAndCloning(source, "procEnv.MaxMatches");
            }

            SequenceRuleCallMatcherGenerator.EmitPreMatchEventFiring(source, ruleMatcherGenerators, fireDebugEvents);

            // emit code for rule-based filtering
            for(int i = 0; i < seqMulti.RulePrefixedSequences.Count; ++i)
            {
                ruleMatcherGenerators[i].EmitFiltering(source);
                ruleMatcherGenerators[i].EmitToMatchListAdding(source, matchListName, matchToConstructIndexName, i);
            }

            // emit code for match class (non-rule-based) filtering
            foreach(SequenceFilterCallBase sequenceFilterCall in seqMulti.Filters)
            {
                EmitMatchClassFilterCall(source, sequenceFilterCall, matchListName, false);
            }

            SequenceRuleCallMatcherGenerator.EmitMatchEventFiring(source, ruleMatcherGenerators, seqMulti.Filters.Count > 0, matchListName, fireDebugEvents);

            return matchListName;
        }

        private string GetRuleCallOfSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, int index, String matchListName, SourceBuilder source)
        {
            SequenceRuleCall ruleCall = (SequenceRuleCall)seqMulti.Sequences[index];
            if(index == 0)
                return "GRGEN_LIBGR.MatchListHelper.AddReturn(" + matchListName + ", " + GetSequenceExpressionMultiRuleCallAssignment(seqMulti, source) + ")";
            else
                return "GRGEN_LIBGR.MatchListHelper.AddReturn(" + GetRuleCallOfSequenceMultiRuleAllCall(seqMulti, index - 1, matchListName, source) + ",\n" + GetSequenceExpressionMultiRuleCall(seqMulti, index, source) + ")";
        }

        private string GetSequenceExpressionMultiRuleCallAssignment(SequenceMultiRuleAllCall seqMulti, SourceBuilder source)
        {
            SequenceRuleCall ruleCall = (SequenceRuleCall)seqMulti.Sequences[0];
            String matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Rule_" + ruleCall.Name;
            String patternName = ruleCall.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String multiRuleCallResultName = "multi_rule_call_result_" + seqMulti.Id;
            source.AppendFrontFormat("GRGEN_LIBGR.IMatches[] {0} = null;\n", multiRuleCallResultName);
            SourceBuilder matchesSourceBuilder = new SourceBuilder();
            matchesSourceBuilder.AppendFormat("((GRGEN_LIBGR.IMatchesExact<{0}>)(({1} = procEnv.MatchForQuery({2},{3}))[0]))",
                matchType, multiRuleCallResultName, fireDebugEvents ? "true" : "false", GetActionCallObjects(seqMulti, source));
            for(int i = 0; i < ruleCall.Filters.Count; ++i)
            {
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitFilterCall(matchesSourceBuilder, ruleCall.Filters[i], patternName, matchesSource, ruleCall.PackagePrefixedName, true);
            }
            return matchesSourceBuilder.ToString() + ".ToListExact()";
        }

        private string GetActionCallObjects(SequenceMultiRuleAllCall seqMulti, SourceBuilder source)
        {
            SourceBuilder matchesSourceBuilder = new SourceBuilder();

            bool first = true;
            for(int i = 0; i < seqMulti.Sequences.Count; ++i)
            {
                SequenceRuleCall ruleCall = (SequenceRuleCall)seqMulti.Sequences[i];

                if(first)
                    first = false;
                else
                    matchesSourceBuilder.Append(",");

                matchesSourceBuilder.AppendFormat("new GRGEN_LIBGR.ActionCall({0}, procEnv.MaxMatches{1})",
                    "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Action_" + ruleCall.Name + ".Instance",
                    seqHelper.BuildParametersInObject(ruleCall, ruleCall.ArgumentExpressions, source));
            }

            return matchesSourceBuilder.ToString();
        }

        private string GetSequenceExpressionMultiRuleCall(SequenceMultiRuleAllCall seqMulti, int index, SourceBuilder source)
        {
            SequenceRuleCall ruleCall = (SequenceRuleCall)seqMulti.Sequences[index];
            String matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Rule_" + ruleCall.Name;
            String patternName = ruleCall.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String multiRuleCallResultName = "multi_rule_call_result_" + seqMulti.Id;
            SourceBuilder matchesSourceBuilder = new SourceBuilder();
            matchesSourceBuilder.AppendFormat("((GRGEN_LIBGR.IMatchesExact<{0}>)(({1})[{2}]))",
                matchType, multiRuleCallResultName, index);
            for(int i = 0; i < ruleCall.Filters.Count; ++i)
            {
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitFilterCall(matchesSourceBuilder, ruleCall.Filters[i], patternName, matchesSource, ruleCall.PackagePrefixedName, true);
            }
            return matchesSourceBuilder.ToString() + ".ToListExact()";
        }

        private string GetSequenceExpressionFunctionCall(SequenceExpressionFunctionCall seqFuncCall, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            if(seqFuncCall.IsExternal)
                sb.Append("GRGEN_EXPR.ExternalFunctions.");
            else
                sb.AppendFormat("{0}Functions.", "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqFuncCall.Package));
            sb.Append(seqFuncCall.Name);
            sb.Append("(procEnv, graph");
            sb.Append(seqHelper.BuildParameters(seqFuncCall, seqFuncCall.ArgumentExpressions, source));
            sb.Append(")");
            return sb.ToString();
        }

        private string GetSequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall seqFuncCall, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            if(seqFuncCall.TargetExpr.Type(env) == "")
            {
                sb.Append("((GRGEN_LIBGR.IGraphElement)");
                sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                sb.Append(").ApplyFunctionMethod(procEnv, graph, ");
                sb.Append("\"" + seqFuncCall.Name + "\"");
                sb.Append(seqHelper.BuildParametersInObject(seqFuncCall, seqFuncCall.ArgumentExpressions, source));
                sb.Append(")");
            }
            else
            {
                sb.Append("((");
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqFuncCall.TargetExpr.Type(env), model));
                sb.Append(")");
                sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                sb.Append(").");
                sb.Append(seqFuncCall.Name);
                sb.Append("(procEnv, graph");
                sb.Append(seqHelper.BuildParameters(seqFuncCall, seqFuncCall.ArgumentExpressions, TypesHelper.GetInheritanceType(seqFuncCall.TargetExpr.Type(env), model).GetFunctionMethod(seqFuncCall.Name), source));
                sb.Append(")");
            }
            return sb.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------

        #region Graph expressions

        private string GetSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqEqualsAny, SourceBuilder source)
        {
            if(seqEqualsAny.IncludingAttributes)
                return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", true)";
            else
                return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", false)";
        }

        private string GetSequenceExpressionGetEquivalent(SequenceExpressionGetEquivalent seqGetEquivalent, SourceBuilder source)
        {
            if(seqGetEquivalent.IncludingAttributes)
                return "GRGEN_LIBGR.GraphHelper.GetEquivalent((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqGetEquivalent.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqGetEquivalent.SubgraphSet, source) + ", true)";
            else
                return "GRGEN_LIBGR.GraphHelper.GetEquivalent((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqGetEquivalent.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqGetEquivalent.SubgraphSet, source) + ", false)";
        }

        private string GetSequenceExpressionCanonize(SequenceExpressionCanonize seqCanonize, SourceBuilder source)
        {
            return "((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqCanonize.Graph, source) + ").Canonize()";
        }

        private string GetSequenceExpressionNameof(SequenceExpressionNameof seqNameof, SourceBuilder source)
        {
            if(seqNameof.NamedEntity != null)
                return "GRGEN_LIBGR.GraphHelper.Nameof(" + GetSequenceExpression(seqNameof.NamedEntity, source) + ", graph)";
            else
                return "GRGEN_LIBGR.GraphHelper.Nameof(null, graph)";
        }

        private string GetSequenceExpressionUniqueof(SequenceExpressionUniqueof seqUniqueof, SourceBuilder source)
        {
            if(seqUniqueof.UniquelyIdentifiedEntity != null)
                return "GRGEN_LIBGR.GraphHelper.Uniqueof(" + GetSequenceExpression(seqUniqueof.UniquelyIdentifiedEntity, source) + ", graph)";
            else
                return "GRGEN_LIBGR.GraphHelper.Uniqueof(null, graph)";
        }

        private string GetSequenceExpressionThis(SequenceExpressionThis seqThis, SourceBuilder source)
        {
            return "graph";
        }

        private string GetSequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph seqFromGraph, SourceBuilder source)
        {
            string profilingArgument = seqFromGraph.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper.GetGraphElement((GRGEN_LIBGR.INamedGraph)graph, \"" + seqFromGraph.ElementName + "\"" + profilingArgument + ")";
        }

        private string GetSequenceExpressionNodeByName(SequenceExpressionNodeByName seqNodeByName, SourceBuilder source)
        {
            string profilingArgument = seqNodeByName.EmitProfiling ? ", procEnv" : "";
            string nodeType = seqNodeByName.NodeType != null ? seqHelper.ExtractNodeType(source, seqNodeByName.NodeType) : null;
            if(nodeType != null)
                return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + ", " + nodeType + profilingArgument + ")";
            else
                return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + profilingArgument + ")";
        }

        private string GetSequenceExpressionEdgeByName(SequenceExpressionEdgeByName seqEdgeByName, SourceBuilder source)
        {
            string profilingArgument = seqEdgeByName.EmitProfiling ? ", procEnv" : "";
            string edgeType = seqEdgeByName.EdgeType != null ? seqHelper.ExtractEdgeType(source, seqEdgeByName.EdgeType) : null;
            if(edgeType != null)
                return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + ", " + edgeType + profilingArgument + ")";
            else
                return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + profilingArgument + ")";
        }

        private string GetSequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique seqNodeByUnique, SourceBuilder source)
        {
            string profilingArgument = seqNodeByUnique.EmitProfiling ? ", procEnv" : "";
            string nodeType = seqNodeByUnique.NodeType != null ? seqHelper.ExtractNodeType(source, seqNodeByUnique.NodeType) : null;
            if(nodeType != null)
                return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + ", " + nodeType + profilingArgument + ")";
            else
                return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + profilingArgument + ")";
        }

        private string GetSequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique seqEdgeByUnique, SourceBuilder source)
        {
            string profilingArgument = seqEdgeByUnique.EmitProfiling ? ", procEnv" : "";
            string edgeType = seqEdgeByUnique.EdgeType != null ? seqHelper.ExtractEdgeType(source, seqEdgeByUnique.EdgeType) : null;
            if(edgeType != null)
                return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + ", " + edgeType + profilingArgument + ")";
            else
                return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + profilingArgument + ")";
        }

        private string GetSequenceExpressionSource(SequenceExpressionSource seqSrc, SourceBuilder source)
        {
            return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqSrc.Edge, source) + ").Source";
        }

        private string GetSequenceExpressionTarget(SequenceExpressionTarget seqTgt, SourceBuilder source)
        {
            return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqTgt.Edge, source) + ").Target";
        }

        private string GetSequenceExpressionOpposite(SequenceExpressionOpposite seqOpp, SourceBuilder source)
        {
            return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqOpp.Edge, source) + ").Opposite((GRGEN_LIBGR.INode)(" + GetSequenceExpression(seqOpp.Node, source) + "))";
        }

        private string GetSequenceExpressionEmpty(SequenceExpressionEmpty seqEmpty, SourceBuilder source)
        {
            return "(graph.NumNodes+graph.NumEdges==0)";
        }

        private string GetSequenceExpressionSize(SequenceExpressionSize seqSize, SourceBuilder source)
        {
            return "(graph.NumNodes+graph.NumEdges)";
        }

        private string GetSequenceExpressionNodes(SequenceExpressionNodes seqNodes, SourceBuilder source)
        {
            string nodeType = seqHelper.ExtractNodeType(source, seqNodes.NodeType);
            string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper.Nodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionEdges(SequenceExpressionEdges seqEdges , SourceBuilder source)
        {
            string edgeType = seqHelper.ExtractEdgeType(source, seqEdges.EdgeType);
            string edgeRootType = SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqEdges.EdgeType, env);
            string directedness = seqHelper.GetDirectedness(edgeRootType);
            string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper.Edges" + directedness + "(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionCountNodes(SequenceExpressionCountNodes seqNodes, SourceBuilder source)
        {
            string nodeType = seqHelper.ExtractNodeType(source, seqNodes.NodeType);
            string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper.CountNodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionCountEdges(SequenceExpressionCountEdges seqEdges, SourceBuilder source)
        {
            string edgeType = seqHelper.ExtractEdgeType(source, seqEdges.EdgeType);
            string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper.CountEdges(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident seqAdjInc, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqAdjInc.SourceNode, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqAdjInc.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqAdjInc.OppositeNodeType);
            string directedness = seqHelper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqAdjInc.EdgeType, env));
            string function;
            switch(seqAdjInc.SequenceExpressionType)
            {
            case SequenceExpressionType.AdjacentNodes:
                function = "Adjacent"; break;
            case SequenceExpressionType.AdjacentNodesViaIncoming:
                function = "AdjacentIncoming"; break;
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
                function = "AdjacentOutgoing"; break;
            case SequenceExpressionType.IncidentEdges:
                function = "Incident" + directedness; break;
            case SequenceExpressionType.IncomingEdges:
                function = "Incoming" + directedness; break;
            case SequenceExpressionType.OutgoingEdges:
                function = "Outgoing" + directedness; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqAdjInc.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident seqCntAdjInc, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqCntAdjInc.SourceNode, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqCntAdjInc.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqCntAdjInc.OppositeNodeType);
            string function;
            switch(seqCntAdjInc.SequenceExpressionType)
            {
            case SequenceExpressionType.CountAdjacentNodes:
                function = "CountAdjacent"; break;
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
                function = "CountAdjacentIncoming"; break;
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
                function = "CountAdjacentOutgoing"; break;
            case SequenceExpressionType.CountIncidentEdges:
                function = "CountIncident"; break;
            case SequenceExpressionType.CountIncomingEdges:
                function = "CountIncoming"; break;
            case SequenceExpressionType.CountOutgoingEdges:
                function = "CountOutgoing"; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqCntAdjInc.EmitProfiling ? ", procEnv" : "";
            if(seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodes
                || seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaIncoming
                || seqCntAdjInc.SequenceExpressionType == SequenceExpressionType.CountAdjacentNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.CountIncidentEdges || SequenceExpressionType.CountIncomingEdges || SequenceExpressionType.CountOutgoingEdges
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionReachable(SequenceExpressionReachable seqReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqReach.SourceNode, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqReach.OppositeNodeType);
            string directedness = seqHelper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqReach.EdgeType, env));
            string function;
            switch(seqReach.SequenceExpressionType)
            {
            case SequenceExpressionType.ReachableNodes:
                function = "Reachable"; break;
            case SequenceExpressionType.ReachableNodesViaIncoming:
                function = "ReachableIncoming"; break;
            case SequenceExpressionType.ReachableNodesViaOutgoing:
                function = "ReachableOutgoing"; break;
            case SequenceExpressionType.ReachableEdges:
                function = "ReachableEdges" + directedness; break;
            case SequenceExpressionType.ReachableEdgesViaIncoming:
                function = "ReachableEdgesIncoming" + directedness; break;
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                function = "ReachableEdgesOutgoing" + directedness; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqReach.EmitProfiling ? ", procEnv" : "";
            if(seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodes
                || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaIncoming
                || seqReach.SequenceExpressionType == SequenceExpressionType.ReachableNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.ReachableEdges || SequenceExpressionType.ReachableEdgesViaIncoming || SequenceExpressionType.ReachableEdgesViaOutgoing
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionCountReachable(SequenceExpressionCountReachable seqCntReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqCntReach.SourceNode, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqCntReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqCntReach.OppositeNodeType);
            string function;
            switch(seqCntReach.SequenceExpressionType)
            {
            case SequenceExpressionType.CountReachableNodes:
                function = "CountReachable"; break;
            case SequenceExpressionType.CountReachableNodesViaIncoming:
                function = "CountReachableIncoming"; break;
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
                function = "CountReachableOutgoing"; break;
            case SequenceExpressionType.CountReachableEdges:
                function = "CountReachableEdges"; break;
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
                function = "CountReachableEdgesIncoming"; break;
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                function = "CountReachableEdgesOutgoing"; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqCntReach.EmitProfiling ? ", procEnv" : "";
            if(seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodes
                || seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodesViaIncoming
                || seqCntReach.SequenceExpressionType == SequenceExpressionType.CountReachableNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.CountReachableEdges || SequenceExpressionType.CountReachableEdgesViaIncoming || SequenceExpressionType.CountReachableEdgesViaOutgoing
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable seqBoundReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
            string depth = GetSequenceExpression(seqBoundReach.Depth, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqBoundReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqBoundReach.OppositeNodeType);
            string directedness = seqHelper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqBoundReach.EdgeType, env));
            string function;
            switch(seqBoundReach.SequenceExpressionType)
            {
            case SequenceExpressionType.BoundedReachableNodes:
                function = "BoundedReachable"; break;
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
                function = "BoundedReachableIncoming"; break;
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
                function = "BoundedReachableOutgoing"; break;
            case SequenceExpressionType.BoundedReachableEdges:
                function = "BoundedReachableEdges" + directedness; break;
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
                function = "BoundedReachableEdgesIncoming" + directedness; break;
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                function = "BoundedReachableEdgesOutgoing" + directedness; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqBoundReach.EmitProfiling ? ", procEnv" : "";
            if(seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodes
                || seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaIncoming
                || seqBoundReach.SequenceExpressionType == SequenceExpressionType.BoundedReachableNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.BoundedReachableEdges || SequenceExpressionType.BoundedReachableEdgesViaIncoming || SequenceExpressionType.BoundedReachableEdgesViaOutgoing
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth seqBoundReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
            string depth = GetSequenceExpression(seqBoundReach.Depth, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqBoundReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqBoundReach.OppositeNodeType);
            string function;
            switch(seqBoundReach.SequenceExpressionType)
            {
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
                function = "BoundedReachableWithRemainingDepth"; break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
                function = "BoundedReachableWithRemainingDepthIncoming"; break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                function = "BoundedReachableWithRemainingDepthOutgoing"; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqBoundReach.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable seqCntBoundReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqCntBoundReach.SourceNode, source);
            string depth = GetSequenceExpression(seqCntBoundReach.Depth, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqCntBoundReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqCntBoundReach.OppositeNodeType);
            string function;
            switch(seqCntBoundReach.SequenceExpressionType)
            {
            case SequenceExpressionType.CountBoundedReachableNodes:
                function = "CountBoundedReachable"; break;
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
                function = "CountBoundedReachableIncoming"; break;
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
                function = "CountBoundedReachableOutgoing"; break;
            case SequenceExpressionType.CountBoundedReachableEdges:
                function = "CountBoundedReachableEdges"; break;
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
                function = "CountBoundedReachableEdgesIncoming"; break;
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                function = "CountBoundedReachableEdgesOutgoing"; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqCntBoundReach.EmitProfiling ? ", procEnv" : "";
            if(seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodes
                || seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodesViaIncoming
                || seqCntBoundReach.SequenceExpressionType == SequenceExpressionType.CountBoundedReachableNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.CountBoundedReachableEdges || SequenceExpressionType.CountBoundedReachableEdgesViaIncoming || SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident seqIsAdjInc, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqIsAdjInc.SourceNode, source);
            string endElement = GetSequenceExpression(seqIsAdjInc.EndElement, source);
            string endElementType;
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqIsAdjInc.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqIsAdjInc.OppositeNodeType);
            string function;
            switch(seqIsAdjInc.SequenceExpressionType)
            {
            case SequenceExpressionType.IsAdjacentNodes:
                function = "IsAdjacent";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
                function = "IsAdjacentIncoming";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
                function = "IsAdjacentOutgoing";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsIncidentEdges:
                function = "IsIncident";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            case SequenceExpressionType.IsIncomingEdges:
                function = "IsIncoming";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            case SequenceExpressionType.IsOutgoingEdges:
                function = "IsOutgoing";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            default:
                function = "INTERNAL ERROR";
                endElementType = "INTERNAL ERROR";
                break;
            }
            string profilingArgument = seqIsAdjInc.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper." + function + "((GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionIsReachable(SequenceExpressionIsReachable seqIsReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqIsReach.SourceNode, source);
            string endElement = GetSequenceExpression(seqIsReach.EndElement, source);
            string endElementType;
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqIsReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqIsReach.OppositeNodeType);
            string function;
            switch(seqIsReach.SequenceExpressionType)
            {
            case SequenceExpressionType.IsReachableNodes:
                function = "IsReachable";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsReachableNodesViaIncoming:
                function = "IsReachableIncoming";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
                function = "IsReachableOutgoing";
                endElementType = "(GRGEN_LIBGR.INode)";
                break;
            case SequenceExpressionType.IsReachableEdges:
                function = "IsReachableEdges";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
                function = "IsReachableEdgesIncoming";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                function = "IsReachableEdgesOutgoing";
                endElementType = "(GRGEN_LIBGR.IEdge)";
                break;
            default:
                function = "INTERNAL ERROR";
                endElementType = "INTERNAL ERROR";
                break;
            }
            string profilingArgument = seqIsReach.EmitProfiling ? ", procEnv" : "";
            return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", " + endElementType + " " + endElement
                + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
        }

        private string GetSequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable seqIsBoundReach, SourceBuilder source)
        {
            string sourceNode = GetSequenceExpression(seqIsBoundReach.SourceNode, source);
            string endElement = GetSequenceExpression(seqIsBoundReach.EndElement, source);
            string depth = GetSequenceExpression(seqIsBoundReach.Depth, source);
            string incidentEdgeType = seqHelper.ExtractEdgeType(source, seqIsBoundReach.EdgeType);
            string adjacentNodeType = seqHelper.ExtractNodeType(source, seqIsBoundReach.OppositeNodeType);
            string function;
            switch(seqIsBoundReach.SequenceExpressionType)
            {
            case SequenceExpressionType.IsBoundedReachableNodes:
                function = "IsBoundedReachable"; break;
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
                function = "IsBoundedReachableIncoming"; break;
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
                function = "IsBoundedReachableOutgoing"; break;
            case SequenceExpressionType.IsBoundedReachableEdges:
                function = "IsBoundedReachableEdges"; break;
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
                function = "IsBoundedReachableEdgesIncoming"; break;
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                function = "IsBoundedReachableEdgesOutgoing"; break;
            default:
                function = "INTERNAL ERROR"; break;
            }
            string profilingArgument = seqIsBoundReach.EmitProfiling ? ", procEnv" : "";
            if(seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodes
                || seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaIncoming
                || seqIsBoundReach.SequenceExpressionType == SequenceExpressionType.IsBoundedReachableNodesViaOutgoing)
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (GRGEN_LIBGR.INode)" + endElement + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
            else // SequenceExpressionType.IsBoundedReachableEdges || SequenceExpressionType.IsBoundedReachableEdgesViaIncoming || SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing
            {
                return "GRGEN_LIBGR.GraphHelper." + function + "(graph, (GRGEN_LIBGR.INode)" + sourceNode + ", (GRGEN_LIBGR.IEdge)" + endElement + ", (int)" + depth
                    + ", (GRGEN_LIBGR.EdgeType)" + incidentEdgeType + ", (GRGEN_LIBGR.NodeType)" + adjacentNodeType + profilingArgument + ")";
            }
        }

        private string GetSequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph seqInduced, SourceBuilder source)
        {
            return "GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInduced.NodeSet, source) + ", graph)";
        }

        private string GetSequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph seqDefined, SourceBuilder source)
        {
            if(seqDefined.EdgeSet.Type(env) == "set<Edge>")
                return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
            else if(seqDefined.EdgeSet.Type(env) == "set<UEdge>")
                return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
            else if(seqDefined.EdgeSet.Type(env) == "set<AEdge>")
                return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
            else
                return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
        }

        #endregion Graph expressions

        //-------------------------------------------------------------------------------------------------------------------

        #region Container expressions

        private string GetSequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString seqIn, SourceBuilder source)
        {
            string containerOrString;
            string type;
            if(seqIn.ContainerOrStringExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess seqInAttribute = (SequenceExpressionAttributeAccess)(seqIn.ContainerOrStringExpr);
                string element = "((GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(seqInAttribute.Source, source) + ")";
                containerOrString = element + ".GetAttribute(\"" + seqInAttribute.AttributeName + "\")";
                type = seqInAttribute.Type(env);
            }
            else
            {
                containerOrString = GetSequenceExpression(seqIn.ContainerOrStringExpr, source);
                type = seqIn.ContainerOrStringExpr.Type(env);
            }

            if(type == "")
            {
                string valueExpr = GetSequenceExpression(seqIn.Expr, source);
                string containerExpr = GetSequenceExpression(seqIn.ContainerOrStringExpr, source);
                return "GRGEN_LIBGR.ContainerHelper.InContainerOrString(procEnv, " + containerExpr + ", " + valueExpr + ")";
            }
            else if(type == "string")
            {
                string str = containerOrString;
                string sourceExpr = "((string)" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return str + ".Contains(" + sourceExpr + ")";
            }
            else if(type.StartsWith("array"))
            {
                string array = containerOrString;
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(type), model);
                string sourceExpr = "((" + arrayValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return array + ".Contains(" + sourceExpr + ")";
            }
            else if(type.StartsWith("deque"))
            {
                string deque = containerOrString;
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(type), model);
                string sourceExpr = "((" + dequeValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return deque + ".Contains(" + sourceExpr + ")";
            }
            else
            {
                string dictionary = containerOrString;
                string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(type), model);
                string sourceExpr = "((" + dictSrcType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return dictionary + ".ContainsKey(" + sourceExpr + ")";
            }
        }

        private string GetSequenceExpressionContainerSize(SequenceExpressionContainerSize seqContainerSize, SourceBuilder source)
        {
            string container = GetContainerValue(seqContainerSize, source);

            if(seqContainerSize.ContainerType(env) == "")
            {
                string containerExpr = GetSequenceExpression(seqContainerSize.ContainerExpr, source);
                return "GRGEN_LIBGR.ContainerHelper.ContainerSize(procEnv, " + containerExpr + ")";
            }
            else if(seqContainerSize.ContainerType(env).StartsWith("array"))
            {
                string array = container;
                return array + ".Count";
            }
            else if(seqContainerSize.ContainerType(env).StartsWith("deque"))
            {
                string deque = container;
                return deque + ".Count";
            }
            else
            {
                string dictionary = container;
                return dictionary + ".Count";
            }
        }

        private string GetSequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty seqContainerEmpty, SourceBuilder source)
        {
            string container = GetContainerValue(seqContainerEmpty, source);

            if(seqContainerEmpty.ContainerType(env) == "")
            {
                string containerExpr = GetSequenceExpression(seqContainerEmpty.ContainerExpr, source);
                return "GRGEN_LIBGR.ContainerHelper.ContainerEmpty(procEnv, " + containerExpr + ")";
            }
            else if(seqContainerEmpty.ContainerType(env).StartsWith("array"))
            {
                string array = container;
                return "(" + array + ".Count==0)";
            }
            else if(seqContainerEmpty.ContainerType(env).StartsWith("deque"))
            {
                string deque = container;
                return "(" + deque + ".Count==0)";
            }
            else
            {
                string dictionary = container;
                return "(" + dictionary + ".Count==0)";
            }
        }

        private string GetSequenceExpressionContainerAccess(SequenceExpressionContainerAccess seqContainerAccess, SourceBuilder source)
        {
            string container;
            string ContainerType;
            if(seqContainerAccess.ContainerExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess seqContainerAttribute = (SequenceExpressionAttributeAccess)(seqContainerAccess.ContainerExpr);
                string element = "((GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(seqContainerAttribute.Source, source) + ")";
                container = element + ".GetAttribute(\"" + seqContainerAttribute.AttributeName + "\")";
                if(seqContainerAttribute.Source.Type(env) == "")
                    ContainerType = "";
                else
                {
                    InheritanceType inheritanceType = TypesHelper.GetInheritanceType(seqContainerAttribute.Source.Type(env), env.Model);
                    AttributeType attributeType = inheritanceType.GetAttributeType(seqContainerAttribute.AttributeName);
                    ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);
                }
            }
            else
            {
                container = GetSequenceExpression(seqContainerAccess.ContainerExpr, source);
                ContainerType = seqContainerAccess.ContainerExpr.Type(env);
            }

            if(ContainerType == "")
            {
                string keyExpr = GetSequenceExpression(seqContainerAccess.KeyExpr, source);
                string containerExpr = GetSequenceExpression(seqContainerAccess.ContainerExpr, source);
                return "GRGEN_LIBGR.ContainerHelper.ContainerAccess(procEnv, " + containerExpr + ", " + keyExpr + ")";
            }
            else if(ContainerType.StartsWith("array"))
            {
                string array = container;
                string sourceExpr = "((int)" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                return array + "[" + sourceExpr + "]";
            }
            else if(ContainerType.StartsWith("deque"))
            {
                string deque = container;
                string sourceExpr = "((int)" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                return deque + "[" + sourceExpr + "]";
            }
            else
            {
                string dictionary = container;
                string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                string sourceExpr = "((" + dictSrcType + ")" + GetSequenceExpression(seqContainerAccess.KeyExpr, source) + ")";
                return dictionary + "[" + sourceExpr + "]";
            }
        }

        private string GetSequenceExpressionContainerPeek(SequenceExpressionContainerPeek seqContainerPeek, SourceBuilder source)
        {
            string container = GetContainerValue(seqContainerPeek, source);

            if(seqContainerPeek.KeyExpr != null)
            {
                if(seqContainerPeek.ContainerType(env) == "")
                    return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                else if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                {
                    string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerPeek.ContainerType(env)), model);
                    return "GRGEN_LIBGR.ContainerHelper.Peek<" + arrayValueType + ">(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                }
                else if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                {
                    string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerPeek.ContainerType(env)), model);
                    return "GRGEN_LIBGR.ContainerHelper.Peek<" + dequeValueType + ">(" + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + container + ")";
                }
                else
                {
                    string dictKeyType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerPeek.ContainerType(env)), model);
                    string dictValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqContainerPeek.ContainerType(env)), model);
                    return "GRGEN_LIBGR.ContainerHelper.Peek<" + dictKeyType + "," + dictValueType + ">(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                }
            }
            else
            {
                if(seqContainerPeek.ContainerType(env) == "")
                    return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ")";
                else if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                {
                    string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerPeek.ContainerType(env)), model);
                    return "GRGEN_LIBGR.ContainerHelper.Peek<" + arrayValueType + ">(" + container + ")";
                }
                else //if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                {
                    string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqContainerPeek.ContainerType(env)), model);
                    return "GRGEN_LIBGR.ContainerHelper.Peek<" + dequeValueType + ">(" + container + ")";
                }
            }
        }

        private string GetSequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GRGEN_LIBGR.ContainerHelper.FillSet(new Dictionary<");
            sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
            sb.Append(", GRGEN_LIBGR.SetValueType>(), ");
            sb.Append("\"");
            sb.Append(seqConstr.ValueType);
            sb.Append("\", ");
            sb.Append(GetSequenceExpression(seqConstr.SetToCopy, source));
            sb.Append(", graph.Model)");
            return sb.ToString();
        }

        private string GetSequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GRGEN_LIBGR.ContainerHelper.FillMap(new Dictionary<");
            sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.KeyType, model));
            sb.Append(", ");
            sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
            sb.Append(">(), ");
            sb.Append("\"");
            sb.Append(seqConstr.KeyType);
            sb.Append("\", ");
            sb.Append("\"");
            sb.Append(seqConstr.ValueType);
            sb.Append("\", ");
            sb.Append(GetSequenceExpression(seqConstr.MapToCopy, source));
            sb.Append(", graph.Model)");
            return sb.ToString();
        }

        private string GetSequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GRGEN_LIBGR.ContainerHelper.FillArray(new List<");
            sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
            sb.Append(">(), ");
            sb.Append("\"");
            sb.Append(seqConstr.ValueType);
            sb.Append("\", ");
            sb.Append(GetSequenceExpression(seqConstr.ArrayToCopy, source));
            sb.Append(", graph.Model)");
            return sb.ToString();
        }

        private string GetSequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GRGEN_LIBGR.ContainerHelper.FillDeque(new GRGEN_LIBGR.Deque<");
            sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
            sb.Append(">(), ");
            sb.Append("\"");
            sb.Append(seqConstr.ValueType);
            sb.Append("\", ");
            sb.Append(GetSequenceExpression(seqConstr.DequeToCopy, source));
            sb.Append(", graph.Model)");
            return sb.ToString();
        }

        private string GetSequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray seqContainerAsArray, SourceBuilder source)
        {
            string container = GetContainerValue(seqContainerAsArray, source);

            if(seqContainerAsArray.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.AsArray(" + container + ")";
            }
            else if(seqContainerAsArray.ContainerType(env).StartsWith("set<"))
            {
                string setType = TypesHelper.XgrsTypeToCSharpType(seqContainerAsArray.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.SetAsArray((" + setType + ")(" + container + "))";
            }
            else if(seqContainerAsArray.ContainerType(env).StartsWith("map<"))
            {
                string setType = TypesHelper.XgrsTypeToCSharpType(seqContainerAsArray.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.MapAsArray((" + setType + ")(" + container + "))";
            }
            else if(seqContainerAsArray.ContainerType(env).StartsWith("deque<"))
            {
                string setType = TypesHelper.XgrsTypeToCSharpType(seqContainerAsArray.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.DequeAsArray((" + setType + ")(" + container + "))";
            }
            else //if(seqContainerAsArray.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqContainerAsArray.ContainerType(env), model);
                return "((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionStringLength(SequenceExpressionStringLength seqStringLength, SourceBuilder source)
        {
            return "((string)" + GetSequenceExpression(seqStringLength.StringExpr, source) + ").Length";
        }

        private string GetSequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith seqStartsWith, SourceBuilder source)
        {
            return "((string)" + GetSequenceExpression(seqStartsWith.StringExpr, source) + ").StartsWith((string)"
                + GetSequenceExpression(seqStartsWith.StringToSearchForExpr, source) + ", StringComparison.InvariantCulture" + ")";
        }

        private string GetSequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith seqEndsWith, SourceBuilder source)
        {
            return "((string)" + GetSequenceExpression(seqEndsWith.StringExpr, source) + ").EndsWith((string)"
                + GetSequenceExpression(seqEndsWith.StringToSearchForExpr, source) + ", StringComparison.InvariantCulture" + ")";
        }

        private string GetSequenceExpressionStringSubstring(SequenceExpressionStringSubstring seqSubstring, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("((string)");
            sb.Append(GetSequenceExpression(seqSubstring.StringExpr, source));
            sb.Append(").Substring((int)");
            sb.Append(GetSequenceExpression(seqSubstring.StartIndexExpr, source));
            if(seqSubstring.LengthExpr != null)
            {
                sb.Append(", (int)");
                sb.Append(GetSequenceExpression(seqSubstring.LengthExpr, source));
            }
            sb.Append(")");
            return sb.ToString();
        }

        private string GetSequenceExpressionStringReplace(SequenceExpressionStringReplace seqReplace, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(((string)");
            sb.Append(GetSequenceExpression(seqReplace.StringExpr, source));
            sb.Append(").Substring(0, (int)");
            sb.Append(GetSequenceExpression(seqReplace.StartIndexExpr, source));
            sb.Append(") + ");
            sb.Append(GetSequenceExpression(seqReplace.ReplaceStringExpr, source));
            sb.Append(" + ((string)");
            sb.Append(GetSequenceExpression(seqReplace.StringExpr, source));
            sb.Append(").Substring((int)");
            sb.Append(GetSequenceExpression(seqReplace.StartIndexExpr, source));
            sb.Append(" + (int)");
            sb.Append(GetSequenceExpression(seqReplace.LengthExpr, source));
            sb.Append("))");
            return sb.ToString();
        }

        private string GetSequenceExpressionStringToLower(SequenceExpressionStringToLower seqToLower, SourceBuilder source)
        {
            return "((string)" + GetSequenceExpression(seqToLower.StringExpr, source) + ").ToLowerInvariant()";
        }

        private string GetSequenceExpressionStringToUpper(SequenceExpressionStringToUpper seqToUpper, SourceBuilder source)
        {
            return "((string)" + GetSequenceExpression(seqToUpper.StringExpr, source) + ").ToUpperInvariant()";
        }

        private string GetSequenceExpressionStringAsArray(SequenceExpressionStringAsArray seqStringAsArray, SourceBuilder source)
        {
            return "GRGEN_LIBGR.ContainerHelper.StringAsArray(" 
                + "(string)(" + GetSequenceExpression(seqStringAsArray.StringExpr, source) + ")," 
                + "(string)(" + GetSequenceExpression(seqStringAsArray.SeparatorExpr, source) + "))";
        }

        private string GetSequenceExpressionMapDomain(SequenceExpressionMapDomain seqMapDomain, SourceBuilder source)
        {
            string container = GetContainerValue(seqMapDomain, source);

            if(seqMapDomain.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Domain((IDictionary)(" + container + "))";
            }
            else //if(seqMapDomain.ContainerType(env).StartsWith("map<"))
            {
                string mapType = TypesHelper.XgrsTypeToCSharpType(seqMapDomain.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Domain((" + mapType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionMapRange(SequenceExpressionMapRange seqMapRange, SourceBuilder source)
        {
            string container = GetContainerValue(seqMapRange, source);

            if(seqMapRange.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Range((IDictionary)(" + container + "))";
            }
            else //if(seqMapDomain.ContainerType(env).StartsWith("map<"))
            {
                string mapType = TypesHelper.XgrsTypeToCSharpType(seqMapRange.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Range((" + mapType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("fillFromSequence_" + seqConstr.Id);
            sb.Append("(");
            for(int i = 0; i < seqConstr.ContainerItems.Length; ++i)
            {
                if(i > 0)
                    sb.Append(", ");
                sb.Append("(");
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
                sb.Append(")");
                sb.Append("(");
                sb.Append(GetSequenceExpression(seqConstr.ContainerItems[i], source));
                sb.Append(")");
            }
            sb.Append(")");
            return sb.ToString();
        }

        private string GetSequenceExpressionMapConstructor(SequenceExpressionMapConstructor seqConstr, SourceBuilder source)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("fillFromSequence_" + seqConstr.Id);
            sb.Append("(");
            for(int i = 0; i < seqConstr.ContainerItems.Length; ++i)
            {
                if(i > 0)
                    sb.Append(", ");
                sb.Append("(");
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.KeyType, model));
                sb.Append(")");
                sb.Append("(");
                sb.Append(GetSequenceExpression(seqConstr.MapKeyItems[i], source));
                sb.Append("), ");
                sb.Append("(");
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqConstr.ValueType, model));
                sb.Append(")");
                sb.Append("(");
                sb.Append(GetSequenceExpression(seqConstr.ContainerItems[i], source));
                sb.Append(")");
            }
            sb.Append(")");
            return sb.ToString();
        }

        private string GetSequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf seqIndexOf, SourceBuilder source)
        {
            string container = GetContainerValue(seqIndexOf, source);

            if(seqIndexOf.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.IndexOf(" + container + ", " 
                    + GetSequenceExpression(seqIndexOf.ValueToSearchForExpr, source)
                    + (seqIndexOf.StartPositionExpr != null ? "," + GetSequenceExpression(seqIndexOf.StartPositionExpr, source) : "")
                    + ")";
            }
            else if(seqIndexOf.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqIndexOf.ContainerType(env), model);
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.IndexOf((" + arrayType + ")(" + container + "), "
                    + "(" + arrayValueType + ")(" + GetSequenceExpression(seqIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else if(seqIndexOf.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqIndexOf.ContainerType(env), model);
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.IndexOf((" + dequeType + ")(" + container + "), "
                    + "(" + dequeValueType + ")(" + GetSequenceExpression(seqIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else //if(seqIndexOf.Type(env)=="string")
            {
                return "((string)(" + container + ")).IndexOf("
                    + "(string)(" + GetSequenceExpression(seqIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqIndexOf.StartPositionExpr, source) + ")" : "")
                    + ", StringComparison.InvariantCulture"
                    + ")";
            }
        }

        private string GetSequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf seqLastIndexOf, SourceBuilder source)
        {
            string container = GetContainerValue(seqLastIndexOf, source);

            if(seqLastIndexOf.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf(" + container + ", "
                    + GetSequenceExpression(seqLastIndexOf.ValueToSearchForExpr, source)
                    + (seqLastIndexOf.StartPositionExpr != null ? "," + GetSequenceExpression(seqLastIndexOf.StartPositionExpr, source) : "")
                    + ")";
            }
            else if(seqLastIndexOf.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqLastIndexOf.ContainerType(env), model);
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqLastIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf((" + arrayType + ")(" + container + "), "
                    + "(" + arrayValueType + ")(" + GetSequenceExpression(seqLastIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqLastIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqLastIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else if(seqLastIndexOf.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqLastIndexOf.ContainerType(env), model);
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqLastIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf((" + dequeType + ")(" + container + "), "
                    + "(" + dequeValueType + ")(" + GetSequenceExpression(seqLastIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqLastIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqLastIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else //if(seqIndexOf.Type(env)=="string")
            {
                return "((string)(" + container + ")).LastIndexOf("
                    + "(string)(" + GetSequenceExpression(seqLastIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqLastIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqLastIndexOf.StartPositionExpr, source) + ")" : "")
                    + ", StringComparison.InvariantCulture"
                    + ")";
            }
        }

        private string GetSequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered seqArrayIndexOfOrdered, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayIndexOfOrdered, source);

            if(seqArrayIndexOfOrdered.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.IndexOfOrdered((IList)(" + container + "), "
                    + GetSequenceExpression(seqArrayIndexOfOrdered.ValueToSearchForExpr, source)
                    + ")";
            }
            else //if(seqArraySum.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayIndexOfOrdered.ContainerType(env), model);
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayIndexOfOrdered.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.IndexOfOrdered((" + arrayType + ")(" + container + "), "
                    + "(" + arrayValueType + ")(" + GetSequenceExpression(seqArrayIndexOfOrdered.ValueToSearchForExpr, source) + ")"
                    + ")";
            }
        }

        private string GetSequenceExpressionArraySum(SequenceExpressionArraySum seqArraySum, SourceBuilder source)
        {
            string container = GetContainerValue(seqArraySum, source);

            if(seqArraySum.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Sum((IList)(" + container + "))";
            }
            else //if(seqArraySum.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArraySum.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Sum((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayProd(SequenceExpressionArrayProd seqArrayProd, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayProd, source);

            if(seqArrayProd.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Prod((IList)(" + container + "))";
            }
            else //if(seqArrayProd.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayProd.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Prod((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayMin(SequenceExpressionArrayMin seqArrayMin, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayMin, source);

            if(seqArrayMin.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Min((IList)(" + container + "))";
            }
            else //if(seqArrayMin.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayMin.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Min((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayMax(SequenceExpressionArrayMax seqArrayMax, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayMax, source);

            if(seqArrayMax.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Max((IList)(" + container + "))";
            }
            else //if(seqArrayMax.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayMax.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Max((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayAvg(SequenceExpressionArrayAvg seqArrayAvg, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayAvg, source);

            if(seqArrayAvg.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Avg((IList)(" + container + "))";
            }
            else //if(seqArrayAvg.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayAvg.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Avg((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayMed(SequenceExpressionArrayMed seqArrayMed, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayMed, source);

            if(seqArrayMed.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Med((IList)(" + container + "))";
            }
            else //if(seqArrayMed.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayMed.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Med((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered seqArrayMedUnordered, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayMedUnordered, source);

            if(seqArrayMedUnordered.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.MedUnordered((IList)(" + container + "))";
            }
            else //if(seqArrayMedUnordered.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayMedUnordered.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.MedUnordered((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayVar(SequenceExpressionArrayVar seqArrayVar, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayVar, source);

            if(seqArrayVar.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Var((IList)(" + container + "))";
            }
            else //if(seqArrayVar.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayVar.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Var((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayDev(SequenceExpressionArrayDev seqArrayDev, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayDev, source);

            if(seqArrayDev.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Dev((IList)(" + container + "))";
            }
            else //if(seqArrayDev.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayDev.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Dev((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayAnd(SequenceExpressionArrayAnd seqArrayAnd, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayAnd, source);

            if(seqArrayAnd.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.And((IList)(" + container + "))";
            }
            else //if(seqArrayAnd.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayAnd.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.And((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayOr(SequenceExpressionArrayOr seqArrayOr, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOr, source);

            if(seqArrayOr.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Or((IList)(" + container + "))";
            }
            else //if(seqArrayOr.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOr.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Or((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet seqArrayOrDequeAsSet, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOrDequeAsSet, source);

            if(seqArrayOrDequeAsSet.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayOrDequeAsSet(" + container + ")";
            }
            else if(seqArrayOrDequeAsSet.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeAsSet.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsSet((" + arrayType + ")(" + container + "))";
            }
            else //if(seqArrayOrDequeAsSet.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeAsSet.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.DequeAsSet((" + dequeType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap seqArrayAsMap, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayAsMap, source);

            if(seqArrayAsMap.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsMap((IList)(" + container + "))";
            }
            else //if(seqArrayAsMap.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayAsMap.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsMap((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque seqArrayAsDeque, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayAsDeque, source);

            if(seqArrayAsDeque.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsDeque((IList)(" + container + "))";
            }
            else //if(seqArrayAsDeque.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayAsDeque.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsDeque((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayAsString(SequenceExpressionArrayAsString seqArrayAsString, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayAsString, source);

            if(seqArrayAsString.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsString((IList)(" + container + "),"
                    + GetSequenceExpression(seqArrayAsString.Separator, source) + ")";
            }
            else //if(seqArrayAsString.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayAsString.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayAsString((" + arrayType + ")(" + container + ")," 
                    + GetSequenceExpression(seqArrayAsString.Separator, source) + ")";
            }
        }

        private string GetSequenceExpressionArraySubarray(SequenceExpressionArraySubarray seqArraySubarray, SourceBuilder source)
        {
            string container = GetContainerValue(seqArraySubarray, source);

            if(seqArraySubarray.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Subarray((IList)(" + container + "),"
                    + GetSequenceExpression(seqArraySubarray.Start, source) + ","
                    + GetSequenceExpression(seqArraySubarray.Length, source) + ")";
            }
            else //if(seqArrayDev.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArraySubarray.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Subarray((" + arrayType + ")(" + container + "),"
                    + GetSequenceExpression(seqArraySubarray.Start, source) + ","
                    + GetSequenceExpression(seqArraySubarray.Length, source) + ")";
            }
        }

        private string GetSequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque seqDequeSubdeque, SourceBuilder source)
        {
            string container = GetContainerValue(seqDequeSubdeque, source);

            if(seqDequeSubdeque.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Subdeque((IDeque)(" + container + "),"
                    + GetSequenceExpression(seqDequeSubdeque.Start, source) + ","
                    + GetSequenceExpression(seqDequeSubdeque.Length, source) + ")";
            }
            else //if(seqArrayDev.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqDequeSubdeque.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Subdeque((" + dequeType + ")(" + container + "),"
                    + GetSequenceExpression(seqDequeSubdeque.Start, source) + ","
                    + GetSequenceExpression(seqDequeSubdeque.Length, source) + ")";
            }
        }

        private string GetSequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending seqArrayOrderAscending, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOrderAscending, source);

            if(seqArrayOrderAscending.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayOrderAscending((IList)(" + container + "))";
            }
            else //if(seqArrayOrderAscending.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrderAscending.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayOrderAscending((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending seqArrayOrderDescending, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOrderDescending, source);

            if(seqArrayOrderDescending.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayOrderDescending((IList)(" + container + "))";
            }
            else //if(seqArrayOrderDescending.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrderDescending.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayOrderDescending((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayGroup(SequenceExpressionArrayGroup seqArrayGroup, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayGroup, source);

            if(seqArrayGroup.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayGroup((IList)(" + container + "))";
            }
            else //if(seqArrayGroup.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayGroup.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayGroup((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach seqArrayKeepOneForEach, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayKeepOneForEach, source);

            if(seqArrayKeepOneForEach.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayKeepOneForEach((IList)(" + container + "))";
            }
            else //if(seqArrayKeepOneForEach.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayKeepOneForEach.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayKeepOneForEach((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayReverse(SequenceExpressionArrayReverse seqArrayReverse, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayReverse, source);

            if(seqArrayReverse.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayReverse((IList)(" + container + "))";
            }
            else //if(seqArrayReverse.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayReverse.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.ArrayReverse((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle seqArrayShuffle, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayShuffle, source);

            if(seqArrayShuffle.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.Shuffle((IList)(" + container + "))";
            }
            else //if(seqArrayShuffle.ContainerType(env).StartsWith("array"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayShuffle.ContainerType(env), model);
                return "GRGEN_LIBGR.ContainerHelper.Shuffle((" + arrayType + ")(" + container + "))";
            }
        }

        private string GetSequenceExpressionArrayExtract(SequenceExpressionArrayExtract seqArrayExtract, SourceBuilder source)
        {
            string array = GetContainerValue(seqArrayExtract, source);

            if(seqArrayExtract.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.ArrayExtract(" + array + ", \"" + seqArrayExtract.memberOrAttributeName + "\", procEnv)";
            }
            else //if(seqArrayExtract.ContainerType(env).StartsWith("array"))
            {
                string arrayType = seqArrayExtract.ContainerType(env);
                string arrayValueType = TypesHelper.ExtractSrc(arrayType);
                string typeOfMemberOrAttribute = env.TypeOfMemberOrAttribute(arrayValueType, seqArrayExtract.memberOrAttributeName);
                if(arrayValueType.StartsWith("match<class ")) // match class type
                {
                    string member = seqArrayExtract.memberOrAttributeName;
                    string packageName;
                    string matchClassName = TypesHelper.SeparatePackage(TypesHelper.GetMatchClassName(arrayValueType), out packageName);
                    string matchClass = NamesOfEntities.MatchClassName(matchClassName, packageName);
                    return "GRGEN_ACTIONS." + matchClass + ".Extractor.Extract_" + member +"(" + array+ ")";
                }
                else if(arrayValueType.StartsWith("match<")) //match type
                {
                    string member = seqArrayExtract.memberOrAttributeName;
                    string packageName;
                    string ruleName = TypesHelper.SeparatePackage(TypesHelper.GetRuleName(arrayValueType), out packageName);
                    string ruleClass = NamesOfEntities.RulePatternClassName(ruleName, packageName, false);
                    return "GRGEN_ACTIONS." + ruleClass + ".Extractor.Extract_" + member + "(" + array +")";
                }
                else // node/edge type
                {
                    string attribute = seqArrayExtract.memberOrAttributeName;
                    string packageName;
                    string graphElementTypeName = TypesHelper.SeparatePackage(arrayValueType, out packageName);
                    string arrayHelperClass = NamesOfEntities.ArrayHelperClassName(graphElementTypeName, packageName, attribute);
                    return "GRGEN_MODEL." + arrayHelperClass +".Extract(" + array + ")";
                }
            }
        }

        private string GetSequenceExpressionArrayMap(SequenceExpressionArrayMap seqArrayMap, SourceBuilder source)
        {
            String arrayMapMethodName = "ArrayMap_" + seqArrayMap.Id.ToString();

            String arrayInputType = seqArrayMap.ContainerType(env) == "" ?
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayMap.ContainerExpr.Type(env), model);
            String elementInputType = seqArrayMap.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayMap.ContainerExpr.Type(env)), model);
            String elementOutputType = TypesHelper.XgrsTypeToCSharpType(seqArrayMap.TypeName, model);
            String arrayOutputType = TypesHelper.XgrsTypeToCSharpType("array<" + seqArrayMap.TypeName + ">", model);

            SourceBuilder sb = new SourceBuilder();
            sb.AppendFrontFormat("{0}(procEnv", arrayMapMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayMap.MappingExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", ({0}){1}", arrayInputType, GetContainerValue(seqArrayMap, source));

            if(seqArrayMap.ArrayAccess != null)
                variables.Remove(seqArrayMap.ArrayAccess);
            if(seqArrayMap.Index != null)
                variables.Remove(seqArrayMap.Index);
            variables.Remove(seqArrayMap.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")");

            GenerateSequenceExpressionArrayMap(seqArrayMap);

            return sb.ToString();
        }

        private void GenerateSequenceExpressionArrayMap(SequenceExpressionArrayMap seqArrayMap)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String arrayMapMethodName = "ArrayMap_" + seqArrayMap.Id.ToString();

            string sourceVar = "source";
            string targetVar = "target";
            string resultVar = "result";

            String arrayInputType = seqArrayMap.ContainerType(env) == "" ? 
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayMap.ContainerExpr.Type(env), model);
            String elementInputType = seqArrayMap.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayMap.ContainerExpr.Type(env)), model);
            String elementOutputType = TypesHelper.XgrsTypeToCSharpType(seqArrayMap.TypeName, model);
            String arrayOutputType = TypesHelper.XgrsTypeToCSharpType("array<" + seqArrayMap.TypeName + ">", model);

            sb.AppendFront("static " + arrayOutputType + " " + arrayMapMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayMap.MappingExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} {1}", arrayInputType, sourceVar);

            if(seqArrayMap.ArrayAccess != null)
                variables.Remove(seqArrayMap.ArrayAccess);
            if(seqArrayMap.Index != null)
                variables.Remove(seqArrayMap.Index);
            variables.Remove(seqArrayMap.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", arrayOutputType, targetVar);

            if(seqArrayMap.ArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.ArrayAccess));
                sb.AppendFront(seqHelper.SetVar(seqArrayMap.ArrayAccess, sourceVar));
            }

            String indexVar = "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            if(seqArrayMap.Index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.Index));
                sb.AppendFront(seqHelper.SetVar(seqArrayMap.Index, indexVar));
            }
            sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.Var));
            sb.AppendFront(seqHelper.SetVar(seqArrayMap.Var, sourceVar + "[" + indexVar + "]"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(seqArrayMap.MappingExpr, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} {1} = {2};\n", elementOutputType, resultVar, seqExpr);
            sb.AppendFrontFormat("{0}.Add({1});\n", targetVar, resultVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private string GetSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqArrayRemoveIf, SourceBuilder source)
        {
            String arrayRemoveIfMethodName = "ArrayRemoveIf_" + seqArrayRemoveIf.Id.ToString();

            String arrayType = seqArrayRemoveIf.ContainerType(env) == "" ?
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayRemoveIf.ContainerExpr.Type(env), model);
            String elementType = seqArrayRemoveIf.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayRemoveIf.ContainerExpr.Type(env)), model);

            SourceBuilder sb = new SourceBuilder();
            sb.AppendFrontFormat("{0}(procEnv", arrayRemoveIfMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayRemoveIf.ConditionExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", ({0}){1}", arrayType, GetContainerValue(seqArrayRemoveIf, source));

            if(seqArrayRemoveIf.ArrayAccess != null)
                variables.Remove(seqArrayRemoveIf.ArrayAccess);
            if(seqArrayRemoveIf.Index != null)
                variables.Remove(seqArrayRemoveIf.Index);
            variables.Remove(seqArrayRemoveIf.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")");

            GenerateSequenceExpressionArrayRemoveIf(seqArrayRemoveIf);

            return sb.ToString();
        }

        private void GenerateSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqArrayRemoveIf)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String arrayRemoveIfMethodName = "ArrayRemoveIf_" + seqArrayRemoveIf.Id.ToString();

            string sourceVar = "source";
            string targetVar = "target";

            String arrayType = seqArrayRemoveIf.ContainerType(env) == "" ?
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayRemoveIf.ContainerExpr.Type(env), model);
            String elementType = seqArrayRemoveIf.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayRemoveIf.ContainerExpr.Type(env)), model);

            sb.AppendFront("static " + arrayType + " " + arrayRemoveIfMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayRemoveIf.ConditionExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} {1}", arrayType, sourceVar);

            if(seqArrayRemoveIf.ArrayAccess != null)
                variables.Remove(seqArrayRemoveIf.ArrayAccess);
            if(seqArrayRemoveIf.Index != null)
                variables.Remove(seqArrayRemoveIf.Index);
            variables.Remove(seqArrayRemoveIf.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", arrayType, targetVar);

            if(seqArrayRemoveIf.ArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayRemoveIf.ArrayAccess));
                sb.AppendFront(seqHelper.SetVar(seqArrayRemoveIf.ArrayAccess, "source"));
            }

            String indexVar = "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            if(seqArrayRemoveIf.Index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayRemoveIf.Index));
                sb.AppendFront(seqHelper.SetVar(seqArrayRemoveIf.Index, indexVar));
            }
            sb.AppendFront(seqHelper.DeclareVar(seqArrayRemoveIf.Var));
            sb.AppendFront(seqHelper.SetVar(seqArrayRemoveIf.Var, sourceVar + "[" + indexVar + "]"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(seqArrayRemoveIf.ConditionExpr, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFront("if(!(bool)(");
            sb.Append(seqExpr);
            sb.Append("))\n");
            sb.AppendFrontIndentedFormat("{0}.Add({1}[{2}]);\n", targetVar, sourceVar, indexVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private string GetSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqArrayMap, SourceBuilder source)
        {
            String arrayMapMethodName = "ArrayMapStartWithAccumulateBy_" + seqArrayMap.Id.ToString();

            String arrayInputType = seqArrayMap.ContainerType(env) == "" ?
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayMap.ContainerExpr.Type(env), model);
            String elementInputType = seqArrayMap.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayMap.ContainerExpr.Type(env)), model);
            String elementOutputType = TypesHelper.XgrsTypeToCSharpType(seqArrayMap.TypeName, model);
            String arrayOutputType = TypesHelper.XgrsTypeToCSharpType("array<" + seqArrayMap.TypeName + ">", model);

            SourceBuilder sb = new SourceBuilder();
            sb.AppendFrontFormat("{0}(procEnv", arrayMapMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayMap.MappingExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            seqArrayMap.InitExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", ({0}){1}", arrayInputType, GetContainerValue(seqArrayMap, source));

            if(seqArrayMap.InitArrayAccess != null)
                variables.Remove(seqArrayMap.InitArrayAccess);
            if(seqArrayMap.ArrayAccess != null)
                variables.Remove(seqArrayMap.ArrayAccess);
            variables.Remove(seqArrayMap.PreviousAccumulationAccess);
            if(seqArrayMap.Index != null)
                variables.Remove(seqArrayMap.Index);
            variables.Remove(seqArrayMap.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")");

            GenerateSequenceExpressionArrayMapStartWithAccumulateBy(seqArrayMap);

            return sb.ToString();
        }

        private void GenerateSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqArrayMap)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String arrayMapMethodName = "ArrayMapStartWithAccumulateBy_" + seqArrayMap.Id.ToString();

            string sourceVar = "source";
            string targetVar = "target";
            string resultVar = "result";

            String arrayInputType = seqArrayMap.ContainerType(env) == "" ?
                "IList" : TypesHelper.XgrsTypeToCSharpType(seqArrayMap.ContainerExpr.Type(env), model);
            String elementInputType = seqArrayMap.ContainerType(env) == "" ?
                "object" : TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayMap.ContainerExpr.Type(env)), model);
            String elementOutputType = TypesHelper.XgrsTypeToCSharpType(seqArrayMap.TypeName, model);
            String arrayOutputType = TypesHelper.XgrsTypeToCSharpType("array<" + seqArrayMap.TypeName + ">", model);

            sb.AppendFront("static " + arrayOutputType + " " + arrayMapMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            seqArrayMap.MappingExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            seqArrayMap.InitExpr.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} {1}", arrayInputType, sourceVar);

            if(seqArrayMap.InitArrayAccess != null)
                variables.Remove(seqArrayMap.InitArrayAccess);
            if(seqArrayMap.ArrayAccess != null)
                variables.Remove(seqArrayMap.ArrayAccess);
            variables.Remove(seqArrayMap.PreviousAccumulationAccess);
            if(seqArrayMap.Index != null)
                variables.Remove(seqArrayMap.Index);
            variables.Remove(seqArrayMap.Var);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            sb.AppendFrontFormat("{0} {1} = new {0}();\n", arrayOutputType, targetVar);

            if(seqArrayMap.InitArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.InitArrayAccess));
                sb.AppendFront(seqHelper.SetVar(seqArrayMap.InitArrayAccess, sourceVar));
            }

            if(seqArrayMap.ArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.ArrayAccess));
                sb.AppendFront(seqHelper.SetVar(seqArrayMap.ArrayAccess, sourceVar));
            }

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(seqArrayMap.InitExpr, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.PreviousAccumulationAccess));
            sb.AppendFront(seqHelper.SetVar(seqArrayMap.PreviousAccumulationAccess, seqExpr));

            String indexVar = "index";
            sb.AppendFrontFormat("for(int {0} = 0; {0} < {1}.Count; ++{0})\n", indexVar, sourceVar);
            sb.AppendFront("{\n");
            sb.Indent();

            if(seqArrayMap.Index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.Index));
                sb.AppendFront(seqHelper.SetVar(seqArrayMap.Index, indexVar));
            }
            sb.AppendFront(seqHelper.DeclareVar(seqArrayMap.Var));
            sb.AppendFront(seqHelper.SetVar(seqArrayMap.Var, sourceVar + "[" + indexVar + "]"));

            declarations = new SourceBuilder();
            seqExpr = GetSequenceExpression(seqArrayMap.MappingExpr, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} {1} = {2};\n", elementOutputType, resultVar, seqExpr);
            sb.AppendFrontFormat("{0}.Add({1});\n", targetVar, resultVar);

            sb.AppendFront(seqHelper.SetVar(seqArrayMap.PreviousAccumulationAccess, resultVar));

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("return {0};\n", targetVar);

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private string GetSequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy seqArrayOrderAscendingBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccess(seqArrayOrderAscendingBy, "orderAscendingBy", "OrderAscendingBy", source);
        }

        private string GetSequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy seqArrayOrderDescendingBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccess(seqArrayOrderDescendingBy, "orderDescendingBy", "OrderDescendingBy", source);
        }

        private string GetSequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy seqArrayOrderGroupBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccess(seqArrayOrderGroupBy, "groupBy", "GroupBy", source);
        }

        private string GetSequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy seqArrayKeepOneForEachBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccess(seqArrayKeepOneForEachBy, "keepOneForEachBy", "KeepOneForEachBy", source);
        }

        private string GetSequenceExpressionArrayByAttributeAccess(SequenceExpressionArrayByAttributeAccess seqArrayByAttributeAccess, 
            String methodName, String methodNameUpperCase, SourceBuilder source)
        {
            string array = GetContainerValue(seqArrayByAttributeAccess, source);

            if(seqArrayByAttributeAccess.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper." + methodNameUpperCase + "(" + array + ", \"" + seqArrayByAttributeAccess.memberOrAttributeName + "\", procEnv)";
            }
            else //if(seqArrayOrderAscendingBy.ContainerType(env).StartsWith("array"))
            {
                string arrayType = seqArrayByAttributeAccess.ContainerType(env);
                string arrayValueType = TypesHelper.ExtractSrc(arrayType);
                string typeOfMemberOrAttribute = env.TypeOfMemberOrAttribute(arrayValueType, seqArrayByAttributeAccess.memberOrAttributeName);
                if(arrayValueType.StartsWith("match<class ")) // match class type
                {
                    string member = seqArrayByAttributeAccess.memberOrAttributeName;
                    string packageName;
                    string matchClassName = TypesHelper.SeparatePackage(TypesHelper.GetMatchClassName(arrayValueType), out packageName);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    return "GRGEN_ACTIONS." + packagePrefix + "ArrayHelper.Array_" + matchClassName + "_" + methodName + "_" + member
                        + "((List<GRGEN_ACTIONS." + packagePrefix + "IMatch_" + matchClassName + ">)" + array + ")";
                }
                else if(arrayValueType.StartsWith("match<")) //match type
                {
                    string member = seqArrayByAttributeAccess.memberOrAttributeName;
                    string packageName;
                    string ruleName = TypesHelper.SeparatePackage(TypesHelper.GetRuleName(arrayValueType), out packageName);
                    string ruleClass = NamesOfEntities.RulePatternClassName(ruleName, packageName, false);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    return "GRGEN_ACTIONS." + packagePrefix + "ArrayHelper.Array_" + ruleName + "_" + methodName + "_" + member
                        + "((List<GRGEN_ACTIONS." + packagePrefix + ruleClass + ".IMatch_" + ruleName + ">)" + array + ")";
                }
                else // node/edge type
                {
                    string attribute = seqArrayByAttributeAccess.memberOrAttributeName;
                    string packageName;
                    string graphElementTypeName = TypesHelper.SeparatePackage(arrayValueType, out packageName);
                    string graphElementTypeInterfaceName = "I" + graphElementTypeName;
                    string arrayHelperClass = NamesOfEntities.ArrayHelperClassName(graphElementTypeName, packageName, attribute);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    return "GRGEN_MODEL." + arrayHelperClass + ".Array" + methodNameUpperCase
                        + "((List<GRGEN_MODEL." + packagePrefix + graphElementTypeInterfaceName + ">)" + array + ")";
                }
            }
        }

        private string GetSequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy seqArrayIndexOfBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccessIndexOf(seqArrayIndexOfBy, "indexOfBy", "IndexOfBy", seqArrayIndexOfBy.ValueToSearchForExpr, seqArrayIndexOfBy.StartPositionExpr, source);
        }

        private string GetSequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy seqArrayLastIndexOfBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccessIndexOf(seqArrayLastIndexOfBy, "lastIndexOfBy", "LastIndexOfBy", seqArrayLastIndexOfBy.ValueToSearchForExpr, seqArrayLastIndexOfBy.StartPositionExpr, source);
        }

        private string GetSequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy seqArrayIndexOfOrderedBy, SourceBuilder source)
        {
            return GetSequenceExpressionArrayByAttributeAccessIndexOf(seqArrayIndexOfOrderedBy, "indexOfOrderedBy", "IndexOfOrderedBy", seqArrayIndexOfOrderedBy.ValueToSearchForExpr, null, source);
        }

        private string GetSequenceExpressionArrayByAttributeAccessIndexOf(SequenceExpressionArrayByAttributeAccessIndexOf seqArrayByAttributeAccess,
            String methodName, String methodNameUpperCase, SequenceExpression valueToSearchForExpr, SequenceExpression startPositionExpr, SourceBuilder source)
        {
            string array = GetContainerValue(seqArrayByAttributeAccess, source);

            if(seqArrayByAttributeAccess.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper." + methodNameUpperCase + "(" + array + ", \"" + seqArrayByAttributeAccess.memberOrAttributeName + "\", procEnv)";
            }
            else //if(seqArrayOrderAscendingBy.ContainerType(env).StartsWith("array"))
            {
                SourceBuilder sb = new SourceBuilder();
                string arrayType = seqArrayByAttributeAccess.ContainerType(env);
                string arrayValueType = TypesHelper.ExtractSrc(arrayType);
                string typeOfMemberOrAttribute = env.TypeOfMemberOrAttribute(arrayValueType, seqArrayByAttributeAccess.memberOrAttributeName);
                if(arrayValueType.StartsWith("match<class ")) // match class type
                {
                    string member = seqArrayByAttributeAccess.memberOrAttributeName;
                    string memberType = TypesHelper.XgrsTypeToCSharpType(seqArrayByAttributeAccess.ValueToSearchForExpr.Type(env), model);
                    string packageName;
                    string matchClassName = TypesHelper.SeparatePackage(TypesHelper.GetMatchClassName(arrayValueType), out packageName);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    sb.Append("GRGEN_ACTIONS." + packagePrefix + "ArrayHelper.Array_" + matchClassName + "_" + methodName + "_" + member
                        + "((List<GRGEN_ACTIONS." + packagePrefix + "IMatch_" + matchClassName + ">)" + array);
                    sb.Append(", (" + memberType + ")");
                }
                else if(arrayValueType.StartsWith("match<")) //match type
                {
                    string member = seqArrayByAttributeAccess.memberOrAttributeName;
                    string memberType = TypesHelper.XgrsTypeToCSharpType(seqArrayByAttributeAccess.ValueToSearchForExpr.Type(env), model);
                    string packageName;
                    string ruleName = TypesHelper.SeparatePackage(TypesHelper.GetRuleName(arrayValueType), out packageName);
                    string ruleClass = NamesOfEntities.RulePatternClassName(ruleName, packageName, false);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    sb.Append("GRGEN_ACTIONS." + packagePrefix + "ArrayHelper.Array_" + ruleName + "_" + methodName + "_" + member
                        + "((List<GRGEN_ACTIONS." + packagePrefix + ruleClass + ".IMatch_" + ruleName + ">)" + array);
                    sb.Append(", (" + memberType + ")");
                }
                else // node/edge type
                {
                    string attribute = seqArrayByAttributeAccess.memberOrAttributeName;
                    string attributeType = TypesHelper.XgrsTypeToCSharpType(seqArrayByAttributeAccess.ValueToSearchForExpr.Type(env), model);
                    string packageName;
                    string graphElementTypeName = TypesHelper.SeparatePackage(arrayValueType, out packageName);
                    string graphElementTypeInterfaceName = "I" + graphElementTypeName;
                    string arrayHelperClass = NamesOfEntities.ArrayHelperClassName(graphElementTypeName, packageName, attribute);
                    string packagePrefix = packageName != null ? packageName + "." : "";
                    sb.Append("GRGEN_MODEL." + arrayHelperClass + ".Array" + methodNameUpperCase
                        + "((List<GRGEN_MODEL." + packagePrefix + graphElementTypeInterfaceName + ">)" + array);
                    sb.Append(", (" + attributeType + ")");
                }
                sb.Append(GetSequenceExpression(valueToSearchForExpr, source));
                if(startPositionExpr != null)
                    sb.Append(", (int)" + GetSequenceExpression(startPositionExpr, source));
                sb.Append(")");
                return sb.ToString();
            }
        }

        private string GetContainerValue(SequenceExpressionContainer container, SourceBuilder source)
        {
            if(container.ContainerExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess attribute = (SequenceExpressionAttributeAccess)container.ContainerExpr;
                return "((GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(attribute.Source, source) + ")" + ".GetAttribute(\"" + attribute.AttributeName + "\")";
            }
            else
                return GetSequenceExpression(container.ContainerExpr, source);
        }

        private string GetSequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor seqConstr, SourceBuilder source)
        {
            return "new " + "Match_" + seqConstr.ConstructedType + "()";
        }

        private string GetSequenceExpressionMathMin(SequenceExpressionMathMin seqMathMin, SourceBuilder source)
        {
            if(seqMathMin.Left.Type(env) == "byte")
                return "Math.Min((SByte)" + GetSequenceExpression(seqMathMin.Left, source) + ", (SByte)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else if(seqMathMin.Left.Type(env) == "short")
                return "Math.Min((Int16)" + GetSequenceExpression(seqMathMin.Left, source) + ", (Int16)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else if(seqMathMin.Left.Type(env) == "int")
                return "Math.Min((Int32)" + GetSequenceExpression(seqMathMin.Left, source) + ", (Int32)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else if(seqMathMin.Left.Type(env) == "long")
                return "Math.Min((Int64)" + GetSequenceExpression(seqMathMin.Left, source) + ", (Int64)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else if(seqMathMin.Left.Type(env) == "float")
                return "Math.Min((Single)" + GetSequenceExpression(seqMathMin.Left, source) + ", (Single)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else if(seqMathMin.Left.Type(env) == "double")
                return "Math.Min((Double)" + GetSequenceExpression(seqMathMin.Left, source) + ", (Double)" + GetSequenceExpression(seqMathMin.Right, source) + ")";
            else //if(seqMathMin.Left.Type(env) == "")
                return "GRGEN_LIBGR.MathHelper.Min(" + GetSequenceExpression(seqMathMin.Left, source) + ", " + GetSequenceExpression(seqMathMin.Right, source) + ")";
        }

        private string GetSequenceExpressionMathMax(SequenceExpressionMathMax seqMathMax, SourceBuilder source)
        {
            if(seqMathMax.Left.Type(env) == "byte")
                return "Math.Max((SByte)" + GetSequenceExpression(seqMathMax.Left, source) + ", (SByte)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else if(seqMathMax.Left.Type(env) == "short")
                return "Math.Max((Int16)" + GetSequenceExpression(seqMathMax.Left, source) + ", (Int16)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else if(seqMathMax.Left.Type(env) == "int")
                return "Math.Max((Int32)" + GetSequenceExpression(seqMathMax.Left, source) + ", (Int32)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else if(seqMathMax.Left.Type(env) == "long")
                return "Math.Max((Int64)" + GetSequenceExpression(seqMathMax.Left, source) + ", (Int64)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else if(seqMathMax.Left.Type(env) == "float")
                return "Math.Max((Single)" + GetSequenceExpression(seqMathMax.Left, source) + ", (Single)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else if(seqMathMax.Left.Type(env) == "double")
                return "Math.Max((Double)" + GetSequenceExpression(seqMathMax.Left, source) + ", (Double)" + GetSequenceExpression(seqMathMax.Right, source) + ")";
            else //if(seqMathMax.Left.Type(env) == "")
                return "GRGEN_LIBGR.MathHelper.Max(" + GetSequenceExpression(seqMathMax.Left, source) + ", " + GetSequenceExpression(seqMathMax.Right, source) + ")";
        }

        private string GetSequenceExpressionMathAbs(SequenceExpressionMathAbs seqMathAbs, SourceBuilder source)
        {
            if(seqMathAbs.Argument.Type(env) == "byte")
                return "Math.Abs((SByte)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else if(seqMathAbs.Argument.Type(env) == "short")
                return "Math.Abs((Int16)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else if(seqMathAbs.Argument.Type(env) == "int")
                return "Math.Abs((Int32)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else if(seqMathAbs.Argument.Type(env) == "long")
                return "Math.Abs((Int64)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else if(seqMathAbs.Argument.Type(env) == "float")
                return "Math.Abs((Single)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else if(seqMathAbs.Argument.Type(env) == "double")
                return "Math.Abs((Double)" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
            else //if(seqMathAbs.Argument.Type(env) == "")
                return "GRGEN_LIBGR.MathHelper.Abs(" + GetSequenceExpression(seqMathAbs.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathCeil(SequenceExpressionMathCeil seqMathCeil, SourceBuilder source)
        {
            return "Math.Ceiling((double)" + GetSequenceExpression(seqMathCeil.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathFloor(SequenceExpressionMathFloor seqMathFloor, SourceBuilder source)
        {
            return "Math.Floor((double)" + GetSequenceExpression(seqMathFloor.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathRound(SequenceExpressionMathRound seqMathRound, SourceBuilder source)
        {
            return "Math.Round((double)" + GetSequenceExpression(seqMathRound.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathTruncate(SequenceExpressionMathTruncate seqMathTruncate, SourceBuilder source)
        {
            return "Math.Truncate((double)" + GetSequenceExpression(seqMathTruncate.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathSqr(SequenceExpressionMathSqr seqMathSqr, SourceBuilder source)
        {
            return "GRGEN_LIBGR.MathHelper.Sqr((double)" + GetSequenceExpression(seqMathSqr.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathSqrt(SequenceExpressionMathSqrt seqMathSqrt, SourceBuilder source)
        {
            return "Math.Sqrt((double)" + GetSequenceExpression(seqMathSqrt.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathPow(SequenceExpressionMathPow seqMathPow, SourceBuilder source)
        {
            if(seqMathPow.Left != null)
                return "Math.Pow((double)" + GetSequenceExpression(seqMathPow.Left, source) + ", (double)" + GetSequenceExpression(seqMathPow.Right, source) + ")";
            else
                return "Math.Exp((double)" + GetSequenceExpression(seqMathPow.Right, source) + ")";
        }

        private string GetSequenceExpressionMathLog(SequenceExpressionMathLog seqMathLog, SourceBuilder source)
        {
            if(seqMathLog.Right != null)
                return "Math.Log((double)" + GetSequenceExpression(seqMathLog.Left, source) + ", (double)" + GetSequenceExpression(seqMathLog.Right, source) + ")";
            else
                return "Math.Log((double)" + GetSequenceExpression(seqMathLog.Left, source) + ")";
        }

        private string GetSequenceExpressionMathSgn(SequenceExpressionMathSgn seqMathSgn, SourceBuilder source)
        {
            return "Math.Sign((double)" + GetSequenceExpression(seqMathSgn.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathSin(SequenceExpressionMathSin seqMathSin, SourceBuilder source)
        {
            return "Math.Sin((double)" + GetSequenceExpression(seqMathSin.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathCos(SequenceExpressionMathCos seqMathCos, SourceBuilder source)
        {
            return "Math.Cos((double)" + GetSequenceExpression(seqMathCos.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathTan(SequenceExpressionMathTan seqMathTan, SourceBuilder source)
        {
            return "Math.Tan((double)" + GetSequenceExpression(seqMathTan.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathArcSin(SequenceExpressionMathArcSin seqMathArcSin, SourceBuilder source)
        {
            return "Math.Asin((double)" + GetSequenceExpression(seqMathArcSin.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathArcCos(SequenceExpressionMathArcCos seqMathArcCos, SourceBuilder source)
        {
            return "Math.Acos((double)" + GetSequenceExpression(seqMathArcCos.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathArcTan(SequenceExpressionMathArcTan seqMathArcTan, SourceBuilder source)
        {
            return "Math.Atan((double)" + GetSequenceExpression(seqMathArcTan.Argument, source) + ")";
        }

        private string GetSequenceExpressionMathPi(SequenceExpressionMathPi seqMathPi, SourceBuilder source)
        {
            return "Math.PI";
        }

        private string GetSequenceExpressionMathE(SequenceExpressionMathE seqMathE, SourceBuilder source)
        {
            return "Math.E";
        }

        private string GetSequenceExpressionMathByteMin(SequenceExpressionMathByteMin seqMathByteMin, SourceBuilder source)
        {
            return "SByte.MinValue";
        }

        private string GetSequenceExpressionMathByteMax(SequenceExpressionMathByteMax seqMathByteMax, SourceBuilder source)
        {
            return "SByte.MaxValue";
        }

        private string GetSequenceExpressionMathShortMin(SequenceExpressionMathShortMin seqMathShortMin, SourceBuilder source)
        {
            return "Int16.MinValue";
        }

        private string GetSequenceExpressionMathShortMax(SequenceExpressionMathShortMax seqMathShortMax, SourceBuilder source)
        {
            return "Int16.MaxValue";
        }

        private string GetSequenceExpressionMathIntMin(SequenceExpressionMathIntMin seqMathIntMin, SourceBuilder source)
        {
            return "Int32.MinValue";
        }

        private string GetSequenceExpressionMathIntMax(SequenceExpressionMathIntMax seqMathIntMax, SourceBuilder source)
        {
            return "Int32.MaxValue";
        }

        private string GetSequenceExpressionMathLongMin(SequenceExpressionMathLongMin seqMathLongMin, SourceBuilder source)
        {
            return "Int64.MinValue";
        }

        private string GetSequenceExpressionMathLongMax(SequenceExpressionMathLongMax seqMathLongMax, SourceBuilder source)
        {
            return "Int64.MaxValue";
        }

        private string GetSequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin seqMathFloatMin, SourceBuilder source)
        {
            return "Single.MinValue";
        }

        private string GetSequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax seqMathFloatMax, SourceBuilder source)
        {
            return "Single.MaxValue";
        }

        private string GetSequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin seqMathDoubleMin, SourceBuilder source)
        {
            return "Double.MinValue";
        }

        private string GetSequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax seqMathDoubleMax, SourceBuilder source)
        {
            return "Double.MaxValue";
        }

        #endregion Container expressions

        //-------------------------------------------------------------------------------------------------------------------

        #region Filters

        internal void EmitFilterCall(SourceBuilder source, SequenceFilterCallCompiled sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            if(sequenceFilterCall.Filter is IFilterAutoSupplied)
            {
                IFilterAutoSupplied filterAutoSupplied = (IFilterAutoSupplied)sequenceFilterCall.Filter;
                EmitFilterAutoSuppliedCall(source, filterAutoSupplied, sequenceFilterCall.ArgumentExpressions, matchesSource, chainable);
            }
            else if(sequenceFilterCall.Filter is IFilterAutoGenerated)
            {
                IFilterAutoGenerated filterAutoGenerated = (IFilterAutoGenerated)sequenceFilterCall.Filter;
                EmitFilterAutoGeneratedCall(source, filterAutoGenerated, patternName, matchesSource, chainable);
            }
            else
            {
                IFilterFunction filterFunction = (IFilterFunction)sequenceFilterCall.Filter;
                EmitFilterFunctionCall(source, filterFunction, sequenceFilterCall.ArgumentExpressions, matchesSource, chainable);
            }
        }

        private void EmitFilterAutoSuppliedCall(SourceBuilder source, IFilterAutoSupplied filterAutoSupplied, SequenceExpression[] argumentExpressions, string matchesSource, bool chainable)
        {
            source.AppendFrontFormat("{0}.FilterExact_{1}(", matchesSource, filterAutoSupplied.Name);
            bool first = true;
            for(int i = 0; i < argumentExpressions.Length; ++i)
            {
                if(first)
                    first = false;
                else
                    source.Append(", ");
                source.AppendFormat("({0})({1})",
                    TypesHelper.TypeName(filterAutoSupplied.Inputs[i]),
                    GetSequenceExpression(argumentExpressions[i], source));
            }
            source.Append(")");
            if(!chainable)
                source.Append(";\n");
        }

        private void EmitFilterAutoGeneratedCall(SourceBuilder source, IFilterAutoGenerated filterAutoGenerated, string patternName, string matchesSource, bool chainable)
        {
            source.AppendFrontFormat("{0}MatchFilters.Filter_{1}_{2}(procEnv, {3})",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterAutoGenerated.PackageOfApplyee),
                patternName, ((LGSPFilterAutoGenerated)filterAutoGenerated).NameWithUnderscoreSuffix, matchesSource);
            if(!chainable)
                source.Append(";\n");
        }

        private void EmitFilterFunctionCall(SourceBuilder source, IFilterFunction filterFunction, SequenceExpression[] argumentExpressions, string matchesSource, bool chainable)
        {
            source.AppendFrontFormat("{0}MatchFilters.Filter_{1}(procEnv, {2}",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchesSource);
            List<String> inputTypes = seqHelper.actionsTypeInformation.filterFunctionsToInputTypes[filterFunction.PackagePrefixedName];
            for(int i = 0; i < argumentExpressions.Length; ++i)
            {
                source.AppendFormat(", ({0})({1})",
                    TypesHelper.XgrsTypeToCSharpType(inputTypes[i], model),
                    GetSequenceExpression(argumentExpressions[i], source));
            }
            source.Append(")");
            if(!chainable)
                source.Append(";\n");
        }

        internal void EmitFilterCall(SourceBuilder source, SequenceFilterCallBase sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            if(sequenceFilterCall is SequenceFilterCallLambdaExpressionCompiled)
                EmitFilterCall(source, (SequenceFilterCallLambdaExpressionCompiled)sequenceFilterCall, patternName, matchesSource, packagePrefixedRuleName, chainable);
            else //if(sequenceFilterCall is SequenceFilterCallCompiled)
                EmitFilterCall(source, (SequenceFilterCallCompiled)sequenceFilterCall, patternName, matchesSource, packagePrefixedRuleName, chainable);
        }

        internal void EmitFilterCall(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            FilterCallWithLambdaExpression filterCall = sequenceFilterCall.FilterCall;
            if(filterCall.PlainName == "assign")
                EmitFilterCallAssign(source, sequenceFilterCall, patternName, matchesSource, packagePrefixedRuleName, chainable);
            else if(filterCall.PlainName == "removeIf")
                EmitFilterCallRemoveIf(source, sequenceFilterCall, patternName, matchesSource, packagePrefixedRuleName, chainable);
            else if(filterCall.PlainName == "assignStartWithAccumulateBy")
                EmitFilterCallAssignStartWithAccumulateBy(source, sequenceFilterCall, patternName, matchesSource, packagePrefixedRuleName, chainable);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void EmitFilterCallAssign(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            String filterAssignMethodName = "FilterAssign_" + sequenceFilterCall.Id.ToString();

            String arrayInputType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementInputType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);

            source.AppendFrontFormat("{0}(procEnv", filterAssignMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchesSource);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceFilterAssign(sequenceFilterCall, packagePrefixedRuleName);
        }

        public void EmitFilterCallRemoveIf(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            String filterRemoveIfMethodName = "FilterRemoveIf_" + sequenceFilterCall.Id.ToString();

            String arrayType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);

            source.AppendFrontFormat("{0}(procEnv", filterRemoveIfMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchesSource);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceFilterRemoveIf(sequenceFilterCall, packagePrefixedRuleName);
        }

        public void EmitFilterCallAssignStartWithAccumulateBy(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string patternName, string matchesSource, string packagePrefixedRuleName, bool chainable)
        {
            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + sequenceFilterCall.Id.ToString();

            String arrayInputType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementInputType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);

            source.AppendFrontFormat("{0}(procEnv", filterAssignMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            sequenceFilterCall.FilterCall.initExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchesSource);

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.initArrayAccess);
            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            variables.Remove(sequenceFilterCall.FilterCall.previousAccumulationAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceFilterAssignStartWithAccumulateBy(sequenceFilterCall, packagePrefixedRuleName);
        }

        private void GenerateSequenceFilterAssign(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string packagePrefixedRuleName)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterAssignMethodName = "FilterAssign_" + sequenceFilterCall.Id.ToString();

            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model) + ">";
            String arrayType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);
            String matchElementType = env.TypeOfMemberOrAttribute("match<" + packagePrefixedRuleName + ">", sequenceFilterCall.FilterCall.Entity);

            sb.AppendFront("static " + matchesType + " " + filterAssignMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matches", matchesType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("{0} matchListCopy = new {0}();\n", arrayType);
                sb.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
                sb.AppendFront("{\n");
                sb.AppendFrontIndentedFormat("matchListCopy.Add(({0})match.Clone());\n", elementType);
                sb.AppendFront("}\n");

                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopy"));
            }

            sb.AppendFront("int index = 0;\n");
            sb.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} result = {1};\n", matchElementType, seqExpr);
            sb.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", sequenceFilterCall.FilterCall.Entity); // TODO: directly access entity in match instead, requires name prefix
            sb.AppendFront("++index;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("return matches;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private void GenerateSequenceFilterRemoveIf(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string packagePrefixedRuleName)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterRemoveIfMethodName = "FilterRemoveIf_" + sequenceFilterCall.Id.ToString();

            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model) + ">";
            String arrayType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);

            sb.AppendFront("static " + matchesType + " " + filterRemoveIfMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matches", matchesType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            sb.AppendFront(arrayType + " matchList = matches.ToListExact();\n");

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("{0} matchListCopy = new {0}(matchList);\n", arrayType);

                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopy"));
            }

            sb.AppendFront("for(int index = 0; index < matchList.Count; ++index)\n");
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFrontFormat("{0} match = matchList[index];\n", elementType);
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("if((bool)({0}))\n", seqExpr);
            sb.AppendFrontIndented("matchList[index] = null;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("matches.FromListExact();\n");
            sb.AppendFront("return matches;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private void GenerateSequenceFilterAssignStartWithAccumulateBy(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, string packagePrefixedRuleName)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + sequenceFilterCall.Id.ToString();

            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model) + ">";
            String arrayType = TypesHelper.XgrsTypeToCSharpType("array<match<" + packagePrefixedRuleName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<" + packagePrefixedRuleName + ">", model);
            String matchElementType = env.TypeOfMemberOrAttribute("match<" + packagePrefixedRuleName + ">", sequenceFilterCall.FilterCall.Entity);

            sb.AppendFront("static " + matchesType + " " + filterAssignMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            sequenceFilterCall.FilterCall.initExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matches", matchesType);

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.initArrayAccess);
            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            variables.Remove(sequenceFilterCall.FilterCall.previousAccumulationAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            if(sequenceFilterCall.FilterCall.initArrayAccess != null || sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("{0} matchListCopy = new {0}();\n", arrayType);
                sb.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
                sb.AppendFront("{\n");
                sb.AppendFrontIndentedFormat("matchListCopy.Add(({0})match.Clone());\n", elementType);
                sb.AppendFront("}\n");
            }

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.initArrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.initArrayAccess, "matchListCopy"));
            }

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.initExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.previousAccumulationAccess));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.previousAccumulationAccess, seqExpr));

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopy"));
            }

            sb.AppendFront("int index = 0;\n");
            sb.AppendFrontFormat("foreach({0} match in matches)\n", elementType);
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            declarations = new SourceBuilder();
            seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} result = {1};\n", matchElementType, seqExpr);
            sb.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", sequenceFilterCall.FilterCall.Entity); // TODO: directly access entity in match instead, requires name prefix
            sb.AppendFront("++index;\n");

            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.previousAccumulationAccess, "result"));

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFront("return matches;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        internal void EmitMatchClassFilterCall(SourceBuilder source, SequenceFilterCallBase sequenceFilterCall, string matchListName, bool chainable)
        {
            if(sequenceFilterCall is SequenceFilterCallLambdaExpressionCompiled)
                EmitMatchClassFilterCall(source, (SequenceFilterCallLambdaExpressionCompiled)sequenceFilterCall, matchListName, chainable);
            else
                EmitMatchClassFilterCall(source, (SequenceFilterCallCompiled)sequenceFilterCall, matchListName, chainable);
        }

        internal void EmitMatchClassFilterCall(SourceBuilder source, SequenceFilterCallCompiled sequenceFilterCall, string matchListName, bool chainable)
        {
            if(sequenceFilterCall.Filter is IFilterAutoSupplied)
            {
                IFilterAutoSupplied filterAutoSupplied = (IFilterAutoSupplied)sequenceFilterCall.Filter;
                EmitMatchClassFilterAutoSuppliedCall(source, filterAutoSupplied, sequenceFilterCall.ArgumentExpressions, matchListName, chainable);
            }
            else if(sequenceFilterCall.Filter is IFilterAutoGenerated)
            {
                IFilterAutoGenerated filterAutoGenerated = (IFilterAutoGenerated)sequenceFilterCall.Filter;
                EmitMatchClassFilterAutoGeneratedCall(source, filterAutoGenerated, sequenceFilterCall.MatchClassName, matchListName, chainable);
            }
            else
            {
                IFilterFunction filterFunction = (IFilterFunction)sequenceFilterCall.Filter;
                EmitMatchClassFilterFunctionCall(source, filterFunction, sequenceFilterCall.ArgumentExpressions, matchListName, chainable);
            }
        }

        private void EmitMatchClassFilterAutoSuppliedCall(SourceBuilder source, IFilterAutoSupplied filterAutoSupplied, SequenceExpression[] argumentExpressions, string matchListName, bool chainable)
        {
            source.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.Filter_{0}({1}",
                filterAutoSupplied.Name, matchListName);
            for(int i = 0; i < argumentExpressions.Length; ++i)
            {
                source.AppendFormat(", ({0})({1})",
                    TypesHelper.TypeName(filterAutoSupplied.Inputs[i]),
                    GetSequenceExpression(argumentExpressions[i], source));
            }
            source.Append(")");
            if(!chainable)
                source.Append(";\n");
        }

        private void EmitMatchClassFilterAutoGeneratedCall(SourceBuilder source, IFilterAutoGenerated filterAutoGenerated, string matchClassName, string matchListName, bool chainable)
        {
            source.AppendFrontFormat("{0}MatchClassFilters.Filter_{1}_{2}(procEnv, {3})",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterAutoGenerated.PackageOfApplyee),
                matchClassName, ((LGSPFilterAutoGenerated)filterAutoGenerated).NameWithUnderscoreSuffix, matchListName);
            if(!chainable)
                source.Append(";\n");
        }

        private void EmitMatchClassFilterFunctionCall(SourceBuilder source, IFilterFunction filterFunction, SequenceExpression[] argumentExpressions, string matchListName, bool chainable)
        {
            source.AppendFrontFormat("{0}MatchClassFilters.Filter_{1}(procEnv, {2}",
                "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(filterFunction.Package), filterFunction.Name, matchListName);
            List<String> inputTypes = seqHelper.actionsTypeInformation.filterFunctionsToInputTypes[filterFunction.PackagePrefixedName];
            for(int i = 0; i < argumentExpressions.Length; ++i)
            {
                source.AppendFormat(", ({0})({1})",
                    TypesHelper.XgrsTypeToCSharpType(inputTypes[i], model),
                    GetSequenceExpression(argumentExpressions[i], source));
            }
            source.Append(")");
            if(!chainable)
                source.Append(";\n");
        }

        internal void EmitMatchClassFilterCall(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, String matchListName, bool chainable)
        {
            FilterCallWithLambdaExpression filterCall = sequenceFilterCall.FilterCall;
            if(filterCall.PlainName == "assign")
                EmitMatchClassFilterCallAssign(source, sequenceFilterCall, matchListName, chainable);
            else if(filterCall.PlainName == "removeIf")
                EmitMatchClassFilterCallRemoveIf(source, sequenceFilterCall, matchListName, chainable);
            else if(filterCall.PlainName == "assignStartWithAccumulateBy")
                EmitMatchClassFilterCallAssignStartWithAccumulateBy(source, sequenceFilterCall, matchListName, chainable);
            else
                throw new Exception("Unknown lambda expression filter call (available are assign, removeIf, assignStartWithAccumulateBy)");
        }

        public void EmitMatchClassFilterCallAssign(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, String matchListName, bool chainable)
        {
            String filterAssignMethodName = "FilterAssign_" + sequenceFilterCall.Id.ToString();

            source.AppendFrontFormat("{0}(procEnv", filterAssignMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchListName);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceMatchClassFilterAssign(sequenceFilterCall);
        }

        public void EmitMatchClassFilterCallRemoveIf(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, String matchListName, bool chainable)
        {
            String filterRemoveIfMethodName = "FilterRemoveIf_" + sequenceFilterCall.Id.ToString();

            source.AppendFrontFormat("{0}(procEnv", filterRemoveIfMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchListName);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceMatchClassFilterRemoveIf(sequenceFilterCall);
        }

        public void EmitMatchClassFilterCallAssignStartWithAccumulateBy(SourceBuilder source, SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall, String matchListName, bool chainable)
        {
            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + sequenceFilterCall.Id.ToString();

            source.AppendFrontFormat("{0}(procEnv", filterAssignMethodName);

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            sequenceFilterCall.FilterCall.initExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            source.Append(", ");
            source.Append(matchListName);

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.initArrayAccess);
            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            variables.Remove(sequenceFilterCall.FilterCall.previousAccumulationAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                source.AppendFormat(", ({0}){1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            source.Append(")");

            if(!chainable)
                source.Append(";\n");

            GenerateSequenceMatchClassFilterAssignStartWithAccumulateBy(sequenceFilterCall);
        }

        private void GenerateSequenceMatchClassFilterAssign(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterAssignMethodName = "FilterAssign_" + sequenceFilterCall.Id.ToString();

            String arrayType = "IList<GRGEN_LIBGR.IMatch>";//TypesHelper.XgrsTypeToCSharpType("array<match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">", model);
            String matchElementType = env.TypeOfMemberOrAttribute("match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">", sequenceFilterCall.FilterCall.Entity);

            sb.AppendFront("static " + arrayType + " " + filterAssignMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matchList", arrayType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            sb.AppendFrontFormat("List<{0}> matchListCopy = GRGEN_LIBGR.MatchListHelper.ToList<{0}>(matchList);\n", elementType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("List<{0}> matchListCopyForArrayAccess = new List<{0}>();\n", elementType);
                sb.AppendFrontFormat("foreach({0} match in matchList)\n", elementType);
                sb.AppendFront("{\n");
                sb.AppendFrontIndentedFormat("matchListCopyForArrayAccess.Add(({0})match.Clone());\n", elementType);
                sb.AppendFront("}\n");

                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopyForArrayAccess"));
            }

            sb.AppendFront("for(int index = 0; index < matchListCopy.Count; ++index)\n");
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFrontFormat("{0} match = matchListCopy[index];\n", elementType);
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} result = {1};\n", matchElementType, seqExpr);
            sb.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", sequenceFilterCall.FilterCall.Entity);  // TODO: directly access entity in match instead, requires name prefix

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.FromList<{0}>(matchList, matchListCopy);\n", elementType);
            sb.AppendFront("return matchList;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private void GenerateSequenceMatchClassFilterRemoveIf(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterRemoveIfMethodName = "FilterRemoveIf_" + sequenceFilterCall.Id.ToString();

            String arrayType = "IList<GRGEN_LIBGR.IMatch>";//TypesHelper.XgrsTypeToCSharpType("array<match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">", model);

            sb.AppendFront("static " + arrayType + " " + filterRemoveIfMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matchList", arrayType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            sb.AppendFrontFormat("List<{0}> matchListCopy = GRGEN_LIBGR.MatchListHelper.ToList<{0}>(matchList);\n", elementType);

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("List<{0}> matchListCopyForArrayAccess = new List<{0}>();\n", elementType);
                sb.AppendFrontFormat("foreach({0} match in matchList)\n", elementType);
                sb.AppendFront("{\n");
                sb.AppendFrontIndentedFormat("matchListCopyForArrayAccess.Add(({0})match.Clone());\n", elementType);
                sb.AppendFront("}\n");

                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopyForArrayAccess"));
            }

            sb.AppendFront("for(int index = 0; index < matchListCopy.Count; ++index)\n");
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFrontFormat("{0} match = matchListCopy[index];\n", elementType);
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("if((bool)({0}))\n", seqExpr);
            sb.AppendFrontIndented("matchListCopy[index] = null;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.FromList<{0}>(matchList, matchListCopy);\n", elementType);
            sb.AppendFront("return matchList;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        private void GenerateSequenceMatchClassFilterAssignStartWithAccumulateBy(SequenceFilterCallLambdaExpressionCompiled sequenceFilterCall)
        {
            SourceBuilder sb = new SourceBuilder();
            sb.Indent();
            sb.Indent();

            String filterAssignMethodName = "FilterAssignStartWithAccumulateBy_" + sequenceFilterCall.Id.ToString();

            String arrayType = "IList<GRGEN_LIBGR.IMatch>";//TypesHelper.XgrsTypeToCSharpType("array<match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">>", model);
            String elementType = TypesHelper.XgrsTypeToCSharpType("match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">", model);
            String matchElementType = env.TypeOfMemberOrAttribute("match<class " + sequenceFilterCall.MatchClassPackagePrefixedName + ">", sequenceFilterCall.FilterCall.Entity);

            sb.AppendFront("static " + arrayType + " " + filterAssignMethodName + "(");
            sb.Append("GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");

            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            List<SequenceExpressionConstructor> constructors = new List<SequenceExpressionConstructor>();
            sequenceFilterCall.FilterCall.lambdaExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor
            sequenceFilterCall.FilterCall.initExpression.GetLocalVariables(variables, constructors); // potential todo: handle like a container constructor

            sb.AppendFormat(", {0} matchList", arrayType);

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.initArrayAccess);
            if(sequenceFilterCall.FilterCall.arrayAccess != null)
                variables.Remove(sequenceFilterCall.FilterCall.arrayAccess);
            variables.Remove(sequenceFilterCall.FilterCall.previousAccumulationAccess);
            if(sequenceFilterCall.FilterCall.index != null)
                variables.Remove(sequenceFilterCall.FilterCall.index);
            variables.Remove(sequenceFilterCall.FilterCall.element);

            foreach(SequenceVariable variable in variables.Keys)
            {
                sb.AppendFormat(", {0} {1}", TypesHelper.XgrsTypeToCSharpType(variable.Type, model),
                    "var_" + variable.Prefix + variable.PureName);
            }

            sb.Append(")\n");
            sb.AppendFront("{\n");
            sb.Indent();

            sb.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");

            sb.AppendFrontFormat("List<{0}> matchListCopy = GRGEN_LIBGR.MatchListHelper.ToList<{0}>(matchList);\n", elementType);

            if(sequenceFilterCall.FilterCall.initArrayAccess != null || sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFrontFormat("List<{0}> matchListCopyForArrayAccess = new List<{0}>();\n", elementType);
                sb.AppendFrontFormat("foreach({0} match in matchList)\n", elementType);
                sb.AppendFront("{\n");
                sb.AppendFrontIndentedFormat("matchListCopyForArrayAccess.Add(({0})match.Clone());\n", elementType);
                sb.AppendFront("}\n");
            }

            if(sequenceFilterCall.FilterCall.initArrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.initArrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.initArrayAccess, "matchListCopyForArrayAccess"));
            }

            SourceBuilder declarations = new SourceBuilder();
            String seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.initExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.previousAccumulationAccess));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.previousAccumulationAccess, seqExpr));

            if(sequenceFilterCall.FilterCall.arrayAccess != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.arrayAccess));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.arrayAccess, "matchListCopyForArrayAccess"));
            }

            sb.AppendFront("for(int index = 0; index < matchListCopy.Count; ++index)\n");
            sb.AppendFront("{\n");
            sb.Indent();

            if(sequenceFilterCall.FilterCall.index != null)
            {
                sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.index));
                sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.index, "index"));
            }
            sb.AppendFrontFormat("{0} match = matchListCopy[index];\n", elementType);
            sb.AppendFront(seqHelper.DeclareVar(sequenceFilterCall.FilterCall.element));
            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.element, "match"));

            declarations = new SourceBuilder();
            seqExpr = GetSequenceExpression(sequenceFilterCall.FilterCall.lambdaExpression, declarations);
            sb.Append(declarations.ToString());
            sb.AppendFrontFormat("{0} result = {1};\n", matchElementType, seqExpr);
            sb.AppendFrontFormat("match.SetMember(\"{0}\", result);\n", sequenceFilterCall.FilterCall.Entity);  // TODO: directly access entity in match instead, requires name prefix

            sb.AppendFront(seqHelper.SetVar(sequenceFilterCall.FilterCall.previousAccumulationAccess, "result"));

            sb.Unindent();
            sb.AppendFront("}\n");

            sb.AppendFrontFormat("GRGEN_LIBGR.MatchListHelper.FromList<{0}>(matchList, matchListCopy);\n", elementType);
            sb.AppendFront("return matchList;\n");

            sb.Unindent();
            sb.AppendFront("}\n");

            perElementMethodSource.Append(sb.ToString());
        }

        #endregion Filters
    }
}
