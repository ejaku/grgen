using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr.sequenceParser;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Specifies how often the rule should be applied
    /// </summary>
    public enum ActionMode
    {
        /// <summary>
        /// Apply the rule until no more matches can be found
        /// </summary>
        Zero,

        /// <summary>
        /// Apply the rule until the number of matches found reaches a fix point
        /// </summary>
        Fix,

        /// <summary>
        /// Apply the rule infinitely
        /// </summary>
        Max
    }

    #region ActionDelegates
    public delegate void AfterMatchHandler(IMatches matches, bool special);
    public delegate void BeforeFinishHandler(IMatches matches, bool special);
    public delegate void RewriteNextMatchHandler();
    public delegate void AfterFinishHandler(IMatches matches, bool special);
    public delegate void EnterSequenceHandler(Sequence seq);
    public delegate void ExitSequenceHandler(Sequence seq);
    #endregion ActionDelegates

    /// <summary>
    /// A container of rules also managing some parts of rule application with sequences.
    /// </summary>
    public abstract class BaseActions
    {
        private Random randomGenerator = new Random();
        private IGraphElement[] noElems = new IGraphElement[] { };

        private PerformanceInfo perfInfo = null;

        #region Abstract members

        /// <summary>
        /// An associated name.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// An MD5 hash of the used IGraphModel.
        /// Probably useless...
        /// </summary>
        public abstract String ModelMD5Hash { get; }

        /// <summary>
        /// The associated graph.
        /// </summary>
        public abstract IGraph Graph { get; set; }

        /// <summary>
        /// Enumerates all actions managed by this BaseActions instance.
        /// </summary>
        public abstract IEnumerable<IAction> Actions { get; }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        protected abstract IAction GetIAction(String name);

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        public IAction GetAction(String name)
        {
            return GetIAction(name);
        }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of paramteres for the stuff to do</param>
        public abstract void Custom(params object[] args);

        /// <summary>
        /// The maximum number of matches to be returned for a RuleAll sequence element.
        /// If it is zero or less, the number of matches is unlimited.
        /// </summary>
        public abstract int MaxMatches { get; set; }

        /// <summary>
        /// If PerformanceInfo is non-null, this object is used to accumulate information about time, found matches and applied rewrites.
        /// The user is responsible for resetting the PerformanceInfo object.
        /// </summary>
        public PerformanceInfo PerformanceInfo
        {
            get { return perfInfo; }
            set { perfInfo = value; }
        }

        #endregion Abstract members

        /// <summary>
        /// Executes the modifications of the according rule to the given match/matches.
        /// </summary>
        /// <param name="matches">The matches object returned by a previous matcher call.</param>
        /// <param name="which">The index of the match in the matches object to be applied,
        /// or -1, if all matches are to be applied.</param>
        /// <param name="perfInfo">A PerformanceInfo object accumulating the number of rewrites performed, or null.</param>
        /// <returns>A possibly empty array of objects returned by the last applied rewrite.</returns>
        public object[] Replace(IMatches matches, int which, PerformanceInfo perfInfo)
        {
            object[] retElems = null;
            if(which != -1)
            {
                if(which < 0 || which >= matches.Count)
                    throw new ArgumentOutOfRangeException("\"which\" is out of range!");

                retElems = matches.Producer.Modify(Graph, matches.GetMatch(which));
                if(perfInfo != null) perfInfo.RewritesPerformed++;
            }
            else
            {
                bool first = true;
                foreach(IMatch match in matches)
                {
                    if(first) first = false;
                    else if(OnRewritingNextMatch != null) OnRewritingNextMatch();
                    retElems = matches.Producer.Modify(Graph, match);
                    if(perfInfo != null) perfInfo.RewritesPerformed++;
                }
                if(retElems == null) retElems = noElems;
            }
            return retElems;
        }

        /// <summary>
        /// Apply a rewrite rule
        /// </summary>
        /// <param name="ruleObject">RuleObject to be applied</param>
        /// <param name="which">The index of the match to be rewritten or -1 to rewrite all matches</param>
        /// <param name="localMaxMatches">Specifies the maximum number of matches to be found (if less or equal 0 the number of matches
        /// depends on MaxMatches)</param>
        /// <param name="special">Specifies whether the %-modifier has been used for this rule, which may have a special meaning for
        /// the application</param>
        /// <param name="test">If true, no rewrite step is performed.</param>
        /// <returns>The number of matches found</returns>
        public int ApplyRewrite(RuleObject ruleObject, int which, int localMaxMatches, bool special, bool test)
        {
            int curMaxMatches = (localMaxMatches > 0) ? localMaxMatches : MaxMatches;

            object[] parameters;
            if(ruleObject.ParamVars.Length > 0)
            {
                parameters = ruleObject.Parameters;
                for(int i = 0; i < ruleObject.ParamVars.Length; i++)
                    parameters[i] = Graph.GetVariableValue(ruleObject.ParamVars[i]);
            }
            else parameters = null;

            if(perfInfo != null) perfInfo.StartLocal();
            IMatches matches = ruleObject.Action.Match(Graph, curMaxMatches, parameters);
            if(perfInfo != null)
            {
                perfInfo.StopMatch();              // total match time does NOT include listeners anymore
                perfInfo.MatchesFound += matches.Count;
            }

            if(OnMatched != null) OnMatched(matches, special);
            if(matches.Count == 0) return 0;

            if(test) return matches.Count;

            if(OnFinishing != null) OnFinishing(matches, special);

            if(perfInfo != null) perfInfo.StartLocal();
            object[] retElems = Replace(matches, which, perfInfo);
            for(int i = 0; i < ruleObject.ReturnVars.Length; i++)
                Graph.SetVariableValue(ruleObject.ReturnVars[i], retElems[i]);
            if(perfInfo != null) perfInfo.StopRewrite();            // total rewrite time does NOT include listeners anymore

            if(OnFinished != null) OnFinished(matches, special);

            return matches.Count;
        }

        /// <summary>
        /// Apply a graph rewrite rule.
        /// </summary>
        /// <param name="ruleObject">RuleObject to be applied.</param>
        /// <param name="mode">The mode for the loo.p</param>
        /// <param name="times">The maximum number of iterations.</param>
        /// <param name="special">Specifies the "special" flag, which may have a special meaning for the application
        /// receiving events with this flag.</param>
        /// <returns>The number of rewrites actually performed.</returns>
        public int ApplyGraphRewrite(RuleObject ruleObject, ActionMode mode, int times, bool special)
        {
            int oldMatches, matches = 0, i = 0;
            int maxMatches = mode == ActionMode.Fix ? -1 : 1;
            if(perfInfo != null) perfInfo.Start();

            do
            {
                oldMatches = matches;
                matches = ApplyRewrite(ruleObject, 0, maxMatches, special, false);
                i++;
            }
            while(((mode == ActionMode.Zero && matches > 0) || (mode == ActionMode.Fix && oldMatches != matches)
                || (mode == ActionMode.Max)) && i < times);

            if(perfInfo != null) perfInfo.Stop();

            return (matches != 0) ? i : i - 1;
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="sequence">The graph rewrite sequence</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(Sequence sequence)
        {
            if(perfInfo != null) perfInfo.Start();

//            int applied = 0;
//            int res = ApplyGRS(sequence, ref applied, perfInfo);
            bool res = sequence.Apply(this);

            if(perfInfo != null) perfInfo.Stop();
            return res; // > 0;
        }

        /// <summary>
        /// Apply a graph rewrite sequence.
        /// </summary>
        /// <param name="seqStr">The graph rewrite sequence in form of a string</param>
        /// <returns>The result of the sequence.</returns>
        public bool ApplyGraphRewriteSequence(String seqStr)
        {
            return ApplyGraphRewriteSequence(SequenceParser.ParseSequence(seqStr, this));
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seq">The sequence to be executed</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(Sequence seq)
        {
            IGraph curGraph = Graph;
            Graph = Graph.Clone("clonedGraph");
            bool res = seq.Apply(this);
            Graph = curGraph;
            return res;
        }

        /// <summary>
        /// Tests whether the given sequence succeeds on a clone of the associated graph.
        /// </summary>
        /// <param name="seqStr">The sequence to be executed in form of a string</param>
        /// <returns>True, iff the sequence succeeds on the cloned graph </returns>
        public bool ValidateWithSequence(String seqStr)
        {
            return ValidateWithSequence(SequenceParser.ParseSequence(seqStr, this));
        }

        #region Events

        /// <summary>
        /// Fired after all requested matches of a rule have been matched.
        /// </summary>
        public event AfterMatchHandler OnMatched;
        
        /// <summary>
        /// Fired before the rewrite step of a rule, when at least one match has been found.
        /// </summary>
        public event BeforeFinishHandler OnFinishing;

        /// <summary>
        /// Fired before the next match is rewritten. It is not fired before rewriting the first match.
        /// </summary>
        public event RewriteNextMatchHandler OnRewritingNextMatch;

        /// <summary>
        /// Fired after the rewrite step of a rule.
        /// Note, that the given matches object may contain invalid entries,
        /// as parts of the match may have been deleted!
        /// </summary>
        public event AfterFinishHandler OnFinished;

        /// <summary>
        /// Fired when a sequence is entered.
        /// </summary>
        public event EnterSequenceHandler OnEntereringSequence;

        /// <summary>
        /// Fired when a sequence is left.
        /// </summary>
        public event ExitSequenceHandler OnExitingSequence;

        /// <summary>
        /// Fires an OnEnteringSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is entered.</param>
        public void EnteringSequence(Sequence seq)
        {
            EnterSequenceHandler handler = OnEntereringSequence;
            if(handler != null) handler(seq);
        }

        /// <summary>
        /// Fires an OnExitingSequence event.
        /// </summary>
        /// <param name="seq">The sequence which is exited.</param>
        public void ExitingSequence(Sequence seq)
        {
            ExitSequenceHandler handler = OnExitingSequence;
            if(handler != null) handler(seq);
        }

        /// <summary>
        /// Fires an OnMatched event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Matched(IMatches matches, bool special)
        {
            AfterMatchHandler handler = OnMatched;
            if(handler != null) handler(matches, special);
        }

        /// <summary>
        /// Fires an OnFinishing event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finishing(IMatches matches, bool special)
        {
            BeforeFinishHandler handler = OnFinishing;
            if(handler != null) handler(matches, special);
        }

        /// <summary>
        /// Fires an OnFinished event.
        /// </summary>
        /// <param name="matches">The match result.</param>
        /// <param name="special">The "special" flag of this rule application.</param>
        public void Finished(IMatches matches, bool special)
        {
            AfterFinishHandler handler = OnFinished;
            if(handler != null) handler(matches, special);
        }
        #endregion Events
    }
}