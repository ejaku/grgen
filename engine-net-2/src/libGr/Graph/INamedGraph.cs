/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, Moritz Kroll

using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An named IGraph (which is an attributed, typed and directed multigraph with multiple inheritance on node and edge types),
    /// with a unique name assigned to each node and edge; allowing to access an element by name and a name by element.
    /// </summary>
    public interface INamedGraph : IGraph
    {
        /// <summary>
        /// Sets the name for a graph element. Any previous name will be overwritten.
        /// </summary>
        /// <param name="elem">The graph element to be named.</param>
        /// <param name="name">The new name for the graph element.</param>
        void SetElementName(IGraphElement elem, String name);

        /// <summary>
        /// Sets a name of the form prefix + number for the graph element,
        /// with number being the first number from 0 on yielding an element name not already available in the graph
        /// </summary>
        void SetElementPrefixName(IGraphElement element, String prefix);

        /// <summary>
        /// Returns the name for the given element.
        /// </summary>
        /// <param name="elem">Element of which the name is to be found</param>
        /// <returns>The name of the given element</returns>
        String GetElementName(IGraphElement elem);

        /// <summary>
        /// Gets the graph element for the given name.
        /// </summary>
        /// <param name="name">The name of a graph element.</param>
        /// <returns>The graph element for the given name or null, if there is no graph element with this name.</returns>
        IGraphElement GetGraphElement(String name);

        /// <summary>
        /// Gets the node for the given name.
        /// </summary>
        /// <param name="name">The name of a node.</param>
        /// <returns>The node for the given name or null, if there is no node with this name.</returns>
        INode GetNode(String name);

        /// <summary>
        /// Gets the graph element for the given name.
        /// </summary>
        /// <param name="name">The name of an edge.</param>
        /// <returns>The edge for the given name or null, if there is no edge with this name.</returns>
        IEdge GetEdge(String name);


        /// <summary>
        /// Adds an existing node to the graph and names it.
        /// </summary>
        /// <param name="node">The existing node.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        void AddNode(INode node, String elemName);

        /// <summary>
        /// Adds a new named node to the graph.
        /// </summary>
        /// <param name="nodeType">The node type for the new node.</param>
        /// <param name="elemName">The name for the new node or null if it is to be auto-generated.</param>
        /// <returns>The newly created node.</returns>
        INode AddNode(NodeType nodeType, String elemName);

        /// <summary>
        /// Adds an existing edge to the graph, names it, and assigns it to the given variable.
        /// </summary>
        /// <param name="edge">The edge to be added.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        void AddEdge(IEdge edge, String elemName);

        /// <summary>
        /// Adds a new named edge to the graph and assigns it to the given variable.
        /// </summary>
        /// <param name="edgeType">The edge type for the new edge.</param>
        /// <param name="source">The source of the edge.</param>
        /// <param name="target">The target of the edge.</param>
        /// <param name="elemName">The name for the edge or null if it is to be auto-generated.</param>
        /// <returns>The newly created edge.</returns>
        IEdge AddEdge(EdgeType edgeType, INode source, INode target, String elemName);


        /// <summary>
        /// Duplicates a named graph.
        /// The new graph will use the same model as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure and names as this graph.</returns>
        INamedGraph CloneNamed(String newName);


        /// <summary>
        /// Merges the source node into the target node,
        /// i.e. all edges incident to the source node are redirected to the target node, then the source node is deleted.
        /// </summary>
        /// <param name="target">The node which remains after the merge.</param>
        /// <param name="source">The node to be merged.</param>
        void Merge(INode target, INode source);

        /// <summary>
        /// Changes the source node of the edge from the old source to the given new source.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        void RedirectSource(IEdge edge, INode newSource);

        /// <summary>
        /// Changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        void RedirectTarget(IEdge edge, INode newTarget);

        /// <summary>
        /// Changes the source of the edge from the old source to the given new source,
        /// and changes the target node of the edge from the old target to the given new target.
        /// </summary>
        /// <param name="edge">The edge to redirect.</param>
        /// <param name="newSource">The new source node of the edge.</param>
        /// <param name="newTarget">The new target node of the edge.</param>
        void RedirectSourceAndTarget(IEdge edge, INode newSource, INode newTarget);
    }
}
