/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for search program candidate filtering operations
    /// </summary>
    abstract class CheckCandidate : CheckOperation
    {
        protected CheckCandidate(string patternElementName)
        {
            PatternElementName = patternElementName;
        }

        public string PatternElementName;
    }

    /// <summary>
    /// Available types of CheckCandidateForType operations
    /// </summary>
    enum CheckCandidateForTypeType
    {
        ByIsAllowedType, //check by inspecting the IsAllowedType array of the rule pattern
        ByIsMyType,      // check by inspecting the IsMyType array of the graph element's type model
        ByTypeID         // check by comparing against a given type ID
    }

    /// <summary>
    /// Class representing "check whether candidate is of allowed type" operation
    /// </summary>
    class CheckCandidateForType : CheckCandidate
    {
        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByIsMyType);
            Type = type;
            TypeName = rulePatternTypeNameOrTypeName;
            IsNode = isNode;
        }

        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            string isAllowedArrayName,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByIsAllowedType);
            Type = type;
            RulePatternTypeName = rulePatternTypeNameOrTypeName;
            IsAllowedArrayName = isAllowedArrayName;
            IsNode = isNode;
        }

        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string[] typeIDs,
            bool isNode)
        : base(patternElementName)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByTypeID);
            Type = type;
            TypeIDs = (string[])typeIDs.Clone();
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForType ");
            if(Type == CheckCandidateForTypeType.ByIsAllowedType)
            {
                builder.Append("ByIsAllowedType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            }
            else if(Type == CheckCandidateForTypeType.ByIsMyType)
            {
                builder.Append("ByIsMyType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, TypeName, IsNode);
            }
            else // Type == CheckCandidateForTypeType.ByTypeID
            {
                builder.Append("ByTypeID ");
                builder.AppendFormat("on {0} ids:{1} node:{2}\n",
                    PatternElementName, string.Join(",", TypeIDs), IsNode);
            }
            
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if(Type == CheckCandidateForTypeType.ByIsAllowedType)
            {
                string isAllowedTypeArrayMemberOfRulePattern =
                    IsAllowedArrayName + "_IsAllowedType";
                sourceCode.AppendFrontFormat("if(!{0}.{1}[{2}.lgspType.TypeID]) ",
                    RulePatternTypeName, isAllowedTypeArrayMemberOfRulePattern,
                    variableContainingCandidate);
            }
            else if(Type == CheckCandidateForTypeType.ByIsMyType)
            {
                sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.lgspType.TypeID]) ",
                    TypeName, variableContainingCandidate);
            }
            else // Type == CheckCandidateForTypeType.ByTypeID)
            {
                sourceCode.AppendFront("if(");
                bool first = true;
                foreach(string typeID in TypeIDs)
                {
                    if(first)
                        first = false;
                    else
                        sourceCode.Append(" && ");

                    sourceCode.AppendFormat("{0}.lgspType.TypeID!={1}",
                        variableContainingCandidate, typeID);
                }
                sourceCode.Append(") ");
            }
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly CheckCandidateForTypeType Type;

        public readonly string RulePatternTypeName; // only valid if ByIsAllowedType
        public readonly string IsAllowedArrayName; // only valid if ByIsAllowedType
        public readonly string TypeName; // only valid if ByIsMyType
        public readonly string[] TypeIDs; // only valid if ByTypeID

        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing "check whether candidate is identical to another element" operation
    /// </summary>
    class CheckCandidateForIdentity : CheckCandidate
    {
        public CheckCandidateForIdentity(
            string patternElementName,
            string otherPatternElementName)
        : base(patternElementName)
        {
            OtherPatternElementName = otherPatternElementName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.Append("CheckCandidate ForIdentity ");
            builder.AppendFormat("by {0} == {1}\n", PatternElementName, OtherPatternElementName);
            
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingOtherElement = NamesOfEntities.CandidateVariable(OtherPatternElementName);
            sourceCode.AppendFrontFormat("if({0}!={1}) ", 
                variableContainingCandidate, variableContainingOtherElement);
            
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string OtherPatternElementName;
    }

    /// <summary>
    /// Class representing some check candidate operation,
    /// which was determined at generation time to always fail 
    /// </summary>
    class CheckCandidateFailed : CheckCandidate
    {
        public CheckCandidateFailed()
            : base(null)
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate Failed \n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check failed code
            CheckFailedOperations.Emit(sourceCode);
        }
    }

    /// <summary>
    /// The different positions of some edge to check the candidate node against
    /// if directed edge: source, target
    /// if arbitrary directed, undirected, arbitrary edge: source-or-target for first node, the-other for second node
    /// </summary>
    enum CheckCandidateForConnectednessType
    {
        Source,
        Target,
        SourceOrTarget,
        TheOther
    }

    /// <summary>
    /// Class representing "check whether candidate is connected to the elements
    ///   it should be connected to, according to the pattern" operation
    /// </summary>
    class CheckCandidateForConnectedness : CheckCandidate
    {
        public CheckCandidateForConnectedness(
            string patternElementName, 
            string patternNodeName,
            string patternEdgeName,
            CheckCandidateForConnectednessType connectednessType)
        : base(patternElementName) // pattern element is the candidate to check, either node or edge
        {
            Debug.Assert(connectednessType != CheckCandidateForConnectednessType.TheOther);

            PatternNodeName = patternNodeName;
            PatternEdgeName = patternEdgeName;
            ConnectednessType = connectednessType;
        }

        public CheckCandidateForConnectedness(
            string patternElementName,
            string patternNodeName,
            string patternEdgeName,
            string theOtherPatternNodeName,
            CheckCandidateForConnectednessType connectednessType)
        : base(patternElementName) // pattern element is the candidate to check, either node or edge
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.TheOther);

            PatternNodeName = patternNodeName;
            PatternEdgeName = patternEdgeName;
            TheOtherPatternNodeName = theOtherPatternNodeName;
            ConnectednessType = connectednessType;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForConnectedness ");
            builder.AppendFormat("{0}=={1}.{2}\n",
                PatternNodeName, PatternEdgeName, ConnectednessType.ToString());
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(ConnectednessType == CheckCandidateForConnectednessType.Source)
            {
                // emit check decision for is candidate connected to already found partial match, i.e. edge source equals node
                sourceCode.AppendFrontFormat("if({0}.lgspSource != {1}) ",
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else if(ConnectednessType == CheckCandidateForConnectednessType.Target)
            {
                // emit check decision for is candidate connected to already found partial match, i.e. edge target equals node
                sourceCode.AppendFrontFormat("if({0}.lgspTarget != {1}) ",
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else if(ConnectednessType == CheckCandidateForConnectednessType.SourceOrTarget)
            {
                // we've to check both node positions of the edge, we do so by checking source or target dependent on the direction run
                sourceCode.AppendFrontFormat("if( ({0}==0 ? {1}.lgspSource : {1}.lgspTarget) != {2}) ",
                    NamesOfEntities.DirectionRunCounterVariable(PatternEdgeName), 
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else //ConnectednessType == CheckCandidateForConnectednessType.TheOther
            {
                // we've to check the node position of the edge the first node is not assigned to
                sourceCode.AppendFrontFormat("if( ({0}=={1}.lgspSource ? {1}.lgspTarget : {1}.lgspSource) != {2}) ",
                    NamesOfEntities.CandidateVariable(TheOtherPatternNodeName),
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly string PatternNodeName;
        public readonly string PatternEdgeName;
        public readonly string TheOtherPatternNodeName; // only valid if ConnectednessType==TheOther
        public readonly CheckCandidateForConnectednessType ConnectednessType;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other local pattern element within this isomorphy space, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written/removed by AcceptCandidate/AbandonCandidate
    /// </summary>
    class CheckCandidateForIsomorphy : CheckCandidate
    {
        public CheckCandidateForIsomorphy(
            string patternElementName,
            List<string> namesOfPatternElementsToCheckAgainst,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel,
            bool lockForAllThreads)
        : base(patternElementName)
        {
            NamesOfPatternElementsToCheckAgainst = namesOfPatternElementsToCheckAgainst;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
            LockForAllThreads = lockForAllThreads;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphy ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2} ",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
            if(NamesOfPatternElementsToCheckAgainst != null)
            {
                builder.Append("against ");
                foreach(string name in NamesOfPatternElementsToCheckAgainst)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            builder.AppendFormat("parallel:{0} first for all:{1} ",
                Parallel, LockForAllThreads);
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (to another pattern element)
            // as this would cause a homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE ? ");

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED << isoSpace";
                if(LockForAllThreads)
                {
                    sourceCode.AppendFormat("( flagsPerElement0[{0}.uniqueId] & {1} ) != 0",
                        variableContainingCandidate, isMatchedBit);
                }
                else
                {
                    sourceCode.AppendFormat("( flagsPerElement[{0}.uniqueId] & {1} ) != 0",
                        variableContainingCandidate, isMatchedBit);
                }

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.perThreadInIsoSpaceMatchedElements[{0}][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                        + ".ContainsKey({1}))", 
                        LockForAllThreads ? "0" : "threadId",
                        variableContainingCandidate);
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE ? ");

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace";
                sourceCode.AppendFormat("({0}.lgspFlags & {1}) != 0", variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.inIsoSpaceMatchedElements[isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }

            // but only if isomorphy is demanded (NamesOfPatternElementsToCheckAgainst empty)
            // otherwise homomorphy to certain elements is allowed, 
            // so we only fail if the graph element is matched to one of the not allowed elements,
            // given in NamesOfPatternElementsToCheckAgainst 
            if(NamesOfPatternElementsToCheckAgainst != null)
            {
                Debug.Assert(NamesOfPatternElementsToCheckAgainst.Count > 0);

                sourceCode.Append("\n");
                sourceCode.Indent();

                if(NamesOfPatternElementsToCheckAgainst.Count == 1)
                {
                    string name = NamesOfPatternElementsToCheckAgainst[0];
                    sourceCode.AppendFrontFormat("&& {0}=={1}\n", variableContainingCandidate,
                        NamesOfEntities.CandidateVariable(name));
                }
                else
                {
                    bool first = true;
                    foreach(string name in NamesOfPatternElementsToCheckAgainst)
                    {
                        if(first)
                        {
                            sourceCode.AppendFrontFormat("&& ({0}=={1}\n", variableContainingCandidate,
                                NamesOfEntities.CandidateVariable(name));
                            sourceCode.Indent();
                            first = false;
                        }
                        else
                        {
                            sourceCode.AppendFrontFormat("|| {0}=={1}\n", variableContainingCandidate,
                               NamesOfEntities.CandidateVariable(name));
                        }
                    }
                    sourceCode.AppendFront(")\n");
                    sourceCode.Unindent();
                }

                // close decision
                sourceCode.AppendFront(")\n");
                sourceCode.Unindent();
            }
            else
            {
                // close decision
                sourceCode.Append(")\n");
            }

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly List<string> NamesOfPatternElementsToCheckAgainst;
        public readonly string NegativeIndependentNamePrefix; // "" if top-level
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
        public readonly bool LockForAllThreads;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other non-local pattern element within this isomorphy space, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written by AcceptCandidateGlobal/AbandonCandidateGlobal
    /// </summary>
    class CheckCandidateForIsomorphyGlobal : CheckCandidate
    {
        public CheckCandidateForIsomorphyGlobal(
            string patternElementName,
            List<string> globallyHomomorphElements,
            bool isNode,
            bool neverAboveMaxIsoSpace,
            bool parallel)
        : base(patternElementName)
        {
            GloballyHomomorphElements = globallyHomomorphElements;
            IsNode = isNode;
            NeverAboveMaxIsoSpace = neverAboveMaxIsoSpace;
            Parallel = parallel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyGlobal ");
            builder.AppendFormat("on {0} node:{1} ",
                PatternElementName, IsNode);
            if(GloballyHomomorphElements != null)
            {
                builder.Append("but accept if ");
                foreach(string name in GloballyHomomorphElements)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (in another subpattern to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            if(Parallel)
            {
                if(!NeverAboveMaxIsoSpace)
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE ? ");

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlagsParallel.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFormat("( flagsPerElementGlobal[{0}.uniqueId] & {1} ) == {1}",
                    variableContainingCandidate,
                    isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.perThreadInIsoSpaceMatchedElementsGlobal[threadId][isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlagsParallel.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }
            else
            {
                if(!NeverAboveMaxIsoSpace)
                    sourceCode.Append("(isoSpace < (int) GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE ? ");

                string isMatchedBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << isoSpace";
                sourceCode.AppendFormat("({0}.lgspFlags & {1})=={1}",
                    variableContainingCandidate, isMatchedBit);

                if(!NeverAboveMaxIsoSpace)
                {
                    sourceCode.Append(" : ");
                    sourceCode.AppendFormat("graph.inIsoSpaceMatchedElementsGlobal[isoSpace - (int)"
                        + "GRGEN_LGSP.LGSPElemFlags.MAX_ISO_SPACE]"
                        + ".ContainsKey({0}))", variableContainingCandidate);
                }
            }

            if(GloballyHomomorphElements != null)
            {
                // don't fail if candidate was globally matched by an element
                // it is allowed to be globally homomorph to 
                // (element from alternative case declared to be non-isomorph to element from enclosing pattern)
                foreach(string name in GloballyHomomorphElements)
                {
                    sourceCode.AppendFormat(" && {0}!={1}",
                        variableContainingCandidate, NamesOfEntities.CandidateVariable(name));
                }
            }
            sourceCode.Append(")\n");

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly List<string> GloballyHomomorphElements;
        public readonly bool IsNode; // node|edge
        public readonly bool NeverAboveMaxIsoSpace;
        public readonly bool Parallel;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other pattern element on the pattern derivation path, to ensure required isomorphy" operation
    /// </summary>
    class CheckCandidateForIsomorphyPatternPath : CheckCandidate
    {
        public CheckCandidateForIsomorphyPatternPath(
            string patternElementName,
            bool isNode,
            bool always,
            string lastMatchAtPreviousNestingLevel)
        : base(patternElementName)
        {
            IsNode = isNode;
            Always = always;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyPatternPath ");
            builder.AppendFormat("on {0} node:{1} last match at previous nesting level in:{2}",
                PatternElementName, IsNode, LastMatchAtPreviousNestingLevel);
            builder.Append("\n");
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (previously on the pattern derivation path to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBySomeBit = "(uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";

            if(!Always)
                sourceCode.Append("searchPatternpath && ");

            sourceCode.AppendFormat("({0}.lgspFlags & {1})=={1} && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched({0}, {2})",
                variableContainingCandidate, isMatchedBySomeBit, LastMatchAtPreviousNestingLevel);

            sourceCode.Append(")\n");

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public readonly bool IsNode; // node|edge
        public readonly bool Always; // have a look at searchPatternpath or search always
        readonly string LastMatchAtPreviousNestingLevel;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the storage map" operation
    /// </summary>
    class CheckCandidateMapWithStorage : CheckCandidate
    {
        public CheckCandidateMapWithStorage(
            string patternElementName,
            string sourcePatternElementName,
            string storageName,
            string storageKeyTypeName,
            bool isNode)
        : base(patternElementName)
        {
            SourcePatternElementName = sourcePatternElementName;
            StorageName = storageName;
            StorageKeyTypeName = storageKeyTypeName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapWithStorage ");
            builder.AppendFormat("on {0} by {1} from {2} node:{3}\n",
                PatternElementName, SourcePatternElementName, 
                StorageName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from storage
            string variableContainingStorage = NamesOfEntities.Variable(StorageName);
            string variableContainingSourceElement = NamesOfEntities.CandidateVariable(SourcePatternElementName);
            string tempVariableForMapResult = NamesOfEntities.MapWithStorageTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if(!{0}.TryGetValue(({1}){2}, out {3})) ",
                variableContainingStorage, StorageKeyTypeName, 
                variableContainingSourceElement, tempVariableForMapResult);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                
            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForMapResult);
        }

        public readonly string SourcePatternElementName;
        public readonly string StorageName;
        public readonly string StorageKeyTypeName;
        readonly bool IsNode;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the name map" operation
    /// </summary>
    class CheckCandidateMapByName : CheckCandidate
    {
        public CheckCandidateMapByName(
            string patternElementName,
            bool isNode)
        : base(patternElementName)
        {
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapByName ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from name map
            string tempVariableForMapResult = NamesOfEntities.MapByNameTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if({0}==null || !({0} is {1}))", 
                tempVariableForMapResult, IsNode ? "GRGEN_LIBGR.INode" : "GRGEN_LIBGR.IEdge");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForMapResult);
        }

        readonly bool IsNode;
    }

    /// <summary>
    /// Class representing "check whether candidate is contained in the unique index" operation
    /// </summary>
    class CheckCandidateMapByUnique : CheckCandidate
    {
        public CheckCandidateMapByUnique(
            string patternElementName,
            bool isNode)
        : base(patternElementName)
        {
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate MapByUnique ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
            // then operations for case check failed
            if(CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit initialization with element mapped from unique index
            string tempVariableForUniqueResult = NamesOfEntities.MapByUniqueTemporary(PatternElementName);
            sourceCode.AppendFrontFormat("if({0}==null || !({0} is {1}))",
                tempVariableForUniqueResult, IsNode ? "GRGEN_LIBGR.INode" : "GRGEN_LIBGR.IEdge");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // assign the value to the candidate variable, cast it to the variable type
            string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("{0} = ({1}){2};\n",
                variableContainingCandidate, typeOfVariableContainingCandidate, tempVariableForUniqueResult);
        }

        readonly bool IsNode;
    }
}
