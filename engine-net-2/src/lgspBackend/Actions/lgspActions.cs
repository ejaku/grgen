/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit

//#define ASSERT_ALL_UNMAPPED_AFTER_MATCH

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An object representing an executable rule of the LGSPBackend.
    /// </summary>
    public abstract class LGSPAction
    {
        protected LGSPAction(PatternGraph patternGraph)
        {
            this.patternGraph = patternGraph;
        }

        /// <summary>
        /// The LGSPRulePattern object from which this LGSPAction object has been created.
        /// </summary>
        public abstract LGSPRulePattern rulePattern { get; }

        /// <summary>
        /// The PatternGraph object of the main graph
        /// </summary>
        public readonly PatternGraph patternGraph;

        /// <summary>
        /// The name of the action (without prefixes)
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// null if this is a global type, otherwise the package the type is contained in.
        /// </summary>
        public string Package
        {
            get { return patternGraph.Package; }
        }

        /// <summary>
        /// The name of the type in case of a global type,
        /// the name of the type prefixed by the name of the package otherwise.
        /// </summary>
        public string PackagePrefixedName
        {
            get { return patternGraph.PackagePrefixedName; }
        }
    }


    /// <summary>
    /// A container of rules also managing some parts of rule application with sequences.
    /// Abstract base class with empty actions, the derived classes fill the actions dictionary.
    /// </summary>
    public abstract class LGSPActions : BaseActions
    {
        private LGSPGraph graph;

        private readonly LGSPMatcherGenerator matcherGenerator;
        private readonly String modelAssemblyName;
        private readonly String actionsAssemblyName;

        protected readonly Dictionary<String, LGSPAction> actions = new Dictionary<String, LGSPAction>(); // action names -> action objects, filled by derived classes

        private static int actionID = 0;

        protected readonly Dictionary<String, String> customCommandsToDescriptions;


        /// <summary>
        /// Constructs a new LGSPActions instance.
        /// </summary>
        /// <param name="lgspgraph">The associated graph.</param>
        protected LGSPActions(LGSPGraph lgspgraph)
        {
            graph = lgspgraph;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);

            modelAssemblyName = Assembly.GetAssembly(graph.Model.GetType()).Location;
            actionsAssemblyName = Assembly.GetAssembly(this.GetType()).Location;

            customCommandsToDescriptions = new Dictionary<string, string>();
            FillCustomCommandDescriptions();

#if ASSERT_ALL_UNMAPPED_AFTER_MATCH
            OnMatched += new AfterMatchHandler(AssertAllUnmappedAfterMatch);
#endif
        }

        /// <summary>
        /// Constructs a new LGSPActions instance.
        /// This constructor is deprecated.
        /// </summary>
        /// <param name="lgspgraph">The associated graph.</param>
        /// <param name="modelAsmName">The name of the model assembly.</param>
        /// <param name="actionsAsmName">The name of the actions assembly.</param>
        protected LGSPActions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
        {
            graph = lgspgraph;
            modelAssemblyName = modelAsmName;
            actionsAssemblyName = actionsAsmName;
            matcherGenerator = new LGSPMatcherGenerator(graph.Model);

            customCommandsToDescriptions = new Dictionary<string, string>();
            FillCustomCommandDescriptions();

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

        /// <summary>
        /// Loads a LGSPActions instance from the given file.
        /// If the file is a ".cs" file it will be compiled first.
        /// </summary>
        public static LGSPActions LoadActions(String actionFilename, LGSPGraph graph)
        {
            Assembly assembly;
            String assemblyName;

            String extension = Path.GetExtension(actionFilename);
            if(extension.Equals(".cs", StringComparison.OrdinalIgnoreCase))
            {
                CSharpCodeProvider compiler = new CSharpCodeProvider();
                CompilerParameters compParams = new CompilerParameters();
                compParams.ReferencedAssemblies.Add("System.dll");
                compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(IBackend)).Location);
                compParams.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(LGSPActions)).Location);
                compParams.ReferencedAssemblies.Add(graph.modelAssemblyName);

                //                compParams.GenerateInMemory = true;
                compParams.CompilerOptions = "/optimize";
                compParams.OutputAssembly = String.Format("lgsp-action-assembly-{0:00000}.dll", actionID++);

                CompilerResults compResults = compiler.CompileAssemblyFromFile(compParams, actionFilename);
                if(compResults.Errors.HasErrors)
                {
                    String errorMsg = compResults.Errors.Count + " Errors:";
                    foreach(CompilerError error in compResults.Errors)
                    {
                        errorMsg += String.Format("\r\nLine: {0} - {1}", error.Line, error.ErrorText);
                    }
                    throw new ArgumentException("Illegal actions C# source code: " + errorMsg);
                }

                assembly = compResults.CompiledAssembly;
                assemblyName = compParams.OutputAssembly;
            }
            else if(extension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
            {
                assembly = Assembly.LoadFrom(actionFilename);
                assemblyName = actionFilename;
                LGSPBackend.Instance.AddAssembly(assembly);          // TODO: still needed??
            }
            else
            {
                throw new ArgumentException("The action filename must be either a .cs or a .dll filename!");
            }

            Type actionsType = null;
            try
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
                    if(type.BaseType == typeof(LGSPActions))
                    {
                        if(actionsType != null)
                        {
                            throw new ArgumentException(
                                "The given action file contains more than one LGSPActions implementation!");
                        }
                        actionsType = type;
                    }
                }
            }
            catch(ReflectionTypeLoadException e)
            {
                String errorMsg = "";
                foreach(Exception ex in e.LoaderExceptions)
                {
                    errorMsg += "- " + ex.Message + Environment.NewLine;
                }
                if(errorMsg.Length == 0)
                    errorMsg = e.Message;
                throw new ArgumentException(errorMsg);
            }
            if(actionsType == null)
                throw new ArgumentException("The given action file doesn't contain an LGSPActions implementation!");

            LGSPActions actions = (LGSPActions)Activator.CreateInstance(actionsType, graph, graph.modelAssemblyName, assemblyName);

            if(graph.Model.MD5Hash != actions.ModelMD5Hash)
                throw new ArgumentException("The given action file has been compiled with another model assembly!");

            return actions;
        }

        /// <summary>
        /// The associated graph.
        /// </summary>
        public override IGraph Graph
        {
            get { return graph; }
            set { graph = (LGSPGraph) value; }
        }

        /// <summary>
        /// The statistics file used for generating the matchers, null if none was used.
        /// </summary>
        public abstract string StatisticsPath { get; }

        /// <summary>
        /// Replaces the given action by a new action instance with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="action">The action to be replaced.</param>
        /// <returns>The new action instance.</returns>
        public LGSPAction GenerateAction(LGSPAction action)
        {
            matcherGenerator.LazyNegativeIndependentConditionEvaluation = LazyNIC;
            matcherGenerator.InlineIndependents = InlineIndependents;
            matcherGenerator.Profile = Profile;
            LGSPAction newAction = matcherGenerator.GenerateAction(graph, modelAssemblyName, actionsAssemblyName, (LGSPAction) action);
            actions[action.Name] = newAction;
            return newAction;
        }

        /// <summary>
        /// Replaces the given action by a new action instance with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="actionName">The name of the action to be replaced.</param>
        /// <returns>The new action instance.</returns>
        public LGSPAction GenerateAction(String actionName)
        {
            LGSPAction action = (LGSPAction) GetAction(actionName);
            if(action == null)
                throw new ArgumentException("\"" + actionName + "\" is not the name of an action!\n");
            return GenerateAction(action);
        }

        /// <summary>
        /// Replaces the given actions by new action instances with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="oldActions">An array of actions to be replaced.</param>
        /// <returns>An array with the new action instances.</returns>
        public LGSPAction[] GenerateActions(params LGSPAction[] oldActions)
        {
            matcherGenerator.LazyNegativeIndependentConditionEvaluation = LazyNIC;
            matcherGenerator.InlineIndependents = InlineIndependents;
            matcherGenerator.Profile = Profile;
            LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                actionsAssemblyName, oldActions);
            for(int i = 0; i < oldActions.Length; ++i)
            {
                actions[oldActions[i].Name] = newActions[i];
            }

            return newActions;
        }

        /// <summary>
        /// Replaces the given actions by new action instances with a search plan adapted
        /// to the current analysis data of the associated graph.
        /// </summary>
        /// <param name="actionNames">An array of names of actions to be replaced.</param>
        /// <returns>An array with the new action instances.</returns>
        public LGSPAction[] GenerateActions(params String[] actionNames)
        {
            LGSPAction[] oldActions = new LGSPAction[actionNames.Length];
            for(int i = 0; i < oldActions.Length; i++)
            {
                oldActions[i] = (LGSPAction) GetAction((String) actionNames[i]);
                if(oldActions[i] == null)
                    throw new ArgumentException("\"" + (String) actionNames[i] + "\"' is not the name of an action!");
            }
            return GenerateActions(oldActions);
        }

        /// <summary>
        /// Replaces a given action by another one.
        /// For internal use.
        /// </summary>
        /// <param name="actionName">The name of the action to be replaced.</param>
        /// <param name="newAction">The new action.</param>
        public void ReplaceAction(String actionName, LGSPAction newAction)
        {
            actions[actionName] = newAction;
        }

        private void FillCustomCommandDescriptions()
        {
            customCommandsToDescriptions.Add("gen_searchplan",
                "- gen_searchplan: Generates a new searchplan for a given action\n" +
                "     depending on a previous graph analysis.\n");
            customCommandsToDescriptions.Add("explain",
                "- explain: explains the searchplan in use for a given action.\n");
            customCommandsToDescriptions.Add("dump_sourcecode",
                "- dump_sourcecode: Sets dumping of C# files for new searchplans.\n");
            customCommandsToDescriptions.Add("dump_searchplan",
                "- dump_searchplan: Sets dumping of VCG and TXT files of new\n" +
                "     searchplans (with some intermediate steps).\n");
        }

        /// <summary>
        /// The action-backend dependent commands that are available, and a description of each command.
        /// </summary>
        public override IDictionary<String, String> CustomCommandsAndDescriptions
        {
            get { return customCommandsToDescriptions; }
        }

        /// <summary>
        /// Does action-backend dependent stuff.
        /// </summary>
        /// <param name="args">Any kind of parameters for the stuff to do; first parameter has to be the command</param>
        public override void Custom(params object[] args)
        {
            if(args.Length == 0)
                throw new ArgumentException("No command given");

            String command = (String)args[0];
            switch(command)
            {
            case "gen_searchplan":
                {
                    if(graph.statistics.edgeCounts == null)
                        throw new ArgumentException("Graph not analyzed yet!\nPlease execute 'custom graph analyze'!");
                    LGSPAction[] oldActions;
                    if(args.Length == 1)
                    {
                        oldActions = new LGSPAction[actions.Count];
                        int i = 0;
                        foreach(LGSPAction action in actions.Values)
                        {
                            oldActions[i] = action;
                            ++i;
                        }
                    }
                    else
                    {
                        oldActions = new LGSPAction[args.Length - 1];
                        for(int i = 0; i < oldActions.Length; i++)
                        {
                            oldActions[i] = (LGSPAction)GetAction((String)args[i + 1]);
                            if(oldActions[i] == null)
                            {
                                throw new ArgumentException("'" + (String)args[i + 1] + "' is not the name of an action!\n"
                                    + "Please use 'show actions' to get a list of the available names.");
                            }
                        }
                    }

                    int startticks = Environment.TickCount;
                    matcherGenerator.LazyNegativeIndependentConditionEvaluation = LazyNIC;
                    matcherGenerator.InlineIndependents = InlineIndependents;
                    matcherGenerator.Profile = Profile;
                    LGSPAction[] newActions = matcherGenerator.GenerateActions(graph, modelAssemblyName,
                        actionsAssemblyName, oldActions);
                    int stopticks = Environment.TickCount;
                    ConsoleUI.outWriter.Write("Searchplans for actions ");
                    for(int i = 0; i < oldActions.Length; i++)
                    {
                        actions[oldActions[i].Name] = newActions[i];
                        if(i != 0)
                            ConsoleUI.outWriter.Write(", ");
                        ConsoleUI.outWriter.Write("'" + oldActions[i].Name + "'");
                    }
                    ConsoleUI.outWriter.WriteLine(" generated in " + (stopticks - startticks) + " ms.");
                    return;
                }

            case "dump_sourcecode":
                if(args.Length != 2)
                {
                    throw new ArgumentException("Usage: dump_sourcecode <bool>\n"
                        + "If <bool> == true, C# files will be dumped for new searchplans.");
                }

                if(!bool.TryParse((String) args[1], out matcherGenerator.DumpDynSourceCode))
                    throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                return;

            case "dump_searchplan":
                if(args.Length != 2)
                {
                    throw new ArgumentException("Usage: dump_searchplan <bool>\n"
                        + "If <bool> == true, VCG and TXT files will be dumped for new searchplans.");
                }

                if(!bool.TryParse((String) args[1], out matcherGenerator.DumpSearchPlan))
                    throw new ArgumentException("Illegal bool value specified: \"" + (String) args[1] + "\"");
                return;

            case "explain":
                {
                    if(args.Length != 2)
                    {
                        throw new ArgumentException("Usage: explain <name>\n"
                            + "Explains the searchplan of the given action.");
                    }
                    LGSPAction action = (LGSPAction)GetAction((String)args[1]);
                    if(action == null)
                    {
                        throw new ArgumentException("'" + (String)args[1] + "' is not the name of an action!\n"
                            + "Please use 'show actions' to get a list of the available names.");
                    }

                    if(action.patternGraph.schedules[0] == null)
                    {
                        LGSPGraphStatistics graphStatistics = null;
                        if(StatisticsPath != null)
                        {
                            ConsoleUI.outWriter.WriteLine("static search plans from " + StatisticsPath);
                            graphStatistics = new LGSPGraphStatistics(graph.Model);
                            GraphStatisticsParserSerializer parserSerializer = new GraphStatisticsParserSerializer(graphStatistics);
                            parserSerializer.Parse(StatisticsPath);
                        }
                        else
                            ConsoleUI.outWriter.WriteLine("static search plans");
                        LGSPMatcherGenerator matcherGen = new LGSPMatcherGenerator(graph.Model);
                        matcherGen.FillInStaticSearchPlans(graphStatistics, InlineIndependents, action);
                    }
                    SourceBuilder sb = new SourceBuilder();
                    foreach(KeyValuePair<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpattern
                        in action.rulePattern.patternGraph.usedSubpatterns)
                    {
                        usedSubpattern.Key.patternGraph.Explain(sb, graph.Model);
                    }
                    action.patternGraph.Explain(sb, graph.Model);
                    ConsoleUI.outWriter.WriteLine(sb.ToString());
                    return;
                }

            default:
                throw new ArgumentException("Unknown command: " + command);
            }
        }

        /// <summary>
        /// Enumerates all actions managed by this LGSPActions instance.
        /// </summary>
        public override IEnumerable<IAction> Actions
        {
            get
            {
                foreach(IAction action in actions.Values)
                {
                    yield return action;
                }
            }
        }

        /// <summary>
        /// Gets the action with the given name.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        /// <returns>The action with the given name, or null, if no such action exists.</returns>
        public override IAction GetAction(string name)
        {
            LGSPAction action;
            if(!actions.TryGetValue(name, out action))
                return null;
            return (IAction)action;
        }
    }


    /// <summary>
    /// Abstract base class for generated subpattern matching actions
    /// each object of an inheriting class represents a subpattern matching tasks
    /// which might be stored on the open tasks stack and executed later on.
    /// In addition to user-specified subpatterns, alternatives are mapped to subpattern actions, too.
    /// </summary>
    public abstract class LGSPSubpatternAction
    {
        protected LGSPSubpatternAction(PatternGraph[] patternGraphs)
        {
            this.patternGraphs = patternGraphs;
        }

        /// <summary>
        /// The PatternGraph object from which this matching task object has been created
        /// </summary>
        protected PatternGraph patternGraph;

        /// <summary>
        /// The PatternGraph objects from which this matching task object has been created
        /// (non-null in case of an alternative, contains the pattern graphs of the alternative cases then)
        /// </summary>
        protected readonly PatternGraph[] patternGraphs;

        /// <summary>
        /// The action execution environment which contains the host graph in which to search for matches
        /// </summary>
        protected LGSPActionExecutionEnvironment actionEnv;

        /// <summary>
        /// The subpattern actions which have to be executed until a full match is found
        /// The inheriting class contains the preset subpattern connection elements
        /// </summary>
        protected Stack<LGSPSubpatternAction> openTasks;

        /// <summary>
        /// Entry point to the temporary match object stack representing the pattern nesting from innermost outwards.
        /// Needed for patternpath checking in negatives/independents, used as attachment point / is top of stack.
        /// </summary>
        public IMatch matchOfNestingPattern;

        /// <summary>
        /// Last match at the previous nesting level in the temporary match object stack representing the pattern nesting from innermost outwards.
        /// Needed for patternpath checking in negatives/independents, used as starting point of patternpath isomorphy checks.
        /// </summary>
        public IMatch lastMatchAtPreviousNestingLevel;

        /// <summary>
        /// Tells whether this subpattern has to search the pattern path when matching
        /// </summary>
        public bool searchPatternpath;

        /// <summary>
        /// Searches for the subpattern as specified by RulePattern.
        /// Takes care of search state as given by found partial matches, isoSpace to search in
        /// and maximum number of matches to search for(zero = find all matches)
        /// (and open tasks via this).
        /// </summary>
        public abstract void myMatch(List<Stack<IMatch>> foundPartialMatches, int maxMatches, int isoSpace);

        /// <summary>
        /// Searches for the subpattern as specified by RulePattern,
        /// with a matcher that can be called from a parallelized rule matcher.
        /// Maps to the normal matcher if the subpattern is not used from any parallelized rule.
        /// </summary>
        public virtual void myMatch_parallelized(List<Stack<IMatch>> foundPartialMatches, int maxMatches, int isoSpace, int threadId)
        {
            myMatch(foundPartialMatches, maxMatches, isoSpace);
        }
    }


    /// <summary>
    /// Class containing global functions for checking whether node/edge is matched on patternpath
    /// </summary>
    public sealed class PatternpathIsomorphyChecker
    {
        public static bool IsMatched(LGSPNode node, IMatch lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(lastMatchAtPreviousNestingLevel!=null);

            // move through matches stack backwards to starting rule,
            // check if node is already matched somewhere on the derivation path
            IMatch match = lastMatchAtPreviousNestingLevel;
            while(match != null)
            {
                for(int i = 0; i < match.NumberOfNodes; ++i)
                {
                    if(match.getNodeAt(i) == node)
                        return true;
                }
                match = match.MatchOfEnclosingPattern;
            }
            return false;
        }

        public static bool IsMatched(LGSPEdge edge, IMatch lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(lastMatchAtPreviousNestingLevel != null);

            // move through matches stack backwards to starting rule,
            // check if edge is already matched somewhere on the derivation path
            IMatch match = lastMatchAtPreviousNestingLevel;
            while(match != null)
            {
                for(int i = 0; i < match.NumberOfEdges; ++i)
                {
                    if(match.getEdgeAt(i) == edge)
                        return true;
                }
                match = match.MatchOfEnclosingPattern;
            }
            return false;
        }
    }
}
