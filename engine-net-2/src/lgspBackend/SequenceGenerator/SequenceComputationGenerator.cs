/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using COMP_HELPER = de.unika.ipd.grGen.lgsp.SequenceComputationGeneratorHelper;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence computation generator contains code to generate sequence computations;
    /// it is in use by the sequence generator.
    /// </summary>
    public class SequenceComputationGenerator
    {
        readonly IGraphModel model;

        readonly SequenceCheckingEnvironmentCompiled env;

        readonly SequenceExpressionGenerator exprGen;

        readonly SequenceGeneratorHelper seqHelper;

        readonly bool fireDebugEvents;


        public SequenceComputationGenerator(IGraphModel model, SequenceCheckingEnvironmentCompiled env,
            SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper seqHelper, bool fireDebugEvents)
        {
            this.model = model;
            this.env = env;
            this.exprGen = seqExprGen;
            this.seqHelper = seqHelper;
            this.fireDebugEvents = fireDebugEvents;
        }

  		public void EmitSequenceComputation(SequenceComputation seqComp, SourceBuilder source)
		{
            // take care that the operations returning a value are emitted similarily to expressions,
            // whereas the operations returning no value are emitted as statements
            switch(seqComp.SequenceComputationType)
            {
            case SequenceComputationType.Then:
                EmitSequenceComputationThen((SequenceComputationThen)seqComp, source);
                break;
            case SequenceComputationType.VariableDeclaration:
                EmitSequenceComputationVariableDeclaration((SequenceComputationVariableDeclaration)seqComp, source);
                break;
            case SequenceComputationType.VAlloc:
                EmitSequenceComputationVAlloc((SequenceComputationVAlloc)seqComp, source);
                break;
            case SequenceComputationType.VFree:
            case SequenceComputationType.VFreeNonReset:
                EmitSequenceComputationVFree((SequenceComputationVFree)seqComp, source);
                break;
            case SequenceComputationType.VReset:
                EmitSequenceComputationVReset((SequenceComputationVReset)seqComp, source);
                break;
            case SequenceComputationType.DebugAdd:
                EmitSequenceComputationDebugAdd((SequenceComputationDebugAdd)seqComp, source);
                break;
            case SequenceComputationType.DebugRem:
                EmitSequenceComputationDebugRem((SequenceComputationDebugRem)seqComp, source);
                break;
            case SequenceComputationType.DebugEmit:
                EmitSequenceComputationDebugEmit((SequenceComputationDebugEmit)seqComp, source);
                break;
            case SequenceComputationType.DebugHalt:
                EmitSequenceComputationDebugHalt((SequenceComputationDebugHalt)seqComp, source);
                break;
            case SequenceComputationType.DebugHighlight:
                EmitSequenceComputationDebugHighlight((SequenceComputationDebugHighlight)seqComp, source);
                break;
            case SequenceComputationType.Assert:
                EmitSequenceComputationAssert((SequenceComputationAssert)seqComp, source);
                break;
            case SequenceComputationType.Emit:
                EmitSequenceComputationEmit((SequenceComputationEmit)seqComp, source);
                break;
            case SequenceComputationType.Record:
                EmitSequenceComputationRecord((SequenceComputationRecord)seqComp, source);
                break;
            case SequenceComputationType.Export:
                EmitSequenceComputationExport((SequenceComputationExport)seqComp, source);
                break;
            case SequenceComputationType.DeleteFile:
                EmitSequenceComputationDeleteFile((SequenceComputationDeleteFile)seqComp, source);
                break;
            case SequenceComputationType.GraphAdd:
                EmitSequenceComputationGraphAdd((SequenceComputationGraphAdd)seqComp, source);
                break;
            case SequenceComputationType.GraphRem:
                EmitSequenceComputationGraphRem((SequenceComputationGraphRem)seqComp, source);
                break;
            case SequenceComputationType.GraphClear:
                EmitSequenceComputationGraphClear((SequenceComputationGraphClear)seqComp, source);
                break;
            case SequenceComputationType.GraphRetype:
                EmitSequenceComputationGraphRetype((SequenceComputationGraphRetype)seqComp, source);
                break;
            case SequenceComputationType.GraphAddCopy:
                EmitSequenceComputationGraphAddCopy((SequenceComputationGraphAddCopy)seqComp, source);
                break;
            case SequenceComputationType.GraphMerge:
                EmitSequenceComputationGraphMerge((SequenceComputationGraphMerge)seqComp, source);
                break;
            case SequenceComputationType.GraphRedirectSource:
                EmitSequenceComputationGraphRedirectSource((SequenceComputationGraphRedirectSource)seqComp, source);
                break;
            case SequenceComputationType.GraphRedirectTarget:
                EmitSequenceComputationGraphRedirectTarget((SequenceComputationGraphRedirectTarget)seqComp, source);
                break;
            case SequenceComputationType.GraphRedirectSourceAndTarget:
                EmitSequenceComputationGraphRedirectSourceAndTarget((SequenceComputationGraphRedirectSourceAndTarget)seqComp, source);
                break;
            case SequenceComputationType.Insert:
                EmitSequenceComputationInsert((SequenceComputationInsert)seqComp, source);
                break;
            case SequenceComputationType.InsertCopy:
                EmitSequenceComputationInsertCopy((SequenceComputationInsertCopy)seqComp, source);
                break;
            case SequenceComputationType.InsertInduced:
                EmitSequenceComputationInsertInduced((SequenceComputationInsertInduced)seqComp, source);
                break;
            case SequenceComputationType.InsertDefined:
                EmitSequenceComputationInsertDefined((SequenceComputationInsertDefined)seqComp, source);
                break;
            case SequenceComputationType.Expression:
                EmitSequenceComputationExpression((SequenceExpression)seqComp, source);
                break;
            case SequenceComputationType.BuiltinProcedureCall:
                EmitSequenceComputationBuiltinProcedureCall((SequenceComputationBuiltinProcedureCall)seqComp, source);
                break;
            case SequenceComputationType.ProcedureCall:
                EmitSequenceComputationProcedureCall((SequenceComputationProcedureCall)seqComp, source);
                break;
            case SequenceComputationType.ProcedureMethodCall:
                EmitSequenceComputationProcedureMethodCall((SequenceComputationProcedureMethodCall)seqComp, source);
                break;
            case SequenceComputationType.ContainerAdd:
                EmitSequenceComputationContainerAdd((SequenceComputationContainerAdd)seqComp, source);
                break;
            case SequenceComputationType.ContainerRem:
                EmitSequenceComputationContainerRem((SequenceComputationContainerRem)seqComp, source);
                break;
            case SequenceComputationType.ContainerClear:
                EmitSequenceComputationContainerClear((SequenceComputationContainerClear)seqComp, source);
                break;
            case SequenceComputationType.ContainerAddAll:
                EmitSequenceComputationContainerAddAll((SequenceComputationContainerAddAll)seqComp, source);
                break;
            case SequenceComputationType.SynchronizationEnter:
                EmitSequenceComputationSynchronizationEnter((SequenceComputationSynchronizationEnter)seqComp, source);
                break;
            case SequenceComputationType.SynchronizationTryEnter:
                EmitSequenceComputationSynchronizationTryEnter((SequenceComputationSynchronizationTryEnter)seqComp, source);
                break;
            case SequenceComputationType.SynchronizationExit:
                EmitSequenceComputationSynchronizationExit((SequenceComputationSynchronizationExit)seqComp, source);
                break;
            case SequenceComputationType.GetEquivalentOrAdd:
                EmitSequenceComputationGetEquivalentOrAdd((SequenceComputationGetEquivalentOrAdd)seqComp, source);
                break;
            case SequenceComputationType.Assignment:
                EmitSequenceComputationAssignment((SequenceComputationAssignment)seqComp, source);
                break;
            default:
				throw new Exception("Unknown sequence computation type: " + seqComp.SequenceComputationType);
			}
		}

        public void EmitSequenceComputationThen(SequenceComputationThen seqThen, SourceBuilder source)
        {
            EmitSequenceComputation(seqThen.left, source);
            EmitSequenceComputation(seqThen.right, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqThen, COMP_HELPER.GetResultVar(seqThen.right)));
        }

        public void EmitSequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration seqVarDecl, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(seqVarDecl.Target, TypesHelper.DefaultValueString(seqVarDecl.Target.Type, model)));
            source.AppendFront(COMP_HELPER.SetResultVar(seqVarDecl, seqHelper.GetVar(seqVarDecl.Target)));
        }

        public void EmitSequenceComputationVAlloc(SequenceComputationVAlloc seqVAlloc, SourceBuilder source)
        {
            source.Append("graph.AllocateVisitedFlag()");
        }

        public void EmitSequenceComputationVFree(SequenceComputationVFree seqVFree, SourceBuilder source)
        {
            String visitedFlagExpr = exprGen.GetSequenceExpression(seqVFree.VisitedFlagExpression, source);
            if(seqVFree.Reset)
                source.AppendFrontFormat("graph.FreeVisitedFlag((int){0});\n", visitedFlagExpr);
            else
                source.AppendFrontFormat("graph.FreeVisitedFlagNonReset((int){0});\n", visitedFlagExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqVFree, "null"));
        }

        public void EmitSequenceComputationVReset(SequenceComputationVReset seqVReset, SourceBuilder source)
        {
            String visitedFlagExpr = exprGen.GetSequenceExpression(seqVReset.VisitedFlagExpression, source);
            source.AppendFrontFormat("graph.ResetVisitedFlag((int){0});\n", visitedFlagExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqVReset, "null"));
        }

        public void EmitSequenceComputationDebugAdd(SequenceComputationDebugAdd seqDebug, SourceBuilder source)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            source.AppendFront("procEnv.DebugEntering(");
            EmitDebugProcedureArguments(seqDebug.ArgExprs, source);
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugRem(SequenceComputationDebugRem seqDebug, SourceBuilder source)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            source.AppendFront("procEnv.DebugExiting(");
            EmitDebugProcedureArguments(seqDebug.ArgExprs, source);
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugEmit(SequenceComputationDebugEmit seqDebug, SourceBuilder source)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            source.AppendFront("procEnv.DebugEmitting(");
            EmitDebugProcedureArguments(seqDebug.ArgExprs, source);
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugHalt(SequenceComputationDebugHalt seqDebug, SourceBuilder source)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            source.AppendFront("procEnv.DebugHalting(");
            EmitDebugProcedureArguments(seqDebug.ArgExprs, source);
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        private void EmitDebugProcedureArguments(IList<SequenceExpression> arguments, SourceBuilder source)
        {
            for(int i = 0; i < arguments.Count; ++i)
            {
                String argExpr = exprGen.GetSequenceExpression(arguments[i], source);
                if(i == 0)
                    source.AppendFormat("(string){0}", argExpr);
                else
                    source.AppendFormat(", {0}", argExpr);
            }
        }
        public void EmitSequenceComputationDebugHighlight(SequenceComputationDebugHighlight seqDebug, SourceBuilder source)
        {
            if(!SequenceBase.FireDebugEvents)
                return;

            source.AppendFront("List<object> values = new List<object>();\n");
            source.AppendFront("List<string> annotations = new List<string>();\n");
            for(int i = 1; i < seqDebug.ArgExprs.Count; ++i)
            {
                String argExpr = exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source);
                if(i % 2 == 1)
                    source.AppendFrontFormat("values.Add({0});\n", argExpr);
                else
                    source.AppendFrontFormat("annotations.Add((string){0});\n", argExpr);
            }
            String firstArgExpr = exprGen.GetSequenceExpression(seqDebug.ArgExprs[0], source);
            source.AppendFrontFormat("procEnv.DebugHighlighting({0}, values, annotations);\n", firstArgExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationAssert(SequenceComputationAssert seqAssert, SourceBuilder source)
        {
            source.AppendFront("procEnv.UserProxy.HandleAssert(");
            source.Append(seqAssert.IsAlways ? "true" : "false");
            foreach(SequenceExpression argExpr in seqAssert.ArgExprs)
            {
                source.Append(", () => ");
                source.Append(exprGen.GetSequenceExpression(argExpr, source));
            }
            if(seqAssert.ArgExprs.Count == 1)
            {
                source.Append(", () => \"\"");
            }
            source.AppendFront(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqAssert, "null"));
        }

        public void EmitSequenceComputationEmit(SequenceComputationEmit seqEmit, SourceBuilder source)
        {
            bool declarationEmitted = false;
            String emitWriter = seqEmit.IsDebug ? "EmitWriterDebug" : "EmitWriter";
            for(int i = 0; i < seqEmit.Expressions.Count; ++i)
            {
                if(!(seqEmit.Expressions[i] is SequenceExpressionConstant))
                {
                    string emitVal = "emitval_" + seqEmit.Id;
                    if(!declarationEmitted)
                    {
                        source.AppendFrontFormat("object {0};\n", emitVal);
                        declarationEmitted = true;
                    }
                    String emitExpr = exprGen.GetSequenceExpression(seqEmit.Expressions[i], source);
                    source.AppendFrontFormat("{0} = {1};\n", emitVal, emitExpr);
                    if(seqEmit.Expressions[i].Type(env) == ""
                        || seqEmit.Expressions[i].Type(env).StartsWith("set<") || seqEmit.Expressions[i].Type(env).StartsWith("map<")
                        || seqEmit.Expressions[i].Type(env).StartsWith("array<") || seqEmit.Expressions[i].Type(env).StartsWith("deque<"))
                    {
                        source.AppendFrontFormat("if({0} is IDictionary)\n", emitVal);
                        source.AppendFrontIndentedFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary){1}, graph, false, null, null));\n",
                            emitWriter, emitVal);
                        source.AppendFrontFormat("else if({0} is IList)\n", emitVal);
                        source.AppendFrontIndentedFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString((IList){1}, graph, false, null, null));\n",
                            emitWriter, emitVal);
                        source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)\n", emitVal);
                        source.AppendFrontIndentedFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque){1}, graph, false, null, null));\n",
                            emitWriter, emitVal);
                        source.AppendFront("else\n");
                        source.AppendFrontIndentedFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString({1}, graph, false, null, null));\n",
                            emitWriter, emitVal);
                    }
                    else
                    {
                        source.AppendFrontFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString({1}, graph, false, null, null));\n",
                            emitWriter, emitVal);
                    }
                }
                else
                {
                    SequenceExpressionConstant constant = (SequenceExpressionConstant)seqEmit.Expressions[i];
                    if(constant.Constant is string)
                    {
                        String text = (string)constant.Constant;
                        text = text.Replace("\n", "\\n");
                        text = text.Replace("\r", "\\r");
                        text = text.Replace("\t", "\\t");
                        source.AppendFrontFormat("procEnv.{0}.Write(\"{1}\");\n",
                            emitWriter, text);
                    }
                    else
                    {
                        String emitExpr = exprGen.GetSequenceExpression(seqEmit.Expressions[i], source);
                        source.AppendFrontFormat("procEnv.{0}.Write(GRGEN_LIBGR.EmitHelper.ToString({1}, graph, false, null, null));\n",
                            emitWriter, emitExpr);
                    }
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqEmit, "null"));
        }

        public void EmitSequenceComputationRecord(SequenceComputationRecord seqRec, SourceBuilder source)
        {
            if(!(seqRec.Expression is SequenceExpressionConstant))
            {
                string recVal = "recval_" + seqRec.Id;
                String recExpr = exprGen.GetSequenceExpression(seqRec.Expression, source);
                source.AppendFront("object " + recVal + " = " + recExpr + ";\n");
                if(seqRec.Expression.Type(env) == ""
                    || seqRec.Expression.Type(env).StartsWith("set<") || seqRec.Expression.Type(env).StartsWith("map<")
                    || seqRec.Expression.Type(env).StartsWith("array<") || seqRec.Expression.Type(env).StartsWith("deque<"))
                {
                    source.AppendFrontFormat("if({0} is IDictionary)\n", recVal);
                    source.AppendFrontIndentedFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary){0}, graph, false, null, null));\n",
                        recVal);
                    source.AppendFrontFormat("else if({0} is IList)\n", recVal);
                    source.AppendFrontIndentedFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IList){0}, graph, false, null, null));\n",
                        recVal);
                    source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)\n", recVal);
                    source.AppendFrontIndentedFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque){0}, graph, false, null, null));\n",
                        recVal);
                    source.AppendFront("else\n");
                    source.AppendFrontIndentedFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString({0}, graph, false, null, null));\n",
                        recVal);
                }
                else
                {
                    source.AppendFrontFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString({0}, graph, false, null, null));\n",
                        recVal);
                }
            }
            else
            {
                SequenceExpressionConstant constant = (SequenceExpressionConstant)seqRec.Expression;
                if(constant.Constant is string)
                {
                    String text = (string)constant.Constant;
                    text = text.Replace("\n", "\\n");
                    text = text.Replace("\r", "\\r");
                    text = text.Replace("\t", "\\t");
                    source.AppendFrontFormat("procEnv.Recorder.Write(\"{0}\");\n", text);
                }
                else
                {
                    String recExpr = exprGen.GetSequenceExpression(seqRec.Expression, source);
                    source.AppendFrontFormat("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString({0}, graph, false, null, null));\n",
                        recExpr);
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqRec, "null"));
        }

        public void EmitSequenceComputationExport(SequenceComputationExport seqExp, SourceBuilder source)
        {
            string expFileName = "expfilename_" + seqExp.Id;
            source.AppendFrontFormat("object {0} = {1};\n",
                expFileName, exprGen.GetSequenceExpression(seqExp.Name, source));
            string expArguments = "exparguments_" + seqExp.Id;
            source.AppendFrontFormat("List<string> {0} = new List<string>();\n", expArguments);
            source.AppendFrontFormat("{0}.Add({1}.ToString());\n", expArguments, expFileName);
            string expGraph = "expgraph_" + seqExp.Id;
            if(seqExp.Graph != null)
            {
                String expExpr = exprGen.GetSequenceExpression(seqExp.Graph, source);
                source.AppendFrontFormat("GRGEN_LIBGR.IGraph {0} = (GRGEN_LIBGR.IGraph){1};\n", expGraph, expExpr);
            }
            else
                source.AppendFrontFormat("GRGEN_LIBGR.IGraph {0} = graph;\n", expGraph);
            source.AppendFrontFormat("if({0} is GRGEN_LIBGR.INamedGraph)\n", expGraph);
            source.AppendFrontIndentedFormat("GRGEN_LIBGR.Porter.Export((GRGEN_LIBGR.INamedGraph){0}, {1});\n",
                expGraph, expArguments);
            source.AppendFront("else\n");
            source.AppendFrontIndentedFormat("GRGEN_LIBGR.Porter.Export({0}, {1});\n",
                expGraph, expArguments);
            source.AppendFront(COMP_HELPER.SetResultVar(seqExp, "null"));
        }

        public void EmitSequenceComputationDeleteFile(SequenceComputationDeleteFile seqDelFile, SourceBuilder source)
        {
            string delFileName = "delfilename_" + seqDelFile.Id;
            String delFileNameExpr = exprGen.GetSequenceExpression(seqDelFile.Name, source);
            source.AppendFrontFormat("object {0} = {1};\n", delFileName, delFileNameExpr);
            source.AppendFrontIndentedFormat("System.IO.File.Delete((string){0});\n", delFileName);
            source.AppendFront(COMP_HELPER.SetResultVar(seqDelFile, "null"));
        }

        public void EmitSequenceComputationGraphAdd(SequenceComputationGraphAdd seqAdd, SourceBuilder source)
        {
            if(seqAdd.ExprSrc == null)
            {
                string typeExpr = exprGen.GetSequenceExpression(seqAdd.Expr, source);
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.AddNodeOfType({0}, graph)", typeExpr);
            }
            else
            {
                string typeExpr = exprGen.GetSequenceExpression(seqAdd.Expr, source);
                string srcExpr = exprGen.GetSequenceExpression(seqAdd.ExprSrc, source);
                string tgtExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.AddEdgeOfType({0}, (GRGEN_LIBGR.INode){1}, (GRGEN_LIBGR.INode){2}, graph)",
                    typeExpr, srcExpr, tgtExpr);
            }
        }

        public void EmitSequenceComputationGraphRem(SequenceComputationGraphRem seqRem, SourceBuilder source)
        {
            string remVal = "remval_" + seqRem.Id;
            string seqRemExpr = exprGen.GetSequenceExpression(seqRem.Expr, source);
            if(seqRem.Expr.Type(env) == "")
            {
                source.AppendFrontFormat("GRGEN_LIBGR.IGraphElement {0} = (GRGEN_LIBGR.IGraphElement){1};\n",
                    remVal, seqRemExpr);
                source.AppendFrontFormat("if({0} is GRGEN_LIBGR.IEdge)\n", remVal);
                source.AppendFrontIndentedFormat("graph.Remove((GRGEN_LIBGR.IEdge){0});\n", remVal);
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("graph.RemoveEdges((GRGEN_LIBGR.INode){0});\n", remVal);
                source.AppendFrontFormat("graph.Remove((GRGEN_LIBGR.INode){0});\n", remVal);
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "Node", model))
                {
                    source.AppendFrontFormat("GRGEN_LIBGR.INode {0} = (GRGEN_LIBGR.INode){1};\n", remVal, seqRemExpr);
                    source.AppendFrontFormat("graph.RemoveEdges({0});\n", remVal);
                    source.AppendFrontFormat("graph.Remove({0});\n", remVal);
                }
                else if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "AEdge", model))
                {
                    source.AppendFrontFormat("GRGEN_LIBGR.IEdge {0} = (GRGEN_LIBGR.IEdge){1};\n", remVal, seqRemExpr);
                    source.AppendFrontFormat("graph.Remove({0});\n", remVal);
                }
                else
                    source.AppendFront("throw new Exception(\"rem() on non-node/edge\");\n");
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqRem, "null"));
        }

        public void EmitSequenceComputationGraphClear(SequenceComputationGraphClear seqClr, SourceBuilder source)
        {
            source.AppendFront("graph.Clear();\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqClr, "null"));
        }

        public void EmitSequenceComputationGraphRetype(SequenceComputationGraphRetype seqRetype, SourceBuilder source)
        {
            string typeExpr = exprGen.GetSequenceExpression(seqRetype.TypeExpr, source);
            string elemExpr = exprGen.GetSequenceExpression(seqRetype.ElemExpr, source);
            source.AppendFormat("GRGEN_LIBGR.GraphHelper.RetypeGraphElement((GRGEN_LIBGR.IGraphElement){0}, {1}, graph)",
                elemExpr, typeExpr);
        }

        public void EmitSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqAddCopy, SourceBuilder source)
        {
            if(seqAddCopy.ExprSrc == null)
            {
                string nodeExpr = exprGen.GetSequenceExpression(seqAddCopy.Expr, source);
                String functionName = seqAddCopy.Deep ? "AddCopyOfNode" : "AddCloneOfNode";
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.{0}({1}, graph)", functionName, nodeExpr);
            }
            else
            {
                string edgeExpr = exprGen.GetSequenceExpression(seqAddCopy.Expr, source);
                string srcExpr = exprGen.GetSequenceExpression(seqAddCopy.ExprSrc, source);
                string tgtExpr = exprGen.GetSequenceExpression(seqAddCopy.ExprDst, source);
                String functionName = seqAddCopy.Deep ? "AddCopyOfEdge" : "AddCloneOfEdge";
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.{0}({1}, (GRGEN_LIBGR.INode){2}, (GRGEN_LIBGR.INode){3}, graph)",
                    functionName, edgeExpr, srcExpr, tgtExpr);
            }
        }

        public void EmitSequenceComputationGraphMerge(SequenceComputationGraphMerge seqMrg, SourceBuilder source)
        {
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqMrg.TargetNodeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqMrg.SourceNodeExpr, source);
            source.AppendFrontFormat("graph.Merge((GRGEN_LIBGR.INode){0}, (GRGEN_LIBGR.INode){1}, \"merge\");\n",
                tgtNodeExpr, srcNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqMrg, "null"));
        }

        public void EmitSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectSource((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old source\");\n",
                edgeExpr, srcNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqRedir, "null"));
        }

        public void EmitSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old target\");\n",
                edgeExpr, tgtNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqRedir, "null"));
        }

        public void EmitSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, (GRGEN_LIBGR.INode){2}, \"old source\", \"old target\");\n",
                edgeExpr, srcNodeExpr, tgtNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqRedir, "null"));
        }

        public void EmitSequenceComputationInsert(SequenceComputationInsert seqIns, SourceBuilder source)
        {
            string graphExpr = exprGen.GetSequenceExpression(seqIns.Graph, source);
            source.AppendFrontFormat("GRGEN_LIBGR.GraphHelper.Insert((GRGEN_LIBGR.IGraph){0}, graph);\n", graphExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqIns, "null"));
        }

        public void EmitSequenceComputationInsertCopy(SequenceComputationInsertCopy seqInsCopy, SourceBuilder source)
        {
            string graphExpr = exprGen.GetSequenceExpression(seqInsCopy.Graph, source);
            string rootNodeExpr = exprGen.GetSequenceExpression(seqInsCopy.RootNode, source);
            source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph){0}, (GRGEN_LIBGR.INode){1}, graph)",
                graphExpr, rootNodeExpr);
        }

        public void EmitSequenceComputationInsertInduced(SequenceComputationInsertInduced seqInsInd, SourceBuilder source)
        {
            String nodeSetExpr = exprGen.GetSequenceExpression(seqInsInd.NodeSet, source);
            String rootNodeExpr = exprGen.GetSequenceExpression(seqInsInd.RootNode, source);
            source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>){0}, (GRGEN_LIBGR.INode){1}, graph)",
                nodeSetExpr, rootNodeExpr);
        }

        public void EmitSequenceComputationInsertDefined(SequenceComputationInsertDefined seqInsDef, SourceBuilder source)
        {
            String edgeSetExpr = exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source);
            String rootEdgeExpr = exprGen.GetSequenceExpression(seqInsDef.RootEdge, source);
            if(seqInsDef.EdgeSet.Type(env) == "set<Edge>")
            {
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertDefinedDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>){0}, (GRGEN_LIBGR.IDEdge){1}, graph)",
                    edgeSetExpr, rootEdgeExpr);
            }
            else if(seqInsDef.EdgeSet.Type(env) == "set<UEdge>")
            {
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertDefinedUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>){0}, (GRGEN_LIBGR.IUEdge){1}, graph)",
                    edgeSetExpr, rootEdgeExpr);
            }
            else if(seqInsDef.EdgeSet.Type(env) == "set<AEdge>")
            {
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>){0}, (GRGEN_LIBGR.IEdge){1}, graph)",
                    edgeSetExpr, rootEdgeExpr);
            }
            else
            {
                source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary){0}, (GRGEN_LIBGR.IEdge){1}, graph)",
                    edgeSetExpr, rootEdgeExpr);
            }
        }

        public void EmitSequenceComputationSynchronizationEnter(SequenceComputationSynchronizationEnter seqEnter, SourceBuilder source)
        {
            source.AppendFrontIndentedFormat("System.Threading.Monitor.Enter({0});\n",
                exprGen.GetSequenceExpression(seqEnter.LockObjectExpr, source));
            source.AppendFront(COMP_HELPER.SetResultVar(seqEnter, "null"));
        }

        public void EmitSequenceComputationSynchronizationTryEnter(SequenceComputationSynchronizationTryEnter seqTryEnter, SourceBuilder source)
        {
            source.AppendFrontIndentedFormat("System.Threading.Monitor.TryEnter({0})",
                exprGen.GetSequenceExpression(seqTryEnter.LockObjectExpr, source));
        }

        public void EmitSequenceComputationSynchronizationExit(SequenceComputationSynchronizationExit seqExit, SourceBuilder source)
        {
            source.AppendFrontIndentedFormat("System.Threading.Monitor.Exit({0});\n",
                exprGen.GetSequenceExpression(seqExit.LockObjectExpr, source));
            source.AppendFront(COMP_HELPER.SetResultVar(seqExit, "null"));
        }

        public void EmitSequenceComputationGetEquivalentOrAdd(SequenceComputationGetEquivalentOrAdd seqGetEquivalentOrAdd, SourceBuilder source)
        {
            source.AppendFrontIndentedFormat("GRGEN_LIBGR.GraphHelper.GetEquivalentOrAdd({0}, {1}, {2})",
                exprGen.GetSequenceExpression(seqGetEquivalentOrAdd.Subgraph, source),
                exprGen.GetSequenceExpression(seqGetEquivalentOrAdd.SubgraphArray, source),
                seqGetEquivalentOrAdd.IncludingAttributes ? "true" : "false");
        }

        public void EmitSequenceComputationExpression(SequenceExpression seqExpr, SourceBuilder source)
        {
            String expr = exprGen.GetSequenceExpression(seqExpr, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqExpr, expr));
        }

        public void EmitSequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall seqCall, SourceBuilder source)
        {
            SourceBuilder sb = new SourceBuilder();
            EmitSequenceComputation(seqCall.BuiltinProcedure, sb);
            if(seqCall.ReturnVars.Count > 0)
            {
                source.AppendFront(seqHelper.SetVar(seqCall.ReturnVars[0], sb.ToString()));
                source.AppendFront(COMP_HELPER.SetResultVar(seqCall, seqHelper.GetVar(seqCall.ReturnVars[0])));
            }
            else
            {
                source.AppendFront(sb.ToString() + ";\n");
                source.AppendFront(COMP_HELPER.SetResultVar(seqCall, "null"));
            }
        }

        public void EmitSequenceComputationProcedureCall(SequenceComputationProcedureCall seqCall, SourceBuilder source)
        {
            String returnParameterDeclarations;
            String returnArguments;
            String returnAssignments;
            seqHelper.BuildReturnParameters(seqCall, seqCall.ReturnVars, out returnParameterDeclarations, out returnArguments, out returnAssignments);

            if(returnParameterDeclarations.Length != 0)
                source.AppendFront(returnParameterDeclarations + "\n");

            String arguments = seqHelper.BuildParameters(seqCall, seqCall.ArgumentExpressions, source);
            if(seqCall.IsExternal)
                source.AppendFront("GRGEN_EXPR.ExternalProcedures.");
            else
                source.AppendFrontFormat("{0}Procedures.", "GRGEN_ACTIONS." + TypesHelper.GetPackagePrefixDot(seqCall.Package));
            source.AppendFormat("{0}(procEnv, graph{1}{2});\n", seqCall.Name, arguments, returnArguments);

            if(returnAssignments.Length != 0)
                source.AppendFront(returnAssignments + "\n");

            source.AppendFront(COMP_HELPER.SetResultVar(seqCall, "null"));
        }

        public void EmitSequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall seqCall, SourceBuilder source)
        {
            String type = seqCall.TargetExpr != null ? seqCall.TargetExpr.Type(env) : seqCall.TargetVar.Type;
            if(type == "")
            {
                string tmpVarName = "tmpvar_" + seqHelper.GetUniqueId();
                String target;
                if(seqCall.TargetExpr != null)
                    target = exprGen.GetSequenceExpression(seqCall.TargetExpr, source);
                else
                    target = seqHelper.GetVar(seqCall.TargetVar);
                String arguments = seqHelper.BuildParametersInObject(seqCall, seqCall.ArgumentExpressions, source);
                source.AppendFrontFormat("object[] {0} = ((GRGEN_LIBGR.IGraphElement){1}).ApplyProcedureMethod(procEnv, graph, \"{2}\"{3});\n",
                    tmpVarName, target, seqCall.Name, arguments);
                for(int i = 0; i < seqCall.ReturnVars.Length; ++i)
                {
                    source.Append(seqHelper.SetVar(seqCall.ReturnVars[i], tmpVarName));
                }
            }
            else
            {
                String returnParameterDeclarations;
                String returnArguments;
                String returnAssignments;
                seqHelper.BuildReturnParameters(seqCall, seqCall.ReturnVars, TypesHelper.GetInheritanceType(type, model),
                    out returnParameterDeclarations, out returnArguments, out returnAssignments);

                if(returnParameterDeclarations.Length != 0)
                    source.AppendFront(returnParameterDeclarations + "\n");

                String target;
                if(seqCall.TargetExpr != null)
                    target = exprGen.GetSequenceExpression(seqCall.TargetExpr, source);
                else
                    target = seqHelper.GetVar(seqCall.TargetVar);
                IProcedureDefinition procedureMethod = TypesHelper.GetInheritanceType(type, model).GetProcedureMethod(seqCall.Name);
                String arguments = seqHelper.BuildParameters(seqCall, seqCall.ArgumentExpressions, procedureMethod, source);
                source.AppendFrontFormat("(({0}){1}).{2}(procEnv, graph{3}{4});\n", 
                    TypesHelper.XgrsTypeToCSharpType(type, model), target, seqCall.Name, arguments, returnArguments);
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqCall, "null"));
        }

        //-------------------------------------------------------------------------------------------------------------------

        #region Container procedures

        public void EmitSequenceComputationContainerAdd(SequenceComputationContainerAdd seqAdd, SourceBuilder source)
        {
            string container = GetContainerValue(seqAdd, source);

            if(seqAdd.ContainerType(env) == "")
                EmitSequenceComputationContainerAddUnknownType(seqAdd, container, source);
            else if(seqAdd.ContainerType(env).StartsWith("array"))
                EmitSequenceComputationContainerAddArray(seqAdd, container, source);
            else if(seqAdd.ContainerType(env).StartsWith("deque"))
                EmitSequenceComputationContainerAddDeque(seqAdd, container, source);
            else
                EmitSequenceComputationContainerAddSetMap(seqAdd, container, source);
        }

        public void EmitSequenceComputationContainerAddUnknownType(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            if(seqAdd.Attribute != null)
                EmitAttributeEventInitialization(seqAdd.Attribute, source);
            string containerVar = "tmp_eval_once_" + seqAdd.Id;
            source.AppendFrontFormat("object {0} = {1};\n", containerVar, container);
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFrontFormat("object {0} = {1};\n",
                sourceValue, exprGen.GetSequenceExpression(seqAdd.Expr, source));
            string destinationValue = null;
            string destinationExpr = null;
            if(seqAdd.ExprDst != null)
            {
                destinationValue = "dstval_" + seqAdd.Id;
                destinationExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
            }
            source.AppendFrontFormat("if({0} is IList)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't add non-int key to array\");\n");
            else
            {
                string array = "((System.Collections.IList)" + containerVar + ")";
                if(destinationValue != null)
                    source.AppendFrontFormat("int {0} = (int){1};\n", destinationValue, destinationExpr);
                if(seqAdd.Attribute != null)
                {
                    if(destinationValue != null)
                        EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, destinationValue, source);
                    else
                        EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
                }
                if(destinationValue != null)
                    source.AppendFrontFormat("{0}.Insert({1}, {2});\n", array, destinationValue, sourceValue);
                else
                    source.AppendFrontFormat("{0}.Add({1});\n", array, sourceValue);
                if(seqAdd.Attribute != null)
                    EmitAttributeChangedEvent(seqAdd.Attribute, source);
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't add non-int key to deque\");\n");
            else
            {
                string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                if(destinationValue != null)
                    source.AppendFrontFormat("int {0} = (int){1};\n", destinationValue, destinationExpr);
                if(seqAdd.Attribute != null)
                {
                    if(destinationValue != null)
                        EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, destinationValue, source);
                    else
                        EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
                }
                if(destinationValue != null)
                    source.AppendFrontFormat("{0}.EnqueueAt({1}, {2});\n", deque, destinationValue, sourceValue);
                else
                    source.AppendFrontFormat("{0}.Enqueue({1});\n", deque, sourceValue);
                if(seqAdd.Attribute != null)
                    EmitAttributeChangedEvent(seqAdd.Attribute, source);
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            if(destinationValue != null)
                source.AppendFrontFormat("object {0} = {1};\n", destinationValue, destinationExpr);
            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(seqAdd.Attribute != null)
            {
                if(destinationValue != null) // must be map
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, destinationValue, sourceValue, source);
                else
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
            }
            if(destinationValue != null)
                source.AppendFrontFormat("{0}[{1}] = {2};\n", dictionary, sourceValue, destinationValue);
            else
                source.AppendFrontFormat("{0}[{1}] = null;\n", dictionary, sourceValue);
            if(seqAdd.Attribute != null)
                EmitAttributeChangedEvent(seqAdd.Attribute, source);

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, containerVar));
        }

        public void EmitSequenceComputationContainerAddArray(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string array = container;
            string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                arrayValueType, sourceValue, exprGen.GetSequenceExpression(seqAdd.Expr, source));
            string destinationValue = null;
            string destinationExpr = null;
            if(seqAdd.ExprDst != null)
            {
                destinationValue = "dstval_" + seqAdd.Id;
                destinationExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
                source.AppendFrontFormat("int {0} = (int){1};\n", destinationValue, destinationExpr);
            }
            if(seqAdd.Attribute != null)
            {
                EmitAttributeEventInitialization(seqAdd.Attribute, source);

                if(destinationValue != null)
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, destinationValue, source);
                else
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
            }
            if(destinationValue != null)
                source.AppendFrontFormat("((System.Collections.IList){0}).Insert({1}, {2});\n", array, destinationValue, sourceValue);
            else
                source.AppendFrontFormat("((System.Collections.IList){0}).Add({1});\n", array, sourceValue);
            if(seqAdd.Attribute != null)
                EmitAttributeChangedEvent(seqAdd.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, container));
        }

        public void EmitSequenceComputationContainerAddDeque(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string deque = container;
            string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                dequeValueType, sourceValue, exprGen.GetSequenceExpression(seqAdd.Expr, source));
            string destinationValue = null;
            string destinationExpr = null;
            if(seqAdd.ExprDst != null)
            {
                destinationValue = "dstval_" + seqAdd.Id;
                destinationExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
                source.AppendFrontFormat("int {0} = (int){1};\n", destinationValue, destinationExpr);
            }
            if(seqAdd.Attribute != null)
            {
                EmitAttributeEventInitialization(seqAdd.Attribute, source);

                if(destinationValue != null)
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, destinationValue, source);
                else
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
            }
            if(destinationValue != null)
                source.AppendFrontFormat("((GRGEN_LIBGR.IDeque){0}).EnqueueAt({1}, {2});\n", deque, destinationValue, sourceValue);
            else
                source.AppendFrontFormat("((GRGEN_LIBGR.IDeque){0}).Enqueue({1});\n", deque, sourceValue);
            if(seqAdd.Attribute != null)
                EmitAttributeChangedEvent(seqAdd.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, container));
        }

        public void EmitSequenceComputationContainerAddSetMap(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string dictionary = container;
            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = " srcval_" + seqAdd.Id;
            source.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                dictSrcType, sourceValue, exprGen.GetSequenceExpression(seqAdd.Expr, source));
            string dictDstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqAdd.ContainerType(env)), model);
            string destinationValue = null;
            string destinationExpr = null;
            if(seqAdd.ExprDst != null)
            {
                destinationValue = "dstval_" + seqAdd.Id;
                destinationExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
                source.AppendFrontFormat("{0} {1} = ({0}){2};\n", dictDstType, destinationValue, destinationExpr);
            }
            if(seqAdd.Attribute != null)
            {
                EmitAttributeEventInitialization(seqAdd.Attribute, source);

                if(destinationValue != null) // must be map
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, destinationValue, sourceValue, source);
                else
                    EmitAttributeChangingEvent(seqAdd.Attribute, AttributeChangeType.PutElement, sourceValue, "null", source);
            }
            if(destinationValue != null)
                source.AppendFrontFormat("((System.Collections.IDictionary){0})[{1}] = {2};\n", dictionary, sourceValue, destinationValue);
            else
                source.AppendFrontFormat("((System.Collections.IDictionary){0})[{1}] = null;\n", dictionary, sourceValue);
            if(seqAdd.Attribute != null)
                EmitAttributeChangedEvent(seqAdd.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, container));
        }

        public void EmitSequenceComputationContainerRem(SequenceComputationContainerRem seqDel, SourceBuilder source)
        {
            string container = GetContainerValue(seqDel, source);

            if(seqDel.ContainerType(env) == "")
                EmitSequenceComputationContainerRemUnknownType(seqDel, container, source);
            else if(seqDel.ContainerType(env).StartsWith("array"))
                EmitSequenceComputationContainerRemArray(seqDel, container, source);
            else if(seqDel.ContainerType(env).StartsWith("deque"))
                EmitSequenceComputationContainerRemDeque(seqDel, container, source);
            else
                EmitSequenceComputationContainerRemSetMap(seqDel, container, source);
        }

        public void EmitSequenceComputationContainerRemUnknownType(SequenceComputationContainerRem seqDel, String container, SourceBuilder source)
        {
            if(seqDel.Attribute != null)
                EmitAttributeEventInitialization(seqDel.Attribute, source);
            string containerVar = "tmp_eval_once_" + seqDel.Id;
            source.AppendFrontFormat("object {0} = {1};\n", containerVar, container);
            string sourceValue = null;
            string sourceExpr = null;
            if(seqDel.Expr != null)
            {
                sourceValue = "srcval_" + seqDel.Id;
                sourceExpr = exprGen.GetSequenceExpression(seqDel.Expr, source);
            }

            source.AppendFrontFormat("if({0} is IList)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't remove non-int index from array\");\n");
            else
            {
                string array = "((System.Collections.IList)" + containerVar + ")";
                if(sourceValue != null)
                    source.AppendFrontFormat("int {0} = (int){1};\n", sourceValue, sourceExpr);
                if(seqDel.Attribute != null)
                {
                    if(sourceValue != null)
                        EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);
                    else
                        EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", "null", source);
                }
                if(sourceValue == null)
                    source.AppendFrontFormat("{0}.RemoveAt({0}.Count - 1);\n", array);
                else
                    source.AppendFrontFormat("{0}.RemoveAt({1});\n", array, sourceValue);
                if(seqDel.Attribute != null)
                    EmitAttributeChangedEvent(seqDel.Attribute, source);
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't remove non-int index from deque\");\n");
            else
            {
                string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                if(sourceValue != null)
                    source.AppendFrontFormat("int {0} = (int){1};\n", sourceValue, sourceExpr);
                if(seqDel.Attribute != null)
                {
                    if(sourceValue != null)
                        EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);
                    else
                        EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", "null", source);
                }
                if(sourceValue == null)
                    source.AppendFrontFormat("{0}.Dequeue();\n", deque);
                else
                    source.AppendFrontFormat("{0}.DequeueAt({1});\n", deque, sourceValue);
                if(seqDel.Attribute != null)
                    EmitAttributeChangedEvent(seqDel.Attribute, source);
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(sourceValue != null)
                source.AppendFrontFormat("object {0} = {1};\n", sourceValue, sourceExpr);
            if(seqDel.Attribute != null)
            {
                string attrType = "attrType_" + seqDel.Attribute.Id;
                source.AppendFrontFormat("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType({0})) == \"SetValueType\")\n",
                    attrType);
                source.AppendFront("{\n");
                source.Indent();

                EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, sourceValue, "null", source);

                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();

                EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);

                source.Unindent();
                source.AppendFront("}\n");
            }
            if(sourceValue == null)
                source.AppendFrontFormat("throw new Exception(\"{0}.rem() only possible on array or deque!\");\n", seqDel.Container.PureName);
            else
                source.AppendFrontFormat("{0}.Remove({1});\n", dictionary, sourceValue);
            if(seqDel.Attribute != null)
                EmitAttributeChangedEvent(seqDel.Attribute, source);

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, containerVar));
        }

        public void EmitSequenceComputationContainerRemArray(SequenceComputationContainerRem seqDel, String container, SourceBuilder source)
        {
            string array = container;
            string sourceValue = null;
            string sourceExpr = null;
            if(seqDel.Expr != null)
            {
                sourceValue = "srcval_" + seqDel.Id;
                sourceExpr = exprGen.GetSequenceExpression(seqDel.Expr, source);
                source.AppendFrontFormat("int {0} = (int){1};\n", sourceValue, sourceExpr);
            }
            if(seqDel.Attribute != null)
            {
                EmitAttributeEventInitialization(seqDel.Attribute, source);

                if(sourceValue != null)
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);
                else
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", "null", source);
            }
            if(sourceValue == null)
                source.AppendFrontFormat("((System.Collections.IList){0}).RemoveAt({0}.Count - 1);\n", array);
            else
                source.AppendFrontFormat("((System.Collections.IList){0}).RemoveAt({1});\n", array, sourceValue);
            if(seqDel.Attribute != null)
                EmitAttributeChangedEvent(seqDel.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, container));
        }

        public void EmitSequenceComputationContainerRemDeque(SequenceComputationContainerRem seqDel, string container, SourceBuilder source)
        {
            string deque = container;
            string sourceValue = null;
            string sourceExpr = null;
            if(seqDel.Expr != null)
            {
                sourceValue = "srcval_" + seqDel.Id;
                sourceExpr = exprGen.GetSequenceExpression(seqDel.Expr, source);
                source.AppendFrontFormat("int {0} = (int){1};\n", sourceValue, sourceExpr);
            }
            if(seqDel.Attribute != null)
            {
                EmitAttributeEventInitialization(seqDel.Attribute, source);

                if(sourceValue != null)
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);
                else
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", "null", source);
            }
            if(sourceValue == null)
                source.AppendFrontFormat("((GRGEN_LIBGR.IDeque){0}).Dequeue();\n", deque);
            else
                source.AppendFrontFormat("((GRGEN_LIBGR.IDeque){0}).DequeueAt({1});\n", deque, sourceValue);
            if(seqDel.Attribute != null)
                EmitAttributeChangedEvent(seqDel.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, container));
        }

        public void EmitSequenceComputationContainerRemSetMap(SequenceComputationContainerRem seqDel, string container, SourceBuilder source)
        {
            string dictionary = container;
            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqDel.Id;
            string sourceExpr = exprGen.GetSequenceExpression(seqDel.Expr, source);
            source.AppendFrontFormat("{0} {1} = ({0}){2};\n", dictSrcType, sourceValue, sourceExpr);
            if(seqDel.Attribute != null)
            {
                EmitAttributeEventInitialization(seqDel.Attribute, source);

                if(TypesHelper.ExtractDst(seqDel.ContainerType(env)) == "SetValueType")
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, sourceValue, "null", source);
                else
                    EmitAttributeChangingEvent(seqDel.Attribute, AttributeChangeType.RemoveElement, "null", sourceValue, source);
            }
            source.AppendFrontFormat("((System.Collections.IDictionary){0}).Remove({1});\n", dictionary, sourceValue);
            if(seqDel.Attribute != null)
                EmitAttributeChangedEvent(seqDel.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, container));
        }

        public void EmitSequenceComputationContainerClear(SequenceComputationContainerClear seqClear, SourceBuilder source)
        {
            string container = GetContainerValue(seqClear, source);

            if(seqClear.ContainerType(env) == "")
                EmitSequenceComputationContainerClearUnknownType(seqClear, container, source);
            else if(seqClear.ContainerType(env).StartsWith("array"))
                EmitSequenceComputationContainerClearArray(seqClear, container, source);
            else if(seqClear.ContainerType(env).StartsWith("deque"))
                EmitSequenceComputationContainerClearDeque(seqClear, container, source);
            else
                EmitSequenceComputationContainerClearSetMap(seqClear, container, source);
        }

        public void EmitSequenceComputationContainerClearUnknownType(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            if(seqClear.Attribute != null)
                EmitAttributeEventInitialization(seqClear.Attribute, source);
            string containerVar = "tmp_eval_once_" + seqClear.Id;
            source.AppendFrontFormat("object {0} = {1};\n", containerVar, container);

            source.AppendFrontFormat("if({0} is IList)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            string array = "((System.Collections.IList)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                String index = "i_" + seqClear.Id;
                source.AppendFrontFormat("for(int {0} = {1}.Count; {0} >= 0; --{0})\n", index, array);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", index, source);

                source.Unindent();
            }
            source.AppendFrontFormat("{0}.Clear();\n", array);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)\n", containerVar);
            source.AppendFront("{\n");
            source.Indent();

            string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                String index = "i_" + seqClear.Id;
                source.AppendFrontFormat("for(int {0} = {1}.Count; {0} >= 0; --{0})\n", index, deque);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", index, source);

                source.Unindent();
            }
            source.AppendFrontFormat("{0}.Clear();\n", deque);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                string attrType = "attrType_" + seqClear.Attribute.Id;
                source.AppendFrontFormat("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType({0})) == \"SetValueType\")\n",
                    attrType);
                source.AppendFront("{\n");
                source.Indent();
                String kvp = "kvp_" + seqClear.Id;
                source.AppendFrontFormat("foreach(DictionaryEntry {0} in {1})\n", kvp, dictionary);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, kvp, "null", source);

                source.Unindent();
                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("foreach(DictionaryEntry {0} in {1})\n", kvp, dictionary);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", kvp, source);

                source.Unindent();
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.AppendFrontFormat("{0}.Clear();\n", dictionary);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, containerVar));
        }

        public void EmitSequenceComputationContainerClearArray(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string array = container;
            if(seqClear.Attribute != null)
            {
                EmitAttributeEventInitialization(seqClear.Attribute, source);

                String index = "i_" + seqClear.Id;
                source.AppendFrontFormat("for(int {0} = {1}.Count; {0} >= 0; --{0})\n", index, array);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", index, source);

                source.Unindent();
            }
            source.AppendFrontFormat("((System.Collections.IList){0}).Clear();\n", array);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        public void EmitSequenceComputationContainerClearDeque(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string deque = container;
            if(seqClear.Attribute != null)
            {
                EmitAttributeEventInitialization(seqClear.Attribute, source);

                String index = "i_" + seqClear.Id;
                source.AppendFrontFormat("for(int {0} = {1}.Count; {0} >= 0; --{0})\n", index, deque);
                source.Indent();

                EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", index, source);

                source.Unindent();
            }
            source.AppendFrontFormat("((GRGEN_LIBGR.IDeque){0}).Clear();\n", deque);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        public void EmitSequenceComputationContainerClearSetMap(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string dictionary = container;
            if(seqClear.Attribute != null)
            {
                EmitAttributeEventInitialization(seqClear.Attribute, source);

                if(TypesHelper.ExtractDst(seqClear.ContainerType(env)) == "SetValueType")
                {
                    String kvp = "kvp_" + seqClear.Id;
                    source.AppendFrontFormat("foreach(DictionaryEntry {0} in (System.Collections.IDictionary){1})\n", kvp, dictionary);
                    source.Indent();

                    EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, kvp, "null", source);

                    source.Unindent();
                }
                else
                {
                    String kvp = "kvp" + seqClear.Id;
                    source.AppendFrontFormat("foreach(DictionaryEntry {0} in (System.Collections.IDictionary){1})\n", kvp, dictionary);
                    source.Indent();

                    EmitAttributeChangingEvent(seqClear.Attribute, AttributeChangeType.RemoveElement, "null", kvp, source);

                    source.Unindent();
                }
            }
            source.AppendFrontFormat("{0}.Clear();\n", dictionary);
            if(seqClear.Attribute != null)
                EmitAttributeChangedEvent(seqClear.Attribute, source);
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        public void EmitSequenceComputationContainerAddAll(SequenceComputationContainerAddAll seqAddAll, SourceBuilder source)
        {
            string container = GetContainerValue(seqAddAll, source);
            string expression = exprGen.GetSequenceExpression(seqAddAll.Expr, source);

            if(seqAddAll.ContainerType(env) == "")
                source.AppendFrontFormat("GRGEN_LIBGR.ContainerHelper.ContainerAddAll({0}, {1});\n", container, expression);
            else //if(seqAddAll.ContainerType(env).StartsWith("array") || seqAddAll.ContainerType(env).StartsWith("set"))
            {
                source.AppendFrontFormat("GRGEN_LIBGR.ContainerHelper.AddAll(({0})({1}), ({0})({2}));\n",
                    TypesHelper.XgrsTypeToCSharpType(seqAddAll.Container.Type, model), container, expression);
            }
        }

        private string GetContainerValue(SequenceComputationContainer container, SourceBuilder source)
        {
            if(container.Container != null)
                return seqHelper.GetVar(container.Container);
            else
            {
                String attributeSourceExpr = exprGen.GetSequenceExpression(container.Attribute.Source, source);
                return "((GRGEN_LIBGR.IAttributeBearer)" + attributeSourceExpr + ")" + ".GetAttribute(\"" + container.Attribute.AttributeName + "\")";
            }
        }

        private void EmitAttributeEventInitialization(SequenceExpressionAttributeAccess attrAccess, SourceBuilder source)
        {
            string element = "elem_" + attrAccess.Id;
            string attrType = "attrType_" + attrAccess.Id;

            String attrSourceExpr = exprGen.GetSequenceExpression(attrAccess.Source, source);
            source.AppendFrontFormat("GRGEN_LIBGR.IAttributeBearer {0} = (GRGEN_LIBGR.IAttributeBearer){1};\n",
                element, attrSourceExpr);
            source.AppendFrontFormat("GRGEN_LIBGR.AttributeType {0} = {1}.Type.GetAttributeType(\"{2}\");\n",
                attrType, element, attrAccess.AttributeName);
        }

        private void EmitAttributeChangingEvent(SequenceExpressionAttributeAccess attrAccess, AttributeChangeType attrChangeType,
            String newValue, String keyValue, SourceBuilder source)
        {
            EmitAttributeChangingEvent(attrAccess.Id, attrChangeType, newValue, keyValue, source);
        }

        private void EmitAttributeChangedEvent(SequenceExpressionAttributeAccess attrAccess, SourceBuilder source)
        {
            EmitAttributeChangedEvent(attrAccess.Id, source);
        }

        #endregion Container procedures

        //-------------------------------------------------------------------------------------------------------------------

        #region Assignment (Assignments by target type)

        public void EmitSequenceComputationAssignment(SequenceComputationAssignment seqAssign, SourceBuilder source)
        {
            if(seqAssign.SourceValueProvider is SequenceComputationAssignment)
            {
                EmitSequenceComputation(seqAssign.SourceValueProvider, source);
                EmitAssignment(seqAssign.Target, COMP_HELPER.GetResultVar(seqAssign.SourceValueProvider), source);
                source.AppendFront(COMP_HELPER.SetResultVar(seqAssign, COMP_HELPER.GetResultVar(seqAssign.Target)));
            }
            else
            {
                string comp = exprGen.GetSequenceExpression((SequenceExpression)seqAssign.SourceValueProvider, source);
                EmitAssignment(seqAssign.Target, comp, source);
                source.AppendFront(COMP_HELPER.SetResultVar(seqAssign, COMP_HELPER.GetResultVar(seqAssign.Target)));
            }
        }

        void EmitAssignment(AssignmentTarget tgt, string sourceValueComputation, SourceBuilder source)
		{
			switch(tgt.AssignmentTargetType)
			{
            case AssignmentTargetType.YieldingToVar:
                EmitAssignmentYieldingToVar((AssignmentTargetYieldingVar)tgt, sourceValueComputation, source);
                break;
            case AssignmentTargetType.Visited:
                EmitAssignmentVisited((AssignmentTargetVisited)tgt, sourceValueComputation, source);
                break;
            case AssignmentTargetType.IndexedVar:
                EmitAssignmentIndexedVar((AssignmentTargetIndexedVar)tgt, sourceValueComputation, source);
                break;
            case AssignmentTargetType.Var:
                EmitAssignmentVar((AssignmentTargetVar)tgt, sourceValueComputation, source);
				break;
            case AssignmentTargetType.Attribute:
                EmitAssignmentAttribute((AssignmentTargetAttribute)tgt, sourceValueComputation, source);
                break;
            case AssignmentTargetType.AttributeIndexed:
                EmitAssignmentAttributeIndexed((AssignmentTargetAttributeIndexed)tgt, sourceValueComputation, source);
                break;
			default:
				throw new Exception("Unknown assignment target type: " + tgt.AssignmentTargetType);
			}
		}

        void EmitAssignmentYieldingToVar(AssignmentTargetYieldingVar tgtYield, string sourceValueComputation, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(tgtYield.DestVar, sourceValueComputation));
            source.AppendFront(COMP_HELPER.SetResultVar(tgtYield, seqHelper.GetVar(tgtYield.DestVar)));
        }

        void EmitAssignmentVisited(AssignmentTargetVisited tgtVisitedFlag, string sourceValueComputation, SourceBuilder source)
        {
            String visval = "visval_" + tgtVisitedFlag.Id;
            source.AppendFrontFormat("bool {0} = (bool){1};\n", visval, sourceValueComputation);
            String visitedFlagExpr = tgtVisitedFlag.VisitedFlagExpression != null ? exprGen.GetSequenceExpression(tgtVisitedFlag.VisitedFlagExpression, source) : "0";
            source.AppendFrontFormat("graph.SetVisited((GRGEN_LIBGR.IGraphElement){0}, (int){1}, {2});\n",
                seqHelper.GetVar(tgtVisitedFlag.GraphElementVar), visitedFlagExpr, visval);
            source.AppendFront(COMP_HELPER.SetResultVar(tgtVisitedFlag, visval));
        }

        void EmitAssignmentIndexedVar(AssignmentTargetIndexedVar tgtIndexedVar, string sourceValueComputation, SourceBuilder source)
        {
            string container = "container_" + tgtIndexedVar.Id;
            source.AppendFrontFormat("object {0} = {1};\n", container, seqHelper.GetVar(tgtIndexedVar.DestVar));
            string key = "key_" + tgtIndexedVar.Id;
            string keyExpr = exprGen.GetSequenceExpression(tgtIndexedVar.KeyExpression, source);
            source.AppendFrontFormat("object {0} = {1};\n", key, keyExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(tgtIndexedVar, container)); // container is a reference, so we can assign it already here before the changes

            if(tgtIndexedVar.DestVar.Type == "")
            {
                EmitAssignmentIndexedVarUnknownType(tgtIndexedVar, sourceValueComputation,
                    container, key, source);
            }
            else if(tgtIndexedVar.DestVar.Type.StartsWith("array"))
            {
                string array = seqHelper.GetVar(tgtIndexedVar.DestVar);
                source.AppendFrontFormat("if({0}.Count > (int){1})\n", array, key);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("{0}[(int){1}] = {2};\n", array, key, sourceValueComputation);
                source.Unindent();
                source.AppendFront("}\n");
            }
            else if(tgtIndexedVar.DestVar.Type.StartsWith("deque"))
            {
                string deque = seqHelper.GetVar(tgtIndexedVar.DestVar);
                source.AppendFrontFormat("if({0}.Count > (int){1})\n", deque, key);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("{0}[(int){1}] = {2};\n", deque, key, sourceValueComputation);
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                string dictionary = seqHelper.GetVar(tgtIndexedVar.DestVar);
                string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtIndexedVar.DestVar.Type), model);
                source.AppendFrontFormat("if({0}.ContainsKey(({1}){2}))\n", dictionary, dictSrcType, key);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("{0}[({1}){2}] = {3};\n", dictionary, dictSrcType, key, sourceValueComputation);
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitAssignmentIndexedVarUnknownType(AssignmentTargetIndexedVar tgtIndexedVar, string sourceValueComputation, string container, string key, SourceBuilder source)
        {
            source.AppendFrontFormat("if({0} is IList)\n", container);
            source.AppendFront("{\n");
            source.Indent();

            string array = "((System.Collections.IList)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true)\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
            }
            else
            {
                source.AppendFrontFormat("if({0}.Count > (int){1})\n", array, key);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("{0}[(int){1}] = {2};\n", array, key, sourceValueComputation);
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IDeque)", container);
            source.AppendFront("{\n");
            source.Indent();

            string deque = "((GRGEN_LIBGR.IDeque)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true)\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in deque\");\n");
            }
            else
            {
                source.AppendFrontFormat("if({0}.Count > (int){1})\n", deque, key);
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFrontFormat("{0}[(int){1}] = {2};\n", deque, key, sourceValueComputation);
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront("else\n");
            source.AppendFront("{\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + container + ")";
            source.AppendFrontFormat("if({0}.Contains({1}))\n", dictionary, key);
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFrontFormat("{0}[{1}] = {2};\n", dictionary, key, sourceValueComputation);
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        void EmitAssignmentVar(AssignmentTargetVar tgtVar, string sourceValueComputation, SourceBuilder source)
        {
            source.AppendFront(seqHelper.SetVar(tgtVar.DestVar, sourceValueComputation));
            source.AppendFront(COMP_HELPER.SetResultVar(tgtVar, seqHelper.GetVar(tgtVar.DestVar)));
        }

        void EmitAssignmentAttribute(AssignmentTargetAttribute tgtAttr, string sourceValueComputation, SourceBuilder source)
        {
            string element = "elem_" + tgtAttr.Id;
            string attrType = "attrType_" + tgtAttr.Id;
            string value = "value_" + tgtAttr.Id;

            if(tgtAttr.DestVar.Type != "")
            {
                if(TypesHelper.GetGraphElementType(tgtAttr.DestVar.Type, model) != null)
                {
                    EmitAttributeAssignWithEventInitialization(tgtAttr, element, attrType, value, sourceValueComputation, source);

                    EmitAttributeChangingEvent(tgtAttr.Id, AttributeChangeType.Assign, value, "null", source);

                    source.AppendFrontFormat("{0}.SetAttribute(\"{1}\", {2});\n", element, tgtAttr.AttributeName, value);

                    EmitAttributeChangedEvent(tgtAttr.Id, source);
                }
                else
                {
                    EmitAttributeAssignInitialization(tgtAttr, element, attrType, value, sourceValueComputation, source);

                    source.AppendFrontFormat("{0}.SetAttribute(\"{1}\", {2});\n", element, tgtAttr.AttributeName, value);
                }
            }
            else
            {
                source.AppendFrontFormat("object {0} = {1};\n", value, sourceValueComputation);
                source.AppendFrontFormat("GRGEN_LIBGR.ContainerHelper.AssignAttribute({0}, {1}, {2}, {3});\n",
                    seqHelper.GetVar(tgtAttr.DestVar), value, "\"" + tgtAttr.AttributeName + "\"", "graph");
            }

            source.AppendFront(COMP_HELPER.SetResultVar(tgtAttr, "value_" + tgtAttr.Id));
        }

        private void EmitAttributeAssignWithEventInitialization(AssignmentTargetAttribute tgtAttr, String element, String attrType, 
            String value, string sourceValueComputation, SourceBuilder source)
        {
            source.AppendFrontFormat("GRGEN_LIBGR.IGraphElement {0} = (GRGEN_LIBGR.IGraphElement){1};\n",
                element, seqHelper.GetVar(tgtAttr.DestVar));
            source.AppendFrontFormat("GRGEN_LIBGR.AttributeType {0};\n", attrType);
            source.AppendFrontFormat("object {0} = {1};\n", value, sourceValueComputation);
            source.AppendFrontFormat("{0} = GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer({1}, \"{2}\", {0}, out {3});\n",
                value, element, tgtAttr.AttributeName, attrType);
        }

        private void EmitAttributeAssignInitialization(AssignmentTargetAttribute tgtAttr, String element, String attrType,
            String value, string sourceValueComputation, SourceBuilder source)
        {
            source.AppendFrontFormat("GRGEN_LIBGR.IBaseObject {0} = (GRGEN_LIBGR.IBaseObject){1};\n",
                element, seqHelper.GetVar(tgtAttr.DestVar));
            source.AppendFrontFormat("object {0} = {1};\n", value, sourceValueComputation);
        }

        void EmitAssignmentAttributeIndexed(AssignmentTargetAttributeIndexed tgtAttrIndexedVar, string sourceValueComputation, SourceBuilder source)
        {
            string element = "elem_" + tgtAttrIndexedVar.Id;
            string attrType = "attrType_" + tgtAttrIndexedVar.Id;
            string value = "value_" + tgtAttrIndexedVar.Id;

            if(TypesHelper.GetGraphElementType(tgtAttrIndexedVar.DestVar.Type, model) != null)
                EmitAttributeAssignWithEventInitialization(tgtAttrIndexedVar, element, attrType, value, sourceValueComputation, source);
            else // statically known object type or statically not known type
                EmitAttributeAssignInitialization(tgtAttrIndexedVar, element, attrType, value, sourceValueComputation, source);

            string container = "container_" + tgtAttrIndexedVar.Id;
            source.AppendFrontFormat("object {0} = {1}.GetAttribute(\"{2}\");\n",
                container, element, tgtAttrIndexedVar.AttributeName);
            string key = "key_" + tgtAttrIndexedVar.Id;
            string keyExpr = exprGen.GetSequenceExpression(tgtAttrIndexedVar.KeyExpression, source);
            source.AppendFrontFormat("object {0} = {1};\n", key, keyExpr);

            source.AppendFront(COMP_HELPER.SetResultVar(tgtAttrIndexedVar, value));

            if(tgtAttrIndexedVar.DestVar.Type == "")
            {
                source.AppendFrontFormat("GRGEN_LIBGR.ContainerHelper.AssignAttributeIndexed({0}, {1}, {2}, {3}, {4});\n",
                    element, key, value, "\"" + tgtAttrIndexedVar.AttributeName + "\"", "graph");
            }
            else
            {
                if(TypesHelper.GetGraphElementType(tgtAttrIndexedVar.DestVar.Type, model) != null)
                    EmitAttributeChangingEvent(tgtAttrIndexedVar.Id, AttributeChangeType.AssignElement, value, key, source);

                InheritanceType inheritanceType = TypesHelper.GetInheritanceType(tgtAttrIndexedVar.DestVar.Type, env.Model);
                AttributeType attributeType = inheritanceType.GetAttributeType(tgtAttrIndexedVar.AttributeName);
                string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

                if(ContainerType.StartsWith("array"))
                {
                    string array = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    source.AppendFrontFormat("if({0}.Count > (int){1})\n", array, key);
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFrontFormat("{0}[(int){1}] = {2};\n", array, key, sourceValueComputation);
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else if(ContainerType.StartsWith("deque"))
                {
                    string deque = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    source.AppendFrontFormat("if({0}.Count > (int){1})\n", deque, key);
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFrontFormat("{0}[(int){1}] = {2};\n", deque, key, sourceValueComputation);
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else
                {
                    string dictionary = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtAttrIndexedVar.DestVar.Type), model);
                    source.AppendFrontFormat("if({0}.ContainsKey(({1}){2}))\n", dictionary, dictSrcType, key);
                    source.AppendFront("{\n");
                    source.Indent();
                    source.AppendFrontFormat("{0}[({1}){2}] = {3};\n", dictionary, dictSrcType, key, value);
                    source.Unindent();
                    source.AppendFront("}\n");
                }

                if(TypesHelper.GetGraphElementType(tgtAttrIndexedVar.DestVar.Type, model) != null)
                    EmitAttributeChangedEvent(tgtAttrIndexedVar.Id, source);
            }
        }

        private void EmitAttributeAssignWithEventInitialization(AssignmentTargetAttributeIndexed tgtAttrIndexedVar, String element, String attrType,
            String value, String sourceValueComputation, SourceBuilder source)
        {
            source.AppendFrontFormat("GRGEN_LIBGR.IGraphElement {0} = (GRGEN_LIBGR.IGraphElement){1};\n",
                element, seqHelper.GetVar(tgtAttrIndexedVar.DestVar));
            source.AppendFrontFormat("GRGEN_LIBGR.AttributeType {0} = {1}.Type.GetAttributeType(\"{2}\");\n",
                attrType, element, tgtAttrIndexedVar.AttributeName);
            source.AppendFrontFormat("object {0} = {1};\n", value, sourceValueComputation);
        }

        private void EmitAttributeAssignInitialization(AssignmentTargetAttributeIndexed tgtAttrIndexedVar, String element, String attrType,
            String value, String sourceValueComputation, SourceBuilder source)
        {
            source.AppendFrontFormat("GRGEN_LIBGR.IBaseObject {0} = (GRGEN_LIBGR.IBaseObject){1};\n",
                element, seqHelper.GetVar(tgtAttrIndexedVar.DestVar));
            source.AppendFrontFormat("object {0} = {1};\n", value, sourceValueComputation);
        }

        #endregion Assignment (Assignments by target type)

        private void EmitAttributeChangingEvent(int id, AttributeChangeType attrChangeType,
            String newValue, String keyValue, SourceBuilder source)
        {
            string element = "elem_" + id;
            string attrType = "attrType_" + id;
            source.AppendFrontFormat("if({0} is GRGEN_LIBGR.INode)\n", element);
            source.AppendFrontIndentedFormat("graph.ChangingNodeAttribute((GRGEN_LIBGR.INode){0}, {1}, GRGEN_LIBGR.AttributeChangeType.{2}, {3}, {4});\n",
                element, attrType, attrChangeType.ToString(), newValue, keyValue);
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IEdge)\n", element);
            source.AppendFrontIndentedFormat("graph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge){0}, {1}, GRGEN_LIBGR.AttributeChangeType.{2}, {3}, {4});\n",
                element, attrType, attrChangeType.ToString(), newValue, keyValue);
            source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IObject)\n", element);
            source.AppendFrontIndentedFormat("graph.ChangingObjectAttribute((GRGEN_LIBGR.IObject){0}, {1}, GRGEN_LIBGR.AttributeChangeType.{2}, {3}, {4});\n",
                element, attrType, attrChangeType.ToString(), newValue, keyValue);
        }

        private void EmitAttributeChangedEvent(int id, SourceBuilder source)
        {
            string element = "elem_" + id;
            string attrType = "attrType_" + id;
            if(fireDebugEvents)
            {
                source.AppendFrontFormat("if({0} is GRGEN_LIBGR.INode)\n", element);
                source.AppendFrontIndentedFormat("graph.ChangedNodeAttribute((GRGEN_LIBGR.INode){0}, {1});\n",
                    element, attrType);
                source.AppendFrontFormat("else if({0} is GRGEN_LIBGR.IEdge)\n", element);
                source.AppendFrontIndentedFormat("graph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge){0}, {1});\n",
                    element, attrType);
            }
        }
    }
}
