/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit, based on engine-net by Moritz Kroll

using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// class determining names of entities in generated source code from pattern element entities
    /// </summary>
    static class NamesOfEntities
    {
        /// <summary>
        /// Returns name of the candidate variable which will be created within the search program
        /// holding over time the candidates for the given pattern element
        /// </summary>
        public static string CandidateVariable(string patternElementName)
        {
            return "candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the type variable which will be created within the search program
        /// holding the type object which will be used for determining the candidates
        /// for the given pattern element
        /// </summary>
        public static string TypeForCandidateVariable(string patternElementName)
        {
            return "type_candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of non-graph-element variable
        /// </summary>
        public static string Variable(string variableName)
        {
            return "var_" + variableName;
        }

        /// <summary>
        /// Returns name of the type of the type variable
        /// </summary>
        public static string TypeOfVariableContainingType(bool isNode)
        {
            return "GRGEN_LIBGR." + (isNode ? "Node" : "Edge") + "Type";
        }

        /// <summary>
        /// Returns name of the type id variable which will be created within the search program
        /// holding the type id which will be used for determining the candidates
        /// for the given pattern element   (determined out of type object in iteration)
        /// </summary>
        public static string TypeIdForCandidateVariable(string patternElementName)
        {
            return "type_id_candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the list head variable which will be created within the search program
        /// holding the list head of the list accessed by type id with the graph elements of that type
        /// for finding out when iteration of the candidates for the given pattern element has finished
        /// </summary>
        public static string CandidateIterationListHead(string patternElementName)
        {
            return "head_candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the container entry variable which will be created within the search program
        /// holding the dictionary entry (key-value-pair) or list/deque entry of the storage to pick an element from
        /// </summary>
        public static string CandidateIterationContainerEntry(string patternElementName)
        {
            return "storage_candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the temporary variable which will be created within the search program
        /// for retrieving the element via TryGet from the storage map; must be casted to the needed type afterwards
        /// </summary>
        public static string MapWithStorageTemporary(string patternElementName)
        {
            return "map_with_storage_temporary_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the temporary variable which will be created within the search program
        /// for retrieving the element from the name map of the named graph; must be casted to the needed type afterwards
        /// </summary>
        public static string MapByNameTemporary(string patternElementName)
        {
            return "map_by_name_temporary_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the temporary variable which will be created within the search program
        /// for retrieving the element from the unique index of the graph; must be casted to the needed type afterwards
        /// </summary>
        public static string MapByUniqueTemporary(string patternElementName)
        {
            return "map_by_unique_temporary_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the entry variable which will be created within the search program
        /// holding the entry of the index to pick an element from
        /// </summary>
        public static string CandidateIterationIndexEntry(string patternElementName)
        {
            return "index_candidate_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the method called when a maybe preset element is not set
        /// </summary>
        public static string MissingPresetHandlingMethod(string patternElementName)
        {
            return "MissingPreset_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the variable which will be created within the search program
        /// backing up the value of the isMatched-Bit of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfIsMatchedBit(string patternElementName, string negativeIndependentNamePrefix)
        {
            return "prev_" + negativeIndependentNamePrefix + "_" + CandidateVariable(patternElementName);
        }

        /// <summary>
        /// Returns name of the variable which will be created within the search program
        /// backing up the value of the global isMatched-Bit of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfIsMatchedGlobalBit(string patternElementName, string negativeIndependentNamePrefix)
        {
            return "prevGlobal_" + negativeIndependentNamePrefix + "_" + CandidateVariable(patternElementName);
        }

        /// <summary>
        /// Returns name of the variable which will be created within the search program
        /// backing up the value of the some global isMatched-Bit of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfIsMatchedGlobalInSomePatternBit(string patternElementName, string negativeIndependentNamePrefix)
        {
            return "prevSomeGlobal_" + negativeIndependentNamePrefix + "_" + CandidateVariable(patternElementName);
        }

        /// <summary>
        /// Returns name of the task variable which will be created within the search program
        /// holding the task object whose connections need to be filled before being pushed on the open tasks stack
        /// </summary>
        public static string TaskVariable(string subpatternElementName, string negativeIndependentNamePrefix)
        {
            return "taskFor_" + negativeIndependentNamePrefix + subpatternElementName;
        }

        /// <summary>
        /// Returns name of the type of the task variable
        /// </summary>
        public static string TypeOfTaskVariable(string subpatternName, bool isAlternative, bool isIterated)
        {
            Debug.Assert(!(isAlternative && isIterated));
            if(isAlternative)
                return "AlternativeAction_" + subpatternName;
            if(isIterated)
                return "IteratedAction_" + subpatternName;
            return
                "PatternAction_" + subpatternName;
        }

        /// <summary>
        /// Returns name of the rule pattern class
        /// </summary>
        public static string RulePatternClassName(string rulePatternName, string packageName, bool isSubpattern)
        {
            return (packageName != null ? packageName + "." : "") + (isSubpattern ? "Pattern_" : "Rule_") + rulePatternName;
        }

        /// <summary>
        /// Returns name of the match class
        /// </summary>
        public static string MatchClassName(string matchClassName, string packageName)
        {
            return (packageName != null ? packageName + "." : "") + "MatchClassInfo_" + matchClassName;
        }

        /// <summary>
        /// Returns name of the comparer class
        /// </summary>
        public static string ArrayHelperClassName(string graphElementTypeName, string packageName, string entityName)
        {
            return (packageName != null ? packageName + "." : "") + "ArrayHelper_" + graphElementTypeName + "_" + entityName;
        }

        /// <summary>
        /// Returns name of the exact index set type
        /// </summary>
        public static string IndexSetType(string modelName)
        {
            return "GRGEN_MODEL." + modelName + "IndexSet";
        }

        /// <summary>
        /// Returns name of the match class
        /// </summary>
        public static string MatchClassName(string patternGraphName)
        {
            return "Match_" + patternGraphName;
        }

        /// <summary>
        /// Returns name of the match interface
        /// </summary>
        public static string MatchInterfaceName(string patternGraphName)
        {
            return "IMatch_" + patternGraphName;
        }

        /// <summary>
        /// Returns the name of the match interface for an iterated in a pattern graph
        /// </summary>
        public static string MatchInterfaceName(string patternGraphName, string iteratedName)
        {
            return "IMatch_" + patternGraphName + "_" + iteratedName;
        }

        /// <summary>
        /// Returns name of the action member variable storing the matched independent
        /// </summary>
        public static string MatchedIndependentVariable(string patternGraphName)
        {
            return "matched_independent_" + patternGraphName;
        }

        /// <summary>
        /// Returns name of the search program variable which will be filled
        /// if the pattern was matched and is needed in patternpath/global isomorphy checks
        /// </summary>
        public static string PatternpathMatch(string patternGraphName)
        {
            return "patternpath_match_" + patternGraphName;
        }

        /// <summary>
        /// Returns name of the given element in the match class with correct match part prefix
        /// </summary>
        public static string MatchName(string unprefixedElementName, BuildMatchObjectType matchPart)
        {
            switch(matchPart)
            {
            case BuildMatchObjectType.Node: return "node_" + unprefixedElementName;
            case BuildMatchObjectType.Edge: return "edge_" + unprefixedElementName;
            case BuildMatchObjectType.Variable: return "var_" + unprefixedElementName;
            case BuildMatchObjectType.Subpattern: return unprefixedElementName;
            case BuildMatchObjectType.InlinedSubpattern: return unprefixedElementName;
            case BuildMatchObjectType.Iteration: return unprefixedElementName;
            case BuildMatchObjectType.Alternative: return unprefixedElementName;
            case BuildMatchObjectType.Independent: return unprefixedElementName;
            default: return "INTERNAL ERROR";
            }
        }

        /// <summary>
        /// Returns name of the given element in the match class with correct match part prefix
        /// </summary>
        public static string MatchName(string unprefixedElementName, EntityType type)
        {
            switch(type)
            {
            case EntityType.Node: return "node_" + unprefixedElementName;
            case EntityType.Edge: return "edge_" + unprefixedElementName;
            case EntityType.Variable: return "var_" + unprefixedElementName;
            default: return "INTERNAL ERROR";
            }
        }

        /// <summary>
        /// Returns name of the state variable storing which direction run is currently underway
        /// </summary>
        public static string DirectionRunCounterVariable(string patternElementName)
        {
            return "directionRunCounterOf_" + patternElementName;
        }

        /// <summary>
        /// Returns the name of the variable that contains the hash of the current local match,
        /// better: the hash of the locally matched fields of the pattern modulo the inlined independent elements
        /// </summary>
        public static string DuplicateMatchHashVariable()
        {
            return "duplicateMatchHash";
        }

        /// <summary>
        /// Returns the name of the variable that contains the currently focused match with equal hash from the found local matches
        /// </summary>
        public static string DuplicateMatchCandidateVariable()
        {
            return "duplicateMatchCandidate";
        }

        /// <summary>
        /// Returns the name of the variable that contains the found local matches,
        /// accessible by the hash code of the locally matched fields of the pattern modulo the inlined independent elements
        /// </summary>
        public static string FoundMatchesForFilteringVariable()
        {
            return "foundLocalMatchesModuloElementsFromInlinedIndependents";
        }

        /// <summary>
        /// Returns a string representation of the given entity type
        /// </summary>
        public static string ToString(EntityType type)
        {
            switch(type)
            {
            case EntityType.Node: return "Node";
            case EntityType.Edge: return "Edge";
            case EntityType.Variable: return "Variable";
            default: return "INTERNAL ERROR";
            }
        }

        public static string IterationParallelizationListHead(string patternElementName)
        {
            return "parallel_preset_head_candidate_" + patternElementName;
        }

        public static string IterationParallelizationNextCandidate(string patternElementName)
        {
            return "parallel_preset_candidate_" + patternElementName;
        }

        public static string IterationParallelizationIterator(string patternElementName)
        {
            return "parallel_preset_iterator_" + patternElementName;
        }

        public static string IterationParallelizationDirectionRunCounterVariable(string patternElementName)
        {
            return "parallel_preset_directionRunCounterOf_" + patternElementName;
        }

        public static string IterationParallelizationParallelPresetCandidate(string patternElementName)
        {
            return "parallel_preset_candidate_" + patternElementName;
        }
    }
}

