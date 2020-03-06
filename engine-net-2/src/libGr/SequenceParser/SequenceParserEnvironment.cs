/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

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
        public IGraphModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// The name of the package the sequence is contained in (defining some context), null if it is not contained in a package. 
        /// Also null in case of an interpreted sequence, only compiled sequences may appear within a package.
        /// </summary>
        public virtual String PackageContext
        {
            get { return null; }
        }

        /// <summary>
        /// Gives the rule of the match this stands for in the if clause of the debug match event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string RuleOfMatchThis
        {
            get { return null; }
        }

        /// <summary>
        /// Gives the graph element type of the graph element this stands for in the if clause of the debug new/delete/retype/set-attributes event.
        /// Null if the context is not the one of a debug event condition.
        /// </summary>
        public virtual string TypeOfGraphElementThis
        {
            get { return null; }
        }


        protected SequenceParserEnvironment(IGraphModel model)
        {
            this.model = model;
        }


        abstract public bool IsSequenceName(String ruleOrSequenceName, String package);

        abstract public SequenceSequenceCall CreateSequenceSequenceCall(String sequenceName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special);


        abstract public SequenceRuleCall CreateSequenceRuleCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, List<SequenceFilterCall> filters, bool isRuleForMultiRuleAllCallReturningArrays);

        abstract public SequenceRuleAllCall CreateSequenceRuleAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test,
            bool chooseRandom, SequenceVariable varChooseRandom,
            bool chooseRandom2, SequenceVariable varChooseRandom2, bool choice, List<SequenceFilterCall> filters);

        abstract public SequenceRuleCountAllCall CreateSequenceRuleCountAllCall(String ruleName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph,
            bool special, bool test, SequenceVariable countResult, List<SequenceFilterCall> filters);

        abstract public bool IsFilterFunctionName(String filterFunctionName, String package, String ruleName, String actionPackage);


        abstract public bool IsProcedureName(String procedureName, String package);

        abstract public string GetProcedureNames();

        abstract public SequenceComputationProcedureCall CreateSequenceComputationProcedureCall(String procedureName, String packagePrefix,
            List<SequenceExpression> argExprs, List<SequenceVariable> returnVars);

        public SequenceComputationProcedureMethodCall CreateSequenceComputationProcedureMethodCall(SequenceExpression targetExpr,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            return new SequenceComputationProcedureMethodCall(targetExpr, procedureName, argExprs, returnVars);
        }

        public SequenceComputationProcedureMethodCall CreateSequenceComputationProcedureMethodCall(SequenceVariable targetVar,
            String procedureName, List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
        {
            return new SequenceComputationProcedureMethodCall(targetVar, procedureName, argExprs, returnVars);
        }


        abstract public bool IsFunctionName(String functionName, String package);

        abstract public string GetFunctionNames();

        abstract public SequenceExpressionFunctionCall CreateSequenceExpressionFunctionCall(String functionName, String packagePrefix,
            List<SequenceExpression> argExprs);

        public SequenceExpressionFunctionMethodCall CreateSequenceExpressionFunctionMethodCall(SequenceExpression fromExpr,
            String functionMethodName, List<SequenceExpression> argExprs)
        {
            return new SequenceExpressionFunctionMethodCall(fromExpr, functionMethodName, argExprs);
        }
    }
}
