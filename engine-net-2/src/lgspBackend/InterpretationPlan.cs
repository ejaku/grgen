/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
    /// A helper class containing a match found by iteration plan execution
    /// </summary>
    public class FoundMatch
    {
        public FoundMatch(int numNodes, int numEdges)
        {
            nodes = new INode[numNodes];
            edges = new IEdge[numEdges];
        }

        /// <summary>
        /// The nodes found, linked to their corresponding pattern nodes in the pattern graph by index
        /// </summary>
        public INode[] nodes;

        /// <summary>
        /// The edges found, linked to their corresponding pattern edges in the pattern graph by index
        /// </summary>
        public IEdge[] edges;
    }

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
        /// emits the interpretation plan operation (as c# code string) into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder builder);

        /// <summary>
        /// Executes the interpretation plan (starting with this operation)
        /// </summary>
        /// <param name="graph">The graph over which the plan is to be interpreted</param>
        /// <param name="includingAttributes">Whether to check for isomorphy including attributes or without them (== vs ~~)</param>
        /// <param name="matches">If not null, the list is filled with the matches; only in this case are all matches iterated</param>
        /// <returns>true if execution succeeded, i.e. a match was found; false otherwise</returns>
        public abstract bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches);

        /// <summary>
        /// The next interpretation plan operation
        /// </summary>
        public InterpretationPlan next;

        /// <summary>
        /// The previous interpretation plan operation
        /// </summary>
        public InterpretationPlan prev;

        /// A unique identifier denoting this interpretation plan operation
        /// </summary>
        public int Id;

        protected void AssignId()
        {
            Id = idOrigin;
            ++idOrigin;
        }

        private static int idOrigin = 0;
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

        /// <summary>
        /// The node representation in the search plan; the elementID - 1 is the index in the nodes array of the pattern graph
        /// </summary>
        public SearchPlanNodeNode planNodeNode;
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

        /// <summary>
        /// The edge representation in the search plan; the elementID - 1 is the index in the edges array of the pattern graph
        /// </summary>
        public SearchPlanEdgeNode planEdgeNode;
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
        public InterpretationPlanStart(string comparisonMatcherName)
        {
            this.comparisonMatcherName = comparisonMatcherName;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("Start {0}\n", comparisonMatcherName);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("public class {0} : GRGEN_LGSP.GraphComparisonMatcher\n", comparisonMatcherName);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFront("public bool IsIsomorph(GRGEN_LGSP.PatternGraph thisPattern, GRGEN_LGSP.LGSPGraph graph, bool includingAttributes)\n");
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.AppendFront("return false;\n");
            builder.Unindent();
            builder.AppendFront("}\n");
            builder.AppendFront("public string Name { get { return \"" + comparisonMatcherName + "\"; } }\n");
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            return next.Execute(graph, includingAttributes, matches);
        }

        string comparisonMatcherName;

        public string ComparisonMatcherName { get { return comparisonMatcherName; } }
    }

    /// <summary>
    /// Interpretation plan operation which looks up a node in the graph
    /// </summary>
    public class InterpretationPlanLookupNode : InterpretationPlanNodeMatcher
    {
        public InterpretationPlanLookupNode(int targetType, SearchPlanNodeNode planNodeNode)
        {
            this.targetType = targetType;
            this.planNodeNode = planNodeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: LookupNode {1}\n", this.Id, targetType);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("for(GRGEN_LGSP.LGSPNode head{0} = graph.nodesByTypeHeads[{1}], candidate{0} = head{0}.lgspTypeNext; candidate{0} != head{0}; candidate{0} = candidate{0}.lgspTypeNext)\n", this.Id, targetType);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
            for(LGSPNode head = graph.nodesByTypeHeads[targetType], candidate = head.lgspTypeNext; candidate != head; candidate = candidate.lgspTypeNext)
            {
                if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                    continue;
                candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                node = candidate;
                matched |= next.Execute(graph, includingAttributes, matches);
                candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                if(matches==null && matched)
                    return true;
            }
            return matched;
        }

        int targetType;
    }

    /// <summary>
    /// Interpretation plan operation which looks up an edge in the graph
    /// </summary>
    public class InterpretationPlanLookupEdge : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanLookupEdge(int targetType, SearchPlanEdgeNode planEdgeNode)
        {
            this.targetType = targetType;
            this.planEdgeNode = planEdgeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: LookupEdge {1}\n", this.Id, targetType);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("for(GRGEN_LGSP.LGSPEdge head{0} = graph.edgesByTypeHeads[{1}], candidate{0} = head{0}.lgspTypeNext; candidate{0} != head{0}; candidate{0} = candidate{0}.lgspTypeNext)\n", this.Id, targetType);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
            for(LGSPEdge head = graph.edgesByTypeHeads[targetType], candidate = head.lgspTypeNext; candidate != head; candidate = candidate.lgspTypeNext)
            {
                if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                    continue;
                candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
                edge = candidate;
                matched |= next.Execute(graph, includingAttributes, matches);
                candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                if(matches==null &&  matched)
                    return true;
            }
            return matched;
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
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: BothDirections\n", this.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("for(direction{0} = 0; direction{0} < 2; ++direction{0})\n", this.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
            for(direction = 0; direction < 2; ++direction)
            {
                matched |= next.Execute(graph, includingAttributes, matches);
                if(matches==null && matched)
                    return true;
            }
            return matched;
        }
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an incoming edge from a source node
    /// </summary>
    public class InterpretationPlanIncoming : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanIncoming(int targetType, InterpretationPlanNodeMatcher source, 
            SearchPlanEdgeNode planEdgeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.planEdgeNode = planEdgeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: Incoming {1} from {2}\n", this.Id, targetType, source.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge head{0} = candidate{1}.lgspInhead;\n", this.Id, source.Id);
            builder.AppendFrontFormat("if(head{0} != null)\n", this.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge candidate{0} = head{0};\n", this.Id);
            builder.AppendFront("do\n");
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
            builder.AppendFrontFormat("while((candidate{0} = candidate{0}.lgspInNext) != head{0});\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
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
                    matched |= next.Execute(graph, includingAttributes, matches);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matches==null && matched)
                        return true;
                }
                while((candidate = candidate.lgspInNext) != head);
            }
            return matched;
        }

        int targetType;
        InterpretationPlanNodeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an outgoing edge from a source node
    /// </summary>
    public class InterpretationPlanOutgoing : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanOutgoing(int targetType, InterpretationPlanNodeMatcher source,
            SearchPlanEdgeNode planEdgeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.planEdgeNode = planEdgeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: Outgoing {1} to {2}\n", this.Id, targetType, source.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge head{0} = candidate{1}.lgspOuthead;\n", this.Id, source.Id);
            builder.AppendFrontFormat("if(head{0} != null)\n", this.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge candidate{0} = head{0};\n", this.Id);
            builder.AppendFront("do\n");
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
            builder.AppendFrontFormat("while((candidate{0} = candidate{0}.lgspOutNext) != head{0});\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
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
                    matched |= next.Execute(graph, includingAttributes, matches);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matches==null && matched)
                        return true;
                }
                while((candidate = candidate.lgspOutNext) != head);
            }
            return matched;
        }

        int targetType;
        InterpretationPlanNodeMatcher source;
    }

    /// <summary>
    /// Interpretation plan operation which retrieves an edge to be matched bidirectionally from a source node
    /// </summary>
    public class InterpretationPlanIncomingOrOutgoing : InterpretationPlanEdgeMatcher
    {
        public InterpretationPlanIncomingOrOutgoing(int targetType, InterpretationPlanNodeMatcher source,
            InterpretationPlanDirectionVariable directionVariable, SearchPlanEdgeNode planEdgeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.directionVariable = directionVariable;
            this.planEdgeNode = planEdgeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: IncomingOrOutgoing {1} from/to {2} direction {3}\n", this.Id, targetType, source.Id, directionVariable.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge head{0} = direction{2}==0 ? candidate{1}.lgspInhead : candidate{1}.lgspOuthead;\n", this.Id, source.Id, directionVariable.Id);
            builder.AppendFrontFormat("if(head{0} != null)\n", this.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPEdge candidate{0} = head{0};\n", this.Id);
            builder.AppendFront("do\n");
            builder.AppendFront("{\n");
            builder.Indent();
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
            builder.AppendFrontFormat("while((candidate{0} = (direction{1}==0 ? candidate{0}.lgspInhead : candidate{0}.lgspOuthead)) != head{0});\n", this.Id, directionVariable.Id);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            bool matched = false;
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
                    matched |= next.Execute(graph, includingAttributes, matches);
                    candidate.lgspFlags &= ~((uint)LGSPElemFlags.IS_MATCHED);
                    if(matches==null && matched)
                        return true;
                }
                while((candidate = (directionVariable.direction==0 ? candidate.lgspInNext : candidate.lgspOutNext)) != head);
            }
            return matched;
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
        public InterpretationPlanImplicitTarget(int targetType, InterpretationPlanEdgeMatcher source,
            SearchPlanNodeNode planNodeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.planNodeNode = planNodeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitTarget {1} of {2}\n", this.Id, targetType, source.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFront("do {\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPNode candidate{0} = candidate{1}.lgspTarget;\n", this.Id, source.Id);
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("} while(false);\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            LGSPNode candidate = source.edge.lgspTarget;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph, includingAttributes, matches);
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
        public InterpretationPlanImplicitSource(int targetType, InterpretationPlanEdgeMatcher source,
            SearchPlanNodeNode planNodeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.planNodeNode = planNodeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitSource {1} of {2}\n", this.Id, targetType, source.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFront("do {\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPNode candidate{0} = candidate{1}.lgspSource;\n", this.Id, source.Id);
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("} while(false);\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            LGSPNode candidate = source.edge.lgspSource;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph, includingAttributes, matches);
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
        public InterpretationPlanImplicitSourceOrTarget(int targetType, InterpretationPlanEdgeMatcher source,
            InterpretationPlanDirectionVariable directionVariable, SearchPlanNodeNode planNodeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.directionVariable = directionVariable;
            this.planNodeNode = planNodeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitSourceOrTarget {1} of {2} direction {3}\n", this.Id, targetType, source.Id, directionVariable.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFront("do {\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPNode candidate{0} = direction{2} == 0 ? candidate{1}.lgspSource : candidate{1}.lgspTarget;\n", this.Id, source.Id, directionVariable.Id);
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("} while(false);\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            LGSPNode candidate = directionVariable.direction == 0 ? source.edge.lgspSource : source.edge.lgspTarget;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph, includingAttributes, matches);
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
        public InterpretationPlanImplicitTheOther(int targetType, InterpretationPlanEdgeMatcher source,
            InterpretationPlanNodeMatcher theOther, SearchPlanNodeNode planNodeNode)
        {
            this.targetType = targetType;
            this.source = source;
            this.theOther = theOther;
            this.planNodeNode = planNodeNode;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("{0}: ImplicitTheOther {1} of {2} the other is {3}\n", this.Id, targetType, source.Id, theOther.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFront("do {\n");
            builder.Indent();
            builder.AppendFrontFormat("GRGEN_LGSP.LGSPNode candidate{0} = candidate{2} == candidate{1}.lgspSource ? candidate{1}.lgspTarget : candidate{1}.lgspSource;\n", this.Id, source.Id, theOther.Id);
            builder.AppendFrontFormat("if(candidate{0}.lgspType.TypeID != {1})\n", this.Id, targetType);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("if((candidate{0}.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED) != 0)\n", this.Id);
            builder.AppendFront("\tcontinue;\n");
            builder.AppendFrontFormat("candidate{0}.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED;\n", this.Id);
            next.Emit(builder);
            builder.AppendFrontFormat("candidate{0}.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n", this.Id);
            builder.Unindent();
            builder.AppendFront("} while(false);\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            LGSPNode candidate = theOther.node == source.edge.lgspSource ? source.edge.lgspTarget : source.edge.lgspSource;
            if(candidate.lgspType.TypeID != targetType)
                return false;
            if((candidate.lgspFlags & (uint)LGSPElemFlags.IS_MATCHED) != 0)
                return false;
            candidate.lgspFlags |= (uint)LGSPElemFlags.IS_MATCHED;
            node = candidate;
            bool matched = next.Execute(graph, includingAttributes, matches);
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
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0} -{1}->\n", node.Id, edge.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("if(candidate{0}.lgspSource == candidate{1})\n", edge.Id, node.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if(edge.edge.lgspSource == node.node)
                return next.Execute(graph, includingAttributes, matches);
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
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness -{0}-> {1}\n", edge.Id, node.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("if(candidate{0}.lgspTarget == candidate{1})\n", edge.Id, node.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if(edge.edge.lgspTarget == node.node)
                return next.Execute(graph, includingAttributes, matches);
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
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0}? <-{1}-> ?{0} direction {2}\n", node.Id, edge.Id, directionVariable.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("if((direction{2} == 0 ? candidate{0}.lgspSource : candidate{0}.lgspTarget) == candidate{1})\n", edge.Id, node.Id, directionVariable.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if((directionVariable.direction == 0 ? edge.edge.lgspSource : edge.edge.lgspTarget) == node.node)
                return next.Execute(graph, includingAttributes, matches);
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
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("CheckConnectedness {0}? <-{1}-> ?{0} the other {2}\n", node.Id, edge.Id, theOther.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFrontFormat("if((candidate{2} == candidate{0}.lgspSource ? candidate{0}.lgspTarget : candidate{0}.lgspSource) == candidate{1})\n", edge.Id, node.Id, theOther.Id);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if((theOther.node == edge.edge.lgspSource ? edge.edge.lgspTarget : edge.edge.lgspSource) == node.node)
                return next.Execute(graph, includingAttributes, matches);
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
        public InterpretationPlanCheckCondition(expression.AreAttributesEqual condition, InterpretationPlanNodeMatcher nodeMatcher, int indexOfCorrespondingNode)
        {
            this.condition = condition;
            this.nodeMatcher = nodeMatcher;
            this.indexOfCorrespondingElement = indexOfCorrespondingNode;
            AssignId();
        }

        public InterpretationPlanCheckCondition(expression.AreAttributesEqual condition, InterpretationPlanEdgeMatcher edgeMatcher, int indexOfCorrespondingEdge)
        {
            this.condition = condition;
            this.edgeMatcher = edgeMatcher;
            this.indexOfCorrespondingElement = indexOfCorrespondingEdge;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            if(nodeMatcher != null)
                builder.AppendFrontFormat("CheckCondition on node {0}\n", nodeMatcher.Id);
            else
                builder.AppendFrontFormat("CheckCondition on edge {0}\n", edgeMatcher.Id);
            next.Dump(builder);
        }

        public override void Emit(SourceBuilder builder)
        {
            if(nodeMatcher!=null)
                builder.AppendFrontFormat("if(!includingAttributes || thisPattern.correspondingNodes[{1}].AreAttributesEqual(candidate{0}))\n", nodeMatcher.Id, indexOfCorrespondingElement);
            else
                builder.AppendFrontFormat("if(!includingAttributes || thisPattern.correspondingEdges[{1}].AreAttributesEqual(candidate{0}))\n", edgeMatcher.Id, indexOfCorrespondingElement);
            builder.AppendFront("{\n");
            builder.Indent();
            next.Emit(builder);
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if(!includingAttributes || condition.Execute(nodeMatcher != null ? (IGraphElement)nodeMatcher.node : (IGraphElement)edgeMatcher.edge))
                return next.Execute(graph, includingAttributes, matches);
            else
                return false;
        }

        expression.AreAttributesEqual condition;
        InterpretationPlanNodeMatcher nodeMatcher;
        InterpretationPlanEdgeMatcher edgeMatcher;
        int indexOfCorrespondingElement;
    }

    /// <summary>
    /// Interpretation plan operation which completes a match;
    /// no own functionality, it just succeeds when execution reaches it
    /// </summary>
    public class InterpretationPlanMatchComplete : InterpretationPlan
    {
        public InterpretationPlanMatchComplete(int numNodes, int numEdges)
        {
            this.numNodes = numNodes;
            this.numEdges = numEdges;
            AssignId();
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("MatchComplete\n");
        }

        public override void Emit(SourceBuilder builder)
        {
            builder.AppendFront("{\n");
            builder.Indent();
            // emit code to unmark all matched elements -- which are all elements of the graph -- and leave
            builder.AppendFront("foreach(GRGEN_LIBGR.NodeType nodeType in graph.Model.NodeModel.Types)\n");
            builder.AppendFront("\tfor(GRGEN_LGSP.LGSPNode nodeHead = graph.nodesByTypeHeads[nodeType.TypeID], node = nodeHead.lgspTypeNext; node != nodeHead; node = node.lgspTypeNext)\n");
            builder.AppendFront("\t\tnode.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n");

            builder.AppendFront("foreach(GRGEN_LIBGR.EdgeType edgeType in graph.Model.EdgeModel.Types)\n");
            builder.AppendFront("\tfor(GRGEN_LGSP.LGSPEdge edgeHead = graph.edgesByTypeHeads[edgeType.TypeID], edge = edgeHead.lgspTypeNext; edge != edgeHead; edge = edge.lgspTypeNext)\n");
            builder.AppendFront("\t\tedge.lgspFlags &= ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED);\n");

            builder.AppendFront("return true;\n");
            builder.Unindent();
            builder.AppendFront("}\n");
        }

        public override bool Execute(LGSPGraph graph, bool includingAttributes, List<FoundMatch> matches)
        {
            if(matches != null)
            {
                FoundMatch match = new FoundMatch(numNodes, numEdges);

                InterpretationPlan cur = this;
                while(cur != null)
                {
                    if(cur is InterpretationPlanNodeMatcher)
                    {
                        InterpretationPlanNodeMatcher nm = (InterpretationPlanNodeMatcher)cur;
                        match.nodes[nm.planNodeNode.ElementID - 1] = nm.node;
                    }
                    else if(cur is InterpretationPlanEdgeMatcher)
                    {
                        InterpretationPlanEdgeMatcher em = (InterpretationPlanEdgeMatcher)cur;
                        match.edges[em.planEdgeNode.ElementID - 1] = em.edge;
                    }

                    cur = cur.prev;
                }

                matches.Add(match);
            }
            return true;
        }

        int numNodes;
        int numEdges;
    }
}

