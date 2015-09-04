/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// A class for building the interpretation plan data structure from a scheduled search plan
    /// </summary>
    public class InterpretationPlanBuilder
    {
        // the scheduled search plan to build an interpretation plan for
        private ScheduledSearchPlan ssp;

        // the search plan graph for determining the pattern element needed for attribute checking
        private SearchPlanGraph spg;

        // the model over which the patterns are to be searched
        private IGraphModel model;


        /// <summary>
        /// Creates an interpretation plan builder for the given scheduled search plan.
        /// Only a limited amount of search operations is supported, the ones needed for isomorphy checking.
        /// </summary>
        /// <param name="ssp">the scheduled search plan to build an interpretation plan for</param>
        /// <param name="spg">the search plan graph for determining the pattern element needed for attribute checking</param>
        /// <param name="model">the model over which the patterns are to be searched</param>
        public InterpretationPlanBuilder(ScheduledSearchPlan ssp, SearchPlanGraph spg, IGraphModel model)
        {
            this.ssp = ssp;
            this.spg = spg;
            this.model = model;
        }

        /// <summary>
        /// Builds interpretation plan from scheduled search plan.
        /// </summary>
        public InterpretationPlan BuildInterpretationPlan(string comparisonMatcherName)
        {
            // create the start operation which is a nop but needed as first insertion point
            InterpretationPlan plan = new InterpretationPlanStart(comparisonMatcherName);

            // build the interpretation plan into it, starting with the search operation at position 0 in the scheduled search plan
            BuildInterpretationPlan(plan, 0);

            // clean the helper variables in the search plan nodes used in building the interpretation plan
            for(int i = 0; i < ssp.Operations.Length; ++i)
            {
                if(ssp.Operations[i].Element is SearchPlanNodeNode)
                {
                    SearchPlanNodeNode node = ssp.Operations[i].Element as SearchPlanNodeNode;
                    node.nodeMatcher = null;
                }
                else if(ssp.Operations[i].Element is SearchPlanEdgeNode)
                {
                    SearchPlanEdgeNode edge = ssp.Operations[i].Element as SearchPlanEdgeNode;
                    edge.edgeMatcher = null;
                    edge.directionVariable = null;
                }
            }

            // and return the plan starting with the created start operation
            return plan;
        }

        /// <summary>
        /// Recursively assembles interpretation plan from scheduled search plan, beginning at index 0.
        /// Decides which specialized build procedure is to be called.
        /// The specialized build procedure then calls this procedure again, 
        /// in order to process the next search plan operation at the following index.
        /// The insertionPoint is the lastly built operation;
        /// the next operation built is inserted into the next link of it, 
        /// then it becomes the current insertionPoint.
        /// </summary>
        private void BuildInterpretationPlan(InterpretationPlan insertionPoint, int index)
        {
            if (index >= ssp.Operations.Length)
            { // end of scheduled search plan reached, stop recursive iteration
                buildMatchComplete(insertionPoint);
                return;
            }

            SearchOperation op = ssp.Operations[index];

            // for current scheduled search plan operation 
            // insert corresponding interpretation plan operations into interpretation plan
            switch (op.Type)
            {
                case SearchOperationType.Lookup:
                    if(((SearchPlanNode)op.Element).NodeType == PlanNodeType.Node)
                        buildLookup(insertionPoint, index, (SearchPlanNodeNode)op.Element);
                    else
                        buildLookup(insertionPoint, index, (SearchPlanEdgeNode)op.Element);
                    break;

                case SearchOperationType.ImplicitSource:
                    buildImplicit(insertionPoint, index,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        ImplicitNodeType.Source);
                    break;

                case SearchOperationType.ImplicitTarget:
                    buildImplicit(insertionPoint, index,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        ImplicitNodeType.Target);
                    break;

                case SearchOperationType.Implicit:
                    buildImplicit(insertionPoint, index,
                        (SearchPlanEdgeNode)op.SourceSPNode,
                        (SearchPlanNodeNode)op.Element,
                        ImplicitNodeType.SourceOrTarget);
                    break;

                case SearchOperationType.Incoming:
                    buildIncident(insertionPoint, index,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.Incoming);
                    break;

                case SearchOperationType.Outgoing:
                    buildIncident(insertionPoint, index,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.Outgoing);
                    break;

                case SearchOperationType.Incident:
                    buildIncident(insertionPoint, index,
                        (SearchPlanNodeNode)op.SourceSPNode,
                        (SearchPlanEdgeNode)op.Element,
                        IncidentEdgeType.IncomingOrOutgoing);
                    break;

                case SearchOperationType.Condition:
                    buildCondition(insertionPoint, index,
                        (PatternCondition)op.Element);
                    break;

                default:
                    throw new Exception("Unknown or unsupported search operation");
            }
        }

        /// <summary>
        /// Interpretation plan operations implementing the
        /// Lookup node search plan operation
        /// are created and inserted into the interpretation plan at the insertion point
        /// </summary>
        private void buildLookup(
            InterpretationPlan insertionPoint, int index,
            SearchPlanNodeNode target)
        {
            // get candidate = iterate available nodes
            target.nodeMatcher = new InterpretationPlanLookupNode(target.PatternElement.TypeID, target);
            target.nodeMatcher.prev = insertionPoint;
            insertionPoint.next = target.nodeMatcher;
            insertionPoint = insertionPoint.next;

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFromLookup(insertionPoint,
                target);

            // continue with next operation
            target.Visited = true;
            BuildInterpretationPlan(insertionPoint, index + 1);
            target.Visited = false;
        }

        /// <summary>
        /// Interpretation plan operations implementing the
        /// Lookup edge search plan operation
        /// are created and inserted into the interpretation plan at the insertion point
        /// </summary>
        private void buildLookup(
            InterpretationPlan insertionPoint, int index,
            SearchPlanEdgeNode target)
        {
            // get candidate = iterate available edges
            target.edgeMatcher = new InterpretationPlanLookupEdge(target.PatternElement.TypeID, target);
            target.edgeMatcher.prev = insertionPoint;
            insertionPoint.next = target.edgeMatcher;
            insertionPoint = insertionPoint.next;

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFromLookup(insertionPoint,
                target);

            // continue with next operation
            target.Visited = true;
            BuildInterpretationPlan(insertionPoint, index + 1);
            target.Visited = false;
        }

        /// <summary>
        /// Interpretation plan operations implementing the
        /// Implicit Source|Target|SourceOrTarget search plan operation
        /// are created and inserted into the interpretation plan at the insertion point
        /// </summary>
        private void buildImplicit(
            InterpretationPlan insertionPoint, int index,
            SearchPlanEdgeNode source,
            SearchPlanNodeNode target,
            ImplicitNodeType nodeType)
        {
            // get candidate = demanded node from edge
            insertionPoint = insertImplicitNodeFromEdge(insertionPoint,
                source, target, nodeType);

            // check connectedness of candidate
            SearchPlanNodeNode otherNodeOfOriginatingEdge = null;
            if (nodeType == ImplicitNodeType.Source) otherNodeOfOriginatingEdge = source.PatternEdgeTarget;
            if (nodeType == ImplicitNodeType.Target) otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            if (source.PatternEdgeTarget == source.PatternEdgeSource) // reflexive sign needed in unfixed direction case, too
                otherNodeOfOriginatingEdge = source.PatternEdgeSource;
            insertionPoint = decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(insertionPoint,
                target, source, otherNodeOfOriginatingEdge);

            // continue with next operation
            target.Visited = true;
            BuildInterpretationPlan(insertionPoint, index + 1);
            target.Visited = false;
        }

        /// <summary>
        /// Interpretation plan operations implementing the
        /// Extend Incoming|Outgoing|IncomingOrOutgoing search plan operation
        /// are created and inserted into the interpretation plan at the insertion point
        /// </summary>
        private void buildIncident(
            InterpretationPlan insertionPoint, int index,
            SearchPlanNodeNode source,
            SearchPlanEdgeNode target,
            IncidentEdgeType edgeType)
        {
            // get candidate = iterate available incident edges
            insertionPoint = insertIncidentEdgeFromNode(insertionPoint,
                source, target, edgeType);

            // check connectedness of candidate
            insertionPoint = decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(insertionPoint,
                target, source, edgeType==IncidentEdgeType.Incoming);

            // continue with next operation
            target.Visited = true;
            BuildInterpretationPlan(insertionPoint, index + 1);
            target.Visited = false;
        }

        /// <summary>
        /// Interpretation plan operations implementing the
        /// CheckCondition search plan operation
        /// are created and inserted into the interpretation plan at the insertion point
        /// </summary>
        private void buildCondition(
            InterpretationPlan insertionPoint, int index,
            PatternCondition condition)
        {
            // check condition
            expression.AreAttributesEqual aae = (expression.AreAttributesEqual)condition.ConditionExpression;
            foreach(SearchPlanNode spn in spg.Nodes)
            {
                if(spn.PatternElement == aae.thisInPattern)
                {
                    InterpretationPlanCheckCondition checkCondition;
                    if(spn is SearchPlanNodeNode)
                    {
                        SearchPlanNodeNode nodeNode = (SearchPlanNodeNode)spn;
                        checkCondition = new InterpretationPlanCheckCondition(
                            aae, nodeNode.nodeMatcher, Array.IndexOf(aae.thisInPattern.pointOfDefinition.nodes, aae.thisInPattern));
                    }
                    else
                    {
                        SearchPlanEdgeNode edgeNode = (SearchPlanEdgeNode)spn;
                        checkCondition = new InterpretationPlanCheckCondition(
                            aae, edgeNode.edgeMatcher, Array.IndexOf(aae.thisInPattern.pointOfDefinition.edges, aae.thisInPattern));
                    }

                    checkCondition.prev = insertionPoint;
                    insertionPoint.next = checkCondition;
                    insertionPoint = insertionPoint.next;

                    // continue with next operation
                    BuildInterpretationPlan(insertionPoint, index + 1);
                    return;
                }
            }
            throw new Exception("Internal error constructing interpretation plan!");
        }

        /// <summary>
        /// The closing matching completed interpretation plan operation is added at the insertion point
        /// </summary>
        private void buildMatchComplete(InterpretationPlan insertionPoint)
        {
            insertionPoint.next = new InterpretationPlanMatchComplete(
                ssp.PatternGraph.nodesPlusInlined.Length,
                ssp.PatternGraph.edgesPlusInlined.Length);
            insertionPoint.next.prev = insertionPoint;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Inserts code to get an implicit node from an edge
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan insertImplicitNodeFromEdge(
            InterpretationPlan insertionPoint, 
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode currentNode,
            ImplicitNodeType nodeType)
        {
            int targetType = currentNode.PatternElement.TypeID;
            if (nodeType == ImplicitNodeType.Source)
            {
                currentNode.nodeMatcher = new InterpretationPlanImplicitSource(targetType, edge.edgeMatcher,
                    currentNode);
            }
            else if(nodeType == ImplicitNodeType.Target)
            {
                currentNode.nodeMatcher = new InterpretationPlanImplicitTarget(targetType, edge.edgeMatcher,
                    currentNode);
            }
            else
            {
                Debug.Assert(nodeType != ImplicitNodeType.TheOther);

                if (currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
                {
                    currentNode.nodeMatcher = new InterpretationPlanImplicitTheOther(targetType, edge.edgeMatcher,
                        edge.PatternEdgeSource == currentNode ? edge.PatternEdgeTarget.nodeMatcher : edge.PatternEdgeSource.nodeMatcher,
                        currentNode);
                }
                else // edge connects to first incident node
                {
                    if (edge.PatternEdgeSource == edge.PatternEdgeTarget)
                    {
                        // reflexive edge without direction iteration as we don't want 2 matches 
                        currentNode.nodeMatcher = new InterpretationPlanImplicitSource(targetType, edge.edgeMatcher,
                            currentNode);
                    }
                    else
                    {
                        edge.directionVariable = new InterpretationPlanBothDirections();
                        insertionPoint.next = edge.directionVariable;
                        insertionPoint = insertionPoint.next;
                        currentNode.nodeMatcher = new InterpretationPlanImplicitSourceOrTarget(targetType, edge.edgeMatcher, 
                            edge.directionVariable, currentNode);
                    }
                }
            }

            currentNode.nodeMatcher.prev = insertionPoint;
            insertionPoint.next = currentNode.nodeMatcher;
            insertionPoint = insertionPoint.next;
            return insertionPoint;
        }

        /// <summary>
        /// Inserts code to get an incident edge from some node
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan insertIncidentEdgeFromNode(
            InterpretationPlan insertionPoint, 
            SearchPlanNodeNode node,
            SearchPlanEdgeNode currentEdge,
            IncidentEdgeType incidentType)
        {
            int targetType = currentEdge.PatternElement.TypeID;
            if (incidentType == IncidentEdgeType.Incoming)
            {
                currentEdge.edgeMatcher = new InterpretationPlanIncoming(targetType, node.nodeMatcher,
                    currentEdge);
            }
            else if(incidentType == IncidentEdgeType.Outgoing)
            {
                currentEdge.edgeMatcher = new InterpretationPlanOutgoing(targetType, node.nodeMatcher,
                    currentEdge);
            }
            else // IncidentEdgeType.IncomingOrOutgoing
            {
                if (currentEdge.PatternEdgeSource == currentEdge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    currentEdge.edgeMatcher = new InterpretationPlanIncoming(targetType, node.nodeMatcher,
                        currentEdge);
                }
                else
                {
                    currentEdge.directionVariable = new InterpretationPlanBothDirections();
                    insertionPoint.next = currentEdge.directionVariable;
                    insertionPoint = insertionPoint.next;
                    currentEdge.edgeMatcher = new InterpretationPlanIncomingOrOutgoing(targetType, node.nodeMatcher, 
                        currentEdge.directionVariable, currentEdge);
                }
            }

            currentEdge.edgeMatcher.prev = insertionPoint;
            insertionPoint.next = currentEdge.edgeMatcher;
            insertionPoint = insertionPoint.next;
            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node just determined by lookup
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfNodeFromLookup(
            InterpretationPlan insertionPoint,
            SearchPlanNodeNode node)
        {
            // check for edges required by the pattern to be incident to the given node
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(insertionPoint,
                        node, edge, CheckCandidateForConnectednessType.Source);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(insertionPoint,
                        node, edge);
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(insertionPoint,
                        node, edge, CheckCandidateForConnectednessType.Target);
                }
                else
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(insertionPoint,
                        node, edge);
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node just drawn from edge
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfImplicitNodeFromEdge(
            InterpretationPlan insertionPoint,
            SearchPlanNodeNode node,
            SearchPlanEdgeNode originatingEdge,
            SearchPlanNodeNode otherNodeOfOriginatingEdge)
        {
            // check for edges required by the pattern to be incident to the given node
            // only if the node was not taken from the given originating edge
            //   with the exception of reflexive edges, as these won't get checked thereafter
            foreach (SearchPlanEdgeNode edge in node.OutgoingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(insertionPoint,
                            node, edge, CheckCandidateForConnectednessType.Source);
                    }
                }
                else
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(insertionPoint,
                            node, edge);
                    }
                }
            }
            foreach (SearchPlanEdgeNode edge in node.IncomingPatternEdges)
            {
                if (((PatternEdge)edge.PatternElement).fixedDirection
                    || edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeFixedDirection(insertionPoint,
                            node, edge, CheckCandidateForConnectednessType.Target);
                    }
                }
                else
                {
                    if (edge != originatingEdge || node == otherNodeOfOriginatingEdge)
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfNodeBothDirections(insertionPoint,
                            node, edge);
                    }
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node and edge of fixed direction
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfNodeFixedDirection(
            InterpretationPlan insertionPoint,
            SearchPlanNodeNode currentNode,
            SearchPlanEdgeNode edge,
            CheckCandidateForConnectednessType connectednessType)
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.Source || connectednessType == CheckCandidateForConnectednessType.Target);

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            // only if edge is already matched by now (signaled by visited)
            if (edge.Visited)
            {
                if(connectednessType == CheckCandidateForConnectednessType.Source)
                {
                    insertionPoint.next = new InterpretationPlanCheckConnectednessSource(
                            currentNode.nodeMatcher, edge.edgeMatcher);
                    insertionPoint = insertionPoint.next;
                }
                else //if(connectednessType == CheckCandidateForConnectednessType.Target)
                {
                    insertionPoint.next = new InterpretationPlanCheckConnectednessTarget(
                            currentNode.nodeMatcher, edge.edgeMatcher);
                    insertionPoint = insertionPoint.next;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given node and edge in both directions
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfNodeBothDirections(
            InterpretationPlan insertionPoint,
            SearchPlanNodeNode currentNode,
            SearchPlanEdgeNode edge)
        {
            Debug.Assert(edge.PatternEdgeSource != edge.PatternEdgeTarget);

            // check whether the pattern edges which must be incident to the candidate node (according to the pattern)
            // are really incident to it
            if (currentNodeIsFirstIncidentNodeOfEdge(currentNode, edge))
            {
                edge.directionVariable = new InterpretationPlanBothDirections();
                insertionPoint.next = edge.directionVariable;
                insertionPoint = insertionPoint.next;
                insertionPoint.next = new InterpretationPlanCheckConnectednessSourceOrTarget(
                        currentNode.nodeMatcher, edge.edgeMatcher, edge.directionVariable);
                insertionPoint = insertionPoint.next;
            }
            if (currentNodeIsSecondIncidentNodeOfEdge(currentNode, edge))
            {
                insertionPoint.next = new InterpretationPlanCheckConnectednessTheOther(
                        currentNode.nodeMatcher, edge.edgeMatcher,
                        edge.PatternEdgeSource == currentNode ? edge.PatternEdgeTarget.nodeMatcher : edge.PatternEdgeSource.nodeMatcher);
                insertionPoint = insertionPoint.next;
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge determined by lookup
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfEdgeFromLookup(
            InterpretationPlan insertionPoint,
            SearchPlanEdgeNode edge)
        {
            if (((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                if (edge.PatternEdgeSource != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(insertionPoint,
                        edge, CheckCandidateForConnectednessType.Source);
                }
                if (edge.PatternEdgeTarget != null)
                {
                    insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(insertionPoint,
                        edge, CheckCandidateForConnectednessType.Target);
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(insertionPoint,
                    edge, false);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge determined from incident node
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertions point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfIncidentEdgeFromNode(
            InterpretationPlan insertionPoint,
            SearchPlanEdgeNode edge,
            SearchPlanNodeNode originatingNode,
            bool edgeIncomingAtOriginatingNode)
        {
            if (((PatternEdge)edge.PatternElement).fixedDirection)
            {
                // don't need to check if the edge is not required by the pattern to be incident to some given node
                // or if the edge was taken from the given originating node
                if (edge.PatternEdgeSource != null)
                {
                    if (!(!edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeSource == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(insertionPoint,
                            edge, CheckCandidateForConnectednessType.Source);
                    }
                }
                if (edge.PatternEdgeTarget != null)
                {
                    if (!(edgeIncomingAtOriginatingNode
                            && edge.PatternEdgeTarget == originatingNode))
                    {
                        insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(insertionPoint,
                            edge, CheckCandidateForConnectednessType.Target);
                    }
                }
            }
            else
            {
                insertionPoint = decideOnAndInsertCheckConnectednessOfEdgeBothDirections(insertionPoint,
                    edge, true);
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge of fixed direction
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfEdgeFixedDirection(
            InterpretationPlan insertionPoint,
            SearchPlanEdgeNode edge,
            CheckCandidateForConnectednessType connectednessType)
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.Source || connectednessType == CheckCandidateForConnectednessType.Target);

            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            // don't need to, if that node is not matched by now (signaled by visited)
            SearchPlanNodeNode nodeRequiringCheck = 
                connectednessType == CheckCandidateForConnectednessType.Source ?
                    edge.PatternEdgeSource : edge.PatternEdgeTarget;
            if (nodeRequiringCheck.Visited)
            {
                if(connectednessType == CheckCandidateForConnectednessType.Source)
                {
                    insertionPoint.next = new InterpretationPlanCheckConnectednessSource(
                            nodeRequiringCheck.nodeMatcher, edge.edgeMatcher);
                    insertionPoint = insertionPoint.next;
                }
                else //if(connectednessType == CheckCandidateForConnectednessType.Target)
                {
                    insertionPoint.next = new InterpretationPlanCheckConnectednessTarget(
                            nodeRequiringCheck.nodeMatcher, edge.edgeMatcher);
                    insertionPoint = insertionPoint.next;
                }
            }

            return insertionPoint;
        }

        /// <summary>
        /// Decides which check connectedness operations are needed for the given edge in both directions
        /// and inserts them into the interpretation plan
        /// receives insertion point, returns new insertion point
        /// </summary>
        private InterpretationPlan decideOnAndInsertCheckConnectednessOfEdgeBothDirections(
            InterpretationPlan insertionPoint,
            SearchPlanEdgeNode edge,
            bool edgeDeterminationContainsFirstNodeLoop)
        {
            // check whether source/target-nodes of the candidate edge
            // are the same as the already found nodes to which the edge must be incident
            if (!edgeDeterminationContainsFirstNodeLoop && currentEdgeConnectsToFirstIncidentNode(edge))
            {
                SearchPlanNodeNode nodeRequiringFirstNodeLoop = edge.PatternEdgeSource != null ?
                    edge.PatternEdgeSource : edge.PatternEdgeTarget;

                // due to currentEdgeConnectsToFirstIncidentNode: at least on incident node available
                if (edge.PatternEdgeSource == edge.PatternEdgeTarget)
                {
                    // reflexive edge without direction iteration as we don't want 2 matches 
                    insertionPoint.next = new InterpretationPlanCheckConnectednessSource(
                            nodeRequiringFirstNodeLoop.nodeMatcher, edge.edgeMatcher); // might be Target as well
                    insertionPoint = insertionPoint.next;
                }
                else
                {
                    edge.directionVariable = new InterpretationPlanBothDirections();
                    insertionPoint.next = edge.directionVariable;
                    insertionPoint = insertionPoint.next;
                    insertionPoint.next = new InterpretationPlanCheckConnectednessSourceOrTarget(
                            nodeRequiringFirstNodeLoop.nodeMatcher, edge.edgeMatcher, edge.directionVariable);
                    insertionPoint = insertionPoint.next;
                }
            }
            if (currentEdgeConnectsToSecondIncidentNode(edge))
            {
                // due to currentEdgeConnectsToSecondIncidentNode: both incident node available
                insertionPoint.next = new InterpretationPlanCheckConnectednessTheOther(
                        edge.PatternEdgeTarget.nodeMatcher, edge.edgeMatcher, edge.PatternEdgeSource.nodeMatcher);
                insertionPoint = insertionPoint.next;
            }

            return insertionPoint;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// returns true if the node which gets currently determined in the schedule
        /// is the first incident node of the edge which gets connected to it
        /// only of interest for edges of unfixed direction
        /// </summary>
        private bool currentNodeIsFirstIncidentNodeOfEdge(
            SearchPlanNodeNode currentNode, SearchPlanEdgeNode edge)
        {
            //Debug.Assert(!currentNode.Visited);
            Debug.Assert(!((PatternEdge)edge.PatternElement).fixedDirection);

            if (!edge.Visited) return false;

            if (currentNode == edge.PatternEdgeSource)
                return edge.PatternEdgeTarget==null || !edge.PatternEdgeTarget.Visited;
            else
                return edge.PatternEdgeSource==null || !edge.PatternEdgeSource.Visited;
        }

        /// <summary>
        /// returns true if the node which gets currently determined in the schedule
        /// is the second incident node of the edge which gets connected to it
        /// only of interest for edges of unfixed direction
        /// </summary>
        private bool currentNodeIsSecondIncidentNodeOfEdge(
            SearchPlanNodeNode currentNode, SearchPlanEdgeNode edge)
        {
            //Debug.Assert(!currentNode.Visited);
            Debug.Assert(!((PatternEdge)edge.PatternElement).fixedDirection);

            if (!edge.Visited) return false;

            if (edge.PatternEdgeSource == null || edge.PatternEdgeTarget == null)
                return false;

            if (currentNode == edge.PatternEdgeSource)
                return edge.PatternEdgeTarget.Visited;
            else
                return edge.PatternEdgeSource.Visited;
        }

        /// <summary>
        /// returns true if only one incident node of the edge which gets currently determined in the schedule
        /// was already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsOnlyToFirstIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if (currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited
                && (currentEdge.PatternEdgeTarget == null || !currentEdge.PatternEdgeTarget.Visited))
            {
                return true;
            }
            if (currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited
                && (currentEdge.PatternEdgeSource == null || !currentEdge.PatternEdgeSource.Visited))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// returns true if at least one incident node of the edge which gets currently determined in the schedule
        /// was already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsToFirstIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if (currentEdge.PatternEdgeSource != null && currentEdge.PatternEdgeSource.Visited)
                return true;
            if (currentEdge.PatternEdgeTarget != null && currentEdge.PatternEdgeTarget.Visited)
                return true;

            return false;
        }

        /// <summary>
        /// returns true if both incident nodes of the edge which gets currently determined in the schedule
        /// were already computed; only of interest for edges of unfixed direction
        /// </summary>
        private bool currentEdgeConnectsToSecondIncidentNode(SearchPlanEdgeNode currentEdge)
        {
            //Debug.Assert(!currentEdge.Visited);
            Debug.Assert(!((PatternEdge)currentEdge.PatternElement).fixedDirection);

            if (currentEdge.PatternEdgeSource == null || currentEdge.PatternEdgeTarget == null)
                return false;

            if (currentEdge.PatternEdgeSource.Visited && currentEdge.PatternEdgeTarget.Visited)
                return true;

            return false;
        }
    }
}

