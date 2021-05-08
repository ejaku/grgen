/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System.Collections.Generic;

namespace de.unika.ipd.grGen.libGr
{
    #region ActionExecutionDelegates

    /// <summary>
    /// Represents a method called when execution of a pattern matching construct begins.
    /// </summary>
    /// <param name="patternMatchingConstruct">The pattern matching construct.</param>
    public delegate void BeginExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct);

    /// <summary>
    /// Represents a method called after all requested matches of a multi action or an action have been matched,
    /// yet before filtering/selection (single element array in case of a single rule/test application).
    /// </summary>
    /// <param name="matches">The matches found (an array of matches objects, one matches object per rule).</param>
    public delegate void PreMatchHandler(IMatches[] matches);

    /// <summary>
    /// Represents a method called after all requested matches of an action have been matched.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="match">If not null, specifies the one current match from the matches 
    /// (to highlight the currently processed match during backtracking and the for matches loop).</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterMatchHandler(IMatches matches, IMatch match, bool special);

    /// <summary>
    /// Represents a method called before the rewrite step of an action, when at least one match has been found.
    /// </summary>
    /// <param name="matches">The matches found.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void BeforeFinishHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called during rewriting a set of matches before the next match is rewritten.
    /// It is not fired before rewriting the first match.
    /// </summary>
    public delegate void RewriteNextMatchHandler();

    /// <summary>
    /// Represents a method called after the rewrite step of a rule.
    /// </summary>
    /// <param name="matches">The matches found.
    /// This may contain invalid entries, because parts of the matches may have been deleted.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    public delegate void AfterFinishHandler(IMatches matches, bool special);

    /// <summary>
    /// Represents a method called when execution of a pattern matching construct ends.
    /// </summary>
    /// <param name="patternMatchingConstruct">The pattern matching construct.</param>
    /// <param name="result">The result in case of a sequence expression, or null otherwise.</param>
    public delegate void EndExecutionHandler(IPatternMatchingConstruct patternMatchingConstruct, object result);

    #endregion ActionExecutionDelegates


    /// <summary>
    /// An environment for the execution of actions (without embedded sequences).
    /// Holds a reference to the current graph.
    /// </summary>
    public interface IActionExecutionEnvironment
    {
        /// <summary>
        /// Returns the graph currently focused in processing / sequence execution.
        /// This may be the initial main graph, or a subgraph switched to, the current top element of the graph usage stack.
        /// </summary>
        IGraph Graph { get; set; }

        /// <summary>
        /// Returns the named graph currently focused in processing / sequence execution.
        /// Returns null if this graph is not a named but an unnamed graph.
        /// </summary>
        INamedGraph NamedGraph { get; }

        /// <summary>
        /// The actions employed by this graph processing environment
        /// </summary>
        IActions Actions { get; set; }

        /// <summary>
        /// The backend to be used for graph creation when a graph is imported
        /// </summary>
        IBackend Backend { get; }

        /// <summary>
        /// PerformanceInfo is used to accumulate information about needed time, found matches and applied rewrites.
        /// And additionally search steps carried out if profiling instrumentation code was generated.
        /// It must not be null.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// This is typically done at the start of a rewrite sequence, to measure its performance.
        /// </summary>
        PerformanceInfo PerformanceInfo { get; }

        /// <summary>
        /// Tells whether execution is interrupted because a highlight statement was hit.
        /// Consequence: the timer that normally prints match statistics every second remains silent
        /// </summary>
        bool HighlightingUnderway { get; set; }

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        int MaxMatches { get; set; }

        /// <summary>
        /// The action execution environment dependent commands that are available, and a description of each command.
        /// </summary>
        IDictionary<string, string> CustomCommandsAndDescriptions { get; }

        /// <summary>
        /// Does action execution environment dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        void Custom(params object[] args);


        #region Graph rewriting

        /// <summary>
        /// Retrieves the newest version of an IAction object currently available for this graph.
        /// This may be the given object.
        /// </summary>
        /// <param name="action">The IAction object.</param>
        /// <returns>The newest version of the given action.</returns>
        IAction GetNewestActionVersion(IAction action);

        /// <summary>
        /// Sets the newest action version for a static action.
        /// </summary>
        /// <param name="staticAction">The original action generated by GrGen.exe.</param>
        /// <param name="newAction">A new action instance.</param>
        void SetNewestActionVersion(IAction staticAction, IAction newAction);

        /// <summary>
        /// Matches a rewrite rule.
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on the MaxMatches property)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="filters">The name of the filters to apply to the matches before rewriting, in the order of filtering.</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCall> filters);

        /// <summary>
        /// Matches a rewrite rule, without firing the Matched event (but fires the PreMatched event - for internal or non-debugger use).
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on the MaxMatches property)</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches MatchWithoutEvent(IAction action, object[] arguments, int localMaxMatches);

        /// <summary>
        /// Matches the rewrite rules, without firing the Matched event, but fires the PreMatched event (for internal or non-debugger use).
        /// </summary>
        /// <param name="actions">The rules to invoke</param>
        /// <returns>A list of matches objects containing the found matches.</returns>
        IMatches[] MatchWithoutEvent(params ActionCall[] actions);

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <returns>A list with the return values of each of the rewrites applied. 
        /// Each element is a possibly empty array of objects that were returned by their rewrite.</returns>
        List<object[]> Replace(IMatches matches, int which);

        /// <summary>
        /// Matches a rewrite rule, without firing the Matched event, but with firing the PreMatch event and Cloning of the matches
        /// (so they can stored, or used in an expression combining multiple queries like [?r] + [?r], 
        /// or the action can be called multiple times in a multi rule all call query (on different parameters)).
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on the MaxMatches property)</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches MatchForQuery(IAction action, object[] arguments, int localMaxMatches);

        /// <summary>
        /// Matches the rewrite rules, without firing the Matched event, but with firing the PreMatch event and Cloning of the matches
        /// (so they can stored, or used in an expression combining multiple queries).
        /// </summary>
        /// <param name="actions">The rules to invoke</param>
        /// <returns>A list of matches objects containing the found matches.</returns>
        IMatches[] MatchForQuery(params ActionCall[] actions);

        #endregion Graph rewriting


        #region Events

        /// <summary>
        /// Fired when execution of a pattern matching construct begins.
        /// </summary>
        event BeginExecutionHandler OnBeginExecution;

        /// <summary>
        /// Fired after all requested matches of a rule have been matched (after filtering/selection of matches).
        /// </summary>
        event AfterMatchHandler OnMatched;

        /// <summary>
        /// Fired after all requested matches of a multi rule or rule have been matched, yet before filtering/selection of the matches.
        /// Allows a lookahead on the matches, on all found matches, in contrast to OnMatched that only reports the ones that are applied in the end.
        /// Also fired for queries, esp. for queries used for computing rule(/test) arguments, which don't cause a firing of other events.
        /// </summary>
        event PreMatchHandler OnPreMatched;

        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        event BeforeFinishHandler OnFinishing;

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        event RewriteNextMatchHandler OnRewritingNextMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        event AfterFinishHandler OnFinished;

        /// <summary>
        /// Fired when execution of a pattern matching construct ends.
        /// </summary>
        event EndExecutionHandler OnEndExecution;


        /// <summary>
        /// Fires an OnBeginExecution event.
        /// </summary>
        /// <param name="patternMatchingConstruct">The pattern matching construct.</param>
        void BeginExecution(IPatternMatchingConstruct patternMatchingConstruct);

        /// <summary>
        /// Fires an OnPreMatched event.
        /// </summary>
        /// <param name="matchesArray">The IMatches objects returned by the matchers of the actions (of the multi-rule application).</param>
        void PreMatched(IMatches[] matchesArray);

        /// <summary>
        /// Fires an OnPreMatched event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher of the single action.</param>
        void PreMatched(IMatches matches);

        /// <summary>
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="match">If not null, specifies the one current match from the matches 
        /// (to highlight the currently processed match during backtracking and the for matches loop).</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Matched(IMatches matches, IMatch match, bool special);

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finishing(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnRewritingNextMatch event.
        /// </summary>
        void RewritingNextMatch();

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher. The elements may be invalid.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void Finished(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnEndExecution event.
        /// </summary>
        /// <param name="patternMatchingConstruct">The pattern matching construct.</param>
        /// <param name="result">The result in case of a sequence expression, or null otherwise.</param>
        void EndExecution(IPatternMatchingConstruct patternMatchingConstruct, object result);

        #endregion Events
    }
}
