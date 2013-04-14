/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;
using de.unika.ipd.grGen.expression;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Representation of the pattern to search for, 
    /// containing nested alternative, iterated, negative, and independent-patterns, 
    /// plus references to the rules of the used subpatterns.
    /// Accessible via IPatternGraph as meta information to the user about the matching action.
    /// Skeleton data structure for the matcher generation pipeline which stores intermediate results here, 
    /// which saves us from representing the nesting structure again and again in the pipeline's data structures
    /// </summary>
    public class PatternGraph : IPatternGraph
    {
        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>        
        public IPatternNode[] Nodes { get { return nodes; } }

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        public IPatternEdge[] Edges { get { return edges; } }

        /// <summary>
        /// An array of all pattern variables.
        /// </summary>
        public IPatternVariable[] Variables { get { return variables; } }

        /// <summary>
        /// Returns the source pattern node of the given edge, null if edge dangles to the left
        /// </summary>
        public IPatternNode GetSource(IPatternEdge edge)
        {
            return GetSource((PatternEdge)edge);
        }

        /// <summary>
        /// Returns the target pattern node of the given edge, null if edge dangles to the right
        /// </summary>
        public IPatternNode GetTarget(IPatternEdge edge)
        {
            return GetTarget((PatternEdge)edge);
        }

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        public bool[,] HomomorphicNodes { get { return homomorphicNodes; } }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        public bool[,] HomomorphicEdges { get { return homomorphicEdges; } }

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node globally,
        /// i.e. the nodes are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        public bool[,] HomomorphicNodesGlobal { get { return homomorphicNodesGlobal; } }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge globally,
        /// i.e. the edges are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        public bool[,] HomomorphicEdgesGlobal { get { return homomorphicEdgesGlobal; } }

        /// <summary>
        /// A one-dimensional array telling which pattern node is to be matched non-isomorphic against any other node.
        /// </summary>
        public bool[] TotallyHomomorphicNodes { get { return totallyHomomorphicNodes; } }

        /// <summary>
        /// A one-dimensional array telling which pattern edge is to be matched non-isomorphic against any other edge.
        /// </summary>
        public bool[] TotallyHomomorphicEdges { get { return totallyHomomorphicEdges; } }

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public IPatternGraphEmbedding[] EmbeddedGraphs { get { return embeddedGraphs; } }

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public IAlternative[] Alternatives { get { return alternatives; } }

        /// <summary>
        /// An array of iterateds, each iterated is matched as often as possible within the specified bounds.
        /// </summary>
        public IIterated[] Iterateds { get { return iterateds;  } }

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        public IPatternGraph[] NegativePatternGraphs { get { return negativePatternGraphs; } }

        /// <summary>
        /// An array of independent pattern graphs which must get matched in addition to the main pattern
        /// (PACs - Positive Application Conditions).
        /// </summary>
        public IPatternGraph[] IndependentPatternGraphs { get { return independentPatternGraphs; } }

        /// <summary>
        /// The pattern graph which contains this pattern graph, null if this is a top-level-graph
        /// </summary>
        public IPatternGraph EmbeddingGraph { get { return embeddingGraph; } }

        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        public String name;

        /// <summary>
        /// Prefix for name from nesting path
        /// </summary>
        public String pathPrefix;

        /// <summary>
        /// Tells whether the elements from the parent patterns (but not sibling patterns)
        /// should be isomorphy locked, i.e. not again matchable, even in negatives/independents,
        /// which are normally hom to all. This allows to match paths without a specified end,
        /// eagerly, i.e. as long as a successor exists, even in case of a cycles in the graph.
        /// </summary>
        public bool isPatternpathLocked;

        /// <summary>
        /// If this pattern graph is a negative or independent nested inside an iterated,
        /// it breaks the iterated instead of only the current iterated case (if true).
        /// </summary>
        public bool isIterationBreaking;

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>
        public PatternNode[] nodes;

        /// <summary>
        /// An array of all pattern nodes plus the nodes inlined into this pattern.
        /// </summary>
        public PatternNode[] nodesPlusInlined;

        /// <summary>
        /// Normally null. In case this is a pattern created from a graph,
        /// an array of all nodes which created the pattern nodes in nodes, coupled by position.
        /// </summary>
        public INode[] correspondingNodes;

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        public PatternEdge[] edges;

        /// <summary>
        /// An array of all pattern edges plus the edges inlined into this pattern.
        /// </summary>
        public PatternEdge[] edgesPlusInlined;

        /// <summary>
        /// Normally null. In case this is a pattern created from a graph,
        /// an array of all edges which created the pattern edges in edges, coupled by position.
        /// </summary>
        public IEdge[] correspondingEdges;

        /// <summary>
        /// An array of all pattern variables.
        /// </summary>
        public PatternVariable[] variables;

        /// <summary>
        /// An array of all pattern variables plus the variables inlined into this pattern.
        /// </summary>
        public PatternVariable[] variablesPlusInlined;

        /// <summary>
        /// Returns the source pattern node of the given edge, null if edge dangles to the left.
        /// </summary>
        public PatternNode GetSource(PatternEdge edge)
        {
            if (edgeToSourceNode.ContainsKey(edge))
            {
                return edgeToSourceNode[edge];
            }

            if (edge.PointOfDefinition != this
                && embeddingGraph != null)
            {
                return embeddingGraph.GetSource(edge);
            }

            return null;
        }

        /// <summary>
        /// Returns the source pattern node of the given edge, null if edge dangles to the left.
        /// Taking inlined stuff into account.
        /// </summary>
        public PatternNode GetSourcePlusInlined(PatternEdge edge)
        {
            if (edgeToSourceNodePlusInlined.ContainsKey(edge))
            {
                return edgeToSourceNodePlusInlined[edge];
            }

            if (edge.PointOfDefinition != this
                && embeddingGraph != null)
            {
                return embeddingGraph.GetSourcePlusInlined(edge);
            }

            return null;
        }

        /// <summary>
        /// Returns the target pattern node of the given edge, null if edge dangles to the right.
        /// </summary>
        public PatternNode GetTarget(PatternEdge edge)
        {
            if (edgeToTargetNode.ContainsKey(edge))
            {
                return edgeToTargetNode[edge];
            }

            if (edge.PointOfDefinition != this
                && embeddingGraph != null)
            {
                return embeddingGraph.GetTarget(edge);
            }

            return null;
        }

        /// <summary>
        /// Returns the target pattern node of the given edge, null if edge dangles to the right.
        /// Taking inlined stuff into account.
        /// </summary>
        public PatternNode GetTargetPlusInlined(PatternEdge edge)
        {
            if(edgeToTargetNodePlusInlined.ContainsKey(edge))
            {
                return edgeToTargetNodePlusInlined[edge];
            }

            if(edge.PointOfDefinition != this
                && embeddingGraph != null)
            {
                return embeddingGraph.GetTargetPlusInlined(edge);
            }

            return null;
        }

        /// <summary>
        /// Contains the source node of the pattern edges in this graph if specified.
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToSourceNode = new Dictionary<PatternEdge,PatternNode>();

        /// <summary>
        /// Contains the source node of the pattern edges in this graph if specified.
        /// Plus the additional information from inlined stuff.
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToSourceNodePlusInlined = new Dictionary<PatternEdge, PatternNode>();

        /// <summary>
        /// Contains the target node of the pattern edges in this graph if specified.
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToTargetNode = new Dictionary<PatternEdge,PatternNode>();

        /// <summary>
        /// Contains the target node of the pattern edges in this graph if specified.
        /// Plus the additional information from inlined stuff.
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToTargetNodePlusInlined = new Dictionary<PatternEdge, PatternNode>();

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[,] homomorphicNodes;

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[,] homomorphicEdges;

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node globally,
        /// i.e. the nodes are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[,] homomorphicNodesGlobal;

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge globally,
        /// i.e. the edges are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[,] homomorphicEdgesGlobal;

        /// <summary>
        /// An array telling which pattern node is to be matched non-isomorphic(/independent) against any other node.
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[] totallyHomomorphicNodes;

        /// <summary>
        /// An array telling which pattern edge is to be matched non-isomorphic(/independent) against any other edge.
        /// Including the additional information from inlined stuff.
        /// </summary>
        public bool[] totallyHomomorphicEdges;
        
        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public PatternGraphEmbedding[] embeddedGraphs;

        /// <summary>
        /// An array of all embedded graphs plus the embedded graphs inlined into this pattern.
        /// </summary>
        public PatternGraphEmbedding[] embeddedGraphsPlusInlined;

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public Alternative[] alternatives;

        /// <summary>
        /// An array of all alternatives plus the alternatives inlined into this pattern.
        /// </summary>
        public Alternative[] alternativesPlusInlined;

        /// <summary>
        /// An array of iterateds, each iterated is matched as often as possible within the specified bounds.
        /// </summary>
        public Iterated[] iterateds;

        /// <summary>
        /// An array of all iterateds plus the iterateds inlined into this pattern.
        /// </summary>
        public Iterated[] iteratedsPlusInlined;

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        public PatternGraph[] negativePatternGraphs;

        /// <summary>
        /// An array of all negative pattern graphs plus the negative pattern graphs inlined into this pattern.
        /// </summary>
        public PatternGraph[] negativePatternGraphsPlusInlined;

        /// <summary>
        /// An array of independent pattern graphs which must get matched in addition to the main pattern
        /// (PACs - Positive Application Conditions).
        /// </summary>
        public PatternGraph[] independentPatternGraphs;

        /// <summary>
        /// An array of all independent pattern graphs plus the pattern graphs inlined into this pattern.
        /// </summary>
        public PatternGraph[] independentPatternGraphsPlusInlined;

        /// <summary>
        /// The pattern graph which contains this pattern graph, null if this is a top-level-graph 
        /// </summary>
        public PatternGraph embeddingGraph;

        /// <summary>
        /// The conditions used in this pattern graph or it's nested graphs
        /// </summary>
        public PatternCondition[] Conditions;

        /// <summary>
        /// An array of all conditions plus the conditions inlined into this pattern.
        /// </summary>
        public PatternCondition[] ConditionsPlusInlined;

        /// <summary>
        /// The yielding assignments used in this pattern graph or it's nested graphs
        /// </summary>
        public PatternYielding[] Yieldings;

        /// <summary>
        /// An array of all yielding assignments plus the yielding assignments inlined into this pattern.
        /// </summary>
        public PatternYielding[] YieldingsPlusInlined;

        /// <summary>
        /// Tells whether a def entity (node, edge, variable) is existing in this pattern graph
        /// </summary>
        public bool isDefEntityExisting = false;

        /// <summary>
        /// Tells whether a def entity (node, edge, variable) is existing in this pattern graph after inlining
        /// </summary>
        public bool isDefEntityExistingPlusInlined = false;

        /// <summary>
        /// Tells whether a non local def entity (node, edge, variable) is existing in this pattern graph
        /// </summary>
        public bool isNonLocalDefEntityExisting = false;

        /// <summary>
        /// Tells whether a non local def entity (node, edge, variable) is existing in this pattern graph after inlining
        /// </summary>
        public bool isNonLocalDefEntityExistingPlusInlined = false;

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Links to the original pattern graph in case this pattern graph was inlined, otherwise null;
        /// the embeddingGraph of the original pattern graph references the original containing pattern
        /// </summary>
        public PatternGraph originalPatternGraph;

        /// <summary>
        /// Links to the original subpattern embedding which was inlined in case this (negative or independent) pattern graph was inlined, otherwise null.
        /// </summary>
        public PatternGraphEmbedding originalSubpatternEmbedding;

        /// <summary>
        /// Copies all the elements in the pattern graph to the XXXPlusInlined attributes.
        /// This duplicates the pattern, the duplicate is used for the computing and emitting the real code,
        /// whereas the original version is retained as interface to the user (and used in generating the match building).
        /// When subpatterns/embedded graphs get inlined, only the duplicate is changed.
        /// </summary>
        public void PrepareInline()
        {
            // nodes,edges,variables:
            // werden einfach als referenz übernommen, weil zeigen auf das gleiche parent
            // diejenigen die geinlined werden müssen kopiert werden, zeigen auf neues pattern
            nodesPlusInlined = (PatternNode[])nodes.Clone();
            edgesPlusInlined = (PatternEdge[])edges.Clone();
            variablesPlusInlined = (PatternVariable[])variables.Clone();
            edgeToSourceNodePlusInlined.Clear();
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndSource in edgeToSourceNode)
                edgeToSourceNodePlusInlined.Add(edgeAndSource.Key, edgeAndSource.Value);
            edgeToTargetNodePlusInlined.Clear();
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndTarget in edgeToTargetNode)
                edgeToTargetNodePlusInlined.Add(edgeAndTarget.Key, edgeAndTarget.Value);

            // alternative,iterated,negative,independent als referenz übernommen,
            // existieren nur einmal, deren elemente werden geinlined
            alternativesPlusInlined = (Alternative[])alternatives.Clone();
            iteratedsPlusInlined = (Iterated[])iterateds.Clone();
            negativePatternGraphsPlusInlined = (PatternGraph[])negativePatternGraphs.Clone();
            independentPatternGraphsPlusInlined = (PatternGraph[])independentPatternGraphs.Clone();

            // condition, yielding; the inlined ones need to be rewritten
            // parameter passing needs to be rewritten
            ConditionsPlusInlined = (PatternCondition[])Conditions.Clone();
            YieldingsPlusInlined = (PatternYielding[])Yieldings.Clone();

            // subpattern embeddings werden tief kopiert, weil geshared
            // für den fall dass sie geinlined werden, elemente von ihnen geinlined werden
            embeddedGraphsPlusInlined = (PatternGraphEmbedding[])embeddedGraphs.Clone();
        }

        /// <summary>
        /// Instantiates a new PatternGraph object as a copy from an original pattern graph, used for inlining.
        /// We create the inlined elements as clones from the original stuff so a maybe already done inlining pass inside a subpattern does not influence us when we inline that subpattern.
        /// </summary>
        /// <param name="original">The original pattern graph to be copy constructed.</param>
        /// <param name="inlinedSubpatternEmbedding">The embedding which just gets inlined.</param>
        /// <param name="newHost">The pattern graph the new pattern element will be contained in.</param>
        /// <param name="nameSuffix">The suffix to be added to the name of the pattern graph and its elements (to avoid name collisions).</param>
        /// Elements might have been already copied in the containing pattern(s), their copies have to be reused in this case.
        public PatternGraph(PatternGraph original, PatternGraphEmbedding inlinedSubpatternEmbedding, PatternGraph newHost, String nameSuffix,
            Dictionary<PatternNode, PatternNode> nodeToCopy_,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy_,
            Dictionary<PatternVariable, PatternVariable> variableToCopy_)
        {
            // changes should be visible top-down, but not for siblings or parents, so we add to/use clones 
            Dictionary<PatternNode, PatternNode> nodeToCopy = new Dictionary<PatternNode,PatternNode>(nodeToCopy_.Count);
            foreach(KeyValuePair<PatternNode, PatternNode> kvp in nodeToCopy_)
                nodeToCopy.Add(kvp.Key, kvp.Value);
            Dictionary<PatternEdge, PatternEdge> edgeToCopy = new Dictionary<PatternEdge,PatternEdge>(edgeToCopy_.Count);
            foreach(KeyValuePair<PatternEdge, PatternEdge> kvp in edgeToCopy_)
                edgeToCopy.Add(kvp.Key, kvp.Value);
            Dictionary<PatternVariable, PatternVariable> variableToCopy = new Dictionary<PatternVariable, PatternVariable>(variableToCopy_.Count);
            foreach(KeyValuePair<PatternVariable, PatternVariable> kvp in variableToCopy_)
                variableToCopy.Add(kvp.Key, kvp.Value);

            name = original.name + nameSuffix + "_in_" + inlinedSubpatternEmbedding.PointOfDefinition.pathPrefix + inlinedSubpatternEmbedding.PointOfDefinition.name;
            originalSubpatternEmbedding = inlinedSubpatternEmbedding;
            pathPrefix = original.pathPrefix;
            isPatternpathLocked = original.isPatternpathLocked;
            isIterationBreaking = original.isIterationBreaking;

            nodes = (PatternNode[])original.nodes.Clone();
            nodesPlusInlined = new PatternNode[original.nodes.Length];
            for(int i = 0; i < original.nodes.Length; ++i)
            {
                PatternNode node = original.nodes[i];
                if(nodeToCopy.ContainsKey(node))
                {
                    nodesPlusInlined[i] = nodeToCopy[node];
                }
                else
                {
                    PatternNode newNode = new PatternNode(node, inlinedSubpatternEmbedding, this, nameSuffix);
                    nodesPlusInlined[i] = newNode;
                    nodeToCopy[node] = newNode;
                }
            }

            edges = (PatternEdge[])original.edges.Clone();
            edgesPlusInlined = new PatternEdge[original.edges.Length];
            for(int i = 0; i < original.edges.Length; ++i)
            {
                PatternEdge edge = original.edges[i];
                if(edgeToCopy.ContainsKey(edge))
                {
                    edgesPlusInlined[i] = edgeToCopy[edge];
                }
                else
                {
                    PatternEdge newEdge = new PatternEdge(edge, inlinedSubpatternEmbedding, this, nameSuffix);
                    edgesPlusInlined[i] = newEdge;
                    edgeToCopy[edge] = newEdge;
                }
            }

            variables = (PatternVariable[])original.variables.Clone();
            variablesPlusInlined = new PatternVariable[original.variables.Length];
            for(int i = 0; i < original.variables.Length; ++i)
            {
                PatternVariable variable = original.variables[i];
                if(variableToCopy.ContainsKey(variable))
                {
                    variablesPlusInlined[i] = variableToCopy[variable];
                }
                else
                {
                    PatternVariable newVariable = new PatternVariable(variable, inlinedSubpatternEmbedding, this, nameSuffix);
                    variablesPlusInlined[i] = newVariable;
                    variableToCopy[variable] = newVariable;
                }
            }

            PatchUsersOfCopiedElements(nodeToCopy, edgeToCopy, variableToCopy);


            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndSource in original.edgeToSourceNode)
                edgeToSourceNode.Add(edgeAndSource.Key, edgeAndSource.Value);
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndTarget in original.edgeToTargetNode)
                edgeToTargetNode.Add(edgeAndTarget.Key, edgeAndTarget.Value);
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndSource in original.edgeToSourceNode)
                edgeToSourceNodePlusInlined.Add(edgeToCopy[edgeAndSource.Key], nodeToCopy[edgeAndSource.Value]);
            foreach(KeyValuePair<PatternEdge, PatternNode> edgeAndTarget in original.edgeToTargetNode)
                edgeToTargetNodePlusInlined.Add(edgeToCopy[edgeAndTarget.Key], nodeToCopy[edgeAndTarget.Value]);

            homomorphicNodes = (bool[,])original.homomorphicNodes.Clone();
            homomorphicEdges = (bool[,])original.homomorphicEdges.Clone();

            homomorphicNodesGlobal = (bool[,])original.homomorphicNodesGlobal.Clone();
            homomorphicEdgesGlobal = (bool[,])original.homomorphicEdgesGlobal.Clone();

            totallyHomomorphicNodes = (bool[])original.totallyHomomorphicNodes.Clone();
            totallyHomomorphicEdges = (bool[])original.totallyHomomorphicEdges.Clone();


            Conditions = (PatternCondition[])original.Conditions.Clone();
            ConditionsPlusInlined = new PatternCondition[original.Conditions.Length];
            for(int i = 0; i < original.Conditions.Length; ++i)
            {
                PatternCondition cond = original.Conditions[i];
                PatternCondition newCond = new PatternCondition(cond, inlinedSubpatternEmbedding, nameSuffix);
                ConditionsPlusInlined[i] = newCond;
            }

            Yieldings = (PatternYielding[])original.Yieldings.Clone();
            YieldingsPlusInlined = new PatternYielding[original.Yieldings.Length];
            for(int i = 0; i < original.Yieldings.Length; ++i)
            {
                PatternYielding yield = original.Yieldings[i];
                PatternYielding newYield = new PatternYielding(yield, inlinedSubpatternEmbedding, nameSuffix);
                YieldingsPlusInlined[i] = newYield;
            }

            negativePatternGraphs = (PatternGraph[])original.negativePatternGraphs.Clone();
            negativePatternGraphsPlusInlined = new PatternGraph[original.negativePatternGraphs.Length];
            for(int i = 0; i < original.negativePatternGraphs.Length; ++i)
            {
                PatternGraph neg = original.negativePatternGraphs[i];
                PatternGraph newNeg = new PatternGraph(neg, inlinedSubpatternEmbedding, this, nameSuffix,
                    nodeToCopy, edgeToCopy, variableToCopy);
                negativePatternGraphsPlusInlined[i] = newNeg;
            }

            independentPatternGraphs = (PatternGraph[])original.independentPatternGraphs.Clone();
            independentPatternGraphsPlusInlined = new PatternGraph[original.independentPatternGraphs.Length];
            for(int i = 0; i < original.independentPatternGraphs.Length; ++i)
            {
                PatternGraph idpt = original.independentPatternGraphs[i];
                PatternGraph newIdpt = new PatternGraph(idpt, inlinedSubpatternEmbedding, this, nameSuffix,
                    nodeToCopy, edgeToCopy, variableToCopy);
                independentPatternGraphsPlusInlined[i] = newIdpt;
            }

            alternatives = (Alternative[])original.alternatives.Clone();
            alternativesPlusInlined = new Alternative[original.alternatives.Length];
            for(int i = 0; i < original.alternatives.Length; ++i)
            {
                Alternative alt = original.alternatives[i];
                Alternative newAlt = new Alternative(alt, inlinedSubpatternEmbedding, this, nameSuffix, this.pathPrefix + this.name + "_",
                    nodeToCopy, edgeToCopy, variableToCopy);
                alternativesPlusInlined[i] = newAlt;
            }

            iterateds = (Iterated[])original.iterateds.Clone();
            iteratedsPlusInlined = new Iterated[original.iterateds.Length];
            for(int i = 0; i < original.iterateds.Length; ++i)
            {
                Iterated iter = original.iterateds[i];
                Iterated newIter = new Iterated(iter, inlinedSubpatternEmbedding, this, nameSuffix,
                    nodeToCopy, edgeToCopy, variableToCopy);
                iteratedsPlusInlined[i] = newIter;
            }

            embeddedGraphs = (PatternGraphEmbedding[])original.embeddedGraphs.Clone();
            embeddedGraphsPlusInlined = new PatternGraphEmbedding[original.embeddedGraphs.Length];
            for(int i = 0; i < original.embeddedGraphs.Length; ++i)
            {
                PatternGraphEmbedding sub = original.embeddedGraphs[i];
                PatternGraphEmbedding newSub = new PatternGraphEmbedding(sub, inlinedSubpatternEmbedding, this, nameSuffix);
                embeddedGraphsPlusInlined[i] = newSub;
            }

            embeddingGraph = newHost;
            originalPatternGraph = original;

            maybeNullElementNames = new String[0];
            schedules = new ScheduledSearchPlan[1];
            schedulesIncludingNegativesAndIndependents = new ScheduledSearchPlan[1];
            availabilityOfMaybeNullElements = new Dictionary<String, bool>[1];
            FillElementsAvailability(new List<PatternElement>(), 0, new Dictionary<String, bool>(), 0);

            // TODO: das zeugs das vom analyzer berechnet wird, das bei der konstruktion berechnet wird
            patternGraphsOnPathToEnclosedPatternpath = new List<string>();
        }

        public void PatchUsersOfCopiedElements(
            Dictionary<PatternNode, PatternNode> nodeToCopy,
            Dictionary<PatternEdge, PatternEdge> edgeToCopy,
            Dictionary<PatternVariable, PatternVariable> variableToCopy)
        {
            foreach(PatternNode node in nodesPlusInlined)
            {
                if(node.Storage != null)
                    node.Storage.PatchUsersOfCopiedElements(nodeToCopy, edgeToCopy, variableToCopy);
                if(node.StorageIndex != null)
                    node.StorageIndex.PatchUsersOfCopiedElements(nodeToCopy, edgeToCopy, variableToCopy);
                if(node.ElementBeforeCasting is PatternNode)
                {
                    if(node.ElementBeforeCasting!=null && nodeToCopy.ContainsKey((PatternNode)node.ElementBeforeCasting))
                        node.ElementBeforeCasting = nodeToCopy[(PatternNode)node.ElementBeforeCasting];
                }
                else
                {
                    if(node.ElementBeforeCasting!=null && edgeToCopy.ContainsKey((PatternEdge)node.ElementBeforeCasting))
                        node.ElementBeforeCasting = edgeToCopy[(PatternEdge)node.ElementBeforeCasting];
                }
            }
            foreach(PatternEdge edge in edgesPlusInlined)
            {
                if(edge.Storage != null)
                    edge.Storage.PatchUsersOfCopiedElements(nodeToCopy, edgeToCopy, variableToCopy);
                if(edge.StorageIndex != null)
                    edge.StorageIndex.PatchUsersOfCopiedElements(nodeToCopy, edgeToCopy, variableToCopy);
                if(edge.ElementBeforeCasting is PatternNode)
                {
                    if(edge.ElementBeforeCasting!=null && nodeToCopy.ContainsKey((PatternNode)edge.ElementBeforeCasting))
                        edge.ElementBeforeCasting = nodeToCopy[(PatternNode)edge.ElementBeforeCasting];
                }
                else
                {
                    if(edge.ElementBeforeCasting!=null && edgeToCopy.ContainsKey((PatternEdge)edge.ElementBeforeCasting))
                        edge.ElementBeforeCasting = edgeToCopy[(PatternEdge)edge.ElementBeforeCasting];
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructs a PatternGraph object.
        /// </summary>
        /// <param name="name">The name of the pattern graph.</param>
        /// <param name="pathPrefix">Prefix for name from nesting path.</param>
        /// <param name="isPatternpathLocked"> Tells whether the elements from the parent patterns (but not sibling patterns)
        /// should be isomorphy locked, i.e. not again matchable, even in negatives/independents,
        /// which are normally hom to all. This allows to match paths without a specified end,
        /// eagerly, i.e. as long as a successor exists, even in case of a cycles in the graph.</param>
        /// <param name="isIterationBreaking"> If this pattern graph is a negative or independent nested inside an iterated,
        /// it breaks the iterated instead of only the current iterated case (if true).</param>
        /// <param name="nodes">An array of all pattern nodes.</param>
        /// <param name="edges">An array of all pattern edges.</param>
        /// <param name="variables">An array of all pattern variables.</param>
        /// <param name="embeddedGraphs">An array with subpattern embeddings,
        /// i.e. subpatterns and the way they are connected to the pattern.</param>
        /// <param name="alternatives">An array of alternatives, each alternative contains
        /// in its cases the subpatterns to choose out of.</param>
        /// <param name="iterateds">An array of iterated patterns, each iterated is matched as often as possible within the specified bounds.</param>
        /// <param name="negativePatternGraphs">An array of negative pattern graphs which make the
        /// search fail if they get matched (NACs - Negative Application Conditions).</param>
        /// <param name="independentPatternGraphs">An array of independent pattern graphs which make the
        /// search fail if they don't get matched (PACs - Positive sApplication Conditions).</param>
        /// <param name="conditions">The conditions used in this pattern graph or its nested graphs.</param>
        /// <param name="yieldings">The yieldings used in this pattern graph or its nested graphs.</param>
        /// <param name="homomorphicNodes">A two-dimensional array describing which pattern node may
        /// be matched non-isomorphic to which pattern node.</param>
        /// <param name="homomorphicEdges">A two-dimensional array describing which pattern edge may
        /// be matched non-isomorphic to which pattern edge.</param>
        /// <param name="homomorphicNodesGlobal">A two-dimensional array describing which pattern node
        /// may be matched non-isomorphic to which pattern node globally, i.e. the nodes are contained
        /// in different, but locally nested patterns (alternative cases, iterateds).</param>
        /// <param name="homomorphicEdgesGlobal">A two-dimensional array describing which pattern edge
        /// may be matched non-isomorphic to which pattern edge globally, i.e. the edges are contained
        /// in different, but locally nested patterns (alternative cases, iterateds).</param>
        /// <param name="totallyHomomorphicNodes"> An array telling which pattern node is to be matched non-isomorphic(/independent) against any other node.</param>
        /// <param name="totallyHomomorphicEdges"> An array telling which pattern edge is to be matched non-isomorphic(/independent) against any other edge.</param>
        public PatternGraph(String name, String pathPrefix, 
            bool isPatternpathLocked, bool isIterationBreaking,
            PatternNode[] nodes, PatternEdge[] edges,
            PatternVariable[] variables, PatternGraphEmbedding[] embeddedGraphs,
            Alternative[] alternatives, Iterated[] iterateds,
            PatternGraph[] negativePatternGraphs, PatternGraph[] independentPatternGraphs,
            PatternCondition[] conditions, PatternYielding[] yieldings,
            bool[,] homomorphicNodes, bool[,] homomorphicEdges,
            bool[,] homomorphicNodesGlobal, bool[,] homomorphicEdgesGlobal,
            bool[] totallyHomomorphicNodes, bool[] totallyHomomorphicEdges)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.isPatternpathLocked = isPatternpathLocked;
            this.isIterationBreaking = isIterationBreaking;
            this.nodes = nodes;
            this.edges = edges;
            this.variables = variables;
            this.embeddedGraphs = embeddedGraphs;
            this.alternatives = alternatives;
            this.iterateds = iterateds;
            this.negativePatternGraphs = negativePatternGraphs;
            this.independentPatternGraphs = independentPatternGraphs;
            this.Conditions = conditions;
            this.Yieldings = yieldings;
            this.homomorphicNodes = homomorphicNodes;
            this.homomorphicEdges = homomorphicEdges;
            this.homomorphicNodesGlobal = homomorphicNodesGlobal;
            this.homomorphicEdgesGlobal = homomorphicEdgesGlobal;
            this.totallyHomomorphicNodes = totallyHomomorphicNodes;
            this.totallyHomomorphicEdges = totallyHomomorphicEdges;

            // create schedule arrays; normally only one schedule per pattern graph,
            // but each maybe null parameter causes a doubling of the number of schedules
            List<PatternElement> elements = new List<PatternElement>();
            foreach(PatternNode node in nodes) {
                if(node.MaybeNull) {
                    elements.Add(node);
                }
            }
            foreach(PatternEdge edge in edges) {
                if(edge.MaybeNull) {
                    elements.Add(edge);
                }
            } 

            maybeNullElementNames = new String[elements.Count];
            for(int i=0; i<elements.Count; ++i) {
                maybeNullElementNames[i] = elements[i].Name;
            }
            int numCombinations = (int)Math.Pow(2, elements.Count);
            schedules = new ScheduledSearchPlan[numCombinations];
            schedulesIncludingNegativesAndIndependents = new ScheduledSearchPlan[numCombinations];
            availabilityOfMaybeNullElements = new Dictionary<String,bool>[numCombinations];
            FillElementsAvailability(elements, 0, new Dictionary<String, bool>(), 0);
        }

        private int FillElementsAvailability(List<PatternElement> elements, int elementsIndex, 
            Dictionary<String, bool> baseDict, int availabilityIndex)
        {
            if(elementsIndex<elements.Count)
            {
                Dictionary<String, bool> dictTrue = new Dictionary<String, bool>(baseDict);
                dictTrue.Add(elements[elementsIndex].Name, true);
                availabilityIndex = FillElementsAvailability(elements, elementsIndex+1, dictTrue, availabilityIndex);
                Dictionary<String, bool> dictFalse = new Dictionary<String, bool>(baseDict);
                dictFalse.Add(elements[elementsIndex].Name, false);
                availabilityIndex = FillElementsAvailability(elements, elementsIndex+1, dictFalse, availabilityIndex);
            }
            else
            {
                availabilityOfMaybeNullElements[availabilityIndex] = baseDict;
                ++availabilityIndex;
            }
            return availabilityIndex;
        }

        public void AdaptToMaybeNull(int availabilityIndex)
        {
            // for the not available elements, set them to not preset, i.e. pointOfDefintion == patternGraph
            foreach(KeyValuePair<string,bool> elemIsAvail in availabilityOfMaybeNullElements[availabilityIndex])
            {
                if(elemIsAvail.Value) {
                    continue;
                }

                foreach(PatternNode node in nodes)
                {
                    if(node.Name!=elemIsAvail.Key) {
                        continue;
                    }

                    Debug.Assert(node.pointOfDefinition==null);
                    node.pointOfDefinition = this;
                }

                foreach(PatternEdge edge in edges)
                {
                    if(edge.Name!=elemIsAvail.Key) {
                        continue;
                    }

                    Debug.Assert(edge.pointOfDefinition==null);
                    edge.pointOfDefinition = this;
                }
            }
        }

        public void RevertMaybeNullAdaption(int availabilityIndex)
        {
            // revert the not available elements set to not preset again to preset, i.e. pointOfDefintion == null
            foreach(KeyValuePair<string,bool> elemIsAvail in availabilityOfMaybeNullElements[availabilityIndex])
            {
                if(elemIsAvail.Value) {
                    continue;
                }

                foreach(PatternNode node in nodes)
                {
                    if(node.Name!=elemIsAvail.Key) {
                        continue;
                    }

                    Debug.Assert(node.pointOfDefinition==this);
                    node.pointOfDefinition = null;
                }

                foreach(PatternEdge edge in edges)
                {
                    if(edge.Name!=elemIsAvail.Key) {
                        continue;
                    }

                    Debug.Assert(edge.pointOfDefinition==this);
                    edge.pointOfDefinition = null;
                }
            }
        }

        // -------- intermediate results of matcher generation ----------------------------------
        // all of the following is only used in generating the matcher, 
        // the inlined versions plain overwrite the original versions (computed by extending original versions or again from scratch)
        // (with exception of the patternpath informations, which is still safe, as these prevent any inlining)
        
        /// <summary>
        /// Names of the elements which may be null
        /// The following members are ordered along it/generated along this order
        /// </summary>
        public String[] maybeNullElementNames;

        /// <summary>
        /// The schedules for this pattern graph without any nested pattern graphs.
        /// Normally one, but each maybe null action preset causes doubling of schedules
        /// </summary>
        public ScheduledSearchPlan[] schedules;

        /// <summary>
        /// The schedules for this pattern graph including negatives and independents (and subpatterns?).   TODO
        /// Normally one, but each maybe null action preset causes doubling of schedules
        /// </summary>
        public ScheduledSearchPlan[] schedulesIncludingNegativesAndIndependents;

        /// <summary>
        /// For each schedule the availability of the maybe null presets - true if is available, false if not
        /// Empty dictionary if there are no maybe null action preset elements
        /// </summary>
        public Dictionary<String, bool>[] availabilityOfMaybeNullElements;

        //////////////////////////////////////////////////////////////////////////////////////////////
        // if you get a null pointer access on one of these members,
        // it might be because you didn't run a PatternGraphAnalyzer before the LGSPMatcherGenerator

        /// <summary>
        /// The independents nested within this pattern graph,
        /// but only independents not nested within negatives.
        /// Set of pattern graphs, with dummy null pattern graph due to lacking set class in c#.
        /// Contains first the nested independents before inlinig, afterwards the ones after inlining.
        /// </summary>
        public Dictionary<PatternGraph, PatternGraph> nestedIndependents;

        /// <summary>
        /// The nodes from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Includes inlined elements after inlining.
        /// Set of names, with dummy bool due to lacking set class in c#
        /// </summary>
        public Dictionary<String, bool> neededNodes;

        /// <summary>
        /// The edges from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Includes inlined elements after inlining.
        /// Set of names, with dummy bool due to lacking set class in c#
        /// </summary>
        public Dictionary<String, bool> neededEdges;

        /// <summary>
        /// The variables from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Includes inlined elements after inlining.
        /// Map of names to types.
        /// </summary>
        public Dictionary<String, GrGenType> neededVariables;

        /// <summary>
        /// The subpatterns used by this pattern (directly as well as indirectly),
        /// only filled/valid if this is a top level pattern graph of a rule or subpattern.
        /// Set of matching patterns, with dummy null matching pattern due to lacking set class in c#
        /// Contains first the used subpatterns before inlinnig, afterwards the ones after inlining.
        /// </summary>
        public Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpatterns;

        /// <summary>
        /// The names of the pattern graphs which are on a path to some 
        /// enclosed negative/independent with patternpath modifier.
        /// Needed for patternpath processing setup (to write to patternpath matches stack).
        /// </summary>
        public List<String> patternGraphsOnPathToEnclosedPatternpath;

        /// <summary>
        /// Tells whether the pattern graph is on a path from some 
        /// enclosing negative/independent with patternpath modifier.
        /// Needed for patternpath processing setup (to check patternpath matches stack).
        /// </summary>
        public bool isPatternGraphOnPathFromEnclosingPatternpath = false;

        /// <summary>
        /// Gives the maximum negLevel of the pattern reached by negative/independent nesting,
        /// clipped by LGSPElemFlags.MAX_NEG_LEVEL+1 which is the critical point of interest,
        /// this might happen by heavy nesting or by a subpattern call path with
        /// direct or indirect recursion on it including a negative/independent which gets passed.
        /// </summary>
        public int maxNegLevel = 0;

        //////////////////////////////////////////////////////////////////////////////////////////////

        public bool WasInlinedHere(PatternGraphEmbedding embedding)
        {
            for(int i = 0; i < embeddedGraphsPlusInlined.Length; ++i)
            {
                if(embeddedGraphsPlusInlined[i] == embedding)
                {
                    return true;
                }
            }
            return false;
        }

        public void DumpOriginal(SourceBuilder sb)
        {
            sb.AppendFrontFormat("PatternGraph {0}: {1}\n", GetObjectId(this), name);
            sb.Indent();

            foreach(PatternNode node in nodesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}\n", GetObjectId(node), node.name, GetObjectId(node.pointOfDefinition));
            foreach(PatternEdge edge in edgesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}\n", GetObjectId(edge), edge.name, GetObjectId(edge.pointOfDefinition));
            foreach(PatternVariable var in variablesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}\n", GetObjectId(var), var.name, GetObjectId(var.pointOfDefinition));

            foreach(PatternGraphEmbedding sub in embeddedGraphsPlusInlined)
            {
                sb.AppendFrontFormat("sub {0}: {1} of {2}, pod: {3}\n", GetObjectId(sub), sub.name, GetObjectId(sub.matchingPatternOfEmbeddedGraph.patternGraph), GetObjectId(sub.PointOfDefinition));
            }

            foreach(PatternGraph neg in negativePatternGraphsPlusInlined)
            {
                sb.AppendFrontFormat("neg {0}: {1}, parent: {2}\n", GetObjectId(neg), neg.name, GetObjectId(neg.embeddingGraph));
                neg.DumpOriginal(sb);
            }
            foreach(PatternGraph idpt in independentPatternGraphsPlusInlined)
            {
                sb.AppendFrontFormat("idpt {0}: {1}, parent: {2}\n", GetObjectId(idpt), idpt.name, GetObjectId(idpt.embeddingGraph));
                idpt.DumpOriginal(sb);
            }

            foreach(Alternative alt in alternativesPlusInlined)
            {
                sb.AppendFrontFormat("alt {0}: {1}\n", GetObjectId(alt), alt.name);
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.DumpOriginal(sb);
                }
            }
            foreach(Iterated iter in iteratedsPlusInlined)
            {
                sb.AppendFrontFormat("iter {0}: {1}\n", GetObjectId(iter), iter.iteratedPattern.name);
                iter.iteratedPattern.DumpOriginal(sb);
            }

            sb.Unindent();
        }

        public void DumpInlined(SourceBuilder sb)
        {
            sb.AppendFrontFormat("PatternGraph {0}: {1}\n", GetObjectId(this), name);
            sb.Indent();

            foreach(PatternNode node in nodesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}, ori: {3}, ori-embed: {4}\n", GetObjectId(node), node.name, GetObjectId(node.pointOfDefinition), GetObjectId(node.originalNode), GetObjectId(node.originalSubpatternEmbedding));
            foreach(PatternEdge edge in edgesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}, ori: {3}, ori-embed: {4}\n", GetObjectId(edge), edge.name, GetObjectId(edge.pointOfDefinition), GetObjectId(edge.originalEdge), GetObjectId(edge.originalSubpatternEmbedding));
            foreach(PatternVariable var in variablesPlusInlined)
                sb.AppendFrontFormat("{0}: {1}, pod: {2}, ori: {3}, ori-embed: {4}\n", GetObjectId(var), var.name, GetObjectId(var.pointOfDefinition), GetObjectId(var.originalVariable), GetObjectId(var.originalSubpatternEmbedding));

            foreach(PatternGraphEmbedding sub in embeddedGraphsPlusInlined)
            {
                sb.AppendFrontFormat("sub {0}: {1} of {2}, pod: {3}, ori: {4}, ori-embed: {5} inlined: {6}\n", GetObjectId(sub), sub.name, GetObjectId(sub.matchingPatternOfEmbeddedGraph.patternGraph), GetObjectId(sub.PointOfDefinition), GetObjectId(sub.originalEmbedding), GetObjectId(sub.originalSubpatternEmbedding), sub.inlined);
            }

            foreach(PatternGraph neg in negativePatternGraphsPlusInlined)
            {
                sb.AppendFrontFormat("neg {0}: {1}, parent: {2}, ori: {3}, ori-embed: {4}\n", GetObjectId(neg), neg.name, GetObjectId(neg.embeddingGraph), GetObjectId(neg.originalPatternGraph), GetObjectId(neg.originalSubpatternEmbedding));
                neg.DumpInlined(sb);
            }
            foreach(PatternGraph idpt in independentPatternGraphsPlusInlined)
            {
                sb.AppendFrontFormat("idpt {0}: {1}, parent: {2}, ori: {3}, ori-embed: {4}\n", GetObjectId(idpt), idpt.name, GetObjectId(idpt.embeddingGraph), GetObjectId(idpt.originalPatternGraph), GetObjectId(idpt.originalSubpatternEmbedding));
                idpt.DumpInlined(sb);
            }

            foreach(Alternative alt in alternativesPlusInlined)
            {
                sb.AppendFrontFormat("alt {0}: {1}, ori: {2}, ori-embed: {3}\n", GetObjectId(alt), alt.name, GetObjectId(alt.originalAlternative), GetObjectId(alt.originalSubpatternEmbedding));
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.DumpInlined(sb);
                }
            }
            foreach(Iterated iter in iteratedsPlusInlined)
            {
                sb.AppendFrontFormat("iter {0}: {1}, ori: {2}, ori-embed: {3}\n", GetObjectId(iter), iter.iteratedPattern.name, GetObjectId(iter.originalIterated), GetObjectId(iter.originalSubpatternEmbedding));
                iter.iteratedPattern.DumpInlined(sb);
            }

            sb.Unindent();
        }

        private string GetObjectId(object obj)
        {
            if(obj != null)
                return String.Format("0x{0:X}", obj.GetHashCode());
            else
                return "0x00000000";
        }

        public void Explain(SourceBuilder sb, IGraphModel model)
        {
            sb.AppendFrontFormat("{0}:\n", name);
            sb.Indent();

            foreach(ScheduledSearchPlan ssp in schedulesIncludingNegativesAndIndependents)
                ssp.Explain(sb, model);

            foreach(PatternGraphEmbedding sub in embeddedGraphsPlusInlined)
                sb.AppendFrontFormat("subpattern usage {0}:{1}\n", sub.name, sub.EmbeddedGraph.Name);

            foreach(Alternative alt in alternativesPlusInlined)
            {
                sb.AppendFront("alternative {\n");
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.Explain(sb, model);
                }
                sb.AppendFront("}\n");
            }
            foreach(Iterated iter in iteratedsPlusInlined)
            {
                sb.AppendFront("iterated[" + iter.minMatches + ":" + iter.maxMatches + "] {\n"); 
                iter.iteratedPattern.Explain(sb, model);
                sb.AppendFront("}\n");
            }

            sb.Unindent();
        }

        public void ExplainNested(SourceBuilder sb, IGraphModel model)
        {
            foreach(PatternGraphEmbedding sub in embeddedGraphsPlusInlined)
                sb.AppendFrontFormat("subpattern usage {0}:{1}\n", sub.name, sub.EmbeddedGraph.Name);

            foreach(Alternative alt in alternativesPlusInlined)
            {
                sb.AppendFront("alternative {\n");
                foreach(PatternGraph altCase in alt.alternativeCases)
                {
                    altCase.Explain(sb, model);
                }
                sb.AppendFront("}\n");
            }
            foreach(Iterated iter in iteratedsPlusInlined)
            {
                sb.AppendFront("iterated[" + iter.minMatches + ":" + iter.maxMatches + "] {\n");
                iter.iteratedPattern.Explain(sb, model);
                sb.AppendFront("}\n");
            }
        }
    }


    /// <summary>
    /// A description of a GrGen matching pattern, that's a subpattern/subrule or the base for some rule.
    /// </summary>
    public abstract class LGSPMatchingPattern : IMatchingPattern
    {
        /// <summary>
        /// The main pattern graph.
        /// </summary>
        public IPatternGraph PatternGraph { get { return patternGraph; } }

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        public GrGenType[] Inputs { get { return inputs; } }

        /// <summary>
        /// An array of the names corresponding to rule parameters.
        /// </summary>
        public String[] InputNames { get { return inputNames; } }

        /// <summary>
        /// An array of the names of the def elements yielded out of this pattern.
        /// </summary>
        public String[] DefNames { get { return defNames; } }

        /// <summary>
        /// The annotations of the matching pattern (test/rule/subpattern)
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }

        /// <summary>
        /// The main pattern graph.
        /// </summary>
        public PatternGraph patternGraph;

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        public GrGenType[] inputs; // redundant convenience, information already given by/within the PatternElements

        /// <summary>
        /// Names of the rule parameter elements
        /// </summary>
        public string[] inputNames;

        /// <summary>
        /// An array of GrGen types corresponding to def elments yielded out of this pattern.
        /// </summary>
        public GrGenType[] defs; // redundant convenience, information already given by/within the PatternElements

        /// <summary>
        /// Names of the def elements yielded out of this pattern.
        /// </summary>
        public string[] defNames;

        /// <summary>
        /// The annotations of the matching pattern (test/rule/subpattern)
        /// </summary>
        public IDictionary<string, string> annotations = new Dictionary<string, string>();

        /// <summary>
        /// Our name
        /// </summary>
        public string name;

        /// <summary>
        /// A count of using occurances of this subpattern
        /// </summary>
        public int uses;
    }

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public abstract class LGSPRulePattern : LGSPMatchingPattern, IRulePattern
    {
        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        public GrGenType[] Outputs { get { return outputs; } }

        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        public GrGenType[] outputs;

        /// <summary>
        /// An array of the names of the available filters (external extensions)
        /// </summary>
        public String[] Filters { get { return filters; } }

        /// <summary>
        /// An array of the names of the available filters (external extensions)
        /// </summary>
        public String[] filters;
    }

    /// <summary>
    /// Class which instantiates and stores all the rule and subpattern representations ready for iteration
    /// </summary>
    public abstract class LGSPRuleAndMatchingPatterns
    {
        /// <summary>
        /// All the rule representations generated
        /// </summary>
        public abstract LGSPRulePattern[] Rules { get; }

        /// <summary>
        /// All the subrule representations generated
        /// </summary>
        public abstract LGSPMatchingPattern[] Subpatterns { get; }

        /// <summary>
        /// All the rule and subrule representations generated
        /// </summary>
        public abstract LGSPMatchingPattern[] RulesAndSubpatterns { get; }

        /// <summary>
        /// All the defined sequence representations generated
        /// </summary>
        public abstract DefinedSequenceInfo[] DefinedSequences { get; }

        /// <summary>
        /// All the function representations generated
        /// </summary>
        public abstract FunctionInfo[] Functions { get; }

        /// <summary>
        /// All the computation representations generated
        /// </summary>
        public abstract ComputationInfo[] Computations { get; }
    }
}
