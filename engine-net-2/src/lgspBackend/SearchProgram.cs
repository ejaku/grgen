/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

//#define ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION

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
        /// Appends the given element to the search program operations list
        /// whose closing element until now was this element.
        /// Returns the new closing element - the given element.
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
        /// Insert the given element into the search program operations list
        /// between this and the succeeding element.
        /// Returns the element after this - the given element.
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
        /// bearing the search nesting/iteration structure.
        /// default: false (cause only few operations are search nesting operations)
        /// </summary>
        public virtual bool IsSearchNestingOperation()
        {
            return false;
        }

        /// <summary>
        /// returns the nested search operations list anchor
        /// null if list not created or IsSearchNestingOperation == false.
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

        protected string RulePatternClassName;
        protected List<string> NamesOfPatternGraphsOnPathToEnclosedPatternpath;
        public string Name;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Class representing the search program of a matching action, i.e. some test or rule
    /// The list forming concatenation field is used for adding missing preset search subprograms.
    /// </summary>
    class SearchProgramOfAction : SearchProgram
    {
        public SearchProgramOfAction(string rulePatternClassName,
            string patternName, string[] parameterTypes, string[] parameterNames, string name,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            bool containsSubpatterns, 
            string[] dispatchConditions, List<string> suffixedMatcherNames, List<string[]> arguments)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;

            PatternName = patternName;
            Parameters = "";
            for (int i = 0; i < parameterTypes.Length; ++i)
            {
                Parameters += ", " + parameterTypes[i] + " " + parameterNames[i];
            }
            SetupSubpatternMatching = containsSubpatterns;

            DispatchConditions = dispatchConditions;
            SuffixedMatcherNames = suffixedMatcherNames;
            Arguments = arguments;
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
            string matchType = RulePatternClassName + "." + NamesOfEntities.MatchInterfaceName(PatternName);
            string matchesType = "GRGEN_LIBGR.IMatchesExact<" + matchType + ">";
            sourceCode.AppendFrontFormat("public {0} {1}("
                    + "GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches{2})\n", matchesType, Name, Parameters);
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            if(Arguments!=null)
            {
                sourceCode.AppendFront("// maybe null dispatching\n");
                EmitMaybeNullDispatching(sourceCode, 0, 0);
            }

            sourceCode.AppendFront("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");
            sourceCode.AppendFront("matches.Clear();\n");
            sourceCode.AppendFront("int negLevel = 0;\n");
            
            if(NamesOfPatternGraphsOnPathToEnclosedPatternpath.Count > 0)
                sourceCode.AppendFront("bool searchPatternpath = false;\n");
            foreach (string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            if (SetupSubpatternMatching)
            {
                sourceCode.AppendFront("Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<"
                        + "GRGEN_LGSP.LGSPSubpatternAction>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<"
                        + "GRGEN_LIBGR.IMatch>>();\n");
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
            }

            OperationsList.Emit(sourceCode);

#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
            sourceCode.AppendFront("graph.EnsureEmptyFlags();\n");
#endif
            sourceCode.AppendFront("return matches;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            // emit search subprograms
            if (Next != null)
            {
                Next.Emit(sourceCode);
            }
        }

        private int EmitMaybeNullDispatching(SourceBuilder sourceCode, int conditionLevel, int emittedCounter)
        {
            if(conditionLevel<DispatchConditions.Length)
            {
                sourceCode.AppendFrontFormat("if({0}!=null) {{\n", DispatchConditions[conditionLevel]);
                sourceCode.Indent();
                emittedCounter = EmitMaybeNullDispatching(sourceCode, conditionLevel+1, emittedCounter);
                sourceCode.Unindent();
                sourceCode.AppendFront("} else {\n");
                sourceCode.Indent();
                emittedCounter = EmitMaybeNullDispatching(sourceCode, conditionLevel+1, emittedCounter);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else
            {
                if(emittedCounter>0) // first entry are we ourselves, don't call, just nop
                {
                    sourceCode.AppendFrontFormat("return {0}(actionEnv, maxMatches", SuffixedMatcherNames[emittedCounter]);
                    foreach(string argument in Arguments[emittedCounter]) {
                        sourceCode.AppendFormat(", {0}", argument);                        
                    }
                    sourceCode.Append(");\n");
                }
                ++emittedCounter;
            }
            return emittedCounter;
        }

        public string PatternName;
        public string Parameters;
        public bool SetupSubpatternMatching;

        string[] DispatchConditions;
        List<string> SuffixedMatcherNames; // for maybe null dispatcher
        List<string[]> Arguments; // for maybe null dispatcher
    }

    /// <summary>
    /// Class representing the search program of a subpattern
    /// </summary>
    class SearchProgramOfSubpattern : SearchProgram
    {
        public SearchProgramOfSubpattern(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
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

            sourceCode.AppendFront("public override void " + Name + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                + "int maxMatches, int negLevel)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");

            foreach (string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

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
        public SearchProgramOfAlternative(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
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

            sourceCode.AppendFront("public override void " + Name + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                + "int maxMatches, int negLevel)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");

            foreach (string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

            OperationsList.Emit(sourceCode);

            sourceCode.AppendFront("return;\n");
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }
    }

    /// <summary>
    /// Class representing the search program of an iterated pattern 
    /// </summary>
    class SearchProgramOfIterated : SearchProgram
    {
        public SearchProgramOfIterated(string rulePatternClassName,
            List<string> namesOfPatternGraphsOnPathToEnclosedPatternpath,
            string name)
        {
            RulePatternClassName = rulePatternClassName;
            NamesOfPatternGraphsOnPathToEnclosedPatternpath =
                namesOfPatternGraphsOnPathToEnclosedPatternpath;
            Name = name;
        }

        /// <summary>
        /// Dumps search program 
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("Search program {0} of iterated\n", Name);
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

            sourceCode.AppendFront("public override void " + Name + "(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, "
                + "int maxMatches, int negLevel)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            sourceCode.AppendFront("bool patternFound = false;\n");
            sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;\n");

            foreach (string graphsOnPath in NamesOfPatternGraphsOnPathToEnclosedPatternpath)
            {
                sourceCode.AppendFrontFormat("{0}.{1} {2} = null;\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(graphsOnPath),
                    NamesOfEntities.PatternpathMatch(graphsOnPath));
            }

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
    /// Class representing "draw variable from input parameters array" operation
    /// </summary>
    class ExtractVariable : SearchProgramOperation
    {
        public ExtractVariable(string varType, string varName)
        {
            VarType = varType;
            VarName = varName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("ExtractVariable " + VarName + ":" + VarType + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront(VarType + " " + NamesOfEntities.Variable(VarName) + " = (" + VarType + ")" + VarName + ";\n");
        }

        public string VarType;
        public string VarName;
    }

    /// <summary>
    /// Available entity types 
    /// </summary>
    enum EntityType
    {
        Node,
        Edge,
        Variable
    }

    /// <summary>
    /// Class representing "declare a def to be yielded to variable" operation
    /// </summary>
    class DeclareDefElement : SearchProgramOperation
    {
        public DeclareDefElement(EntityType entityType, string typeOfEntity, string nameOfEntity, string initialization)
        {
            Type = entityType;
            TypeOfEntity = typeOfEntity;
            NameOfEntity = nameOfEntity;
            Initialization = initialization;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("Declare def " + NamesOfEntities.ToString(Type) + " " + NameOfEntity + ":" + TypeOfEntity +  " = " + Initialization + "\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type == EntityType.Node)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = " + Initialization + ";\n");
            else if(Type == EntityType.Edge)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.CandidateVariable(NameOfEntity) + " = " + Initialization + ";\n");
            else //if(Type == EntityType.Variable)
                sourceCode.AppendFront(TypeOfEntity + " " + NamesOfEntities.Variable(NameOfEntity) + " = " + Initialization + ";\n");
        }

        public EntityType Type;
        public string TypeOfEntity;
        public string NameOfEntity;
        public string Initialization; // only valid if Variable, only not null if initialization given
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
        IncidentEdges, // incident edges
        StorageElements, // available elements of the storage variable
        StorageAttributeElements // available elements of the storage attribute
    }

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
            string storageName,
            string storageIterationType,
            bool isDict,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageName = storageName;
            StorageIterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string storageOwnerName,
            string storageOwnerTypeName,
            string storageAttributeName,
            string storageIterationType,
            bool isDict,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByIterationType.StorageAttributeElements);
            Type = type;
            PatternElementName = patternElementName;
            StorageOwnerName = storageOwnerName;
            StorageOwnerTypeName = storageOwnerTypeName;
            StorageAttributeName = storageAttributeName;
            StorageIterationType = storageIterationType;
            IsDict = isDict;
            IsNode = isNode;
        }

        public GetCandidateByIteration(
            GetCandidateByIterationType type,
            string patternElementName,
            string startingPointNodeName,
            IncidentEdgeType edgeType)
        {
            Debug.Assert(type == GetCandidateByIterationType.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            EdgeType = edgeType;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("GetCandidate ByIteration ");
            if (Type == GetCandidateByIterationType.GraphElements) {
                builder.Append("GraphElements ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else if(Type == GetCandidateByIterationType.StorageElements) {
                builder.Append("StorageElements ");
                builder.AppendFormat("on {0} from {1} node:{2} {3}\n",
                    PatternElementName, StorageName, IsNode, IsDict?"Dictionary":"List");
            } else if(Type == GetCandidateByIterationType.StorageAttributeElements) {
                builder.Append("StorageAttributeElements ");
                builder.AppendFormat("on {0} from {1}.{2} node:{3} {4}\n",
                    PatternElementName, StorageOwnerName, StorageAttributeName, IsNode, IsDict?"Dictionary":"List");
            } else { //Type==GetCandidateByIterationType.IncidentEdges
                builder.Append("IncidentEdges ");
                builder.AppendFormat("on {0} from {1} edge type:{2}\n",
                    PatternElementName, StartingPointNodeName, EdgeType.ToString());
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

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List", StorageName);
                }

                // emit loop header with variable containing dictionary entry
                string variableContainingStorage =
                    NamesOfEntities.Variable(StorageName);
                string storageIterationVariable = 
                    NamesOfEntities.CandidateIterationDictionaryOrListEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    StorageIterationType, storageIterationVariable, variableContainingStorage);
                
                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with key from dictionary entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == GetCandidateByIterationType.StorageAttributeElements)
            {
                if(sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Pick {0} {1} from {2} {3}.{4}\n",
                        IsNode ? "node" : "edge", PatternElementName, IsDict?"Dictionary":"List", StorageOwnerName, StorageAttributeName);
                }

                // emit loop header with variable containing dictionary entry
                string variableContainingStorage =
                    "((" + StorageOwnerTypeName + ")" + NamesOfEntities.CandidateVariable(StorageOwnerName) + ")." + StorageAttributeName;
                string storageIterationVariable =
                    NamesOfEntities.CandidateIterationDictionaryOrListEntry(PatternElementName);
                sourceCode.AppendFrontFormat("foreach({0} {1} in {2})\n",
                    StorageIterationType, storageIterationVariable, variableContainingStorage);

                // open loop
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                // emit candidate variable, initialized with key from dictionary entry
                string typeOfVariableContainingCandidate = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
                string variableContainingCandidate =
                    NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}{3};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, 
                    storageIterationVariable, IsDict?".Key":"");

                // emit loop body
                NestedOperationsList.Emit(sourceCode);

                // close loop
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else //Type==GetCandidateByIterationType.IncidentEdges
            {
                if (sourceCode.CommentSourceCode)
                {
                    sourceCode.AppendFrontFormat("// Extend {0} {1} from {2} \n",
                            EdgeType.ToString(), PatternElementName, StartingPointNodeName);
                }

                if (EdgeType != IncidentEdgeType.IncomingOrOutgoing)
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

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

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

                    // emit loop body
                    NestedOperationsList.Emit(sourceCode);

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

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public GetCandidateByIterationType Type;
        public bool IsNode; // node|edge - only available if GraphElements|StorageElements|StorageAttributeElements
        public bool IsDict; // Dictionary|List - only available if StorageElements|StorageAttributeElements
        public string StorageName; // only available if StorageElements
        public string StorageOwnerName; // only available if StorageAttributeElements
        public string StorageOwnerTypeName; // only available if StorageAttributeElements
        public string StorageAttributeName; // only available if StorageAttributeElements
        public string StorageIterationType; // only available if StorageElements|StorageAttributeElements
        public string StartingPointNodeName; // from pattern - only available if IncidentEdges
        public IncidentEdgeType EdgeType; // only available if IncidentEdges

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Available types of GetCandidateByDrawing operations
    /// </summary>
    enum GetCandidateByDrawingType
    {
        NodeFromEdge, // draw node from given edge
        MapWithStorage, // map element by storage map lookup
        FromInputs, // draw element from action inputs
        FromSubpatternConnections, // draw element from subpattern connections
        FromOtherElementForCast // draw element from other element for type cast
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
            ImplicitNodeType nodeType)
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType != ImplicitNodeType.TheOther);

            Type = type;
            PatternElementName = patternElementName;
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
        {
            Debug.Assert(type == GetCandidateByDrawingType.NodeFromEdge);
            Debug.Assert(nodeType == ImplicitNodeType.TheOther);

            Type = type;
            PatternElementName = patternElementName;
            PatternElementTypeName = patternElementTypeName;
            StartingPointEdgeName = startingPointEdgeName;
            TheOtherPatternElementName = theOtherPatternElementName;
            NodeType = nodeType;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromInputs || type == GetCandidateByDrawingType.FromSubpatternConnections);
            
            Type = type;
            PatternElementName = patternElementName;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string sourcePatternElementName,
            string storageName,
            string storageValueTypeName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.MapWithStorage);

            Type = type;
            PatternElementName = patternElementName;
            SourcePatternElementName = sourcePatternElementName;
            StorageName = storageName;
            StorageValueTypeName = storageValueTypeName;
            IsNode = isNode;
        }

        public GetCandidateByDrawing(
            GetCandidateByDrawingType type,
            string patternElementName,
            string sourcePatternElementName,
            bool isNode)
        {
            Debug.Assert(type == GetCandidateByDrawingType.FromOtherElementForCast);

            Type = type;
            PatternElementName = patternElementName;
            SourcePatternElementName = sourcePatternElementName;
            IsNode = isNode;
        }


        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("GetCandidate ByDrawing ");
            if(Type==GetCandidateByDrawingType.NodeFromEdge) {
                builder.Append("NodeFromEdge ");
                builder.AppendFormat("on {0} of {1} from {2} implicit node type:{3}\n",
                    PatternElementName, PatternElementTypeName, 
                    StartingPointEdgeName, NodeType.ToString());
            } if(Type==GetCandidateByDrawingType.MapWithStorage) {
                builder.Append("MapWithStorage ");
                builder.AppendFormat("on {0} by {1} from {2} node:{3}\n",
                    PatternElementName, SourcePatternElementName, 
                    StorageName, IsNode);
            } else if(Type==GetCandidateByDrawingType.FromInputs) {
                builder.Append("FromInputs ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else if(Type==GetCandidateByDrawingType.FromSubpatternConnections) {
                builder.Append("FromSubpatternConnections ");
                builder.AppendFormat("on {0} node:{1}\n",
                    PatternElementName, IsNode);
            } else { //if(Type==GetCandidateByDrawingType.FromOtherElementForCast)
                builder.Append("FromOtherElementForCast ");
                builder.AppendFormat("on {0} from {1} node:{2}\n",
                    PatternElementName, SourcePatternElementName, IsNode);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if(Type==GetCandidateByDrawingType.NodeFromEdge)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Implicit {0} {1} from {2} \n",
                            NodeType.ToString(), PatternElementName, StartingPointEdgeName);

                if (NodeType == ImplicitNodeType.Source || NodeType == ImplicitNodeType.Target)
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
                else if (NodeType == ImplicitNodeType.SourceOrTarget)
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
                    sourceCode.AppendFrontFormat("// Map {0} by {1}[{2}] \n",
                        PatternElementName, StorageName, SourcePatternElementName);

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
            else //if(Type == GetCandidateByDrawingType.FromOtherElementForCast)
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// Element {0} as type cast from other element {1} \n", 
                        PatternElementName, SourcePatternElementName);

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

        public GetCandidateByDrawingType Type;
        public string PatternElementTypeName; // only valid if NodeFromEdge
        public string TheOtherPatternElementName; // only valid if NodeFromEdge and TheOther
        public string StartingPointEdgeName; // from pattern - only valid if NodeFromEdge
        ImplicitNodeType NodeType; // only valid if NodeFromEdge
        public bool IsNode; // node|edge
        public string SourcePatternElementName; // only valid if MapWithStorage|FromOtherElementForCast
        public string StorageName; // only valid if MapWithStorage
        public string StorageValueTypeName; // only valid if MapWithStorage
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
            if (NestedOperationsList != null)
            {
                builder.Indent();
                NestedOperationsList.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
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

        public string PatternElementName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing nesting operation which executes the body once;
    /// needed for iterated, to prevent a return out of the matcher program
    /// circumventing the maxMatchesIterReached code which must get called if matching fails
    /// </summary>
    class ReturnPreventingDummyIteration : SearchProgramOperation
    {
        public ReturnPreventingDummyIteration()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFront("ReturnPreventingDummyIteration \n");

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
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// dummy iteration for iterated return prevention\n");

            // open loop
            sourceCode.AppendFront("do\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            // emit loop body
            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("} while(false);\n");
        }

        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public SearchProgramList NestedOperationsList;
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
        {
            Type = type;
            PatternElementName = patternElementName;
            if (type == CheckCandidateForTypeType.ByIsAllowedType) {
                RulePatternTypeName = rulePatternTypeNameOrTypeName;
            } else if (type == CheckCandidateForTypeType.ByIsMyType) {
                TypeName = rulePatternTypeNameOrTypeName;
            } else { // CheckCandidateForTypeType.ByTypeID
                Debug.Assert(false);
            }
            IsNode = isNode;
        }

        public CheckCandidateForType(
            CheckCandidateForTypeType type,
            string patternElementName,
            string[] typeIDs,
            bool isNode)
        {
            Debug.Assert(type == CheckCandidateForTypeType.ByTypeID);
            Type = type;
            PatternElementName = patternElementName;
            TypeIDs = (string[])typeIDs.Clone();
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
            } else { // Type == CheckCandidateForTypeType.ByTypeID
                builder.Append("ByTypeID ");
                builder.AppendFormat("on {0} ids:{1} node:{2}\n",
                    PatternElementName, string.Join(",", TypeIDs), IsNode);
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
                sourceCode.AppendFrontFormat("if(!{0}.{1}[{2}.lgspType.TypeID]) ",
                    RulePatternTypeName, isAllowedTypeArrayMemberOfRulePattern,
                    variableContainingCandidate);
            }
            else if (Type == CheckCandidateForTypeType.ByIsMyType)
            {
                sourceCode.AppendFrontFormat("if(!{0}.isMyType[{1}.lgspType.TypeID]) ",
                    TypeName, variableContainingCandidate);
            }
            else // Type == CheckCandidateForTypeType.ByTypeID)
            {
                sourceCode.AppendFront("if(");
                bool first = true;
                foreach (string typeID in TypeIDs)
                {
                    if (first) first = false;
                    else sourceCode.Append(" && ");

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

        public CheckCandidateForTypeType Type;

        public string RulePatternTypeName; // only valid if ByIsAllowedType
        public string TypeName; // only valid if ByIsMyType
        public string[] TypeIDs; // only valid if ByTypeID

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
        {
            Debug.Assert(connectednessType != CheckCandidateForConnectednessType.TheOther);

            // pattern element is the candidate to check, either node or edge
            PatternElementName = patternElementName;
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
        {
            Debug.Assert(connectednessType == CheckCandidateForConnectednessType.TheOther);

            // pattern element is the candidate to check, either node or edge
            PatternElementName = patternElementName;
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
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (ConnectednessType == CheckCandidateForConnectednessType.Source)
            {
                // emit check decision for is candidate connected to already found partial match, i.e. edge source equals node
                sourceCode.AppendFrontFormat("if({0}.lgspSource != {1}) ",
                    NamesOfEntities.CandidateVariable(PatternEdgeName),
                    NamesOfEntities.CandidateVariable(PatternNodeName));
            }
            else if (ConnectednessType == CheckCandidateForConnectednessType.Target)
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

        public string PatternNodeName;
        public string PatternEdgeName;
        public string TheOtherPatternNodeName; // only valid if ConnectednessType==TheOther
        public CheckCandidateForConnectednessType ConnectednessType;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other pattern element, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written/removed by AcceptCandidate/AbandonCandidate
    /// </summary>
    class CheckCandidateForIsomorphy : CheckCandidate
    {
        public CheckCandidateForIsomorphy(
            string patternElementName,
            List<string> namesOfPatternElementsToCheckAgainst,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NamesOfPatternElementsToCheckAgainst = namesOfPatternElementsToCheckAgainst;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphy ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2} ",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
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
                sourceCode.Append("(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? ");
            }
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel";
            sourceCode.AppendFormat("({0}.lgspFlags & {1}) != 0", variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Append(" : ");
                sourceCode.AppendFormat("graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1]"
                    + ".{0}.ContainsKey({1}))", IsNode ? "fst" : "snd", variableContainingCandidate);
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
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing "check whether candidate is not already mapped 
    ///   to some other (non-local) pattern element within this isomorphy space, to ensure required isomorphy" operation
    /// required graph element to pattern element mapping is written by AcceptCandidateGlobal/AbandonCandidateGlobal
    /// </summary>
    class CheckCandidateForIsomorphyGlobal : CheckCandidate
    {
        public CheckCandidateForIsomorphyGlobal(
            string patternElementName,
            List<string> globallyHomomorphElements,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            GloballyHomomorphElements = globallyHomomorphElements;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphyGlobal ");
            builder.AppendFormat("on {0} node:{1} ",
                PatternElementName, IsNode);
            if (GloballyHomomorphElements != null)
            {
                builder.Append("but accept if ");
                foreach (string name in GloballyHomomorphElements)
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
            // (in another subpattern to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Append("(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? ");
            }
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel";

            sourceCode.AppendFormat("({0}.lgspFlags & {1})=={1}",
                variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Append(" : ");
                sourceCode.AppendFormat("graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1]"
                    + ".{0}.ContainsKey({1}))", IsNode ? "fst" : "snd", variableContainingCandidate);
            }

            if (GloballyHomomorphElements != null)
            {
                // don't fail if candidate was globally matched by an element
                // it is allowed to be globally homomorph to 
                // (element from alternative case declared to be non-isomorph to element from enclosing pattern)
                foreach (string name in GloballyHomomorphElements)
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

        public List<string> GloballyHomomorphElements;
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
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
        {
            PatternElementName = patternElementName;
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
            // (previously on the pattern derivation path to another pattern element)
            // as this would cause a inter-pattern-homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string isMatchedBySomeBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";

            if (!Always) {
                sourceCode.Append("searchPatternpath && ");
            }

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

        public bool IsNode; // node|edge
        public bool Always; // have a look at searchPatternpath or search always
        string LastMatchAtPreviousNestingLevel;
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
        {
            PatternElementName = patternElementName;
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

        public string SourcePatternElementName;
        public string StorageName;
        public string StorageKeyTypeName;
        bool IsNode;
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
    /// Base class for search program operations
    /// filtering partial match by searching further patterns based on found one
    /// i.e. by negative or independent patterns (nac/pac)
    /// </summary>
    abstract class CheckPartialMatchByNegativeOrIndependent : CheckPartialMatch
    {
        public override bool IsSearchNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedSearchOperationsList()
        {
            return NestedOperationsList;
        }

        public string[] NeededElements;

        // search program of the negative/independent pattern
        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing "check whether the negative pattern applies" operation
    /// </summary>
    class CheckPartialMatchByNegative : CheckPartialMatchByNegativeOrIndependent
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
    }

    /// <summary>
    /// Class representing "check whether the independent pattern applies" operation
    /// </summary>
    class CheckPartialMatchByIndependent : CheckPartialMatchByNegativeOrIndependent
    {
        public CheckPartialMatchByIndependent(string[] neededElements)
        {
            NeededElements = neededElements;
        }

        public override void Dump(SourceBuilder builder)
        {
            Debug.Assert(CheckFailedOperations == null, "check independent without direct check failed code");
            // first dump local content
            builder.AppendFront("CheckPartialMatch ByIndependent with ");
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
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFront("// IndependentPattern \n");
            // currently needed because of multiple idptMapped backup variables with same name
            // todo: assign names to independents, mangle that name in, then remove block again
            // todo: remove (idpt)mapped backup variables altogether, then remove block again
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");

            //if(sourceCode.CommentSourceCode) reinsert when block is removed
            //    sourceCode.AppendFront("// IndependentPattern end\n");
        }

        // the corresponding check failed operation located after this one (in contrast to the check succeeded operation nested inside this one)
        public CheckContinueMatchingOfIndependentFailed CheckIndependentFailed;
    }

    /// <summary>
    /// Class representing "check whether the condition applies" operation
    /// </summary>
    class CheckPartialMatchByCondition : CheckPartialMatch
    {
        public CheckPartialMatchByCondition(
            string conditionExpression,
            string[] neededNodes,
            string[] neededEdges,
            string[] neededVariables)
        {
            ConditionExpression = conditionExpression;
            NeededVariables = neededVariables;
            
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
            builder.AppendFormat("{0} with ", ConditionExpression);
            foreach(string neededElement in NeededElements)
            {
                builder.Append(neededElement);
                builder.Append(" ");
            }
            foreach(string neededVar in NeededVariables)
            {
                builder.Append(neededVar);
                builder.Append(" ");
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
            // emit condition expression
            sourceCode.AppendFormat("!({0})", ConditionExpression);
            // close decision
            sourceCode.Append(") ");

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string ConditionExpression;
        public string[] NeededElements;
        public bool[] NeededElementIsNode;
        public string[] NeededVariables;
    }

    /// <summary>
    /// Class representing "check whether the subpatterns of the pattern were found" operation
    /// </summary>
    class CheckPartialMatchForSubpatternsFound : CheckPartialMatch
    {
        public CheckPartialMatchForSubpatternsFound(string negativeIndependentNamePrefix, bool isIterationBreaking)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
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
            sourceCode.AppendFrontFormat("if({0}matchesList.Count>0) ", NegativeIndependentNamePrefix);

            // emit check failed code
            sourceCode.Append("{\n");
            sourceCode.Indent();
            CheckFailedOperations.Emit(sourceCode);
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate checking succeded;
    /// writing isomorphy information to graph, for isomorphy checking later on
    /// (mapping graph element to pattern element)
    /// </summary>
    class AcceptCandidate : SearchProgramOperation
    {
        public AcceptCandidate(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }

            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel";
            sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
            sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else {\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} = graph.atNegLevelMatchedElements[negLevel - (int) "
                    + "GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{1}.ContainsKey({2}) ? 1U : 0U;\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.AppendFrontFormat("if({0} == 0) graph.atNegLevelMatchedElements[negLevel - (int) "
                    + "GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{1}.Add({2},{2});\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
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
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidateGlobal ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember = NamesOfEntities.VariableWithBackupOfIsMatchedGlobalBit(
                PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }

            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel";
            sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
            sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                variableContainingCandidate, isMatchedBit);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else {\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0} = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) "
                    + "GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{1}.ContainsKey({2}) ? 1U : 0U;\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.AppendFrontFormat("if({0} == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) "
                    + "GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{1}.Add({2},{2});\n",
                    variableContainingBackupOfMappedMember, IsNode ? "fst" : "snd", variableContainingCandidate);

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing operations to execute upon candidate gets accepted 
    /// into a complete match of its subpattern, locking candidate for patternpath checks later on
    /// </summary>
    class AcceptCandidatePatternpath : SearchProgramOperation
    {
        public AcceptCandidatePatternpath(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptCandidatePatternPath ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingBackupOfMappedMemberGlobalSome =
                NamesOfEntities.VariableWithBackupOfIsMatchedGlobalInSomePatternBit(PatternElementName, NegativeIndependentNamePrefix);
            sourceCode.AppendFrontFormat("uint {0};\n", variableContainingBackupOfMappedMemberGlobalSome);
            string isMatchedInSomePatternBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0} = {1}.lgspFlags & {2};\n",
                variableContainingBackupOfMappedMemberGlobalSome, variableContainingCandidate, isMatchedInSomePatternBit);
            sourceCode.AppendFrontFormat("{0}.lgspFlags |= {1};\n",
                variableContainingCandidate, isMatchedInSomePatternBit);
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing operations to execute upon iterated pattern was accepted (increase counter)
    /// </summary>
    class AcceptIterated : SearchProgramOperation
    {
        public AcceptIterated()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptIterated \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("// accept iterated instance match\n");
            sourceCode.AppendFront("++numMatchesIter;\n");
        }
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
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidate ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate =
                NamesOfEntities.CandidateVariable(PatternElementName);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }

            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel";
            sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                variableContainingCandidate, isMatchedBit, variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else { \n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                sourceCode.Indent();
                sourceCode.AppendFrontFormat(
                    "graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{0}.Remove({1});\n",
                    IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if top-level
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
            string negativeIndependentNamePrefix,
            bool isNode,
            bool neverAboveMaxNegLevel)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidateGlobal ");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingBackupOfMappedMember = NamesOfEntities.VariableWithBackupOfIsMatchedGlobalBit(
                PatternElementName, NegativeIndependentNamePrefix);
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
            }

            string isMatchedBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel";
            sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                variableContainingCandidate, isMatchedBit, variableContainingBackupOfMappedMember);

            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("} else { \n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("if({0} == 0) {{\n", variableContainingBackupOfMappedMember);
                sourceCode.Indent();
                sourceCode.AppendFrontFormat(
                    "graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].{0}.Remove({1});\n",
                    IsNode ? "fst" : "snd", variableContainingCandidate);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if positive
        public bool IsNode; // node|edge
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing operations undoing effects of patternpath candidate acceptance 
    /// into complete match of it's subpattern when performing the backtracking step (unlocks candidate)
    /// </summary>
    class AbandonCandidatePatternpath : SearchProgramOperation
    {
        public AbandonCandidatePatternpath(
            string patternElementName,
            string negativeIndependentNamePrefix,
            bool isNode)
        {
            PatternElementName = patternElementName;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonCandidatePatternpath");
            builder.AppendFormat("on {0} negNamePrefix:{1} node:{2}\n",
                PatternElementName, NegativeIndependentNamePrefix, IsNode);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
            string variableContainingBackupOfMappedMemberGlobalSome =
                NamesOfEntities.VariableWithBackupOfIsMatchedGlobalInSomePatternBit(PatternElementName, NegativeIndependentNamePrefix);
            string isMatchedInSomePatternBit = "(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN";
            sourceCode.AppendFrontFormat("{0}.lgspFlags = {0}.lgspFlags & ~({1}) | {2};\n",
                variableContainingCandidate, isMatchedInSomePatternBit, variableContainingBackupOfMappedMemberGlobalSome);
        }

        public string PatternElementName;
        public string NegativeIndependentNamePrefix; // "" if positive
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Class representing operations to execute upon iterated pattern was abandoned (decrease counter)
    /// </summary>
    class AbandonIterated : SearchProgramOperation
    {
        public AbandonIterated()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AbandonIterated \n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("--numMatchesIter;\n");
        }
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern without contained subpatterns was matched
    /// </summary>
    class PositivePatternWithoutSubpatternsMatched : SearchProgramOperation
    {
        public PositivePatternWithoutSubpatternsMatched(
            string rulePatternClassName,
            string patternName)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
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
            sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));

            // emit match building operations
            MatchBuildingOperations.Emit(sourceCode);

            sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");
        }

        string RulePatternClassName;
        string PatternName;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a subpattern without contained subpatterns was matched (as the last element of the search)
    /// </summary>
    class LeafSubpatternMatched : SearchProgramOperation
    {
        public LeafSubpatternMatched(
            string rulePatternClassName, 
            string patternName,
            bool isIteratedNullMatch)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            IsIteratedNullMatch = isIteratedNullMatch;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LeafSubpatternMatched {0}\n", IsIteratedNullMatch ? "IteratedNullMatch " : "");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();\n");
            sourceCode.AppendFront("foundPartialMatches.Add(currentFoundPartialMatch);\n");

            sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
            if (IsIteratedNullMatch) {
                sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");
            } else {
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
            }
            sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");
        }

        string RulePatternClassName;
        string PatternName;
        bool IsIteratedNullMatch;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of PatternAndSubpatternsMatched operations
    /// </summary>
    enum PatternAndSubpatternsMatchedType
    {
        Action,
        Iterated,
        IteratedNullMatch,
        SubpatternOrAlternative
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a positive pattern was matched and all of it's subpatterns were matched at least once
    /// </summary>
    class PatternAndSubpatternsMatched : SearchProgramOperation
    {
        public PatternAndSubpatternsMatched(
            string rulePatternClassName,
            string patternName,
            PatternAndSubpatternsMatchedType type)
        {
            RulePatternClassName = rulePatternClassName;
            PatternName = patternName;
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PatternAndSubpatternsMatched ");
            switch (Type)
            {
                case PatternAndSubpatternsMatchedType.Action: builder.Append("Action \n"); break;
                case PatternAndSubpatternsMatchedType.Iterated: builder.Append("Iterated \n"); break;
                case PatternAndSubpatternsMatchedType.IteratedNullMatch: builder.Append("IteratedNullMatch \n"); break;
                case PatternAndSubpatternsMatchedType.SubpatternOrAlternative: builder.Append("SubpatternOrAlternative \n"); break;
                default: builder.Append("INTERNAL ERROR\n"); break;
            }

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type==PatternAndSubpatternsMatchedType.Iterated)
            {
                sourceCode.AppendFront("patternFound = true;\n");
            }

            if (Type!=PatternAndSubpatternsMatchedType.Action)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0}.{1} match = new {0}.{1}();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                if (Type == PatternAndSubpatternsMatchedType.IteratedNullMatch) {
                    sourceCode.AppendFront("match._isNullMatch = true; // null match of iterated pattern\n");
                }

                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("currentFoundPartialMatch.Push(match);\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else // top-level pattern with subpatterns/alternatives
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it\n");
                sourceCode.AppendFront("foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                sourceCode.AppendFrontFormat("{0}.{1} match = matches.GetNextUnfilledPosition();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternName));
                MatchBuildingOperations.Emit(sourceCode); // emit match building operations
                sourceCode.AppendFront("matches.PositionWasFilledFixIt();\n");

                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                sourceCode.AppendFront("matchesList.Clear();\n");
            }
        }

        string RulePatternClassName;
        string PatternName;
        PatternAndSubpatternsMatchedType Type;

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Available types of NegativeIndependentPatternMatched operations
    /// </summary>
    enum NegativeIndependentPatternMatchedType
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
        public NegativePatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("NegativePatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern found\n");
            }
            else
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// negative pattern with contained subpatterns found\n");

                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public NegativeIndependentPatternMatchedType Type;
        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a independent pattern was matched
    /// </summary>
    class IndependentPatternMatched : SearchProgramOperation
    {
        public IndependentPatternMatched(
            NegativeIndependentPatternMatchedType type,
            string negativeIndependentNamePrefix)
        {
            Type = type;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("IndependentPatternMatched ");
            builder.Append(Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns ?
                "WithoutSubpatterns\n" : "ContainingSubpatterns\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == NegativeIndependentPatternMatchedType.WithoutSubpatterns)
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern found\n");
            }
            else
            {
                if (sourceCode.CommentSourceCode)
                    sourceCode.AppendFront("// independent pattern with contained subpatterns found\n");
                sourceCode.AppendFrontFormat("Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = {0}matchesList[0];\n",
                    NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("{0}matchesList.Clear();\n", NegativeIndependentNamePrefix);
            }
        }

        public NegativeIndependentPatternMatchedType Type;
        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Available types of BuildMatchObject operations
    /// </summary>
    enum BuildMatchObjectType
    {
        Node,        // build match object with match for node 
        Edge,        // build match object with match for edge
        Variable,    // build match object with match for variable
        Subpattern,  // build match object with match for subpattern
        Iteration,   // build match object with match for iteration
        Alternative, // build match object with match for alternative
        Independent  // build match object with match for independent
    }

    /// <summary>
    /// Class representing "pattern was matched, now build match object" operation
    /// </summary>
    class BuildMatchObject : SearchProgramOperation
    {
        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementType,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string pathPrefixForEnum,
            string matchObjectName,
            int numSubpatterns)
        {
            Debug.Assert(type != BuildMatchObjectType.Independent);
            Type = type;
            PatternElementType = patternElementType;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            PathPrefixForEnum = pathPrefixForEnum;
            MatchObjectName = matchObjectName;
            NumSubpatterns = numSubpatterns; // only valid if type == Alternative
        }

        public BuildMatchObject(
            BuildMatchObjectType type,
            string patternElementUnprefixedName,
            string patternElementName,
            string rulePatternClassName,
            string matchObjectName)
        {
            Debug.Assert(type == BuildMatchObjectType.Independent);
            Type = type;
            PatternElementUnprefixedName = patternElementUnprefixedName;
            PatternElementName = patternElementName;
            RulePatternClassName = rulePatternClassName;
            MatchObjectName = matchObjectName;
        }

        public override void Dump(SourceBuilder builder)
        {
            string typeDescr;
            switch(Type)
            {
                case BuildMatchObjectType.Node:        typeDescr = "Node";        break;
                case BuildMatchObjectType.Edge:        typeDescr = "Edge";        break;
                case BuildMatchObjectType.Variable:    typeDescr = "Variable";    break;
                case BuildMatchObjectType.Subpattern:  typeDescr = "Subpattern";  break;
                case BuildMatchObjectType.Iteration:   typeDescr = "Iteration";   break;
                case BuildMatchObjectType.Alternative: typeDescr = "Alternative"; break;
                case BuildMatchObjectType.Independent: typeDescr = "Independent"; break;
                default:                               typeDescr = ">>UNKNOWN<<"; break;
            }

            builder.AppendFrontFormat("BuildMatchObject {0} name {1} with {2} within {3}\n",
                typeDescr, MatchObjectName, PatternElementName, RulePatternClassName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string matchName = NamesOfEntities.MatchName(PatternElementUnprefixedName, Type);
            if (Type == BuildMatchObjectType.Node || Type == BuildMatchObjectType.Edge)
            {
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableContainingCandidate);
            }
            else if(Type == BuildMatchObjectType.Variable)
            {
                string variableName = NamesOfEntities.Variable(PatternElementName);
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, variableName);
            }
            else if(Type == BuildMatchObjectType.Subpattern)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = (@{2})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, PatternElementType);
                sourceCode.AppendFrontFormat("{0}._{1}._matchOfEnclosingPattern = {0};\n",
                    MatchObjectName, matchName);
            }
            else if(Type == BuildMatchObjectType.Iteration)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = new GRGEN_LGSP.LGSPMatchesList<{2}.{3}, {2}.{4}>(null);\n",
                    MatchObjectName, matchName,
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType), NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is {0}.{1}) ", 
                    RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.Append("{\n");
                sourceCode.Indent();
                sourceCode.AppendFrontFormat("{0}.{1} cfpm = ({0}.{1})currentFoundPartialMatch.Pop();\n",
                    RulePatternClassName, NamesOfEntities.MatchClassName(PatternElementType));
                sourceCode.AppendFront("if(cfpm.IsNullMatch) break;\n");
                sourceCode.AppendFrontFormat("cfpm.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName);
                sourceCode.AppendFrontFormat("{0}._{1}.Add(cfpm);\n",
                    MatchObjectName, matchName);
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else if(Type == BuildMatchObjectType.Alternative)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = ({2}.{3})currentFoundPartialMatch.Pop();\n",
                    MatchObjectName, matchName, RulePatternClassName, NamesOfEntities.MatchInterfaceName(PatternElementType));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
            else //if (Type == BuildMatchObjectType.Independent)
            {
                sourceCode.AppendFrontFormat("{0}._{1} = {2};\n",
                    MatchObjectName, matchName, NamesOfEntities.MatchedIndependentVariable(PatternElementName));
                sourceCode.AppendFrontFormat("{0} = new {1}({0});\n",
                    NamesOfEntities.MatchedIndependentVariable(PatternElementName),
                    RulePatternClassName + "." + NamesOfEntities.MatchClassName(PatternElementName));
                sourceCode.AppendFrontFormat("{0}._{1}.SetMatchOfEnclosingPattern({0});\n",
                    MatchObjectName, matchName);
            }
        }

        public BuildMatchObjectType Type;
        public string PatternElementType;
        public string PatternElementUnprefixedName;
        public string PatternElementName;
        public string RulePatternClassName;
        public string PathPrefixForEnum;
        public string MatchObjectName;
        public int NumSubpatterns;
    }

    /// <summary>
    /// Class representing implicit yield assignment operations,
    /// to bubble up values from nested patterns and subpatterns to the containing pattern
    /// </summary>
    class BubbleUpYieldAssignment : SearchProgramOperation
    {
        public BubbleUpYieldAssignment(
            EntityType type,
            string targetPatternElementName,
            string nestedMatchObjectName,
            string sourcePatternElementUnprefixedName
            )
        {
            Type = type;
            TargetPatternElementName = targetPatternElementName;
            NestedMatchObjectName = nestedMatchObjectName;
            SourcePatternElementUnprefixedName = sourcePatternElementUnprefixedName;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("BubbleUpYieldAssignemt {0} {1} = {2}.{3}\n",
                NamesOfEntities.ToString(Type), TargetPatternElementName, NestedMatchObjectName, SourcePatternElementUnprefixedName);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            string targetPatternElement = Type==EntityType.Variable ? NamesOfEntities.Variable(TargetPatternElementName) : NamesOfEntities.CandidateVariable(TargetPatternElementName);
            string sourcePatternElement = NamesOfEntities.MatchName(SourcePatternElementUnprefixedName, Type);
            sourceCode.AppendFrontFormat("{0} = {1}._{2};\n",
                targetPatternElement, NestedMatchObjectName, sourcePatternElement);
        }

        EntityType Type;
        string TargetPatternElementName;
        string NestedMatchObjectName;
        string SourcePatternElementUnprefixedName;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested iterateds
    /// </summary>
    class BubbleUpYieldIterated : SearchProgramOperation
    {
        public BubbleUpYieldIterated(string nestedMatchObjectName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0}\n", NestedMatchObjectName);

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
            sourceCode.AppendFrontFormat("if({0}.Count>0) {{\n", NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

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

        string NestedMatchObjectName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around explicit yield accumulate up assignments for nested iterateds
    /// </summary>
    class AccumulateUpYieldIterated : SearchProgramOperation
    {
        public AccumulateUpYieldIterated(string nestedMatchObjectName, string iteratedMatchTypeName, string helperMatchName)
        {
            NestedMatchObjectName = nestedMatchObjectName;
            IteratedMatchTypeName = iteratedMatchTypeName;
            HelperMatchName = helperMatchName;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldIterated on {0} type {1}\n", 
                NestedMatchObjectName, IteratedMatchTypeName);

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
            sourceCode.AppendFrontFormat("foreach({0} {1} in {2}) {{\n", 
                IteratedMatchTypeName, HelperMatchName, NestedMatchObjectName);
            sourceCode.Indent();

            NestedOperationsList.Emit(sourceCode);

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

        string NestedMatchObjectName;
        string IteratedMatchTypeName;
        string HelperMatchName;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing a block around implicit yield bubble up assignments for nested alternatives
    /// </summary>
    class BubbleUpYieldAlternativeCase : SearchProgramOperation
    {
        public BubbleUpYieldAlternativeCase(string matchObjectName, string nestedMatchObjectName,
            string alternativeCaseMatchTypeName, string helperMatchName, bool first)
        {
            MatchObjectName = matchObjectName;
            NestedMatchObjectName = nestedMatchObjectName;
            AlternativeCaseMatchTypeName = alternativeCaseMatchTypeName;
            HelperMatchName = helperMatchName;
            First = first;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("BubbleUpYieldAlternativeCase on {0}.{1} case match type {2} {3}\n",
                MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName, First ? "first" : "");

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
            sourceCode.AppendFrontFormat("{0}if({1}.{2} is {3}) {{\n",
                First ? "" : "else ", MatchObjectName, NestedMatchObjectName, AlternativeCaseMatchTypeName);
            sourceCode.Indent();
            sourceCode.AppendFrontFormat("{0} {1} = ({0}){2}.{3};\n",
                AlternativeCaseMatchTypeName, HelperMatchName, MatchObjectName, NestedMatchObjectName);

            NestedOperationsList.Emit(sourceCode);

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

        string MatchObjectName;
        string NestedMatchObjectName;
        string AlternativeCaseMatchTypeName;
        string HelperMatchName;
        bool First;

        public SearchProgramList NestedOperationsList;
    }

    /// <summary>
    /// Class representing (explicit) local yield assignment operations
    /// </summary>
    class LocalYieldAssignment : SearchProgramOperation
    {
        public LocalYieldAssignment(string assignment)
        {
            Assignment = assignment;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("LocalYieldAssignment {0}\n", Assignment);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("{0};\n", Assignment);
        }

        string Assignment;
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
            IncidentEdgeType incidentType)
        {
            Debug.Assert(type == AdjustListHeadsTypes.IncidentEdges);
            Type = type;
            PatternElementName = patternElementName;
            StartingPointNodeName = startingPointNodeName;
            IncidentType = incidentType;
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
                builder.AppendFormat("on {0} from:{1} incident type:{2}\n",
                    PatternElementName, StartingPointNodeName, IncidentType.ToString());
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
                if (IncidentType == IncidentEdgeType.Incoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName),
                        NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else if (IncidentType == IncidentEdgeType.Outgoing)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName),
                        NamesOfEntities.CandidateVariable(PatternElementName));
                }
                else // IncidentType == IncidentEdgeType.IncomingOrOutgoing
                {
                    sourceCode.AppendFrontFormat("if({0}==0)",
                        NamesOfEntities.DirectionRunCounterVariable(PatternElementName));
                    sourceCode.Append(" {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                         NamesOfEntities.CandidateVariable(StartingPointNodeName),
                         NamesOfEntities.CandidateVariable(PatternElementName));
                    sourceCode.Unindent();
                    sourceCode.AppendFront("} else {\n");
                    sourceCode.Indent();
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName),
                        NamesOfEntities.CandidateVariable(PatternElementName));
                    sourceCode.Unindent();
                    sourceCode.AppendFront("}\n");
                }
            }
        }

        public AdjustListHeadsTypes Type;
        public string PatternElementName;
        public bool IsNode; // node|edge - only valid if GraphElements
        public string StartingPointNodeName; // only valid if IncidentEdges
        public IncidentEdgeType IncidentType; // only valid if IncidentEdges
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
    /// available types of check continue matching maximum matches reached operations (at which level/where nested inside does the check occur?)
    /// </summary>
    enum CheckMaximumMatchesType
    {
        Action,
        Subpattern,
        Iterated
    }

    /// <summary>
    /// Class representing "check if matching process is to be aborted because
    /// the maximum number of matches has been reached" operation
    /// listHeadAdjustment==false prevents listentrick
    /// </summary>
    class CheckContinueMatchingMaximumMatchesReached : CheckContinueMatching
    {
        public CheckContinueMatchingMaximumMatchesReached(CheckMaximumMatchesType type, bool listHeadAdjustment)
        {
            Type = type;
            ListHeadAdjustment = listHeadAdjustment;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            if (Type == CheckMaximumMatchesType.Action) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Action level ");
            } else if (Type == CheckMaximumMatchesType.Subpattern) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached at Subpattern level ");
            } else if (Type == CheckMaximumMatchesType.Iterated) {
                builder.AppendFront("CheckContinueMatching MaximumMatchesReached if Iterated ");
            }
            if (ListHeadAdjustment) builder.Append("ListHeadAdjustment ");
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

            if (Type == CheckMaximumMatchesType.Action) {
                sourceCode.AppendFront("if(maxMatches > 0 && matches.Count >= maxMatches)\n");
            } else if (Type == CheckMaximumMatchesType.Subpattern) {
                sourceCode.AppendFront("if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)\n");
            } else if (Type == CheckMaximumMatchesType.Iterated) {
                sourceCode.AppendFront("if(true) // as soon as there's a match, it's enough for iterated\n");
            }
    
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public CheckMaximumMatchesType Type;
        public bool ListHeadAdjustment;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort negative matching process always succeeds
    /// </summary>
    class CheckContinueMatchingOfNegativeFailed : CheckContinueMatching
    {
        public CheckContinueMatchingOfNegativeFailed(bool isIterationBreaking)
        {
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfNegativeFailed ");
            builder.Append(IsIterationBreaking ? "IterationBreaking\n" : "\n");

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
            if(IsIterationBreaking)
                sourceCode.AppendFront("breakIteration = true;\n");

            CheckFailedOperations.Emit(sourceCode);
        }

        public bool IsIterationBreaking;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort independent matching process always succeeds
    /// </summary>
    class CheckContinueMatchingOfIndependentFailed : CheckContinueMatching
    {
        public CheckContinueMatchingOfIndependentFailed(CheckPartialMatchByIndependent checkIndependent, bool isIterationBreaking)
        {
            CheckIndependent = checkIndependent;
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfIndependentFailed ");
            builder.Append(IsIterationBreaking ? "IterationBreaking\n" : "\n");
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
            if(IsIterationBreaking)
                sourceCode.AppendFront("breakIteration = true;\n");

            CheckFailedOperations.Emit(sourceCode);
        }

        // the independent which failed
        public CheckPartialMatchByIndependent CheckIndependent;
        public bool IsIterationBreaking;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always fail.
    /// Check of abort independent matching process always fails
    /// </summary>
    class CheckContinueMatchingOfIndependentSucceeded : CheckContinueMatching
    {
        public CheckContinueMatchingOfIndependentSucceeded()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching OfIndependentSucceeded \n");
            // then operations for case check failed .. wording a bit rotten
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
    /// Class representing "check if matching process is to be continued with iterated pattern null match" operation
    /// </summary>
    class CheckContinueMatchingIteratedPatternNonNullMatchFound : CheckContinueMatching
    {
        public CheckContinueMatchingIteratedPatternNonNullMatchFound(bool isIterationBreaking)
        {
            IsIterationBreaking = isIterationBreaking;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFrontFormat("CheckContinueMatching IteratedPatternFound {0}\n",
                IsIterationBreaking ? "IterationBreaking" : "");
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
                sourceCode.AppendFront("// Check whether the iterated pattern null match was found\n");

            sourceCode.Append("maxMatchesIterReached:\n");
            if(IsIterationBreaking)
                sourceCode.AppendFront("if(!patternFound && numMatchesIter>=minMatchesIter && !breakIteration)\n");
            else
                sourceCode.AppendFront("if(!patternFound && numMatchesIter>=minMatchesIter)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool IsIterationBreaking;
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
#if ENSURE_FLAGS_IN_GRAPH_ARE_EMPTY_AT_LEAVING_TOP_LEVEL_MATCHING_ACTION
                    sourceCode.AppendFront("graph.EnsureEmptyFlags();\n");
#endif
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
            builder.AppendFront("GotoLabel ");
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
                string typeOfVariableContainingElementAtRandomPosition = "GRGEN_LGSP."
                    + (IsNode ? "LGSPNode" : "LGSPEdge");
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
                    IsIncoming ? "lgspInhead" : "lgspOuthead";
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
                    IsIncoming ? "lgspInNext" : "lgspOutNext";
                sourceCode.AppendFrontFormat("for(GRGEN_LGSP.LGSPEdge edge = {0}.{1}; edge!={0}.{1}; edge=edge.{2}) ++{3};\n",
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
                sourceCode.AppendFrontFormat("GRGEN_LGSP.LGSPEdge {0}",
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
    /// Available types of PushSubpatternTask and PopSubpatternTask operations
    /// </summary>
    enum PushAndPopSubpatternTaskTypes
    {
        Subpattern,
        Alternative,
        Iterated
    }

    /// <summary>
    /// Class representing "push a subpattern tasks to the open tasks stack" operation
    /// </summary>
    class PushSubpatternTask : SearchProgramOperation
    {
        public PushSubpatternTask(
            PushAndPopSubpatternTaskTypes type,
            string subpatternName,
            string subpatternElementName,
            string[] connectionName,
            string[] argumentExpressions,
            string negativeIndependentNamePrefix,
            string searchPatternpath,
            string matchOfNestingPattern,
            string lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(connectionName.Length == argumentExpressions.Length);
            Debug.Assert(type == PushAndPopSubpatternTaskTypes.Subpattern);
            Type = type;
            SubpatternName = subpatternName;
            SubpatternElementName = subpatternElementName;

            ConnectionName = connectionName;
            ArgumentExpressions = argumentExpressions;

            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;

            SearchPatternpath = searchPatternpath;
            MatchOfNestingPattern = matchOfNestingPattern;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
        }

        public PushSubpatternTask(
            PushAndPopSubpatternTaskTypes type,
            string pathPrefix,
            string alternativeOrIteratedName,
            string rulePatternClassName,
            string[] connectionName,
            string[] argumentExpressions,
            string negativeIndependentNamePrefix,
            string searchPatternpath,
            string matchOfNestingPattern,
            string lastMatchAtPreviousNestingLevel)
        {
            Debug.Assert(connectionName.Length == argumentExpressions.Length);
            Debug.Assert(type == PushAndPopSubpatternTaskTypes.Alternative
                || type == PushAndPopSubpatternTaskTypes.Iterated);
            Type = type;
            PathPrefix = pathPrefix;
            AlternativeOrIteratedName = alternativeOrIteratedName;
            RulePatternClassName = rulePatternClassName;

            ConnectionName = connectionName;
            ArgumentExpressions = argumentExpressions;

            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;

            SearchPatternpath = searchPatternpath;
            MatchOfNestingPattern = matchOfNestingPattern;
            LastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            if(Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFront("PushSubpatternTask Subpattern ");
            } else if(Type == PushAndPopSubpatternTaskTypes.Alternative) {
                builder.AppendFront("PushSubpatternTask Alternative ");
            } else { // Type==PushAndPopSubpatternTaskTypes.Iterated
                builder.AppendFront("PushSubpatternTask Iterated ");
            }

            if(Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFormat("{0} of {1} ", SubpatternElementName, SubpatternName);
            } else {
                builder.AppendFormat("{0}/{1} ", PathPrefix, AlternativeOrIteratedName);
            }

            builder.Append("with ");
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                builder.AppendFormat("{0} <- {1} ",
                    ConnectionName[i], ArgumentExpressions[i]);
            }
            builder.Append("\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode) {
                if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                    sourceCode.AppendFrontFormat("// Push subpattern matching task for {0}\n", SubpatternElementName);
                } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                    sourceCode.AppendFrontFormat("// Push alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Push iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                }
            }

            string variableContainingTask;
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern)
            {
                // create matching task for subpattern
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks);\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix);
            }
            else if (Type == PushAndPopSubpatternTaskTypes.Alternative)
            {
                // create matching task for alternative
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, true, false);
                string patternGraphPath = RulePatternClassName + ".Instance.";
                if(RulePatternClassName.Substring(RulePatternClassName.IndexOf('_')+1) == PathPrefix.Substring(0, PathPrefix.Length-1))
                    patternGraphPath += "patternGraph";
                else
                    patternGraphPath += PathPrefix.Substring(0, PathPrefix.Length - 1);
                string alternativeCases = patternGraphPath + ".alternatives[(int)" + RulePatternClassName + "."
                    + PathPrefix + "AltNums.@" + AlternativeOrIteratedName + "].alternativeCases";
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks, {3});\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix, alternativeCases);
            }
            else // if(Type == PushAndPopSubpatternTaskTypes.Iterated)
            {
                // create matching task for iterated
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                string typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, false, true);
                sourceCode.AppendFrontFormat("{0} {1} = {0}.getNewTask(actionEnv, {2}openTasks);\n",
                    typeOfVariableContainingTask, variableContainingTask, NegativeIndependentNamePrefix);
            }
            
            // fill in connections
            for (int i = 0; i < ConnectionName.Length; ++i)
            {
                sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                    variableContainingTask, ConnectionName[i], ArgumentExpressions[i]);
            }

            // fill in values needed for patternpath handling
            sourceCode.AppendFrontFormat("{0}.searchPatternpath = {1};\n",
                variableContainingTask, SearchPatternpath);
            sourceCode.AppendFrontFormat("{0}.matchOfNestingPattern = {1};\n", 
                variableContainingTask, MatchOfNestingPattern);
            sourceCode.AppendFrontFormat("{0}.lastMatchAtPreviousNestingLevel = {1};\n",
                variableContainingTask, LastMatchAtPreviousNestingLevel);

            // push matching task to open tasks stack
            sourceCode.AppendFrontFormat("{0}openTasks.Push({1});\n", NegativeIndependentNamePrefix, variableContainingTask);
        }

        public PushAndPopSubpatternTaskTypes Type;
        public string SubpatternName; // only valid if Type==Subpattern
        public string SubpatternElementName; // only valid if Type==Subpattern
        string PathPrefix; // only valid if Type==Alternative|Iterated
        string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        string RulePatternClassName; // only valid if Type==Alternative|Iterated
        public string[] ConnectionName;
        public string[] ArgumentExpressions;
        public string NegativeIndependentNamePrefix;
        public string SearchPatternpath;
        public string MatchOfNestingPattern;
        public string LastMatchAtPreviousNestingLevel;
    }

    /// <summary>
    /// Class representing "pop a subpattern tasks from the open tasks stack" operation
    /// </summary>
    class PopSubpatternTask : SearchProgramOperation
    {
        public PopSubpatternTask(string negativeIndependentNamePrefix, PushAndPopSubpatternTaskTypes type,
            string subpatternOrAlternativeOrIteratedName, string subpatternElementNameOrPathPrefix)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            Type = type;
            if (type == PushAndPopSubpatternTaskTypes.Subpattern) {
                SubpatternName = subpatternOrAlternativeOrIteratedName;
                SubpatternElementName = subpatternElementNameOrPathPrefix;
            } else if (type == PushAndPopSubpatternTaskTypes.Alternative) {
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            } else { // if (type == PushAndPopSubpatternTaskTypes.Iterated)
                AlternativeOrIteratedName = subpatternOrAlternativeOrIteratedName;
                PathPrefix = subpatternElementNameOrPathPrefix;
            }
        }

        public override void Dump(SourceBuilder builder)
        {
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                builder.AppendFrontFormat("PopSubpatternTask Subpattern {0}\n", SubpatternElementName);
            } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                builder.AppendFrontFormat("PopSubpatternTask Alternative {0}\n", AlternativeOrIteratedName);
            } else { // Type==PushAndPopSubpatternTaskTypes.Iterated
                builder.AppendFrontFormat("PopSubpatternTask Iterated {0}\n", AlternativeOrIteratedName);
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode) {
                if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                    sourceCode.AppendFrontFormat("// Pop subpattern matching task for {0}\n", SubpatternElementName);
                } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                    sourceCode.AppendFrontFormat("// Pop alternative matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                    sourceCode.AppendFrontFormat("// Pop iterated matching task for {0}\n", PathPrefix + AlternativeOrIteratedName);
                }
            }

            sourceCode.AppendFrontFormat("{0}openTasks.Pop();\n", NegativeIndependentNamePrefix);

            string variableContainingTask;
            string typeOfVariableContainingTask;
            if (Type == PushAndPopSubpatternTaskTypes.Subpattern) {
                variableContainingTask = NamesOfEntities.TaskVariable(SubpatternElementName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(SubpatternName, false, false);
            } else if (Type == PushAndPopSubpatternTaskTypes.Alternative) {
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, true, false);
            } else { // if(Type==PushAndPopSubpatternTaskTypes.Iterated)
                variableContainingTask = NamesOfEntities.TaskVariable(AlternativeOrIteratedName, NegativeIndependentNamePrefix);
                typeOfVariableContainingTask = NamesOfEntities.TypeOfTaskVariable(PathPrefix + AlternativeOrIteratedName, false, true);
            }
            sourceCode.AppendFrontFormat("{0}.releaseTask({1});\n", typeOfVariableContainingTask, variableContainingTask);
        }

        public PushAndPopSubpatternTaskTypes Type;
        public string SubpatternName; // only valid if Type==Subpattern
        public string SubpatternElementName; // only valid if Type==Subpattern
        public string PathPrefix; // only valid if Type==Alternative|Iterated
        public string AlternativeOrIteratedName; // only valid if Type==Alternative|Iterated
        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class representing "execute open subpattern matching tasks" operation
    /// </summary>
    class MatchSubpatterns : SearchProgramOperation
    {
        public MatchSubpatterns(string negativeIndependentNamePrefix)
        {
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("MatchSubpatterns {0}\n", NegativeIndependentNamePrefix!="" ? "of "+NegativeIndependentNamePrefix : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Match subpatterns {0}\n", 
                    NegativeIndependentNamePrefix!="" ? "of "+NegativeIndependentNamePrefix : "");

            sourceCode.AppendFrontFormat("{0}openTasks.Peek().myMatch({0}matchesList, {1}, negLevel);\n",
                NegativeIndependentNamePrefix, NegativeIndependentNamePrefix=="" ? "maxMatches - foundPartialMatches.Count" : "1");
        }

        public string NegativeIndependentNamePrefix;
    }

    /// <summary>
    /// Class representing "create new matches list for following matches" operation
    /// </summary>
    class NewMatchesListForFollowingMatches : SearchProgramOperation
    {
        public NewMatchesListForFollowingMatches(bool onlyIfMatchWasFound)
        {
            OnlyIfMatchWasFound = onlyIfMatchWasFound;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("NewMatchesListForFollowingMatches {0}\n",
                OnlyIfMatchWasFound ? "if match was found" : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (OnlyIfMatchWasFound)
            {
                sourceCode.AppendFront("if(matchesList.Count>0) {\n");
                sourceCode.Indent();
            }

            sourceCode.AppendFront("if(matchesList==foundPartialMatches) {\n");
            sourceCode.AppendFront("    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();\n");
            sourceCode.AppendFront("} else {\n");
            sourceCode.AppendFront("    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {\n");
            sourceCode.AppendFront("        foundPartialMatches.Add(match);\n");
            sourceCode.AppendFront("    }\n");
            sourceCode.AppendFront("    matchesList.Clear();\n");
            sourceCode.AppendFront("}\n");

            if (OnlyIfMatchWasFound)
            {
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
        }

        public bool OnlyIfMatchWasFound;
    }

    /// <summary>
    /// Available types of InitializeSubpatternMatching and the corresponding FinalizeSubpatternMatching operations
    /// </summary>
    enum InitializeFinalizeSubpatternMatchingType
    {
        Normal, // normal subpattern/alternative, whose tasks get removed when pattern gets matched
        Iteration, // iteration whose task stays on the tasks stack
        EndOfIteration // end of iteration, now the task gets removed from the tasks stack
    }

    /// <summary>
    /// Class representing "initialize subpattern matching" operation
    /// </summary>
    class InitializeSubpatternMatching : SearchProgramOperation
    {
        public InitializeSubpatternMatching(InitializeFinalizeSubpatternMatchingType type)
        {
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            switch (Type)
            {
            case InitializeFinalizeSubpatternMatchingType.Normal:
                builder.AppendFront("InitializeSubpatternMatching Normal\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.Iteration:
                builder.AppendFront("InitializeSubpatternMatching Iteration\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.EndOfIteration:
                builder.AppendFront("InitializeSubpatternMatching EndOfIteration\n");
                break;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type != InitializeFinalizeSubpatternMatchingType.Iteration)
            {
                sourceCode.AppendFront("openTasks.Pop();\n");
            }
            if (Type != InitializeFinalizeSubpatternMatchingType.EndOfIteration)
            {
                sourceCode.AppendFront("List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;\n");
                sourceCode.AppendFront("if(matchesList.Count!=0) throw new ApplicationException(); //debug assert\n");

                if (Type == InitializeFinalizeSubpatternMatchingType.Iteration)
                {
                    sourceCode.AppendFront("// if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object\n");
                    sourceCode.AppendFront("if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;\n");
                }
            }
        }

        public InitializeFinalizeSubpatternMatchingType Type;
    }

    /// <summary>
    /// Class representing "finalize subpattern matching" operation
    /// </summary>
    class FinalizeSubpatternMatching : SearchProgramOperation
    {
        public FinalizeSubpatternMatching(InitializeFinalizeSubpatternMatchingType type)
        {
            Type = type;
        }

        public override void Dump(SourceBuilder builder)
        {
            switch (Type)
            {
            case InitializeFinalizeSubpatternMatchingType.Normal:
                builder.AppendFront("FinalizeSubpatternMatching Normal\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.Iteration:
                builder.AppendFront("FinalizeSubpatternMatching Iteration\n");
                break;
            case InitializeFinalizeSubpatternMatchingType.EndOfIteration:
                builder.AppendFront("FinalizeSubpatternMatching EndOfIteration\n");
                break;
            }
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type != InitializeFinalizeSubpatternMatchingType.Iteration)
            {
                sourceCode.AppendFrontFormat("openTasks.Push(this);\n");
            }
        }

        InitializeFinalizeSubpatternMatchingType Type;
    }

    /// <summary>
    /// Class representing "initialize negative/independent matching" operation
    /// it opens an isomorphy space at the next negLevel, finalizeXXX will close it
    /// </summary>
    class InitializeNegativeIndependentMatching : SearchProgramOperation
    {
        public InitializeNegativeIndependentMatching(
            bool containsSubpatterns,
            string negativeIndependentNamePrefix,
            bool neverAboveMaxNegLevel)
        {
            SetupSubpatternMatching = containsSubpatterns;
            NegativeIndependentNamePrefix = negativeIndependentNamePrefix;
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("InitializeNegativeIndependentMatching {0}\n", 
                SetupSubpatternMatching ? "SetupSubpatternMatching" : "");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("++negLevel;\n");
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && "
                    + "negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {\n");
                sourceCode.Indent();
                sourceCode.AppendFront("graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, "
                    + "GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1]"
                    + ".fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1]"
                    + ".snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            if (SetupSubpatternMatching)
            {
                sourceCode.AppendFrontFormat("Stack<GRGEN_LGSP.LGSPSubpatternAction> {0}openTasks ="
                    + " new Stack<GRGEN_LGSP.LGSPSubpatternAction>();\n", NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("List<Stack<GRGEN_LIBGR.IMatch>> {0}foundPartialMatches ="
                    + " new List<Stack<GRGEN_LIBGR.IMatch>>();\n", NegativeIndependentNamePrefix);
                sourceCode.AppendFrontFormat("List<Stack<GRGEN_LIBGR.IMatch>> {0}matchesList ="
                    + " {0}foundPartialMatches;\n", NegativeIndependentNamePrefix);
            }
        }

        public bool SetupSubpatternMatching;
        public string NegativeIndependentNamePrefix;
        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing "finalize negative/independent matching" operation
    /// it closes an isomorphy space opened by initializeXXX, returning to the previous negLevel
    /// </summary>
    class FinalizeNegativeIndependentMatching : SearchProgramOperation
    {
        public FinalizeNegativeIndependentMatching(bool neverAboveMaxNegLevel)
        {
            NeverAboveMaxNegLevel = neverAboveMaxNegLevel;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("FinalizeNegativeIndependentMatching\n");
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            if (!NeverAboveMaxNegLevel)
            {
                sourceCode.AppendFront("if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {\n");
                sourceCode.Indent();
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel - "
                    + "(int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();\n");
                sourceCode.AppendFront("graph.atNegLevelMatchedElements[negLevel - "
                    + "(int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            sourceCode.AppendFront("--negLevel;\n");
        }

        public bool NeverAboveMaxNegLevel;
    }

    /// <summary>
    /// Class representing "push match for patternpath" operation
    /// push match to the match objects stack used for patternpath checking
    /// </summary>
    class PushMatchForPatternpath : SearchProgramOperation
    {
        public PushMatchForPatternpath(
            string rulePatternClassName,
            string patternGraphName,
            string matchOfNestingPattern)
        {
            RulePatternClassName = rulePatternClassName;
            PatternGraphName = patternGraphName;
            MatchOfNestingPattern = matchOfNestingPattern;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFrontFormat("PushMatchOfNestingPattern of {0} nestingPattern in:{1}\n",
                PatternGraphName, MatchOfNestingPattern);
        }

        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFrontFormat("// build match of {0} for patternpath checks\n", 
                PatternGraphName);
            sourceCode.AppendFrontFormat("if({0}==null) {0} = new {1}.{2}();\n", 
                NamesOfEntities.PatternpathMatch(PatternGraphName),
                RulePatternClassName,
                NamesOfEntities.MatchClassName(PatternGraphName));
            sourceCode.AppendFrontFormat("{0}._matchOfEnclosingPattern = {1};\n",
                NamesOfEntities.PatternpathMatch(PatternGraphName), MatchOfNestingPattern);
        }

        string RulePatternClassName;
        string PatternGraphName;
        string MatchOfNestingPattern;
    }
}
