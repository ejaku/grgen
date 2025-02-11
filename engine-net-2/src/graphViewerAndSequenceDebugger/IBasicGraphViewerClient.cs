/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using de.unika.ipd.grGen.libGr;
using System;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    /// <summary>
    /// interface to the basic graph viewer clients (yComp and MSAGL, offering the core graph viewer functionality, utilized by the GraphViewerClient that adds its own functionality)
    /// </summary>
    public interface IBasicGraphViewerClient
    {
        void Close();

        void SleepAndDoEvents(); // only implemented in the graph viewer client of the internal WindowsForms based MSAGL graph viewer, the graph viewer client (proxy) of the external yComp app only sleeps without pumping WindowsForms event

        event ConnectionLostHandler OnConnectionLost; // may come directly, may come only when a Sync is executed, depending on the implementation; may come when the connection to yComp is lost, or when the MSAGL host window is closed

        bool CommandAvailable { get; }

        bool ConnectionLost { get; }

        String ReadCommand();

        void SetLayout(String moduleName);

        /// <summary>
        /// Retrieves the available options of the current layouter of yComp and the current values.
        /// </summary>
        /// <returns>A description of the available options of the current layouter of yComp
        /// and the current values.</returns>
        String GetLayoutOptions();

        /// <summary>
        /// Sets a layout option of the current layouter of yComp.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value.</param>
        /// <returns>"optionset\n", or a error message, if setting the option failed.</returns>
        String SetLayoutOption(String optionName, String optionValue);

        /// <summary>
        /// Forces yComp to relayout the graph.
        /// </summary>
        void ForceLayout();

        /// <summary>
        /// Shows the graph (without relayout).
        /// </summary>
        void Show();

        /// <summary>
        /// Sends a "sync" request and waits for a "sync" answer
        /// </summary>
        bool Sync();

        void AddSubgraphNode(String name, String nrName, String nodeLabel);

        void AddNode(String name, String nrName, String nodeLabel);

        void SetNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString);

        void AddEdge(String edgeName, String srcName, String tgtName, String edgeRealizerName, String edgeLabel);

        void SetEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString);

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        void ChangeNode(String nodeName, String realizer);

        /// <summary>
        /// Sets the edge realizer of the given edge.
        /// If realizer is null, the realizer for the type of the edge is used.
        /// </summary>
        void ChangeEdge(String edgeName, String realizer);

        void SetNodeLabel(String name, String label);

        void SetEdgeLabel(String name, String label);

        void ClearNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString);

        void ClearEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString);

        void DeleteNode(String nodeName);

        void DeleteEdge(String edgeName);

        void RenameNode(String oldName, String newName); // deprecated - used in the past (for retyping, zombie_-names of deleted elements), not needed anymore (esp. due to the current behavior of the named graph), does't work with MSAGL, you have to take care of this if you want to use it again

        void RenameEdge(String oldName, String newName); // deprecated - used in the past (for retyping, zombie_-names of deleted elements), not needed anymore (esp. due to the current behavior of the named graph), does't work with MSAGL, you have to take care of this if you want to use it again

        void ClearGraph();

        void WaitForElement(bool val);

        void MoveNode(String srcName, String tgtName);

        void AddNodeRealizer(String name, GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape);

        void AddEdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle);

        String Encode(String str);
    }
}
