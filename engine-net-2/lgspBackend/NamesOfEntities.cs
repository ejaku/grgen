/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// class determining names of entities in generated source code from pattern element entities 
    /// </summary>
    class NamesOfEntities
    {
        /// <summary>
        /// Returns name of the candidate variable which will be created within the seach program
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
        /// Returns name of the type of the type variable
        /// </summary>
        public static string TypeOfVariableContainingType(bool isNode)
        {
            return (isNode ? "Node" : "Edge") + "Type";
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
        /// Returns name of the method called when a maybe preset element is not set
        /// </summary>
        public static string MissingPresetHandlingMethod(string patternElementName)
        {
            return "MissingPreset_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the variable which will be created within the seach program
        /// backing up the value of the isMatched-Bit of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfIsMatchedBit(string patternElementName, string negativeNamePrefix)
        {
            return "prev_" + negativeNamePrefix + "_" + CandidateVariable(patternElementName);
        }

        /// <summary>
        /// Returns name of the variable which will be created within the seach program
        /// backing up the value of the isMatched-Bit of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfIsMatchedBitGlobal(string patternElementName, string negativeNamePrefix)
        {
            return "prevGlobal_" + negativeNamePrefix + "_" + CandidateVariable(patternElementName);
        }

        /// <summary>
        /// Returns name of the task variable which will be created within the seach program
        /// holding the task object whose connections need to be filled before being pushed on the open tasks stack
        /// </summary>
        public static string TaskVariable(string subpatternElementName)
        {
            return "taskFor_" + subpatternElementName;
        }

        /// <summary>
        /// Returns name of the type of the task variable
        /// </summary>
        public static string TypeOfTaskVariable(string subpatternName, bool isAlternative)
        {
            return (isAlternative ? "AlternativeAction_" : "PatternAction_") + subpatternName;
        }

        /// <summary>
        /// Returns name of the rule pattern class
        /// </summary>
        public static string RulePatternClassName(string rulePatternName, bool isSubpattern)
        {
            return (isSubpattern ? "Pattern_" : "Rule_") + rulePatternName;
        }

        /// <summary>
        /// Returns name of the state variable storing which direction run is currently underway
        /// </summary>
        public static string DirectionRunCounterVariable(string patternElementName)
        {
            return "directionRunCounterOf_" + patternElementName;
        }
    }
}

