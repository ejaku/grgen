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
        /// </summary>
        public abstract void Dump(SourceBuilder builder);

        /// <summary>
        /// emits c# code implementing search program operation into source builder
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
        /// returns whether operation is a nesting operation 
        /// containing other elements within some list inside
        /// which might be nesting operations themselves -> yes
        /// or is an elementary search program operation 
        /// or an operation which can only contain elementary operations -> no
        /// (check preset returns false, too, despite further check is nested within,
        /// more general: check failed operations are not to be regarded as nested, 
        /// only search iteration operations)
        /// </summary>
        public abstract bool IsNestingOperation();

        /// <summary>
        /// returns the nested list anchor
        /// null if list not created or IsNestingOperation == false
        /// </summary>
        public abstract SearchProgramOperation GetNestedOperationsList();

        /// <summary>
        /// returns operation enclosing this operation
        /// </summary>
        public SearchProgramOperation GetEnclosingOperation()
        {
            SearchProgramOperation potentiallyNestingOperation = this;
            SearchProgramOperation nestedOperation;

            // iterate list leftwards, leftmost list element is list anchor element,
            // which contains uplink to enclosing operation in it's previous member
            // step over nesting operations we're not nested in
            do
            {
                nestedOperation = potentiallyNestingOperation;
                potentiallyNestingOperation = nestedOperation.Previous;
            }
            while (!potentiallyNestingOperation.IsNestingOperation() 
                || potentiallyNestingOperation.GetNestedOperationsList()!=nestedOperation);

            return potentiallyNestingOperation;
        }
    }

    /// <summary>
    /// Search program list anchor element,
    /// containing first list element within inherited Next member
    /// Inherited to be able to access the first element via Next
    /// Previous points to enclosing search program operation
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

        /// <summary>
        /// Emits code for search program list
        /// </summary>
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

        public override bool IsNestingOperation()
        {
            return false; // starts list, but doesn't contain one
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }
    }

    /// <summary>
    /// Class representing a search program,
    /// which is a list of search program operations
    ///   some search program operations contain nested search program operations,
    ///   yielding a search program tree in fact
    /// representing/assembling a backtracking search program
    /// for finding a homomorphic mapping of the pattern graph within the host graph.
    /// is itself the outermost enclosing operation
    /// list forming concatenation field used for adding search subprograms
    /// </summary>
    class SearchProgram : SearchProgramOperation
    {
        public SearchProgram(string name, 
            string[] parameters, 
            bool[] parameterIsNode,
            bool setupSubpatternMatching,
            bool isSubpattern)
        {
            Name = name;

            Parameters = parameters;
            ParameterIsNode = parameterIsNode;

            IsSubprogram = parameters!=null;
            SetupSubpatternMatching = setupSubpatternMatching;
            IsSubpattern = isSubpattern;

            // check constraints regarding kind of search program
            Debug.Assert(IsSubprogram ? !SetupSubpatternMatching && !IsSubpattern : true);
            Debug.Assert(SetupSubpatternMatching ? !IsSubpattern && !IsSubprogram : true);
            Debug.Assert(IsSubpattern ? !SetupSubpatternMatching && !IsSubprogram : true);
        }

        /// <summary>
        /// Dumps search program followed by search subprograms
        /// </summary>
        public override void Dump(SourceBuilder builder)
        {
            // first dump local content
            builder.AppendFrontFormat("{0}Search {1}program {2}{3}",
                IsSubpattern ? "Subpattern " : "", IsSubprogram ? "sub" : "",
                Name, SetupSubpatternMatching ? " with subpattern matching setup" : "");
            // parameters
            if (Parameters != null)
            {
                for (int i = 0; i < Parameters.Length; ++i)
                {
                    string typeOfParameterVariableContainingCandidate =
                        ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                    string parameterVariableContainingCandidate =
                        NamesOfEntities.CandidateVariable(Parameters[i],
                            ParameterIsNode[i]);
                    builder.AppendFormat(", {0} {1}",
                        typeOfParameterVariableContainingCandidate,
                        parameterVariableContainingCandidate);
                }
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
        /// then the search program operations list in dept first walk over search program operations list
        /// then tail of matching function of the current search progran
        /// and finally continue in search program list by emitting following search program
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
#if RANDOM_LOOKUP_LIST_START
            sourceCode.AppendFront("private Random random = new Random(13795661);\n");
#endif

            if (IsSubprogram)
            {
#if PRODUCE_UNSAFE_MATCHERS
                sourceCode.AppendFront("unsafe ");
#endif
                sourceCode.AppendFront("public void " + Name + "(LGSPGraph graph, int maxMatches, IGraphElement[] parameters");
                for (int i = 0; i < Parameters.Length; ++i)
                {
                    string typeOfParameterVariableContainingCandidate =
                        ParameterIsNode[i] ? "LGSPNode" : "LGSPEdge";
                    string parameterVariableContainingCandidate =
                        NamesOfEntities.CandidateVariable(Parameters[i],
                            ParameterIsNode[i]);
                    sourceCode.AppendFormat(", {0} {1}",
                        typeOfParameterVariableContainingCandidate,
                        parameterVariableContainingCandidate);
                }
                sourceCode.Append(")\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

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
            else if (IsSubpattern)
            {
                sourceCode.Indent(); // we're within some namespace
                sourceCode.Indent(); // we're within some class

#if PRODUCE_UNSAFE_MATCHERS
                soureCode.AppendFront("unsafe ");
#endif
                sourceCode.AppendFront("public override void " + Name + "()\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();

                OperationsList.Emit(sourceCode);

                sourceCode.AppendFront("return;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
            }
            else // !IsSubprogram && !IsSubpattern
            {
                sourceCode.Indent(); // we're within some namespace
                sourceCode.Indent(); // we're within some class

#if PRODUCE_UNSAFE_MATCHERS
                soureCode.AppendFront("unsafe ");
#endif
                sourceCode.AppendFront("public LGSPMatches " + Name + "(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)\n");
                sourceCode.AppendFront("{\n");
                sourceCode.Indent();
                //            // [0] Matches matches = new Matches(this);
                //            sourceCode.AppendFront("LGSPMatches matches = new LGSPMatches(this);\n");
                sourceCode.AppendFront("matches.matches.Clear();\n");
                
                if (SetupSubpatternMatching)
                {
                    sourceCode.AppendFront("Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();\n");
                    sourceCode.AppendFront("List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();\n");
                }

                OperationsList.Emit(sourceCode);

                sourceCode.AppendFront("return matches;\n");
                sourceCode.Unindent();
                sourceCode.AppendFront("}\n");
                //            int compileGenMatcher = Environment.TickCount;
                //            long genSourceTicks = startGenMatcher.ElapsedTicks;
                
                // emit search subprograms
                if (Next != null)
                {
                    Next.Emit(sourceCode);
                }
            }
        }

        public override bool IsNestingOperation()
        {
            return true; // contains complete nested search program
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return OperationsList;
        }

        public string Name;
        public bool IsSubprogram;
        public bool SetupSubpatternMatching;
        public bool IsSubpattern;

        public string[] Parameters;
        public bool[] ParameterIsNode;

        public SearchProgramList OperationsList;
    }

    /// <summary>
    /// Base class for search program check operations
    /// contains list anchor for operations to execute when check failed
    /// </summary>
    abstract class CheckOperation : SearchProgramOperation
    {
        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

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

        /// <summary>
        /// Emits code for get type by iteration search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            // todo: randomisierte auswahl des typen wenn RANDOM_LOOKUP_LIST_START ?

            // emit type iteration loop header
            string typeOfVariableContainingType = NamesOfEntities.TypeOfVariableContainingType(IsNode);
            string variableContainingTypeForCandidate = NamesOfEntities.TypeForCandidateVariable(
                PatternElementName, IsNode);
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
            string variableContainingTypeIDForCandidate = NamesOfEntities.TypeIdForCandidateVariable(
                PatternElementName, IsNode);
            sourceCode.AppendFrontFormat("int {0} = {1}.TypeID;\n",
                variableContainingTypeIDForCandidate, variableContainingTypeForCandidate);

            NestedOperationsList.Emit(sourceCode);

            // close loop
            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public override bool IsNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedOperationsList()
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

        /// <summary>
        /// Emits code for get type by drawing search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            if(sourceCode.CommentSourceCode)
                sourceCode.AppendFrontFormat("// Lookup {0} \n", PatternElementName);

            string variableContainingTypeIDForCandidate = NamesOfEntities.TypeIdForCandidateVariable(
                PatternElementName, IsNode);
            sourceCode.AppendFrontFormat("int {0} = {1};\n",
                variableContainingTypeIDForCandidate, TypeID);
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
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

        /// <summary>
        /// Emits code for get candidate by iteration search program operation
        /// </summary>
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
                string variableContainingListHead = NamesOfEntities.CandidateIterationListHead(
                    PatternElementName, IsNode);
                sourceCode.AppendFormat("{0} {1}",
                    typeOfVariableContainingListHead, variableContainingListHead);
                // emit initialization of variable containing graph elements list head
                string graphMemberContainingListHeadByType =
                    IsNode ? "nodesByTypeHeads" : "edgesByTypeHeads";
                string variableContainingTypeIDForCandidate = NamesOfEntities.TypeIdForCandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFormat(" = graph.{0}[{1}], ",
                    graphMemberContainingListHeadByType, variableContainingTypeIDForCandidate);
                // emit declaration and initialization of variable containing candidates
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                    PatternElementName, IsNode);
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
                string variableContainingListHead = NamesOfEntities.CandidateIterationListHead(
                    PatternElementName, false);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingListHead, variableContainingListHead);
                // emit initialization of variable containing incident edges list head
                string variableContainingStartingPointNode = NamesOfEntities.CandidateVariable(
                    StartingPointNodeName, true);
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
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                    PatternElementName, false);
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

        public override bool IsNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedOperationsList()
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

        /// <summary>
        /// Emits code for get candidate by drawing search program operation
        /// </summary>
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
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                    PatternElementName, true);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with demanded node from variable containing edge
                string variableContainingStartingPointEdge = NamesOfEntities.CandidateVariable(
                    StartingPointEdgeName, false);
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
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFrontFormat("{0} {1}",
                    typeOfVariableContainingCandidate, variableContainingCandidate);
                // emit initialization with element from input parameters array
                sourceCode.AppendFormat(" = ({0}) parameters[{1}];\n",
                    typeOfVariableContainingCandidate, InputIndex);
            }
            else //Type==GetCandidateByDrawingType.FromSubpatternConnections
            {
                if(sourceCode.CommentSourceCode)
                    sourceCode.AppendFrontFormat("// PatPreset {0} \n", PatternElementName);

                // emit declaration of variable containing candidate node
                // and initialization with element from subpattern connections
                string typeOfVariableContainingCandidate =
                    IsNode ? "LGSPNode" : "LGSPEdge";
                string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                    PatternElementName, IsNode);
                sourceCode.AppendFrontFormat("{0} {1} = {2};\n",
                    typeOfVariableContainingCandidate, variableContainingCandidate, PatternElementName);
            }
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
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

        /// <summary>
        /// Emits code for check candidate for type search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                PatternElementName, IsNode);
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

        /// <summary>
        /// Emits code for check candidate failed search program operation
        /// </summary>
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

        /// <summary>
        /// Emits code for check candidate for connectedness search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check decision for is candidate connected to already found partial match
            // i.e. edge source/target equals node
            sourceCode.AppendFrontFormat("if({0}.{1} != {2}) ",
                NamesOfEntities.CandidateVariable(PatternEdgeName, false),
                CheckSource ? "source" : "target",
                NamesOfEntities.CandidateVariable(PatternNodeName, true));
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
    /// </summary>
    class CheckCandidateForIsomorphy : CheckCandidate
    {
        public CheckCandidateForIsomorphy(
            string patternElementName,
            List<string> namesOfPatternElementsToCheckAgainst,
            bool positive,
            bool isNode)
        {
            PatternElementName = patternElementName;
            NamesOfPatternElementsToCheckAgainst = namesOfPatternElementsToCheckAgainst;
            Positive = positive;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckCandidate ForIsomorphy ");
            builder.AppendFormat("on {0} positive:{1} node:{2} ",
                PatternElementName, Positive, IsNode);
            if (NamesOfPatternElementsToCheckAgainst != null)
            {
                builder.Append("against ");
                foreach (string name in NamesOfPatternElementsToCheckAgainst)
                {
                    builder.AppendFormat("{0} ", name);
                }
            }
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits code for check candidate for isomorphy search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            // open decision whether to fail
            sourceCode.AppendFront("if(");

            // fail if graph element contained within candidate was already matched
            // (to another pattern element)
            // as this would cause a homomorphic match
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                PatternElementName, IsNode);
            string isMatchedBit = Positive ? "isMatched" : "isMatchedNeg";
            sourceCode.AppendFormat("{0}.{1}", variableContainingCandidate, isMatchedBit);

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
                        NamesOfEntities.CandidateVariable(name, IsNode));
                }
                else
                {
                    bool first = true;
                    foreach (string name in NamesOfPatternElementsToCheckAgainst)
                    {
                        if (first)
                        {
                            sourceCode.AppendFrontFormat("&& ({0}=={1}\n", variableContainingCandidate,
                                NamesOfEntities.CandidateVariable(name, IsNode));
                            sourceCode.Indent();
                            first = false;
                        }
                        else
                        {
                            sourceCode.AppendFrontFormat("|| {0}=={1}\n", variableContainingCandidate,
                               NamesOfEntities.CandidateVariable(name, IsNode));
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
        public bool Positive; // positive|negative
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

        /// <summary>
        /// Emits code for check candidate for preset search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            // emit check whether candidate was preset (not null)
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                PatternElementName, IsNode);
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
            sourceCode.Append("graph, maxMatches, parameters");
            for (int i = 0; i < NeededElements.Length; ++i)
            {
                sourceCode.AppendFormat(", {0}", NamesOfEntities.CandidateVariable(
                    NeededElements[i], NeededElementIsNode[i]));
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

        /// <summary>
        /// Emits code for check partial match by negative pattern search program operation
        /// </summary>
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

        public override bool IsNestingOperation()
        {
            return true;
        }

        public override SearchProgramOperation GetNestedOperationsList()
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

        /// <summary>
        /// Emits code for check partial match by condition search program operation
        /// </summary>
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
                sourceCode.Append(NamesOfEntities.CandidateVariable(
                    NeededElements[i], NeededElementIsNode[i]));
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
    /// Base class for search program operations 
    /// to execute upon candidate checking succeded
    /// (of the pattern part under construction)
    /// </summary>
    abstract class AcceptIntoPartialMatch : SearchProgramOperation
    {
    }

    /// <summary>
    /// Class representing "write information to graph, to what pattern element 
    ///   is graph element mapped to, for isomorphy checking later on" operation
    /// </summary>
    class AcceptIntoPartialMatchWriteIsomorphy : AcceptIntoPartialMatch
    {
        public AcceptIntoPartialMatchWriteIsomorphy(
            string patternElementName,
            bool positive,
            bool isNode)
        {
            PatternElementName = patternElementName;
            Positive = positive;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("AcceptIntoPartialMatch WriteIsomorphy ");
            builder.AppendFormat("on {0} positive:{1} node:{2}\n",
                PatternElementName, Positive, IsNode);
        }

        /// <summary>
        /// Emits code for accept into partial match write isomorphy search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = 
                NamesOfEntities.CandidateVariable(PatternElementName, IsNode);
            string variableContainingBackupOfMappedMember =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, IsNode, Positive);
            string isMatchedBit = Positive ? "isMatched" : "isMatchedNeg";
            sourceCode.AppendFrontFormat("bool {0} = {1}.{2};\n",
                variableContainingBackupOfMappedMember, variableContainingCandidate, isMatchedBit);
            sourceCode.AppendFrontFormat("{0}.{1} = true;\n",
                variableContainingCandidate, isMatchedBit);
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

        public string PatternElementName;
        public bool Positive; // positive|negative
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Base class for search program operations
    /// undoing effects of candidate acceptance 
    /// when performing the backtracking step
    /// (of the pattern part under construction)
    /// </summary>
    abstract class WithdrawFromPartialMatch : SearchProgramOperation
    {
    }

    /// <summary>
    /// Class representing "remove information from graph, to what pattern element
    ///   is graph element mapped to, as not needed any more" operation
    /// </summary>
    class WithdrawFromPartialMatchRemoveIsomorphy : WithdrawFromPartialMatch
    {
        public WithdrawFromPartialMatchRemoveIsomorphy(
            string patternElementName,
            bool positive,
            bool isNode)
        {
            PatternElementName = patternElementName;
            Positive = positive;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("WithdrawFromPartialMatch RemoveIsomorphy ");
            builder.AppendFormat("on {0} positive:{1} node:{2}\n",
                PatternElementName, Positive, IsNode);
        }

        /// <summary>
        /// Emits code for withdraw from partial match remove isomorphy search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = 
                NamesOfEntities.CandidateVariable(PatternElementName, IsNode);
            string variableContainingBackupOfIsMatchedBit =
                NamesOfEntities.VariableWithBackupOfIsMatchedBit(PatternElementName, IsNode, Positive);
            string isMatchedBit = Positive ? "isMatched" : "isMatchedNeg";
            sourceCode.AppendFrontFormat("{0}.{1} = {2};\n",
                variableContainingCandidate, isMatchedBit, variableContainingBackupOfIsMatchedBit);
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

        public string PatternElementName;
        public bool Positive; // positive|negative
        public bool IsNode; // node|edge
    }

    /// <summary>
    /// Base class for search program operations
    /// to be executed when a partial match becomes a complete match
    /// (of the pattern part under construction)
    /// </summary>
    abstract class PartialMatchComplete : SearchProgramOperation
    {
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a partial positive match becomes a complete match
    /// </summary>
    class PartialMatchCompletePositive : PartialMatchComplete
    {
        public PartialMatchCompletePositive()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PartialMatchComplete Positive \n");

            if (MatchBuildingOperations != null)
            {
                builder.Indent();
                MatchBuildingOperations.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits code for partial match of positive pattern complete search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("LGSPMatch match = matches.matches.GetNewMatch();\n");

            // emit match building operations
            MatchBuildingOperations.Emit(sourceCode);

            sourceCode.AppendFront("matches.matches.CommitMatch();\n");
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

        public SearchProgramList MatchBuildingOperations;
    }

    /// <summary>
    /// Class yielding operations to be executed 
    /// when a partial negative match becomes a complete match
    /// </summary>
    class PartialMatchCompleteNegative : PartialMatchComplete
    {
        public PartialMatchCompleteNegative()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PartialMatchComplete Negative \n");
        }

        /// <summary>
        /// Emits code for partial match of negative pattern complete search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            // nothing to emit
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }
    }

    /// <summary>
    /// Class representing "partial match is complete, now build match object" operation
    /// </summary>
    class PartialMatchCompleteBuildMatchObject : PartialMatchComplete
    {
        public PartialMatchCompleteBuildMatchObject(
            string patternElementName,
            string matchIndex,
            bool isNode)
        {
            PatternElementName = patternElementName;
            MatchIndex = matchIndex;
            IsNode = isNode;
        }

        public override void Dump(SourceBuilder builder)
        {
            builder.AppendFront("PartialMatchComplete BuildMatchObject ");
            builder.AppendFormat("on {0} index:{1} node:{2}\n", 
                PatternElementName, MatchIndex, IsNode);
        }

        /// <summary>
        /// Emits code for partial match complete build match object search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            string variableContainingCandidate = NamesOfEntities.CandidateVariable(
                PatternElementName, IsNode);
            string matchObjectElementMember =
                IsNode ? "Nodes" : "Edges";
            sourceCode.AppendFrontFormat("match.{0}[{1}] = {2};\n",
                matchObjectElementMember, MatchIndex,
                variableContainingCandidate);
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

        public string PatternElementName;
        public string MatchIndex;
        public bool IsNode; // node|edge
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

        /// <summary>
        /// Emits code for adjust list heads search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            if (Type == AdjustListHeadsTypes.GraphElements)
            {
                sourceCode.AppendFrontFormat("graph.MoveHeadAfter({0});\n",
                    NamesOfEntities.CandidateVariable(PatternElementName, IsNode));
            }
            else //Type == AdjustListHeadsTypes.IncidentEdges
            {
                if (IsIncoming)
                {
                    sourceCode.AppendFrontFormat("{0}.MoveInHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName, true),
                        NamesOfEntities.CandidateVariable(PatternElementName, false));
                }
                else
                {
                    sourceCode.AppendFrontFormat("{0}.MoveOutHeadAfter({1});\n",
                        NamesOfEntities.CandidateVariable(StartingPointNodeName, true),
                        NamesOfEntities.CandidateVariable(PatternElementName, false));
                }
            }
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
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
    /// the maximum number of matches has been reached" operation
    /// listHeadAdjustment==false prevents listentrick
    /// </summary>
    class CheckContinueMatchingMaximumMatchesReached : CheckContinueMatching
    {
        public CheckContinueMatchingMaximumMatchesReached(bool listHeadAdjustment)
        {
            ListHeadAdjustment = listHeadAdjustment;
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching MaximumMatchesReached ");
            if (ListHeadAdjustment) {
                builder.Append("ListHeadAdjustment\n");
            } else {
                builder.Append("\n");
            }
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits code for check whether to continue matching for are maximum matches reached search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFront("if(maxMatches > 0 && matches.matches.Count >= maxMatches)\n");
            sourceCode.AppendFront("{\n");
            sourceCode.Indent();

            CheckFailedOperations.Emit(sourceCode);

            sourceCode.Unindent();
            sourceCode.AppendFront("}\n");
        }

        public bool ListHeadAdjustment;
    }

    /// <summary>
    /// Class representing check abort matching process operation
    /// which was determined at generation time to always succeed.
    /// Check of abort negative matching process always succeeds
    /// </summary>
    class CheckContinueMatchingFailed : CheckContinueMatching
    {
        public CheckContinueMatchingFailed()
        {
        }

        public override void Dump(SourceBuilder builder)
        {
            // first dump check
            builder.AppendFront("CheckContinueMatching Failed \n");
            // then operations for case check failed
            if (CheckFailedOperations != null)
            {
                builder.Indent();
                CheckFailedOperations.Dump(builder);
                builder.Unindent();
            }
        }

        /// <summary>
        /// Emits code for check whether to continue matching failed search program operation
        /// </summary>
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

        /// <summary>
        /// Emits code for continue search program operation
        /// </summary>
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

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
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

        /// <summary>
        /// Emits code for goto label search program operation
        /// </summary>
        public override void Emit(SourceBuilder sourceCode)
        {
            sourceCode.AppendFormat("{0}: ;\n", LabelName);
        }

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
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

        /// <summary>
        /// Emits code for randomize list heads search program operation
        /// </summary>
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
                string variableContainingTypeIDForCandidate = NamesOfEntities.TypeIdForCandidateVariable(
                        PatternElementName, IsNode);
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
                string variableContainingStartingPointNode = NamesOfEntities.CandidateVariable(
                    StartingPointNodeName, true);
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

        public override bool IsNestingOperation()
        {
            return false;
        }

        public override SearchProgramOperation GetNestedOperationsList()
        {
            return null;
        }

        public RandomizeListHeadsTypes Type;

        public string PatternElementName;

        public bool IsNode; // node|edge - only valid if GraphElements

        public string StartingPointNodeName; // only valid if IncidentEdges
        public bool IsIncoming; // only valid if IncidentEdges
    }
}
