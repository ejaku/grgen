/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule or sequence or procedure or function invocation.
    /// It stores the input arguments (values) and
    /// tells with which sequence expressions to compute them.
    /// </summary>
    public class InvocationParameterBindings
    {
        /// <summary>
        /// The name of the rule or sequence or procedure or function.
        /// Used for generation, where the rule or sequence or procedure or function representation objects do not exist yet.
        /// </summary>
        public String Name;

        /// <summary>
        /// An array of expressions used to compute the input arguments.
        /// It must have the same length as Arguments.
        /// If an entry is null, the according entry in Arguments is used unchanged.
        /// Otherwise the entry in Arguments is filled with the evaluation result of the expression.
        /// The sequence parser generates argument expressions for every entry;
        /// they may be omitted by a user assembling an invocation at API level.
        /// </summary>
        public SequenceExpression[] ArgumentExpressions;

        /// <summary>
        /// Buffer to store the argument values for the call;
        /// used by libGr to avoid unneccessary memory allocations.
        /// </summary>
        public object[] Arguments;

        /// <summary>
        /// Instantiates a new InvocationParameterBindings object
        /// </summary>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        public InvocationParameterBindings(SequenceExpression[] argExprs, object[] arguments)
        {
            if(argExprs.Length != arguments.Length)
                throw new ArgumentException("Lengths of argument expression array and argument array do not match");
            Name = "<Unknown rule/sequence/function>";
            ArgumentExpressions = argExprs;
            Arguments = arguments;
        }

        public virtual void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression seqExpr in ArgumentExpressions)
                seqExpr.GetLocalVariables(variables, containerConstructors);
        }
    }

    /// <summary>
    /// An object representing a rule or sequence or procedure invocation.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public abstract class InvocationParameterBindingsWithReturns : InvocationParameterBindings
    {
        /// <summary>
        /// An array of variables used for the return values.
        /// Might be empty if the rule/sequence/procedure caller is not interested in available returns values.
        /// </summary>
        public SequenceVariable[] ReturnVars;

        /// <summary>
        /// Instantiates a new InvocationParameterBindingsWithReturns object
        /// </summary>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public InvocationParameterBindingsWithReturns(SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars)
            : base(argExprs, arguments)
        {
            foreach(SequenceVariable var in returnVars)
                if(var==null) throw new Exception("Null entry in return vars");
            ReturnVars = returnVars;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            foreach(SequenceExpression seqExpr in ArgumentExpressions)
                seqExpr.GetLocalVariables(variables, containerConstructors);
            foreach(SequenceVariable seqVar in ReturnVars)
                seqVar.GetLocalVariables(variables);
        }
    }

    /// <summary>
    /// An object representing a rule invocation.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public class RuleInvocationParameterBindings : InvocationParameterBindingsWithReturns
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public IAction Action;

        /// <summary>
        /// The subgraph to be switched to for rule execution
        /// </summary>
        public SequenceVariable Subgraph;

        /// <summary>
        /// Instantiates a new RuleInvocationParameterBindings object
        /// </summary>
        /// <param name="action">The IAction instance to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public RuleInvocationParameterBindings(IAction action,
            SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars, SequenceVariable subgraph)
            : base(argExprs, arguments, returnVars)
        {
            Action = action;
            if(action != null) Name = action.Name;
            Subgraph = subgraph;
        }

        public RuleInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            RuleInvocationParameterBindings copy = (RuleInvocationParameterBindings)MemberwiseClone();
            copy.ArgumentExpressions = new SequenceExpression[ArgumentExpressions.Length];
            for(int i=0; i<ArgumentExpressions.Length;++i)
                copy.ArgumentExpressions[i] = ArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            copy.ReturnVars = new SequenceVariable[ReturnVars.Length];
            for(int i = 0; i < ReturnVars.Length; ++i)
                copy.ReturnVars[i] = ReturnVars[i].Copy(originalToCopy, procEnv);
            copy.Arguments = new object[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
                copy.Arguments[i] = Arguments[i];
            if(copy.Subgraph != null)
                copy.Subgraph = Subgraph.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            if(Subgraph != null)
                Subgraph.GetLocalVariables(variables);
        }
    }

    /// <summary>
    /// An object representing a sequence invocation.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public class SequenceInvocationParameterBindings : InvocationParameterBindingsWithReturns
    {
        /// <summary>
        /// The defined sequence to be used
        /// </summary>
        public SequenceDefinition SequenceDef;

        /// <summary>
        /// The subgraph to be switched to for sequence execution
        /// </summary>
        public SequenceVariable Subgraph;

        /// <summary>
        /// Instantiates a new SequenceInvocationParameterBindings object
        /// </summary>
        /// <param name="sequenceDef">The defined sequence to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public SequenceInvocationParameterBindings(SequenceDefinition sequenceDef,
            SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars, SequenceVariable subgraph)
            : base(argExprs, arguments, returnVars)
        {
            SequenceDef = sequenceDef;
            if(sequenceDef != null) Name = sequenceDef.SequenceName;
            Subgraph = subgraph;
        }

        public SequenceInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            SequenceInvocationParameterBindings copy = (SequenceInvocationParameterBindings)MemberwiseClone();
            copy.ArgumentExpressions = new SequenceExpression[ArgumentExpressions.Length];
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
                copy.ArgumentExpressions[i] = ArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            copy.ReturnVars = new SequenceVariable[ReturnVars.Length];
            for(int i = 0; i < ReturnVars.Length; ++i)
                copy.ReturnVars[i] = ReturnVars[i].Copy(originalToCopy, procEnv);
            copy.Arguments = new object[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
                copy.Arguments[i] = Arguments[i];
            if(copy.Subgraph != null)
                copy.Subgraph = Subgraph.Copy(originalToCopy, procEnv);
            return copy;
        }

        public override void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables,
            List<SequenceExpressionContainerConstructor> containerConstructors)
        {
            base.GetLocalVariables(variables, containerConstructors);
            if(Subgraph != null)
                Subgraph.GetLocalVariables(variables);
        }
    }

    /// <summary>
    /// An object representing a procedure.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public class ProcedureInvocationParameterBindings : InvocationParameterBindingsWithReturns
    {
        /// <summary>
        /// The procedure to be used
        /// </summary>
        public ProcedureInfo ProcedureDef;

        /// <summary>
        /// Instantiates a new ProcedureInvocationParameterBindings object
        /// </summary>
        /// <param name="procedureDef">The defined procedure to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public ProcedureInvocationParameterBindings(ProcedureInfo procedureDef,
            SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars)
            : base(argExprs, arguments, returnVars)
        {
            ProcedureDef = procedureDef;
            if(procedureDef != null) Name = procedureDef.name;
        }

        public ProcedureInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            ProcedureInvocationParameterBindings copy = (ProcedureInvocationParameterBindings)MemberwiseClone();
            copy.ArgumentExpressions = new SequenceExpression[ArgumentExpressions.Length];
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
                copy.ArgumentExpressions[i] = ArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            copy.ReturnVars = new SequenceVariable[ReturnVars.Length];
            for(int i = 0; i < ReturnVars.Length; ++i)
                copy.ReturnVars[i] = ReturnVars[i].Copy(originalToCopy, procEnv);
            copy.Arguments = new object[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
                copy.Arguments[i] = Arguments[i];
            return copy;
        }
    }

    /// <summary>
    /// An object representing a function invocation.
    /// It stores the input arguments (values) and
    /// tells with which function to compute them.
    /// </summary>
    public class FunctionInvocationParameterBindings : InvocationParameterBindings
    {
        /// <summary>
        /// The function to be used
        /// </summary>
        public FunctionInfo FunctionDef;

        /// <summary>
        /// The type returned
        /// </summary>
        public string ReturnType;

        /// <summary>
        /// Instantiates a new FunctionInvocationParameterBindings object
        /// </summary>
        /// <param name="functionDef">The defined function to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        public FunctionInvocationParameterBindings(FunctionInfo functionDef,
            SequenceExpression[] argExprs, object[] arguments)
            : base(argExprs, arguments)
        {
            FunctionDef = functionDef;
            if(functionDef != null) Name = functionDef.name;
        }

        public FunctionInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy, IGraphProcessingEnvironment procEnv)
        {
            FunctionInvocationParameterBindings copy = (FunctionInvocationParameterBindings)MemberwiseClone();
            copy.ArgumentExpressions = new SequenceExpression[ArgumentExpressions.Length];
            for(int i = 0; i < ArgumentExpressions.Length; ++i)
                copy.ArgumentExpressions[i] = ArgumentExpressions[i].CopyExpression(originalToCopy, procEnv);
            copy.Arguments = new object[Arguments.Length];
            for(int i = 0; i < Arguments.Length; ++i)
                copy.Arguments[i] = Arguments[i];
            return copy;
        }
    }
}
