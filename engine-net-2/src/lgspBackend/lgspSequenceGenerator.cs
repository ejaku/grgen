/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A closure for an exec statement in an alternative, iterated or subpattern,
    /// containing the entities needed for the exec execution.
    /// These exec are executed at the end of the rule which directly or indirectly used them,
    /// long after the alternative/iterated/subpattern modification containing them has been applied.
    /// The real stuff depends on the xgrs and is generated, implementing this abstract class.
    /// </summary>
    public abstract class LGSPEmbeddedSequenceClosure
    {
        /// <summary>
        /// Executes the embedded sequence closure
        /// </summary>
        /// <param name="procEnv">the processing environment on which to apply the sequence, esp. containing the graph</param>
        /// <returns>the result of sequence execution</returns>
        public abstract bool exec(LGSPGraphProcessingEnvironment procEnv);
    }

    /// <summary>
    /// The C#-part responsible for compiling the XGRSs of the exec statements.
    /// </summary>
    public class LGSPSequenceGenerator
    {
        IGraphModel model;

        SequenceCheckingEnvironmentCompiled env;

        NeededEntitiesEmitter neededEntitiesEmitter;

        SequenceGenerator seqGen;

        SequenceGeneratorHelper helper;

        ActionNames actionNames;

        bool fireDebugEvents;
        bool emitProfiling;


        public LGSPSequenceGenerator(IGraphModel model, ActionsTypeInformation actionsTypeInformation,
            bool fireDebugEvents, bool emitProfiling)
        {
            this.model = model;

            this.actionNames = new ActionNames(actionsTypeInformation);

            this.env = new SequenceCheckingEnvironmentCompiled(actionNames, actionsTypeInformation, model);

            this.helper = new SequenceGeneratorHelper(model, actionsTypeInformation, env);

            SequenceExpressionGenerator exprGen = new SequenceExpressionGenerator(model, env, helper);

            this.helper.SetSequenceExpressionGenerator(exprGen);

            SequenceComputationGenerator compGen = new SequenceComputationGenerator(model, env, exprGen, helper, fireDebugEvents);

            this.seqGen = new SequenceGenerator(model, env, compGen, exprGen, helper, fireDebugEvents, emitProfiling);

            this.neededEntitiesEmitter = new NeededEntitiesEmitter(compGen, helper);

            this.fireDebugEvents = fireDebugEvents;
            this.emitProfiling = emitProfiling;
        }

		public bool GenerateXGRSCode(string xgrsName, String package, String xgrsStr,
            String[] paramNames, GrGenType[] paramTypes,
            String[] defToBeYieldedToNames, GrGenType[] defToBeYieldedToTypes,
            SourceBuilder source, int lineNr)
		{
			Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for (int i = 0; i < paramNames.Length; i++)
            {
                varDecls.Add(paramNames[i], TypesHelper.DotNetTypeToXgrsType(paramTypes[i]));
            }
            for(int i = 0; i < defToBeYieldedToNames.Length; i++)
            {
                varDecls.Add(defToBeYieldedToNames[i], TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]));
            }

			Sequence seq;
            try
            {
                SequenceParserEnvironment parserEnv = new SequenceParserEnvironment(package, actionNames, model);
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(xgrsStr, parserEnv, varDecls, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("The exec statement \"" + xgrsStr
                        + "\" given on line " + lineNr + " reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(emitProfiling);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("The exec statement \"" + xgrsStr
                    + "\" given on line " + lineNr + " caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            source.Append("\n");
            source.AppendFront("public static bool ApplyXGRS_" + xgrsName + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
			for(int i = 0; i < paramNames.Length; i++)
			{
				source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(paramTypes[i]), model) + " var_");
				source.Append(paramNames[i]);
			}
            for(int i = 0; i < defToBeYieldedToTypes.Length; i++)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(defToBeYieldedToTypes[i]), model) + " var_");
                source.Append(defToBeYieldedToNames[i]);
            }
            source.Append(")\n");
			source.AppendFront("{\n");
			source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

			neededEntitiesEmitter.Reset();

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\", \"{1}\");\n", InjectExec(xgrsName), xgrsStr.Replace("\\", "\\\\").Replace("\"", "\\\""));
            }

            neededEntitiesEmitter.EmitNeededVarAndRuleEntities(seq, source);

			seqGen.EmitSequence(seq, source);

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\");\n", InjectExec(xgrsName));
            }

            source.AppendFront("return " + seqGen.GetSequenceResult(seq) + ";\n");
			source.Unindent();
			source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                SequenceContainerConstructorEmitter.GenerateContainerConstructor(model, cc, source);
            }

			return true;
		}

        private string InjectExec(string execName)
        {
            string rulePart = execName.Substring(0, execName.LastIndexOf('_'));
            string execNumberPart = execName.Substring(execName.LastIndexOf('_'));
            return rulePart + ".exec" + execNumberPart;
        }

        public bool GenerateDefinedSequence(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            Dictionary<String, String> varDecls = new Dictionary<String, String>();
            for(int i = 0; i < sequence.Parameters.Length; i++)
            {
                varDecls.Add(sequence.Parameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]));
            }
            for(int i = 0; i < sequence.OutParameters.Length; i++)
            {
                varDecls.Add(sequence.OutParameters[i], TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]));
            }

            Sequence seq;
            try
            {
                SequenceParserEnvironment parserEnv = new SequenceParserEnvironment(sequence.Package, actionNames, model);
                List<string> warnings = new List<string>();
                seq = SequenceParser.ParseSequence(sequence.XGRS, parserEnv, varDecls, warnings);
                foreach(string warning in warnings)
                {
                    Console.Error.WriteLine("In the defined sequence " + sequence.Name
                        + " the exec part \"" + sequence.XGRS
                        + "\" reported back:\n" + warning);
                }
                seq.Check(env);
                seq.SetNeedForProfilingRecursive(emitProfiling);
            }
            catch(ParseException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n" + ex.Message);
                return false;
            }
            catch(SequenceParserException ex)
            {
                Console.Error.WriteLine("In the defined sequence " + sequence.Name
                    + " the exec part \"" + sequence.XGRS
                    + "\" caused the following error:\n");
                HandleSequenceParserException(ex);
                return false;
            }

            // exact sequence definition compiled class
            source.Append("\n");

            if(sequence.Package != null)
            {
                source.AppendFrontFormat("namespace {0}\n", sequence.Package);
                source.AppendFront("{\n");
                source.Indent();
            }

            source.AppendFront("public class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateInternalDefinedSequenceApplicationMethod(source, sequence, seq);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            if(sequence.Package != null)
            {
                source.Unindent();
                source.AppendFront("}\n");
            }

            return true;
        }

        public bool GenerateExternalDefinedSequence(SourceBuilder source, ExternalDefinedSequenceInfo sequence)
        {
            // exact sequence definition compiled class
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + " : GRGEN_LIBGR.SequenceDefinitionCompiled\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateSequenceDefinedSingleton(source, sequence);

            source.Append("\n");
            GenerateExactExternalDefinedSequenceApplicationMethod(source, sequence);

            source.Append("\n");
            GenerateGenericExternalDefinedSequenceApplicationMethod(source, sequence);

            // end of exact sequence definition compiled class
            source.Unindent();
            source.AppendFront("}\n");

            return true;
        }

        public void GenerateExternalDefinedSequencePlaceholder(SourceBuilder source, ExternalDefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.Append("\n");
            source.AppendFront("public partial class Sequence_" + sequence.Name + "\n");
            source.AppendFront("{\n");
            source.Indent();

            GenerateInternalDefinedSequenceApplicationMethodStub(source, sequence, externalActionsExtensionFilename);

            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateSequenceDefinedSingleton(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            String className = "Sequence_" + sequence.Name;
            source.AppendFront("private static "+className+" instance = null;\n");

            source.AppendFront("public static "+className+" Instance { get { if(instance==null) instance = new "+className+"(); return instance; } }\n");

            source.AppendFront("private "+className+"() : base(\""+sequence.PackagePrefixedName+"\", SequenceInfo_"+sequence.Name+".Instance) { }\n");
        }

        private void GenerateInternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence, Sequence seq)
        {
            source.AppendFront("public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = procEnv.graph;\n");
            source.AppendFront("GRGEN_LGSP.LGSPActions actions = procEnv.curActions;\n");

            neededEntitiesEmitter.Reset();

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugEntering(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.Parameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.Parameters[i]);
                }
                source.Append(");\n");
            }

            neededEntitiesEmitter.EmitNeededVarAndRuleEntities(seq, source);

            seqGen.EmitSequence(seq, source);

            if(fireDebugEvents)
            {
                source.AppendFrontFormat("procEnv.DebugExiting(\"{0}\"", sequence.Name);
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.Append(", var_");
                    source.Append(sequence.OutParameters[i]);
                }
                source.Append(");\n");
            }

            source.AppendFront("return " + seqGen.GetSequenceResult(seq) + ";\n");
            source.Unindent();
            source.AppendFront("}\n");

            List<SequenceExpressionContainerConstructor> containerConstructors = new List<SequenceExpressionContainerConstructor>();
            Dictionary<SequenceVariable, SetValueType> variables = new Dictionary<SequenceVariable, SetValueType>();
            seq.GetLocalVariables(variables, containerConstructors, null);
            foreach(SequenceExpressionContainerConstructor cc in containerConstructors)
            {
                SequenceContainerConstructorEmitter.GenerateContainerConstructor(model, cc, source);
            }
        }

        private void GenerateInternalDefinedSequenceApplicationMethodStub(SourceBuilder source, DefinedSequenceInfo sequence, String externalActionsExtensionFilename)
        {
            source.AppendFrontFormat("// You must implement the following function in the same partial class in ./{0}\n", externalActionsExtensionFilename);

            source.AppendFront("//public static bool ApplyXGRS_" + sequence.Name + "(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
        }

        private void GenerateExactExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public static bool Apply_" + sequence.Name + "(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model) + " var_");
                source.Append(sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref " + TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model) + " var_");
                source.Append(sequence.OutParameters[i]);
            }
            source.Append(")\n");
            source.AppendFront("{\n");
            source.Indent();

            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " vari_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }
            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");
            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("var_" + sequence.OutParameters[i]);
                    source.Append(" = vari_" + sequence.OutParameters[i] + ";\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void GenerateGenericExternalDefinedSequenceApplicationMethod(SourceBuilder source, DefinedSequenceInfo sequence)
        {
            source.AppendFront("public override bool Apply(GRGEN_LIBGR.SequenceInvocationParameterBindings sequenceInvocation, GRGEN_LIBGR.IGraphProcessingEnvironment procEnv)");
            source.AppendFront("{\n");
            source.Indent();
            source.AppendFront("GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;\n");

            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.ParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.Parameters[i]);
                source.Append(" = (" + typeName + ")sequenceInvocation.ArgumentExpressions[" + i + "].Evaluate((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv);\n");
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                string typeName = TypesHelper.XgrsTypeToCSharpType(TypesHelper.DotNetTypeToXgrsType(sequence.OutParameterTypes[i]), model);
                source.AppendFront(typeName + " var_" + sequence.OutParameters[i]);
                source.Append(" = " + TypesHelper.DefaultValueString(typeName, model) + ";\n");
            }

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.SwitchToSubgraph((GRGEN_LIBGR.IGraph)sequenceInvocation.Subgraph.GetVariableValue(procEnv)); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

            source.AppendFront("bool result = ApplyXGRS_" + sequence.Name + "((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv");
            for(int i = 0; i < sequence.Parameters.Length; ++i)
            {
                source.Append(", var_" + sequence.Parameters[i]);
            }
            for(int i = 0; i < sequence.OutParameters.Length; ++i)
            {
                source.Append(", ref var_" + sequence.OutParameters[i]);
            }
            source.Append(");\n");

            source.AppendFront("if(sequenceInvocation.Subgraph!=null)\n");
            source.AppendFront("\t{ procEnv.ReturnFromSubgraph(); graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph; }\n");

            if(sequence.OutParameters.Length > 0)
            {
                source.AppendFront("if(result) {\n");
                source.Indent();
                for(int i = 0; i < sequence.OutParameters.Length; ++i)
                {
                    source.AppendFront("sequenceInvocation.ReturnVars[" + i + "].SetVariableValue(var_" + sequence.OutParameters[i] + ", procEnv);\n");
                }
                source.Unindent();
                source.AppendFront("}\n");
            }

            source.AppendFront("return result;\n");
            source.Unindent();
            source.AppendFront("}\n");
        }

        private void HandleSequenceParserException(SequenceParserException ex)
        {
            if(ex.Name == null 
                && ex.Kind != SequenceParserError.TypeMismatch
                && ex.Kind != SequenceParserError.FilterError
                && ex.Kind != SequenceParserError.FilterParameterError
                && ex.Kind != SequenceParserError.OperatorNotFound)
            {
                Console.Error.WriteLine("Unknown " + ex.DefinitionTypeName + ": \"{0}\"", ex.Name);
                return;
            }

            switch(ex.Kind)
            {
                case SequenceParserError.BadNumberOfParametersOrReturnParameters:
                    if(helper.actionsTypeInformation.InputTypes(ex.Name).Count != ex.NumGivenInputs && helper.actionsTypeInformation.OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of parameters and return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(helper.actionsTypeInformation.InputTypes(ex.Name).Count != ex.NumGivenInputs)
                        Console.Error.WriteLine("Wrong number of parameters for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else if(helper.actionsTypeInformation.OutputTypes(ex.Name).Count != ex.NumGivenOutputs)
                        Console.Error.WriteLine("Wrong number of return values for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    else
                        goto default;
                    break;

                case SequenceParserError.BadParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.BadReturnParameter:
                    Console.Error.WriteLine("The " + (ex.BadParamIndex + 1) + ". return parameter is not valid for " + ex.DefinitionTypeName + " \"" + ex.Name + "\"!");
                    break;

                case SequenceParserError.FilterError:
                    Console.Error.WriteLine("Can't apply filter " + ex.FilterName + " to rule " + ex.Name + "!");
                    return;

                case SequenceParserError.FilterParameterError:
                    Console.Error.WriteLine("Filter parameter mismatch for filter \"" + ex.FilterName + "\" applied to \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.OperatorNotFound:
                    Console.Error.WriteLine("Operator not found/arguments not of correct type: " + ex.Expression);
                    return;

                case SequenceParserError.RuleNameUsedByVariable:
                    Console.Error.WriteLine("The name of the variable conflicts with the name of action/sequence \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.VariableUsedWithParametersOrReturnParameters:
                    Console.Error.WriteLine("The variable \"" + ex.Name + "\" may neither receive parameters nor return values!");
                    return;

                case SequenceParserError.UnknownAttribute:
                    Console.WriteLine("Unknown attribute \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.UnknownProcedure:
                    Console.WriteLine("Unknown procedure \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.UnknownFunction:
                    Console.WriteLine("Unknown function \"" + ex.Name + "\"!");
                    return;

                case SequenceParserError.TypeMismatch:
                    Console.Error.WriteLine("The construct \"" + ex.VariableOrFunctionName + "\" expects:" + ex.ExpectedType + " but is / is given " + ex.GivenType + "!");
                    return;

                default:
                    throw new ArgumentException("Invalid error kind: " + ex.Kind);
            }

            if(helper.actionsTypeInformation.rulesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of rule/test: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.rulesToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.rulesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.sequencesToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of sequence: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.sequencesToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.sequencesToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.proceduresToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature procedure: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.proceduresToInputTypes[ex.Name]);
                PrintOutputParams(helper.actionsTypeInformation.proceduresToOutputTypes[ex.Name]);
                Console.Error.WriteLine();
            }
            else if(helper.actionsTypeInformation.functionsToInputTypes.ContainsKey(ex.Name))
            {
                Console.Error.Write("Signature of function: {0}", ex.Name);
                PrintInputParams(helper.actionsTypeInformation.functionsToInputTypes[ex.Name]);
                Console.Error.Write(" : ");
                Console.Error.Write(helper.actionsTypeInformation.functionsToOutputType[ex.Name]);
                Console.Error.WriteLine();
            }
        }

        private void PrintInputParams(List<String> nameToInputTypes)
        {
            if(nameToInputTypes.Count != 0)
            {
                Console.Error.Write("(");
                bool first = true;
                foreach(String typeName in nameToInputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }

        private void PrintOutputParams(List<String> nameToOutputTypes)
        {
            if(nameToOutputTypes.Count != 0)
            {
                Console.Error.Write(" : (");
                bool first = true;
                foreach(String typeName in nameToOutputTypes)
                {
                    Console.Error.Write("{0}{1}", first ? "" : ", ", typeName);
                    first = false;
                }
                Console.Error.Write(")");
            }
        }
    }
}
