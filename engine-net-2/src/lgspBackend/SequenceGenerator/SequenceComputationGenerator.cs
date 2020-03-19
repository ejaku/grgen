/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
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
            if(seqVFree.Reset)
                source.AppendFront("graph.FreeVisitedFlag((int)" + exprGen.GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
            else
                source.AppendFront("graph.FreeVisitedFlagNonReset((int)" + exprGen.GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqVFree, "null"));
        }

        public void EmitSequenceComputationVReset(SequenceComputationVReset seqVReset, SourceBuilder source)
        {
            source.AppendFront("graph.ResetVisitedFlag((int)" + exprGen.GetSequenceExpression(seqVReset.VisitedFlagExpression, source) + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqVReset, "null"));
        }

        public void EmitSequenceComputationDebugAdd(SequenceComputationDebugAdd seqDebug, SourceBuilder source)
        {
            source.AppendFront("procEnv.DebugEntering(");
            for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
            {
                if(i == 0)
                    source.Append("(string)");
                else
                    source.Append(", ");
                source.Append(exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source));
            }
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugRem(SequenceComputationDebugRem seqDebug, SourceBuilder source)
        {
            source.AppendFront("procEnv.DebugExiting(");
            for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
            {
                if(i == 0)
                    source.Append("(string)");
                else
                    source.Append(", ");
                source.Append(exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source));
            }
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugEmit(SequenceComputationDebugEmit seqDebug, SourceBuilder source)
        {
            source.AppendFront("procEnv.DebugEmitting(");
            for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
            {
                if(i == 0)
                    source.Append("(string)");
                else
                    source.Append(", ");
                source.Append(exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source));
            }
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugHalt(SequenceComputationDebugHalt seqDebug, SourceBuilder source)
        {
            source.AppendFront("procEnv.DebugHalting(");
            for(int i = 0; i < seqDebug.ArgExprs.Count; ++i)
            {
                if(i == 0)
                    source.Append("(string)");
                else
                    source.Append(", ");
                source.Append(exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source));
            }
            source.Append(");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
        }

        public void EmitSequenceComputationDebugHighlight(SequenceComputationDebugHighlight seqDebug, SourceBuilder source)
        {
            source.AppendFront("List<object> values = new List<object>();\n");
            source.AppendFront("List<string> annotations = new List<string>();\n");
            for(int i = 1; i < seqDebug.ArgExprs.Count; ++i)
            {
                if(i % 2 == 1)
                    source.AppendFront("values.Add(" + exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source) + ");\n");
                else
                    source.AppendFront("annotations.Add((string)" + exprGen.GetSequenceExpression(seqDebug.ArgExprs[i], source) + ");\n");
            }
            source.AppendFront("procEnv.DebugHighlighting(" + exprGen.GetSequenceExpression(seqDebug.ArgExprs[0], source) + ", values, annotations);\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDebug, "null"));
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
                        source.AppendFront("object " + emitVal + ";\n");
                        declarationEmitted = true;
                    }
                    source.AppendFront(emitVal + " = " + exprGen.GetSequenceExpression(seqEmit.Expressions[i], source) + ";\n");
                    if(seqEmit.Expressions[i].Type(env) == ""
                        || seqEmit.Expressions[i].Type(env).StartsWith("set<") || seqEmit.Expressions[i].Type(env).StartsWith("map<")
                        || seqEmit.Expressions[i].Type(env).StartsWith("array<") || seqEmit.Expressions[i].Type(env).StartsWith("deque<"))
                    {
                        source.AppendFront("if(" + emitVal + " is IDictionary)\n");
                        source.AppendFront("\tprocEnv." + emitWriter + ".Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary)" + emitVal + ", graph));\n");
                        source.AppendFront("else if(" + emitVal + " is IList)\n");
                        source.AppendFront("\tprocEnv." + emitWriter + ".Write(GRGEN_LIBGR.EmitHelper.ToString((IList)" + emitVal + ", graph));\n");
                        source.AppendFront("else if(" + emitVal + " is GRGEN_LIBGR.IDeque)\n");
                        source.AppendFront("\tprocEnv." + emitWriter + ".Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque)" + emitVal + ", graph));\n");
                        source.AppendFront("else\n\t");
                    }
                    source.AppendFront("procEnv." + emitWriter + ".Write(GRGEN_LIBGR.EmitHelper.ToString(" + emitVal + ", graph));\n");
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
                        source.AppendFront("procEnv." + emitWriter + ".Write(\"" + text + "\");\n");
                    }
                    else
                        source.AppendFront("procEnv." + emitWriter + ".Write(GRGEN_LIBGR.EmitHelper.ToString(" + exprGen.GetSequenceExpression(seqEmit.Expressions[i], source) + ", graph));\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqEmit, "null"));
        }

        public void EmitSequenceComputationRecord(SequenceComputationRecord seqRec, SourceBuilder source)
        {
            if(!(seqRec.Expression is SequenceExpressionConstant))
            {
                string recVal = "recval_" + seqRec.Id;
                source.AppendFront("object " + recVal + " = " + exprGen.GetSequenceExpression(seqRec.Expression, source) + ";\n");
                if(seqRec.Expression.Type(env) == ""
                    || seqRec.Expression.Type(env).StartsWith("set<") || seqRec.Expression.Type(env).StartsWith("map<")
                    || seqRec.Expression.Type(env).StartsWith("array<") || seqRec.Expression.Type(env).StartsWith("deque<"))
                {
                    source.AppendFront("if(" + recVal + " is IDictionary)\n");
                    source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IDictionary)" + recVal + ", graph));\n");
                    source.AppendFront("else if(" + recVal + " is IList)\n");
                    source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((IList)" + recVal + ", graph));\n");
                    source.AppendFront("else if(" + recVal + " is GRGEN_LIBGR.IDeque)\n");
                    source.AppendFront("\tprocEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString((GRGEN_LIBGR.IDeque)" + recVal + ", graph));\n");
                    source.AppendFront("else\n\t");
                }
                source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString(" + recVal + ", graph));\n");
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
                    source.AppendFront("procEnv.Recorder.Write(\"" + text + "\");\n");
                }
                else
                    source.AppendFront("procEnv.Recorder.Write(GRGEN_LIBGR.EmitHelper.ToString(" + exprGen.GetSequenceExpression(seqRec.Expression, source) + ", graph));\n");
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqRec, "null"));
        }

        public void EmitSequenceComputationExport(SequenceComputationExport seqExp, SourceBuilder source)
        {
            string expFileName = "expfilename_" + seqExp.Id;
            source.AppendFront("object " + expFileName + " = " + exprGen.GetSequenceExpression(seqExp.Name, source) + ";\n");
            string expArguments = "exparguments_" + seqExp.Id;
            source.AppendFront("List<string> " + expArguments + " = new List<string>();\n");
            source.AppendFront(expArguments + ".Add(" + expFileName + ".ToString());\n");
            string expGraph = "expgraph_" + seqExp.Id;
            if(seqExp.Graph != null)
                source.AppendFront("GRGEN_LIBGR.IGraph " + expGraph + " = (GRGEN_LIBGR.IGraph)" + exprGen.GetSequenceExpression(seqExp.Graph, source) + ";\n");
            else
                source.AppendFront("GRGEN_LIBGR.IGraph " + expGraph + " = graph;\n");
            source.AppendFront("if(" + expGraph + " is GRGEN_LIBGR.INamedGraph)\n");
            source.AppendFront("\tGRGEN_LIBGR.Porter.Export((GRGEN_LIBGR.INamedGraph)" + expGraph + ", " + expArguments + ");\n");
            source.AppendFront("else\n");
            source.AppendFront("\tGRGEN_LIBGR.Porter.Export(" + expGraph + ", " + expArguments + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqExp, "null"));
        }

        public void EmitSequenceComputationDeleteFile(SequenceComputationDeleteFile seqDelFile, SourceBuilder source)
        {
            string delFileName = "delfilename_" + seqDelFile.Id;
            source.AppendFront("object " + delFileName + " = " + exprGen.GetSequenceExpression(seqDelFile.Name, source) + ";\n");
            source.AppendFront("\tSystem.IO.File.Delete((string)" + delFileName + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDelFile, "null"));
        }

        public void EmitSequenceComputationGraphAdd(SequenceComputationGraphAdd seqAdd, SourceBuilder source)
        {
            if(seqAdd.ExprSrc == null)
            {
                string typeExpr = exprGen.GetSequenceExpression(seqAdd.Expr, source);
                source.Append("GRGEN_LIBGR.GraphHelper.AddNodeOfType(" + typeExpr + ", graph)");
            }
            else
            {
                string typeExpr = exprGen.GetSequenceExpression(seqAdd.Expr, source);
                string srcExpr = exprGen.GetSequenceExpression(seqAdd.ExprSrc, source);
                string tgtExpr = exprGen.GetSequenceExpression(seqAdd.ExprDst, source);
                source.Append("GRGEN_LIBGR.GraphHelper.AddEdgeOfType(" + typeExpr + ", (GRGEN_LIBGR.INode)" + srcExpr + ", (GRGEN_LIBGR.INode)" + tgtExpr + ", graph)");
            }
        }

        public void EmitSequenceComputationGraphRem(SequenceComputationGraphRem seqRem, SourceBuilder source)
        {
            string remVal = "remval_" + seqRem.Id;
            string seqRemExpr = exprGen.GetSequenceExpression(seqRem.Expr, source);
            if(seqRem.Expr.Type(env) == "")
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement " + remVal + " = (GRGEN_LIBGR.IGraphElement)" + seqRemExpr + ";\n");
                source.AppendFront("if(" + remVal + " is GRGEN_LIBGR.IEdge)\n");
                source.AppendFront("\tgraph.Remove((GRGEN_LIBGR.IEdge)" + remVal + ");\n");
                source.AppendFront("else\n");
                source.AppendFront("\t{graph.RemoveEdges((GRGEN_LIBGR.INode)" + remVal + "); graph.Remove((GRGEN_LIBGR.INode)" + remVal + ");}\n");
            }
            else
            {
                if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "Node", model))
                {
                    source.AppendFront("GRGEN_LIBGR.INode " + remVal + " = (GRGEN_LIBGR.INode)" + seqRemExpr + ";\n");
                    source.AppendFront("graph.RemoveEdges(" + remVal + "); graph.Remove(" + remVal + ");\n");
                }
                else if(TypesHelper.IsSameOrSubtype(seqRem.Expr.Type(env), "AEdge", model))
                {
                    source.AppendFront("GRGEN_LIBGR.IEdge " + remVal + " = (GRGEN_LIBGR.IEdge)" + seqRemExpr + ";\n");
                    source.AppendFront("\tgraph.Remove(" + remVal + ");\n");
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
            source.Append("GRGEN_LIBGR.GraphHelper.RetypeGraphElement((GRGEN_LIBGR.IGraphElement)" + elemExpr + ", " + typeExpr + ", graph)");
        }

        public void EmitSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqAddCopy, SourceBuilder source)
        {
            if(seqAddCopy.ExprSrc == null)
            {
                string nodeExpr = exprGen.GetSequenceExpression(seqAddCopy.Expr, source);
                source.Append("GRGEN_LIBGR.GraphHelper.AddCopyOfNode(" + nodeExpr + ", graph)");
            }
            else
            {
                string edgeExpr = exprGen.GetSequenceExpression(seqAddCopy.Expr, source);
                string srcExpr = exprGen.GetSequenceExpression(seqAddCopy.ExprSrc, source);
                string tgtExpr = exprGen.GetSequenceExpression(seqAddCopy.ExprDst, source);
                source.Append("GRGEN_LIBGR.GraphHelper.AddCopyOfEdge(" + edgeExpr + ", (GRGEN_LIBGR.INode)" + srcExpr + ", (GRGEN_LIBGR.INode)" + tgtExpr + ", graph)");
            }
        }

        public void EmitSequenceComputationGraphMerge(SequenceComputationGraphMerge seqMrg, SourceBuilder source)
        {
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqMrg.TargetNodeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqMrg.SourceNodeExpr, source);
            source.AppendFrontFormat("graph.Merge((GRGEN_LIBGR.INode){0}, (GRGEN_LIBGR.INode){1}, \"merge\");\n", tgtNodeExpr, srcNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqMrg, "null"));
        }

        public void EmitSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectSource((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old source\");\n", edgeExpr, srcNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqRedir, "null"));
        }

        public void EmitSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old target\");\n", edgeExpr, tgtNodeExpr);
            source.AppendFront(COMP_HELPER.SetResultVar(seqRedir, "null"));
        }

        public void EmitSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqRedir, SourceBuilder source)
        {
            string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
            string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
            string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
            source.AppendFrontFormat("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, (GRGEN_LIBGR.INode){2}, \"old source\", \"old target\");\n", edgeExpr, srcNodeExpr, tgtNodeExpr);
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
            source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph){0}, (GRGEN_LIBGR.INode){1}, graph)", graphExpr, rootNodeExpr);
        }

        public void EmitSequenceComputationInsertInduced(SequenceComputationInsertInduced seqInsInd, SourceBuilder source)
        {
            source.Append("GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsInd.NodeSet, source) + ", (GRGEN_LIBGR.INode)" + exprGen.GetSequenceExpression(seqInsInd.RootNode, source) + ", graph)");
        }

        public void EmitSequenceComputationInsertDefined(SequenceComputationInsertDefined seqInsDef, SourceBuilder source)
        {
            if(seqInsDef.EdgeSet.Type(env) == "set<Edge>")
                source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IDEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
            else if(seqInsDef.EdgeSet.Type(env) == "set<UEdge>")
                source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IUEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
            else if(seqInsDef.EdgeSet.Type(env) == "set<AEdge>")
                source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
            else
                source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
        }

        public void EmitSequenceComputationExpression(SequenceExpression seqExpr, SourceBuilder source)
        {
            source.AppendFront(COMP_HELPER.SetResultVar(seqExpr, exprGen.GetSequenceExpression(seqExpr, source)));
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

            if(seqCall.IsExternal)
                source.AppendFront("GRGEN_EXPR.ExternalProcedures.");
            else
                source.AppendFrontFormat("GRGEN_ACTIONS.{0}Procedures.", TypesHelper.GetPackagePrefixDot(seqCall.Package));
            source.Append(seqCall.Name);
            source.Append("(procEnv, graph");
            source.Append(seqHelper.BuildParameters(seqCall, seqCall.ArgumentExpressions));
            source.Append(returnArguments);
            source.Append(");\n");

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
                source.AppendFront("object[] " + tmpVarName + " = ");
                source.Append("((GRGEN_LIBGR.IGraphElement)");
                if(seqCall.TargetExpr != null)
                    source.Append(exprGen.GetSequenceExpression(seqCall.TargetExpr, source));
                else
                    source.Append(seqHelper.GetVar(seqCall.TargetVar));
                source.Append(").ApplyProcedureMethod(procEnv, graph, ");
                source.Append("\"" + seqCall.Name + "\"");
                source.Append(seqHelper.BuildParametersInObject(seqCall, seqCall.ArgumentExpressions));
                source.Append(");\n");
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
                seqHelper.BuildReturnParameters(seqCall, seqCall.ReturnVars, TypesHelper.GetNodeOrEdgeType(type, model), out returnParameterDeclarations, out returnArguments, out returnAssignments);

                if(returnParameterDeclarations.Length != 0)
                    source.AppendFront(returnParameterDeclarations + "\n");

                source.AppendFront("((");
                source.Append(TypesHelper.XgrsTypeToCSharpType(type, model));
                source.Append(")");
                if(seqCall.TargetExpr != null)
                    source.Append(exprGen.GetSequenceExpression(seqCall.TargetExpr, source));
                else
                    source.Append(seqHelper.GetVar(seqCall.TargetVar));
                source.Append(").");
                source.Append(seqCall.Name);
                source.Append("(procEnv, graph");
                source.Append(seqHelper.BuildParameters(seqCall, seqCall.ArgumentExpressions, TypesHelper.GetNodeOrEdgeType(type, model).GetProcedureMethod(seqCall.Name)));
                source.Append(returnArguments);
                source.Append(");\n");
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
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqAdd.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
            }
            string containerVar = "tmp_eval_once_" + seqAdd.Id;
            source.AppendFront("object " + containerVar + " = " + container + ";\n");
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFront("object " + sourceValue + " = " + exprGen.GetSequenceExpression(seqAdd.Expr, source) + ";\n");
            string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
            source.AppendFront("if(" + containerVar + " is IList) {\n");
            source.Indent();

            if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't add non-int key to array\");\n");
            else
            {
                string array = "((System.Collections.IList)" + containerVar + ")";
                if(destinationValue != null)
                    source.AppendFront("int " + destinationValue + " = (int)" + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                if(seqAdd.Attribute != null)
                {
                    if(destinationValue != null)
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                    }
                    else
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                    }
                }
                if(destinationValue == null)
                    source.AppendFront(array + ".Add(" + sourceValue + ");\n");
                else
                    source.AppendFront(array + ".Insert(" + destinationValue + ", " + sourceValue + ");\n");
                if(seqAdd.Attribute != null)
                {
                    if(fireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    }
                }
            }

            source.Unindent();
            source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            if(destinationValue != null && !TypesHelper.IsSameOrSubtype(seqAdd.ExprDst.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't add non-int key to deque\");\n");
            else
            {
                string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                if(destinationValue != null)
                    source.AppendFront("int " + destinationValue + " = (int)" + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
                if(seqAdd.Attribute != null)
                {
                    if(destinationValue != null)
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                    }
                    else
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                    }
                }
                if(destinationValue == null)
                    source.AppendFront(deque + ".Enqueue(" + sourceValue + ");\n");
                else
                    source.AppendFront(deque + ".EnqueueAt(" + destinationValue + ", " + sourceValue + ");\n");
                if(seqAdd.Attribute != null)
                {
                    if(fireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    }
                }
            }

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            if(destinationValue != null)
                source.AppendFront("object " + destinationValue + " = " + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(seqAdd.Attribute != null)
            {
                if(seqAdd.ExprDst != null) // must be map
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                }
            }
            if(destinationValue == null)
                source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
            else
                source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");
            if(seqAdd.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                }
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, containerVar));
        }

        public void EmitSequenceComputationContainerAddArray(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string array = container;
            string arrayValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFront(arrayValueType + " " + sourceValue + " = (" + arrayValueType + ")" + exprGen.GetSequenceExpression(seqAdd.Expr, source) + ";\n");
            string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
            if(destinationValue != null)
                source.AppendFront("int " + destinationValue + " = (int)" + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
            if(seqAdd.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqAdd.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                if(destinationValue != null)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                }
            }
            if(destinationValue == null)
                source.AppendFront(array + ".Add(" + sourceValue + ");\n");
            else
                source.AppendFront(array + ".Insert(" + destinationValue + ", " + sourceValue + ");\n");
            if(seqAdd.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, container));
        }

        public void EmitSequenceComputationContainerAddDeque(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string deque = container;
            string dequeValueType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqAdd.Id;
            source.AppendFront(dequeValueType + " " + sourceValue + " = (" + dequeValueType + ")" + exprGen.GetSequenceExpression(seqAdd.Expr, source) + ";\n");
            string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
            if(destinationValue != null)
                source.AppendFront("int " + destinationValue + " = (int)" + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
            if(seqAdd.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqAdd.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                if(destinationValue != null)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", " + destinationValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                }
            }
            if(destinationValue == null)
                source.AppendFront(deque + ".Enqueue(" + sourceValue + ");\n");
            else
                source.AppendFront(deque + ".EnqueueAt(" + destinationValue + ", " + sourceValue + ");\n");
            if(seqAdd.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqAdd, container));
        }

        public void EmitSequenceComputationContainerAddSetMap(SequenceComputationContainerAdd seqAdd, String container, SourceBuilder source)
        {
            string dictionary = container;
            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqAdd.ContainerType(env)), model);
            string sourceValue = " srcval_" + seqAdd.Id;
            source.AppendFront(dictSrcType + " " + sourceValue + " = (" + dictSrcType + ")" + exprGen.GetSequenceExpression(seqAdd.Expr, source) + ";\n");
            string dictDstType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractDst(seqAdd.ContainerType(env)), model);
            string destinationValue = seqAdd.ExprDst == null ? null : "dstval_" + seqAdd.Id;
            if(destinationValue != null)
                source.AppendFront(dictDstType + " " + destinationValue + " = (" + dictDstType + ")" + exprGen.GetSequenceExpression(seqAdd.ExprDst, source) + ";\n");
            if(seqAdd.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqAdd.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqAdd.Id + " = elem_" + seqAdd.Id + ".Type.GetAttributeType(\"" + seqAdd.Attribute.AttributeName + "\");\n");
                if(destinationValue != null) // must be map
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + destinationValue + ", " + sourceValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ", GRGEN_LIBGR.AttributeChangeType.PutElement, " + sourceValue + ", null);\n");
                }
            }
            if(destinationValue == null)
                source.AppendFront(dictionary + "[" + sourceValue + "] = null;\n");
            else
                source.AppendFront(dictionary + "[" + sourceValue + "] = " + destinationValue + ";\n");
            if(seqAdd.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqAdd.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqAdd.Id + ", attrType_" + seqAdd.Id + ");\n");
                }
            }
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
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqDel.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
            }
            string containerVar = "tmp_eval_once_" + seqDel.Id;
            source.AppendFront("object " + containerVar + " = " + container + ";\n");
            string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
            source.AppendFront("if(" + containerVar + " is IList) {\n");
            source.Indent();

            if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't remove non-int index from array\");\n");
            else
            {
                string array = "((System.Collections.IList)" + containerVar + ")";
                if(sourceValue != null)
                    source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
                if(seqDel.Attribute != null)
                {
                    if(sourceValue != null)
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                    }
                    else
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                    }
                }
                if(sourceValue == null)
                    source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
                else
                    source.AppendFront(array + ".RemoveAt(" + sourceValue + ");\n");
                if(seqDel.Attribute != null)
                {
                    if(fireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    }
                }
            }

            source.Unindent();
            source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            if(sourceValue != null && !TypesHelper.IsSameOrSubtype(seqDel.Expr.Type(env), "int", model))
                source.AppendFront("throw new Exception(\"Can't remove non-int index from deque\");\n");
            else
            {
                string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
                if(sourceValue != null)
                    source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
                if(seqDel.Attribute != null)
                {
                    if(sourceValue != null)
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                    }
                    else
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                    }
                }
                if(sourceValue == null)
                    source.AppendFront(deque + ".Dequeue();\n");
                else
                    source.AppendFront(deque + ".DequeueAt(" + sourceValue + ");\n");
                if(seqDel.Attribute != null)
                {
                    if(fireDebugEvents)
                    {
                        source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                        source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                        source.AppendFront("else\n");
                        source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    }
                }
            }

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(sourceValue != null)
                source.AppendFront("object " + sourceValue + " = " + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
            if(seqDel.Attribute != null)
            {
                source.AppendFront("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType(attrType_" + seqDel.Id + ")) == \"SetValueType\")\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                source.AppendFront("else\n");
                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                source.AppendFront("else\n");
                source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            if(sourceValue == null)
                source.AppendFront("throw new Exception(\"" + seqDel.Container.PureName + ".rem() only possible on array or deque!\");\n");
            else
                source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");
            if(seqDel.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                }
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, containerVar));
        }

        public void EmitSequenceComputationContainerRemArray(SequenceComputationContainerRem seqDel, String container, SourceBuilder source)
        {
            string array = container;
            string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
            if(sourceValue != null)
                source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
            if(seqDel.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqDel.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                if(sourceValue != null)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                }
            }
            if(sourceValue == null)
                source.AppendFront(array + ".RemoveAt(" + array + ".Count - 1);\n");
            else
                source.AppendFront(array + ".RemoveAt(" + sourceValue + ");\n");
            if(seqDel.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, container));
        }

        public void EmitSequenceComputationContainerRemDeque(SequenceComputationContainerRem seqDel, string container, SourceBuilder source)
        {
            string deque = container;
            string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
            if(sourceValue != null)
                source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
            if(seqDel.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqDel.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                if(sourceValue != null)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, null);\n");
                }
            }
            if(sourceValue == null)
                source.AppendFront(deque + ".Dequeue();\n");
            else
                source.AppendFront(deque + ".DequeueAt(" + sourceValue + ");\n");
            if(seqDel.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqDel, container));
        }

        public void EmitSequenceComputationContainerRemSetMap(SequenceComputationContainerRem seqDel, string container, SourceBuilder source)
        {
            string dictionary = container;
            string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.ContainerType(env)), model);
            string sourceValue = "srcval_" + seqDel.Id;
            source.AppendFront(dictSrcType + " " + sourceValue + " = (" + dictSrcType + ")" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
            if(seqDel.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqDel.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqDel.Id + " = elem_" + seqDel.Id + ".Type.GetAttributeType(\"" + seqDel.Attribute.AttributeName + "\");\n");
                if(TypesHelper.ExtractDst(seqDel.ContainerType(env)) == "SetValueType")
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, " + sourceValue + ", null);\n");
                }
                else
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, " + sourceValue + ");\n");
                }
            }
            source.AppendFront(dictionary + ".Remove(" + sourceValue + ");\n");
            if(seqDel.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqDel.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqDel.Id + ", attrType_" + seqDel.Id + ");\n");
                }
            }
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
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqClear.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
            }
            string containerVar = "tmp_eval_once_" + seqClear.Id;
            source.AppendFront("object " + containerVar + " = " + container + ";\n");

            source.AppendFront("if(" + containerVar + " is IList) {\n");
            source.Indent();

            string array = "((System.Collections.IList)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                source.AppendFront("for(int i_" + seqClear.Id + " = " + array + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
            }
            source.AppendFront(array + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("\telse\n");
                    source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }

            source.Unindent();
            source.AppendFront("} else if(" + containerVar + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            string deque = "((GRGEN_LIBGR.IDeque)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                source.AppendFront("for(int i_" + seqClear.Id + " = " + deque + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
            }
            source.AppendFront(deque + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("\telse\n");
                    source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + containerVar + ")";
            if(seqClear.Attribute != null)
            {
                source.AppendFront("if(GRGEN_LIBGR.TypesHelper.ExtractDst(GRGEN_LIBGR.TypesHelper.AttributeTypeToXgrsType(attrType_" + seqClear.Id + ")) == \"SetValueType\")\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                source.Unindent();
                source.AppendFront("}\n");
                source.AppendFront("else\n");
                source.AppendFront("{\n");
                source.Indent();
                source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            source.AppendFront(dictionary + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("\telse\n");
                    source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }

            source.Unindent();
            source.AppendFront("}\n");
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, containerVar));
        }

        public void EmitSequenceComputationContainerClearArray(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string array = container;
            if(seqClear.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqClear.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                source.AppendFront("for(int i_" + seqClear.Id + " = " + array + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
            }
            source.AppendFront(array + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("\telse\n");
                    source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        public void EmitSequenceComputationContainerClearDeque(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string deque = container;
            if(seqClear.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqClear.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                source.AppendFront("for(int i_" + seqClear.Id + " = " + deque + ".Count; i_" + seqClear.Id + " >= 0; --i_" + seqClear.Id + ")\n");
                source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\t\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
                source.AppendFront("\telse\n");
                source.AppendFront("\t\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, i_" + seqClear.Id + ");\n");
            }
            source.AppendFront(deque + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("\tif(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\t\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("\telse\n");
                    source.AppendFront("\t\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        public void EmitSequenceComputationContainerClearSetMap(SequenceComputationContainerClear seqClear, String container, SourceBuilder source)
        {
            string dictionary = container;
            if(seqClear.Attribute != null)
            {
                source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(seqClear.Attribute.Source, source) + ";\n");
                source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + seqClear.Id + " = elem_" + seqClear.Id + ".Type.GetAttributeType(\"" + seqClear.Attribute.AttributeName + "\");\n");
                if(TypesHelper.ExtractDst(seqClear.ContainerType(env)) == "SetValueType")
                {
                    source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                    source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, kvp_" + seqClear.Id + ", null);\n");
                }
                else
                {
                    source.AppendFront("foreach(DictionaryEntry kvp_" + seqClear.Id + " in " + dictionary + ")\n");
                    source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ", GRGEN_LIBGR.AttributeChangeType.RemoveElement, null, kvp_" + seqClear.Id + ");\n");
                }
            }
            source.AppendFront(dictionary + ".Clear();\n");
            if(seqClear.Attribute != null)
            {
                if(fireDebugEvents)
                {
                    source.AppendFront("if(elem_" + seqClear.Id + " is GRGEN_LIBGR.INode)\n");
                    source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                    source.AppendFront("else\n");
                    source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + seqClear.Id + ", attrType_" + seqClear.Id + ");\n");
                }
            }
            source.AppendFront(COMP_HELPER.SetResultVar(seqClear, container));
        }

        private string GetContainerValue(SequenceComputationContainer container, SourceBuilder source)
        {
            if(container.Container != null)
                return seqHelper.GetVar(container.Container);
            else
                return "((GRGEN_LIBGR.IGraphElement)" + exprGen.GetSequenceExpression(container.Attribute.Source, source) + ")" + ".GetAttribute(\"" + container.Attribute.AttributeName + "\")";
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
            source.AppendFront("bool visval_" + tgtVisitedFlag.Id + " = (bool)" + sourceValueComputation + ";\n");
            source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)" + seqHelper.GetVar(tgtVisitedFlag.GraphElementVar)
                + ", (int)" + exprGen.GetSequenceExpression(tgtVisitedFlag.VisitedFlagExpression, source) + ", visval_" + tgtVisitedFlag.Id + ");\n");
            source.AppendFront(COMP_HELPER.SetResultVar(tgtVisitedFlag, "visval_" + tgtVisitedFlag.Id));
        }

        void EmitAssignmentIndexedVar(AssignmentTargetIndexedVar tgtIndexedVar, string sourceValueComputation, SourceBuilder source)
        {
            string container = "container_" + tgtIndexedVar.Id;
            source.AppendFront("object " + container + " = " + seqHelper.GetVar(tgtIndexedVar.DestVar) + ";\n");
            string key = "key_" + tgtIndexedVar.Id;
            source.AppendFront("object " + key + " = " + exprGen.GetSequenceExpression(tgtIndexedVar.KeyExpression, source) + ";\n");
            source.AppendFront(COMP_HELPER.SetResultVar(tgtIndexedVar, container)); // container is a reference, so we can assign it already here before the changes

            if(tgtIndexedVar.DestVar.Type == "")
            {
                EmitAssignmentIndexedVarUnknownType(tgtIndexedVar, sourceValueComputation,
                    container, key, source);
            }
            else if(tgtIndexedVar.DestVar.Type.StartsWith("array"))
            {
                string array = seqHelper.GetVar(tgtIndexedVar.DestVar);
                source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            else if(tgtIndexedVar.DestVar.Type.StartsWith("deque"))
            {
                string deque = seqHelper.GetVar(tgtIndexedVar.DestVar);
                source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
            else
            {
                string dictionary = seqHelper.GetVar(tgtIndexedVar.DestVar);
                string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtIndexedVar.DestVar.Type), model);
                source.AppendFront("if(" + dictionary + ".ContainsKey((" + dictSrcType + ")" + key + ")) {\n");
                source.Indent();
                source.AppendFront(dictionary + "[(" + dictSrcType + ")" + key + "] = " + sourceValueComputation + ";\n");
                source.Unindent();
                source.AppendFront("}\n");
            }
        }

        void EmitAssignmentIndexedVarUnknownType(AssignmentTargetIndexedVar tgtIndexedVar, string sourceValueComputation, string container, string key, SourceBuilder source)
        {
            source.AppendFront("if(" + container + " is IList) {\n");
            source.Indent();

            string array = "((System.Collections.IList)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true) {\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
            }
            else
            {
                source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else if(" + container + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            string deque = "((GRGEN_LIBGR.IDeque)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true) {\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in deque\");\n");
            }
            else
            {
                source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + container + ")";
            source.AppendFront("if(" + dictionary + ".Contains(" + key + ")) {\n");
            source.Indent();
            source.AppendFront(dictionary + "[" + key + "] = " + sourceValueComputation + ";\n");
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
            source.AppendFront("object value_" + tgtAttr.Id + " = " + sourceValueComputation + ";\n");
            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttr.Id + " = (GRGEN_LIBGR.IGraphElement)" + seqHelper.GetVar(tgtAttr.DestVar) + ";\n");
            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + tgtAttr.Id + ";\n");
            source.AppendFront("value_" + tgtAttr.Id + " = GRGEN_LIBGR.ContainerHelper.IfAttributeOfElementIsContainerThenCloneContainer(elem_" + tgtAttr.Id + ", \"" + tgtAttr.AttributeName + "\", value_" + tgtAttr.Id + ", out attrType_" + tgtAttr.Id + ");\n");
            source.AppendFront("if(elem_" + tgtAttr.Id + " is GRGEN_LIBGR.INode)\n");
            source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ", GRGEN_LIBGR.AttributeChangeType.Assign, value_" + tgtAttr.Id + ", null);\n");
            source.AppendFront("else\n");
            source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ", GRGEN_LIBGR.AttributeChangeType.Assign, value_" + tgtAttr.Id + ", null);\n");
            source.AppendFront("elem_" + tgtAttr.Id + ".SetAttribute(\"" + tgtAttr.AttributeName + "\", value_" + tgtAttr.Id + ");\n");
            if(fireDebugEvents)
            {
                source.AppendFront("if(elem_" + tgtAttr.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ");\n");
                source.AppendFront("else\n");
                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttr.Id + ", attrType_" + tgtAttr.Id + ");\n");
            }
            source.AppendFront(COMP_HELPER.SetResultVar(tgtAttr, "value_" + tgtAttr.Id));
        }

        void EmitAssignmentAttributeIndexed(AssignmentTargetAttributeIndexed tgtAttrIndexedVar, string sourceValueComputation, SourceBuilder source)
        {
            string value = "value_" + tgtAttrIndexedVar.Id;
            source.AppendFront("object " + value + " = " + sourceValueComputation + ";\n");
            source.AppendFront(COMP_HELPER.SetResultVar(tgtAttrIndexedVar, "value_" + tgtAttrIndexedVar.Id));
            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttrIndexedVar.Id + " = (GRGEN_LIBGR.IGraphElement)" + seqHelper.GetVar(tgtAttrIndexedVar.DestVar) + ";\n");
            string container = "container_" + tgtAttrIndexedVar.Id;
            source.AppendFront("object " + container + " = elem_" + tgtAttrIndexedVar.Id + ".GetAttribute(\"" + tgtAttrIndexedVar.AttributeName + "\");\n");
            string key = "key_" + tgtAttrIndexedVar.Id;
            source.AppendFront("object " + key + " = " + exprGen.GetSequenceExpression(tgtAttrIndexedVar.KeyExpression, source) + ";\n");

            source.AppendFront("GRGEN_LIBGR.AttributeType attrType_" + tgtAttrIndexedVar.Id + " = elem_" + tgtAttrIndexedVar.Id + ".Type.GetAttributeType(\"" + tgtAttrIndexedVar.AttributeName + "\");\n");
            source.AppendFront("if(elem_" + tgtAttrIndexedVar.Id + " is GRGEN_LIBGR.INode)\n");
            source.AppendFront("\tgraph.ChangingNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ", GRGEN_LIBGR.AttributeChangeType.AssignElement, " + value + ", " + key + ");\n");
            source.AppendFront("else\n");
            source.AppendFront("\tgraph.ChangingEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ", GRGEN_LIBGR.AttributeChangeType.AssignElement, " + value + ", " + key + ");\n");

            if(tgtAttrIndexedVar.DestVar.Type == "")
            {
                EmitAssignmentAttributeIndexedUnknownType(tgtAttrIndexedVar, sourceValueComputation,
                    value, container, key, source);
            }
            else
            {
                GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(tgtAttrIndexedVar.DestVar.Type, env.Model);
                AttributeType attributeType = nodeOrEdgeType.GetAttributeType(tgtAttrIndexedVar.AttributeName);
                string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

                if(ContainerType.StartsWith("array"))
                {
                    string array = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                    source.Indent();
                    source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else if(ContainerType.StartsWith("deque"))
                {
                    string deque = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                    source.Indent();
                    source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
                else
                {
                    string dictionary = seqHelper.GetVar(tgtAttrIndexedVar.DestVar);
                    string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtAttrIndexedVar.DestVar.Type), model);
                    source.AppendFront("if(" + dictionary + ".ContainsKey((" + dictSrcType + ")" + key + ")) {\n");
                    source.Indent();
                    source.AppendFront(dictionary + "[(" + dictSrcType + ")" + key + "] = " + value + ";\n");
                    source.Unindent();
                    source.AppendFront("}\n");
                }
            }

            if(fireDebugEvents)
            {
                source.AppendFront("if(elem_" + tgtAttrIndexedVar.Id + " is GRGEN_LIBGR.INode)\n");
                source.AppendFront("\tgraph.ChangedNodeAttribute((GRGEN_LIBGR.INode)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ");\n");
                source.AppendFront("else\n");
                source.AppendFront("\tgraph.ChangedEdgeAttribute((GRGEN_LIBGR.IEdge)elem_" + tgtAttrIndexedVar.Id + ", attrType_" + tgtAttrIndexedVar.Id + ");\n");
            }
        }

        void EmitAssignmentAttributeIndexedUnknownType(AssignmentTargetAttributeIndexed tgtAttrIndexedVar, string sourceValueComputation,
            string value, string container, string key, SourceBuilder source)
        {
            source.AppendFront("if(" + container + " is IList) {\n");
            source.Indent();

            string array = "((System.Collections.IList)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtAttrIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true) {\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in array\");\n");
            }
            else
            {
                source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(array + "[(int)" + key + "] = " + value + ";\n");
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else if(" + container + " is GRGEN_LIBGR.IDeque) {\n");
            source.Indent();

            string deque = "((GRGEN_LIBGR.IDeque)" + container + ")";
            if(!TypesHelper.IsSameOrSubtype(tgtAttrIndexedVar.KeyExpression.Type(env), "int", model))
            {
                source.AppendFront("if(true) {\n");
                source.Indent();
                source.AppendFront("throw new Exception(\"Can't access non-int index in deque\");\n");
            }
            else
            {
                source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                source.Indent();
                source.AppendFront(deque + "[(int)" + key + "] = " + value + ";\n");
            }
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("} else {\n");
            source.Indent();

            string dictionary = "((System.Collections.IDictionary)" + container + ")";
            source.AppendFront("if(" + dictionary + ".Contains(" + key + ")) {\n");
            source.Indent();
            source.AppendFront(dictionary + "[" + key + "] = " + value + ";\n");
            source.Unindent();
            source.AppendFront("}\n");

            source.Unindent();
            source.AppendFront("}\n");
        }

        #endregion Assignment (Assignments by target type)
    }
}
