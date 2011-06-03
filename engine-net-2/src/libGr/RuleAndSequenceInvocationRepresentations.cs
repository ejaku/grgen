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
    /// It tells from where (which variables/constants) to get the input values
    /// and where (which variables) to store the output values.
    /// </summary>
    public abstract class InvocationParameterBindings
    {
        /// <summary>
        /// The name of the rule or the sequence. Used for generation, where the rule or sequence representation objects do not exist yet.
        /// </summary>
        public String Name;

        /// <summary>
        /// An array of variables used for the parameters.
        /// It must have the same length as Parameters.
        /// If an entry is null, the according entry in parameters is used unchanged.
        /// </summary>
        public SequenceVariable[] ParamVars;

        /// <summary>
        /// An array of variables used for the return values.
        /// Might be empty if the rule/sequence caller is not interested in available returns values.
        /// </summary>
        public SequenceVariable[] ReturnVars;

        /// <summary>
        /// Buffer to store parameters used by libGr to avoid unneccessary memory allocation.
        /// Also holds constant parameters at the positions where ParamVars has null entries.
        /// </summary>
        public object[] Parameters;

        /// <summary>
        /// Instantiates a new InvocationParameterBindings object
        /// </summary>
        /// <param name="paramVars">An array of variable used for the parameters</param>
        /// <param name="paramConsts">An array of constants used for the parameters.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public InvocationParameterBindings(SequenceVariable[] paramVars, object[] paramConsts, SequenceVariable[] returnVars)
        {
            if(paramVars.Length != paramConsts.Length)
                throw new ArgumentException("Lengths of variable and constant parameter array do not match");
            foreach(SequenceVariable var in returnVars)
                if(var==null) throw new Exception("Null entry in return vars");
            Name = "<Unknown rule/sequence>";
            ParamVars = paramVars;
            ReturnVars = returnVars;
            Parameters = paramConsts;
        }

        public void GetLocalVariables(Dictionary<SequenceVariable, SetValueType> variables)
        {
            foreach(SequenceVariable seqVar in ParamVars)
                seqVar.GetLocalVariables(variables);
            foreach(SequenceVariable seqVar in ReturnVars)
                seqVar.GetLocalVariables(variables);
        }
    }

    /// <summary>
    /// An object representing a rule invocation.
    /// It tells from where (which variables/constants) to get the input values
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
        /// <param name="paramVars">An array of variable used for the parameters</param>
        /// <param name="paramConsts">An array of constants used for the parameters.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public RuleInvocationParameterBindings(IAction action,
            SequenceVariable[] paramVars, object[] paramConsts, SequenceVariable[] returnVars)
            : base(paramVars, paramConsts, returnVars)
        {
            Action = action;
            if(action != null) Name = action.Name;
        }

        public RuleInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            RuleInvocationParameterBindings copy = (RuleInvocationParameterBindings)MemberwiseClone();
            copy.ParamVars = new SequenceVariable[ParamVars.Length];
            for(int i=0; i<ParamVars.Length;++i)
                copy.ParamVars[i] = ParamVars[i].Copy(originalToCopy);
            copy.ReturnVars = new SequenceVariable[ReturnVars.Length];
            for(int i = 0; i < ReturnVars.Length; ++i)
                copy.ReturnVars[i] = ReturnVars[i].Copy(originalToCopy);
            copy.Parameters = new object[Parameters.Length];
            for(int i = 0; i < Parameters.Length; ++i)
                copy.Parameters[i] = Parameters[i];
            return copy;
        }
    }

    /// <summary>
    /// An object representing a sequence invocation.
    /// It tells from where (which variables/constants) to get the input values
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
        /// <param name="paramVars">An array of variable used for the parameters</param>
        /// <param name="paramConsts">An array of constants used for the parameters.</param>
        /// <param name="returnVars">An array of variables used for the return values</param>
        public SequenceInvocationParameterBindings(SequenceDefinition sequenceDef,
            SequenceVariable[] paramVars, object[] paramConsts, SequenceVariable[] returnVars)
            : base(paramVars, paramConsts, returnVars)
        {
            SequenceDef = sequenceDef;
            if(sequenceDef != null) Name = sequenceDef.SequenceName;
        }

        public SequenceInvocationParameterBindings Copy(Dictionary<SequenceVariable, SequenceVariable> originalToCopy)
        {
            SequenceInvocationParameterBindings copy = (SequenceInvocationParameterBindings)MemberwiseClone();
            copy.ParamVars = new SequenceVariable[ParamVars.Length];
            for(int i = 0; i < ParamVars.Length; ++i)
                copy.ParamVars[i] = ParamVars[i].Copy(originalToCopy);
            copy.ReturnVars = new SequenceVariable[ReturnVars.Length];
            for(int i = 0; i < ReturnVars.Length; ++i)
                copy.ReturnVars[i] = ReturnVars[i].Copy(originalToCopy);
            copy.Parameters = new object[Parameters.Length];
            for(int i = 0; i < Parameters.Length; ++i)
                copy.Parameters[i] = Parameters[i];
            return copy;
        }
    }
}
