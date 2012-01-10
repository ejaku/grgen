/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define USE_HIGHPERFORMANCE_COUNTER

using System;

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
        /// <returns>An array of objects returned by the last applicance of the rule.
        /// It is only valid until the next graph rewrite with this rule.</returns>
        object[] ModifyAll(IActionExecutionEnvironment actionEnv, IMatches matches);

        /// <summary>
        /// Tries to apply this rule to the given graph processing environment/its current graph once.
        /// The rule may not require any parameters.
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
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] ApplyAll(int maxMatches, IActionExecutionEnvironment actionEnv);

        /// <summary>
        /// Tries to apply this rule to all occurrences in the current graph of the graph processing environment "at once".
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="maxMatches">The maximum number of matches to be rewritten.</param>
        /// <param name="actionEnv">The action execution environment, esp. giving access to the host graph.</param>
        /// <param name="parameters">An array of parameters (nodes, edges, values) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>A possibly empty array of objects returned by the last applicance of the rule,
        /// which is only valid until the next graph rewrite with this rule,
        /// or null, if no match was found.</returns>
        object[] ApplyAll(int maxMatches, IActionExecutionEnvironment actionEnv, params object[] parameters);

        /// <summary>
        /// Applies this rule to the given processing environment/its current graph as often as possible.
        /// The rule may not require any parameters.
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
        /// The rule may not require any parameters.
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
        /// The rule may not require any parameters.
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
    }
}
