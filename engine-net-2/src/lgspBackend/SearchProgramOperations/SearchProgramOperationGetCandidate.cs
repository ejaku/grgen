/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

using System;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for search program candidate determining operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    abstract class GetCandidate : SearchProgramOperation
    {
        protected GetCandidate(string patternElementName)
        {
            PatternElementName = patternElementName;
        }

        public string PatternElementName;
    }

    /// <summary>
    /// Available types of GetCandidateByIteration operations
    /// </summary>
    enum GetCandidateByIterationType
    {
        GraphElements, // available graph elements
        IncidentEdges, // incident edges
        StorageElements, // available elements of the storage variable
        StorageAttributeElements, // available elements of the storage attribute
        IndexElements, // available elements of the index
        //GraphElementAttribute, // available graph element of the attribute
        //GlobalVariable, // available element of the global non-storage variable
        //GlobalVariableStorageElements // available elements of the storage global variable
    } // TODO: letzte 3

    /// <summary>
    /// The different possibilites an edge might be incident to some node
    /// incoming; outgoing; incoming or outgoing if arbitrary directed, undirected, arbitrary
    /// </summary>
    enum IncidentEdgeType
    {
        Incoming,
        Outgoing,
        IncomingOrOutgoing
    }

    /// <summary>
    /// The different possibilties an index might be accessed
    /// </summary>
    enum IndexAccessType
    {
        Equality,
        Ascending,
        Descending
    }

    /// <summary>
    /// Class representing "get candidate by iteration" operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    class GetCandidateByIteration : GetCandidate
    {
        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            bool parallel,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            Parallel = parallel;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration ");
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict?"Dictionary":"List/Deque");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict?"Dictionary":"List/Deque");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                // code comments: lookup comment was already emitted with type iteration/drawing

                // open loop header 
                sourceCode.AppendFrontFormat("for(");
                // emit declaration of variable containing graph elements list head
                string typeOfVariableContainingListHead = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingListHead =
                    NamesOfEntities.CandidateIterationListHead(PatternElementName);
                sourceCode.AppendFormat("{0} {1}",
                    typeOfVariableContainingListHead, variableContainingListHead);
                // emit initialization of variable containing graph elements list head
                string graphMemberContainingListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                string variableContainingTypeIDForCandidate =
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFormat(" = graph.{0}[{1}], ",
                    graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                // emit declaration and initialization of variable containing candidates
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFormat("{0} = {1}.lgspTypeNext; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop condition: check for head reached again 
                sourceCode.AppendFormat("{0} != {1}; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop increment: switch to next element of same type
                sourceCode.AppendFormat("{0} = {0}.lgspTypeNext",
                    variableContainingCandidate);
                // close loop header
                sourceCode.Append(")\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List/Deque", StorageName);
                }

                // emit loop header with variable containing container entry
                string variableContainingStorage =
                    NamesOfEntities.Variable(StorageName);
                string storageIterationVariable = 
                    NamesOfEntities.CandidateIterationContainerEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    IterationType, storageIterationVariable, variableContainingStorage);
                
                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List/Deque", StorageOwnerName, StorageAttributeName);
                }

                // emit loop header with variable containing container entry
                string variableContainingStorage =
                    "((" + StorageOwnerTypeName + ")" + NamesOfEntities.CandidateVariable(StorageOwnerName) + ")." + StorageAttributeName;
                string storageIterationVariable =
                    NamesOfEntities.CandidateIterationContainerEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    IterationType, storageIterationVariable, variableContainingStorage);

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                // emit loop header with variable containing container entry
                string indexIterationVariable =
                    NamesOfEntities.CandidateIterationIndexEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in (({2})graph.Indices).{3}.",
                        IterationType, indexIterationVariable, IndexSetType, IndexName);
                if(IndexAccessType==IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}))\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}))\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    else if(IndexFrom != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}))\n",
                            accessType, indexFromIncluded, IndexFrom);
                    else if(IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}))\n",
                            accessType, indexToIncluded, IndexTo);
                    else
                        sourceCode.AppendFormat("Lookup{0}())\n",
                            accessType);
                }

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    indexIterationVariable);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if(EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    // emit declaration of variable containing incident edges list head
                    string variableContainingListHead =
                        NamesOfEntities.CandidateIterationListHead(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}", variableContainingListHead);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    string memberOfNodeContainingListHead =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInhead" : "lgspOuthead";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointNode, memberOfNodeContainingListHead);

                    // emit execute the following code only if head != null
                    // todo: replace by check == null and continue
                    sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates
                    string typeOfVariableContainingCandidate = "GRGEN_LGSP.LGSPEdge";
                    string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                        typeOfVariableContainingCandidate, variableContainingCandidate,
                        variableContainingListHead);
                    // open loop
                    sourceCode.AppendFront("do\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit loop tail
                    // - emit switch to next edge in list within assignment expression
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";
                    sourceCode.AppendFrontFormat("while( ({0} = {0}.{1})",
                        variableContainingCandidate, memberOfEdgeContainingNextEdge);
                    // - check condition that head has been reached again (compare with assignment value)
                    sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                    // close the head != null check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    // we've to search both lists, we do so by first searching incoming, then outgoing
                    string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
                    // emit declaration of variable containing incident edges list head
                    string variableContainingListHead =
                        NamesOfEntities.CandidateIterationListHead(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}", variableContainingListHead);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    sourceCode.AppendFormat(" = {0}==0 ? {1}.lgspInhead : {1}.lgspOuthead;\n",
                        directionRunCounter, variableContainingStartingPointNode);

                    // emit execute the following code only if head != null
                    // todo: replace by check == null and continue
                    sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates
                    string typeOfVariableContainingCandidate = "GRGEN_LGSP.LGSPEdge";
                    string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                        typeOfVariableContainingCandidate, variableContainingCandidate,
                        variableContainingListHead);
                    // open loop
                    sourceCode.AppendFront("do\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    EmitProfilingAsNeededPre(sourceCode);
                    
                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // emit loop tail
                    // - emit switch to next edge in list within assignment expression
                    sourceCode.AppendFrontFormat("while( ({0}==0 ? {1} = {1}.lgspInNext : {1} = {1}.lgspOutNext)",
                        directionRunCounter, variableContainingCandidate);
                    // - check condition that head has been reached again (compare with assignment value)
                    sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                    // close the head != null check
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                } //EdgeType 
            } //Type
        }

        private void EmitProfilingAsNeededPre(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(PackagePrefixedActionName != null && EmitFirstLoopProfiling)
            {
                if(Parallel)
                {
                    sourceCode.AppendFront("++actionEnv.PerformanceInfo.LoopStepsPerThread[threadId];\n");
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
                }
                else
                {
                    sourceCode.AppendFront("++loopSteps;\n");
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchSteps;\n");
                }
            }
            if(Parallel)
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
            else
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchSteps;\n");
        }

        private void EmitProfilingAsNeededPost(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(PackagePrefixedActionName != null && EmitFirstLoopProfiling)
            {
                if(Parallel)
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                }
                else
                {
                    sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                    sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[0].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchSteps - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                }
            }
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public readonly GetCandidateByIterationType Type;
        public readonly bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements|IndexElements
        public readonly bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public readonly string StorageName; // only available if StorageElements
        public readonly string StorageOwnerName; // only available if StorageAttributeElements
        public readonly string StorageOwnerTypeName; // only available if StorageAttributeElements
        public readonly string StorageAttributeName; // only available if StorageAttributeElements
        public readonly string IterationType; // only available if StorageElements|StorageAttributeElements|IndexElements
        public readonly string IndexName; // only available if IndexElements
        public readonly string IndexSetType; // only available if IndexElements
        public readonly IndexAccessType IndexAccessType; // only available if IndexElements
        public readonly string IndexEqual; // only available if IndexElements
        public readonly string IndexFrom; // only available if IndexElements
        public readonly bool IndexFromIncluded; // only available if IndexElements
        public readonly string IndexTo; // only available if IndexElements
        public readonly bool IndexToIncluded; // only available if IndexElements
        public readonly string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public readonly IncidentEdgeType EdgeType; // only available if IncidentEdges
        public readonly bool Parallel;
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedActionName;
        public readonly bool EmitFirstLoopProfiling;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing parallelized "get candidate by iteration" operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    class GetCandidateByIterationParallel : GetCandidate
    {
        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallel(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration Parallel");
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            }
            else
            { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Parallelized Lookup {0} \n", PatternElementName);

                if(EmitProfiling)
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                string variableContainingParallelizedCandidate =
                    NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                string variableContainingParallelizedListHead =
                    NamesOfEntities.IterationParallelizationListHead(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for head reached again 
                sourceCode.AppendFormat("{0} != {1}",
                    variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration and initialization of variable containing candidates from parallelized next candidate
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelizedCandidate);
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                // emit loop increment: switch to next element of same type
                sourceCode.AppendFrontFormat("{0} = {0}.lgspTypeNext;\n",
                    variableContainingParallelizedCandidate);
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator, IsDict ? ".Key" : "");
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageOwnerName, StorageAttributeName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator, IsDict ? ".Key" : "");
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                EmitIterationParallelizationLockAquire(sourceCode);

                // variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);

                // open loop header, and emit early out condition: another thread already found the required amount of matches 
                sourceCode.AppendFront("while(!maxMatchesFound && (");
                // emit loop condition: check for iteration end 
                sourceCode.AppendFormat("{0}.MoveNext()",
                    variableContainingParallelizedIterator);
                // close loop header
                sourceCode.Append("))\n");

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with container entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.Current;\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingParallelizedIterator);
                sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");
                sourceCode.AppendFront("++iterationNumber;\n");

                EmitIterationParallelizationLockRelease(sourceCode);

                EmitProfilingAsNeededPre(sourceCode);

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                EmitProfilingAsNeededPost(sourceCode);

                sourceCode.Append("\n");
                EmitIterationParallelizationLockAquire(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                EmitIterationParallelizationLockRelease(sourceCode);
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Parallelized Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if(EmitProfiling)
                    sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");

                if(EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    EmitIterationParallelizationLockAquire(sourceCode);

                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);

                    // open loop header, and emit early out condition: another thread already found the required amount of matches 
                    sourceCode.AppendFront("while(!maxMatchesFound && (");
                    // emit loop condition: check whether head has been reached again 
                    sourceCode.AppendFormat("{0} != {1} || iterationNumber==0",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                    // close loop header
                    sourceCode.Append("))\n");

                    // open loop
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates from parallelized next candidate
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                    // emit loop increment: switch to next edge in list
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";
                    sourceCode.AppendFrontFormat("{0} = {0}.{1};\n",
                        variableContainingParallelizedCandidate, memberOfEdgeContainingNextEdge);
                    sourceCode.AppendFront("++iterationNumber;\n");

                    EmitIterationParallelizationLockRelease(sourceCode);

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    sourceCode.Append("\n");
                    EmitIterationParallelizationLockAquire(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    EmitIterationParallelizationLockRelease(sourceCode);
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    EmitIterationParallelizationLockAquire(sourceCode);

                    string directionRunCounterParallelized =
                        NamesOfEntities.IterationParallelizationDirectionRunCounterVariable(PatternElementName);
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);

                    // open loop header, and emit early out condition: another thread already found the required amount of matches 
                    sourceCode.AppendFront("while(!maxMatchesFound && (");
                    // emit loop condition: check whether head has been reached again 
                    sourceCode.AppendFormat("{0} != {1} || iterationNumber==0",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);
                    // close loop header
                    sourceCode.Append("))\n");

                    // open loop
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();

                    // emit declaration and initialization of variable containing candidates from parallelized next candidate
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    sourceCode.AppendFront("currentIterationNumber = iterationNumber;\n");

                    // emit loop increment: switch to next edge in list
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {0}.lgspInNext : {0}.lgspOutNext;\n",
                        variableContainingParallelizedCandidate, directionRunCounterParallelized);
                    sourceCode.AppendFront("++iterationNumber;\n");
                    
                    EmitIterationParallelizationLockRelease(sourceCode);

                    EmitProfilingAsNeededPre(sourceCode);

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

                    EmitProfilingAsNeededPost(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    // get iteration parallelization lock
                    sourceCode.Append("\n");
                    EmitIterationParallelizationLockAquire(sourceCode);

                    // close loop
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");

                    EmitIterationParallelizationLockRelease(sourceCode);
                } //EdgeType 
            } //Type
        }

        private void EmitProfilingAsNeededPre(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(PackagePrefixedActionName != null && EmitFirstLoopProfiling)
            {
                sourceCode.AppendFront("++actionEnv.PerformanceInfo.LoopStepsPerThread[threadId];\n");
                sourceCode.AppendFront("searchStepsAtLoopStepBegin = actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
            }
            sourceCode.AppendFront("++actionEnv.PerformanceInfo.SearchStepsPerThread[threadId];\n");
        }

        private void EmitProfilingAsNeededPost(SourceBuilder sourceCode)
        {
            if(!EmitProfiling)
                return;

            if(PackagePrefixedActionName != null && EmitFirstLoopProfiling)
            {
                sourceCode.AppendFrontFormat("if(maxMatches==1) actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepSingle.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
                sourceCode.AppendFrontFormat("else actionEnv.PerformanceInfo.ActionProfiles[\"{0}\"].averagesPerThread[threadId].searchStepsPerLoopStepMultiple.Add(actionEnv.PerformanceInfo.SearchStepsPerThread[threadId] - searchStepsAtLoopStepBegin);\n", PackagePrefixedActionName);
            }
        }

        private void EmitIterationParallelizationLockAquire(SourceBuilder sourceCode)
        {
            //sourceCode.AppendFront("Monitor.Enter(this);//lock parallel matching enumeration with action object\n");
            sourceCode.AppendFront("while(Interlocked.CompareExchange(ref iterationLock, 1, 0) != 0) Thread.SpinWait(10);//lock parallel matching enumeration with iteration lock\n");
        }

        private void EmitIterationParallelizationLockRelease(SourceBuilder sourceCode)
        {
            //sourceCode.AppendFront("Monitor.Exit(this);//unlock parallel matching enumeration with action object\n\n");
            sourceCode.AppendFront("Interlocked.Exchange(ref iterationLock, 0);//unlock parallel matching enumeration with iteration lock\n\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public readonly GetCandidateByIterationType Type;
        public readonly bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements
        public readonly bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public readonly string StorageName; // only available if StorageElements
        public readonly string StorageOwnerName; // only available if StorageAttributeElements
        public readonly string StorageOwnerTypeName; // only available if StorageAttributeElements
        public readonly string StorageAttributeName; // only available if StorageAttributeElements
        public readonly string IterationType; // only available if StorageElements|StorageAttributeElements
        public readonly string IndexName; // only available if IndexElements
        public readonly string IndexSetType; // only available if IndexElements
        public readonly IndexAccessType IndexAccessType; // only available if IndexElements
        public readonly string IndexEqual; // only available if IndexElements
        public readonly string IndexFrom; // only available if IndexElements
        public readonly bool IndexFromIncluded; // only available if IndexElements
        public readonly string IndexTo; // only available if IndexElements
        public readonly bool IndexToIncluded; // only available if IndexElements
        public readonly string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public readonly IncidentEdgeType EdgeType; // only available if IncidentEdges
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedActionName;
        public readonly bool EmitFirstLoopProfiling;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing setup of parallelized "get candidate by iteration" operations,
    /// distributing work to worker threads, collecting their results
    /// </summary>
    class GetCandidateByIterationParallelSetup : GetCandidate
    {
        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool enclosingLoop,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            EnclosingLoop = enclosingLoop;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            StorageName = storageName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            IterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string equality,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Equality);
            IndexAccessType = indexAccessType;
            IndexEqual = equality;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string indexName,
            string indexIterationType,
            string indexSetType,
            IndexAccessType indexAccessType,
            string from,
            bool fromIncluded,
            string to,
            bool toIncluded,
            bool isNode,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IndexElements);
            Type = type;
            IndexName = indexName;
            IterationType = indexIterationType;
            IndexSetType = indexSetType;
            Debug.Assert(indexAccessType == IndexAccessType.Ascending || indexAccessType == IndexAccessType.Descending);
            IndexAccessType = indexAccessType;
            IndexFrom = from;
            IndexFromIncluded = fromIncluded;
            IndexTo = to;
            IndexToIncluded = toIncluded;
            IsNode = isNode;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public GetCandidateByIterationParallelSetup(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType,
            string rulePatternClassName,
            string patternName,
            string[] parameterNames,
            bool enclosingLoop,
            bool wasIndependentInlined,
            bool emitProfiling,
            string packagePrefixedActionName,
            bool emitFirstLoopProfiling)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            ParameterNames = parameterNames;
            EnclosingLoop = enclosingLoop;
            WasIndependentInlined = wasIndependentInlined;
            EmitProfiling = emitProfiling;
            PackagePrefixedActionName = packagePrefixedActionName;
            EmitFirstLoopProfiling = emitFirstLoopProfiling;
        }

        public override void Dump(SourceBuilder builder)
        {
            // dump local content
            builder.AppendFront("GetCandidate ByIteration ParallelSetup");
            if(Type == GetCandidateByIterationType.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict ? "Dictionary" : "List/Deque");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                builder.Append("IndexElements ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, IndexName, IsNode);
            }
            else
            { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("iterationNumber = 0;\n");
            sourceCode.AppendFront("numThreadsSignaled = 0;\n");

            if(Type == GetCandidateByIterationType.GraphElements)
            {
                // code comments: lookup comment was already emitted with type iteration/drawing

                // emit initialization of variable containing graph elements list head
                string variableContainingParallelizedListHead =
                    NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                string graphMemberContainingListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                string variableContainingTypeIDForCandidate =
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = graph.{1}[{2}];\n",
                    variableContainingParallelizedListHead, graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                // emit initialization of variable containing candidates
                string variableContainingParallelizedCandidate =
                    NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = {1}.lgspTypeNext;\n",
                    variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelizedCandidate);
 
                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("if({0} == {1})\n",
                    variableContainingCandidate, variableContainingParallelizedListHead);
                sourceCode.AppendFrontIndented("break;\n");
                sourceCode.AppendFrontFormat("{0} = {0}.lgspTypeNext;\n",
                    variableContainingCandidate);
                sourceCode.AppendFront("++numThreadsSignaled;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                string variableContainingStorage =
                    NamesOfEntities.Variable(StorageName);
                sourceCode.AppendFrontFormat("{0} = {1}.GetEnumerator();\n",
                    variableContainingParallelizedIterator, variableContainingStorage);

                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFrontFormat("numThreadsSignaled = Math.Min(numWorkerThreads, {0}.Count);\n",
                    variableContainingStorage);
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict ? "Dictionary" : "List/Deque", StorageOwnerName, StorageAttributeName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                string variableContainingStorage =
                    "((" + StorageOwnerTypeName + ")" + NamesOfEntities.CandidateVariable(StorageOwnerName) + ")." + StorageAttributeName;
                sourceCode.AppendFrontFormat("{0} = {1}.GetEnumerator();\n",
                    variableContainingParallelizedIterator, variableContainingStorage);

                // emit prerun determining the number of threads to wake up
                sourceCode.AppendFrontFormat("numThreadsSignaled = Math.Min(numWorkerThreads, {0}.Count);\n",
                    variableContainingStorage);
                sourceCode.AppendFront("\n");
            }
            else if(Type == GetCandidateByIterationType.IndexElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from index {2}\n",
                        IsNode ? "node" : "edge", PatternElementName, IndexName);
                }

                // initialize variable containing candidates from parallelized next candidate
                string variableContainingParallelizedIterator =
                    NamesOfEntities.IterationParallelizationIterator(PatternElementName);
                sourceCode.AppendFrontFormat("{0} = (({1})graph.Indices).{2}.",
                    variableContainingParallelizedIterator, IndexSetType, IndexName);

                if(IndexAccessType == IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}).GetEnumerator();\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}).GetEnumerator();\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    else if(IndexFrom != null)
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}).GetEnumerator();\n",
                            accessType, indexFromIncluded, IndexFrom);
                    else if(IndexTo != null)
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}).GetEnumerator();\n",
                            accessType, indexToIncluded, IndexTo);
                    else
                        sourceCode.AppendFormat("Lookup{0}().GetEnumerator();\n",
                            accessType);
                }

                // emit prerun determining the number of threads to wake up             
                sourceCode.AppendFrontFormat("foreach({0} indexPreIteration in (({1})graph.Indices).{2}.",
                        IterationType, IndexSetType, IndexName);
                if(IndexAccessType == IndexAccessType.Equality)
                {
                    sourceCode.AppendFormat("Lookup({0}))\n", IndexEqual);
                }
                else
                {
                    String accessType = IndexAccessType == IndexAccessType.Ascending ? "Ascending" : "Descending";
                    String indexFromIncluded = IndexFromIncluded ? "Inclusive" : "Exclusive";
                    String indexToIncluded = IndexToIncluded ? "Inclusive" : "Exclusive";
                    if(IndexFrom != null && IndexTo != null)
                    {
                        sourceCode.AppendFormat("Lookup{0}From{1}To{2}({3}, {4}))\n",
                            accessType, indexFromIncluded, indexToIncluded, IndexFrom, IndexTo);
                    }
                    else if(IndexFrom != null)
                    {
                        sourceCode.AppendFormat("Lookup{0}From{1}({2}))\n",
                            accessType, indexFromIncluded, IndexFrom);
                    }
                    else if(IndexTo != null)
                    {
                        sourceCode.AppendFormat("Lookup{0}To{1}({2}))\n",
                            accessType, indexToIncluded, IndexTo);
                    }
                    else
                    {
                        sourceCode.AppendFormat("Lookup{0}())\n",
                            accessType);
                    }
                }
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("++numThreadsSignaled;\n");
                sourceCode.AppendFront("if(numThreadsSignaled >= numWorkerThreads)\n");
                sourceCode.AppendFrontIndented("break;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("\n");
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if(EdgeType != IncidentEdgeType.IncomingOrOutgoing)
                {
                    // emit initialization of variable containing incident edges list head
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    string memberOfNodeContainingListHead =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInhead" : "lgspOuthead";
                    sourceCode.AppendFrontFormat("{0} = {1}.{2};\n",
                        variableContainingParallelizedListHead, variableContainingStartingPointNode, memberOfNodeContainingListHead);
                    // emit initialization of variable containing incident edges
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} = {1};\n",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                    // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);
                    string memberOfEdgeContainingNextEdge =
                        EdgeType == IncidentEdgeType.Incoming ? "lgspInNext" : "lgspOutNext";

                    // emit prerun determining the number of threads to wake up
                    sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("if({0} == null || ({0} == {1} && numThreadsSignaled>0))\n",
                        variableContainingCandidate, variableContainingParallelizedListHead);
                    sourceCode.AppendFrontIndented("break;\n");
                    sourceCode.AppendFrontFormat("{0} = {0}.{1};\n",
                        variableContainingCandidate, memberOfEdgeContainingNextEdge);
                    sourceCode.AppendFront("++numThreadsSignaled;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                    sourceCode.AppendFront("\n");
                }
                else //EdgeType == IncidentEdgeType.IncomingOrOutgoing
                {
                    // emit initialization of direction run counter, to search first incoming, then outgoing
                    string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
                    // emit initialization of variable containing incident edges list head
                    string variableContainingParallelizedListHead =
                        NamesOfEntities.IterationParallelizationListHead(PatternElementName);
                    string variableContainingStartingPointNode =
                        NamesOfEntities.CandidateVariable(StartingPointNodeName);
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {2}.lgspInhead : {2}.lgspOuthead;\n",
                        variableContainingParallelizedListHead, directionRunCounter, variableContainingStartingPointNode);
                    // emit initialization of variable containing incident edges
                    string variableContainingParallelizedCandidate =
                        NamesOfEntities.IterationParallelizationNextCandidate(PatternElementName);
                    sourceCode.AppendFrontFormat("{0} = {1};\n",
                        variableContainingParallelizedCandidate, variableContainingParallelizedListHead);

                    // emit declaration and initialization of variable containing candidates from next candidate, for pre-run
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0} = {1};\n",
                        variableContainingCandidate, variableContainingParallelizedCandidate);

                    // emit prerun determining the number of threads to wake up
                    sourceCode.AppendFront("for(int i=0; i<numWorkerThreads; ++i)\n");
                    sourceCode.AppendFront("{\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("if({0} == null || ({0} == {1} && numThreadsSignaled>0))\n",
                        variableContainingCandidate, variableContainingParallelizedListHead);
                    sourceCode.AppendFrontIndented("break;\n");
                    sourceCode.AppendFrontFormat("{0} = {1}==0 ? {0}.lgspInNext : {0}.lgspOutNext;\n",
                        variableContainingCandidate, directionRunCounter);
                    sourceCode.AppendFront("++numThreadsSignaled;\n");
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                    sourceCode.AppendFront("\n");
                } //EdgeType 
            } //Type

            // only use parallelized body if there are at least one or two iterations available
            if(EnclosingLoop)
                sourceCode.AppendFront("if(numThreadsSignaled>0)\n"); // if no iteration is available we don't use the parallelized body
            else
                sourceCode.AppendFront("if(numThreadsSignaled>1)\n"); // if one iteration is available we use the normal matcher
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit matcher assignment, worker signaling, and waiting for completion
            //sourceCode.AppendFrontFormat("GRGEN_LIBGR.ConsoleUI.outWriter.WriteLine(\"signaling of parallel matchers for {0}\");\n", RulePatternClassName);
            sourceCode.AppendFront("maxMatchesFound = false;\n");
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.Task = myMatch_parallelized_body;\n");
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.StartWork(numThreadsSignaled);\n");
            //sourceCode.AppendFrontFormat("GRGEN_LIBGR.ConsoleUI.outWriter.WriteLine(\"awaiting of parallel matchers for {0}\");\n", RulePatternClassName);
            sourceCode.AppendFront("GRGEN_LGSP.WorkerPool.WaitForWorkDone();\n");
            
            // emit matches list building from matches lists of the matcher threads (obeying order of sequential iteration)
            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("Dictionary<int, {0}> {1} = null;\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName), NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFrontFormat("if(maxMatches != 1) {0} = new Dictionary<int, {1}>();\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(), RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName));
            }

            sourceCode.AppendFront("int threadOfLastlyChosenMatch = 0;\n");
            sourceCode.AppendFront("while(matches.Count<maxMatches || maxMatches<=0)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("int minIterationValue = Int32.MaxValue;\n");
            sourceCode.AppendFront("int minIterationValueIndex = Int32.MaxValue;\n");
            sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("if(parallelTaskMatches[i].Count == 0)\n");
            sourceCode.AppendFrontIndented("continue;\n");
            sourceCode.AppendFront("if(parallelTaskMatches[i].First.IterationNumber < minIterationValue)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("minIterationValue = parallelTaskMatches[i].First.IterationNumber;\n");
            sourceCode.AppendFront("minIterationValueIndex = i;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("if(minIterationValueIndex<Int32.MaxValue)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(WasIndependentInlined)
            {
                // check whether matches filtering is needed
                sourceCode.AppendFrontFormat("if({0} != null)\n", NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // check whether the hash of the current match is already contained in the overall matches hash map
                sourceCode.AppendFrontFormat("if({0}.ContainsKey(parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}))\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // check whether one of the matches with the same hash in the overall hash map is equal to the current match
                sourceCode.AppendFrontFormat("{0} {1} = {2}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{3}];\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.DuplicateMatchCandidateVariable(),
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("do {\n");
                sourceCode.Indent();

                // check for same elements (recursively, as we're post-matching, just checking local elements is not sufficient anymore)
                // with optimization: same iteration number means the elements were already checked in their matcher thread, no need to do it again
                sourceCode.AppendFront("if(");
                sourceCode.AppendFormat("{0}.IterationNumber != parallelTaskMatches[minIterationValueIndex].FirstImplementation.IterationNumber",
                    NamesOfEntities.DuplicateMatchCandidateVariable());
                sourceCode.Append(" && ");
                sourceCode.AppendFormat("{0}.IsEqual(parallelTaskMatches[minIterationValueIndex].FirstImplementation)",
                    NamesOfEntities.DuplicateMatchCandidateVariable());
                sourceCode.Append(")\n");

                // the current local match is equivalent to one of the already found ones, a duplicate
                // so remove it and continue
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("parallelTaskMatches[minIterationValueIndex].RemoveFirst();\n");
                sourceCode.AppendFrontFormat("goto {0};\n", "continue_after_duplicate_match_removal_" + PatternName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // close "check whether one of the matches with same hash in the overall hash map is equal to the current match"
                // switching to next match with the same hash, if available
                sourceCode.Unindent();
                sourceCode.AppendFront("} ");
                sourceCode.AppendFormat("while(({0} = {0}.nextWithSameHash) != null);\n",
                    NamesOfEntities.DuplicateMatchCandidateVariable());

                // close "check whether hash of current match is already contained in overall matches hash map (if matches filtering is needed)"
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // result when this points is reached: no equal hash is contained in the overall hash map (but a match with same hash may exist)

                // check whether hash is contained in found matches hash map
                sourceCode.AppendFrontFormat("if(!{0}.ContainsKey(parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}))\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // if not, just insert it
                sourceCode.AppendFrontFormat("{0}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{1}] = parallelTaskMatches[minIterationValueIndex].FirstImplementation;\n",
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("else\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // otherwise loop till end of collision list of the hash set, insert there
                // other this code completed, the match will be processed like normal, as if there was no matches filtering
                sourceCode.AppendFrontFormat("{0} {1} = {2}[parallelTaskMatches[minIterationValueIndex].FirstImplementation.{3}];\n",
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName),
                    NamesOfEntities.DuplicateMatchCandidateVariable(),
                    NamesOfEntities.FoundMatchesForFilteringVariable(),
                    NamesOfEntities.DuplicateMatchHashVariable());
                sourceCode.AppendFrontFormat("while({0}.nextWithSameHash != null) {0} = {0}.nextWithSameHash;\n",
                    NamesOfEntities.DuplicateMatchCandidateVariable());

                sourceCode.AppendFrontFormat("{0}.nextWithSameHash = parallelTaskMatches[minIterationValueIndex].FirstImplementation;\n",
                     NamesOfEntities.DuplicateMatchCandidateVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // close "check whether matches filtering is needed"
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternName);
            sourceCode.AppendFrontFormat("{0} match = matches.GetNextUnfilledPosition();\n", matchType);
            sourceCode.AppendFrontFormat("match.AssignContent(({0})parallelTaskMatches[minIterationValueIndex].First);\n", matchType);
            sourceCode.AppendFront("parallelTaskMatches[minIterationValueIndex].RemoveFirst();\n");
            sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
            sourceCode.AppendFront("threadOfLastlyChosenMatch = minIterationValueIndex;\n");
            sourceCode.AppendFront("continue;\n");
            sourceCode.Unindent();

            sourceCode.AppendFront("}\n");
            sourceCode.AppendFront("else\n");
            sourceCode.AppendFrontIndented("break;\n"); // break iteration, minIterationValueIndex == Int32.MaxValue
            sourceCode.Unindent();
            sourceCode.Append("continue_after_duplicate_match_removal_" + PatternName + ": ;\n");
            sourceCode.AppendFront("}\n");

            // emit adjust list heads
            sourceCode.AppendFront("for(int i=0; i<moveHeadAfterNodes[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFrontIndented("graph.MoveHeadAfter(moveHeadAfterNodes[threadOfLastlyChosenMatch][i]);\n");
            sourceCode.AppendFront("for(int i=0; i<moveHeadAfterEdges[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFrontIndented("graph.MoveHeadAfter(moveHeadAfterEdges[threadOfLastlyChosenMatch][i]);\n");
            sourceCode.AppendFront("for(int i=0; i<moveOutHeadAfter[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFrontIndented("moveOutHeadAfter[threadOfLastlyChosenMatch][i].Key.MoveOutHeadAfter(moveOutHeadAfter[threadOfLastlyChosenMatch][i].Value);\n");
            sourceCode.AppendFront("for(int i=0; i<moveInHeadAfter[threadOfLastlyChosenMatch].Count; ++i)\n");
            sourceCode.AppendFrontIndented("moveInHeadAfter[threadOfLastlyChosenMatch][i].Key.MoveInHeadAfter(moveInHeadAfter[threadOfLastlyChosenMatch][i].Value);\n");

            // emit cleaning of variables that must be empty for next run
            sourceCode.AppendFront("for(int i=0; i<numThreadsSignaled; ++i)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("parallelTaskMatches[i].Clear();\n");
            sourceCode.AppendFront("moveHeadAfterNodes[i].Clear();\n");
            sourceCode.AppendFront("moveHeadAfterEdges[i].Clear();\n");
            sourceCode.AppendFront("moveOutHeadAfter[i].Clear();\n");
            sourceCode.AppendFront("moveInHeadAfter[i].Clear();\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            if(WasIndependentInlined)
            {
                sourceCode.AppendFrontFormat("if({0} != null)\n", NamesOfEntities.FoundMatchesForFilteringVariable());
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("foreach({0} toClean in {1}.Values) toClean.CleanNextWithSameHash();\n",
                    matchType, NamesOfEntities.FoundMatchesForFilteringVariable());

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }

            if(EmitProfiling)
                sourceCode.AppendFront("parallelMatcherUsed = true;\n");

            // use normal matcher if there is only one iteration available and no enclosing loop around us
            // this re-executes the head stuff before the first iteration,
            // but that's most of the times cheaper than starting the parallel workers
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
            if(!EnclosingLoop)
            {
                sourceCode.AppendFront("else if(numThreadsSignaled==1)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                sourceCode.AppendFront("myMatch(actionEnv, maxMatches");
                foreach(string parameterName in ParameterNames)
                {
                    sourceCode.Append(", ");
                    sourceCode.Append(parameterName);
                }
                sourceCode.Append(");\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            // just continue matching/with cleaning code if there were zero iterations
        }

        public override bool IsSearchNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return null;
        }

        public readonly GetCandidateByIterationType Type;
        public readonly bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements
        public readonly bool IsDict; // Dictionary(set/map)|List/Deque(array/deque) - only available if StorageElements|StorageAttributeElements
        public readonly string StorageName; // only available if StorageElements
        public readonly string StorageOwnerName; // only available if StorageAttributeElements
        public readonly string StorageOwnerTypeName; // only available if StorageAttributeElements
        public readonly string StorageAttributeName; // only available if StorageAttributeElements
        public readonly string IterationType; // only available if StorageElements|StorageAttributeElements
        public readonly string IndexName; // only available if IndexElements
        public readonly string IndexSetType; // only available if IndexElements
        public readonly IndexAccessType IndexAccessType; // only available if IndexElements
        public readonly string IndexEqual; // only available if IndexElements
        public readonly string IndexFrom; // only available if IndexElements
        public readonly bool IndexFromIncluded; // only available if IndexElements
        public readonly string IndexTo; // only available if IndexElements
        public readonly bool IndexToIncluded; // only available if IndexElements
        public readonly string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public readonly IncidentEdgeType EdgeType; // only available if IncidentEdges
        public readonly string RulePatternClassName;
        public readonly string PatternName;
        public readonly string[] ParameterNames; // the parameters to forward to the normal matcher in case that is to be used because there's only a single iteration
        public readonly bool EnclosingLoop; // in case of an enclosing loop we can't forward to the normal matcher
        public readonly bool WasIndependentInlined;
        public readonly bool EmitProfiling;
        public readonly string PackagePrefixedActionName;
        public readonly bool EmitFirstLoopProfiling;
    }

    /// <summary>
    /// Available types of GetCandidateByDrawing operations
    /// </summary>
    enum GetCandidateByDrawingType
    {
        NodeFromEdge, // draw node from given edge
        MapWithStorage, // map element by storage map lookup
        MapByName, // map element by name map lookup
        MapByUnique, // map element by unique map lookup
        FromInputs, // draw element from action inputs
        FromSubpatternConnections, // draw element from subpattern connections
        FromParallelizationTask, // draw element from parallelization preset
        FromParallelizationTaskVar, // draw variable from parallelization preset
        FromOtherElementForCast, // draw element from other element for type cast
        FromOtherElementForAssign, // draw element from other element for assignment
    }

    /// <summary>
    /// The different possibilites of drawing an implicit node from an edge
    /// if directed edge: source, target
    /// if arbitrary directed, undirected, arbitrary: source-or-target for first node, the-other for second node
    /// </summary>
    enum ImplicitNodeType
    {
        Source,
        Target,
        SourceOrTarget,
        TheOther
    }

    /// <summary>
    /// Class representing "get node (or edge) by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class GetCandidateByDrawing : GetCandidate
    {
        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string patternElementTypeName,
            string startingPointEdgeName,
            ImplicitNodeType nodeType)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType != ImplicitNodeType.TheOther);

            Type = type;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            NodeType = nodeType;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string patternElementTypeName,
            string startingPointEdgeName,
            string theOtherPatternElementName,
            ImplicitNodeType nodeType)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType == ImplicitNodeType.TheOther);

            Type = type;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            TheOtherPatternElementName = theOtherPatternElementName;
            NodeType = nodeType;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromInputs 
                || type == GetCandidateByDrawingType.FromSubpatternConnections
                || type == GetCandidateByDrawingType.FromParallelizationTask);
            
            Type = type;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string typeName)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromParallelizationTaskVar);

            Type = type;
            TypeName = typeName;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string sourcePatternElementName,
            string storageName,
            string storageValueTypeName,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.MapWithStorage);

            Type = type;
            SourcePatternElementName = sourcePatternElementName;
            StorageName = storageName;
            StorageValueTypeName = storageValueTypeName;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string source,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromOtherElementForCast 
                || type == GetCandidateByDrawingType.FromOtherElementForAssign
                || type == GetCandidateByDrawingType.MapByName
                || type == GetCandidateByDrawingType.MapByUnique);

            Type = type;
            if(type == GetCandidateByDrawingType.MapByName || type == GetCandidateByDrawingType.MapByUnique)
                SourceExpression = source;
            else
                SourcePatternElementName = source;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetCandidate ByDrawing ");
            if(Type==GetCandidateByDrawingType.NodeFromEdge)
            {
                builder.Append("NodeFromEdge ");
                builder.AppendFormat("on {0} of {1} from {2} implicit node type:{3}\n",
                    PatternElementName, PatternElementTypeName, 
                    StartingPointEdgeName, NodeType.ToString());
            }
            else if(Type==GetCandidateByDrawingType.MapWithStorage)
            {
                builder.Append("MapWithStorage ");
                builder.AppendFormat("on {0} by {1} from {2} node:{3}\n",
                    PatternElementName, SourcePatternElementName, 
                    StorageName, IsNode);
            }
            else if(Type==GetCandidateByDrawingType.MapByName)
            {
                builder.Append("MapByName ");
                builder.AppendFormat("on {0} from name map by {1} node:{2}\n",
                    PatternElementName, SourceExpression, IsNode);
            }
            else if(Type==GetCandidateByDrawingType.MapByUnique)
            {
                builder.Append("MapByUnique ");
                builder.AppendFormat("on {0} from unique map by {1} node:{2}\n",
                    PatternElementName, SourceExpression, IsNode);
            }
            else if(Type==GetCandidateByDrawingType.FromInputs)
            {
                builder.Append("FromInputs ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type==GetCandidateByDrawingType.FromSubpatternConnections)
            {
                builder.Append("FromSubpatternConnections ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTask)
            {
                builder.Append("FromParallelizationTask ");
                builder.AppendFormat("on {0} node:{1} \n",
                    PatternElementName, IsNode);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTaskVar)
            {
                builder.Append("FromParallelizationTaskVar ");
                builder.AppendFormat("on {0} \n",
                    PatternElementName);
            }
            else if(Type == GetCandidateByDrawingType.FromOtherElementForCast)
            {
                builder.Append("FromOtherElementForCast ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, SourcePatternElementName, IsNode);
            }
            else //if(Type==GetCandidateByDrawingType.FromOtherElementForAssign)
            {
                builder.Append("FromOtherElementForAssign ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, SourcePatternElementName, IsNode);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type==GetCandidateByDrawingType.NodeFromEdge)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Implicit {0} {1} from {2} \n",
                            NodeType.ToString(), PatternElementName, StartingPointEdgeName);
                }

                if(NodeType == ImplicitNodeType.Source || NodeType == ImplicitNodeType.Target)
                {
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with demanded node from variable containing edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    string whichImplicitNode =
                        NodeType==ImplicitNodeType.Source ? "lgspSource" : "lgspTarget";
                    sourceCode.AppendFormat(" = {0}.{1};\n",
                        variableContainingStartingPointEdge, whichImplicitNode);
                }
                else if(NodeType == ImplicitNodeType.SourceOrTarget)
                {
                    // we've to look at both nodes, we do so by first handling source, then target
                    string directionRunCounter = 
                        NamesOfEntities.DirectionRunCounterVariable(StartingPointEdgeName);
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate = 
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with demanded node from variable containing edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    sourceCode.AppendFormat(" = {0}==0 ? {1}.lgspSource : {1}.lgspTarget;\n",
                        directionRunCounter, variableContainingStartingPointEdge);
                }
                else // NodeType == ImplicitNodeType.TheOther
                {
                    // emit declaration of variable containing candidate node
                    string variableContainingCandidate =
                        NamesOfEntities.CandidateVariable(PatternElementName);
                    sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPNode {0}", variableContainingCandidate);
                    // emit initialization with other node from edge
                    string variableContainingStartingPointEdge =
                        NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                    sourceCode.AppendFormat(" = {0}=={1}.lgspSource ? {1}.lgspTarget : {1}.lgspSource;\n",
                        NamesOfEntities.CandidateVariable(TheOtherPatternElementName),
                        variableContainingStartingPointEdge);
                }
            }
            else if(Type == GetCandidateByDrawingType.MapWithStorage)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Map {0} by {1}[{2}] \n",
                       PatternElementName, StorageName, SourcePatternElementName);
                }

                // emit declaration of variable to hold element mapped from storage
                string typeOfTempVariableForMapResult = StorageValueTypeName;
                string tempVariableForMapResult = NamesOfEntities.MapWithStorageTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.MapByName)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Map {0} by name map with {1} \n",
                       PatternElementName, SourceExpression);
                }

                // emit declaration of variable to hold element mapped by name
                // emit initialization with element from name map lookup
                string typeOfTempVariableForMapResult = "GRGEN_LIBGR.IGraphElement";
                string tempVariableForMapResult = NamesOfEntities.MapByNameTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ((GRGEN_LGSP.LGSPNamedGraph)graph).GetGraphElement(({2}).ToString());\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult, SourceExpression);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.MapByUnique)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Map {0} by unique index with {1} \n",
                       PatternElementName, SourceExpression);
                }

                // emit declaration of variable to hold element mapped by unique id
                // emit initialization with element from unique map lookup
                string typeOfTempVariableForMapResult = "GRGEN_LIBGR.IGraphElement";
                string tempVariableForMapResult = NamesOfEntities.MapByUniqueTemporary(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = graph.GetGraphElement((int)({2}));\n",
                    typeOfTempVariableForMapResult, tempVariableForMapResult, SourceExpression);
                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
            }
            else if(Type == GetCandidateByDrawingType.FromInputs)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Preset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with element from input parameters array
                sourceCode.AppendFormat(" = ({0}){1};\n",
                    typeOfVariableContainingCandidate, PatternElementName);
            }
            else if(Type==GetCandidateByDrawingType.FromSubpatternConnections)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// SubPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                // and initialization with element from subpattern connections
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, PatternElementName);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTask)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// ParallelizationPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node or edge
                // and initialization with element from parallelization task
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, variableContainingParallelPreset);
            }
            else if(Type == GetCandidateByDrawingType.FromParallelizationTaskVar)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// ParallelizationPresetVar {0} \n", PatternElementName);

                // emit declaration of variable containing parameter forwarded
                // and initialization with element from parallelization task
                string variableContainingVariable = NamesOfEntities.Variable(PatternElementName);
                string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    TypeName, variableContainingVariable, variableContainingParallelPreset);
            }
            else if(Type == GetCandidateByDrawingType.FromOtherElementForCast)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Element {0} as type cast from other element {1} \n",
                       PatternElementName, SourcePatternElementName);
                }

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with other element candidate variable
                string variableContainingSource = NamesOfEntities.CandidateVariable(SourcePatternElementName);
                sourceCode.AppendFormat(" = {0};\n", variableContainingSource);
            }
            else //if(Type == GetCandidateByDrawingType.FromOtherElementForAssign)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Element {0} assigned from other element {1} \n",
                       PatternElementName, SourcePatternElementName);
                }

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with other element candidate variable
                string variableContainingSource = NamesOfEntities.CandidateVariable(SourcePatternElementName);
                sourceCode.AppendFormat(" = {0};\n", variableContainingSource);
            }
        }

        public readonly GetCandidateByDrawingType Type;
        public readonly string PatternElementTypeName; // only valid if NodeFromEdge
        public readonly string TheOtherPatternElementName; // only valid if NodeFromEdge and TheOther
        public readonly string StartingPointEdgeName; // from pattern - only valid if NodeFromEdge
        readonly ImplicitNodeType NodeType; // only valid if NodeFromEdge
        public readonly bool IsNode; // node|edge
        public readonly string SourcePatternElementName; // only valid if MapWithStorage|FromOtherElementForCast|FromOtherElementForAssign
        public readonly string SourceExpression; // only valid if MapByName
        public readonly string StorageName; // only valid if MapWithStorage
        public readonly string StorageValueTypeName; // only valid if MapWithStorage
        public readonly string TypeName; // only valid if FromParallelizationTaskVar
    }

    /// <summary>
    /// Class representing "get node or edge by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class WriteParallelPreset : GetCandidate
    {
        public WriteParallelPreset(
            string patternElementName,
            bool isNode)
        : base(patternElementName)
        {
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("WriteParallelPreset ");
            builder.AppendFormat("on {0} node:{1} \n",
                PatternElementName, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// WriteParallelizationPreset {0} \n", PatternElementName);

            // initialize parallelization task with variable containing candidate node
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
            sourceCode.AppendFrontFormat("{0} = {1};\n",
                variableContainingParallelPreset, variableContainingCandidate);
        }

        public readonly bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing "get variable by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class WriteParallelPresetVar : GetCandidate
    {
        public WriteParallelPresetVar(
            string varName,
            string varType)
        : base(varName)
        {
            VarType = varType;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("WriteParallelPreset ");
            builder.AppendFormat("on {0} of type:{1}\n",
                PatternElementName, VarType);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// WriteParallelizationPresetVar {0} \n", PatternElementName);

            string variableContainingParallelPreset = NamesOfEntities.IterationParallelizationParallelPresetCandidate(PatternElementName);
            string variableContainingVariable = NamesOfEntities.Variable(PatternElementName);
            sourceCode.AppendFrontFormat("{0} = {1};\n",
                variableContainingParallelPreset, variableContainingVariable);
        }

        public readonly string VarType;
    }

    /// <summary>
    /// Class representing operation iterating both directions of an edge of unfixed direction 
    /// </summary>
    class BothDirectionsIteration : SearchProgramOperation
    {
        public BothDirectionsIteration(string patternElementName)
        {
            PatternElementName = patternElementName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BothDirectionsIteration on {0}\n", PatternElementName);

            // then nested content
            if(NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// both directions of {0}\n", PatternElementName);

            // emit loop header of loop iterating both directions of an edge of not fixed direction
            string directionRunCounter = NamesOfEntities.DirectionRunCounterVariable(PatternElementName);
            sourceCode.AppendFrontFormat("for(");
            sourceCode.AppendFormat("int {0} = 0; ", directionRunCounter);
            sourceCode.AppendFormat("{0} < 2; ", directionRunCounter);
            sourceCode.AppendFormat("++{0}", directionRunCounter);
            sourceCode.Append(")\n");

            // open loop
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit loop body
            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public readonly string PatternElementName;

        public SearchProgramList NestedOperationsList;
    }
}
