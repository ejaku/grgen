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

        /// <summary>
        /// Returns the edges in the index whose attribute is the same as the value given, as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexSame(IAttributeIndex index, object value)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of nodes in the index whose attribute is the same as the value given
        /// </summary>
        public static int CountNodesFromIndexSame(IAttributeIndex index, object value)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++count;
            }
            return count;
        }

        public static int CountNodesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes in the index whose attribute is in the range from from to to
        /// </summary>
        public static int CountNodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++count;
            }
            return count;
        }

        public static int CountNodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the edges in the index whose attribute is the same as the value given
        /// </summary>
        public static int CountEdgesFromIndexSame(IAttributeIndex index, object value)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++count;
            }
            return count;
        }

        public static int CountEdgesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the edges in the index whose attribute is in the range from from to to
        /// </summary>
        public static int CountEdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++count;
            }
            return count;
        }

        public static int CountEdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                ++count;
            }
            return count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the candidate node is contained in the nodes in the index whose attribute is the same as the value given
        /// </summary>
        public static bool IsInNodesFromIndexSame(INode candidate, IAttributeIndex index, object value)
        {
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexSame(INode candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(node == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate node is contained in the nodes in the index whose attribute is in the range from from to to
        /// </summary>
        public static bool IsInNodesFromIndexFromTo(INode candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexFromTo(INode candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(node == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate edge is contained in the edges in the index whose attribute is the same as the value given
        /// </summary>
        public static bool IsInEdgesFromIndexSame(IEdge candidate, IAttributeIndex index, object value)
        {
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexSame(IEdge candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate edge is contained in the edges in the index whose attribute is in the range from from to to
        /// </summary>
        public static bool IsInEdgesFromIndexFromTo(IEdge candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexFromTo(IEdge candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        private static IEnumerable<IGraphElement> GetIndexEnumerable(IAttributeIndex index, object value)
        {
            return index.LookupElements(value);
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
