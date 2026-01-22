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

using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    public class SequenceComputationRenderer
    {
        readonly IDebuggerEnvironment env;
        readonly SequenceExpressionRenderer seqExprRenderer;

        public SequenceComputationRenderer(IDebuggerEnvironment env, SequenceExpressionRenderer seqExprRenderer)
        {
            this.env = env;
            this.seqExprRenderer = seqExprRenderer;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GUI TODO: sub-sequence as AST instead of a string, switchable in the GUI
        internal void PrintSequenceComputation(SequenceComputation seqComp, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(seqComp.SequenceComputationType)
            {
            case SequenceComputationType.Then:
                PrintSequenceComputationThen((SequenceComputationThen)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VAlloc:
                PrintSequenceComputationVAlloc((SequenceComputationVAlloc)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VFree:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VFreeNonReset:
            case SequenceComputationType.VReset:
                PrintSequenceComputationVFree((SequenceComputationVFree)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerAdd:
                PrintSequenceComputationContainerAdd((SequenceComputationContainerAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerRem:
                PrintSequenceComputationContainerRem((SequenceComputationContainerRem)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ContainerClear:
                PrintSequenceComputationContainerClear((SequenceComputationContainerClear)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Assignment:
                PrintSequenceComputationAssignment((SequenceComputationAssignment)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.VariableDeclaration:
                PrintSequenceComputationVariableDeclaration((SequenceComputationVariableDeclaration)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Emit:
                PrintSequenceComputationEmit((SequenceComputationEmit)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Record:
                PrintSequenceComputationRecord((SequenceComputationRecord)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Export:
                PrintSequenceComputationExport((SequenceComputationExport)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.DeleteFile:
                PrintSequenceComputationDeleteFile((SequenceComputationDeleteFile)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphAdd:
                PrintSequenceComputationGraphAdd((SequenceComputationGraphAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRem:
                PrintSequenceComputationGraphRem((SequenceComputationGraphRem)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphClear:
                PrintSequenceComputationGraphClear((SequenceComputationGraphClear)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRetype:
                PrintSequenceComputationGraphRetype((SequenceComputationGraphRetype)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphAddCopy:
                PrintSequenceComputationGraphAddCopy((SequenceComputationGraphAddCopy)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphMerge:
                PrintSequenceComputationGraphMerge((SequenceComputationGraphMerge)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectSource:
                PrintSequenceComputationGraphRedirectSource((SequenceComputationGraphRedirectSource)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectTarget:
                PrintSequenceComputationGraphRedirectTarget((SequenceComputationGraphRedirectTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GraphRedirectSourceAndTarget:
                PrintSequenceComputationGraphRedirectSourceAndTarget((SequenceComputationGraphRedirectSourceAndTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Insert:
                PrintSequenceComputationInsert((SequenceComputationInsert)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertCopy:
                PrintSequenceComputationInsertCopy((SequenceComputationInsertCopy)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertInduced:
                PrintSequenceComputationInsertInduced((SequenceComputationInsertInduced)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.InsertDefined:
                PrintSequenceComputationInsertDefined((SequenceComputationInsertDefined)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ProcedureCall:
                PrintSequenceComputationProcedureCall((SequenceComputationProcedureCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.BuiltinProcedureCall:
                PrintSequenceComputationBuiltinProcedureCall((SequenceComputationBuiltinProcedureCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.ProcedureMethodCall:
                PrintSequenceComputationProcedureMethodCall((SequenceComputationProcedureMethodCall)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.DebugAdd:
            case SequenceComputationType.DebugRem:
            case SequenceComputationType.DebugEmit:
            case SequenceComputationType.DebugHalt:
            case SequenceComputationType.DebugHighlight:
                PrintSequenceComputationDebug((SequenceComputationDebug)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Assert:
                PrintSequenceComputationAssert((SequenceComputationAssert)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationEnter:
                PrintSequenceComputationSynchronizationEnter((SequenceComputationSynchronizationEnter)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationTryEnter:
                PrintSequenceComputationSynchronizationTryEnter((SequenceComputationSynchronizationTryEnter)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.SynchronizationExit:
                PrintSequenceComputationSynchronizationExit((SequenceComputationSynchronizationExit)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.GetEquivalentOrAdd:
                PrintSequenceComputationGetEquivalentOrAdd((SequenceComputationGetEquivalentOrAdd)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.AssignmentTarget: // every assignment target (lhs value) is a computation
                PrintSequenceAssignmentTarget((AssignmentTarget)seqComp, parent, highlightingMode);
                break;
            case SequenceComputationType.Expression: // every expression (rhs value) is a computation
                seqExprRenderer.PrintSequenceExpression((SequenceExpression)seqComp, parent, highlightingMode);
                break;
            default:
                Debug.Assert(false);
                break;
            }
        }

        private void PrintSequenceComputationThen(SequenceComputationThen seqCompThen, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceComputation(seqCompThen.left, seqCompThen, highlightingMode);
            env.PrintHighlighted("; ", highlightingMode);
            if(seqCompThen.right is SequenceExpression)
            {
                env.PrintHighlighted("{", highlightingMode);
                seqExprRenderer.PrintSequenceExpression((SequenceExpression)seqCompThen.right, seqCompThen, highlightingMode);
                env.PrintHighlighted("}", highlightingMode);
            }
            else
            {
                PrintSequenceComputation(seqCompThen.right, seqCompThen, highlightingMode);
            }
        }

        private void PrintSequenceComputationVAlloc(SequenceComputationVAlloc seqCompVAlloc, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("valloc()", highlightingMode);
        }

        private void PrintSequenceComputationVFree(SequenceComputationVFree seqCompVFree, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted((seqCompVFree.Reset ? "vfree" : "vfreenonreset") + "(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompVFree.VisitedFlagExpression, seqCompVFree, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationVReset(SequenceComputationVReset seqCompVReset, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("vreset(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompVReset.VisitedFlagExpression, seqCompVReset, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerAdd(SequenceComputationContainerAdd seqCompContainerAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerAdd.Name + ".add(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompContainerAdd.Expr, seqCompContainerAdd, highlightingMode);
            if(seqCompContainerAdd.ExprDst != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(seqCompContainerAdd.ExprDst, seqCompContainerAdd, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerRem(SequenceComputationContainerRem seqCompContainerRem, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerRem.Name + ".rem(", highlightingMode);
            if(seqCompContainerRem.Expr != null)
            {
                seqExprRenderer.PrintSequenceExpression(seqCompContainerRem.Expr, seqCompContainerRem, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationContainerClear(SequenceComputationContainerClear seqCompContainerClear, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompContainerClear.Name + ".clear()", highlightingMode);
        }

        private void PrintSequenceComputationAssignment(SequenceComputationAssignment seqCompAssign, SequenceBase parent, HighlightingMode highlightingMode)
        {
            PrintSequenceAssignmentTarget(seqCompAssign.Target, seqCompAssign, highlightingMode);
            env.PrintHighlighted("=", highlightingMode);
            PrintSequenceComputation(seqCompAssign.SourceValueProvider, seqCompAssign, highlightingMode);
        }

        private void PrintSequenceComputationVariableDeclaration(SequenceComputationVariableDeclaration seqCompVarDecl, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompVarDecl.Target.Name, highlightingMode);
        }

        private void PrintSequenceComputationDebug(SequenceComputationDebug seqCompDebug, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Debug::" + seqCompDebug.Name + "(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression seqExpr in seqCompDebug.ArgExprs)
            {
                if(!first)
                    env.PrintHighlighted(", ", highlightingMode);
                else
                    first = false;
                seqExprRenderer.PrintSequenceExpression(seqExpr, seqCompDebug, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationAssert(SequenceComputationAssert seqCompAssert, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompAssert.IsAlways)
                env.PrintHighlighted("assertAlways(", highlightingMode);
            else
                env.PrintHighlighted("assert(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression expr in seqCompAssert.ArgExprs)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
                else
                    seqExprRenderer.PrintSequenceExpression(expr, seqCompAssert, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationEnter(SequenceComputationSynchronizationEnter seqCompEnter, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::enter(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompEnter.LockObjectExpr, seqCompEnter, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationTryEnter(SequenceComputationSynchronizationTryEnter seqCompTryEnter, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::tryenter(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompTryEnter.LockObjectExpr, seqCompTryEnter, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationSynchronizationExit(SequenceComputationSynchronizationExit seqCompExit, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("Synchronization::exit(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompExit.LockObjectExpr, seqCompExit, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGetEquivalentOrAdd(SequenceComputationGetEquivalentOrAdd seqCompGetEquivalentOrAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("getEquivalentOrAdd(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGetEquivalentOrAdd.Subgraph, seqCompGetEquivalentOrAdd, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGetEquivalentOrAdd.SubgraphArray, seqCompGetEquivalentOrAdd, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationEmit(SequenceComputationEmit seqCompEmit, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompEmit.IsDebug)
                env.PrintHighlighted("emitdebug(", highlightingMode);
            else
                env.PrintHighlighted("emit(", highlightingMode);
            bool first = true;
            foreach(SequenceExpression expr in seqCompEmit.Expressions)
            {
                if(first)
                    first = false;
                else
                    env.PrintHighlighted(", ", highlightingMode);
                SequenceExpressionConstant exprConst = expr as SequenceExpressionConstant;
                if(exprConst != null && exprConst.Constant is string)
                    env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
                else
                    seqExprRenderer.PrintSequenceExpression(expr, seqCompEmit, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationRecord(SequenceComputationRecord seqCompRec, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("record(", highlightingMode);

            SequenceExpressionConstant exprConst = seqCompRec.Expression as SequenceExpressionConstant;
            if(exprConst != null && exprConst.Constant is string)
                env.PrintHighlighted(SequenceExpressionConstant.ConstantAsString(exprConst.Constant), highlightingMode);
            else
                seqExprRenderer.PrintSequenceExpression(seqCompRec.Expression, seqCompRec, highlightingMode);

            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationExport(SequenceComputationExport seqCompExport, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::export(", highlightingMode);
            if(seqCompExport.Graph != null)
            {
                seqExprRenderer.PrintSequenceExpression(seqCompExport.Graph, seqCompExport, highlightingMode);
                env.PrintHighlighted(", ", highlightingMode);
            }
            seqExprRenderer.PrintSequenceExpression(seqCompExport.Name, seqCompExport, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationDeleteFile(SequenceComputationDeleteFile seqCompDelFile, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("File::deleteFile(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompDelFile.Name, seqCompDelFile, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphAdd(SequenceComputationGraphAdd seqCompGraphAdd, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("add(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphAdd.Expr, seqCompGraphAdd, highlightingMode);
            if(seqCompGraphAdd.ExprSrc != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(seqCompGraphAdd.ExprSrc, seqCompGraphAdd, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(seqCompGraphAdd.ExprDst, seqCompGraphAdd, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRem(SequenceComputationGraphRem seqCompGraphRem, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("rem(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRem.Expr, seqCompGraphRem, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphClear(SequenceComputationGraphClear seqCompGraphClear, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("clear()", highlightingMode);
        }

        private void PrintSequenceComputationGraphRetype(SequenceComputationGraphRetype seqCompGraphRetype, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("retype(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRetype.ElemExpr, seqCompGraphRetype, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRetype.TypeExpr, seqCompGraphRetype, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphAddCopy(SequenceComputationGraphAddCopy seqCompGraphAddCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(seqCompGraphAddCopy.Deep ? "addCopy(" : "addClone(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphAddCopy.Expr, seqCompGraphAddCopy, highlightingMode);
            if(seqCompGraphAddCopy.ExprSrc != null)
            {
                env.PrintHighlighted(",", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(seqCompGraphAddCopy.ExprSrc, seqCompGraphAddCopy, highlightingMode);
                env.PrintHighlighted(",", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(seqCompGraphAddCopy.ExprDst, seqCompGraphAddCopy, highlightingMode);
            }
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphMerge(SequenceComputationGraphMerge seqCompGraphMerge, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("merge(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphMerge.TargetNodeExpr, seqCompGraphMerge, highlightingMode);
            env.PrintHighlighted(", ", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphMerge.SourceNodeExpr, seqCompGraphMerge, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectSource(SequenceComputationGraphRedirectSource seqCompGraphRedirectSrc, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSource(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectSrc.EdgeExpr, seqCompGraphRedirectSrc, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectSrc.SourceNodeExpr, seqCompGraphRedirectSrc, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectTarget(SequenceComputationGraphRedirectTarget seqCompGraphRedirectTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectTgt.EdgeExpr, seqCompGraphRedirectTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectTgt.TargetNodeExpr, seqCompGraphRedirectTgt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationGraphRedirectSourceAndTarget(SequenceComputationGraphRedirectSourceAndTarget seqCompGraphRedirectSrcTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("redirectSourceAndTarget(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectSrcTgt.EdgeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectSrcTgt.SourceNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompGraphRedirectSrcTgt.TargetNodeExpr, seqCompGraphRedirectSrcTgt, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsert(SequenceComputationInsert seqCompInsert, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insert(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsert.Graph, seqCompInsert, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertCopy(SequenceComputationInsertCopy seqCompInsertCopy, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insert(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertCopy.Graph, seqCompInsertCopy, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertCopy.RootNode, seqCompInsertCopy, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertInduced(SequenceComputationInsertInduced seqCompInsertInduced, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insertInduced(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertInduced.NodeSet, seqCompInsertInduced, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertInduced.RootNode, seqCompInsertInduced, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationInsertDefined(SequenceComputationInsertDefined seqCompInsertDefined, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted("insertDefined(", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertDefined.EdgeSet, seqCompInsertDefined, highlightingMode);
            env.PrintHighlighted(",", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(seqCompInsertDefined.RootEdge, seqCompInsertDefined, highlightingMode);
            env.PrintHighlighted(")", highlightingMode);
        }

        private void PrintSequenceComputationBuiltinProcedureCall(SequenceComputationBuiltinProcedureCall seqCompBuiltinProcCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompBuiltinProcCall.ReturnVars.Count > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompBuiltinProcCall.ReturnVars.Count; ++i)
                {
                    env.PrintHighlighted(seqCompBuiltinProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompBuiltinProcCall.ReturnVars.Count - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            PrintSequenceComputation(seqCompBuiltinProcCall.BuiltinProcedure, seqCompBuiltinProcCall, highlightingMode);
        }

        private void PrintSequenceComputationProcedureCall(SequenceComputationProcedureCall seqCompProcCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(seqCompProcCall.ReturnVars.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ReturnVars.Length; ++i)
                {
                    env.PrintHighlighted(seqCompProcCall.ReturnVars[i].Name, highlightingMode);
                    if(i != seqCompProcCall.ReturnVars.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            env.PrintHighlighted(seqCompProcCall.Name, highlightingMode);
            if(seqCompProcCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < seqCompProcCall.ArgumentExpressions.Length; ++i)
                {
                    seqExprRenderer.PrintSequenceExpression(seqCompProcCall.ArgumentExpressions[i], seqCompProcCall, highlightingMode);
                    if(i != seqCompProcCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        private void PrintSequenceComputationProcedureMethodCall(SequenceComputationProcedureMethodCall sequenceComputationProcedureMethodCall, SequenceBase parent, HighlightingMode highlightingMode)
        {
            if(sequenceComputationProcedureMethodCall.ReturnVars.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ReturnVars.Length; ++i)
                {
                    env.PrintHighlighted(sequenceComputationProcedureMethodCall.ReturnVars[i].Name, highlightingMode);
                    if(i != sequenceComputationProcedureMethodCall.ReturnVars.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")=", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetExpr != null)
            {
                seqExprRenderer.PrintSequenceExpression(sequenceComputationProcedureMethodCall.TargetExpr, sequenceComputationProcedureMethodCall, highlightingMode);
                env.PrintHighlighted(".", highlightingMode);
            }
            if(sequenceComputationProcedureMethodCall.TargetVar != null)
                env.PrintHighlighted(sequenceComputationProcedureMethodCall.TargetVar.ToString() + ".", highlightingMode);
            env.PrintHighlighted(sequenceComputationProcedureMethodCall.Name, highlightingMode);
            if(sequenceComputationProcedureMethodCall.ArgumentExpressions.Length > 0)
            {
                env.PrintHighlighted("(", highlightingMode);
                for(int i = 0; i < sequenceComputationProcedureMethodCall.ArgumentExpressions.Length; ++i)
                {
                    seqExprRenderer.PrintSequenceExpression(sequenceComputationProcedureMethodCall.ArgumentExpressions[i], sequenceComputationProcedureMethodCall, highlightingMode);
                    if(i != sequenceComputationProcedureMethodCall.ArgumentExpressions.Length - 1)
                        env.PrintHighlighted(",", highlightingMode);
                }
                env.PrintHighlighted(")", highlightingMode);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void PrintSequenceAssignmentTarget(AssignmentTarget assTgt, SequenceBase parent, HighlightingMode highlightingMode)
        {
            switch(assTgt.AssignmentTargetType)
            {
            case AssignmentTargetType.Var:
                PrintSequenceAssignmentTargetVar((AssignmentTargetVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.YieldingToVar:
                PrintSequenceAssignmentTargetYieldingVar((AssignmentTargetYieldingVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.IndexedVar:
                PrintSequenceAssignmentTargetIndexedVar((AssignmentTargetIndexedVar)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.Attribute:
                PrintSequenceAssignmentTargetAttribute((AssignmentTargetAttribute)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.AttributeIndexed:
                PrintSequenceAssignmentTargetAttributeIndexed((AssignmentTargetAttributeIndexed)assTgt, parent, highlightingMode);
                break;
            case AssignmentTargetType.Visited:
                PrintSequenceAssignmentTargetVisited((AssignmentTargetVisited)assTgt, parent, highlightingMode);
                break;
            }
        }

        private void PrintSequenceAssignmentTargetVar(AssignmentTargetVar assTgtVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtVar.DestVar.Name, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetYieldingVar(AssignmentTargetYieldingVar assTgtYieldingVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtYieldingVar.DestVar.Name, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetIndexedVar(AssignmentTargetIndexedVar assTgtIndexedVar, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtIndexedVar.DestVar.Name + "[", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(assTgtIndexedVar.KeyExpression, assTgtIndexedVar, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceAssignmentTargetAttribute(AssignmentTargetAttribute assTgtAttribute, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtAttribute.DestVar.Name + "." + assTgtAttribute.AttributeName, highlightingMode);
        }

        private void PrintSequenceAssignmentTargetAttributeIndexed(AssignmentTargetAttributeIndexed assTgtAttributeIndexed, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtAttributeIndexed.DestVar.Name + "." + assTgtAttributeIndexed.AttributeName + "[", highlightingMode);
            seqExprRenderer.PrintSequenceExpression(assTgtAttributeIndexed.KeyExpression, assTgtAttributeIndexed, highlightingMode);
            env.PrintHighlighted("]", highlightingMode);
        }

        private void PrintSequenceAssignmentTargetVisited(AssignmentTargetVisited assTgtVisited, SequenceBase parent, HighlightingMode highlightingMode)
        {
            env.PrintHighlighted(assTgtVisited.GraphElementVar.Name + ".visited", highlightingMode);
            if(assTgtVisited.VisitedFlagExpression != null)
            {
                env.PrintHighlighted("[", highlightingMode);
                seqExprRenderer.PrintSequenceExpression(assTgtVisited.VisitedFlagExpression, assTgtVisited, highlightingMode);
                env.PrintHighlighted("]", highlightingMode);
            }
        }
    }
}
