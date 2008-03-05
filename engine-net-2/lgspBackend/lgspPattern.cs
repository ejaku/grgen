using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    [DebuggerDisplay("PatternElement ({name}:{TypeID})")]
    public abstract class PatternElement : IPatternElement
    {
        public String Name { get { return name; } }
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

    [DebuggerDisplay("PatternEdge ({ToString()})")]
    public class PatternEdge : PatternElement, IPatternEdge
    {
        public IPatternNode Source { get { return source; } }
        public IPatternNode Target { get { return target; } }

        public PatternNode source;
        public PatternNode target;

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="source">The source pattern node for this edge.</param>
        /// <param name="target">The target pattern node for this edge.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="name">The name of the pattern edge.</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        /// <param name="cost"> default cost/priority from frontend, user priority if given</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternEdge(PatternNode source, PatternNode target, 
            int typeID, String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType,
            float cost, int parameterIndex)
            : base(typeID, name, unprefixedName, allowedTypes, isAllowedType, cost, parameterIndex)
        {
            this.source = source;
            this.target = target;
        }

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
        public String Name { get { return name; } }
        public IPatternNode[] Nodes { get { return nodes; } }
        public IPatternEdge[] Edges { get { return edges; } }
        public bool[,] HomomorphicNodes { get { return homomorphicNodes; } }
        public bool[,] HomomorphicEdges { get { return homomorphicEdges; } }
        public IPatternGraphEmbedding[] EmbeddedGraphs { get { return embeddedGraphs; } }
        public IAlternative[] Alternatives { get { return alternatives; } }
        public IPatternGraph[] NegativePatternGraphs { get { return negativePatternGraphs; } }

        public String name;
        public String pathPrefix;
        public bool isIndependent;
        public PatternNode[] nodes;
        public PatternEdge[] edges;
        public bool[,] homomorphicNodes;
        public bool[,] homomorphicEdges;
        public PatternGraphEmbedding[] embeddedGraphs;
        public Alternative[] alternatives;
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

    public class PatternGraphEmbedding : IPatternGraphEmbedding
    {
        public String Name { get { return name; } }
        public IPatternGraph EmbeddedGraph { get { return ruleOfEmbeddedGraph.patternGraph; } }
        public IPatternElement[] Connections { get { return connections; } }
        public PatternGraph PointOfDefinition; // the pattern this complex subpattern element gets matched

        public String name;
        public LGSPRulePattern ruleOfEmbeddedGraph;
        public PatternElement[] connections;

        public PatternGraphEmbedding(String name, LGSPRulePattern ruleOfEmbeddedGraph, PatternElement[] connections)
        {
            this.name = name;
            this.ruleOfEmbeddedGraph = ruleOfEmbeddedGraph;
            this.connections = connections;
        }
    }

    public class Alternative : IAlternative
    {
        public IPatternGraph[] AlternativeCases { get { return alternativeCases; } }

        public String name;
        public String pathPrefix;
        public PatternGraph[] alternativeCases;

        public Alternative(String name, String pathPrefix, PatternGraph[] cases)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.alternativeCases = cases;
        }
    }

    public abstract class LGSPRulePattern : IRulePattern
    {
        public IPatternGraph PatternGraph { get { return patternGraph; } }
        
        public GrGenType[] Inputs { get { return inputs; } }
        public GrGenType[] Outputs { get { return outputs; } }

        public abstract String[] AddedNodeNames { get; }
        public abstract String[] AddedEdgeNames { get; }

        public IGraphElement[] Modify(IGraph graph, IMatch match)
        {
            return Modify((LGSPGraph)graph, (LGSPMatch)match);
        }

        public abstract IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match);
        public abstract IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match);


        // performance optimization: saves us usage of new in Modify for returning empty array
        public static IGraphElement[] EmptyReturnElements = new IGraphElement[] { };

        public PatternGraph patternGraph;

        public GrGenType[] inputs; // redundant convenience, information already given by/within the PatternElements
        public string[] inputNames;
        public GrGenType[] outputs;
        public string[] outputNames;

        public string name;
        public bool isSubpattern; // are we a rule or a subpattern?

        public abstract void initialize();
    }
}
