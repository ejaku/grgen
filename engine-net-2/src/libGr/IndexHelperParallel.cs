/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections.Generic;

// don't forget IndexHelper.cs for the non-parallelized versions

namespace de.unika.ipd.grGen.libGr
{
    public static partial class IndexHelper
    {
        /// <summary>
        /// Returns the nodes in the index whose attribute is the same as the value given, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexSame(IAttributeIndex index, object value, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> NodesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> NodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesSet[node] = null;
            }
            return nodesSet;
        }
    }
}
