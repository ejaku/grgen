/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence type checking (with/giving access to model and action signatures).
    /// Concrete subclass for interpreted sequences.
    /// </summary>
    public class SequenceCheckingEnvironmentInterpreted : SequenceCheckingEnvironment
    {
        // constructor for interpreted sequences
        public SequenceCheckingEnvironmentInterpreted(IActions actions)
        {
            this.actions = actions;
        }

        // the information available if this is an interpreted sequence 

        private readonly IActions actions;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IGraphModel Model
        {
            get { return actions.Graph.Model; }
        }

        public override string TypeOfTopLevelEntityInRule(string ruleName, string entityName)
        {
            IAction rule = actions.GetAction(ruleName);
            if(rule==null)
                throw new SequenceParserException(ruleName, SequenceParserError.UnknownRule);

            foreach(IPatternNode node in rule.RulePattern.PatternGraph.Nodes)
            {
                if(node.UnprefixedName == entityName)
                    return TypesHelper.DotNetTypeToXgrsType(node.Type);
            }

            foreach(IPatternEdge edge in rule.RulePattern.PatternGraph.Edges)
            {
                if(edge.UnprefixedName == entityName)
                    return TypesHelper.DotNetTypeToXgrsType(edge.Type);
            }

            foreach(IPatternVariable var in rule.RulePattern.PatternGraph.Variables)
            {
                if(var.UnprefixedName == entityName)
                    return TypesHelper.DotNetTypeToXgrsType(var.Type);
            }

            throw new SequenceParserException(ruleName, entityName, SequenceParserError.UnknownPatternElement);
        }

        public override string TypeOfMemberOrAttribute(string matchOrGraphElementType, string memberOrAttribute)
        {
            if(matchOrGraphElementType.StartsWith("match<class "))
            {
                MatchClassFilterer matchClass = actions.GetMatchClass(TypesHelper.GetMatchClassName(matchOrGraphElementType));
                IPatternElement element = matchClass.info.GetPatternElement(memberOrAttribute);
                GrGenType elementType = element.Type;
                return TypesHelper.DotNetTypeToXgrsType(elementType);
            }
            else if(matchOrGraphElementType.StartsWith("match<"))
            {
                IAction action = actions.GetAction(TypesHelper.GetRuleName(matchOrGraphElementType));
                IPatternElement element = action.RulePattern.PatternGraph.GetPatternElement(memberOrAttribute);
                GrGenType elementType = element.Type;
                return TypesHelper.DotNetTypeToXgrsType(elementType);
            }
            else
            {
                GrGenType graphElementType = TypesHelper.GetNodeOrEdgeType(matchOrGraphElementType, Model);
                AttributeType attributeType = graphElementType.GetAttributeType(memberOrAttribute);
                return TypesHelper.AttributeTypeToXgrsType(attributeType);
            }
        }

        protected override int NumInputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return SequenceBase.GetAction(ruleInvocation).RulePattern.Inputs.Length;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceSequenceCallInterpreted seqInvocation = (SequenceSequenceCallInterpreted)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.InputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return seqDef.SeqInfo.ParameterTypes.Length;
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Inputs.Length;
                else
                    return ((SequenceComputationProcedureCallInterpreted)procInvocation).ProcedureDef.Inputs.Length;
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetFunctionMethod(funcInvocation.Name).Inputs.Length;
                else
                    return ((SequenceExpressionFunctionCallInterpreted)funcInvocation).FunctionDef.Inputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override int NumOutputParameters(Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return SequenceBase.GetAction(ruleInvocation).RulePattern.Outputs.Length;
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceSequenceCallInterpreted seqInvocation = (SequenceSequenceCallInterpreted)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.OutputVariables.Length;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return seqDef.SeqInfo.OutParameterTypes.Length;
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return ownerType.GetProcedureMethod(procInvocation.Name).Outputs.Length;
                else
                    return ((SequenceComputationProcedureCallInterpreted)procInvocation).ProcedureDef.Outputs.Length;
            }
            throw new Exception("Internal error");
        }

        protected override string InputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return TypesHelper.DotNetTypeToXgrsType(SequenceBase.GetAction(ruleInvocation).RulePattern.Inputs[i]);
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceSequenceCallInterpreted seqInvocation = (SequenceSequenceCallInterpreted)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.InputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.ParameterTypes[i]);
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(((SequenceComputationProcedureCallInterpreted)procInvocation).ProcedureDef.Inputs[i]);
            }
            else if(invocation is FunctionInvocation)
            {
                FunctionInvocation funcInvocation = (FunctionInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetFunctionMethod(funcInvocation.Name).Inputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(((SequenceExpressionFunctionCallInterpreted)funcInvocation).FunctionDef.Inputs[i]);
            }
            throw new Exception("Internal error");
        }

        protected override string OutputParameterType(int i, Invocation invocation, GrGenType ownerType)
        {
            if(invocation is RuleInvocation)
            {
                RuleInvocation ruleInvocation = (RuleInvocation)invocation;
                return TypesHelper.DotNetTypeToXgrsType(SequenceBase.GetAction(ruleInvocation).RulePattern.Outputs[i]);
            }
            else if(invocation is SequenceInvocation)
            {
                SequenceSequenceCallInterpreted seqInvocation = (SequenceSequenceCallInterpreted)invocation;
                if(seqInvocation.SequenceDef is SequenceDefinitionInterpreted)
                {
                    SequenceDefinitionInterpreted seqDef = (SequenceDefinitionInterpreted)seqInvocation.SequenceDef;
                    return seqDef.OutputVariables[i].Type;
                }
                else
                {
                    SequenceDefinitionCompiled seqDef = (SequenceDefinitionCompiled)seqInvocation.SequenceDef;
                    return TypesHelper.DotNetTypeToXgrsType(seqDef.SeqInfo.OutParameterTypes[i]);
                }
            }
            else if(invocation is ProcedureInvocation)
            {
                ProcedureInvocation procInvocation = (ProcedureInvocation)invocation;
                if(ownerType != null)
                    return TypesHelper.DotNetTypeToXgrsType(ownerType.GetProcedureMethod(procInvocation.Name).Outputs[i]);
                else
                    return TypesHelper.DotNetTypeToXgrsType(((SequenceComputationProcedureCallInterpreted)procInvocation).ProcedureDef.Outputs[i]);
            }
            throw new Exception("Internal error");
        }

        protected override bool IsMatchClassExisting(SequenceFilterCall sequenceFilterCall)
        {
            SequenceFilterCallInterpreted sequenceFilterCallInterpreted = (SequenceFilterCallInterpreted)sequenceFilterCall;
            return sequenceFilterCallInterpreted.MatchClass != null;
        }

        protected override string GetMatchClassName(SequenceFilterCall sequenceFilterCall)
        {
            SequenceFilterCallInterpreted sequenceFilterCallInterpreted = (SequenceFilterCallInterpreted)sequenceFilterCall;
            return sequenceFilterCallInterpreted.MatchClass.info.PackagePrefixedName;
        }

        public override bool IsRuleImplementingMatchClass(string rulePackagePrefixedName, string matchClassPackagePrefixedName)
        {
            return actions.GetAction(rulePackagePrefixedName).RulePattern.GetImplementedMatchClass(matchClassPackagePrefixedName) != null;
        }
    }
}
