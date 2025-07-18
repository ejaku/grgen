/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Moritz Kroll, Edgar Jakumeit, Nicholas Tung

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using de.unika.ipd.grGen.libConsoleAndOS;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.libGr.sequenceParser;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace de.unika.ipd.grGen.grShell
{
    // TODO: the classification as struct is dubious - change to class?
    public struct Param
    {
        public String Key; // the attribute name

        // for basic types, enums
        public String Value; // the attribute value
        public ElementDef ObjectValue;

        // for container types (set, map, array, deque) attributed
        public String Type; // set/map(domain) type, array/deque value type
        public String TgtType; // map target type, array/deque index type
        public ArrayList Values; // set/map(domain) values, array/deque values
        public ArrayList TgtValues; // map target values, array/deque index values

        public Param(String key)
        {
            Key = key;
            Value = null;
            ObjectValue = null;
            Type = null;
            TgtType = null;
            Values = null;
            TgtValues = null;
        }
    }

    public class ElementDef
    {
        public readonly String ElemName;
        public readonly String VarName;
        public readonly String TypeName;
        public readonly ArrayList Attributes;

        public ElementDef(String elemName, String varName, String typeName, ArrayList attributes)
        {
            ElemName = elemName;
            VarName = varName;
            TypeName = typeName;
            Attributes = attributes;
        }
    }

    public class NewGraphOptions
    {
        public List<String> ExternalAssembliesReferenced = new List<String>();
        public String Statistics = null;
        public bool KeepDebug = false;
        public bool LazyNIC = false;
        public bool NoDebugEvents = false;
        public bool NoEvents = false;
        public bool NoInline = false;
        public bool Profile = false; // set to true to test profiling
    }

    public interface IGrShellImplForDriver
    {
        void QuitDebugMode();
        bool Evaluate(SequenceExpression seqExpr);
        void Cleanup();
        bool nonDebugNonGuiExitOnError { set; }
    }

    public interface IGrShellImplForSequenceApplierAndDebugger
    {
        bool ActionsExists();
        GrShellImpl GetGrShellImpl();
        ShellGraphProcessingEnvironment curShellProcEnv { get; }
        ElementRealizers realizers { get; }
        NewGraphOptions newGraphOptions { get; }
        bool nonDebugNonGuiExitOnError { get; }
        String debugLayout { get; }
        Dictionary<String, Dictionary<String, String>> debugLayoutOptions { get; }
        Dictionary<String, String> debugOptions { get; }
        bool detailModePreMatchEnabled { get; }
        bool detailModePostMatchEnabled { get; }
        GrGenType GetGraphElementType(String typeName);
        void HandleSequenceParserException(SequenceParserException ex);
        string ShowGraphWith(String programName, String arguments, bool keep);
        IGraphElement GetElemByName(String elemName);
        IObject GetClassObjectByName(String objName);
    }

    /// <summary>
    /// Implementation class containing application logic for generated GrShell parser class.
    /// Public methods are called by the GrShell command line parser (which is called by the GrShellDriver),
    /// other classes access this shell main-functionality class via dedicated/limited interfaces/views.
    /// </summary>
    public class GrShellImpl : IGrShellImplForDriver, IGrShellImplForSequenceApplierAndDebugger
    {
        // view on GrShellImpl from GrShellDriver
        void IGrShellImplForDriver.QuitDebugMode()
        {
            QuitDebugMode();
        }
        bool IGrShellImplForDriver.Evaluate(SequenceExpression if_)
        {
            return Evaluate(if_);
        }
        void IGrShellImplForDriver.Cleanup()
        {
            Cleanup();
        }
        bool IGrShellImplForDriver.nonDebugNonGuiExitOnError
        {
            set { nonDebugNonGuiExitOnError = value; }
        }

        // view on GrShellImpl from GrShellSequenceApplierAndDebugger
        bool IGrShellImplForSequenceApplierAndDebugger.ActionsExists()
        {
            return ActionsExists();
        }
        GrShellImpl IGrShellImplForSequenceApplierAndDebugger.GetGrShellImpl()
        {
            return this;
        }
        ShellGraphProcessingEnvironment IGrShellImplForSequenceApplierAndDebugger.curShellProcEnv
        {
            get { return curShellProcEnv; }
        }
        ElementRealizers IGrShellImplForSequenceApplierAndDebugger.realizers
        {
            get { return realizers; }
        }
        NewGraphOptions IGrShellImplForSequenceApplierAndDebugger.newGraphOptions
        {
            get { return newGraphOptions; }
        }
        bool IGrShellImplForSequenceApplierAndDebugger.nonDebugNonGuiExitOnError
        {
            get { return nonDebugNonGuiExitOnError; }
        }
        String IGrShellImplForSequenceApplierAndDebugger.debugLayout
        {
            get { return debugLayout; }
        }
        Dictionary<String, Dictionary<String, String>> IGrShellImplForSequenceApplierAndDebugger.debugLayoutOptions
        {
            get { return debugLayoutOptions; }
        }
        Dictionary<String, String> IGrShellImplForSequenceApplierAndDebugger.debugOptions
        {
            get { return debugOptions; }
        }
        bool IGrShellImplForSequenceApplierAndDebugger.detailModePreMatchEnabled
        {
            get { return detailModePreMatchEnabled; }
        }
        bool IGrShellImplForSequenceApplierAndDebugger.detailModePostMatchEnabled
        {
            get { return detailModePostMatchEnabled; }
        }
        GrGenType IGrShellImplForSequenceApplierAndDebugger.GetGraphElementType(String typeName)
        {
            return GetGraphElementType(typeName);
        }
        void IGrShellImplForSequenceApplierAndDebugger.HandleSequenceParserException(SequenceParserException ex)
        {
            GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
        }
        string IGrShellImplForSequenceApplierAndDebugger.ShowGraphWith(String programName, String arguments, bool keep)
        {
            return ShowGraphWith(programName, arguments, keep);
        }

        internal class ShowGraphParam
        {
            public readonly String ProgramName;
            public readonly String Arguments;
            public readonly String GraphFilename;
            public readonly bool KeepFile;

            public ShowGraphParam(String programName, String arguments, String graphFilename, bool keepFile)
            {
                ProgramName = programName;
                Arguments = arguments;
                GraphFilename = graphFilename;
                KeepFile = keepFile;
            }
        }

        delegate void SetNodeDumpColorProc(NodeType type, GrColor color);
        delegate void SetEdgeDumpColorProc(EdgeType type, GrColor color);

        private IBackend curGraphBackend = LGSPBackend.Instance;
        private String backendFilename = null;
        private String[] backendParameters = null;

        private readonly List<ShellGraphProcessingEnvironment> shellProcEnvs = new List<ShellGraphProcessingEnvironment>();
        private ShellGraphProcessingEnvironment curShellProcEnv = null; // one of the shellProcEnvs

        private readonly GrShellSequenceApplierAndDebugger seqApplierAndDebugger;

        private readonly ElementRealizers realizers = new ElementRealizers();

        private bool nonDebugNonGuiExitOnError = false;
        private bool silence = false; // node/edge created successfully messages

        private readonly NewGraphOptions newGraphOptions = new NewGraphOptions();

        private String debugLayout = "Orthogonal";

        /// <summary>
        /// Maps layouts to layout option names to their values.
        /// This only reflects the settings made by the user and may even contain illegal entries,
        /// if the options were set before yComp was attached.
        /// </summary>
        private readonly Dictionary<String, Dictionary<String, String>> debugLayoutOptions = new Dictionary<String, Dictionary<String, String>>();

        /// <summary>
        /// Maps debug option names to their values.
        /// </summary>
        private readonly Dictionary<String, String> debugOptions = new Dictionary<String, String>();

        bool detailModePreMatchEnabled = false;
        bool detailModePostMatchEnabled = true;


        public GrShellImpl()
        {
            seqApplierAndDebugger = new GrShellSequenceApplierAndDebugger(this);
        }

        private bool BackendExists()
        {
            if(curGraphBackend == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No backend. Select a backend, first.");
                return false;
            }
            return true;
        }

        private bool GraphExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.NamedGraph == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No graph. Make a new graph, first.");
                return false;
            }
            return true;
        }

        private bool ActionsExists()
        {
            if(curShellProcEnv == null || curShellProcEnv.ProcEnv.Actions == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No actions. Select an actions object, first.");
                return false;
            }
            return true;
        }

        public INamedGraph CurrentGraph
        {
            get
            {
                if(!GraphExists())
                    return null;
                return curShellProcEnv.ProcEnv.NamedGraph;
            }
        }

        public IActions CurrentActions
        {
            get
            {
                if(!ActionsExists())
                    return null;
                return curShellProcEnv.ProcEnv.Actions;
            }
        }

        public ShellGraphProcessingEnvironment GetShellGraphProcEnv(String graphName)
        {
            foreach(ShellGraphProcessingEnvironment shellGraph in shellProcEnvs)
            {
                if(shellGraph.ProcEnv.NamedGraph.Name == graphName)
                    return shellGraph;
            }

            ConsoleUI.errorOutWriter.WriteLine("Unknown graph: \"{0}\"", graphName);
            return null;
        }

        public ShellGraphProcessingEnvironment GetShellGraphProcEnv(int index)
        {
            if((uint)index >= (uint)shellProcEnvs.Count)
                ConsoleUI.errorOutWriter.WriteLine("Graph index out of bounds!");

            return shellProcEnvs[index];
        }

        public void External(String lineContentForExternalObjectType)
        {
            if(!GraphExists())
                return;
            curShellProcEnv.ProcEnv.Graph.Model.External(lineContentForExternalObjectType, curShellProcEnv.ProcEnv.Graph);
        }

        public object Askfor(String typeName)
        {
            if(typeName == null)
            {
                seqApplierAndDebugger.PauseUntilEnterPressed("Pause... press enter to continue.");
                return null;
            }

            return seqApplierAndDebugger.Askfor(typeName, curShellProcEnv.ProcEnv.NamedGraph);
        }

        private bool Evaluate(SequenceExpression if_)
        {
            object res = if_.Evaluate(curShellProcEnv.ProcEnv);
            return (bool)res;
        }

        private void QuitDebugMode()
        {
            seqApplierAndDebugger.QuitDebugModeAsNeeded();
        }

        private void Cleanup()
        {
            foreach(ShellGraphProcessingEnvironment shellProcEnv in shellProcEnvs)
            {
                if(shellProcEnv.ProcEnv.EmitWriter != ConsoleUI.outWriter)
                {
                    shellProcEnv.ProcEnv.EmitWriter.Close();
                    shellProcEnv.ProcEnv.EmitWriter = ConsoleUI.outWriter;
                }
            }
            ConsoleUI.outWriter.Flush();
            ConsoleUI.errorOutWriter.Flush();
        }

        #region get/set variable

        public object GetVarValue(String varName)
        {
            if(!GraphExists())
                return null;
            object val = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(val == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            return val;
        }

        public void SetVariable(String varName, object elem)
        {
            if(!GraphExists())
                return;
            curShellProcEnv.ProcEnv.SetVariableValue(varName, elem);
        }

        public void SetVariableIndexed(String varName, object value, object index)
        {
            if(!GraphExists())
                return;

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

        #endregion get/set variable

        #region get graph element or class object

        public IGraphElement GetElemByVar(String varName)
        {
            if(!GraphExists())
                return null;
            object elem = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(elem == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            if(!(elem is IGraphElement))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not a graph element!", varName);
                return null;
            }
            return (IGraphElement)elem;
        }

        public IAttributeBearer GetGraphElementOrClassObjectByVar(String varName)
        {
            if(!GraphExists())
                return null;
            object elem = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(elem == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            if(!(elem is IGraphElement) && !(elem is IBaseObject))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is neither a graph element nor a (transient) class object!", varName);
                return null;
            }
            return (IAttributeBearer)elem;
        }

        public IGraphElement GetElemByName(String elemName)
        {
            if(!GraphExists())
                return null;
            IGraphElement elem = curShellProcEnv.ProcEnv.NamedGraph.GetGraphElement(elemName);
            if(elem == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown graph element: \"{0}\"", elemName);
                return null;
            }
            return elem;
        }

        public INode GetNodeByVar(String varName)
        {
            IGraphElement elem = GetElemByVar(varName);
            if(elem == null)
                return null;
            if(!(elem is INode))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not a node!", varName);
                return null;
            }
            return (INode)elem;
        }

        public INode GetNodeByName(String elemName)
        {
            IGraphElement elem = GetElemByName(elemName);
            if(elem == null)
                return null;
            if(!(elem is INode))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not a node!", elemName);
                return null;
            }
            return (INode)elem;
        }

        public IEdge GetEdgeByVar(String varName)
        {
            IGraphElement elem = GetElemByVar(varName);
            if(elem == null)
                return null;
            if(!(elem is IEdge))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not an edge!", varName);
                return null;
            }
            return (IEdge)elem;
        }

        public IEdge GetEdgeByName(String elemName)
        {
            IGraphElement elem = GetElemByName(elemName);
            if(elem == null)
                return null;
            if(!(elem is IEdge))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not an edge!", elemName);
                return null;
            }
            return (IEdge)elem;
        }

        public IObject GetClassObjectByVar(String varName)
        {
            if(!GraphExists())
                return null;
            object elem = curShellProcEnv.ProcEnv.GetVariableValue(varName);
            if(elem == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown variable: \"{0}\"", varName);
                return null;
            }
            if(!(elem is IObject))
            {
                ConsoleUI.errorOutWriter.WriteLine("\"{0}\" is not a class object!", varName);
                return null;
            }
            return (IObject)elem;
        }

        public IObject GetClassObjectByName(String objName)
        {
            if(!GraphExists())
                return null;
            IObject obj = curShellProcEnv.objectNamerAndIndexer.GetObject(objName);
            if(obj == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown class object: \"{0}\"", objName);
                return null;
            }
            return obj;
        }

        public IAttributeBearer GetGraphElementOrClassObjectByName(String elemOrObjName)
        {
            if(!GraphExists())
                return null;
            IObject obj = curShellProcEnv.objectNamerAndIndexer.GetObject(elemOrObjName);
            if(obj == null)
            {
                IGraphElement elem = curShellProcEnv.ProcEnv.NamedGraph.GetGraphElement(elemOrObjName);
                if(elem == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown class object or graph element: \"{0}\"", elemOrObjName);
                    return null;
                }
                return elem;
            }
            return obj;
        }

        #endregion get graph element or class object

        #region get/set attribute

        public object GetElementAttribute(IAttributeBearer owner, String attributeName)
        {
            if(owner == null)
                return null;

            AttributeType attrType = GetElementAttributeType(owner, attributeName);
            if(attrType == null)
                return null;

            return owner.GetAttribute(attributeName);
        }

        public void SetElementAttribute(IAttributeBearer owner, Param param)
        {
            if(owner == null)
                return;

            ArrayList attributes = new ArrayList();
            attributes.Add(param);
            if(!CheckAttributes(owner.Type, attributes))
                return;

            SetAttributes(owner, attributes);
        }

        public void SetElementAttributeIndexed(IAttributeBearer owner, String attrName, String val, object index)
        {
            if(owner == null)
                return;

            InheritanceType type = owner.Type;
            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.PackagePrefixedName, attrName);
                return;
            }
            if(attrType.Kind != AttributeKind.ArrayAttr && attrType.Kind != AttributeKind.DequeAttr && attrType.Kind != AttributeKind.MapAttr)
            {
                ConsoleUI.errorOutWriter.WriteLine("Attribute for indexed assignment must be of array or deque or map type!");
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
            object attr = owner.GetAttribute(attrName);
            if(attr == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Can't retrieve attribute " + attrName + " !");
                return;
            }
            if(!(attr is IList) && !(attr is IDictionary) && !(attr is IDeque))
            {
                ConsoleUI.errorOutWriter.WriteLine("Attribute " + attrName + " is not of array/deque/map type!");
                return;
            }

            if(owner is IGraphElement)
                BaseGraph.ChangingAttributeAssignElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, value, index);

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
                IDictionary map = (IDictionary)attr;
                map[index] = value;
            }

            if(owner is IGraphElement)
                BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
        }

        #endregion get/set attribute

        #region container add/remove

        private object GetAttribute(IAttributeBearer owner, String attrName)
        {
            if(owner == null)
                return null;
            AttributeType attrType = GetElementAttributeType(owner, attrName);
            if(attrType == null)
                return null;
            return owner.GetAttribute(attrName);
        }

        public void ContainerAdd(IAttributeBearer owner, String attrName, object keyObj)
        {
            object attr = GetAttribute(owner, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType;
                Type valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if(dict == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a set.");
                    return;
                }
                if(keyType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Set type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(valueType != typeof(SetValueType))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Not a set.");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingSetAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj);

                dict[keyObj] = null;

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not an array.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Array type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj, null);

                array.Add(keyObj);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a deque.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Deque type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj, null);

                deque.Enqueue(keyObj);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else
                ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is neither a set nor an array nor a deque.");
        }

        public void IndexedContainerAdd(IAttributeBearer owner, String attrName, object keyObj, object valueObj)
        {
            object attr = GetAttribute(owner, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType;
                Type valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if(dict == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a map.");
                    return;
                }
                if(keyType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(valueType != valueObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Value type must be " + valueType + ", but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingMapAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj, valueObj);

                dict[keyObj] = valueObj;

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not an array.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Value type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(typeof(int) != valueObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Index type must be int, but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj, valueObj);

                array.Insert((int)valueObj, keyObj);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a deque.");
                    return;
                }
                if(valueType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Value type must be " + valueType + ", but is " + keyObj.GetType() + ".");
                    return;
                }
                if(typeof(int) != valueObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Index type must be int, but is " + valueObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributePutElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj, valueObj);

                deque.EnqueueAt((int)valueObj, keyObj);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else
                ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is neither a map nor an array nor a deque.");
        }

        public void ContainerRemove(IAttributeBearer owner, String attrName, object keyObj)
        {
            object attr = GetAttribute(owner, attrName);
            if(attr == null)
                return;

            if(attr is IDictionary)
            {
                Type keyType;
                Type valueType;
                IDictionary dict = ContainerHelper.GetDictionaryTypes(attr, out keyType, out valueType);
                if(dict == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a set/map.");
                    return;
                }
                if(keyType != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Key type must be " + keyType + ", but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                {
                    if(attrType.Kind == AttributeKind.SetAttr)
                        BaseGraph.ChangingSetAttributeRemoveElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj);
                    else
                        BaseGraph.ChangingMapAttributeRemoveElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj);
                }

                dict.Remove(keyObj);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IList)
            {
                Type valueType;
                IList array = ContainerHelper.GetListType(attr, out valueType);
                if(array == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not an array.");
                    return;
                }
                if(keyObj != null && typeof(int) != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Key/Index type must be int, but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributeRemoveElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj);

                if(keyObj != null)
                    array.RemoveAt((int)keyObj);
                else
                    array.RemoveAt(array.Count - 1);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else if(attr is IDeque)
            {
                Type valueType;
                IDeque deque = ContainerHelper.GetDequeType(attr, out valueType);
                if(deque == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a deque.");
                    return;
                }
                if(keyObj != null && typeof(int) != keyObj.GetType())
                {
                    ConsoleUI.errorOutWriter.WriteLine("Key/Index type must be int, but is " + keyObj.GetType() + ".");
                    return;
                }

                AttributeType attrType = owner.Type.GetAttributeType(attrName);

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributeRemoveElement(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, keyObj);

                if(keyObj != null)
                    deque.DequeueAt((int)keyObj);
                else
                    deque.Dequeue();

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            else
                ConsoleUI.errorOutWriter.WriteLine(GetNameOfGraphElementOrClassObject(owner) + "." + attrName + " is not a container.");
        }

        #endregion container add/remove

        #region get graph element type and compare type

        public NodeType GetNodeType(String typeName)
        {
            if(!GraphExists())
                return null;
            NodeType type = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(typeName);
            if(type == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown node type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public EdgeType GetEdgeType(String typeName)
        {
            if(!GraphExists())
                return null;
            EdgeType type = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(typeName);
            if(type == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown edge type: \"{0}\"", typeName);
                return null;
            }
            return type;
        }

        public GrGenType GetGraphElementType(String typeName)
        {
            if(!GraphExists())
                return null;
            GrGenType type = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(typeName);
            if(type != null)
                return type;
            type = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(typeName);
            if(type != null)
                return type;
            ConsoleUI.errorOutWriter.WriteLine("Unknown graph element type: \"{0}\"", typeName);
            return null;
        }

        public void NodeTypeIsA(INode node1, INode node2)
        {
            if(node1 == null || node2 == null)
                return;

            NodeType type1 = node1.Type;
            NodeType type2 = node2.Type;

            ConsoleUI.outWriter.WriteLine("{0} type {1} is a node: {2}", type1.PackagePrefixedName, type2.PackagePrefixedName,
                type1.IsA(type2) ? "yes" : "no");
        }

        public void EdgeTypeIsA(IEdge edge1, IEdge edge2)
        {
            if(edge1 == null || edge2 == null)
                return;

            EdgeType type1 = edge1.Type;
            EdgeType type2 = edge2.Type;

            ConsoleUI.outWriter.WriteLine("{0} type {1} is an edge: {2}", type1.PackagePrefixedName, type2.PackagePrefixedName,
                type1.IsA(type2) ? "yes" : "no");
        }

        #endregion get graph element type and compare type

        #region "help" commands

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
                    ConsoleUI.outWriter.WriteLine("No further help available.\n");
                    break;
                }
            }

            ConsoleUI.outWriter.Write("\nList of available commands:\n");

            ConsoleUI.outWriter.Write(
                  " - cd <path>                 Changes the current working directory to the path\n"
                + " - clear graph [<graph>]     Clears the current or the given graph\n"
                + " - custom actions ...        Action backend specific commands\n"
                + " - custom graph ...          Graph backend specific commands\n"
                + " - debug ...                 Debugging related commands\n"
                + " - delete ...                Deletes something\n"
                + " - dump ...                  Dump related commands\n"
                + " - echo <text>               Writes the given text to the console\n"
                + " - eval <expr>               Evaluates the given sequence expression\n"
                + " - (exec | xgrs) <xgrs>      Executes the given extended graph rewrite sequence\n"
                + " - exit | quit               Exits the GrShell\n"
                + " - export ...                Exports the current graph.\n"
                + " - help <command>*           Displays this help or help about a command\n"
                + " - if se clt [else clf] endif Conditionally executes the command lines clt\n"
                + "                             if the sequence expression se evaluates to true\n"
                + "                             or otherwise the command lines clf\n"
                + " - import ...                Imports a graph instance and/or a graph model\n"
                + " - include <filename>        Includes and executes the given .grs-script\n"
                + " - ls                        Lists the directories and files in the current\n"
                + "                             working directory, highlighting relevant files\n"
                + " - new ...                   Creation commands\n"
                + " - pwd                       Prints the path to the current working directory\n"
                + " - record ...                Records the changes of the current graph\n"
                + " - recordflush               Flushes the buffers of the recordings to disk\n"
                + " - redirect ...              Redirects edges or emit instruction output\n"
                + " - replay ...                Replays the recorded changes to a graph\n"
                + " - retype ...                Retype commands\n"
                + " - save graph <filename>     Saves the current graph as a GrShell script\n"
                + " - select ...                Selection commands\n"
                + " - show ...                  Shows something\n"
                + " - validate ...              Validate related commands\n"
                + " - ! <command>               Executes the given system command\n");
            ConsoleUI.outWriter.Write("Type \"help <command>\" to get extended help on <command> (in case of ...)\n");

            ConsoleUI.outWriter.Write("\nList of variable handling and user input constructs,\n"
                + "as well as status changes and type comparisons:\n"
                + " - askfor                    Waits until the user presses enter\n"
                + " - edge type <e1> is <e2>    Tells whether the edge e1 has a type compatible\n"
                + "                             to the type of e2\n"
                + " - node type <n1> is <n2>    Tells whether the node n1 has a type compatible\n"
                + "                             to the type of n2\n"
                + " - randomseed (time|<seed>)  Sets the seed of the random number generator\n"
                + "                             to the current time or the given integer\n"
                + " - silence (on | off)        Switches \"new ... created\" messages on/off\n"
                + " - silence exec (on | off)   Print match statistics during execution every sec?\n"
                + " - <elem>.<member>           Shows the given graph element member\n"
                + " - <elem>.<member> = <val>   Sets the value of the given element member\n"
                + " - <elem>.<memb>.add(<val>)  Adds the value to the given set member\n"
                + " - <elem>.<memb>.add(<k>,<v>) Adds values k->v to the given map member\n"
                + " - <elem>.rem(<val>)         Removes value from the given set/map member\n"
                + " - <var> = <exp>             Assigns the given expression to <var>, <exp>:\n"
                + "                               - <elem>\n"
                + "                               - <elem>.<member>\n"
                + "                               - <val>, a value literal (see user manual)\n"
                + " - <var> = askfor <type>     Asks the user to input a value of given type\n"
                + "                             a node/edge is to be entered in yComp (debug\n"
                + "                             mode must be enabled); other values are to be\n"
                + "                             entered on the keyboard (format as in <expr>\n"
                + "                             but constant only)\n");
            ConsoleUI.outWriter.Write("Also see \"help new\" for compiler options\n\n");
        }

        public void HelpNew(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"new\":\n"
                + " - new [new] graph <filename> [<graphname>]\n"
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

            ConsoleUI.outWriter.WriteLine("List of compilation configuration options starting with \"new\":\n"
                + " - new add reference <filename>\n"
                + "   Configures a reference to an external assembly\n"
                + "   to be linked into the generated assemblies.\n"
                + " - new set keepdebug (on | off)\n"
                + "   switches on/off whether to keep the generated files and to add debug symbols\n"
                + "   (includes emitting of some validity checking code)\n"
                + " - new set lazynic (on | off)\n"
                + "   switches on/off whether to execute negatives, independents, and conditions\n"
                + "   lazily only at the end of matching (normally asap)\n"
                + " - new set nodebugevents (on | off)\n"
                + "   switches on/off debugging related action events\n"
                + " - new set noevents (on | off)\n"
                + "   switches on/off attribute change events\n"
                + " - new set noinline (on | off)\n"
                + "   switches on/off whether to inline subpatterns\n"
                + " - new set profile (on | off)\n"
                + "   switches on/off whether to emit profiling information\n"
                + " - new set statistics <filename>\n"
                + "   Generates assemblies using the statistics file specified,\n"
                + "   yielding pattern matchers adapted to the class of graphs described there.\n");
        }

        public void HelpSelect(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"select\":\n"
                + " - select backend <dllname> [<paramlist>]\n"
                + "   Selects the backend to be used to create graphs.\n"
                + "   Defaults to the lgspBackend.\n\n"
                + " - select graph <name>\n"
                + "   Selects the given already loaded graph.\n\n"
                + " - select actions <dllname>\n"
                + "   Selects the actions assembly for the current graph.\n\n");
        }

        public void HelpDelete(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"delete\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"retype\":\n"
                + " - retype node<type>\n"
                + "   Retypes the given node from the current graph to the given type.\n\n"
                + " - retype -edge<type>-> or -edge<type>-\n"
                + "   Retypes the given edge from the current graph to the given type.\n");
        }

        public void HelpRedirect(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"redirect\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"show\":\n"
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
                + "   Shows the current graph with the given program and optional arguments.\n"
                + "   The program determines the format used for dumping (either .vcg or .dot)\n"
                + "   Example: show graph ycomp\n"
                + "   Example: show graph dot (graphviz must be in the search path)\n"
                + "   Example: show graph MSAGL (no dumping but in-memory display)\n\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            // Not mentioned: debug grs

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"debug\":\n"
                + " - debug (exec | xgrs) <xgrs>\n"
                + "   Debugs the given extended graph rewrite sequence.\n\n"
                + " - debug eval <expr>\n"
                + "   Debugs the given sequence expression.\n\n"
                + " - debug (enable | disable)\n"
                + "   Enables/disables debug mode.\n\n"
                + " - debug layout\n"
                + "   Forces yComp to relayout the graph.\n\n"
                + " - debug set layout [<algo>]\n"
                + "   Selects the layout algorithm. If algorithm is not given,\n"
                + "   all available layout algorithms are listed.\n\n"
                + " - debug get layout\n"
                + "   Lists the currently selected layout algorithm (also see above).\n\n"
                + " - debug get layout options\n"
                + "   Lists all available layout options for the current layout algorithm.\n\n"
                + " - debug set layout option <name> <value>\n"
                + "   Sets the value of the given layout option.\n\n"
                + " - debug with (MSAGL | yComp)\n"
                + "   Switches to debugging with a WindowsForms GUI and a MSAGL graph viewer,\n"
                + "   or to debugging with a console UI and the external yComp graph viewer.\n\n"
                + " - debug get options\n"
                + "   Lists all available debugging options.\n\n"
                + " - debug set option <name> <value>\n"
                + "   Sets the value of the given debugging option.\n\n");
        }

        public void HelpDump(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"dump\":\n"
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
                    ConsoleUI.outWriter.WriteLine("\nNo further help available.");
                    break;
                }
            }

            ConsoleUI.outWriter.WriteLine("\nList of available commands for \"custom\":\n"
                + " - custom graph:\n");
            CustomGraph(new List<String>());
            ConsoleUI.outWriter.WriteLine(" - custom actions:\n");
            CustomActions(new List<String>());
        }

        public void HelpValidate(List<String> commands)
        {
            if(commands.Count > 1)
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("List of available commands for \"validate\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("List of available commands for \"import\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("List of available commands for \"export\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("List of available commands for \"record\":\n"
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
                ConsoleUI.outWriter.WriteLine("\nNo further help available.");

            ConsoleUI.outWriter.WriteLine("List of available commands for \"replay\":\n"
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

        #endregion "help" commands

        #region external shell execution and file system commands 

        public void ExecuteCommandLineInExternalShell(String cmdLine)
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
                ConsoleUI.errorOutWriter.WriteLine("Unable to execute file \"" + filename + "\": " + e.Message);
            }
        }

        public bool ChangeDirectory(String filename)
        {
            try
            {
                Directory.SetCurrentDirectory(filename);
                ConsoleUI.outWriter.WriteLine("Changed current working directory to " + filename);
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Error during cd to \"" + filename + "\": " + e.Message);
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
                    ConsoleUI.consoleOut.PrintHighlighted(nestedDirectory.Name, HighlightingMode.Directory);
                    ConsoleUI.outWriter.Write(" ");
                }
                ConsoleUI.outWriter.WriteLine();
                FileInfo[] filesInDirectory = dir.GetFiles();
                foreach(FileInfo file in filesInDirectory)
                {
                    if(file.Name.EndsWith(".grs"))
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.GrsFile);
                    else if(file.Name.EndsWith(".grsi"))
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.GrsiFile);
                    else if(file.Name.EndsWith(".grg"))
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.GrgFile);
                    else if(file.Name.EndsWith(".gri"))
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.GriFile);
                    else if(file.Name.EndsWith(".gm"))
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.GmFile);
                    else
                        ConsoleUI.consoleOut.PrintHighlighted(file.Name, HighlightingMode.None);
                    ConsoleUI.outWriter.Write(" ");
                }
                ConsoleUI.outWriter.WriteLine();
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Error on ls command: " + e.Message);
                return false;
            }
            return true;
        }

        public bool PrintWorkingDirectory()
        {
            try
            {
                ConsoleUI.outWriter.WriteLine(Directory.GetCurrentDirectory());
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Error on pwd command: " + e.Message);
                return false;
            }
            return true;
        }

        #endregion external shell execution and file system commands

        #region "new graph" commands

        public bool NewGraph(String specFilename, String graphName, bool forceRebuild,
            String persistenceProvider, String connectionParameters)
        {
            if(!BackendExists())
                return false;

            // replace wrong directory separator chars by the right ones
            if(Path.DirectorySeparatorChar != '\\')
                specFilename = specFilename.Replace('\\', Path.DirectorySeparatorChar);

            if(specFilename.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || specFilename.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                bool success = NewGraphFromTargetFile(specFilename, graphName, persistenceProvider, connectionParameters);
                if(!success)
                    return false;
            }
            else
            {
                String correctedSpecFilename = AppendFilenameSuffixAsNeeded(specFilename);
                if(correctedSpecFilename == null)
                    return false; // error message already emitted
                specFilename = correctedSpecFilename;

                if(specFilename.EndsWith(".gm", StringComparison.OrdinalIgnoreCase))
                {
                    bool success = NewGraphFromModelFile(specFilename, graphName, forceRebuild, persistenceProvider, connectionParameters);
                    if(!success)
                        return false;
                }
                else if(specFilename.EndsWith(".grg", StringComparison.OrdinalIgnoreCase))
                {
                    bool success = NewGraphFromActionsFile(specFilename, graphName, forceRebuild, persistenceProvider, connectionParameters);
                    if(!success)
                        return false;
                }
                else
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown specification file format of file: \"" + specFilename + "\"");
                    return false;
                }
            }

            shellProcEnvs.Add(curShellProcEnv);
            seqApplierAndDebugger.RestartDebuggerOnNewGraphAsNeeded();

            return true;
        }

        private bool NewGraphFromTargetFile(String specFilename, String graphName, String persistenceProvider, String connectionParameters)
        {
            IGlobalVariables globalVariables = new LGSPGlobalVariables();
            INamedGraph graph;
            try
            {
                if(persistenceProvider != null)
                {
                    graph = curGraphBackend.CreatePersistentNamedGraph(specFilename, globalVariables, graphName,
                        persistenceProvider, connectionParameters, backendParameters);
                }
                else
                    graph = curGraphBackend.CreateNamedGraph(specFilename, globalVariables, graphName, backendParameters);
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create graph with model \"{0}\":\n{1}", specFilename, e.Message);
                return false;
            }

            try
            {
                curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine(ex);
                ConsoleUI.errorOutWriter.WriteLine("Unable to create new graph: {0}", ex.Message);
                return false;
            }

            ConsoleUI.outWriter.WriteLine("New graph \"{0}\" of model \"{1}\" created.", graphName, graph.Model.ModelName);
            return true;
        }

        private static String AppendFilenameSuffixAsNeeded(String specFilename)
        {
            if(!File.Exists(specFilename))
            {
                String ruleFilename = specFilename + ".grg";
                if(!File.Exists(ruleFilename))
                {
                    String gmFilename = specFilename + ".gm";
                    if(!File.Exists(gmFilename))
                    {
                        ConsoleUI.errorOutWriter.WriteLine("The specification file \"" + specFilename + "\" or \"" + ruleFilename + "\" or \"" + gmFilename + "\" does not exist!");
                        return null;
                    }
                    else
                        specFilename = gmFilename;
                }
                else
                    specFilename = ruleFilename;
            }
            return specFilename;
        }

        private bool NewGraphFromModelFile(String specFilename, String graphName, bool forceRebuild, String persistenceProvider, String connectionParameters)
        {
            IGlobalVariables globalVariables = new LGSPGlobalVariables();
            INamedGraph graph;
            try
            {
                ProcessSpecFlags flags = NewGraphOptionsToFlags(newGraphOptions, forceRebuild);
                if(persistenceProvider != null)
                {
                    graph = curGraphBackend.CreatePersistentNamedFromSpec(specFilename, globalVariables, graphName, null,
                        flags, newGraphOptions.ExternalAssembliesReferenced, 0,
                        persistenceProvider, connectionParameters);
                }
                else
                {
                    graph = curGraphBackend.CreateNamedFromSpec(specFilename, globalVariables, graphName, null,
                        flags, newGraphOptions.ExternalAssembliesReferenced, 0);
                }
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}\nStack:\n({2})\n", specFilename, e.Message, e.StackTrace);
                return false;
            }

            try
            {
                curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine(ex);
                ConsoleUI.errorOutWriter.WriteLine("Unable to create new graph: {0}", ex.Message);
                return false;
            }

            ConsoleUI.outWriter.WriteLine("New graph \"{0}\" created from spec file \"{1}\".", graphName, specFilename);
            return true;
        }

        private bool NewGraphFromActionsFile(String specFilename, String graphName, bool forceRebuild, String persistenceProvider, String connectionParameters)
        {
            IGlobalVariables globalVariables = new LGSPGlobalVariables();
            INamedGraph graph;
            IActions actions;

            try
            {
                ProcessSpecFlags flags = NewGraphOptionsToFlags(newGraphOptions, forceRebuild);
                if(persistenceProvider != null)
                {
                    curGraphBackend.CreatePersistentNamedFromSpec(specFilename, globalVariables, graphName, newGraphOptions.Statistics,
                        flags, newGraphOptions.ExternalAssembliesReferenced, 0,
                        persistenceProvider, connectionParameters,
                        out graph, out actions);
                }
                else
                {
                    curGraphBackend.CreateNamedFromSpec(specFilename, globalVariables, graphName, newGraphOptions.Statistics,
                        flags, newGraphOptions.ExternalAssembliesReferenced, 0,
                        out graph, out actions);
                }
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create graph from specification file \"{0}\":\n{1}\nStack:\n({2})\n", specFilename, e.Message, e.StackTrace);
                return false;
            }

            try
            {
                curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, specFilename);
                curShellProcEnv.ProcEnv.Actions = actions;
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine(ex);
                ConsoleUI.errorOutWriter.WriteLine("Unable to create new shell processing environment: {0}", ex.Message);
                return false;
            }

            ConsoleUI.outWriter.WriteLine("New graph \"{0}\" and actions created from spec file \"{1}\".", graphName, specFilename);
            return true;
        }

        private static ProcessSpecFlags NewGraphOptionsToFlags(NewGraphOptions newGraphOptions, bool forceRebuild)
        {
            ProcessSpecFlags flags = newGraphOptions.KeepDebug
                ? ProcessSpecFlags.KeepGeneratedFiles | ProcessSpecFlags.CompileWithDebug
                : ProcessSpecFlags.UseNoExistingFiles;
            if(newGraphOptions.Profile)
                flags |= ProcessSpecFlags.Profile;
            if(newGraphOptions.NoDebugEvents)
                flags |= ProcessSpecFlags.NoDebugEvents;
            if(newGraphOptions.NoEvents)
                flags |= ProcessSpecFlags.NoEvents;
            if(newGraphOptions.LazyNIC)
                flags |= ProcessSpecFlags.LazyNIC;
            if(newGraphOptions.NoInline)
                flags |= ProcessSpecFlags.Noinline;
            if(forceRebuild)
                flags |= ProcessSpecFlags.GenerateEvenIfSourcesDidNotChange;
            return flags;
        }

        public bool AddNewGraph(String graphName)
        {
            if(curShellProcEnv.NameToSubgraph.ContainsKey(graphName))
            {
                ConsoleUI.errorOutWriter.WriteLine("The graph name {0} is already in use!", graphName);
                return false;
            }
            INamedGraph graph = (INamedGraph)curShellProcEnv.ProcEnv.Graph.CreateEmptyEquivalent(graphName);
            curShellProcEnv.NameToSubgraph.Add(graphName, graph);
            if(!silence)
                ConsoleUI.outWriter.WriteLine("New subgraph \"{0}\" created.", curShellProcEnv.NameToSubgraph[graphName]);

            return ChangeGraph(graphName);
        }

        #endregion "new graph" commands

        #region graph commands (delete, clear, in subgraph)

        public bool ChangeGraph(String graphName)
        {
            if(!curShellProcEnv.NameToSubgraph.ContainsKey(graphName))
            {
                ConsoleUI.errorOutWriter.WriteLine("No graph with name {0} known!", graphName);
                return false;
            }
            if(curShellProcEnv.ProcEnv.IsInSubgraph)
            {
                if(!silence)
                    ConsoleUI.outWriter.WriteLine("Returning from subgraph \"{0}\".", curShellProcEnv.ProcEnv.NamedGraph.Name);
                curShellProcEnv.ProcEnv.ReturnFromSubgraph();
            }
            if(curShellProcEnv.ProcEnv.IsInSubgraph)
            {
                ConsoleUI.errorOutWriter.WriteLine("GrShell is not capable of deeply nested subgraph processing, only uses main graph plus optionally one subgraph at a time.");
                return false;
            }
            if(curShellProcEnv.NameToSubgraph[graphName] == curShellProcEnv.ProcEnv.NamedGraph)
            {
                if(!silence)
                    ConsoleUI.outWriter.WriteLine("(Back) At main host graph \"{0}\".", curShellProcEnv.ProcEnv.NamedGraph.Name);
                return true;
            }

            if(!silence)
                ConsoleUI.outWriter.WriteLine("Switching to subgraph \"{0}\".", curShellProcEnv.NameToSubgraph[graphName]);
            curShellProcEnv.ProcEnv.SwitchToSubgraph(curShellProcEnv.NameToSubgraph[graphName]);

            return true;
        }

        public void ClearGraph(ShellGraphProcessingEnvironment shellGraphProcEnv, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists())
                    return;
                shellGraphProcEnv = curShellProcEnv;
            }
            else if(shellGraphProcEnv == null)
                return;

            shellGraphProcEnv.ProcEnv.Graph.Clear();
            shellGraphProcEnv.objectNamerAndIndexer.Clear();
        }

        public bool DestroyGraph(ShellGraphProcessingEnvironment shellGraphProcEnv, bool shellGraphSpecified)
        {
            if(!shellGraphSpecified)
            {
                if(!GraphExists())
                    return false;
                shellGraphProcEnv = curShellProcEnv;
            }
            else if(shellGraphProcEnv == null)
                return false;

            seqApplierAndDebugger.DisableDebuggerAfterDeletionAsNeeded(shellGraphProcEnv);

            if(shellGraphProcEnv == curShellProcEnv)
                curShellProcEnv = null;
            shellProcEnvs.Remove(shellGraphProcEnv);

            return true;
        }

        #endregion graph commands (delete, clear, in subgraph)

        #region "new" graph element/class object commands

        public INode NewNode(ElementDef elemDef)
        {
            if(!GraphExists())
                return null;

            NodeType nodeType;
            if(elemDef.TypeName != null)
            {
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(elemDef.TypeName);
                if(nodeType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown node type: \"" + elemDef.TypeName + "\"");
                    return null;
                }
                if(nodeType.IsAbstract)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Abstract node type \"" + elemDef.TypeName + "\" may not be instantiated!");
                    return null;
                }
            }
            else
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;

            if(elemDef.Attributes != null && !CheckAttributes(nodeType, elemDef.Attributes))
                return null;

            INode node;
            try
            {
                node = curShellProcEnv.ProcEnv.NamedGraph.AddNode(nodeType, elemDef.ElemName);
                curShellProcEnv.ProcEnv.SetVariableValue(elemDef.VarName, node);
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create new node: {0}", e.Message);
                return null;
            }
            if(node == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Creation of new node failed.");
                return null;
            }

            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(node, elemDef.Attributes))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unexpected failure: Unable to set node attributes inspite of check?!");
                    curShellProcEnv.ProcEnv.NamedGraph.Remove(node);
                    return null;
                }
            }

            if(!silence)
                ConsoleUI.outWriter.WriteLine("New node \"{0}\" of type \"{1}\" has been created.", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), node.Type.PackagePrefixedName);

            return node;
        }

        public IEdge NewEdge(ElementDef elemDef, INode node1, INode node2, bool directed)
        {
            if(node1 == null || node2 == null)
                return null;

            EdgeType edgeType;
            if(elemDef.TypeName != null)
            {
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(elemDef.TypeName);
                if(edgeType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown edge type: \"" + elemDef.TypeName + "\"");
                    return null;
                }
                if(edgeType.IsAbstract)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Abstract edge type \"" + elemDef.TypeName + "\" may not be instantiated!");
                    return null;
                }
            }
            else
            {
                if(directed)
                    edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType("Edge");
                else
                    edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType("UEdge");
            }

            if(elemDef.Attributes != null && !CheckAttributes(edgeType, elemDef.Attributes))
                return null;

            IEdge edge;
            try
            {
                edge = curShellProcEnv.ProcEnv.NamedGraph.AddEdge(edgeType, node1, node2, elemDef.ElemName);
                curShellProcEnv.ProcEnv.SetVariableValue(elemDef.VarName, edge);
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create new edge: {0}", e.Message);
                return null;
            }
            if(edge == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Creation of new edge failed.");
                return null;
            }

            if(elemDef.Attributes != null)
            {
                if(!SetAttributes(edge, elemDef.Attributes))
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unexpected failure: Unable to set edge attributes inspite of check?!");
                    curShellProcEnv.ProcEnv.NamedGraph.Remove(edge);
                    return null;
                }
            }

            if(!silence)
            {
                if(directed)
                    ConsoleUI.outWriter.WriteLine("New edge \"{0}\" of type \"{1}\" has been created from \"{2}\" to \"{3}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node1), curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node2));
                else
                    ConsoleUI.outWriter.WriteLine("New edge \"{0}\" of type \"{1}\" has been created between \"{2}\" and \"{3}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node1), curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node2));
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
                        ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be either \"true\" or \"false\"!", attribute);
                        throw new Exception("Unknown boolean literal" + valueString);
                    }
                    value = val;
                    break;
                }
            case AttributeKind.ByteAttr:
                {
                    sbyte val;
                    if(valueString.StartsWith("0x"))
                    {
                        if(!SByte.TryParse(RemoveTypeSuffix(valueString.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a byte (signed)!", attribute);
                            throw new Exception("Unknown byte literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!SByte.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a byte (signed)!", attribute);
                        throw new Exception("Unknown byte literal" + valueString);
                    }
                    value = val;
                    break;
                }
            case AttributeKind.ShortAttr:
                {
                    short val;
                    if(valueString.StartsWith("0x"))
                    {
                        if(!Int16.TryParse(RemoveTypeSuffix(valueString.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber,
                            System.Globalization.CultureInfo.InvariantCulture, out val))
                        {
                            ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a short!", attribute);
                            throw new Exception("Unknown short literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!Int16.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a short!", attribute);
                        throw new Exception("Unknown short literal" + valueString);
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
                            ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be an integer!", attribute);
                            throw new Exception("Unknown integer literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!Int32.TryParse(valueString, out val))
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be an integer!", attribute);
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
                            ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a long!", attribute);
                            throw new Exception("Unknown long literal" + valueString);
                        }
                        value = val;
                        break;
                    }
                    if(!Int64.TryParse(RemoveTypeSuffix(valueString), out val))
                    {
                        ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be a long!", attribute);
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
                        ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be a (single) floating point number!", attribute);
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
                        ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be a (double) floating point number!", attribute);
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
                    {
                        value = curShellProcEnv.ProcEnv.Graph.Model.Parse(
                            new StringReader(valueString), null, curShellProcEnv.ProcEnv.Graph);
                    }
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
                            ConsoleUI.errorOutWriter.WriteLine("No graph with name {0} known!", valueString);
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
            case AttributeKind.InternalClassObjectAttr:
                {
                    if(valueString[0] == '@' && valueString[1] == '@' && valueString[2] == '(' && valueString[valueString.Length - 1] == ')')
                    {
                        if((valueString[3] == '\"' || valueString[3] == '\'') && (valueString[valueString.Length - 2] == '\"' || valueString[valueString.Length - 2] == '\''))
                            value = GetClassObjectByName(valueString.Substring(4, valueString.Length - 6));
                        else
                            value = GetClassObjectByName(valueString.Substring(3, valueString.Length - 4));
                    }
                    else if(valueString == "null")
                        value = null;
                    else
                        value = GetClassObjectByVar(valueString);
                    break;
                }
            }
            return value;
        }

        public String RemoveTypeSuffix(String value)
        {
            if(value.EndsWith("y") || value.EndsWith("Y")
                || value.EndsWith("s") || value.EndsWith("S")
                || value.EndsWith("l") || value.EndsWith("L"))
            {
                return value.Substring(0, value.Length - 1);
            }
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
                        valueString = valueString.Substring(valueString.IndexOf("::") + "::".Length);

                    int val;
                    if(Int32.TryParse(valueString, out val))
                        value = Enum.ToObject(attrType.EnumType.EnumType, val);
                    else
                        value = Enum.Parse(attrType.EnumType.EnumType, valueString);
                }
                catch(Exception)
                {
                }
                if(value == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Attribute {0} must be one of the following values:", attribute);
                    foreach(EnumMember member in attrType.EnumType.Members)
                    {
                        ConsoleUI.errorOutWriter.WriteLine(" - {0} = {1}", member.Name, member.Value);
                    }
                    throw new Exception("Unknown enum member");
                }
            }
            else
                value = ParseAttributeValue(attrType.Kind, valueString, attribute);
            return value;
        }

        private bool CheckAttributes(InheritanceType type, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = type.GetAttributeType(par.Key);

                try
                {
                    GetCheckAttributes(par, type, attributes, true);
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private bool SetAttributes(IAttributeBearer owner, ArrayList attributes)
        {
            foreach(Param par in attributes)
            {
                AttributeType attrType = owner.Type.GetAttributeType(par.Key);

                object value;
                try
                {
                    value = GetCheckAttributes(par, owner.Type, attributes, false);
                }
                catch(Exception ex)
                {
                    ConsoleUI.errorOutWriter.WriteLine(ex.ToString());
                    return false;
                }

                if(owner is IGraphElement)
                    BaseGraph.ChangingAttributeAssign(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType, value);

                owner.SetAttribute(par.Key, value);

                if(owner is IGraphElement)
                    BaseGraph.ChangedAttribute(curShellProcEnv.ProcEnv.NamedGraph, (IGraphElement)owner, attrType);
            }
            return true;
        }

        private object GetCheckAttributes(Param par, InheritanceType type, ArrayList attributes, bool onlyCheck)
        {
            AttributeType attrType = type.GetAttributeType(par.Key);
            if(attrType == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Type \"{0}\" does not have an attribute \"{1}\"!", type.PackagePrefixedName, par.Key);
                throw new Exception("Attribute unknown"); ;
            }

            object value = null;
            IDictionary setmap = null;
            IList array = null;
            IDeque deque = null;
            switch(attrType.Kind)
            {
            case AttributeKind.SetAttr:
                if(par.Value != "set")
                {
                    ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be a set constructor!", par.Key);
                    throw new Exception("Set literal expected");
                }
                setmap = ContainerHelper.NewDictionary(
                    TypesHelper.GetType(par.Type, curShellProcEnv.ProcEnv.NamedGraph.Model),
                    typeof(SetValueType));
                foreach(object val in par.Values)
                {
                    object obj;
                    if(val is ElementDef)
                        obj = GetObjectOrParseAttributeValue((ElementDef)val, null, null, null, onlyCheck);
                    else
                        obj = GetObjectOrParseAttributeValue(null, attrType.ValueType, (String)val, par.Key, onlyCheck);
                    if(!onlyCheck)
                        setmap[obj] = null;
                }
                value = setmap;
                break;
            case AttributeKind.MapAttr:
                if(par.Value != "map")
                {
                    ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be a map constructor!", par.Key);
                    throw new Exception("Map literal expected");
                }
                setmap = ContainerHelper.NewDictionary(
                    TypesHelper.GetType(par.Type, curShellProcEnv.ProcEnv.NamedGraph.Model),
                    TypesHelper.GetType(par.TgtType, curShellProcEnv.ProcEnv.NamedGraph.Model));
                IEnumerator tgtValEnum = par.TgtValues.GetEnumerator();
                foreach(object val in par.Values)
                {
                    tgtValEnum.MoveNext();
                    object obj;
                    if(val is ElementDef)
                        obj = GetObjectOrParseAttributeValue((ElementDef)val, null, null, null, onlyCheck);
                    else
                        obj = GetObjectOrParseAttributeValue(null, attrType.KeyType, (String)val, par.Key, onlyCheck);
                    object tgtObj;
                    if(tgtValEnum.Current is ElementDef)
                        tgtObj = GetObjectOrParseAttributeValue((ElementDef)tgtValEnum.Current, null, null, null, onlyCheck);
                    else
                        tgtObj = GetObjectOrParseAttributeValue(null, attrType.ValueType, (String)tgtValEnum.Current, par.Key, onlyCheck);
                    if(!onlyCheck)
                        setmap[obj] = tgtObj;
                }
                value = setmap;
                break;
            case AttributeKind.ArrayAttr:
                if(par.Value != "array")
                {
                    ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be an array constructor!", par.Key);
                    throw new Exception("Array literal expected");
                }
                array = ContainerHelper.NewList(
                    TypesHelper.GetType(par.Type, curShellProcEnv.ProcEnv.NamedGraph.Model));
                foreach(object val in par.Values)
                {
                    object obj;
                    if(val is ElementDef)
                        obj = GetObjectOrParseAttributeValue((ElementDef)val, null, null, null, onlyCheck);
                    else
                        obj = GetObjectOrParseAttributeValue(null, attrType.ValueType, (String)val, par.Key, onlyCheck);
                    if(!onlyCheck)
                        array.Add(obj);
                }
                value = array;
                break;
            case AttributeKind.DequeAttr:
                if(par.Value != "deque")
                {
                    ConsoleUI.errorOutWriter.WriteLine("Attribute \"{0}\" must be a deque constructor!", par.Key);
                    throw new Exception("Deque literal expected");
                }
                deque = ContainerHelper.NewDeque(
                    TypesHelper.GetType(par.Type, curShellProcEnv.ProcEnv.NamedGraph.Model));
                foreach(object val in par.Values)
                {
                    object obj;
                    if(val is ElementDef)
                        obj = GetObjectOrParseAttributeValue((ElementDef)val, null, null, null, onlyCheck);
                    else
                        obj = GetObjectOrParseAttributeValue(null, attrType.ValueType, (String)val, par.Key, onlyCheck);
                    if(!onlyCheck)
                        deque.Enqueue(obj);
                }
                value = deque;
                break;
            default:
                value = GetObjectOrParseAttributeValue(par.ObjectValue, attrType, par.Value, par.Key, onlyCheck);
                break;
            }

            return value;
        }

        object GetObjectOrParseAttributeValue(ElementDef elemDef, AttributeType attrType, string value, string attr, bool onlyCheck)
        {
            if(elemDef != null)
                return GetObject(elemDef, onlyCheck);
            else
                return ParseAttributeValue(attrType, value, attr);
        }

        public object GetObject(ElementDef elemDef, bool onlyCheck)
        {
            if(elemDef.TypeName == null)
            {
                return curShellProcEnv.objectNamerAndIndexer.GetObject(elemDef.ElemName);
            }
            else
            {
                ObjectType objType = curShellProcEnv.ProcEnv.NamedGraph.Model.ObjectModel.GetType(elemDef.TypeName);
                if(objType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Class object type \"{0}\" not known!", elemDef.TypeName);
                    throw new Exception("Class object type not known");
                }
                if(onlyCheck)
                {
                    foreach(Param objAttr in elemDef.Attributes)
                    {
                        GetObjectOrParseAttributeValue(objAttr.ObjectValue,
                            objType.GetAttributeType(objAttr.Key), objAttr.Value, objAttr.Key, onlyCheck);
                    }
                    return null;
                }
                else
                {
                    IObject obj = objType.CreateObject(curShellProcEnv.ProcEnv.NamedGraph, elemDef.ElemName);
                    if(elemDef.ElemName != null)
                    {
                        curShellProcEnv.objectNamerAndIndexer.AssignName(obj, elemDef.ElemName);
                        foreach(Param objAttr in elemDef.Attributes)
                        {
                            obj.SetAttribute(objAttr.Key, GetObjectOrParseAttributeValue(objAttr.ObjectValue,
                                objType.GetAttributeType(objAttr.Key), objAttr.Value, objAttr.Key, onlyCheck));
                        }
                    }
                    return obj;
                }
            }
        }

        #endregion "new" graph element/class object commands

        #region graph element commands (delete, retype, redirect)

        public bool Remove(INode node)
        {
            if(node == null)
                return false;

            string name = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node); // get name before remove

            try
            {
                curShellProcEnv.ProcEnv.NamedGraph.RemoveEdges(node);
                curShellProcEnv.ProcEnv.NamedGraph.Remove(node);
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to remove node: " + e.Message);
                return false;
            }

            if(!silence)
                ConsoleUI.outWriter.WriteLine("Node \"{0}\" of type \"{1}\" has been deleted.", name, node.Type.PackagePrefixedName);

            return true;
        }

        public bool Remove(IEdge edge)
        {
            if(edge == null)
                return false;

            string name = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge); // get name before remove

            curShellProcEnv.ProcEnv.NamedGraph.Remove(edge);

            if(!silence)
                ConsoleUI.outWriter.WriteLine("Edge \"{0}\" of type \"{1}\" has been deleted.", name, edge.Type.PackagePrefixedName);

            return true;
        }

        public INode Retype(INode node, String newType)
        {
            if(node == null || newType == null)
                return null;

            try
            {
                NodeType nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.GetType(newType);
                if(nodeType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown node type: \"" + newType + "\"");
                    return null;
                }
                if(nodeType.IsAbstract)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Abstract node type \"" + newType + "\" may not be instantiated!");
                    return null;
                }

                String oldType = node.Type.PackagePrefixedName;
                node = curShellProcEnv.ProcEnv.NamedGraph.Retype(node, nodeType);
                if(!silence)
                    ConsoleUI.outWriter.WriteLine("Node \"{0}\" has been retyped from \"{1}\" to \"{2}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), oldType, node.Type.PackagePrefixedName);

                return node;
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to retype node: " + e.Message);
                return null;
            }
        }

        public IEdge Retype(IEdge edge, String newType)
        {
            if(edge == null || newType == null)
                return null;

            try
            {
                EdgeType edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.GetType(newType);
                if(edgeType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown edge type: \"" + newType + "\"");
                    return null;
                }
                if(edgeType.IsAbstract)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Abstract edge type \"" + newType + "\" may not be instantiated!");
                    return null;
                }

                String oldType = edge.Type.PackagePrefixedName;
                edge = curShellProcEnv.ProcEnv.NamedGraph.Retype(edge, edgeType);
                if(!silence)
                    ConsoleUI.outWriter.WriteLine("Edge \"{0}\" has been retyped from \"{1}\" to \"{2}\".", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge), oldType, edge.Type.PackagePrefixedName);

                return edge;
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to retype edge: " + e.Message);
                return null;
            }
        }

        public bool Redirect(IEdge edge, String direction, INode node)
        {
            if(!GraphExists())
                return false;

            if(edge == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No edge given to redirect command");
                return false;
            }
            if(direction == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No direction given to redirect command");
                return false;
            }
            if(node == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("No node given to redirect command");
                return false;
            }

            try
            {
                bool redirectSource = false;
                if(direction.ToLower() == "source".ToLower())
                {
                    redirectSource = true;
                }
                else if(direction.ToLower() == "target".ToLower())
                {
                    redirectSource = false;
                }
                else
                {
                    ConsoleUI.errorOutWriter.WriteLine("direction must be either \"source\" or \"target\"");
                    return false;
                }

                String oldNodeName;
                if(redirectSource)
                {
                    oldNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Source);
                    curShellProcEnv.ProcEnv.NamedGraph.RedirectSource(edge, node, oldNodeName);
                }
                else
                {
                    oldNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Target);
                    curShellProcEnv.ProcEnv.NamedGraph.RedirectTarget(edge, node, oldNodeName);
                }

                if(!silence)
                {
                    String edgeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge);
                    String directionName = redirectSource ? "source" : "target";
                    String newNodeName = curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node);
                    ConsoleUI.outWriter.WriteLine("Edge \"{0}\" \"{1}\" has been redirected from \"{2}\" to \"{3}\".", edgeName, directionName, oldNodeName, newNodeName);
                }
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to redirect edge: " + e.Message);
                return false;
            }
            return true;
        }

        #endregion graph element commands (delete, retype, redirect)

        #region "show" type related information commands

        public void ShowNodeTypes()
        {
            if(!GraphExists())
                return;

            if(curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.Types.Length == 0)
                ConsoleUI.errorOutWriter.WriteLine("This model has no node types!");
            else
            {
                ConsoleUI.outWriter.WriteLine("Node types:");
                foreach(NodeType type in curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.Types)
                {
                    ConsoleUI.outWriter.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
                }
            }
        }

        public void ShowEdgeTypes()
        {
            if(!GraphExists())
                return;

            if(curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.Types.Length == 0)
                ConsoleUI.errorOutWriter.WriteLine("This model has no edge types!");
            else
            {
                ConsoleUI.outWriter.WriteLine("Edge types:");
                foreach(EdgeType type in curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.Types)
                {
                    ConsoleUI.outWriter.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
                }
            }
        }

        public void ShowSuperTypes(GraphElementType elemType, bool isNode)
        {
            if(elemType == null)
                return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
                ConsoleUI.errorOutWriter.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.PackagePrefixedName + "\" has no super types!");
            else
            {
                ConsoleUI.outWriter.WriteLine("Super types of " + (isNode ? "node" : "edge") + " type \"" + elemType.PackagePrefixedName + "\":");
                foreach(GrGenType type in elemType.SuperTypes)
                {
                    ConsoleUI.outWriter.WriteLine(" - \"" + type.PackagePrefixedName + "\"");
                }
            }
        }

        public void ShowSubTypes(GraphElementType elemType, bool isNode)
        {
            if(elemType == null)
                return;

            if(!elemType.SuperTypes.GetEnumerator().MoveNext())
                ConsoleUI.errorOutWriter.WriteLine((isNode ? "Node" : "Edge") + " type \"" + elemType.PackagePrefixedName + "\" has no super types!");
            else
            {
                ConsoleUI.outWriter.WriteLine("Sub types of " + (isNode ? "node" : "edge") + " type \"{0}\":", elemType.PackagePrefixedName);
                foreach(GrGenType type in elemType.SubTypes)
                {
                    ConsoleUI.outWriter.WriteLine(" - \"{0}\"", type.PackagePrefixedName);
                }
            }
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
                if(onlyType != null && attrType.OwnerType != onlyType)
                    continue;

                if(first)
                {
                    ConsoleUI.outWriter.WriteLine(" - {0,-24} type::attribute", "kind");
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
                ConsoleUI.outWriter.WriteLine(" - {0,-24} {1}::{2}", kind, attrType.OwnerType.PackagePrefixedName, attrType.Name);
            }
            if(first)
                ConsoleUI.errorOutWriter.WriteLine(" - No attribute types found.");
        }

        /// <summary>
        /// Displays the attributes from the given type or all types, if typeName is null.
        /// If showAll is false, inherited attributes are not shown (only applies to a given type)
        /// </summary>
        public void ShowAvailableNodeAttributes(bool showOnly, NodeType nodeType)
        {
            if(nodeType == null)
            {
                ConsoleUI.outWriter.WriteLine("The available attributes for nodes:");
                ShowAvailableAttributes(curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.AttributeTypes, null);
            }
            else
            {
                ConsoleUI.outWriter.WriteLine("The available attributes for {0} \"{1}\":",
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
                ConsoleUI.outWriter.WriteLine("The available attributes for edges:");
                ShowAvailableAttributes(curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.AttributeTypes, null);
            }
            else
            {
                ConsoleUI.outWriter.WriteLine("The available attributes for {0} \"{1}\":",
                    (showOnly ? "edge type only" : "edge type"), edgeType.PackagePrefixedName);
                ShowAvailableAttributes(edgeType.AttributeTypes, showOnly ? edgeType : null);
            }
        }

        #endregion "show" type related information commands

        #region "show" graph and graph element/class object related information commands

        private bool ShowElements<T>(IEnumerable<T> elements) where T : IGraphElement
        {
            if(!elements.GetEnumerator().MoveNext())
                return false;

            ConsoleUI.outWriter.WriteLine("{0,-20} {1}", "name", "type");
            foreach(IGraphElement elem in elements)
            {
                ConsoleUI.outWriter.WriteLine("{0,-20} {1}", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem), elem.Type.PackagePrefixedName);
            }

            return true;
        }

        public void ShowNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists())
                    return;
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;
            }

            IEnumerable<INode> nodes = only ? curShellProcEnv.ProcEnv.NamedGraph.GetExactNodes(nodeType)
                : curShellProcEnv.ProcEnv.NamedGraph.GetCompatibleNodes(nodeType);
            if(!ShowElements(nodes))
                ConsoleUI.errorOutWriter.WriteLine("There are no nodes " + (only ? "compatible to" : "of") + " type \"" + nodeType.PackagePrefixedName + "\"!");
        }

        public void ShowEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists())
                    return;
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.RootType;
            }

            IEnumerable<IEdge> edges = only ? curShellProcEnv.ProcEnv.NamedGraph.GetExactEdges(edgeType)
                : curShellProcEnv.ProcEnv.NamedGraph.GetCompatibleEdges(edgeType);
            if(!ShowElements(edges))
                ConsoleUI.errorOutWriter.WriteLine("There are no edges of " + (only ? "compatible to" : "of") + " type \"" + edgeType.PackagePrefixedName + "\"!");
        }

        public void ShowNumNodes(NodeType nodeType, bool only)
        {
            if(nodeType == null)
            {
                if(!GraphExists())
                    return;
                nodeType = curShellProcEnv.ProcEnv.NamedGraph.Model.NodeModel.RootType;
            }
            if(only)
            {
                ConsoleUI.outWriter.WriteLine("Number of nodes of the type \"" + nodeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumExactNodes(nodeType));
            }
            else
            {
                ConsoleUI.outWriter.WriteLine("Number of nodes compatible to type \"" + nodeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumCompatibleNodes(nodeType));
            }
        }

        public void ShowNumEdges(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
            {
                if(!GraphExists())
                    return;
                edgeType = curShellProcEnv.ProcEnv.NamedGraph.Model.EdgeModel.RootType;
            }
            if(only)
            {
                ConsoleUI.outWriter.WriteLine("Number of edges of the type \"" + edgeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumExactEdges(edgeType));
            }
            else
            {
                ConsoleUI.outWriter.WriteLine("Number of edges compatible to type \"" + edgeType.PackagePrefixedName + "\": "
                    + curShellProcEnv.ProcEnv.NamedGraph.GetNumCompatibleEdges(edgeType));
            }
        }

        public void ShowGraphs()
        {
            if(shellProcEnvs.Count == 0)
            {
                ConsoleUI.errorOutWriter.WriteLine("No graphs available.");
                return;
            }
            ConsoleUI.outWriter.WriteLine("Graphs:");
            for(int i = 0; i < shellProcEnvs.Count; ++i)
            {
                ConsoleUI.outWriter.WriteLine(" - \"" + shellProcEnvs[i].ProcEnv.Graph.Name + "\" (" + i + ")");
            }
        }

        private AttributeType GetElementAttributeType(IAttributeBearer owner, String attributeName)
        {
            AttributeType attrType = owner.Type.GetAttributeType(attributeName);
            if(attrType == null)
            {
                ConsoleUI.outWriter.WriteLine(GetKind(owner) + " \"" + GetNameOfGraphElementOrClassObject(owner)
                    + "\" does not have an attribute \"" + attributeName + "\"!");
                return attrType;
            }
            return attrType;
        }

        public string GetKind(IAttributeBearer owner)
        {
            if(owner is INode)
                return "Node";
            else if(owner is IEdge)
                return "Edge";
            else if(owner is IObject)
                return "Object";
            else if(owner is ITransientObject)
                return "TransientObject";
            else
                throw new Exception("Internal error - unsupported type!");
        }

        public String GetNameOfGraphElementOrClassObject(IAttributeBearer owner)
        {
            if(!GraphExists())
                return null;
            if(owner is IGraphElement)
                return curShellProcEnv.ProcEnv.NamedGraph.GetElementName((IGraphElement)owner);
            else if(owner is IObject)
                return curShellProcEnv.objectNamerAndIndexer.GetName((IObject)owner); // TODO: maybe get from object, previously reverse search
            else
                return null;
        }

        public void ShowElementAttributes(IAttributeBearer owner)
        {
            if(owner == null)
                return;
            if(owner.Type.NumAttributes == 0)
            {
                ConsoleUI.errorOutWriter.WriteLine("{0} \"{1}\" of type \"{2}\" does not have any attributes!", GetKind(owner),
                    GetNameOfGraphElementOrClassObject(owner), owner.Type.PackagePrefixedName);
                return;
            }
            ConsoleUI.outWriter.WriteLine("All attributes for {0} \"{1}\" of type \"{2}\":", GetKind(owner),
                GetNameOfGraphElementOrClassObject(owner), owner.Type.PackagePrefixedName);
            foreach(AttributeType attrType in owner.Type.AttributeTypes)
            {
                ConsoleUI.outWriter.WriteLine(" - {0}::{1} = {2}", attrType.OwnerType.PackagePrefixedName,
                    attrType.Name, EmitHelper.ToStringAutomatic(owner.GetAttribute(attrType.Name), curShellProcEnv.ProcEnv.NamedGraph, false, curShellProcEnv.objectNamerAndIndexer, curShellProcEnv.transientObjectNamerAndIndexer, null));
            }
        }

        public void ShowElementAttribute(IAttributeBearer owner, String attributeName)
        {
            if(owner == null)
                return;

            AttributeType attrType = GetElementAttributeType(owner, attributeName);
            if(attrType == null)
                return;

            ConsoleUI.outWriter.Write("The value of attribute \"" + attributeName + "\" is: \"");
            ConsoleUI.outWriter.Write(EmitHelper.ToStringAutomatic(owner.GetAttribute(attributeName), curShellProcEnv.ProcEnv.NamedGraph, false, curShellProcEnv.objectNamerAndIndexer, curShellProcEnv.transientObjectNamerAndIndexer, null));
            ConsoleUI.outWriter.WriteLine("\".");
        }

        #endregion "show" graph and graph element/class object related information commands

        #region "show graph" command

        public string ShowGraphWith(String programName, String arguments, bool keep)
        {
            if(!GraphExists())
                return "";
            if(nonDebugNonGuiExitOnError)
                return "";

            return seqApplierAndDebugger.ShowGraphWith(programName, arguments, keep);
        }

        #endregion "show graph" command

        #region "show" actions, profile, backend, var commands

        public void ShowActions()
        {
            if(!ActionsExists())
                return;

            if(!curShellProcEnv.ProcEnv.Actions.Actions.GetEnumerator().MoveNext())
            {
                ConsoleUI.errorOutWriter.WriteLine("No actions available.");
                return;
            }

            ConsoleUI.outWriter.WriteLine("Actions:");
            foreach(IAction action in curShellProcEnv.ProcEnv.Actions.Actions)
            {
                ConsoleUI.outWriter.Write(" - " + action.Name);
                if(action.RulePattern.Inputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write("(");
                    bool isFirst = true;
                    foreach(GrGenType inType in action.RulePattern.Inputs)
                    {
                        if(!isFirst)
                            ConsoleUI.outWriter.Write(", ");
                        else
                            isFirst = false;
                        ConsoleUI.outWriter.Write(inType.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }

                if(action.RulePattern.Outputs.Length != 0)
                {
                    ConsoleUI.outWriter.Write(" : (");
                    bool isFirst = true;
                    foreach(GrGenType outType in action.RulePattern.Outputs)
                    {
                        if(!isFirst)
                            ConsoleUI.outWriter.Write(", ");
                        else
                            isFirst = false;
                        ConsoleUI.outWriter.Write(outType.PackagePrefixedName);
                    }
                    ConsoleUI.outWriter.Write(")");
                }
                ConsoleUI.outWriter.WriteLine();
            }
        }

        public void ShowProfile(string action)
        {
            if(!ActionsExists())
                return;

            if(action != null)
            {
                if(curShellProcEnv.ProcEnv.PerformanceInfo.ActionProfiles.ContainsKey(action))
                    ShowProfile(action, curShellProcEnv.ProcEnv.PerformanceInfo.ActionProfiles[action]);
                else
                    ConsoleUI.errorOutWriter.WriteLine("No profile available.");
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
            ConsoleUI.outWriter.WriteLine("profile for action " + name + ":");

            ConsoleUI.outWriter.Write("  calls total: ");
            ConsoleUI.outWriter.WriteLine(profile.callsTotal);
            ConsoleUI.outWriter.Write("  steps of first loop of pattern matcher total: ");
            ConsoleUI.outWriter.WriteLine(profile.loopStepsTotal);
            ConsoleUI.outWriter.Write("  search steps total (in pattern matching, if checking, yielding): ");
            ConsoleUI.outWriter.WriteLine(profile.searchStepsTotal);
            ConsoleUI.outWriter.Write("  search steps total during eval computation): ");
            ConsoleUI.outWriter.WriteLine(profile.searchStepsDuringEvalTotal);
            ConsoleUI.outWriter.Write("  search steps total during exec-ution (incl. actions called): ");
            ConsoleUI.outWriter.WriteLine(profile.searchStepsDuringExecTotal);

            for(int i = 0; i < profile.averagesPerThread.Length; ++i)
            {
                ProfileAverages profileAverages = profile.averagesPerThread[i];

                if(profile.averagesPerThread.Length > 1)
                {
                    ConsoleUI.outWriter.WriteLine("for thread " + i + ":");
                    ConsoleUI.outWriter.Write("  search steps total: ");
                    ConsoleUI.outWriter.WriteLine(profileAverages.searchStepsTotal);
                    ConsoleUI.outWriter.Write("  loop steps total: ");
                    ConsoleUI.outWriter.WriteLine(profileAverages.loopStepsTotal);
                }

                double searchStepsSingle = profileAverages.searchStepsSingle.Get();
                ConsoleUI.outWriter.Write("  search steps until one match: ");
                ConsoleUI.outWriter.WriteLine(searchStepsSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double loopStepsSingle = profileAverages.loopStepsSingle.Get();
                ConsoleUI.outWriter.Write("  loop steps until one match: ");
                ConsoleUI.outWriter.WriteLine(loopStepsSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double searchStepsPerLoopStepSingle = profileAverages.searchStepsPerLoopStepSingle.Get();
                ConsoleUI.outWriter.Write("  search steps per loop step (until one match): ");
                ConsoleUI.outWriter.WriteLine(searchStepsPerLoopStepSingle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));

                double searchStepsMultiple = profileAverages.searchStepsMultiple.Get();
                ConsoleUI.outWriter.Write("  search steps until more than one match: ");
                ConsoleUI.outWriter.WriteLine(searchStepsMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double loopStepsMultiple = profileAverages.loopStepsMultiple.Get();
                ConsoleUI.outWriter.Write("  loop steps until more than one match: ");
                ConsoleUI.outWriter.WriteLine(loopStepsMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                double searchStepsPerLoopStepMultiple = profileAverages.searchStepsPerLoopStepMultiple.Get();
                ConsoleUI.outWriter.Write("  search steps per loop step (until more than one match): ");
                ConsoleUI.outWriter.WriteLine(searchStepsPerLoopStepMultiple.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));

                if(profile.averagesPerThread.Length == 1)
                {
                    double regularWorkAmountDistributionFactor = (1 + loopStepsSingle) * (1 + searchStepsPerLoopStepSingle) / (1 + searchStepsSingle) + (1 + loopStepsMultiple) * (1 + searchStepsPerLoopStepMultiple) / (1 + searchStepsMultiple);
                    double workAmountFactor = Math.Log(1 + loopStepsSingle, 2) * Math.Log(1 + loopStepsSingle, 2) * Math.Log(1 + searchStepsSingle, 2) / 10 + Math.Log(1 + loopStepsMultiple, 2) * Math.Log(1 + loopStepsMultiple, 2) * Math.Log(1 + searchStepsMultiple, 2) / 10;
                    ConsoleUI.outWriter.Write("  parallelization potential: ");
                    ConsoleUI.outWriter.WriteLine((regularWorkAmountDistributionFactor * workAmountFactor).ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                }

                ConsoleUI.outWriter.WriteLine();
            }
        }

        public void ShowBackend()
        {
            if(!BackendExists())
                return;

            ConsoleUI.outWriter.WriteLine("Backend name: {0}", curGraphBackend.Name);
            if(!curGraphBackend.ArgumentNames.GetEnumerator().MoveNext())
                ConsoleUI.errorOutWriter.WriteLine("This backend has no arguments.");

            ConsoleUI.outWriter.WriteLine("Arguments:");
            foreach(String str in curGraphBackend.ArgumentNames)
            {
                ConsoleUI.outWriter.WriteLine(" - {0}", str);
            }
        }

        public void ShowVar(String name)
        {
            // TODO: comment in transientObjectNamerAndIndexer and regenerate test records
            object val = GetVarValue(name);
            if(val != null)
            {
                string type;
                string content;
                if(val.GetType().Name=="Dictionary`2")
                {
                    EmitHelper.ToString((IDictionary)val, out type, out content,
                        null, curShellProcEnv!=null ? curShellProcEnv.ProcEnv.NamedGraph : null, false, 
                        curShellProcEnv!=null ? curShellProcEnv.objectNamerAndIndexer : null, /*curShellProcEnv != null ? curShellProcEnv.transientObjectNamerAndIndexer : */null, null);
                    ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val.GetType().Name == "List`1")
                {
                    EmitHelper.ToString((IList)val, out type, out content,
                        null, curShellProcEnv != null ? curShellProcEnv.ProcEnv.NamedGraph : null, false,
                        curShellProcEnv != null ? curShellProcEnv.objectNamerAndIndexer : null, /*curShellProcEnv != null ? curShellProcEnv.transientObjectNamerAndIndexer : */null, null);
                    ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val.GetType().Name == "Deque`1")
                {
                    EmitHelper.ToString((IDeque)val, out type, out content,
                        null, curShellProcEnv != null ? curShellProcEnv.ProcEnv.NamedGraph : null, false, 
                        curShellProcEnv != null ? curShellProcEnv.objectNamerAndIndexer : null, /*curShellProcEnv != null ? curShellProcEnv.transientObjectNamerAndIndexer : */null, null);
                    ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                    return;
                }
                else if(val is LGSPNode && GraphExists())
                {
                    LGSPNode node = (LGSPNode)val;
                    ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + node.Type.PackagePrefixedName + " is: \"" + curShellProcEnv.ProcEnv.NamedGraph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                else if(val is LGSPEdge && GraphExists())
                {
                    LGSPEdge edge = (LGSPEdge)val;
                    ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + edge.Type.PackagePrefixedName + " is: \"" + curShellProcEnv.ProcEnv.NamedGraph.GetElementName((IGraphElement)val) + "\"");
                    //ShowElementAttributes((IGraphElement)val);
                    return;
                }
                EmitHelper.ToString(val, out type, out content,
                    null, curShellProcEnv!=null ? curShellProcEnv.ProcEnv.NamedGraph : null, false, 
                    curShellProcEnv != null ? curShellProcEnv.objectNamerAndIndexer : null, /*curShellProcEnv != null ? curShellProcEnv.transientObjectNamerAndIndexer : */null, null);
                ConsoleUI.outWriter.WriteLine("The value of variable \"" + name + "\" of type " + type + " is: \"" + content + "\"");
                return;
            }
        }

        #endregion "show" actions, profile, backend, var commands

        #region sequence related and "debug enable/disable" commands

        public void ParseAndDefineSequence(String str, Token tok, out bool noError)
        {
            try
            {
                SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(CurrentActions);
                List<String> warnings = new List<String>();
                ISequenceDefinition seqDef = SequenceParser.ParseSequenceDefinition(str, parserEnv, warnings);
                foreach(string warning in warnings)
                {
                    ConsoleUI.outWriter.WriteLine("The sequence definition at line " + tok.beginLine + " reported back: " + warning);
                }
                ((SequenceDefinition)seqDef).SetNeedForProfilingRecursive(GetEmitProfiling());
                DefineRewriteSequence(seqDef);
                noError = true;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence definition at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to process sequence definition at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to process sequence definition at line " + tok.beginLine + ": " + ex);
                ConsoleUI.outWriter.WriteLine("(You tried to overwrite a compiled sequence?)");
                noError = false;
            }
        }

        private void DefineRewriteSequence(ISequenceDefinition seqDef)
        {
            bool overwritten = ((BaseActions)CurrentActions).RegisterGraphRewriteSequenceDefinition((SequenceDefinition)seqDef);
            if(overwritten)
                ConsoleUI.outWriter.WriteLine("Replaced old sequence definition by new one for " + seqDef.Name);
            else
                ConsoleUI.outWriter.WriteLine("Registered sequence definition for " + seqDef.Name);
        }

        public SequenceExpression ParseSequenceExpression(String str, String ruleOfMatchThis, String typeOfGraphElementThis, Token tok, out bool noError)
        {
            try
            {
                Dictionary<String, String> predefinedVariables = new Dictionary<String, String>();
                predefinedVariables.Add("this", "");
                SequenceParserEnvironmentInterpreted parserEnv = ruleOfMatchThis!=null || typeOfGraphElementThis !=null ?
                    new SequenceParserEnvironmentInterpretedDebugEventCondition(CurrentActions, ruleOfMatchThis, typeOfGraphElementThis) :
                    new SequenceParserEnvironmentInterpreted(CurrentActions);
                List<String> warnings = new List<String>();
                SequenceExpression seqExpr = SequenceParser.ParseSequenceExpression(str, predefinedVariables, parserEnv, warnings);
                foreach(string warning in warnings)
                {
                    ConsoleUI.outWriter.WriteLine("The sequence expression at line " + tok.beginLine + " reported back: " + warning);
                }
                noError = true;
                return seqExpr;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
                return null;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
                return null;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine + ": " + ex);
                noError = false;
                return null;
            }
        }

        public void ApplySequenceExpression(SequenceExpression seqExpr, Token tok, out bool noError)
        {
            try
            {
                seqExpr.SetNeedForProfilingRecursive(GetEmitProfiling());
                seqApplierAndDebugger.ApplyRewriteSequenceExpression(seqExpr, false);
                noError = !seqApplierAndDebugger.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence expression at line " + tok.beginLine + ": " + ex);
                noError = false;
            }
        }

        public Sequence ParseSequence(String str, Token tok, out bool noError)
        {
            try
            {
                SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(CurrentActions);
                List<String> warnings = new List<String>();
                Sequence seq = SequenceParser.ParseSequence(str, parserEnv, warnings);
                foreach(string warning in warnings)
                {
                    ConsoleUI.outWriter.WriteLine("The sequence at line " + tok.beginLine + " reported back: " + warning);
                }
                noError = true;
                return seq;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
                return null;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
                return null;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse sequence at line " + tok.beginLine + ": " + ex);
                noError = false;
                return null;
            }
        }

        public void ApplySequence(Sequence seq, Token tok, out bool noError)
        {
            try
            {
                seq.SetNeedForProfilingRecursive(GetEmitProfiling());
                seqApplierAndDebugger.ApplyRewriteSequence(seq, false);
                noError = !seqApplierAndDebugger.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to execute sequence at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to execute sequence at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to execute sequence at line " + tok.beginLine + ": " + ex);
                noError = false;
            }
        }

        public void DebugSequence(Sequence seq, Token tok, out bool noError)
        {
            try
            {
                seq.SetNeedForProfilingRecursive(GetEmitProfiling());
                seqApplierAndDebugger.DebugRewriteSequence(seq);
                noError = !seqApplierAndDebugger.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug execute sequence at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug execute sequence at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug execute sequence at line " + tok.beginLine + ": " + ex);
                noError = false;
            }
        }

        public void DebugSequenceExpression(SequenceExpression seqExpr, Token tok, out bool noError)
        {
            try
            {
                seqExpr.SetNeedForProfilingRecursive(GetEmitProfiling());
                seqApplierAndDebugger.DebugRewriteSequenceExpression(seqExpr);
                noError = !seqApplierAndDebugger.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug evaluate sequence expression at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug evaluate sequence expression at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to debug evaluate sequence expression at line " + tok.beginLine + ": " + ex);
                noError = false;
            }
        }

        public bool SetDebugMode(bool enable)
        {
            if(enable && seqApplierAndDebugger.GraphViewerType == GraphViewerTypes.MSAGL)
                ConsoleUI.outWriter.WriteLine("Note that the MSAGL-based debugger GUI will be frozen unless you debug a sequence (/graph changes occur).");
            return seqApplierAndDebugger.SetDebugMode(enable);
        }

        #endregion sequence related and "debug enable/disable" commands

        #region "dump" commands

        public void DumpGraph(String filename)
        {
            if(!GraphExists())
                return;

            try
            {
                using(VCGDumper dump = new VCGDumper(filename, curShellProcEnv.VcgFlags, debugLayout))
                    GraphDumper.Dump(curShellProcEnv.ProcEnv.NamedGraph, dump, curShellProcEnv.DumpInfo);
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to dump graph: " + ex.Message);
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
                ConsoleUI.errorOutWriter.Write("Unknown color: " + colorName);
                goto showavail;
            }
            return color;

showavail:
            ConsoleUI.outWriter.WriteLine("\nAvailable colors are:");
            foreach(String name in Enum.GetNames(typeof(GrColor)))
            {
                ConsoleUI.outWriter.WriteLine(" - {0}", name);
            }
            ConsoleUI.outWriter.WriteLine();
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
                ConsoleUI.errorOutWriter.Write("Unknown node shape: " + shapeName);
                goto showavail;
            }
            return shape;

showavail:
            ConsoleUI.outWriter.WriteLine("\nAvailable node shapes are:");
            foreach(String name in Enum.GetNames(typeof(GrNodeShape)))
            {
                ConsoleUI.outWriter.WriteLine(" - {0}", name);
            }
            ConsoleUI.outWriter.WriteLine();
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
                ConsoleUI.errorOutWriter.Write("Unknown edge style: " + styleName);
                goto showavail;
            }
            if(style == GrLineStyle.Invisible)
            {
                ConsoleUI.errorOutWriter.Write("Edge style invisible not available");
                return null;
            }

            return style;

showavail:
            ConsoleUI.outWriter.WriteLine("\nAvailable line styles are:");
            foreach(String name in Enum.GetNames(typeof(GrLineStyle)))
            {
                ConsoleUI.outWriter.WriteLine(" - {0}", name);
            }
            ConsoleUI.outWriter.WriteLine();
            return null;
        }

        public bool SetDumpLabel(GraphElementType type, String label, bool only)
        {
            if(type == null)
                return false;

            if(only)
                curShellProcEnv.DumpInfo.SetElemTypeLabel(type, label);
            else
            {
                foreach(GraphElementType subType in type.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.SetElemTypeLabel(subType, label);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        private bool SetDumpColor(NodeType type, String colorName, bool only, SetNodeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                {
                    setDumpColorProc(subType, (GrColor)color);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        private bool SetDumpColor(EdgeType type, String colorName, bool only, SetEdgeDumpColorProc setDumpColorProc)
        {
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            if(only)
                setDumpColorProc(type, (GrColor) color);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                {
                    setDumpColorProc(subType, (GrColor)color);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDumpNodeTypeColor(NodeType type, String colorName, bool only)
        {
            if(type == null)
                return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeColor);
        }

        public bool SetDumpNodeTypeBorderColor(NodeType type, String colorName, bool only)
        {
            if(type == null)
                return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeBorderColor);
        }

        public bool SetDumpNodeTypeTextColor(NodeType type, String colorName, bool only)
        {
            if(type == null)
                return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetNodeTypeTextColor);
        }

        public bool SetDumpNodeTypeShape(NodeType type, String shapeName, bool only)
        {
            if(type == null)
                return false;

            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null)
                return false;

            if(only)
                curShellProcEnv.DumpInfo.SetNodeTypeShape(type, (GrNodeShape) shape);
            else
            {
                foreach(NodeType subType in type.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.SetNodeTypeShape(subType, (GrNodeShape)shape);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDumpEdgeTypeColor(EdgeType type, String colorName, bool only)
        {
            if(type == null)
                return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetEdgeTypeColor);
        }

        public bool SetDumpEdgeTypeTextColor(EdgeType type, String colorName, bool only)
        {
            if(type == null)
                return false;
            return SetDumpColor(type, colorName, only, curShellProcEnv.DumpInfo.SetEdgeTypeTextColor);
        }

        public bool SetDumpEdgeTypeThickness(EdgeType type, int thickness, bool only)
        {
            if(type == null)
                return false;
            if(thickness < 1 || thickness > 5)
            {
                ConsoleUI.errorOutWriter.WriteLine("Edge thickness must be in [1..5]");
                return false;
            }

            if(only)
                curShellProcEnv.DumpInfo.SetEdgeTypeThickness(type, thickness);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.SetEdgeTypeThickness(subType, thickness);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDumpEdgeTypeLineStyle(EdgeType type, String styleName, bool only)
        {
            if(type == null)
                return false;
            GrLineStyle? style = ParseGrLineStyle(styleName);
            if(style == null)
                return false;

            if(only)
                curShellProcEnv.DumpInfo.SetEdgeTypeLineStyle(type, (GrLineStyle)style);
            else
            {
                foreach(EdgeType subType in type.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.SetEdgeTypeLineStyle(subType, (GrLineStyle)style);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

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
            if(nodeType == null)
                return false;

            if(only)
                curShellProcEnv.DumpInfo.ExcludeNodeType(nodeType);
            else
            {
                foreach(NodeType subType in nodeType.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.ExcludeNodeType(subType);
                }
            }

            return true;
        }

        public bool AddDumpExcludeEdgeType(EdgeType edgeType, bool only)
        {
            if(edgeType == null)
                return false;

            if(only)
                curShellProcEnv.DumpInfo.ExcludeEdgeType(edgeType);
            else
            {
                foreach(EdgeType subType in edgeType.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.ExcludeEdgeType(subType);
                }
            }

            return true;
        }

        public bool AddDumpGroupNodesBy(NodeType nodeType, bool exactNodeType, EdgeType edgeType, bool exactEdgeType,
                NodeType adjNodeType, bool exactAdjNodeType, GroupMode groupMode)
        {
            if(nodeType == null || edgeType == null || adjNodeType == null)
                return false;

            curShellProcEnv.DumpInfo.AddOrExtendGroupNodeType(nodeType, exactNodeType, edgeType, exactEdgeType,
                adjNodeType, exactAdjNodeType, groupMode);

            return true;
        }

        public bool AddDumpInfoTag(GraphElementType type, String attrName, bool only, bool isshort)
        {
            if(type == null)
                return false;

            AttributeType attrType = type.GetAttributeType(attrName);
            if(attrType == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Type \"" + type.PackagePrefixedName + "\" has no attribute \"" + attrName + "\"");
                return false;
            }

            InfoTag infoTag = new InfoTag(attrType, isshort);
            if(only)
                curShellProcEnv.DumpInfo.AddTypeInfoTag(type, infoTag);
            else
            {
                foreach(GraphElementType subtype in type.SubOrSameTypes)
                {
                    curShellProcEnv.DumpInfo.AddTypeInfoTag(subtype, infoTag);
                }
            }

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public void DumpReset()
        {
            if(!GraphExists())
                return;

            curShellProcEnv.DumpInfo.Reset();
            realizers.ReSetElementRealizers();

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();
        }

        #endregion "dump" commands

        #region "debug on" event watching configuration rules

        public void DebugOnAdd(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown comparison function: " + comparison);
                ConsoleUI.outWriter.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Add, message, mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a Debug::add command continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnRem(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown comparison function: " + comparison);
                ConsoleUI.outWriter.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Rem, message, mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a Debug::rem command continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnEmit(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown comparison function: " + comparison);
                ConsoleUI.outWriter.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Emit, message, mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a Debug::emit command continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnHalt(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown comparison function: " + comparison);
                ConsoleUI.outWriter.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Halt, message, mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(break_)
            {
                ConsoleUI.outWriter.Write("Notice: a Debug::halt command causes a break by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnHighlight(string comparison, string message, bool break_)
        {
            SubruleMesssageMatchingMode mode = ParseComparison(comparison);
            if(mode == SubruleMesssageMatchingMode.Undefined)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unknown comparison function: " + comparison);
                ConsoleUI.outWriter.WriteLine("Available are: equals, startsWith, endsWith, contains");
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Highlight, message, mode,
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(break_)
            {
                ConsoleUI.outWriter.Write("Notice: a Debug::highlight causes a break by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnMatch(string actionname, bool break_, SequenceExpression if_)
        {
            if(curShellProcEnv.ProcEnv.Actions.GetAction(actionname) == null)
            {
                ConsoleUI.errorOutWriter.WriteLine("Action unknown: " + actionname);
                return;
            }
            SubruleDebuggingConfigurationRule configurationRule = new SubruleDebuggingConfigurationRule(
                SubruleDebuggingEvent.Match, curShellProcEnv.ProcEnv.Actions.GetAction(actionname),
                break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                if_);
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
        }

        public void DebugOnNew(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule configurationRule;
            if(elemName != null)
            {
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.New, elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            else
            {
                if(graphElemType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown graph element type");
                    return;
                }
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.New, graphElemType, only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a new node/edge continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnDelete(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule configurationRule;
            if(elemName != null)
            {
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Delete, elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            else
            {
                if(graphElemType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown graph element type");
                    return;
                }
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Delete, graphElemType, only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a delete node/edge continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnRetype(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule configurationRule;
            if(elemName != null)
            {
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Retype, elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            else
            {
                if(graphElemType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown graph element type");
                    return;
                }
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.Retype, graphElemType, only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: a retype node/edge continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
            }
        }

        public void DebugOnSetAttributes(GrGenType graphElemType, bool only, string elemName, bool break_, SequenceExpression if_)
        {
            SubruleDebuggingConfigurationRule configurationRule;
            if(elemName != null)
            {
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.SetAttributes, elemName,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            else
            {
                if(graphElemType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unknown graph element type");
                    return;
                }
                configurationRule = new SubruleDebuggingConfigurationRule(
                    SubruleDebuggingEvent.SetAttributes, graphElemType, only,
                    break_ ? SubruleDebuggingDecision.Break : SubruleDebuggingDecision.Continue,
                    if_);
            }
            curShellProcEnv.SubruleDebugConfig.Add(configurationRule);
            if(!break_)
            {
                ConsoleUI.outWriter.Write("Notice: an attribute assignment to a node/edge continues by default -- ");
                ConsoleUI.outWriter.WriteLine(configurationRule.ToString());
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

        #endregion "debug on" event watching configuration rules

        #region "debug" layout and mode commands

        public void DebugWith(string debuggerName)
        {
            if(debuggerName.Equals("msagl", StringComparison.InvariantCultureIgnoreCase))
            {
                bool success = seqApplierAndDebugger.DebugWith(GraphViewerTypes.MSAGL);
                if(success) // error message printed by seqApplierAndDebugger in case of failure
                    ConsoleUI.outWriter.WriteLine("Changed debugger/graph viewer to MSAGL.");
            }
            else if(debuggerName.Equals("ycomp", StringComparison.InvariantCultureIgnoreCase))
            {
                bool success = seqApplierAndDebugger.DebugWith(GraphViewerTypes.YComp);
                if(success) // error message printed by seqApplierAndDebugger in case of failure
                    ConsoleUI.outWriter.WriteLine("Changed debugger/graph viewer to yComp.");
            }
            else
            {
                ConsoleUI.errorOutWriter.WriteLine("\"" + debuggerName + "\" is not a valid debugger/graph viewer name!");
                ConsoleUI.outWriter.WriteLine("Available debuggers/graph viewers are yComp and MSAGL.");
            }
        }

        public void DebugLayout()
        {
            seqApplierAndDebugger.DebugDoLayout();
        }

        public void SetDebugLayout(String layout)
        {
            if(layout == null
                || !GraphViewerClient.IsValidLayout(GraphViewerTypes.YComp, layout) && !GraphViewerClient.IsValidLayout(GraphViewerTypes.MSAGL, layout))
            {
                if(layout != null)
                    ConsoleUI.errorOutWriter.WriteLine("\"" + layout + "\" is not a valid layout name!");
                ConsoleUI.outWriter.WriteLine("Available layouts from yComp:");
                foreach(String layoutName in GraphViewerClient.AvailableLayouts(GraphViewerTypes.YComp))
                {
                    ConsoleUI.outWriter.WriteLine(" - " + layoutName);
                }
                ConsoleUI.outWriter.WriteLine("Available layouts from MSAGL:");
                foreach(String layoutName in GraphViewerClient.AvailableLayouts(GraphViewerTypes.MSAGL))
                {
                    ConsoleUI.outWriter.WriteLine(" - " + layoutName);
                }
                ConsoleUI.outWriter.WriteLine("Current layout: " + debugLayout);
                return;
            }

            seqApplierAndDebugger.SetDebugLayout(layout);

            debugLayout = layout;
        }

        public void GetDebugLayout()
        {
            ConsoleUI.outWriter.WriteLine("Current layout: " + debugLayout);
        }

        public void GetDebugLayoutOptions()
        {
            seqApplierAndDebugger.GetDebugLayoutOptions();
        }

        public void SetDebugLayoutOption(String optionName, String optionValue)
        {
            Dictionary<String, String> optMap;
            if(!debugLayoutOptions.TryGetValue(debugLayout, out optMap))
            {
                optMap = new Dictionary<String, String>();
                debugLayoutOptions[debugLayout] = optMap;
            }

            // only remember option if no error was reported (no error in case debugger was not started yet, option is then remembered for next startup)
            if(seqApplierAndDebugger.SetDebugLayoutOption(optionName, optionValue))
            {
                optMap[optionName] = optionValue;
                ChangeOrientationIfNecessary(optionName, optionValue);
            }
        }

        public void GetDebugOptions()
        {
            seqApplierAndDebugger.GetDebugOptions();
        }

        public void SetDebugOption(String optionName, String optionValue)
        {
            // only remember option if no error was reported (no error in case debugger was not started yet, option is then remembered for next startup)
            if(seqApplierAndDebugger.SetDebugOption(optionName, optionValue))
            {
                debugOptions[optionName] = optionValue;
            }
        }

        private void ChangeOrientationIfNecessary(String optionName, string optionValue)
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

        public void SetDebugMatchModePre(bool enable)
        {
            detailModePreMatchEnabled = enable;
            seqApplierAndDebugger.SetMatchModePre(enable);
        }

        public void SetDebugMatchModePost(bool enable)
        {
            detailModePostMatchEnabled = enable;
            seqApplierAndDebugger.SetMatchModePost(enable);
        }

        public bool SetDebugNodeModeColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            realizers.ChangeNodeColor((ElementMode)mode, (GrColor)color);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugNodeModeBorderColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            realizers.ChangeNodeBorderColor((ElementMode)mode, (GrColor)color);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugNodeModeTextColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            realizers.ChangeNodeTextColor((ElementMode)mode, (GrColor)color);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugNodeModeShape(String modeName, String shapeName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrNodeShape? shape = ParseGrNodeShape(shapeName);
            if(shape == null)
                return false;

            realizers.ChangeNodeShape((ElementMode)mode, (GrNodeShape)shape);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugEdgeModeColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            realizers.ChangeEdgeColor((ElementMode)mode, (GrColor)color);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugEdgeModeTextColor(String modeName, String colorName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrColor? color = ParseGrColor(colorName);
            if(color == null)
                return false;

            realizers.ChangeEdgeTextColor((ElementMode)mode, (GrColor)color);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugEdgeModeThickness(String modeName, int thickness)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            if(thickness < 1 || thickness > 5)
            {
                ConsoleUI.errorOutWriter.WriteLine("Edge thickness must be in [1..5]");
                return false;
            }

            realizers.ChangeEdgeThickness((ElementMode)mode, thickness);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
        }

        public bool SetDebugEdgeModeStyle(String modeName, String styleName)
        {
            ElementMode? mode = ParseElementMode(modeName);
            if(mode == null)
                return false;
            GrLineStyle? style = ParseGrLineStyle(styleName);
            if(style == null)
                return false;

            realizers.ChangeEdgeStyle((ElementMode)mode, (GrLineStyle)style);

            seqApplierAndDebugger.UpdateDebuggerDisplayAsNeeded();

            return true;
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
                ConsoleUI.errorOutWriter.Write("Unknown mode: " + modeName);
                goto showavail;
            }
            return mode;

showavail:
            ConsoleUI.outWriter.WriteLine("\nAvailable modes are:");
            foreach(String name in Enum.GetNames(typeof(ElementMode)))
            {
                ConsoleUI.outWriter.WriteLine(" - {0}", name);
            }
            ConsoleUI.outWriter.WriteLine();
            return null;
        }

        #endregion "debug" layout and mode commands

        #region graph input/output commands (import, export, record, replay, save session)

        private String StringToTextToken(String str)
        {
            if(str.IndexOf('\"') != -1)
                return "\'" + str + "\'";
            else
                return "\"" + str + "\"";
        }

        public void SaveGraph(String filename)
        {
            if(!GraphExists())
                return;

            FileStream file = null;
            StreamWriter sw;
            try
            {
                file = new FileStream(filename, FileMode.Create);
                sw = new StreamWriter(file);
            }
            catch(IOException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to create file \"{0}\": ", e.Message);
                if(file != null)
                    file.Close();
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

                GRSExport.ExportYouMustCloseStreamWriter(graph, sw, "", false, null);

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
                {
                    sw.WriteLine("dump set node only {0} color {1}", nodeTypeColor.Key.PackagePrefixedName, nodeTypeColor.Value);
                }

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeBorderColor in curShellProcEnv.DumpInfo.NodeTypeBorderColors)
                {
                    sw.WriteLine("dump set node only {0} bordercolor {1}", nodeTypeBorderColor.Key.PackagePrefixedName, nodeTypeBorderColor.Value);
                }

                foreach(KeyValuePair<NodeType, GrColor> nodeTypeTextColor in curShellProcEnv.DumpInfo.NodeTypeTextColors)
                {
                    sw.WriteLine("dump set node only {0} textcolor {1}", nodeTypeTextColor.Key.PackagePrefixedName, nodeTypeTextColor.Value);
                }

                foreach(KeyValuePair<NodeType, GrNodeShape> nodeTypeShape in curShellProcEnv.DumpInfo.NodeTypeShapes)
                {
                    sw.WriteLine("dump set node only {0} shape {1}", nodeTypeShape.Key.PackagePrefixedName, nodeTypeShape.Value);
                }

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeColor in curShellProcEnv.DumpInfo.EdgeTypeColors)
                {
                    sw.WriteLine("dump set edge only {0} color {1}", edgeTypeColor.Key.PackagePrefixedName, edgeTypeColor.Value);
                }

                foreach(KeyValuePair<EdgeType, GrColor> edgeTypeTextColor in curShellProcEnv.DumpInfo.EdgeTypeTextColors)
                {
                    sw.WriteLine("dump set edge only {0} textcolor {1}", edgeTypeTextColor.Key.PackagePrefixedName, edgeTypeTextColor.Value);
                }

                if((curShellProcEnv.VcgFlags & VCGFlags.EdgeLabels) == 0)
                    sw.WriteLine("dump set edge labels off");

                foreach(NodeType excludedNodeType in curShellProcEnv.DumpInfo.ExcludedNodeTypes)
                {
                    sw.WriteLine("dump add node only " + excludedNodeType.PackagePrefixedName + " exclude");
                }

                foreach(EdgeType excludedEdgeType in curShellProcEnv.DumpInfo.ExcludedEdgeTypes)
                {
                    sw.WriteLine("dump add edge only " + excludedEdgeType.PackagePrefixedName + " exclude");
                }

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

                foreach(KeyValuePair<GraphElementType, List<InfoTag>> infoTagPair in curShellProcEnv.DumpInfo.InfoTags)
                {
                    String kind;
                    if(infoTagPair.Key.IsNodeType)
                        kind = "node";
                    else
                        kind = "edge";

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
                        {
                            sw.WriteLine("debug set layout option " + option.Key + " " + option.Value);
                        }
                    }
                    sw.WriteLine("debug set layout " + debugLayout);
                }

                sw.WriteLine("# end of graph \"{0}\" saved by grShell", graph.Name);
            }
            catch(IOException e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Write error: Unable to export file: {0}", e.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        public bool Export(List<String> filenameParameters)
        {
            if(!GraphExists())
                return false;

            try
            {
                int startTime = Environment.TickCount;
                Porter.Export(curShellProcEnv.ProcEnv.NamedGraph, filenameParameters);
                ConsoleUI.outWriter.WriteLine("export done after: " + (Environment.TickCount - startTime) + " ms");
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to export graph: " + e.Message);
                return false;
            }
            ConsoleUI.outWriter.WriteLine("Graph \"" + curShellProcEnv.ProcEnv.NamedGraph.Name + "\" exported.");
            return true;
        }

        public bool Import(List<String> filenameParameters)
        {
            if(!BackendExists())
                return false;

            if(newGraphOptions.ExternalAssembliesReferenced.Count > 0 || newGraphOptions.Statistics != null
                || newGraphOptions.KeepDebug || newGraphOptions.Profile 
                || newGraphOptions.NoDebugEvents || newGraphOptions.NoEvents || newGraphOptions.LazyNIC || newGraphOptions.NoInline)
            {
                ConsoleUI.outWriter.WriteLine("Warning: \"new set\" and \"new add\" commands in force are ignored when the actions are built from an import. Ensure that the files are up to date, e.g. by using a \"new graph\" (or even \"new new graph\") before the import.");
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
                ConsoleUI.outWriter.WriteLine("import done after: " + (Environment.TickCount - startTime) + " ms");
                ConsoleUI.outWriter.WriteLine("graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                startTime = Environment.TickCount;

                if(graph is INamedGraph) // grs import returns already named graph
                    curShellProcEnv = new ShellGraphProcessingEnvironment((INamedGraph)graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");
                else // constructor building named graph
                    curShellProcEnv = new ShellGraphProcessingEnvironment(graph, backendFilename, backendParameters, graph.Model.ModelName + ".gm");

                seqApplierAndDebugger.ChangeDebuggerGraphAsNeeded(curShellProcEnv);

                INamedGraph importedNamedGraph = (INamedGraph)curShellProcEnv.ProcEnv.NamedGraph;
                if(actions!=null)
                    ((BaseActions)actions).Graph = importedNamedGraph;
                ConsoleUI.outWriter.WriteLine("shell import done after: " + (Environment.TickCount - startTime) + " ms");
                ConsoleUI.outWriter.WriteLine("shell graph size after import: " + System.GC.GetTotalMemory(true) + " bytes");
                curShellProcEnv.ProcEnv.Actions = actions;
                shellProcEnvs.Add(curShellProcEnv);
                ShowNumNodes(null, false);
                ShowNumEdges(null, false);
            }
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to import graph: " + e.Message);
                return false;
            }

            ConsoleUI.outWriter.WriteLine("Graph \"" + graph.Name + "\" imported.");
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
            catch(Exception e)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to import graph for union: " + e.Message);
                return false;
            }

            // convenience
            INodeModel node_model = this.CurrentGraph.Model.NodeModel;
            IEdgeModel edge_model = this.CurrentGraph.Model.EdgeModel;
            INamedGraph namedGraph = graph as INamedGraph;

            Dictionary<INode, INode> oldToNewNodeMap = new Dictionary<INode, INode>();

            foreach(INode node in graph.Nodes)
            {
                NodeType typ = node_model.GetType(node.Type.PackagePrefixedName);
                INode newNode = CurrentGraph.AddNode(typ);

                String name = null;
                if(namedGraph != null)
                    name = namedGraph.GetElementName(node);
                if(name != null)
                    CurrentGraph.SetElementPrefixName(newNode, name);

                foreach(AttributeType attr in typ.AttributeTypes)
                {
                    try
                    {
                        newNode.SetAttribute(attr.Name, node.GetAttribute(attr.Name));
                    }
                    catch
                    {
                        ConsoleUI.errorOutWriter.WriteLine("failed to copy attribute {0}", attr.Name);
                    }
                }
                oldToNewNodeMap[node] = newNode;
            }

            foreach(IEdge edge in graph.Edges)
            {
                EdgeType typ = edge_model.GetType(edge.Type.PackagePrefixedName);
                INode newSource = oldToNewNodeMap[edge.Source];
                INode newTarget = oldToNewNodeMap[edge.Target];
                if((newSource == null) || (newTarget == null))
                {
                    ConsoleUI.errorOutWriter.WriteLine("failed to retrieve the new node equivalent from the dictionary!!");
                    return false;
                }
                IEdge newEdge = CurrentGraph.AddEdge(typ, newSource, newTarget);

                String name = null;
                if(namedGraph != null)
                    name = namedGraph.GetElementName(edge);
                if(name != null)
                    CurrentGraph.SetElementPrefixName(newEdge, name);

                foreach(AttributeType attr in typ.AttributeTypes)
                {
                    try
                    {
                        newEdge.SetAttribute(attr.Name, edge.GetAttribute(attr.Name));
                    }
                    catch
                    {
                        ConsoleUI.errorOutWriter.WriteLine("failed to copy attribute {0}", attr.Name);
                    }
                }
            }

            Dictionary<IObject, IObject> alreadyVisitedObjectsSet = new Dictionary<IObject, IObject>();

            foreach(INode node in oldToNewNodeMap.Values)
            {
                foreach(AttributeType attr in node.Type.AttributeTypes)
                {
                    if(IsInternalClassObjectUsedInAttribute(attr))
                    {
                        RenameAttribute(node, attr, alreadyVisitedObjectsSet);
                    }
                }
            }

            foreach(INode node in oldToNewNodeMap.Values)
            {
                foreach(IEdge edge in node.Outgoing)
                {
                    foreach(AttributeType attr in edge.Type.AttributeTypes)
                    {
                        if(IsInternalClassObjectUsedInAttribute(attr))
                        {
                            RenameAttribute(edge, attr, alreadyVisitedObjectsSet);
                        }
                    }
                }
            }

            ConsoleUI.outWriter.WriteLine("Graph \"" + graph.Name + "\" imported and added to current graph \"" + CurrentGraph.Name + "\"");
            return true;
        }

        private static bool IsInternalClassObjectUsedInAttribute(AttributeType attrType)
        {
            if(attrType.Kind == AttributeKind.InternalClassObjectAttr)
                return true;
            if(attrType.Kind == AttributeKind.SetAttr
                || attrType.Kind == AttributeKind.MapAttr
                || attrType.Kind == AttributeKind.ArrayAttr
                || attrType.Kind == AttributeKind.DequeAttr)
            {
                if(attrType.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                    return true;
            }
            if(attrType.Kind == AttributeKind.MapAttr)
            {
                if(attrType.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                    return true;
            }
            return false;
        }

        private void RenameAttribute(IAttributeBearer obj, AttributeType attr, IDictionary<IObject, IObject> alreadyVisitedObjectsSet)
        {
            if(attr.Kind == AttributeKind.InternalClassObjectAttr)
            {
                RenameInternalClassObject((IObject)obj.GetAttribute(attr.Name), alreadyVisitedObjectsSet);
            }
            else if(attr.Kind == AttributeKind.SetAttr)
            {
                IDictionary set = (IDictionary)obj.GetAttribute(attr.Name);
                foreach(IObject value in set)
                {
                    RenameInternalClassObject(value, alreadyVisitedObjectsSet);
                }
            }
            else if(attr.Kind == AttributeKind.MapAttr)
            {
                IDictionary map = (IDictionary)obj.GetAttribute(attr.Name);
                if(attr.KeyType.Kind == AttributeKind.InternalClassObjectAttr)
                {
                    foreach(IObject key in map.Keys)
                    {
                        RenameInternalClassObject(key, alreadyVisitedObjectsSet);
                    }
                }
                if(attr.ValueType.Kind == AttributeKind.InternalClassObjectAttr)
                {
                    foreach(IObject value in map.Values)
                    {
                        RenameInternalClassObject(value, alreadyVisitedObjectsSet);
                    }
                }
            }
            else if(attr.Kind == AttributeKind.ArrayAttr)
            {
                IList array = (IList)obj.GetAttribute(attr.Name);
                foreach(IObject value in array)
                {
                    RenameInternalClassObject(value, alreadyVisitedObjectsSet);
                }
            }
            else if(attr.Kind == AttributeKind.DequeAttr)
            {
                IDeque deque = (IDeque)obj.GetAttribute(attr.Name);
                foreach(IObject value in deque)
                {
                    RenameInternalClassObject(value, alreadyVisitedObjectsSet);
                }
            }
        }

        private void RenameInternalClassObject(IObject obj, IDictionary<IObject, IObject> alreadyVisitedObjectsSet)
        {
            if(obj == null)
                return;

            if(alreadyVisitedObjectsSet.ContainsKey(obj))
                return;

            alreadyVisitedObjectsSet.Add(obj, null);

            obj.SetUniqueId(CurrentGraph.GlobalVariables.FetchObjectUniqueId());

            foreach(AttributeType attr in obj.Type.AttributeTypes)
            {
                if(IsInternalClassObjectUsedInAttribute(attr))
                {
                    RenameAttribute(obj, attr, alreadyVisitedObjectsSet);
                }
            }
        }

        public bool Record(String filename, bool actionSpecified, bool start)
        {
            if(!GraphExists())
                return false;

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
                ConsoleUI.errorOutWriter.WriteLine("Unable to change recording status: " + e.Message);
                return false;
            }

            if(curShellProcEnv.ProcEnv.Recorder.IsRecording(filename))
                ConsoleUI.outWriter.WriteLine("Started recording to \"" + filename + "\"");
            else
                ConsoleUI.outWriter.WriteLine("Stopped recording to \"" + filename + "\"");
            return true;
        }

        public bool RecordFlush()
        {
            if(!GraphExists())
                return false;

            curShellProcEnv.ProcEnv.Recorder.Flush();
            
            return true;
        }

        public bool Replay(String filename, String from, String to, GrShellDriver driver)
        {
            if(!GraphExists())
                return false;

            ConsoleUI.outWriter.Write("Replaying \"" + filename + "\"");
            if(from != null)
                ConsoleUI.outWriter.Write(" from \"" + from + "\"");
            if(to != null)
                ConsoleUI.outWriter.Write(" to \"" + to + "\"");
            ConsoleUI.outWriter.WriteLine("..");

            if(driver.Include(filename, from, to))
            {
                ConsoleUI.outWriter.Write("..replaying \"" + filename + "\"");
                if(from != null)
                    ConsoleUI.outWriter.Write(" from \"" + from + "\"");
                if(to != null)
                    ConsoleUI.outWriter.Write(" to \"" + to + "\"");
                ConsoleUI.outWriter.WriteLine(" ended");
                return true;
            }
            else
            {
                ConsoleUI.errorOutWriter.WriteLine("Error in replaying \"" + filename + "\"");
                return false;
            }
        }

        #endregion graph input/output commands (import, export, record, replay, save session)

        #region "validate" commands

        private bool PrintValidateCulprits<T>(IEnumerable<T> elems, bool first) where T : IGraphElement
        {
            foreach(IGraphElement elem in elems)
            {
                if(!first)
                    ConsoleUI.outWriter.Write(',');
                else
                    first = false;
                ConsoleUI.outWriter.Write("\"{0}\"", curShellProcEnv.ProcEnv.NamedGraph.GetElementName(elem));
            }
            return first;
        }

        public bool ValidateWithSequence(String str, Token tok, out bool noError)
        {
            bool validated = false;

            try
            {
                SequenceParserEnvironmentInterpreted parserEnv = new SequenceParserEnvironmentInterpreted(CurrentActions);
                List<String> warnings = new List<String>();
                Sequence seq = SequenceParser.ParseSequence(str, parserEnv, warnings);
                foreach(string warning in warnings)
                {
                    ConsoleUI.outWriter.WriteLine("The validate sequence at line " + tok.beginLine + " reported back: " + warning);
                }
                seq.SetNeedForProfilingRecursive(GetEmitProfiling());
                validated = ValidateWithSequence(seq);
                noError = !seqApplierAndDebugger.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to parse validate sequence at line " + tok.beginLine);
                GrShellSequenceApplierAndDebugger.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to execute validate sequence at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                ConsoleUI.outWriter.WriteLine("Unable to execute validate sequence at line " + tok.beginLine + ": " + ex);
                noError = false;
            }

            return validated;
        }

        public bool Validate(bool strict, bool onlySpecified)
        {
            if(!GraphExists())
                return false;

            ValidationMode mode = ValidationMode.OnlyMultiplicitiesOfMatchingTypes;
            if(strict) mode = onlySpecified ? ValidationMode.StrictOnlySpecified : ValidationMode.Strict;
            List<ConnectionAssertionError> errors;
            bool valid = curShellProcEnv.ProcEnv.NamedGraph.Validate(mode, out errors);
            if(valid)
                ConsoleUI.outWriter.WriteLine("The graph is valid.");
            else
            {
                ConsoleUI.errorOutWriter.WriteLine("The graph is NOT valid:");
                foreach(ConnectionAssertionError error in errors)
                {
                    PrintValidateError(error);
                }
            }

            return valid;
        }

        private void PrintValidateError(ConnectionAssertionError error)
        {
            ValidateInfo valInfo = error.ValidateInfo;
            switch(error.CAEType)
            {
            case CAEType.EdgeNotSpecified:
                {
                    IEdge edge = (IEdge)error.Elem;
                    ConsoleUI.errorOutWriter.WriteLine("  CAE: {0} \"{1}\" -- {2} \"{3}\" {6} {4} \"{5}\" not specified",
                        edge.Source.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Source),
                        edge.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge),
                        edge.Target.Type.PackagePrefixedName, curShellProcEnv.ProcEnv.NamedGraph.GetElementName(edge.Target),
                        edge.Type.Directedness==Directedness.Directed ? "-->" : "--");
                    break;
                }
            case CAEType.NodeTooFewSources:
                {
                    INode node = (INode)error.Elem;
                    ConsoleUI.errorOutWriter.Write("  CAE: {0} \"{1}\" [{2}<{3}] -- {4} ", valInfo.SourceType.PackagePrefixedName,
                        curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceLower, valInfo.EdgeType.PackagePrefixedName);
                    bool first = PrintValidateCulprits(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if(valInfo.EdgeType.Directedness!=Directedness.Directed)
                    {
                        PrintValidateCulprits(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        ConsoleUI.errorOutWriter.WriteLine(" -- {0}", valInfo.TargetType.PackagePrefixedName);
                    }
                    else
                        ConsoleUI.errorOutWriter.WriteLine(" --> {0}", valInfo.TargetType.PackagePrefixedName);
                    break;
                }
            case CAEType.NodeTooManySources:
                {
                    INode node = (INode)error.Elem;
                    ConsoleUI.errorOutWriter.Write("  CAE: {0} \"{1}\" [{2}>{3}] -- {4} ", valInfo.SourceType.PackagePrefixedName,
                        curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges,
                        valInfo.SourceUpper, valInfo.EdgeType.PackagePrefixedName);
                    bool first = PrintValidateCulprits(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), true);
                    if(valInfo.EdgeType.Directedness!=Directedness.Directed)
                    {
                        PrintValidateCulprits(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.TargetType), first);
                        ConsoleUI.errorOutWriter.WriteLine(" -- {0}", valInfo.TargetType.PackagePrefixedName);
                    }
                    else
                        ConsoleUI.errorOutWriter.WriteLine(" --> {0}", valInfo.TargetType.PackagePrefixedName);
                    break;
                }
            case CAEType.NodeTooFewTargets:
                {
                    INode node = (INode)error.Elem;
                    ConsoleUI.errorOutWriter.Write("  CAE: {0} -- {1} ", valInfo.SourceType.PackagePrefixedName,
                             valInfo.EdgeType.PackagePrefixedName);
                    bool first = PrintValidateCulprits(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if(valInfo.EdgeType.Directedness!=Directedness.Directed)
                    {
                        PrintValidateCulprits(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        ConsoleUI.errorOutWriter.WriteLine(" -- {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    }
                    else
                    {
                        ConsoleUI.errorOutWriter.WriteLine(" --> {0} \"{1}\" [{2}<{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetLower);
                    }
                    break;
                }
            case CAEType.NodeTooManyTargets:
                {
                    INode node = (INode)error.Elem;
                    ConsoleUI.errorOutWriter.Write("  CAE: {0} -- {1} ", valInfo.SourceType.PackagePrefixedName,
                        valInfo.EdgeType.PackagePrefixedName);
                    bool first = PrintValidateCulprits(IncomingEdgeFromNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), true);
                    if(valInfo.EdgeType.Directedness!=Directedness.Directed)
                    {
                        PrintValidateCulprits(OutgoingEdgeToNodeOfType(node, valInfo.EdgeType, valInfo.SourceType), first);
                        ConsoleUI.errorOutWriter.WriteLine(" -- {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                    }
                    else
                    {
                        ConsoleUI.errorOutWriter.WriteLine(" --> {0} \"{1}\" [{2}>{3}]", valInfo.TargetType.PackagePrefixedName,
                            curShellProcEnv.ProcEnv.NamedGraph.GetElementName(node), error.FoundEdges, valInfo.TargetUpper);
                    }
                    break;
                }
            }
        }

        private IEnumerable<IEdge> OutgoingEdgeToNodeOfType(INode node, EdgeType edgeType, NodeType targetNodeType)
        {
            foreach(IEdge outEdge in node.GetExactOutgoing(edgeType))
            {
                if(!outEdge.Target.Type.IsA(targetNodeType))
                    continue;
                yield return outEdge;
            }
        }

        private IEnumerable<IEdge> IncomingEdgeFromNodeOfType(INode node, EdgeType edgeType, NodeType sourceNodeType)
        {
            foreach(IEdge inEdge in node.GetExactIncoming(edgeType))
            {
                if(!inEdge.Source.Type.IsA(sourceNodeType))
                    continue;
                yield return inEdge;
            }
        }

        private bool ValidateWithSequence(Sequence seq)
        {
            if(!GraphExists())
                return false;
            if(!ActionsExists())
                return false;

            bool valid = curShellProcEnv.ProcEnv.ValidateWithSequence(seq);
            if(valid)
                ConsoleUI.outWriter.WriteLine("The graph is valid with respect to the given sequence.");
            else
                ConsoleUI.errorOutWriter.WriteLine("The graph is NOT valid with respect to the given sequence!");
            return valid;
        }

        #endregion "validate" commands

        #region "select" commands

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
                    if(!type.IsClass || type.IsNotPublic)
                        continue;
                    if(type.GetInterface("IBackend") != null)
                    {
                        if(backendType != null)
                        {
                            ConsoleUI.errorOutWriter.WriteLine("The given backend contains more than one IBackend implementation!");
                            return false;
                        }
                        backendType = type;
                    }
                }
                if(backendType == null)
                {
                    ConsoleUI.errorOutWriter.WriteLine("The given backend doesn't contain an IBackend implementation!");
                    return false;
                }
                curGraphBackend = (IBackend)Activator.CreateInstance(backendType);
                backendFilename = assemblyName;
                backendParameters = (String[])parameters.ToArray(typeof(String));
                ConsoleUI.outWriter.WriteLine("Backend selected successfully.");
            }
            catch(Exception ex)
            {
                ConsoleUI.errorOutWriter.WriteLine("Unable to load backend: {0}", ex.Message);
                return false;
            }
            return true;
        }

        public void SelectGraphProcEnv(ShellGraphProcessingEnvironment shellGraphProcEnv)
        {
            curShellProcEnv = shellGraphProcEnv ?? curShellProcEnv;
        }

        public bool SelectActions(String actionFilename)
        {
            if(!GraphExists())
                return false;

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
                ConsoleUI.errorOutWriter.WriteLine("Unable to load the actions from \"{0}\":\n{1}", actionFilename, ex.Message);
                return false;
            }
            return true;
        }

        #endregion "select" commands

        #region "custom" commands

        public void CustomGraph(List<String> parameterList)
        {
            if(!GraphExists())
                return;

            if(parameterList.Count == 0 
                || !curShellProcEnv.ProcEnv.NamedGraph.CustomCommandsAndDescriptions.ContainsKey(parameterList[0]))
            {
                if(parameterList.Count > 0)
                    ConsoleUI.errorOutWriter.WriteLine("Unknown command!");

                ConsoleUI.outWriter.WriteLine("Possible commands:");
                foreach(String description in curShellProcEnv.ProcEnv.NamedGraph.CustomCommandsAndDescriptions.Values)
                {
                    ConsoleUI.outWriter.Write(description);
                }
                ConsoleUI.outWriter.WriteLine();

                return;
            }

            try
            {
                curShellProcEnv.ProcEnv.NamedGraph.Custom(parameterList.ToArray());
            }
            catch(ArgumentException e)
            {
                ConsoleUI.errorOutWriter.WriteLine(e.Message);
            }
        }

        public void CustomActions(List<String> parameterList)
        {
            if(!ActionsExists())
                return;

            if(parameterList.Count == 0 
                || (!curShellProcEnv.ProcEnv.Actions.CustomCommandsAndDescriptions.ContainsKey(parameterList[0])
                    && !curShellProcEnv.ProcEnv.CustomCommandsAndDescriptions.ContainsKey(parameterList[0])))
            {
                if(parameterList.Count > 0)
                    ConsoleUI.errorOutWriter.WriteLine("Unknown command!");

                ConsoleUI.outWriter.WriteLine("Possible commands:");
                foreach(String description in curShellProcEnv.ProcEnv.Actions.CustomCommandsAndDescriptions.Values)
                {
                    ConsoleUI.outWriter.Write(description);
                }
                foreach(String description in curShellProcEnv.ProcEnv.CustomCommandsAndDescriptions.Values)
                {
                    ConsoleUI.outWriter.Write(description);
                }
                ConsoleUI.outWriter.WriteLine();

                return;
            }

            if(curShellProcEnv.ProcEnv.Actions.CustomCommandsAndDescriptions.ContainsKey(parameterList[0]))
            {
                try
                {
                    curShellProcEnv.ProcEnv.Actions.Custom(parameterList.ToArray());
                }
                catch(ArgumentException e)
                {
                    ConsoleUI.errorOutWriter.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    curShellProcEnv.ProcEnv.Custom(parameterList.ToArray());
                }
                catch(ArgumentException e)
                {
                    ConsoleUI.errorOutWriter.WriteLine(e.Message);
                }
            }
        }

        #endregion "custom" commands

        #region shell and environment configuration

        public bool Silence
        {
            set
            {
                silence = value;
                if(silence)
                    ConsoleUI.errorOutWriter.WriteLine("Disabled \"new node/edge created successfully\"-messages");
                else
                    ConsoleUI.errorOutWriter.WriteLine("Enabled \"new node/edge created successfully\"-messages");
            }
        }

        public bool SilenceExec
        {
            set { seqApplierAndDebugger.SilenceExec = value; }
        }

        public void SetRandomSeed(int seed)
        {
            Sequence.randomGenerator = new Random(seed);
        }

        public bool RedirectEmit(String filename)
        {
            if(!GraphExists())
                return false;

            if(curShellProcEnv.ProcEnv.EmitWriter != ConsoleUI.outWriter)
                curShellProcEnv.ProcEnv.EmitWriter.Close();
            if(filename == "-")
                curShellProcEnv.ProcEnv.EmitWriter = ConsoleUI.outWriter;
            else
            {
                try
                {
                    curShellProcEnv.ProcEnv.EmitWriter = new StreamWriter(filename);
                }
                catch(Exception ex)
                {
                    ConsoleUI.errorOutWriter.WriteLine("Unable to redirect emit to file \"" + filename + "\":\n" + ex.Message);
                    curShellProcEnv.ProcEnv.EmitWriter = ConsoleUI.outWriter;
                    return false;
                }
            }
            return true;
        }

        #endregion shell and environment configuration

        #region compiler configuration new graph options

        public bool NewGraphAddReference(String externalAssemblyReference)
        {
            if(!newGraphOptions.ExternalAssembliesReferenced.Contains(externalAssemblyReference))
                newGraphOptions.ExternalAssembliesReferenced.Add(externalAssemblyReference);
            return true;
        }

        public bool NewGraphSetKeepDebug(bool on)
        {
            newGraphOptions.KeepDebug = on;
            return true;
        }

        public bool NewGraphSetStatistics(String filepath)
        {
            if(!File.Exists(filepath))
            {
                ConsoleUI.errorOutWriter.WriteLine("Can't find statistics file {0}!", filepath);
                return false;
            }
            newGraphOptions.Statistics = filepath;
            return true;
        }

        public bool NewGraphSetLazyNIC(bool on)
        {
            newGraphOptions.LazyNIC = on;
            return true;
        }

        public bool NewGraphSetNoDebugEvents(bool on)
        {
            newGraphOptions.NoDebugEvents = on;
            SequenceBase.noDebugEvents = on;
            return true;
        }

        public bool NewGraphSetNoEvents(bool on)
        {
            newGraphOptions.NoEvents = on;
            SequenceBase.noEvents = on;
            return true;
        }

        public bool NewGraphSetNoInline(bool on)
        {
            newGraphOptions.NoInline = on;
            return true;
        }

        public bool NewGraphSetProfile(bool on)
        {
            newGraphOptions.Profile = on;
            return true;
        }

        private bool GetEmitProfiling()
        {
            return newGraphOptions.Profile;
        }

        #endregion compiler configuration new graph options
    }
}
