/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

        private readonly PerformanceInfo perfInfo = new PerformanceInfo();
        private bool highlightingUnderway = false;

        private int maxMatches = 0;
        protected readonly Dictionary<String, String> customCommandsToDescriptions;


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

        public IMatches Match(IAction action, object[] arguments, int localMaxMatches, bool special, List<FilterCallBase> filters)
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

            for(int i = 0; i < filters.Count; ++i)
            {
                action.Filter(this, matches, filters[i]);
            }

            if(matches.Count > 0) // ensure that Matched is only called when a match exists
                Matched(matches, null, special);

            return matches;
        }

        public IMatches MatchWithoutEvent(IAction action, object[] arguments, int localMaxMatches)
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

            return matches;
        }

        public List<object[]> Replace(IMatches matches, int which)
        {
            List<object[]> returns;
            object[] retElems;

            if(which != -1)
            {
                if(which < 0 || which >= matches.Count)
                    throw new ArgumentOutOfRangeException("\"which\" is out of range!");

                returns = matches.Producer.Reserve(0); 

                retElems = matches.Producer.Modify(this, matches.GetMatch(which));

                returns.Add(retElems);

                ++PerformanceInfo.RewritesPerformed;
            }
            else
            {
                returns = matches.Producer.Reserve(matches.Count);

                int curResultNum = 0;
                bool first = true;
                foreach(IMatch match in matches)
                {
                    if(first)
                        first = false;
                    else if(OnRewritingNextMatch != null)
                        OnRewritingNextMatch();

                    retElems = matches.Producer.Modify(this, match);

                    object[] curResult = returns[curResultNum];
                    retElems.CopyTo(curResult, 0);

                    ++PerformanceInfo.RewritesPerformed;
                    ++curResultNum;
                }
            }

            return returns;
        }

        #endregion Graph rewriting


        #region Events

        public event AfterMatchHandler OnMatched;
        public event BeforeFinishHandler OnFinishing;
        public event RewriteNextMatchHandler OnRewritingNextMatch;
        public event AfterFinishHandler OnFinished;

        public void Matched(IMatches matches, IMatch match, bool special)
        {
            if(OnMatched != null)
                OnMatched(matches, match, special);
        }

        public void Finishing(IMatches matches, bool special)
        {
            if(OnFinishing != null)
                OnFinishing(matches, special);
        }

        public void RewritingNextMatch()
        {
            if(OnRewritingNextMatch != null)
                OnRewritingNextMatch();
        }

        public void Finished(IMatches matches, bool special)
        {
            if(OnFinished != null)
                OnFinished(matches, special);
        }

        #endregion Events
    }
}
