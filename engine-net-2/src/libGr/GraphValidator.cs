/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace de.unika.ipd.grGen.libGr
{
    public class GraphValidator
    {
        /// <summary>
        /// Checks whether a graph meets its connection assertions.
        /// </summary>
        /// <param name="graph">The graph to validate.</param>
        /// <param name="mode">The validation mode to apply.</param>
        /// <param name="errors">If the graph is not valid, this refers to a List of ConnectionAssertionError objects, otherwise it is null.</param>
        /// <returns>True, if the graph is valid.</returns>
        public static bool Validate(IGraph graph, ValidationMode mode, out List<ConnectionAssertionError> errors)
        {
            bool result = true;
            Dictionary<IEdge, bool> checkedOutEdges = new Dictionary<IEdge, bool>(2 * graph.NumEdges);
            Dictionary<IEdge, bool> checkedInEdges = new Dictionary<IEdge, bool>(2 * graph.NumEdges);
            errors = new List<ConnectionAssertionError>();

            int numConnectionAssertions = 0;
            foreach(ValidateInfo valInfo in graph.Model.ValidateInfo)
            {
                // Check outgoing count on nodes of source type
                foreach(INode node in graph.GetCompatibleNodes(valInfo.SourceType))
                {
                    result &= ValidateSource(node, valInfo, errors, checkedOutEdges, checkedInEdges);
                }
                // Check incoming count on nodes of target type
                foreach(INode node in graph.GetCompatibleNodes(valInfo.TargetType))
                {
                    result &= ValidateTarget(node, valInfo, errors, checkedOutEdges, checkedInEdges);
                }

                ++numConnectionAssertions;
            }

            if(mode == ValidationMode.StrictOnlySpecified)
            {
                Dictionary<EdgeType, bool> strictnessCheckedEdgeTypes = new Dictionary<EdgeType, bool>(2 * numConnectionAssertions);
                foreach(ValidateInfo valInfo in graph.Model.ValidateInfo)
                {
                    if(strictnessCheckedEdgeTypes.ContainsKey(valInfo.EdgeType))
                        continue;

                    foreach(IEdge edge in graph.GetExactEdges(valInfo.EdgeType))
                    {
                        // Some edges with connection assertions specified are not covered; strict only specified validation prohibits that!
                        if(!checkedOutEdges.ContainsKey(edge) || !checkedInEdges.ContainsKey(edge))
                        {
                            errors.Add(new ConnectionAssertionError(CAEType.EdgeNotSpecified, edge, 0, null));
                            result = false;
                        }
                    }
                    strictnessCheckedEdgeTypes.Add(valInfo.EdgeType, true);
                }
            }

            if(mode == ValidationMode.Strict
                && (graph.NumEdges != checkedOutEdges.Count || graph.NumEdges != checkedInEdges.Count))
            {
                // Some edges are not covered; strict validation prohibits that!
                foreach(IEdge edge in graph.Edges)
                {
                    if(!checkedOutEdges.ContainsKey(edge) || !checkedInEdges.ContainsKey(edge))
                    {
                        errors.Add(new ConnectionAssertionError(CAEType.EdgeNotSpecified, edge, 0, null));
                        result = false;
                    }
                }
            }

            if(result) errors = null;
            return result;
        }

        private static bool ValidateSource(INode node, ValidateInfo valInfo, List<ConnectionAssertionError> errors,
            Dictionary<IEdge, bool> checkedOutEdges, Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check outgoing edges
            long num = CountOutgoing(node, valInfo.EdgeType, valInfo.TargetType, checkedOutEdges);
            if(valInfo.BothDirections)
            {
                long incoming = CountIncoming(node, valInfo.EdgeType, valInfo.TargetType, checkedInEdges);
                num -= CountReflexive(node, valInfo.EdgeType, valInfo.TargetType, num, incoming);
                num += incoming;
            }

            if(num < valInfo.SourceLower)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooFewSources, node, num, valInfo));
                result = false;
            }
            else if(num > valInfo.SourceUpper)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooManySources, node, num, valInfo));
                result = false;
            }

            return result;
        }

        private static bool ValidateTarget(INode node, ValidateInfo valInfo, List<ConnectionAssertionError> errors,
            Dictionary<IEdge, bool> checkedOutEdges, Dictionary<IEdge, bool> checkedInEdges)
        {
            bool result = true;

            // Check incoming edges
            long num = CountIncoming(node, valInfo.EdgeType, valInfo.SourceType, checkedInEdges);
            if(valInfo.BothDirections)
            {
                long outgoing = CountOutgoing(node, valInfo.EdgeType, valInfo.SourceType, checkedOutEdges);
                num -= CountReflexive(node, valInfo.EdgeType, valInfo.SourceType, outgoing, num);
                num += outgoing;
            }

            if(num < valInfo.TargetLower)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooFewTargets, node, num, valInfo));
                result = false;
            }
            else if(num > valInfo.TargetUpper)
            {
                errors.Add(new ConnectionAssertionError(CAEType.NodeTooManyTargets, node, num, valInfo));
                result = false;
            }

            return result;
        }

        private static long CountOutgoing(INode node, EdgeType edgeType, NodeType targetNodeType,
            Dictionary<IEdge, bool> checkedOutEdges)
        {
            long num = 0;
            foreach(IEdge outEdge in node.GetExactOutgoing(edgeType))
            {
                if(!outEdge.Target.Type.IsA(targetNodeType)) continue;
                checkedOutEdges[outEdge] = true;
                ++num;
            }
            return num;
        }

        private static long CountIncoming(INode node, EdgeType edgeType, NodeType sourceNodeType,
            Dictionary<IEdge, bool> checkedInEdges)
        {
            long num = 0;
            foreach(IEdge inEdge in node.GetExactIncoming(edgeType))
            {
                if(!inEdge.Source.Type.IsA(sourceNodeType)) continue;
                checkedInEdges[inEdge] = true;
                ++num;
            }
            return num;
        }

        private static long CountReflexive(INode node, EdgeType edgeType, NodeType oppositeNodeType,
            long outgoing, long incoming)
        {
            long num = 0;
            if(outgoing <= incoming)
            {
                foreach(IEdge outEdge in node.GetExactOutgoing(edgeType))
                {
                    if(!outEdge.Target.Type.IsA(oppositeNodeType)) continue;
                    if(outEdge.Target != node) continue;
                    ++num;
                }
            }
            else
            {
                foreach(IEdge inEdge in node.GetExactIncoming(edgeType))
                {
                    if(!inEdge.Source.Type.IsA(oppositeNodeType)) continue;
                    if(inEdge.Source != node) continue;
                    ++num;
                }
            }
            return num;
        }
    }
}
