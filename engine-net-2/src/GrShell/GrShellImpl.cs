/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using ASTdapter;
using grIO;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.grShell
{
    // TODO: the classification as struct is dubious - change to class?
    public struct Param
    {
        public String Key; // the attribute name

        // for basic types, enums
        public String Value; // the attribute value

        // for set, map attributed
        public String Type; // set/map(domain) type, array/deque value type
        public String TgtType; // map target type, array/deque index type
        public ArrayList Values; // set/map(domain) values, array/deque values
        public ArrayList TgtValues; // map target values, array/deque index values

        public Param(String key)
        {
            Key = key;
            Value = null;
            Type = null;
            TgtType = null;
            Values = null;
            TgtValues = null;
        }

        public Param(String key, String value)
        {
            Key = key;
            Value = value;
            Type = null;
            TgtType = null;
            Values = null;
            TgtValues = null;
        }

        public Param(String key, String value, String type)
        {
            Key = key;
            Value = value;
            Type = type;
            TgtType = null;
            Values = new ArrayList();
            TgtValues = null;
        }

        public Param(String key, String value, String type, String tgtType)
        {
            Key = key;
            Value = value;
            Type = type;
            TgtType = tgtType;
            Values = new ArrayList();
            TgtValues = new ArrayList();
        }
    }

    public class ElementDef
    {
        public String ElemName;
        public String VarName;
        public String TypeName;
        public ArrayList Attributes;

        public ElementDef(String elemName, String varName, String typeName, ArrayList attributes)
        {
            ElemName = elemName;
            VarName = varName;
            TypeName = typeName;
            Attributes = attributes;
        }
    }

    public class ShellGraphProcessingEnvironment
    {
        public IGraphProcessingEnvironment ProcEnv;

        public DumpInfo DumpInfo;
        public SubruleDebuggingConfiguration SubruleDebugConfig;
        public VCGFlags VcgFlags = VCGFlags.OrientTopToBottom | VCGFlags.EdgeLabels;
        public ParserPackage Parser = null;

        public String BackendFilename;
        public String[] BackendParameters;
        public String ModelFilename;
        public String ActionsFilename = null;

        public Dictionary<string, INamedGraph> NameToSubgraph = new Dictionary<string, INamedGraph>(); // maps subgraph name to subgraph


        public ShellGraphProcessingEnvironment(IGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            LGSPNamedGraph Graph = new LGSPNamedGraph((LGSPGraph)graph);
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
            ProcEnv = new LGSPGraphProcessingEnvironment(Graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public ShellGraphProcessingEnvironment(INamedGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            LGSPNamedGraph Graph = (LGSPNamedGraph)graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            SubruleDebugConfig = new SubruleDebuggingConfiguration();
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
            ProcEnv = new LGSPGraphProcessingEnvironment(Graph, null);
            NameToSubgraph.Add(Graph.Name, Graph);
        }

        public ShellGraphProcessingEnvironment Clone(string name)
        {
            string realname = (name == null) ? ProcEnv.Graph.Name + "-clone" : name;
            ShellGraphProcessingEnvironment result = new ShellGraphProcessingEnvironment(ProcEnv.Graph.Clone(realname),
                BackendFilename, BackendParameters, ModelFilename);
            result.ProcEnv.Actions = this.ProcEnv.Actions;
            result.DumpInfo = this.DumpInfo;
            result.SubruleDebugConfig = this.SubruleDebugConfig;
            result.VcgFlags = this.VcgFlags;
            result.Parser = this.Parser;
            result.ActionsFilename = this.ActionsFilename;
            result.ProcEnv.EmitWriter = this.ProcEnv.EmitWriter;
            return result;
        }
    }

    public interface IGrShellUI
    {
        void ShowMsgAskForEnter(string msg);
        bool ShowMsgAskForYesNo(string msg);
        string ShowMsgAskForString(string msg);
    }

    public class EOFException : IOException
    {
    }

    /// <summary>
    /// Console interface for simple user prompts (e.g. for debugging)
    /// </summary>
    public class GrShellConsoleUI : IGrShellUI
    {
        protected TextReader in_;
        protected TextWriter out_;

        public GrShellConsoleUI(TextReader in_, TextWriter out_)
        {
            this.in_ = in_;
            this.out_ = out_;
        }

        public string ReadOrEofErr()
        {
            string result = this.in_.ReadLine();
            if (result == null) {
                throw new EOFException();
            }
            return result;
        }

        public void ShowMsgAskForEnter(string msg)
        {
            this.out_.Write(msg + " [enter] ");
            ReadOrEofErr();
        }

        public bool ShowMsgAskForYesNo(string msg)
        {
            while (true)
            {
                this.out_.Write(msg + " [y(es)/n(o)] ");
                string result = ReadOrEofErr();
                if (result.Equals("y", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("yes", StringComparison.InvariantCultureIgnoreCase)) {
                    return true;
                } else if (result.Equals("n", StringComparison.InvariantCultureIgnoreCase) ||
                    result.Equals("no", StringComparison.InvariantCultureIgnoreCase)) {
                    return false;
                }
            }
        }

        public string ShowMsgAskForString(string msg)
        {
            this.out_.Write(msg);
            return ReadOrEofErr();
        }
    }

    public class StatisticsSource
    {
        public StatisticsSource(IGraph graph, IActionExecutionEnvironment actionEnv)
        {
            this.graph = graph;
            this.actionEnv = actionEnv;
        }

        public int MatchesFound
        {
            get { return actionEnv.PerformanceInfo.MatchesFound; }
        }

        public int RewritesPerformed
        {
            get { return actionEnv.PerformanceInfo.RewritesPerformed; }
        }

        public long GraphChanges
        {
            get { return graph.ChangesCounter; }
        }

        public IActionExecutionEnvironment ActionEnv
        {
            get { return actionEnv; }
        }

        IGraph graph;
        IActionExecutionEnvironment actionEnv;
    }


    public class GrShellImpl
    {
        public static readonly String VersionString = "GrShell v4.3";

        IBackend curGraphBackend = new LGSPBackend();
        String backendFilename = null;
        String[] backendParameters = null;

        List<ShellGraphProcessingEnvironment> shellProcEnvs = new List<ShellGraphProcessingEnvironment>();
        ShellGraphProcessingEnvironment curShellProcEnv = null;

        bool silence = false; // node/edge created successfully messages
        bool silenceExec = false; // print match statistics during sequence execution on timer
        bool cancelSequence = false;

        public ElementRealizers realizers = new ElementRealizers();

        Debugger debugger = null;

        bool pendingDebugEnable = false;
        String debugLayout = "Orthogonal";
        public bool nonDebugNonGuiExitOnError = false;
        public bool showIncludes = false;
        public TextWriter debugOut = System.Console.Out;
        public TextWriter errOut = System.Console.Error;
        public IGrShellUI UserInterface = new GrShellConsoleUI(Console.In, Console.Out);

        List<String> newGraphExternalAssembliesReferenced = new List<String>();
        String newGraphStatistics = null;
        bool newGraphKeepDebug = false;
        bool newGraphLazyNIC = false;
        bool newGraphNoinline = false;
        bool newGraphProfile = false;
        //bool newGraphProfile = true; // uncomment to test profiling

        /// <summary>
        /// Maps layouts to layout option names to their values.
        /// This only reflects the settings made by the user and may even contain illegal entries,
        /// if the options were set before yComp was attached.
        /// </summary>
        Dictionary<String, Dictionary<String, String>> debugLayoutOptions = new Dictionary<String, Dictionary<String, String>>();

        IWorkaround workaround = WorkaroundManager.Workaround;
        public LinkedList<GrShellTokenManager> TokenSourceStack = new LinkedList<GrShellTokenManager>();

        public GrShellImpl()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
        }

        public bool OperationCancelled { get { return cancelSequence; } }
        public IWorkaround Workaround { get { return workaround; } }
        public bool InDebugMode { get { return debugger != null && !debugger.ConnectionLost; } }

        public static void PrintVersion()
        {
            Console.WriteLine(VersionString + " (enter \"help\" for a list of commands)");
        }

        private bool BackendExists()
        {
            if(curGraphBackend == null)
            {
                errOut.WriteLine("No backend. Select a backend, first.");
                return false;
            }
            return true;
        }

        private bool GraphExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.NamedGraph == null)
            {
                errOut.WriteLine("No graph. Make a new graph, first.");
                return false;
            }
            return true;
        }

        private bool ActionsExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.Actions == null)
            {
                errOut.WriteLine("No actions. Select an actions object, first.");
                return false;
            }
            return true;
        }

        public ShellGraphProcessingEnvironment CurrentShellProcEnv { get { return curShellProcEnv; } }

        public INamedGraph CurrentGraph
        {
            get
            {
                if(!GraphExists()) return null;
                return curShellProcEnv.ProcEnv.NamedGraph;
            }
        }

        public IActions CurrentActions
        {
            get
            {
                if (!ActionsExists()) return null;
                return curShellProcEnv.ProcEnv.Actions;
            }
        }

        public bool Silence
        {
            get
            {
                return silence;
            }
            set
            {
                silence = value;
                if (silence) errOut.WriteLine("Disabled \"new node/edge created successfully\"-messages");
                else errOut.WriteLine("Enabled \"new node/edge created successfully\"-messages");
            }
        }

        public bool SilenceExec
        {
            get
            {
                return silenceExec;
            }
            set
            {
                silenceExec = value;
                if(silenceExec) errOut.WriteLine("Disabled printing match statistics during non-debug sequence execution every second");
                else errOut.WriteLine("Enabled printing match statistics during non-debug sequence execution every second");
            }
        }

        public IGraphElement GetElemByVar(String varName)
        {
            if(!GraphExists()) return null;
            object elem = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(elem == null)
            {
                errOut.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            if(!(elem is IGraphElement))
            {
                errOut.WriteLine("\"{0}\" is not a graph element!", varName);
                return null;
            }
            return (IGraphElement) elem;
        }

        public IGraphElement GetElemByVarOrNull(String varName)
        {
            if(!GraphExists()) return null;
            return curShellProcEnv.ProcEnv.GetVariableValue(varName) as IGraphElement;
        }

        public IGraphElement GetElemByName(String elemName)
        {
            if(!GraphExists()) return null;
            IGraphElement elem = curShellProcEnv.ProcEnv.NamedGraph.GetGraphElement(elemName);
            if(elem == null)
            {
                errOut.WriteLine("Unknown graph element: \"{0}\"", elemName);
                return null;
            }
            return elem;
        }

        public INode GetNodeByVar(String varName)
        {
            IGraphElement elem = GetElemByVar(varName);
            if(elem == null) return null;
            if(!(elem is INode))
            {
                errOut.WriteLine("\"{0}\" is not a node!", varName);
                return null;
            }
            return (INode) elem;
        }

        public INode GetNodeByName(String elemName)
        {
            IGraphElement elem = GetElemByName(elemName);
            if(elem == null) return null;
            if(!(elem is INode))
            {
                errOut.WriteLine("\"{0}\" is not a node!", elemName);
                return null;
            }
            return (INode) elem;
        }

        public IEdge GetEdgeByVar(String varName)
        {
            IGraphElement elem = GetElemByVar(varName);
            if(elem == null) return null;
            if(!(elem is IEdge))
            {
                errOut.WriteLine("\"{0}\" is not an edge!", varName);
                return null;
            }
            return (IEdge) elem;
        }

        public IEdge GetEdgeByName(String elemName)
        {
            IGraphElement elem = GetElemByName(elemName);
            if(elem == null) return null;
            if(!(elem is IEdge))
            {
                errOut.WriteLine("\"{0}\" is not an edge!", elemName);
                return null;
            }
            return (IEdge) elem;
        }

        public NodeType GetNodeType(String typeName)
        {
            if(!GraphExists()) return null;
            NodeType type = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(typeName);
            if(type == null)
            {
                errOut.WriteLine("Unknown node type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public EdgeType GetEdgeType(String typeName)
        {
            if(!GraphExists()) return null;
            EdgeType type = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(typeName);
            if(type == null)
            {
                errOut.WriteLine("Unknown edge type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public GrGenType GetGraphElementType(String typeName)
        {
            if(!GraphExists()) return null;
            GrGenType type = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(typeName);
            if(type != null)
                return type;
            type = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(typeName);
            if(type != null)
                return type;
            errOut.WriteLine("Unknown graph element type: \"{0}\"", typeName);
            return null;
        }

        public ShellGraphProcessingEnvironment GetShellGraphProcEnv(String graphName)
        {
            foreach(ShellGraphProcessingEnvironment shellGraph in shellProcEnvs)
                if(shellGraph.ProcEnv.NamedGraph.Name == graphName)
                    return shellGraph;

            errOut.WriteLine("Unknown graph: \"{0}\"", graphName);
            return null;
        }

        public ShellGraphProcessingEnvironment GetShellGraphProcEnv(int index)
        {
            if((uint) index >= (uint) shellProcEnvs.Count)
                errOut.WriteLine("Graph index out of bounds!");

            return shellProcEnvs[index];
        }

        public IGraphProcessingEnvironment GetProcEnv()
        {
            return curShellProcEnv.ProcEnv;
        }

        public int NumGraphs
        {
            get { return this.shellProcEnvs.Count; }
        }

        public void HandleSequenceParserException(SequenceParserException ex)
        {
            IAction action = ex.Action;
            errOut.WriteLine(ex.Message);

            debugOut.Write("Prototype: {0}", ex.Name);
            if(action == null)
            {
                debugOut.WriteLine("");
                return;
            }

            if(action.RulePattern.Inputs.Length != 0)
            {
                debugOut.Write("(");
                bool first = true;
                foreach(GrGenType type in action.RulePattern.Inputs)
                {
                    debugOut.Write("{0}{1}", first ? "" : ", ", type.PackagePrefixedName);
                    first = false;
                }
                debugOut.Write(")");
            }
            if(action.RulePattern.Outputs.Length != 0)
            {
                debugOut.Write(" : (");
                bool first = true;
                foreach(GrGenType type in action.RulePattern.Outputs)
                {
                    debugOut.Write("{0}{1}", first ? "" : ", ", type.PackagePrefixedName);
                    first = false;
                }
                debugOut.Write(")");
            }
            debugOut.WriteLine();
        }

        public IAction GetAction(String actionName, int numParams, int numReturns, bool retSpecified)
        {
            if(!ActionsExists()) return null;
            IAction action = curShellProcEnv.ProcEnv.Actions.GetAction(actionName);
            if(action == null)
            {
                debugOut.WriteLine("Unknown action: \"{0}\"", actionName);
                return null;
            }
            if(action.RulePattern.Inputs.Length != numParams || action.RulePattern.Outputs.Length != numReturns && retSpecified)
            {
                if(action.RulePattern.Inputs.Length != numParams && action.RulePattern.Outputs.Length != numReturns)
                    debugOut.WriteLine("Wrong number of parameters and return values for action \"{0}\"!", actionName);
                else if(action.RulePattern.Inputs.Length != numParams)
                    debugOut.WriteLine("Wrong number of parameters for action \"{0}\"!", actionName);
                else
                    debugOut.WriteLine("Wrong number of return values for action \"{0}\"!", actionName);
                debugOut.Write("Prototype: {0}", actionName);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    debugOut.Write("(");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Inputs)
                    {
                        debugOut.Write("{0}{1}", first ? "" : ",", type.PackagePrefixedName);
                    }
                    debugOut.Write(")");
                }
                if(action.RulePattern.Outputs.Length != 0)
                {
                    debugOut.Write(" : (");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Outputs)
                    {
                        debugOut.Write("{0}{1}", first ? "" : ",", type.PackagePrefixedName);
                    }
                    debugOut.Write(")");
                }
                debugOut.WriteLine();
                return null;
            }
            return action;
        }

        #region Help text methods

        public void Help(List<String> commands)
        {
            if(commands.Count != 0)
            {
                switch(commands[0])
                {
                    case "new":
                        HelpNew(commands);
                        return;

                    case "select":
                        HelpSelect(commands);
                        return;

                    case "delete":
                        HelpDelete(commands);
                        return;

                    case "retype":
                        HelpRetype(commands);
                        return;

                    case "redirect":
                        HelpRedirect(commands);
                        return;

                    case "show":
                        HelpShow(commands);
                        return;

                    case "debug":
                        HelpDebug(commands);
                        return;

                    case "dump":
                        HelpDump(commands);
                        return;

                    case "custom":
                        HelpCustom(commands);
                        return;

                    case "validate":
                        HelpValidate(commands);
                        return;

                    case "import":
                        HelpImport(commands);
                        return;

                    case "export":
                        HelpExport(commands);
                        return;

                    case "record":
                        HelpRecord(commands);
                        return;

                    case "replay":
                        HelpReplay(commands);
                        return;

                    default:
                        debugOut.WriteLine("No further help available.\n");
                        break;
                }
            }

            // Not mentioned: open graph, grs

            debugOut.WriteLine("\nList of available commands:\n"
                + " - new ...                   Creation commands\n"
                + " - select ...                Selection commands\n"
                + " - include <filename>        Includes and executes the given .grs-script\n"
                + " - silence (on | off)        Switches \"new ... created\" messages on/off\n"
                + " - silence exec (on | off)   Print match statistics during execution every sec?\n"
                + " - delete ...                Deletes something\n"
                + " - retype ...                Retype commands\n"
                + " - clear graph [<graph>]     Clears the current or the given graph\n"
                + " - show ...                  Shows something\n"
                + " - node type <n1> is <n2>    Tells whether the node n1 has a type compatible\n"
                + "                             to the type of n2\n"
                + " - edge type <e1> is <e2>    Tells whether the edge e1 has a type compatible\n"
                + "                             to the type of e2\n"
                + " - debug ...                 Debugging related commands\n"
                + " - (exec | xgrs) <xgrs>      Executes the given extended graph rewrite sequence\n"
                + " - validate ...              Validate related commands\n"
                + " - dump ...                  Dump related commands\n"
                + " - save graph <filename>     Saves the current graph as a GrShell script\n"
                + " - export ...                Exports the current graph.\n"
                + " - import ...                Imports a graph instance and/or a graph model\n"
                + " - record ...                Records the changes of the current graph\n"
                + " - replay ...                Replays the recorded changes to a graph\n"
                + " - echo <text>               Writes the given text to the console\n"
                + " - custom graph ...          Graph backend specific commands\n"
                + " - custom actions ...        Action backend specific commands\n"
                + " - redirect ...              Redirects edges or emit instruction output\n"
                + " - sync io                   Writes out all files (grIO framework)\n"
                + " - parse file <filename>     Parses the given file (ASTdapter framework)\n"
                + " - parse <text>              Parses the given string (ASTdapter framework)\n"
                + " - randomseed (time|<seed>)  Sets the seed of the random number generator\n"
                + "                             to the current time or the given integer\n"
                + " - <elem>.<member>           Shows the given graph element member\n"
                + " - <elem>.<member> = <val>   Sets the value of the given element member\n"
                + " - <elem>.<memb>.add(<val>)  Adds the value to the given set member\n"
                + " - <elem>.<memb>.add(<k>,<v>)Adds values k->v to the given map member\n"
                + " - <elem>.rem(<val>)         Removes value from the given set/map member\n"
                + " - <var> = <exp>             Assigns the given expression to <var>, <exp>:\n"
                + "                               - <elem>\n"
                + "                               - <elem>.<member>\n"
                + "                               - <val>, a value literal (see user manual)\n"
                + " - askfor                    Waits until the user presses enter\n"
                + " - <var> = askfor <type>     Asks the user to input a value of given type\n"
                + "                             a node/edge is to be entered in yComp (debug\n"
                + "                             mode must be enabled); other values are to be\n"
                + "                             entered on the keyboard (format as in <expr>\n"
                + "                             but constant only)\n"
                + " - ! <command>               Executes the given system command\n"
                + " - help <command>*           Displays this help or help about a command\n"
                + " - exit | quit               Exits the GrShell\n"
                + "Type \"help <command>\" to get extended help on <command> (in case of ...)\n");
        }

        public void HelpNew(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"new\":\n"
                + " - new graph <filename> [<graphname>]\n"
                + "   Creates a graph from the given .gm or .grg file and optionally\n"
                + "   assigns it the given name.\n\n"
                + " - new [<var>][:<type>['('[$=<name>,][<attributes>]')']]\n"
                + "   Creates a node of the given type, name, and attributes and\n"
                + "   assigns it to the given variable.\n"
                + "   Examples:\n"
                + "     - new :Process\n"
                + "     - new proc1:Process($=\"first process\", speed=4.6, id=300)\n\n"
                + " - new <srcNode> -[<var>][:<type>['('[$=<name>,][<attributes>]')']]-> <tgtNode>\n"
                + "   Creates an edge of the given type, name, and attributes from srcNode\n"
                + "   to tgtNode and assigns it to the given variable.\n"
                + "   Examples:\n"
                + "     - new n1 --> n2\n"
                + "     - new proc1 -:next-> proc2\n"
                + "     - new proc1 -req:request(amount=5)-> res1\n");
        }

        public void HelpSelect(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"select\":\n"
                + " - select backend <dllname> [<paramlist>]\n"
                + "   Selects the backend to be used to create graphs.\n"
                + "   Defaults to the lgspBackend.\n\n"
                + " - select graph <name>\n"
                + "   Selects the given already loaded graph.\n\n"
                + " - select actions <dllname>\n"
                + "   Selects the actions assembly for the current graph.\n\n"
                + " - select parser <dllname> <startmethod>\n"
                + "   Selects the ANTLR parser assembly and the name of the start symbol method\n"
                + "   (ASTdapter framework)\n");
        }

        public void HelpDelete(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"delete\":\n"
                + " - delete node <node>\n"
                + "   Deletes the given node from the current graph.\n\n"
                + " - delete edge <edge>\n"
                + "   Deletes the given edge from the current graph.\n\n"
                + " - delete graph [<graph>]\n"
                + "   Deletes the current or the given graph.\n");
        }

        public void HelpRetype(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"retype\":\n"
                + " - retype node<type>\n"
                + "   Retypes the given node from the current graph to the given type.\n\n"
                + " - retype -edge<type>-> or -edge<type>-\n"
                + "   Retypes the given edge from the current graph to the given type.\n");
        }

        public void HelpRedirect(List<String> commands)
        {
            if (commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"redirect\":\n"
                + " - redirect <edge> (source|target) <node>\n"
                + "   Redirects the source or target of the edge to the new node given.\n"
                + " - redirect emit <filename>\n"
                + "   Redirects the GrGen emit instructions to a file\n"
                + " - redirect emit -\n"
                + "   Afterwards emit instructions write to stdout again\n");
        }

        public void HelpShow(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"show\":\n"
                + " - show (nodes|edges) [[only] <type>]\n"
                + "   Shows all nodes/edges, the nodes/edges compatible to the given type, or\n"
                + "   only nodes/edges of the exact type.\n\n"
                + " - show num (nodes|edges) [[only] <type>]\n"
                + "   Shows the number of elements as above.\n\n"
                + " - show (node|edge) types\n"
                + "   Shows the node/edge types of the current graph model.\n\n"
                + " - show (node|edge) (sub|super) types <type>\n"
                + "   Shows the sub/super types of the given type.\n\n"
                + " - show (node|edge) attributes [[only] <type>]\n"
                + "   Shows all attributes of all types, of all types compatible to the given\n"
                + "   type, or only of the given type.\n\n"
                + " - show (node|edge) <elem>\n"
                + "   Shows the attributes of the given node/edge.\n\n"
                + " - show var <var>\n"
                + "   Shows the value of the given variable.\n\n"
                + " - show <element>.<attribute>\n"
                + "   Shows the value of the given attribute of the given value.\n\n"
                + " - show graph <program> [<arguments>]\n"
                + "   Shows the current graph in VCG format with the given program.\n"
                + "   The name of the temporary VCG file will always be the first parameter.\n"
                + "   Example: show graph ycomp\n\n"
                + " - show graphs\n"
                + "   Lists the names of the currently loaded graphs.\n\n"
                + " - show actions\n"
                + "   Lists the available actions associated with the current graph.\n\n"
                + " - show profile [actionname]\n"
                + "   Lists the profile collected for actionname, or all actions.\n\n"
                + " - show backend\n"
                + "   Shows the name of the current backend and its parameters.\n");
        }

        public void HelpDebug(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            // Not mentioned: debug grs

            debugOut.WriteLine("\nList of available commands for \"debug\":\n"
                + " - debug (exec | xgrs) <xgrs>\n"
                + "   Debugs the given XGRS.\n\n"
                + " - debug (enable | disable)\n"
                + "   Enables/disables debug mode.\n\n"
                + " - debug layout\n"
                + "   Forces yComp to relayout the graph.\n\n"
                + " - debug set layout [<algo>]\n"
                + "   Selects the layout algorithm for yComp. If algorithm is not given,\n"
                + "   all available layout algorithms are listed.\n\n"
                + " - debug get layout options\n"
                + "   Lists all available layout options for the current layout algorithm.\n\n"
                + " - debug set layout option <name> <value>\n"
                + "   Sets the value of the given layout option.\n\n");
        }

        public void HelpDump(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("\nList of available commands for \"dump\":\n"
                + " - dump graph <filename>\n"
                + "   Dumps the current graph to a file in VCG format.\n\n"
                + " - dump set node [only] <type> <property> [<value>]\n"
                + "   Sets dump properties for the given or all compatible node types.\n"
                + "   If no value is given, a list of possible values is shown.\n"
                + "   Supported properties:\n"
                + "    - color\n"
                + "    - bordercolor\n"
                + "    - shape\n"
                + "    - textcolor\n"
                + "    - labels (on | off | <constanttext>)\n\n"
                + " - dump set edge [only] <type> <property> [<value>]\n"
                + "   Sets dump properties for the given or all compatible edge types.\n"
                + "   If no value is given, a list of possible values is shown.\n"
                + "   Supported properties:\n"
                + "    - color\n"
                + "    - textcolor\n"
                + "    - labels (on | off | <constanttext>)\n\n"
                + " - dump add (node | edge) [only] <type> exclude\n"
                + "   Excludes the given or compatible types from dumping.\n\n"
                + " - dump add node [only] <nodetype> group [by [hidden] <mode>\n"
                + "                [[only] <edgetype> [with [only] adjnodetype]]]\n"
                + "   Declares the given nodetype as a group node type.\n"
                + "   The mode determines by which edges the incident nodes are grouped\n"
                + "   into the group node. The available modes are:\n"
                + "    - no\n"
                + "    - incoming\n"
                + "    - outgoing\n"
                + "    - any\n"
                + "   Furthermore, the edge types and the incident node types can be restricted.\n\n"
                + " - dump add (node | edge) [only] <type> infotag <member>\n"
                + "   Adds an info tag to the given or compatible node types, which is displayed\n"
                + "   as \"<member> = <value>\" under the label of the graph element.\n\n"
                + " - dump add (node | edge) [only] <type> shortinfotag <member>\n"
                + "   Adds an info tag to the given or compatible node types, which is displayed\n"
                + "   as \"<value>\" under the label of the graph element.\n");
        }

        public void HelpCustom(List<String> commands)
        {
            if(commands.Count > 1)
            {
                switch(commands[1])
                {
                    case "graph":
                        CustomGraph(new List<String>());
                        return;

                    case "actions":
                        CustomActions(new List<String>());
                        return;

                    default:
                        debugOut.WriteLine("\nNo further help available.");
                        break;
                }
            }

            debugOut.WriteLine("\nList of available commands for \"custom\":\n"
                + " - custom graph:\n\n");
            CustomGraph(new List<String>());
            debugOut.WriteLine("\n - custom actions:\n\n");
            CustomActions(new List<String>());
            debugOut.WriteLine();
        }

        public void HelpValidate(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("List of available commands for \"validate\":\n"
                + " - validate [exitonfailure] (exec | xgrs) <xgrs>\n"
                + "   Validates the current graph according to the given XGRS (true = valid)\n"
                + "   The xgrs is applied to a clone of the original graph\n"
                + "   If exitonfailure is specified and the graph is invalid the shell is exited\n\n"
                + " - validate [exitonfailure] [strict]\n"
                + "   Validates the current graph according to the connection assertions given by\n"
                + "   the graph model. In strict mode, all graph elements have to be mentioned\n"
                + "   as part of a connection assertion. If exitonfailure is specified\n"
                + "   and the graph is invalid the shell is exited\n");
        }

        public void HelpImport(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("List of available commands for \"import\":\n"
                + " - import <filename> [<modeloverride>]\n"
                + "   Imports the graph from the file <filename>,\n"
                + "   the filename extension specifies the format to be used.\n"
                + "   Available formats are GRS with \".grs\" and GXL with \".gxl\".\n"
                + "   The GRS-file must come with the \".gm\" of the original graph,\n"
                + "   and the \".grg\" of same name if you want to apply the rules to it.\n"
                + "   The <modeloverride> may specifiy a \".gm\" file to get the model to use from,\n"
                + "   instead of using the model included in the GXL file.\n"
                + "   To apply GrGen.NET rules on an imported GXL graph,\n"
                + "   a compatible model override must be used, followed by a\n"
                + "   \"select actions <dll-name>\" of the actions dll built from the \".grg\"\n"
                + "   with the rules including the overriding model.\n");
        }

        public void HelpExport(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("List of available commands for \"export\":\n"
                + " - export <filename>\n"
                + "   Exports the current graph into the file <filename>,\n"
                + "   the filename extension specifies the format to be used.\n"
                + "   Available formats are GRS with \".grs\" or \".grsi\" (just other name)\n"
                + "   and GXL with \".gxl\".\n"
                + "   The GXL-file contains sa model of its own,\n"
                + "   the GRS-file is only complete with the \".gm\" of the original graph\n"
                + "   (and the \".grg\" if you want to use the original rules).\n");
        }

        public void HelpRecord(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("List of available commands for \"record\":\n"
                + " - record <filename> [start|stop]\n"
                + "   Starts or stops recording of graph changes to the file <filename>,\n"
                + "   if neither start nor stop are given, recording to the file is toggled.\n"
                + "   Recording starts with an export of the instance graph in GRS format,\n"
                + "   afterwards the command returns but all changes to the instance graph \n"
                + "   are recorded to the file until the recording stop command is issued.\n"
                + "   If a .gz suffix is given the recording is saved zipped.\n");
        }

        public void HelpReplay(List<String> commands)
        {
            if(commands.Count > 1)
            {
                debugOut.WriteLine("\nNo further help available.");
            }

            debugOut.WriteLine("List of available commands for \"replay\":\n"
                + " - replay <filename> [from <linetext>] [to <linetext>]\n"
                + "   Plays a recording back: the graph at the time the recording was started\n"
                + "   is recreated, then the changes which occured are carried out again,\n"
                + "   so you end up with the graph at the time the recording was stopped.\n"
                + "   Instead of replaying the entire GRS file you may restrict replaying\n"
                + "   to parts of the file by giving the line to start at (exclusive) and/or\n"
                + "   the line to stop at (exclusive). Lines are specified by their\n"
                + "   textual content which is searched in the file.\n"
                + "   If a .gz suffix is given a zipped recording is read.\n");
        }

        #endregion Help text methods

        public void SyncIO()
        {
            Infrastructure.Flush(curShellProcEnv.ProcEnv.NamedGraph);
        }

        public void ExecuteCommandLine(String cmdLine)
        {
            // treat first word as the filename and the rest as arguments

            cmdLine = cmdLine.Trim();
            int firstSpace = cmdLine.IndexOf(' ');
            String filename;
            String args;
            if(firstSpace == -1)
            {
                filename = cmdLine;
                args = "";
            }
            else
            {
                filename = cmdLine.Substring(0, firstSpace);
                args = cmdLine.Substring(firstSpace + 1, cmdLine.Length - firstSpace - 1);
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(filename, args);
                Process cmdProcess = Process.Start(startInfo);
                cmdProcess.WaitForExit();
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to execute file \"" + filename + "\": " + e.Message);
            }
        }

        public bool ChangeDirectory(String filename)
        {
            try
            {
                Directory.SetCurrentDirectory(filename);
                debugOut.WriteLine("Changed current working directory to " + filename);
            }
            catch(Exception e)
            {
                errOut.WriteLine("Error during cd to \"" + filename + "\": " + e.Message);
                return false;
            }
            return true;
        }

        public bool ListDirectory()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                DirectoryInfo[] nestedDirectories = dir.GetDirectories();
                foreach(DirectoryInfo nestedDirectory in nestedDirectories)
                {
                    Workaround.PrintHighlighted(nestedDirectory.Name, HighlightingMode.Directory);
                    debugOut.Write(" ");
                }
                debugOut.WriteLine();
                FileInfo[] filesInDirectory = dir.GetFiles();
                foreach(FileInfo file in filesInDirectory)
                {
                    if(file.Name.EndsWith(".grs"))
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.GrsFile);
                    else if(file.Name.EndsWith(".grsi"))
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.GrsiFile);
                    else if(file.Name.EndsWith(".grg"))
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.GrgFile);
                    else if(file.Name.EndsWith(".gri"))
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.GriFile);
                    else if(file.Name.EndsWith(".gm"))
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.GmFile);
                    else
                        Workaround.PrintHighlighted(file.Name, HighlightingMode.None);
                    debugOut.Write(" ");
                }
                debugOut.WriteLine();
            }
            catch(Exception e)
            {
                errOut.WriteLine("Error on ls command: " + e.Message);
                return false;
            }
            return true;
        }

        public bool PrintWorkingDirectory()
        {
            try
            {
                debugOut.WriteLine(Directory.GetCurrentDirectory());
            }
            catch(Exception e)
            {
                errOut.WriteLine("Error on pwd command: " + e.Message);
                return false;
            }
            return true;
        }

        public bool Include(GrShell grShell, String filename, String from, String to)
        {
            try
            {
                if(showIncludes)
                    debugOut.WriteLine("Including " + filename);
                TextReader reader = null;
                if (filename.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase)) {
                    FileStream filereader = new FileStream(filename, FileMode.Open,  FileAccess.Read);
                    reader = new StreamReader(new GZipStream(filereader, CompressionMode.Decompress));
                } else {
                    reader = new StreamReader(filename);
                }
                if(from != null || to != null)
                    reader = new FromToReader(reader, from, to);
                using(reader)
                {
                    SimpleCharStream charStream = new SimpleCharStream(reader);
                    GrShellTokenManager tokenSource = new GrShellTokenManager(charStream);
                    TokenSourceStack.AddFirst(tokenSource);
                    bool oldShowPrompt = grShell.ShowPrompt;
                    grShell.ShowPrompt = false;
                    try
                    {
                        grShell.ReInit(tokenSource);
                        while(!grShell.Quit && !grShell.Eof)
                        {
                            if(!grShell.ParseShellCommand())
                            {
                                errOut.WriteLine("Shell command parsing failed in include of \"" + filename + "\" (at nesting level " + TokenSourceStack.Count + ")");
                                return false;
                            }
                        }
                        grShell.Eof = false;
                    }
                    finally
                    {
                        if(showIncludes)
                            debugOut.WriteLine("Leaving " + filename);
                        TokenSourceStack.RemoveFirst();
                        grShell.ReInit(TokenSourceStack.First.Value);
                        grShell.ShowPrompt = oldShowPrompt;
                    }
                }
            }
            catch(Exception e)
            {
                errOut.WriteLine("Error during include of \"" + filename + "\": " + e.Message);
                return false;
            }
            return true;
        }

        public void Quit()
        {
            if(InDebugMode)
                SetDebugMode(false);

            debugOut.WriteLine("Bye!\n");
        }

        public void Cleanup()
        {
            foreach(ShellGraphProcessingEnvironment shellProcEnv in shellProcEnvs)
            {
                if(shellProcEnv.ProcEnv.EmitWriter != Console.Out)
                {
                    shellProcEnv.ProcEnv.EmitWriter.Close();
                    shellProcEnv.ProcEnv.EmitWriter = Console.Out;
                }
            }
            debugOut.Flush();
            errOut.Flush();
        }

        #region Model operations
        public bool SelectBackend(String assemblyName, ArrayList parameters)
        {
            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                assemblyName = assemblyName.Replace('\\', Path.DirectorySeparatorChar);

            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type backendType = null;
                foreach(Type type in assembly.GetTypes())
                {
                    if(!type.IsClass || type.IsNotPublic) continue;
                    if(type.GetInterface("IBackend") != null)
                    {
                        if(backendType != null)
                        {
                            errOut.WriteLine("The given backend contains more than one IBackend implementation!");
                            return false;
                        }
                        backendType = type;
                    }
                }
                if(backendType == null)
                {
                    errOut.WriteLine("The given backend doesn't contain an IBackend implementation!");
                    return false;
                }
                curGraphBackend = (IBackend) Activator.CreateInstance(backendType);
                backendFilename = assemblyName;
                backendParameters = (String[]) parameters.ToArray(typeof(String));
                debugOut.WriteLine("Backend selected successfully.");
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to load backend: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public bool NewGraphAddReference(String externalAssemblyReference)
        {
            if(!newGraphExternalAssembliesReferenced.Contains(externalAssemblyReference))
                newGraphExternalAssembliesReferenced.Add(externalAssemblyReference);
            return true;
        }

        public bool NewGraphSetKeepDebug(bool on)
        {
            newGraphKeepDebug = on;
            return true;
        }

        public bool NewGraphSetStatistics(String filepath)
        {
            if(!File.Exists(filepath))
            {
                errOut.WriteLine("Can't find statistics file {0}!", filepath);
                return false;
            }
            newGraphStatistics = filepath;
            return true;
        }

        public bool NewGraphSetLazyNIC(bool on)
        {
            newGraphLazyNIC = on;
            return true;
        }

        public bool NewGraphSetNoinline(bool on)
        {
            newGraphNoinline = on;
            return true;
        }

        public bool NewGraphSetProfile(bool on)
        {
            newGraphProfile = on;
            return true;
        }

        public bool GetEmitProfiling()
        {
            return newGraphProfile;
        }

        public bool NewGraph(String specFilename, String graphName, bool forceRebuild)
        {
            if(!BackendExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                specFilename = specFilename.Replace('\\', Path.DirectorySeparatorChar);

            if(specFilename.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || specFilename.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                INamedGraph graph;
                try
                {
                    graph = curGraphBackend.CreateNamedGraph(specFilename, graphName, backendParameters);
                }
                catch(Exception e)
                {
                    errOut.WriteLine("Unable to create graph with model \"{0}\":\n{1}", specFilename, e.Message);
                    return false;
                }
                try
                {
                    curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
                }
                catch(Exception ex)
                {
                    errOut.WriteLine(ex);
                    errOut.WriteLine("Unable to create new graph: {0}", ex.Message);
                    return false;
                }
                shellProcEnvs.Add(curShellProcEnv);
                debugOut.WriteLine("New graph \"{0}\" of model \"{1}\" created.", graphName, graph.Model.ModelName);
            }
            else
            {
                if(!File.Exists(specFilename))
                {
                    String ruleFilename = specFilename + ".grg";
                    if(!File.Exists(ruleFilename))
                    {
                        String gmFilename = specFilename + ".gm";
                        if(!File.Exists(gmFilename))
                        {
                            errOut.WriteLine("The specification file \"" + specFilename + "\" or \"" + ruleFilename + "\" or \"" + gmFilename + "\" does not exist!");
                            return false;
                        }
                        else specFilename = gmFilename;
                    }
                    else specFilename = ruleFilename;
                }

                if(specFilename.EndsWith(".gm", StringComparison.OrdinalIgnoreCase))
                {
                    INamedGraph graph;
                    try
                    {
                        ProcessSpecFlags flags = newGraphKeepDebug ? ProcessSpecFlags.KeepGeneratedFiles | ProcessSpecFlags.CompileWithDebug : ProcessSpecFlags.UseNoExistingFiles;
                        if(newGraphLazyNIC) flags |= ProcessSpecFlags.LazyNIC;
                        if(newGraphNoinline) flags |= ProcessSpecFlags.Noinline;
                        if(newGraphProfile) flags |= ProcessSpecFlags.Profile;
                        if(forceRebuild) flags |= ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange;
                        graph = curGraphBackend.CreateNamedFromSpec(specFilename, graphName, null,
                            flags, newGraphExternalAssembliesReferenced, 0);
                    }
                    catch(Exception e)
                    {
                        errOut.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}\nStack:\n({2})\n", specFilename, e.Message, e.StackTrace);
                        return false;
                    }
                    try
                    {
                        curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
                    }
                    catch(Exception ex)
                    {
                        errOut.WriteLine(ex);
                        errOut.WriteLine("Unable to create new graph: {0}", ex.Message);
                        return false;
                    }
                    shellProcEnvs.Add(curShellProcEnv);
                    debugOut.WriteLine("New graph \"{0}\" created from spec file \"{1}\".", graphName, specFilename);
                }
                else if(specFilename.EndsWith(".grg", StringComparison.OrdinalIgnoreCase))
                {
                    INamedGraph graph;
                    IActions actions;
                    
                    try
                    {
                        ProcessSpecFlags flags = newGraphKeepDebug ? ProcessSpecFlags.KeepGeneratedFiles | ProcessSpecFlags.CompileWithDebug : ProcessSpecFlags.UseNoExistingFiles;
                        if(newGraphLazyNIC) flags |= ProcessSpecFlags.LazyNIC;
                        if(newGraphNoinline) flags |= ProcessSpecFlags.Noinline;
                        if(newGraphProfile) flags |= ProcessSpecFlags.Profile;
                        if(forceRebuild) flags |= ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange;
                        curGraphBackend.CreateNamedFromSpec(specFilename, graphName, newGraphStatistics,
                            flags, newGraphExternalAssembliesReferenced, 0,
                            out graph, out actions);
                    }
                    catch(Exception e)
                    {
                        errOut.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}\nStack:\n({2})\n", specFilename, e.Message, e.StackTrace);
                        return false;
                    }

                    try
                    {
                        curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
                    }
                    catch(Exception ex)
                    {
                        errOut.WriteLine(ex);
                        errOut.WriteLine("Unable to create new shell processing environment: {0}", ex.Message);
                        return false;
                    }
                    curShellProcEnv.ProcEnv.Actions = actions;
                    shellProcEnvs.Add(curShellProcEnv);
                    debugOut.WriteLine("New graph \"{0}\" and actions created from spec file \"{1}\".", graphName, specFilename);
                }
                else
                {
                    errOut.WriteLine("Unknown specification file format of file: \"" + specFilename + "\"");
                    return false;
                }
            }

            if(InDebugMode) { // switch to new graph from old graph
                SetDebugMode(false);
                pendingDebugEnable = true;
            }

            if(pendingDebugEnable)
                SetDebugMode(true);

            return true;
        }

   		public bool AddNewGraph(String graphName)
        {
            if(curShellProcEnv.NameToSubgraph.ContainsKey(graphName))
            {
                errOut.WriteLine("The graph name {0} is already in use!", graphName);
                return false;
            }
            INamedGraph graph = (INamedGraph)curShellProcEnv.ProcEnv.Graph.CreateEmptyEquivalent(graphName);
            curShellProcEnv.NameToSubgraph.Add(graphName, graph);
            if(!silence)
                debugOut.WriteLine("New subgraph \"{0}\" created.", curShellProcEnv.NameToSubgraph[graphName]);
            return ChangeGraph(graphName);
        }

		public bool ChangeGraph(String graphName)
        {
            if(!curShellProcEnv.NameToSubgraph.ContainsKey(graphName))
            {
                errOut.WriteLine("No graph with name {0} known!", graphName);
                return false;
            }
            if(curShellProcEnv.ProcEnv.IsInSubgraph)
            {
                if(!silence)
                    debugOut.WriteLine("Returning from subgraph \"{0}\".", curShellProcEnv.ProcEnv.NamedGraph.Name);
                curShellProcEnv.ProcEnv.ReturnFromSubgraph();
            }
            if(curShellProcEnv.ProcEnv.IsInSubgraph)
            {
                errOut.WriteLine("GrShell is not capable of deeply nested subgraph processing, only uses main graph plus optionally one subgraph at a time.");
                return false;
            }
            if(curShellProcEnv.NameToSubgraph[graphName] == curShellProcEnv.ProcEnv.NamedGraph)
            {
                if(!silence)
                    debugOut.WriteLine("(Back) At main host graph \"{0}\".", curShellProcEnv.ProcEnv.NamedGraph.Name);
                return true;
            }

            if(!silence)
                debugOut.WriteLine("Switching to subgraph \"{0}\".", curShellProcEnv.NameToSubgraph[graphName]);
            curShellProcEnv.ProcEnv.SwitchToSubgraph(curShellProcEnv.NameToSubgraph[graphName]);
            return true;
        }

        public void SelectShellGraphProcEnv(ShellGraphProcessingEnvironment shellGraphProcEnv)
        {
            curShellProcEnv = shellGraphProcEnv ?? curShellProcEnv;
        }

        public bool SelectActions(String actionFilename)
        {
            if(!GraphExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                actionFilename = actionFilename.Replace('\\', Path.DirectorySeparatorChar);

            try
            {
                curShellProcEnv.ProcEnv.Actions = LGSPActions.LoadActions(actionFilename, (LGSPGraph)curShellProcEnv.ProcEnv.NamedGraph);
                curShellProcEnv.ActionsFilename = actionFilename;
                ((LGSPGraphProcessingEnvironment)curShellProcEnv.ProcEnv).Initialize((LGSPGraph)curShellProcEnv.ProcEnv.NamedGraph, (LGSPActions)curShellProcEnv.ProcEnv.Actions);
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to load the actions from \"{0}\":\n{1}", actionFilename, ex.Message);
                return false;
            }
            return true;
        }

        public bool SelectParser(String parserAssemblyName, String mainMethodName)
        {
            if(!GraphExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                parserAssemblyName = parserAssemblyName.Replace('\\', Path.DirectorySeparatorChar);

            try
            {
                curShellProcEnv.Parser = new ParserPackage(parserAssemblyName, mainMethodName);
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to load parser from \"" + parserAssemblyName + "\":\n" + ex.Message);
                return false;
            }
            return true;
        }
        #endregion Model operations

        #region "new" graph element commands
        public INode NewNode(ElementDef elemDef)
        {
            if(!GraphExists()) return null;

            NodeType nodeType;
            if(elemDef.TypeName != null)
            {
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(elemDef.TypeName);
                if(nodeType == null)
                {
                    errOut.WriteLine("Unknown node type: \"" + elemDef.TypeName + "\"");
                    return null;
                }
                if(nodeType.IsAbstract)
                {
                    errOut.WriteLine("Abstract node type \"" + elemDef.TypeName + "\" may not be instantiated!");
                    return null;
                }
            }
            else nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;

            if(elemDef.Attributes != null && !CheckAttributes(nodeType, elemDef.Attributes)) return null;

            INode node;
            try
            {
                node = curShellProcEnv.ProcEnv.NamedGraph.AddNode(nodeType, elemDef.ElemName);
                curShellProcEnv.ProcEnv.SetVariableValue(elemDef.VarName, node);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to create new node: {0}", e.Message);
                return null;
            }
            if(node == null)
            {
                errOut.WriteLine("Creation of new node failed.");
                return null;
            }


            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(node, elemDef.Attributes))
                {
                    errOut.WriteLine("Unexpected failure: Unable to set node attributes inspite of check?!");
                    curShellProcEnv.ProcEnv.NamedGraph.Remove(node);
                    return null;
                }
            }

            if (!silence)
            {
                debugOut.WriteLine("New node \"{0}\" of type \"{1}\" has been created.", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), node.Type.PackagePrefixedName);
            }

            return node;
        }

        public IEdge NewEdge(ElementDef elemDef, INode node1, INode node2, bool directed)
        {
            if(node1 == null || node2 == null) return null;

            EdgeType edgeType;
            if(elemDef.TypeName != null)
            {
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(elemDef.TypeName);
                if(edgeType == null)
                {
                    errOut.WriteLine("Unknown edge type: \"" + elemDef.TypeName + "\"");
                    return null;
                }
                if(edgeType.IsAbstract)
                {
                    errOut.WriteLine("Abstract edge type \"" + elemDef.TypeName + "\" may not be instantiated!");
                    return null;
                }
            }
            else
            {
                if (directed) edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType("Edge");
                else edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType("UEdge");
            }

            if(elemDef.Attributes != null && !CheckAttributes(edgeType, elemDef.Attributes)) return null;

            IEdge edge;
            try
            {
                edge = curShellProcEnv.ProcEnv.NamedGraph.AddEdge(edgeType, node1, node2, elemDef.ElemName);
                curShellProcEnv.ProcEnv.SetVariableValue(elemDef.VarName, edge);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to create new edge: {0}", e.Message);
                return null;
            }
            if(edge == null)
            {
                errOut.WriteLine("Creation of new edge failed.");
                return null;
            }

            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(edge, elemDef.Attributes))
                {
                    errOut.WriteLine("Unexpected failure: Unable to set edge attributes inspite of check?!");
                    curShellProcEnv.ProcEnv.NamedGraph.Remove(edge);
                    return null;
                }
            }

            if (!silence)
            {
                if(directed)
                    debugOut.WriteLine("New edge \"{0}\" of type \"{1}\" has been created from \"{2}\" to \"{3}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node1), curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node2));
                else
                    debugOut.WriteLine("New edge \"{0}\" of type \"{1}\" has been created between \"{2}\" and \"{3}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node1), curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node2));
            }

            return edge;
        }

        private object ParseAttributeValue(AttributeKind attrKind, String valueString, String attribute) // not set/map/enum
        {
            object value = null;
            switch(attrKind)
            {
                case AttributeKind.BooleanAttr:
                {
                    bool val;
                    if(valueString.Equals("true", StringComparison.OrdinalIgnoreCase))
                        val = true;
                    else if(valueString.Equals("false", StringComparison.OrdinalIgnoreCase))
                        val = false;
                    else
                    {
                        errOut.WriteLine("Attribute {0} must be either \"true\" or \"false\"!", attribute);
                        throw new Exception("Unknown boolean literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.ByteAttr:
                {
                    sbyte val;
                    if (valueString.StartsWith("0x"))
                    {
                        if (!SByte.TryParse(RemoveTypeSuffix(valueString.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            errOut.WriteLine("Attribute {0} must be a byte (signed)!", attribute);
                            throw new Exception("Unknown byte literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if (!SByte.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        errOut.WriteLine("Attribute {0} must be a byte (signed)!", attribute);
                        throw new Exception("Unknown byte literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.ShortAttr:
                {
                    short val;
                    if (valueString.StartsWith("0x"))
                    {
                        if (!Int16.TryParse(RemoveTypeSuffix(valueString.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            errOut.WriteLine("Attribute {0} must be a short!", attribute);
                            throw new Exception("Unknown short literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if (!Int16.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        errOut.WriteLine("Attribute {0} must be a short!", attribute);
                        throw new Exception("Unknown short literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.IntegerAttr:
                {
                    int val;
                    if (valueString.StartsWith("0x"))
                    {
                        if (!Int32.TryParse(valueString.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            errOut.WriteLine("Attribute {0} must be an integer!", attribute);
                            throw new Exception("Unknown integer literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if (!Int32.TryParse(valueString, out val))
                    {
                        errOut.WriteLine("Attribute {0} must be an integer!", attribute);
                        throw new Exception("Unknown integer literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.LongAttr:
                {
                    long val;
                    if(valueString.StartsWith("0x"))
                    {
                        if(!Int64.TryParse(RemoveTypeSuffix(valueString.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            errOut.WriteLine("Attribute {0} must be a long!", attribute);
                            throw new Exception("Unknown long literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!Int64.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        errOut.WriteLine("Attribute {0} must be a long!", attribute);
                        throw new Exception("Unknown long literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.StringAttr:
                    value = valueString;
                    break;
                case AttributeKind.FloatAttr:
                {
                    float val;
                    if(!Single.TryParse(valueString.Substring(0, valueString.Length-1), // cut f suffix
                        System.Globalization.NumberStyles.Float,
				        System.Globalization.CultureInfo.InvariantCulture, out val))
                    {
                        errOut.WriteLine("Attribute \"{0}\" must be a (single) floating point number!", attribute);
                        throw new Exception("Unknown float literal " + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.DoubleAttr:
                {
                    double val;
                    if(valueString[valueString.Length-1] == 'd') // cut d suffix if given
                        valueString = valueString.Substring(0, valueString.Length-1);
                    if(!Double.TryParse(valueString, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out val))
                    {
                        errOut.WriteLine("Attribute \"{0}\" must be a (double) floating point number!", attribute);
                        throw new Exception("Unknown double literal" + valueString);
                    }
                    value = val;
                    break;
                }
                case AttributeKind.ObjectAttr:
                {
                    if(valueString == "null")
                        value = null;
                    else
                        value = curShellProcEnv.ProcEnv.Graph.Model.Parse(
                            new StringReader(valueString), null, curShellProcEnv.ProcEnv.Graph);
                    break;
                }
                case AttributeKind.GraphAttr:
                {
                    if(valueString == "null")
                        value = null;
                    else
                    {
                        if(!curShellProcEnv.NameToSubgraph.ContainsKey(valueString))
                        {
                            errOut.WriteLine("No graph with name {0} known!", valueString);
                            throw new Exception("Unknown graph literal " + valueString);
                        }
                        else
                            value = curShellProcEnv.NameToSubgraph[valueString];
                    }
                    break;
                }
                case AttributeKind.NodeAttr:
                {
                    if(valueString[0] == '@' && valueString[1] == '(' && valueString[valueString.Length - 1] == ')')
                    {
                        if((valueString[2] == '\"' || valueString[2] == '\'') && (valueString[valueString.Length - 2] == '\"' || valueString[valueString.Length - 2] == '\''))
                            value = GetNodeByName(valueString.Substring(3, valueString.Length - 5));
                        else
                            value = GetNodeByName(valueString.Substring(2, valueString.Length - 3));
                    }
                    else if(valueString == "null")
                        value = null;
                    else
                        value = GetNodeByVar(valueString);
                    break;
                }
                case AttributeKind.EdgeAttr:
                {
                    if(valueString[0] == '@' && valueString[1] == '(' && valueString[valueString.Length - 1] == ')')
                    {
                        if((valueString[2] == '\"' || valueString[2] == '\'') && (valueString[valueString.Length - 2] == '\"' || valueString[valueString.Length - 2] == '\''))
                            value = GetEdgeByName(valueString.Substring(3, valueString.Length - 5));
                        else
                            value = GetEdgeByName(valueString.Substring(2, valueString.Length - 3));
                    }
                    else if(valueString == "null")
                        value = null;
                    else
                        value = GetEdgeByVar(valueString);
                    break;
                }
            }
            return value;
        }

        internal String RemoveTypeSuffix(String value)
        {
            if (value.EndsWith("y") || value.EndsWith("Y")
                || value.EndsWith("s") || value.EndsWith("S")
                || value.EndsWith("l") || value.EndsWith("L"))
                return value.Substring(0, value.Length - 1);
            else
                return value;
        }

        private object ParseAttributeValue(AttributeType attrType, String valueString, String attribute) // not set/map
        {
            object value = null;
            if(attrType.Kind==AttributeKind.EnumAttr)
            {
                try
                {
                    if(valueString.IndexOf("::") != -1)
                    {
                        valueString = valueString.Substring(valueString.IndexOf("::") + "::".Length);
                    }

                    int val;
                    if(Int32.TryParse(valueString, out val))
                    {
                        value = Enum.ToObject(attrType.EnumType.EnumType, val);
                    }
                    else
                    {
                        value = Enum.Parse(attrType.EnumType.EnumType, valueString);
                    }
                }
                catch(Exception)
                {
                }
                if(value == null)
                {
                    errOut.WriteLine("Attribute {0} must be one of the following values:", attribute);
                    foreach(EnumMember member in attrType.EnumType.Members)
                        errOut.WriteLine(" - {0} = {1}", member.Name, member.Value);
                    throw new Exception("Unknown enum member");
                }
            }
            else
            {
                value = ParseAttributeValue(attrType.Kind, valueString, attribute);
            }
            return value;
        }

        private bool CheckAttributes(GrGenType type, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = type.GetAttributeType(par.Key);
                object value = null;
                try
                {
                    if(attrType == null)
                    {
                        errOut.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.PackagePrefixedName, par.Key);
                        return false;
                    }
                    IDictionary setmap = null;
                    IList array = null;
                    IDeque deque = null;
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        if(par.Value!="set") {
                            errOut.WriteLine("Attribute \"{0}\" must be a set constructor!", par.Key);
                            throw new Exception("Set literal expected");
                        }
                        setmap = ContainerHelper.NewDictionary(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph),
                            typeof(de.unika.ipd.grGen.libGr.SetValueType));
                        foreach(object val in par.Values)
                        {
                            setmap[ParseAttributeValue(attrType.ValueType, (String)val, par.Key)] = null;
                        }
                        value = setmap;
                        break;
                    case AttributeKind.MapAttr:
                        if(par.Value!="map") {
                            errOut.WriteLine("Attribute \"{0}\" must be a map constructor!", par.Key);
                            throw new Exception("Map literal expected");
                        }
                        setmap = ContainerHelper.NewDictionary(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph),
                            ContainerHelper.GetTypeFromNameForContainer(par.TgtType, curShellProcEnv.ProcEnv.NamedGraph));
                        IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
                        foreach(object val in par.Values)
                        {
                            tgtValEnum.MoveNext();
                            setmap[ParseAttributeValue(attrType.KeyType, (String)val, par.Key)] =
                                ParseAttributeValue(attrType.ValueType, (String)tgtValEnum.Current, par.Key);
                        }
                        value = setmap;
                        break;
                    case AttributeKind.ArrayAttr:
                        if(par.Value!="array") {
                            errOut.WriteLine("Attribute \"{0}\" must be an array constructor!", par.Key);
                            throw new Exception("Array literal expected");
                        }
                        array = ContainerHelper.NewList(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph));
                        foreach(object val in par.Values)
                        {
                            array.Add(ParseAttributeValue(attrType.ValueType, (String)val, par.Key));
                        }
                        value = array;
                        break;
                    case AttributeKind.DequeAttr:
                        if(par.Value != "deque")
                        {
                            errOut.WriteLine("Attribute \"{0}\" must be a deque constructor!", par.Key);
                            throw new Exception("Deque literal expected");
                        }
                        deque = ContainerHelper.NewDeque(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph));
                        foreach(object val in par.Values)
                        {
                            deque.Enqueue(ParseAttributeValue(attrType.ValueType, (String)val, par.Key));
                        }
                        value = deque;
                        break;
                    default:
                        value = ParseAttributeValue(attrType, par.Value, par.Key);
                        break;
                    }
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private bool SetAttributes(IGraphElement elem, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = elem.Type.GetAttributeType(par.Key);
                object value = null;
                try
                {
                    if(attrType == null)
                    {
                        errOut.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", elem.Type.PackagePrefixedName, par.Key);
                        return false;
                    }
                    IDictionary setmap = null;
                    IList array = null;
                    IDeque deque = null;
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        if(par.Value!="set") {
                            errOut.WriteLine("Attribute \"{0}\" must be a set constructor!", par.Key);
                            throw new Exception("Set literal expected");
                        }
                        setmap = ContainerHelper.NewDictionary(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph),
                            typeof(de.unika.ipd.grGen.libGr.SetValueType));
                        foreach(object val in par.Values)
                        {
                            setmap[ParseAttributeValue(attrType.ValueType, (String)val, par.Key)] = null;
                        }
                        value = setmap;
                        break;
                    case AttributeKind.MapAttr:
                        if(par.Value!="map") {
                            errOut.WriteLine("Attribute \"{0}\" must be a map constructor!", par.Key);
                            throw new Exception("Map literal expected");
                        }
                        setmap = ContainerHelper.NewDictionary(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph),
                            ContainerHelper.GetTypeFromNameForContainer(par.TgtType, curShellProcEnv.ProcEnv.NamedGraph));
                        IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
                        foreach(object val in par.Values)
                        {
                            tgtValEnum.MoveNext();
                            setmap[ParseAttributeValue(attrType.KeyType, (String)val, par.Key)] =
                                ParseAttributeValue(attrType.ValueType, (String)tgtValEnum.Current, par.Key);
                        }
                        value = setmap;
                        break;
                    case AttributeKind.ArrayAttr:
                        if(par.Value!="array") {
                            errOut.WriteLine("Attribute \"{0}\" must be an array constructor!", par.Key);
                            throw new Exception("Array literal expected");
                        }
                        array = ContainerHelper.NewList(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph));
                        foreach(object val in par.Values)
                        {
                            array.Add(ParseAttributeValue(attrType.ValueType, (String)val, par.Key));
                        }
                        value = array;
                        break;
                    case AttributeKind.DequeAttr:
                        if(par.Value != "deque")
                        {
                            errOut.WriteLine("Attribute \"{0}\" must be a deque constructor!", par.Key);
                            throw new Exception("Deque literal expected");
                        }
                        deque = ContainerHelper.NewDeque(
                            ContainerHelper.GetTypeFromNameForContainer(par.Type, curShellProcEnv.ProcEnv.NamedGraph));
                        foreach(object val in par.Values)
                        {
                            deque.Enqueue(ParseAttributeValue(attrType.ValueType, (String)val, par.Key));
                        }
                        value = deque;
                        break;
                    default:
                        value = ParseAttributeValue(attrType, par.Value, par.Key);
                        break;
                    }
                }
                catch(Exception)
                {
                    return false;
                }

                AttributeChangeType changeType = AttributeChangeType.Assign;
                if (elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
                elem.SetAttribute(par.Key, value);
            }
            return true;
        }
        #endregion "new" graph element commands

        #region "remove" / "destroy" / "retype" commands
        public bool Remove(INode node)
        {
            if(node == null) return false;

            string name = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node); // get name before remove

            try
            {
                curShellProcEnv.ProcEnv.NamedGraph.RemoveEdges(node);
                curShellProcEnv.ProcEnv.NamedGraph.Remove(node);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to remove node: " + e.Message);
                return false;
            }

            if(!silence)
            {
                debugOut.WriteLine("Node \"{0}\" of type \"{1}\" has been deleted.", name, node.Type.PackagePrefixedName);
            }

            return true;
        }

        public bool Remove(IEdge edge)
        {
            if(edge == null) return false;

            string name = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge); // get name before remove

            curShellProcEnv.ProcEnv.NamedGraph.Remove(edge);

            if(!silence)
            {
                debugOut.WriteLine("Edge \"{0}\" of type \"{1}\" has been deleted.", name, edge.Type.PackagePrefixedName);
            }

            return true;
        }

        public INode Retype(INode node, String newType)
        {
            if(node == null || newType == null) return null;

            try
            {
                NodeType nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(newType);
                if(nodeType == null)
                {
                    errOut.WriteLine("Unknown node type: \"" + newType + "\"");
                    return null;
                }
                if(nodeType.IsAbstract)
                {
                    errOut.WriteLine("Abstract node type \"" + newType + "\" may not be instantiated!");
                    return null;
                }

                String oldType = node.Type.PackagePrefixedName;
                node = curShellProcEnv.ProcEnv.NamedGraph.Retype(node, nodeType);
                if(!silence)
                {
                    debugOut.WriteLine("Node \"{0}\" has been retyped from \"{1}\" to \"{2}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), oldType, node.Type.PackagePrefixedName);
                }
                return node;
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to retype node: " + e.Message);
                return null;
            }
        }

        public IEdge Retype(IEdge edge, String newType)
        {
            if(edge == null || newType == null) return null;
            try
            {
                EdgeType edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(newType);
                if(edgeType == null)
                {
                    errOut.WriteLine("Unknown edge type: \"" + newType + "\"");
                    return null;
                }
                if(edgeType.IsAbstract)
                {
                    errOut.WriteLine("Abstract edge type \"" + newType + "\" may not be instantiated!");
                    return null;
                }

                String oldType = edge.Type.PackagePrefixedName;
                edge = curShellProcEnv.ProcEnv.NamedGraph.Retype(edge, edgeType);
                if(!silence)
                {
                    debugOut.WriteLine("Edge \"{0}\" has been retyped from \"{1}\" to \"{2}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), oldType, edge.Type.PackagePrefixedName);
                }
                return edge;
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to retype edge: " + e.Message);
                return null;
            }
        }

        public void ClearGraph(ShellGraphProcessingEnvironment shellGraphProcEnv, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists()) return;
                shellGraphProcEnv = curShellProcEnv;
            }
            else if(shellGraphProcEnv == null) return;

            shellGraphProcEnv.ProcEnv.Graph.Clear();
        }

        public bool DestroyGraph(ShellGraphProcessingEnvironment shellGraphProcEnv, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists()) return false;
                shellGraphProcEnv = curShellProcEnv;
            }
            else if(shellGraphProcEnv == null) return false;

            if(InDebugMode && debugger.ShellProcEnv == shellGraphProcEnv) SetDebugMode(false);

            if(shellGraphProcEnv == curShellProcEnv)
                curShellProcEnv = null;
            shellProcEnvs.Remove(shellGraphProcEnv);

            return true;
        }
        #endregion "remove" / "destroy" / "retype" commands

        #region "show" commands
        private bool ShowElements<T>(IEnumerable<T> elements) where T : IGraphElement
        {
            if(!elements.GetEnumerator().MoveNext()) return false;

            debugOut.WriteLine("{0,-20} {1}", "name", "type");
            foreach(IGraphElement elem in elements)
            {
                debugOut.WriteLine("{0,-20} {1}", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem), elem.Type.PackagePrefixedName);
            }
            return true;
        }

        public void ShowNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists()) return;
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;
            }

            IEnumerable<INode> nodes = only ? curShellProcEnv.ProcEnv.NamedGraph.GetExactNodes(nodeType)
                : curShellProcEnv.ProcEnv.NamedGraph.GetCompatibleNodes(nodeType);
            if(!ShowElements(nodes))
                errOut.WriteLine("There are no nodes " + (only ? "compatible to" : "of") + " type \"" + nodeType.PackagePrefixedName + "\"!");
        }

        public void ShowEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists()) return;
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.RootType;
            }

            IEnumerable<IEdge> edges = only ? curShellProcEnv.ProcEnv.NamedGraph.GetExactEdges(edgeType)
                : curShellProcEnv.ProcEnv.NamedGraph.GetCompatibleEdges(edgeType);
            if(!ShowElements(edges))
                errOut.WriteLine("There are no edges of " + (only ? "compatible to" : "of") + " type \"" + edgeType.PackagePrefixedName + "\"!");
        }

        public void ShowNumNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists()) return;
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;
            }
            if(only)
                debugOut.WriteLine("Number of nodes of the type \"" + nodeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumExactNodes(nodeType));
            else
                debugOut.WriteLine("Number of nodes compatible to type \"" + nodeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumCompatibleNodes(nodeType));
        }

        public void ShowNumEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists()) return;
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.RootType;
            }
            if(only)
                debugOut.WriteLine("Number of edges of the type \"" + edgeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumExactEdges(edgeType));
            else
                debugOut.WriteLine("Number of edges compatible to type \"" + edgeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumCompatibleEdges(edgeType));
        }

        public void ShowNodeTypes()
        {
            if(!GraphExists()) return;

            if(curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.Types.Length == 0)
            {
                errOut.WriteLine("This model has no node types!");
            }
            else
            {
                debugOut.WriteLine("Node types:");
                foreach(NodeType type in curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.Types)
                    debugOut.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
            }
        }

        public void ShowEdgeTypes()
        {
            if(!GraphExists()) return;

            if(curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.Types.Length == 0)
            {
                errOut.WriteLine("This model has no edge types!");
            }
            else
            {
                debugOut.WriteLine("Edge types:");
                foreach(EdgeType type in curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.Types)
                    debugOut.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
            }
        }

        public void ShowSuperTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                errOut.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.PackagePrefixedName + "\" has no super types!");
            }
            else
            {
                debugOut.WriteLine("Super types of " + (isNode ? "node" : "edge") + " type \"" + elemType.PackagePrefixedName + "\":");
                foreach(GrGenType type in elemType.SuperTypes)
                    debugOut.WriteLine(" - \"" + type.PackagePrefixedName + "\"");
            }
        }

        public void ShowSubTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                errOut.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.PackagePrefixedName + "\" has no super types!");
            }
            else
            {
                debugOut.WriteLine("Sub types of " + (isNode ? "node" : "edge") + " type \"{0}\":", elemType.PackagePrefixedName);
                foreach(GrGenType type in elemType.SubTypes)
                    debugOut.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
            }
        }

        internal class ShowGraphParam
        {
            public readonly String ProgramName;
            public readonly String Arguments;
            public readonly String GraphFilename;

            public ShowGraphParam(String programName, String arguments, String graphFilename)
            {
                ProgramName = programName;
                Arguments = arguments;
                GraphFilename = graphFilename;
            }
        }

        /// <summary>
        /// Executes the specified viewer and deletes the dump file after the viewer has exited
        /// </summary>
        /// <param name="obj">A ShowGraphParam object</param>
        private void ShowGraphThread(object obj)
        {
            ShowGraphParam param = (ShowGraphParam) obj;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(param.ProgramName,
                    (param.Arguments == null) ? param.GraphFilename : (param.Arguments + " " + param.GraphFilename));
                Process viewer = Process.Start(startInfo);
                viewer.WaitForExit();
            }
            catch(Exception e)
            {
                errOut.WriteLine(e.Message);
            }
            finally
            {
                File.Delete(param.GraphFilename);
            }
        }

        public string ShowGraphWith(String programName, String arguments)
        {
            if(!GraphExists()) return "";
            if(nonDebugNonGuiExitOnError) return "";

            String filename;
            int id = 0;
            do
            {
                filename = "tmpgraph" + id + ".vcg";
                id++;
            }
            while(File.Exists(filename));

            VCGDumper dump = new VCGDumper(filename, curShellProcEnv.VcgFlags, debugLayout);
            GraphDumper.Dump(curShellProcEnv.ProcEnv.NamedGraph, dump, curShellProcEnv.DumpInfo);
            dump.FinishDump();

            Thread t = new Thread(new ParameterizedThreadStart(ShowGraphThread));
            t.Start(new ShowGraphParam(programName, arguments, filename));

            return filename;
        }

        public void ShowGraphs()
        {
            if(shellProcEnvs.Count == 0)
            {
                errOut.WriteLine("No graphs available.");
                return;
            }
            debugOut.WriteLine("Graphs:");
            for(int i = 0; i < shellProcEnvs.Count; i++)
                debugOut.WriteLine(" - \"" + shellProcEnvs[i].ProcEnv.Graph.Name + "\" (" + i + ")");
        }

        public void ShowActions()
        {
            if(!ActionsExists()) return;

            if(!curShellProcEnv.ProcEnv.Actions.Actions.GetEnumerator().MoveNext())
            {
                errOut.WriteLine("No actions available.");
                return;
            }

            debugOut.WriteLine("Actions:");
            foreach(IAction action in curShellProcEnv.ProcEnv.Actions.Actions)
            {
                debugOut.Write(" - " + action.Name);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    debugOut.Write("(");
                    bool isFirst = true;
                    foreach(GrGenType inType in action.RulePattern.Inputs)
                    {
                        if(!isFirst) debugOut.Write(", ");
                        else isFirst = false;
                        debugOut.Write(inType.PackagePrefixedName);
                    }
                    debugOut.Write(")");
                }

                if(action.RulePattern.Outputs.Length != 0)
                {
                    debugOut.Write(" : (");
                    bool isFirst = true;
                    foreach(GrGenType outType in action.RulePattern.Outputs)
                    {
                        if(!isFirst) debugOut.Write(", ");
                        else isFirst = false;
                        debugOut.Write(outType.PackagePrefixedName);
                    }
                    debugOut.Write(")");
                }
                debugOut.WriteLine();
            }
        }

        public void ShowProfile(string action)
        {
            if(!ActionsExists()) return;

            if(action != null)
            {
                if(curShellProcEnv.ProcEnv.PerformanceInfo.ActionProfiles.ContainsKey(action))
                    ShowProfile(action, curShellProcEnv.ProcEnv.PerformanceInfo.ActionProfiles[action]);
                else
                    errOut.WriteLine("No profile available.");
            }
            else
            {
                foreach(KeyValuePair<string, ActionProfile> profile in curShellProcEnv.ProcEnv.PerformanceInfo.ActionProfiles)
                {
                    ShowProfile(profile.Key, profile.Value);
                }
            }
        }

        private void ShowProfile(string name, ActionProfile profile)
        {
            debugOut.WriteLine("profile for action " + name + ":");

            debugOut.Write("  calls total: ");
            debugOut.WriteLine(profile.callsTotal);
            debugOut.Write("  search steps total: ");
            debugOut.WriteLine(profile.searchStepsTotal);
            debugOut.Write("  loop steps total: ");
            debugOut.WriteLine(profile.loopStepsTotal);

            for(int i = 0; i < profile.averagesPerThread.Length; ++i)
            {
                ProfileAverages profileAverages = profile.averagesPerThread[i];

                if(profile.averagesPerThread.Length > 1)
                {
                    debugOut.WriteLine("for thread " + i + ":");
                    debugOut.Write("  search steps total: ");
                    debugOut.WriteLine(profileAverages.searchStepsTotal);
                    debugOut.Write("  loop steps total: ");
                    debugOut.WriteLine(profileAverages.loopStepsTotal);
                }

                double searchStepsSingle = profileAverages.searchStepsSingle.Get();
                debugOut.Write("  search steps until one match: ");
                debugOut.WriteLine(searchStepsSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double loopStepsSingle = profileAverages.loopStepsSingle.Get();
                debugOut.Write("  loop steps until one match: ");
                debugOut.WriteLine(loopStepsSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double searchStepsPerLoopStepSingle = profileAverages.searchStepsPerLoopStepSingle.Get();
                debugOut.Write("  search steps per loop step (until one match): ");
                debugOut.WriteLine(searchStepsPerLoopStepSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));

                double searchStepsMultiple = profileAverages.searchStepsMultiple.Get();
                debugOut.Write("  search steps until more than one match: ");
                debugOut.WriteLine(searchStepsMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double loopStepsMultiple = profileAverages.loopStepsMultiple.Get();
                debugOut.Write("  loop steps until more than one match: ");
                debugOut.WriteLine(loopStepsMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double searchStepsPerLoopStepMultiple = profileAverages.searchStepsPerLoopStepMultiple.Get();
                debugOut.Write("  search steps per loop step (until more than one match): ");
                debugOut.WriteLine(searchStepsPerLoopStepMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));

                if(profile.averagesPerThread.Length == 1)
                {
                    double regularWorkAmountDistributionFactor = (1 + loopStepsSingle) * (1 + searchStepsPerLoopStepSingle) / (1 + searchStepsSingle) + (1 + loopStepsMultiple) * (1 + searchStepsPerLoopStepMultiple) / (1 + searchStepsMultiple);
                    double workAmountFactor = Math.Log(1 + loopStepsSingle, 2) * Math.Log(1 + loopStepsSingle, 2) * Math.Log(1 + searchStepsSingle, 2) / 10 + Math.Log(1 + loopStepsMultiple, 2) * Math.Log(1 + loopStepsMultiple, 2) * Math.Log(1 + searchStepsMultiple, 2) / 10;
                    debugOut.Write("  parallelization potential: ");
                    debugOut.WriteLine((regularWorkAmountDistributionFactor * workAmountFactor).ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                }

                debugOut.WriteLine();
            }
        }

        public void ShowBackend()
        {
            if(!BackendExists()) return;

            debugOut.WriteLine("Backend name: {0}", curGraphBackend.Name);
            if(!curGraphBackend.ArgumentNames.GetEnumerator().MoveNext())
            {
                errOut.WriteLine("This backend has no arguments.");
            }

            debugOut.WriteLine("Arguments:");
            foreach(String str in curGraphBackend.ArgumentNames)
                debugOut.WriteLine(" - {0}", str);
        }

        /// <summary>
        /// Displays the attribute types and names for the given attrTypes.
        /// If onlyType is not null, it shows only the attributes of exactly the type.
        /// </summary>
        private void ShowAvailableAttributes(IEnumerable<AttributeType> attrTypes, GrGenType onlyType)
        {
            bool first = true;
            foreach(AttributeType attrType in attrTypes)
            {
                if(onlyType != null && attrType.OwnerType != onlyType) continue;

                if(first)
                {
                    debugOut.WriteLine(" - {0,-24} type::attribute", "kind");
                    first = false;
                }

                String kind;
                switch(attrType.Kind)
                {
                    case AttributeKind.ByteAttr: kind = "byte"; break;
                    case AttributeKind.ShortAttr: kind = "short"; break;
                    case AttributeKind.IntegerAttr: kind = "int"; break;
                    case AttributeKind.LongAttr: kind = "long"; break;
                    case AttributeKind.BooleanAttr: kind = "boolean"; break;
                    case AttributeKind.StringAttr: kind = "string"; break;
                    case AttributeKind.EnumAttr: kind = attrType.EnumType.PackagePrefixedName; break;
                    case AttributeKind.FloatAttr: kind = "float"; break;
                    case AttributeKind.DoubleAttr: kind = "double"; break;
                    case AttributeKind.ObjectAttr: kind = "object"; break;
                    case AttributeKind.GraphAttr: kind = "graph"; break;
                    default: kind = "<INVALID>"; break;
                }
                debugOut.WriteLine(" - {0,-24} {1}::{2}", kind, attrType.OwnerType.PackagePrefixedName, attrType.Name);
            }
            if(first)
                errOut.WriteLine(" - No attribute types found.");
        }

        /// <summary>
        /// Displays the attributes from the given type or all types, if typeName is null.
        /// If showAll is false, inherited attributes are not shown (only applies to a given type)
        /// </summary>
        public void ShowAvailableNodeAttributes(bool showOnly, NodeType nodeType)
        {
            if(nodeType == null)
            {
                debugOut.WriteLine("The available attributes for nodes:");
                ShowAvailableAttributes(curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.AttributeTypes, null);
            }
            else
            {
                debugOut.WriteLine("The available attributes for {0} \"{1}\":",
                    (showOnly ? "node type only" : "node type"), nodeType.PackagePrefixedName);
                ShowAvailableAttributes(nodeType.AttributeTypes, showOnly ? nodeType : null);
            }
        }

        /// <summary>
        /// Displays the attributes from the given type or all types, if typeName is null.
        /// If showAll is false, inherited attributes are not shown (only applies to a given type)
        /// </summary>
        public void ShowAvailableEdgeAttributes(bool showOnly, EdgeType edgeType)
        {
            if(edgeType == null)
            {
                debugOut.WriteLine("The available attributes for edges:");
                ShowAvailableAttributes(curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.AttributeTypes, null);
            }
            else
            {
                debugOut.WriteLine("The available attributes for {0} \"{1}\":",
                    (showOnly ? "edge type only" : "edge type"), edgeType.PackagePrefixedName);
                ShowAvailableAttributes(edgeType.AttributeTypes, showOnly ? edgeType : null);
            }
        }

        public AttributeType GetElementAttributeType(IGraphElement elem, String attributeName)
        {
            AttributeType attrType = elem.Type.GetAttributeType(attributeName);
            if(attrType == null)
            {
                debugOut.WriteLine(((elem is INode) ? "Node" : "Edge") + " \"" + curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem)
                    + "\" does not have an attribute \"" + attributeName + "\"!");
                return attrType;
            }
            return attrType;
        }

        public void ShowElementAttributes(IGraphElement elem)
        {
            if(elem == null) return;
            if(elem.Type.NumAttributes == 0)
            {
                errOut.WriteLine("{0} \"{1}\" of type \"{2}\" does not have any attributes!", (elem is INode) ? "Node" : "Edge",
                    curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem), elem.Type.PackagePrefixedName);
                return;
            }
            debugOut.WriteLine("All attributes for {0} \"{1}\" of type \"{2}\":", (elem is INode) ? "node" : "edge",
                curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem), elem.Type.PackagePrefixedName);
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
                debugOut.WriteLine(" - {0}::{1} = {2}", attrType.OwnerType.PackagePrefixedName,
                    attrType.Name, EmitHelper.ToStringAutomatic(elem.GetAttribute(attrType.Name), curShellProcEnv.ProcEnv.NamedGraph));
        }

        public void ShowElementAttribute(IGraphElement elem, String attributeName)
        {
            if(elem == null) return;

            AttributeType attrType = GetElementAttributeType(elem, attributeName);
            if(attrType == null) return;

            debugOut.Write("The value of attribute \"" + attributeName + "\" is: \"");
            debugOut.Write(EmitHelper.ToStringAutomatic(elem.GetAttribute(attributeName), curShellProcEnv.ProcEnv.NamedGraph));
            debugOut.WriteLine("\".");
        }

        public void ShowVar(String name)
        {
            object val = GetVarValue(name);
            if (val != null)
            {
                string type;
                string content;
                if(val.GetType().Name=="Dictionary`2")
                {
                    EmitHelper.ToString((IDictionary)val, out type, out content,
                        null, curShellProcEnv!=null ? curShellProcEnv.ProcEnv.NamedGraph : null);
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val.GetType().Name == "List`1")
                {
                    EmitHelper.ToString((IList)val, out type, out content,
                        null, curShellProcEnv != null ? curShellProcEnv.ProcEnv.NamedGraph : null);
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val.GetType().Name == "Deque`1")
                {
                    EmitHelper.ToString((IDeque)val, out type, out content,
                        null, curShellProcEnv != null ? curShellProcEnv.ProcEnv.NamedGraph : null);
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val is LGSPNode && GraphExists())
                {
                    LGSPNode node = (LGSPNode)val;
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + node.Type.PackagePrefixedName + " is: \"" + curShellProcEnv.ProcEnv.NamedGraph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                else if(val is LGSPEdge && GraphExists())
                {
                    LGSPEdge edge = (LGSPEdge)val;
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + edge.Type.PackagePrefixedName + " is: \"" + curShellProcEnv.ProcEnv.NamedGraph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                EmitHelper.ToString(val, out type, out content,
                    null, curShellProcEnv!=null ? curShellProcEnv.ProcEnv.NamedGraph : null);
                debugOut.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                return;
            }
        }

        #endregion "show" commands

        public object GetElementAttribute(IGraphElement elem, String attributeName)
        {
            if(elem == null) return null;

            AttributeType attrType = GetElementAttributeType(elem, attributeName);
            if(attrType == null) return null;

            return elem.GetAttribute(attributeName);
        }

        public void SetElementAttribute(IGraphElement elem, Param param)
        {
            if(elem == null) return;
            ArrayList attributes = new ArrayList();
            attributes.Add(param);
            if(!CheckAttributes(elem.Type, attributes)) return;
            SetAttributes(elem, attributes);
        }

        public void SetElementAttributeIndexed(IGraphElement elem, String attrName, String val, object index)
        {
            if(elem == null) return;

            GrGenType type = elem.Type;
            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                errOut.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.PackagePrefixedName, attrName);
                return;
            }
            if(attrType.Kind!=AttributeKind.ArrayAttr && attrType.Kind!=AttributeKind.DequeAttr && attrType.Kind!=AttributeKind.MapAttr)
            {
                errOut.WriteLine("Attribute for indexed assignment must be of array or deque or map type!");
                return;
            }

            object value = null;
            try
            {
                value = ParseAttributeValue(attrType.ValueType, val, attrName);
            }
            catch(Exception)
            {
                return;
            }
            object attr = elem.GetAttribute(attrName);
            if(attr==null)
            {
                errOut.WriteLine("Can't retrieve attribute "+attrName+" !");
                return;
            }
            if(!(attr is IList) && !(attr is IDictionary) && !(attr is IDeque))
            {
                errOut.WriteLine("Attribute " + attrName + " is not of array/deque/map type!");
                return;
            }

            AttributeChangeType changeType = AttributeChangeType.AssignElement;
            if(elem is INode)
                curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, index);
            else
                curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, index);
            if(attr is IList)
            {
                IList array = (IList)attr;
                array[(int)index] = value;
            }
            else if(attr is IDeque)
            {
                IDeque deque = (IDeque)attr;
                deque[(int)index] = value;
            }
            else
            {
                IDictionary setmap = (IDictionary)attr;
                setmap[index] = value;
            }
        }

        public object GetVarValue(String varName)
        {
            if(!GraphExists()) return null;
            object val = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(val == null)
            {
                errOut.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            return val;
        }

        public void SetVariable(String varName, object elem)
        {
            if(!GraphExists()) return;
            curShellProcEnv.ProcEnv.SetVariableValue(varName, elem);
        }

        public void SetVariableIndexed(String varName, object value, object index)
        {
            if(!GraphExists()) return;

            object var = GetVarValue(varName);
            if(var == null)
                return;

            if(var is IList)
            {
                IList array = (IList)var;
                array[(int)index] = value;
            }
            else if(var is IDeque)
            {
                IDeque deque = (IDeque)var;
                deque[(int)index] = value;
            }
            else
            {
                IDictionary setmap = (IDictionary)var;
                setmap[index] = value;
            }
        }

        public void SetRandomSeed(int seed)
        {
            Sequence.randomGenerator = new Random(seed);
        }

        public bool RedirectEmit(String filename)
        {
            if(!GraphExists()) return false;

            if(curShellProcEnv.ProcEnv.EmitWriter != Console.Out)
                curShellProcEnv.ProcEnv.EmitWriter.Close();
            if(filename == "-")
                curShellProcEnv.ProcEnv.EmitWriter = Console.Out;
            else
            {
                try
                {
                    curShellProcEnv.ProcEnv.EmitWriter = new StreamWriter(filename);
                }
                catch(Exception ex)
                {
                    errOut.WriteLine("Unable to redirect emit to file \"" + filename + "\":\n" + ex.Message);
                    curShellProcEnv.ProcEnv.EmitWriter = Console.Out;
                    return false;
                }
            }
            return true;
        }

        public bool Redirect(IEdge edge, String direction, INode node)
        {
            if(!GraphExists()) return false;
            if(edge == null)
            {
                errOut.WriteLine("No edge given to redirect command");
                return false;
            }
            if(direction == null)
            {
                errOut.WriteLine("No direction given to redirect command");
                return false;
            }
            if(node == null)
            {
                errOut.WriteLine("No node given to redirect command");
                return false;
            }

            try
            {
                bool redirectSource = false;
                if(direction.ToLower() == "source".ToLower()) {
                    redirectSource = true;
                } else if (direction.ToLower() == "target".ToLower()) {
                    redirectSource = false;
                } else {
                    errOut.WriteLine("direction must be either \"source\" or \"target\"");
                    return false;
                }

                String oldNodeName; 
                if (redirectSource) {
                    oldNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Source);
                    curShellProcEnv.ProcEnv.NamedGraph.RedirectSource(edge, node, oldNodeName);
                } else {
                    oldNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Target);
                    curShellProcEnv.ProcEnv.NamedGraph.RedirectTarget(edge, node, oldNodeName);
                }

                if(!silence)
                {
                    String edgeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge);
                    String directionName = redirectSource ? "source" : "target";
                    String newNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node);
                    debugOut.WriteLine("Edge \"{0}\" \"{1}\" has been redirected from \"{2}\" to \"{3}\".", edgeName, directionName, oldNodeName, newNodeName);
                }
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to redirect edge: " + e.Message);
                return false;
            }
            return true;
        }

        public bool ParseFile(String filename)
        {
            if(!GraphExists()) return false;
            if(curShellProcEnv.Parser == null)
            {
                errOut.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
                return false;
            }
            try
            {
                using(FileStream file = new FileStream(filename, FileMode.Open))
                {
                    ASTdapter.ASTdapter astDapter = new ASTdapter.ASTdapter(curShellProcEnv.Parser);
                    astDapter.Load(file, curShellProcEnv.ProcEnv.NamedGraph);
                }
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to parse file \"" + filename + "\":\n" + ex.Message);
                return false;
            }
            return true;
        }

        public bool ParseString(String str)
        {
            if(!GraphExists()) return false;
            if(curShellProcEnv.Parser == null)
            {
                errOut.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
                return false;
            }
            try
            {
                ASTdapter.ASTdapter astDapter = new ASTdapter.ASTdapter(curShellProcEnv.Parser);
                astDapter.Load(str, curShellProcEnv.ProcEnv.NamedGraph);
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to parse string \"" + str + "\":\n" + ex.Message);
                return false;
            }
            return true;
        }

#if DEBUGACTIONS
        private void ShowSequenceDetails(Sequence seq, PerformanceInfo perfInfo)
        {
            switch(seq.OperandClass)
            {
                case Sequence.OperandType.Concat:
                    ShowSequenceDetails(seq.LeftOperand, perfInfo);
                    ShowSequenceDetails(seq.RightOperand, perfInfo);
                    break;
                case Sequence.OperandType.Star:
                case Sequence.OperandType.Max:
                    ShowSequenceDetails(seq.LeftOperand, perfInfo);
                    break;
                case Sequence.OperandType.Rule:
                case Sequence.OperandType.RuleAll:
                    debugOut.WriteLine(" - {0,-18}: Matches = {1,6}  Match = {2,6} ms  Rewrite = {3,6} ms",
                        ((IAction) seq.Value).Name, seq.GetTotalApplied(),
                        perfInfo.TimeDiffToMS(seq.GetTotalMatchTime()), perfInfo.TimeDiffToMS(seq.GetTotalRewriteTime()));
                    break;
            }
        }
#endif

        bool ContainsSpecial(Sequence seq)
        {
            if((seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall) 
                && ((SequenceRuleCall)seq).Special)
                return true;

            foreach(Sequence child in seq.Children)
                if(ContainsSpecial(child)) return true;

            return false;
        }

        public void DefineRewriteSequence(ISequenceDefinition seqDef)
        {
            bool overwritten = ((BaseActions)CurrentActions).RegisterGraphRewriteSequenceDefinition((SequenceDefinition)seqDef);
            if(overwritten)
                debugOut.WriteLine("Replaced old sequence definition by new one for " + seqDef.Name);
            else
                debugOut.WriteLine("Registered sequence definition for " + seqDef.Name);
        }

        Sequence curGRS;
        SequenceRuleCall curRule;

        public void ApplyRewriteSequence(Sequence seq, bool debug)
        {
            bool installedDumpHandlers = false;

            if(!ActionsExists()) return;

            if(debug || CheckDebuggerAlive())
            {
                debugger.NotifyOnConnectionLost = true;
                debugger.InitNewRewriteSequence(seq, debug);
            }

            if(!InDebugMode && ContainsSpecial(seq))
            {
                curShellProcEnv.ProcEnv.OnEntereringSequence += DumpOnEntereringSequence;
                curShellProcEnv.ProcEnv.OnExitingSequence += DumpOnExitingSequence;
                installedDumpHandlers = true;
            }
            else curShellProcEnv.ProcEnv.OnEntereringSequence += NormalEnteringSequenceHandler;

            curGRS = seq;
            curRule = null;

            debugOut.WriteLine("Executing Graph Rewrite Sequence (CTRL+C for abort) ...");
            cancelSequence = false;
            workaround.PreventComputerGoingIntoSleepMode(true);
            curShellProcEnv.ProcEnv.PerformanceInfo.Reset();
            StatisticsSource statisticsSource = new StatisticsSource(curShellProcEnv.ProcEnv.NamedGraph, curShellProcEnv.ProcEnv);
            Timer timer = null;
            if(!debug && !silenceExec) timer = new Timer(new TimerCallback(PrintStatistics), statisticsSource, 1000, 1000);

            try
            {
                bool result = curShellProcEnv.ProcEnv.ApplyGraphRewriteSequence(seq);
                if(timer != null) timer.Dispose();

                seq.ResetExecutionState();
                debugOut.WriteLine("Executing Graph Rewrite Sequence done after {0} ms with result {1}:",
                    (curShellProcEnv.ProcEnv.PerformanceInfo.TimeNeeded * 1000).ToString("F1", System.Globalization.CultureInfo.InvariantCulture), result);
                if(newGraphProfile)
                    debugOut.WriteLine(" - {0} search steps executed", curShellProcEnv.ProcEnv.PerformanceInfo.SearchSteps);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
                debugOut.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
                debugOut.WriteLine(" - {0} rewrites performed in {1} ms", perfInfo.RewritesPerformed, perfInfo.TotalRewriteTimeMS);
#if DEBUGACTIONS
                debugOut.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                debugOut.WriteLine(" - {0} matches found", curShellProcEnv.ProcEnv.PerformanceInfo.MatchesFound);
                debugOut.WriteLine(" - {0} rewrites performed", curShellProcEnv.ProcEnv.PerformanceInfo.RewritesPerformed);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(timer != null) timer.Dispose();
                if(curRule == null)
                    errOut.WriteLine("Rewrite sequence aborted!");
                else
                {
                    errOut.WriteLine("Rewrite sequence aborted after position:");
                    Debugger.PrintSequence(curGRS, curRule, Workaround);
                    errOut.WriteLine();
                }
            }
            workaround.PreventComputerGoingIntoSleepMode(false);
            curRule = null;
            curGRS = null;

            if(InDebugMode)
            {
                debugger.NotifyOnConnectionLost = false;
                debugger.FinishRewriteSequence();
            }

            StreamWriter emitWriter = curShellProcEnv.ProcEnv.EmitWriter as StreamWriter;
            if(emitWriter != null)
                emitWriter.Flush();

            if(installedDumpHandlers)
            {
                curShellProcEnv.ProcEnv.OnEntereringSequence -= DumpOnEntereringSequence;
                curShellProcEnv.ProcEnv.OnExitingSequence -= DumpOnExitingSequence;
            }
            else curShellProcEnv.ProcEnv.OnEntereringSequence -= NormalEnteringSequenceHandler;
        }

        // called from a timer while a sequence is executed outside of the debugger 
        // (this may still mean the debugger is open and attached ("debug enable"), but just not under user control)
        static void PrintStatistics(Object state)
        {
            StatisticsSource statisticsSource = (StatisticsSource)state;
            if(!statisticsSource.ActionEnv.HighlightingUnderway)
                Console.WriteLine(" ... {0} matches, {1} rewrites, {2} graph changes until now ...", statisticsSource.MatchesFound, statisticsSource.RewritesPerformed, statisticsSource.GraphChanges);
        }

        public void Cancel()
        {
            if(InDebugMode)
                debugger.AbortRewriteSequence();
            throw new OperationCanceledException();                 // abort rewrite sequence
        }

        void NormalEnteringSequenceHandler(Sequence seq)
        {
            if(cancelSequence)
                Cancel();

            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
                curRule = (SequenceRuleCall) seq;
        }

        void DumpOnEntereringSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
            {
                curRule = (SequenceRuleCall) seq;
                if(curRule.Special)
                    curShellProcEnv.ProcEnv.OnFinishing += DumpOnFinishing;
            }
        }

        void DumpOnExitingSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.RuleCall || seq.SequenceType == SequenceType.RuleAllCall || seq.SequenceType == SequenceType.RuleCountAllCall)
            {
                SequenceRuleCall ruleSeq = (SequenceRuleCall) seq;
                if(ruleSeq != null && ruleSeq.Special)
                    curShellProcEnv.ProcEnv.OnFinishing -= DumpOnFinishing;
            }

            if(cancelSequence)
                Cancel();
        }

        void DumpOnFinishing(IMatches matches, bool special)
        {
            int i = 1;
            debugOut.WriteLine("Matched " + matches.Producer.Name + " rule:");
            foreach(IMatch match in matches)
            {
                debugOut.WriteLine(" - " + i + ". match:");
                DumpMatch(match, "   ");
                ++i;
            }
        }

        void DumpMatch(IMatch match, String indentation)
        {
            int i = 0;
            foreach (INode node in match.Nodes)
                debugOut.WriteLine(indentation + match.Pattern.Nodes[i++].UnprefixedName + ": " + curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node));
            int j = 0;
            foreach (IEdge edge in match.Edges)
                debugOut.WriteLine(indentation + match.Pattern.Edges[j++].UnprefixedName + ": " + curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge));

            foreach(IMatch nestedMatch in match.EmbeddedGraphs)
            {
                debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatch nestedMatch in match.Alternatives)
            {
                debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
            foreach (IMatches nestedMatches in match.Iterateds)
            {
                foreach (IMatch nestedMatch in nestedMatches)
                {
                    debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                    DumpMatch(nestedMatch, indentation + "  ");
                }
            }
            foreach (IMatch nestedMatch in match.Independents)
            {
                debugOut.WriteLine(indentation + nestedMatch.Pattern.Name + ":");
                DumpMatch(nestedMatch, indentation + "  ");
            }
        }

        void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if(curGRS == null || cancelSequence) return;
            if(curRule == null) errOut.WriteLine("Cancelling...");
            else errOut.WriteLine("Cancelling: Waiting for \"" + curRule.ParamBindings.Action.Name + "\" to finish...");
            e.Cancel = true;        // we handled the cancel event
            cancelSequence = true;
        }

        /// <summary>
        /// Enables or disables debug mode.
        /// </summary>
        /// <param name="enable">Whether to enable or not.</param>
        /// <returns>True, if the mode has the desired value at the end of the function.</returns>
        public bool SetDebugMode(bool enable)
        {
            if(nonDebugNonGuiExitOnError) {
                return true;
            }

            if(enable)
            {
                if(CurrentShellProcEnv == null)
                {
                    errOut.WriteLine("Debug mode will be enabled as soon as a graph has been created!");
                    pendingDebugEnable = true;
                    return false;
                }
                if(InDebugMode && CheckDebuggerAlive())
                {
                    errOut.WriteLine("You are already in debug mode!");
                    return true;
                }

                Dictionary<String, String> optMap;
                debugLayoutOptions.TryGetValue(debugLayout, out optMap);
                try
                {
                    debugger = new Debugger(this, debugLayout, optMap);
                    curShellProcEnv.ProcEnv.UserProxy = debugger;
                }
                catch(Exception ex)
                {
                    if(ex.Message != "Connection to yComp lost")
                        errOut.WriteLine(ex.Message);
                    return false;
                }
                pendingDebugEnable = false;
            }
            else
            {
                if(CurrentShellProcEnv == null && pendingDebugEnable)
                {
                    debugOut.WriteLine("Debug mode will not be enabled anymore when a graph has been created.");
                    pendingDebugEnable = false;
                    return true;
                }

                if(!InDebugMode)
                {
                    errOut.WriteLine("You are not in debug mode!");
                    return true;
                }

                curShellProcEnv.ProcEnv.UserProxy = null;
                debugger.Close();
                debugger = null;
            }
            return true;
        }

        public bool CheckDebuggerAlive()
        {
            if(!InDebugMode) return false;
            if(!debugger.YCompClient.Sync())
            {
                debugger = null;
                return false;
            }
            return true;
        }

        public void DebugRewriteSequence(Sequence seq)
        {
            if(nonDebugNonGuiExitOnError)
            {
                ApplyRewriteSequence(seq, false);
                return;
            }

            bool debugModeActivated;

            if(!CheckDebuggerAlive())
            {
                if(!SetDebugMode(true)) return;
                debugModeActivated = true;
            }
            else debugModeActivated = false;

            ApplyRewriteSequence(seq, true);

            if(debugModeActivated && CheckDebuggerAlive())   // enabled debug mode here and didn't loose connection?
            {
                if (UserInterface.ShowMsgAskForYesNo("Do you want to leave debug mode?")) {
                    SetDebugMode(false);
                }
            }
        }

        public void DebugLayout()
        {
            if(!CheckDebuggerAlive())
            {
                debugOut.WriteLine("YComp is not active, yet!");
                return;
            }
            debugger.ForceLayout();
        }

        public void SetDebugLayout(String layout)
        {
            if(layout == null || !YCompClient.IsValidLayout(layout))
            {
                if(layout != null)
                    errOut.WriteLine("\"" + layout + "\" is not a valid layout name!");
                debugOut.WriteLine("Available layouts:");
                foreach(String layoutName in YCompClient.AvailableLayouts)
                    debugOut.WriteLine(" - " + layoutName);
                debugOut.WriteLine("Current layout: " + debugLayout);
                return;
            }

            if(InDebugMode)
                debugger.SetLayout(layout);

            debugLayout = layout;
        }

        public void GetDebugLayoutOptions()
        {
            if(!CheckDebuggerAlive())
            {
                errOut.WriteLine("Layout options can only be read, when YComp is active!");
                return;
            }

            debugger.GetLayoutOptions();
        }

        public void SetDebugLayoutOption(String optionName, String optionValue)
        {
            Dictionary<String, String> optMap;
            if(!debugLayoutOptions.TryGetValue(debugLayout, out optMap))
            {
                optMap = new Dictionary<String, String>();
                debugLayoutOptions[debugLayout] = optMap;
            }

            if(!CheckDebuggerAlive())
            {
                optMap[optionName] = optionValue; // remember option for debugger startup
                ChangeOrientationIfNecessary(optionName, optionValue);
                return;
            }

            if(debugger.SetLayoutOption(optionName, optionValue)) // only remember option if no error was reported
            {
                optMap[optionName] = optionValue;
                ChangeOrientationIfNecessary(optionName, optionValue);
            }
        }

        void ChangeOrientationIfNecessary(String optionName, string optionValue)
        {
            if(optionName.ToLower() != "orientation")
                return;

            switch(optionValue.ToLower())
            {
            case "top_to_bottom":
                curShellProcEnv.VcgFlags = curShellProcEnv.VcgFlags & ~VCGFlags.OrientMask | VCGFlags.OrientTopToBottom;
                break;
            case "bottom_to_top":
                curShellProcEnv.VcgFlags = curShellProcEnv.VcgFlags & ~VCGFlags.OrientMask | VCGFlags.OrientBottomToTop;
                break;
            case "left_to_right":
                curShellProcEnv.VcgFlags = curShellProcEnv.VcgFlags & ~VCGFlags.OrientMask | VCGFlags.OrientLeftToRight;
                break;
            case "right_to_left":
                curShellProcEnv.VcgFlags = curShellProcEnv.VcgFlags & ~VCGFlags.OrientMask | VCGFlags.OrientRightToLeft;
                break;
            default:
                Debug.Assert(false, "Unknown orientation: " + optionValue);
                break;
            }
        }

        public object Askfor(String typeName)
        {
            if(typeName == null)
            {
                UserInterface.ShowMsgAskForEnter("Pause..");
                return null;
            }

            if(TypesHelper.GetNodeOrEdgeType(typeName, curShellProcEnv.ProcEnv.NamedGraph.Model)!=null) // if type is node/edge type let the user select the element in yComp
            {
                if(!CheckDebuggerAlive())
                {
                    errOut.WriteLine("debug mode must be enabled (yComp available) for asking for a node/edge type");
                    return null;
                }

                debugOut.WriteLine("Select an element of type " + typeName + " by double clicking in yComp (ESC for abort)...");

                String id = debugger.ChooseGraphElement();
                if(id == null)
                    return null;

                debugOut.WriteLine("Received @(\"" + id + "\")");

                IGraphElement elem = curShellProcEnv.ProcEnv.NamedGraph.GetGraphElement(id);
                if(elem == null)
                {
                    errOut.WriteLine("Graph element does not exist (anymore?).");
                    return null;
                }
                if(!TypesHelper.IsSameOrSubtype(elem.Type.PackagePrefixedName, typeName, curShellProcEnv.ProcEnv.NamedGraph.Model))
                {
                    errOut.WriteLine(elem.Type.PackagePrefixedName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return elem;
            }
            else // else let the user type in the value
            {
                String inputValue = UserInterface.ShowMsgAskForString("Enter a value of type " + typeName + ": ");
                StringReader reader = new StringReader(inputValue);
                GrShell shellForParsing = new GrShell(reader);
                shellForParsing.SetImpl(this);
                object val = shellForParsing.Constant();
                String valTypeName = TypesHelper.XgrsTypeOfConstant(val, curShellProcEnv.ProcEnv.NamedGraph.Model);
                if(!TypesHelper.IsSameOrSubtype(valTypeName, typeName, curShellProcEnv.ProcEnv.NamedGraph.Model))
                {
                    errOut.WriteLine(valTypeName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return val;
            }
        }

        private ElementMode? ParseElementMode(String modeName)
        {
            ElementMode mode;

            if(modeName == null)
                goto showavail;

            try
            {
                mode = (ElementMode)Enum.Parse(typeof(ElementMode), modeName, true);
            }
            catch(ArgumentException)
            {
                errOut.Write("Unknown mode: " + modeName);
                goto showavail;
            }
            return mode;

showavail:
            debugOut.WriteLine("\nAvailable modes are:");
            foreach(String name in Enum.GetNames(typeof(ElementMode)))
                debugOut.WriteLine(" - {0}", name);
            debugOut.WriteLine();
            return null;
        }


        #region "dump" commands
        public void DumpGraph(String filename)
        {
            if(!GraphExists()) return;

            try
            {
                using(VCGDumper dump = new VCGDumper(filename, curShellProcEnv.VcgFlags, debugLayout))
                    GraphDumper.Dump(curShellProcEnv.ProcEnv.NamedGraph, dump, curShellProcEnv.DumpInfo);
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to dump graph: " + ex.Message);
            }
        }

        private GrColor? ParseGrColor(String colorName)
        {
            GrColor color;

            if(colorName == null)
                goto showavail;

            try
            {
                color = (GrColor) Enum.Parse(typeof(GrColor), colorName, true);
            }
            catch(ArgumentException)
            {
                errOut.Write("Unknown color: " + colorName);
                goto showavail;
            }
            return color;

showavail:
            debugOut.WriteLine("\nAvailable colors are:");
            foreach(String name in Enum.GetNames(typeof(GrColor)))
                debugOut.WriteLine(" - {0}", name);
            debugOut.WriteLine();
            return null;
        }

        private GrNodeShape? ParseGrNodeShape(String shapeName)
        {
            GrNodeShape shape;

            if(shapeName == null)
                goto showavail;

            try
            {
                shape = (GrNodeShape) Enum.Parse(typeof(GrNodeShape), shapeName, true);
            }
            catch(ArgumentException)
            {
                errOut.Write("Unknown node shape: " + shapeName);
                goto showavail;
            }
            return shape;

showavail:
            debugOut.WriteLine("\nAvailable node shapes are:");
            foreach(String name in Enum.GetNames(typeof(GrNodeShape)))
                debugOut.WriteLine(" - {0}", name);
            debugOut.WriteLine();
            return null;
        }

        private GrLineStyle? ParseGrLineStyle(String styleName)
        {
            GrLineStyle style;

            if(styleName == null)
                goto showavail;

            try
            {
                style = (GrLineStyle)Enum.Parse(typeof(GrLineStyle), styleName, true);
            }
            catch(ArgumentException)
            {
                errOut.Write("Unknown edge style: " + styleName);
                goto showavail;
            }
            if(style == GrLineStyle.Invisible)
            {
                errOut.Write("Edge style invisible not available");
                return null;
            }

            return style;

showavail:
            debugOut.WriteLine("\nAvailable line styles are:");
            foreach(String name in Enum.GetNames(typeof(GrLineStyle)))
                debugOut.WriteLine(" - {0}", name);
            debugOut.WriteLine();
            return null;
        }

		public void DebugOnAdd(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                errOut.WriteLine("Unknown comparison function: " + comparison);
                debugOut.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Add,
                message,
                mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a Debug::add command continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnRem(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                errOut.WriteLine("Unknown comparison function: " + comparison);
                debugOut.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Rem,
                message,
                mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a Debug::rem command continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnEmit(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                errOut.WriteLine("Unknown comparison function: " + comparison);
                debugOut.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Emit,
                message,
                mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a Debug::emit command continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnHalt(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                errOut.WriteLine("Unknown comparison function: " + comparison);
                debugOut.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Halt,
                message,
                mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(break_)
            {
                debugOut.Write("Notice: a Debug::halt command causes a break by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnHighlight(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                errOut.WriteLine("Unknown comparison function: " + comparison);
                debugOut.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Highlight,
                message,
                mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(break_)
            {
                debugOut.Write("Notice: a Debug::highlight causes a break by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnMatch(string actionname, bool break_, SequenceExpression if_)
        {
            if(GetProcEnv().Actions.GetAction(actionname) == null)
            {
                errOut.WriteLine("Action unknown: "+actionname);
                return;
            }
            SubruleDebuggingConfigurationRule cr = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Match,
                GetProcEnv().Actions.GetAction(actionname),
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                if_
                );
            curShellProcEnv.SubruleDebugConfig.Add(cr);
        }

        public void DebugOnNew(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule cr;
            if(elemName != null)
            {
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.New,
                    elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            else
            {
                if(graphElemType == null)
                {
                    errOut.WriteLine("Unknown graph element type");
                    return;
                }
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.New,
                    graphElemType,
                    only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a new node/edge continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnDelete(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule cr;
            if(elemName != null)
            {
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Delete,
                    elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            else
            {
                if(graphElemType == null)
                {
                    errOut.WriteLine("Unknown graph element type");
                    return;
                }
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Delete,
                    graphElemType,
                    only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a delete node/edge continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnRetype(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule cr;
            if(elemName != null)
            {
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Retype,
                    elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            else
            {
                if(graphElemType == null)
                {
                    errOut.WriteLine("Unknown graph element type");
                    return;
                }
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Retype,
                    graphElemType,
                    only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: a retype node/edge continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        public void DebugOnSetAttributes(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule cr;
            if(elemName != null)
            {
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.SetAttributes,
                    elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            else
            {
                if(graphElemType == null)
                {
                    errOut.WriteLine("Unknown graph element type");
                    return;
                }
                cr = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.SetAttributes,
                    graphElemType,
                    only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_
                    );
            }
            curShellProcEnv.SubruleDebugConfig.Add(cr);
            if(!break_)
            {
                debugOut.Write("Notice: an attribute assignment to a node/edge continues by default -- ");
                debugOut.WriteLine(cr.ToString());
            }
        }

        private SubruleMesssageMatchingMode ParseComparison(string comparison)
        {
            switch(comparison)
            {
                case "equals":
                    return SubruleMesssageMatchingMode.Equals;
                case "startsWith":
                    return SubruleMesssageMatchingMode.StartsWith;
                case "endsWith":
                    return SubruleMesssageMatchingMode.EndsWith;
                case "contains":
                    return SubruleMesssageMatchingMode.Contains;
                default:
                    return SubruleMesssageMatchingMode.Undefined;
            }
        }

        public bool SetDumpLabel(GrGenType type, String label, bool only)
        {
            if(type == null) return false;

            if(only)
                curShellProcEnv.DumpInfo.SetElemTypeLabel(type, label);
            else
            {
                foreach(GrGenType subType in type.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.SetElemTypeLabel(subType, label);
            }

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        delegate void SetNodeDumpColorProc(NodeType type, GrColor color);

        private bool SetDumpColor(NodeType type, String colorName, bool only, SetNodeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                    setDumpColorProc(subType, (GrColor) color);
            }

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        delegate void SetEdgeDumpColorProc(EdgeType type, GrColor color);

        private bool SetDumpColor(EdgeType type, String colorName, bool only, SetEdgeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                    setDumpColorProc(subType, (GrColor) color);
            }

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDumpNodeTypeColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeColor);
        }

        public bool SetDumpNodeTypeBorderColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeBorderColor);
        }

        public bool SetDumpNodeTypeTextColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeTextColor);
        }

        public bool SetDumpNodeTypeShape(NodeType type, String shapeName, bool only)
        {
            if(type == null) return false;

            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null) return false;

            if(only)
                curShellProcEnv.DumpInfo.SetNodeTypeShape(type, (GrNodeShape) shape);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.SetNodeTypeShape(subType, (GrNodeShape) shape);
            }
            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugNodeModeColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            realizers.ChangeNodeColor((ElementMode)mode, (GrColor)color);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugNodeModeBorderColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            realizers.ChangeNodeBorderColor((ElementMode)mode, (GrColor)color);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugNodeModeTextColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            realizers.ChangeNodeTextColor((ElementMode)mode, (GrColor)color);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugNodeModeShape(String modeName, String shapeName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null) return false;

            realizers.ChangeNodeShape((ElementMode)mode, (GrNodeShape)shape);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDumpEdgeTypeColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetEdgeTypeColor);
        }

        public bool SetDumpEdgeTypeTextColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetEdgeTypeTextColor);
        }

        public bool SetDumpEdgeTypeThickness(EdgeType type, int thickness, bool only)
        {
            if(type == null) return false;
            if(thickness < 1 || thickness > 5)
            {
                errOut.WriteLine("Edge thickness must be in [1..5]");
                return false;
            }

            if(only)
                curShellProcEnv.DumpInfo.SetEdgeTypeThickness(type, thickness);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.SetEdgeTypeThickness(subType, thickness);
            }

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDumpEdgeTypeLineStyle(EdgeType type, String styleName, bool only)
        {
            if(type == null) return false;
            GrLineStyle? style = ParseGrLineStyle(styleName);
            if(style == null) return false;

            if(only)
                curShellProcEnv.DumpInfo.SetEdgeTypeLineStyle(type, (GrLineStyle)style);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.SetEdgeTypeLineStyle(subType, (GrLineStyle)style);
            }

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugEdgeModeColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            realizers.ChangeEdgeColor((ElementMode)mode, (GrColor)color);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugEdgeModeTextColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return false;

            realizers.ChangeEdgeTextColor((ElementMode)mode, (GrColor)color);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugEdgeModeThickness(String modeName, int thickness)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            if(thickness < 1 || thickness > 5)
            {
                errOut.WriteLine("Edge thickness must be in [1..5]");
                return false;
            }

            realizers.ChangeEdgeThickness((ElementMode)mode, thickness);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDebugEdgeModeStyle(String modeName, String styleName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null) return false;
            GrLineStyle? style = ParseGrLineStyle(styleName);
            if(style == null) return false;

            realizers.ChangeEdgeStyle((ElementMode)mode, (GrLineStyle)style);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool AddDumpExcludeGraph()
        {
            curShellProcEnv.DumpInfo.ExcludeGraph();
            return true;
        }

        public bool SetDumpExcludeGraphOption(int contextDepth)
        {
            curShellProcEnv.DumpInfo.SetExcludeGraphContextDepth(contextDepth);
            return true;
        }

        public bool AddDumpExcludeNodeType(NodeType nodeType, bool only)
        {
            if(nodeType == null) return false;

            if(only)
                curShellProcEnv.DumpInfo.ExcludeNodeType(nodeType);
            else
                foreach(NodeType subType in nodeType.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.ExcludeNodeType(subType);

            return true;
        }

        public bool AddDumpExcludeEdgeType(EdgeType edgeType, bool only)
        {
            if(edgeType == null) return false;

            if(only)
                curShellProcEnv.DumpInfo.ExcludeEdgeType(edgeType);
            else
                foreach(EdgeType subType in edgeType.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.ExcludeEdgeType(subType);

            return true;
        }

        public bool AddDumpGroupNodesBy(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType adjNodeType, bool exactAdjNodeType, GroupMode groupMode)
        {
            if(nodeType == null || edgeType == null || adjNodeType == null) return false;

            curShellProcEnv.DumpInfo.AddOrExtendGroupNodeType(nodeType, exactNodeType, edgeType, exactEdgeType,
                adjNodeType, exactAdjNodeType, groupMode);

            return true;
        }

        public bool AddDumpInfoTag(GrGenType type, String attrName, bool only, bool isshort)
        {
            if(type == null) return false;

            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                errOut.WriteLine("Type \"" + type.PackagePrefixedName + "\" has no attribute \"" + attrName + "\"");
                return false;
            }

            InfoTag infoTag = new InfoTag(attrType, isshort);
            if(only)
                curShellProcEnv.DumpInfo.AddTypeInfoTag(type, infoTag);
            else
                foreach(GrGenType subtype in type.SubOrSameTypes)
                    curShellProcEnv.DumpInfo.AddTypeInfoTag(subtype, infoTag);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public void DumpReset()
        {
            if(!GraphExists()) return;

            curShellProcEnv.DumpInfo.Reset();
            realizers.ReSetElementRealizers();

            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }
        #endregion "dump" commands

        #region "container" commands

        public object GetAttribute(IGraphElement elem, String attrName)
        {
            if(elem == null) return null;
            AttributeType attrType = GetElementAttributeType(elem, attrName);
            if(attrType == null) return null;
            return elem.GetAttribute(attrName);
        }

        public void ContainerAdd(IGraphElement elem, String attrName, object keyObj)
        {
            object attr = GetAttribute(elem, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType, valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if(dict == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a set.");
                    return;
                }
                if(keyType != keyObj.GetType())
                {
                    errOut.WriteLine("Set type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(valueType != typeof(SetValueType))
                {
                    errOut.WriteLine("Not a set.");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, keyObj, null);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, keyObj, null);
                dict[keyObj] = null;
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not an array.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    errOut.WriteLine("Array type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, keyObj, null);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, keyObj, null);
                array.Add(keyObj);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a deque.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    errOut.WriteLine("Deque type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, keyObj, null);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, keyObj, null);
                deque.Enqueue(keyObj);
            }
            else
            {
                errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is neither a set nor an array nor a deque.");
            }
        }

        public void IndexedContainerAdd(IGraphElement elem, String attrName, object keyObj, object valueObj)
        {
            object attr = GetAttribute(elem, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType, valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if(dict == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a map.");
                    return;
                }
                if(keyType != keyObj.GetType())
                {
                    errOut.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(valueType != valueObj.GetType())
                {
                    errOut.WriteLine("Value type must be " + valueType + ", but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, valueObj, keyObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, valueObj, keyObj);
                dict[keyObj] = valueObj;
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not an array.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    errOut.WriteLine("Value type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(typeof(int) != valueObj.GetType())
                {
                    errOut.WriteLine("Index type must be int, but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, keyObj, valueObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, keyObj, valueObj);
                array.Insert((int)valueObj, keyObj);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a deque.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    errOut.WriteLine("Value type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(typeof(int) != valueObj.GetType())
                {
                    errOut.WriteLine("Index type must be int, but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.PutElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, keyObj, valueObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, keyObj, valueObj);
                deque.EnqueueAt((int)valueObj, keyObj);
            }
            else
            {
                errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is neither a map nor an array nor a deque.");
            }
        }

        public void ContainerRemove(IGraphElement elem, String attrName, object keyObj)
        {
            object attr = GetAttribute(elem, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType, valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if (dict == null) {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a set/map.");
                    return;
                }
                if(keyType != keyObj.GetType()) {
                    errOut.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                bool isSet = attrType.Kind == AttributeKind.SetAttr; // otherwise map
                AttributeChangeType changeType = AttributeChangeType.RemoveElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, isSet ? keyObj : null, isSet ? null : keyObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, isSet ? keyObj : null, isSet ? null : keyObj);
                dict.Remove(keyObj);
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not an array.");
                    return;
                }
                if(keyObj != null && typeof(int) != keyObj.GetType())
                {
                    errOut.WriteLine("Key/Index type must be int, but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.RemoveElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, null, keyObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, null, keyObj);
                if(keyObj != null)
                    array.RemoveAt((int)keyObj);
                else
                    array.RemoveAt(array.Count - 1);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is not a deque.");
                    return;
                }
                if(keyObj != null && typeof(int) != keyObj.GetType())
                {
                    errOut.WriteLine("Key/Index type must be int, but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = elem.Type.GetAttributeType(attrName);
                AttributeChangeType changeType = AttributeChangeType.RemoveElement;
                if(elem is INode)
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingNodeAttribute((INode)elem, attrType, changeType, null, keyObj);
                else
                    curShellProcEnv.ProcEnv.NamedGraph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, null, keyObj);
                if(keyObj != null)
                    deque.DequeueAt((int)keyObj);
                else
                    deque.Dequeue();
            }
            else
            {
                errOut.WriteLine(curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem) + "." + attrName + " is neither a map nor a set nor an array.");
            }
        }

        #endregion "container" commands

        private String StringToTextToken(String str)
        {
            if(str.IndexOf('\"') != -1) return "\'" + str + "\'";
            else return "\"" + str + "\"";
        }

        public void SaveGraph(String filename)
        {
            if(!GraphExists()) return;

            FileStream file = null;
            StreamWriter sw;
            try
            {
                file = new FileStream(filename, FileMode.Create);
                sw = new StreamWriter(file);
            }
            catch(IOException e)
            {
                errOut.WriteLine("Unable to create file \"{0}\": ", e.Message);
                if(file != null) file.Close();
                return;
            }
            try
            {
                INamedGraph graph = curShellProcEnv.ProcEnv.NamedGraph;
                sw.WriteLine("# Graph \"{0}\" saved by grShell", graph.Name);
                sw.WriteLine();
				if(curShellProcEnv.BackendFilename != null)
				{
					sw.Write("select backend " + StringToTextToken(curShellProcEnv.BackendFilename));
					foreach(String param in curShellProcEnv.BackendParameters)
					{
						sw.Write(" " + StringToTextToken(param));
					}
					sw.WriteLine();
				}

                // save graph

                GRSExport.ExportYouMustCloseStreamWriter(graph, sw, "");

                // save variables

                foreach(INode node in graph.Nodes)
                {
                    LinkedList<Variable> vars = curShellProcEnv.ProcEnv.GetElementVariables(node);
                    if(vars != null)
                    {
                        foreach(Variable var in vars)
                        {
                            sw.WriteLine("{0} = @(\"{1}\")", var.Name, graph.GetElementName(node));
                        }
                    }
                }

                foreach(IEdge edge in graph.Edges)
                {
                    LinkedList<Variable> vars = curShellProcEnv.ProcEnv.GetElementVariables(edge);
                    if(vars != null)
                    {
                        foreach(Variable var in vars)
                        {
                            sw.WriteLine("{0} = @(\"{1}\")", var.Name, graph.GetElementName(edge));
                        }
                    }
                }

                // save dump information

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeColor in curShellProcEnv.DumpInfo.NodeTypeColors)
                    sw.WriteLine("dump set node only {0} color {1}", nodeTypeColor.Key.PackagePrefixedName, nodeTypeColor.Value);

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeBorderColor in curShellProcEnv.DumpInfo.NodeTypeBorderColors)
                    sw.WriteLine("dump set node only {0} bordercolor {1}", nodeTypeBorderColor.Key.PackagePrefixedName, nodeTypeBorderColor.Value);

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeTextColor in curShellProcEnv.DumpInfo.NodeTypeTextColors)
                    sw.WriteLine("dump set node only {0} textcolor {1}", nodeTypeTextColor.Key.PackagePrefixedName, nodeTypeTextColor.Value);

                foreach(KeyValuePair<NodeType, GrNodeShape> nodeTypeShape in curShellProcEnv.DumpInfo.NodeTypeShapes)
                    sw.WriteLine("dump set node only {0} shape {1}", nodeTypeShape.Key.PackagePrefixedName, nodeTypeShape.Value);

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeColor in curShellProcEnv.DumpInfo.EdgeTypeColors)
                    sw.WriteLine("dump set edge only {0} color {1}", edgeTypeColor.Key.PackagePrefixedName, edgeTypeColor.Value);

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeTextColor in curShellProcEnv.DumpInfo.EdgeTypeTextColors)
                    sw.WriteLine("dump set edge only {0} textcolor {1}", edgeTypeTextColor.Key.PackagePrefixedName, edgeTypeTextColor.Value);

                if((curShellProcEnv.VcgFlags & VCGFlags.EdgeLabels) == 0)
                    sw.WriteLine("dump set edge labels off");

                foreach(NodeType excludedNodeType in curShellProcEnv.DumpInfo.ExcludedNodeTypes)
                    sw.WriteLine("dump add node only " + excludedNodeType.PackagePrefixedName + " exclude");

                foreach(EdgeType excludedEdgeType in curShellProcEnv.DumpInfo.ExcludedEdgeTypes)
                    sw.WriteLine("dump add edge only " + excludedEdgeType.PackagePrefixedName + " exclude");

                foreach(GroupNodeType groupNodeType in curShellProcEnv.DumpInfo.GroupNodeTypes)
                {
                    foreach(KeyValuePair<EdgeType, Dictionary<NodeType, GroupMode>> ekvp in groupNodeType.GroupEdges)
                    {
                        foreach(KeyValuePair<NodeType, GroupMode> nkvp in ekvp.Value)
                        {
                            String groupModeStr;
                            switch(nkvp.Value & GroupMode.GroupAllNodes)
                            {
                                case GroupMode.None:               groupModeStr = "no";       break;
                                case GroupMode.GroupIncomingNodes: groupModeStr = "incoming"; break;
                                case GroupMode.GroupOutgoingNodes: groupModeStr = "outgoing"; break;
                                case GroupMode.GroupAllNodes:      groupModeStr = "any";      break;
                                default: groupModeStr = "This case does not exist by definition..."; break;
                            }
                            sw.WriteLine("dump add node only " + groupNodeType.NodeType.PackagePrefixedName
                                + " group by " + ((nkvp.Value & GroupMode.Hidden) != 0 ? "hidden " : "") + groupModeStr
                                + " only " + ekvp.Key.PackagePrefixedName + " with only " + nkvp.Key.PackagePrefixedName);
                        }
                    }
                }

                foreach(KeyValuePair<GrGenType, List<InfoTag>> infoTagPair in curShellProcEnv.DumpInfo.InfoTags)
                {
                    String kind;
                    if(infoTagPair.Key.IsNodeType) kind = "node";
                    else kind = "edge";

                    foreach(InfoTag infoTag in infoTagPair.Value)
                    {
                        sw.WriteLine("dump add " + kind + " only " + infoTagPair.Key.PackagePrefixedName
                            + (infoTag.ShortInfoTag ? " shortinfotag " : " infotag ") + infoTag.AttributeType.Name);
                    }
                }

                if(debugLayoutOptions.Count != 0)
                {
                    foreach(KeyValuePair<String, Dictionary<String, String>> layoutOptions in debugLayoutOptions)
                    {
                        sw.WriteLine("debug set layout " + layoutOptions.Key);
                        foreach(KeyValuePair<String, String> option in layoutOptions.Value)
                            sw.WriteLine("debug set layout option " + option.Key + " " + option.Value);
                    }
                    sw.WriteLine("debug set layout " + debugLayout);
                }

                sw.WriteLine("# end of graph \"{0}\" saved by grShell", graph.Name);
            }
            catch(IOException e)
            {
                errOut.WriteLine("Write error: Unable to export file: {0}", e.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        public bool Export(List<String> filenameParameters)
        {
            if(!GraphExists()) return false;

            try
            {
                int startTime = Environment.TickCount;
                Porter.Export(curShellProcEnv.ProcEnv.NamedGraph, filenameParameters);
                debugOut.WriteLine("export done after: " + (Environment.TickCount - startTime) + " ms");
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to export graph: " + e.Message);
                return false;
            }
            debugOut.WriteLine("Graph \"" + curShellProcEnv.ProcEnv.NamedGraph.Name + "\" exported.");
            return true;
        }

        public bool Import(List<String> filenameParameters)
        {
            if(!BackendExists()) return false;

            if(newGraphExternalAssembliesReferenced.Count > 0 || newGraphStatistics != null
                || newGraphKeepDebug || newGraphLazyNIC || newGraphNoinline || newGraphProfile)
            {
                debugOut.WriteLine("Warning: \"new set\" and \"new add\" commands in force are ignored when the actions are built from an import. Ensure that the files are up to date, e.g. by using a \"new graph\" (or even \"new new graph\") before the import.");
            }

            if(filenameParameters[0] == "add")
            {
                filenameParameters.RemoveAt(0);
                return ImportDUnion(filenameParameters);
            }

            IGraph graph;
            try
            {
                int startTime = Environment.TickCount;
                IActions actions;
                graph = Porter.Import(curGraphBackend, filenameParameters, out actions);
                debugOut.WriteLine("import done after: " + (Environment.TickCount - startTime) + " ms");
                debugOut.WriteLine("graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                startTime = Environment.TickCount;

                if(graph is INamedGraph) // grs import returns already named graph
                    curShellProcEnv = new ShellGraphProcessingEnvironment((INamedGraph)graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");
                else // constructor building named graph
                    curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");
                if(InDebugMode)
                    debugger.ShellProcEnv = curShellProcEnv;
                INamedGraph importedNamedGraph = (INamedGraph)curShellProcEnv.ProcEnv.NamedGraph;
                if(actions!=null) ((BaseActions)actions).Graph = importedNamedGraph;
                debugOut.WriteLine("shell import done after: " + (Environment.TickCount - startTime) + " ms");
                debugOut.WriteLine("shell graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                curShellProcEnv.ProcEnv.Actions = actions;
                shellProcEnvs.Add(curShellProcEnv);
                ShowNumNodes(null, false);
                ShowNumEdges(null, false);
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to import graph: " + e.Message);
                return false;
            }

            debugOut.WriteLine("Graph \"" + graph.Name + "\" imported.");
            return true;
        }

        private bool ImportDUnion(List<string> filenameParameters)
        {
            IGraph graph;
            IActions actions;
            try
            {
                graph = Porter.Import(curGraphBackend, filenameParameters, out actions);
            }
            catch (Exception e)
            {
                errOut.WriteLine("Unable to import graph for union: " + e.Message);
                return false;
            }

            // convenience
            INodeModel node_model = this.CurrentGraph.Model.NodeModel;
            IEdgeModel edge_model = this.CurrentGraph.Model.EdgeModel;
            INamedGraph namedGraph = graph as INamedGraph;

            Dictionary<INode, INode> oldToNewNodeMap = new Dictionary<INode, INode>();

            foreach (INode node in graph.Nodes)
            {
                NodeType typ = node_model.GetType(node.Type.PackagePrefixedName);
                INode newNode = CurrentGraph.AddNode(typ);

                String name = null;
                if (namedGraph != null) name = namedGraph.GetElementName(node);
                if (name != null) CurrentGraph.SetElementPrefixName(newNode, name);

                foreach (AttributeType attr in typ.AttributeTypes)
                {
                    try { newNode.SetAttribute(attr.Name, node.GetAttribute(attr.Name)); }
                    catch { errOut.WriteLine("failed to copy attribute {0}", attr.Name); }
                }
                oldToNewNodeMap[node] = newNode;
            }

            foreach (IEdge edge in graph.Edges)
            {
                EdgeType typ = edge_model.GetType(edge.Type.PackagePrefixedName);
                INode newSource = oldToNewNodeMap[edge.Source];
                INode newTarget = oldToNewNodeMap[edge.Target];
                if ((newSource == null) || (newTarget == null)) {
                    errOut.WriteLine("failed to retrieve the new node equivalent from the dictionary!!");
                    return false;
                }
                IEdge newEdge = CurrentGraph.AddEdge(typ, newSource, newTarget);

                String name = null;
                if (namedGraph != null) name = namedGraph.GetElementName(edge);
                if (name != null) CurrentGraph.SetElementPrefixName(newEdge, name);

                foreach (AttributeType attr in typ.AttributeTypes)
                {
                    try { newEdge.SetAttribute(attr.Name, edge.GetAttribute(attr.Name)); }
                    catch { errOut.WriteLine("failed to copy attribute {0}", attr.Name); }
                }
            }

            debugOut.WriteLine("Graph \"" + graph.Name + "\" imported and added to current graph \"" + CurrentGraph.Name + "\"");
            return true;
        }

        public bool Record(String filename, bool actionSpecified, bool start)
        {
            if(!GraphExists()) return false;

            try
            {
                if(actionSpecified)
                {
                    if(start)
                        curShellProcEnv.ProcEnv.Recorder.StartRecording(filename);
                    else
                        curShellProcEnv.ProcEnv.Recorder.StopRecording(filename);
                }
                else
                {
                    if(curShellProcEnv.ProcEnv.Recorder.IsRecording(filename))
                        curShellProcEnv.ProcEnv.Recorder.StopRecording(filename);
                    else
                        curShellProcEnv.ProcEnv.Recorder.StartRecording(filename);
                }
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to change recording status: " + e.Message);
                return false;
            }

            if(curShellProcEnv.ProcEnv.Recorder.IsRecording(filename))
                debugOut.WriteLine("Started recording to \"" + filename + "\"");
            else
                debugOut.WriteLine("Stopped recording to \"" + filename + "\"");
            return true;
        }

        public bool Replay(String filename, GrShell grShell, String from, String to)
        {
            if(!GraphExists()) return false;

            debugOut.Write("Replaying \"" + filename + "\"");
            if(from != null) debugOut.Write(" from \"" + from + "\"");
            if(to != null) debugOut.Write(" to \"" + to + "\"");
            debugOut.WriteLine("..");

            if(Include(grShell, filename, from, to))
            {
                debugOut.Write("..replaying \"" + filename + "\"");
                if(from != null) debugOut.Write(" from \"" + from + "\"");
                if(to != null) debugOut.Write(" to \"" + to + "\"");
                debugOut.WriteLine(" ended");
                return true;
            }
            else
            {
                errOut.WriteLine("Error in replaying \"" + filename + "\"");
                return false;
            }
        }

/*        private String FormatName(IGraphElement elem)
        {
            String name;
            if(!curShellGraph.IDToName.TryGetValue(elem.ID, out name))
                return elem.ID.ToString();
            else
                return String.Format("\"{0}\" ({1})", name, elem.ID);
        }*/

        private bool DumpElems<T>(IEnumerable<T> elems, bool first) where T : IGraphElement
        {
            foreach (IGraphElement elem in elems)
            {
                if (!first)
                    debugOut.Write(',');
                else
                    first = false;
                debugOut.Write("\"{0}\"", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem));
            }
            return first;
        }

        public bool Validate(bool strict, bool onlySpecified)
        {
            if(!GraphExists()) return false;

            ValidationMode mode = ValidationMode.OnlyMultiplicitiesOfMatchingTypes;
            if(strict) mode = onlySpecified ? ValidationMode.StrictOnlySpecified : ValidationMode.Strict;
            List<ConnectionAssertionError> errors;
            bool valid = curShellProcEnv.ProcEnv.NamedGraph.Validate(mode, out errors);
            if(valid)
                debugOut.WriteLine("The graph is valid.");
            else
            {
                errOut.WriteLine("The graph is NOT valid:");
                foreach(ConnectionAssertionError error in errors)
                {
                    PrintValidateError(error);
                }
            }

            return valid;
        }

        public void PrintValidateError(ConnectionAssertionError error)
        {
            ValidateInfo valInfo = error.ValidateInfo;
            switch (error.CAEType)
            {
                case CAEType.EdgeNotSpecified:
                {
                    IEdge edge = (IEdge)error.Elem;
                    errOut.WriteLine("  CAE: {0} \"{1}\" -- {2} \"{3}\" {6} {4} \"{5}\" not specified",
                        edge.Source.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Source),
                        edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge),
                        edge.Target.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Target),
                        edge.Type.Directedness==Directedness.Directed ? "-->" : "--");
                    break;
                }
                case CAEType.NodeTooFewSources:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} \"{1}\" [{2}<{3}] -- {4} ", valInfo.SourceType.PackagePrefixedName,
                        curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceLower, valInfo.EdgeType.PackagePrefixedName);
                    bool first = DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        errOut.WriteLine(" -- {0}", valInfo.TargetType.PackagePrefixedName);
                    } else {
                        errOut.WriteLine(" --> {0}", valInfo.TargetType.PackagePrefixedName);
                    }
                    break;
                }
                case CAEType.NodeTooManySources:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} \"{1}\" [{2}>{3}] -- {4} ", valInfo.SourceType.PackagePrefixedName,
                        curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceUpper, valInfo.EdgeType.PackagePrefixedName);
                    bool first = DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        errOut.WriteLine(" -- {0}", valInfo.TargetType.PackagePrefixedName);
                    } else {
                        errOut.WriteLine(" --> {0}", valInfo.TargetType.PackagePrefixedName);
                    }
                    break;
                }
                case CAEType.NodeTooFewTargets:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} -- {1} ", valInfo.SourceType.PackagePrefixedName,
                             valInfo.EdgeType.PackagePrefixedName);
                    bool first = DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        errOut.WriteLine(" -- {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    } else {
                        errOut.WriteLine(" --> {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    }
                    break;
                }
                case CAEType.NodeTooManyTargets:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} -- {1} ", valInfo.SourceType.PackagePrefixedName,
                        valInfo.EdgeType.PackagePrefixedName);
                    bool first = DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        errOut.WriteLine(" -- {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                    } else {
                        errOut.WriteLine(" --> {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                    }
                    break;
                }
            }
        }

        IEnumerable<IEdge> OutgoingEdgeToNodeOfType(INode node, EdgeType edgeType, NodeType targetNodeType)
        {
            foreach (IEdge outEdge in node.GetExactOutgoing(edgeType))
            {
                if (!outEdge.Target.Type.IsA(targetNodeType)) continue;
                yield return outEdge;
            }
        }

        IEnumerable<IEdge> IncomingEdgeFromNodeOfType(INode node, EdgeType edgeType, NodeType sourceNodeType)
        {
            foreach (IEdge inEdge in node.GetExactIncoming(edgeType))
            {
                if (!inEdge.Source.Type.IsA(sourceNodeType)) continue;
                yield return inEdge;
            }
        }

        public bool ValidateWithSequence(Sequence seq)
        {
            if(!GraphExists()) return false;
            if(!ActionsExists()) return false;

            bool valid = curShellProcEnv.ProcEnv.ValidateWithSequence(seq);
            if (valid)
                debugOut.WriteLine("The graph is valid with respect to the given sequence.");
            else
                errOut.WriteLine("The graph is NOT valid with respect to the given sequence!");
            return valid;
        }

        public void NodeTypeIsA(INode node1, INode node2)
        {
            if(node1 == null || node2 == null) return;

            NodeType type1 = node1.Type;
            NodeType type2 = node2.Type;

            debugOut.WriteLine("{0} type {1} is a node: {2}", type1.PackagePrefixedName, type2.PackagePrefixedName,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void EdgeTypeIsA(IEdge edge1, IEdge edge2)
        {
            if(edge1 == null || edge2 == null) return;

            EdgeType type1 = edge1.Type;
            EdgeType type2 = edge2.Type;

            debugOut.WriteLine("{0} type {1} is an edge: {2}", type1.PackagePrefixedName, type2.PackagePrefixedName,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void CustomGraph(List<String> parameterList)
        {
            if(!GraphExists()) return;

            String[] parameters = parameterList.ToArray();
            try
            {
                curShellProcEnv.ProcEnv.NamedGraph.Custom(parameters);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine(e.Message);
            }
        }

        public void CustomActions(List<String> parameterList)
        {
            if(!ActionsExists()) return;

            String[] parameters = parameterList.ToArray();
            try
            {
                curShellProcEnv.ProcEnv.Actions.Custom(parameters);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine(e.Message);
            }
        }
    }
}
