/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define USE_HIGHPERFORMANCE_COUNTER

using System;
using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing an executable rule.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        String Name { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        String Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        String PackagePrefixedName { get; }

        /// <summary>
        /// The RulePattern object from which this IAction object has been created.
        /// </summary>
        IRulePattern RulePattern { get; }


        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern in the current graph of the graph processing environment.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        IMatches Match(IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters);

        /// <summary>
        /// Performs the rule specific modifications to the current graph of the graph processing environment with the given match.
        /// The graph and match object must have the correct type for the used backend (and this action).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="match">The match of the rule to apply.</param>
        /// <returns>An array of objects returned by the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] Modify(IActionExecutionEnvironment actionEnv, IMatch match);

        /// <summary>
        /// Performs the rule specific modifications to the current graph of the graph processing environment with all of the given matches.
        /// The graph and match object must have the correct type for the used backend.
        /// No OnRewritingNextMatch events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="matches">The matches of the rule to apply.</param>
        /// <returns>A list of arrays of objects returned by the last application of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        List<object[]> ModifyAll(IActionExecutionEnvironment actionEnv, IMatches matches);

        /// <summary>
        /// Returns a list of arrays with the given number of list elements;
        /// the array size is as needed for storing the return values.
        /// The list/its members are only valid until the next allocate or graph rewrite with this rule.
        /// </summary>
        List<object[]> Reserve(int numReturns);

        /// <summary>
        /// Tries to apply this rule to the given graph processing environment/its current graph once.
        /// Only applicable for parameterless rules. Shows better performance than the normal Apply called without parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] Apply(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Tries to apply this rule to the given processing environment/its current graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] Apply(IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Tries to apply this rule to all occurrences in the current graph of the graph processing environment "at once".
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyAll called without parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>A list of arrays of objects (the arrays may be empty) returned by the last application of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        List<object[]> ApplyAll(int maxMatches, IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Tries to apply this rule to all occurrences in the current graph of the graph processing environment "at once".
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A list of arrays of objects (the arrays may be empty) returned by the last application of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        List<object[]> ApplyAll(int maxMatches, IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyStar called without parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyPlus called without parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to The action execution environment/its current graph at most max times.
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyMinMax called without parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IActionExecutionEnvironment actionEnv, int min, int max);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph at most max times.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters);

        /// <summary>
        /// Filters the matches found by this action 
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="matches">The matches to inspect and filter</param>
        /// <param name="filter">The filter to apply</param>
        void Filter(IActionExecutionEnvironment actionEnv, IMatches matches, FilterCall filter);
    }

    /// <summary>
    /// An object representing an executable sequence.
    /// </summary>
    public interface ISequenceDefinition
    {
        /// <summary>
        /// The name of the sequence
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The annotations of the sequence
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }

        /// <summary>
        /// Applies this sequence.
        /// </summary>
        /// <param name="sequenceInvocation">Sequence invocation object for this sequence application,
        ///     containing the input parameter sources and output parameter targets</param>
        /// <param name="procEnv">The graph processing environment on which this sequence is to be applied.
        ///     Contains especially the graph on which this sequence is to be applied.
        ///     The rules will only be chosen during the Sequence object instantiation, so
        ///     exchanging rules will have no effect for already existing Sequence objects.</param>
        /// <param name="env">The execution environment giving access to the names and user interface (null if not available)</param>
        /// <returns>True, iff the sequence succeeded</returns>
        bool Apply(SequenceInvocationParameterBindings sequenceInvocation,
            IGraphProcessingEnvironment procEnv);
    }

    /// <summary>
    /// An object representing an executable procedure.
    /// </summary>
    public interface IProcedureDefinition
    {
        /// <summary>
        /// The name of the procedure
        /// </summary>
        String Name { get; }

        /// <summary>
        /// The annotations of the procedure
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        string Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        string PackagePrefixedName { get; }

        /// <summary>
        /// Names of the procedure parameters.
        /// </summary>
        string[] InputNames { get; }

        /// <summary>
        /// The GrGen types of the procedure parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// The GrGen types of the procedure return values.
        /// </summary>
        GrGenType[] Outputs { get; }

        /// <summary>
        // Tells whether the procedure is an externally defined one or an internal one
        /// </summary>
        bool IsExternal { get; }

        /// <summary>
        /// Applies this procedure with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns an array of output values.
        /// Attention: at the next call of Apply, the array returned from previous call is overwritten with the new return values.
        /// </summary>
        object[] Apply(IActionExecutionEnvironment actionEnv, IGraph graph, ProcedureInvocationParameterBindings paramBindings);
    }

    /// <summary>
    /// An object representing an executable function.
    /// </summary>
    public interface IFunctionDefinition
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The annotations of the function
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Annotations { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        string Package { get; }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        string PackagePrefixedName { get; }

        /// <summary>
        /// Names of the function parameters.
        /// </summary>
        string[] InputNames { get; }

        /// <summary>
        /// The GrGen types of the function parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// The GrGen type of the function return value.
        /// </summary>
        GrGenType Output { get; }

        /// <summary>
        // Tells whether the function is an externally defined one or an internal one
        /// </summary>
        bool IsExternal { get; }

        /// <summary>
        /// Applies this function with the given action environment on the given graph.
        /// Takes the parameters from paramBindings as inputs.
        /// Returns the one output value.
        /// </summary>
        object Apply(IActionExecutionEnvironment actionEnv, IGraph graph, FunctionInvocationParameterBindings paramBindings);
    }
}
