using System;

namespace de.unika.ipd.grGen.libGr
{
    /// <summary>
    /// An element of a rule pattern.
    /// </summary>
    public interface IPatternElement
    {
        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        String Name { get; }

/*        /// <summary>
        /// The GrGen type of the element.
        /// </summary>
        IType Type { get; }*/
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
    public interface IPatternNode : IPatternElement
    {
        // currently empty
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    public interface IPatternEdge : IPatternElement
    {
        /// <summary>
        /// The source pattern node of the edge.
        /// </summary>
        IPatternNode Source { get; }
        
        /// <summary>
        /// The target pattern node of the edge.
        /// </summary>
        IPatternNode Target { get; }
    }
    
    /// <summary>
    /// A pattern graph.
    /// </summary>
    public interface IPatternGraph
    {
        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        String Name { get; }

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>
        IPatternNode[] Nodes { get; }

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        IPatternEdge[] Edges { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        bool[,] HomomorphicNodes { get; }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        bool[,] HomomorphicEdges { get; }

        /// <summary>
        /// An array specifiying for each pattern node, whether it may be matched non-isomorphic to all other nodes.
        /// </summary>
        bool[] HomomorphicToAllNodes { get; }

        /// <summary>
        /// An array specifiying for each pattern edge, whether it may be matched non-isomorphic to all other edges.
        /// </summary>
        bool[] HomomorphicToAllEdges { get; }

        /// <summary>
        /// An array specifiying for each pattern node, whether it must be matched isomorphic to all other nodes.
        /// </summary>
        bool[] IsomorphicToAllNodes { get; }

        /// <summary>
        /// An array specifiying for each pattern edge, whether it must be matched isomorphic to all other edges.
        /// </summary>
        bool[] IsomorphicToAllEdges { get; }

        /// <summary>
        /// Not yet implemented...
        /// </summary>
        IPatternGraphEmbedding[] EmbeddedGraphs { get; } // niy
    }

    /// <summary>
    /// Not yet implemented...
    /// </summary>
    public interface IPatternGraphEmbedding // niy
    {
        /// <summary>
        /// The name of the usage of the subgraph.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Not yet implemented...
        /// </summary>
        IPatternGraph EmbeddedGraph { get; }

        /// <summary>
        /// Not yet implemented...
        /// </summary>
        IPatternElement[] Connections { get; }
    }

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public interface IRulePattern
    {
        /// <summary>
        /// The main pattern graph.
        /// </summary>
        IPatternGraph PatternGraph { get; }

        /// <summary>
        /// An array of pattern graphs of NACs (Negative Application Conditions).
        /// </summary>
        IPatternGraph[] NegativePatternGraphs { get; }

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        GrGenType[] Inputs { get; }

        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        GrGenType[] Outputs { get; }

        /// <summary>
        /// The names of the nodes added in Modify() in order of adding
        /// </summary>
        String[] AddedNodeNames { get; }

        /// <summary>
        /// The names of the edges added in Modify() in order of adding
        /// </summary>
        String[] AddedEdgeNames { get; }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match (rewrite part).
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <param name="graph">The host graph for this modification.</param>
        /// <param name="match">The match which is used for this rewrite.</param>
        /// <returns>An array of elements returned by the rule</returns>
        IGraphElement[] Modify(IGraph graph, IMatch match);
    }
}

