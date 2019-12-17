/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr.sequenceParser
{
    /// <summary>
    /// An evironment class for the sequence parser, gives it access to the entitites and types that can be referenced in the sequence.
    /// </summary>
    public abstract class SequenceParserEnvironment
    {
        /// <summary>
        /// The model used in the specification
        /// </summary>
        private readonly IGraphModel model;
        public IGraphModel Model { get { return model; } }

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package. 
        /// Also null in case of an interpreted sequence, only compiled sequences may appear within a package.
        /// </summary>
        public virtual String PackageContext { get { return null; } }

        /// <summary>
        /// Gives the rule of the match this stands for in the if clause of the debug match event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string RuleOfMatchThis { get { return null; } }

        /// <summary>
        /// Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string TypeOfGraphElementThis { get { return null; } }


        protected SequenceParserEnvironment(IGraphModel model)
        {
            this.model = model;
        }


        abstract public bool IsSequenceName(String ruleOrSequenceName, String package);

        abstract public SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph);


        abstract public RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph);

        abstract public bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage);


        abstract public bool IsProcedureName(String procedureName, String package);

        abstract public string GetProcedureNames();

        abstract public ProcedureInvocationParameterBindings CreateProcedureInvocationParameterBindings(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars);

        static public ProcedureInvocationParameterBindings CreateProcedureMethodInvocationParameterBindings(String procedureName,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(null,
                argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

            paramBindings.Name = procedureName;

            return paramBindings;
        }


        abstract public bool IsFunctionName(String functionName, String package);

        abstract public string GetFunctionNames();

        abstract public FunctionInvocationParameterBindings CreateFunctionInvocationParameterBindings(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs);

        static public FunctionInvocationParameterBindings CreateFunctionMethodInvocationParameterBindings(String functionMethodName,
            List<SequenceExpression> argExprs)
        {
            FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(null,
                argExprs.ToArray(), new object[argExprs.Count]);

            paramBindings.Name = functionMethodName;
            paramBindings.ReturnType = "";

            return paramBindings;
        }
    }
}
