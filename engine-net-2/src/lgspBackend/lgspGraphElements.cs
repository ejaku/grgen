/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Flags for graph elements.
    /// </summary>
    [Flags]
    public enum LGSPElemFlags : uint
    {
        /// <summary>
        /// Some variable contains this element.
        /// </summary>
        HAS_VARIABLES = 1 << 0,

        /// <summary>
        /// This element has already been matched within some enclosing pattern
        /// during the current matching process, needed for patternpath checks.
        /// </summary>
        IS_MATCHED_BY_SOME_ENCLOSING_PATTERN = 1 << 1,

        /// <summary>
        /// This element has already been matched within an pattern
        /// of this neg level during the current matching process.
        /// This mask must be shifted left by the current neg level.
        /// </summary>
        IS_MATCHED_BY_ENCLOSING_PATTERN = 1 << 2,

        /// <summary>
        /// This element has already been matched within the local pattern
        /// during the current matching process.
        /// This mask must be shifted left by the current neg level.
        /// </summary>
        IS_MATCHED = IS_MATCHED_BY_ENCLOSING_PATTERN << (int) (MAX_NEG_LEVEL + 1),

        /// <summary>
        /// Maximum neg level which can be handled by the flags.
        /// </summary>
        MAX_NEG_LEVEL = 8,

        /// <summary>
        /// This element has already been visited by a visitor.
        /// This mask must be shifted left by the according visitor ID.
        /// </summary>
        IS_VISITED = IS_MATCHED << (int) (MAX_NEG_LEVEL + 1),

        /// <summary>
        /// Number of visitors which can be handled by the flags.
        /// </summary>
        NUM_SUPPORTED_VISITOR_IDS = 8
    }

    /// <summary>
    /// Class implementing nodes in the libGr search plan backend
    /// </summary>
    [DebuggerDisplay("LGSPNode ({Type})")]
    public abstract class LGSPNode : INode
    {
        /// <summary>
        /// The node type of the node.
        /// </summary>
        public NodeType lgspType;

        /// <summary>
        /// contains some booleans coded as bitvector
        /// </summary>
        public uint lgspFlags;

        /// <summary>
        /// Previous and next node in the list containing all the nodes of one type.
        /// The node is not part of a graph, iff typePrev is null.
        /// If typePrev is null and typeNext is not null, this node has been retyped and typeNext
        /// points to the new node.
        /// These special cases are neccessary to handle the following situations:
        /// "delete node + return edge", "hom + delete + return", "hom + retype + return", "hom + retype + delete",
        /// "hom + retype + delete + return".
        /// </summary>
        public LGSPNode lgspTypePrev, lgspTypeNext;

        /// <summary>
        /// Entry node into the outgoing edges list - not of type edge head, real edge or null
        /// </summary>
        public LGSPEdge lgspOuthead;

        /// <summary>
        /// Entry node into the incoming edges list - not of type edge head, real edge or null
        /// </summary>
        public LGSPEdge lgspInhead;

        /// <summary>
        /// Instantiates an LGSPNode object.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        protected LGSPNode(NodeType nodeType)
        {
            lgspType = nodeType;
        }

        /// <summary>
        /// This is true, if this node is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        public bool Valid
        {
            [DebuggerStepThrough]
            get { return lgspTypePrev != null; }
        }

		/// <summary>
		/// The element which replaced this element (Valid is false in this case)
		/// or null, if this element has not been replaced or is still a valid member of a graph.
		/// </summary>
		public IGraphElement ReplacedByElement
		{
            [DebuggerStepThrough]
            get { return ReplacedByNode; }
		}

		/// <summary>
		/// The node which replaced this node (Valid is false in this case)
		/// or null, if this node has not been replaced or is still a valid member of a graph.
		/// </summary>
		public LGSPNode ReplacedByNode
        {
            [DebuggerStepThrough]
            get { return lgspTypePrev != null ? null : lgspTypeNext; }
        }

		/// <summary>
		/// The node which replaced this node (Valid is false in this case)
		/// or null, if this node has not been replaced or is still a valid member of a graph.
		/// </summary>
        INode INode.ReplacedByNode
        {
            [DebuggerStepThrough]
            get { return ReplacedByNode; }
        }


        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleOutgoing(EdgeType edgeType)
        {
            if(lgspOuthead == null) yield break;
            LGSPEdge cur = lgspOuthead.lgspOutNext;
            LGSPEdge next;
            while(lgspOuthead != null && cur != lgspOuthead)
            {
                next = cur.lgspOutNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(lgspOuthead != null && lgspOuthead.Type.IsA(edgeType))
                yield return lgspOuthead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleIncoming(EdgeType edgeType)
        {
            if(lgspInhead == null) yield break;
            LGSPEdge cur = lgspInhead.lgspInNext;
            LGSPEdge next;
            while(lgspInhead != null && cur != lgspInhead)
            {
                next = cur.lgspInNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(lgspInhead != null && lgspInhead.Type.IsA(edgeType))
                yield return lgspInhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleIncident(EdgeType edgeType)
        {
            if(lgspOuthead != null)
            {
                LGSPEdge cur = lgspOuthead.lgspOutNext;
                LGSPEdge next;
                while(lgspOuthead != null && cur != lgspOuthead)
                {
                    next = cur.lgspOutNext;
                    if(cur.Type.IsA(edgeType))
                        yield return cur;
                    cur = next;
                }
                if(lgspOuthead != null && lgspOuthead.Type.IsA(edgeType))
                    yield return lgspOuthead;
            }

            if(lgspInhead != null)
            {
                LGSPEdge cur = lgspInhead.lgspInNext;
                LGSPEdge next;
                while(lgspInhead != null && cur != lgspInhead)
                {
                    next = cur.lgspInNext;
                    if(cur.Type.IsA(edgeType))
                        yield return cur;
                    cur = next;
                }
                if(lgspInhead != null && lgspInhead.Type.IsA(edgeType))
                    yield return lgspInhead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactOutgoing(EdgeType edgeType)
        {
            if(lgspOuthead == null) yield break;
            LGSPEdge cur = lgspOuthead.lgspOutNext;
            LGSPEdge next;
            while(lgspOuthead != null && cur != lgspOuthead)
            {
                next = cur.lgspOutNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(lgspOuthead != null && lgspOuthead.Type == edgeType)
                yield return lgspOuthead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactIncoming(EdgeType edgeType)
        {
            if(lgspInhead == null) yield break;
            LGSPEdge cur = lgspInhead.lgspInNext;
            LGSPEdge next;
            while(lgspInhead != null && cur != lgspInhead)
            {
                next = cur.lgspInNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(lgspInhead != null && lgspInhead.Type == edgeType)
                yield return lgspInhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactIncident(EdgeType edgeType)
        {
            if(lgspOuthead != null)
            {
                LGSPEdge cur = lgspOuthead.lgspOutNext;
                LGSPEdge next;
                while(lgspOuthead != null && cur != lgspOuthead)
                {
                    next = cur.lgspOutNext;
                    if(cur.Type == edgeType)
                        yield return cur;
                    cur = next;
                }
                if(lgspOuthead != null && lgspOuthead.Type == edgeType)
                    yield return lgspOuthead;
            }

            if(lgspInhead != null)
            {
                LGSPEdge cur = lgspInhead.lgspInNext;
                LGSPEdge next;
                while(lgspInhead != null && cur != lgspInhead)
                {
                    next = cur.lgspInNext;
                    if(cur.Type == edgeType)
                        yield return cur;
                    cur = next;
                }
                if(lgspInhead != null && lgspInhead.Type == edgeType)
                    yield return lgspInhead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges
        /// </summary>
        public IEnumerable<IEdge> Outgoing
        {
            get
            {
                if(lgspOuthead == null) yield break;
                LGSPEdge cur = lgspOuthead.lgspOutNext;
                LGSPEdge next;
                while(lgspOuthead != null && cur != lgspOuthead)
                {
                    next = cur.lgspOutNext;
                    yield return cur;
                    cur = next;
                }
                if(lgspOuthead != null)
                    yield return lgspOuthead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges
        /// </summary>
        public IEnumerable<IEdge> Incoming
        {
            get
            {
                if(lgspInhead == null) yield break;
                LGSPEdge cur = lgspInhead.lgspInNext;
                LGSPEdge next;
                while(lgspInhead != null && cur != lgspInhead)
                {
                    next = cur.lgspInNext;
                    yield return cur;
                    cur = next;
                }
                if(lgspInhead != null)
                    yield return lgspInhead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incident edges
        /// </summary>
        public IEnumerable<IEdge> Incident
        {
            get
            {
                if(lgspOuthead != null)
                {
                    LGSPEdge cur = lgspOuthead.lgspOutNext;
                    LGSPEdge next;
                    while(lgspOuthead != null && cur != lgspOuthead)
                    {
                        next = cur.lgspOutNext;
                        yield return cur;
                        cur = next;
                    }
                    if(lgspOuthead != null)
                        yield return lgspOuthead;
                }

                if(lgspInhead != null)
                {
                    LGSPEdge cur = lgspInhead.lgspInNext;
                    LGSPEdge next;
                    while(lgspInhead != null && cur != lgspInhead)
                    {
                        next = cur.lgspInNext;
                        yield return cur;
                        cur = next;
                    }
                    if(lgspInhead != null)
                        yield return lgspInhead;
                }
            }
        }

        internal bool HasOutgoing { [DebuggerStepThrough] get { return lgspOuthead != null; } }
        internal bool HasIncoming { [DebuggerStepThrough] get { return lgspInhead != null; } }

        internal void AddOutgoing(LGSPEdge edge)
        {
            if(lgspOuthead == null)
            {
                lgspOuthead = edge;
                edge.lgspOutNext = edge;
                edge.lgspOutPrev = edge;
            }
            else
            {
                lgspOuthead.lgspOutPrev.lgspOutNext = edge;
                edge.lgspOutPrev = lgspOuthead.lgspOutPrev;
                edge.lgspOutNext = lgspOuthead;
                lgspOuthead.lgspOutPrev = edge;
            }
        }

        internal void AddIncoming(LGSPEdge edge)
        {
            if(lgspInhead == null)
            {
                lgspInhead = edge;
                edge.lgspInNext = edge;
                edge.lgspInPrev = edge;
            }
            else
            {
                lgspInhead.lgspInPrev.lgspInNext = edge;
                edge.lgspInPrev = lgspInhead.lgspInPrev;
                edge.lgspInNext = lgspInhead;
                lgspInhead.lgspInPrev = edge;
            }
        }

        internal void RemoveOutgoing(LGSPEdge edge)
        {
            if(edge == lgspOuthead)
            {
                lgspOuthead = edge.lgspOutNext;
                if(lgspOuthead == edge)
                    lgspOuthead = null;
            }
            edge.lgspOutPrev.lgspOutNext = edge.lgspOutNext;
            edge.lgspOutNext.lgspOutPrev = edge.lgspOutPrev;

            edge.lgspOutNext = null;
            edge.lgspOutPrev = null;
        }

        internal void RemoveIncoming(LGSPEdge edge)
        {
            if(edge == lgspInhead)
            {
                lgspInhead = edge.lgspInNext;
                if(lgspInhead == edge)
                    lgspInhead = null;
            }
            edge.lgspInPrev.lgspInNext = edge.lgspInNext;
            edge.lgspInNext.lgspInPrev = edge.lgspInPrev;

            edge.lgspInNext = null;
            edge.lgspInPrev = null;
        }

        /// <summary>
        /// Moves the head of the outgoing list after the given edge.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="edge">The edge.</param>
        public void MoveOutHeadAfter(LGSPEdge edge)
        {
            lgspOuthead = edge.lgspOutNext;
        }

        /// <summary>
        /// Moves the head of the incoming list after the given edge.
        /// Part of the "list trick".
        /// </summary>
        /// <param name="edge">The edge.</param>
        public void MoveInHeadAfter(LGSPEdge edge)
        {
            lgspInhead = edge.lgspInNext;
        }

        /// <summary>
        /// The NodeType of the node.
        /// </summary>
        public NodeType Type { [DebuggerStepThrough] get { return lgspType; } }

        /// <summary>
        /// The GrGenType of the node.
        /// </summary>
        GrGenType IGraphElement.Type { [DebuggerStepThrough] get { return lgspType; } }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type.
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return lgspType.IsA(otherType);
        }

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all graph element attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a copy of this node.
        /// All attributes will be transfered to the new node.
        /// The node will not be associated to a graph, yet.
        /// So it will not have any incident edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        public abstract INode Clone();

        /// <summary>
        /// Returns whether the attributes of this element and that are equal.
        /// If types are unequal the result is false, otherwise the conjunction of equality comparison of the attributes.
        /// </summary>
        public abstract bool AreAttributesEqual(IGraphElement that);

        /// <summary>
        /// Recycles this node. This may pool the node or just ignore it.
        /// </summary>
        public abstract void Recycle();

        /// <summary>
        /// Returns the name of the type of this node.
        /// </summary>
        /// <returns>The name of the type of this node.</returns>
        public override string ToString()
        {
            return Type.ToString();
        }
    }

    /// <summary>
    /// Special head node of the lists containing all the nodes of one type
    /// </summary>
    [DebuggerDisplay("LGSPNodeHead")]
    public class LGSPNodeHead : LGSPNode
    {
        public LGSPNodeHead() : base(null) { }

        public override object GetAttribute(string attrName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override INode Clone()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool AreAttributesEqual(IGraphElement that)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Recycle()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ResetAllAttributes()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    /// <summary>
    /// Class implementing edges in the libGr search plan backend
    /// </summary>
    [DebuggerDisplay("LGSPEdge ({Source} -{Type}-> {Target})")]
    public abstract class LGSPEdge : IEdge
    {
        /// <summary>
        /// The EdgeType of the edge.
        /// </summary>
        public EdgeType lgspType;

        /// <summary>
        /// contains some booleans coded as bitvector
        /// </summary>
        public uint lgspFlags;

        /// <summary>
        /// Previous and next edge in the list containing all the edges of one type.
        /// The node is not part of a graph, iff typePrev is null.
        /// If typePrev is null and typeNext is not null, this node has been retyped and typeNext
        /// points to the new node.
        /// These special cases are neccessary to handle the following situations:
        /// "delete node + return edge", "hom + delete + return", "hom + retype + return", "hom + retype + delete",
        /// "hom + retype + delete + return".
        /// </summary>
        public LGSPEdge lgspTypeNext, lgspTypePrev;

        /// <summary>
        /// source and target nodes of this edge
        /// </summary>
        public LGSPNode lgspSource, lgspTarget;

        /// <summary>
        /// previous and next edge in the incoming list of the target node containing all of it's incoming edges
        /// </summary>
        public LGSPEdge lgspInNext, lgspInPrev;

        /// <summary>
        /// previous and next edge in the outgoing list of the source node containing all of it's outgoing edges
        /// </summary>
        public LGSPEdge lgspOutNext, lgspOutPrev;

        /// <summary>
        /// Instantiates an LGSPEdge object.
        /// </summary>
        /// <param name="edgeType">The edge type.</param>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="targetNode">The target node.</param>
        protected LGSPEdge(EdgeType edgeType, LGSPNode sourceNode, LGSPNode targetNode)
        {
            lgspType = edgeType;
            lgspSource = sourceNode;
            lgspTarget = targetNode;
        }

        /// <summary>
        /// This is true, if this edge is a valid graph element, i.e. it is part of a graph.
        /// </summary>
        public bool Valid
        {
            [DebuggerStepThrough]
            get { return lgspTypePrev != null; }
        }

		/// <summary>
		/// The element which replaced this element (Valid is false in this case)
		/// or null, if this element has not been replaced or is still a valid member of a graph.
		/// </summary>
		public IGraphElement ReplacedByElement
		{
            [DebuggerStepThrough]
            get { return ReplacedByEdge; }
		}

		/// <summary>
		/// The edge which replaced this edge (Valid is false in this case)
		/// or null, if this edge has not been replaced or is still a valid member of a graph.
		/// </summary>
        public LGSPEdge ReplacedByEdge
        {
            get { return lgspTypePrev != null ? null : lgspTypeNext; }
        }

		/// <summary>
		/// The edge which replaced this edge (Valid is false in this case)
		/// or null, if this edge has not been replaced or is still a valid member of a graph.
		/// </summary>
		IEdge IEdge.ReplacedByEdge
        {
            [DebuggerStepThrough]
            get { return ReplacedByEdge; }
        }

        /// <summary>
        /// The source node of the edge.
        /// </summary>
        public INode Source { [DebuggerStepThrough] get { return lgspSource; } }

        /// <summary>
        /// The target node of the edge.
        /// </summary>
        public INode Target { [DebuggerStepThrough] get { return lgspTarget; } }

        /// <summary>
        /// Retrieves the other incident node of this edge.
        /// </summary>
        /// <remarks>If the given node is not the source, the source will be returned.</remarks>
        /// <param name="sourceOrTarget">One node of this edge.</param>
        /// <returns>The other node of this edge.</returns>
        public INode GetOther(INode sourceOrTarget)
        {
            if(sourceOrTarget == lgspSource) return lgspTarget;
            else return lgspSource;
        }

        /// <summary>
        /// The EdgeType of the edge.
        /// </summary>
        public EdgeType Type { [DebuggerStepThrough] get { return lgspType; } }

        /// <summary>
        /// The GrGenType of the edge.
        /// </summary>
        GrGenType IGraphElement.Type { [DebuggerStepThrough] get { return lgspType; } }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return lgspType.IsA(otherType);
        }

        /// <summary>
        /// Returns the graph element attribute with the given attribute name.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        public abstract object GetAttribute(string attrName);

        /// <summary>
        /// Sets the graph element attribute with the given attribute name to the given value.
        /// If the graph element type doesn't have an attribute with this name, a NullReferenceException is thrown.
        /// </summary>
        /// <param name="attrName">The name of the attribute.</param>
        /// <param name="value">The new value for the attribute. It must have the correct type.
        /// Otherwise a TargetException is thrown.</param>
        public abstract void SetAttribute(string attrName, object value);

        /// <summary>
        /// Resets all graph element attributes to their initial values.
        /// </summary>
        public abstract void ResetAllAttributes();

        /// <summary>
        /// Creates a copy of this edge.
        /// All attributes will be transfered to the new edge.
        /// The edge will not be associated to a graph, yet.
        /// So it will not have any assigned variables.
        /// </summary>
        /// <param name="newSource">The new source node for the new edge.</param>
        /// <param name="newTarget">The new target node for the new edge.</param>
        /// <returns>A copy of this edge.</returns>
        public abstract IEdge Clone(INode newSource, INode newTarget);

        /// <summary>
        /// Returns whether the attributes of this element and that are equal.
        /// If types are unequal the result is false, otherwise the conjunction of equality comparison of the attributes.
        /// </summary>
        public abstract bool AreAttributesEqual(IGraphElement that);

        /// <summary>
        /// Recycles this edge. This may pool the edge or just ignore it.
        /// </summary>
        public abstract void Recycle();

        /// <summary>
        /// Returns the name of the type of this edge.
        /// </summary>
        /// <returns>The name of the type of this edge.</returns>
        public override string ToString()
        {
            return Type.ToString();
        }
    }

    /// <summary>
    /// Special head edge of the lists containing all the edges of one type
    /// </summary>
    [DebuggerDisplay("LGSPEdgeHead")]
    public class LGSPEdgeHead : LGSPEdge
    {
        public LGSPEdgeHead() : base(null, null, null) { }

        public override object GetAttribute(string attrName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override IEdge Clone(INode newSource, INode newTarget)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool AreAttributesEqual(IGraphElement that)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Recycle()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ResetAllAttributes()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
