/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections;
using System.Text;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// The sequence generator helper contains miscellaneous code in use by the
    /// sequence generator, sequence computation generator, and sequence expression generator.
    /// It esp. contains parameter type information and code for parameter building, as well as type computation, variable access and constant generation.
    /// </summary>
    public class SequenceGeneratorHelper
    {
        readonly IGraphModel model;

        public readonly ActionsTypeInformation actionsTypeInformation;

        readonly SequenceCheckingEnvironment env;

        SequenceExpressionGenerator exprGen;

        // a counter for unique temporary variables needed as dummy variables
        // to receive the return/out values of rules/sequnces in case no assignment is given
        int tmpVarCtr;


        public SequenceGeneratorHelper(IGraphModel model, ActionsTypeInformation actionsTypeInformation, SequenceCheckingEnvironmentCompiled checkEnv)
        {
            this.model = model;
            this.actionsTypeInformation = actionsTypeInformation;
            this.env = checkEnv;
        }

        public void SetSequenceExpressionGenerator(SequenceExpressionGenerator exprGen)
        {
            this.exprGen = exprGen;
        }

        /// <summary>
        /// Returns string containing a C# expression to get the value of the sequence-local variable / graph-global variable given
        /// </summary>
        public string GetVar(SequenceVariable seqVar)
        {
            if(seqVar.Type == "")
                return "procEnv.GetVariableValue(\"" + seqVar.PureName + "\")";
            else
                return "var_" + seqVar.Prefix + seqVar.PureName;
        }

        /// <summary>
        /// Returns string containing a C# assignment to set the sequence-local variable / graph-global variable given
        /// to the value as computed by the C# expression in the string given
        /// </summary>
        public string SetVar(SequenceVariable seqVar, String valueToWrite)
        {
            if(seqVar.Type == "")
                return "procEnv.SetVariableValue(\"" + seqVar.PureName + "\", " + valueToWrite + ");\n";
            else
            {
                String cast = "(" + TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model) + ")";
                return "var_" + seqVar.Prefix + seqVar.PureName + " = " + cast + "(" + valueToWrite + ");\n";
            }
        }

        /// <summary>
        /// Returns string containing a C# declaration of the variable given
        /// </summary>
        public string DeclareVar(SequenceVariable seqVar)
        {
            if(seqVar.Type != "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(TypesHelper.XgrsTypeToCSharpType(seqVar.Type, model));
                sb.Append(" ");
                sb.Append("var_" + seqVar.Prefix + seqVar.PureName);
                sb.Append(" = ");
                sb.Append(TypesHelper.DefaultValueString(seqVar.Type, model));
                sb.Append(";\n");
                return sb.ToString();
            }
            else
                return "";
        }

        public string GetUniqueId()
        {
            String id = tmpVarCtr.ToString();
            ++tmpVarCtr;
            return id;
        }

        public String BuildParameters(Invocation invocation, SequenceExpression[] ArgumentExpressions, SourceBuilder source)
        {
            String parameters = "";
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                String typeName;
                if(actionsTypeInformation.rulesToInputTypes.ContainsKey(invocation.PackagePrefixedName))
                    typeName = actionsTypeInformation.rulesToInputTypes[invocation.PackagePrefixedName][i];
                else if(actionsTypeInformation.sequencesToInputTypes.ContainsKey(invocation.PackagePrefixedName))
                    typeName = actionsTypeInformation.sequencesToInputTypes[invocation.PackagePrefixedName][i];
                else if(actionsTypeInformation.proceduresToInputTypes.ContainsKey(invocation.PackagePrefixedName))
                    typeName = actionsTypeInformation.proceduresToInputTypes[invocation.PackagePrefixedName][i];
                else
                    typeName = actionsTypeInformation.functionsToInputTypes[invocation.PackagePrefixedName][i];
                String cast = "(" + TypesHelper.XgrsTypeToCSharpType(typeName, model) + ")";
                parameters += ", " + cast + exprGen.GetSequenceExpression(ArgumentExpressions[i], source);
            }
            return parameters;
        }

        public String BuildParameters(Invocation invocation, SequenceExpression[] ArgumentExpressions, IFunctionDefinition functionMethod, SourceBuilder source)
        {
            String parameters = "";
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                if(ArgumentExpressions[i] != null)
                {
                    String typeName = TypesHelper.DotNetTypeToXgrsType(functionMethod.Inputs[i]);
                    String cast = "(" + TypesHelper.XgrsTypeToCSharpType(typeName, model) + ")";
                    parameters += ", " + cast + exprGen.GetSequenceExpression(ArgumentExpressions[i], source);
                }
                else
                {
                    // the sequence parser always emits all argument expressions, for interpreted and compiled
                    throw new Exception("Internal error: missing argument expressions");
                }
            }
            return parameters;
        }

        public String BuildParameters(Invocation invocation, SequenceExpression[] ArgumentExpressions, IProcedureDefinition procedureMethod, SourceBuilder source)
        {
            String parameters = "";
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                String typeName = TypesHelper.DotNetTypeToXgrsType(procedureMethod.Inputs[i]);
                String cast = "(" + TypesHelper.XgrsTypeToCSharpType(typeName, model) + ")";
                parameters += ", " + cast + exprGen.GetSequenceExpression(ArgumentExpressions[i], source);
            }
            return parameters;
        }

        public String BuildParametersInObject(Invocation invocation, SequenceExpression[] ArgumentExpressions, SourceBuilder source)
        {
            String parameters = ", new object[] { ";
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                if(ArgumentExpressions[i] != null)
                {
                    if(i != 0)
                        parameters += ", ";
                    parameters += exprGen.GetSequenceExpression(ArgumentExpressions[i], source);
                }
                else
                {
                    // the sequence parser always emits all argument expressions, for interpreted and compiled
                    throw new Exception("Internal error: missing argument expressions");
                }
            }
            return parameters + " }";
        }

        public String BuildParametersInDeclarations(Invocation invocation, SequenceExpression[] ArgumentExpressions, SourceBuilder source, out String declarations)
        {
            String parameters = "";
            declarations = "";
            for(int i = 0; i < ArgumentExpressions.Length; i++)
            {
                String typeName;
                if(actionsTypeInformation.rulesToInputTypes.ContainsKey(invocation.PackagePrefixedName))
                    typeName = actionsTypeInformation.rulesToInputTypes[invocation.PackagePrefixedName][i];
                else 
                    typeName = actionsTypeInformation.sequencesToInputTypes[invocation.PackagePrefixedName][i];
                String type = TypesHelper.XgrsTypeToCSharpType(typeName, model);
                String name = "tmpvar_" + GetUniqueId();
                declarations += type + " " + name + " = " + "(" + type + ")" + exprGen.GetSequenceExpression(ArgumentExpressions[i], source) + ";";
                parameters += ", " + name;
            }
            return parameters;
        }

        public void BuildOutParameters(SequenceInvocation invocation, SequenceVariable[] ReturnVars, out String outParameterDeclarations, out String outArguments, out String outAssignments)
        {
            outParameterDeclarations = "";
            outArguments = "";
            outAssignments = "";
            for(int i = 0; i < actionsTypeInformation.sequencesToOutputTypes[invocation.PackagePrefixedName].Count; ++i)
            {
                String varName;
                if(ReturnVars.Length != 0)
                    varName = GetUniqueId() + ReturnVars[i].PureName;
                else
                    varName = GetUniqueId();
                String typeName = actionsTypeInformation.sequencesToOutputTypes[invocation.PackagePrefixedName][i];
                outParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " tmpvar_" + varName
                    + " = " + TypesHelper.DefaultValueString(typeName, model) + ";";
                outArguments += ", ref tmpvar_" + varName;
                if(ReturnVars.Length != 0)
                    outAssignments += SetVar(ReturnVars[i], "tmpvar_" + varName);
            }
        }

        public void BuildReturnParameters(RuleInvocation invocation, SequenceVariable[] ReturnVars,
            out String returnParameterDeclarations, out String returnArguments, out String returnAssignments,
            out String returnParameterDeclarationsAllCall, out String intermediateReturnAssignmentsAllCall, out String returnAssignmentsAllCall)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            StringBuilder sbReturnParameterDeclarations = new StringBuilder();
            StringBuilder sbReturnArguments = new StringBuilder();
            StringBuilder sbReturnAssignments = new StringBuilder();
            StringBuilder sbReturnParameterDeclarationsAllCall = new StringBuilder();
            StringBuilder sbIntermediateReturnAssignmentsAllCall = new StringBuilder();
            StringBuilder sbReturnAssignmentsAllCall = new StringBuilder();

            for(int i = 0; i < actionsTypeInformation.rulesToOutputTypes[invocation.PackagePrefixedName].Count; ++i)
            {
                String varName;
                if(ReturnVars.Length != 0)
                    varName = GetUniqueId() + ReturnVars[i].PureName;
                else
                    varName = GetUniqueId();
                String typeName = actionsTypeInformation.rulesToOutputTypes[invocation.PackagePrefixedName][i];

                String tmpvarName = "tmpvar_" + varName;
                sbReturnParameterDeclarations.Append(TypesHelper.XgrsTypeToCSharpType(typeName, model));
                sbReturnParameterDeclarations.Append(" ");
                sbReturnParameterDeclarations.Append(tmpvarName);
                sbReturnParameterDeclarations.Append("; ");

                String returnListValueVarType = typeName;
                String tmpvarListName = "tmpvarlist_" + varName;
                if(ReturnVars.Length != 0 && ReturnVars[i].Type != "" && ReturnVars[i].Type.StartsWith("array<"))
                    returnListValueVarType = TypesHelper.ExtractSrc(ReturnVars[i].Type);
                if(ReturnVars.Length != 0)
                {
                    sbReturnParameterDeclarationsAllCall.Append("List<");
                    sbReturnParameterDeclarationsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbReturnParameterDeclarationsAllCall.Append("> ");
                    sbReturnParameterDeclarationsAllCall.Append(tmpvarListName);
                    sbReturnParameterDeclarationsAllCall.Append(" = new List<");
                    sbReturnParameterDeclarationsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbReturnParameterDeclarationsAllCall.Append(">(); ");
                }

                sbReturnArguments.Append(", out ");
                sbReturnArguments.Append(tmpvarName);

                if(ReturnVars.Length != 0)
                {
                    sbReturnAssignments.Append(SetVar(ReturnVars[i], tmpvarName));

                    sbIntermediateReturnAssignmentsAllCall.Append(tmpvarListName);
                    sbIntermediateReturnAssignmentsAllCall.Append(".Add((");
                    sbIntermediateReturnAssignmentsAllCall.Append(TypesHelper.XgrsTypeToCSharpType(returnListValueVarType, model));
                    sbIntermediateReturnAssignmentsAllCall.Append(")");
                    sbIntermediateReturnAssignmentsAllCall.Append(tmpvarName);
                    sbIntermediateReturnAssignmentsAllCall.Append("); ");
                    
                    sbReturnAssignmentsAllCall.Append(SetVar(ReturnVars[i], tmpvarListName));
                }
            }

            returnParameterDeclarations = sbReturnParameterDeclarations.ToString();
            returnArguments = sbReturnArguments.ToString();
            returnAssignments = sbReturnAssignments.ToString();
            returnParameterDeclarationsAllCall = sbReturnParameterDeclarationsAllCall.ToString();
            intermediateReturnAssignmentsAllCall = sbIntermediateReturnAssignmentsAllCall.ToString();
            returnAssignmentsAllCall = sbReturnAssignmentsAllCall.ToString();
        }

        public void BuildReturnParameters(ProcedureInvocation invocation, SequenceVariable[] ReturnVars, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < actionsTypeInformation.proceduresToOutputTypes[invocation.PackagePrefixedName].Count; ++i)
            {
                String varName;
                if(ReturnVars.Length != 0)
                    varName = GetUniqueId() + ReturnVars[i].PureName;
                else
                    varName = GetUniqueId();
                String typeName = actionsTypeInformation.proceduresToOutputTypes[invocation.PackagePrefixedName][i];
                String tmpvarName = "tmpvar_" + varName;
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " " + tmpvarName + "; ";
                returnArguments += ", out " + tmpvarName;
                if(ReturnVars.Length != 0)
                    returnAssignments += SetVar(ReturnVars[i], tmpvarName);
            }
        }

        public void BuildReturnParameters(ProcedureInvocation invocation, SequenceVariable[] ReturnVars, InheritanceType ownerType, out String returnParameterDeclarations, out String returnArguments, out String returnAssignments)
        {
            // can't use the normal xgrs variables for return value receiving as the type of an out-parameter must be invariant
            // this is bullshit, as it is perfectly safe to assign a subtype to a variable of a supertype
            // so we create temporary variables of exact type, which are used to receive the return values,
            // and finally we assign these temporary variables to the real xgrs variables

            returnParameterDeclarations = "";
            returnArguments = "";
            returnAssignments = "";
            for(int i = 0; i < ownerType.GetProcedureMethod(invocation.Name).Outputs.Length; ++i)
            {
                String varName;
                if(ReturnVars.Length != 0)
                    varName = GetUniqueId() + ReturnVars[i].PureName;
                else
                    varName = GetUniqueId();
                String typeName = TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(invocation.Name).Outputs[i]);
                String tmpvarName = "tmpvar_" + varName;
                returnParameterDeclarations += TypesHelper.XgrsTypeToCSharpType(typeName, model) + " " + tmpvarName + "; ";
                returnArguments += ", out " + tmpvarName;
                if(ReturnVars.Length != 0)
                    returnAssignments += SetVar(ReturnVars[i], tmpvarName);
            }
        }

        public string ExtractNodeType(SourceBuilder source, SequenceExpression typeExpr)
        {
            string adjacentNodeType = "graph.Model.NodeModel.RootType";
            if(typeExpr != null)
            {
                if(typeExpr.Type(env) != "")
                {
                    if(typeExpr.Type(env) == "string")
                        adjacentNodeType = "graph.Model.NodeModel.GetType((string)" + exprGen.GetSequenceExpression(typeExpr, source) + ")";
                    else
                        adjacentNodeType = "(GRGEN_LIBGR.NodeType)" + exprGen.GetSequenceExpression(typeExpr, source);
                }
                else
                {
                    adjacentNodeType = exprGen.GetSequenceExpression(typeExpr, source) + " is string ? "
                        + "graph.Model.NodeModel.GetType((string)" + exprGen.GetSequenceExpression(typeExpr, source) + ")"
                        + " : " + "(GRGEN_LIBGR.NodeType)" + exprGen.GetSequenceExpression(typeExpr, source);
                }
            }
            return "(" + adjacentNodeType + ")";
        }

        public string ExtractEdgeType(SourceBuilder source, SequenceExpression typeExpr)
        {
            string incidentEdgeType = "graph.Model.EdgeModel.RootType";
            if(typeExpr != null)
            {
                if(typeExpr.Type(env) != "")
                {
                    if(typeExpr.Type(env) == "string")
                        incidentEdgeType = "graph.Model.EdgeModel.GetType((string)" + exprGen.GetSequenceExpression(typeExpr, source) + ")";
                    else
                        incidentEdgeType = "(GRGEN_LIBGR.EdgeType)" + exprGen.GetSequenceExpression(typeExpr, source);
                }
                else
                {
                    incidentEdgeType = exprGen.GetSequenceExpression(typeExpr, source) + " is string ? "
                        + "graph.Model.EdgeModel.GetType((string)" + exprGen.GetSequenceExpression(typeExpr, source) + ")"
                        + " : " + "(GRGEN_LIBGR.EdgeType)" + exprGen.GetSequenceExpression(typeExpr, source);
                }
            }
            return "(" + incidentEdgeType + ")";
        }

        public string GetDirectedness(String edgeRootType)
        {
            if(edgeRootType == "Edge")
                return "Directed";
            else if(edgeRootType == "UEdge")
                return "Undirected";
            else
                return "";
        }

        public string GetConstant(object constant)
        {
            if(constant is bool)
                return (bool)constant == true ? "true" : "false";
            else if(constant is Enum)
            {
                Enum enumConst = (Enum)constant;
                return enumConst.GetType().ToString() + "." + enumConst.ToString();
            }
            else if(constant is IDictionary)
            {
                Type keyType;
                Type valueType;
                ContainerHelper.GetDictionaryTypes(constant, out keyType, out valueType);
                String srcType = "typeof(" + TypesHelper.PrefixedTypeFromType(keyType) + ")";
                String dstType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewDictionary(" + srcType + "," + dstType + ")";
            }
            else if(constant is IList)
            {
                Type valueType;
                ContainerHelper.GetListType(constant, out valueType);
                String dequeValueType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewList(" + dequeValueType + ")";
            }
            else if(constant is IDeque)
            {
                Type valueType;
                ContainerHelper.GetDequeType(constant, out valueType);
                String dequeValueType = "typeof(" + TypesHelper.PrefixedTypeFromType(valueType) + ")";
                return "GRGEN_LIBGR.ContainerHelper.NewDeque(" + dequeValueType + ")";
            }
            else if(constant is string)
                return "\"" + constant.ToString() + "\"";
            else if(constant is float)
                return ((float)constant).ToString(System.Globalization.CultureInfo.InvariantCulture) + "f";
            else if(constant is double)
                return "((double)" + ((double)constant).ToString(System.Globalization.CultureInfo.InvariantCulture) + ")";
            else if(constant is sbyte)
                return "((sbyte)" + constant.ToString() + ")";
            else if(constant is short)
                return "((short)" + constant.ToString() + ")";
            else if(constant is long)
                return "((long)" + constant.ToString() + ")";
            else if(constant is NodeType)
                return "(GRGEN_LIBGR.TypesHelper.GetNodeType(\"" + constant + "\", graph.Model))";
            else if(constant is EdgeType)
                return "(GRGEN_LIBGR.TypesHelper.GetEdgeType(\"" + constant + "\", graph.Model))";
            else
            {
                if(constant == null)
                    return "null";
                else
                    return constant.ToString();
            }
        }

        public static string Escape(String str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        public static string ConstructTypeValue(PatternMatchingConstructType constructType)
        {
            return "GRGEN_LIBGR.PatternMatchingConstructType." + constructType.ToString();
        }
    }
}
