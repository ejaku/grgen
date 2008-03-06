using System.Collections.Generic;
using System.Diagnostics;

namespace de.unika.ipd.grGen.lgsp
{
    /// <summary>
    /// Base class for all search program operations, containing concatenation fields,
    /// so that search program operations can form a linked search program list
    /// - double linked list; next points to the following list element or null;
    /// previous points to the preceding list element 
    /// or the enclosing search program operation within the list anchor element
    /// </summary>
    abstract class SearchProgramOperation
    {
        public SearchProgramOperation Next;
        public SearchProgramOperation Previous;

        /// <summary>
        /// dumps search program operation (as string) into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Dump(SourceBuilder builder);

        /// <summary>
        /// emits c# code implementing search program operation into source builder
        /// to be implemented by concrete subclasses
        /// </summary>
        public abstract void Emit(SourceBuilder sourceCode);

        /// <summary>
        /// appends the new element to the search program operations list
        /// whose closing element until now was this
        /// returns the new closing element - the new elment
        /// </summary>
        public SearchProgramOperation Append(SearchProgramOperation newElement)
        {
            Debug.Assert(Next == null, "Append only at end of list");
            Debug.Assert(newElement.Previous == null, "Append only of element without predecessor");
            Next = newElement;
            newElement.Previous = this;
            return newElement;
        }

        /// <summary>
        /// insert the new element into the search program operations list
        /// between this and the succeeding element
        /// returns the element after this - the new element
        /// </summary>
        public SearchProgramOperation Insert(SearchProgramOperation newElement)
        {
            Debug.Assert(newElement.Previous == null, "Insert only of single unconnected element (previous)");
            Debug.Assert(newElement.Next == null, "Insert only of single unconnected element (next)");
            
            if (Next == null)
            {
                return Append(newElement);
            }

            SearchProgramOperation Successor = Next;
            Next = newElement;
            newElement.Next = Successor;
            Successor.Previous = newElement;
            newElement.Previous = this;           
            return newElement;
        }

        /// <summary>
        /// returns whether operation is a search nesting operation 
        /// containing other elements within some list inside
        /// bearing the search nesting/iteration structure
        /// default: false (cause only few operations are search nesting operations)
        /// </summary>
        public virtual bool IsSearchNestingOperation()
        {
            return false;
        }

        /// <summary>
        /// returns the nested search operations list anchor
        /// null if list not created or IsSearchNestingOperation == false
        /// default: null (cause only few search operations are nesting operations)
        /// </summary>
        public virtual SearchProgramOperation GetNestedSearchOperationsList()
        {
            return null;
        }

        /// <summary>
        /// returns operation enclosing this operation
        /// </summary>
        public SearchProgramOperation GetEnclosingSearchOperation()
        {
            SearchProgramOperation potentiallyNestingOperation = this;
            SearchProgramOperation nestedOperation;

            // iterate list leftwards, leftmost list element is list anchor element,
            // which contains uplink to enclosing search operation in it's previous member
            // step over search nesting operations we're not nested in 
            do
            {
                nestedOperation = potentiallyNestingOperation;
                potentiallyNestingOperation = nestedOperation.Previous;
            }
            while (!potentiallyNestingOperation.IsSearchNestingOperation() 
                || potentiallyNestingOperation.GetNestedSearchOperationsList()!=nestedOperation);

            return potentiallyNestingOperation;
        }
    }

    /// <summary>
    /// Search program list anchor element,
    /// containing first list element within inherited Next member
    /// Inherited to be able to access the first element via Next
    /// Previous points to enclosing search program operation
    /// (starts list, but doesn't contain one)
    /// </summary>
    class SearchProgramList : SearchProgramOperation
    {
        public SearchProgramList(SearchProgramOperation enclosingOperation)
        {
            Previous = enclosingOperation;
        }

        public override void Dump(SourceBuilder builder)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local dump-methods
            while (currentOperation != null)
            {
                currentOperation.Dump(builder);
                currentOperation = currentOperation.Next;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            SearchProgramOperation currentOperation = Next;

            // depth first walk over nested search program lists
            // walk current list here, recursive descent within local Emit-methods
            while (currentOperation != null)
            {
                currentOperation.Emit(sourceCode);
                currentOperation = currentOperation.Next;
            }
        }
    }

    /// <summary>
    /// Abstract base class for search programs.
    /// A search program is a list of search program operations,
    ///   some search program operations contain nested search program operations,
    ///   yielding a search program operation tree in fact
    /// represents/assembling a backtracking search program,
    /// for finding a homomorphic mapping of the pattern graph within the host graph.
    /// A search program is itself the outermost enclosing operation.
    /// </summary>
    abstract class SearchProgram : SearchProgramOperation
    {
        public override bool IsSearchNestingOperation()
        {
            return true; // contains complete nested search program
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return OperationsList;
        }

        public string Name;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Class representing the search program of a matching action, i.e. some test or rule
    /// The list forming concatenation field is used for adding missing preset search subprograms.
    /// </summary>
    class SearchProgramOfAction : SearchProgram
    {
        public SearchProgramOfAction(string name, bool containsSubpatterns)
        {
            Name = name;

            SetupSubpatternMatching = containsSubpatterns;
        }

        /// <summary>
        /// Dumps search program followed by missing preset search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of action {1}",
                Name, SetupSubpatternMatching ? "with subpattern matching setup\n" : "\n");

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next missing preset search subprogram
            if (Next != null)
            {
                Next.Dump(builder);
            }
        }

        /// <summary>
        /// Emits the matcher source code for all search programs
        /// first head of matching function of the current search program
        /// then the search program operations list in depth first walk over search program operations list
        /// then tail of matching function of the current search program
        /// and finally continues in missing preset search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif
            sourceCode.AppendFront("public LGSPMatches " + Name + "(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("matches.matchesList.Clear();\n");
            sourceCode.AppendFront("const int MAX_NEG_LEVEL = 5;\n");
            sourceCode.AppendFront("int negLevel = 0;\n");

            if (SetupSubpatternMatching)
            {
                sourceCode.AppendFront("Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();\n");
                sourceCode.AppendFront("List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();\n");
                sourceCode.AppendFront("List<Stack<LGSPMatch>> matchesList = foundPartialMatches;\n");
            }

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return matches;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit search subprograms
            if (Next != null)
            {
                Next.Emit(sourceCode);
            }
        }

        public bool SetupSubpatternMatching;
    }

    /// <summary>
    /// Class representing the search program of a missing preset matching action,
    /// originating from some test or rule with parameters which may be preset but may be null, too
    /// The list forming concatenation field is used for adding further missing preset search subprograms.
    /// </summary>
    class SearchProgramOfMissingPreset : SearchProgram
    {
        public SearchProgramOfMissingPreset(string name, bool containsSubpatterns,
            string[] parameters, bool[] parameterIsNode)
        {
            Name = name;

            SetupSubpatternMatching = containsSubpatterns;

            Parameters = parameters;
            ParameterIsNode = parameterIsNode;
        }

        /// <summary>
        /// Dumps search program followed by further missing preset search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of missing preset {1}",
                Name, SetupSubpatternMatching ? "with subpattern matching setup" : "");
            // parameters
            for (int i = 0; i < Parameters.Length; ++i)
            {
                string typeOfParameterVariableContainingCandidate =
                    ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                string parameterVariableContainingCandidate =
                    NamesOfEntities.CandidateVariable(Parameters[i]);
                builder.AppendFormat(", {0} {1}",
                    typeOfParameterVariableContainingCandidate,
                    parameterVariableContainingCandidate);
            }
            builder.Append("\n");

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }

            // then next search subprogram
            if (Next != null)
            {
                Next.Dump(builder);
            }
        }

        /// <summary>
        /// Emits the matcher source code for all search programs
        /// first head of matching function of the current search program
        /// then the search program operations list in depth first walk over search program operations list
        /// then tail of matching function of the current search program
        /// and finally continues in missing preset search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            sourceCode.AppendFront("public void " + Name + "(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList");
            for (int i = 0; i < Parameters.Length; ++i)
            {
                string typeOfParameterVariableContainingCandidate =
                    ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                string parameterVariableContainingCandidate =
                    NamesOfEntities.CandidateVariable(Parameters[i]);
                sourceCode.AppendFormat(", {0} {1}",
                    typeOfParameterVariableContainingCandidate,
                    parameterVariableContainingCandidate);
            }
            sourceCode.Append(")\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("const int MAX_NEG_LEVEL = 5;\n");
            sourceCode.AppendFront("int negLevel = 0;\n");

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit next search subprogram
            if (Next != null)
            {
                Next.Emit(sourceCode);
            }
        }

        public string[] Parameters;
        public bool[] ParameterIsNode;

        public bool SetupSubpatternMatching;
    }

    /// <summary>
    /// Class representing the search program of a subpattern
    /// </summary>
    class SearchProgramOfSubpattern : SearchProgram
    {
        public SearchProgramOfSubpattern(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Dumps search program 
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of subpattern\n", Name);
            builder.Append("\n");

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits the matcher source code for the search program
        /// head, search program operations list in depth first walk over search program operations list, tail
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            sourceCode.AppendFront("public override void " + Name + "(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("const int MAX_NEG_LEVEL = 5;\n");

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing the search program of an alternative
    /// </summary>
    class SearchProgramOfAlternative : SearchProgram
    {
        public SearchProgramOfAlternative(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Dumps search program
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of alternative case\n", Name);

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits the matcher source code for the search program
        /// head, search program operations list in depth first walk over search program operations list, tail
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            sourceCode.AppendFront("public override void " + Name + "(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            sourceCode.AppendFront("const int MAX_NEG_LEVEL = 5;\n");

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing "match the pattern of the alternative case" operation
    /// </summary>
    class GetPartialMatchOfAlternative : SearchProgramOperation
    {
        public GetPartialMatchOfAlternative(string pathPrefix, string caseName, string rulePatternClassName)
        {
            PathPrefix = pathPrefix;
            CaseName = caseName;
            RulePatternClassName = rulePatternClassName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("GetPartialMatchOfAlternative case {0}{1}\n", PathPrefix, CaseName);

            // then nested content
            if (OperationsList != null)
            {
                builder.Indent();
                OperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Alternative case {0}{1} \n", PathPrefix, CaseName);

            sourceCode.AppendFront("do {\n");
            sourceCode.Indent();
            string whichCase = RulePatternClassName + "." + PathPrefix + "CaseNums.@" + CaseName;
            sourceCode.AppendFrontFormat("patternGraph = patternGraphs[(int){0}];\n", whichCase);
            
            OperationsList.Emit(sourceCode);
            
            sourceCode.Unindent();
            sourceCode.AppendFront("} while(false);\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true; // contains complete nested search program of alternative case
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return OperationsList;
        }

        public string PathPrefix;
        public string CaseName;
        public string RulePatternClassName;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Base class for search program check operations
    /// contains list anchor for operations to execute when check failed
    /// (check is not a search operation, thus the check failed operations are not search nested operations)
    /// </summary>
    abstract class CheckOperation : SearchProgramOperation
    {
        // (nested) operations to execute when check failed
        public SearchProgramList CheckFailedOperations;
    }

    /// <summary>
    /// Base class for search program type determining operations,
    /// setting current type for following get candidate operation
    /// </summary>
    abstract class GetType : SearchProgramOperation
    {
    }

    /// <summary>
    /// Available types of GetTypeByIteration operations
    /// </summary>
    enum GetTypeByIterationType
    {
        ExplicitelyGiven, // iterate the explicitely given types
        AllCompatible // iterate all compatible types of the pattern element type
    }

    /// <summary>
    /// Class representing "iterate over the allowed types" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByIteration : GetType
    {
        public GetTypeByIteration(
            GetTypeByIterationType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeName,
            bool isNode)
        {
            Type = type;
            PatternElementName = patternElementName;
            if (type == GetTypeByIterationType.ExplicitelyGiven) {
                TypeName = rulePatternTypeNameOrTypeName;
            } else { // type == GetTypeByIterationType.AllCompatible
                RulePatternTypeName = rulePatternTypeNameOrTypeName;
            }
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetType ByIteration ");
            if(Type==GetTypeByIterationType.ExplicitelyGiven) {
                builder.Append("ExplicitelyGiven ");
                builder.AppendFormat("on {0} in {1} node:{2}\n", 
                    PatternElementName, TypeName, IsNode);
            } else { // Type==GetTypeByIterationType.AllCompatible
                builder.Append("AllCompatible ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            }
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            // todo: randomisierte auswahl des typen wenn RANDOM_LOOKUP_LIST_START ?

            // emit type iteration loop header
            string typeOfVariableContainingType = NamesOfEntities.TypeOfVariableContainingType(IsNode);
            string variableContainingTypeForCandidate = 
                NamesOfEntities.TypeForCandidateVariable(PatternElementName);
            string containerWithAvailableTypes;
            if (Type == GetTypeByIterationType.ExplicitelyGiven)
            {
                containerWithAvailableTypes = TypeName
                    + "." + PatternElementName + "_AllowedTypes";
            }
            else //(Type == GetTypeByIterationType.AllCompatible)
            {
                containerWithAvailableTypes = RulePatternTypeName
                    + ".typeVar.SubOrSameTypes";
            }
            
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                typeOfVariableContainingType, variableContainingTypeForCandidate,
                containerWithAvailableTypes);

            // open loop
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit type id setting and loop body 
            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1}.TypeID;\n",
                variableContainingTypeIDForCandidate, variableContainingTypeForCandidate);

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

        public GetTypeByIterationType Type;
        public string PatternElementName;
        public string RulePatternTypeName; // only valid if ExplicitelyGiven
        public string TypeName; // only valid if AllCompatible
        public bool IsNode; // node|edge

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "get the allowed type" operation,
    /// setting type id to use in the following get candidate by element iteration
    /// </summary>
    class GetTypeByDrawing : GetType
    {
        public GetTypeByDrawing(
            string patternElementName,
            string typeID,
            bool isNode)
        {
            PatternElementName = patternElementName;
            TypeID = typeID;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetType GetTypeByDrawing ");
            builder.AppendFormat("on {0} id:{1} node:{2}\n",
                PatternElementName, TypeID, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            string variableContainingTypeIDForCandidate = 
                NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("int {0} = {1};\n",
                variableContainingTypeIDForCandidate, TypeID);
        }

        public string PatternElementName;
        public string TypeID;
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Base class for search program candidate determining operations,
    /// setting current candidate for following check candidate operation
    /// </summary>
    abstract class GetCandidate : SearchProgramOperation
    {
        public string PatternElementName;
    }

    /// <summary>
    /// Available types of GetCandidateByIteration operations
    /// </summary>
    enum GetCandidateByIterationType
    {
        GraphElements, // available graph elements
        IncidentEdges // incident edges
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
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByIterationType.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            bool getIncoming)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            GetIncoming = getIncoming;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration ");
            if (Type == GetCandidateByIterationType.GraphElements) {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} incoming:{2}\n",
                    PatternElementName, StartingPointNodeName, GetIncoming);
            }
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == GetCandidateByIterationType.GraphElements)
            {
                // --- emit loop header ---
                // open loop header 
                sourceCode.AppendFrontFormat("for(");
                // emit declaration of variable containing graph elements list head
                string typeOfVariableContainingListHead =
                    IsNode ? "LGSPNode" : "LGSPEdge";
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
                sourceCode.AppendFormat("{0} = {1}.typeNext; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop condition: check for head reached again 
                sourceCode.AppendFormat("{0} != {1}; ",
                    variableContainingCandidate, variableContainingListHead);
                // emit loop increment: switch to next element of same type
                sourceCode.AppendFormat("{0} = {0}.typeNext",
                    variableContainingCandidate);
                // close loop header
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
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            GetIncoming ? "incoming" : "outgoing",
                            PatternElementName, StartingPointNodeName);

                // emit declaration of variable containing incident edges list head
                string typeOfVariableContainingListHead = "LGSPEdge";
                string variableContainingListHead =
                    NamesOfEntities.CandidateIterationListHead(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingListHead, variableContainingListHead);
                // emit initialization of variable containing incident edges list head
                string variableContainingStartingPointNode = 
                    NamesOfEntities.CandidateVariable(StartingPointNodeName);
                string memberOfNodeContainingListHead =
                    GetIncoming ? "inhead" : "outhead";
                sourceCode.AppendFormat(" = {0}.{1};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);

                // emit execute the following code only if head != null
                // todo: replace by check == null and continue
                sourceCode.AppendFrontFormat("if({0} != null)\n", variableContainingListHead);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration and initialization of variable containing candidates
                string typeOfVariableContainingCandidate = "LGSPEdge";
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate,
                    variableContainingListHead);
                // open loop
                sourceCode.AppendFront("do\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // emit loop tail
                // - emit switch to next edge in list within assignment expression
                string memberOfEdgeContainingNextEdge =
                    GetIncoming ? "inNext" : "outNext";
                sourceCode.AppendFrontFormat("while( ({0} = {0}.{1})",
                    variableContainingCandidate, memberOfEdgeContainingNextEdge);
                // - check condition that head has been reached again (compare with assignment value)
                sourceCode.AppendFormat(" != {0} );\n", variableContainingListHead);

                // close the head != null check
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
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

        public GetCandidateByIterationType Type;
        public bool IsNode; // node|edge - only available if GraphElements
        public string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public bool GetIncoming; // incoming|outgoing - only available if IncidentEdges

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Available types of GetCandidateByDrawing operations
    /// </summary>
    enum GetCandidateByDrawingType
    {
        NodeFromEdge, // draw node from given edge
        FromInputs, // draw element from action inputs
        FromSubpatternConnections // draw element from subpattern connections
    }

    /// <summary>
    /// Class representing "get node by drawing" operation,
    /// setting current candidate for following check candidate operations
    /// </summary>
    class GetCandidateByDrawing : GetCandidate
    {
        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string patternElementTypeName,
            string startingPointEdgeName,
            bool getSource)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Type = type;
            PatternElementName = patternElementName;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            GetSource = getSource;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string inputIndex,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromInputs);
            Type = type;
            PatternElementName = patternElementName;
            InputIndex = inputIndex;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromSubpatternConnections);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetCandidate ByDrawing ");
            if(Type==GetCandidateByDrawingType.NodeFromEdge) {
                builder.Append("NodeFromEdge ");
                builder.AppendFormat("on {0} of {1} from {2} source:{3}\n",
                    PatternElementName, PatternElementTypeName, 
                    StartingPointEdgeName, GetSource);
            } else if(Type==GetCandidateByDrawingType.FromInputs) {
                builder.Append("FromInputs ");
                builder.AppendFormat("on {0} index:{1} node:{2}\n",
                    PatternElementName, InputIndex, IsNode);
            } else { // Type==GetCandidateByDrawingType.FromSubpatternConnections
                builder.Append("FromSubpatternConnections ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type==GetCandidateByDrawingType.NodeFromEdge)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Implicit {0} {1} from {2} \n",
                            GetSource ? "source" : "target",
                            PatternElementName, StartingPointEdgeName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate = "LGSPNode";
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with demanded node from variable containing edge
                string variableContainingStartingPointEdge =
                    NamesOfEntities.CandidateVariable(StartingPointEdgeName);
                string whichImplicitNode = 
                    GetSource ? "source" : "target";
                sourceCode.AppendFormat(" = {0}.{1};\n",
                    variableContainingStartingPointEdge, whichImplicitNode);
            }
            else if(Type==GetCandidateByDrawingType.FromInputs)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Preset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                string typeOfVariableContainingCandidate =
                    IsNode ? "LGSPNode" : "LGSPEdge";
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with element from input parameters array
                sourceCode.AppendFormat(" = ({0}) parameters[{1}];\n",
                    typeOfVariableContainingCandidate, InputIndex);
            }
            else //Type==GetCandidateByDrawingType.FromSubpatternConnections
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// SubPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                // and initialization with element from subpattern connections
                string typeOfVariableContainingCandidate =
                    IsNode ? "LGSPNode" : "LGSPEdge";
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, PatternElementName);
            }
        }

        public GetCandidateByDrawingType Type;
        public string PatternElementTypeName; // only valid if NodeFromEdge
        public string StartingPointEdgeName; // from pattern - only valid if NodeFromEdge
        public bool GetSource; // source|target - only valid if NodeFromEdge
        public string InputIndex; // only valid if FromInputs
        public bool IsNode; // node|edge - only valid if FromInputs

    }

    /// <summary>
    /// Base class for search program candidate filtering operations
    /// </summary>
    abstract class CheckCandidate : CheckOperation
    {
        public string PatternElementName;
    }

    /// <summary>
    /// Available types of CheckCandidateForType operations
    /// </summary>
    enum CheckCandidateForTypeType
    {
        ByIsAllowedType, //check by inspecting the IsAllowedType array of the rule pattern
        ByIsMyType, // check by inspecting the IsMyType array of the graph element's type model
        ByAllowedTypes // check by comparing against a member of the AllowedTypes array of the rule pattern
    }

    /// <summary>
    /// Class representing "check whether candidate is of allowed type" operation
    /// </summary>
    class CheckCandidateForType : CheckCandidate
    {
        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string rulePatternTypeNameOrTypeNameOrTypeID,
            bool isNode)
        {
            Type = type;
            PatternElementName = patternElementName;
            if (type == CheckCandidateForTypeType.ByIsAllowedType) {
                RulePatternTypeName = rulePatternTypeNameOrTypeNameOrTypeID;
            } else if (type == CheckCandidateForTypeType.ByIsMyType) {
                TypeName = rulePatternTypeNameOrTypeNameOrTypeID;
            } else { // CheckCandidateForTypeType.ByAllowedTypes
                TypeID = rulePatternTypeNameOrTypeNameOrTypeID;
            }
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForType ");
            if (Type == CheckCandidateForTypeType.ByIsAllowedType) {
                builder.Append("ByIsAllowedType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, RulePatternTypeName, IsNode);
            } else if (Type == CheckCandidateForTypeType.ByIsMyType) {
                builder.Append("ByIsMyType ");
                builder.AppendFormat("on {0} in {1} node:{2}\n",
                    PatternElementName, TypeName, IsNode);
            } else { // Type == CheckCandidateForTypeType.ByAllowedTypes
                builder.Append("ByAllowedTypes ");
                builder.AppendFormat("on {0} id:{1} node:{2}\n",
                    PatternElementName, TypeID, IsNode);
            }
            
            // then operations for case check failed
            if (CheckFailedOperations != null)
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
            if (Type == CheckCandidateForTypeType.ByIsAllowedType)
            {
                string isAllowedTypeArrayMemberOfRulePattern =
                    PatternElementName + "_IsAllowedType";
                sourceCode.AppendFrontFormat("if(!{0}.{1}[{2}.type.TypeID]) ",
                    RulePatternTypeName, isAllowedTypeArrayMemberOfRulePattern,
                    variableContainingCandidate);
            }
            else if (Type == CheckCandidateForTypeType.ByIsMyType)
            {
                sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.type.TypeID]) ",
                    TypeName, variableContainingCandidate);
            }
            else // Type == CheckCandidateForTypeType.ByAllowedTypes)
            {
                sourceCode.AppendFrontFormat("if({0}.type.TypeID != {1}) ",
                    variableContainingCandidate, TypeID);
            }
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public CheckCandidateForTypeType Type;

        public string RulePatternTypeName; // only valid if ByIsAllowedType
        public string TypeName; // only valid if ByIsMyType
        public string TypeID; // only valid if ByAllowedTypes

        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing some check candidate operation,
    /// which was determined at generation time to always fail 
    /// </summary>
    class CheckCandidateFailed : CheckCandidate
    {
        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate Failed \n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
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
    /// Class representing "check whether candidate is connected to the elements
    ///   it should be connected to, according to the pattern" operation
    /// </summary>
    class CheckCandidateForConnectedness : CheckCandidate
    {
        public CheckCandidateForConnectedness(
            string patternElementName, 
            string patternNodeName,
            string patternEdgeName,
            bool checkSource)
        {
            // pattern element is the candidate to check, either node or edge
            PatternElementName = patternElementName;
            PatternNodeName = patternNodeName;
            PatternEdgeName = patternEdgeName;
            CheckSource = checkSource; 
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForConnectedness ");
            builder.AppendFormat("{0}=={1}.{2}\n",
                PatternNodeName, PatternEdgeName, CheckSource ? "source" : "target");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision for is candidate connected to already found partial match
            // i.e. edge source/target equals node
            sourceCode.AppendFrontFormat("if({0}.{1} != {2}) ",
                NamesOfEntities.CandidateVariable(PatternEdgeName),
                CheckSource ? "source" : "target",
                NamesOfEntities.CandidateVariable(PatternNodeName));
            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string PatternNodeName;
        public string PatternEdgeName;
        public bool CheckSource; // source|target
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other pattern element, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written by AcceptCandidate
    /// </summary>
    class CheckCandidateForIsomorphy : CheckCandidate
    {
        public CheckCandidateForIsomorphy(
            string patternElementName,
            List<string> namesOfPatternElementsToCheckAgainst,
            string negativeNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NamesOfPatternElementsToCheckAgainst = namesOfPatternElementsToCheckAgainst;
            NegativeNamePrefix = negativeNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphy ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2} ",
                PatternElementName, NegativeNamePrefix, IsNode);
            if (NamesOfPatternElementsToCheckAgainst != null)
            {
                builder.Append("against ");
                foreach (string name in NamesOfPatternElementsToCheckAgainst)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
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
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Append("(negLevel<=MAX_NEG_LEVEL ? ");
            }
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = IsNode ? "LGSPNode.IS_MATCHED<<negLevel" : "LGSPEdge.IS_MATCHED<<negLevel";
            sourceCode.AppendFormat("({0}.flags & {1}) == {1}", variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Append(" : ");
                sourceCode.AppendFormat("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].{0}.ContainsKey({1}))",
                    IsNode ? "fst" : "snd", variableContainingCandidate);
            }

            // but only if isomorphy is demanded (NamesOfPatternElementsToCheckAgainst empty)
            // otherwise homomorphy to certain elements is allowed, 
            // so we only fail if the graph element is matched to one of the not allowed elements,
            // given in NamesOfPatternElementsToCheckAgainst 
            if (NamesOfPatternElementsToCheckAgainst != null)
            {
                Debug.Assert(NamesOfPatternElementsToCheckAgainst.Count > 0);

                sourceCode.Append("\n");
                sourceCode.Indent();

                if (NamesOfPatternElementsToCheckAgainst.Count == 1)
                {
                    string name = NamesOfPatternElementsToCheckAgainst[0];
                    sourceCode.AppendFrontFormat("&& {0}=={1}\n", variableContainingCandidate,
                        NamesOfEntities.CandidateVariable(name));
                }
                else
                {
                    bool first = true;
                    foreach (string name in NamesOfPatternElementsToCheckAgainst)
                    {
                        if (first)
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

        public List<string> NamesOfPatternElementsToCheckAgainst;
        public string NegativeNamePrefix; // "" if positive
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other pattern element, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written by AcceptCandidate
    /// </summary>
    class CheckCandidateForIsomorphyGlobal : CheckCandidate
    {
        public CheckCandidateForIsomorphyGlobal(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyGlobal ");
            builder.AppendFormat("on {0} node:{1} \n",
                PatternElementName, IsNode);
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // fail if graph element contained within candidate was already matched
            // (in another subpattern to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = IsNode ? "LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN" 
                : "LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("if(({0}.flags & {1})=={1})\n",
                variableContainingCandidate, isMatchedBit);

            // emit check failed code
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing "check whether candidate was preset (not null)" operation
    /// </summary>
    class CheckCandidateForPreset : CheckCandidate
    {
        public CheckCandidateForPreset(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public void CompleteWithArguments(
            List<string> neededElements,
            List<bool> neededElementIsNode)
        {
            NeededElements = new string[neededElements.Count];
            NeededElementIsNode = new bool[neededElementIsNode.Count];
            int i = 0;
            foreach (string ne in neededElements)
            {
                NeededElements[i] = ne;
                ++i;
            }
            i = 0;
            foreach (bool nein in neededElementIsNode)
            {
                NeededElementIsNode[i] = nein;
                ++i;
            }
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForPreset ");
            builder.AppendFormat("on {0} node:{1} ",
                PatternElementName, IsNode);
            if (NeededElements != null)
            {
                builder.Append("with ");
                foreach (string neededElement in NeededElements)
                {
                    builder.AppendFormat("{0} ", neededElement);
                }
            }
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check whether candidate was preset (not null)
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            sourceCode.AppendFrontFormat("if({0} == null) ",
                variableContainingCandidate);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            // emit call to search program doing lookup if candidate was not preset
            string nameOfMissingPresetHandlingMethod = NamesOfEntities.MissingPresetHandlingMethod(
                PatternElementName);
            sourceCode.AppendFrontFormat("{0}",
                nameOfMissingPresetHandlingMethod);
            // emit call arguments
            sourceCode.Append("(");
            sourceCode.Append("graph, maxMatches, parameters, null, null, null");
            for (int i = 0; i < NeededElements.Length; ++i)
            {
                sourceCode.AppendFormat(", {0}", NamesOfEntities.CandidateVariable(NeededElements[i]));
            }
            sourceCode.Append(")");
            sourceCode.Append(";\n");

            // emit further check failed code
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string[] NeededElements;
        public bool[] NeededElementIsNode;
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Base class for search program operations
    /// filtering partial match
    /// (of the pattern part under construction)
    /// </summary>
    abstract class CheckPartialMatch : CheckOperation
    {
    }

    /// <summary>
    /// Class representing "check whether the negative pattern applies" operation
    /// </summary>
    class CheckPartialMatchByNegative : CheckPartialMatch
    {
        public CheckPartialMatchByNegative(string[] neededElements)
        {
            NeededElements = neededElements;
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check negative without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByNegative with ");
            foreach (string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then nested content
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// NegativePattern \n");
            // currently needed because of multiple negMapped backup variables with same name
            // todo: assign names to negatives, mangle that name in, then remove block again
            // todo: remove (neg)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// NegativePattern end\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public string[] NeededElements;

        // search program of the negative pattern
        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "check whether the condition applies" operation
    /// </summary>
    class CheckPartialMatchByCondition : CheckPartialMatch
    {
        public CheckPartialMatchByCondition(
            string conditionID,
            string rulePatternTypeName,
            string[] neededNodes,
            string[] neededEdges)
        {
            ConditionID = conditionID;
            RulePatternTypeName = rulePatternTypeName;
            
            int i = 0;
            NeededElements = new string[neededNodes.Length + neededEdges.Length];
            NeededElementIsNode = new bool[neededNodes.Length + neededEdges.Length];
            foreach (string neededNode in neededNodes)
            {
                NeededElements[i] = neededNode;
                NeededElementIsNode[i] = true;
                ++i;
            }
            foreach (string neededEdge in neededEdges)
            {
                NeededElements[i] = neededEdge;
                NeededElementIsNode[i] = false;
                ++i;
            }
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ByCondition ");
            builder.AppendFormat("id:{0} in {1} with ",
                ConditionID, RulePatternTypeName);
            foreach (string neededElement in NeededElements)
            {
                builder.AppendFormat("{0} ", neededElement);
            }
            builder.Append("\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }

        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Condition \n");

            // open decision
            sourceCode.AppendFront("if(");
            // emit call to condition checking code
            sourceCode.AppendFormat("!{0}.Condition_{1}",
                RulePatternTypeName, ConditionID);
            // emit call arguments
            sourceCode.Append("(");
            bool first = true;
            for (int i = 0; i < NeededElements.Length; ++i)
            {
                if (!first)
                {
                    sourceCode.Append(", ");
                }
                else
                {
                    first = false;
                }
                sourceCode.Append(NamesOfEntities.CandidateVariable(NeededElements[i]));
            }
            sourceCode.Append(")");
            // close decision
            sourceCode.Append(") ");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string ConditionID;
        public string RulePatternTypeName;
        public string[] NeededElements;
        public bool[] NeededElementIsNode;
    }

    /// <summary>
    /// Class representing "check whether the subpatterns of the pattern were found" operation
    /// </summary>
    class CheckPartialMatchForSubpatternsFound : CheckPartialMatch
    {
        public CheckPartialMatchForSubpatternsFound(string negativePrefix)
        {
            NegativePrefix = negativePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckPartialMatch ForSubpatternsFound\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }

        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether subpatterns were found \n");

            // emit decision
            sourceCode.AppendFrontFormat("if({0}matchesList.Count>0) ", NegativePrefix);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string NegativePrefix;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate checking succeded;
    /// (currently only) writing isomorphy information to graph, for isomorphy checking later on
    /// (mapping graph element to pattern element)
    /// </summary>
    class AcceptCandidate : SearchProgramOperation
    {
        public AcceptCandidate(
            string patternElementName,
            string negativeNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeNamePrefix = negativeNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }

            string isMatchedBit = IsNode ? "LGSPNode.IS_MATCHED<<negLevel" : "LGSPEdge.IS_MATCHED<<negLevel";
            sourceCode.AppendFrontFormat("{0} = {1}.flags & {2};\n",
                variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
            sourceCode.AppendFrontFormat("{0}.flags |= {1};\n",
                variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else {\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].{1}.ContainsKey({2}) ? 1U : 0U;\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.AppendFrontFormat("if({0}==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].{1}.Add({2},{2});\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeNamePrefix; // "" if positive
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate gets accepted 
    /// into a complete match of its subpattern, locking candidate for other subpatterns
    /// </summary>
    class AcceptCandidateGlobal : SearchProgramOperation
    {
        public AcceptCandidateGlobal(
            string patternElementName,
             bool isNode)
        {
            PatternElementName = patternElementName;
             IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidateGlobal ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = IsNode ? "LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN"
                : "LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0}.flags |= {1};\n",
                variableContainingCandidate, isMatchedBit);
        }

        public string PatternElementName;
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing operations undoing effects of candidate acceptance 
    /// when performing the backtracking step;
    /// (currently only) restoring isomorphy information in graph, as not needed any more
    /// (mapping graph element to pattern element)
    /// </summary>
    class AbandonCandidate : SearchProgramOperation
    {
        public AbandonCandidate(
            string patternElementName,
            string negativeNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeNamePrefix = negativeNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }
   
            sourceCode.AppendFrontFormat("{0}.flags = {0}.flags & ~{1} | {1};\n",
                variableContainingCandidate, variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else { \n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("if({0}==0)", variableContainingBackupOfMappedMember);
                sourceCode.Append(" {\n"); // wtf? appending this string directly to string above throws exception
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].{0}.Remove({1});\n",
                    IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeNamePrefix; // "" if positive
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing operations undoing effects of candidate acceptance 
    /// into complete match of it's subpattern when performing the backtracking step (unlocks candidate)
    /// </summary>
    class AbandonCandidateGlobal : SearchProgramOperation
    {
        public AbandonCandidateGlobal(
            string patternElementName,
            bool isNode)
        {
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidateGlobal ");
            builder.AppendFormat("on {0} node:{1}\n",
                PatternElementName, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = IsNode ? "LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN" 
                : "LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0}.flags &= ~{1};\n", variableContainingCandidate, isMatchedBit);
        }

        public string PatternElementName;
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern without contained subpatterns was matched
    /// </summary>
    class PositivePatternWithoutSubpatternsMatched : SearchProgramOperation
    {
        public PositivePatternWithoutSubpatternsMatched()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PositivePatternMatched \n");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();\n");
            sourceCode.AppendFront("match.patternGraph = rulePattern.patternGraph;\n");

            // emit match building operations
            MatchBuildingOperations.Emit(sourceCode);

            sourceCode.AppendFront("matches.matchesList.PositionWasFilledFixIt();\n");
        }

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a subpattern without contained subpatterns was matched (as the last element of the search)
    /// </summary>
    class LeafSubpatternMatched : SearchProgramOperation
    {
        public LeafSubpatternMatched(string numNodes, string numEdges)
        {
            NumNodes = numNodes;
            NumEdges = numEdges;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("LeafSubpatternMatched \n");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();\n");
            sourceCode.AppendFront("foundPartialMatches.Add(currentFoundPartialMatch);\n");

            sourceCode.AppendFrontFormat("LGSPMatch match = new LGSPMatch(new LGSPNode[{0}], new LGSPEdge[{1}], new LGSPMatch[0]);\n",
                NumNodes, NumEdges);
            sourceCode.AppendFront("match.patternGraph = patternGraph;\n");
            MatchBuildingOperations.Emit(sourceCode); // emit match building operations
            sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");
        }

        public string NumNodes;
        public string NumEdges;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern was matched and all of it's subpatterns were matched at least once
    /// </summary>
    class PatternAndSubpatternsMatched : SearchProgramOperation
    {
        public PatternAndSubpatternsMatched()
        {
            IsSubpatternOrAlternative = false;
        }

        public PatternAndSubpatternsMatched(int numNodes, int numEdges, 
            int numSubpatterns, int numAlternatives)
        {
            IsSubpatternOrAlternative = true;
            NumNodes = numNodes;
            NumEdges = numEdges;
            NumSubpatterns = numSubpatterns;
            NumAlternatives = numAlternatives;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PatternAndSubpatternsMatched ");
            builder.AppendFormat("isSubpatternOrAlternative:{0} \n", IsSubpatternOrAlternative);

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (IsSubpatternOrAlternative)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object\n");
                sourceCode.AppendFront("foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("LGSPMatch match = new LGSPMatch(new LGSPNode[{0}], new LGSPEdge[{1}], new LGSPMatch[{2}+{3}]);\n",
                    NumNodes, NumEdges, NumSubpatterns, NumAlternatives);
                sourceCode.AppendFrontFormat("match.patternGraph = patternGraph;\n");
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else // top-level pattern with subpatterns/alternatives
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it\n");
                sourceCode.AppendFront("foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFront("LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();\n");
                sourceCode.AppendFront("match.patternGraph = rulePattern.patternGraph;\n");
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("matches.matchesList.PositionWasFilledFixIt();\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("matchesList.Clear();\n");
            }
        }

        public bool IsSubpatternOrAlternative;
        public int NumNodes;
        public int NumEdges;
        public int NumSubpatterns;
        public int NumAlternatives;

        public SearchProgramList MatchBuildingOperations;
    }

    enum NegativePatternMatchedType
    {
        WithoutSubpatterns,
        ContainingSubpatterns
    };

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a negative pattern was matched
    /// </summary>
    class NegativePatternMatched : SearchProgramOperation
    {
        public NegativePatternMatched(NegativePatternMatchedType type, string negativePrefix)
        {
            Type = type;
            NegativePrefix = negativePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("NegativePatternMatched ");
            builder.Append(Type == NegativePatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == NegativePatternMatchedType.WithoutSubpatterns)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern found\n");
            }
            else
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern with contained subpatterns found\n");

                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativePrefix);
            }
        }

        public NegativePatternMatchedType Type;
        public string NegativePrefix;
    }

    /// <summary>
    /// Available types of BuildMatchObject operations
    /// </summary>
    enum BuildMatchObjectType
    {
        Node, // build match object with match for node 
        Edge, // build match object with match for edge
        Subpattern, // build match object with match for subpattern
        Alternative // build match object with match for alternative
    }

    /// <summary>
    /// Class representing "pattern was matched, now build match object" operation
    /// </summary>
    class BuildMatchObject : SearchProgramOperation
    {
        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string pathPrefixForEnum,
            int numSubpatterns)
        {
            Type = type;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            PathPrefixForEnum = pathPrefixForEnum;
            NumSubpatterns = numSubpatterns; // only valid if type == Alternative
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("BuildMatchObject ");
            if (Type == BuildMatchObjectType.Node) builder.Append("Node ");
            if (Type == BuildMatchObjectType.Edge) builder.Append("Edge ");
            if (Type == BuildMatchObjectType.Subpattern) builder.Append("Subpattern ");
            if (Type == BuildMatchObjectType.Subpattern) builder.Append("Alternative ");
            builder.AppendFormat("with {0} within {1}\n", PatternElementName, RulePatternClassName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == BuildMatchObjectType.Node || Type == BuildMatchObjectType.Edge)
            {
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                string matchObjectElementMember =
                    Type==BuildMatchObjectType.Node ? "Nodes" : "Edges";
                string nameToIndexEnum = PathPrefixForEnum +
                    (Type==BuildMatchObjectType.Node ? "NodeNums" : "EdgeNums");
                 sourceCode.AppendFrontFormat("match.{0}[(int){1}.{2}.@{3}] = {4};\n",
                    matchObjectElementMember, RulePatternClassName, nameToIndexEnum, PatternElementUnprefixedName,
                    variableContainingCandidate);
            }
            else if (Type == BuildMatchObjectType.Subpattern)
            {
                string nameToIndexEnum = PathPrefixForEnum + "SubNums";
                sourceCode.AppendFrontFormat("match.EmbeddedGraphs[(int){0}.{1}.@{2}]",
                    RulePatternClassName, nameToIndexEnum, PatternElementUnprefixedName);
                sourceCode.Append(" = currentFoundPartialMatch.Pop();\n");
            }
            else // Type == BuildMatchObjectType.Alternative
            {
                string nameToIndexEnum = PathPrefixForEnum + "AltNums";
                sourceCode.AppendFrontFormat("match.EmbeddedGraphs[((int){0}.{1}.@{2})+{3}]",
                    RulePatternClassName, nameToIndexEnum, PatternElementUnprefixedName, NumSubpatterns);
                sourceCode.Append(" = currentFoundPartialMatch.Pop();\n");
            }
        }

        public BuildMatchObjectType Type;
        public string PatternElementUnprefixedName;
        public string PatternElementName;
        public string RulePatternClassName;
        public string PathPrefixForEnum;
        public int NumSubpatterns;
    }

    /// <summary>
    /// Available types of AdjustListHeads operations
    /// </summary>
    enum AdjustListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class AdjustListHeads : SearchProgramOperation
    {
        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == AdjustListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public AdjustListHeads(
            AdjustListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            bool isIncoming)
        {
            Debug.Assert(type == AdjustListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IsIncoming = isIncoming;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AdjustListHeads ");
            if(Type==AdjustListHeadsTypes.GraphElements) {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else { // Type==AdjustListHeadsTypes.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                    PatternElementName, StartingPointNodeName, IsIncoming);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == AdjustListHeadsTypes.GraphElements)
            {
                sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                    NamesOfEntities.CandidateVariable(PatternElementName));
            }
            else //Type == AdjustListHeadsTypes.IncidentEdges
            {
                if (IsIncoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName),
                        NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName),
                        NamesOfEntities.CandidateVariable(PatternElementName));
                }
            }
        }

        public AdjustListHeadsTypes Type;
        public string PatternElementName;
        public bool IsNode; // node|edge - only valid if GraphElements
        public string StartingPointNodeName; // only valid if IncidentEdges
        public bool IsIncoming; // only valid if IncidentEdges
    }

    /// <summary>
    /// Base class for search program operations
    /// to check whether to continue the matching process 
    /// (of the pattern part under construction)
    /// </summary>
    abstract class CheckContinueMatching : CheckOperation
    {
    }

    /// <summary>
    /// Class representing "check if matching process is to be aborted because
    /// there are no tasks to execute left" operation
    /// </summary>
    class CheckContinueMatchingTasksLeft : CheckContinueMatching
    {
        public CheckContinueMatchingTasksLeft()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching TasksLeft\n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// Check whether there are subpattern matching tasks left to execute\n");

            sourceCode.AppendFront("if(openTasks.Count==0)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing "check if matching process is to be aborted because
    /// the maximum number of matches has been reached" operation
    /// listHeadAdjustment==false prevents listentrick
    /// </summary>
    class CheckContinueMatchingMaximumMatchesReached : CheckContinueMatching
    {
        public CheckContinueMatchingMaximumMatchesReached(bool subpatternLevel, bool listHeadAdjustment)
        {
            SubpatternLevel = subpatternLevel;
            ListHeadAdjustment = listHeadAdjustment;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching MaximumMatchesReached ");
            if (ListHeadAdjustment) builder.Append("ListHeadAdjustment ");
            if (SubpatternLevel) builder.Append("SubpatternLevel ");
            builder.Append("\n");

            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// if enough matches were found, we leave\n");
            if (SubpatternLevel)
                sourceCode.AppendFront("if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)\n");
            else
                sourceCode.AppendFront("if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)\n");
                
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool SubpatternLevel;
        public bool ListHeadAdjustment;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort negative matching process always succeeds
    /// </summary>
    class CheckContinueMatchingOfNegativeFailed : CheckContinueMatching
    {
        public CheckContinueMatchingOfNegativeFailed()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfNegativeFailed \n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // nothing locally, just emit check failed code
            CheckFailedOperations.Emit(sourceCode);
        }
    }

    /// <summary>
    /// Available types of ContinueOperation operations
    /// </summary>
    enum ContinueOperationType
    {
        ByReturn,
        ByContinue,
        ByGoto
    }

    /// <summary>
    /// Class representing "continue matching there" control flow operations
    /// </summary>
    class ContinueOperation : SearchProgramOperation
    {
        public ContinueOperation(ContinueOperationType type,
            bool returnMatches)
        {
            Debug.Assert(type == ContinueOperationType.ByReturn);
            Type = type;
            ReturnMatches = returnMatches;
        }

        public ContinueOperation(ContinueOperationType type)
        {
            Debug.Assert(type == ContinueOperationType.ByContinue);
            Type = type;
        }

        public ContinueOperation(ContinueOperationType type,
            string labelName)
        {
            Debug.Assert(type == ContinueOperationType.ByGoto);
            Type = type;
            LabelName = labelName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ContinueOperation ");
            if(Type==ContinueOperationType.ByReturn) {
                builder.Append("ByReturn ");
                builder.AppendFormat("return matches:{0}\n", ReturnMatches);
            } else if(Type==ContinueOperationType.ByContinue) {
                builder.Append("ByContinue\n");
            } else { // Type==ContinueOperationType.ByGoto
                builder.Append("ByGoto ");
                builder.AppendFormat("{0}\n", LabelName);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == ContinueOperationType.ByReturn)
            {
                if (ReturnMatches)
                {
                    sourceCode.AppendFront("return matches;\n");
                }
                else
                {
                    sourceCode.AppendFront("return;\n");
                }
            }
            else if (Type == ContinueOperationType.ByContinue)
            {
                sourceCode.AppendFront("continue;\n");
            }
            else //Type == ContinueOperationType.ByGoto
            {
                sourceCode.AppendFrontFormat("goto {0};\n", LabelName);
            }
        }

        public ContinueOperationType Type;
        public bool ReturnMatches; // only valid if ByReturn
        public string LabelName; // only valid if ByGoto
    }

    /// <summary>
    /// Class representing location within code named with label,
    /// potential target of goto operation
    /// </summary>
    class GotoLabel : SearchProgramOperation
    {
        public GotoLabel()
        {
            LabelName = "label" + labelId.ToString();
            ++labelId;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("Goto Label ");
            builder.AppendFormat("{0}\n", LabelName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("{0}: ;\n", LabelName);
        }

        public string LabelName;
        private static int labelId = 0;
    }

    /// <summary>
    /// Available types of RandomizeListHeads operations
    /// </summary>
    enum RandomizeListHeadsTypes
    {
        GraphElements,
        IncidentEdges
    }

    /// <summary>
    /// Class representing "adjust list heads" operation ("listentrick")
    /// </summary>
    class RandomizeListHeads : SearchProgramOperation
    {
        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.GraphElements);
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public RandomizeListHeads(
            RandomizeListHeadsTypes type,
            string patternElementName,
            string startingPointNodeName,
            bool isIncoming)
        {
            Debug.Assert(type == RandomizeListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IsIncoming = isIncoming;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("RandomizeListHeads ");
            if (Type == RandomizeListHeadsTypes.GraphElements)
            {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            }
            else
            { // Type==RandomizeListHeadsTypes.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from:{1} incoming:{2}\n",
                    PatternElementName, StartingPointNodeName, IsIncoming);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            // --- move list head from current position to random position ---

            if (Type == RandomizeListHeadsTypes.GraphElements)
            {
                // emit declaration of variable containing random position to move list head to
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFormat("int {0}", variableContainingRandomPosition);
                // emit initialization with ramdom position
                string graphMemberContainingElementListCountsByType =
                    IsNode ? "nodesByTypeCounts" : "edgesByTypeCounts";
                string variableContainingTypeIDForCandidate = 
                    NamesOfEntities.TypeIdForCandidateVariable(PatternElementName);
                sourceCode.AppendFormat(" = random.Next(graph.{0}[{1}]);\n",
                    graphMemberContainingElementListCountsByType,
                    variableContainingTypeIDForCandidate);
                // emit declaration of variable containing element at random position
                string typeOfVariableContainingElementAtRandomPosition =
                    IsNode ? "LGSPNode" : "LGSPEdge";
                string variableContainingElementAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingElementAtRandomPosition,
                    variableContainingElementAtRandomPosition);
                // emit initialization with element list head
                string graphMemberContainingElementListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                sourceCode.AppendFormat(" = graph.{0}[{1}];\n",
                    graphMemberContainingElementListHeadByType, variableContainingTypeIDForCandidate);
                // emit iteration to get element at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.Next;\n",
                    variableContainingRandomPosition, variableContainingElementAtRandomPosition);
                // iteration left, element is the one at the requested random position
                // move list head after element at random position, 
                sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                    variableContainingElementAtRandomPosition);
                // effect is new random starting point for following iteration
            }
            else //Type == RandomizeListHeadsTypes.IncidentEdges
            {
                // emit "randomization only if list is not empty"
                string variableContainingStartingPointNode =
                    NamesOfEntities.CandidateVariable(StartingPointNodeName);
                string memberOfNodeContainingListHead =
                    IsIncoming ? "inhead" : "outhead";
                sourceCode.AppendFrontFormat("if({0}.{1}!=null)\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit declaration of variable containing random position to move list head to, initialize it to 0 
                string variableContainingRandomPosition =
                    "random_position_" + PatternElementName;
                sourceCode.AppendFrontFormat("int {0} = 0;", variableContainingRandomPosition);
                // misuse variable to store length of list which is computed within the follwing iteration
                string memberOfEdgeContainingNextEdge =
                    IsIncoming ? "inNext" : "outNext";
                sourceCode.AppendFrontFormat("for(LGSPEdge edge = {0}.{1}; edge!={0}.{1}; edge=edge.{2}) ++{3};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead,
                    memberOfEdgeContainingNextEdge, variableContainingRandomPosition);
                // emit initialization of variable containing ramdom position
                // now that the necessary length of the list is known after the iteration
                // given in the variable itself
                sourceCode.AppendFrontFormat("{0} = random.Next({0});\n",
                    variableContainingRandomPosition);
                // emit declaration of variable containing edge at random position
                string variableContainingEdgeAtRandomPosition =
                    "random_element_" + PatternElementName;
                sourceCode.AppendFrontFormat("LGSPEdge {0}",
                    variableContainingEdgeAtRandomPosition);
                // emit initialization with edge list head
                sourceCode.AppendFormat(" = {0}.{1};\n",
                    variableContainingStartingPointNode, memberOfNodeContainingListHead);
                // emit iteration to get edge at random position
                sourceCode.AppendFrontFormat(
                    "for(int i = 0; i < {0}; ++i) {1} = {1}.{2};\n",
                    variableContainingRandomPosition,
                    variableContainingEdgeAtRandomPosition,
                    memberOfEdgeContainingNextEdge);
                // iteration left, edge is the one at the requested random position
                // move list head after edge at random position, 
                if (IsIncoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        variableContainingStartingPointNode,
                        variableContainingEdgeAtRandomPosition);
                }

                // close list is not empty check
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                // effect is new random starting point for following iteration
            }
        }

        public RandomizeListHeadsTypes Type;
        public string PatternElementName;
        public bool IsNode; // node|edge - only valid if GraphElements
        public string StartingPointNodeName; // only valid if IncidentEdges
        public bool IsIncoming; // only valid if IncidentEdges
    }

    /// <summary>
    /// Available types of PushSubpatternTask operations
    /// </summary>
    enum PushSubpatternTaskTypes
    {
        Subpattern,
        Alternative
    }

    /// <summary>
    /// Class representing "push a subpattern tasks to the open tasks stack" operation
    /// </summary>
    class PushSubpatternTask : SearchProgramOperation
    {
        public PushSubpatternTask(
            string subpatternName,
            string subpatternElementName,
            string[] connectionName,
            string[] patternElementBoundToConnectionName,
            bool[] patternElementBoundToConnectionIsNode,
            string negativePrefix)
        {
            Debug.Assert(connectionName.Length == patternElementBoundToConnectionName.Length
                && patternElementBoundToConnectionName.Length == patternElementBoundToConnectionIsNode.Length);
            Type = PushSubpatternTaskTypes.Subpattern;
            SubpatternName = subpatternName;
            SubpatternElementName = subpatternElementName;

            ConnectionName = connectionName;
            PatternElementBoundToConnectionName = patternElementBoundToConnectionName;
            PatternElementBoundToConnectionIsNode = patternElementBoundToConnectionIsNode;

            NegativePrefix = negativePrefix;
        }

        public PushSubpatternTask(
            string pathPrefix,
            string alternativeName,
            string rulePatternClassName,
            string[] connectionName,
            string[] patternElementBoundToConnectionName,
            bool[] patternElementBoundToConnectionIsNode,
            string negativePrefix)
        {
            Debug.Assert(connectionName.Length == patternElementBoundToConnectionName.Length
                && patternElementBoundToConnectionName.Length == patternElementBoundToConnectionIsNode.Length);
            Type = PushSubpatternTaskTypes.Alternative;
            PathPrefix = pathPrefix;
            AlternativeName = alternativeName;
            RulePatternClassName = rulePatternClassName;

            ConnectionName = connectionName;
            PatternElementBoundToConnectionName = patternElementBoundToConnectionName;
            PatternElementBoundToConnectionIsNode = patternElementBoundToConnectionIsNode;

            NegativePrefix = negativePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("PushSubpatternTask {0} ",
                Type==PushSubpatternTaskTypes.Alternative ? "Alternative" : "Subpattern");
            if (Type == PushSubpatternTaskTypes.Alternative) {
                builder.AppendFormat("{0} of {1} ", SubpatternElementName, SubpatternName);
            } else {
                builder.AppendFormat("{0}/{1} ", PathPrefix, AlternativeName);
            }
            builder.Append("with ");
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                builder.AppendFormat("{0} <- {1} isNode:{2} ",
                    ConnectionName[i], PatternElementBoundToConnectionName[i], 
                    PatternElementBoundToConnectionIsNode[i]);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode) {
                string type = Type==PushSubpatternTaskTypes.Alternative ? "alternative" : "subpattern";
                string what = Type==PushSubpatternTaskTypes.Alternative ? PathPrefix+AlternativeName : SubpatternElementName;
                sourceCode.AppendFrontFormat("// Push {0} matching task for {1}\n", type, what);
            }

            bool isAlternative = Type == PushSubpatternTaskTypes.Alternative;
            string variableContainingTask;
            if (isAlternative)
            {
                // create matching task for alternative
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeName);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix+AlternativeName, true);
                string alternativeCases = "patternGraph.alternatives[(int)" + RulePatternClassName + "."
                    + PathPrefix+"AltNums.@" + AlternativeName + "].alternativeCases";
                sourceCode.AppendFrontFormat("{0} {1} = new {0}(graph, {2}openTasks, {3});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativePrefix, alternativeCases);
            }
            else
            {
                // create matching task for subpattern
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false);
                sourceCode.AppendFrontFormat("{0} {1} = new {0}(graph, {2}openTasks);\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativePrefix);
            }
            
            // fill in connections
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                string variableContainingPatternElementToBeBound = 
                    NamesOfEntities.CandidateVariable(PatternElementBoundToConnectionName[i]);
                sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                    variableContainingTask, ConnectionName[i], variableContainingPatternElementToBeBound);
            }

            // push matching task to open tasks stack
            sourceCode.AppendFrontFormat("{0}openTasks.Push({1});\n", NegativePrefix, variableContainingTask);
        }

        public PushSubpatternTaskTypes Type;
        public string SubpatternName; // only valid if Type==Subpattern
        public string SubpatternElementName; // only valid if Type==Subpattern
        string PathPrefix; // only valid if Type==Alternative
        string AlternativeName; // only valid if Type==Alternative
        string RulePatternClassName; // only valid if Type==Alternative
        public string[] ConnectionName;
        public string[] PatternElementBoundToConnectionName;
        public bool[] PatternElementBoundToConnectionIsNode;
        public string NegativePrefix;
    }

    /// <summary>
    /// Class representing "pop a subpattern tasks from the open tasks stack" operation
    /// </summary>
    class PopSubpatternTask : SearchProgramOperation
    {
        public PopSubpatternTask(string subpatternElementName, string negativePrefix)
        {
            SubpatternElementName = subpatternElementName;
            NegativePrefix = negativePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("PopSubpatternTask {0}\n", SubpatternElementName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Pop subpattern matching task for {0}\n", SubpatternElementName);

            sourceCode.AppendFrontFormat("{0}openTasks.Pop();\n", NegativePrefix);
        }

        public string SubpatternElementName;
        public string NegativePrefix;
    }

    /// <summary>
    /// Class representing "execute open subpattern matching tasks" operation
    /// </summary>
    class MatchSubpatterns : SearchProgramOperation
    {
        public MatchSubpatterns(string negativePrefix)
        {
            NegativePrefix = negativePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("MatchSubpatterns {0}\n", NegativePrefix!="" ? "of "+NegativePrefix : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Match subpatterns {0}\n", 
                    NegativePrefix!="" ? "of "+NegativePrefix : "");

            sourceCode.AppendFrontFormat("{0}openTasks.Peek().myMatch({0}matchesList, {1}, negLevel);\n",
                NegativePrefix, NegativePrefix=="" ? "maxMatches - foundPartialMatches.Count" : "1");
        }

        public string NegativePrefix;
    }

    /// <summary>
    /// Class representing "create new matches list for following matches" operation
    /// </summary>
    class NewMatchesListForFollowingMatches : SearchProgramOperation
    {
        public NewMatchesListForFollowingMatches()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("NewMatchesListForFollowingMatches\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("if(matchesList==foundPartialMatches) {\n");
            sourceCode.AppendFront("    matchesList = new List<Stack<LGSPMatch>>();\n");
            sourceCode.AppendFront("} else {\n");
            sourceCode.AppendFront("    foreach(Stack<LGSPMatch> match in matchesList) {\n");
            sourceCode.AppendFront("        foundPartialMatches.Add(match);\n");
            sourceCode.AppendFront("    }\n");
            sourceCode.AppendFront("    matchesList.Clear();\n");
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing "initialize subpattern matching" operation
    /// </summary>
    class InitializeSubpatternMatching : SearchProgramOperation
    {
        public InitializeSubpatternMatching()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("InitializeSubpatternMatching\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("openTasks.Pop();\n");
            sourceCode.AppendFront("List<Stack<LGSPMatch>> matchesList = foundPartialMatches;\n");
            sourceCode.AppendFront("if(matchesList.Count!=0) throw new ApplicationException(); //debug assert\n");
        }
    }

    /// <summary>
    /// Class representing "finalize subpattern matching" operation
    /// </summary>
    class FinalizeSubpatternMatching : SearchProgramOperation
    {
        public FinalizeSubpatternMatching()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("FinalizeSubpatternMatching\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("openTasks.Push(this);\n");
        }
    }

    /// <summary>
    /// Class representing "initialize negative matching" operation
    /// </summary>
    class InitializeNegativeMatching : SearchProgramOperation
    {
        public InitializeNegativeMatching(
            bool containsSubpatterns,
            string negativePrefix,
            bool neverAboveMaxNegLevel)
        {
            SetupSubpatternMatching = containsSubpatterns;
            NegativePrefix = negativePrefix;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("InitializeNegativeMatching {0}\n", 
                SetupSubpatternMatching ? "SetupSubpatternMatching" : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("++negLevel;\n");
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {\n");
                sourceCode.Indent();
                sourceCode.AppendFront("graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            if (SetupSubpatternMatching)
            {
                sourceCode.AppendFrontFormat("Stack<LGSPSubpatternAction> {0}openTasks ="
                    + " new Stack<LGSPSubpatternAction>();\n", NegativePrefix);
                sourceCode.AppendFrontFormat("List<Stack<LGSPMatch>> {0}foundPartialMatches ="
                    + " new List<Stack<LGSPMatch>>();\n", NegativePrefix);
                sourceCode.AppendFrontFormat("List<Stack<LGSPMatch>> {0}matchesList ="
                    + " {0}foundPartialMatches;\n", NegativePrefix);
            }
        }

        public bool SetupSubpatternMatching;
        public string NegativePrefix;
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing "finalize subpattern matching" operation
    /// </summary>
    class FinalizeNegativeMatching : SearchProgramOperation
    {
        public FinalizeNegativeMatching(bool neverAboveMaxNegLevel)
        {
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("FinalizeNegativeMatching\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel > MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            sourceCode.AppendFront("--negLevel;\n");
        }

        public bool NeverAboveMaxNegLevel;
    }
}
