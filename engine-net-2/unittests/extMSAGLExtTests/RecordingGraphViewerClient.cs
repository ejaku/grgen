// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.graphViewerAndSequenceDebugger;

namespace extMSAGLExtTests
{
    /// <summary>
    /// IBasicGraphViewerClient mock that just records the graph building calls as text lines
    /// (method name followed by the arguments, separated by spaces), for the checks in the tests.
    /// </summary>
    public class RecordingGraphViewerClient : IBasicGraphViewerClient
    {
        public readonly List<string> Calls = new List<string>();

        void Record(string method, params object[] arguments)
        {
            Calls.Add(method + (arguments.Length > 0 ? " " + string.Join(" ", Array.ConvertAll(arguments, argument => "" + argument)) : ""));
        }

        public int CountCalls(string method)
        {
            int count = 0;
            foreach(string call in Calls)
            {
                if(call == method || call.StartsWith(method + " "))
                    ++count;
            }
            return count;
        }

        public void Close() { }
        public void SleepAndDoEvents() { }
        public event ConnectionLostHandler OnConnectionLost { add { } remove { } }
        public bool CommandAvailable { get { return false; } }
        public bool ConnectionLost { get { return false; } }
        public string ReadCommand() { return ""; }

        public void SetLayout(string moduleName) { Record("SetLayout", moduleName); }
        public string GetLayoutOptions() { Record("GetLayoutOptions"); return ""; }
        public string SetLayoutOption(string optionName, string optionValue) { Record("SetLayoutOption", optionName, optionValue); return ""; }
        public void ForceLayout() { Record("ForceLayout"); }
        public void Show() { Record("Show"); }
        public bool Sync() { Record("Sync"); return true; }

        public void AddSubgraphNode(string name, string nrName, string nodeLabel) { Record("AddSubgraphNode", name, nrName, nodeLabel); }
        public void AddSubgraphNode(string name, string nrName, string nodeLabel, string type) { Record("AddSubgraphNode", name, nrName, nodeLabel, type); }
        public void AddNode(string name, string nrName, string nodeLabel) { Record("AddNode", name, nrName, nodeLabel); }
        public void AddNode(string name, string nrName, string nodeLabel, string type) { Record("AddNode", name, nrName, nodeLabel, type); }
        public void SetNodeAttribute(string name, string ownerTypeName, string attrTypeName, string attrTypeString, string attrValueString)
        { Record("SetNodeAttribute", name, ownerTypeName, attrTypeName, attrTypeString, attrValueString); }

        public void AddEdge(string edgeName, string srcName, string tgtName, string edgeRealizerName, string edgeLabel)
        { Record("AddEdge", edgeName, srcName, tgtName, edgeRealizerName, edgeLabel); }
        public void AddEdge(string edgeName, string srcName, string tgtName, string edgeRealizerName, string edgeLabel, string type)
        { Record("AddEdge", edgeName, srcName, tgtName, edgeRealizerName, edgeLabel, type); }
        public void SetEdgeAttribute(string name, string ownerTypeName, string attrTypeName, string attrTypeString, string attrValueString)
        { Record("SetEdgeAttribute", name, ownerTypeName, attrTypeName, attrTypeString, attrValueString); }

        public void ChangeNode(string nodeName, string realizer) { Record("ChangeNode", nodeName, realizer); }
        public void ChangeEdge(string edgeName, string realizer) { Record("ChangeEdge", edgeName, realizer); }
        public void SetNodeLabel(string name, string label) { Record("SetNodeLabel", name, label); }
        public void SetNodeLabel(string name, string label, string type) { Record("SetNodeLabel", name, label, type); }
        public void SetEdgeLabel(string name, string label) { Record("SetEdgeLabel", name, label); }
        public void SetEdgeLabel(string name, string label, string type) { Record("SetEdgeLabel", name, label, type); }
        public void ClearNodeAttribute(string name, string ownerTypeName, string attrTypeName, string attrTypeString)
        { Record("ClearNodeAttribute", name, ownerTypeName, attrTypeName, attrTypeString); }
        public void ClearEdgeAttribute(string name, string ownerTypeName, string attrTypeName, string attrTypeString)
        { Record("ClearEdgeAttribute", name, ownerTypeName, attrTypeName, attrTypeString); }
        public void DeleteNode(string nodeName) { Record("DeleteNode", nodeName); }
        public void DeleteEdge(string edgeName) { Record("DeleteEdge", edgeName); }
        public void RenameNode(string oldName, string newName) { Record("RenameNode", oldName, newName); }
        public void RenameEdge(string oldName, string newName) { Record("RenameEdge", oldName, newName); }
        public void ClearGraph() { Record("ClearGraph"); }
        public void WaitForElement(bool val) { Record("WaitForElement", val); }
        public void MoveNode(string srcName, string tgtName) { Record("MoveNode", srcName, tgtName); }

        public void AddNodeRealizer(string name, GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape)
        { Record("AddNodeRealizer", name, borderColor, color, textColor, nodeShape); }
        public void AddEdgeRealizer(string name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        { Record("AddEdgeRealizer", name, color, textColor, lineWidth, lineStyle); }

        public string Encode(string str) { return str; }
    }
}
