/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// A proxy querying or simulating a user for choices during sequence execution
    /// TODO: general user proxy, not just for sequence execution
    /// </summary>
    public interface IUserProxyForSequenceExecution
    {
        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen directions is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        int ChooseDirection(int direction, Sequence seq);

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq);

        /// <summary>
        /// returns the maybe user altered point within the interval series, denoting the sequence to execute next
        /// the randomly chosen point is supplied; the sequence with the intervals and their corresponding sequences is supplied
        /// </summary>
        double ChoosePoint(double pointToExecute, SequenceWeightedOne seq);

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq);

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq);

        /// <summary>
        /// returns the maybe user altered random number in the range 0.0 - 1.0 exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        double ChooseRandomNumber(double randomNumber, Sequence seq);

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        object ChooseValue(string type, Sequence seq);

        /// <summary>
        /// highlights the arguments in the graphs if debugging is active
        /// </summary>
        void Highlight(string arguments, Sequence seq);

        /// <summary>
        /// highlights the values in the graphs if debugging is active (annotating them with the source names)
        /// </summary>
        void Highlight(List<object> values, List<string> sourceNames);
    }


    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Abstract base class, there are two concrete subclasses, one for interpreted, one for compiled sequences
    /// </summary>
    public abstract class SequenceCheckingEnvironment
    {
        /// <summary>
        /// The model giving access to graph element types for checking.
        /// </summary>
        public abstract IGraphModel Model { get; }

        /// <summary>
        /// Helper for checking rule calls, rule all calls, and sequence calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence to check, must be a rule call, a rule all call, or a sequence call</param>
        public void CheckCall(Sequence seq)
        {
            InvocationParameterBindingsWithReturns paramBindings = ExtractParameterBindings(seq);

            // check the name against the available names
            if(!IsCalledEntityExisting(paramBindings))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownRuleOrSequence);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings) != paramBindings.ArgumentExpressions.Length
                    || paramBindings.ReturnVars.Length != 0 && NumOutputParameters(paramBindings) != paramBindings.ReturnVars.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i]!=null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // Check return types
            for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
            {
                if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings), paramBindings.ReturnVars[i].Type, Model))
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
            }

            // Check filter in case there is one
            if(seq is SequenceRuleCall)
                if((seq as SequenceRuleCall).Filter!=null)
                    if(!IsFilterExisting(seq as SequenceRuleCall))
                        throw new SequenceParserException(paramBindings.Name, (seq as SequenceRuleCall).Filter, SequenceParserError.FilterError);

            SequenceVariable subgraph;
            if(paramBindings is RuleInvocationParameterBindings)
                subgraph = ((RuleInvocationParameterBindings)paramBindings).Subgraph;
            else
                subgraph = ((SequenceInvocationParameterBindings)paramBindings).Subgraph;
            if(subgraph!=null && !TypesHelper.IsSameOrSubtype("graph", subgraph.Type, Model))
                throw new SequenceParserException(paramBindings.Name, subgraph.Type, SequenceParserError.SubgraphTypeError);
    
            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking procedure calls.
        /// Checks whether called entity exists, type checks the input, type checks the output.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence computation to check, must be a procedure call</param>
        public void CheckProcedureCall(SequenceComputation seq)
        {
            InvocationParameterBindingsWithReturns paramBindings = (seq as SequenceComputationProcedureCall).ParamBindings;

            // check the name against the available names
            if(!IsCalledEntityExisting(paramBindings))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownProcedure);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings) != paramBindings.ArgumentExpressions.Length
                    || paramBindings.ReturnVars.Length != 0 && NumOutputParameters(paramBindings) != paramBindings.ReturnVars.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // Check return types
            for(int i = 0; i < paramBindings.ReturnVars.Length; ++i)
            {
                if(!TypesHelper.IsSameOrSubtype(OutputParameterType(i, paramBindings), paramBindings.ReturnVars[i].Type, Model))
                    throw new SequenceParserException(paramBindings, SequenceParserError.BadReturnParameter, i);
            }

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper for checking function calls.
        /// Checks whether called entity exists, and type checks the input.
        /// Throws an exception when an error is found.
        /// </summary>
        /// <param name="seq">The sequence expression to check, must be a function call</param>
        public void CheckFunctionCall(SequenceExpression seq)
        {
            InvocationParameterBindings paramBindings = (seq as SequenceExpressionFunctionCall).ParamBindings;

            // check the name against the available names
            if(!IsCalledEntityExisting(paramBindings))
                throw new SequenceParserException(paramBindings, SequenceParserError.UnknownFunction);

            // Check whether number of parameters and return parameters match
            if(NumInputParameters(paramBindings) != paramBindings.ArgumentExpressions.Length)
                throw new SequenceParserException(paramBindings, SequenceParserError.BadNumberOfParametersOrReturnParameters);

            // Check parameter types
            for(int i = 0; i < paramBindings.ArgumentExpressions.Length; i++)
            {
                paramBindings.ArgumentExpressions[i].Check(this);

                if(paramBindings.ArgumentExpressions[i] != null)
                {
                    if(!TypesHelper.IsSameOrSubtype(paramBindings.ArgumentExpressions[i].Type(this), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
                else
                {
                    if(paramBindings.Arguments[i] != null && !TypesHelper.IsSameOrSubtype(TypesHelper.XgrsTypeOfConstant(paramBindings.Arguments[i], Model), InputParameterType(i, paramBindings), Model))
                        throw new SequenceParserException(paramBindings, SequenceParserError.BadParameter, i);
                }
            }

            // ok, this is a well-formed invocation
        }

        /// <summary>
        /// Helper which returns the type of the given top level entity of the given rule.
        /// Throws an exception in case the rule of the given name does not exist 
        /// or in case it does not contain an entity of the given name.
        /// </summary>
        public abstract string TypeOfTopLevelEntityInRule(string ruleName, string entityName);

        private InvocationParameterBindingsWithReturns ExtractParameterBindings(SequenceBase seq)
        {
            if(seq is SequenceRuleCall) // hint: a rule all call is a rule call, too
                return (seq as SequenceRuleCall).ParamBindings;
            else
                return (seq as SequenceSequenceCall).ParamBindings;
        }

        protected abstract bool IsCalledEntityExisting(InvocationParameterBindings paramBindings);
        protected abstract int NumInputParameters(InvocationParameterBindings paramBindings);
        protected abstract int NumOutputParameters(InvocationParameterBindings paramBindings);
        protected abstract string InputParameterType(int i, InvocationParameterBindings paramBindings);
        protected abstract string OutputParameterType(int i, InvocationParameterBindings paramBindings);
        protected abstract bool IsFilterExisting(SequenceRuleCall seq);
    }

    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for interpreted sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentInterpreted : SequenceCheckingEnvironment
    {
        // constructor for interpreted sequences
        public SequenceCheckingEnvironmentInterpreted(BaseActions actions)
        {
            this.actions = actions;
        }

        // the information available if this is an interpreted sequence 

        private BaseActions actions;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IGraphModel Model { get { return actions.Graph.Model; } }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            IAction rule = actions.GetAction(ruleName);
            if(rule==null)
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            foreach(IPatternNode node in rule.RulePattern.PatternGraph.Nodes)
                if(node.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(node.Type);

            foreach(IPatternEdge edge in rule.RulePattern.PatternGraph.Edges)
                if(edge.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(edge.Type);

            foreach(IPatternVariable var in rule.RulePattern.PatternGraph.Variables)
                if(var.UnprefixedName==entityName)
                    return TypesHelper.DotNetTypeToXgrsType(var.Type);

            throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);
        }

        protected override bool IsCalledEntityExisting(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action != null;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return seqParamBindings.SequenceDef != null;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return procParamBindings.ProcedureDef != null;
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return funcParamBindings.FunctionDef != null;
            }
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Inputs.Length;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.InputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return seqDef.SeqInfo.ParameterTypes.Length;
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return procParamBindings.ProcedureDef.inputs.Length;
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return funcParamBindings.FunctionDef.inputs.Length;
            }
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Outputs.Length;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.OutputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return seqDef.SeqInfo.OutParameterTypes.Length;
                }
            }
            else
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return procParamBindings.ProcedureDef.outputs.Length;
            }
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Inputs[i]);
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.InputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.ParameterTypes[i]);
                }
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(procParamBindings.ProcedureDef.inputs[i]);
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(funcParamBindings.FunctionDef.inputs[i]);
            }
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Outputs[i]);
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                if(seqParamBindings.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqParamBindings.SequenceDef;
                    return seqDef.OutputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqParamBindings.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.OutParameterTypes[i]);
                }
            }
            else
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(procParamBindings.ProcedureDef.outputs[i]);
            }
        }

        protected override bool IsFilterExisting(SequenceRuleCall seq)
        {
            return Array.IndexOf(seq.ParamBindings.Action.RulePattern.Filters, seq.Filter) != -1;
        }
    }

    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for compiled sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentCompiled : SequenceCheckingEnvironment
    {
        // constructor for compiled sequences
        public SequenceCheckingEnvironmentCompiled(String[] ruleNames, String[] sequenceNames, String[] procedureNames, String[] functionNames,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes, Dictionary<String, List<String>> rulesToFilters,
            Dictionary<String, List<String>> rulesToTopLevelEntities, Dictionary<String, List<String>> rulesToTopLevelEntityTypes, 
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            Dictionary<String, List<String>> proceduresToInputTypes, Dictionary<String, List<String>> proceduresToOutputTypes,
            Dictionary<String, List<String>> functionsToInputTypes, Dictionary<String, String> functionsToOutputType,
            IGraphModel model)
        {
            this.ruleNames = ruleNames;
            this.sequenceNames = sequenceNames;
            this.procedureNames = procedureNames;
            this.functionNames = functionNames;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.rulesToFilters = rulesToFilters;
            this.rulesToTopLevelEntities = rulesToTopLevelEntities;
            this.rulesToTopLevelEntityTypes = rulesToTopLevelEntityTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
            this.proceduresToInputTypes = proceduresToInputTypes;
            this.proceduresToOutputTypes = proceduresToOutputTypes;
            this.functionsToInputTypes = functionsToInputTypes;
            this.functionsToOutputType = functionsToOutputType;
            this.model = model;
        }

        // the information available if this is a compiled sequence 

        // the rule names available in the .grg to compile
        private String[] ruleNames;

        // the sequence names available in the .grg to compile
        private String[] sequenceNames;

        // the procedure names available in the .grg to compile
        private String[] procedureNames;

        // the function names available in the .grg to compile
        private String[] functionNames;

        // maps rule names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> rulesToOutputTypes;

        // maps rule names available in the .grg to compile to the list of the match filter names
        private Dictionary<String, List<String>> rulesToFilters;

        // maps rule names available in the .grg to compile to the list of the top level entity names (nodes,edges,variables)
        private Dictionary<String, List<String>> rulesToTopLevelEntities;
        // maps rule names available in the .grg to compile to the list of the top level entity types
        private Dictionary<String, List<String>> rulesToTopLevelEntityTypes;

        // maps sequence names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> sequencesToOutputTypes;

        // maps procedure names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> proceduresToInputTypes;
        // maps procedure names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> proceduresToOutputTypes;

        // maps function names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> functionsToInputTypes;
        // maps function names available in the .grg to compile to the list of the output typ name
        private Dictionary<String, String> functionsToOutputType;

        // returns rule or sequence name to input types dictionary depending on argument
        private Dictionary<String, List<String>> toInputTypes(bool rule) { return rule ? rulesToInputTypes : sequencesToInputTypes; }

        // returns rule or sequence name to output types dictionary depending on argument
        private Dictionary<String, List<String>> toOutputTypes(bool rule) { return rule ? rulesToOutputTypes : sequencesToOutputTypes; }

        // the model object of the .grg to compile
        private IGraphModel model;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// the model giving access to graph element types for checking
        /// </summary>
        public override IGraphModel Model { get { return model; } }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            if(!rulesToTopLevelEntities.ContainsKey(ruleName))
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            if(!rulesToTopLevelEntities[ruleName].Contains(entityName))
                throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);

            int indexOfEntity = rulesToTopLevelEntities[ruleName].IndexOf(entityName);
            return rulesToTopLevelEntityTypes[ruleName][indexOfEntity];
        }

        protected override bool IsCalledEntityExisting(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return Array.IndexOf(ruleNames, ruleParamBindings.Name) != -1;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return Array.IndexOf(sequenceNames, seqParamBindings.Name) != -1;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return Array.IndexOf(procedureNames, procParamBindings.Name) != -1;
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return Array.IndexOf(functionNames, funcParamBindings.Name) != -1;
            }
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.Name].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.Name].Count;
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return proceduresToInputTypes[procParamBindings.Name].Count;
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return functionsToInputTypes[funcParamBindings.Name].Count;
            }
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.Name].Count;
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.Name].Count;
            }
            else
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return proceduresToOutputTypes[procParamBindings.Name].Count;
            }
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.Name][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.Name][i];
            }
            else if(paramBindings is ProcedureInvocationParameterBindings)
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return proceduresToInputTypes[procParamBindings.Name][i];
            }
            else
            {
                FunctionInvocationParameterBindings funcParamBindings = (FunctionInvocationParameterBindings)paramBindings;
                return functionsToInputTypes[funcParamBindings.Name][i];
            }
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.Name][i];
            }
            else if(paramBindings is SequenceInvocationParameterBindings)
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.Name][i];
            }
            else
            {
                ProcedureInvocationParameterBindings procParamBindings = (ProcedureInvocationParameterBindings)paramBindings;
                return proceduresToOutputTypes[procParamBindings.Name][i];
            }
        }

        protected override bool IsFilterExisting(SequenceRuleCall seq)
        {
            return rulesToFilters[seq.ParamBindings.Name].Contains(seq.Filter);
        }
    }


    /// <summary>
    /// The common base of sequence, sequence computation, and sequence expression objects,
    /// with some common infrastructure.
    /// </summary>
    public abstract class SequenceBase
    {
        /// <summary>
        /// Checks the sequence /expression for errors utilizing the given checking environment
        /// reports them by exception
        /// </summary>s
        public abstract void Check(SequenceCheckingEnvironment env);

        /// <summary>
        /// Returns the type of the sequence /expression (for sequences always "boolean")
        /// </summary>
        public abstract string Type(SequenceCheckingEnvironment env);
        
        /// <summary>
        /// A common random number generator for all sequence /expression objects.
        /// It uses a time-dependent seed.
        /// </summary>
        public static Random randomGenerator = new Random();

        /// <summary>
        /// The precedence of this operator. Zero is the highest priority, int.MaxValue the lowest.
        /// Used to add needed parentheses for printing sequences /expressions
        /// TODO: WTF? das ist im Parser genau umgekehrt implementiert!
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// A string symbol representing this sequence /expression kind.
        /// </summary>
        public abstract String Symbol { get; }

        /// <summary>
        /// returns the sequence /expresion id - every sequence /expression is assigned a unique id used in xgrs code generation
        /// for copies the old id is just taken over, does not cause problems as code is only generated once per defined sequence
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// stores the sequence /expression unique id
        /// </summary>
        protected int id;

        /// <summary>
        /// the static member used to assign the unique ids to the sequence /expression instances
        /// </summary>
        protected static int idSource = 0;
    }
}
