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

        /// <summary>
        /// Returns the edges in the index whose attribute is the same as the value given, as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexSame(IAttributeIndex index, object value, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet[edge] = null;
            }
            return edgesSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the count of the nodes in the index whose attribute is the same as the value given
        /// </summary>
        public static int CountNodesFromIndexSame(IAttributeIndex index, object value, int threadId)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++count;
            }
            return count;
        }

        public static int CountNodesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the nodes in the index whose attribute is in the range from from to to
        /// </summary>
        public static int CountNodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++count;
            }
            return count;
        }

        public static int CountNodesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the edges in the index whose attribute is the same as the value given
        /// </summary>
        public static int CountEdgesFromIndexSame(IAttributeIndex index, object value, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++count;
            }
            return count;
        }

        public static int CountEdgesFromIndexSame(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                ++count;
            }
            return count;
        }

        /// <summary>
        /// Returns the count of the edges in the index whose attribute is in the range from from to to
        /// </summary>
        public static int CountEdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++count;
            }
            return count;
        }

        public static int CountEdgesFromIndexFromTo(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            int count = 0;
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                ++count;
            }
            return count;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns whether the candidate node is contained in the nodes in the index whose attribute is the same as the value given
        /// </summary>
        public static bool IsInNodesFromIndexSame(INode candidate, IAttributeIndex index, object value, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, value))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexSame(INode candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(node == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate node is contained in the nodes in the index whose attribute is in the range from from to to
        /// </summary>
        public static bool IsInNodesFromIndexFromTo(INode candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexFromTo(INode candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(node == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate edge is contained in the edges in the index whose attribute is the same as the value given
        /// </summary>
        public static bool IsInEdgesFromIndexSame(IEdge candidate, IAttributeIndex index, object value, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexSame(IEdge candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the candidate edge is contained in the edges in the index whose attribute is in the range from from to to
        /// </summary>
        public static bool IsInEdgesFromIndexFromTo(IEdge candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexFromTo(IEdge candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the nodes in the index whose attribute is the same as the value given, as array
        /// </summary>
        public static List<INode> NodesFromIndexSameAsArray(IAttributeIndex index, object value, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexSameAsArray(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as array, ordered ascendingly
        /// </summary>
        public static List<INode> NodesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as array, ordered descendingly
        /// </summary>
        public static List<INode> NodesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is the same as the value given, as array
        /// </summary>
        public static List<IEdge> EdgesFromIndexSameAsArray(IAttributeIndex index, object value, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexSameAsArray(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as array, ordered ascendingly
        /// </summary>
        public static List<IEdge> EdgesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as array, ordered descendingly
        /// </summary>
        public static List<IEdge> EdgesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv, int threadId)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(int threadId, params IndexAccess[] indexAccesses)
        {
            return NodesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), threadId);
        }

        /// <summary>
        /// Returns the nodes that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, int threadId)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountNodesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, threadId);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the nodes from the first index query
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(INode node in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                nodesSet.Add(node, null);
            }

            // a series of sets is produced by reducing to the nodes that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                nodesSet = RemoveNodesThatDontAppearInTheIndexAccessResult(nodesSet, indexAccesses[i], threadId);
            }

            return nodesSet;
        }

        private static Dictionary<INode, SetValueType> RemoveNodesThatDontAppearInTheIndexAccessResult(Dictionary<INode, SetValueType> nodesSet, IndexAccess indexAccess, int threadId)
        {
            Dictionary<INode, SetValueType> resultSet = new Dictionary<INode, SetValueType>(nodesSet.Count);
            foreach(INode node in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                if(nodesSet.ContainsKey(node))
                {
                    resultSet.Add(node, null);
                }
            }
            return resultSet;
        }

        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(IActionExecutionEnvironment actionEnv, int threadId, params IndexAccess[] indexAccesses)
        {
            return NodesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), actionEnv, threadId);
        }

        /// <summary>
        /// Returns the nodes that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountNodesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, actionEnv, threadId);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the nodes from the first index query
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(INode node in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                nodesSet.Add(node, null);
            }

            // a series of sets is produced by reducing to the nodes that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                nodesSet = RemoveNodesThatDontAppearInTheIndexAccessResult(nodesSet, indexAccesses[i], actionEnv, threadId);
            }

            return nodesSet;
        }

        private static Dictionary<INode, SetValueType> RemoveNodesThatDontAppearInTheIndexAccessResult(Dictionary<INode, SetValueType> nodesSet, IndexAccess indexAccess, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<INode, SetValueType> resultSet = new Dictionary<INode, SetValueType>(nodesSet.Count);
            foreach(INode node in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(nodesSet.ContainsKey(node))
                {
                    resultSet.Add(node, null);
                }
            }
            return resultSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(int threadId, params IndexAccess[] indexAccesses)
        {
            return EdgesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), threadId);
        }

        /// <summary>
        /// Returns the edges that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, int threadId)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountEdgesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, threadId);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the edges from the first index query
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(IEdge edge in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                edgesSet.Add(edge, null);
            }

            // a series of sets is produced by reducing to the edges that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                edgesSet = RemoveEdgesThatDontAppearInTheIndexAccessResult(edgesSet, indexAccesses[i], threadId);
            }

            return edgesSet;
        }

        private static Dictionary<IEdge, SetValueType> RemoveEdgesThatDontAppearInTheIndexAccessResult(Dictionary<IEdge, SetValueType> edgesSet, IndexAccess indexAccess, int threadId)
        {
            Dictionary<IEdge, SetValueType> resultSet = new Dictionary<IEdge, SetValueType>(edgesSet.Count);
            foreach(IEdge edge in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                if(edgesSet.ContainsKey(edge))
                {
                    resultSet.Add(edge, null);
                }
            }
            return resultSet;
        }

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(IActionExecutionEnvironment actionEnv, int threadId, params IndexAccess[] indexAccesses)
        {
            return EdgesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), actionEnv, threadId);
        }

        /// <summary>
        /// Returns the edges that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, IActionExecutionEnvironment actionEnv, int threadId)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountEdgesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, actionEnv, threadId);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the edges from the first index query
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(IEdge edge in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                edgesSet.Add(edge, null);
            }

            // a series of sets is produced by reducing to the edges that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                edgesSet = RemoveEdgesThatDontAppearInTheIndexAccessResult(edgesSet, indexAccesses[i], actionEnv, threadId);
            }

            return edgesSet;
        }

        private static Dictionary<IEdge, SetValueType> RemoveEdgesThatDontAppearInTheIndexAccessResult(Dictionary<IEdge, SetValueType> edgesSet, IndexAccess indexAccess, IActionExecutionEnvironment actionEnv, int threadId)
        {
            Dictionary<IEdge, SetValueType> resultSet = new Dictionary<IEdge, SetValueType>(edgesSet.Count);
            foreach(IEdge edge in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
                if(edgesSet.ContainsKey(edge))
                {
                    resultSet.Add(edge, null);
                }
            }
            return resultSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the bottom node with lowest indexed value from the index, or null
        /// </summary>
        public static INode MinNodeFromIndex(IAttributeIndex index, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsAscending().GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current as INode : null;
        }

        public static INode MinNodeFromIndex(IAttributeIndex index, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsAscending().GetEnumerator();
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return enumerator.MoveNext() ? enumerator.Current as INode : null;
        }

        /// <summary>
        /// Returns the top node with highest indexed value from the index, or null
        /// </summary>
        public static INode MaxNodeFromIndex(IAttributeIndex index, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsDescending().GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current as INode : null;
        }

        public static INode MaxNodeFromIndex(IAttributeIndex index, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsDescending().GetEnumerator();
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return enumerator.MoveNext() ? enumerator.Current as INode : null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the bottom edge with lowest indexed value from the index, or null
        /// </summary>
        public static IEdge MinEdgeFromIndex(IAttributeIndex index, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsAscending().GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current as IEdge : null;
        }

        public static IEdge MinEdgeFromIndex(IAttributeIndex index, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsAscending().GetEnumerator();
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return enumerator.MoveNext() ? enumerator.Current as IEdge : null;
        }

        /// <summary>
        /// Returns the top edge with highest indexed value from the index, or null
        /// </summary>
        public static IEdge MaxEdgeFromIndex(IAttributeIndex index, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsDescending().GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current as IEdge : null;
        }

        public static IEdge MaxEdgeFromIndex(IAttributeIndex index, IActionExecutionEnvironment actionEnv, int threadId)
        {
            IEnumerator<IGraphElement> enumerator = index.LookupElementsDescending().GetEnumerator();
            ++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];
            return enumerator.MoveNext() ? enumerator.Current as IEdge : null;
        }
    }
}
