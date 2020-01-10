/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence computation generator contains code to generate sequence computations,
    /// it is in use by the sequence generator, also supplying result variable handling to it.
    /// </summary>
    public class SequenceComputationGenerator
    {
        IGraphModel model;

        SequenceCheckingEnvironmentCompiled env;

        SequenceExpressionGenerator exprGen;

        SequenceGeneratorHelper helper;

        bool fireDebugEvents;


        public SequenceComputationGenerator(IGraphModel model, SequenceCheckingEnvironmentCompiled env, SequenceExpressionGenerator seqExprGen, SequenceGeneratorHelper helper, bool fireDebugEvents)
        {
            this.model = model;
            this.env = env;
            this.exprGen = seqExprGen;
            this.helper = helper;
            this.fireDebugEvents = fireDebugEvents;
        }

        /// <summary>
        /// Returns a string containing a C# expression to get the value of the result variable of the sequence or sequence computation construct
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string GetResultVar(SequenceBase seq)
        {
            return "res_" + seq.Id;
        }

        /// <summary>
        /// Returns a string containing a C# assignment to set the result variable of the sequence or sequence computation construct
        /// to the value as computed by the C# expression in the string given
        /// (every sequence part writes a success-value which is read by other parts determining execution flow)
        /// </summary>
        public string SetResultVar(SequenceBase seq, String valueToWrite)
        {
            if(seq is Sequence)
                return "res_" + seq.Id + " = (bool)(" + valueToWrite + ");\n";
            else
                return "res_" + seq.Id + " = " + valueToWrite + ";\n";
        }

        /// <summary>
        /// Returns a string containing a C# declaration of the result variable of the sequence (or sequence computation) construct
        /// </summary>
        public string DeclareResultVar(SequenceBase seq)
        {
            if(seq is Sequence)
                return "bool res_" + seq.Id + ";\n";
            else
                return "object res_" + seq.Id + ";\n";
        }

  		public void EmitSequenceComputation(SequenceComputation seqComp, SourceBuilder source)
		{
            // take care that the operations returning a value are emitted similarily to expressions,
            // whereas the operations returning no value are emitted as statements
            switch(seqComp.SequenceComputationType)
            {
                case SequenceComputationType.Then:
                {
                    SequenceComputationThen seqThen = (SequenceComputationThen)seqComp;
                    EmitSequenceComputation(seqThen.left, source);
                    EmitSequenceComputation(seqThen.right, source);
                    source.AppendFront(SetResultVar(seqThen, GetResultVar(seqThen.right)));
                    break;
                }
                
                case SequenceComputationType.Assignment:
                {
                    SequenceComputationAssignment seqAssign = (SequenceComputationAssignment)seqComp;
                    if(seqAssign.SourceValueProvider is SequenceComputationAssignment)
                    {
                        EmitSequenceComputation(seqAssign.SourceValueProvider, source);
                        EmitAssignment(seqAssign.Target, GetResultVar(seqAssign.SourceValueProvider), source);
                        source.AppendFront(SetResultVar(seqAssign, GetResultVar(seqAssign.Target)));
                    }
                    else
                    {
                        string comp = exprGen.GetSequenceExpression((SequenceExpression)seqAssign.SourceValueProvider, source);
                        EmitAssignment(seqAssign.Target, comp, source);
                        source.AppendFront(SetResultVar(seqAssign, GetResultVar(seqAssign.Target)));
                    }
                    break;
                }

                case SequenceComputationType.VariableDeclaration:
                {
                    SequenceComputationVariableDeclaration seqVarDecl = (SequenceComputationVariableDeclaration)seqComp;
                    source.AppendFront(helper.SetVar(seqVarDecl.Target, TypesHelper.DefaultValueString(seqVarDecl.Target.Type, model)));
                    source.AppendFront(SetResultVar(seqVarDecl, helper.GetVar(seqVarDecl.Target)));
                    break;
                }

                case SequenceComputationType.ContainerAdd:
                {
                    SequenceComputationContainerAdd seqAdd = (SequenceComputationContainerAdd)seqComp;

                    string container = GetContainerValue(seqAdd);

                    if(seqAdd.ContainerType(env) == "")
                    {
                        if(seqAdd.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqAdd.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqAdd, containerVar));
                    }
                    else if(seqAdd.ContainerType(env).StartsWith("array"))
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
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqAdd.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    else if(seqAdd.ContainerType(env).StartsWith("deque"))
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
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqAdd.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    else
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
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqAdd.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqAdd.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqAdd, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerRem:
                {
                    SequenceComputationContainerRem seqDel = (SequenceComputationContainerRem)seqComp;

                    string container = GetContainerValue(seqDel);

                    if(seqDel.ContainerType(env) == "")
                    {
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqDel.Attribute.SourceVar) + ";\n");
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
                            source.AppendFront("throw new Exception(\""+seqDel.Container.PureName+".rem() only possible on array or deque!\");\n");
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
                        source.AppendFront(SetResultVar(seqDel, containerVar));
                    }
                    else if(seqDel.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqDel.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    else if(seqDel.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        string sourceValue = seqDel.Expr == null ? null : "srcval_" + seqDel.Id;
                        if(sourceValue != null)
                            source.AppendFront("int " + sourceValue + " = (int)" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqDel.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    else
                    {
                        string dictionary = container;
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(seqDel.ContainerType(env)), model);
                        string sourceValue = "srcval_" + seqDel.Id;
                        source.AppendFront(dictSrcType + " " + sourceValue + " = (" + dictSrcType + ")" + exprGen.GetSequenceExpression(seqDel.Expr, source) + ";\n");
                        if(seqDel.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqDel.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqDel.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqDel, container));
                    }
                    break;
                }

                case SequenceComputationType.ContainerClear:
                {
                    SequenceComputationContainerClear seqClear = (SequenceComputationContainerClear)seqComp;

                    string container = GetContainerValue(seqClear);

                    if(seqClear.ContainerType(env) == "")
                    {
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqClear.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqClear, containerVar));
                    }
                    else if(seqClear.ContainerType(env).StartsWith("array"))
                    {
                        string array = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqClear.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    else if(seqClear.ContainerType(env).StartsWith("deque"))
                    {
                        string deque = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqClear.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    else
                    {
                        string dictionary = container;
                        if(seqClear.Attribute != null)
                        {
                            source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + seqClear.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(seqClear.Attribute.SourceVar) + ";\n");
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
                        source.AppendFront(SetResultVar(seqClear, container));
                    }
                    break;
                }

                case SequenceComputationType.VAlloc:
                    source.Append("graph.AllocateVisitedFlag()");
                    break;

                case SequenceComputationType.VFree:
                case SequenceComputationType.VFreeNonReset:
                {
                    SequenceComputationVFree seqVFree = (SequenceComputationVFree)seqComp;
                    if(seqVFree.Reset)
                        source.AppendFront("graph.FreeVisitedFlag((int)" + exprGen.GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
                    else
                        source.AppendFront("graph.FreeVisitedFlagNonReset((int)" + exprGen.GetSequenceExpression(seqVFree.VisitedFlagExpression, source) + ");\n");
                    source.AppendFront(SetResultVar(seqVFree, "null"));
                    break;
                }

                case SequenceComputationType.VReset:
                {
                    SequenceComputationVReset seqVReset = (SequenceComputationVReset)seqComp;
                    source.AppendFront("graph.ResetVisitedFlag((int)" + exprGen.GetSequenceExpression(seqVReset.VisitedFlagExpression, source) + ");\n");
                    source.AppendFront(SetResultVar(seqVReset, "null"));
                    break;
                }

                case SequenceComputationType.DebugAdd:
                {
                    SequenceComputationDebugAdd seqDebug = (SequenceComputationDebugAdd)seqComp;
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
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugRem:
                {
                    SequenceComputationDebugRem seqDebug = (SequenceComputationDebugRem)seqComp;
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
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugEmit:
                {
                    SequenceComputationDebugEmit seqDebug = (SequenceComputationDebugEmit)seqComp;
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
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugHalt:
                {
                    SequenceComputationDebugHalt seqDebug = (SequenceComputationDebugHalt)seqComp;
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
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.DebugHighlight:
                {
                    SequenceComputationDebugHighlight seqDebug = (SequenceComputationDebugHighlight)seqComp;
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
                    source.AppendFront(SetResultVar(seqDebug, "null"));
                    break;
                }

                case SequenceComputationType.Emit:
                {
                    SequenceComputationEmit seqEmit = (SequenceComputationEmit)seqComp;
                    bool declarationEmitted = false;
                    String emitWriter = seqEmit.IsDebug ? "EmitWriterDebug" : "EmitWriter";
                    for(int i = 0; i < seqEmit.Expressions.Count; ++i)
                    {
                        if(!(seqEmit.Expressions[i] is SequenceExpressionConstant))
                        {
                            string emitVal = "emitval_" + seqEmit.Id;
                            if(!declarationEmitted) {
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
                    source.AppendFront(SetResultVar(seqEmit, "null"));
                    break;
                }

                case SequenceComputationType.Record:
                {
                    SequenceComputationRecord seqRec = (SequenceComputationRecord)seqComp;
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
                    } else {
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
                    source.AppendFront(SetResultVar(seqRec, "null"));
                    break;
                }

                case SequenceComputationType.Export:
                {
                    SequenceComputationExport seqExp = (SequenceComputationExport)seqComp;
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
                    source.AppendFront(SetResultVar(seqExp, "null"));
                    break;
                }

                case SequenceComputationType.DeleteFile:
                {
                    SequenceComputationDeleteFile seqDelFile = (SequenceComputationDeleteFile)seqComp;
                    string delFileName = "delfilename_" + seqDelFile.Id;
                    source.AppendFront("object " + delFileName + " = " + exprGen.GetSequenceExpression(seqDelFile.Name, source) + ";\n");
                    source.AppendFront("\tSystem.IO.File.Delete((string)" + delFileName + ");\n");
                    source.AppendFront(SetResultVar(seqDelFile, "null"));
                    break;
                }

                case SequenceComputationType.GraphAdd:
                {
                    SequenceComputationGraphAdd seqAdd = (SequenceComputationGraphAdd)seqComp;
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
                    break;
                }
                
                case SequenceComputationType.GraphRem:
                {
                    SequenceComputationGraphRem seqRem = (SequenceComputationGraphRem)seqComp;
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
                    source.AppendFront(SetResultVar(seqRem, "null"));
                    break;
                }

                case SequenceComputationType.GraphClear:
                {
                    SequenceComputationGraphClear seqClr = (SequenceComputationGraphClear)seqComp;
                    source.AppendFront("graph.Clear();\n");
                    source.AppendFront(SetResultVar(seqClr, "null"));
                    break;
                }

                case SequenceComputationType.GraphRetype:
                {
                    SequenceComputationGraphRetype seqRetype = (SequenceComputationGraphRetype)seqComp;
                    string typeExpr = exprGen.GetSequenceExpression(seqRetype.TypeExpr, source);
                    string elemExpr = exprGen.GetSequenceExpression(seqRetype.ElemExpr, source);
                    source.Append("GRGEN_LIBGR.GraphHelper.RetypeGraphElement((GRGEN_LIBGR.IGraphElement)" + elemExpr + ", "  + typeExpr + ", graph)");
                    break;
                }

                case SequenceComputationType.GraphAddCopy:
                {
                    SequenceComputationGraphAddCopy seqAddCopy = (SequenceComputationGraphAddCopy)seqComp;
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
                    break;
                }

                case SequenceComputationType.GraphMerge:
                {
                    SequenceComputationGraphMerge seqMrg = (SequenceComputationGraphMerge)seqComp;
                    string tgtNodeExpr = exprGen.GetSequenceExpression(seqMrg.TargetNodeExpr, source);
                    string srcNodeExpr = exprGen.GetSequenceExpression(seqMrg.SourceNodeExpr, source);
                    source.AppendFrontFormat("graph.Merge((GRGEN_LIBGR.INode){0}, (GRGEN_LIBGR.INode){1}, \"merge\");\n", tgtNodeExpr, srcNodeExpr);
                    source.AppendFront(SetResultVar(seqMrg, "null"));
                    break;
                }
                
                case SequenceComputationType.GraphRedirectSource:
                {
                    SequenceComputationGraphRedirectSource seqRedir = (SequenceComputationGraphRedirectSource)seqComp;
                    string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectSource((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old source\");\n", edgeExpr, srcNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.GraphRedirectTarget:
                {
                    SequenceComputationGraphRedirectTarget seqRedir = (SequenceComputationGraphRedirectTarget)seqComp;
                    string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, \"old target\");\n", edgeExpr, tgtNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.GraphRedirectSourceAndTarget:
                {
                    SequenceComputationGraphRedirectSourceAndTarget seqRedir = (SequenceComputationGraphRedirectSourceAndTarget)seqComp;
                    string edgeExpr = exprGen.GetSequenceExpression(seqRedir.EdgeExpr, source);
                    string srcNodeExpr = exprGen.GetSequenceExpression(seqRedir.SourceNodeExpr, source);
                    string tgtNodeExpr = exprGen.GetSequenceExpression(seqRedir.TargetNodeExpr, source);
                    source.AppendFrontFormat("graph.RedirectSourceAndTarget((GRGEN_LIBGR.IEdge){0}, (GRGEN_LIBGR.INode){1}, (GRGEN_LIBGR.INode){2}, \"old source\", \"old target\");\n", edgeExpr, srcNodeExpr, tgtNodeExpr);
                    source.AppendFront(SetResultVar(seqRedir, "null"));
                    break;
                }

                case SequenceComputationType.Insert:
                {
                    SequenceComputationInsert seqIns = (SequenceComputationInsert)seqComp;
                    string graphExpr = exprGen.GetSequenceExpression(seqIns.Graph, source);
                    source.AppendFrontFormat("GRGEN_LIBGR.GraphHelper.Insert((GRGEN_LIBGR.IGraph){0}, graph);\n", graphExpr);
                    source.AppendFront(SetResultVar(seqIns, "null"));
                    break;
                }

                case SequenceComputationType.InsertCopy:
                {
                    SequenceComputationInsertCopy seqInsCopy = (SequenceComputationInsertCopy)seqComp;
                    string graphExpr = exprGen.GetSequenceExpression(seqInsCopy.Graph, source);
                    string rootNodeExpr = exprGen.GetSequenceExpression(seqInsCopy.RootNode, source);
                    source.AppendFormat("GRGEN_LIBGR.GraphHelper.InsertCopy((GRGEN_LIBGR.IGraph){0}, (GRGEN_LIBGR.INode){1}, graph)", graphExpr, rootNodeExpr);
                    break;
                }

                case SequenceComputationType.InsertInduced:
                {
                    SequenceComputationInsertInduced seqInsInd = (SequenceComputationInsertInduced)seqComp;
                    source.Append("GRGEN_LIBGR.GraphHelper.InsertInduced((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsInd.NodeSet, source) + ", (GRGEN_LIBGR.INode)" + exprGen.GetSequenceExpression(seqInsInd.RootNode, source) + ", graph)");
                    break;
                }

                case SequenceComputationType.InsertDefined:
                {
                    SequenceComputationInsertDefined seqInsDef = (SequenceComputationInsertDefined)seqComp;
                    if(seqInsDef.EdgeSet.Type(env)=="set<Edge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedDirected((IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IDEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else if (seqInsDef.EdgeSet.Type(env) == "set<UEdge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefinedUndirected((IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IUEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else if (seqInsDef.EdgeSet.Type(env) == "set<AEdge>")
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    else
                        source.Append("GRGEN_LIBGR.GraphHelper.InsertDefined((IDictionary)" + exprGen.GetSequenceExpression(seqInsDef.EdgeSet, source) + ", (GRGEN_LIBGR.IEdge)" + exprGen.GetSequenceExpression(seqInsDef.RootEdge, source) + ", graph)");
                    break;
                }

                case SequenceComputationType.Expression:
                {
                    SequenceExpression seqExpr = (SequenceExpression)seqComp;
                    source.AppendFront(SetResultVar(seqExpr, exprGen.GetSequenceExpression(seqExpr, source)));
                    break;
                }

                case SequenceComputationType.BuiltinProcedureCall:
                {
                    SequenceComputationBuiltinProcedureCall seqCall = (SequenceComputationBuiltinProcedureCall)seqComp;
                    SourceBuilder sb = new SourceBuilder();
                    EmitSequenceComputation(seqCall.BuiltinProcedure, sb);
                    if(seqCall.ReturnVars.Count > 0)
                    {
                        source.AppendFront(helper.SetVar(seqCall.ReturnVars[0], sb.ToString()));
                        source.AppendFront(SetResultVar(seqCall, helper.GetVar(seqCall.ReturnVars[0])));
                    }
                    else
                    {
                        source.AppendFront(sb.ToString() + ";\n");
                        source.AppendFront(SetResultVar(seqCall, "null"));
                    }
                    break;
                }

                case SequenceComputationType.ProcedureCall:
                {
                    SequenceComputationProcedureCall seqCall = (SequenceComputationProcedureCall)seqComp;

                    String returnParameterDeclarations;
                    String returnArguments;
                    String returnAssignments;
                    helper.BuildReturnParameters(seqCall.ProcedureInvocation, seqCall.ReturnVars, out returnParameterDeclarations, out returnArguments, out returnAssignments);

                    if(returnParameterDeclarations.Length != 0)
                        source.AppendFront(returnParameterDeclarations + "\n");

                    if(seqCall.IsExternalProcedureCalled)
                        source.AppendFront("GRGEN_EXPR.ExternalProcedures.");
                    else
                        source.AppendFrontFormat("GRGEN_ACTIONS.{0}Procedures.", TypesHelper.GetPackagePrefixDot(seqCall.ProcedureInvocation.Package));
                    source.Append(seqCall.ProcedureInvocation.Name);
                    source.Append("(procEnv, graph");
                    source.Append(helper.BuildParameters(seqCall.ProcedureInvocation, seqCall.ArgumentExpressions));
                    source.Append(returnArguments);
                    source.Append(");\n");

                    if(returnAssignments.Length != 0)
                        source.AppendFront(returnAssignments + "\n");

                    source.AppendFront(SetResultVar(seqCall, "null"));
                    break;
                }

                case SequenceComputationType.ProcedureMethodCall:
                {
                    SequenceComputationProcedureMethodCall seqCall = (SequenceComputationProcedureMethodCall)seqComp;
                    String type = seqCall.TargetExpr != null ? seqCall.TargetExpr.Type(env) : seqCall.TargetVar.Type;
                    if(type == "")
                    {
                        string tmpVarName = "tmpvar_" + helper.GetUniqueId();
                        source.AppendFront("object[] " + tmpVarName + " = ");
                        source.Append("((GRGEN_LIBGR.IGraphElement)");
                        if(seqCall.TargetExpr != null)
                            source.Append(exprGen.GetSequenceExpression(seqCall.TargetExpr, source));
                        else
                            source.Append(helper.GetVar(seqCall.TargetVar));
                        source.Append(").ApplyProcedureMethod(procEnv, graph, ");
                        source.Append("\"" + seqCall.ProcedureInvocation.Name + "\"");
                        source.Append(helper.BuildParametersInObject(seqCall.ProcedureInvocation, seqCall.ArgumentExpressions));
                        source.Append(");\n");
                        for(int i = 0; i < seqCall.ReturnVars.Length; i++)
                            source.Append(helper.SetVar(seqCall.ReturnVars[i], tmpVarName));
                    }
                    else
                    {
                        String returnParameterDeclarations;
                        String returnArguments;
                        String returnAssignments;
                        helper.BuildReturnParameters(seqCall.ProcedureInvocation, seqCall.ReturnVars, TypesHelper.GetNodeOrEdgeType(type, model), out returnParameterDeclarations, out returnArguments, out returnAssignments);

                        if(returnParameterDeclarations.Length != 0)
                            source.AppendFront(returnParameterDeclarations + "\n");

                        source.AppendFront("((");
                        source.Append(TypesHelper.XgrsTypeToCSharpType(type, model));
                        source.Append(")");
                        if(seqCall.TargetExpr != null)
                            source.Append(exprGen.GetSequenceExpression(seqCall.TargetExpr, source));
                        else
                            source.Append(helper.GetVar(seqCall.TargetVar));
                        source.Append(").");
                        source.Append(seqCall.ProcedureInvocation.Name);
                        source.Append("(procEnv, graph");
                        source.Append(helper.BuildParameters(seqCall.ProcedureInvocation, seqCall.ArgumentExpressions, TypesHelper.GetNodeOrEdgeType(type, model).GetProcedureMethod(seqCall.ProcedureInvocation.Name)));
                        source.Append(returnArguments);
                        source.Append(");\n");
                    }
                    source.AppendFront(SetResultVar(seqCall, "null"));
                    break;
                }

				default:
					throw new Exception("Unknown sequence computation type: " + seqComp.SequenceComputationType);
			}
		}

   		void EmitAssignment(AssignmentTarget tgt, string sourceValueComputation, SourceBuilder source)
		{
			switch(tgt.AssignmentTargetType)
			{
                case AssignmentTargetType.YieldingToVar:
                {
                    AssignmentTargetYieldingVar tgtYield = (AssignmentTargetYieldingVar)tgt;
                    source.AppendFront(helper.SetVar(tgtYield.DestVar, sourceValueComputation));
                    source.AppendFront(SetResultVar(tgtYield, helper.GetVar(tgtYield.DestVar)));
                    break;
                }

                case AssignmentTargetType.Visited:
                {
                    AssignmentTargetVisited tgtVisitedFlag = (AssignmentTargetVisited)tgt;
                    source.AppendFront("bool visval_"+tgtVisitedFlag.Id+" = (bool)"+sourceValueComputation+";\n");
                    source.AppendFront("graph.SetVisited((GRGEN_LIBGR.IGraphElement)"+helper.GetVar(tgtVisitedFlag.GraphElementVar)
                        + ", (int)" + exprGen.GetSequenceExpression(tgtVisitedFlag.VisitedFlagExpression, source) + ", visval_" + tgtVisitedFlag.Id + ");\n");
                    source.AppendFront(SetResultVar(tgtVisitedFlag, "visval_"+tgtVisitedFlag.Id));
                    break;
                }

                case AssignmentTargetType.IndexedVar:
                {
                    AssignmentTargetIndexedVar tgtIndexedVar = (AssignmentTargetIndexedVar)tgt;
                    string container = "container_" + tgtIndexedVar.Id;
                    source.AppendFront("object " + container + " = " + helper.GetVar(tgtIndexedVar.DestVar) + ";\n");
                    string key = "key_" + tgtIndexedVar.Id;
                    source.AppendFront("object " + key + " = " + exprGen.GetSequenceExpression(tgtIndexedVar.KeyExpression, source) + ";\n");
                    source.AppendFront(SetResultVar(tgtIndexedVar, container)); // container is a reference, so we can assign it already here before the changes

                    if(tgtIndexedVar.DestVar.Type == "")
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
                    else if(tgtIndexedVar.DestVar.Type.StartsWith("array"))
                    {
                        string array = helper.GetVar(tgtIndexedVar.DestVar);
                        source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                        source.Indent();
                        source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else if(tgtIndexedVar.DestVar.Type.StartsWith("deque"))
                    {
                        string deque = helper.GetVar(tgtIndexedVar.DestVar);
                        source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                        source.Indent();
                        source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    else
                    {
                        string dictionary = helper.GetVar(tgtIndexedVar.DestVar);
                        string dictSrcType = TypesHelper.XgrsTypeToCSharpType(TypesHelper.ExtractSrc(tgtIndexedVar.DestVar.Type), model);
                        source.AppendFront("if(" + dictionary + ".ContainsKey((" + dictSrcType + ")" + key + ")) {\n");
                        source.Indent();
                        source.AppendFront(dictionary + "[(" + dictSrcType + ")" + key + "] = " + sourceValueComputation + ";\n");
                        source.Unindent();
                        source.AppendFront("}\n");
                    }
                    break;
                }

                case AssignmentTargetType.Var:
				{
                    AssignmentTargetVar tgtVar = (AssignmentTargetVar)tgt;
                    source.AppendFront(helper.SetVar(tgtVar.DestVar, sourceValueComputation));
                    source.AppendFront(SetResultVar(tgtVar, helper.GetVar(tgtVar.DestVar)));
					break;
				}

                case AssignmentTargetType.Attribute:
                {
                    AssignmentTargetAttribute tgtAttr = (AssignmentTargetAttribute)tgt;
                    source.AppendFront("object value_" + tgtAttr.Id + " = " + sourceValueComputation + ";\n");
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttr.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(tgtAttr.DestVar) + ";\n");
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
                    source.AppendFront(SetResultVar(tgtAttr, "value_" + tgtAttr.Id));
                    break;
                }

                case AssignmentTargetType.AttributeIndexed:
                {
                    AssignmentTargetAttributeIndexed tgtAttrIndexedVar = (AssignmentTargetAttributeIndexed)tgt;
                    string value = "value_" + tgtAttrIndexedVar.Id;
                    source.AppendFront("object " + value + " = " + sourceValueComputation + ";\n");
                    source.AppendFront(SetResultVar(tgtAttrIndexedVar, "value_" + tgtAttrIndexedVar.Id));
                    source.AppendFront("GRGEN_LIBGR.IGraphElement elem_" + tgtAttrIndexedVar.Id + " = (GRGEN_LIBGR.IGraphElement)" + helper.GetVar(tgtAttrIndexedVar.DestVar) + ";\n");
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
                    else
                    {
                        GrGenType nodeOrEdgeType = TypesHelper.GetNodeOrEdgeType(tgtAttrIndexedVar.DestVar.Type, env.Model);
                        AttributeType attributeType = nodeOrEdgeType.GetAttributeType(tgtAttrIndexedVar.AttributeName);
                        string ContainerType = TypesHelper.AttributeTypeToXgrsType(attributeType);

                        if(ContainerType.StartsWith("array"))
                        {
                            string array = helper.GetVar(tgtAttrIndexedVar.DestVar);
                            source.AppendFront("if(" + array + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(array + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                        else if(ContainerType.StartsWith("deque"))
                        {
                            string deque = helper.GetVar(tgtAttrIndexedVar.DestVar);
                            source.AppendFront("if(" + deque + ".Count > (int)" + key + ") {\n");
                            source.Indent();
                            source.AppendFront(deque + "[(int)" + key + "] = " + sourceValueComputation + ";\n");
                            source.Unindent();
                            source.AppendFront("}\n");
                        }
                        else
                        {
                            string dictionary = helper.GetVar(tgtAttrIndexedVar.DestVar);
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
                    break;
                }

				default:
					throw new Exception("Unknown assignment target type: " + tgt.AssignmentTargetType);
			}
		}

        private string GetContainerValue(SequenceComputationContainer container)
        {
            if(container.Container != null)
                return helper.GetVar(container.Container);
            else
                return "((GRGEN_LIBGR.IGraphElement)" + helper.GetVar(container.Attribute.SourceVar) + ")" + ".GetAttribute(\"" + container.Attribute.AttributeName + "\")";
        }
    }
}
