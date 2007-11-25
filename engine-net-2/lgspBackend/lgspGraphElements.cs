//#define ELEMENTKNOWSVARIABLES

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public abstract class LGSPNode : INode
    {
        public NodeType type;
//        public IAttributes attributes;

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
//            attributes = type.CreateAttributes();
        }

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

        public NodeType Type { get { return type; } }
        GrGenType IGraphElement.Type { get { return type; } }

        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
        }

        public abstract object GetAttribute(string attrName);

/*        public object GetAttribute(string attrName)
        {
            return attributes.GetType().GetProperty(attrName).GetValue(attributes, null);
        }*/

        public abstract void SetAttribute(string attrName, object value);

/*        public void SetAttribute(string attrName, object value)
        {
            attributes.GetType().GetProperty(attrName).SetValue(attributes, value, null);
        }*/

        public abstract INode Clone();
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
    }

    public abstract class LGSPEdge : IEdge
    {
        public EdgeType type;
//        public IAttributes attributes;

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
//            attributes = type.CreateAttributes();
            source = sourceNode;
            target = targetNode;
        }

        public INode Source { get { return source; } }
        public INode Target { get { return target; } }

        public EdgeType Type { get { return type; } }
        GrGenType IGraphElement.Type { get { return type; } }

        public bool InstanceOf(GrGenType otherType)
        {
            return type.IsA(otherType);
        }

        public abstract object GetAttribute(string attrName);

/*        public object GetAttribute(string attrName)
        {
            return attributes.GetType().GetProperty(attrName).GetValue(attributes, null);
        }*/

        public abstract void SetAttribute(string attrName, object value);

/*        public void SetAttribute(string attrName, object value)
        {
            attributes.GetType().GetProperty(attrName).SetValue(attributes, value, null);
        }*/

        public abstract IEdge Clone(INode newSource, INode newTarget);
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
    }
}