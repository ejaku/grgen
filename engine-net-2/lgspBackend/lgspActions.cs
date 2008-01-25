//#define ASSERT_ALL_UNMAPPED_AFTER_MATCH

using System;
using System.Collections;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.IO;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    public delegate LGSPMatches MatchInvoker(LGSPGraph graph, int maxMatches, IGraphElement[] parameters);

    /// <summary>
    /// An object representing a match of the LGSPBackend.
    /// </summary>
    public class LGSPMatch : IMatch
    {
        /// <summary>
        /// Not implemented yet
        /// </summary>
        public PatternGraph Pattern { get { return patternGraph; } }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        public PatternGraph patternGraph;

        /// <summary>
        /// An array of all nodes in the match.
        /// The order is given by the Nodes array of the according IPatternGraph.
        /// </summary>
        public LGSPNode[] Nodes;

        /// <summary>
        /// An array of all edges in the match.
        /// The order is given by the Edges array of the according IPatternGraph.
        /// </summary>
        public LGSPEdge[] Edges;

        /// <summary>
        /// Not implemented yet
        /// </summary>
        public LGSPMatch[] EmbeddedGraphs;

        /// <summary>
        /// The next match in the linked list of matches.
        /// </summary>
        public LGSPMatch nextMatch;

        /// <summary>
        /// Constructs a new match object.
        /// </summary>
        /// <param name="nodes">The nodes of the match.</param>
        /// <param name="edges">The edges of the match.</param>
        public LGSPMatch(LGSPNode[] nodes, LGSPEdge[] edges)
        {
            this.Nodes = nodes;
            this.Edges = edges; 
        }

        #region IMatch Members

        IPatternGraph IMatch.Pattern
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        INode[] IMatch.Nodes
        {
            get { return Nodes; }
        }

        IEdge[] IMatch.Edges
        {
            get { return Edges; }
        }

        IMatch[] IMatch.EmbeddedGraphs
        {
            get { return EmbeddedGraphs; }
        }

        #endregion
    }

    public class LGSPMatchesList : IEnumerable<IMatch>
    {
        private int numNodes, numEdges;
        private int count;
        private LGSPMatch root;
        private LGSPMatch last;

        public LGSPMatchesList(int numNodes, int numEdges)
        {
            this.numNodes = numNodes;
            this.numEdges = numEdges;
            last = root = new LGSPMatch(new LGSPNode[numNodes], new LGSPEdge[numEdges]);
        }

        public int Count { get { return count; } }
        public LGSPMatch First { get { return count > 0 ? root : null; } }

        public void Clear()
        {
            count = 0;
            last = root;
        }

        public LGSPMatch GetNewMatch()
        {
            return last;    
        }

        public void CommitMatch()
        {
            count++;
            if (last.nextMatch == null) last.nextMatch = new LGSPMatch(new LGSPNode[numNodes], new LGSPEdge[numEdges]);
            last = last.nextMatch;
        }

        public LGSPMatch this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Index out of range: " + index);
                LGSPMatch cur = root;
                for (int i = 0; i < index; i++, cur = cur.nextMatch) ;
                return cur;
            }
        }

        public IEnumerator<IMatch> GetEnumerator()
        {
            LGSPMatch cur = root;
            for (int i = 0; i < count; i++, cur = cur.nextMatch)
                yield return cur;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// An object representing a (possibly empty) set of matches in a graph before the rewrite has been applied.
    /// It is returned by IAction.Match() and given to the OnMatched event.
    /// </summary>
    public class LGSPMatches : IMatches
    {
        /// <summary>
        /// The action object used to generate this LGSPMatches object.
        /// </summary>
        public LGSPAction producer;
//        public List<LGSPMatch> matches = new List<LGSPMatch>();
//        public SingleLinkedList<LGSPMatch> matches = new SingleLinkedList<LGSPMatch>();

        /// <summary>
        /// The matches list containing all matches.
        /// </summary>
        public LGSPMatchesList matches;

        /// <summary>
        /// Constructs a new LGSPMatches instance.
        /// </summary>
        /// <param name="producer">The action object used to generate this LGSPMatches object</param>
        /// <param name="numNodes">The number of nodes matched by the given action.</param>
        /// <param name="numEdges">The number of edges matched by the given action.</param>
        public LGSPMatches(LGSPAction producer, int numNodes, int numEdges)
        {
            this.producer = producer;
            matches = new LGSPMatchesList(numNodes, numEdges);
        }

        /// <summary>
        /// The action object used to generate this LGSPMatches object
        /// </summary>
        public IAction Producer { get { return producer; } }

        /// <summary>
        /// The number of matches found by Producer
        /// </summary>
        public int Count { get { return matches.Count; } }

        /// <summary>
        /// Returns the match with the given index. Invalid indices cause an IndexOutOfRangeException.
        /// This may be slow. If you want to iterate over the elements the Matches IEnumerable should be used.
        /// </summary>
        public IMatch GetMatch(int index) { return matches[index]; }

        /// <summary>
        /// Returns an enumerator over all found matches.
        /// </summary>
        public IEnumerator<IMatch> GetEnumerator()
        {
            return matches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return matches.GetEnumerator();
        }
    }

    /// <summary>
    /// An object representing an executable rule of the LGSPBackend.
    /// </summary>
    public abstract class LGSPAction : IAction
    {
        /// <summary>
        /// The name of the rule
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The LGSPRulePattern object from which this LGSPAction object has been created.
        /// </summary>
        public LGSPRulePattern rulePattern;

        /// <summary>
        /// The RulePattern object from which this LGSPAction object has been created.
        /// </summary>
        public IRulePattern RulePattern { get { return rulePattern; } }

        /// <summary>
        /// A delegate pointing to the current matcher program for this rule.
        /// </summary>
        public MatchInvoker DynamicMatch;

        /// <summary>
        /// If the pattern of this action contains subpatterns, here's the list of the subpattern matching actions
        /// </summary>
        protected List<LGSPAction> subpatterns = new List<LGSPAction>();


        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern.
        /// </summary>
        /// <param name="graph">The host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An LGSPMatches object containing the found matches.</returns>
        public LGSPMatches Match(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            return DynamicMatch(graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// </summary>
        /// <returns>An array of elements returned by the rule.</returns>
        public IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, match);
            else
                return rulePattern.ModifyNoReuse(graph, match);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule must not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
        public IGraphElement[] Apply(LGSPGraph graph)
        {
            LGSPMatches matches = DynamicMatch(graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.matches.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
		/// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
		public IGraphElement[] Apply(LGSPGraph graph, params IGraphElement[] parameters)
        {
            LGSPMatches matches = DynamicMatch(graph, 1, parameters);
            if(matches.Count <= 0) return null;

            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify(graph, matches.matches.First);
            else
                return rulePattern.ModifyNoReuse(graph, matches.matches.First);
        }

        #region IAction Members

        /// <summary>
        /// Searches for a graph pattern as specified by RulePattern.
        /// </summary>
        /// <param name="graph">The host graph.</param>
        /// <param name="maxMatches">The maximum number of matches to be searched for, or zero for an unlimited search.</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>An IMatches object containing the found matches.</returns>
        IMatches IAction.Match(IGraph graph, int maxMatches, IGraphElement[] parameters)
        {
//            return Match((LGSPGraph) graph, maxMatches, parameters);
            return DynamicMatch((LGSPGraph) graph, maxMatches, parameters);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match.
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <returns>An array of elements returned by the rule.</returns>
        IGraphElement[] IAction.Modify(IGraph graph, IMatch match)
        {
//            return Modify((LGSPGraph)graph, (LGSPMatch)match);
//            return rulePattern.Modify((LGSPGraph)graph, (LGSPMatch)match);
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, (LGSPMatch) match);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, (LGSPMatch) match);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
		/// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
		IGraphElement[] IAction.Apply(IGraph graph)
        {
            LGSPMatches matches = DynamicMatch((LGSPGraph) graph, 1, null);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.matches.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.matches.First);
        }

        /// <summary>
        /// Tries to apply this rule to the given graph once.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
		/// <returns>A possibly empty array of IGraphElement instances returned by the rule,
		/// or null, if no match was found.</returns>
		IGraphElement[] IAction.Apply(IGraph graph, params IGraphElement[] parameters)
        {
            LGSPMatches matches = DynamicMatch((LGSPGraph) graph, 1, parameters);
            if(matches.Count <= 0) return null;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                return rulePattern.Modify((LGSPGraph) graph, matches.matches.First);
            else
                return rulePattern.ModifyNoReuse((LGSPGraph) graph, matches.matches.First);
        }

        #endregion

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>Always returns true.</returns>
        public bool ApplyStar(IGraph graph)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
            }
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>Always returns true.</returns>
        public bool ApplyStar(IGraph graph, params IGraphElement[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches;
            while(true)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
            }
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        public bool ApplyPlus(IGraph graph)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches = DynamicMatch(lgraph, 1, null);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
                matches = DynamicMatch(lgraph, 1, null);
            }
            while(matches.Count > 0);
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph as often as possible.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least once.</returns>
        public bool ApplyPlus(IGraph graph, params IGraphElement[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches = DynamicMatch(lgraph, 1, parameters);
            if(matches.Count <= 0) return false;
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
                matches = DynamicMatch(lgraph, 1, parameters);
            }
            while(matches.Count > 0);
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph at most max times.
        /// The rule may not require any parameters.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        public bool ApplyMinMax(IGraph graph, int min, int max)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, null);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
            }
            return true;
        }

        /// <summary>
        /// Applies this rule to the given graph at most max times.
        /// No Matched/Finished events are triggered by this function.
        /// </summary>
        /// <param name="graph">Host graph for this rule</param>
        /// <param name="min">The minimum number of applications to be "successful".</param>
        /// <param name="max">The maximum number of applications to be applied.</param>
        /// <param name="parameters">An array of graph elements (nodes and/or edges) of the types specified by RulePattern.Inputs.
        /// The array must contain at least RulePattern.Inputs.Length elements.</param>
        /// <returns>True, if the rule was applied at least min times.</returns>
        public bool ApplyMinMax(IGraph graph, int min, int max, params IGraphElement[] parameters)
        {
            LGSPGraph lgraph = (LGSPGraph) graph;
            LGSPMatches matches;
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch(lgraph, 1, parameters);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization)
                    rulePattern.Modify(lgraph, matches.matches.First);
                else
                    rulePattern.ModifyNoReuse(lgraph, matches.matches.First);
            }
            return true;
        }
    }

    public abstract class LGSPActions : BaseActions
    {
        private LGSPGraph graph;
        private IDumperFactory dumperFactory;
        private LGSPMatcherGenerator matcherGenerator;
        private String modelAssemblyName, actionsAssemblyName;
        int maxMatches = 0;

        protected Dictionary<String, LGSPAction> actions = new Dictionary<String, LGSPAction>();


        public LGSPActions(LGSPGraph lgspgraph)
        {
            graph = lgspgraph;
			graph.curActions = this;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);

            modelAssemblyName = Assembly.GetAssembly(graph.Model.GetType()).Location;
            actionsAssemblyName = Assembly.GetAssembly(this.GetType()).Location;

#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
            OnMatched += new AfterMatchHandler(AssertAllUnmappedAfterMatch);
#endif
        }

        public LGSPActions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
        {
            graph = lgspgraph;
			graph.curActions = this;
			dumperFactory = dumperfactory;
            modelAssemblyName = modelAsmName;
            actionsAssemblyName = actionsAsmName;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);
#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
            OnMatched += new AfterMatchHandler(AssertAllUnmappedAfterMatch);
#endif
        }

#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
        void AssertAllUnmappedAfterMatch(IMatches matches, bool special)
        {
            foreach(INode node in graph.GetCompatibleNodes(graph.Model.NodeModel.RootType))
            {
                LGSPNode lnode = (LGSPNode) node;
                if(lnode.mappedTo != 0)
                {
                    throw new Exception("Node \"" + graph.GetElementName(lnode) + "\" not unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
                if(lnode.negMappedTo != 0)
                {
                    throw new Exception("Node \"" + graph.GetElementName(lnode) + "\" not neg-unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
            }
            foreach(IEdge edge in graph.GetCompatibleEdges(graph.Model.EdgeModel.RootType))
            {
                LGSPEdge ledge = (LGSPEdge) edge;
                if(ledge.mappedTo != 0)
                {
                    throw new Exception("Edge \"" + graph.GetElementName(ledge) + "\" not unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
                if(ledge.negMappedTo != 0)
                {
                    throw new Exception("Edge \"" + graph.GetElementName(ledge) + "\" not neg-unmapped by action \""
                        + matches.Producer.Name + "\"!");
                }
            }
        }
#endif

        public override IGraph Graph { get { return graph; } set { graph = (LGSPGraph) value; } }

        public LGSPAction GenerateAction(LGSPAction action)
        {
            LGSPAction newAction;
            newAction = matcherGenerator.GenerateAction(graph, modelAssemblyName, actionsAssemblyName, (LGSPAction) action);
            actions[action.Name] = newAction;
            return newAction;
        }

        public LGSPAction GenerateAction(String actionName)
        {
            LGSPAction action = (LGSPAction) GetAction(actionName);
            if(action == null)
                throw new ArgumentException("\"" + actionName + "\" is not the name of an action!\n");
            return GenerateAction(action);
        }

        public LGSPAction[] GenerateActions(params LGSPAction[] oldActions)
        {
            LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                actionsAssemblyName, oldActions);
            for(int i = 0; i < oldActions.Length; i++)
                actions[oldActions[i].Name] = newActions[i];

            return newActions;
        }

        public LGSPAction[] GenerateActions(params String[] actionName)
        {
            LGSPAction[] oldActions = new LGSPAction[actionName.Length];
            for(int i = 0; i < oldActions.Length; i++)
            {
                oldActions[i] = (LGSPAction) GetAction((String) actionName[i]);
                if(oldActions[i] == null)
                    throw new ArgumentException("\"" + (String) actionName[i] + "\"' is not the name of an action!");
            }
            return GenerateActions(oldActions);
        }

        public void ReplaceAction(String actionName, LGSPAction newAction)
        {
            actions[actionName] = newAction;
        }

        public override void Custom(params object[] args)
        {
            if(args.Length == 0) goto invalidCommand;

            switch((String) args[0])
            {
                case "set_max_matches":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: set_max_matches <integer>\nIf <integer> <= 0, all matches will be matched.");

                    int maxMatches;
                    if(!int.TryParse((String) args[1], out maxMatches))
                        throw new ArgumentException("Illegal integer value specified: \"" + (String) args[1] + "\"");
                    MaxMatches = maxMatches;
                    return;

                case "gen_searchplan":
                {
                    if(graph.edgeCounts == null)
                        throw new ArgumentException("Graph not analyzed yet!\nPlease execute 'custom graph analyze'!");
                    if(args.Length < 2)
                        throw new ArgumentException("Usage: gen_searchplan <actionname>*");
                    LGSPAction[] oldActions = new LGSPAction[args.Length - 1];
                    for(int i = 0; i < oldActions.Length; i++)
                    {
                        oldActions[i] = (LGSPAction) GetAction((String) args[i + 1]);
                        if(oldActions[i] == null)
                            throw new ArgumentException("'" + (String) args[i + 1] + "' is not the name of an action!\n"
                                + "Please use 'show actions' to get a list of the available names.");
                    }
                    int startticks = Environment.TickCount;
                    LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                        actionsAssemblyName, oldActions);
                    int stopticks = Environment.TickCount;
                    Console.Write("Searchplans for actions ");
                    for(int i = 0; i < oldActions.Length; i++)
                    {
                        actions[oldActions[i].Name] = newActions[i];
                        if(i != 0) Console.Write(", ");
                        Console.Write("'" + oldActions[i].Name + "'");
                    }
                    Console.WriteLine(" generated in " + (stopticks - startticks) + " ms.");
                    return;
                }

                case "dump_sourcecode":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: dump_sourcecode <bool>\n"
                                + "If <bool> == true, C# files will be dumped for new searchplans.");

                    if(!bool.TryParse((String) args[1], out matcherGenerator.DumpDynSourceCode))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                    return;

                case "dump_searchplan":
                    if(args.Length != 2)
                        throw new ArgumentException("Usage: dump_searchplan <bool>\n"
                                + "If <bool> == true, VCG and TXT files will be dumped for new searchplans.");

                    if(!bool.TryParse((String) args[1], out matcherGenerator.DumpSearchPlan))
                        throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                    return;
            }

invalidCommand:
            throw new ArgumentException("Possible commands:\n"
                + "- set_max_matches: Sets the maximum number of matches to be found\n"
                + "     during matching\n"
                + "- gen_searchplan:  Generates a new searchplan for a given action\n"
                + "     depending on a previous graph analysis\n"
                + "- dump_sourcecode: Sets dumping of C# files for new searchplans\n"
                + "- dump_searchplan: Sets dumping of VCG and TXT files of new\n"
                + "     searchplans (with some intermediate steps)");
        }

        public override int MaxMatches { get { return maxMatches; } set { maxMatches = value; } }

        public override IEnumerable<IAction> Actions { get { foreach(IAction action in actions.Values) yield return action; } }

        public new LGSPAction GetAction(string name)
        {
            LGSPAction action;
            if(!actions.TryGetValue(name, out action)) return null;
            return action;
        }

        protected override IAction GetIAction(string name)
        {
            return GetAction(name);
        }
    }
}
