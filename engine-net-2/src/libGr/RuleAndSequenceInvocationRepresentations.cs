/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing a rule or sequence invocation.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public abstract class InvocationParameterBindings
    {
        /// <summary>
        /// The name of the rule or the sequence. Used for generation, where the rule or sequence representation objects do not exist yet.
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
        /// An array of variables used for the return values.
        /// Might be empty if the rule/sequence caller is not interested in available returns values.
        /// </summary>
        public SequenceVariable[] ReturnVars;

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
        /// <param name="returnVars">An array of variables used for the return values</param>
        public InvocationParameterBindings(SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars)
        {
            if(argExprs.Length != arguments.Length)
                throw new ArgumentException("Lengths of argument expression array and argument array do not match");
            foreach(SequenceVariable var in returnVars)
                if(var==null) throw new Exception("Null entry in return vars");
            Name = "<Unknown rule/sequence>";
            ArgumentExpressions = argExprs;
            ReturnVars = returnVars;
            Arguments = arguments;
        }

        public void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            foreach(SequenceExpression seqExpr in ArgumentExpressions)
                seqExpr.GetLocalVariables(variables);
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
    public class RuleInvocationParameterBindings : InvocationParameterBindings
    {
        /// <summary>
        /// The IAction instance to be used
        /// </summary>
        public IAction Action;

        /// <summary>
        /// Instantiates a new RuleInvocationParameterBindings object
        /// </summary>
        /// <param name="action">The IAction instance to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public RuleInvocationParameterBindings(IAction action,
            SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars)
            : base(argExprs, arguments, returnVars)
        {
            Action = action;
            if(action != null) Name = action.Name;
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
            return copy;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation.
    /// It stores the input arguments (values),
    /// tells with which sequence expressions to compute them,
    /// and where (which variables) to store the output values.
    /// </summary>
    public class SequenceInvocationParameterBindings : InvocationParameterBindings
    {
        /// <summary>
        /// The defined sequence to be used
        /// </summary>
        public SequenceDefinition SequenceDef;

        /// <summary>
        /// Instantiates a new SequenceInvocationParameterBindings object
        /// </summary>
        /// <param name="sequenceDef">The defined sequence to be used</param>
        /// <param name="argExprs">An array of expressions used to compute the arguments</param>
        /// <param name="arguments">An array of arguments.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public SequenceInvocationParameterBindings(SequenceDefinition sequenceDef,
            SequenceExpression[] argExprs, object[] arguments, SequenceVariable[] returnVars)
            : base(argExprs, arguments, returnVars)
        {
            SequenceDef = sequenceDef;
            if(sequenceDef != null) Name = sequenceDef.SequenceName;
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
            return copy;
        }
    }
}
