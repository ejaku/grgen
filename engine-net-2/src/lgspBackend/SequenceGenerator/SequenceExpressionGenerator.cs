/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
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
        IGraphModel model;

        SequenceCheckingEnvironment env;

        SequenceGeneratorHelper helper;


        public SequenceExpressionGenerator(IGraphModel model, SequenceCheckingEnvironment env, SequenceGeneratorHelper helper)
        {
            this.model = model;
            this.env = env;
            this.helper = helper;
        }

        // source is needed for a method call chain or expressions that require temporary variables, 
        // to emit the state changing computation methods or the temporary variable declarations (not the assignement, needs to be computed from inside the expression)
        // before returning the final expression method call ready to be emitted
        public string GetSequenceExpression(SequenceExpression expr, SourceBuilder source)
        {
            switch(expr.SequenceExpressionType)
            {
                case SequenceExpressionType.Conditional:
                {
                    SequenceExpressionConditional seqCond = (SequenceExpressionConditional)expr;
                    return "( (bool)" + GetSequenceExpression(seqCond.Condition, source)
                        + " ? (object)" + GetSequenceExpression(seqCond.TrueCase, source)
                        + " : (object)" + GetSequenceExpression(seqCond.FalseCase, source) + " )";
                }

                case SequenceExpressionType.LazyOr:
                {
                    SequenceExpressionLazyOr seq = (SequenceExpressionLazyOr)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " || (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }

                case SequenceExpressionType.LazyAnd:
                {
                    SequenceExpressionLazyAnd seq = (SequenceExpressionLazyAnd)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " && (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictOr:
                {
                    SequenceExpressionStrictOr seq = (SequenceExpressionStrictOr)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " | (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictXor:
                {
                    SequenceExpressionStrictXor seq = (SequenceExpressionStrictXor)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " ^ (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }
                
                case SequenceExpressionType.StrictAnd:
                {
                    SequenceExpressionStrictAnd seq = (SequenceExpressionStrictAnd)expr;
                    return "((bool)" + GetSequenceExpression(seq.Left, source) + " & (bool)" + GetSequenceExpression(seq.Right, source) + ")";
                }

                case SequenceExpressionType.Equal:
                {
                    SequenceExpressionEqual seq = (SequenceExpressionEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.EqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.EqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Equal, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.StructuralEqual:
                {
                    SequenceExpressionStructuralEqual seq = (SequenceExpressionStructuralEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    return SequenceExpressionHelperStatic.StructuralEqualStatic(leftExpr, rightExpr);
                }

                case SequenceExpressionType.NotEqual:
                {
                    SequenceExpressionNotEqual seq = (SequenceExpressionNotEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.NotEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.NotEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.NotEqual, " + leftType + ", " + rightType + ", graph.Model), "
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Lower:
                {
                    SequenceExpressionLower seq = (SequenceExpressionLower)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.LowerStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Lower, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Greater:
                {
                    SequenceExpressionGreater seq = (SequenceExpressionGreater)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.GreaterStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Greater, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.LowerEqual:
                {
                    SequenceExpressionLowerEqual seq = (SequenceExpressionLowerEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.LowerEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.LowerEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.LowerEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.GreaterEqual:
                {
                    SequenceExpressionGreaterEqual seq = (SequenceExpressionGreaterEqual)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.GreaterEqualStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.GreaterEqualObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.GreaterEqual, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Plus:
                {
                    SequenceExpressionPlus seq = (SequenceExpressionPlus)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.PlusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.PlusObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Plus, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Minus:
                {
                    SequenceExpressionMinus seq = (SequenceExpressionMinus)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.MinusStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.MinusObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Minus, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Mul:
                {
                    SequenceExpressionMul seq = (SequenceExpressionMul)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.MulStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.MulObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mul, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Div:
                {
                    SequenceExpressionDiv seq = (SequenceExpressionDiv)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.DivStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.DivObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Div, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Mod:
                {
                    SequenceExpressionMod seq = (SequenceExpressionMod)expr;
                    string leftExpr = GetSequenceExpression(seq.Left, source);
                    string rightExpr = GetSequenceExpression(seq.Right, source);
                    string leftType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + leftExpr + ", graph.Model)";
                    string rightType = "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + rightExpr + ", graph.Model)";
                    if(seq.BalancedTypeStatic != "")
                        return SequenceExpressionHelperStatic.ModStatic(leftExpr, rightExpr, seq.BalancedTypeStatic, seq.LeftTypeStatic, seq.RightTypeStatic, model);
                    else
                        return "GRGEN_LIBGR.SequenceExpressionHelper.ModObjects("
                            + leftExpr + ", " + rightExpr + ", "
                            + "GRGEN_LIBGR.SequenceExpressionTypeHelper.Balance(GRGEN_LIBGR.SequenceExpressionType.Mod, " + leftType + ", " + rightType + ", graph.Model),"
                            + leftType + ", " + rightType + ", graph)";
                }

                case SequenceExpressionType.Not:
                {
                    SequenceExpressionNot seqNot = (SequenceExpressionNot)expr;
                    return "!" + "((bool)" + GetSequenceExpression(seqNot.Operand, source) + ")";
                }

                case SequenceExpressionType.Cast:
                {
                    SequenceExpressionCast seqCast = (SequenceExpressionCast)expr;
                    string targetType = "UNSUPPORTED TYPE CAST";
                    if(seqCast.TargetType is NodeType)
                        targetType = ((NodeType)seqCast.TargetType).NodeInterfaceName;
                    if(seqCast.TargetType is EdgeType)
                        targetType = ((EdgeType)seqCast.TargetType).EdgeInterfaceName;
                    // TODO: handle the non-node and non-edge-types, too
                    return "((" + targetType + ")" + GetSequenceExpression(seqCast.Operand, source) + ")";
                }

                case SequenceExpressionType.Def:
                {
                    SequenceExpressionDef seqDef = (SequenceExpressionDef)expr;
                    String condition = "(";
                    bool isFirst = true;
                    foreach(SequenceExpression var in seqDef.DefVars)
                    {
                        if(isFirst) isFirst = false;
                        else condition += " && ";
                        condition += GetSequenceExpression(var, source) + "!=null";
                    }
                    condition += ")";
                    return condition;
                }

                case SequenceExpressionType.InContainer:
                {
                    SequenceExpressionInContainer seqIn = (SequenceExpressionInContainer)expr;

                    string container;
                    string ContainerType;
                    if(seqIn.ContainerExpr is SequenceExpressionAttributeAccess)
                    {
                        SequenceExpressionAttributeAccess seqInAttribute = (SequenceExpressionAttributeAccess)(seqIn.ContainerExpr);
                        string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqInAttribute.SourceVar) + ")";
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
                        SourceBuilder sb = new SourceBuilder();

                        string sourceExpr = GetSequenceExpression(seqIn.Expr, source);
                        string containerVar = "tmp_eval_once_" + seqIn.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Contains(" + sourceExpr + ")");

                        sb.AppendFront(")");

                        return sb.ToString();
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

                case SequenceExpressionType.IsVisited:
                {
                    SequenceExpressionIsVisited seqIsVisited = (SequenceExpressionIsVisited)expr;
                    return "graph.IsVisited("
                        + "(GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqIsVisited.GraphElementVar)
                        + ", (int)" + GetSequenceExpression(seqIsVisited.VisitedFlagExpr, source)
                        + ")";
                }

                case SequenceExpressionType.Nodes:
                {
                    SequenceExpressionNodes seqNodes = (SequenceExpressionNodes)expr;
                    string nodeType = helper.ExtractNodeType(source, seqNodes.NodeType);
                    string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.Nodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.Edges:
                {
                    SequenceExpressionEdges seqEdges = (SequenceExpressionEdges)expr;
                    string edgeType = helper.ExtractEdgeType(source, seqEdges.EdgeType);
                    string edgeRootType = SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqEdges.EdgeType, env);
                    string directedness = helper.GetDirectedness(edgeRootType);
                    string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.Edges" + directedness + "(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountNodes:
                {
                    SequenceExpressionCountNodes seqNodes = (SequenceExpressionCountNodes)expr;
                    string nodeType = helper.ExtractNodeType(source, seqNodes.NodeType);
                    string profilingArgument = seqNodes.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.CountNodes(graph, (GRGEN_LIBGR.NodeType)" + nodeType + profilingArgument + ")";
                }

                case SequenceExpressionType.CountEdges:
                {
                    SequenceExpressionCountEdges seqEdges = (SequenceExpressionCountEdges)expr;
                    string edgeType = helper.ExtractEdgeType(source, seqEdges.EdgeType);
                    string profilingArgument = seqEdges.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.CountEdges(graph, (GRGEN_LIBGR.EdgeType)" + edgeType + profilingArgument + ")";
                }

                case SequenceExpressionType.Now:
                {
                    SequenceExpressionNow seqNow = (SequenceExpressionNow)expr;
                    return "DateTime.UtcNow.ToFileTime()";
                }

                case SequenceExpressionType.Empty:
                {
                    SequenceExpressionEmpty seqEmpty = (SequenceExpressionEmpty)expr;
                    return "(graph.NumNodes+graph.NumEdges==0)";
                }

                case SequenceExpressionType.Size:
                {
                    SequenceExpressionSize seqSize = (SequenceExpressionSize)expr;
                    return "(graph.NumNodes+graph.NumEdges)";
                }

                case SequenceExpressionType.AdjacentNodes:
                case SequenceExpressionType.AdjacentNodesViaIncoming:
                case SequenceExpressionType.AdjacentNodesViaOutgoing:
                case SequenceExpressionType.IncidentEdges:
                case SequenceExpressionType.IncomingEdges:
                case SequenceExpressionType.OutgoingEdges:
                {
                    SequenceExpressionAdjacentIncident seqAdjInc = (SequenceExpressionAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqAdjInc.SourceNode, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqAdjInc.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqAdjInc.OppositeNodeType);
                    string directedness = helper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqAdjInc.EdgeType, env));
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

                case SequenceExpressionType.CountAdjacentNodes:
                case SequenceExpressionType.CountAdjacentNodesViaIncoming:
                case SequenceExpressionType.CountAdjacentNodesViaOutgoing:
                case SequenceExpressionType.CountIncidentEdges:
                case SequenceExpressionType.CountIncomingEdges:
                case SequenceExpressionType.CountOutgoingEdges:
                {
                    SequenceExpressionCountAdjacentIncident seqCntAdjInc = (SequenceExpressionCountAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqCntAdjInc.SourceNode, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqCntAdjInc.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqCntAdjInc.OppositeNodeType);
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

                case SequenceExpressionType.ReachableNodes:
                case SequenceExpressionType.ReachableNodesViaIncoming:
                case SequenceExpressionType.ReachableNodesViaOutgoing:
                case SequenceExpressionType.ReachableEdges:
                case SequenceExpressionType.ReachableEdgesViaIncoming:
                case SequenceExpressionType.ReachableEdgesViaOutgoing:
                {
                    SequenceExpressionReachable seqReach = (SequenceExpressionReachable)expr;
                    string sourceNode = GetSequenceExpression(seqReach.SourceNode, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqReach.OppositeNodeType);
                    string directedness = helper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqReach.EdgeType, env));
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

                case SequenceExpressionType.CountReachableNodes:
                case SequenceExpressionType.CountReachableNodesViaIncoming:
                case SequenceExpressionType.CountReachableNodesViaOutgoing:
                case SequenceExpressionType.CountReachableEdges:
                case SequenceExpressionType.CountReachableEdgesViaIncoming:
                case SequenceExpressionType.CountReachableEdgesViaOutgoing:
                {
                    SequenceExpressionCountReachable seqCntReach = (SequenceExpressionCountReachable)expr;
                    string sourceNode = GetSequenceExpression(seqCntReach.SourceNode, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqCntReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqCntReach.OppositeNodeType);
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

                case SequenceExpressionType.BoundedReachableNodes:
                case SequenceExpressionType.BoundedReachableNodesViaIncoming:
                case SequenceExpressionType.BoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.BoundedReachableEdges:
                case SequenceExpressionType.BoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.BoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionBoundedReachable seqBoundReach = (SequenceExpressionBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqBoundReach.Depth, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqBoundReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqBoundReach.OppositeNodeType);
                    string directedness = helper.GetDirectedness(SequenceExpressionGraphQuery.GetEdgeRootTypeWithDirection(seqBoundReach.EdgeType, env));
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

                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepth:
                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaIncoming:
                case SequenceExpressionType.BoundedReachableNodesWithRemainingDepthViaOutgoing:
                {
                    SequenceExpressionBoundedReachableWithRemainingDepth seqBoundReach = (SequenceExpressionBoundedReachableWithRemainingDepth)expr;
                    string sourceNode = GetSequenceExpression(seqBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqBoundReach.Depth, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqBoundReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqBoundReach.OppositeNodeType);
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

                case SequenceExpressionType.CountBoundedReachableNodes:
                case SequenceExpressionType.CountBoundedReachableNodesViaIncoming:
                case SequenceExpressionType.CountBoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.CountBoundedReachableEdges:
                case SequenceExpressionType.CountBoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.CountBoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionCountBoundedReachable seqCntBoundReach = (SequenceExpressionCountBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqCntBoundReach.SourceNode, source);
                    string depth = GetSequenceExpression(seqCntBoundReach.Depth, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqCntBoundReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqCntBoundReach.OppositeNodeType);
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

                case SequenceExpressionType.IsAdjacentNodes:
                case SequenceExpressionType.IsAdjacentNodesViaIncoming:
                case SequenceExpressionType.IsAdjacentNodesViaOutgoing:
                case SequenceExpressionType.IsIncidentEdges:
                case SequenceExpressionType.IsIncomingEdges:
                case SequenceExpressionType.IsOutgoingEdges:
                {
                    SequenceExpressionIsAdjacentIncident seqIsAdjInc = (SequenceExpressionIsAdjacentIncident)expr;
                    string sourceNode = GetSequenceExpression(seqIsAdjInc.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsAdjInc.EndElement, source);
                    string endElementType;
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqIsAdjInc.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqIsAdjInc.OppositeNodeType);
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

                case SequenceExpressionType.IsReachableNodes:
                case SequenceExpressionType.IsReachableNodesViaIncoming:
                case SequenceExpressionType.IsReachableNodesViaOutgoing:
                case SequenceExpressionType.IsReachableEdges:
                case SequenceExpressionType.IsReachableEdgesViaIncoming:
                case SequenceExpressionType.IsReachableEdgesViaOutgoing:
                {
                    SequenceExpressionIsReachable seqIsReach = (SequenceExpressionIsReachable)expr;
                    string sourceNode = GetSequenceExpression(seqIsReach.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsReach.EndElement, source);
                    string endElementType;
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqIsReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqIsReach.OppositeNodeType);
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

                case SequenceExpressionType.IsBoundedReachableNodes:
                case SequenceExpressionType.IsBoundedReachableNodesViaIncoming:
                case SequenceExpressionType.IsBoundedReachableNodesViaOutgoing:
                case SequenceExpressionType.IsBoundedReachableEdges:
                case SequenceExpressionType.IsBoundedReachableEdgesViaIncoming:
                case SequenceExpressionType.IsBoundedReachableEdgesViaOutgoing:
                {
                    SequenceExpressionIsBoundedReachable seqIsBoundReach = (SequenceExpressionIsBoundedReachable)expr;
                    string sourceNode = GetSequenceExpression(seqIsBoundReach.SourceNode, source);
                    string endElement = GetSequenceExpression(seqIsBoundReach.EndElement, source);
                    string depth = GetSequenceExpression(seqIsBoundReach.Depth, source);
                    string incidentEdgeType = helper.ExtractEdgeType(source, seqIsBoundReach.EdgeType);
                    string adjacentNodeType = helper.ExtractNodeType(source, seqIsBoundReach.OppositeNodeType);
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

                case SequenceExpressionType.InducedSubgraph:
                {
                    SequenceExpressionInducedSubgraph seqInduced = (SequenceExpressionInducedSubgraph)expr;
                    return "GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqInduced.NodeSet, source) + ", graph)";
                }

                case SequenceExpressionType.DefinedSubgraph:
                {
                    SequenceExpressionDefinedSubgraph seqDefined = (SequenceExpressionDefinedSubgraph)expr;
                    if (seqDefined.EdgeSet.Type(env) == "set<Edge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else if (seqDefined.EdgeSet.Type(env) == "set<UEdge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraphUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else if (seqDefined.EdgeSet.Type(env) == "set<AEdge>")
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.DefinedSubgraph((IDictionary)" + GetSequenceExpression(seqDefined.EdgeSet, source) + ", graph)";
                }

                case SequenceExpressionType.EqualsAny:
                {
                    SequenceExpressionEqualsAny seqEqualsAny = (SequenceExpressionEqualsAny)expr;
                    if(seqEqualsAny.IncludingAttributes)
                        return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", true)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqEqualsAny.Subgraph, source) + ", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)" + GetSequenceExpression(seqEqualsAny.SubgraphSet, source) + ", false)";
                }

                case SequenceExpressionType.Nameof:
                {
                    SequenceExpressionNameof seqNameof = (SequenceExpressionNameof)expr;
                    if(seqNameof.NamedEntity != null)
                        return "GRGEN_LIBGR.GraphHelper.Nameof(" + GetSequenceExpression(seqNameof.NamedEntity, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.Nameof(null, graph)";
                }

                case SequenceExpressionType.Uniqueof:
                {
                    SequenceExpressionUniqueof seqUniqueof = (SequenceExpressionUniqueof)expr;
                    if(seqUniqueof.UniquelyIdentifiedEntity != null)
                        return "GRGEN_LIBGR.GraphHelper.Uniqueof(" + GetSequenceExpression(seqUniqueof.UniquelyIdentifiedEntity, source) + ", graph)";
                    else
                        return "GRGEN_LIBGR.GraphHelper.Uniqueof(null, graph)";
                }

                case SequenceExpressionType.Typeof:
                {
                    SequenceExpressionTypeof seqTypeof = (SequenceExpressionTypeof)expr;
                    return "GRGEN_LIBGR.TypesHelper.XgrsTypeOfConstant(" + GetSequenceExpression(seqTypeof.Entity, source) + ", graph.Model)";
                }

                case SequenceExpressionType.ExistsFile:
                {
                    SequenceExpressionExistsFile seqExistsFile = (SequenceExpressionExistsFile)expr;
                    return "System.IO.File.Exists((string)" + GetSequenceExpression(seqExistsFile.Path, source) + ")";
                }

                case SequenceExpressionType.Import:
                {
                    SequenceExpressionImport seqImport = (SequenceExpressionImport)expr;
                    return "GRGEN_LIBGR.GraphHelper.Import(" + GetSequenceExpression(seqImport.Path, source) + ", graph)";
                }

                case SequenceExpressionType.Copy:
                {
                    SequenceExpressionCopy seqCopy = (SequenceExpressionCopy)expr;
                    if(seqCopy.ObjectToBeCopied.Type(env)=="graph")
                        return "GRGEN_LIBGR.GraphHelper.Copy((GRGEN_LIBGR.IGraph)(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("set<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("map<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("array<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) 
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("deque<"))
                        return "new " + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model)
                            + "((" + TypesHelper.XgrsTypeToCSharpType(seqCopy.ObjectToBeCopied.Type(env), model) + ")"
                            + "(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + "))";
                    else if(seqCopy.ObjectToBeCopied.Type(env).StartsWith("match<")) {
                        string rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env));
                        string matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqCopy.ObjectToBeCopied.Type(env)));                     
                        return "((" + matchInterfaceName + ")(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ").Clone())";
                    }
                    else //if(seqCopy.ObjectToBeCopied.Type(env) == "")
                        return "GRGEN_LIBGR.TypesHelper.Clone(" + GetSequenceExpression(seqCopy.ObjectToBeCopied, source) + ")";
                }

                case SequenceExpressionType.Canonize:
                {
                    SequenceExpressionCanonize seqCanonize = (SequenceExpressionCanonize)expr;
                    return "((GRGEN_LIBGR.IGraph)" + GetSequenceExpression(seqCanonize.Graph, source) + ").Canonize()";
                }

                case SequenceExpressionType.Random:
                {
                    SequenceExpressionRandom seqRandom = (SequenceExpressionRandom)expr;
                    if(seqRandom.UpperBound != null)
                        return "GRGEN_LIBGR.Sequence.randomGenerator.Next((int)" + GetSequenceExpression(seqRandom.UpperBound, source) + ")";
                    else
                        return "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()";
                }

                case SequenceExpressionType.ContainerSize:
                {
                    SequenceExpressionContainerSize seqContainerSize = (SequenceExpressionContainerSize)expr;

                    string container = GetContainerValue(seqContainerSize, source);

                    if(seqContainerSize.ContainerType(env) == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string containerVar = "tmp_eval_once_" + seqContainerSize.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Count");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Count");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Count");

                        sb.AppendFront(")");

                        return sb.ToString();
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

                case SequenceExpressionType.ContainerEmpty:
                {
                    SequenceExpressionContainerEmpty seqContainerEmpty = (SequenceExpressionContainerEmpty)expr;
                    
                    string container = GetContainerValue(seqContainerEmpty, source);

                    if(seqContainerEmpty.ContainerType(env) == "")
                    {
                        SourceBuilder sb = new SourceBuilder();

                        string containerVar = "tmp_eval_once_" + seqContainerEmpty.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        sb.AppendFront(array + ".Count==0");

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        sb.AppendFront(deque + ".Count==0");

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + ".Count==0");

                        sb.AppendFront(")");

                        return sb.ToString();
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

                case SequenceExpressionType.ContainerAccess:
                {
                    SequenceExpressionContainerAccess seqContainerAccess = (SequenceExpressionContainerAccess)expr; // todo: dst type unknownTypesHelper.ExtractSrc(seqMapAccessToVar.Setmap.Type)
                    string container;
                    string ContainerType;
                    if(seqContainerAccess.ContainerExpr is SequenceExpressionAttributeAccess)
                    {
                        SequenceExpressionAttributeAccess seqContainerAttribute = (SequenceExpressionAttributeAccess)(seqContainerAccess.ContainerExpr);
                        string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqContainerAttribute.SourceVar) + ")";
                        container = element + ".GetAttribute(\"" + seqContainerAttribute.AttributeName + "\")";
                        if(seqContainerAttribute.SourceVar.Type == "")
                            ContainerType = "";
                        else
                        {
                            GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(seqContainerAttribute.SourceVar.Type, env.Model);
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
                        SourceBuilder sb = new SourceBuilder();

                        string sourceExpr = GetSequenceExpression(seqContainerAccess.KeyExpr, source);
                        string containerVar = "tmp_eval_once_" + seqContainerAccess.Id;
                        source.AppendFront("object " + containerVar + " = null;\n");
                        sb.AppendFront("((" + containerVar + " = " + container + ") is IList ? ");

                        string array = "((System.Collections.IList)" + containerVar + ")";
                        if(!TypesHelper.IsSameOrSubtype(seqContainerAccess.KeyExpr.Type(env), "int", model))
                        {
                            sb.AppendFront(array + "[-1]");
                        }
                        else
                        {
                            sb.AppendFront(array + "[(int)" + sourceExpr + "]");
                        }

                        sb.AppendFront(" : ");

                        sb.AppendFront(containerVar + " is GRGEN_LIBGR.IDeque ? ");

                        string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                        if(!TypesHelper.IsSameOrSubtype(seqContainerAccess.KeyExpr.Type(env), "int", model))
                        {
                            sb.AppendFront(deque + "[-1]");
                        }
                        else
                        {
                            sb.AppendFront(deque + "[(int)" + sourceExpr + "]");
                        }

                        sb.AppendFront(" : ");

                        string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
                        sb.AppendFront(dictionary + "[" + sourceExpr + "]");

                        sb.AppendFront(")");

                        return sb.ToString();
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

                case SequenceExpressionType.ContainerPeek:
                {
                    SequenceExpressionContainerPeek seqContainerPeek = (SequenceExpressionContainerPeek)expr;

                    string container = GetContainerValue(seqContainerPeek, source);

                    if(seqContainerPeek.KeyExpr != null)
                    {
                        if(seqContainerPeek.ContainerType(env) == "")
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                        {
                            return container + "[(int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + "]";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                        {
                            return container + "[(int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + "]";
                        }
                        else // statically known set/map/deque
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ", (int)" + GetSequenceExpression(seqContainerPeek.KeyExpr, source) + ")";
                        }
                    }
                    else
                    {
                        if(seqContainerPeek.ContainerType(env).StartsWith("array"))
                        {
                            return container + "[" + container + ".Count - 1]";
                        }
                        else if(seqContainerPeek.ContainerType(env).StartsWith("deque"))
                        {
                            return container + "[0]";
                        }
                        else
                        {
                            return "GRGEN_LIBGR.ContainerHelper.Peek(" + container + ")";
                        }
                    }
                }

                case SequenceExpressionType.Constant:
                {
                    SequenceExpressionConstant seqConst = (SequenceExpressionConstant)expr;
                    return helper.GetConstant(seqConst.Constant);
                }

                case SequenceExpressionType.This:
                {
                    SequenceExpressionThis seqThis = (SequenceExpressionThis)expr;
                    return "graph";
                }

                case SequenceExpressionType.SetCopyConstructor:
                {
                    SequenceExpressionSetCopyConstructor seqConstr = (SequenceExpressionSetCopyConstructor)expr;
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

                case SequenceExpressionType.SetConstructor:
                case SequenceExpressionType.ArrayConstructor:
                case SequenceExpressionType.DequeConstructor:
                {
                    SequenceExpressionContainerConstructor seqConstr = (SequenceExpressionContainerConstructor)expr;
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

                case SequenceExpressionType.MapConstructor:
                {
                    SequenceExpressionMapConstructor seqConstr = (SequenceExpressionMapConstructor)expr;
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

                case SequenceExpressionType.GraphElementAttribute:
                {
                    SequenceExpressionAttributeAccess seqAttr = (SequenceExpressionAttributeAccess)expr;
                    string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqAttr.SourceVar) + ")";
                    string value = element + ".GetAttribute(\"" + seqAttr.AttributeName + "\")";
                    string type = seqAttr.Type(env);
                    if (type == ""
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

                case SequenceExpressionType.ElementOfMatch:
                {
                    SequenceExpressionMatchAccess seqMA = (SequenceExpressionMatchAccess)expr;
                    String rulePatternClassName = "Rule_" + TypesHelper.ExtractSrc(seqMA.SourceVar.Type);
                    String matchInterfaceName = rulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(TypesHelper.ExtractSrc(seqMA.SourceVar.Type));                     
                    string match = "((" + matchInterfaceName + ")" + helper.GetVar(seqMA.SourceVar) + ")";
                    if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                        return match + ".node_" + seqMA.ElementName;
                    else if(TypesHelper.GetNodeType(seqMA.Type(env), model) != null)
                        return match + ".edge_" + seqMA.ElementName;
                    else
                        return match + ".var_" + seqMA.ElementName;
                }

                case SequenceExpressionType.ElementFromGraph:
                {
                    SequenceExpressionElementFromGraph seqFromGraph = (SequenceExpressionElementFromGraph)expr;
                    string profilingArgument = seqFromGraph.EmitProfiling ? ", procEnv" : "";
                    return "GRGEN_LIBGR.GraphHelper.GetGraphElement((GRGEN_LIBGR.INamedGraph)graph, \"" + seqFromGraph.ElementName + "\"" + profilingArgument + ")";
                }

                case SequenceExpressionType.NodeByName:
                {
                    SequenceExpressionNodeByName seqNodeByName = (SequenceExpressionNodeByName)expr;
                    string profilingArgument = seqNodeByName.EmitProfiling ? ", procEnv" : "";
                    string nodeType = seqNodeByName.NodeType!=null ? helper.ExtractNodeType(source, seqNodeByName.NodeType) : null;
                    if(nodeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + ", " + nodeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqNodeByName.NodeName, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.EdgeByName:
                {
                    SequenceExpressionEdgeByName seqEdgeByName = (SequenceExpressionEdgeByName)expr;
                    string profilingArgument = seqEdgeByName.EmitProfiling ? ", procEnv" : "";
                    string edgeType = seqEdgeByName.EdgeType != null ? helper.ExtractEdgeType(source, seqEdgeByName.EdgeType) : null;
                    if (edgeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + ", " + edgeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, (string)" + GetSequenceExpression(seqEdgeByName.EdgeName, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.NodeByUnique:
                {
                    SequenceExpressionNodeByUnique seqNodeByUnique = (SequenceExpressionNodeByUnique)expr;
                    string profilingArgument = seqNodeByUnique.EmitProfiling ? ", procEnv" : "";
                    string nodeType = seqNodeByUnique.NodeType != null ? helper.ExtractNodeType(source, seqNodeByUnique.NodeType) : null;
                    if (nodeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + ", " + nodeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetNode(graph, (int)" + GetSequenceExpression(seqNodeByUnique.NodeUniqueId, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.EdgeByUnique:
                {
                    SequenceExpressionEdgeByUnique seqEdgeByUnique = (SequenceExpressionEdgeByUnique)expr;
                    string profilingArgument = seqEdgeByUnique.EmitProfiling ? ", procEnv" : "";
                    string edgeType = seqEdgeByUnique.EdgeType != null ? helper.ExtractEdgeType(source, seqEdgeByUnique.EdgeType) : null;
                    if (edgeType != null)
                        return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + ", " + edgeType + profilingArgument + ")";
                    else
                        return "GRGEN_LIBGR.GraphHelper.GetEdge(graph, (int)" + GetSequenceExpression(seqEdgeByUnique.EdgeUniqueId, source) + profilingArgument + ")";
                }

                case SequenceExpressionType.Source:
                {
                    SequenceExpressionSource seqSrc = (SequenceExpressionSource)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqSrc.Edge, source) + ").Source";
                }

                case SequenceExpressionType.Target:
                {
                    SequenceExpressionTarget seqTgt = (SequenceExpressionTarget)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqTgt.Edge, source) + ").Target";
                }

                case SequenceExpressionType.Opposite:
                {
                    SequenceExpressionOpposite seqOpp = (SequenceExpressionOpposite)expr;
                    return "((GRGEN_LIBGR.IEdge)" + GetSequenceExpression(seqOpp.Edge, source) + ").Opposite((GRGEN_LIBGR.INode)(" + GetSequenceExpression(seqOpp.Node, source) + "))";
                }

                case SequenceExpressionType.Variable:
                {
                    SequenceExpressionVariable seqVar = (SequenceExpressionVariable)expr;
                    return helper.GetVar(seqVar.Variable);
                }

                case SequenceExpressionType.FunctionCall:
                {
                    SequenceExpressionFunctionCall seqFuncCall = (SequenceExpressionFunctionCall)expr;
                    StringBuilder sb = new StringBuilder();
                    if(seqFuncCall.IsExternalFunctionCalled)
                        sb.Append("GRGEN_EXPR.ExternalFunctions.");
                    else
                        sb.AppendFormat("GRGEN_ACTIONS.{0}Functions.", TypesHelper.GetPackagePrefixDot(seqFuncCall.ParamBindings.Package));
                    sb.Append(seqFuncCall.ParamBindings.Name);
                    sb.Append("(procEnv, graph");
                    sb.Append(helper.BuildParameters(seqFuncCall.ParamBindings));
                    sb.Append(")");
                    return sb.ToString();
                }

                case SequenceExpressionType.FunctionMethodCall:
                {
                    SequenceExpressionFunctionMethodCall seqFuncCall = (SequenceExpressionFunctionMethodCall)expr;
                    StringBuilder sb = new StringBuilder();
                    if(seqFuncCall.TargetExpr.Type(env) == "")
                    {
                        sb.Append("((GRGEN_LIBGR.IGraphElement)");
                        sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                        sb.Append(").ApplyFunctionMethod(procEnv, graph, ");
                        sb.Append("\"" + seqFuncCall.ParamBindings.Name+ "\"");
                        sb.Append(helper.BuildParametersInObject(seqFuncCall.ParamBindings));
                        sb.Append(")");
                    }
                    else
                    {
                        sb.Append("((");
                        sb.Append(TypesHelper.XgrsTypeToCSharpType(seqFuncCall.TargetExpr.Type(env), model));
                        sb.Append(")");
                        sb.Append(GetSequenceExpression(seqFuncCall.TargetExpr, source));
                        sb.Append(").");
                        sb.Append(seqFuncCall.ParamBindings.Name);
                        sb.Append("(procEnv, graph");
                        sb.Append(helper.BuildParameters(seqFuncCall.ParamBindings, TypesHelper.GetNodeOrEdgeType(seqFuncCall.TargetExpr.Type(env), model).GetFunctionMethod(seqFuncCall.ParamBindings.Name)));
                        sb.Append(")");
                    }
                    return sb.ToString();
                }

                default:
                    throw new Exception("Unknown sequence expression type: " + expr.SequenceExpressionType);
            }
        }

        private string GetContainerValue(SequenceExpressionContainer container, SourceBuilder source)
        {
            if(container.ContainerExpr is SequenceExpressionAttributeAccess)
            {
                SequenceExpressionAttributeAccess attribute = (SequenceExpressionAttributeAccess)container.ContainerExpr;
                return "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(attribute.SourceVar) + ")" + ".GetAttribute(\"" + attribute.AttributeName + "\")";
            }
            else
                return GetSequenceExpression(container.ContainerExpr, source);
        }
    }
}
