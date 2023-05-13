/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;

using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.expression
{
    /// <summary>
    /// Class representing a graph is-isomorph comparison.
    /// </summary>
    public class GRAPH_EQ : Operator
    {
        public GRAPH_EQ(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_EQ(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a graph is-not-isomorph comparison.
    /// </summary>
    public class GRAPH_NE : Operator
    {
        public GRAPH_NE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_NE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("!((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").IsIsomorph((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing a graph is-structural-equal (isomorph disregarding attributes) comparison.
    /// </summary>
    public class GRAPH_SE : Operator
    {
        public GRAPH_SE(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GRAPH_SE(Left.Copy(renameSuffix), Right.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IGraph)");
            Left.Emit(sourceCode);
            sourceCode.Append(").HasSameStructure((GRGEN_LIBGR.IGraph)");
            Right.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Left;
            yield return Right;
        }

        protected readonly Expression Left;
        protected readonly Expression Right;
    }

    /// <summary>
    /// Class representing graph entity expression
    /// </summary>
    public class GraphEntityExpression : Expression
    {
        public GraphEntityExpression(String entity)
        {
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GraphEntityExpression(Entity + renameSuffix);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append(NamesOfEntities.CandidateVariable(Entity));
        }

        public readonly String Entity;
    }

    /// <summary>
    /// Class representing nameof expression
    /// </summary>
    public class Nameof : Expression
    {
        public Nameof(Expression entity)
        {
            Entity = entity;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Entity != null)
                return new Nameof(Entity.Copy(renameSuffix));
            else
                return new Nameof(null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Entity != null)
            {
                sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nameof(");
                Entity.Emit(sourceCode);
                sourceCode.Append(", graph)");
            }
            else
                sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nameof(null, graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Entity != null)
                yield return Entity;
        }

        readonly Expression Entity;
    }

    public enum UniqueofType
    {
        Node,
        Edge,
        Graph,
        ClassObject
    }

    /// <summary>
    /// Class representing unique id expression
    /// </summary>
    public class Uniqueof : Expression
    {
        public Uniqueof(Expression entity, UniqueofType type)
        {
            Entity = entity;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            if(Entity != null)
                return new Uniqueof(Entity.Copy(renameSuffix), Type);
            else
                return new Uniqueof(null, Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Entity == null)
            {
                sourceCode.Append("((GRGEN_LGSP.LGSPGraph)graph).GraphId");
            }
            else
            {
                sourceCode.Append("(");
                if(Type == UniqueofType.Node)
                    sourceCode.Append("(GRGEN_LGSP.LGSPNode)");
                else if(Type == UniqueofType.Edge)
                    sourceCode.Append("(GRGEN_LGSP.LGSPEdge)");
                else if(Type == UniqueofType.Graph)
                    sourceCode.Append("(GRGEN_LGSP.LGSPGraph)");
                else //if(Type == UniqueofType.ClassObject)
                    sourceCode.Append("(GRGEN_LGSP.LGSPObject)");
                Entity.Emit(sourceCode);
                if(Type == UniqueofType.Graph)
                    sourceCode.Append(").GraphId");
                else
                    sourceCode.Append(").uniqueId");
            }
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            if(Entity != null)
                yield return Entity;
        }

        readonly Expression Entity;
        readonly UniqueofType Type;
    }

    /// <summary>
    /// Class representing this expression
    /// </summary>
    public class This : Expression
    {
        public This()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new This();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("graph");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield break;
        }
    }

    /// <summary>
    /// Class representing an incidence count index access expression.
    /// </summary>
    public class IncidenceCountIndexAccess : Expression
    {
        public IncidenceCountIndexAccess(String target, Expression keyExpr, String type)
        {
            Target = target;
            KeyExpr = keyExpr;
            Type = type;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IncidenceCountIndexAccess(Target, KeyExpr.Copy(renameSuffix), Type);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((GRGEN_LIBGR.IIncidenceCountIndex)graph.Indices.GetIndex(\"" + Target + "\")).GetIncidenceCount(");
            sourceCode.Append("(" + Type + ")");
            KeyExpr.Emit(sourceCode);
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return KeyExpr;
        }

        readonly String Target;
        readonly Expression KeyExpr;
        readonly String Type;
    }

    /// <summary>
    /// Class representing expression returning the nodes of a node type (as set)
    /// </summary>
    public class Nodes : Expression
    {
        public Nodes(Expression nodeType)
        {
            NodeType = nodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Nodes(NodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Nodes(graph, ");
            NodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return NodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression NodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the edges of an edge type (as set)
    /// </summary>
    public class Edges : Expression
    {
        public Edges(Expression edgeType, Directedness directedness)
        {
            EdgeType = edgeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Edges(EdgeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Edges");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, ");
            EdgeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return EdgeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }
        
        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression EdgeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of nodes of a node type
    /// </summary>
    public class CountNodes : Expression
    {
        public CountNodes(Expression nodeType)
        {
            NodeType = nodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountNodes(NodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountNodes(graph, ");
            NodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return NodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression NodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of edges of an edge type
    /// </summary>
    public class CountEdges : Expression
    {
        public CountEdges(Expression edgeType)
        {
            EdgeType = edgeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountEdges(EdgeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountEdges(graph, ");
            EdgeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return EdgeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression EdgeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the graph is empty
    /// </summary>
    public class Empty : Expression
    {
        public Empty()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Empty();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(graph.NumNodes+graph.NumEdges==0)");
        }
    }

    /// <summary>
    /// Class representing expression returning the number of graph elements
    /// </summary>
    public class Size : Expression
    {
        public Size()
        {
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Size();
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(graph.NumNodes+graph.NumEdges)");
        }
    }

    /// <summary>
    /// Class representing expression returning the source node of an edge
    /// </summary>
    public class Source : Expression
    {
        public Source(Expression edge)
        {
            Edge = edge;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Source(Edge.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Source)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
        }

        readonly Expression Edge;
    }

    /// <summary>
    /// Class representing expression returning the target node of an edge
    /// </summary>
    public class Target : Expression
    {
        public Target(Expression edge)
        {
            Edge = edge;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Target(Edge.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Target)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
        }

        readonly Expression Edge;
    }

    /// <summary>
    /// Class representing expression returning the opposite node of an edge and a node
    /// </summary>
    public class Opposite : Expression
    {
        public Opposite(Expression edge, Expression node)
        {
            Edge = edge;
            Node = node;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Opposite(Edge.Copy(renameSuffix), Node.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("((");
            Edge.Emit(sourceCode);
            sourceCode.Append(").Opposite(");
            Node.Emit(sourceCode);
            sourceCode.Append("))");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Edge;
            yield return Node;
        }

        readonly Expression Edge;
        readonly Expression Node;
    }

    /// <summary>
    /// Class representing expression returning the node for a name (or null)
    /// </summary>
    public class NodeByName : Expression
    {
        public NodeByName(Expression name, Expression nodeType)
        {
            Name = name;
            NodeType = nodeType;
        }

        public NodeByName(Expression name)
        {
            Name = name;
            NodeType = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new NodeByName(Name.Copy(renameSuffix), NodeType != null ? NodeType.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.GetNode((GRGEN_LIBGR.INamedGraph)graph, ");
            Name.Emit(sourceCode);
            if(NodeType != null)
            {
                sourceCode.Append(", ");
                NodeType.Emit(sourceCode);
            }
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Name;
            if(NodeType != null)
                yield return NodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression Name;
        readonly Expression NodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the edge for a name (or null)
    /// </summary>
    public class EdgeByName : Expression
    {
        public EdgeByName(Expression name, Expression edgeType)
        {
            Name = name;
            EdgeType = edgeType;
        }

        public EdgeByName(Expression name)
        {
            Name = name;
            EdgeType = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EdgeByName(Name.Copy(renameSuffix), EdgeType != null ? EdgeType.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.GetEdge((GRGEN_LIBGR.INamedGraph)graph, ");
            Name.Emit(sourceCode);
            if(EdgeType != null)
            {
                sourceCode.Append(", ");
                EdgeType.Emit(sourceCode);
            }
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Name;
            if(EdgeType != null)
                yield return EdgeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression Name;
        readonly Expression EdgeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the node for a unique id(or null)
    /// </summary>
    public class NodeByUnique : Expression
    {
        public NodeByUnique(Expression unique, Expression nodeType)
        {
            Unique = unique;
            NodeType = nodeType;
        }

        public NodeByUnique(Expression unique)
        {
            Unique = unique;
            NodeType = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new NodeByUnique(Unique.Copy(renameSuffix), NodeType != null ? NodeType.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.GetNode(graph, ");
            Unique.Emit(sourceCode);
            if(NodeType != null)
            {
                sourceCode.Append(", ");
                NodeType.Emit(sourceCode);
            }
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Unique;
            if(NodeType != null)
                yield return NodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression Unique;
        readonly Expression NodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the edge for a unique id(or null)
    /// </summary>
    public class EdgeByUnique : Expression
    {
        public EdgeByUnique(Expression unique, Expression edgeType)
        {
            Unique = unique;
            EdgeType = edgeType;
        }

        public EdgeByUnique(Expression unique)
        {
            Unique = unique;
            EdgeType = null;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EdgeByUnique(Unique.Copy(renameSuffix), EdgeType != null ? EdgeType.Copy(renameSuffix) : null);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.GetEdge(graph, ");
            Unique.Emit(sourceCode);
            if(EdgeType != null)
            {
                sourceCode.Append(", ");
                EdgeType.Emit(sourceCode);
            }
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Unique;
            if(EdgeType != null)
                yield return EdgeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression Unique;
        readonly Expression EdgeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the outgoing edges of a node (as set)
    /// </summary>
    public class Outgoing : Expression
    {
        public Outgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Outgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Outgoing");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the incoming edges of a node (as set)
    /// </summary>
    public class Incoming : Expression
    {
        public Incoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Incoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Incoming");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the incident edges of a node (as set)
    /// </summary>
    public class Incident : Expression
    {
        public Incident(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Incident(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Incident");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the outgoing edges of a node
    /// </summary>
    public class CountOutgoing : Expression
    {
        public CountOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the incoming edges of a node
    /// </summary>
    public class CountIncoming : Expression
    {
        public CountIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the incident edges of a node
    /// </summary>
    public class CountIncident : Expression
    {
        public CountIncident(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountIncident(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountIncident((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via outgoing edges
    /// </summary>
    public class AdjacentOutgoing : Expression
    {
        public AdjacentOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new AdjacentOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.AdjacentOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via incoming edges
    /// </summary>
    public class AdjacentIncoming : Expression
    {
        public AdjacentIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new AdjacentIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.AdjacentIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the adjacent nodes of a node (as set) reachable via incident edges
    /// </summary>
    public class Adjacent : Expression
    {
        public Adjacent(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Adjacent(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Adjacent((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the adjacent nodes of a node reachable via outgoing edges
    /// </summary>
    public class CountAdjacentOutgoing : Expression
    {
        public CountAdjacentOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountAdjacentOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountAdjacentOutgoing(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the adjacent nodes of a node reachable via incoming edges
    /// </summary>
    public class CountAdjacentIncoming : Expression
    {
        public CountAdjacentIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountAdjacentIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountAdjacentIncoming(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the adjacent nodes of a node reachable via incident edges
    /// </summary>
    public class CountAdjacent : Expression
    {
        public CountAdjacent(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountAdjacent(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountAdjacent(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an outgoing edge
    /// </summary>
    public class IsAdjacentOutgoing : Expression
    {
        public IsAdjacentOutgoing(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacentOutgoing(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentOutgoing(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an incoming edge
    /// </summary>
    public class IsAdjacentIncoming : Expression
    {
        public IsAdjacentIncoming(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacentIncoming(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacentIncoming(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is adjacent to the start node with an incident edge
    /// </summary>
    public class IsAdjacent : Expression
    {
        public IsAdjacent(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsAdjacent(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsAdjacent(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }
        
        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an outgoing edge
    /// </summary>
    public class IsOutgoing : Expression
    {
        public IsOutgoing(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsOutgoing(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsOutgoing(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an incoming edge
    /// </summary>
    public class IsIncoming : Expression
    {
        public IsIncoming(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsIncoming(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsIncoming(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is incident to the start node with an incident edge
    /// </summary>
    public class IsIncident : Expression
    {
        public IsIncident(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsIncident(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsIncident(");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via outgoing edges of a node (as set)
    /// </summary>
    public class ReachableEdgesOutgoing : Expression
    {
        public ReachableEdgesOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdgesOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesOutgoing");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel) 
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via incoming edges of a node (as set)
    /// </summary>
    public class ReachableEdgesIncoming : Expression
    {
        public ReachableEdgesIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdgesIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdgesIncoming");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges via incident edges of a node (as set)
    /// </summary>
    public class ReachableEdges : Expression
    {
        public ReachableEdges(Expression node, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableEdges(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableEdges");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges via outgoing edges of a node 
    /// </summary>
    public class CountReachableEdgesOutgoing : Expression
    {
        public CountReachableEdgesOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachableEdgesOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesOutgoing(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges via incoming edges of a node
    /// </summary>
    public class CountReachableEdgesIncoming : Expression
    {
        public CountReachableEdgesIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachableEdgesIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdgesIncoming(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges via incident edges of a node
    /// </summary>
    public class CountReachableEdges : Expression
    {
        public CountReachableEdges(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachableEdges(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachableEdges(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via outgoing edges
    /// </summary>
    public class ReachableOutgoing : Expression
    {
        public ReachableOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via incoming edges
    /// </summary>
    public class ReachableIncoming : Expression
    {
        public ReachableIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new ReachableIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.ReachableIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes of a node (as set) reachable via incident edges
    /// </summary>
    public class Reachable : Expression
    {
        public Reachable(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Reachable(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.Reachable((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes of a node reachable via outgoing edges
    /// </summary>
    public class CountReachableOutgoing : Expression
    {
        public CountReachableOutgoing(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachableOutgoing(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachableOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes of a node reachable via incoming edges
    /// </summary>
    public class CountReachableIncoming : Expression
    {
        public CountReachableIncoming(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachableIncoming(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachableIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes of a node reachable via incident edges
    /// </summary>
    public class CountReachable : Expression
    {
        public CountReachable(Expression node, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountReachable(Node.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountReachable((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges within the given depth via outgoing edges of a node (as set)
    /// </summary>
    public class BoundedReachableEdgesOutgoing : Expression
    {
        public BoundedReachableEdgesOutgoing(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableEdgesOutgoing(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesOutgoing");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges within the given depth via incoming edges of a node (as set)
    /// </summary>
    public class BoundedReachableEdgesIncoming : Expression
    {
        public BoundedReachableEdgesIncoming(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableEdgesIncoming(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdgesIncoming");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable edges within the given depth via incident edges of a node (as set)
    /// </summary>
    public class BoundedReachableEdges : Expression
    {
        public BoundedReachableEdges(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType, Directedness directedness)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableEdges(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableEdges");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public readonly Directedness Directedness;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges within the given depth via outgoing edges of a node
    /// </summary>
    public class CountBoundedReachableEdgesOutgoing : Expression
    {
        public CountBoundedReachableEdgesOutgoing(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachableEdgesOutgoing(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesOutgoing(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges within the given depth via incoming edges of a node
    /// </summary>
    public class CountBoundedReachableEdgesIncoming : Expression
    {
        public CountBoundedReachableEdgesIncoming(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachableEdgesIncoming(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdgesIncoming(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable edges within the given depth via incident edges of a node
    /// </summary>
    public class CountBoundedReachableEdges : Expression
    {
        public CountBoundedReachableEdges(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachableEdges(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableEdges(graph, (GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as set) reachable via outgoing edges
    /// </summary>
    public class BoundedReachableOutgoing : Expression
    {
        public BoundedReachableOutgoing(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableOutgoing(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as set) reachable via incoming edges
    /// </summary>
    public class BoundedReachableIncoming : Expression
    {
        public BoundedReachableIncoming(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableIncoming(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as set) reachable via incident edges
    /// </summary>
    public class BoundedReachable : Expression
    {
        public BoundedReachable(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachable(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachable((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes within the given depth of a node reachable via outgoing edges
    /// </summary>
    public class CountBoundedReachableOutgoing : Expression
    {
        public CountBoundedReachableOutgoing(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachableOutgoing(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes within the given depth of a node reachable via incoming edges
    /// </summary>
    public class CountBoundedReachableIncoming : Expression
    {
        public CountBoundedReachableIncoming(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachableIncoming(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachableIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the count of the reachable nodes within the given depth of a node reachable via incident edges
    /// </summary>
    public class CountBoundedReachable : Expression
    {
        public CountBoundedReachable(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new CountBoundedReachable(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.CountBoundedReachable((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as map including the remaining depth) reachable via outgoing edges
    /// </summary>
    public class BoundedReachableWithRemainingDepthOutgoing : Expression
    {
        public BoundedReachableWithRemainingDepthOutgoing(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableWithRemainingDepthOutgoing(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthOutgoing((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as map including the remaining depth) reachable via incoming edges
    /// </summary>
    public class BoundedReachableWithRemainingDepthIncoming : Expression
    {
        public BoundedReachableWithRemainingDepthIncoming(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableWithRemainingDepthIncoming(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepthIncoming((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the reachable nodes within the given depth of a node (as map including the remaining depth) reachable via incident edges
    /// </summary>
    public class BoundedReachableWithRemainingDepth : Expression
    {
        public BoundedReachableWithRemainingDepth(Expression node, Expression depth, Expression incidentEdgeType, Expression adjacentNodeType)
        {
            Node = node;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new BoundedReachableWithRemainingDepth(Node.Copy(renameSuffix), Depth.Copy(renameSuffix), IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.BoundedReachableWithRemainingDepth((GRGEN_LIBGR.INode)");
            Node.Emit(sourceCode);
            sourceCode.Append(", ");
            Depth.Emit(sourceCode);
            sourceCode.Append(", ");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", ");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Node;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        public readonly Expression Node;
        public readonly Expression Depth;
        public readonly Expression IncidentEdgeType;
        public readonly Expression AdjacentNodeType;
        public bool Parallel;
        public bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via outgoing edges
    /// </summary>
    public class IsReachableOutgoing : Expression
    {
        public IsReachableOutgoing(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableOutgoing(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via incoming edges
    /// </summary>
    public class IsReachableIncoming : Expression
    {
        public IsReachableIncoming(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableIncoming(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node via incident edges
    /// </summary>
    public class IsReachable : Expression
    {
        public IsReachable(Expression startNode, Expression endNode,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachable(StartNode.Copy(renameSuffix), EndNode.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachable(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via outgoing edges
    /// </summary>
    public class IsReachableEdgesOutgoing : Expression
    {
        public IsReachableEdgesOutgoing(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdgesOutgoing(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via incoming edges
    /// </summary>
    public class IsReachableEdgesIncoming : Expression
    {
        public IsReachableEdgesIncoming(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdgesIncoming(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdgesIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node via incident edges
    /// </summary>
    public class IsReachableEdges : Expression
    {
        public IsReachableEdges(Expression startNode, Expression endEdge,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsReachableEdges(StartNode.Copy(renameSuffix), EndEdge.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsReachableEdges(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node within the given depth via outgoing edges
    /// </summary>
    public class IsBoundedReachableOutgoing : Expression
    {
        public IsBoundedReachableOutgoing(Expression startNode, Expression endNode, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachableOutgoing(StartNode.Copy(renameSuffix), 
                EndNode.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node within the given depth via incoming edges
    /// </summary>
    public class IsBoundedReachableIncoming : Expression
    {
        public IsBoundedReachableIncoming(Expression startNode, Expression endNode, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachableIncoming(StartNode.Copy(renameSuffix),
                EndNode.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end node is reachable from the start node within the given depth via incident edges
    /// </summary>
    public class IsBoundedReachable : Expression
    {
        public IsBoundedReachable(Expression startNode, Expression endNode, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndNode = endNode;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachable(StartNode.Copy(renameSuffix),
                EndNode.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndNode;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachable(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.INode)");
            EndNode.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndNode;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node within the given depth via outgoing edges
    /// </summary>
    public class IsBoundedReachableEdgesOutgoing : Expression
    {
        public IsBoundedReachableEdgesOutgoing(Expression startNode, Expression endEdge, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachableEdgesOutgoing(StartNode.Copy(renameSuffix),
                EndEdge.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesOutgoing(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node within the given depth via incoming edges
    /// </summary>
    public class IsBoundedReachableEdgesIncoming : Expression
    {
        public IsBoundedReachableEdgesIncoming(Expression startNode, Expression endEdge, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachableEdgesIncoming(StartNode.Copy(renameSuffix),
                EndEdge.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdgesIncoming(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning whether the end edge is reachable from the start node within the given depth via incident edges
    /// </summary>
    public class IsBoundedReachableEdges : Expression
    {
        public IsBoundedReachableEdges(Expression startNode, Expression endEdge, Expression depth,
            Expression incidentEdgeType, Expression adjacentNodeType)
        {
            StartNode = startNode;
            EndEdge = endEdge;
            Depth = depth;
            IncidentEdgeType = incidentEdgeType;
            AdjacentNodeType = adjacentNodeType;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new IsBoundedReachableEdges(StartNode.Copy(renameSuffix), 
                EndEdge.Copy(renameSuffix), Depth.Copy(renameSuffix),
                IncidentEdgeType.Copy(renameSuffix), AdjacentNodeType.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.IsBoundedReachableEdges(graph, ");
            sourceCode.Append("(GRGEN_LIBGR.INode)");
            StartNode.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.IEdge)");
            EndEdge.Emit(sourceCode);
            sourceCode.Append(", (int)");
            Depth.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.EdgeType)");
            IncidentEdgeType.Emit(sourceCode);
            sourceCode.Append(", (GRGEN_LIBGR.NodeType)");
            AdjacentNodeType.Emit(sourceCode);
            if(Profiling)
                sourceCode.AppendFront(", actionEnv");
            if(Parallel)
                sourceCode.Append(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return StartNode;
            yield return EndEdge;
            yield return Depth;
            yield return IncidentEdgeType;
            yield return AdjacentNodeType;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        public override void SetNeedForProfiling(bool profiling)
        {
            Profiling = profiling;
        }

        readonly Expression StartNode;
        readonly Expression EndEdge;
        readonly Expression Depth;
        readonly Expression IncidentEdgeType;
        readonly Expression AdjacentNodeType;
        bool Parallel;
        bool Profiling;
    }

    /// <summary>
    /// Class representing expression returning the induced subgraph from the given set of nodes
    /// </summary>
    public class InducedSubgraph : Expression
    {
        public InducedSubgraph(Expression nodeSet)
        {
            NodeSet = nodeSet;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new InducedSubgraph(NodeSet.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.InducedSubgraph((IDictionary<GRGEN_LIBGR.INode, GRGEN_LIBGR.SetValueType>)");
            NodeSet.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return NodeSet;
        }

        readonly Expression NodeSet;
    }

    /// <summary>
    /// Class representing expression returning the defined subgraph from the given set of edges
    /// </summary>
    public class DefinedSubgraph : Expression
    {
        public DefinedSubgraph(Expression edgeSet, Directedness directedness)
        {
            EdgeSet = edgeSet;
            Directedness = directedness;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new DefinedSubgraph(EdgeSet.Copy(renameSuffix), Directedness);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.DefinedSubgraph");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("Directed");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("Undirected");
            sourceCode.Append("(");
            if(Directedness == Directedness.Directed)
                sourceCode.Append("(IDictionary<GRGEN_LIBGR.IDEdge, GRGEN_LIBGR.SetValueType>)");
            if(Directedness == Directedness.Undirected)
                sourceCode.Append("(IDictionary<GRGEN_LIBGR.IUEdge, GRGEN_LIBGR.SetValueType>)");
            if(Directedness == Directedness.Arbitrary)
                sourceCode.Append("(IDictionary<GRGEN_LIBGR.IEdge, GRGEN_LIBGR.SetValueType>)");
            EdgeSet.Emit(sourceCode);
            sourceCode.Append(", graph)");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return EdgeSet;
        }

        readonly Expression EdgeSet;
        readonly Directedness Directedness;
    }

    /// <summary>
    /// Class representing expression returning whether the given subgraph is equal to any of the given set of subgraphs
    /// </summary>
    public class EqualsAny : Expression
    {
        public EqualsAny(Expression subgraph, Expression subgraphSet, bool includingAttributes)
        {
            Subgraph = subgraph;
            SubgraphSet = subgraphSet;
            IncludingAttributes = includingAttributes;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new EqualsAny(Subgraph.Copy(renameSuffix), SubgraphSet.Copy(renameSuffix), IncludingAttributes);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.EqualsAny((GRGEN_LIBGR.IGraph)");
            Subgraph.Emit(sourceCode);
            sourceCode.Append(", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)");
            SubgraphSet.Emit(sourceCode);
            sourceCode.Append(", ");
            sourceCode.Append(IncludingAttributes ? "true" : "false");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Subgraph;
            yield return SubgraphSet;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        bool Parallel;

        readonly Expression Subgraph;
        readonly Expression SubgraphSet;
        readonly bool IncludingAttributes;
    }

    /// <summary>
    /// Class representing expression returning the equivalent subgraph from the given set to the given subgraph
    /// </summary>
    public class GetEquivalent : Expression
    {
        public GetEquivalent(Expression subgraph, Expression subgraphSet, bool includingAttributes)
        {
            Subgraph = subgraph;
            SubgraphSet = subgraphSet;
            IncludingAttributes = includingAttributes;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new GetEquivalent(Subgraph.Copy(renameSuffix), SubgraphSet.Copy(renameSuffix), IncludingAttributes);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("GRGEN_LIBGR.GraphHelper.GetEquivalent((GRGEN_LIBGR.IGraph)");
            Subgraph.Emit(sourceCode);
            sourceCode.Append(", (IDictionary<GRGEN_LIBGR.IGraph, GRGEN_LIBGR.SetValueType>)");
            SubgraphSet.Emit(sourceCode);
            sourceCode.Append(", ");
            sourceCode.Append(IncludingAttributes ? "true" : "false");
            if(Parallel)
                sourceCode.AppendFront(", threadId");
            sourceCode.Append(")");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Subgraph;
            yield return SubgraphSet;
        }

        public override void SetNeedForParallelizedVersion(bool parallel)
        {
            Parallel = parallel;
        }

        bool Parallel;

        readonly Expression Subgraph;
        readonly Expression SubgraphSet;
        readonly bool IncludingAttributes;
    }

    /// <summary>
    /// Class representing expression returning a canonical string representation of a graph
    /// </summary>
    public class Canonize : Expression
    {
        public Canonize(Expression expr)
        {
            Expr = expr;
        }

        public override Expression Copy(string renameSuffix)
        {
            return new Canonize(Expr.Copy(renameSuffix));
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.Append("(");
            Expr.Emit(sourceCode);
            sourceCode.Append(").Canonize()");
        }

        public override IEnumerator<ExpressionOrYielding> GetEnumerator()
        {
            yield return Expr;
        }

        readonly Expression Expr;
    }


    /////////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// Class representing a comparison of all the attributes.
    /// Is not generated into code, does not exist at source level.
    /// An internal thing only used for the interpretation plan, isomorphy checking.
    /// (todo: Makes sense to offer sth like this at source level, too?)
    /// </summary>
    public class AreAttributesEqual : Expression
    {
        public AreAttributesEqual(IGraphElement this_, PatternElement thisInPattern)
        {
            this.this_ = this_;
            this.thisInPattern = thisInPattern;
        }

        public override Expression Copy(string renameSuffix)
        {
            throw new Exception("Not implemented!");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            throw new Exception("Not implemented!");
        }

        public bool Execute(IGraphElement that)
        {
            return this_.IsDeeplyEqual(that, new Dictionary<object, object>());
        }

        public readonly IGraphElement this_;
        public readonly PatternElement thisInPattern;
    }
}
