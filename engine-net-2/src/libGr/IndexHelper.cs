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
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, value))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexSame(INode candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

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
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(node == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInNodesFromIndexFromTo(INode candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

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
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexSame(IEdge candidate, IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

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
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        public static bool IsInEdgesFromIndexFromTo(IEdge candidate, IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            if(!candidate.Type.IsA(((AttributeIndexDescription)index.Description).GraphElementType))
                return false;

            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(edge == candidate)
                    return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns the nodes in the index whose attribute is the same as the value given, as array
        /// </summary>
        public static List<INode> NodesFromIndexSameAsArray(IAttributeIndex index, object value)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexSameAsArray(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as array, ordered ascendingly
        /// </summary>
        public static List<INode> NodesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the nodes in the index whose attribute is in the range from from to to, as array, ordered descendingly
        /// </summary>
        public static List<INode> NodesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        public static List<INode> NodesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            List<INode> nodesArray = new List<INode>();
            foreach(INode node in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesArray.Add(node);
            }
            return nodesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is the same as the value given, as array
        /// </summary>
        public static List<IEdge> EdgesFromIndexSameAsArray(IAttributeIndex index, object value)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexSameAsArray(IAttributeIndex index, object value, IActionExecutionEnvironment actionEnv)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, value))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as array, ordered ascendingly
        /// </summary>
        public static List<IEdge> EdgesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexFromToAsArrayAscending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerable(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        /// <summary>
        /// Returns the edges in the index whose attribute is in the range from from to to, as array, ordered descendingly
        /// </summary>
        public static List<IEdge> EdgesFromIndexFromToAsArrayDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        public static List<IEdge> EdgesFromIndexFromToAsArrayDescendings(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo, IActionExecutionEnvironment actionEnv)
        {
            List<IEdge> edgesArray = new List<IEdge>();
            foreach(IEdge edge in GetIndexEnumerableDescending(index, from, includingFrom, to, includingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesArray.Add(edge);
            }
            return edgesArray;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(params IndexAccess[] indexAccesses)
        {
            return NodesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses));
        }

        /// <summary>
        /// Returns the nodes that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountNodesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the nodes from the first index query
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(INode node in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                nodesSet.Add(node, null);
            }

            // a series of sets is produced by reducing to the nodes that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i=1; i < indexAccesses.Count; ++i)
            {
                nodesSet = RemoveNodesThatDontAppearInTheIndexAccessResult(nodesSet, indexAccesses[i]);
            }
            
            return nodesSet;
        }

        private static Dictionary<INode, SetValueType> RemoveNodesThatDontAppearInTheIndexAccessResult(Dictionary<INode, SetValueType> nodesSet, IndexAccess indexAccess)
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

        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(IActionExecutionEnvironment actionEnv, params IndexAccess[] indexAccesses)
        {
            return NodesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), actionEnv);
        }

        /// <summary>
        /// Returns the nodes that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<INode, SetValueType> NodesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, IActionExecutionEnvironment actionEnv)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountNodesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, actionEnv);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the nodes from the first index query
            Dictionary<INode, SetValueType> nodesSet = new Dictionary<INode, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(INode node in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                nodesSet.Add(node, null);
            }

            // a series of sets is produced by reducing to the nodes that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                nodesSet = RemoveNodesThatDontAppearInTheIndexAccessResult(nodesSet, indexAccesses[i], actionEnv);
            }

            return nodesSet;
        }

        private static Dictionary<INode, SetValueType> RemoveNodesThatDontAppearInTheIndexAccessResult(Dictionary<INode, SetValueType> nodesSet, IndexAccess indexAccess, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<INode, SetValueType> resultSet = new Dictionary<INode, SetValueType>(nodesSet.Count);
            foreach(INode node in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(nodesSet.ContainsKey(node))
                {
                    resultSet.Add(node, null);
                }
            }
            return resultSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(params IndexAccess[] indexAccesses)
        {
            return EdgesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses));
        }

        /// <summary>
        /// Returns the edges that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountEdgesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo);
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
                edgesSet = RemoveEdgesThatDontAppearInTheIndexAccessResult(edgesSet, indexAccesses[i]);
            }

            return edgesSet;
        }

        private static Dictionary<IEdge, SetValueType> RemoveEdgesThatDontAppearInTheIndexAccessResult(Dictionary<IEdge, SetValueType> edgesSet, IndexAccess indexAccess)
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

        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(IActionExecutionEnvironment actionEnv, params IndexAccess[] indexAccesses)
        {
            return EdgesFromIndexMultipleFromTo(new List<IndexAccess>(indexAccesses), actionEnv);
        }

        /// <summary>
        /// Returns the edges that appear in the result sets of all index accesses/queries (multi-index-join), as set
        /// </summary>
        public static Dictionary<IEdge, SetValueType> EdgesFromIndexMultipleFromTo(List<IndexAccess> indexAccesses, IActionExecutionEnvironment actionEnv)
        {
            if(indexAccesses.Count == 0)
                throw new System.Exception("At least one index access must be given");

            foreach(IndexAccess indexAccess in indexAccesses)
            {
                indexAccess.NumberOfResults = CountEdgesFromIndexFromTo(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo, actionEnv);
            }

            indexAccesses.Sort(TheIndexAccessComparer);

            // the initial set are the edges from the first index query
            Dictionary<IEdge, SetValueType> edgesSet = new Dictionary<IEdge, SetValueType>(indexAccesses[0].NumberOfResults);
            foreach(IEdge edge in GetIndexEnumerable(indexAccesses[0].Index, indexAccesses[0].From, indexAccesses[0].IncludingFrom, indexAccesses[0].To, indexAccesses[0].IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                edgesSet.Add(edge, null);
            }

            // a series of sets is produced by reducing to the edges that appear also in the result sets of the queries of the other indices, index query by index query
            for(int i = 1; i < indexAccesses.Count; ++i)
            {
                edgesSet = RemoveEdgesThatDontAppearInTheIndexAccessResult(edgesSet, indexAccesses[i], actionEnv);
            }

            return edgesSet;
        }

        private static Dictionary<IEdge, SetValueType> RemoveEdgesThatDontAppearInTheIndexAccessResult(Dictionary<IEdge, SetValueType> edgesSet, IndexAccess indexAccess, IActionExecutionEnvironment actionEnv)
        {
            Dictionary<IEdge, SetValueType> resultSet = new Dictionary<IEdge, SetValueType>(edgesSet.Count);
            foreach(IEdge edge in GetIndexEnumerable(indexAccess.Index, indexAccess.From, indexAccess.IncludingFrom, indexAccess.To, indexAccess.IncludingTo))
            {
                ++actionEnv.PerformanceInfo.SearchSteps;
                if(edgesSet.ContainsKey(edge))
                {
                    resultSet.Add(edge, null);
                }
            }
            return resultSet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public class IndexAccess
        {
            public IndexAccess(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
            {
                Index = index;
                From = from;
                IncludingFrom = includingFrom;
                To = to;
                IncludingTo = includingTo;
            }

            public IndexAccess(IAttributeIndex index, object from, object to)
            {
                Index = index;
                From = from;
                IncludingFrom = true;
                To = to;
                IncludingTo = true;
            }

            public IAttributeIndex Index;
            public object From;
            public bool IncludingFrom;
            public object To;
            public bool IncludingTo;

            internal int NumberOfResults; // used to carry out a performance optimization: join query results from lowest number of results to highest number of results
        }

        internal class IndexAccessComparer : IComparer<IndexAccess>
        {
            public int Compare(IndexAccess this_, IndexAccess that)
            {
                return this_.NumberOfResults.CompareTo(that.NumberOfResults);
            }
        }
        static IndexAccessComparer TheIndexAccessComparer = new IndexAccessComparer();

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

        private static IEnumerable<IGraphElement> GetIndexEnumerableDescending(IAttributeIndex index, object from, bool includingFrom, object to, bool includingTo)
        {
            if(from != null)
            {
                if(includingFrom)
                {
                    if(to != null)
                    {
                        if(includingTo)
                            return index.LookupElementsDescendingFromInclusiveToInclusive(from, to);
                        else
                            return index.LookupElementsDescendingFromInclusiveToExclusive(from, to);
                    }
                    else
                    {
                        return index.LookupElementsDescendingFromInclusive(from);
                    }
                }
                else
                {
                    if(to != null)
                    {
                        if(includingTo)
                            return index.LookupElementsDescendingFromExclusiveToInclusive(from, to);
                        else
                            return index.LookupElementsDescendingFromExclusiveToExclusive(from, to);
                    }
                    else
                    {
                        return index.LookupElementsDescendingFromExclusive(from);
                    }
                }
            }
            else
            {
                if(to != null)
                {
                    if(includingTo)
                        return index.LookupElementsDescendingToInclusive(to);
                    else
                        return index.LookupElementsDescendingToExclusive(to);
                }
                else
                {
                    return index.LookupElementsDescending();
                }
            }
        }
    }
}
