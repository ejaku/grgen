//#define ELEMENTKNOWSVARIABLES
#define OLDMAPPEDFIELDS

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.lgsp
{
    public abstract class LGSPGraphElement<T>
    {
        public GrGenType type;
        public IAttributes attributes;
#if OLDMAPPEDFIELDS
        public int mappedTo;
        public int negMappedTo;
#else
        /// <summary>
        /// Determines whether the element is currently mapped during the matching process.
        /// </summary>
        public bool isMapped;

        /// <summary>
        /// Determines whether the element has been mapped in a prior matching step,
        /// e.g. the main pattern when currently matching a NAC.
        /// </summary>
        public bool isOldMapped;

        /// <summary>
        /// Only temporarily!!
        /// </summary>
        public bool isNegMapped;
#endif

        public T typeNext, typePrev;

#if ELEMENTKNOWSVARIABLES
        /// <summary>
        /// List of variables pointing to this element or null if there is no such variable
        /// </summary>
        public LinkedList<Variable> variableList;
#else
        public bool hasVariables;
#endif

        internal LGSPGraphElement()
        {
        }

        public LGSPGraphElement(GrGenType elementType)
        {
            type = elementType;
            attributes = elementType.CreateAttributes();
        }

        public IType Type { get { return type; } }
        public bool InstanceOf(IType otherType)
        {
            return type.IsA(otherType);
        }

        public object GetAttribute(String attrName)
        {
            return attributes.GetType().GetProperty(attrName).GetValue(attributes, null);
        }

        public void SetAttribute(String attrName, object value)
        {
            attributes.GetType().GetProperty(attrName).SetValue(attributes, value, null);
        }
    }

    public class LGSPNode : LGSPGraphElement<LGSPNode>, INode
    {
        public LGSPEdge outhead;
        public LGSPEdge inhead;

        // Only for heads of linked lists
        public LGSPNode()
        {
        }

        public LGSPNode(GrGenType nodeType) : base(nodeType)
        {
        }

        public IEnumerable<IEdge> GetCompatibleOutgoing(IType edgeType)
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
        public IEnumerable<IEdge> GetCompatibleIncoming(IType edgeType)
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

        public IEnumerable<IEdge> GetExactOutgoing(IType edgeType)
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
        public IEnumerable<IEdge> GetExactIncoming(IType edgeType)
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
    }

    public class LGSPEdge : LGSPGraphElement<LGSPEdge>, IEdge
    {
        public LGSPNode source, target;

        public LGSPEdge inNext, inPrev, outNext, outPrev;
              
        // only for heads of linked lists                                 
        public LGSPEdge()
        {
        }

        public LGSPEdge(GrGenType edgeType, LGSPNode sourceNode, LGSPNode targetNode) : base(edgeType)
        {
            source = sourceNode;
            target = targetNode;
        }

        public INode Source { get { return source; } }
        public INode Target { get { return target; } }
    }
}