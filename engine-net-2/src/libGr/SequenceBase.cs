/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        object ChooseValue(string type, Sequence seq);
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
        public void CheckRuleCallRuleAllCallSequenceCall(Sequence seq)
        {
            InvocationParameterBindings paramBindings = ExtractParameterBindings(seq);

            // check the rule name against the available rule names
            if(!IsRuleOrSequenceExisting(paramBindings))
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

            // ok, this is a well-formed rule invocation
        }

        private InvocationParameterBindings ExtractParameterBindings(Sequence seq)
        {
            if(seq is SequenceRuleCall) // hint: a rule all call is a rule call, too
                return (seq as SequenceRuleCall).ParamBindings;
            else
                return (seq as SequenceSequenceCall).ParamBindings;
        }

        protected abstract bool IsRuleOrSequenceExisting(InvocationParameterBindings paramBindings);
        protected abstract int NumInputParameters(InvocationParameterBindings paramBindings);
        protected abstract int NumOutputParameters(InvocationParameterBindings paramBindings);
        protected abstract string InputParameterType(int i, InvocationParameterBindings paramBindings);
        protected abstract string OutputParameterType(int i, InvocationParameterBindings paramBindings);
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

        protected override bool IsRuleOrSequenceExisting(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action != null;
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return seqParamBindings.SequenceDef != null;
            }
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Inputs.Length;
            }
            else
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
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return ruleParamBindings.Action.RulePattern.Outputs.Length;
            }
            else
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
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Inputs[i]);
            }
            else
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
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return TypesHelper.DotNetTypeToXgrsType(ruleParamBindings.Action.RulePattern.Outputs[i]);
            }
            else
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
        }
    }

    /// <summary>
    /// Environment for sequence checking giving access to model and action signatures.
    /// Concrete subclass for compiled sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentCompiled : SequenceCheckingEnvironment
    {
        // constructor for compiled sequences
        public SequenceCheckingEnvironmentCompiled(String[] ruleNames, String[] sequenceNames,
            Dictionary<String, List<String>> rulesToInputTypes, Dictionary<String, List<String>> rulesToOutputTypes,
            Dictionary<String, List<String>> sequencesToInputTypes, Dictionary<String, List<String>> sequencesToOutputTypes,
            IGraphModel model)
        {
            this.ruleNames = ruleNames;
            this.sequenceNames = sequenceNames;
            this.rulesToInputTypes = rulesToInputTypes;
            this.rulesToOutputTypes = rulesToOutputTypes;
            this.sequencesToInputTypes = sequencesToInputTypes;
            this.sequencesToOutputTypes = sequencesToOutputTypes;
            this.model = model;
        }

        // the information available if this is a compiled sequence 

        // the rule names available in the .grg to compile
        private String[] ruleNames;

        // the sequence names available in the .grg to compile
        private String[] sequenceNames;

        // maps rule names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> rulesToInputTypes;
        // maps rule names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> rulesToOutputTypes;

        // maps sequence names available in the .grg to compile to the list of the input typ names
        private Dictionary<String, List<String>> sequencesToInputTypes;
        // maps sequence names available in the .grg to compile to the list of the output typ names
        private Dictionary<String, List<String>> sequencesToOutputTypes;

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

        protected override bool IsRuleOrSequenceExisting(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return Array.IndexOf(ruleNames, ruleParamBindings.Name) != -1;
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return Array.IndexOf(sequenceNames, seqParamBindings.Name) != -1;
            }
        }

        protected override int NumInputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.Name].Count;
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.Name].Count;
            }
        }

        protected override int NumOutputParameters(InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.Name].Count;
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.Name].Count;
            }
        }

        protected override string InputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToInputTypes[ruleParamBindings.Name][i];
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToInputTypes[seqParamBindings.Name][i];
            }
        }

        protected override string OutputParameterType(int i, InvocationParameterBindings paramBindings)
        {
            if(paramBindings is RuleInvocationParameterBindings)
            {
                RuleInvocationParameterBindings ruleParamBindings = (RuleInvocationParameterBindings)paramBindings;
                return rulesToOutputTypes[ruleParamBindings.Name][i];
            }
            else
            {
                SequenceInvocationParameterBindings seqParamBindings = (SequenceInvocationParameterBindings)paramBindings;
                return sequencesToOutputTypes[seqParamBindings.Name][i];
            }
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
