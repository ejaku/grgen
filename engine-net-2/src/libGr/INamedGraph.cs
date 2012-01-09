/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// Environment for sequence exection giving access to graph element names, with null user interface
    /// </summary>
    public class SequenceExecutionEnvironmentNamedGraphOnly : SequenceExecutionEnvironment
    {
        INamedGraph namedGraph;

        public SequenceExecutionEnvironmentNamedGraphOnly(INamedGraph namedGraph)
        {
            this.namedGraph = namedGraph;
        }

        /// <summary>
        /// returns the named graph on which the sequence is to be executed, containing the names
        /// </summary>
        public INamedGraph GetNamedGraph()
        {
            return namedGraph;
        }

        /// <summary>
        /// returns the maybe user altered direction of execution for the sequence given
        /// the randomly chosen direction is supplied; 0: execute left operand first, 1: execute right operand first
        /// </summary>
        public int ChooseDirection(int direction, Sequence seq)
        {
            return direction;
        }

        /// <summary>
        /// returns the maybe user altered sequence to execute next for the sequence given
        /// the randomly chosen sequence is supplied; the object with all available sequences is supplied
        /// </summary>
        public int ChooseSequence(int seqToExecute, List<Sequence> sequences, SequenceNAry seq)
        {
            return seqToExecute;
        }

        /// <summary>
        /// returns the maybe user altered match to execute next for the sequence given
        /// the randomly chosen total match is supplied; the sequence with the rules and matches is supplied
        /// </summary>
        public int ChooseMatch(int totalMatchExecute, SequenceSomeFromSet seq)
        {
            return totalMatchExecute;
        }

        /// <summary>
        /// returns the maybe user altered match to apply next for the sequence given
        /// the randomly chosen match is supplied; the object with all available matches is supplied
        /// </summary>
        public int ChooseMatch(int matchToApply, IMatches matches, int numFurtherMatchesToApply, Sequence seq)
        {
            return matchToApply;
        }

        /// <summary>
        /// returns the maybe user altered random number in the range 0 - upperBound exclusive for the sequence given
        /// the random number chosen is supplied
        /// </summary>
        public int ChooseRandomNumber(int randomNumber, int upperBound, Sequence seq)
        {
            return randomNumber;
        }

        /// <summary>
        /// returns a user chosen/input value of the given type
        /// no random input value is supplied, the user must give a value
        /// </summary>
        public object ChooseValue(string type, Sequence seq)
        {
            return null;
        }

        /// <summary>
        /// informs debugger about the end of a loop iteration, so it can display the state at the end of the iteration
        /// </summary>
        public void EndOfIteration(bool continueLoop, Sequence seq)
        {
        }
    }


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
        /// Gets the graph element for a given name.
        /// </summary>
        /// <param name="name">The name of a graph element.</param>
        /// <returns>The graph element for the given name or null, if there is no graph element with this name.</returns>
        IGraphElement GetGraphElement(String name);

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
        /// The new graph will use the same model and backend as the other.
        /// </summary>
        /// <param name="newName">Name of the new graph.</param>
        /// <returns>A new graph with the same structure and names as this graph.</returns>
        INamedGraph CloneNamed(String newName);

        /// <summary>
        /// Dumps the graph with a given graph dumper and default dump style.
        /// </summary>
        /// <param name="dumper">The graph dumper to be used.</param>
        void Dump(IDumper dumper);
    }
}
