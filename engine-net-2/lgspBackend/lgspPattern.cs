using System;
using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    public enum PatternElementType { Normal, Preset, NegElement };

    [DebuggerDisplay("PatternElement ({name}:{TypeID})")]
    public abstract class PatternElement : IPatternElement
    {
        public String Name { get { return name; } }

        public int TypeID;
        public String name;
        public GrGenType[] AllowedTypes;
        public bool[] IsAllowedType;
        public PatternElementType PatternElementType;
        public int ParameterIndex;                      // only valid if PatternElementType == PatternElementType.Preset

        /// <summary>
        /// Instantiates a new PatternElement object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern element</param>
        /// <param name="name">The name of the pattern element</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternElement(int typeID, String name, GrGenType[] allowedTypes, bool[] isAllowedType,
            PatternElementType patternElementType, int parameterIndex)
        {
            this.TypeID = typeID;
            this.name = name;
            this.AllowedTypes = allowedTypes;
            this.IsAllowedType = isAllowedType;
            this.PatternElementType = patternElementType;
            this.ParameterIndex = parameterIndex;
        }
    }

    [DebuggerDisplay("PatternNode ({name}:{TypeID})")]
    public class PatternNode : PatternElement, IPatternNode
    {
        public PlanNode TempPlanMapping;

        /// <summary>
        /// Instantiates a new PatternNode object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern node</param>
        /// <param name="name">The name of the pattern node</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        public PatternNode(int typeID, String name, GrGenType[] allowedTypes, bool[] isAllowedType)
            : base(typeID, name, allowedTypes, isAllowedType, PatternElementType.Normal, -1) { }

        /// <summary>
        /// Instantiates a new PatternNode object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern node</param>
        /// <param name="name">The name of the pattern node</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        public PatternNode(int typeID, String name, GrGenType[] allowedTypes, bool[] isAllowedType,
            PatternElementType patternElementType)
            : base(typeID, name, allowedTypes, isAllowedType, patternElementType, -1) { }

        /// <summary>
        /// Instantiates a new PatternNode object
        /// </summary>
        /// <param name="typeID">The type ID of the pattern node</param>
        /// <param name="name">The name of the pattern node</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternNode(int typeID, String name, GrGenType[] allowedTypes, bool[] isAllowedType,
            PatternElementType patternElementType, int parameterIndex)
            : base(typeID, name, allowedTypes, isAllowedType, patternElementType, parameterIndex) { }
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
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        public PatternEdge(PatternNode source, PatternNode target, int typeID, String name, GrGenType[] allowedTypes,
            bool[] isAllowedType)
            : base(typeID, name, allowedTypes, isAllowedType, PatternElementType.Normal, -1)
        {
            this.source = source;
            this.target = target;
        }

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="source">The source pattern node for this edge.</param>
        /// <param name="target">The target pattern node for this edge.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="name">The name of the pattern edge.</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        public PatternEdge(PatternNode source, PatternNode target, int typeID, String name, GrGenType[] allowedTypes,
            bool[] isAllowedType, PatternElementType patternElementType)
            : base(typeID, name, allowedTypes, isAllowedType, patternElementType, -1)
        {
            this.source = source;
            this.target = target;
        }

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="source">The source pattern node for this edge.</param>
        /// <param name="target">The target pattern node for this edge.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="name">The name of the pattern edge.</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="patternElementType">Specifies what kind of pattern element this is.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds</param>
        public PatternEdge(PatternNode source, PatternNode target, int typeID, String name, GrGenType[] allowedTypes,
            bool[] isAllowedType, PatternElementType patternElementType, int parameterIndex)
            : base(typeID, name, allowedTypes, isAllowedType, patternElementType, parameterIndex)
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

    public class PatternGraph : IPatternGraph
    {
        public String Name { get { return name; } }
        public IPatternNode[] Nodes { get { return nodes; } }
        public IPatternEdge[] Edges { get { return edges; } }
        public bool[,] HomomorphicNodes { get { return homomorphicNodes; } }
        public bool[,] HomomorphicEdges { get { return homomorphicEdges; } }
        public bool[] HomomorphicToAllNodes { get { return homToAllNodes; } }
        public bool[] HomomorphicToAllEdges { get { return homToAllEdges; } }
        public bool[] IsomorphicToAllNodes { get { return isoToAllNodes; } }
        public bool[] IsomorphicToAllEdges { get { return isoToAllEdges; } }
        public IPatternGraphEmbedding[] EmbeddedGraphs { get { return embeddedGraphs; } }

        public String name;
        public PatternNode[] nodes;
        public PatternEdge[] edges;
        public bool[,] homomorphicNodes;
        public bool[,] homomorphicEdges;
        public bool[] homToAllNodes;
        public bool[] homToAllEdges;
        public bool[] isoToAllNodes;
        public bool[] isoToAllEdges;
        public PatternGraphEmbedding[] embeddedGraphs;

        public Condition[] Conditions;

        public PatternGraph(PatternNode[] nodes, PatternEdge[] edges, Condition[] conditions,
            bool[,] homomorphicNodes, bool[,] homomorphicEdges,
            bool[] homToAllNodes, bool[] homToAllEdges,
            bool[] isoToAllNodes, bool[] isoToAllEdges)
        {
            this.nodes = nodes;
            this.edges = edges;
            this.homomorphicNodes = homomorphicNodes;
            this.homomorphicEdges = homomorphicEdges;
            this.homToAllNodes = homToAllNodes;
            this.homToAllEdges = homToAllEdges;
            this.isoToAllNodes = isoToAllNodes;
            this.isoToAllEdges = isoToAllEdges;
            Conditions = conditions;
        }
    }

    public class PatternGraphEmbedding : IPatternGraphEmbedding
    {
        public IPatternGraph EmbeddedGraph { get { return embeddedGraph; } }
        public IPatternElement[] Connections { get { return connections; } }

        public IPatternGraph embeddedGraph;
        public PatternElement[] connections;
    }

    public abstract class LGSPRulePattern : IRulePattern
    {
        public IPatternGraph PatternGraph { get { return patternGraph; } }
        public IPatternGraph[] NegativePatternGraphs { get { return negativePatternGraphs; } }

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
        public PatternGraph[] negativePatternGraphs;

        public GrGenType[] inputs; // information already given by the PatternElements within PatternGraph
        public GrGenType[] outputs;
    }
}
