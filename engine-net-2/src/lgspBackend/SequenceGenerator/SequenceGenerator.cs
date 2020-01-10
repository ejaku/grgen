/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence generator contains code to generate sequences, the sequence trees to be more precise,
    /// it is used by the lgsp sequence generator driver, emitting sequence heads and surrounding code.
    /// </summary>
    public class SequenceGenerator
    {
        IGraphModel model;

        SequenceCheckingEnvironmentCompiled env;

        SequenceComputationGenerator compGen;

        SequenceExpressionGenerator exprGen;

        SequenceGeneratorHelper helper;

        bool fireDebugEvents;
        bool emitProfiling;


        public SequenceGenerator(IGraphModel model, SequenceCheckingEnvironmentCompiled env, 
            SequenceComputationGenerator compGen, SequenceExpressionGenerator exprGen, SequenceGeneratorHelper helper,
            bool fireDebugEvents, bool emitProfiling)
        {
            this.model = model;

            this.env = env;

            this.compGen = compGen;

            this.exprGen = exprGen;

            this.helper = helper;

            this.fireDebugEvents = fireDebugEvents;
            this.emitProfiling = emitProfiling;
        }

		public void EmitSequence(Sequence seq, SourceBuilder source)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.RuleCall:
                case SequenceType.RuleAllCall:
                case SequenceType.RuleCountAllCall:
                    EmitRuleOrRuleAllCall((SequenceRuleCall)seq, source);
                    break;

                case SequenceType.SequenceCall:
                    EmitSequenceCall((SequenceSequenceCall)seq, source);
                    break;

				case SequenceType.Not:
				{
					SequenceNot seqNot = (SequenceNot) seq;
					EmitSequence(seqNot.Seq, source);
					source.AppendFront(compGen.SetResultVar(seqNot, "!"+compGen.GetResultVar(seqNot.Seq)));
					break;
				}

				case SequenceType.LazyOr:
				case SequenceType.LazyAnd:
                case SequenceType.IfThen:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Random)
					{
                        Debug.Assert(seq.SequenceType != SequenceType.IfThen);

                        source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
                        EmitLazyOp(seqBin, source, true);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
                        source.Indent();
                        EmitLazyOp(seqBin, source, false);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
                        EmitLazyOp(seqBin, source, false);
					}
					break;
				}

                case SequenceType.ThenLeft:
                case SequenceType.ThenRight:
				case SequenceType.StrictAnd:
				case SequenceType.StrictOr:
				case SequenceType.Xor:
				{
					SequenceBinary seqBin = (SequenceBinary) seq;
					if(seqBin.Random)
					{
                        source.AppendFront("if(GRGEN_LIBGR.Sequence.randomGenerator.Next(2) == 1)\n");
						source.AppendFront("{\n");
						source.Indent();
						EmitSequence(seqBin.Right, source);
						EmitSequence(seqBin.Left, source);
						source.Unindent();
						source.AppendFront("}\n");
						source.AppendFront("else\n");
						source.AppendFront("{\n");
                        source.Indent();
						EmitSequence(seqBin.Left, source);
						EmitSequence(seqBin.Right, source);
						source.Unindent();
						source.AppendFront("}\n");
					}
					else
					{
						EmitSequence(seqBin.Left, source);
						EmitSequence(seqBin.Right, source);
					}

                    if(seq.SequenceType==SequenceType.ThenLeft) {
                        source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Left)));
                        break;
                    } else if(seq.SequenceType==SequenceType.ThenRight) {
                        source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Right)));
                        break;
                    }

                    String op;
				    switch(seq.SequenceType)
				    {
					    case SequenceType.StrictAnd: op = "&"; break;
					    case SequenceType.StrictOr:  op = "|"; break;
					    case SequenceType.Xor:       op = "^"; break;
					    default: throw new Exception("Internal error in EmitSequence: Should not have reached this!");
				    }
				    source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqBin.Left) + " "+op+" " + compGen.GetResultVar(seqBin.Right)));
					break;
				}

                case SequenceType.IfThenElse:
                {
                    SequenceIfThenElse seqIf = (SequenceIfThenElse)seq;

                    EmitSequence(seqIf.Condition, source);

                    source.AppendFront("if(" + compGen.GetResultVar(seqIf.Condition) + ")");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.TrueCase, source);
                    source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.TrueCase)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    source.AppendFront("else\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    EmitSequence(seqIf.FalseCase, source);
                    source.AppendFront(compGen.SetResultVar(seqIf, compGen.GetResultVar(seqIf.FalseCase)));

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForContainer:
                {
                    SequenceForContainer seqFor = (SequenceForContainer)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    if(seqFor.Container.Type == "")
                    {
                        // type not statically known? -> might be Dictionary or List or Deque dynamically, must decide at runtime
                        source.AppendFront("if(" + helper.GetVar(seqFor.Container) + " is IList) {\n");
                        source.Indent();

                        source.AppendFront("IList entry_" + seqFor.Id + " = (IList) " + helper.GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else if(" + helper.GetVar(seqFor.Container) + " is GRGEN_LIBGR.IDeque) {\n");
                        source.Indent();

                        source.AppendFront("GRGEN_LIBGR.IDeque entry_" + seqFor.Id + " = (GRGEN_LIBGR.IDeque) " + helper.GetVar(seqFor.Container) + ";\n");
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0} < entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("} else {\n");
                        source.Indent();

                        source.AppendFront("foreach(DictionaryEntry entry_" + seqFor.Id + " in (IDictionary)" + helper.GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));
                        }
                        EmitSequence(seqFor.Seq, source);
                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");

                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("array"))
                    {
                        String arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("List<{0}> entry_{1} = (List<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", arrayValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(seqFor.Container.Type.StartsWith("deque"))
                    {
                        String dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqFor.Container.Type), model);
                        source.AppendFrontFormat("GRGEN_LIBGR.Deque<{0}> entry_{1} = (GRGEN_LIBGR.Deque<{0}>) " + helper.GetVar(seqFor.Container) + ";\n", dequeValueType, seqFor.Id);
                        source.AppendFrontFormat("for(int index_{0}=0; index_{0}<entry_{0}.Count; ++index_{0})\n", seqFor.Id);
                        source.AppendFront("{\n");
                        source.Indent();

                        if(seqFor.VarDst != null)
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "index_" + seqFor.Id));
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }
                        else
                        {
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + "[index_" + seqFor.Id + "]"));
                        }

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        String srcTypeXgrs = TypesHelper.ExtractSrc(seqFor.Container.Type);
                        String srcType = TypesHelper.XgrsTypeToCSharpType(srcTypeXgrs, model);
                        String dstTypeXgrs = TypesHelper.ExtractDst(seqFor.Container.Type);
                        String dstType = TypesHelper.XgrsTypeToCSharpType(dstTypeXgrs, model);
                        source.AppendFront("foreach(KeyValuePair<" + srcType + "," + dstType + "> entry_" + seqFor.Id + " in " + helper.GetVar(seqFor.Container) + ")\n");
                        source.AppendFront("{\n");
                        source.Indent();

                        if(dstTypeXgrs== "SetValueType")
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));
                        else
                            source.AppendFront(helper.SetVar(seqFor.Var, "entry_" + seqFor.Id + ".Key"));

                        if (seqFor.VarDst != null)
                            source.AppendFront(helper.SetVar(seqFor.VarDst, "entry_" + seqFor.Id + ".Value"));

                        EmitSequence(seqFor.Seq, source);

                        source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                        source.Unindent();
                        source.AppendFront("}\n");
                    }

                    break;
                }

                case SequenceType.ForIntegerRange:
                {
                    SequenceForIntegerRange seqFor = (SequenceForIntegerRange)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String ascendingVar = "ascending_" + seqFor.Id;
                    String entryVar = "entry_" + seqFor.Id;
                    String limitVar = "limit_" + seqFor.Id;
                    source.AppendFrontFormat("int {0} = (int)({1});\n", entryVar, exprGen.GetSequenceExpression(seqFor.Left, source));
                    source.AppendFrontFormat("int {0} = (int)({1});\n", limitVar, exprGen.GetSequenceExpression(seqFor.Right, source));
                    source.AppendFront("bool " + ascendingVar + " = " + entryVar + " <= " + limitVar + ";\n");

                    source.AppendFront("while(" + ascendingVar + " ? " + entryVar + " <= " + limitVar + " : " + entryVar + " >= " + limitVar + ")\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront("if(" + ascendingVar + ") ++" + entryVar + "; else --" + entryVar + ";\n");

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessEquality:
                {
                    SequenceForIndexAccessEquality seqFor = (SequenceForIndexAccessEquality)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);
                    source.Append("(");
                    source.Append(exprGen.GetSequenceExpression(seqFor.Expr, source));
                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForIndexAccessOrdering:
                {
                    SequenceForIndexAccessOrdering seqFor = (SequenceForIndexAccessOrdering)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    String indexVar = "index_" + seqFor.Id;
                    source.AppendFrontFormat("GRGEN_LIBGR.IAttributeIndex {0} = (GRGEN_LIBGR.IAttributeIndex)procEnv.Graph.Indices.GetIndex(\"{1}\");\n", indexVar, seqFor.IndexName);
                    String entryVar = "entry_" + seqFor.Id;
                    source.AppendFrontFormat("foreach(GRGEN_LIBGR.IGraphElement {0} in {1}.LookupElements",
                        entryVar, indexVar);

                    if(seqFor.Ascending)
                        source.Append("Ascending");
                    else
                        source.Append("Descending");
                    if(seqFor.From() != null && seqFor.To() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
                        source.Append(", ");
                        source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
                    }
                    else if(seqFor.From() != null)
                    {
                        source.Append("From");
                        if(seqFor.IncludingFrom())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.From(), source));
                    }
                    else if(seqFor.To() != null)
                    {
                        source.Append("To");
                        if(seqFor.IncludingTo())
                            source.Append("Inclusive");
                        else
                            source.Append("Exclusive");
                        source.Append("(");
                        source.Append(exprGen.GetSequenceExpression(seqFor.To(), source));
                    }
                    else
                    {
                        source.Append("(");
                    }

                    source.Append("))\n");
                    source.AppendFront("{\n");
                    source.Indent();

                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, entryVar));

                    EmitSequence(seqFor.Seq, source);

                    source.Unindent();
                    source.AppendFront("}\n");
                    break;
                }

                case SequenceType.ForAdjacentNodes:
                case SequenceType.ForAdjacentNodesViaIncoming:
                case SequenceType.ForAdjacentNodesViaOutgoing:
                case SequenceType.ForIncidentEdges:
                case SequenceType.ForIncomingEdges:
                case SequenceType.ForOutgoingEdges:
                case SequenceType.ForReachableNodes:
                case SequenceType.ForReachableNodesViaIncoming:
                case SequenceType.ForReachableNodesViaOutgoing:
                case SequenceType.ForReachableEdges:
                case SequenceType.ForReachableEdgesViaIncoming:
                case SequenceType.ForReachableEdgesViaOutgoing:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 2 ? seqFor.ArgExprs[1] : null;
                    string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForAdjacentNodes:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaIncoming:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForAdjacentNodesViaOutgoing:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = theOther;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForIncidentEdges:
                            edgeMethod = "Incident";
                            theOther = "edge_" + seqFor.Id + ".Opposite(node_" + seqFor.Id + ")";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForIncomingEdges:
                            edgeMethod = "Incoming";
                            theOther = "edge_" + seqFor.Id + ".Source";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForOutgoingEdges:
                            edgeMethod = "Outgoing";
                            theOther = "edge_" + seqFor.Id + ".Target";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id; ;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = emitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForReachableNodes || seqFor.SequenceType == SequenceType.ForReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForReachableEdges || seqFor.SequenceType == SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GraphHelper.Reachable{1}(node_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        if(emitProfiling)
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.{1})\n",
                                seqFor.Id, edgeMethod);
                        else
                            source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in node_{0}.GetCompatible{1}({2}))\n",
                                seqFor.Id, edgeMethod, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    if(seqFor.SequenceType != SequenceType.ForReachableNodes && seqFor.SequenceType != SequenceType.ForReachableNodesViaIncoming && seqFor.SequenceType != SequenceType.ForReachableNodesViaOutgoing
                        && seqFor.SequenceType != SequenceType.ForReachableEdges && seqFor.SequenceType != SequenceType.ForReachableEdgesViaIncoming || seqFor.SequenceType != SequenceType.ForReachableEdgesViaOutgoing)
                    {
                        if(emitProfiling)
                        {
                            source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                            source.AppendFrontFormat("if(!edge_{0}.InstanceOf(", seqFor.Id);
                            source.Append(incidentEdgeTypeExpr);
                            source.Append("))\n");
                            source.AppendFront("\tcontinue;\n");
                        }

                        // incident/adjacent needs a check for adjacent node, cause only incident edge can be type constrained in the loop
                        // reachable already allows to iterate exactly the edges of interest
                        source.AppendFrontFormat("if(!{0}.InstanceOf({1}))\n",
                            theOther, adjacentNodeTypeExpr);
                        source.AppendFront("\tcontinue;\n");
                    }

                    source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForBoundedReachableNodes:
                case SequenceType.ForBoundedReachableNodesViaIncoming:
                case SequenceType.ForBoundedReachableNodesViaOutgoing:
                case SequenceType.ForBoundedReachableEdges:
                case SequenceType.ForBoundedReachableEdgesViaIncoming:
                case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    string sourceNodeExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[0], source);
                    source.AppendFrontFormat("GRGEN_LIBGR.INode node_{0} = (GRGEN_LIBGR.INode)({1});\n", seqFor.Id, sourceNodeExpr);
                    string depthExpr = exprGen.GetSequenceExpression(seqFor.ArgExprs[1], source);
                    source.AppendFrontFormat("int depth_{0} = (int)({1});\n", seqFor.Id, depthExpr);

                    SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 3 ? seqFor.ArgExprs[2] : null;
                    string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                    SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 4 ? seqFor.ArgExprs[3] : null;
                    string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);

                    string iterationVariable; // valid for incident/adjacent and reachable
                    string iterationType;
                    string edgeMethod = null; // only valid for incident/adajcent
                    string theOther = null; // only valid for incident/adjacent
                    string reachableMethod = null; // only valid for reachable
                    switch(seqFor.SequenceType)
                    {
                        case SequenceType.ForBoundedReachableNodes:
                            reachableMethod = "";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaIncoming:
                            reachableMethod = "Incoming";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableNodesViaOutgoing:
                            reachableMethod = "Outgoing";
                            iterationVariable = "iter_" + seqFor.Id;
                            iterationType = AdjacentNodeType != null ? AdjacentNodeType.Type(env) : "Node";
                            break;
                        case SequenceType.ForBoundedReachableEdges:
                            reachableMethod = "Edges";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaIncoming:
                            reachableMethod = "EdgesIncoming";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        case SequenceType.ForBoundedReachableEdgesViaOutgoing:
                            reachableMethod = "EdgesOutgoing";
                            iterationVariable = "edge_" + seqFor.Id;
                            iterationType = IncidentEdgeType != null ? IncidentEdgeType.Type(env) : "AEdge";
                            break;
                        default:
                            edgeMethod = theOther = iterationVariable = iterationType = "INTERNAL ERROR";
                            break;
                    }

                    string profilingArgument = emitProfiling ? ", procEnv" : "";
                    if(seqFor.SequenceType == SequenceType.ForBoundedReachableNodes || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableNodesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode iter_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    else if(seqFor.SequenceType == SequenceType.ForBoundedReachableEdges || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaIncoming || seqFor.SequenceType == SequenceType.ForBoundedReachableEdgesViaOutgoing)
                    {
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge edge_{0} in GRGEN_LIBGR.GraphHelper.BoundedReachable{1}(node_{0}, depth_{0}, ({2}), ({3}), graph" + profilingArgument + "))\n",
                            seqFor.Id, reachableMethod, incidentEdgeTypeExpr, adjacentNodeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();

                    source.AppendFront(helper.SetVar(seqFor.Var, iterationVariable));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForNodes:
                case SequenceType.ForEdges:
                {
                    SequenceForFunction seqFor = (SequenceForFunction)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    if (seqFor.SequenceType == SequenceType.ForNodes)
                    {
                        SequenceExpression AdjacentNodeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string adjacentNodeTypeExpr = helper.ExtractNodeType(source, AdjacentNodeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.INode elem_{0} in graph.GetCompatibleNodes({1}))\n", seqFor.Id, adjacentNodeTypeExpr);
                    }
                    else
                    {
                        SequenceExpression IncidentEdgeType = seqFor.ArgExprs.Count >= 1 ? seqFor.ArgExprs[0] : null;
                        string incidentEdgeTypeExpr = helper.ExtractEdgeType(source, IncidentEdgeType);
                        source.AppendFrontFormat("foreach(GRGEN_LIBGR.IEdge elem_{0} in graph.GetCompatibleEdges({1}))\n", seqFor.Id, incidentEdgeTypeExpr);
                    }
                    source.AppendFront("{\n");
                    source.Indent();
                    
                    if(emitProfiling)
                        source.AppendFront("++procEnv.PerformanceInfo.SearchSteps;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, "elem_" + seqFor.Id));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    
                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

                case SequenceType.ForMatch:
                {
                    SequenceForMatch seqFor = (SequenceForMatch)seq;

                    source.AppendFront(compGen.SetResultVar(seqFor, "true"));

                    RuleInvocation ruleInvocation = seqFor.Rule.RuleInvocation;
                    SequenceExpression[] ArgumentExpressions = seqFor.Rule.ArgumentExpressions;
                    SequenceVariable[] ReturnVars = seqFor.Rule.ReturnVars;
                    String specialStr = seqFor.Rule.Special ? "true" : "false";
                    String parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
                    String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
                    String patternName = ruleInvocation.Name;
                    String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                    String matchName = "match_" + seqFor.Id;
                    String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                    String matchesName = "matches_" + seqFor.Id;
                    source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name)
                        + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
                    for(int i=0; i<seqFor.Rule.Filters.Count; ++i)
                    {
                        EmitFilterCall(source, seqFor.Rule.Filters[i], patternName, matchesName);
                    }

                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                    source.Indent();
                    source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
                    source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                    if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    String returnParameterDeclarationsAllCall;
                    String intermediateReturnAssignmentsAllCall;
                    String returnAssignmentsAllCall;
                    helper.BuildReturnParameters(ruleInvocation, ReturnVars,
                        out returnParameterDeclarations, out returnArguments, out returnAssignments,
                        out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

                    // apply the sequence for every match found
                    String enumeratorName = "enum_" + seqFor.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    source.AppendFront(helper.SetVar(seqFor.Var, matchName));

                    EmitSequence(seqFor.Seq, source);

                    source.AppendFront(compGen.SetResultVar(seqFor, compGen.GetResultVar(seqFor) + " & " + compGen.GetResultVar(seqFor.Seq)));
                    source.Unindent();
                    source.AppendFront("}\n");

                    source.Unindent();
                    source.AppendFront("}\n");

                    break;
                }

				case SequenceType.IterationMin:
				{
                    SequenceIterationMin seqMin = (SequenceIterationMin)seq;
					source.AppendFront("long i_" + seqMin.Id + " = 0;\n");
					source.AppendFront("while(true)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMin.Seq, source);
					source.AppendFront("if(!" + compGen.GetResultVar(seqMin.Seq) + ") break;\n");
					source.AppendFront("i_" + seqMin.Id + "++;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(compGen.SetResultVar(seqMin, "i_" + seqMin.Id + " >= " + seqMin.Min));
					break;
				}

				case SequenceType.IterationMinMax:
				{
                    SequenceIterationMinMax seqMinMax = (SequenceIterationMinMax)seq;
					source.AppendFront("long i_" + seqMinMax.Id + " = 0;\n");
					source.AppendFront("for(; i_" + seqMinMax.Id + " < " + seqMinMax.Max + "; i_" + seqMinMax.Id + "++)\n");
					source.AppendFront("{\n");
					source.Indent();
					EmitSequence(seqMinMax.Seq, source);
                    source.AppendFront("if(!" + compGen.GetResultVar(seqMinMax.Seq) + ") break;\n");
					source.Unindent();
					source.AppendFront("}\n");
					source.AppendFront(compGen.SetResultVar(seqMinMax, "i_" + seqMinMax.Id + " >= " + seqMinMax.Min));
					break;
				}

                case SequenceType.DeclareVariable:
                {
                    SequenceDeclareVariable seqDeclVar = (SequenceDeclareVariable)seq;
                    source.AppendFront(helper.SetVar(seqDeclVar.DestVar, TypesHelper.DefaultValueString(seqDeclVar.DestVar.Type, env.Model)));
                    source.AppendFront(compGen.SetResultVar(seqDeclVar, "true"));
                    break;
                }

				case SequenceType.AssignConstToVar:
				{
					SequenceAssignConstToVar seqToVar = (SequenceAssignConstToVar) seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetConstant(seqToVar.Constant)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
					break;
				}

                case SequenceType.AssignContainerConstructorToVar:
                {
                    SequenceAssignContainerConstructorToVar seqToVar = (SequenceAssignContainerConstructorToVar)seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, exprGen.GetSequenceExpression(seqToVar.Constructor, source)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignVarToVar:
                {
                    SequenceAssignVarToVar seqToVar = (SequenceAssignVarToVar)seq;
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, helper.GetVar(seqToVar.Variable)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignSequenceResultToVar:
                {
                    SequenceAssignSequenceResultToVar seqToVar = (SequenceAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.OrAssignSequenceResultToVar:
                {
                    SequenceOrAssignSequenceResultToVar seqToVar = (SequenceOrAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "|| (bool)" + helper.GetVar(seqToVar.DestVar)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AndAssignSequenceResultToVar:
                {
                    SequenceAndAssignSequenceResultToVar seqToVar = (SequenceAndAssignSequenceResultToVar)seq;
                    EmitSequence(seqToVar.Seq, source);
                    source.AppendFront(helper.SetVar(seqToVar.DestVar, compGen.GetResultVar(seqToVar.Seq) + "&& (bool)" + helper.GetVar(seqToVar.DestVar)));
                    source.AppendFront(compGen.SetResultVar(seqToVar, "true"));
                    break;
                }

                case SequenceType.AssignUserInputToVar:
                {
                    throw new Exception("Internal Error: the AssignUserInputToVar is interpreted only (no Debugger available at lgsp level)");
                }

                case SequenceType.AssignRandomIntToVar:
                {
                    SequenceAssignRandomIntToVar seqRandomToVar = (SequenceAssignRandomIntToVar)seq;
                    source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.Next(" + seqRandomToVar.Number + ")"));
                    source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.AssignRandomDoubleToVar:
                {
                    SequenceAssignRandomDoubleToVar seqRandomToVar = (SequenceAssignRandomDoubleToVar)seq;
                    source.AppendFront(helper.SetVar(seqRandomToVar.DestVar, "GRGEN_LIBGR.Sequence.randomGenerator.NextDouble()"));
                    source.AppendFront(compGen.SetResultVar(seqRandomToVar, "true"));
                    break;
                }

                case SequenceType.LazyOrAll:
                {
                    SequenceLazyOrAll seqAll = (SequenceLazyOrAll)seq;
                    EmitSequenceAll(seqAll, true, true, source);
                    break;
                }

                case SequenceType.LazyAndAll:
                {
                    SequenceLazyAndAll seqAll = (SequenceLazyAndAll)seq;
                    EmitSequenceAll(seqAll, false, true, source);
                    break;
                }

                case SequenceType.StrictOrAll:
                {
                    SequenceStrictOrAll seqAll = (SequenceStrictOrAll)seq;
                    EmitSequenceAll(seqAll, true, false, source);
                    break;
                }

                case SequenceType.StrictAndAll:
                {
                    SequenceStrictAndAll seqAll = (SequenceStrictAndAll)seq;
                    EmitSequenceAll(seqAll, false, false, source);
                    break;
                }

                case SequenceType.WeightedOne:
                {
                    SequenceWeightedOne seqWeighted = (SequenceWeightedOne)seq;
                    EmitSequenceWeighted(seqWeighted, source);
                    break;
                }

                case SequenceType.SomeFromSet:
                {
                    SequenceSomeFromSet seqSome = (SequenceSomeFromSet)seq;
                    EmitSequenceSome(seqSome, source);
                    break;
                }

				case SequenceType.Transaction:
				{
					SequenceTransaction seqTrans = (SequenceTransaction) seq;
                    source.AppendFront("int transID_" + seqTrans.Id + " = procEnv.TransactionManager.Start();\n");
					EmitSequence(seqTrans.Seq, source);
                    source.AppendFront("if("+ compGen.GetResultVar(seqTrans.Seq) + ") procEnv.TransactionManager.Commit(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront("else procEnv.TransactionManager.Rollback(transID_" + seqTrans.Id + ");\n");
                    source.AppendFront(compGen.SetResultVar(seqTrans, compGen.GetResultVar(seqTrans.Seq)));
					break;
				}

                case SequenceType.Backtrack:
                {
                    SequenceBacktrack seqBack = (SequenceBacktrack)seq;
                    EmitSequenceBacktrack(seqBack, source);
                    break;
                }

                case SequenceType.Pause:
                {
                    SequencePause seqPause = (SequencePause)seq;
                    source.AppendFront("procEnv.TransactionManager.Pause();\n");
                    EmitSequence(seqPause.Seq, source);
                    source.AppendFront("procEnv.TransactionManager.Resume();\n");
                    source.AppendFront(compGen.SetResultVar(seqPause, compGen.GetResultVar(seqPause.Seq)));
                    break;
                }

                case SequenceType.ExecuteInSubgraph:
                {
                    SequenceExecuteInSubgraph seqExecInSub = (SequenceExecuteInSubgraph)seq;
                    string subgraph;
                    if(seqExecInSub.AttributeName == null)
                        subgraph = helper.GetVar(seqExecInSub.SubgraphVar);
                    else
                    {
                        string element = "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqExecInSub.SubgraphVar) + ")";
                        subgraph = element + ".GetAttribute(\"" + seqExecInSub.AttributeName + "\")";
                    }
                    source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + subgraph + ");\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    EmitSequence(seqExecInSub.Seq, source);
                    source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                    source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
                    source.AppendFront(compGen.SetResultVar(seqExecInSub, compGen.GetResultVar(seqExecInSub.Seq)));
                    break;
                }

                case SequenceType.BooleanComputation:
                {
                    SequenceBooleanComputation seqComp = (SequenceBooleanComputation)seq;
                    compGen.EmitSequenceComputation(seqComp.Computation, source);
                    if(seqComp.Computation.ReturnsValue)
                        source.AppendFront(compGen.SetResultVar(seqComp, "!GRGEN_LIBGR.TypesHelper.IsDefaultValue(" + compGen.GetResultVar(seqComp.Computation) + ")"));
                    else
                        source.AppendFront(compGen.SetResultVar(seqComp, "true"));
                    break;
                }

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}
		}

        public String GetSequenceResult(Sequence seq)
        {
            return compGen.GetResultVar(seq);
        }

        private void EmitLazyOp(SequenceBinary seq, SourceBuilder source, bool reversed)
        {
            Sequence seqLeft;
            Sequence seqRight;
            if(reversed)
            {
                Debug.Assert(seq.SequenceType != SequenceType.IfThen);
                seqLeft = seq.Right;
                seqRight = seq.Left;
            }
            else
            {
                seqLeft = seq.Left;
                seqRight = seq.Right;
            }

            EmitSequence(seqLeft, source);

            if(seq.SequenceType == SequenceType.LazyOr)
            {
                source.AppendFront("if(" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            }
            else if(seq.SequenceType == SequenceType.LazyAnd)
            {
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "false"));
                source.Unindent();
            }
            else
            { //seq.SequenceType==SequenceType.IfThen -- lazy implication
                source.AppendFront("if(!" + compGen.GetResultVar(seqLeft) + ")\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seq, "true"));
                source.Unindent();
            }

            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            EmitSequence(seqRight, source);
            source.AppendFront(compGen.SetResultVar(seq, compGen.GetResultVar(seqRight)));

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitRuleOrRuleAllCall(SequenceRuleCall seqRule, SourceBuilder source)
        {
            RuleInvocation ruleInvocation = seqRule.RuleInvocation;
            SequenceExpression[] ArgumentExpressions = seqRule.ArgumentExpressions;
            SequenceVariable[] ReturnVars = seqRule.ReturnVars;
            String specialStr = seqRule.Special ? "true" : "false";
            String parameterDeclarations = null;
            String parameters = null;
            if(ruleInvocation.Subgraph != null)
                parameters = helper.BuildParametersInDeclarations(ruleInvocation, ArgumentExpressions, out parameterDeclarations);
            else
                parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
            String patternName = ruleInvocation.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seqRule.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seqRule.Id;

            if(ruleInvocation.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + helper.GetVar(ruleInvocation.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name)
                + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                + parameters + ");\n");
            for(int i = 0; i < seqRule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seqRule.Filters[i], patternName, matchesName);
            }

            if(fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
            if(seqRule is SequenceRuleCountAllCall)
            {
                SequenceRuleCountAllCall seqRuleCountAll = (SequenceRuleCountAllCall)seqRule;
                source.AppendFront(helper.SetVar(seqRuleCountAll.CountResult, matchesName + ".Count"));
            }

            if(seqRule is SequenceRuleAllCall
                && ((SequenceRuleAllCall)seqRule).ChooseRandom
                && ((SequenceRuleAllCall)seqRule).MinSpecified)
            {
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                source.AppendFrontFormat("int minmatchesvar_{0} = (int){1};\n", seqRuleAll.Id, helper.GetVar(seqRuleAll.MinVarChooseRandom));
                source.AppendFrontFormat("if({0}.Count < minmatchesvar_{1}) {{\n", matchesName, seqRuleAll.Id);
            }
            else
            {
                source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            }
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqRule, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqRule, "true"));
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            helper.BuildReturnParameters(ruleInvocation, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            if(seqRule.SequenceType == SequenceType.RuleCall)
            {
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            }
            else if(seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
            {
                // iterate through matches, use Modify on each, fire the next match event after the first
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                String enumeratorName = "enum_" + seqRule.Id;
                source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                source.AppendFront("if(" + matchName + "!=" + matchesName + ".FirstExact) procEnv.RewritingNextMatch();\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }
            else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
            {
                // as long as a further rewrite has to be selected: randomly choose next match, rewrite it and remove it from available matches; fire the next match event after the first
                SequenceRuleAllCall seqRuleAll = (SequenceRuleAllCall)seqRule;
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                source.AppendFrontFormat("int numchooserandomvar_{0} = (int){1};\n", seqRuleAll.Id, seqRuleAll.MaxVarChooseRandom != null ? helper.GetVar(seqRuleAll.MaxVarChooseRandom) : (seqRuleAll.MinSpecified ? "2147483647" : "1"));
                source.AppendFrontFormat("if({0}.Count < numchooserandomvar_{1}) numchooserandomvar_{1} = {0}.Count;\n", matchesName, seqRule.Id);
                source.AppendFrontFormat("for(int i = 0; i < numchooserandomvar_{0}; ++i)\n", seqRule.Id);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(i != 0) procEnv.RewritingNextMatch();\n");
                source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                source.Unindent();
                source.AppendFront("}\n");
                if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
            }

            if(fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            source.Unindent();
            source.AppendFront("}\n");

            if(ruleInvocation.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        private void EmitSequenceCall(SequenceSequenceCall seqSeq, SourceBuilder source)
        {
            SequenceInvocation sequenceInvocation = seqSeq.SequenceInvocation;
            SequenceExpression[] ArgumentExpressions = seqSeq.ArgumentExpressions;
            SequenceVariable[] ReturnVars = seqSeq.ReturnVars;
            String parameterDeclarations = null;
            String parameters = null;
            if(sequenceInvocation.Subgraph != null)
                parameters = helper.BuildParametersInDeclarations(sequenceInvocation, ArgumentExpressions, out parameterDeclarations);
            else
                parameters = helper.BuildParameters(sequenceInvocation, ArgumentExpressions);
            String outParameterDeclarations;
            String outArguments;
            String outAssignments;
            helper.BuildOutParameters(sequenceInvocation, ReturnVars, out outParameterDeclarations, out outArguments, out outAssignments);

            if(sequenceInvocation.Subgraph != null)
            {
                source.AppendFront(parameterDeclarations + "\n");
                source.AppendFront("procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)" + helper.GetVar(sequenceInvocation.Subgraph) + ");\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }

            if(outParameterDeclarations.Length != 0)
                source.AppendFront(outParameterDeclarations + "\n");
            source.AppendFront("if(" + TypesHelper.GetPackagePrefixDot(sequenceInvocation.Package) + "Sequence_" + sequenceInvocation.Name + ".ApplyXGRS_" + sequenceInvocation.Name
                                + "(procEnv" + parameters + outArguments + ")) {\n");
            source.Indent();
            if(outAssignments.Length != 0)
                source.AppendFront(outAssignments + "\n");
            source.AppendFront(compGen.SetResultVar(seqSeq, "true"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seqSeq, "false"));
            source.Unindent();
            source.AppendFront("}\n");

            if(sequenceInvocation.Subgraph != null)
            {
                source.AppendFront("procEnv.ReturnFromSubgraph();\n");
                source.AppendFront("graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");
            }
        }

        private void EmitSequenceBacktrack(SequenceBacktrack seq, SourceBuilder source)
        {
            RuleInvocation ruleInvocation = seq.Rule.RuleInvocation;
            SequenceExpression[] ArgumentExpressions = seq.Rule.ArgumentExpressions;
            SequenceVariable[] ReturnVars = seq.Rule.ReturnVars;
            String specialStr = seq.Rule.Special ? "true" : "false";
            String parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
            String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
            String patternName = ruleInvocation.Name;
            String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
            String matchName = "match_" + seq.Id;
            String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            String matchesName = "matches_" + seq.Id;
            source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name)
                + ".Match(procEnv, procEnv.MaxMatches" + parameters + ");\n");
            for(int i=0; i<seq.Rule.Filters.Count; ++i)
            {
                EmitFilterCall(source, seq.Rule.Filters[i], patternName, matchesName);
            }

            source.AppendFront("if(" + matchesName + ".Count==0) {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "true")); // shut up compiler
            source.AppendFront(matchesName + " = (" + matchesType + ")" + matchesName + ".Clone();\n");
            source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");

            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            String returnParameterDeclarationsAllCall;
            String intermediateReturnAssignmentsAllCall;
            String returnAssignmentsAllCall;
            helper.BuildReturnParameters(ruleInvocation, ReturnVars,
                out returnParameterDeclarations, out returnArguments, out returnAssignments,
                out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

            // apply the rule and the following sequence for every match found,
            // until the first rule and sequence execution succeeded
            // rolling back the changes of failing executions until then
            String enumeratorName = "enum_" + seq.Id;
            String matchesTriedName = "matchesTried_" + seq.Id;
            source.AppendFront("int " + matchesTriedName + " = 0;\n");
            source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
            source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
            source.AppendFront("++" + matchesTriedName + ";\n");

            // start a transaction
            source.AppendFront("int transID_" + seq.Id + " = procEnv.TransactionManager.Start();\n");
            source.AppendFront("int oldRewritesPerformed_" + seq.Id + " = procEnv.PerformanceInfo.RewritesPerformed;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", " + matchName + ", " + specialStr + ");\n");
            if(returnParameterDeclarations.Length!=0) source.AppendFront(returnParameterDeclarations + "\n");

            source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
            if(returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
            if(fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

            // rule applied, now execute the sequence
            EmitSequence(seq.Seq, source);

            // if sequence execution failed, roll the changes back and try the next match of the rule
            source.AppendFront("if(!" + compGen.GetResultVar(seq.Seq) + ") {\n");
            source.Indent();
            source.AppendFront("procEnv.TransactionManager.Rollback(transID_" + seq.Id + ");\n");
            source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed = oldRewritesPerformed_" + seq.Id + ";\n");

            source.AppendFront("if(" + matchesTriedName + " < " + matchesName + ".Count) {\n"); // further match available -> try it
            source.Indent();
            source.AppendFront("continue;\n");
            source.Unindent();
            source.AppendFront("} else {\n"); // all matches tried, all failed later on -> end in fail
            source.Indent();
            source.AppendFront(compGen.SetResultVar(seq, "false"));
            source.AppendFront("break;\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");

            // if sequence execution succeeded, commit the changes so far and succeed
            source.AppendFront("procEnv.TransactionManager.Commit(transID_" + seq.Id + ");\n");
            source.AppendFront(compGen.SetResultVar(seq, "true"));
            source.AppendFront("break;\n");

            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceAll(SequenceNAry seqAll, bool disjunction, bool lazy, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqAll, disjunction ? "false" : "true"));
            source.AppendFrontFormat("bool continue_{0} = true;\n", seqAll.Id);
            source.AppendFrontFormat("List<int> sequencestoexecutevar_{0} = new List<int>({1});\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("for(int i = 0; i < {1}; ++i) sequencestoexecutevar_{0}.Add(i);\n", seqAll.Id, seqAll.Sequences.Count);
            source.AppendFrontFormat("while(sequencestoexecutevar_{0}.Count>0 && continue_{0})\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("int positionofsequencetoexecute_{0} = GRGEN_LIBGR.Sequence.randomGenerator.Next(sequencestoexecutevar_{0}.Count);\n", seqAll.Id);
            source.AppendFrontFormat("switch(sequencestoexecutevar_{0}[positionofsequencetoexecute_{0}])\n", seqAll.Id);
            source.AppendFront("{\n");
            source.Indent();
            for(int i = 0; i < seqAll.Sequences.Count; ++i)
            {
                source.AppendFrontFormat("case {0}:\n", i);
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqAll.Sequences[i], source);
                source.AppendFrontFormat("sequencestoexecutevar_{0}.Remove({1});\n", seqAll.Id, i);
                source.AppendFront(compGen.SetResultVar(seqAll, compGen.GetResultVar(seqAll) + (disjunction ? " || " : " && ") + compGen.GetResultVar(seqAll.Sequences[i])));
                if(lazy)
                    source.AppendFrontFormat("if(" + (disjunction?"":"!") + compGen.GetResultVar(seqAll) + ") continue_{0} = false;\n", seqAll.Id);
                source.AppendFront("break;\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.Unindent();
            source.AppendFront("}\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void EmitSequenceWeighted(SequenceWeightedOne seqWeighted, SourceBuilder source)
        {
            source.AppendFrontFormat("double pointtoexec_{0} = GRGEN_LIBGR.Sequence.randomGenerator.NextDouble() * {1};\n", seqWeighted.Id, seqWeighted.Numbers[seqWeighted.Numbers.Count - 1].ToString(System.Globalization.CultureInfo.InvariantCulture));
            for(int i = 0; i < seqWeighted.Sequences.Count; ++i)
            {
                if(i == 0)
                    source.AppendFrontFormat("if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                else if(i == seqWeighted.Sequences.Count - 1)
                    source.AppendFrontFormat("else\n");
                else
                    source.AppendFrontFormat("else if(pointtoexec_{0} <= {1})\n", seqWeighted.Id, seqWeighted.Numbers[i].ToString(System.Globalization.CultureInfo.InvariantCulture));
                source.AppendFront("{\n");
                source.Indent();
                EmitSequence(seqWeighted.Sequences[i], source);
                source.AppendFront(compGen.SetResultVar(seqWeighted, compGen.GetResultVar(seqWeighted.Sequences[i])));
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitSequenceSome(SequenceSomeFromSet seqSome, SourceBuilder source)
        {
            source.AppendFront(compGen.SetResultVar(seqSome, "false"));

            // emit code for matching all the contained rules
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocation ruleInvocation = seqRule.RuleInvocation;
                SequenceExpression[] ArgumentExpressions = seqRule.ArgumentExpressions;
                String specialStr = seqRule.Special ? "true" : "false";
                String parameters = helper.BuildParameters(ruleInvocation, ArgumentExpressions);
                String matchingPatternClassName = TypesHelper.GetPackagePrefixDot(ruleInvocation.Package) + "Rule_" + ruleInvocation.Name;
                String patternName = ruleInvocation.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;
                source.AppendFront(matchesType + " " + matchesName + " = rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name)
                    + ".Match(procEnv, " + (seqRule.SequenceType == SequenceType.RuleCall ? "1" : "procEnv.MaxMatches")
                    + parameters + ");\n");
                for(int j=0; j<seqRule.Filters.Count; ++j)
                {
                    EmitFilterCall(source, seqRule.Filters[j], patternName, matchesName);
                }
                source.AppendFront("procEnv.PerformanceInfo.MatchesFound += " + matchesName + ".Count;\n");
                source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();
                source.AppendFront(compGen.SetResultVar(seqSome, "true"));
                source.Unindent();
                source.AppendFront("}\n");
            }

            // emit code for deciding on the match to rewrite
            String totalMatchToApply = "total_match_to_apply_" + seqSome.Id;
            String curTotalMatch = "cur_total_match_" + seqSome.Id;
            if (seqSome.Random)
            {
                source.AppendFront("int " + totalMatchToApply + " = 0;\n");
                for (int i = 0; i < seqSome.Sequences.Count; ++i)
                {
                    SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                    String matchesName = "matches_" + seqRule.Id;
                    if (seqRule.SequenceType == SequenceType.RuleCall)
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                    else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                        source.AppendFront("if(" + matchesName + ".Count>0) ++" + totalMatchToApply + ";\n");
                    else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                        source.AppendFront(totalMatchToApply + " += " + matchesName + ".Count;\n");
                }
                source.AppendFront(totalMatchToApply + " = GRGEN_LIBGR.Sequence.randomGenerator.Next(" + totalMatchToApply + ");\n");
                source.AppendFront("int " + curTotalMatch + " = 0;\n");
            }

            // code to handle the rewrite next match
            String firstRewrite = "first_rewrite_" + seqSome.Id;
            source.AppendFront("bool " + firstRewrite + " = true;\n");

            // emit code for rewriting all the contained rules which got matched
            for (int i = 0; i < seqSome.Sequences.Count; ++i)
            {
                SequenceRuleCall seqRule = (SequenceRuleCall)seqSome.Sequences[i];
                RuleInvocation ruleInvocation = seqRule.RuleInvocation;
                SequenceVariable[] ReturnVars = seqRule.ReturnVars;
                String specialStr = seqRule.Special ? "true" : "false";
                String matchingPatternClassName = "Rule_" + ruleInvocation.Name;
                String patternName = ruleInvocation.Name;
                String matchType = matchingPatternClassName + "." + NamesOfEntities.MatchInterfaceName(patternName);
                String matchName = "match_" + seqRule.Id;
                String matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
                String matchesName = "matches_" + seqRule.Id;

                if(seqSome.Random)
                    source.AppendFront("if(" + matchesName + ".Count!=0 && " + curTotalMatch + "<=" + totalMatchToApply + ") {\n");
                else
                    source.AppendFront("if(" + matchesName + ".Count!=0) {\n");
                source.Indent();

                String returnParameterDeclarations;
                String returnArguments;
                String returnAssignments;
                String returnParameterDeclarationsAllCall;
                String intermediateReturnAssignmentsAllCall;
                String returnAssignmentsAllCall;
                helper.BuildReturnParameters(ruleInvocation, ReturnVars,
                    out returnParameterDeclarations, out returnArguments, out returnAssignments,
                    out returnParameterDeclarationsAllCall, out intermediateReturnAssignmentsAllCall, out returnAssignmentsAllCall);

                if (seqRule.SequenceType == SequenceType.RuleCall)
                {
                    if (seqSome.Random) {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".FirstExact;\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if (returnAssignments.Length != 0) source.AppendFront(returnAssignments + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");

                    if (seqSome.Random) {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else if (seqRule.SequenceType == SequenceType.RuleCountAllCall || !((SequenceRuleAllCall)seqRule).ChooseRandom) // seq.SequenceType == SequenceType.RuleAll
                {
                    if (seqSome.Random)
                    {
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                    }

                    // iterate through matches, use Modify on each, fire the next match event after the first
                    if(returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                    String enumeratorName = "enum_" + seqRule.Id;
                    source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                    source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                    if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                    source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                    if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                    source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                    if(returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                    source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                    source.AppendFront(firstRewrite + " = false;\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                    if(returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
                    if(seqRule.SequenceType == SequenceType.RuleCountAllCall)
                    {
                        SequenceRuleCountAllCall ruleCountAll = (SequenceRuleCountAllCall)seqRule;
                        source.AppendFront(helper.SetVar(ruleCountAll.CountResult, matchesName + ".Count"));
                    }

                    if (seqSome.Random)
                    {
                        source.Unindent();
                        source.AppendFront("}\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                    }
                }
                else // seq.SequenceType == SequenceType.RuleAll && ((SequenceRuleAll)seqRule).ChooseRandom
                {
                    if (seqSome.Random)
                    {
                        // for the match selected: rewrite it
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                        String enumeratorName = "enum_" + seqRule.Id;
                        source.AppendFront("IEnumerator<" + matchType + "> " + enumeratorName + " = " + matchesName + ".GetEnumeratorExact();\n");
                        source.AppendFront("while(" + enumeratorName + ".MoveNext())\n");
                        source.AppendFront("{\n");
                        source.Indent();
                        source.AppendFront("if(" + curTotalMatch + "==" + totalMatchToApply + ") {\n");
                        source.Indent();
                        source.AppendFront(matchType + " " + matchName + " = " + enumeratorName + ".Current;\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
                        source.AppendFront("++" + curTotalMatch + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        // randomly choose match, rewrite it and remove it from available matches
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarationsAllCall + "\n");
                        source.AppendFront(matchType + " " + matchName + " = " + matchesName + ".GetMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(" + matchesName + ".Count));\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Matched(" + matchesName + ", null, " + specialStr + ");\n");
                        if (fireDebugEvents) source.AppendFront("procEnv.Finishing(" + matchesName + ", " + specialStr + ");\n");
                        source.AppendFront("if(!" + firstRewrite + ") procEnv.RewritingNextMatch();\n");
                        if (returnParameterDeclarations.Length != 0) source.AppendFront(returnParameterDeclarations + "\n");
                        source.AppendFront("rule_" + TypesHelper.PackagePrefixedNameUnderscore(ruleInvocation.Package, ruleInvocation.Name) + ".Modify(procEnv, " + matchName + returnArguments + ");\n");
                        if (returnAssignments.Length != 0) source.AppendFront(intermediateReturnAssignmentsAllCall + "\n");
                        source.AppendFront("procEnv.PerformanceInfo.RewritesPerformed++;\n");
                        source.AppendFront(firstRewrite + " = false;\n");
                        if (returnAssignments.Length != 0) source.AppendFront(returnAssignmentsAllCall + "\n");
                    }
                }

                if (fireDebugEvents) source.AppendFront("procEnv.Finished(" + matchesName + ", " + specialStr + ");\n");

                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        private void EmitFilterCall(SourceBuilder source, FilterCall filterCall, string patternName, string matchesName)
        {
            if(filterCall.Name == "keepFirst" || filterCall.Name == "removeFirst"
                || filterCall.Name == "keepFirstFraction" || filterCall.Name == "removeFirstFraction"
                || filterCall.Name == "keepLast" || filterCall.Name == "removeLast"
                || filterCall.Name == "keepLastFraction" || filterCall.Name == "removeLastFraction")
            {
                switch(filterCall.Name)
                {
                    case "keepFirst":
                        source.AppendFrontFormat("{0}.FilterKeepFirst((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLast":
                        source.AppendFrontFormat("{0}.FilterKeepLast((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepFirstFraction":
                        source.AppendFrontFormat("{0}.FilterKeepFirstFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "keepLastFraction":
                        source.AppendFrontFormat("{0}.FilterKeepLastFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirst":
                        source.AppendFrontFormat("{0}.FilterRemoveFirst((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLast":
                        source.AppendFrontFormat("{0}.FilterRemoveLast((int)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeFirstFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveFirstFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                    case "removeLastFraction":
                        source.AppendFrontFormat("{0}.FilterRemoveLastFraction((double)({1}));\n",
                            matchesName, exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[0], source));
                        break;
                }
            }
            else
            {
                if(filterCall.IsAutoGenerated && filterCall.Name == "auto")
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}(procEnv, {3});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, matchesName);
                else if(filterCall.IsAutoGenerated)
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}_{2}_{3}(procEnv, {4});\n",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), patternName, filterCall.Name, filterCall.EntitySuffixForName, matchesName);
                else
                {
                    source.AppendFrontFormat("GRGEN_ACTIONS.{0}MatchFilters.Filter_{1}(procEnv, {2}",
                        TypesHelper.GetPackagePrefixDot(filterCall.Package), filterCall.Name, matchesName);
                    for(int i = 0; i < filterCall.ArgumentExpressions.Length; ++i)
                    {
                        source.AppendFormat(", ({0})({1})",
                            TypesHelper.XgrsTypeToCSharpType(helper.actionsTypeInformation.filterFunctionsToInputTypes[filterCall.Name][i], model),
                            exprGen.GetSequenceExpression(filterCall.ArgumentExpressions[i], source));
                    } 
                    source.Append(");\n");
                }
            }
        }
    }
}
