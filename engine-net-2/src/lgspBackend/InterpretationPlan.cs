/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class of the interpretation plan data structure,
    /// which consists of a linked list of matching operations
    /// (Benefits compared to scheduled search plan:
    ///   - stores matched graph elements
    ///   - connectedness checking with bidirectional matching is complicated enough it should be computed statically before execution)
    /// The interpretation plan is only used in isomorphy checking.
    /// </summary>
    public abstract class InterpretationPlan
    {
        /// <summary>
        /// dumps interpretation plan operation (as string) into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Dump(SourceBuilder builder);

        /// <summary>
        /// Executes the interpretation plan (starting with this operation)
        /// </summary>
        /// <param name="graph">The graph over which the plan is to be interpreted</param>
        /// <returns>true if execution succeeded, i.e. a match was found; false otherwise</returns>
        public abstract bool Execute(LGSPGraph graph);

        /// <summary>
        /// The next interpretation plan operation
        /// </summary>
        public InterpretationPlan next;
    }

    /// <summary>
    /// An interpretation plan operation which matches a node
    /// </summary>
    public abstract class InterpretationPlanNodeMatcher : InterpretationPlan
    {
        /// <summary>
        /// The node matched by this interpretation plan operation during execution
        /// </summary>
        public LGSPNode node;
    }

    /// <summary>
    /// An interpretation plan operation which matches an edge
    /// </summary>
    public abstract class InterpretationPlanEdgeMatcher : InterpretationPlan
    {
        /// <summary>
        /// The edge matched by this interpretation plan operation during execution
        /// </summary>
        public LGSPEdge edge;
    }

    /// <summary>
    /// An interpretation plan operation which stores a direction decision
    /// </summary>
    public abstract class InterpretationPlanDirectionVariable : InterpretationPlan
    {
        /// <summary>
        /// The direction decided upon by this interpretation plan operation during execution
        /// </summary>
        public int direction;
    }

    /// <summary>
    /// Interpretation plan operation which work as an anchor for an interpretation plan without own functionality
    /// </summary>
    public class InterpretationPlanStart : InterpretationPlan
    {
        public InterpretationPlanStart()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("Start\n");
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            return next.Execute(graph);
        }
    }

    /// <summary>
    /// Interpretation plan operation which looks up a node in the graph
    /// </summary>
    public class InterpretationPlanLookupNode : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanLookupNode(int targetType)
        {
            this.targetType = targetType;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: LookupNode {1}\n", this.GetHashCode(), targetType);
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            for(LGSPNode head = graph.nodesByTypeHeads[targetType], candidate = head.lgspTypeNext; candidate != head; candidate = candidate.lgspTypeNext)
            {
                if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                    continue;
                candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                node = candidate;
                bool matched = next.Execute(graph);
                candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                if(matched)
                    return true;
            }
            return false;
        }

        int targetType;
    }

    /// <summary>
    /// Interpretation plan operation which looks up an edge in the graph
    /// </summary>
    public class InterpretationPlanLookupEdge : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanLookupEdge(int targetType)
        {
            this.targetType = targetType;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: LookupEdge {1}\n", this.GetHashCode(), targetType);
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            for(LGSPEdge head = graph.edgesByTypeHeads[targetType], candidate = head.lgspTypeNext; candidate != head; candidate = candidate.lgspTypeNext)
            {
                if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                    continue;
                candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                edge = candidate;
                bool matched = next.Execute(graph);
                candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                if(matched)
                    return true;
            }
            return false;
        }

        int targetType;
    }

    /// <summary>
    /// Interpretation plan operation which iterates both directions,
    /// needed for matching bidirectional edges in both directions
    /// </summary>
    public class InterpretationPlanBothDirections : InterpretationPlanDirectionVariable
    {
        public InterpretationPlanBothDirections()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: BothDirections\n", this.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            for(direction = 0; direction < 2; ++direction)
            {
                bool matched = next.Execute(graph);
                if(matched)
                    return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an incoming edge from a source node
    /// </summary>
    public class InterpretationPlanIncoming : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanIncoming(int targetType, InterpretationPlanNodeMatcher source)
        {
            this.targetType = targetType;
            this.source = source;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: Incoming {1} from {2}\n", this.GetHashCode(), targetType, source.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPEdge head = source.node.lgspInhead;
            if(head != null)
            {
                LGSPEdge candidate = head;
                do
                {
                    if(candidate.lgspType.TypeID != targetType)
                        continue;
                    if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                        continue;
                    candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                    edge = candidate;
                    bool matched = next.Execute(graph);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matched)
                        return true;
                }
                while((candidate = candidate.lgspInNext) != head);
            }
            return false;
        }

        int targetType;
        InterpretationPlanNodeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an outgoing edge from a source node
    /// </summary>
    public class InterpretationPlanOutgoing : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanOutgoing(int targetType, InterpretationPlanNodeMatcher source)
        {
            this.targetType = targetType;
            this.source = source;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: Outgoing {1} to {2}\n", this.GetHashCode(), targetType, source.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPEdge head = source.node.lgspOuthead;
            if(head != null)
            {
                LGSPEdge candidate = head;
                do
                {
                    if(candidate.lgspType.TypeID != targetType)
                        continue;
                    if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                        continue;
                    candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                    edge = candidate;
                    bool matched = next.Execute(graph);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matched)
                        return true;
                }
                while((candidate = candidate.lgspOutNext) != head);
            }
            return false;
        }

        int targetType;
        InterpretationPlanNodeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an edge to be matched bidirectionally from a source node
    /// </summary>
    public class InterpretationPlanIncomingOrOutgoing : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanIncomingOrOutgoing(int targetType, InterpretationPlanNodeMatcher source, InterpretationPlanDirectionVariable directionVariable)
        {
            this.targetType = targetType;
            this.source = source;
            this.directionVariable = directionVariable;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: IncomingOrOutgoing {1} from/to {2} direction {3}\n", this.GetHashCode(), targetType, source.GetHashCode(), directionVariable.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPEdge head = directionVariable.direction==0 ? source.node.lgspInhead : source.node.lgspOuthead;
            if(head != null)
            {
                LGSPEdge candidate = head;
                do
                {
                    if(candidate.lgspType.TypeID != targetType)
                        continue;
                    if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                        continue;
                    candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                    edge = candidate;
                    bool matched = next.Execute(graph);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matched)
                        return true;
                }
                while((candidate = (directionVariable.direction==0 ? candidate.lgspInNext : candidate.lgspOutNext)) != head);
            }
            return false;
        }

        int targetType;
        InterpretationPlanNodeMatcher source;
        InterpretationPlanDirectionVariable directionVariable;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves the target node of an edge
    /// </summary>
    public class InterpretationPlanImplicitTarget : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanImplicitTarget(int targetType, InterpretationPlanEdgeMatcher source)
        {
            this.targetType = targetType;
            this.source = source;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitTarget {1} of {2}\n", this.GetHashCode(), targetType, source.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPNode candidate = source.edge.lgspTarget;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph);
            candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
            return matched;
        }

        int targetType;
        InterpretationPlanEdgeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves the source node of an edge
    /// </summary>
    public class InterpretationPlanImplicitSource : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanImplicitSource(int targetType, InterpretationPlanEdgeMatcher source)
        {
            this.targetType = targetType;
            this.source = source;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitSource {1} of {2}\n", this.GetHashCode(), targetType, source.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPNode candidate = source.edge.lgspSource;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph);
            candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
            return matched;
        }

        int targetType;
        InterpretationPlanEdgeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves the source or target node of an edge
    /// depending on the current direction to be matched
    /// </summary>
    public class InterpretationPlanImplicitSourceOrTarget : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanImplicitSourceOrTarget(int targetType, InterpretationPlanEdgeMatcher source, InterpretationPlanDirectionVariable directionVariable)
        {
            this.targetType = targetType;
            this.source = source;
            this.directionVariable = directionVariable;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitSourceOrTarget {1} of {2} direction {3}\n", this.GetHashCode(), targetType, source.GetHashCode(), directionVariable.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPNode candidate = directionVariable.direction == 0 ? source.edge.lgspSource : source.edge.lgspTarget;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph);
            candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
            return matched;
        }

        int targetType;
        InterpretationPlanEdgeMatcher source;
        InterpretationPlanDirectionVariable directionVariable;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves the source or target node of an edge
    /// depending on the other node already matched
    /// </summary>
    public class InterpretationPlanImplicitTheOther : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanImplicitTheOther(int targetType, InterpretationPlanEdgeMatcher source, InterpretationPlanNodeMatcher theOther)
        {
            this.targetType = targetType;
            this.source = source;
            this.theOther = theOther;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitTheOther {1} of {2} the other is {3}\n", this.GetHashCode(), targetType, source.GetHashCode(), theOther.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            LGSPNode candidate = theOther.node == source.edge.lgspSource ? source.edge.lgspTarget : source.edge.lgspSource;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph);
            candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
            return matched;
        }

        int targetType;
        InterpretationPlanEdgeMatcher source;
        InterpretationPlanNodeMatcher theOther;
    }

    /// <summary>
    /// Interpretation plan operation which checks the source node of an edge to be identical to a given node
    /// </summary>
    public class InterpretationPlanCheckConnectednessSource : InterpretationPlan
    {
        public InterpretationPlanCheckConnectednessSource(InterpretationPlanNodeMatcher node, InterpretationPlanEdgeMatcher edge)
        {
            this.node = node;
            this.edge = edge;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0} -{1}->\n", node.GetHashCode(), edge.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            if(edge.edge.lgspSource == node.node)
                return next.Execute(graph);
            else
                return false;
        }

        InterpretationPlanNodeMatcher node;
        InterpretationPlanEdgeMatcher edge;
    }

    /// <summary>
    /// Interpretation plan operation which checks the target node of an edge to be identical to a given node
    /// </summary>
    public class InterpretationPlanCheckConnectednessTarget : InterpretationPlan
    {
        public InterpretationPlanCheckConnectednessTarget(InterpretationPlanNodeMatcher node, InterpretationPlanEdgeMatcher edge)
        {
            this.node = node;
            this.edge = edge;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness -{0}-> {1}\n", edge.GetHashCode(), node.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            if(edge.edge.lgspTarget == node.node)
                return next.Execute(graph);
            else
                return false;
        }

        InterpretationPlanNodeMatcher node;
        InterpretationPlanEdgeMatcher edge;
    }

    /// <summary>
    /// Interpretation plan operation which checks the source or target node of an edge to be identical to a given node,
    /// depending on the current direction to be matched
    /// </summary>
    public class InterpretationPlanCheckConnectednessSourceOrTarget : InterpretationPlan
    {
        public InterpretationPlanCheckConnectednessSourceOrTarget(InterpretationPlanNodeMatcher node, InterpretationPlanEdgeMatcher edge, InterpretationPlanDirectionVariable directionVariable)
        {
            this.node = node;
            this.edge = edge;
            this.directionVariable = directionVariable;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0}? <-{1}-> ?{0} direction {2}\n", node.GetHashCode(), edge.GetHashCode(), directionVariable.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            if((directionVariable.direction == 0 ? edge.edge.lgspSource : edge.edge.lgspTarget) == node.node)
                return next.Execute(graph);
            else
                return false;
        }

        InterpretationPlanNodeMatcher node;
        InterpretationPlanEdgeMatcher edge;
        InterpretationPlanDirectionVariable directionVariable;
    }

    /// <summary>
    /// Interpretation plan operation which checks the source or target node of an edge to be identical to a given node,
    /// depending on the other node already matched
    /// </summary>
    public class InterpretationPlanCheckConnectednessTheOther : InterpretationPlan
    {
        public InterpretationPlanCheckConnectednessTheOther(InterpretationPlanNodeMatcher node, InterpretationPlanEdgeMatcher edge, InterpretationPlanNodeMatcher theOther)
        {
            this.node = node;
            this.edge = edge;
            this.theOther = theOther;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0}? <-{1}-> ?{0} the other {2}\n", node.GetHashCode(), edge.GetHashCode(), theOther.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            if((theOther.node == edge.edge.lgspSource ? edge.edge.lgspTarget : edge.edge.lgspSource) == node.node)
                return next.Execute(graph);
            else
                return false;
        }

        InterpretationPlanNodeMatcher node;
        InterpretationPlanEdgeMatcher edge;
        InterpretationPlanNodeMatcher theOther;
    }

    /// <summary>
    /// Interpretation plan operation which checks the AreAttribuesEqual condition
    /// </summary>
    public class InterpretationPlanCheckCondition : InterpretationPlan
    {
        public InterpretationPlanCheckCondition(expression.AreAttributesEqual condition, InterpretationPlanNodeMatcher nodeMatcher)
        {
            this.condition = condition;
            this.nodeMatcher = nodeMatcher;
        }

        public InterpretationPlanCheckCondition(expression.AreAttributesEqual condition, InterpretationPlanEdgeMatcher edgeMatcher)
        {
            this.condition = condition;
            this.edgeMatcher = edgeMatcher;
        }

        public override void Dump(SourceBuilder builder)
        {
            if(nodeMatcher != null)
                builder.AppendFrontFormat("CheckCondition on node {0}\n", nodeMatcher.GetHashCode());
            else
                builder.AppendFrontFormat("CheckCondition on edge {0}\n", edgeMatcher.GetHashCode());
            next.Dump(builder);
        }

        public override bool Execute(LGSPGraph graph)
        {
            if(condition.Execute(nodeMatcher!=null ? (IGraphElement)nodeMatcher.node : (IGraphElement)edgeMatcher.edge))
                return next.Execute(graph);
            else
                return false;
        }

        expression.AreAttributesEqual condition;
        InterpretationPlanNodeMatcher nodeMatcher;
        InterpretationPlanEdgeMatcher edgeMatcher;
    }

    /// <summary>
    /// Interpretation plan operation which completes a match;
    /// no own functionality, it just succeeds when execution reaches it
    /// </summary>
    public class InterpretationPlanMatchComplete : InterpretationPlan
    {
        public InterpretationPlanMatchComplete()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("MatchComplete\n");
        }

        public override bool Execute(LGSPGraph graph)
        {
            return true;
        }
    }
}

