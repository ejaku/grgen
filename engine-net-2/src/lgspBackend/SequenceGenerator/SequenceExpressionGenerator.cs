/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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


        public SequenceExpressionGenerator(IGraphModel model, SequenceCheckingEnvironment env, SequenceGeneratorHelper seqHelper)
        {
            this.model = model;
            this.env = env;
            this.seqHelper = seqHelper;
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
            case SequenceExpressionType.Lower:
                return GetSequenceExpressionLower((SequenceExpressionLower)expr, source);
            case SequenceExpressionType.Greater:
                return GetSequenceExpressionGreater((SequenceExpressionGreater)expr, source);
            case SequenceExpressionType.LowerEqual:
                return GetSequenceExpressionLowerEqual((SequenceExpressionLowerEqual)expr, source);
            case SequenceExpressionType.GreaterEqual:
                return GetSequenceExpressionGreaterEqual((SequenceExpressionGreaterEqual)expr, source);
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
            case SequenceExpressionType.UnaryMinus:
                return GetSequenceExpressionUnaryMinus((SequenceExpressionUnaryMinus)expr, source);
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
                return GetSequenceExpressionCopy((SequenceExpressionCopy)expr, source);
            case SequenceExpressionType.GraphElementAttributeOrElementOfMatch:
                if(((SequenceExpressionAttributeOrMatchAccess)expr).AttributeAccess != null)
                    return GetSequenceExpressionGraphElementAttribute(((SequenceExpressionAttributeOrMatchAccess)expr).AttributeAccess, source);
                else if(((SequenceExpressionAttributeOrMatchAccess)expr).MatchAccess != null)
                    return GetSequenceExpressionElementOfMatch(((SequenceExpressionAttributeOrMatchAccess)expr).MatchAccess, source);
                else
                    return GetSequenceExpressionGraphElementAttributeOrElementOfMatch((SequenceExpressionAttributeOrMatchAccess)expr, source);
            case SequenceExpressionType.Constant:
                return GetSequenceExpressionConstant((SequenceExpressionConstant)expr, source);
            case SequenceExpressionType.Variable:
                return GetSequenceExpressionVariable((SequenceExpressionVariable)expr, source);
            case SequenceExpressionType.RuleQuery:
                return GetSequenceExpressionRuleQuery((SequenceExpressionRuleQuery)expr, source);
            case SequenceExpressionType.MultiRuleQuery:
                return GetSequenceExpressionMultiRuleQuery((SequenceExpressionMultiRuleQuery)expr, source);
            case SequenceExpressionType.FunctionCall:
                return GetSequenceExpressionFunctionCall((SequenceExpressionFunctionCall)expr, source);
            case SequenceExpressionType.FunctionMethodCall:
                return GetSequenceExpressionFunctionMethodCall((SequenceExpressionFunctionMethodCall)expr, source);

            // graph expressions
            case SequenceExpressionType.StructuralEqual:
                return GetSequenceExpressionStructuralEqual((SequenceExpressionStructuralEqual)expr, source);
            case SequenceExpressionType.EqualsAny:
                return GetSequenceExpressionEqualsAny((SequenceExpressionEqualsAny)expr, source);
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
            case SequenceExpressionType.InContainer:
                return GetSequenceExpressionInContainer((SequenceExpressionInContainer)expr, source);
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
            case SequenceExpressionType.StringAsArray:
                return GetSequenceExpressionStringAsArray((SequenceExpressionStringAsArray)expr, source);
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
            case SequenceExpressionType.ArrayOrDequeIndexOf:
                return GetSequenceExpressionArrayOrDequeIndexOf((SequenceExpressionArrayOrDequeIndexOf)expr, source);
            case SequenceExpressionType.ArrayOrDequeLastIndexOf:
                return GetSequenceExpressionArrayOrDequeLastIndexOf((SequenceExpressionArrayOrDequeLastIndexOf)expr, source);
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
            case SequenceExpressionType.ArrayKeepOneForEach:
                return GetSequenceExpressionArrayKeepOneForEach((SequenceExpressionArrayKeepOneForEach)expr, source);
            case SequenceExpressionType.ArrayReverse:
                return GetSequenceExpressionArrayReverse((SequenceExpressionArrayReverse)expr, source);
            case SequenceExpressionType.ArrayExtract:
                return GetSequenceExpressionArrayExtract((SequenceExpressionArrayExtract)expr, source);

            default:
                throw new Exception("Unknown sequence expression type: " + expr.SequenceExpressionType);
            }
        }

        private string GetSequenceExpressionConditional(SequenceExpressionConditional seqCond, SourceBuilder source)
        {
            return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                + " ? (object)" + GetSequenceExpression(seqCond.TrueCase, source)
                + " : (object)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
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
            return "((bool)" + GetSequenceExpression(seq.Left, source) + " ^ (bool)" + GetSequenceExpression(seq.Right, source) + ")";
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

        private string GetSequenceExpressionCast(SequenceExpressionCast seqCast, SourceBuilder source)
        {
            string targetType = "UNSUPPORTED TYPE CAST";
            if(seqCast.TargetType is NodeType)
                targetType = ((NodeType)seqCast.TargetType).NodeInterfaceName;
            if(seqCast.TargetType is EdgeType)
                targetType = ((EdgeType)seqCast.TargetType).EdgeInterfaceName;
            // TODO: handle the non-node and non-edge-types, too
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
                + ", (int)" + GetSequenceExpression(seqIsVisited.VisitedFlagExpr, source)
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

        private string GetSequenceExpressionCopy(SequenceExpressionCopy seqCopy, SourceBuilder source)
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
            else //if(seqCopy.ObjectToBeCopied.Type(env) == "")
                return "GRGEN_LIBGR.TypesHelper.Clone(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ")";
        }

        private string GetSequenceExpressionGraphElementAttributeOrElementOfMatch(SequenceExpressionAttributeOrMatchAccess seqAttrOrMa, SourceBuilder source)
        {
            return "GRGEN_LIBGR.ContainerHelper.GetGraphElementAttributeOrElementOfMatch(" 
                + GetSequenceExpression(seqAttrOrMa.Source, source) + ", (string)(\"" + seqAttrOrMa.AttributeOrElementName + "\"))";
        }

        private string GetSequenceExpressionElementOfMatch(SequenceExpressionMatchAccess seqMA, SourceBuilder source)
        {
            String rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqMA.Source.Type(env));
            String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqMA.Source.Type(env)));
            string match = "((" + matchInterfaceName + ")" + GetSequenceExpression(seqMA.Source, source) + ")";
            if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                return match + ".node_" + seqMA.ElementName;
            else if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                return match + ".edge_" + seqMA.ElementName;
            else
                return match + ".var_" + seqMA.ElementName;
        }

        private string GetSequenceExpressionGraphElementAttribute(SequenceExpressionAttributeAccess seqAttr, SourceBuilder source)
        {
            string element = "((GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(seqAttr.Source, source) + ")";
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

        private string GetSequenceExpressionRuleQuery(SequenceExpressionRuleQuery seqRuleQuery, SourceBuilder source)
        {
            SequenceRuleAllCall ruleCall = seqRuleQuery.RuleCall;
            return GetSequenceExpressionRuleCall(ruleCall, source);
        }

        private string GetSequenceExpressionRuleCall(SequenceRuleCall ruleCall, SourceBuilder source)
        {
            String matchingPatternClassName = "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Rule_" + ruleCall.Name;
            String patternName = ruleCall.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            SourceBuilder matchesSourceBuilder = new SourceBuilder();
            matchesSourceBuilder.AppendFormat("((GRGEN_LIBGR.IMatchesExact<{0}>)procEnv.MatchForQuery({1}, {2}{3}, procEnv.MaxMatches, {4}))",
                matchType, "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(ruleCall.Package) + "Action_" + ruleCall.Name + ".Instance",
                ruleCall.Subgraph != null ? seqHelper.GetVar(ruleCall.Subgraph) : "null",
                seqHelper.BuildParametersInObject(ruleCall, ruleCall.ArgumentExpressions, source), ruleCall.Special ? "true" : "false");
            for(int i = 0; i < ruleCall.Filters.Count; ++i)
            {
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitFilterCall(matchesSourceBuilder, (SequenceFilterCallCompiled)ruleCall.Filters[i], patternName, matchesSource, ruleCall.PackagePrefixedName, true);
            }
            return matchesSourceBuilder.ToString() + ".ToListExact()";
        }

        private string GetSequenceExpressionMultiRuleQuery(SequenceExpressionMultiRuleQuery seqMultiRuleQuery, SourceBuilder source)
        {
            SequenceMultiRuleAllCall seqMulti = seqMultiRuleQuery.MultiRuleCall;

            String matchListName = "MatchList_" + seqMulti.Id;
            source.AppendFrontFormat("List<GRGEN_LIBGR.IMatch> {0} = new List<GRGEN_LIBGR.IMatch>();\n", matchListName);

            SourceBuilder matchesSourceBuilder = new SourceBuilder();

            String matchType = NamesOfEntities.MatchInterfaceName(seqMultiRuleQuery.MatchClass);
            if(seqMulti.Filters.Count != 0)
                matchesSourceBuilder.AppendFormat("GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_LIBGR.IMatch>(");
            else
                matchesSourceBuilder.AppendFormat("GRGEN_LIBGR.MatchListHelper.ToList<{0}>(", matchType);

            matchesSourceBuilder.Append(GetRuleCallOfSequenceMultiRuleAllCall(seqMulti, seqMulti.Sequences.Count - 1, matchListName, source));

            // emit code for match class (non-rule-based) filtering
            foreach(SequenceFilterCall sequenceFilterCall in seqMulti.Filters)
            {
                String matchesSource = matchesSourceBuilder.ToString();
                matchesSourceBuilder.Reset();
                EmitMatchClassFilterCall(matchesSourceBuilder, (SequenceFilterCallCompiled)sequenceFilterCall, matchesSource, true);
            }

            matchesSourceBuilder.Append(")");

            if(seqMulti.Filters.Count != 0)
                return "GRGEN_LIBGR.MatchListHelper.ToList<" + matchType + ">(" + matchesSourceBuilder.ToString() + ")";
            else
                return matchesSourceBuilder.ToString();
        }

        private string GetRuleCallOfSequenceMultiRuleAllCall(SequenceMultiRuleAllCall seqMulti, int index, String matchListName, SourceBuilder source)
        {
            SequenceRuleCall ruleCall = (SequenceRuleCall)seqMulti.Sequences[index];
            if(index == 0)
                return "GRGEN_LIBGR.MatchListHelper.AddReturn(" + matchListName + ", " + GetSequenceExpressionRuleCall(ruleCall, source) + ")";
            else
                return "GRGEN_LIBGR.MatchListHelper.AddReturn(" + GetRuleCallOfSequenceMultiRuleAllCall(seqMulti, index - 1, matchListName, source) + ",\n" + GetSequenceExpressionRuleCall(ruleCall, source) + ")";
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
                sb.Append(seqHelper.BuildParameters(seqFuncCall, seqFuncCall.ArgumentExpressions, TypesHelper.GetNodeOrEdgeType(seqFuncCall.TargetExpr.Type(env), model).GetFunctionMethod(seqFuncCall.Name), source));
                sb.Append(")");
            }
            return sb.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------

        #region Graph expressions

        private string GetSequenceExpressionStructuralEqual(SequenceExpressionStructuralEqual seq, SourceBuilder source)
        {
            string leftExpr = GetSequenceExpression(seq.Left, source);
            string rightExpr = GetSequenceExpression(seq.Right, source);
            return SequenceExpressionGeneratorHelper.StructuralEqualStatic(leftExpr, rightExpr);
        }

        private string GetSequenceExpressionEqualsAny(SequenceExpressionEqualsAny seqEqualsAny, SourceBuilder source)
        {
            if(seqEqualsAny.IncludingAttributes)
                return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", true)";
            else
                return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", false)";
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

        private string GetSequenceExpressionInContainer(SequenceExpressionInContainer seqIn, SourceBuilder source)
        {
            string container;
            string ContainerType;
            if(seqIn.ContainerExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess seqInAttribute = (SequenceExpressionAttributeAccess)(seqIn.ContainerExpr);
                string element = "((GRGEN_LIBGR.IGraphElement)" + GetSequenceExpression(seqInAttribute.Source, source) + ")";
                container = element + ".GetAttribute(\"" + seqInAttribute.AttributeName + "\")";
                ContainerType = seqInAttribute.Type(env);
            }
            else
            {
                container = GetSequenceExpression(seqIn.ContainerExpr, source);
                ContainerType = seqIn.ContainerExpr.Type(env);
            }

            if(ContainerType == "")
            {
                string valueExpr = GetSequenceExpression(seqIn.Expr, source);
                string containerExpr = GetSequenceExpression(seqIn.ContainerExpr, source);
                return "GRGEN_LIBGR.ContainerHelper.InContainer(procEnv, " + containerExpr + ", " + valueExpr + ")";
            }
            else if(ContainerType.StartsWith("array"))
            {
                string array = container;
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                string sourceExpr = "((" + arrayValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return array + ".Contains(" + sourceExpr + ")";
            }
            else if(ContainerType.StartsWith("deque"))
            {
                string deque = container;
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
                string sourceExpr = "((" + dequeValueType + ")" + GetSequenceExpression(seqIn.Expr, source) + ")";
                return deque + ".Contains(" + sourceExpr + ")";
            }
            else
            {
                string dictionary = container;
                string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(ContainerType), model);
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
                    GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(seqContainerAttribute.Source.Type(env), env.Model);
                    AttributeType attributeType = nodeOrEdgeType.GetAttributeType(seqContainerAttribute.AttributeName);
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

        private string GetSequenceExpressionArrayOrDequeIndexOf(SequenceExpressionArrayOrDequeIndexOf seqArrayOrDequeIndexOf, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOrDequeIndexOf, source);

            if(seqArrayOrDequeIndexOf.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.IndexOf(" + container + ", " 
                    + GetSequenceExpression(seqArrayOrDequeIndexOf.ValueToSearchForExpr, source)
                    + (seqArrayOrDequeIndexOf.StartPositionExpr != null ? "," + GetSequenceExpression(seqArrayOrDequeIndexOf.StartPositionExpr, source) : "")
                    + ")";
            }
            else if(seqArrayOrDequeIndexOf.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeIndexOf.ContainerType(env), model);
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayOrDequeIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.IndexOf((" + arrayType + ")(" + container + "), "
                    + "(" + arrayValueType + ")(" + GetSequenceExpression(seqArrayOrDequeIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqArrayOrDequeIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqArrayOrDequeIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else //if(seqArrayOrDequeIndexOf.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeIndexOf.ContainerType(env), model);
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayOrDequeIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.IndexOf((" + dequeType + ")(" + container + "), "
                    + "(" + dequeValueType + ")(" + GetSequenceExpression(seqArrayOrDequeIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqArrayOrDequeIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqArrayOrDequeIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
        }

        private string GetSequenceExpressionArrayOrDequeLastIndexOf(SequenceExpressionArrayOrDequeLastIndexOf seqArrayOrDequeLastIndexOf, SourceBuilder source)
        {
            string container = GetContainerValue(seqArrayOrDequeLastIndexOf, source);

            if(seqArrayOrDequeLastIndexOf.ContainerType(env) == "")
            {
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf(" + container + ", "
                    + GetSequenceExpression(seqArrayOrDequeLastIndexOf.ValueToSearchForExpr, source)
                    + (seqArrayOrDequeLastIndexOf.StartPositionExpr != null ? "," + GetSequenceExpression(seqArrayOrDequeLastIndexOf.StartPositionExpr, source) : "")
                    + ")";
            }
            else if(seqArrayOrDequeLastIndexOf.ContainerType(env).StartsWith("array<"))
            {
                string arrayType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeLastIndexOf.ContainerType(env), model);
                string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayOrDequeLastIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf((" + arrayType + ")(" + container + "), "
                    + "(" + arrayValueType + ")(" + GetSequenceExpression(seqArrayOrDequeLastIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqArrayOrDequeLastIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqArrayOrDequeLastIndexOf.StartPositionExpr, source) + ")" : "")
                    + ")";
            }
            else //if(seqArrayOrDequeLastIndexOf.ContainerType(env).StartsWith("deque<"))
            {
                string dequeType = TypesHelper.XgrsTypeToCSharpType(seqArrayOrDequeLastIndexOf.ContainerType(env), model);
                string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqArrayOrDequeLastIndexOf.ContainerType(env)), model);
                return "GRGEN_LIBGR.ContainerHelper.LastIndexOf((" + dequeType + ")(" + container + "), "
                    + "(" + dequeValueType + ")(" + GetSequenceExpression(seqArrayOrDequeLastIndexOf.ValueToSearchForExpr, source) + ")"
                    + (seqArrayOrDequeLastIndexOf.StartPositionExpr != null ? ", (int)(" + GetSequenceExpression(seqArrayOrDequeLastIndexOf.StartPositionExpr, source) + ")" : "")
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
                    string comparerName = NamesOfEntities.ComparerClassName(graphElementTypeName, packageName, attribute);
                    return "GRGEN_MODEL." + comparerName +".Extract(" + array + ")";
                }
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
            source.AppendFrontFormat("{0}.Filter_{1}(", matchesSource, filterAutoSupplied.Name);
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

        #endregion Filters
    }
}
