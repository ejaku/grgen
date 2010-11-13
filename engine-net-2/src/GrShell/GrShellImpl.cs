/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define MATCHREWRITEDETAIL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public String Type; // set/map(domain) type
        public String TgtType; // map target type
        public ArrayList Values; // set/map(domain) values 
        public ArrayList TgtValues; // map target values

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

    public class ShellGraph
    {
        public NamedGraph Graph;
        public BaseActions Actions = null;
        public DumpInfo DumpInfo;
        public VCGFlags VcgFlags = VCGFlags.OrientTopToBottom | VCGFlags.EdgeLabels;
        public ParserPackage Parser = null;

        public String BackendFilename;
        public String[] BackendParameters;
        public String ModelFilename;
        public String ActionsFilename = null;

        public ShellGraph(IGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            Graph = new NamedGraph(graph);
            Debug.Assert(graph is LGSPGraph);
            if(graph is LGSPGraph) 
                ((LGSPGraph)graph).NamedGraph = Graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
        }

        public ShellGraph(NamedGraph graph, String backendFilename, String[] backendParameters, String modelFilename)
        {
            Graph = graph;
            DumpInfo = new DumpInfo(Graph.GetElementName);
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
        }

        public ShellGraph Clone(string name)
        {
            string realname = (name == null) ? Graph.Name + "-clone" : name;
            ShellGraph result = new ShellGraph(Graph.Clone(realname),
                BackendFilename, BackendParameters, ModelFilename);
            result.Actions = this.Actions;
            result.DumpInfo = this.DumpInfo;
            result.VcgFlags = this.VcgFlags;
            result.Parser = this.Parser;
            result.ActionsFilename = this.ActionsFilename;
            result.Graph.EmitWriter = this.Graph.EmitWriter;
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

    public class GrShellImpl
    {
        public class ClonedGraphStack : Stack<ShellGraph>
        {
            GrShellImpl impl;

            public ClonedGraphStack(GrShellImpl impl)
            {
                this.impl = impl;
                base.Push(impl.CurrentShellGraph);
            }

            public void Push(ShellGraph graph)
            {
                base.Push(graph);
                this.impl.graphs.Add(graph);
                this.SetActive();
            }

            public void PushClone()
            {
                Push(impl.CurrentShellGraph.Clone(null));
            }

            public ShellGraph Pop()
            {
                ShellGraph result = base.Pop();
                this.impl.DestroyGraph(result, true);
                this.SetActive();
                return result;
            }

            /// <summary>return true if the graph is active</summary>
            public bool IsActive()
            {
                return this.impl.CurrentShellGraph == this.Peek();
            }

            /// <summary>set the top graph as active</summary>
            public void SetActive()
            {
                this.impl.curShellGraph = this.Peek();
            }
        }

        public static readonly String VersionString = "GrShell v2.6";

        IBackend curGraphBackend = new LGSPBackend();
        String backendFilename = null;
        String[] backendParameters = null;

        List<ShellGraph> graphs = new List<ShellGraph>();
        ShellGraph curShellGraph = null;

        bool silence = false; // node/edge created successfully messages
        bool cancelSequence = false;
        Debugger debugger = null;
        bool pendingDebugEnable = false;
        String debugLayout = "Orthogonal";
        public bool nonDebugNonGuiExitOnError = false;
        public TextWriter debugOut = System.Console.Out;
        public TextWriter errOut = System.Console.Error;
        public IGrShellUI UserInterface = new GrShellConsoleUI(Console.In, Console.Out);

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

        /// <summary>Get a graph stack to manage the graph list. Requires a current graph.</summary>
        public ClonedGraphStack GetGraphStack()
        {
            if (!this.GraphExists()) {
                return null; 
            }
            return new ClonedGraphStack(this);
        }

        public bool OperationCancelled { get { return cancelSequence; } }
        public IWorkaround Workaround { get { return workaround; } }
        public bool InDebugMode { get { return debugger != null && !debugger.ConnectionLost; } }

        public static void PrintVersion()
        {
            Console.WriteLine(VersionString + " ($Revision$) (enter \"help\" for a list of commands)");
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
            if(curShellGraph == null || curShellGraph.Graph == null)
            {
                errOut.WriteLine("No graph. Make a new graph, first.");
                return false;
            }
            return true;
        }

        private bool ActionsExists()
        {
            if(curShellGraph == null || curShellGraph.Actions == null)
            {
                errOut.WriteLine("No actions. Select an actions object, first.");
                return false;
            }
            return true;
        }

        public ShellGraph CurrentShellGraph { get { return curShellGraph; } }

        public NamedGraph CurrentGraph
        {
            get
            {
                if(!GraphExists()) return null;
                return curShellGraph.Graph;
            }
        }

        public BaseActions CurrentActions
        {
            get
            {
                if (!ActionsExists()) return null;
                return curShellGraph.Actions;
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

        public IGraphElement GetElemByVar(String varName)
        {
            if(!GraphExists()) return null;
            object elem = curShellGraph.Graph.GetVariableValue(varName);
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
            return curShellGraph.Graph.GetVariableValue(varName) as IGraphElement;
        }

        public IGraphElement GetElemByName(String elemName)
        {
            if(!GraphExists()) return null;
            IGraphElement elem = curShellGraph.Graph.GetGraphElement(elemName);
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
            NodeType type = curShellGraph.Graph.Model.NodeModel.GetType(typeName);
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
            EdgeType type = curShellGraph.Graph.Model.EdgeModel.GetType(typeName);
            if(type == null)
            {
                errOut.WriteLine("Unknown edge type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public ShellGraph GetShellGraph(String graphName)
        {
            foreach(ShellGraph shellGraph in graphs)
                if(shellGraph.Graph.Name == graphName)
                    return shellGraph;

            errOut.WriteLine("Unknown graph: \"{0}\"", graphName);
            return null;
        }

        public ShellGraph GetShellGraph(int index)
        {
            if((uint) index >= (uint) graphs.Count)
                errOut.WriteLine("Graph index out of bounds!");

            return graphs[index];
        }

        public int NumGraphs
        {
            get { return this.graphs.Count; }
        }

        public void HandleSequenceParserException(SequenceParserException ex)
        {
            IAction action = ex.Action;
            errOut.WriteLine(ex.Message);

            debugOut.Write("Prototype: {0}", ex.RuleName);
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
                    debugOut.Write("{0}{1}", first ? "" : ", ", type.Name);
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
                    debugOut.Write("{0}{1}", first ? "" : ", ", type.Name);
                    first = false;
                }
                debugOut.Write(")");
            }
            debugOut.WriteLine();
        }

        public IAction GetAction(String actionName, int numParams, int numReturns, bool retSpecified)
        {
            if(!ActionsExists()) return null;
            IAction action = curShellGraph.Actions.GetAction(actionName);
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
                        debugOut.Write("{0}{1}", first ? "" : ",", type.Name);
                    }
                    debugOut.Write(")");
                }
                if(action.RulePattern.Outputs.Length != 0)
                {
                    debugOut.Write(" : (");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Outputs)
                    {
                        debugOut.Write("{0}{1}", first ? "" : ",", type.Name);
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
                + " - delete ...                Deletes something\n"
                + " - clear graph [<graph>]     Clears the current or the given graph\n"
                + " - show ...                  Shows something\n"
                + " - node type <n1> is <n2>    Tells whether the node n1 has a type compatible\n"
                + "                             to the type of n2\n"
                + " - edge type <e1> is <e2>    Tells whether the edge e1 has a type compatible\n"
                + "                             to the type of e2\n"
                + " - debug ...                 Debugging related commands\n"
                + " - xgrs <xgrs>               Executes the given extended graph rewrite sequence\n"
                + " - validate ...              Validate related commands\n"
                + " - dump ...                  Dump related commands\n"
                + " - save graph <filename>     Saves the current graph as a GrShell script\n"
                + " - export ...                Exports the current graph.\n"
                + " - import ...                Imports a graph instance and/or a graph model\n"
                + " - echo <text>               Writes the given text to the console\n"
                + " - custom graph ...          Graph backend specific commands\n"
                + " - custom actions ...        Action backend specific commands\n"
                + " - redirect emit <filename>  Redirects the GrGen emit instructions to a file\n"
                + " - redirect emit -           Afterwards emit instructions write to stdout again\n"
                + " - sync io                   Writes out all files (grIO framework)\n"
                + " - parse file <filename>     Parses the given file (ASTdapter framework)\n"
                + " - parse <text>              Parses the given string (ASTdapter framework)\n"
                + " - randomseed (time|<seed>)  Sets the seed of the random number generator\n"
                + "                             to the current time or the given integer\n"
                + " - <elem>.<member>           Shows the given graph element member\n"
                + " - <elem>.<member> = <val>   Sets the value of the given element member\n"
                + " - <var> = <expr>            Assigns the given expression to <var>.\n"
                + "                             Currently the expression may be:\n"
                + "                               - null\n"
                + "                               - <elem>\n"
                + "                               - <elem>.<member>\n"
                + "                               - <text>\n"
                + "                               - <number> (integer or floating point)\n"
                + "                               - true or false\n"
                + "                               - Enum::Value\n" 
                + "                               - set<S>{ elems } / map<S,T>{ elems }\n"
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
                + " - debug xgrs <xgrs>\n"
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
                + " - validate [exitonfailure] xgrs <xgrs>\n"
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
                + " - export <filename> [withvariables]\n"
                + "   Exports the current graph into the file <filename>,\n"
                + "   the filename extension specifies the format to be used.\n"
                + "   Available formats are GRS with \".grs\" or \".grsi\" (just other name)\n"
                + "   and GXL with \".gxl\".\n"
                + "   The GXL-file contains sa model of its own,\n"
                + "   the GRS-file is only complete with the \".gm\" of the original graph\n"
                + "   (and the \".grg\" if you want to use the original rules).\n"
                + "   The withvariables parameter is only valid for GRS export; if given,\n"
                + "   in addition to nodes and edges the variables get exported, too.\n");
        }

        #endregion Help text methods

        public void SyncIO()
        {
            Infrastructure.Flush(curShellGraph.Graph);
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

        public bool Include(GrShell grShell, String filename)
        {
            try
            {
                using(TextReader reader = new StreamReader(filename))
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
                                return false;
                        }
                        grShell.Eof = false;
                    }
                    finally
                    {
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
            foreach(ShellGraph shellGraph in graphs)
            {
                if(shellGraph.Graph.EmitWriter != Console.Out)
                {
                    shellGraph.Graph.EmitWriter.Close();
                    shellGraph.Graph.EmitWriter = Console.Out;
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

        public bool NewGraph(String specFilename, String graphName)
        {
            if(!BackendExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                specFilename = specFilename.Replace('\\', Path.DirectorySeparatorChar);

            if(specFilename.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || specFilename.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                IGraph graph;
                try
                {
                    graph = curGraphBackend.CreateGraph(specFilename, graphName, backendParameters);
                }
                catch(Exception e)
                {
                    errOut.WriteLine("Unable to create graph with model \"{0}\":\n{1}", specFilename, e.Message);
                    return false;
                }
                try
                {
                    curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, specFilename);
                }
                catch(Exception ex)
                {
                    errOut.WriteLine(ex);
                    errOut.WriteLine("Unable to create new graph: {0}", ex.Message);
                    return false;
                }
                graphs.Add(curShellGraph);
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
                    IGraph graph;
                    try
                    {
                        graph = curGraphBackend.CreateFromSpec(specFilename, graphName);
                    }
                    catch(Exception e)
                    {
                        errOut.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}", specFilename, e.Message);
                        return false;
                    }
                    try
                    {
                        curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, specFilename);
                    }
                    catch(Exception ex)
                    {
                        errOut.WriteLine(ex);
                        errOut.WriteLine("Unable to create new graph: {0}", ex.Message);
                        return false;
                    }
                    graphs.Add(curShellGraph);
                    debugOut.WriteLine("New graph \"{0}\" created from spec file \"{1}\".", graphName, specFilename);
                }
                else if(specFilename.EndsWith(".grg", StringComparison.OrdinalIgnoreCase))
                {
                    IGraph graph;
                    BaseActions actions;
                    try
                    {
                        curGraphBackend.CreateFromSpec(specFilename, graphName, out graph, out actions);
                    }
                    catch(Exception e)
                    {
                        errOut.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}", specFilename, e.Message);
                        return false;
                    }
                    try
                    {
                        curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, specFilename);
                    }
                    catch(Exception ex)
                    {
                        errOut.WriteLine(ex);
                        errOut.WriteLine("Unable to create new graph: {0}", ex.Message);
                        return false;
                    }
                    curShellGraph.Actions = actions;
                    graphs.Add(curShellGraph);
                    debugOut.WriteLine("New graph \"{0}\" and actions created from spec file \"{1}\".", graphName, specFilename);
                }
                else
                {
                    errOut.WriteLine("Unknown specification file format of file: \"" + specFilename + "\"");
                    return false;
                }
            }

            if(pendingDebugEnable)
                SetDebugMode(true);
            return true;
        }

        public bool OpenGraph(String modelFilename, String graphName)
        {
            if(!BackendExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                modelFilename = modelFilename.Replace('\\', Path.DirectorySeparatorChar);
            
            IGraph graph;
            try
            {
                graph = curGraphBackend.OpenGraph(modelFilename, graphName, backendParameters);
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to open graph with model \"{0}\":\n{1}", modelFilename, e.Message);
                return false;
            }
            try
            {
                curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, modelFilename);
            }
            catch(Exception ex)
            {
                errOut.WriteLine("Unable to open graph: {0}", ex.Message);
                return false;
            }
            graphs.Add(curShellGraph);

            if(pendingDebugEnable)
                SetDebugMode(true);
            return true;
        }

        public void SelectGraph(ShellGraph shellGraph)
        {
            curShellGraph = shellGraph ?? curShellGraph;
        }

        public bool SelectActions(String actionFilename)
        {
            if(!GraphExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                actionFilename = actionFilename.Replace('\\', Path.DirectorySeparatorChar);

            try
            {
                curShellGraph.Actions = curShellGraph.Graph.LoadActions(actionFilename);
                curShellGraph.ActionsFilename = actionFilename;
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
                curShellGraph.Parser = new ParserPackage(parserAssemblyName, mainMethodName);
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
                nodeType = curShellGraph.Graph.Model.NodeModel.GetType(elemDef.TypeName);
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
            else nodeType = curShellGraph.Graph.Model.NodeModel.RootType;

            if(elemDef.Attributes != null && !CheckAttributes(nodeType, elemDef.Attributes)) return null;

            INode node;
            try
            {
                node = curShellGraph.Graph.AddNode(nodeType, elemDef.VarName, elemDef.ElemName);
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
                    curShellGraph.Graph.Remove(node);
                    return null;
                }
            }

            if (!silence)
            {
                if (elemDef.ElemName == null)
                    debugOut.WriteLine("New node \"{0}\" of type \"{1}\" has been created.", curShellGraph.Graph.GetElementName(node), node.Type.Name);
                else
                    debugOut.WriteLine("New node \"{0}\" of type \"{1}\" has been created.", elemDef.ElemName, node.Type.Name);
            }

            return node;
        }

        public IEdge NewEdge(ElementDef elemDef, INode node1, INode node2, bool directed)
        {
            if(node1 == null || node2 == null) return null;

            EdgeType edgeType;
            if(elemDef.TypeName != null)
            {
                edgeType = curShellGraph.Graph.Model.EdgeModel.GetType(elemDef.TypeName);
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
                if (directed) edgeType = curShellGraph.Graph.Model.EdgeModel.GetType("Edge");
                else edgeType = curShellGraph.Graph.Model.EdgeModel.GetType("UEdge");
            }

            if(elemDef.Attributes != null && !CheckAttributes(edgeType, elemDef.Attributes)) return null;

            IEdge edge;
            try
            {
                edge = curShellGraph.Graph.AddEdge(edgeType, node1, node2, elemDef.VarName, elemDef.ElemName);
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
                    curShellGraph.Graph.Remove(edge);
                    return null;
                }
            }

            if (!silence)
            {
                if (elemDef.ElemName == null)
                    debugOut.WriteLine("New edge \"{0}\" of type \"{1}\" has been created.", curShellGraph.Graph.GetElementName(edge), edge.Type.Name);
                else
                    debugOut.WriteLine("New edge \"{0}\" of type \"{1}\" has been created.", elemDef.ElemName, edge.Type.Name);
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
                case AttributeKind.IntegerAttr:
                {
                    int val;
                    if(valueString.StartsWith("0x"))
                    {
                        if(!Int32.TryParse(valueString.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            errOut.WriteLine("Attribute {0} must be an integer!", attribute);
                            throw new Exception("Unknown integer literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!Int32.TryParse(valueString, out val))
                    {
                        errOut.WriteLine("Attribute {0} must be an integer!", attribute);
                        throw new Exception("Unknown integer literal" + valueString);
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
                    if(valueString != "null")
                    {
                        errOut.WriteLine("Attribute \"" + attribute + "\" is an object type attribute!\n"
                                + "It is not possible to assign a value other than null to an object type attribute!");
                        throw new Exception("Unknown object literal (only null allowed)" + valueString);
                    }
                    value = null;
                    break;
                }
            }
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
                        errOut.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.Name, par.Key);
                        return false;
                    }
                    IDictionary setmap = null;
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        if(par.Value!="set") {
                            errOut.WriteLine("Attribute \"{0}\" must be a set constructor!", par.Key);
                            throw new Exception("Set literal expected");
                        }
                        setmap = DictionaryHelper.NewDictionary(
                            DictionaryHelper.GetTypeFromNameForDictionary(par.Type, curShellGraph.Graph),
                            typeof(de.unika.ipd.grGen.libGr.SetValueType));
                        foreach(object val in par.Values)
                        {
                            setmap.Add(ParseAttributeValue(attrType.ValueType, (String)val, par.Key), null);
                        }
                        value = setmap;
                        break;
                    case AttributeKind.MapAttr:
                        if(par.Value!="map") {
                            errOut.WriteLine("Attribute \"{0}\" must be a map constructor!", par.Key);
                            throw new Exception("Map literal expected");
                        }
                        setmap = DictionaryHelper.NewDictionary(
                            DictionaryHelper.GetTypeFromNameForDictionary(par.Type, curShellGraph.Graph),
                            DictionaryHelper.GetTypeFromNameForDictionary(par.TgtType, curShellGraph.Graph));
                        IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
                        foreach(object val in par.Values)
                        {
                            tgtValEnum.MoveNext();
                            setmap.Add(ParseAttributeValue(attrType.KeyType, (String)val, par.Key),
                                ParseAttributeValue(attrType.ValueType, (String)tgtValEnum.Current, par.Key));
                        }
                        value = setmap;
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
                        errOut.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", elem.Type.Name, par.Key);
                        return false;
                    }
                    IDictionary setmap = null;
                    switch(attrType.Kind)
                    {
                    case AttributeKind.SetAttr:
                        if(par.Value!="set") {
                            errOut.WriteLine("Attribute \"{0}\" must be a set constructor!", par.Key);
                            throw new Exception("Set literal expected");
                        }
                        setmap = DictionaryHelper.NewDictionary(
                            DictionaryHelper.GetTypeFromNameForDictionary(par.Type, curShellGraph.Graph),
                            typeof(de.unika.ipd.grGen.libGr.SetValueType));
                        foreach(object val in par.Values)
                        {
                            setmap.Add(ParseAttributeValue(attrType.ValueType, (String)val, par.Key), null);
                        }
                        value = setmap;
                        break;
                    case AttributeKind.MapAttr:
                        if(par.Value!="map") {
                            errOut.WriteLine("Attribute \"{0}\" must be a map constructor!", par.Key);
                            throw new Exception("Map literal expected");
                        }
                        setmap = DictionaryHelper.NewDictionary(
                            DictionaryHelper.GetTypeFromNameForDictionary(par.Type, curShellGraph.Graph),
                            DictionaryHelper.GetTypeFromNameForDictionary(par.TgtType, curShellGraph.Graph));
                        IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
                        foreach(object val in par.Values)
                        {
                            tgtValEnum.MoveNext();
                            setmap.Add(ParseAttributeValue(attrType.KeyType, (String)val, par.Key),
                                ParseAttributeValue(attrType.ValueType, (String)tgtValEnum.Current, par.Key));
                        }
                        value = setmap;
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
                    curShellGraph.Graph.ChangingNodeAttribute((INode)elem, attrType, changeType, value, null);
                else
                    curShellGraph.Graph.ChangingEdgeAttribute((IEdge)elem, attrType, changeType, value, null);
                elem.SetAttribute(par.Key, value);
            }
            return true;
        }
        #endregion "new" graph element commands

        #region "remove" / "destroy" commands
        public bool Remove(INode node)
        {
            if(node == null) return false;
            try
            {
                curShellGraph.Graph.RemoveEdges(node);
                curShellGraph.Graph.Remove(node);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine("Unable to remove node: " + e.Message);
                return false;
            }
            return true;
        }

        public bool Remove(IEdge edge)
        {
            if(edge == null) return false;
            curShellGraph.Graph.Remove(edge);
            return true;
        }

        public void ClearGraph(ShellGraph shellGraph, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists()) return;
                shellGraph = curShellGraph;
            }
            else if(shellGraph == null) return;

            shellGraph.Graph.Clear();
        }

        public bool DestroyGraph(ShellGraph shellGraph, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists()) return false;
                shellGraph = curShellGraph;
            }
            else if(shellGraph == null) return false;

            if(InDebugMode && debugger.CurrentShellGraph == shellGraph) SetDebugMode(false);

            if(shellGraph == curShellGraph)
                curShellGraph = null;
            shellGraph.Graph.DestroyGraph();
            graphs.Remove(shellGraph);

            return true;
        }
        #endregion "remove" commands

        #region "show" commands
        private bool ShowElements<T>(IEnumerable<T> elements) where T : IGraphElement
        {
            if(!elements.GetEnumerator().MoveNext()) return false;

            debugOut.WriteLine("{0,-20} {1}", "name", "type");
            foreach(IGraphElement elem in elements)
            {
                debugOut.WriteLine("{0,-20} {1}", curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
            }
            return true;
        }

        public void ShowNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists()) return;
                nodeType = curShellGraph.Graph.Model.NodeModel.RootType;
            }

            IEnumerable<INode> nodes = only ? curShellGraph.Graph.GetExactNodes(nodeType)
                : curShellGraph.Graph.GetCompatibleNodes(nodeType);
            if(!ShowElements(nodes))
                errOut.WriteLine("There are no nodes " + (only ? "compatible to" : "of") + " type \"" + nodeType.Name + "\"!");
        }

        public void ShowEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists()) return;
                edgeType = curShellGraph.Graph.Model.EdgeModel.RootType;
            }

            IEnumerable<IEdge> edges = only ? curShellGraph.Graph.GetExactEdges(edgeType)
                : curShellGraph.Graph.GetCompatibleEdges(edgeType);
            if(!ShowElements(edges))
                errOut.WriteLine("There are no edges of " + (only ? "compatible to" : "of") + " type \"" + edgeType.Name + "\"!");
        }

        public void ShowNumNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists()) return;
                nodeType = curShellGraph.Graph.Model.NodeModel.RootType;
            }
            if(only)
                debugOut.WriteLine("Number of nodes of the type \"" + nodeType.Name + "\": "
                    + curShellGraph.Graph.GetNumExactNodes(nodeType));
            else
                debugOut.WriteLine("Number of nodes compatible to type \"" + nodeType.Name + "\": "
                    + curShellGraph.Graph.GetNumCompatibleNodes(nodeType));
        }

        public void ShowNumEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists()) return;
                edgeType = curShellGraph.Graph.Model.EdgeModel.RootType;
            }
            if(only)
                debugOut.WriteLine("Number of edges of the type \"" + edgeType.Name + "\": "
                    + curShellGraph.Graph.GetNumExactEdges(edgeType));
            else
                debugOut.WriteLine("Number of edges compatible to type \"" + edgeType.Name + "\": "
                    + curShellGraph.Graph.GetNumCompatibleEdges(edgeType));
        }

        public void ShowNodeTypes()
        {
            if(!GraphExists()) return;

            if(curShellGraph.Graph.Model.NodeModel.Types.Length == 0)
            {
                errOut.WriteLine("This model has no node types!");
            }
            else
            {
                debugOut.WriteLine("Node types:");
                foreach(NodeType type in curShellGraph.Graph.Model.NodeModel.Types)
                    debugOut.WriteLine(" - \"{0}\"", type.Name);
            }
        }

        public void ShowEdgeTypes()
        {
            if(!GraphExists()) return;

            if(curShellGraph.Graph.Model.EdgeModel.Types.Length == 0)
            {
                errOut.WriteLine("This model has no edge types!");
            }
            else
            {
                debugOut.WriteLine("Edge types:");
                foreach(EdgeType type in curShellGraph.Graph.Model.EdgeModel.Types)
                    debugOut.WriteLine(" - \"{0}\"", type.Name);
            }
        }

        public void ShowSuperTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                errOut.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.Name + "\" has no super types!");
            }
            else
            {
                debugOut.WriteLine("Super types of " + (isNode ? "node" : "edge") + " type \"" + elemType.Name + "\":");
                foreach(GrGenType type in elemType.SuperTypes)
                    debugOut.WriteLine(" - \"" + type.Name + "\"");
            }
        }

        public void ShowSubTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                errOut.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.Name + "\" has no super types!");
            }
            else
            {
                debugOut.WriteLine("Sub types of " + (isNode ? "node" : "edge") + " type \"{0}\":", elemType.Name);
                foreach(GrGenType type in elemType.SubTypes)
                    debugOut.WriteLine(" - \"{0}\"", type.Name);
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

        public void ShowGraphWith(String programName, String arguments)
        {
            if(!GraphExists()) return;
            if(nonDebugNonGuiExitOnError) return;

            String filename;
            int id = 0;
            do
            {
                filename = "tmpgraph" + id + ".vcg";
                id++;
            }
            while(File.Exists(filename));

            VCGDumper dump = new VCGDumper(filename, curShellGraph.VcgFlags, debugLayout);
            curShellGraph.Graph.Dump(dump, curShellGraph.DumpInfo);
            dump.FinishDump();

            Thread t = new Thread(new ParameterizedThreadStart(ShowGraphThread));
            t.Start(new ShowGraphParam(programName, arguments, filename));
        }

        public void ShowGraphs()
        {
            if(graphs.Count == 0)
            {
                errOut.WriteLine("No graphs available.");
                return;
            }
            debugOut.WriteLine("Graphs:");
            for(int i = 0; i < graphs.Count; i++)
                debugOut.WriteLine(" - \"" + graphs[i].Graph.Name + "\" (" + i + ")");
        }

        public void ShowActions()
        {
            if(!ActionsExists()) return;

            if(!curShellGraph.Actions.Actions.GetEnumerator().MoveNext())
            {
                errOut.WriteLine("No actions available.");
                return;
            }

            debugOut.WriteLine("Actions:");
            foreach(IAction action in curShellGraph.Actions.Actions)
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
                        debugOut.Write(inType.Name);
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
                        debugOut.Write(outType.Name);
                    }
                    debugOut.Write(")");
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
                    case AttributeKind.IntegerAttr: kind = "int"; break;
                    case AttributeKind.BooleanAttr: kind = "boolean"; break;
                    case AttributeKind.StringAttr: kind = "string"; break;
                    case AttributeKind.EnumAttr: kind = attrType.EnumType.Name; break;
                    case AttributeKind.FloatAttr: kind = "float"; break;
                    case AttributeKind.DoubleAttr: kind = "double"; break;
                    case AttributeKind.ObjectAttr: kind = "object"; break;
                    default: kind = "<INVALID>"; break;
                }
                debugOut.WriteLine(" - {0,-24} {1}::{2}", kind, attrType.OwnerType.Name, attrType.Name);
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
                ShowAvailableAttributes(curShellGraph.Graph.Model.NodeModel.AttributeTypes, null);
            }
            else
            {
                debugOut.WriteLine("The available attributes for {0} \"{1}\":", 
                    (showOnly ? "node type only" : "node type"), nodeType.Name);
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
                ShowAvailableAttributes(curShellGraph.Graph.Model.EdgeModel.AttributeTypes, null);
            }
            else
            {
                debugOut.WriteLine("The available attributes for {0} \"{1}\":",
                    (showOnly ? "edge type only" : "edge type"), edgeType.Name);
                ShowAvailableAttributes(edgeType.AttributeTypes, showOnly ? edgeType : null);
            }
        }

        public AttributeType GetElementAttributeType(IGraphElement elem, String attributeName)
        {
            AttributeType attrType = elem.Type.GetAttributeType(attributeName);
            if(attrType == null)
            {
                debugOut.WriteLine(((elem is INode) ? "Node" : "Edge") + " \"" + curShellGraph.Graph.GetElementName(elem)
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
                    curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
                return;
            }
            debugOut.WriteLine("All attributes for {0} \"{1}\" of type \"{2}\":", (elem is INode) ? "node" : "edge",
                curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
                debugOut.WriteLine(" - {0}::{1} = {2}", attrType.OwnerType.Name,
                    attrType.Name, elem.GetAttribute(attrType.Name));
        }

        public void ShowElementAttribute(IGraphElement elem, String attributeName)
        {
            if(elem == null) return;

            AttributeType attrType = GetElementAttributeType(elem, attributeName);
            if(attrType == null) return;

            if (attrType.Kind == AttributeKind.MapAttr)
            {
                Type keyType, valueType;
                IDictionary dict = DictionaryHelper.GetDictionaryTypes(
                    elem.GetAttribute(attributeName), out keyType, out valueType);
                debugOut.Write("The value of attribute \"" + attributeName + "\" is: \"{");
                bool first = true;
                foreach(DictionaryEntry entry in dict)
                {
                    if (first)
                        first = false;
                    else
                        debugOut.Write(", ");
                    debugOut.Write(entry.Key + "->" + entry.Value);
                }
                debugOut.WriteLine("}\".");
            }
            else if (attrType.Kind == AttributeKind.SetAttr)
            {
                Type keyType, valueType;
                IDictionary dict = DictionaryHelper.GetDictionaryTypes(
                    elem.GetAttribute(attributeName), out keyType, out valueType);
                debugOut.Write("The value of attribute \"" + attributeName + "\" is: \"{");
                bool first = true;
                foreach (DictionaryEntry entry in dict)
                {
                    if (first)
                        first = false;
                    else
                        debugOut.Write(", ");
                    debugOut.Write(entry.Key);
                }
                debugOut.WriteLine("}\".");
            }
            else
            {
                debugOut.WriteLine("The value of attribute \"" + attributeName + "\" is: \"" + elem.GetAttribute(attributeName) + "\".");
            }
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
                    DictionaryHelper.ToString((IDictionary)val, out type, out content, curShellGraph!=null ? curShellGraph.Graph : null);
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                if(val is LGSPNode && GraphExists())
                {
                    LGSPNode node = (LGSPNode)val;
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + node.Type.Name + " is: \"" + curShellGraph.Graph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                if(val is LGSPEdge && GraphExists())
                {
                    LGSPEdge edge = (LGSPEdge)val;
                    debugOut.WriteLine("The value of variable \"" + name + "\" of type " + edge.Type.Name + " is: \"" + curShellGraph.Graph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                DictionaryHelper.ToString(val, out type, out content, curShellGraph!=null ? curShellGraph.Graph : null);
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

        public object GetVarValue(String varName)
        {
            if(!GraphExists()) return null;
            object val = curShellGraph.Graph.GetVariableValue(varName);
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
            curShellGraph.Graph.SetVariableValue(varName, elem);
        }

        public void SetRandomSeed(int seed)
        {
            Sequence.randomGenerator = new Random(seed);
        }

        public bool RedirectEmit(String filename)
        {
            if(!GraphExists()) return false;

            if(curShellGraph.Graph.EmitWriter != Console.Out)
                curShellGraph.Graph.EmitWriter.Close();
            if(filename == "-")
                curShellGraph.Graph.EmitWriter = Console.Out;
            else
            {
                try
                {
                    curShellGraph.Graph.EmitWriter = new StreamWriter(filename);
                }
                catch(Exception ex)
                {
                    errOut.WriteLine("Unable to redirect emit to file \"" + filename + "\":\n" + ex.Message);
                    curShellGraph.Graph.EmitWriter = Console.Out;
                    return false;
                }
            }
            return true;
        }

        public bool ParseFile(String filename)
        {
            if(!GraphExists()) return false;
            if(curShellGraph.Parser == null)
            {
                errOut.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
                return false;
            }
            try
            {
                using(FileStream file = new FileStream(filename, FileMode.Open))
                {
                    ASTdapter.ASTdapter astDapter = new ASTdapter.ASTdapter(curShellGraph.Parser);
                    astDapter.Load(file, curShellGraph.Graph);
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
            if(curShellGraph.Parser == null)
            {
                errOut.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
                return false;
            }
            try
            {
                ASTdapter.ASTdapter astDapter = new ASTdapter.ASTdapter(curShellGraph.Parser);
                astDapter.Load(str, curShellGraph.Graph);
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
            if((seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll) && ((SequenceRule)seq).Special)
                return true;

            foreach(Sequence child in seq.Children)
                if(ContainsSpecial(child)) return true;

            return false;
        }

        Sequence curGRS;
        SequenceRule curRule;

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
                curShellGraph.Graph.OnEntereringSequence += new EnterSequenceHandler(DumpOnEntereringSequence);
                curShellGraph.Graph.OnExitingSequence += new ExitSequenceHandler(DumpOnExitingSequence);
                installedDumpHandlers = true;
            }
            else curShellGraph.Graph.OnEntereringSequence += new EnterSequenceHandler(NormalEnteringSequenceHandler);

            curGRS = seq;
            curRule = null;

            debugOut.WriteLine("Executing Graph Rewrite Sequence... (CTRL+C for abort)");
            cancelSequence = false;
            PerformanceInfo perfInfo = new PerformanceInfo();
            curShellGraph.Graph.PerformanceInfo = perfInfo;
            try
            {
                bool result = curShellGraph.Graph.ApplyGraphRewriteSequence(seq, debugger);
                seq.ResetExecutionState();
                debugOut.WriteLine("Executing Graph Rewrite Sequence done after {0} ms with result {1}:", perfInfo.TotalTimeMS, result);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
                debugOut.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
                debugOut.WriteLine(" - {0} rewrites performed in {1} ms", perfInfo.RewritesPerformed, perfInfo.TotalRewriteTimeMS);
#if DEBUGACTIONS
                debugOut.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                debugOut.WriteLine(" - {0} matches found", perfInfo.MatchesFound);
                debugOut.WriteLine(" - {0} rewrites performed", perfInfo.RewritesPerformed);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(curRule == null)
                    errOut.WriteLine("Rewrite sequence aborted!");
                else
                {
                    errOut.WriteLine("Rewrite sequence aborted after position:");
                    Debugger.PrintSequence(curGRS, curRule, Workaround);
                    errOut.WriteLine();
                }
            }
            curShellGraph.Graph.PerformanceInfo = null;
            curRule = null;
            curGRS = null;

            if(InDebugMode)
            {
                debugger.NotifyOnConnectionLost = false;
                debugger.FinishRewriteSequence();
            }

            StreamWriter emitWriter = curShellGraph.Graph.EmitWriter as StreamWriter;
            if(emitWriter != null)
                emitWriter.Flush();

            if(installedDumpHandlers)
            {
                curShellGraph.Graph.OnEntereringSequence -= new EnterSequenceHandler(DumpOnEntereringSequence);
                curShellGraph.Graph.OnExitingSequence -= new ExitSequenceHandler(DumpOnExitingSequence);
            }
            else curShellGraph.Graph.OnEntereringSequence -= new EnterSequenceHandler(NormalEnteringSequenceHandler);
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

            if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll)
                curRule = (SequenceRule) seq;
        }

        void DumpOnEntereringSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll)
            {
                curRule = (SequenceRule) seq;
                if(curRule.Special)
                    curShellGraph.Graph.OnFinishing += new BeforeFinishHandler(DumpOnFinishing);
            }
        }

        void DumpOnExitingSequence(Sequence seq)
        {
            if(seq.SequenceType == SequenceType.Rule || seq.SequenceType == SequenceType.RuleAll)
            {
                SequenceRule ruleSeq = (SequenceRule) seq;
                if(ruleSeq != null && ruleSeq.Special)
                    curShellGraph.Graph.OnFinishing -= new BeforeFinishHandler(DumpOnFinishing);
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
                debugOut.WriteLine(indentation + match.Pattern.Nodes[i++].UnprefixedName + ": " + curShellGraph.Graph.GetElementName(node));
            int j = 0;
            foreach (IEdge edge in match.Edges)
                debugOut.WriteLine(indentation + match.Pattern.Edges[j++].UnprefixedName + ": " + curShellGraph.Graph.GetElementName(edge));

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
                if(CurrentShellGraph == null)
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
                if(CurrentShellGraph == null && pendingDebugEnable)
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
                optMap[optionName] = optionValue;       // remember option for debugger startup
                return;
            }

            if(debugger.SetLayoutOption(optionName, optionValue))
                optMap[optionName] = optionValue;       // only remember option if no error was reported
        }

        public object Askfor(String typeName)
        {
            if(TypesHelper.GetNodeOrEdgeType(typeName, curShellGraph.Graph.Model)!=null) // if type is node/edge type let the user select the element in yComp
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

                IGraphElement elem = curShellGraph.Graph.GetGraphElement(id);
                if(elem == null)
                {
                    errOut.WriteLine("Graph element does not exist (anymore?).");
                    return null;
                }
                if(!TypesHelper.IsSameOrSubtype(elem.Type.Name, typeName, curShellGraph.Graph.Model))
                {
                    errOut.WriteLine(elem.Type.Name + " is not the same type as/a subtype of " + typeName + ".");
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
                String valTypeName = TypesHelper.XgrsTypeOfConstant(val, curShellGraph.Graph.Model);
                if(!TypesHelper.IsSameOrSubtype(valTypeName, typeName, curShellGraph.Graph.Model))
                {
                    errOut.WriteLine(valTypeName + " is not the same type as/a subtype of " + typeName + ".");
                    return null;
                }
                return val;
            }
        }

        #region "dump" commands
        public void DumpGraph(String filename)
        {
            if(!GraphExists()) return;

            try
            {
                using(VCGDumper dump = new VCGDumper(filename, curShellGraph.VcgFlags, debugLayout))
                    curShellGraph.Graph.Dump(dump, curShellGraph.DumpInfo);
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
            debugOut.WriteLine("\nAvailable node shapes are:", shapeName);
            foreach(String name in Enum.GetNames(typeof(GrNodeShape)))
                debugOut.WriteLine(" - {0}", name);
            debugOut.WriteLine();
            return null;
        }

        public bool SetDumpLabel(GrGenType type, String label, bool only)
        {
            if(type == null) return false;

            if(only)
                curShellGraph.DumpInfo.SetElemTypeLabel(type, label);
            else
            {
                foreach(GrGenType subType in type.SubOrSameTypes)
                    curShellGraph.DumpInfo.SetElemTypeLabel(subType, label);
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
            return SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeColor);
        }

        public bool SetDumpNodeTypeBorderColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeBorderColor);
        }

        public bool SetDumpNodeTypeTextColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeTextColor);
        }

        public bool SetDumpNodeTypeShape(NodeType type, String shapeName, bool only)
        {
            if(type == null) return false;

            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null) return false;

            if(only)
                curShellGraph.DumpInfo.SetNodeTypeShape(type, (GrNodeShape) shape);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                    curShellGraph.DumpInfo.SetNodeTypeShape(subType, (GrNodeShape) shape);
            }
            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public bool SetDumpEdgeTypeColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetEdgeTypeColor);
        }

        public bool SetDumpEdgeTypeTextColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return false;
            return SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetEdgeTypeTextColor);
        }

        public bool AddDumpExcludeNodeType(NodeType nodeType, bool only)
        {
            if(nodeType == null) return false;

            if(only)
                curShellGraph.DumpInfo.ExcludeNodeType(nodeType);
            else
                foreach(NodeType subType in nodeType.SubOrSameTypes)
                    curShellGraph.DumpInfo.ExcludeNodeType(subType);

            return true;
        }

        public bool AddDumpExcludeEdgeType(EdgeType edgeType, bool only)
        {
            if(edgeType == null) return false;

            if(only)
                curShellGraph.DumpInfo.ExcludeEdgeType(edgeType);
            else
                foreach(EdgeType subType in edgeType.SubOrSameTypes)
                    curShellGraph.DumpInfo.ExcludeEdgeType(subType);

            return true;
        }

        public bool AddDumpGroupNodesBy(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType adjNodeType, bool exactAdjNodeType, GroupMode groupMode)
        {
            if(nodeType == null || edgeType == null || adjNodeType == null) return false;

            curShellGraph.DumpInfo.AddOrExtendGroupNodeType(nodeType, exactNodeType, edgeType, exactEdgeType,
                adjNodeType, exactAdjNodeType, groupMode);

            return true;
        }

        public bool AddDumpInfoTag(GrGenType type, String attrName, bool only, bool isshort)
        {
            if(type == null) return false;

            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                errOut.WriteLine("Type \"" + type.Name + "\" has no attribute \"" + attrName + "\"");
                return false;
            }

            InfoTag infoTag = new InfoTag(attrType, isshort);
            if(only)
                curShellGraph.DumpInfo.AddTypeInfoTag(type, infoTag);
            else
                foreach(GrGenType subtype in type.SubOrSameTypes)
                    curShellGraph.DumpInfo.AddTypeInfoTag(subtype, infoTag);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();

            return true;
        }

        public void DumpReset()
        {
            if(!GraphExists()) return;

            curShellGraph.DumpInfo.Reset();

            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }
        #endregion "dump" commands

        #region "setmap" commands

        public object GetSetMap(IGraphElement elem, String attrName)
        {
            if(elem == null) return null;
            AttributeType attrType = GetElementAttributeType(elem, attrName);
            if(attrType == null) return null;
            return elem.GetAttribute(attrName);
        }

        public void SetAdd(IGraphElement elem, String attrName, object keyObj)
        {
            object set = GetSetMap(elem, attrName);
            if(set == null)
                return;

            Type keyType, valueType;
            IDictionary dict = DictionaryHelper.GetDictionaryTypes(set, out keyType, out valueType);
            if(dict == null) {
                errOut.WriteLine(curShellGraph.Graph.GetElementName(elem)+"."+attrName + " is not a set.");
                return;
            } if(keyType != keyObj.GetType()) {
                errOut.WriteLine("Set type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                return;
            } if(valueType != typeof(SetValueType)) {
                errOut.WriteLine("Not a set.");
                return;
            }
            
            dict[keyObj] = null;
        }

        public void MapAdd(IGraphElement elem, String attrName, object keyObj, object valueObj)
        {
            object map = GetSetMap(elem, attrName);
            if(map == null)
                return;

            Type keyType, valueType;
            IDictionary dict = DictionaryHelper.GetDictionaryTypes(map, out keyType, out valueType);
            if(dict == null) {
                errOut.WriteLine(curShellGraph.Graph.GetElementName(elem) + "." + attrName + " is not a map.");
                return;
            } if(keyType != keyObj.GetType()) {
                errOut.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                return;
            } if(valueType != valueObj.GetType()) {
                errOut.WriteLine("Value type must be " + valueType + ", but is " + valueObj.GetType() + ".");
                return;
            }

            dict[keyObj] = valueObj;
        }

        public void SetMapRemove(IGraphElement elem, String attrName, object keyObj)
        {
            object setOrMap = GetSetMap(elem, attrName);
            if(setOrMap == null)
                return;

            Type keyType, valueType;
            IDictionary dict = DictionaryHelper.GetDictionaryTypes(setOrMap, out keyType, out valueType);
            if (dict == null) {
                errOut.WriteLine(curShellGraph.Graph.GetElementName(elem) + "." + attrName + " is not a set/map.");
                return;
            } if(keyType != keyObj.GetType()) {
                errOut.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                return;
            }

            dict.Remove(keyObj);
        }

        #endregion "setmap" commands

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
                NamedGraph graph = curShellGraph.Graph;
                sw.WriteLine("# Graph \"{0}\" saved by grShell", graph.Name);
                sw.WriteLine();
				if(curShellGraph.BackendFilename != null)
				{
					sw.Write("select backend " + StringToTextToken(curShellGraph.BackendFilename));
					foreach(String param in curShellGraph.BackendParameters)
					{
						sw.Write(" " + StringToTextToken(param));
					}
					sw.WriteLine();
				}

                GRSExport.ExportYouMustCloseStreamWriter(graph, sw, true);

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeColor in curShellGraph.DumpInfo.NodeTypeColors)
                    sw.WriteLine("dump set node only {0} color {1}", nodeTypeColor.Key.Name, nodeTypeColor.Value);

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeBorderColor in curShellGraph.DumpInfo.NodeTypeBorderColors)
                    sw.WriteLine("dump set node only {0} bordercolor {1}", nodeTypeBorderColor.Key.Name, nodeTypeBorderColor.Value);

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeTextColor in curShellGraph.DumpInfo.NodeTypeTextColors)
                    sw.WriteLine("dump set node only {0} textcolor {1}", nodeTypeTextColor.Key.Name, nodeTypeTextColor.Value);

                foreach(KeyValuePair<NodeType, GrNodeShape> nodeTypeShape in curShellGraph.DumpInfo.NodeTypeShapes)
                    sw.WriteLine("dump set node only {0} shape {1}", nodeTypeShape.Key.Name, nodeTypeShape.Value);

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeColor in curShellGraph.DumpInfo.EdgeTypeColors)
                    sw.WriteLine("dump set edge only {0} color {1}", edgeTypeColor.Key.Name, edgeTypeColor.Value);

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeTextColor in curShellGraph.DumpInfo.EdgeTypeTextColors)
                    sw.WriteLine("dump set edge only {0} textcolor {1}", edgeTypeTextColor.Key.Name, edgeTypeTextColor.Value);

                if((curShellGraph.VcgFlags & VCGFlags.EdgeLabels) == 0)
                    sw.WriteLine("dump set edge labels off");

                foreach(NodeType excludedNodeType in curShellGraph.DumpInfo.ExcludedNodeTypes)
                    sw.WriteLine("dump add node only " + excludedNodeType.Name + " exclude");

                foreach(EdgeType excludedEdgeType in curShellGraph.DumpInfo.ExcludedEdgeTypes)
                    sw.WriteLine("dump add edge only " + excludedEdgeType.Name + " exclude");

                foreach(GroupNodeType groupNodeType in curShellGraph.DumpInfo.GroupNodeTypes)
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
                            sw.WriteLine("dump add node only " + groupNodeType.NodeType.Name
                                + " group by " + ((nkvp.Value & GroupMode.Hidden) != 0 ? "hidden " : "") + groupModeStr
                                + " only " + ekvp.Key.Name + " with only " + nkvp.Key.Name);
                        }
                    }
                }

                foreach(KeyValuePair<GrGenType, List<InfoTag>> infoTagPair in curShellGraph.DumpInfo.InfoTags)
                {
                    String kind;
                    if(infoTagPair.Key.IsNodeType) kind = "node";
                    else kind = "edge";

                    foreach(InfoTag infoTag in infoTagPair.Value)
                    {
                        sw.WriteLine("dump add " + kind + " only " + infoTagPair.Key.Name
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
                Porter.Export(curShellGraph.Graph, filenameParameters);
            }
            catch(Exception e)
            {
                errOut.WriteLine("Unable to export graph: " + e.Message);
                return false;
            }
            debugOut.WriteLine("Graph \"" + curShellGraph.Graph.Name + "\" exported.");
            return true;
        }

        public bool Import(List<String> filenameParameters)
        {
            if(filenameParameters[0]=="add")
            {
                filenameParameters.RemoveAt(0);
                return ImportDUnion(filenameParameters);
            }

            if(!BackendExists()) return false;

            IGraph graph;
            try
            {
                int startTime = Environment.TickCount;
                graph = Porter.Import(curGraphBackend, filenameParameters);
                debugOut.WriteLine("import done after: " + (Environment.TickCount - startTime) + " ms");
                debugOut.WriteLine("graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                startTime = Environment.TickCount;

                if(graph is NamedGraph) // grs import returns already named graph
                    curShellGraph = new ShellGraph((NamedGraph)graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");
                else // constructor building named graph
                    curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");
                NamedGraph importedNamedGraph = (NamedGraph)curShellGraph.Graph;
                LGSPGraph wrappedGraph = (LGSPGraph)importedNamedGraph.WrappedGraph;
                wrappedGraph.NamedGraph = curShellGraph.Graph; // set the named graph property of the lgsp graph to get the nameof operator working
                debugOut.WriteLine("shell import done after: " + (Environment.TickCount - startTime) + " ms");
                debugOut.WriteLine("shell graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                curShellGraph.Actions = graph.Actions;
                graphs.Add(curShellGraph);
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

        public bool ImportDUnion(List<string> filenameParameters)
        {
            if (!BackendExists()) return false;

            bool withVariables = false;
            if (filenameParameters[filenameParameters.Count-1]=="withvariables")
            {
                withVariables = true;
                filenameParameters.RemoveAt(filenameParameters.Count-1);
            }

            IGraph graph;
            try
            {
                graph = Porter.Import(curGraphBackend, filenameParameters);
            }
            catch (Exception e)
            {
                errOut.WriteLine("Unable to import graph for union: " + e.Message);
                return false;
            }

            // convenience
            INodeModel node_model = this.CurrentGraph.Model.NodeModel;
            IEdgeModel edge_model = this.CurrentGraph.Model.EdgeModel;
            NamedGraph namedGraph = graph as NamedGraph;

            Dictionary<INode, INode> oldToNewNodeMap = new Dictionary<INode, INode>();

            foreach (INode node in graph.Nodes)
            {
                NodeType typ = node_model.GetType(node.Type.Name);
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

                if (withVariables)
                {
                    LinkedList<Variable> variables = graph.GetElementVariables(node);
                    if (variables != null)
                    {
                        foreach (Variable var in variables)
                        {
                            CurrentGraph.SetVariableValue(var.Name, var.Value);
                        }
                    }
                }
            }

            foreach (IEdge edge in graph.Edges)
            {
                EdgeType typ = edge_model.GetType(edge.Type.Name);
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

                if (withVariables)
                {
                    LinkedList<Variable> variables = graph.GetElementVariables(edge);
                    if (variables != null)
                    {
                        foreach (Variable var in variables)
                        {
                            CurrentGraph.SetVariableValue(var.Name, var.Value);
                        }
                    }
                }
            }

            debugOut.WriteLine("Graph \"" + graph.Name + "\" imported and added to current graph \"" + CurrentGraph.Name + "\"");
            return true;
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
                debugOut.Write("\"{0}\"", curShellGraph.Graph.GetElementName(elem));
            }
            return first;
        }

        public bool Validate(bool strict, bool onlySpecified)
        {
            if(!GraphExists()) return false;

            ValidationMode mode = ValidationMode.OnlyMultiplicitiesOfMatchingTypes;
            if(strict) mode = onlySpecified ? ValidationMode.StrictOnlySpecified : ValidationMode.Strict;
            List<ConnectionAssertionError> errors;
            bool valid = curShellGraph.Graph.Validate(mode, out errors);
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
                        edge.Source.Type.Name, curShellGraph.Graph.GetElementName(edge.Source),
                        edge.Type.Name, curShellGraph.Graph.GetElementName(edge),
                        edge.Target.Type.Name, curShellGraph.Graph.GetElementName(edge.Target),
                        edge.Type.Directedness==Directedness.Directed ? "-->" : "--");
                    break;
                }
                case CAEType.NodeTooFewSources:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} \"{1}\" [{2}<{3}] -- {4} ", valInfo.SourceType.Name,
                        curShellGraph.Graph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceLower, valInfo.EdgeType.Name);
                    bool first = DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        errOut.WriteLine(" -- {0}", valInfo.TargetType.Name);
                    } else {
                        errOut.WriteLine(" --> {0}", valInfo.TargetType.Name);
                    }
                    break;
                }
                case CAEType.NodeTooManySources:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} \"{1}\" [{2}>{3}] -- {4} ", valInfo.SourceType.Name,
                        curShellGraph.Graph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceUpper, valInfo.EdgeType.Name);
                    bool first = DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        errOut.WriteLine(" -- {0}", valInfo.TargetType.Name);
                    } else {
                        errOut.WriteLine(" --> {0}", valInfo.TargetType.Name);
                    }
                    break;
                }
                case CAEType.NodeTooFewTargets:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} -- {1} ", valInfo.SourceType.Name,
                             valInfo.EdgeType.Name);
                    bool first = DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        errOut.WriteLine(" -- {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.Name,
                            curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    } else {
                        errOut.WriteLine(" --> {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.Name,
                            curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    }
                    break;
                }
                case CAEType.NodeTooManyTargets:
                {
                    INode node = (INode)error.Elem;
                    errOut.Write("  CAE: {0} -- {1} ", valInfo.SourceType.Name,
                        valInfo.EdgeType.Name);
                    bool first = DumpElems(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if (valInfo.EdgeType.Directedness!=Directedness.Directed) {
                        DumpElems(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        errOut.WriteLine(" -- {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.Name,
                            curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                    } else {
                        errOut.WriteLine(" --> {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.Name,
                            curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
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

            bool valid = curShellGraph.Graph.ValidateWithSequence(seq);
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

            debugOut.WriteLine("{0} type {1} is a node: {2}", type1.Name, type2.Name,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void EdgeTypeIsA(IEdge edge1, IEdge edge2)
        {
            if(edge1 == null || edge2 == null) return;

            EdgeType type1 = edge1.Type;
            EdgeType type2 = edge2.Type;

            debugOut.WriteLine("{0} type {1} is an edge: {2}", type1.Name, type2.Name,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void CustomGraph(List<String> parameterList)
        {
            if(!GraphExists()) return;

            String[] parameters = parameterList.ToArray();
            try
            {
                curShellGraph.Graph.Custom(parameters);
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
                curShellGraph.Actions.Custom(parameters);
            }
            catch(ArgumentException e)
            {
                errOut.WriteLine(e.Message);
            }
        }
    }
}
