/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An object representing an executable rule.
    /// The core functions used by the sequences/GrGen itself, the IAction interface contains a lot more convenience helper functions for direct rule application that could be used from own code (and are a bit faster due to missing debug events).
    /// </summary>
    public interface IActionCore : INamed
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        new string Name { get; }

        /// <summary>
        /// null if this is a global action, otherwise the package the action is contained in.
        /// </summary>
        new string Package { get; }

        /// <summary>
        /// The name of the action in case of a global type,
        /// the name of the action prefixed by the name of the package otherwise.
        /// </summary>
        new string PackagePrefixedName { get; }

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
        /// Filters the matches found by this action 
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="matches">The matches to inspect and filter</param>
        /// <param name="filter">The filter to apply</param>
        void Filter(IActionExecutionEnvironment actionEnv, IMatches matches, FilterCallWithArguments filter);

        /// <summary>
        /// Returns a list of arrays with the given number of list elements;
        /// the array size is as needed for storing the return values.
        /// The list/its members are only valid until the next allocate or graph rewrite with this rule.
        /// Internal memory optimization.
        /// </summary>
        List<object[]> Reserve(int numReturns);
    }

    /// <summary>
    /// An object representing an executable rule.
    /// </summary>
    public interface IAction : IActionCore
    {
        /// <summary>
        /// Performs the rule specific modifications to the current graph of the graph processing environment with all of the given matches.
        /// The graph and match object must have the correct type for the used backend.
        /// No OnRewritingNextMatch events are triggered by this function (needed for visual debugging), in contrast to the Replace function of the action execution environment.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="matches">The matches of the rule to apply.</param>
        /// <returns>A list of arrays of objects returned by the applications of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        List<object[]> ModifyAll(IActionExecutionEnvironment actionEnv, IMatches matches);

        /// <summary>
        /// Tries to apply this rule to the given graph processing environment/its current graph once.
        /// Only applicable for parameterless rules. Shows better performance than the normal Apply called without parameters.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>A possibly empty array of objects returned by the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] Apply(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Tries to apply this rule to the given processing environment/its current graph once.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// The null parameter check is omitted also.
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
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>A list of arrays of objects (the arrays may be empty) returned by the last application of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        List<object[]> ApplyAll(int maxMatches, IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Tries to apply this rule to all occurrences in the current graph of the graph processing environment "at once".
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// The null parameter check is omitted also.
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
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// The null parameter check is omitted also.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        bool ApplyStar(IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyPlus called without parameters.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// The null parameter check is omitted also.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        bool ApplyPlus(IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to The action execution environment/its current graph at most max times.
        /// Only applicable for parameterless rules. Shows better performance than the normal ApplyMinMax called without parameters.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IActionExecutionEnvironment actionEnv, int min, int max);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph at most max times.
        /// No Matched/Finished events are triggered by this function (needed for visual debugging).
        /// The null parameter check is omitted also.
        /// </summary>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        bool ApplyMinMax(IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters);
    }
}
