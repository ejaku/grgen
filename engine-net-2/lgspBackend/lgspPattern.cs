using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// An element of a rule pattern.
    /// </summary>
    [DebuggerDisplay("PatternElement ({name}:{TypeID})")]
    public abstract class PatternElement : IPatternElement
    {
        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        public String UnprefixedName { get { return unprefixedName; } }

        public PatternGraph PointOfDefinition; // the pattern this element gets matched (null if rule parameter)

        public int TypeID;
        public String name;
        public String unprefixedName;
        public GrGenType[] AllowedTypes;
        public bool[] IsAllowedType;
        public float Cost; // default cost/priority from frontend, user priority if given
        public int ParameterIndex; // only valid if pattern element is handed in as rule parameter

        /// <summary>
        /// Instantiates a new PatternElement object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern element</param>
        /// <param name="name">The name of the pattern element</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternElement(int typeID, String name, String unprefixedName, 
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex)
        {
            this.TypeID = typeID;
            this.name = name;
            this.unprefixedName = unprefixedName;
            this.AllowedTypes = allowedTypes;
            this.IsAllowedType = isAllowedType;
            this.Cost = cost;
            this.ParameterIndex = parameterIndex;
        }
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
    [DebuggerDisplay("PatternNode ({name}:{TypeID})")]
    public class PatternNode : PatternElement, IPatternNode
    {
        /// <summary>
        /// plan graph node corresponding to this pattern node, used in plan graph generation, just hacked into this place
        /// </summary>
        public PlanNode TempPlanMapping;

        /// <summary>
        /// Instantiates a new PatternNode object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern node</param>
        /// <param name="name">The name of the pattern node</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternNode(int typeID, String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex)
            : base(typeID, name, unprefixedName, allowedTypes, isAllowedType, cost, parameterIndex)
        {
        }
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    [DebuggerDisplay("PatternEdge ({ToString()})")]
    public class PatternEdge : PatternElement, IPatternEdge
    {
        /// <summary>
        /// The source pattern node of the edge.
        /// </summary>
        public IPatternNode Source { get { return source; } }

        /// <summary>
        /// The target pattern node of the edge.
        /// </summary>
        public IPatternNode Target { get { return target; } }

        /// <summary>
        /// The source pattern node of the edge.
        /// </summary>
        public PatternNode source;

        /// <summary>
        /// The target pattern node of the edge.
        /// </summary>
        public PatternNode target;

        /// <summary>
        /// Indicates, whether this pattern edge should be matched with a fixed direction or not.
        /// </summary>
        public bool fixedDirection;

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="source">The source pattern node for this edge.</param>
        /// <param name="target">The target pattern node for this edge.</param>
        /// <param name="fixedDirection">Whether this pattern edge should be matched with a fixed direction or not.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="name">The name of the pattern edge.</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternEdge(PatternNode source, PatternNode target, bool fixedDirection,
            int typeID, String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType,
            float cost, int parameterIndex)
            : base(typeID, name, unprefixedName, allowedTypes, isAllowedType, cost, parameterIndex)
        {
            this.source = source;
            this.target = target;
            this.fixedDirection = fixedDirection;
        }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return source.Name + " -" + Name + ":" + TypeID + "-> " + target.Name;
        }
    }

    public class Condition
    {
        public int ID;
        public String[] NeededNodes;
        public String[] NeededEdges;

        public Condition(int id, String[] neededNodes, String[] neededEdges)
        {
            ID = id;
            NeededNodes = neededNodes;
            NeededEdges = neededEdges;
        }
    }

    /// <summary>
    /// Representation of the pattern to search for, 
    /// containing nested alternative and negative-patterns, plus references to the rules of the used subpatterns.
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
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        public bool[,] HomomorphicNodes { get { return homomorphicNodes; } }

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        public bool[,] HomomorphicEdges { get { return homomorphicEdges; } }

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public IPatternGraphEmbedding[] EmbeddedGraphs { get { return embeddedGraphs; } }

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public IAlternative[] Alternatives { get { return alternatives; } }

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        public IPatternGraph[] NegativePatternGraphs { get { return negativePatternGraphs; } }

        /// <summary>
        /// The name of the pattern graph
        /// </summary>
        public String name;

        public String pathPrefix;
        public bool isIndependent;

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>
        public PatternNode[] nodes;

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        public PatternEdge[] edges;

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        public bool[,] homomorphicNodes;

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        public bool[,] homomorphicEdges;

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public PatternGraphEmbedding[] embeddedGraphs;

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public Alternative[] alternatives;

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        public PatternGraph[] negativePatternGraphs;

        public Condition[] Conditions;

        public ScheduledSearchPlan Schedule;
        public ScheduledSearchPlan ScheduleIncludingNegatives;

        public PatternGraph(String name, String pathPrefix, bool isIndependent,
            PatternNode[] nodes, PatternEdge[] edges,
            PatternGraphEmbedding[] embeddedGraphs, Alternative[] alternatives, 
            PatternGraph[] negativePatternGraphs, Condition[] conditions,
            bool[,] homomorphicNodes, bool[,] homomorphicEdges)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.isIndependent = isIndependent;
            this.nodes = nodes;
            this.edges = edges;
            this.embeddedGraphs = embeddedGraphs;
            this.alternatives = alternatives;
            this.negativePatternGraphs = negativePatternGraphs;
            this.Conditions = conditions;
            this.homomorphicNodes = homomorphicNodes;
            this.homomorphicEdges = homomorphicEdges;
        }
    }

    /// <summary>
    /// Embedding of a subpattern into it's containing pattern
    /// </summary>
    public class PatternGraphEmbedding : IPatternGraphEmbedding
    {
        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The embedded subpattern
        /// </summary>
        public IPatternGraph EmbeddedGraph { get { return ruleOfEmbeddedGraph.patternGraph; } }

        /// <summary>
        /// An array with the connections telling how the subpattern is connected to the containing pattern,
        /// that are the pattern elements of the containing pattern used for that purpose
        /// </summary>
        public IPatternElement[] Connections { get { return connections; } }

        public PatternGraph PointOfDefinition; // the pattern this complex subpattern element gets matched

        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        public String name;

        /// <summary>
        /// The embedded subpattern
        /// </summary>
        public LGSPRulePattern ruleOfEmbeddedGraph;

        /// <summary>
        /// An array with the connections telling how the subpattern is connected to the containing pattern,
        /// that are the pattern elements of the containing pattern used for that purpose
        /// </summary>
        public PatternElement[] connections;

        public PatternGraphEmbedding(String name, LGSPRulePattern ruleOfEmbeddedGraph, PatternElement[] connections)
        {
            this.name = name;
            this.ruleOfEmbeddedGraph = ruleOfEmbeddedGraph;
            this.connections = connections;
        }
    }

    /// <summary>
    /// An alternative is a pattern graph element containing subpatterns
    /// of which one must get successfully matched so that the entire pattern gets matched successfully
    /// </summary>
    public class Alternative : IAlternative
    {
        /// <summary>
        /// Array with the alternative cases
        /// </summary>
        public IPatternGraph[] AlternativeCases { get { return alternativeCases; } }

        public String name;
        public String pathPrefix;

        /// <summary>
        /// Array with the alternative cases
        /// </summary>
        public PatternGraph[] alternativeCases;

        public Alternative(String name, String pathPrefix, PatternGraph[] cases)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.alternativeCases = cases;
        }
    }

    /// <summary>
    /// A description of a GrGen rule.
    /// </summary>
    public abstract class LGSPRulePattern : IRulePattern
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
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        public GrGenType[] Outputs { get { return outputs; } }

        /// <summary>
        /// The names of the nodes added in Modify() in order of adding.
        /// </summary>
        public abstract String[] AddedNodeNames { get; }

        /// <summary>
        /// The names of the edges added in Modify() in order of adding.
        /// </summary>
        public abstract String[] AddedEdgeNames { get; }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match (rewrite part).
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <param name="graph">The host graph for this modification.</param>
        /// <param name="match">The match which is used for this rewrite.</param>
        /// <returns>An array of elements returned by the rule</returns>
        public IGraphElement[] Modify(IGraph graph, IMatch match)
        {
            return Modify((LGSPGraph)graph, (LGSPMatch)match);
        }

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match (rewrite part).
        /// The graph and match object must have the correct type for the used backend.
        /// </summary>
        /// <param name="graph">The host graph for this modification.</param>
        /// <param name="match">The match which is used for this rewrite.</param>
        /// <returns>An array of elements returned by the rule</returns>
        public abstract IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match);

        /// <summary>
        /// Performs the rule specific modifications to the given graph with the given match (rewrite part).
        /// The graph and match object must have the correct type for the used backend.
        /// No reusing of graph elements is used like changing source and target of edges instead of allocating a new edge.
        /// </summary>
        /// <param name="graph">The host graph for this modification.</param>
        /// <param name="match">The match which is used for this rewrite.</param>
        /// <returns>An array of elements returned by the rule</returns>
        public abstract IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match);


        // performance optimization: saves us usage of new in Modify for returning empty array
        public static IGraphElement[] EmptyReturnElements = new IGraphElement[] { };

        /// <summary>
        /// The main pattern graph.
        /// </summary>
        public PatternGraph patternGraph;

        /// <summary>
        /// An array of GrGen types corresponding to rule parameters.
        /// </summary>
        public GrGenType[] inputs; // redundant convenience, information already given by/within the PatternElements

        public string[] inputNames;

        /// <summary>
        /// An array of GrGen types corresponding to rule return values.
        /// </summary>
        public GrGenType[] outputs;

        public string[] outputNames;

        public string name;
        public bool isSubpattern; // are we a rule or a subpattern?

        public abstract void initialize();
    }
}
