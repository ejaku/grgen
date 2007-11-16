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
        public static string CandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_cur_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the type variable which will be created within the search program
        /// holding the type object which will be used for determining the candidates
        /// for the given pattern element
        /// </summary>
        public static string TypeForCandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_type_" + patternElementName;
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
        public static string TypeIdForCandidateVariable(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_type_id_" + patternElementName;
        }

        /// <summary>
        /// Returns name of the list head variable which will be created within the search program
        /// holding the list head of the list accessed by type id with the graph elements of that type
        /// for finding out when iteration of the candidates for the given pattern element has finished
        /// </summary>
        public static string CandidateIterationListHead(string patternElementName, bool isNode)
        {
            return (isNode ? "node" : "edge") + "_head_" + patternElementName;
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
        /// backing up the value of the mapped member of the graph element before assigning to it
        /// </summary>
        public static string VariableWithBackupOfMappedMember(string patternElementName, bool isNode, bool isPositive)
        {
            return CandidateVariable(patternElementName, isNode) + "_prev" + (isPositive ? "MappedTo" : "NegMappedTo");
        }
    }
}

