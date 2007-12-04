//#define ELEMENTKNOWSVARIABLES

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public abstract class LGSPNode : INode
    {
        public NodeType type;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within the local pattern
        /// </summary>
        public bool isMatched = false;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within the local nested negative pattern
        /// </summary>
        public bool isMatchedNeg = false;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within an enclosing pattern
        /// </summary>
        public bool isMatchedByEnclosingPattern = false;

        public LGSPNode typeNext, typePrev;

#if ELEMENTKNOWSVARIABLES
        /// <summary>
        /// List of variables pointing to this element or null if there is no such variable
        /// </summary>
        public LinkedList<Variable> variableList;
#else
        public bool hasVariables;
#endif

        public LGSPEdge outhead;
        public LGSPEdge inhead;

        public LGSPNode(NodeType nodeType)
        {
            type = nodeType;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleOutgoing(EdgeType edgeType)
        {
            if(outhead == null) yield break;
            LGSPEdge cur = outhead.outNext;
            LGSPEdge next;
            while(outhead != null && cur != outhead)
            {
                next = cur.outNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(outhead != null && outhead.Type.IsA(edgeType))
                yield return outhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with the same type or a subtype of the given type
        /// </summary>
        public IEnumerable<IEdge> GetCompatibleIncoming(EdgeType edgeType)
        {
            if(inhead == null) yield break;
            LGSPEdge cur = inhead.inNext;
            LGSPEdge next;
            while(inhead != null && cur != inhead)
            {
                next = cur.inNext;
                if(cur.Type.IsA(edgeType))
                    yield return cur;
                cur = next;
            }
            if(inhead != null && inhead.Type.IsA(edgeType))
                yield return inhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactOutgoing(EdgeType edgeType)
        {
            if(outhead == null) yield break;
            LGSPEdge cur = outhead.outNext;
            LGSPEdge next;
            while(outhead != null && cur != outhead)
            {
                next = cur.outNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(outhead != null && outhead.Type == edgeType)
                yield return outhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges with exactly the given type
        /// </summary>
        public IEnumerable<IEdge> GetExactIncoming(EdgeType edgeType)
        {
            if(inhead == null) yield break;
            LGSPEdge cur = inhead.inNext;
            LGSPEdge next;
            while(inhead != null && cur != inhead)
            {
                next = cur.inNext;
                if(cur.Type == edgeType)
                    yield return cur;
                cur = next;
            }
            if(inhead != null && inhead.Type == edgeType)
                yield return inhead;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all outgoing edges
        /// </summary>
        public IEnumerable<IEdge> Outgoing
        {
            get
            {
                if(outhead == null) yield break;
                LGSPEdge cur = outhead.outNext;
                LGSPEdge next;
                while(outhead != null && cur != outhead)
                {
                    next = cur.outNext;
                    yield return cur;
                    cur = next;
                }
                if(outhead != null)
                    yield return outhead;
            }
        }

        /// <summary>
        /// Returns an IEnumerable&lt;IEdge&gt; over all incoming edges
        /// </summary>
        public IEnumerable<IEdge> Incoming
        {
            get
            {
                if(inhead == null) yield break;
                LGSPEdge cur = inhead.inNext;
                LGSPEdge next;
                while(inhead != null && cur != inhead)
                {
                    next = cur.inNext;
                    yield return cur;
                    cur = next;
                }
                if(inhead != null)
                    yield return inhead;
            }
        }
        internal bool HasOutgoing { get { return outhead != null; } }
        internal bool HasIncoming { get { return inhead != null; } }

        internal void AddOutgoing(LGSPEdge edge)
        {
            if(outhead == null)
            {
                outhead = edge;
                edge.outNext = edge;
                edge.outPrev = edge;
            }
            else
            {
                outhead.outPrev.outNext = edge;
                edge.outPrev = outhead.outPrev;
                edge.outNext = outhead;
                outhead.outPrev = edge;
            }
        }

        internal void AddIncoming(LGSPEdge edge)
        {
            if(inhead == null)
            {
                inhead = edge;
                edge.inNext = edge;
                edge.inPrev = edge;
            }
            else
            {
                inhead.inPrev.inNext = edge;
                edge.inPrev = inhead.inPrev;
                edge.inNext = inhead;
                inhead.inPrev = edge;
            }
        }

        internal void RemoveOutgoing(LGSPEdge edge)
        {
            if(edge == outhead)
            {
                outhead = edge.outNext;
                if(outhead == edge)
                    outhead = null;
            }
            edge.outPrev.outNext = edge.outNext;
            edge.outNext.outPrev = edge.outPrev;

            edge.outNext = null;
            edge.outPrev = null;
        }

        internal void RemoveIncoming(LGSPEdge edge)
        {
            if(edge == inhead)
            {
                inhead = edge.inNext;
                if(inhead == edge)
                    inhead = null;
            }
            edge.inPrev.inNext = edge.inNext;
            edge.inNext.inPrev = edge.inPrev;

            edge.inNext = null;
            edge.inPrev = null;
        }

        public void MoveOutHeadAfter(LGSPEdge edge)
        {
            outhead = edge.outNext;
        }

        public void MoveInHeadAfter(LGSPEdge edge)
        {
            inhead = edge.inNext;
        }

        /// <summary>
        /// Returns the NodeType of the graph element.
        /// </summary>
        public NodeType Type { get { return type; } }

        /// <summary>
        /// Returns the GrGenType of the graph element.
        /// </summary>
        GrGenType IGraphElement.Type { get { return type; } }

        /// <summary>
        /// Returns true, if the graph element is compatible to the given type.
        /// </summary>
        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
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
        /// So it will not have any adjacent edges nor any assigned variables.
        /// </summary>
        /// <returns>A copy of this node.</returns>
        public abstract INode Clone();

        /// <summary>
        /// Recycles this node. This may pool the node or just ignore it.
        /// </summary>
        public abstract void Recycle();
    }

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

        public override void Recycle()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ResetAllAttributes()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public abstract class LGSPEdge : IEdge
    {
        public EdgeType type;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within the local pattern
        /// </summary>
        public bool isMatched = false;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within the local nested negative pattern
        /// </summary>
        public bool isMatchedNeg = false;

        /// <summary>
        /// Tells during the matching process whether the element 
        /// is already matched within an enclosing pattern
        /// </summary>
        public bool isMatchedByEnclosingPattern = false;

        public LGSPEdge typeNext, typePrev;

#if ELEMENTKNOWSVARIABLES
        /// <summary>
        /// List of variables pointing to this element or null if there is no such variable
        /// </summary>
        public LinkedList<Variable> variableList;
#else
        public bool hasVariables;
#endif

        public LGSPNode source, target;

        public LGSPEdge inNext, inPrev, outNext, outPrev;
              
        public LGSPEdge(EdgeType edgeType, LGSPNode sourceNode, LGSPNode targetNode)
        {
            type = edgeType;
            source = sourceNode;
            target = targetNode;
        }

        /// <summary>
        /// The source node of the edge.
        /// </summary>
        public INode Source { get { return source; } }

        /// <summary>
        /// The target node of the edge.
        /// </summary>
        public INode Target { get { return target; } }

        /// <summary>
        /// Returns the EdgeType of the edge.
        /// </summary>
        public EdgeType Type { get { return type; } }

        /// <summary>
        /// Returns the GrGenType of the edge.
        /// </summary>
        GrGenType IGraphElement.Type { get { return type; } }

        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
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
        /// Recycles this edge. This may pool the edge or just ignore it.
        /// </summary>
        public abstract void Recycle();
    }

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