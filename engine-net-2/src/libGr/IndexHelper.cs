/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System.Collections.Generic;

// don't forget IndexHelperParallel.cs for the parallelized versions

namespace de.unika.ipd.grGen.libGr
{
    public static partial class IndexHelper
    {
        /// <summary>
        /// Returns the nodes in the index whose attribute is the same as the value given, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexSame(IAttributeIndex index, object value)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> NodesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        private static IEnumerable<IGraphElement> GetIndexEnumerable(IAttributeIndex index, object value)
        {
            return index.LookupElements(value);
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        public static Dictionary<INode, SetValueType> NodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesSet[node] = null;
            }
            return nodesSet;
        }

        private static IEnumerable<IGraphElement> GetIndexEnumerable(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            if(from != null)
            {
                if(includingFrom)
                {
                    if(to != null)
                    {
                        if(includingTo)
                            return index.LookupElementsAscendingFromInclusiveToInclusive(from, to);
                        else
                            return index.LookupElementsAscendingFromInclusiveToExclusive(from, to);
                    }
                    else
                    {
                        return index.LookupElementsAscendingFromInclusive(from);
                    }
                }
                else
                {
                    if(to != null)
                    {
                        if(includingTo)
                            return index.LookupElementsAscendingFromExclusiveToInclusive(from, to);
                        else
                            return index.LookupElementsAscendingFromExclusiveToExclusive(from, to);
                    }
                    else
                    {
                        return index.LookupElementsAscendingFromExclusive(from);
                    }
                }
            }
            else
            {
                if(to != null)
                {
                    if(includingTo)
                        return index.LookupElementsAscendingToInclusive(to);
                    else
                        return index.LookupElementsAscendingToExclusive(to);
                }
                else
                {
                    return index.LookupElementsAscending();
                }
            }
        }
    }
}
