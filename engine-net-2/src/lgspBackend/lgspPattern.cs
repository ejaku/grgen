/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
    /// An element of a rule pattern.
    /// </summary>
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

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public IPatternGraph PointOfDefinition { get { return pointOfDefinition; } }

        /// <summary>
        /// The type ID of the pattern element.
        /// </summary>
        public int TypeID;

        /// <summary>
        /// The name of the type interface of the pattern element.
        /// </summary>
        public String typeName;

        /// <summary>
        /// The name of the pattern element.
        /// </summary>
        public String name;

        /// <summary>
        /// Pure name of the pattern element as specified in the .grg file without any prefixes.
        /// </summary>
        public String unprefixedName;

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public PatternGraph pointOfDefinition;

        /// <summary>
        /// An array of allowed types for this pattern element.
        /// If it is null, all subtypes of the type specified by typeID (including itself)
        /// are allowed for this pattern element.
        /// </summary>
        public GrGenType[] AllowedTypes;

        /// <summary>
        /// An array containing a bool for each node/edge type (order defined by the TypeIDs)
        /// which is true iff the corresponding type is allowed for this pattern element.
        /// It should be null if allowedTypes is null or empty or has only one element.
        /// </summary>
        public bool[] IsAllowedType;

        /// <summary>
        /// Default cost/priority from frontend, user priority if given.
        /// </summary>
        public float Cost;

        /// <summary>
        /// Specifies to which rule parameter this pattern element corresponds.
        /// Only valid if pattern element is handed in as rule parameter.
        /// </summary>
        public int ParameterIndex;

        /// <summary>
        /// Tells whether this pattern element may be null.
        /// May only be true if pattern element is handed in as rule parameter.
        /// </summary>
        public bool MaybeNull;

        /// <summary>
        /// Instantiates a new PatternElement object.
        /// </summary>
        /// <param name="typeID">The type ID of the pattern element.</param>
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
        /// <param name="name">The name of the pattern element.</param>
        /// <param name="unprefixedName">Pure name of the pattern element as specified in the .grg without any prefixes</param>
        /// <param name="allowedTypes">An array of allowed types for this pattern element.
        ///     If it is null, all subtypes of the type specified by typeID (including itself)
        ///     are allowed for this pattern element.</param>
        /// <param name="isAllowedType">An array containing a bool for each node/edge type (order defined by the TypeIDs)
        ///     which is true iff the corresponding type is allowed for this pattern element.
        ///     It should be null if allowedTypes is null or empty or has only one element.</param>
        /// <param name="cost">Default cost/priority from frontend, user priority if given.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this pattern element corresponds.</param>
        /// <param name="maybeNull">Tells whether this pattern element may be null (is a parameter if true).</param>
        public PatternElement(int typeID, String typeName, 
            String name, String unprefixedName, 
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex, bool maybeNull)
        {
            this.TypeID = typeID;
            this.typeName = typeName;
            this.name = name;
            this.unprefixedName = unprefixedName;
            this.AllowedTypes = allowedTypes;
            this.IsAllowedType = isAllowedType;
            this.Cost = cost;
            this.ParameterIndex = parameterIndex;
            this.MaybeNull = maybeNull;
        }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return Name + ":" + TypeID;
        }
    }

    /// <summary>
    /// A pattern node of a rule pattern.
    /// </summary>
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
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
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
        /// <param name="maybeNull">Tells whether this pattern node may be null (is a parameter if true).</param>
        public PatternNode(int typeID, String typeName,
            String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType, 
            float cost, int parameterIndex, bool maybeNull)
            : base(typeID, typeName, name, unprefixedName, allowedTypes, isAllowedType, cost, parameterIndex, maybeNull)
        {
        }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            return Name + ":" + TypeID;
        }
    }

    /// <summary>
    /// A pattern edge of a rule pattern.
    /// </summary>
    public class PatternEdge : PatternElement, IPatternEdge
    {
        /// <summary>
        /// Indicates, whether this pattern edge should be matched with a fixed direction or not.
        /// </summary>
        public bool fixedDirection;

        /// <summary>
        /// Instantiates a new PatternEdge object
        /// </summary>
        /// <param name="fixedDirection">Whether this pattern edge should be matched with a fixed direction or not.</param>
        /// <param name="typeID">The type ID of the pattern edge.</param>
        /// <param name="typeName">The name of the type interface of the pattern element.</param>
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
        /// <param name="maybeNull">Tells whether this pattern edge may be null (is a parameter if true).</param>
        public PatternEdge(bool fixedDirection,
            int typeID, String typeName, 
            String name, String unprefixedName,
            GrGenType[] allowedTypes, bool[] isAllowedType,
            float cost, int parameterIndex, bool maybeNull)
            : base(typeID, typeName, name, unprefixedName, allowedTypes, isAllowedType, cost, parameterIndex, maybeNull)
        {
            this.fixedDirection = fixedDirection;
        }

        /// <summary>
        /// Converts this instance into a string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            if(fixedDirection)
                return "-" + Name + ":" + TypeID + "->";
            else
                return "<-" + Name + ":" + TypeID + "->";
        }
    }

    /// <summary>
    /// A pattern variable of a rule pattern.
    /// </summary>
    public class PatternVariable : IPatternVariable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public String Name { get { return name; } }

        /// <summary>
        /// The pure name of the pattern element as specified in the .grg without any prefixes.
        /// </summary>
        public String UnprefixedName { get { return unprefixedName; } }

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public IPatternGraph PointOfDefinition { get { return pointOfDefinition; } }

        /// <summary>
        /// The GrGen type of the variable.
        /// </summary>
        public VarType Type;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        public String name;
        
        /// <summary>
        /// Pure name of the variable as specified in the .grg without any prefixes.
        /// </summary>
        public String unprefixedName;

        /// <summary>
        /// The pattern where this element gets matched (null if rule parameter).
        /// </summary>
        public PatternGraph pointOfDefinition;

        /// <summary>
        /// Specifies to which rule parameter this variable corresponds.
        /// </summary>
        public int ParameterIndex;

        /// <summary>
        /// Instantiates a new PatternVariable object.
        /// </summary>
        /// <param name="type">The GrGen type of the variable.</param>
        /// <param name="name">The name of the variable.</param>
        /// <param name="unprefixedName">Pure name of the variable as specified in the .grg without any prefixes.</param>
        /// <param name="parameterIndex">Specifies to which rule parameter this variable corresponds.</param>
        public PatternVariable(VarType type, String name, String unprefixedName, int parameterIndex)
        {
            this.Type = type;
            this.name = name;
            this.unprefixedName = unprefixedName;
            this.ParameterIndex = parameterIndex;
        }
    }

    /// <summary>
    /// Representation of some condition which must be true for the pattern containing it to be matched
    /// </summary>
    public class PatternCondition
    {
        /// <summary>
        /// The condition expression to evaluate
        /// </summary>
        public Expression ConditionExpression;

        /// <summary>
        /// An array of node names needed by this condition.
        /// </summary>
        public String[] NeededNodes;

        /// <summary>
        /// An array of edge names needed by this condition.
        /// </summary>
        public String[] NeededEdges;

        /// <summary>
        /// An array of variable names needed by this condition.
        /// </summary>
        public String[] NeededVariables;

        /// <summary>
        /// An array of variable types (corresponding to the variable names) needed by this condition.
        /// </summary>
        public VarType[] NeededVariableTypes;

        /// <summary>
        /// Constructs a PatternCondition object.
        /// </summary>
        /// <param name="conditionExpression">The condition expression to evaluate.</param>
        /// <param name="neededNodes">An array of node names needed by this condition.</param>
        /// <param name="neededEdges">An array of edge names needed by this condition.</param>
        /// <param name="neededVariables">An array of variable names needed by this condition.</param>
        /// <param name="neededVariableTypes">An array of variable types (corresponding to the variable names) needed by this condition.</param>
        public PatternCondition(Expression conditionExpression, 
            String[] neededNodes, String[] neededEdges, String[] neededVariables, VarType[] neededVariableTypes)
        {
            ConditionExpression = conditionExpression;
            NeededNodes = neededNodes;
            NeededEdges = neededEdges;
            NeededVariables = neededVariables;
            NeededVariableTypes = neededVariableTypes;
        }
    }

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
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public IPatternGraphEmbedding[] EmbeddedGraphs { get { return embeddedGraphs; } }

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public IAlternative[] Alternatives { get { return alternatives; } }

        /// <summary>
        /// An array of iterateds, each iterated is matched as often as possible.
        /// </summary>
        public IPatternGraph[] Iterateds { get { return iterateds; } }

        /// <summary>
        /// An array with the lower bounds the iterated patterns have to be matched to be valid.
        /// </summary>
        public int[] IteratedsMinMatches { get { return minMatches; } }

        /// <summary>
        /// An array with the upper bounds the iterated patterns have to be matched to be valid.
        /// </summary>
        public int[] IteratedsMaxMatches { get { return maxMatches; } }

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
        /// NIY
        /// </summary>
        public bool isPatternpathLocked;

        /// <summary>
        /// An array of all pattern nodes.
        /// </summary>
        public PatternNode[] nodes;

        /// <summary>
        /// An array of all pattern edges.
        /// </summary>
        public PatternEdge[] edges;

        /// <summary>
        /// An array of all pattern variables.
        /// </summary>
        public PatternVariable[] variables;

        /// <summary>
        /// Returns the source pattern node of the given edge, null if edge dangles to the left
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
        /// Returns the target pattern node of the given edge, null if edge dangles to the right
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
        /// contains the source node of the pattern edges in this graph if specified 
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToSourceNode = new Dictionary<PatternEdge,PatternNode>();
        
        /// <summary>
        /// contains the target node of the pattern edges in this graph if specified 
        /// </summary>
        public Dictionary<PatternEdge, PatternNode> edgeToTargetNode = new Dictionary<PatternEdge,PatternNode>();

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node.
        /// </summary>
        public bool[,] homomorphicNodes;

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge.
        /// </summary>
        public bool[,] homomorphicEdges;

        /// <summary>
        /// A two-dimensional array describing which pattern node may be matched non-isomorphic to which pattern node globally,
        /// i.e. the nodes are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        public bool[,] homomorphicNodesGlobal;

        /// <summary>
        /// A two-dimensional array describing which pattern edge may be matched non-isomorphic to which pattern edge globally,
        /// i.e. the edges are contained in different, but locally nested patterns (alternative cases, iterateds).
        /// </summary>
        public bool[,] homomorphicEdgesGlobal;

        /// <summary>
        /// An array with subpattern embeddings, i.e. subpatterns and the way they are connected to the pattern
        /// </summary>
        public PatternGraphEmbedding[] embeddedGraphs;

        /// <summary>
        /// An array of alternatives, each alternative contains in its cases the subpatterns to choose out of.
        /// </summary>
        public Alternative[] alternatives;

        /// <summary>
        /// An array of iterated patterns, each iterated is matched at least as specified in minMatches and at most as specified in maxMatches.
        /// </summary>
        public PatternGraph[] iterateds;

        /// <summary>
        /// An array of integers specifiying how often the corresponding(by array position) iterated pattern must get matched at least
        /// </summary>
        public int[] minMatches;

        /// <summary>
        /// An array of integers specifiying how often the corresponding(by array position) iterated pattern must get matched at most,
        /// with 0 meaning unlimited / as often as possible
        /// </summary>
        public int[] maxMatches;

        /// <summary>
        /// An array of negative pattern graphs which make the search fail if they get matched
        /// (NACs - Negative Application Conditions).
        /// </summary>
        public PatternGraph[] negativePatternGraphs;

        /// <summary>
        /// An array of independent pattern graphs which must get matched in addition to the main pattern
        /// (PACs - Positive Application Conditions).
        /// </summary>
        public PatternGraph[] independentPatternGraphs;

        /// <summary>
        /// The pattern graph which contains this pattern graph, null if this is a top-level-graph 
        /// </summary>
        public PatternGraph embeddingGraph;

        /// <summary>
        /// The conditions used in this pattern graph or it's nested graphs
        /// </summary>
        public PatternCondition[] Conditions;

        /// <summary>
        /// Constructs a PatternGraph object.
        /// </summary>
        /// <param name="name">The name of the pattern graph.</param>
        /// <param name="pathPrefix">Prefix for name from nesting path.</param>
        /// <param name="isPatternpathLocked">NIY</param>
        /// <param name="nodes">An array of all pattern nodes.</param>
        /// <param name="edges">An array of all pattern edges.</param>
        /// <param name="variables">An array of all pattern variables.</param>
        /// <param name="embeddedGraphs">An array with subpattern embeddings,
        /// i.e. subpatterns and the way they are connected to the pattern.</param>
        /// <param name="alternatives">An array of alternatives, each alternative contains
        /// in its cases the subpatterns to choose out of.</param>
        /// <param name="iterateds">An array of iterated patterns, each iterated is matched as often as possible.</param>
        /// <param name="minMatches"> An array of integers specifiying how often the corresponding(by array position) iterated pattern must get matched at least.</param>
        /// <param name="maxMatches"> An array of integers specifiying how often the corresponding(by array position) iterated pattern must get matched at most,
        /// with 0 meaning unlimited / as often as possible.</param>
        /// <param name="negativePatternGraphs">An array of negative pattern graphs which make the
        /// search fail if they get matched (NACs - Negative Application Conditions).</param>
        /// <param name="conditions">The conditions used in this pattern graph or it's nested graphs.</param>
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
        public PatternGraph(String name, String pathPrefix, bool isPatternpathLocked,
            PatternNode[] nodes, PatternEdge[] edges,
            PatternVariable[] variables, PatternGraphEmbedding[] embeddedGraphs,
            Alternative[] alternatives, PatternGraph[] iterateds,
            int[] minMatches, int[] maxMatches,
            PatternGraph[] negativePatternGraphs, PatternGraph[] independentPatternGraphs,
            PatternCondition[] conditions,
            bool[,] homomorphicNodes, bool[,] homomorphicEdges,
            bool[,] homomorphicNodesGlobal, bool[,] homomorphicEdgesGlobal)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.isPatternpathLocked = isPatternpathLocked;
            this.nodes = nodes;
            this.edges = edges;
            this.variables = variables;
            this.embeddedGraphs = embeddedGraphs;
            this.alternatives = alternatives;
            this.iterateds = iterateds;
            this.minMatches = minMatches;
            this.maxMatches = maxMatches;
            this.negativePatternGraphs = negativePatternGraphs;
            this.independentPatternGraphs = independentPatternGraphs;
            this.Conditions = conditions;
            this.homomorphicNodes = homomorphicNodes;
            this.homomorphicEdges = homomorphicEdges;
            this.homomorphicNodesGlobal = homomorphicNodesGlobal;
            this.homomorphicEdgesGlobal = homomorphicEdgesGlobal;

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

        // -------- intermdiate results of matcher generation ----------------------------------

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
        /// The path prefixes and names of the independents nested within this pattern graph
        /// only in top-level-patterns, alternatives, iterateds, only independents not nested within negatives 
        /// </summary>
        public List<Pair<String, String>> pathPrefixesAndNamesOfNestedIndependents;

        /// <summary>
        /// The nodes from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Set of names, with dummy bool due to lacking set class in c#
        /// </summary>
        public Dictionary<String, bool> neededNodes;

        /// <summary>
        /// The edges from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Set of names, with dummy bool due to lacking set class in c#
        /// </summary>
        public Dictionary<String, bool> neededEdges;

        /// <summary>
        /// The variables from the enclosing graph(s) used in this graph or one of it's subgraphs.
        /// Map of names to types
        /// </summary>
        public Dictionary<String, GrGenType> neededVariables;

        /// <summary>
        /// The subpatterns used by this pattern (directly as well as indirectly),
        /// only filled/valid if this is a top level pattern graph of a rule or subpattern.
        /// Set of matching patterns, with dummy null matching pattern due to lacking set class in c#
        /// </summary>
        public Dictionary<LGSPMatchingPattern, LGSPMatchingPattern> usedSubpatterns;

        /// <summary>
        /// The names of the pattern graphs which are on a path to some 
        /// enclosed subpattern usage/alternative/iterated or negative/independent with patternpath modifier.
        /// Needed for patternpath processing setup.
        /// </summary>
        public List<String> patternGraphsOnPathToEnclosedSubpatternOrAlternativeOrIteratedOrPatternpath;
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
        /// The embedded subpattern.
        /// </summary>
        public IPatternGraph EmbeddedGraph { get { return matchingPatternOfEmbeddedGraph.patternGraph; } }


        /// <summary>
        /// The pattern where this complex subpattern element gets matched.
        /// </summary>
        public PatternGraph PointOfDefinition;

        /// <summary>
        /// The name of the usage of the subpattern.
        /// </summary>
        public String name;

        /// <summary>
        /// The embedded subpattern.
        /// </summary>
        public LGSPMatchingPattern matchingPatternOfEmbeddedGraph;

        /// <summary>
        /// An array with the expressions giving the arguments to the subpattern,
        /// that are the pattern variables plus the pattern elements,
        /// with which the subpattern gets connected to the containing pattern.
        /// </summary>
        public Expression[] connections;

        /// <summary>
        /// An array of node names needed by this subpattern embedding.
        /// </summary>
        public String[] neededNodes;

        /// <summary>
        /// An array of edge names needed by this subpattern embedding.
        /// </summary>
        public String[] neededEdges;

        /// <summary>
        /// An array of variable names needed by this subpattern embedding.
        /// </summary>
        public String[] neededVariables;

        /// <summary>
        /// An array of variable types (corresponding to the variable names) needed by this embedding.
        /// </summary>
        public VarType[] neededVariableTypes;

        /// <summary>
        /// Constructs a PatternGraphEmbedding object.
        /// </summary>
        /// <param name="name">The name of the usage of the subpattern.</param>
        /// <param name="matchingPatternOfEmbeddedGraph">The embedded subpattern.</param>
        /// <param name="connections">An array with the expressions defining how the subpattern is connected
        /// to the containing pattern (graph elements and basic variables) .</param>
        public PatternGraphEmbedding(String name, LGSPMatchingPattern matchingPatternOfEmbeddedGraph,
                Expression[] connections, String[] neededNodes, String[] neededEdges,
                String[] neededVariables, VarType[] neededVariableTypes)
        {
            this.name = name;
            this.matchingPatternOfEmbeddedGraph = matchingPatternOfEmbeddedGraph;
            this.connections = connections;
            this.neededNodes = neededNodes;
            this.neededEdges = neededEdges;
            this.neededVariables = neededVariables;
            this.neededVariableTypes = neededVariableTypes;
        }
    }

    /// <summary>
    /// An alternative is a pattern graph element containing subpatterns
    /// of which one must get successfully matched so that the entire pattern gets matched successfully.
    /// </summary>
    public class Alternative : IAlternative
    {
        /// <summary>
        /// Array with the alternative cases.
        /// </summary>
        public IPatternGraph[] AlternativeCases { get { return alternativeCases; } }

        /// <summary>
        /// Name of the alternative.
        /// </summary>
        public String name;

        /// <summary>
        /// Prefix for name from nesting path.
        /// </summary>
        public String pathPrefix;

        /// <summary>
        /// Array with the alternative cases.
        /// </summary>
        public PatternGraph[] alternativeCases;

        /// <summary>
        /// Constructs an Alternative object.
        /// </summary>
        /// <param name="name">Name of the alternative.</param>
        /// <param name="pathPrefix">Prefix for name from nesting path.</param>
        /// <param name="cases">Array with the alternative cases.</param>
        public Alternative(String name, String pathPrefix, PatternGraph[] cases)
        {
            this.name = name;
            this.pathPrefix = pathPrefix;
            this.alternativeCases = cases;
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
        /// An array of the names corresponding to rule parameters;
        /// </summary>
        public String[] InputNames { get { return inputNames; } }

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
        /// Our name
        /// </summary>
        public string name;
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
    }
}
