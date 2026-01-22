/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

// by Edgar Jakumeit, Claude Code with Edgar Jakumeit

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class SequenceExpressionRenderer
    {
        readonly IDebuggerEnvironment env;
        readonly SequenceRenderer seqRenderer;

        public SequenceExpressionRenderer(IDebuggerEnvironment env, SequenceRenderer seqRenderer)
        {
            this.env = env;
            this.seqRenderer = seqRenderer;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal void PrintSequenceExpression(SequenceExpression seqExpr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(seqExpr.SequenceExpressionType)
            {
            case SequenceExpressionType.Conditional:
                PrintSequenceExpressionConditional((SequenceExpressionConditional)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Except:
            case SequenceExpressionType.LazyOr:
            case SequenceExpressionType.LazyAnd:
            case SequenceExpressionType.StrictOr:
            case SequenceExpressionType.StrictXor:
            case SequenceExpressionType.StrictAnd:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Not:
                PrintSequenceExpressionNot((SequenceExpressionNot)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.UnaryPlus:
                PrintSequenceExpressionUnaryPlus((SequenceExpressionUnaryPlus)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.UnaryMinus:
                PrintSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BitwiseComplement:
                PrintSequenceExpressionBitwiseComplement((SequenceExpressionBitwiseComplement)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Cast:
                PrintSequenceExpressionCast((SequenceExpressionCast)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Equal:
            case SequenceExpressionType.NotEqual:
            case SequenceExpressionType.Lower:
            case SequenceExpressionType.LowerEqual:
            case SequenceExpressionType.Greater:
            case SequenceExpressionType.GreaterEqual:
            case SequenceExpressionType.StructuralEqual:
            case SequenceExpressionType.ShiftLeft:
            case SequenceExpressionType.ShiftRight:
            case SequenceExpressionType.ShiftRightUnsigned:
            case SequenceExpressionType.Plus:
            case SequenceExpressionType.Minus:
            case SequenceExpressionType.Mul:
            case SequenceExpressionType.Div:
            case SequenceExpressionType.Mod:
                PrintSequenceExpressionBinary((SequenceBinaryExpression)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Constant:
                PrintSequenceExpressionConstant((SequenceExpressionConstant)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Variable:
                PrintSequenceExpressionVariable((SequenceExpressionVariable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.This:
                PrintSequenceExpressionThis((SequenceExpressionThis)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.New:
                PrintSequenceExpressionNew((SequenceExpressionNew)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MatchClassConstructor:
                PrintSequenceExpressionMatchClassConstructor((SequenceExpressionMatchClassConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.SetConstructor:
                PrintSequenceExpressionSetConstructor((SequenceExpressionSetConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapConstructor:
                PrintSequenceExpressionMapConstructor((SequenceExpressionMapConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayConstructor:
                PrintSequenceExpressionArrayConstructor((SequenceExpressionArrayConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeConstructor:
                PrintSequenceExpressionDequeConstructor((SequenceExpressionDequeConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.SetCopyConstructor:
                PrintSequenceExpressionSetCopyConstructor((SequenceExpressionSetCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapCopyConstructor:
                PrintSequenceExpressionMapCopyConstructor((SequenceExpressionMapCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayCopyConstructor:
                PrintSequenceExpressionArrayCopyConstructor((SequenceExpressionArrayCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeCopyConstructor:
                PrintSequenceExpressionDequeCopyConstructor((SequenceExpressionDequeCopyConstructor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerAsArray:
                PrintSequenceExpressionContainerAsArray((SequenceExpressionContainerAsArray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringLength:
                PrintSequenceExpressionStringLength((SequenceExpressionStringLength)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringStartsWith:
                PrintSequenceExpressionStringStartsWith((SequenceExpressionStringStartsWith)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringEndsWith:
                PrintSequenceExpressionStringEndsWith((SequenceExpressionStringEndsWith)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringSubstring:
                PrintSequenceExpressionStringSubstring((SequenceExpressionStringSubstring)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringReplace:
                PrintSequenceExpressionStringReplace((SequenceExpressionStringReplace)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringToLower:
                PrintSequenceExpressionStringToLower((SequenceExpressionStringToLower)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringToUpper:
                PrintSequenceExpressionStringToUpper((SequenceExpressionStringToUpper)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.StringAsArray:
                PrintSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapDomain:
                PrintSequenceExpressionMapDomain((SequenceExpressionMapDomain)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MapRange:
                PrintSequenceExpressionMapRange((SequenceExpressionMapRange)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Random:
                PrintSequenceExpressionRandom((SequenceExpressionRandom)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Def:
                PrintSequenceExpressionDef((SequenceExpressionDef)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsVisited:
                PrintSequenceExpressionIsVisited((SequenceExpressionIsVisited)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.InContainerOrString:
                PrintSequenceExpressionInContainerOrString((SequenceExpressionInContainerOrString)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerEmpty:
                PrintSequenceExpressionContainerEmpty((SequenceExpressionContainerEmpty)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerSize:
                PrintSequenceExpressionContainerSize((SequenceExpressionContainerSize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerAccess:
                PrintSequenceExpressionContainerAccess((SequenceExpressionContainerAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ContainerPeek:
                PrintSequenceExpressionContainerPeek((SequenceExpressionContainerPeek)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringIndexOf((SequenceExpressionArrayOrDequeOrStringIndexOf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeOrStringLastIndexOf:
                PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf((SequenceExpressionArrayOrDequeOrStringLastIndexOf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfOrdered:
                PrintSequenceExpressionArrayIndexOfOrdered((SequenceExpressionArrayIndexOfOrdered)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArraySum:
                PrintSequenceExpressionArraySum((SequenceExpressionArraySum)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayProd:
                PrintSequenceExpressionArrayProd((SequenceExpressionArrayProd)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrSetMin:
                PrintSequenceExpressionArrayOrSetMin((SequenceExpressionArrayOrSetMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrSetMax:
                PrintSequenceExpressionArrayOrSetMax((SequenceExpressionArrayOrSetMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAvg:
                PrintSequenceExpressionArrayAvg((SequenceExpressionArrayAvg)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMed:
                PrintSequenceExpressionArrayMed((SequenceExpressionArrayMed)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMedUnordered:
                PrintSequenceExpressionArrayMedUnordered((SequenceExpressionArrayMedUnordered)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayVar:
                PrintSequenceExpressionArrayVar((SequenceExpressionArrayVar)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayDev:
                PrintSequenceExpressionArrayDev((SequenceExpressionArrayDev)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAnd:
                PrintSequenceExpressionArrayAnd((SequenceExpressionArrayAnd)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOr:
                PrintSequenceExpressionArrayOr((SequenceExpressionArrayOr)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrDequeAsSet:
                PrintSequenceExpressionArrayOrDequeAsSet((SequenceExpressionArrayOrDequeAsSet)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsMap:
                PrintSequenceExpressionArrayAsMap((SequenceExpressionArrayAsMap)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsDeque:
                PrintSequenceExpressionArrayAsDeque((SequenceExpressionArrayAsDeque)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayAsString:
                PrintSequenceExpressionArrayAsString((SequenceExpressionArrayAsString)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArraySubarray:
                PrintSequenceExpressionArraySubarray((SequenceExpressionArraySubarray)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DequeSubdeque:
                PrintSequenceExpressionDequeSubdeque((SequenceExpressionDequeSubdeque)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderAscending:
                PrintSequenceExpressionArrayOrderAscending((SequenceExpressionArrayOrderAscending)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderDescending:
                PrintSequenceExpressionArrayOrderDescending((SequenceExpressionArrayOrderDescending)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayGroup:
                PrintSequenceExpressionArrayGroup((SequenceExpressionArrayGroup)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayKeepOneForEach:
                PrintSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayReverse:
                PrintSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayShuffle:
                PrintSequenceExpressionArrayShuffle((SequenceExpressionArrayShuffle)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayExtract:
                PrintSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderAscendingBy:
                PrintSequenceExpressionArrayOrderAscendingBy((SequenceExpressionArrayOrderAscendingBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayOrderDescendingBy:
                PrintSequenceExpressionArrayOrderDescendingBy((SequenceExpressionArrayOrderDescendingBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayGroupBy:
                PrintSequenceExpressionArrayGroupBy((SequenceExpressionArrayGroupBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayKeepOneForEachBy:
                PrintSequenceExpressionArrayKeepOneForEachBy((SequenceExpressionArrayKeepOneForEachBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfBy:
                PrintSequenceExpressionArrayIndexOfBy((SequenceExpressionArrayIndexOfBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayLastIndexOfBy:
                PrintSequenceExpressionArrayLastIndexOfBy((SequenceExpressionArrayLastIndexOfBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayIndexOfOrderedBy:
                PrintSequenceExpressionArrayIndexOfOrderedBy((SequenceExpressionArrayIndexOfOrderedBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMap:
                PrintSequenceExpressionArrayMap((SequenceExpressionArrayMap)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayRemoveIf:
                PrintSequenceExpressionArrayRemoveIf((SequenceExpressionArrayRemoveIf)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ArrayMapStartWithAccumulateBy:
                PrintSequenceExpressionArrayMapStartWithAccumulateBy((SequenceExpressionArrayMapStartWithAccumulateBy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ElementFromGraph:
                PrintSequenceExpressionElementFromGraph((SequenceExpressionElementFromGraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.NodeByName:
                PrintSequenceExpressionNodeByName((SequenceExpressionNodeByName)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EdgeByName:
                PrintSequenceExpressionEdgeByName((SequenceExpressionEdgeByName)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.NodeByUnique:
                PrintSequenceExpressionNodeByUnique((SequenceExpressionNodeByUnique)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EdgeByUnique:
                PrintSequenceExpressionEdgeByUnique((SequenceExpressionEdgeByUnique)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Source:
                PrintSequenceExpressionSource((SequenceExpressionSource)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Target:
                PrintSequenceExpressionTarget((SequenceExpressionTarget)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Opposite:
                PrintSequenceExpressionOpposite((SequenceExpressionOpposite)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                PrintSequenceExpressionAttributeOrMatchAccess((SequenceExpressionAttributeOrMatchAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GraphElementAttribute:
                PrintSequenceExpressionAttributeAccess((SequenceExpressionAttributeAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ElementOfMatch:
                PrintSequenceExpressionMatchAccess((SequenceExpressionMatchAccess)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Nodes:
                PrintSequenceExpressionNodes((SequenceExpressionNodes)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Edges:
                PrintSequenceExpressionEdges((SequenceExpressionEdges)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountNodes:
                PrintSequenceExpressionCountNodes((SequenceExpressionCountNodes)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountEdges:
                PrintSequenceExpressionCountEdges((SequenceExpressionCountEdges)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Now:
                PrintSequenceExpressionNow((SequenceExpressionNow)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathMin:
                PrintSequenceExpressionMathMin((SequenceExpressionMathMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathMax:
                PrintSequenceExpressionMathMax((SequenceExpressionMathMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathAbs:
                PrintSequenceExpressionMathAbs((SequenceExpressionMathAbs)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathCeil:
                PrintSequenceExpressionMathCeil((SequenceExpressionMathCeil)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloor:
                PrintSequenceExpressionMathFloor((SequenceExpressionMathFloor)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathRound:
                PrintSequenceExpressionMathRound((SequenceExpressionMathRound)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathTruncate:
                PrintSequenceExpressionMathTruncate((SequenceExpressionMathTruncate)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSqr:
                PrintSequenceExpressionMathSqr((SequenceExpressionMathSqr)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSqrt:
                PrintSequenceExpressionMathSqrt((SequenceExpressionMathSqrt)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathPow:
                PrintSequenceExpressionMathPow((SequenceExpressionMathPow)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLog:
                PrintSequenceExpressionMathLog((SequenceExpressionMathLog)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSgn:
                PrintSequenceExpressionMathSgn((SequenceExpressionMathSgn)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathSin:
                PrintSequenceExpressionMathSin((SequenceExpressionMathSin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathCos:
                PrintSequenceExpressionMathCos((SequenceExpressionMathCos)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathTan:
                PrintSequenceExpressionMathTan((SequenceExpressionMathTan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcSin:
                PrintSequenceExpressionMathArcSin((SequenceExpressionMathArcSin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcCos:
                PrintSequenceExpressionMathArcCos((SequenceExpressionMathArcCos)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathArcTan:
                PrintSequenceExpressionMathArcTan((SequenceExpressionMathArcTan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathPi:
                PrintSequenceExpressionMathPi((SequenceExpressionMathPi)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathE:
                PrintSequenceExpressionMathE((SequenceExpressionMathE)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathByteMin:
                PrintSequenceExpressionMathByteMin((SequenceExpressionMathByteMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathByteMax:
                PrintSequenceExpressionMathByteMax((SequenceExpressionMathByteMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathShortMin:
                PrintSequenceExpressionMathShortMin((SequenceExpressionMathShortMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathShortMax:
                PrintSequenceExpressionMathShortMax((SequenceExpressionMathShortMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathIntMin:
                PrintSequenceExpressionMathIntMin((SequenceExpressionMathIntMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathIntMax:
                PrintSequenceExpressionMathIntMax((SequenceExpressionMathIntMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLongMin:
                PrintSequenceExpressionMathLongMin((SequenceExpressionMathLongMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathLongMax:
                PrintSequenceExpressionMathLongMax((SequenceExpressionMathLongMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloatMin:
                PrintSequenceExpressionMathFloatMin((SequenceExpressionMathFloatMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathFloatMax:
                PrintSequenceExpressionMathFloatMax((SequenceExpressionMathFloatMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathDoubleMin:
                PrintSequenceExpressionMathDoubleMin((SequenceExpressionMathDoubleMin)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MathDoubleMax:
                PrintSequenceExpressionMathDoubleMax((SequenceExpressionMathDoubleMax)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Empty:
                PrintSequenceExpressionEmpty((SequenceExpressionEmpty)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Size:
                PrintSequenceExpressionSize((SequenceExpressionSize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.AdjacentNodes:
            case SequenceExpressionType.AdjacentNodesViaIncoming:
            case SequenceExpressionType.AdjacentNodesViaOutgoing:
            case SequenceExpressionType.IncidentEdges:
            case SequenceExpressionType.IncomingEdges:
            case SequenceExpressionType.OutgoingEdges:
                PrintSequenceExpressionAdjacentIncident((SequenceExpressionAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ReachableNodes:
            case SequenceExpressionType.ReachableNodesViaIncoming:
            case SequenceExpressionType.ReachableNodesViaOutgoing:
            case SequenceExpressionType.ReachableEdges:
            case SequenceExpressionType.ReachableEdgesViaIncoming:
            case SequenceExpressionType.ReachableEdgesViaOutgoing:
                PrintSequenceExpressionReachable((SequenceExpressionReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BoundedReachableNodes:
            case SequenceExpressionType.BoundedReachableNodesViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.BoundedReachableEdges:
            case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionBoundedReachable((SequenceExpressionBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
            case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                PrintSequenceExpressionBoundedReachableWithRemainingDepth((SequenceExpressionBoundedReachableWithRemainingDepth)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountAdjacentNodes:
            case SequenceExpressionType.CountAdjacentNodesViaIncoming:
            case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
            case SequenceExpressionType.CountIncidentEdges:
            case SequenceExpressionType.CountIncomingEdges:
            case SequenceExpressionType.CountOutgoingEdges:
                PrintSequenceExpressionCountAdjacentIncident((SequenceExpressionCountAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountReachableNodes:
            case SequenceExpressionType.CountReachableNodesViaIncoming:
            case SequenceExpressionType.CountReachableNodesViaOutgoing:
            case SequenceExpressionType.CountReachableEdges:
            case SequenceExpressionType.CountReachableEdgesViaIncoming:
            case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountReachable((SequenceExpressionCountReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.CountBoundedReachableNodes:
            case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.CountBoundedReachableEdges:
            case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionCountBoundedReachable((SequenceExpressionCountBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsAdjacentNodes:
            case SequenceExpressionType.IsAdjacentNodesViaIncoming:
            case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
            case SequenceExpressionType.IsIncidentEdges:
            case SequenceExpressionType.IsIncomingEdges:
            case SequenceExpressionType.IsOutgoingEdges:
                PrintSequenceExpressionIsAdjacentIncident((SequenceExpressionIsAdjacentIncident)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsReachableNodes:
            case SequenceExpressionType.IsReachableNodesViaIncoming:
            case SequenceExpressionType.IsReachableNodesViaOutgoing:
            case SequenceExpressionType.IsReachableEdges:
            case SequenceExpressionType.IsReachableEdgesViaIncoming:
            case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsReachable((SequenceExpressionIsReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.IsBoundedReachableNodes:
            case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
            case SequenceExpressionType.IsBoundedReachableEdges:
            case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
            case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                PrintSequenceExpressionIsBoundedReachable((SequenceExpressionIsBoundedReachable)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.InducedSubgraph:
                PrintSequenceExpressionInducedSubgraph((SequenceExpressionInducedSubgraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.DefinedSubgraph:
                PrintSequenceExpressionDefinedSubgraph((SequenceExpressionDefinedSubgraph)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.EqualsAny:
                PrintSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.GetEquivalent:
                PrintSequenceExpressionGetEquivalent((SequenceExpressionGetEquivalent)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Nameof:
                PrintSequenceExpressionNameof((SequenceExpressionNameof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Uniqueof:
                PrintSequenceExpressionUniqueof((SequenceExpressionUniqueof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Typeof:
                PrintSequenceExpressionTypeof((SequenceExpressionTypeof)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.ExistsFile:
                PrintSequenceExpressionExistsFile((SequenceExpressionExistsFile)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Import:
                PrintSequenceExpressionImport((SequenceExpressionImport)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Copy:
                PrintSequenceExpressionCopy((SequenceExpressionCopy)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Canonize:
                PrintSequenceExpressionCanonize((SequenceExpressionCanonize)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.RuleQuery:
                PrintSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MultiRuleQuery:
                PrintSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.MappingClause:
                PrintSequenceExpressionMappingClause((SequenceExpressionMappingClause)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.Scan:
                PrintSequenceExpressionScan((SequenceExpressionScan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.TryScan:
                PrintSequenceExpressionTryScan((SequenceExpressionTryScan)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.FunctionCall:
                PrintSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)seqExpr, parent, highlightingMode);
                break;
            case SequenceExpressionType.FunctionMethodCall:
                PrintSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)seqExpr, parent, highlightingMode);
                break;
            }
        }

        private void PrintSequenceExpressionConditional(SequenceExpressionConditional seqExprCond, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprCond.Condition, seqExprCond, highlightingMode);
            env.PrintHighlighted(" ? ", highlightingMode);
            PrintSequenceExpression(seqExprCond.TrueCase, seqExprCond, highlightingMode);
            env.PrintHighlighted(" : ", highlightingMode);
            PrintSequenceExpression(seqExprCond.FalseCase, seqExprCond, highlightingMode);
        }

        private void PrintSequenceExpressionBinary(SequenceBinaryExpression seqExprBin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprBin.Left, seqExprBin, highlightingMode);
            env.PrintHighlighted(seqExprBin.Operator, highlightingMode);
            PrintSequenceExpression(seqExprBin.Right, seqExprBin, highlightingMode);
        }

        private void PrintSequenceExpressionNot(SequenceExpressionNot seqExprNot, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("!", highlightingMode);
            PrintSequenceExpression(seqExprNot.Operand, seqExprNot, highlightingMode);
        }

        private void PrintSequenceExpressionUnaryPlus(SequenceExpressionUnaryPlus seqExprUnaryPlus, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("+", highlightingMode);
            PrintSequenceExpression(seqExprUnaryPlus.Operand, seqExprUnaryPlus, highlightingMode);
        }

        private void PrintSequenceExpressionUnaryMinus(SequenceExpressionUnaryMinus seqExprUnaryMinus, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("-", highlightingMode);
            PrintSequenceExpression(seqExprUnaryMinus.Operand, seqExprUnaryMinus, highlightingMode);
        }

        private void PrintSequenceExpressionBitwiseComplement(SequenceExpressionBitwiseComplement seqExprBitwiseComplement, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("~", highlightingMode);
            PrintSequenceExpression(seqExprBitwiseComplement.Operand, seqExprBitwiseComplement, highlightingMode);
        }

        private void PrintSequenceExpressionCast(SequenceExpressionCast seqExprCast, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("(", highlightingMode);
            env.PrintHighlighted(seqExprCast.TargetType, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
            PrintSequenceExpression(seqExprCast.Operand, seqExprCast, highlightingMode);
        }

        private void PrintSequenceExpressionConstant(SequenceExpressionConstant seqExprConstant, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(seqExprConstant.Constant), highlightingMode);
        }

        private void PrintSequenceExpressionVariable(SequenceExpressionVariable seqExprVariable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprVariable.Variable.Name, highlightingMode);
        }

        private void PrintSequenceExpressionNew(SequenceExpressionNew seqExprNew, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprNew.ConstructedType, highlightingMode); // TODO: check -- looks suspicious
        }

        private void PrintSequenceExpressionThis(SequenceExpressionThis seqExprThis, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("this", highlightingMode);
        }

        private void PrintSequenceExpressionMatchClassConstructor(SequenceExpressionMatchClassConstructor seqExprMatchClassConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("match<class " + seqExprMatchClassConstructor.ConstructedType + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionSetConstructor(SequenceExpressionSetConstructor seqExprSetConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("set<", highlightingMode);
            env.PrintHighlighted(seqExprSetConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">{", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprSetConstructor, seqExprSetConstructor, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionMapConstructor(SequenceExpressionMapConstructor seqExprMapConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("map<", highlightingMode);
            env.PrintHighlighted(seqExprMapConstructor.KeyType, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            env.PrintHighlighted(seqExprMapConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">{", highlightingMode);
            for(int i = 0; i < seqExprMapConstructor.MapKeyItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprMapConstructor.MapKeyItems[i], seqExprMapConstructor, highlightingMode);
                env.PrintHighlighted("->", highlightingMode);
                PrintSequenceExpression(seqExprMapConstructor.ContainerItems[i], seqExprMapConstructor, highlightingMode);
                if(i != seqExprMapConstructor.MapKeyItems.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayConstructor(SequenceExpressionArrayConstructor seqExprArrayConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("array<", highlightingMode);
            env.PrintHighlighted(seqExprArrayConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprArrayConstructor, seqExprArrayConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionDequeConstructor(SequenceExpressionDequeConstructor seqExprDequeConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("deque<", highlightingMode);
            env.PrintHighlighted(seqExprDequeConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">]", highlightingMode);
            PrintSequenceExpressionContainerConstructor(seqExprDequeConstructor, seqExprDequeConstructor, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
        }

        private void PrintSequenceExpressionContainerConstructor(SequenceExpressionContainerConstructor seqExprContainerConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            for(int i = 0; i < seqExprContainerConstructor.ContainerItems.Length; ++i)
            {
                PrintSequenceExpression(seqExprContainerConstructor.ContainerItems[i], seqExprContainerConstructor, highlightingMode);
                if(i != seqExprContainerConstructor.ContainerItems.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
        }

        private void PrintSequenceExpressionSetCopyConstructor(SequenceExpressionSetCopyConstructor seqExprSetCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("set<", highlightingMode);
            env.PrintHighlighted(seqExprSetCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprSetCopyConstructor.SetToCopy, seqExprSetCopyConstructor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMapCopyConstructor(SequenceExpressionMapCopyConstructor seqExprMapCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("map<", highlightingMode);
            env.PrintHighlighted(seqExprMapCopyConstructor.KeyType, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            env.PrintHighlighted(seqExprMapCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">(", highlightingMode);
            PrintSequenceExpression(seqExprMapCopyConstructor.MapToCopy, seqExprMapCopyConstructor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayCopyConstructor(SequenceExpressionArrayCopyConstructor seqExprArrayCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("array<", highlightingMode);
            env.PrintHighlighted(seqExprArrayCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprArrayCopyConstructor.ArrayToCopy, seqExprArrayCopyConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionDequeCopyConstructor(SequenceExpressionDequeCopyConstructor seqExprDequeCopyConstructor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("deque<", highlightingMode);
            env.PrintHighlighted(seqExprDequeCopyConstructor.ValueType, highlightingMode);
            env.PrintHighlighted(">[", highlightingMode);
            PrintSequenceExpression(seqExprDequeCopyConstructor.DequeToCopy, seqExprDequeCopyConstructor, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionContainerAsArray(SequenceExpressionContainerAsArray seqExprContainerAsArray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprContainerAsArray.Name + ".asArray()", highlightingMode);
        }

        private void PrintSequenceExpressionStringLength(SequenceExpressionStringLength seqExprStringLength, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringLength.StringExpr, seqExprStringLength, highlightingMode);
            env.PrintHighlighted(".length()", highlightingMode);
        }

        private void PrintSequenceExpressionStringStartsWith(SequenceExpressionStringStartsWith seqExprStringStartsWith, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringStartsWith.StringExpr, seqExprStringStartsWith, highlightingMode);
            env.PrintHighlighted(".startsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringStartsWith.StringToSearchForExpr, seqExprStringStartsWith, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringEndsWith(SequenceExpressionStringEndsWith seqExprStringEndsWith, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringEndsWith.StringExpr, seqExprStringEndsWith, highlightingMode);
            env.PrintHighlighted(".endsWith(", highlightingMode);
            PrintSequenceExpression(seqExprStringEndsWith.StringToSearchForExpr, seqExprStringEndsWith, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringSubstring(SequenceExpressionStringSubstring seqExprStringSubstring, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringSubstring.StringExpr, seqExprStringSubstring, highlightingMode);
            env.PrintHighlighted(".substring(", highlightingMode);
            PrintSequenceExpression(seqExprStringSubstring.StartIndexExpr, seqExprStringSubstring, highlightingMode);
            if(seqExprStringSubstring.LengthExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprStringSubstring.LengthExpr, seqExprStringSubstring, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringReplace(SequenceExpressionStringReplace seqExprStringReplace, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringReplace.StringExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(".replace(", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.StartIndexExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.LengthExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprStringReplace.ReplaceStringExpr, seqExprStringReplace, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionStringToLower(SequenceExpressionStringToLower seqExprStringToLower, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringToLower.StringExpr, seqExprStringToLower, highlightingMode);
            env.PrintHighlighted(".toLower()", highlightingMode);
        }

        private void PrintSequenceExpressionStringToUpper(SequenceExpressionStringToUpper seqExprStringToUpper, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringToUpper.StringExpr, seqExprStringToUpper, highlightingMode);
            env.PrintHighlighted(".toUpper()", highlightingMode);
        }

        private void PrintSequenceExpressionStringAsArray(SequenceExpressionStringAsArray seqExprStringAsArray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprStringAsArray.StringExpr, seqExprStringAsArray, highlightingMode);
            env.PrintHighlighted(".asArray(", highlightingMode);
            PrintSequenceExpression(seqExprStringAsArray.SeparatorExpr, seqExprStringAsArray, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionRandom(SequenceExpressionRandom seqExprRandom, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("random(", highlightingMode);
            if(seqExprRandom.UpperBound != null)
                PrintSequenceExpression(seqExprRandom.UpperBound, seqExprRandom, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDef(SequenceExpressionDef seqExprDef, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("def(", highlightingMode);
            for(int i = 0; i < seqExprDef.DefVars.Length; ++i)
            {
                PrintSequenceExpression(seqExprDef.DefVars[i], seqExprDef, highlightingMode);
                if(i != seqExprDef.DefVars.Length - 1)
                    env.PrintHighlighted(",", highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsVisited(SequenceExpressionIsVisited seqExprIsVisited, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprIsVisited.GraphElementVarExpr, seqExprIsVisited, highlightingMode);
            env.PrintHighlighted(".visited", highlightingMode);
            if(seqExprIsVisited.VisitedFlagExpr != null)
            {
                env.PrintHighlighted("[", highlightingMode);
                PrintSequenceExpression(seqExprIsVisited.VisitedFlagExpr, seqExprIsVisited, highlightingMode);
                env.PrintHighlighted("]", highlightingMode);
            }
        }

        private void PrintSequenceExpressionInContainerOrString(SequenceExpressionInContainerOrString seqExprInContainerOrString, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprInContainerOrString.Expr, seqExprInContainerOrString, highlightingMode);
            env.PrintHighlighted(" in ", highlightingMode);
            PrintSequenceExpression(seqExprInContainerOrString.ContainerOrStringExpr, seqExprInContainerOrString, highlightingMode);
        }

        private void PrintSequenceExpressionContainerSize(SequenceExpressionContainerSize seqExprContainerSize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerSize.ContainerExpr, seqExprContainerSize, highlightingMode);
            env.PrintHighlighted(".size()", highlightingMode);
        }

        private void PrintSequenceExpressionContainerEmpty(SequenceExpressionContainerEmpty seqExprContainerEmpty, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerEmpty.ContainerExpr, seqExprContainerEmpty, highlightingMode);
            env.PrintHighlighted(".empty()", highlightingMode);
        }

        private void PrintSequenceExpressionContainerAccess(SequenceExpressionContainerAccess seqExprContainerAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerAccess.ContainerExpr, seqExprContainerAccess, highlightingMode);
            env.PrintHighlighted("[", highlightingMode);
            PrintSequenceExpression(seqExprContainerAccess.KeyExpr, seqExprContainerAccess, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceExpressionContainerPeek(SequenceExpressionContainerPeek seqExprContainerPeek, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprContainerPeek.ContainerExpr, seqExprContainerPeek, highlightingMode);
            env.PrintHighlighted(".peek(", highlightingMode);
            if(seqExprContainerPeek.KeyExpr != null)
                PrintSequenceExpression(seqExprContainerPeek.KeyExpr, seqExprContainerPeek, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeOrStringIndexOf(SequenceExpressionArrayOrDequeOrStringIndexOf seqExprArrayOrDequeOrStringIndexOf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            env.PrintHighlighted(".indexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            if(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringIndexOf, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeOrStringLastIndexOf(SequenceExpressionArrayOrDequeOrStringLastIndexOf seqExprArrayOrDequeOrStringLastIndexOf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ContainerExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            env.PrintHighlighted(".lastIndexOf(", highlightingMode);
            PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.ValueToSearchForExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            if(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayOrDequeOrStringLastIndexOf.StartPositionExpr, seqExprArrayOrDequeOrStringLastIndexOf, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfOrdered(SequenceExpressionArrayIndexOfOrdered seqExprArrayIndexOfOrdered, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ContainerExpr, seqExprArrayIndexOfOrdered, highlightingMode);
            env.PrintHighlighted(".indexOfOrdered(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrdered.ValueToSearchForExpr, seqExprArrayIndexOfOrdered, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArraySum(SequenceExpressionArraySum seqExprArraySum, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArraySum.ContainerExpr, seqExprArraySum, highlightingMode);
            env.PrintHighlighted(".sum()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayProd(SequenceExpressionArrayProd seqExprArrayProd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayProd.ContainerExpr, seqExprArrayProd, highlightingMode);
            env.PrintHighlighted(".prod()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrSetMin(SequenceExpressionArrayOrSetMin seqExprArrayMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMin.ContainerExpr, seqExprArrayMin, highlightingMode);
            env.PrintHighlighted(".min()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrSetMax(SequenceExpressionArrayOrSetMax seqExprArrayMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMax.ContainerExpr, seqExprArrayMax, highlightingMode);
            env.PrintHighlighted(".max()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAvg(SequenceExpressionArrayAvg seqExprArrayAvg, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAvg.ContainerExpr, seqExprArrayAvg, highlightingMode);
            env.PrintHighlighted(".avg()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMed(SequenceExpressionArrayMed seqExprArrayMed, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMed.ContainerExpr, seqExprArrayMed, highlightingMode);
            env.PrintHighlighted(".med()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMedUnordered(SequenceExpressionArrayMedUnordered seqExprArrayMedUnordered, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayMedUnordered.ContainerExpr, seqExprArrayMedUnordered, highlightingMode);
            env.PrintHighlighted(".medUnordered()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayVar(SequenceExpressionArrayVar seqExprArrayVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayVar.ContainerExpr, seqExprArrayVar, highlightingMode);
            env.PrintHighlighted(".var()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayDev(SequenceExpressionArrayDev seqExprArrayDev, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayDev.ContainerExpr, seqExprArrayDev, highlightingMode);
            env.PrintHighlighted(".dev()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAnd(SequenceExpressionArrayAnd seqExprArrayAnd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAnd.ContainerExpr, seqExprArrayAnd, highlightingMode);
            env.PrintHighlighted(".and()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOr(SequenceExpressionArrayOr seqExprArrayOr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOr.ContainerExpr, seqExprArrayOr, highlightingMode);
            env.PrintHighlighted(".or()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrDequeAsSet(SequenceExpressionArrayOrDequeAsSet seqExprArrayOrDequeAsSet, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrDequeAsSet.ContainerExpr, seqExprArrayOrDequeAsSet, highlightingMode);
            env.PrintHighlighted(".asSet()", highlightingMode);
        }

        private void PrintSequenceExpressionMapDomain(SequenceExpressionMapDomain seqExprMapDomain, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMapDomain.ContainerExpr, seqExprMapDomain, highlightingMode);
            env.PrintHighlighted(".domain()", highlightingMode);
        }

        private void PrintSequenceExpressionMapRange(SequenceExpressionMapRange seqExprMapRange, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMapRange.ContainerExpr, seqExprMapRange, highlightingMode);
            env.PrintHighlighted(".range()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsMap(SequenceExpressionArrayAsMap seqExprArrayAsMap, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsMap.ContainerExpr, seqExprArrayAsMap, highlightingMode);
            env.PrintHighlighted(".asSet()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsDeque(SequenceExpressionArrayAsDeque seqExprArrayAsDeque, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsDeque.ContainerExpr, seqExprArrayAsDeque, highlightingMode);
            env.PrintHighlighted(".asDeque()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayAsString(SequenceExpressionArrayAsString seqExprArrayAsString, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayAsString.ContainerExpr, seqExprArrayAsString, highlightingMode);
            env.PrintHighlighted(".asString(", highlightingMode);
            PrintSequenceExpression(seqExprArrayAsString.Separator, seqExprArrayAsString, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArraySubarray(SequenceExpressionArraySubarray seqExprArraySubarray, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArraySubarray.ContainerExpr, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(".subarray(", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Start, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprArraySubarray.Length, seqExprArraySubarray, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDequeSubdeque(SequenceExpressionDequeSubdeque seqExprDequeSubdeque, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprDequeSubdeque.ContainerExpr, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(".subdeque(", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Start, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprDequeSubdeque.Length, seqExprDequeSubdeque, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderAscending(SequenceExpressionArrayOrderAscending seqExprArrayOrderAscending, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrderAscending.ContainerExpr, seqExprArrayOrderAscending, highlightingMode);
            env.PrintHighlighted(".orderAscending()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderDescending(SequenceExpressionArrayOrderDescending seqExprArrayOrderDescending, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayOrderDescending.ContainerExpr, seqExprArrayOrderDescending, highlightingMode);
            env.PrintHighlighted(".orderDescending()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayGroup(SequenceExpressionArrayGroup seqExprArrayGroup, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayGroup.ContainerExpr, seqExprArrayGroup, highlightingMode);
            env.PrintHighlighted(".group()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayKeepOneForEach(SequenceExpressionArrayKeepOneForEach seqExprArrayKeepOneForEach, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayKeepOneForEach.ContainerExpr, seqExprArrayKeepOneForEach, highlightingMode);
            env.PrintHighlighted(".keepOneForEach()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayReverse(SequenceExpressionArrayReverse seqExprArrayReverse, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayReverse.ContainerExpr, seqExprArrayReverse, highlightingMode);
            env.PrintHighlighted(".reverse()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayShuffle(SequenceExpressionArrayShuffle seqExprArrayShuffle, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayShuffle.ContainerExpr, seqExprArrayShuffle, highlightingMode);
            env.PrintHighlighted(".shuffle()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayExtract(SequenceExpressionArrayExtract seqExprArrayExtract, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprArrayExtract.ContainerExpr, seqExprArrayExtract, highlightingMode);
            env.PrintHighlighted(".extract<", highlightingMode);
            env.PrintHighlighted(seqExprArrayExtract.memberOrAttributeName, highlightingMode);
            env.PrintHighlighted(">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMap(SequenceExpressionArrayMap seqExprArrayMap, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayMap.Name + ".map<" + seqExprArrayMap.TypeName + ">{", highlightingMode);
            if(seqExprArrayMap.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMap.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayMap.Index != null)
                env.PrintHighlighted(seqExprArrayMap.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMap.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMap.MappingExpr, seqExprArrayMap, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayRemoveIf(SequenceExpressionArrayRemoveIf seqExprArrayRemoveIf, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayRemoveIf.Name + ".removeIf{", highlightingMode);
            if(seqExprArrayRemoveIf.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayRemoveIf.ArrayAccess.Name + "; ", highlightingMode);
            if(seqExprArrayRemoveIf.Index != null)
                env.PrintHighlighted(seqExprArrayRemoveIf.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayRemoveIf.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayRemoveIf.ConditionExpr, seqExprArrayRemoveIf, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayMapStartWithAccumulateBy(SequenceExpressionArrayMapStartWithAccumulateBy seqExprArrayMapStartWithAccumulateBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Name + ".map<" + seqExprArrayMapStartWithAccumulateBy.TypeName + ">", highlightingMode);
            env.PrintHighlighted("StartWith", highlightingMode);
            env.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.InitArrayAccess.Name + "; ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.InitExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
            env.PrintHighlighted("AccumulateBy", highlightingMode);
            env.PrintHighlighted("{", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.ArrayAccess != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.ArrayAccess.Name + "; ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.PreviousAccumulationAccess.Name + ", ", highlightingMode);
            if(seqExprArrayMapStartWithAccumulateBy.Index != null)
                env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Index.Name + " -> ", highlightingMode);
            env.PrintHighlighted(seqExprArrayMapStartWithAccumulateBy.Var.Name + " -> ", highlightingMode);
            PrintSequenceExpression(seqExprArrayMapStartWithAccumulateBy.MappingExpr, seqExprArrayMapStartWithAccumulateBy, highlightingMode);
            env.PrintHighlighted("}", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderAscendingBy(SequenceExpressionArrayOrderAscendingBy seqExprArrayOrderAscendingBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayOrderAscendingBy.Name + ".orderAscendingBy<" + seqExprArrayOrderAscendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayOrderDescendingBy(SequenceExpressionArrayOrderDescendingBy seqExprArrayOrderDescendingBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayOrderDescendingBy.Name + ".orderDescendingBy<" + seqExprArrayOrderDescendingBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayGroupBy(SequenceExpressionArrayGroupBy seqExprArrayGroupBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayGroupBy.Name + ".groupBy<" + seqExprArrayGroupBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayKeepOneForEachBy(SequenceExpressionArrayKeepOneForEachBy seqExprArrayKeepOneForEachBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayKeepOneForEachBy.Name + ".keepOneForEach<" + seqExprArrayKeepOneForEachBy.memberOrAttributeName + ">()", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfBy(SequenceExpressionArrayIndexOfBy seqExprArrayIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayIndexOfBy.Name + ".indexOfBy<" + seqExprArrayIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfBy.ValueToSearchForExpr, seqExprArrayIndexOfBy, highlightingMode);
            if(seqExprArrayIndexOfBy.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayIndexOfBy.StartPositionExpr, seqExprArrayIndexOfBy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayLastIndexOfBy(SequenceExpressionArrayLastIndexOfBy seqExprArrayLastIndexOfBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayLastIndexOfBy.Name + ".lastIndexOfBy<" + seqExprArrayLastIndexOfBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayLastIndexOfBy.ValueToSearchForExpr, seqExprArrayLastIndexOfBy, highlightingMode);
            if(seqExprArrayLastIndexOfBy.StartPositionExpr != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprArrayLastIndexOfBy.StartPositionExpr, seqExprArrayLastIndexOfBy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionArrayIndexOfOrderedBy(SequenceExpressionArrayIndexOfOrderedBy seqExprArrayIndexOfOrderedBy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprArrayIndexOfOrderedBy.Name + ".indexOfOrderedBy<" + seqExprArrayIndexOfOrderedBy.memberOrAttributeName + ">(", highlightingMode);
            PrintSequenceExpression(seqExprArrayIndexOfOrderedBy.ValueToSearchForExpr, seqExprArrayIndexOfOrderedBy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionElementFromGraph(SequenceExpressionElementFromGraph seqExprElementFromGraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("@(" + seqExprElementFromGraph.ElementName + ")", highlightingMode);
        }

        private void PrintSequenceExpressionNodeByName(SequenceExpressionNodeByName seqExprNodeByName, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nodeByName(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByName.NodeName, seqExprNodeByName, highlightingMode);
            if(seqExprNodeByName.NodeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByName.NodeType, seqExprNodeByName, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdgeByName(SequenceExpressionEdgeByName seqExprEdgeByName, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("edgeByName(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByName.EdgeName, seqExprEdgeByName, highlightingMode);
            if(seqExprEdgeByName.EdgeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByName.EdgeType, seqExprEdgeByName, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionNodeByUnique(SequenceExpressionNodeByUnique seqExprNodeByUnique, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nodeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprNodeByUnique.NodeUniqueId, seqExprNodeByUnique, highlightingMode);
            if(seqExprNodeByUnique.NodeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprNodeByUnique.NodeType, seqExprNodeByUnique, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdgeByUnique(SequenceExpressionEdgeByUnique seqExprEdgeByUnique, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("edgeByUnique(", highlightingMode);
            PrintSequenceExpression(seqExprEdgeByUnique.EdgeUniqueId, seqExprEdgeByUnique, highlightingMode);
            if(seqExprEdgeByUnique.EdgeType != null)
            {
                env.PrintHighlighted(", ", highlightingMode);
                PrintSequenceExpression(seqExprEdgeByUnique.EdgeType, seqExprEdgeByUnique, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionSource(SequenceExpressionSource seqExprSource, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("source(", highlightingMode);
            PrintSequenceExpression(seqExprSource.Edge, seqExprSource, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTarget(SequenceExpressionTarget seqExprTarget, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("target(", highlightingMode);
            PrintSequenceExpression(seqExprTarget.Edge, seqExprTarget, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionOpposite(SequenceExpressionOpposite seqExprOpposite, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("opposite(", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Edge, seqExprOpposite, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprOpposite.Node, seqExprOpposite, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionAttributeAccess(SequenceExpressionAttributeAccess seqExprAttributeAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprAttributeAccess.Source, seqExprAttributeAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprAttributeAccess.AttributeName, highlightingMode);
        }

        private void PrintSequenceExpressionMatchAccess(SequenceExpressionMatchAccess seqExprMatchAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprMatchAccess.Source, seqExprMatchAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprMatchAccess.ElementName, highlightingMode);
        }

        private void PrintSequenceExpressionAttributeOrMatchAccess(SequenceExpressionAttributeOrMatchAccess seqExprAttributeOrMatchAccess, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprAttributeOrMatchAccess.Source, seqExprAttributeOrMatchAccess, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            env.PrintHighlighted(seqExprAttributeOrMatchAccess.AttributeOrElementName, highlightingMode);
        }

        private void PrintSequenceExpressionNodes(SequenceExpressionNodes seqExprNodes, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprNodes.NodeType != null)
                PrintSequenceExpression(seqExprNodes.NodeType, seqExprNodes, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEdges(SequenceExpressionEdges seqExprEdges, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprEdges.EdgeType != null)
                PrintSequenceExpression(seqExprEdges.EdgeType, seqExprEdges, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountNodes(SequenceExpressionCountNodes seqExprCountNodes, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountNodes.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountNodes.NodeType != null)
                PrintSequenceExpression(seqExprCountNodes.NodeType, seqExprCountNodes, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountEdges(SequenceExpressionCountEdges seqExprCountEdges, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountEdges.FunctionSymbol + "(", highlightingMode);
            if(seqExprCountEdges.EdgeType != null)
                PrintSequenceExpression(seqExprCountEdges.EdgeType, seqExprCountEdges, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionEmpty(SequenceExpressionEmpty seqExprEmpty, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("empty()", highlightingMode);
        }

        private void PrintSequenceExpressionNow(SequenceExpressionNow seqExprNow, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Time::now()", highlightingMode);
        }

        private void PrintSequenceExpressionSize(SequenceExpressionSize seqExprSize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("size()", highlightingMode);
        }

        private void PrintSequenceExpressionAdjacentIncident(SequenceExpressionAdjacentIncident seqExprAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprAdjacentIncident.SourceNode, seqExprAdjacentIncident, highlightingMode);
            if(seqExprAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.EdgeType, seqExprAdjacentIncident, highlightingMode);
            }
            if(seqExprAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprAdjacentIncident.OppositeNodeType, seqExprAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountAdjacentIncident(SequenceExpressionCountAdjacentIncident seqExprCountAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountAdjacentIncident.SourceNode, seqExprCountAdjacentIncident, highlightingMode);
            if(seqExprCountAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.EdgeType, seqExprCountAdjacentIncident, highlightingMode);
            }
            if(seqExprCountAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountAdjacentIncident.OppositeNodeType, seqExprCountAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionReachable(SequenceExpressionReachable seqExprReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprReachable.SourceNode, seqExprReachable, highlightingMode);
            if(seqExprReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.EdgeType, seqExprReachable, highlightingMode);
            }
            if(seqExprReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprReachable.OppositeNodeType, seqExprReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountReachable(SequenceExpressionCountReachable seqExprCountReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountReachable.SourceNode, seqExprCountReachable, highlightingMode);
            if(seqExprCountReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.EdgeType, seqExprCountReachable, highlightingMode);
            }
            if(seqExprCountReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountReachable.OppositeNodeType, seqExprCountReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionBoundedReachable(SequenceExpressionBoundedReachable seqExprBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.SourceNode, seqExprBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachable.Depth, seqExprBoundedReachable, highlightingMode);
            if(seqExprBoundedReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.EdgeType, seqExprBoundedReachable, highlightingMode);
            }
            if(seqExprBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachable.OppositeNodeType, seqExprBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionBoundedReachableWithRemainingDepth(SequenceExpressionBoundedReachableWithRemainingDepth seqExprBoundedReachableWithRemainingDepth, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprBoundedReachableWithRemainingDepth.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.SourceNode, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.Depth, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            if(seqExprBoundedReachableWithRemainingDepth.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.EdgeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            }
            if(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprBoundedReachableWithRemainingDepth.OppositeNodeType, seqExprBoundedReachableWithRemainingDepth, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCountBoundedReachable(SequenceExpressionCountBoundedReachable seqExprCountBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCountBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.SourceNode, seqExprCountBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprCountBoundedReachable.Depth, seqExprCountBoundedReachable, highlightingMode);
            if(seqExprCountBoundedReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.EdgeType, seqExprCountBoundedReachable, highlightingMode);
            }
            if(seqExprCountBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprCountBoundedReachable.OppositeNodeType, seqExprCountBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsBoundedReachable(SequenceExpressionIsBoundedReachable seqExprIsBoundedReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsBoundedReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.SourceNode, seqExprIsBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.EndElement, seqExprIsBoundedReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsBoundedReachable.Depth, seqExprIsBoundedReachable, highlightingMode);
            if(seqExprIsBoundedReachable.EdgeType != null)
            { 
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.EdgeType, seqExprIsBoundedReachable, highlightingMode);
            }
            if(seqExprIsBoundedReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsBoundedReachable.OppositeNodeType, seqExprIsBoundedReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsAdjacentIncident(SequenceExpressionIsAdjacentIncident seqExprIsAdjacentIncident, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsAdjacentIncident.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.SourceNode, seqExprIsAdjacentIncident, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsAdjacentIncident.EndElement, seqExprIsAdjacentIncident, highlightingMode);
            if(seqExprIsAdjacentIncident.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.EdgeType, seqExprIsAdjacentIncident, highlightingMode);
            }
            if(seqExprIsAdjacentIncident.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsAdjacentIncident.OppositeNodeType, seqExprIsAdjacentIncident, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionIsReachable(SequenceExpressionIsReachable seqExprIsReachable, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprIsReachable.FunctionSymbol + "(", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.SourceNode, seqExprIsReachable, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprIsReachable.EndElement, seqExprIsReachable, highlightingMode);
            if(seqExprIsReachable.EdgeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.EdgeType, seqExprIsReachable, highlightingMode);
            }
            if(seqExprIsReachable.OppositeNodeType != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprIsReachable.OppositeNodeType, seqExprIsReachable, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionInducedSubgraph(SequenceExpressionInducedSubgraph seqExprInducedSubgraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("inducedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprInducedSubgraph.NodeSet, seqExprInducedSubgraph, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionDefinedSubgraph(SequenceExpressionDefinedSubgraph seqExprDefinedSubgraph, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("definedSubgraph(", highlightingMode);
            PrintSequenceExpression(seqExprDefinedSubgraph.EdgeSet, seqExprDefinedSubgraph, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        // potential todo: change code structure in equals any sequence expression, too
        private void PrintSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqExprEqualsAny, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprEqualsAny.IncludingAttributes ? "equalsAny(" : "equalsAnyStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.Subgraph, seqExprEqualsAny, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprEqualsAny.SubgraphSet, seqExprEqualsAny, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionGetEquivalent(SequenceExpressionGetEquivalent seqExprGetEquivalent, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprGetEquivalent.IncludingAttributes ? "getEquivalent(" : "getEquivalentStructurally(", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.Subgraph, seqExprGetEquivalent, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            PrintSequenceExpression(seqExprGetEquivalent.SubgraphSet, seqExprGetEquivalent, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCanonize(SequenceExpressionCanonize seqExprCanonize, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("canonize(", highlightingMode);
            PrintSequenceExpression(seqExprCanonize.Graph, seqExprCanonize, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionNameof(SequenceExpressionNameof seqExprNameof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("nameof(", highlightingMode);
            if(seqExprNameof.NamedEntity != null)
                PrintSequenceExpression(seqExprNameof.NamedEntity, seqExprNameof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionUniqueof(SequenceExpressionUniqueof seqExprUniqueof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("uniqueof(", highlightingMode);
            if(seqExprUniqueof.UniquelyIdentifiedEntity != null)
                PrintSequenceExpression(seqExprUniqueof.UniquelyIdentifiedEntity, seqExprUniqueof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTypeof(SequenceExpressionTypeof seqExprTypeof, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("typeof(", highlightingMode);
            PrintSequenceExpression(seqExprTypeof.Entity, seqExprTypeof, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionExistsFile(SequenceExpressionExistsFile seqExprExistsFile, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::existsFile(", highlightingMode);
            PrintSequenceExpression(seqExprExistsFile.Path, seqExprExistsFile, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionImport(SequenceExpressionImport seqExprImport, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::import(", highlightingMode);
            PrintSequenceExpression(seqExprImport.Path, seqExprImport, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionCopy(SequenceExpressionCopy seqExprCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprCopy.Deep ? "copy(" : "clone(", highlightingMode);
            PrintSequenceExpression(seqExprCopy.ObjectToBeCopied, seqExprCopy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathMin(SequenceExpressionMathMin seqExprMathMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::min(", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Left, seqExprMathMin, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMin.Right, seqExprMathMin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathMax(SequenceExpressionMathMax seqExprMathMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::max(", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Left, seqExprMathMax, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            PrintSequenceExpression(seqExprMathMax.Right, seqExprMathMax, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathAbs(SequenceExpressionMathAbs seqExprMathAbs, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::abs(", highlightingMode);
            PrintSequenceExpression(seqExprMathAbs.Argument, seqExprMathAbs, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathCeil(SequenceExpressionMathCeil seqExprMathCeil, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::ceil(", highlightingMode);
            PrintSequenceExpression(seqExprMathCeil.Argument, seqExprMathCeil, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloor(SequenceExpressionMathFloor seqExprMathFloor, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floor(", highlightingMode);
            PrintSequenceExpression(seqExprMathFloor.Argument, seqExprMathFloor, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathRound(SequenceExpressionMathRound seqExprMathRound, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::round(", highlightingMode);
            PrintSequenceExpression(seqExprMathRound.Argument, seqExprMathRound, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathTruncate(SequenceExpressionMathTruncate seqExprMathTruncate, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::truncate(", highlightingMode);
            PrintSequenceExpression(seqExprMathTruncate.Argument, seqExprMathTruncate, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSqr(SequenceExpressionMathSqr seqExprMathSqr, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sqr(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqr.Argument, seqExprMathSqr, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSqrt(SequenceExpressionMathSqrt seqExprMathSqrt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sqrt(", highlightingMode);
            PrintSequenceExpression(seqExprMathSqrt.Argument, seqExprMathSqrt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathPow(SequenceExpressionMathPow seqExprPow, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::pow(", highlightingMode);
            if(seqExprPow.Left != null)
            {
                PrintSequenceExpression(seqExprPow.Left, seqExprPow, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
            }
            PrintSequenceExpression(seqExprPow.Right, seqExprPow, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathLog(SequenceExpressionMathLog seqExprMathLog, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::log(", highlightingMode);
            PrintSequenceExpression(seqExprMathLog.Left, seqExprMathLog, highlightingMode);
            if(seqExprMathLog.Right != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                PrintSequenceExpression(seqExprMathLog.Right, seqExprMathLog, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSgn(SequenceExpressionMathSgn seqExprMathSgn, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sgn(", highlightingMode);
            PrintSequenceExpression(seqExprMathSgn.Argument, seqExprMathSgn, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathSin(SequenceExpressionMathSin seqExprMathSin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::sin(", highlightingMode);
            PrintSequenceExpression(seqExprMathSin.Argument, seqExprMathSin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathCos(SequenceExpressionMathCos seqExprMathCos, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::cos(", highlightingMode);
            PrintSequenceExpression(seqExprMathCos.Argument, seqExprMathCos, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathTan(SequenceExpressionMathTan seqExprMathTan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::tan(", highlightingMode);
            PrintSequenceExpression(seqExprMathTan.Argument, seqExprMathTan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcSin(SequenceExpressionMathArcSin seqExprMathArcSin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arcsin(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcSin.Argument, seqExprMathArcSin, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcCos(SequenceExpressionMathArcCos seqExprMathArcCos, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arccos(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcCos.Argument, seqExprMathArcCos, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathArcTan(SequenceExpressionMathArcTan seqExprMathArcTan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::arctan(", highlightingMode);
            PrintSequenceExpression(seqExprMathArcTan.Argument, seqExprMathArcTan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionMathPi(SequenceExpressionMathPi seqExprMathPi, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::pi()", highlightingMode);
        }

        private void PrintSequenceExpressionMathE(SequenceExpressionMathE seqExprMathE, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::e()", highlightingMode);
        }

        private void PrintSequenceExpressionMathByteMin(SequenceExpressionMathByteMin seqExprMathByteMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::byteMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathByteMax(SequenceExpressionMathByteMax seqExprMathByteMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::byteMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathShortMin(SequenceExpressionMathShortMin seqExprMathShortMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::shortMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathShortMax(SequenceExpressionMathShortMax seqExprMathShortMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::shortMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathIntMin(SequenceExpressionMathIntMin seqExprMathIntMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::intMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathIntMax(SequenceExpressionMathIntMax seqExprMathIntMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::intMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathLongMin(SequenceExpressionMathLongMin seqExprMathLongMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::longMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathLongMax(SequenceExpressionMathLongMax seqExprMathLongMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::longMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloatMin(SequenceExpressionMathFloatMin seqExprMathFloatMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floatMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathFloatMax(SequenceExpressionMathFloatMax seqExprMathFloatMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::floatMax()", highlightingMode);
        }

        private void PrintSequenceExpressionMathDoubleMin(SequenceExpressionMathDoubleMin seqExprMathDoubleMin, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::doubleMin()", highlightingMode);
        }

        private void PrintSequenceExpressionMathDoubleMax(SequenceExpressionMathDoubleMax seqExprMathDoubleMax, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Math::doubleMax()", highlightingMode);
        }

        private void PrintSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqExprRuleQuery, SequenceBase parent, HighlightingMode highlightingMode)
        {
            string prefix = "";
            if(seqRenderer.Context.sequenceIdToBreakpointPosMap != null)
            {
                prefix = seqRenderer.GetBreakPrefix(seqExprRuleQuery);
            }

            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprRuleQuery == seqRenderer.Context.highlightSeq)
                highlightingModeLocal = seqRenderer.Context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            // GUI todo: edges from top-level sequence expression node to queries/mappings contained, instead of sea of nodes; and of course full solution with sequence expression tree
            seqRenderer.RenderSequenceBreakpointable(seqExprRuleQuery.RuleCall, seqExprRuleQuery, highlightingModeLocal, prefix); // rule all call with test flag, thus [?r]
        }

        private void PrintSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqExprMultiRuleQuery, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMultiRuleQuery == seqRenderer.Context.highlightSeq)
                highlightingModeLocal = seqRenderer.Context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = seqRenderer.AddNode(seqExprMultiRuleQuery, highlightingModeLocal, "[?[...]]");

            for(int i = seqExprMultiRuleQuery.MultiRuleCall.Sequences.Count - 1; i >= 0; --i)
            {
                SequenceRuleCall rule = (SequenceRuleCall)seqExprMultiRuleQuery.MultiRuleCall.Sequences[i];

                StringBuilder rsb = new StringBuilder();
                string returnAssignmentAsString = null;
                seqRenderer.PrintReturnAssignments(null, rule.ReturnVars, parent, highlightingMode, ref returnAssignmentAsString);
                rsb.Append(returnAssignmentAsString);
                rsb.Append(rule.DebugPrefix);
                string ruleCallAsString = null;
                seqRenderer.PrintRuleCallString(null, rule, parent, highlightingMode, ref ruleCallAsString);
                rsb.Append(ruleCallAsString);

                String ruleNodeName = seqRenderer.AddNode(rule, highlightingMode, rsb.ToString());
                env.guiForDataRendering.graphViewer.AddEdge(seqRenderer.GetUniqueEdgeName(operatorNodeName, ruleNodeName), operatorNodeName, ruleNodeName, seqRenderer.GetEdgeRealizer(highlightingMode), "rule" + i);
            }

            StringBuilder sb = new StringBuilder();
            foreach(SequenceFilterCallBase filterCall in seqExprMultiRuleQuery.MultiRuleCall.Filters)
            {
                string filterCallAsString = null;
                seqRenderer.PrintSequenceFilterCall(filterCall, seqExprMultiRuleQuery.MultiRuleCall, highlightingModeLocal, ref filterCallAsString); //highlightingModeLocal
                sb.Append(filterCallAsString);
            }
            sb.Append("\\<class " + seqExprMultiRuleQuery.MatchClass + ">");
            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "[?[...]]" + sb.ToString());
        }

        private void PrintSequenceExpressionMappingClause(SequenceExpressionMappingClause seqExprMappingClause, SequenceBase parent, HighlightingMode highlightingMode)
        {
            HighlightingMode highlightingModeLocal = highlightingMode;
            if(seqExprMappingClause == seqRenderer.Context.highlightSeq)
                highlightingModeLocal = seqRenderer.Context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

            String operatorNodeName = seqRenderer.AddNode(seqExprMappingClause, highlightingModeLocal, "[:...:]");

            for(int i = seqExprMappingClause.MultiRulePrefixedSequence.RulePrefixedSequences.Count - 1; i >= 0; --i)
            {
                SequenceRulePrefixedSequence seqRulePrefixedSequence = seqExprMappingClause.MultiRulePrefixedSequence.RulePrefixedSequences[i];

                HighlightingMode highlightingModeRulePrefixedSequence = highlightingModeLocal;
                if(seqRulePrefixedSequence == seqRenderer.Context.highlightSeq)
                    highlightingModeRulePrefixedSequence = seqRenderer.Context.success ? HighlightingMode.FocusSucces : HighlightingMode.Focus;

                String subOperatorNodeName = seqRenderer.AddNode(seqRulePrefixedSequence, highlightingModeRulePrefixedSequence, "for{.;.}");
                env.guiForDataRendering.graphViewer.AddEdge(seqRenderer.GetUniqueEdgeName(operatorNodeName, subOperatorNodeName), operatorNodeName, subOperatorNodeName, seqRenderer.GetEdgeRealizer(highlightingMode), "sub" + i);

                String subNodeName = seqRenderer.RenderSequence(seqRulePrefixedSequence.Sequence, seqRulePrefixedSequence, highlightingMode);
                env.guiForDataRendering.graphViewer.AddEdge(seqRenderer.GetUniqueEdgeName(subOperatorNodeName, subNodeName), subOperatorNodeName, subNodeName, seqRenderer.GetEdgeRealizer(highlightingMode), "sub");
                String ruleNodeName = seqRenderer.RenderSequence(seqRulePrefixedSequence.Rule, seqRulePrefixedSequence, highlightingModeRulePrefixedSequence);
                env.guiForDataRendering.graphViewer.AddEdge(seqRenderer.GetUniqueEdgeName(subOperatorNodeName, ruleNodeName), subOperatorNodeName, ruleNodeName, seqRenderer.GetEdgeRealizer(highlightingModeRulePrefixedSequence), "rule");
            }

            StringBuilder sb = new StringBuilder();
            foreach(SequenceFilterCallBase filterCall in seqExprMappingClause.MultiRulePrefixedSequence.Filters)
            {
                string filterCallAsString = null;
                seqRenderer.PrintSequenceFilterCall(filterCall, seqExprMappingClause.MultiRulePrefixedSequence, highlightingModeLocal, ref filterCallAsString);
                sb.Append(filterCallAsString);
            }

            env.guiForDataRendering.graphViewer.SetNodeLabel(operatorNodeName, "[:...:]" + sb.ToString());
        }

        private void PrintSequenceExpressionScan(SequenceExpressionScan seqExprScan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("scan", highlightingMode);
            if(seqExprScan.ResultType != null)
                env.PrintHighlighted("<" + seqExprScan.ResultType + ">", highlightingMode);
            env.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprScan.StringExpr, seqExprScan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionTryScan(SequenceExpressionTryScan seqExprTryScan, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("tryscan", highlightingMode);
            if(seqExprTryScan.ResultType != null)
                env.PrintHighlighted("<" + seqExprTryScan.ResultType + ">", highlightingMode);
            env.PrintHighlighted("(", highlightingMode);
            PrintSequenceExpression(seqExprTryScan.StringExpr, seqExprTryScan, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceExpressionFunctionCall(SequenceExpressionFunctionCall seqExprFunctionCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqExprFunctionCall.Name, highlightingMode);
            if(seqExprFunctionCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqExprFunctionCall.ArgumentExpressions.Length; ++i)
                {
                    PrintSequenceExpression(seqExprFunctionCall.ArgumentExpressions[i], seqExprFunctionCall, highlightingMode);
                    if(i != seqExprFunctionCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        private void PrintSequenceExpressionFunctionMethodCall(SequenceExpressionFunctionMethodCall seqExprFunctionMethodCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceExpression(seqExprFunctionMethodCall.TargetExpr, seqExprFunctionMethodCall, highlightingMode);
            env.PrintHighlighted(".", highlightingMode);
            PrintSequenceExpressionFunctionCall(seqExprFunctionMethodCall, parent, highlightingMode);
        }
    }
}
