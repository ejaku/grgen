/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// before filtering (single element array in case of a single rule/test application).
    /// </summary>
    /// <param name="matches">The matches found (an array of matches objects, one matches object per rule).</param>
    public delegate void MatchedBeforeFilteringHandler(IMatches[] matches);

    /// <summary>
    /// Represents a method called after all requested matches of a multi action or action have been matched,
    /// after filtering (single element arrays in case of a single rule/test application).
    /// </summary>
    /// <param name="matches">The array of IMatches objects found (contains multiple IMatches objects iff a multi construct was matched).</param>
    /// <param name="special">Specifies whether the "special" flag has been used (per rule/test).</param>
    public delegate void MatchedAfterFilteringHandler(IMatches[] matches, bool[] special);

    /// <summary>
    /// Represents a method called when a match has been selected for execution.
    /// </summary>
    /// <param name="match">Specifies the selected match.</param>
    /// <param name="special">Specifies whether the "special" flag has been used.</param>
    /// <param name="matches">The matches found.</param>
    public delegate void MatchSelectedHandler(IMatch match, bool special, IMatches matches);

    /// <summary>
    /// Represents a method called before the selected match is rewritten (comparable to the old BeforeFinishHandler and RewriteNextMatchHandler, now one event, fired per selected match).
    /// </summary>
    public delegate void RewriteSelectedMatchHandler();

    /// <summary>
    /// Represents a method called after the selected match was rewritten (but before embedded execs/emits are executed).
    /// </summary>
    public delegate void SelectedMatchRewrittenHandler();

    /// <summary>
    /// Represents a method called after the selected match was rewritten.
    /// </summary>
    public delegate void FinishedSelectedMatchHandler();

    /// <summary>
    /// Represents a method called after the rewrite step of a rule or multi construct.
    /// </summary>
    /// <param name="matches">The array of IMatches objects found (contains multiple IMatches objects iff a multi construct was matched)
    /// This may contain invalid matched elements, because parts of the matches may have been deleted.</param>
    /// <param name="special">Specifies whether the "special" flag has been used (per rule/test).</param>
    public delegate void FinishedHandler(IMatches[] matches, bool[] special);

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
        /// Allows to enable/disable assertions and find out about assertion state.
        /// </summary>
        bool EnableAssertions { get; set; }

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
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCall> filters, bool fireDebugEvents);

        /// <summary>
        /// Matches a rewrite rule, without firing the Matched event (but fires the MatchedBeforeFiltering event - for internal or non-debugger use).
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on the MaxMatches property)</param>
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches MatchWithoutEvent(IAction action, object[] arguments, int localMaxMatches, bool fireDebugEvents);

        /// <summary>
        /// Matches the rewrite rules, without firing the Matched event, but fires the MatchedBeforeFiltering event (for internal or non-debugger use).
        /// </summary>
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <param name="actions">The rules to invoke</param>
        /// <returns>A list of matches objects containing the found matches.</returns>
        IMatches[] MatchWithoutEvent(bool fireDebugEvents, params ActionCall[] actions);

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// Fires OnRewritingNextMatch events before each rewrite except for the first one.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <param name="special">Whether the special flag was applied to the rule call.</param>
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <returns>A list with the return values of each of the rewrites applied. 
        /// Each element is a possibly empty array of objects that were returned by their rewrite.</returns>
        List<object[]> Replace(IMatches matches, int which, bool special, bool fireDebugEvents);

        /// <summary>
        /// Matches a rewrite rule, without firing the Matched event, but with firing the PreMatch event and Cloning of the matches
        /// (so they can stored, or used in an expression combining multiple queries like [?r] + [?r], 
        /// or the action can be called multiple times in a multi rule all call query (on different parameters)).
        /// </summary>
        /// <param name="action">The rule to invoke</param>
        /// <param name="arguments">The input arguments</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on the MaxMatches property)</param>
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <returns>A matches object containing the found matches.</returns>
        IMatches MatchForQuery(IAction action, object[] arguments, int localMaxMatches, bool fireDebugEvents);

        /// <summary>
        /// Matches the rewrite rules, without firing the Matched event, but with firing the PreMatch event and Cloning of the matches
        /// (so they can stored, or used in an expression combining multiple queries).
        /// </summary>
        /// <param name="fireDebugEvents">Specifies whether debug events (mostly action events) are to be fired.</param>
        /// <param name="actions">The rules to invoke</param>
        /// <returns>A list of matches objects containing the found matches.</returns>
        IMatches[] MatchForQuery(bool fireDebugEvents, params ActionCall[] actions);

        #endregion Graph rewriting


        #region Events

        /// <summary>
        /// Fired when execution of a pattern matching construct begins.
        /// </summary>
        event BeginExecutionHandler OnBeginExecution;

        /// <summary>
        /// Fired after all requested matches of a multi rule or rule have been matched, before filtering of the matches.
        /// Allows a lookahead on the matches, on all found matches, in contrast to OnMatchedAfter that only reports the ones that are applied in the end.
        /// </summary>
        event MatchedBeforeFilteringHandler OnMatchedBefore;

        /// <summary>
        /// Fired after all requested matches of a multi rule or rule have been matched (after filtering of matches).
        /// </summary>
        event MatchedAfterFilteringHandler OnMatchedAfter;

        /// <summary>
        /// Fired when a match was selected for execution (after filtering/selection of matches).
        /// </summary>
        event MatchSelectedHandler OnMatchSelected;

        /// <summary>
        /// Fired before the selected match is rewritten (comparable to the old OnFinishing and OnRewritingNextMatch, now one event, fired per selected match).
        /// </summary>
        event RewriteSelectedMatchHandler OnRewritingSelectedMatch;

        /// <summary>
        /// Fired after the selected match was rewritten (but before embedded sequences are executed).
        /// </summary>
        event SelectedMatchRewrittenHandler OnSelectedMatchRewritten;

        /// <summary>
        /// Fired after the selected match was rewritten and embedded sequences executed.
        /// </summary>
        event FinishedSelectedMatchHandler OnFinishedSelectedMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule/after rule execution has completed.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        event FinishedHandler OnFinished;

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
        /// Fires an OnMatchedBefore event.
        /// </summary>
        /// <param name="matches">The IMatches objects returned by the matchers of the actions (of the multi-rule application).</param>
        void MatchedBeforeFiltering(IMatches[] matches);

        /// <summary>
        /// Fires an OnMatchedBefore event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher of the single action.</param>
        void MatchedBeforeFiltering(IMatches matches);

        /// <summary>
        /// Fires an OnMatchedAfter event.
        /// </summary>
        /// <param name="matches">The array of IMatches objects returned by the matcher of the multi construct.</param>
        /// <param name="special">Whether this is a 'special' match (user defined) (per action).</param>
        void MatchedAfterFiltering(IMatches[] matches, bool[] special);

        /// <summary>
        /// Fires an OnMatchedAfter event.
        /// </summary>
        /// <param name="matches">The IMatches object returned by the matcher.</param>
        /// <param name="special">Whether this is a 'special' match (user defined).</param>
        void MatchedAfterFiltering(IMatches matches, bool special);

        /// <summary>
        /// Fires an OnMatchSelected event.
        /// </summary>
        /// <param name="match">Specifies the selected match.</param>
        /// <param name="special">Specifies whether the "special" flag has been used.</param>
        /// <param name="matches">The matches found.</param>
        void MatchSelected(IMatch match, bool special, IMatches matches);

        /// <summary>
        /// Fires an OnRewritingSelectedMatch event (comparable to the old OnFinishing and OnRewritingNextMatch, now one event, fired per selected match).
        /// </summary>
        void RewritingSelectedMatch();

        /// <summary>
        /// Fires an OnFinishedSelectedMatch event.
        /// </summary>
        void FinishedSelectedMatch();

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The IMatches objects returned by the matchers of the actions (of the multi-rule application). The matched elements may be invalid.</param>
        /// <param name="special">Whether this is a 'special' match (user defined) (per action).</param>
        void Finished(IMatches[] matches, bool[] special);

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
