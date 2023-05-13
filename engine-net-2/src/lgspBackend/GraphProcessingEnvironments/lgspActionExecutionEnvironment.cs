/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An implementation of the IGraphProcessingEnvironment, to be used with LGSPGraphs.
    /// </summary>
    public class LGSPActionExecutionEnvironment : IActionExecutionEnvironment
    {
        protected readonly Stack<LGSPGraph> usedGraphs;
        protected LGSPNamedGraph namedGraphOnTop;

        private readonly Dictionary<IAction, IAction> actionMapStaticToNewest = new Dictionary<IAction, IAction>();
        public LGSPActions curActions;

        private bool enableAssertions;
        public bool EnableAssertions
        {
            get { return enableAssertions; }
            set { enableAssertions = value; }
        }

        private readonly PerformanceInfo perfInfo = new PerformanceInfo();
        private bool highlightingUnderway = false;

        private int maxMatches = 0;
        protected readonly Dictionary<String, String> customCommandsToDescriptions;

        private readonly IMatches[] singleElementMatchesArray = new IMatches[1]; // performance optimization
        private readonly bool[] singleElementSpecialArray = new bool[1]; // performance optimization


        public LGSPActionExecutionEnvironment(LGSPGraph graph, LGSPActions actions)
        {
            usedGraphs = new Stack<LGSPGraph>();
            usedGraphs.Push(graph);
            namedGraphOnTop = graph as LGSPNamedGraph;
            curActions = actions;
            InitActionsProfile(actions);
            customCommandsToDescriptions = new Dictionary<string, string>();
            FillCustomCommandDescriptions();
        }

        public void InitActionsProfile(LGSPActions actions)
        {
            if(actions == null)
                return;
            if(!actions.Profile)
                return;

            perfInfo.ActionProfiles.Clear();
            foreach(IAction action in actions.Actions)
            {
                ActionProfile actionProfile = new ActionProfile();
                int branchingFactor = ((LGSPAction)action).patternGraph.branchingFactor;
                actionProfile.averagesPerThread = new ProfileAverages[branchingFactor];
                for(int i = 0; i < branchingFactor; ++i)
                {
                    actionProfile.averagesPerThread[i] = new ProfileAverages();
                }
                perfInfo.ActionProfiles.Add(action.PackagePrefixedName, actionProfile);
            }
        }

        private void FillCustomCommandDescriptions()
        {
            customCommandsToDescriptions.Add("set_max_matches",
                "- set_max_matches: Sets the maximum number of matches to be found\n" +
                "     during matching (for all-bracketed rule calls like [r]).\n");
            customCommandsToDescriptions.Add("enable_assertions",
                "- enable_assertions: enables(true)/disables(false) assertions.\n");
        }

        public IGraph Graph
        {
            get { return graph; }
            set
            {
                if(usedGraphs.Count != 1)
                    throw new Exception("Can't replace graph while a subgraph is used by an action/sequence!");
                else
                {
                    usedGraphs.Clear();
                    usedGraphs.Push((LGSPGraph)value);
                    namedGraphOnTop = value as LGSPNamedGraph;
                }
            }
        }

        public INamedGraph NamedGraph
        {
            get { return namedGraph; }
        }

        public LGSPGraph graph
        {
            get { return usedGraphs.Peek(); }
        }
        public LGSPNamedGraph namedGraph
        {
            get { return namedGraphOnTop; }
        }

        public IActions Actions
        {
            get { return curActions; }
            set
            {
                curActions = (LGSPActions)value;
                InitActionsProfile((LGSPActions)value);
            }
        }

        public IBackend Backend
        {
            get { return LGSPBackend.Instance; }
        }

        public PerformanceInfo PerformanceInfo
        {
            get { return perfInfo; }
        }

        public bool HighlightingUnderway
        {
            get { return highlightingUnderway; }
            set { highlightingUnderway = value; }
        }

        public int MaxMatches
        {
            get { return maxMatches; }
            set { maxMatches = value; }
        }

        public IDictionary<String, String> CustomCommandsAndDescriptions
        {
            get
            {
                return customCommandsToDescriptions;
            }
        }

        public virtual void Custom(params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
            case "set_max_matches":
                if(args.Length != 2)
                    throw new ArgumentException("Usage: set_max_matches <integer>\nIf <integer> <= 0, all matches will be matched.");

                int newMaxMatches;
                if(!int.TryParse((String)args[1], out newMaxMatches))
                    throw new ArgumentException("Illegal integer value specified: \"" + (String)args[1] + "\"");
                MaxMatches = newMaxMatches;
                return;

            case "enable_assertions":
                if(args.Length != 2)
                    throw new ArgumentException("Usage: enable_assertions <bool>.");

                bool enableAssertions;
                if(!bool.TryParse((String)args[1], out enableAssertions))
                    throw new ArgumentException("Illegal boolean value specified: \"" + (String)args[1] + "\"");
                EnableAssertions = enableAssertions;
                return;

            default:
                throw new ArgumentException("Unknown command: " + command);
            }
        }


        #region Graph rewriting

        public IAction GetNewestActionVersion(IAction action)
        {
            IAction newest;
            if(!actionMapStaticToNewest.TryGetValue(action, out newest))
                return action;
            return newest;
        }

        public void SetNewestActionVersion(IAction staticAction, IAction newAction)
        {
            actionMapStaticToNewest[staticAction] = newAction;
        }

        // Only supports FilterCallWithArguments as filters, the FilterCallWithLambdaExpression is only supported by the graph processing environment
        public virtual IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCall> filters, bool fireDebugEvents)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

#if DEBUGACTIONS || MATCHREWRITEDETAIL // spread over multiple files now, search for the corresponding defines to reactivate
            PerformanceInfo.StartLocal();
#endif
            IMatches matches = action.Match(this, curMaxMatches, arguments);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matches.Count;

            if(fireDebugEvents)
            {
                if(matches.Count > 0)
                    MatchedBeforeFiltering(matches);
            }

            for(int i = 0; i < filters.Count; ++i)
            {
                FilterCallWithArguments filterCallWithArguments = (FilterCallWithArguments)filters[i];
                action.Filter(this, matches, filterCallWithArguments);
            }

            if(fireDebugEvents)
            {
                if(matches.Count > 0) // ensure that Matched is only called when a match exists
                    MatchedAfterFiltering(matches, special);
            }

            return matches;
        }

        public IMatches MatchWithoutEvent(IAction action, object[] arguments, int localMaxMatches, bool fireDebugEvents)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StartLocal();
#endif
            IMatches matches = action.Match(this, curMaxMatches, arguments);

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matches.Count;

            if(fireDebugEvents)
            {
                if(matches.Count > 0)
                    MatchedBeforeFiltering(matches);
            }

            return matches;
        }

        public IMatches[] MatchWithoutEvent(bool fireDebugEvents, params ActionCall[] actions)
        {
            IMatches[] matchesArray = new IMatches[actions.Length];

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StartLocal();
#endif
            int matchesFound = 0;
            for(int i = 0; i < actions.Length; ++i)
            {
                ActionCall actionCall = actions[i];
                IMatches matches = actionCall.Action.Match(this, actionCall.MaxMatches, actionCall.Arguments);
                matchesFound += matches.Count;
                matchesArray[i] = matches;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matchesFound;

            if(fireDebugEvents)
            {
                if(matchesFound > 0)
                    MatchedBeforeFiltering(matchesArray);
            }

            return matchesArray;
        }

        public List<object[]> Replace(IMatches matches, int which, bool special, bool fireDebugEvents)
        {
            List<object[]> returns;
            object[] retElems;

            if(which != -1)
            {
                if(which < 0 || which >= matches.Count)
                    throw new ArgumentOutOfRangeException("\"which\" is out of range!");

                returns = matches.Producer.Reserve(0);

                IMatch match = matches.GetMatch(which);

                if(fireDebugEvents)
                {
                    if(OnMatchSelected != null)
                        OnMatchSelected(match, special, matches);

                    if(OnRewritingSelectedMatch != null)
                        OnRewritingSelectedMatch();
                }

                retElems = matches.Producer.Modify(this, match);

                returns.Add(retElems);

                ++PerformanceInfo.RewritesPerformed;

                if(fireDebugEvents)
                {
                    if(OnFinishedSelectedMatch != null)
                        OnFinishedSelectedMatch();
                }
            }
            else
            {
                returns = matches.Producer.Reserve(matches.Count);

                int curResultNum = 0;
                foreach(IMatch match in matches)
                {
                    if(fireDebugEvents)
                    {
                        if(OnMatchSelected != null)
                            OnMatchSelected(match, special, matches);

                        if(OnRewritingSelectedMatch != null)
                            OnRewritingSelectedMatch();
                    }

                    retElems = matches.Producer.Modify(this, match);

                    object[] curResult = returns[curResultNum];
                    retElems.CopyTo(curResult, 0);

                    ++PerformanceInfo.RewritesPerformed;
                    ++curResultNum;

                    if(fireDebugEvents)
                    {
                        if(OnFinishedSelectedMatch != null)
                            OnFinishedSelectedMatch();
                    }
                }
            }

            return returns;
        }

        public IMatches MatchForQuery(IAction action, object[] arguments, int localMaxMatches, bool fireDebugEvents)
        {
            IMatches matches = MatchWithoutEvent(action, arguments, localMaxMatches, fireDebugEvents);
            matches = matches.Clone();
            return matches;
        }

        public IMatches[] MatchForQuery(bool fireDebugEvents, params ActionCall[] actions)
        {
            IMatches[] matchesArray = new IMatches[actions.Length];

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StartLocal();
#endif
            int matchesFound = 0;
            for(int i = 0; i < actions.Length; ++i)
            {
                ActionCall actionCall = actions[i];
                IMatches matches = actionCall.Action.Match(this, actionCall.MaxMatches, actionCall.Arguments);
                matchesFound += matches.Count;
                matches = matches.Clone(); // must be cloned before the next iteration step, as that could be the same action
                matchesArray[i] = matches;
            }

#if DEBUGACTIONS || MATCHREWRITEDETAIL
            PerformanceInfo.StopMatch();
#endif
            PerformanceInfo.MatchesFound += matchesFound;

            if(fireDebugEvents)
            {
                if(matchesFound > 0)
                    MatchedBeforeFiltering(matchesArray);
            }

            return matchesArray;
        }

        #endregion Graph rewriting


        #region Events

        public event BeginExecutionHandler OnBeginExecution;
        public event MatchedBeforeFilteringHandler OnMatchedBefore;
        public event MatchedAfterFilteringHandler OnMatchedAfter;
        public event MatchSelectedHandler OnMatchSelected;
        public event RewriteSelectedMatchHandler OnRewritingSelectedMatch;
        public event SelectedMatchRewrittenHandler OnSelectedMatchRewritten;
        public event FinishedSelectedMatchHandler OnFinishedSelectedMatch;
        public event FinishedHandler OnFinished;
        public event EndExecutionHandler OnEndExecution;

        public void BeginExecution(IPatternMatchingConstruct patternMatchingConstruct)
        {
            if(OnBeginExecution != null)
                OnBeginExecution(patternMatchingConstruct);
        }

        public void MatchedBeforeFiltering(IMatches[] matches)
        {
            if(OnMatchedBefore != null)
                OnMatchedBefore(matches);
        }

        public void MatchedBeforeFiltering(IMatches matches)
        {
            singleElementMatchesArray[0] = matches;
            if(OnMatchedBefore != null)
                OnMatchedBefore(singleElementMatchesArray);
        }

        public void MatchedAfterFiltering(IMatches[] matches, bool[] special)
        {
            if(OnMatchedAfter != null)
                OnMatchedAfter(matches, special);
        }

        public void MatchedAfterFiltering(IMatches matches, bool special)
        {
            singleElementMatchesArray[0] = matches;
            singleElementSpecialArray[0] = special;
            if(OnMatchedAfter != null)
                OnMatchedAfter(singleElementMatchesArray, singleElementSpecialArray);
        }

        public void MatchSelected(IMatch match, bool special, IMatches matches)
        {
            if(OnMatchSelected != null)
                OnMatchSelected(match, special, matches);
        }

        public void RewritingSelectedMatch()
        {
            if(OnRewritingSelectedMatch != null)
                OnRewritingSelectedMatch();
        }

        public void SelectedMatchRewritten()
        {
            if(OnSelectedMatchRewritten != null)
                OnSelectedMatchRewritten();
        }

        public void FinishedSelectedMatch()
        {
            if(OnFinishedSelectedMatch != null)
                OnFinishedSelectedMatch();
        }

        public void Finished(IMatches[] matches, bool[] special)
        {
            if(OnFinished != null)
                OnFinished(matches, special);
        }

        public void Finished(IMatches matches, bool special)
        {
            singleElementMatchesArray[0] = matches;
            singleElementSpecialArray[0] = special;
            if(OnFinished != null)
                OnFinished(singleElementMatchesArray, singleElementSpecialArray);
        }

        public void EndExecution(IPatternMatchingConstruct patternMatchingConstruct, object result)
        {
            if(OnEndExecution != null)
                OnEndExecution(patternMatchingConstruct, result);
        }

        #endregion Events
    }
}
