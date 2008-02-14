//#define MATCHREWRITEDETAIL

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using de.unika.ipd.grGen.libGr;
using ASTdapter;
using System.Threading;
using grIO;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.grShell
{
    struct Param        // KeyValuePair<String, String> waere natuerlich schoener... CSharpCC kann aber kein .NET 2.0 ...
    {
        public String Key;
        public String Value;

        public Param(String key, String value)
        {
            Key = key;
            Value = value;
        }
    }

    class ElementDef
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

    class ShellGraph
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
            DumpInfo = new DumpInfo(Graph.GetElementName);
            BackendFilename = backendFilename;
            BackendParameters = backendParameters;
            ModelFilename = modelFilename;
        }
    }

    class GrShellImpl
    {
        IBackend curGraphBackend = new LGSPBackend();
        String backendFilename = null;
        String[] backendParameters = null;

        List<ShellGraph> graphs = new List<ShellGraph>();
        ShellGraph curShellGraph = null;

        bool cancelSequence = false;
        Debugger debugger = null;
        bool pendingDebugEnable = false;
        String debugLayout = "Orthogonal";

        IWorkaround workaround = WorkaroundManager.GetWorkaround();
        public LinkedList<GrShellTokenManager> TokenSourceStack = new LinkedList<GrShellTokenManager>();

        public GrShellImpl()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
        }

        public bool OperationCancelled { get { return cancelSequence; } }
        public IWorkaround Workaround { get { return workaround; } }
        public bool InDebugMode { get { return debugger != null; } }

        public static void PrintVersion()
        {
            Console.WriteLine("GrShell version v1.3 ($Revision$)");
        }

        private bool BackendExists()
        {                                       
            if(curGraphBackend == null)
            {
                Console.WriteLine("No backend. Select a backend, first.");
                return false;
            }
            return true;
        }

        private bool GraphExists()
        {
            if(curShellGraph == null || curShellGraph.Graph == null)
            {
                Console.WriteLine("No graph. Make a new graph, first.");
                return false;
            }
            return true;
        }

        private bool ActionsExists()
        {
            if(curShellGraph == null || curShellGraph.Actions == null)
            {
                Console.WriteLine("No actions. Select an actions object, first.");
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

        public IGraphElement GetElemByVar(String varName)
        {
            if(!GraphExists()) return null;
            IGraphElement elem = curShellGraph.Graph.GetVariableValue(varName);
            if(elem == null)
            {
                Console.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            return elem;
        }

        public IGraphElement GetElemByVarOrNull(String varName)
        {
            if(!GraphExists()) return null;
            return curShellGraph.Graph.GetVariableValue(varName);
        }

        public IGraphElement GetElemByName(String elemName)
        {
            if(!GraphExists()) return null;
            IGraphElement elem = curShellGraph.Graph.GetGraphElement(elemName);
            if(elem == null)
            {
                Console.WriteLine("Unknown graph element: \"{0}\"", elemName);
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
                Console.WriteLine("\"{0}\" is not a node!", varName);
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
                Console.WriteLine("\"{0}\" is not a node!", elemName);
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
                Console.WriteLine("\"{0}\" is not an edge!", varName);
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
                Console.WriteLine("\"{0}\" is not an edge!", elemName);
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
                Console.WriteLine("Unknown node type: \"{0}\"", typeName);
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
                Console.WriteLine("Unknown edge type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public ShellGraph GetShellGraph(String graphName)
        {
            foreach(ShellGraph shellGraph in graphs)
                if(shellGraph.Graph.Name == graphName)
                    return shellGraph;

            Console.WriteLine("Unknown graph: \"{0}\"", graphName);
            return null;
        }

        public ShellGraph GetShellGraph(int index)
        {
            if((uint) index >= (uint) graphs.Count)
                Console.WriteLine("Graph index out of bounds!");

            return graphs[index];
        }

        public void HandleSequenceParserRuleException(SequenceParserRuleException ex)
        {
            IAction action = ex.Action;
            if(action == null)
            {
                Console.WriteLine("Unknown rule: \"{0}\"", ex.RuleName);
                return;
            }
            if(action.RulePattern.Inputs.Length != ex.NumGivenInputs && action.RulePattern.Outputs.Length != ex.NumGivenOutputs)
                Console.WriteLine("Wrong number of parameters and return values for action \"{0}\"!", ex.RuleName);
            else if(action.RulePattern.Inputs.Length != ex.NumGivenInputs)
                Console.WriteLine("Wrong number of parameters for action \"{0}\"!", ex.RuleName);
            else
                Console.WriteLine("Wrong number of return values for action \"{0}\"!", ex.RuleName);
            Console.Write("Prototype: {0}", ex.RuleName);
            if(action.RulePattern.Inputs.Length != 0)
            {
                Console.Write("(");
                bool first = true;
                foreach(GrGenType type in action.RulePattern.Inputs)
                {
                    Console.Write("{0}{1}", first ? "" : ", ", type.Name);
                    first = false;
                }
                Console.Write(")");
            }
            if(action.RulePattern.Outputs.Length != 0)
            {
                Console.Write(" : (");
                bool first = true;
                foreach(GrGenType type in action.RulePattern.Outputs)
                {
                    Console.Write("{0}{1}", first ? "" : ", ", type.Name);
                    first = false;
                }
                Console.Write(")");
            }
            Console.WriteLine();
        }

        public IAction GetAction(String actionName, int numParams, int numReturns, bool retSpecified)
        {
            if(!ActionsExists()) return null;
            IAction action = curShellGraph.Actions.GetAction(actionName);
            if(action == null)
            {
                Console.WriteLine("Unknown action: \"{0}\"", actionName);
                return null;
            }
            if(action.RulePattern.Inputs.Length != numParams || action.RulePattern.Outputs.Length != numReturns && retSpecified)
            {
                if(action.RulePattern.Inputs.Length != numParams && action.RulePattern.Outputs.Length != numReturns)
                    Console.WriteLine("Wrong number of parameters and return values for action \"{0}\"!", actionName);
                else if(action.RulePattern.Inputs.Length != numParams)
                    Console.WriteLine("Wrong number of parameters for action \"{0}\"!", actionName);
                else
                    Console.WriteLine("Wrong number of return values for action \"{0}\"!", actionName);
                Console.Write("Prototype: {0}", actionName);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    Console.Write("(");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Inputs)
                    {
                        Console.Write("{0}{1}", first ? "" : ",", type.Name);
                    }
                    Console.Write(")");
                }
                if(action.RulePattern.Outputs.Length != 0)
                {
                    Console.Write(" : (");
                    bool first = true;
                    foreach(GrGenType type in action.RulePattern.Outputs)
                    {
                        Console.Write("{0}{1}", first ? "" : ",", type.Name);
                    }
                    Console.Write(")");
                }
                Console.WriteLine();
                return null;
            }
            return action;
        }

        public void Help()
        {
            Console.WriteLine("TODO: Add help text");
        }

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
                Console.WriteLine("Unable to execute file \"" + filename + "\": " + e.Message);
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
                Console.WriteLine("Error during include of \"" + filename + "\": " + e.Message);
                return false;
            }
            return true;
        }

        public void Quit()
        {
            if(InDebugMode)
                SetDebugMode(false);

            Console.WriteLine("Bye!\n");
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
                            Console.WriteLine("The given backend contains more than one IBackend implementation!");
                            return false;
                        }
                        backendType = type;
                    }
                }
                if(backendType == null)
                {
                    Console.WriteLine("The given backend doesn't contain an IBackend implementation!");
                    return false;
                }
                curGraphBackend = (IBackend) Activator.CreateInstance(backendType);
                backendFilename = assemblyName;
                backendParameters = (String[]) parameters.ToArray(typeof(String));
                Console.WriteLine("Backend selected successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to load backend: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public bool NewGraph(String modelFilename, String graphName)
        {
            if(!BackendExists()) return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                modelFilename = modelFilename.Replace('\\', Path.DirectorySeparatorChar);

            if(modelFilename.EndsWith(".cs") || modelFilename.EndsWith(".dll"))
            {
                IGraph graph;
                try
                {
                    graph = curGraphBackend.CreateGraph(modelFilename, graphName, backendParameters);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to create graph with model \"{0}\":\n{1}", modelFilename, e.Message);
                    return false;
                }
                try
                {
                    curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, modelFilename);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Unable to create new graph: {0}", ex.Message);
                    return false;
                }
                graphs.Add(curShellGraph);
                Console.WriteLine("New graph \"{0}\" of model \"{1}\" created.", graphName, graph.Model.Name);
            }
            else
            {
                if(!File.Exists(modelFilename))
                {
                    String filename = modelFilename + ".grg";
                    if(!File.Exists(filename))
                    {
                        Console.WriteLine("The rule specification file \"" + modelFilename + "\" or \"" + filename + "\" does not exist!");
                        return false;
                    }
                    modelFilename = filename;
                }

                IGraph graph;
                BaseActions actions;
                try
                {
                    curGraphBackend.CreateFromSpec(modelFilename, graphName, out graph, out actions);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}", modelFilename, e.Message);
                    return false;
                }
                try
                {
                    curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, modelFilename);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Unable to create new graph: {0}", ex.Message);
                    return false;
                }
                curShellGraph.Actions = actions;
                graphs.Add(curShellGraph);
                Console.WriteLine("New graph \"{0}\" created from spec file \"{1}\".", graphName, modelFilename);
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
                Console.WriteLine("Unable to open graph with model \"{0}\":\n{1}", modelFilename, e.Message);
                return false;
            }
            try
            {
                curShellGraph = new ShellGraph(graph, backendFilename, backendParameters, modelFilename);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to open graph: {0}", ex.Message);
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
                curShellGraph.Actions = curShellGraph.Graph.LoadActions(actionFilename, curShellGraph.DumpInfo);
                curShellGraph.ActionsFilename = actionFilename;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to load the actions from \"{0}\":\n{1}", actionFilename, ex.Message);
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
                Console.WriteLine("Unable to load parser from \"" + parserAssemblyName + "\":\n" + ex.Message);
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
                    Console.WriteLine("Unknown node type: {0}", elemDef.TypeName);
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
                Console.WriteLine("Unable to create new node: {0}", e.Message);
                return null;
            }
            if(node == null)
            {
                Console.WriteLine("Creation of new node failed.");
                return null;
            }


            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(node, elemDef.Attributes))
                {
                    Console.WriteLine("Unexpected failure: Unable to set node attributes inspite of check?!");
                    curShellGraph.Graph.Remove(node);
                    return null;
                }
            }

            if(elemDef.ElemName == null)
                Console.WriteLine("New node \"{0}\" of type \"{1}\" has been created.",
                    curShellGraph.Graph.GetElementName(node), node.Type.Name);
            else
                Console.WriteLine("New node \"{0}\" of type \"{1}\" has been created.", elemDef.ElemName, node.Type.Name);

            return node;
        }

        public IEdge NewEdge(ElementDef elemDef, INode node1, INode node2)
        {
            if(node1 == null || node2 == null) return null;

            EdgeType edgeType;
            if(elemDef.TypeName != null)
            {
                edgeType = curShellGraph.Graph.Model.EdgeModel.GetType(elemDef.TypeName);
                if(edgeType == null)
                {
                    Console.WriteLine("Unknown edge type: {0}", elemDef.TypeName);
                    return null;
                }
            }
            else edgeType = curShellGraph.Graph.Model.EdgeModel.RootType;

            if(elemDef.Attributes != null && !CheckAttributes(edgeType, elemDef.Attributes)) return null;

            IEdge edge;
            try
            {
                edge = curShellGraph.Graph.AddEdge(edgeType, node1, node2, elemDef.VarName, elemDef.ElemName);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Unable to create new edge: {0}", e.Message);
                return null;
            }
            if(edge == null)
            {
                Console.WriteLine("Creation of new edge failed.");
                return null;
            }

            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(edge, elemDef.Attributes))
                {
                    Console.WriteLine("Unexpected failure: Unable to set edge attributes inspite of check?!");
                    curShellGraph.Graph.Remove(edge);
                    return null;
                }
            }

            if(elemDef.ElemName == null)
                Console.WriteLine("New edge \"{0}\" of type \"{1}\" has been created.",
                    curShellGraph.Graph.GetElementName(edge), edge.Type.Name);
            else
                Console.WriteLine("New edge \"{0}\" of type \"{1}\" has been created.", elemDef.ElemName, edge.Type.Name);
            return edge;
        }

        private bool CheckAttributes(GrGenType type, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = type.GetAttributeType(par.Key);
                if(attrType == null)
                {
                    Console.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.Name, par.Key);
                    return false;
                }
                switch(attrType.Kind)
                {
                    case AttributeKind.BooleanAttr:
                        if(par.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                            break;
                        else if(par.Value.Equals("false", StringComparison.OrdinalIgnoreCase))
                            break;
                        Console.WriteLine("Attribute \"{0}\" must be either \"true\" or \"false\"!", par.Key);
                        return false;
                    case AttributeKind.EnumAttr:
                    {
                        int val;
                        if(Int32.TryParse(par.Value, out val)) break;
                        bool ok = false;
                        foreach(EnumMember member in attrType.EnumType.Members)
                        {
                            if(par.Value == member.Name)
                            {
                                ok = true;
                                break;
                            }
                        }
                        if(ok) break;

                        Console.WriteLine("Attribute \"{0}\" must be one of the following values:", par.Key);
                        foreach(EnumMember member in attrType.EnumType.Members)
                            Console.WriteLine(" - {0} = {1}", member.Name, member.Value);

                        return false;
                    }
                    case AttributeKind.IntegerAttr:
                    {
                        int val;
                        if(Int32.TryParse(par.Value, out val)) break;

                        Console.WriteLine("Attribute \"{0}\" must be an integer!", par.Key);
                        return false;
                    }
                    case AttributeKind.StringAttr:
                        break;
                    case AttributeKind.FloatAttr:
                    {
                        float val;
                        if(Single.TryParse(par.Value, out val)) break;

                        Console.WriteLine("Attribute \"{0}\" must be a floating point number!", par.Key);
                        return false;
                    }
                    case AttributeKind.DoubleAttr:
                    {
                        double val;
                        if(Double.TryParse(par.Value, out val)) break;

                        Console.WriteLine("Attribute \"{0}\" must be a floating point number!", par.Key);
                        return false;
                    }
                    case AttributeKind.ObjectAttr:
                    {
                        Console.WriteLine("Attribute \"" + par.Key + "\" is an object type attribute!\n"
                            + "It is not possible to assign a value to an object type attribute!");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool SetAttributes(IGraphElement elem, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = elem.Type.GetAttributeType(par.Key);
                if(attrType == null)
                {
                    Console.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", elem.Type.Name, par.Key);
                    return false;
                }
                object value = null;
                switch(attrType.Kind)
                {
                    case AttributeKind.BooleanAttr:
                        if(par.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
                            value = true;
                        else if(par.Value.Equals("false", StringComparison.OrdinalIgnoreCase))
                            value = false;
                        else
                        {
                            Console.WriteLine("Attribute {0} must be either \"true\" or \"false\"!", par.Key);
                            return false;
                        }
                        break;
                    case AttributeKind.EnumAttr:
                        {
                            int val;
                            if(Int32.TryParse(par.Value, out val))
                            {
                                value = val;
                            }
                            else
                            {
                                foreach(EnumMember member in attrType.EnumType.Members)
                                {
                                    if(par.Value == member.Name)
                                    {
                                        value = member.Value;
                                        break;
                                    }
                                }
                                if(value == null)
                                {
                                    Console.WriteLine("Attribute {0} must be one of the following values:", par.Key);
                                    foreach(EnumMember member in attrType.EnumType.Members)
                                        Console.WriteLine(" - {0} = {1}", member.Name, member.Value);

                                    return false;
                                }
                            }
                            break;
                        }
                    case AttributeKind.IntegerAttr:
                        {
                            int val;
                            if(!Int32.TryParse(par.Value, out val))
                            {
                                Console.WriteLine("Attribute {0} must be an integer!", par.Key);
                                return false;
                            }
                            value = val;
                            break;
                        }
                    case AttributeKind.StringAttr:
                        value = par.Value;
                        break;
                    case AttributeKind.FloatAttr:
                    {
                        float val;
                        if(!Single.TryParse(par.Value, out val))
                        {
                            Console.WriteLine("Attribute \"{0}\" must be a floating point number!", par.Key);
                            return false;
                        }
                        value = val;
                        break;
                    }
                    case AttributeKind.DoubleAttr:
                    {
                        double val;
                        if(!Double.TryParse(par.Value, out val))
                        {
                            Console.WriteLine("Attribute \"{0}\" must be a floating point number!", par.Key);
                            return false;
                        }
                        value = val;
                        break;
                    }
                    case AttributeKind.ObjectAttr:
                    {
                        Console.WriteLine("Attribute \"" + par.Key + "\" is an object type attribute!\n"
                            + "It is not possible to assign a value to an object type attribute!");
                        return false;
                    }
                }
                if(elem is INode)
                    curShellGraph.Graph.ChangingNodeAttribute((INode) elem, attrType, elem.GetAttribute(par.Key), value);
                else
                    curShellGraph.Graph.ChangingEdgeAttribute((IEdge) elem, attrType, elem.GetAttribute(par.Key), value);
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
                Console.WriteLine("Unable to remove node: " + e.Message);
                return false;
            }
            return false;
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

            Console.WriteLine("{0,-20} {1}", "name", "type");
            foreach(IGraphElement elem in elements)
            {
                Console.WriteLine("{0,-20} {1}", curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
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
                Console.WriteLine("There are no nodes " + (only ? "compatible to" : "of") + " type \"" + nodeType.Name + "\"!");
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
                Console.WriteLine("There are no edges of " + (only ? "compatible to" : "of") + " type \"" + edgeType.Name + "\"!");
        }

        public void ShowNumNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists()) return;
                nodeType = curShellGraph.Graph.Model.NodeModel.RootType;
            }
            if(only)
                Console.WriteLine("Number of nodes of type \"" + nodeType.Name + "\": "
                    + curShellGraph.Graph.GetNumExactNodes(nodeType));
            else
                Console.WriteLine("Number of nodes compatible to type \"" + nodeType.Name + "\": "
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
                Console.WriteLine("Number of edges of type \"" + edgeType.Name + "\": "
                    + curShellGraph.Graph.GetNumExactEdges(edgeType));
            else
                Console.WriteLine("Number of edges compatible to type \"" + edgeType.Name + "\": "
                    + curShellGraph.Graph.GetNumCompatibleEdges(edgeType));
        }

        public void ShowNodeTypes()
        {
            if(!GraphExists()) return;

            if(curShellGraph.Graph.Model.NodeModel.Types.Length == 0)
            {
                Console.WriteLine("This model has no node types!");
            }
            else
            {
                Console.WriteLine("Node types:");
                foreach(NodeType type in curShellGraph.Graph.Model.NodeModel.Types)
                    Console.WriteLine(" - \"{0}\"", type.Name);
            }
        }

        public void ShowEdgeTypes()
        {
            if(!GraphExists()) return;

            if(curShellGraph.Graph.Model.EdgeModel.Types.Length == 0)
            {
                Console.WriteLine("This model has no edge types!");
            }
            else
            {
                Console.WriteLine("Edge types:");
                foreach(EdgeType type in curShellGraph.Graph.Model.EdgeModel.Types)
                    Console.WriteLine(" - \"{0}\"", type.Name);
            }
        }

        public void ShowSuperTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                Console.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.Name + "\" has no super types!");
            }
            else
            {
                Console.WriteLine("Super types of " + (isNode ? "node" : "edge") + " type \"" + elemType.Name + "\":");
                foreach(GrGenType type in elemType.SuperTypes)
                    Console.WriteLine(" - \"" + type.Name + "\"");
            }
        }

        public void ShowSubTypes(GrGenType elemType, bool isNode)
        {
            if(elemType == null) return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
            {
                Console.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.Name + "\" has no super types!");
            }
            else
            {
                Console.WriteLine("Sub types of " + (isNode ? "node" : "edge") + " type \"{0}\":", elemType.Name);
                foreach(GrGenType type in elemType.SubTypes)
                    Console.WriteLine(" - \"{0}\"", type.Name);
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
                Console.WriteLine(e.Message);
            }
            finally
            {
                File.Delete(param.GraphFilename);
            }
        }

        public void ShowGraphWith(String programName, String arguments)
        {
            if(!GraphExists()) return;

            String filename;
            int id = 0;
            do
            {
                filename = "tmpgraph" + id + ".vcg";
                id++;
            }
            while(File.Exists(filename));

            VCGDumper dump = new VCGDumper(filename, curShellGraph.VcgFlags);
            curShellGraph.Graph.Dump(dump, curShellGraph.DumpInfo);
            dump.FinishDump();

            Thread t = new Thread(new ParameterizedThreadStart(ShowGraphThread));
            t.Start(new ShowGraphParam(programName, arguments, filename));
        }

        public void ShowGraphs()
        {
            if(graphs.Count == 0)
            {
                Console.WriteLine("No graphs available.");
                return;
            }
            Console.WriteLine("Graphs:");
            for(int i = 0; i < graphs.Count; i++)
                Console.WriteLine(" - \"" + graphs[i].Graph.Name + "\" (" + i + ")");
        }

        public void ShowActions()
        {
            if(!ActionsExists()) return;

            if(!curShellGraph.Actions.Actions.GetEnumerator().MoveNext())
            {
                Console.WriteLine("No actions available.");
                return;
            }

            Console.WriteLine("Actions:");
            foreach(IAction action in curShellGraph.Actions.Actions)
            {
                Console.Write(" - " + action.Name);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    Console.Write("(");
                    bool isFirst = true;
                    foreach(GrGenType inType in action.RulePattern.Inputs)
                    {
                        if(!isFirst) Console.Write(", ");
                        else isFirst = false;
                        Console.Write(inType.Name);
                    }
                    Console.Write(")");
                }

                if(action.RulePattern.Outputs.Length != 0)
                {
                    Console.Write(" : (");
                    bool isFirst = true;
                    foreach(GrGenType outType in action.RulePattern.Outputs)
                    {
                        if(!isFirst) Console.Write(", ");
                        else isFirst = false;
                        Console.Write(outType.Name);
                    }
                    Console.Write(")");
                }
                Console.WriteLine();
            }
        }

        public void ShowBackend()
        {
            if(!BackendExists()) return;

            Console.WriteLine("Backend name: {0}", curGraphBackend.Name);
            if(!curGraphBackend.ArgumentNames.GetEnumerator().MoveNext())
            {
                Console.WriteLine("This backend has no arguments.");
            }

            Console.WriteLine("Arguments:");
            foreach(String str in curGraphBackend.ArgumentNames)
                Console.WriteLine(" - {0}", str);
        }

        /// <summary>
        /// Displays the attributes from the given type or all types, if typeName is null.
        /// If showAll is false, inherited attributes are not shown (only applies to a given type)
        /// </summary>
        /// <typeparam name="T">An IType interface</typeparam>
        /// <param name="showOnly">If true, only non inherited attributes are shown</param>
        /// <param name="typeName">Type which attributes are to be shown or null to show all attributes of all types</param>
        /// <param name="model">The model to take the attributes from</param>
        private void ShowAvailableAttributes(IEnumerable<AttributeType> attrTypes, GrGenType onlyType)
        {
            bool first = true;
            foreach(AttributeType attrType in attrTypes)
            {
                if(onlyType != null && attrType.OwnerType != onlyType) continue;

                if(first)
                {
                    Console.WriteLine("{0,-24} type::attribute\n", "kind");
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
                Console.WriteLine("{0,-24} {1}::{2}", kind, attrType.OwnerType.Name, attrType.Name);
            }
            if(first)
                Console.WriteLine("No attribute types found.");
        }

        public void ShowAvailableNodeAttributes(bool showOnly, NodeType nodeType)
        {
            if(nodeType == null)
                ShowAvailableAttributes(curShellGraph.Graph.Model.NodeModel.AttributeTypes, null);
            else
                ShowAvailableAttributes(nodeType.AttributeTypes, showOnly ? nodeType : null);
        }

        public void ShowAvailableEdgeAttributes(bool showOnly, EdgeType edgeType)
        {
            if(edgeType == null)
                ShowAvailableAttributes(curShellGraph.Graph.Model.EdgeModel.AttributeTypes, null);
            else
                ShowAvailableAttributes(edgeType.AttributeTypes, showOnly ? edgeType : null);
        }

        public void ShowElementAttributes(IGraphElement elem)
        {
            if(elem == null) return;
            if(elem.Type.NumAttributes == 0)
            {
                Console.WriteLine("{0} \"{1}\" of type \"{2}\" does not have any attributes!", (elem is INode) ? "Node" : "Edge",
                    curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
                return;
            }
            Console.WriteLine("All attributes for {0} \"{1}\" of type \"{2}\":", (elem is INode) ? "node" : "edge",
                curShellGraph.Graph.GetElementName(elem), elem.Type.Name);
            foreach(AttributeType attrType in elem.Type.AttributeTypes)
                Console.WriteLine(" - {0}::{1} = {2}", attrType.OwnerType.Name,
                    attrType.Name, elem.GetAttribute(attrType.Name));
        }

        public void ShowElementAttribute(IGraphElement elem, String attributeName)
        {
            if(elem == null) return;

            AttributeType attrType = elem.Type.GetAttributeType(attributeName);
            if(attrType == null)
            {
                Console.WriteLine(((elem is INode) ? "Node" : "Edge") + " \"" + curShellGraph.Graph.GetElementName(elem)
                    + "\" does not have an attribute \"" + attributeName + "\"!");
                return;
            }
            Console.WriteLine("The value of attribute \"" + attributeName + "\" is: \"" + elem.GetAttribute(attributeName) + "\".");
        }

        #endregion "show" commands

        public void SetElementAttribute(IGraphElement elem, String attributeName, String attributeValue)
        {
            if(elem == null) return;
            ArrayList attributes = new ArrayList();
            attributes.Add(new Param(attributeName, attributeValue));
            if(!CheckAttributes(elem.Type, attributes)) return;
            SetAttributes(elem, attributes);
        }

        public void SetVariable(String varName, IGraphElement elem)
        {
            if(!GraphExists()) return;
            curShellGraph.Graph.SetVariableValue(varName, elem);
        }

        public bool ParseFile(String filename)
        {
            if(!GraphExists()) return false;
            if(curShellGraph.Parser == null)
            {
                Console.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
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
                Console.WriteLine("Unable to parse file \"" + filename + "\":\n" + ex.Message);
                return false;
            }
            return true;
        }

        public bool ParseString(String str)
        {
            if(!GraphExists()) return false;
            if(curShellGraph.Parser == null)
            {
                Console.WriteLine("Please use \"select parser <parserAssembly> <mainMethod>\" first!");
                return false;
            }
            try
            {
                ASTdapter.ASTdapter astDapter = new ASTdapter.ASTdapter(curShellGraph.Parser);
                astDapter.Load(str, curShellGraph.Graph);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to parse string \"" + str + "\":\n" + ex.Message);
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
                    Console.WriteLine(" - {0,-18}: Matches = {1,6}  Match = {2,6} ms  Rewrite = {3,6} ms",
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

            if(InDebugMode) debugger.InitNewRewriteSequence(seq, debug);

            if(!InDebugMode && ContainsSpecial(seq))
            {
                curShellGraph.Actions.OnEntereringSequence += new EnterSequenceHandler(DumpOnEntereringSequence);
                curShellGraph.Actions.OnExitingSequence += new ExitSequenceHandler(DumpOnExitingSequence);
                installedDumpHandlers = true;
            }
            else
            {
                curShellGraph.Actions.OnEntereringSequence += new EnterSequenceHandler(NormalEnteringSequenceHandler);
                curShellGraph.Actions.OnExitingSequence += new ExitSequenceHandler(NormalExitingSequenceHandler);
            }

            curGRS = seq;
            curRule = null;

            Console.WriteLine("Executing Graph Rewrite Sequence... (CTRL+C for abort)");
            cancelSequence = false;
            PerformanceInfo perfInfo = new PerformanceInfo();
            curShellGraph.Actions.PerformanceInfo = perfInfo;
            try
            {
                curShellGraph.Actions.ApplyGraphRewriteSequence(seq);
                Console.WriteLine("Executing Graph Rewrite Sequence done after {0} ms:", perfInfo.TotalTimeMS);
#if DEBUGACTIONS || MATCHREWRITEDETAIL
                Console.WriteLine(" - {0} matches found in {1} ms", perfInfo.MatchesFound, perfInfo.TotalMatchTimeMS);
                Console.WriteLine(" - {0} rewrites performed in {1} ms", perfInfo.RewritesPerformed, perfInfo.TotalRewriteTimeMS);
#if DEBUGACTIONS
                Console.WriteLine("\nDetails:");
                ShowSequenceDetails(seq, perfInfo);
#endif
#else
                Console.WriteLine(" - {0} matches found", perfInfo.MatchesFound);
                Console.WriteLine(" - {0} rewrites performed", perfInfo.RewritesPerformed);
#endif
            }
            catch(OperationCanceledException)
            {
                cancelSequence = true;      // make sure cancelSequence is set to true
                if(curRule == null)
                    Console.WriteLine("Rewrite sequence aborted!");
                else
                {
                    Console.WriteLine("Rewrite sequence aborted after:");
                    Debugger.PrintSequence(curGRS, curRule, Workaround);
                    Console.WriteLine();
                }
            }
            curShellGraph.Actions.PerformanceInfo = null;
            curRule = null;
            curGRS = null;

            if(InDebugMode) debugger.FinishRewriteSequence();
            
            if(installedDumpHandlers)
            {
                curShellGraph.Actions.OnEntereringSequence -= new EnterSequenceHandler(DumpOnEntereringSequence);
                curShellGraph.Actions.OnExitingSequence -= new ExitSequenceHandler(DumpOnExitingSequence);
            }
            else
            {
                curShellGraph.Actions.OnEntereringSequence -= new EnterSequenceHandler(NormalEnteringSequenceHandler);
                curShellGraph.Actions.OnExitingSequence -= new ExitSequenceHandler(NormalExitingSequenceHandler);
            }
        }

        public void WarnDeprecatedGrs(Sequence seq)
        {
            Console.Write(
                "-------------------------------------------------------------------------------\n"
                + "The \"grs\"-command is deprecated and may not be supported in later versions!\n"
                + "An equivalent \"xgrs\"-command is:\n  xgrs ");
            Debugger.PrintSequence(seq, null, Workaround);
            Console.WriteLine("\n-------------------------------------------------------------------------------");
        }

        public void Cancel()
        {
            if(InDebugMode)
                debugger.AbortRewriteSequence();
            throw new OperationCanceledException();                 // abort rewrite sequence
        }

        void NormalExitingSequenceHandler(Sequence seq)
        {
            if(cancelSequence)
                Cancel();
        }

        void NormalEnteringSequenceHandler(Sequence seq)
        {
            SequenceRule ruleSeq = seq as SequenceRule;
            if (ruleSeq != null) curRule = ruleSeq;
        }

        void DumpOnEntereringSequence(Sequence seq)
        {
            SequenceRule ruleSeq = seq as SequenceRule;
            if(ruleSeq != null)
            {
                curRule = ruleSeq;
                if(ruleSeq.Special)
                    curShellGraph.Actions.OnFinishing += new BeforeFinishHandler(DumpOnFinishing);
            }
        }

        void DumpOnExitingSequence(Sequence seq)
        {
            SequenceRule ruleSeq = seq as SequenceRule;
            if(ruleSeq != null && ruleSeq.Special)
                curShellGraph.Actions.OnFinishing -= new BeforeFinishHandler(DumpOnFinishing);

            if(cancelSequence)
                Cancel();
        }

        void DumpOnFinishing(IMatches matches, bool special)
        {
            int i = 1;
            IPatternGraph patternGraph = matches.Producer.RulePattern.PatternGraph;
            Console.WriteLine("Matched " + matches.Producer.Name + " rule:");
            foreach(IMatch match in matches)
            {
                Console.WriteLine(" - " + i++ + ". match:");
                int j = 0;
                foreach(INode node in match.Nodes)
                    Console.WriteLine("   " + patternGraph.Nodes[j++].Name + ": " + curShellGraph.Graph.GetElementName(node));
                j = 0;
                foreach(IEdge edge in match.Edges)
                    Console.WriteLine("   " + patternGraph.Edges[j++].Name + ": " + curShellGraph.Graph.GetElementName(edge));
            }
        }

        void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if(curGRS == null || cancelSequence) return;
            if(curRule == null) Console.WriteLine("Cancelling...");
            else Console.WriteLine("Cancelling: Waiting for \"" + curRule.RuleObj.Action.Name + "\" to finish...");
            e.Cancel = true;        // we handled the cancel event
            cancelSequence = true;
        }

        public void SetDebugMode(bool enable)
        {
            if(enable)
            {
                if(CurrentShellGraph == null)
                {
                    Console.WriteLine("Debug mode will be enabled as soon as a graph has been created!");
                    pendingDebugEnable = true;
                    return;
                }
                if(InDebugMode)
                {
                    Console.WriteLine("You are already in debug mode!");
                    return;
                }

                debugger = new Debugger(this, debugLayout);

                pendingDebugEnable = false;
            }
            else
            {
                if(CurrentShellGraph == null && pendingDebugEnable)
                {
                    Console.WriteLine("Debug mode will not be enabled anymore when a graph has been created.");
                    pendingDebugEnable = false;
                    return;
                }

                if(!InDebugMode)
                {
                    Console.WriteLine("You are not in debug mode!");
                    return;
                }

                debugger.Close();
                debugger = null;
            }
        }

        public void DebugRewriteSequence(Sequence seq)
        {
            bool debugModeActivated;

            if(!InDebugMode)
            {
                SetDebugMode(true);
                debugModeActivated = true;
            }
            else debugModeActivated = false;

            ApplyRewriteSequence(seq, true);

            if(debugModeActivated && InDebugMode)   // enabled debug mode here and didn't loose connection?
            {
                Console.Write("Do you want to leave debug mode (y/n)? ");

                char key;
                while((key = workaround.ReadKey(true).KeyChar) != 'y' && key != 'n') ;
                Console.WriteLine(key);

                if(key == 'y')
                    SetDebugMode(false);
            }
        }

        public void DebugLayout()
        {
            if(!InDebugMode)
            {
                Console.WriteLine("YComp is not active, yet!");
                return;
            }
            debugger.ForceLayout();
        }

        public void SetDebugLayout(String layout)
        {
            if(layout == null || !YCompClient.IsValidLayout(layout))
            {
                if(layout != null)
                    Console.WriteLine("\"" + layout + "\" is not a valid layout name!");
                Console.WriteLine("Available layouts:");
                foreach(String layoutName in YCompClient.AvailableLayouts)
                    Console.WriteLine(" - " + layoutName);
                Console.WriteLine("Current layout: " + debugLayout);
                return;
            }
            if(InDebugMode)
                debugger.SetLayout(layout);
            debugLayout = layout;
        }

        public void GetDebugLayoutOptions()
        {
            if(!InDebugMode)
            {
                Console.WriteLine("Layout options can only be read, when YComp is active!");
                return;
            }
            debugger.GetLayoutOptions();
        }

        public void SetDebugLayoutOption(String optionName, String optionValue)
        {
            if(!InDebugMode)
            {
                Console.WriteLine("Layout options can only be set, when YComp is active!");
                return;
            }
            debugger.SetLayoutOption(optionName, optionValue);
        }

        #region "dump" commands
        public void DumpGraph(String filename)
        {
            if(!GraphExists()) return;

            VCGDumper dump = new VCGDumper(filename, curShellGraph.VcgFlags);
            curShellGraph.Graph.Dump(dump, curShellGraph.DumpInfo);
            dump.FinishDump();
        }

        private GrColor? ParseGrColor(String colorName)
        {
            GrColor color;
            try
            {
                color = (GrColor) Enum.Parse(typeof(GrColor), colorName, true);
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Unknown color: {0}\nAvailable colors are:", colorName);
                foreach(String name in Enum.GetNames(typeof(GrColor)))
                    Console.WriteLine(" - {0}", name);
                return null;
            }
            return color;
        }

        private GrNodeShape? ParseGrNodeShape(String shapeName)
        {
            GrNodeShape shape;
            try
            {
                shape = (GrNodeShape) Enum.Parse(typeof(GrNodeShape), shapeName, true);
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Unknown node shape: {0}\nAvailable node shapes are:", shapeName);
                foreach(String name in Enum.GetNames(typeof(GrNodeShape)))
                    Console.WriteLine(" - {0}", name);
                return null;
            }
            return shape;
        }

        delegate void SetNodeDumpColorProc(NodeType type, GrColor color);

        private void SetDumpColor(NodeType type, String colorName, bool only, SetNodeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                    setDumpColorProc(subType, (GrColor) color);
            }
            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }

        delegate void SetEdgeDumpColorProc(EdgeType type, GrColor color);

        private void SetDumpColor(EdgeType type, String colorName, bool only, SetEdgeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null) return;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                    setDumpColorProc(subType, (GrColor) color);
            }
            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }

        public void SetDumpNodeTypeColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return;
            SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeColor);
        }

        public void SetDumpNodeTypeBorderColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return;
            SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeBorderColor);
        }

        public void SetDumpNodeTypeTextColor(NodeType type, String colorName, bool only)
        {
            if(type == null) return;
            SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetNodeTypeTextColor);
        }

        public void SetDumpNodeTypeShape(NodeType type, String shapeName, bool only)
        {
            if(type == null) return;

            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null) return;

            if(only)
                curShellGraph.DumpInfo.SetNodeTypeShape(type, (GrNodeShape) shape);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                    curShellGraph.DumpInfo.SetNodeTypeShape(subType, (GrNodeShape) shape);
            }
            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }

        public void SetDumpEdgeTypeColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return;
            SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetEdgeTypeColor);
        }

        public void SetDumpEdgeTypeTextColor(EdgeType type, String colorName, bool only)
        {
            if(type == null) return;
            SetDumpColor(type, colorName, only, curShellGraph.DumpInfo.SetEdgeTypeTextColor);
        }

        public void AddDumpExcludeNodeType(NodeType nodeType, bool only)
        {
            if(nodeType == null) return;
            if(only)
                curShellGraph.DumpInfo.ExcludeNodeType(nodeType);
            else
                foreach(NodeType subType in nodeType.SubOrSameTypes)
                    curShellGraph.DumpInfo.ExcludeNodeType(subType);
        }

        public void AddDumpExcludeEdgeType(EdgeType edgeType, bool only)
        {
            if(edgeType == null) return;
            if(only)
                curShellGraph.DumpInfo.ExcludeEdgeType(edgeType);
            else
                foreach(EdgeType subType in edgeType.SubOrSameTypes)
                    curShellGraph.DumpInfo.ExcludeEdgeType(subType);
        }

        public void AddDumpGroupNodesBy(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType adjNodeType, bool exactAdjNodeType, GroupMode groupMode)
        {
            if(nodeType == null || edgeType == null || adjNodeType == null) return;
            curShellGraph.DumpInfo.AddOrExtendGroupNodeType(nodeType, exactNodeType, edgeType, exactEdgeType,
                adjNodeType, exactAdjNodeType, groupMode);
        }

        public void SetDumpEdgeLabels(bool showLabels)
        {
            if(!GraphExists()) return;
            if(showLabels)
                curShellGraph.VcgFlags |= VCGFlags.EdgeLabels;
            else
                curShellGraph.VcgFlags &= ~VCGFlags.EdgeLabels;
        }

        public void AddDumpInfoTag(GrGenType type, String attrName, bool only)
        {
            if(type == null) return;

            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                Console.WriteLine("Type \"" + type.Name + "\" has no attribute \"" + attrName + "\"");
                return;
            }

            if(only)
                curShellGraph.DumpInfo.AddTypeInfoTag(type, attrType);
            else
                foreach(GrGenType subtype in type.SubOrSameTypes)
                    curShellGraph.DumpInfo.AddTypeInfoTag(subtype, attrType);

            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }

        public void DumpReset()
        {
            if(!GraphExists()) return;

            curShellGraph.DumpInfo.Reset();

            if(InDebugMode)
                debugger.UpdateYCompDisplay();
        }
        #endregion "dump" commands

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
                Console.WriteLine("Unable to create file \"{0}\": ", e.Message);
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

                sw.WriteLine("new graph " + StringToTextToken(curShellGraph.ModelFilename) + " " + StringToTextToken(graph.Name));

                int numNodes = 0;
                foreach(INode node in graph.Nodes)
                {
                    sw.Write("new :{0}($ = {1}", node.Type.Name, StringToTextToken(graph.GetElementName(node)));
                    foreach(AttributeType attrType in node.Type.AttributeTypes)
                        sw.Write(", {0} = {1}", attrType.Name, StringToTextToken(node.GetAttribute(attrType.Name).ToString()));
                    sw.WriteLine(")");
                    LinkedList<Variable> vars = graph.GetElementVariables(node);
                    if(vars != null)
                    {
                        foreach(Variable var in vars)
                        {
                            sw.WriteLine("{0} = @({1})", StringToTextToken(var.Name), StringToTextToken(graph.GetElementName(node)));
                        }
                    }
                    numNodes++;
                }
                sw.WriteLine("# Over all number of nodes: {0}", numNodes);
                sw.WriteLine();

                int numEdges = 0;
                foreach(INode node in graph.Nodes)
                {
                    foreach(IEdge edge in node.Outgoing)
                    {
                        sw.Write("new @({0}) - :{1}($ = {2}", StringToTextToken(graph.GetElementName(node)),
                            edge.Type.Name, StringToTextToken(graph.GetElementName(edge)));
                        foreach(AttributeType attrType in edge.Type.AttributeTypes)
                            sw.Write(", {0} = {1}", attrType.Name, StringToTextToken(edge.GetAttribute(attrType.Name).ToString()));
                        sw.WriteLine(") -> @({0})", StringToTextToken(graph.GetElementName(edge.Target)));
                        LinkedList<Variable> vars = graph.GetElementVariables(node);
                        if(vars != null)
                        {
                            foreach(Variable var in vars)
                            {
                                sw.WriteLine("{0} = @({1})", StringToTextToken(var.Name), StringToTextToken(graph.GetElementName(edge)));
                            }
                        }
                        numEdges++;
                    }
                }
                sw.WriteLine("# Over all number of edges: {0}", numEdges);
                sw.WriteLine();

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
                    sw.WriteLine("dump add exclude node only " + excludedNodeType.Name);

                foreach(EdgeType excludedEdgeType in curShellGraph.DumpInfo.ExcludedEdgeTypes)
                    sw.WriteLine("dump add exclude edge only " + excludedEdgeType.Name);

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
                            }
                            sw.WriteLine("dump add node only " + groupNodeType.NodeType.Name
                                + "by " + ((nkvp.Value & GroupMode.Hidden) != 0 ? "hidden " : "")
                                + "only " + ekvp.Key.Name + " with only " + nkvp.Key.Name);
                        }
                    }
                }

                foreach(KeyValuePair<GrGenType, List<AttributeType>> infoTag in curShellGraph.DumpInfo.InfoTags)
                {
                    String kind;
                    if(infoTag.Key.IsA(graph.Model.NodeModel.RootType)) kind = "node";      // TODO: this does not work as type ids are used!
                    else kind = "edge";

                    foreach(AttributeType attrType in infoTag.Value)
                        sw.WriteLine("dump add infotag " + kind + " only " + infoTag.Key.Name + " " + attrType.Name);
                }

                sw.WriteLine("# end of graph \"{0}\" saved by grShell", graph.Name);
            }
            catch(IOException e)
            {
                Console.WriteLine("Write error: Unable to export file: {0}", e.Message);
            }
            finally
            {
                sw.Close();
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

        private void DumpElems<T>(IEnumerable<T> elems) where T : IGraphElement
        {
            bool first = true;
            foreach(IGraphElement elem in elems)
            {
                if(!first)
                    Console.Write(',');
                else
                    first = false;
                Console.Write("\"{0}\"", curShellGraph.Graph.GetElementName(elem));
            }
        }

        public void Validate(bool strict)
        {
            List<ConnectionAssertionError> errors;
            if(!GraphExists()) return;
            if(curShellGraph.Graph.Validate(strict, out errors))
                Console.WriteLine("The graph is valid.");
            else
            {
                Console.WriteLine("The graph is NOT valid:");
                foreach(ConnectionAssertionError error in errors)
                {
                    ValidateInfo valInfo = error.ValidateInfo;
                    switch(error.CAEType)
                    {
                        case CAEType.EdgeNotSpecified:
                        {
                            IEdge edge = (IEdge) error.Elem;
                            Console.WriteLine("  CAE: {0} \"{1}\" -- {2} \"{3}\" --> {4} \"{5}\" not specified",
                                edge.Source.Type.Name, curShellGraph.Graph.GetElementName(edge.Source), 
                                edge.Type.Name, curShellGraph.Graph.GetElementName(edge), 
                                edge.Target.Type.Name, curShellGraph.Graph.GetElementName(edge.Target));
                            break;
                        }
                        case CAEType.NodeTooFewSources:
                        {
                            INode node = (INode) error.Elem;
                            Console.Write("  CAE: {0} \"{1}\" [{2}<{3}] -- {4} ", valInfo.SourceType.Name,
                                curShellGraph.Graph.GetElementName(node), error.FoundEdges,
                                valInfo.SourceLower, valInfo.EdgeType.Name);
                            DumpElems(node.GetCompatibleOutgoing(valInfo.EdgeType));
                            Console.WriteLine(" --> {0}", valInfo.TargetType.Name);
                            break;
                        }
                        case CAEType.NodeTooManySources:
                        {
                            INode node = (INode) error.Elem;
                            Console.Write("  CAE: {0} \"{1}\" [{2}>{3}] -- {4} ", valInfo.SourceType.Name,
                                curShellGraph.Graph.GetElementName(node), error.FoundEdges,
                                valInfo.SourceUpper, valInfo.EdgeType.Name);
                            DumpElems(node.GetCompatibleOutgoing(valInfo.EdgeType));
                            Console.WriteLine(" --> {0}", valInfo.TargetType.Name);
                            break;
                        }
                        case CAEType.NodeTooFewTargets:
                        {
                            INode node = (INode) error.Elem;
                            Console.Write("  CAE: {0} -- {1} ", valInfo.SourceType.Name,
                                valInfo.EdgeType.Name);
                            DumpElems(node.GetCompatibleIncoming(valInfo.EdgeType));
                            Console.WriteLine(" --> {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.Name,
                                curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                            break;
                        }
                        case CAEType.NodeTooManyTargets:
                        {
                            INode node = (INode) error.Elem;
                            Console.Write("  CAE: {0} -- {1} ", valInfo.SourceType.Name,
                                valInfo.EdgeType.Name);
                            DumpElems(node.GetCompatibleIncoming(valInfo.EdgeType));
                            Console.WriteLine(" --> {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.Name,
                                curShellGraph.Graph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                            break;
                        }
                    }
                }
            }
        }

        public void ValidateWithSequence(Sequence seq)
        {
            if(!ActionsExists()) return;

            if(!curShellGraph.Actions.ValidateWithSequence(seq))
                Console.WriteLine("The graph is NOT valid with respect to the given sequence!");
            else
                Console.WriteLine("The graph is valid with respect to the given sequence.");
        }

        public void NodeTypeIsA(INode node1, INode node2)
        {
            if(node1 == null || node2 == null) return;

            NodeType type1 = node1.Type;
            NodeType type2 = node2.Type;

            Console.WriteLine("{0} type {1} is a node: {2}", type1.Name, type2.Name,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void EdgeTypeIsA(IEdge edge1, IEdge edge2)
        {
            if(edge1 == null || edge2 == null) return;

            EdgeType type1 = edge1.Type;
            EdgeType type2 = edge2.Type;

            Console.WriteLine("{0} type {1} is an edge: {2}", type1.Name, type2.Name,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void CustomGraph(ArrayList parameterList)
        {
            if(!GraphExists()) return;

            String[] parameters = (String[]) parameterList.ToArray(typeof(String));
            try
            {
                curShellGraph.Graph.Custom(parameters);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CustomActions(ArrayList parameterList)
        {
            if(!ActionsExists()) return;

            String[] parameters = (String[]) parameterList.ToArray(typeof(String));
            try
            {
                curShellGraph.Actions.Custom(parameters);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
